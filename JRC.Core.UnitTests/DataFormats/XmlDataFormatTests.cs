using System.IO;
using FluentAssertions;
using JRC.Core.DataFormats;
using NUnit.Framework;

namespace JRC.Core.UnitTests.DataFormats
{
	[TestFixture]
	public class XmlDataFormatTests : DataFormatTestsBase
	{
		[Test]
		public void SaveGame_WithOnlyFatherAtSourceSide_SavedStringContainsFather()
		{
			var game = CreateGameState(
				new PersonType[] { PersonType.Father },
				new PersonType[] { },
				RaftPosition.Source
			);
			var ms = new MemoryStream();
			var formatter = new XmlDataFormat();

			formatter.SaveGame(game, ms);
			
			ms.ToASCIIString().Should().Be(
@"<Game raft=""Source"">
	<Source>Father</Source>
	<Destination />
</Game>");
		}

		[Test]
		public void SaveGame_WithNoPeopleAndRaftPositionAtDestination_PositionCorrectlySaved()
		{
			var game = CreateGameState(
				new PersonType[] { },
				new PersonType[] { },
				RaftPosition.Destination
			);
			var ms = new MemoryStream();
			var formatter = new XmlDataFormat();
			
			formatter.SaveGame(game, ms);

			ms.ToASCIIString().Should().Be(
@"<Game raft=""Destination"">
	<Source />
	<Destination />
</Game>");
		}

		[Test]
		public void SaveGame_WithSomePeopleOnBothSides_SavedStringContainsThemAll()
		{
			var game = CreateGameState(
				new PersonType[] { PersonType.Father, PersonType.Policeman },
				new PersonType[] { PersonType.Mother, PersonType.Daughter },
				RaftPosition.Destination
			);
			var ms = new MemoryStream();
			var formatter = new XmlDataFormat();

			formatter.SaveGame(game, ms);

			ms.ToASCIIString().Should().Be(
@"<Game raft=""Destination"">
	<Source>Father,Policeman</Source>
	<Destination>Mother,Daughter</Destination>
</Game>");
		}
	}
}
