using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JRC.Core.Exceptions
{
	public class RaftNotAvailableException : InvalidOperationException
	{
		public RaftNotAvailableException() : base("Raft not available on this side of the river")
		{

		}
	}
}
