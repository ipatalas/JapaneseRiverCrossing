using System.IO;
using System.Linq;
using System.Text;

namespace JRC.Core.DataFormats
{
	class StringDataFormat : DataFormat
	{
		private const string Separator = ",";
		private const string NewLine = "\n";

		internal override void SaveGame(IGameState game, Stream output)
		{
			using (var writer = new StreamWriter(output, Encoding.ASCII))
			{
				string sourceSide = string.Join(Separator, game.Source.Select(p => p.Type.ToString()));
				string destinationSide = string.Join(Separator, game.Destination.Select(p => p.Type.ToString()));
				string raftPosition = game.RaftPosition.ToString();

				writer.NewLine = NewLine;
				writer.WriteLine(sourceSide);
				writer.WriteLine(destinationSide);
				writer.Write(raftPosition);
			}
		}
	}
}
