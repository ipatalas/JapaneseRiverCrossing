using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FakeItEasy;

namespace JRC.Core.UnitTests.DataFormats
{
	public abstract class DataFormatTestsBase
	{
		internal IGameState CreateGameState(IEnumerable<PersonType> source, IEnumerable<PersonType> destination, RaftPosition position)
		{
			Func<IEnumerable<PersonType>, List<Person>> converter = (src) => src.ToList().ConvertAll(t => new Person(t));

			var game = A.Fake<IGameState>();
			A.CallTo(() => game.Source).Returns(converter(source));
			A.CallTo(() => game.Destination).Returns(converter(destination));
			A.CallTo(() => game.RaftPosition).Returns(position);

			return game;
		}
	}

	public static class MemoryStreamExtensions
	{
		public static string ToASCIIString(this MemoryStream stream)
		{
			return Encoding.ASCII.GetString(stream.ToArray());
		}
	}
}
