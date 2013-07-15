using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JRC.Core.Exceptions;

namespace JRC.Core.Rules
{
	public class _2PeopleLimit : IRule
	{
		public string Description
		{
			get { return "Only 2 people can be moved at a time."; }
		}

		public bool Validate(Person[] peopleToMove, IReadOnlyList<Person> peopleToValidate)
		{
			if (peopleToMove.Length > 2)
			{
				throw new RuleBrokenException(this);
			}

			return true;
		}
	}
}
