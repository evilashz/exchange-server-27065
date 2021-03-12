using System;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.OOF
{
	// Token: 0x02000027 RID: 39
	internal sealed class UserOofSettingsSerializerSerializer : XmlSerializer1
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x000052D7 File Offset: 0x000034D7
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("UserOofSettings", "");
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000052E9 File Offset: 0x000034E9
		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((XmlSerializationWriterUserOofSettingsSerializer)writer).Write7_UserOofSettings(objectToSerialize);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000052F7 File Offset: 0x000034F7
		protected override object Deserialize(XmlSerializationReader reader)
		{
			return ((XmlSerializationReaderUserOofSettingsSerializer)reader).Read8_UserOofSettings();
		}
	}
}
