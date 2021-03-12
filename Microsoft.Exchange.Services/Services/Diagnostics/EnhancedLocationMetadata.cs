using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x02000032 RID: 50
	internal enum EnhancedLocationMetadata
	{
		// Token: 0x04000222 RID: 546
		[DisplayName("ELC", "HLC")]
		HasLocation,
		// Token: 0x04000223 RID: 547
		[DisplayName("ELC", "SRC")]
		LocationSource,
		// Token: 0x04000224 RID: 548
		[DisplayName("ELC", "HAN")]
		HasAnnotation,
		// Token: 0x04000225 RID: 549
		[DisplayName("ELC", "HCD")]
		HasCoordinates
	}
}
