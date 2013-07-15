using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JRC.Core.Exceptions
{
	public class PersonConflictException : RuleBrokenException
	{
		public readonly Person Person1;
		public readonly Person Person2;

		public PersonConflictException(IRule rule, Person person1, Person person2) : base(rule)
		{
			Person1 = person1;
			Person2 = person2;
		}
	}
}
