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
	public class DriverRequiredTests
	{
		Person driver = new Person(PersonType.Father);
		Person nonDriver = new Person(PersonType.Son);

		[Test]
		public void Rule_Message()
		{
			var rule = new DriverRequired();

			rule.Description.Should().Be("Only the Father, the Mother and the Policeman know how to operate the raft");
		}

		[Test]
		public void Validate_GivenADriver_ValidatesCorrectly()
		{
			var rule = new DriverRequired();

			bool result = rule.Validate(new Person[] { driver }, new Person[] { });

			result.Should().BeTrue();
		}

		[Test]
		public void Validate_GivenANonDriver_ThrowsException()
		{
			var rule = new DriverRequired();

			Action act = () => rule.Validate(new Person[] { nonDriver }, new Person[] { });

			act.ShouldThrow<RuleBrokenException>().And.Rule.Should().BeOfType<DriverRequired>();
		}
	}
}
