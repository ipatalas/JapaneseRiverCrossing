using System.Collections.Generic;
using JRC.Core.Exceptions;

namespace JRC.Core.Rules
{
	public class MotherAndSons : IRule
	{
		public string Description
		{
			get { return "The Mother cannot stay with any of the sons, without their Father's presence."; }
		}

		public bool Validate(Person[] peopleToMove, IReadOnlyList<Person> peopleToValidate)
		{
			if (peopleToValidate.Has(PersonType.Mother) && peopleToValidate.Has(PersonType.Son) && !peopleToValidate.Has(PersonType.Father))
			{
				throw new PersonConflictException(this, peopleToValidate.GetOne(PersonType.Mother), peopleToValidate.GetOne(PersonType.Son));
			}

			return true;
		}
	}
}
