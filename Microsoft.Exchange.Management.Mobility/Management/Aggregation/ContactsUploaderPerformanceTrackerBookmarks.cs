using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Management.Aggregation
{
	// Token: 0x02000018 RID: 24
	internal enum ContactsUploaderPerformanceTrackerBookmarks
	{
		// Token: 0x04000061 RID: 97
		[DisplayName("CU", "ExportT")]
		ExportTime,
		// Token: 0x04000062 RID: 98
		[DisplayName("CU", "FormatT")]
		FormatTime,
		// Token: 0x04000063 RID: 99
		[DisplayName("CU", "UploadT")]
		UploadTime
	}
}
