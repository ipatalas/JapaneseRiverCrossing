using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace JRC.Core.DataFormats
{
	class XmlDataFormat : DataFormat
	{
		internal override void SaveGame(IGameState game, Stream output)
		{
			var settings = new XmlWriterSettings();
			settings.Indent =true;
			settings.IndentChars = "\t";
			settings.NewLineChars = "\r\n";
			settings.Encoding = Encoding.ASCII;
			settings.OmitXmlDeclaration = true;

			using (var writer = XmlWriter.Create(output, settings))
			{
				writer.WriteStartElement("Game");
				writer.WriteAttributeString("raft", game.RaftPosition.ToString());

				WritePeople(writer, "Source", game.Source);
				WritePeople(writer, "Destination", game.Destination);

				writer.WriteEndElement();
			}
		}

		private void WritePeople(XmlWriter writer, string elementName, IReadOnlyList<Person> people)
		{
			writer.WriteStartElement(elementName);
			if (people.Count > 0)
			{
				writer.WriteString(string.Join(",", people.Select(p => p.Type.ToString())));
			}
			writer.WriteEndElement();
		}
	}
}
