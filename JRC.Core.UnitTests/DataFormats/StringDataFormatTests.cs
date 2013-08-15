using System.IO;
using FluentAssertions;
using JRC.Core.DataFormats;
using NUnit.Framework;

namespace JRC.Core.UnitTests.DataFormats
{
	[TestFixture]
	public class StringDataFormatTests : DataFormatTestsBase
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
			var formatter = new StringDataFormat();

			formatter.SaveGame(game, ms);
			
			ms.ToASCIIString().Should().Be("Father\n\nSource");
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
			var formatter = new StringDataFormat();
			
			formatter.SaveGame(game, ms);

			ms.ToASCIIString().Should().Be("\n\nDestination");
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
			var formatter = new StringDataFormat();

			formatter.SaveGame(game, ms);

			ms.ToASCIIString().Should().Be("Father,Policeman\nMother,Daughter\nDestination");
		}

	
	}

	
}
