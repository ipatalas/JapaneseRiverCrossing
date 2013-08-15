using System;
using JRC.Core.Rules;
using NUnit.Framework;
using FluentAssertions;
using JRC.Core.Exceptions;

namespace JRC.Core.UnitTests.Rules
{
	[TestFixture]
	public class _2PeopleLimitTests
	{
		Person dummy = new Person(PersonType.Father);

		[Test]
		public void Validate_GivenOnePerson_ValidatesCorrectly()
		{
			var rule = new _2PeopleLimit();

			bool result = rule.Validate(new Person[] { dummy }, new Person[] { });

			result.Should().BeTrue();
		}

		[Test]
		public void Validate_Given2Persons_ValidatesCorrectly()
		{
			var rule = new _2PeopleLimit();

			bool result = rule.Validate(new Person[] { dummy, dummy }, new Person[] { });

			result.Should().BeTrue();
		}

		[Test]
		public void Validate_GivenMorePeople_ThrowsException()
		{
			var rule = new _2PeopleLimit();

			Action act = () => rule.Validate(new Person[] { dummy, dummy, dummy }, new Person[] { });

			act.ShouldThrow<RuleBrokenException>().And.Rule.Should().BeOfType<_2PeopleLimit>();
		}
	}
}
