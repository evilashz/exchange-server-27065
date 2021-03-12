using System;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008D1 RID: 2257
	internal sealed class SettingsSerializer : XmlSerializer1
	{
		// Token: 0x0600307D RID: 12413 RVA: 0x00072D2F File Offset: 0x00070F2F
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("Settings", "HMSETTINGS:");
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x00072D41 File Offset: 0x00070F41
		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((XmlSerializationWriterSettings)writer).Write33_Settings(objectToSerialize);
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x00072D4F File Offset: 0x00070F4F
		protected override object Deserialize(XmlSerializationReader reader)
		{
			return ((XmlSerializationReaderSettings)reader).Read33_Settings();
		}
	}
}
