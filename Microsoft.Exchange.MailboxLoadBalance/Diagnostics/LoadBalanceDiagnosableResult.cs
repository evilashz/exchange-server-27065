using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Band;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.Diagnostics
{
	// Token: 0x02000060 RID: 96
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal struct LoadBalanceDiagnosableResult
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600034C RID: 844 RVA: 0x00009F6F File Offset: 0x0000816F
		// (set) Token: 0x0600034D RID: 845 RVA: 0x00009F77 File Offset: 0x00008177
		[DataMember]
		public DirectoryIdentity DatabaseToDrain { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00009F80 File Offset: 0x00008180
		// (set) Token: 0x0600034F RID: 847 RVA: 0x00009F88 File Offset: 0x00008188
		[DataMember]
		public BatchName DrainBatchName { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00009F91 File Offset: 0x00008191
		// (set) Token: 0x06000351 RID: 849 RVA: 0x00009F99 File Offset: 0x00008199
		[DataMember]
		public QueueManagerDiagnosticData QueueManager { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00009FA2 File Offset: 0x000081A2
		// (set) Token: 0x06000353 RID: 851 RVA: 0x00009FAA File Offset: 0x000081AA
		[DataMember]
		public IList<BandMailboxRebalanceData> RebalanceResults { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000354 RID: 852 RVA: 0x00009FB3 File Offset: 0x000081B3
		// (set) Token: 0x06000355 RID: 853 RVA: 0x00009FBB File Offset: 0x000081BB
		[DataMember]
		public SoftDeletedMailboxRemovalResult SoftDeletedMailboxRemovalResult { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00009FC4 File Offset: 0x000081C4
		// (set) Token: 0x06000357 RID: 855 RVA: 0x00009FCC File Offset: 0x000081CC
		[DataMember]
		public SoftDeletedMoveHistoryResult SoftDeletedMoveHistoryResult { get; set; }
	}
}
