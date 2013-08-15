using System;
using JRC.Core.Rules;
using NUnit.Framework;
using FluentAssertions;
using JRC.Core.Exceptions;

namespace JRC.Core.UnitTests.Rules
{
	[TestFixture]
	public class FatherAndDaughtersTests
	{
		Person mother = new Person(PersonType.Mother);
		Person father = new Person(PersonType.Father);
		Person daughter = new Person(PersonType.Daughter);

		[Test]
		public void Validate_OnlyFatherAndDaughter_ThrowsException()
		{
			var rule = new FatherAndDaughters();

			Action act = () => rule.Validate(new Person[] { }, new Person[] { father, daughter });

			act.ShouldThrow<RuleBrokenException>().And.Rule.Should().BeOfType<FatherAndDaughters>();
		}

		[Test]
		public void Validate_FatherAlone_ReturnsTrue()
		{
			var rule = new FatherAndDaughters();

			bool result = rule.Validate(new Person[] { }, new Person[] { father });

			result.Should().BeTrue();
		}

		[Test]
		public void Validate_FatherDaughterAndMother_ReturnsTrue()
		{
			var rule = new FatherAndDaughters();

			bool result = rule.Validate(new Person[] { }, new Person[] { father, daughter, mother });

			result.Should().BeTrue();
		}
	}
}
