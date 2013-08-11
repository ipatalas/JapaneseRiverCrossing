using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JRC.Core.DataFormats
{
	class StringDataFormat : DataFormat
	{
		internal override void SaveGame(IGameState game, Stream output)
		{
			using (var writer = new StreamWriter(output, Encoding.ASCII))
			{
				string sourceSide = string.Join(",", game.Source.Select(p => p.Type.ToString()));
				string destinationSide = string.Join(",", game.Destination.Select(p => p.Type.ToString()));
				string raftPosition = game.RaftPosition.ToString();

				writer.NewLine = "\n";
				writer.WriteLine(sourceSide);
				writer.WriteLine(destinationSide);
				writer.Write(raftPosition);
			}
		}
	}
}
