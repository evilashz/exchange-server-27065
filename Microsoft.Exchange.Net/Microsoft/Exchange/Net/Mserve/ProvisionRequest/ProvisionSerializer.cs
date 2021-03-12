using System;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.ProvisionRequest
{
	// Token: 0x0200089C RID: 2204
	internal sealed class ProvisionSerializer : XmlSerializer1
	{
		// Token: 0x06002F2B RID: 12075 RVA: 0x0006A748 File Offset: 0x00068948
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("Provision", "DeltaSyncV2:");
		}

		// Token: 0x06002F2C RID: 12076 RVA: 0x0006A75A File Offset: 0x0006895A
		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((XmlSerializationWriterProvision)writer).Write5_Provision(objectToSerialize);
		}

		// Token: 0x06002F2D RID: 12077 RVA: 0x0006A768 File Offset: 0x00068968
		protected override object Deserialize(XmlSerializationReader reader)
		{
			return ((XmlSerializationReaderProvision)reader).Read5_Provision();
		}
	}
}
