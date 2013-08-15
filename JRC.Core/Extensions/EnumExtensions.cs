using System.Linq;

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
