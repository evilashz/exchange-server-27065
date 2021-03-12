using System;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.MessageSecurity.EdgeSync
{
	// Token: 0x0200001C RID: 28
	internal sealed class EdgeSyncCredentialSerializer : XmlSerializer2
	{
		// Token: 0x06000085 RID: 133 RVA: 0x0000552E File Offset: 0x0000372E
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("EdgeSyncCredential", "");
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00005540 File Offset: 0x00003740
		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((XmlSerializationWriterEdgeSyncCredential)writer).Write3_EdgeSyncCredential(objectToSerialize);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000554E File Offset: 0x0000374E
		protected override object Deserialize(XmlSerializationReader reader)
		{
			return ((XmlSerializationReaderEdgeSyncCredential)reader).Read3_EdgeSyncCredential();
		}
	}
}
