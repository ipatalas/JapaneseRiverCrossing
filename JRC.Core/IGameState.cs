using System;
using System.Collections.Generic;
namespace JRC.Core
{
	interface IGameState
	{
		IReadOnlyList<Person> Source { get; }
		IReadOnlyList<Person> Destination { get; }
		RaftPosition RaftPosition { get; }
	}
}
