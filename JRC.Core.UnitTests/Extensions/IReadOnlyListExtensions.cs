using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JRC.Core.Extensions
{
	public static class IReadOnlyListExtensions
	{
		public static Person[] GetTwo(this IReadOnlyList<Person> source, PersonType type1, PersonType type2)
		{
			return source.Where(p => p.Type.IsOneOf(type1, type2)).ToArray();
		}
	}
}
