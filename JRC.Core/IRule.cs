using System.Collections.Generic;

namespace JRC.Core
{
	public interface IRule
	{
		string Description { get; }

		bool Validate(Person[] peopleToMove, IReadOnlyList<Person> peopleToValidate);
	}
}
