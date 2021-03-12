using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Services.Diagnostics
{
	// Token: 0x02000039 RID: 57
	internal enum FindPlacesMetadata
	{
		// Token: 0x0400029E RID: 670
		[DisplayName("FPl", "QS")]
		QueryString,
		// Token: 0x0400029F RID: 671
		[DisplayName("FPl", "LT")]
		Latitude,
		// Token: 0x040002A0 RID: 672
		[DisplayName("FPl", "LG")]
		Longitude,
		// Token: 0x040002A1 RID: 673
		[DisplayName("FPl", "PBLT")]
		PhonebookLatency,
		// Token: 0x040002A2 RID: 674
		[DisplayName("FPl", "LCLT")]
		LocationLatency,
		// Token: 0x040002A3 RID: 675
		[DisplayName("FPl", "PBSC")]
		PhonebookStatusCode,
		// Token: 0x040002A4 RID: 676
		[DisplayName("FPl", "LCSC")]
		LocationStatusCode,
		// Token: 0x040002A5 RID: 677
		[DisplayName("FPl", "PBCT")]
		PhonebookResultsCount,
		// Token: 0x040002A6 RID: 678
		[DisplayName("FPl", "LCCT")]
		LocationResultsCount,
		// Token: 0x040002A7 RID: 679
		[DisplayName("FPl", "PBEM")]
		PhonebookErrorMessage,
		// Token: 0x040002A8 RID: 680
		[DisplayName("FPl", "LCEM")]
		LocationErrorMessage,
		// Token: 0x040002A9 RID: 681
		[DisplayName("FPl", "PBEC")]
		PhonebookErrorCode,
		// Token: 0x040002AA RID: 682
		[DisplayName("FPl", "LCEC")]
		LocationErrorCode,
		// Token: 0x040002AB RID: 683
		[DisplayName("FPl", "PBFailed")]
		PhonebookFailed,
		// Token: 0x040002AC RID: 684
		[DisplayName("FPl", "LCFailed")]
		LocationFailed
	}
}
