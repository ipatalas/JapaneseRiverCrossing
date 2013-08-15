using System.Collections.Generic;
using System.Linq;
using JRC.Core.Exceptions;
using JRC.Core.Extensions;

namespace JRC.Core.Rules
{
	public class ThiefWithOthers : IRule
	{
		public string Description
		{
			get { return "The thief (striped shirt) cannot stay with any family member, if the Policeman is not there."; }
		}

		public bool Validate(Person[] peopleToMove, IReadOnlyList<Person> peopleToValidate)
		{
			if (peopleToValidate.Count > 1 && peopleToValidate.Has(PersonType.Thief) && !peopleToValidate.Has(PersonType.Policeman))
			{
				throw new PersonConflictException(this, peopleToValidate.First(), peopleToValidate.GetOne(PersonType.Thief));
			}
			
			return true;
		}
	}
}
