using System;

namespace JRC.Core.Exceptions
{
	public class RuleBrokenException : InvalidOperationException
	{
		public IRule Rule { get; private set; }

		public RuleBrokenException(IRule rule)
		{
			Rule = rule;
		}
	}
}
