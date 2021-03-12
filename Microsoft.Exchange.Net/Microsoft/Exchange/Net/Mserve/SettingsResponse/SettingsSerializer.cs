using System;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008FF RID: 2303
	internal sealed class SettingsSerializer : XmlSerializer1
	{
		// Token: 0x060031E3 RID: 12771 RVA: 0x0007B0B2 File Offset: 0x000792B2
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("Settings", "HMSETTINGS:");
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x0007B0C4 File Offset: 0x000792C4
		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((XmlSerializationWriterSettings)writer).Write43_Settings(objectToSerialize);
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x0007B0D2 File Offset: 0x000792D2
		protected override object Deserialize(XmlSerializationReader reader)
		{
			return ((XmlSerializationReaderSettings)reader).Read43_Settings();
		}
	}
}
