using System;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel
{
	// Token: 0x02000039 RID: 57
	internal static class ComplianceJobConstants
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00007B73 File Offset: 0x00005D73
		internal static DateTime MinComplianceTime
		{
			get
			{
				return new DateTime(1900, 1, 1);
			}
		}

		// Token: 0x040000FE RID: 254
		internal const int MaxTopDetailedRecordsToReturn = 500;
	}
}
