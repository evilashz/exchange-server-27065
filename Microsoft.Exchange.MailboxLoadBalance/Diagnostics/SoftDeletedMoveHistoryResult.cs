using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.SoftDeletedRemoval;

namespace Microsoft.Exchange.MailboxLoadBalance.Diagnostics
{
	// Token: 0x0200006A RID: 106
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal class SoftDeletedMoveHistoryResult
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000A98B File Offset: 0x00008B8B
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x0000A993 File Offset: 0x00008B93
		[DataMember]
		public Guid MailboxGuid { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000A99C File Offset: 0x00008B9C
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x0000A9A4 File Offset: 0x00008BA4
		[DataMember]
		public Guid TargetDatabaseGuid { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000A9AD File Offset: 0x00008BAD
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x0000A9B5 File Offset: 0x00008BB5
		[DataMember]
		public Guid SourceDatabaseGuid { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000A9BE File Offset: 0x00008BBE
		// (set) Token: 0x060003AA RID: 938 RVA: 0x0000A9C6 File Offset: 0x00008BC6
		[DataMember]
		public SoftDeletedMoveHistory MoveHistory { get; set; }
	}
}
