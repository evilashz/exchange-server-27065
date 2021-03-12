using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Diagnostics
{
	// Token: 0x02000068 RID: 104
	[DataContract]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SoftDeletedMailboxRemovalDiagnosticData : CmdletRequestDiagnosticData
	{
		// Token: 0x06000397 RID: 919 RVA: 0x0000A918 File Offset: 0x00008B18
		public SoftDeletedMailboxRemovalDiagnosticData(Guid databaseGuid, Guid mailboxGuid)
		{
			this.MailboxGuid = mailboxGuid;
			this.DatabaseGuid = databaseGuid;
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000A92E File Offset: 0x00008B2E
		// (set) Token: 0x06000399 RID: 921 RVA: 0x0000A936 File Offset: 0x00008B36
		[DataMember]
		public Guid MailboxGuid { get; private set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0000A93F File Offset: 0x00008B3F
		// (set) Token: 0x0600039B RID: 923 RVA: 0x0000A947 File Offset: 0x00008B47
		[DataMember]
		public Guid DatabaseGuid { get; private set; }
	}
}
