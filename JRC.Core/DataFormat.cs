using System.IO;
using JRC.Core.DataFormats;

namespace JRC.Core
{
	public abstract class DataFormat
	{
		public static DataFormat String = new StringDataFormat();
		public static DataFormat Xml = new XmlDataFormat();

		abstract internal void SaveGame(IGameState game, Stream output);
	}
}
