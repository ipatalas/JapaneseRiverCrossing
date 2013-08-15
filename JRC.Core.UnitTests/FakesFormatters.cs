using System.Collections.Generic;
using System.Linq;
using FakeItEasy;

namespace JRC.Core.UnitTests
{
	public class PersonArrayArgumentFormatter : ArgumentValueFormatter<Person[]>
	{
		protected override string GetStringValue(Person[] argumentValue)
		{
			return string.Join(", ", argumentValue.Select(p => p.Type));
		}
	}

	public class PersonListArgumentFormatter : ArgumentValueFormatter<List<Person>>
	{
		protected override string GetStringValue(List<Person> argumentValue)
		{
			return string.Join(", ", argumentValue.Select(p => p.Type));
		}
	}
}
