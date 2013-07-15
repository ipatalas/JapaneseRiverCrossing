using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
