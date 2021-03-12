using System;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EdgeSync.Common
{
	// Token: 0x02000034 RID: 52
	internal sealed class StatusSerializer : XmlSerializer1
	{
		// Token: 0x0600012D RID: 301 RVA: 0x00006EA6 File Offset: 0x000050A6
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("Status", "");
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006EB8 File Offset: 0x000050B8
		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((XmlSerializationWriterStatus)writer).Write5_Status(objectToSerialize);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006EC6 File Offset: 0x000050C6
		protected override object Deserialize(XmlSerializationReader reader)
		{
			return ((XmlSerializationReaderStatus)reader).Read5_Status();
		}
	}
}
