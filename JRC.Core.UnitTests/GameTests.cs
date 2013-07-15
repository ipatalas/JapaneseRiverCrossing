using System;
using NUnit.Framework;
using FluentAssertions;
using JRC.Core;
using System.Diagnostics;
using System.Reflection;
using System.Linq;
using JRC.Core.Extensions;
using JRC.Core.Exceptions;
using System.Collections.Generic;

namespace JRC.Core.UnitTests
{
	[TestFixture]
	public class GameTests
	{
		[Test]
		public void Game_CreateInitialGame_ContainsProperPersons()
		{
			var game = CreateDefaultGame();

			game.Source.Should().HaveCount(8);

			game.Source.Count(p => p.Type == PersonType.Daughter).Should().Be(2);
			game.Source.Count(p => p.Type == PersonType.Son).Should().Be(2);

			game.Source.Should().ContainSingle(p => p.Type == PersonType.Father)
							.And.ContainSingle(p => p.Type == PersonType.Mother)
							.And.ContainSingle(p => p.Type == PersonType.Policeman)
							.And.ContainSingle(p => p.Type == PersonType.Thief);
		}

		[Test]
		public void Game_MoveValidPerson_DestinationContainsMovedPerson()
		{
			var game = CreateDefaultGame();
			var father = game.Source.Single(p => p.Type == PersonType.Father);

			game.MoveToDestination(father);

			game.Source.Should().HaveCount(7);
			game.Destination.Should().ContainSingle(p => p.Type == PersonType.Father);
		}

		[Test]
		public void Game_MoveNonExistingPerson_ThrowsInvalidOperationException()
		{
			var game = CreateDefaultGame();
			var person = new Person(PersonType.Father);

			Action act = () => game.MoveToDestination(person);

			act.ShouldThrow<InvalidOperationException>();
		}

		[Test]
		public void Game_MoveInvalidPerson_ThrowsRuleBrokenExceptionWithDriverRequired()
		{
			var game = CreateDefaultGame();
			var son = game.Source.First(p => p.Type == PersonType.Son);

			Action act = () => game.MoveToDestination(son);

			act.ShouldThrow<RuleBrokenException>()
				.And.Rule.Should().BeOfType<Rules.DriverRequired>();
		}

		[Test]
		public void Game_MovePolicemanAndThief_DestinationContainsThesePeople()
		{
			var game = CreateDefaultGame();
			var p1 = game.Source.Single(p => p.Type == PersonType.Policeman);
			var p2 = game.Source.Single(p => p.Type == PersonType.Thief);

			game.MoveToDestination(p1, p2);

			game.Source.Should().HaveCount(6);
			game.Destination.Should().OnlyContain(p => p.IsOneOf(p1, p2));
		}

		[Test]
		public void Game_Move2Sons_ThrowsRuleBrokenWithDriverRequired()
		{
			var game = CreateDefaultGame();
			var sons = game.Source.Where(p => p.Type == PersonType.Son).ToList();

			Action act = () => game.MoveToDestination(sons[0], sons[1]);

			act.ShouldThrow<RuleBrokenException>()
				.And.Rule.Should().BeOfType<Rules.DriverRequired>();
		}

		[Test]
		public void Game_MoveFatherAndSon_ThrowsConflictExceptionForRemainingSonAndMother()
		{
			var game = CreateDefaultGame();
			var son = game.Source.Get(PersonType.Son);
			var father = game.Source.Get(PersonType.Father);

			Action act = () => game.MoveToDestination(father, son);

			act.ShouldThrow<PersonConflictException>()
				.Where(e => e.Rule is Rules.MotherAndSons)
				.Where(e => e.Person1.Type == PersonType.Mother && e.Person2.Type == PersonType.Son);
		}

		[Test]
		public void Game_MovePeopleWhenThereIsNoRaft_ThrowsRaftNotAvailableException()
		{
			var game = CreateDefaultGame();
			var people = game.Source.Get2(PersonType.Policeman, PersonType.Thief);
			game.MoveToDestination(people);

			Action act = () => game.MoveToDestination(people);

			act.ShouldThrow<RaftNotAvailableException>();
		}

		[Test]
		public void Game_MovePeopleBackFromDestination_SourceContainThesePeopleAgain()
		{
			var game = CreateDefaultGame();
			var people = game.Source.Get2(PersonType.Policeman, PersonType.Thief);
			game.MoveToDestination(people);

			game.MoveToSource(people);

			game.Destination.Count.Should().Be(0);
			game.Source.Should().Contain(people);
		}

		[Test]
		public void Game_MovePeopleToCauseConflictOnDestination_ThrowsRuleBrokenException()
		{
			var game = CreateDefaultGame();
			var people = game.Source.Get2(PersonType.Policeman, PersonType.Thief);
			game.MoveToDestination(people);
			game.MoveToSource(people[0]);

			Action act = () => game.MoveToDestination(game.Source.Get2(PersonType.Father, PersonType.Mother));

			act.ShouldThrow<RuleBrokenException>()
				.And.Rule.Should().BeOfType<Rules.ThiefWithOthers>();
		}

		private static Game CreateDefaultGame()
		{
			var rules = new IRule[] { 
				new Rules._2PeopleLimit(), 
				new Rules.DriverRequired(), 
				new Rules.MotherAndSons(),
				new Rules.ThiefWithOthers()
			};

			return new Game(rules);
		}

		//private static Game CreateGameWithRules(params IRule[] rules)
		//{
		//	return new Game(rules);
		//}
	}
}
