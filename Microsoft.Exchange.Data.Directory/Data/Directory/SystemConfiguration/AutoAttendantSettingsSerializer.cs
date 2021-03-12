using System;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005F8 RID: 1528
	internal sealed class AutoAttendantSettingsSerializer : XmlSerializer1
	{
		// Token: 0x060048B1 RID: 18609 RVA: 0x0010D429 File Offset: 0x0010B629
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("AutoAttendantSettings", string.Empty);
		}

		// Token: 0x060048B2 RID: 18610 RVA: 0x0010D43B File Offset: 0x0010B63B
		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((XmlSerializationWriterAutoAttendantSettings)writer).Write5_AutoAttendantSettings(objectToSerialize);
		}

		// Token: 0x060048B3 RID: 18611 RVA: 0x0010D449 File Offset: 0x0010B649
		protected override object Deserialize(XmlSerializationReader reader)
		{
			return ((XmlSerializationReaderAutoAttendantSettings)reader).Read5_AutoAttendantSettings();
		}
	}
}
