using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JRC.Core.Extensions
{
	public static class EnumExtensions
	{
		public static bool IsOneOf<T>(this T item, params T[] values)
		{
			return values.Contains(item);
		}
	}
}
