using System;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.ProvisionResponse
{
	// Token: 0x020008A5 RID: 2213
	internal sealed class ProvisionSerializer : XmlSerializer1
	{
		// Token: 0x06002F67 RID: 12135 RVA: 0x0006BB43 File Offset: 0x00069D43
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("Provision", "DeltaSyncV2:");
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x0006BB55 File Offset: 0x00069D55
		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((XmlSerializationWriterProvision)writer).Write6_Provision(objectToSerialize);
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x0006BB63 File Offset: 0x00069D63
		protected override object Deserialize(XmlSerializationReader reader)
		{
			return ((XmlSerializationReaderProvision)reader).Read6_Provision();
		}
	}
}
