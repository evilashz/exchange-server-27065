using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x02000057 RID: 87
	internal enum SyncCalendarMetadata
	{
		// Token: 0x040004D5 RID: 1237
		[DisplayName("SCa", "TT")]
		TotalTime,
		// Token: 0x040004D6 RID: 1238
		[DisplayName("SCa", "SSS")]
		SyncStateSize,
		// Token: 0x040004D7 RID: 1239
		[DisplayName("SCa", "SSH")]
		SyncStateHash,
		// Token: 0x040004D8 RID: 1240
		[DisplayName("SCa", "DIC")]
		DeletedItemsCount,
		// Token: 0x040004D9 RID: 1241
		[DisplayName("SCa", "UIC")]
		UpdatedItemsCount,
		// Token: 0x040004DA RID: 1242
		[DisplayName("SCa", "RMwIC")]
		RecurrenceMastersWithInstancesCount,
		// Token: 0x040004DB RID: 1243
		[DisplayName("SCa", "URMwIC")]
		UnchangedRecurrenceMastersWithInstancesCount,
		// Token: 0x040004DC RID: 1244
		[DisplayName("SCa", "RMwoIC")]
		RecurrenceMastersWithoutInstancesCount,
		// Token: 0x040004DD RID: 1245
		[DisplayName("SCa", "Last")]
		IncludesLastItemInRange,
		// Token: 0x040004DE RID: 1246
		[DisplayName("SCa", "XcptId")]
		ExceptionStoreId
	}
}
