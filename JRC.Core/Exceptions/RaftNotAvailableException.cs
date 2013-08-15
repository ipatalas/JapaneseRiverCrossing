using System;

namespace JRC.Core.Exceptions
{
	public class RaftNotAvailableException : InvalidOperationException
	{
		public RaftNotAvailableException() : base("Raft not available on this side of the river")
		{

		}
	}
}
