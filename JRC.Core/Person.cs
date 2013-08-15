using System.Diagnostics;

namespace JRC.Core
{
	[DebuggerDisplay("{Type}")]
	public class Person
	{
		public PersonType Type { get; private set; }

		public Person(PersonType type)
		{
			Type = type;
		}
	}
}
