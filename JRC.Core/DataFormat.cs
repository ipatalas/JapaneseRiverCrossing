using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JRC.Core
{
	public abstract class DataFormat
	{
		public static DataFormat String = new JRC.Core.DataFormats.StringDataFormat();
		public static DataFormat Xml = new JRC.Core.DataFormats.XmlDataFormat();

		abstract internal void SaveGame(IGameState game, Stream output);
	}
}
