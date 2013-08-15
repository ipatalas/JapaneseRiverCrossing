using System;
using NUnit.Framework;
using FluentAssertions;
using System.Linq;
using JRC.Core.Extensions;
using JRC.Core.Exceptions;
using System.Collections.Generic;
using JRC.Core.Rules;
using FakeItEasy;
using System.IO;

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
		public void Game_MovePerson_DestinationContainsMovedPerson()
		{
			var game = CreateGameWithSpecificRules();
			var father = game.Source.GetOne(PersonType.Father);

			game.MoveToDestination(father);

			game.Source.Should().HaveCount(7);
			game.Destination.Should().ContainSingle(p => p.Type == PersonType.Father);
		}

		[Test]
		public void Game_MovePersonToDestination_RaftPositionChanged()
		{
			var game = CreateGameWithSpecificRules();
			var father = game.Source.GetOne(PersonType.Father);

			game.MoveToDestination(father);

			game.RaftPosition.Should().Be(RaftPosition.Destination);
		}

		[Test]
		public void Game_MovePersonToSource_RaftPositionChanged()
		{
			var game = CreateGameWithSpecificRules();
			var father = game.Source.GetOne(PersonType.Father);

			game.MoveToDestination(father);
			game.MoveToSource(father);

			game.RaftPosition.Should().Be(RaftPosition.Source);
		}

		[Test]
		public void Game_MovePerson_SingleRuleIsCalledTwice()
		{
			var rule = A.Fake<IRule>();
			var game = CreateGameWithSpecificRules(rule);
			var father = game.Source.GetOne(PersonType.Father);
			
			game.MoveToDestination(father);

			A.CallTo(rule).Where(x => x.Method.Name == "Validate").MustHaveHappened(Repeated.Exactly.Twice);
		}

		[Test]
		public void Game_MoveFather_RuleGetsCalledWithProperArguments()
		{
			var rule = A.Fake<IRule>();
			var game = CreateGameWithSpecificRules(rule);
			var father = game.Source.GetOne(PersonType.Father);

			var expectedPeopleToValidate = new List<Person>(game.Source);
			expectedPeopleToValidate.Remove(father);

			using (var scope = Fake.CreateScope())
			{
				// act
				game.MoveToDestination(father);

				using (scope.OrderedAssertions())
				{
					A.CallTo(() => rule.Validate(
						A<Person[]>.That.IsSameSequenceAs(new[] { father }),
						A<List<Person>>.That.IsSameSequenceAs(expectedPeopleToValidate)
					)).MustHaveHappened(Repeated.Exactly.Once);

					A.CallTo(() => rule.Validate(
						A<Person[]>.That.IsSameSequenceAs(new[] { father }),
						A<List<Person>>.That.IsSameSequenceAs(new[] { father })
					)).MustHaveHappened(Repeated.Exactly.Once);
				}
			}
		}

		[Test]
		public void Game_MovePersonAndBreakTheRule_ThrowsException()
		{
			var ex = new Exception();

			var rule = A.Fake<IRule>();
			A.CallTo(() => rule.Validate(null, null)).WithAnyArguments().Throws(ex);

			var game = CreateGameWithSpecificRules(rule);
			var father = game.Source.GetOne(PersonType.Father);


			Action act = () => game.MoveToDestination(father);


			act.ShouldThrow<Exception>().And.Should().Be(ex);
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
		public void Game_MovePeopleWhenThereIsNoRaft_ThrowsRaftNotAvailableException()
		{
			var game = CreateDefaultGame();
			var people = game.Source.GetTwo(PersonType.Policeman, PersonType.Thief);
			game.MoveToDestination(people);

			Action act = () => game.MoveToDestination(people);

			act.ShouldThrow<RaftNotAvailableException>();
		}

		[Test]
		public void Game_MovePeopleBackFromDestination_SourceContainThesePeopleAgain()
		{
			var game = CreateDefaultGame();
			var people = game.Source.GetTwo(PersonType.Policeman, PersonType.Thief);
			game.MoveToDestination(people);

			game.MoveToSource(people);

			game.Destination.Count.Should().Be(0);
			game.Source.Should().Contain(people);
		}

		// Save game to a file (integration tests)

		[Test]
		public void Game_SaveGame_DataFormatterCalledOnlyOnce()
		{
			var game = CreateDefaultGame();
			var format = A.Fake<DataFormat>();
			var ms = new MemoryStream();

			game.Save(ms, format);

			A.CallTo(() => format.SaveGame(A<Game>.That.IsEqualTo(game), A<Stream>.That.IsEqualTo(ms))).MustHaveHappened(Repeated.Exactly.Once);
		}

		private static Game CreateDefaultGame()
		{
			var rules = new IRule[] { 
				new _2PeopleLimit(), 
				new DriverRequired(), 
				new MotherAndSons(),
				new ThiefWithOthers(),
				new FatherAndDaughters()
			};

			return new Game(rules);
		}

		private static Game CreateGameWithSpecificRules(params IRule[] rules)
		{
			return new Game(rules);
		}
	}
}
