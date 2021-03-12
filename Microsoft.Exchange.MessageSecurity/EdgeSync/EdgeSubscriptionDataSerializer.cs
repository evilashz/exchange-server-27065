using System;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.MessageSecurity.EdgeSync
{
	// Token: 0x02000017 RID: 23
	internal sealed class EdgeSubscriptionDataSerializer : XmlSerializer1
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00004DD4 File Offset: 0x00002FD4
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("EdgeSubscriptionData", "");
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004DE6 File Offset: 0x00002FE6
		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((XmlSerializationWriterEdgeSubscriptionData)writer).Write3_EdgeSubscriptionData(objectToSerialize);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004DF4 File Offset: 0x00002FF4
		protected override object Deserialize(XmlSerializationReader reader)
		{
			return ((XmlSerializationReaderEdgeSubscriptionData)reader).Read3_EdgeSubscriptionData();
		}
	}
}
