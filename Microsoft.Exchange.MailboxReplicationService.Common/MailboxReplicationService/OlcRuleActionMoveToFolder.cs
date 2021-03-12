using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000B4 RID: 180
	[DataContract]
	internal sealed class OlcRuleActionMoveToFolder : OlcRuleActionBase
	{
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0000B886 File Offset: 0x00009A86
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x0000B88E File Offset: 0x00009A8E
		[DataMember]
		public byte[] FolderId { get; set; }
	}
}
