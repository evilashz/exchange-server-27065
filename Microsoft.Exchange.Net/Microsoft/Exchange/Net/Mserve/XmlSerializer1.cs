using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve
{
	// Token: 0x020008AC RID: 2220
	public abstract class XmlSerializer1 : XmlSerializer
	{
		// Token: 0x06002F9C RID: 12188 RVA: 0x0006C636 File Offset: 0x0006A836
		protected override XmlSerializationReader CreateReader()
		{
			return new XmlSerializationReaderRecipientSyncState();
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x0006C63D File Offset: 0x0006A83D
		protected override XmlSerializationWriter CreateWriter()
		{
			return new XmlSerializationWriterRecipientSyncState();
		}
	}
}
