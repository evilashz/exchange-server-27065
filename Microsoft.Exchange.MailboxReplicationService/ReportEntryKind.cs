using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000089 RID: 137
	public enum ReportEntryKind
	{
		// Token: 0x040002D7 RID: 727
		SourceConnection,
		// Token: 0x040002D8 RID: 728
		TargetConnection,
		// Token: 0x040002D9 RID: 729
		SignaturePreservation,
		// Token: 0x040002DA RID: 730
		StartingFolderHierarchyCreation,
		// Token: 0x040002DB RID: 731
		AggregatedSoftDeletedMessages,
		// Token: 0x040002DC RID: 732
		HierarchyChanges
	}
}
