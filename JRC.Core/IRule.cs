using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JRC.Core
{
	public interface IRule
	{
		string Description { get; }

		bool Validate(Person[] peopleToMove, IReadOnlyList<Person> peopleToValidate);
	}
}
