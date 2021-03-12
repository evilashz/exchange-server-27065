using System;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve
{
	// Token: 0x020008AD RID: 2221
	public sealed class RecipientSyncStateSerializer : XmlSerializer1
	{
		// Token: 0x06002F9F RID: 12191 RVA: 0x0006C64C File Offset: 0x0006A84C
		public override bool CanDeserialize(XmlReader xmlReader)
		{
			return xmlReader.IsStartElement("RecipientSyncState", "");
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x0006C65E File Offset: 0x0006A85E
		protected override void Serialize(object objectToSerialize, XmlSerializationWriter writer)
		{
			((XmlSerializationWriterRecipientSyncState)writer).Write3_RecipientSyncState(objectToSerialize);
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x0006C66C File Offset: 0x0006A86C
		protected override object Deserialize(XmlSerializationReader reader)
		{
			return ((XmlSerializationReaderRecipientSyncState)reader).Read3_RecipientSyncState();
		}
	}
}
