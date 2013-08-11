using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JRC.Core.Extensions
{
	public static class IReadOnlyListExtensions
	{
		public static Person GetOne(this IReadOnlyList<Person> source, PersonType type)
		{
			return source.FirstOrDefault(p => p.Type == type);
		}

		public static bool Has(this IReadOnlyList<Person> source, PersonType type)
		{
			return source.Any(p => p.Type == type);
		}
	}
}
