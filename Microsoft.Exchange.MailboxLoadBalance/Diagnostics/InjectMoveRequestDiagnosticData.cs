using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.Diagnostics
{
	// Token: 0x02000059 RID: 89
	[DataContract]
	internal class InjectMoveRequestDiagnosticData : CmdletRequestDiagnosticData
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00009A4B File Offset: 0x00007C4B
		// (set) Token: 0x0600031F RID: 799 RVA: 0x00009A53 File Offset: 0x00007C53
		[DataMember]
		public bool ArchiveOnly { get; set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000320 RID: 800 RVA: 0x00009A5C File Offset: 0x00007C5C
		// (set) Token: 0x06000321 RID: 801 RVA: 0x00009A64 File Offset: 0x00007C64
		[DataMember]
		public DirectoryMailbox Mailbox { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00009A6D File Offset: 0x00007C6D
		// (set) Token: 0x06000323 RID: 803 RVA: 0x00009A75 File Offset: 0x00007C75
		[DataMember]
		public bool Protect { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00009A7E File Offset: 0x00007C7E
		// (set) Token: 0x06000325 RID: 805 RVA: 0x00009A86 File Offset: 0x00007C86
		[DataMember]
		public DirectoryIdentity TargetDatabase { get; set; }
	}
}
