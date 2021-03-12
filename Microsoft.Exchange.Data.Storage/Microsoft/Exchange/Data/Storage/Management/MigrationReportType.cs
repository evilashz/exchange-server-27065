using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A36 RID: 2614
	[Serializable]
	public enum MigrationReportType
	{
		// Token: 0x04003650 RID: 13904
		[LocDescription(ServerStrings.IDs.MigrationReportUnknown)]
		Unknown,
		// Token: 0x04003651 RID: 13905
		[LocDescription(ServerStrings.IDs.MigrationReportBatchSuccess)]
		BatchSuccessReport,
		// Token: 0x04003652 RID: 13906
		[LocDescription(ServerStrings.IDs.MigrationReportBatchFailure)]
		BatchFailureReport,
		// Token: 0x04003653 RID: 13907
		[LocDescription(ServerStrings.IDs.MigrationReportFinalizationSuccess)]
		FinalizationSuccessReport,
		// Token: 0x04003654 RID: 13908
		[LocDescription(ServerStrings.IDs.MigrationReportFinalizationFailure)]
		FinalizationFailureReport,
		// Token: 0x04003655 RID: 13909
		[LocDescription(ServerStrings.IDs.MigrationReportBatch)]
		BatchReport
	}
}
