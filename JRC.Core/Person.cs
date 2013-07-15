
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
