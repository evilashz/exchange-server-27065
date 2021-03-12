using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x02000059 RID: 89
	internal enum SyncFolderItemsMetadata
	{
		// Token: 0x040004F8 RID: 1272
		[DisplayName("SFi", "TotalT")]
		TotalTime,
		// Token: 0x040004F9 RID: 1273
		[DisplayName("SFi", "IcsT")]
		IcsTime,
		// Token: 0x040004FA RID: 1274
		[DisplayName("SFi", "QueryT")]
		QueryTime,
		// Token: 0x040004FB RID: 1275
		[DisplayName("SFi", "CatchUpT")]
		CatchUpTime,
		// Token: 0x040004FC RID: 1276
		[DisplayName("SFi", "ChangesT")]
		ChangesTime,
		// Token: 0x040004FD RID: 1277
		[DisplayName("SFi", "SSS")]
		SyncStateSize,
		// Token: 0x040004FE RID: 1278
		[DisplayName("SFi", "SSH")]
		SyncStateHash,
		// Token: 0x040004FF RID: 1279
		[DisplayName("SFi", "CC")]
		ItemCount,
		// Token: 0x04000500 RID: 1280
		[DisplayName("SFi", "Last")]
		IncludesLastItemInRange,
		// Token: 0x04000501 RID: 1281
		[DisplayName("SFi", "XcptId")]
		ExceptionItemId
	}
}
