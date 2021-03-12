using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000296 RID: 662
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class PerformanceConstants
	{
		// Token: 0x06001B7B RID: 7035 RVA: 0x0007F387 File Offset: 0x0007D587
		[Conditional("PerfInvestigation")]
		internal static void AssertEfficientPattern(bool condition, string formatString, params object[] parameters)
		{
			ExAssert.RetailAssert(condition, formatString, parameters);
		}

		// Token: 0x04001312 RID: 4882
		internal const int MemoryPropertyBagDefaultCapacity = 8;

		// Token: 0x04001313 RID: 4883
		internal const int DependencyEstimatesCapacity = 256;

		// Token: 0x04001314 RID: 4884
		internal const int DefaultDependencyEstimate = 128;

		// Token: 0x04001315 RID: 4885
		internal const int AverageDisplayNameLengthSwag = 15;

		// Token: 0x04001316 RID: 4886
		internal const int AverageEmailAddressLengthSwag = 15;

		// Token: 0x04001317 RID: 4887
		internal const int AverageRoutingTypeLengthSwag = 4;

		// Token: 0x04001318 RID: 4888
		internal const int AverageServerNameSwag = 20;

		// Token: 0x04001319 RID: 4889
		internal const int CorrelationQueryFetchCount = 50;

		// Token: 0x0400131A RID: 4890
		internal const int MalformedSingleAppointmentFetchCount = 50;

		// Token: 0x0400131B RID: 4891
		internal const int GoodSingleAppointmentFetchCount = 100;

		// Token: 0x0400131C RID: 4892
		internal const int StreamDataCacheSize = 4096;

		// Token: 0x0400131D RID: 4893
		internal const int StreamBodyBufferSize = 65536;

		// Token: 0x0400131E RID: 4894
		internal const int StreamAttachmentBufferSize = 131072;
	}
}
