using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JRC.Core.Rules;
using NUnit.Framework;
using FluentAssertions;
using JRC.Core.Exceptions;

namespace JRC.Core.UnitTests.Rules
{
	[TestFixture]
	public class MotherAndSonsTests
	{
		Person mother = new Person(PersonType.Mother);
		Person father = new Person(PersonType.Father);
		Person son = new Person(PersonType.Son);

		[Test]
		public void Validate_GivenOnlyMotherAndSon_ThrowsException()
		{
			var rule = new MotherAndSons();

			Action act = () => rule.Validate(new Person[] { }, new Person[] { mother, son });

			act.ShouldThrow<RuleBrokenException>().And.Rule.Should().BeOfType<MotherAndSons>();
		}

		[Test]
		public void Validate_GivenOnlyMother_ValidatesCorrectly()
		{
			var rule = new MotherAndSons();

			bool result = rule.Validate(new Person[] { }, new Person[] { mother });

			result.Should().BeTrue();
		}

		[Test]
		public void Validate_GivenMotherSonAndFather_ValidatesCorrectly()
		{
			var rule = new MotherAndSons();

			bool result = rule.Validate(new Person[] { }, new Person[] { mother, son, father });

			result.Should().BeTrue();
		}
	}
}
