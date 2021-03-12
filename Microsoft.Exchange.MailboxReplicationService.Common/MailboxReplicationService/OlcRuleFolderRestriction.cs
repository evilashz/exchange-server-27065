using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000B2 RID: 178
	[DataContract]
	internal sealed class OlcRuleFolderRestriction : OlcRuleRestrictionBase
	{
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000728 RID: 1832 RVA: 0x0000B821 File Offset: 0x00009A21
		// (set) Token: 0x06000729 RID: 1833 RVA: 0x0000B829 File Offset: 0x00009A29
		[DataMember]
		public bool Recursive { get; set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0000B832 File Offset: 0x00009A32
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x0000B83A File Offset: 0x00009A3A
		[DataMember]
		public byte[] FolderId { get; set; }
	}
}
