using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JRC.Core.Extensions
{
	public static class IReadOnlyListExtensions
	{
		public static Person Get(this IReadOnlyList<Person> source, PersonType type)
		{
			return source.FirstOrDefault(p => p.Type == type);
		}

		public static Person[] Get2(this IReadOnlyList<Person> source, PersonType type1, PersonType type2)
		{
			return source.Where(p => p.Type.IsOneOf(type1, type2)).ToArray();
		}

		public static bool Has(this IReadOnlyList<Person> source, PersonType type)
		{
			return source.Any(p => p.Type == type);
		}
	}
}
