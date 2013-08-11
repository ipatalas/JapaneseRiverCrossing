using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JRC.Core.Exceptions;
using JRC.Core.Extensions;

namespace JRC.Core.Rules
{
	public class FatherAndDaughters : IRule
	{
		public string Description
		{
			get { return "The Father cannot stay with any of the daughters, without their Mother's presence."; }
		}

		public bool Validate(Person[] peopleToMove, IReadOnlyList<Person> peopleToValidate)
		{
			if (peopleToValidate.Has(PersonType.Father) && peopleToValidate.Has(PersonType.Daughter) && !peopleToValidate.Has(PersonType.Mother))
			{
				throw new PersonConflictException(this, peopleToValidate.GetOne(PersonType.Father), peopleToValidate.GetOne(PersonType.Daughter));
			}

			return true;
		}
	}
}
