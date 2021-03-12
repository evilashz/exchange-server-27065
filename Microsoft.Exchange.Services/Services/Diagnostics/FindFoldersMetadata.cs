using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x02000036 RID: 54
	internal enum FindFoldersMetadata
	{
		// Token: 0x0400026A RID: 618
		[DisplayName("FF", "TFC")]
		TotalFolderCount,
		// Token: 0x0400026B RID: 619
		[DisplayName("FF", "CFC")]
		TopLevelChildFolderCount
	}
}
