using System;
using JRC.Core.Rules;
using NUnit.Framework;
using FluentAssertions;
using JRC.Core.Exceptions;

namespace JRC.Core.UnitTests.Rules
{
	[TestFixture]
	public class ThiefWithOthersTests
	{
		readonly Person thief = new Person(PersonType.Thief);
		readonly Person policeman = new Person(PersonType.Policeman);
		readonly Person son = new Person(PersonType.Son);

		[Test]
		public void Validate_GivenOnlyThief_ValidatesCorrectly()
		{
			var rule = new ThiefWithOthers();

			bool result = rule.Validate(new Person[] { }, new Person[] { thief });

			result.Should().BeTrue();
		}

		[Test]
		public void Validate_GivenThiefPolicemanAndSon_ValidatesCorrectly()
		{
			var rule = new ThiefWithOthers();

			bool result = rule.Validate(new Person[] { }, new Person[] { thief, policeman, son });

			result.Should().BeTrue();
		}

		[Test]
		public void Validate_GivenThiefAndSon_ThrowsException()
		{
			var rule = new ThiefWithOthers();

			Action act = () => rule.Validate(new Person[] { }, new Person[] { thief, son });

			act.ShouldThrow<RuleBrokenException>().And.Rule.Should().BeOfType<ThiefWithOthers>();
		}
	}
}
