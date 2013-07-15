using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JRC.Core.Exceptions;
using JRC.Core.Extensions;

namespace JRC.Core.Rules
{
	public class DriverRequired : IRule
	{
		private readonly PersonType[] DriverTypes; 

		public string Description
		{
			get { return "Only the Father, the Mother and the Policeman know how to operate the raft"; }
		}

		public DriverRequired()
		{
			DriverTypes = new[] { PersonType.Father, PersonType.Mother, PersonType.Policeman };
		}

		public bool Validate(Person[] peopleToMove, IReadOnlyList<Person> peopleToValidate)
		{
			if (!peopleToMove.Any(p => p.Type.IsOneOf(DriverTypes)))
			{
				throw new RuleBrokenException(this);
			}

			return true;
		}
	}
}
