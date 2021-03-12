using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000D RID: 13
	[DataContract]
	internal sealed class PopAccountSettings : ItemPropertiesBase
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00002DFE File Offset: 0x00000FFE
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00002E06 File Offset: 0x00001006
		[DataMember]
		public PopAccountInfo[] PopAccounts { get; set; }
	}
}
