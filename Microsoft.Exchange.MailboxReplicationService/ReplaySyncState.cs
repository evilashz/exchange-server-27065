using System;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200005F RID: 95
	public sealed class ReplaySyncState : XMLSerializableBase
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0001D301 File Offset: 0x0001B501
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x0001D309 File Offset: 0x0001B509
		[XmlElement(ElementName = "ProviderState")]
		public string ProviderState { get; set; }

		// Token: 0x060004DB RID: 1243 RVA: 0x0001D312 File Offset: 0x0001B512
		public static ReplaySyncState Deserialize(string data)
		{
			return XMLSerializableBase.Deserialize<ReplaySyncState>(data, true);
		}
	}
}
