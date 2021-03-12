using System;

namespace Microsoft.Exchange.Hygiene.Data.AsyncQueue
{
	// Token: 0x0200001E RID: 30
	internal class AsyncQueueReportSchema
	{
		// Token: 0x04000078 RID: 120
		internal static readonly HygienePropertyDefinition ReportProperty = new HygienePropertyDefinition("AsyncQueueReport", typeof(string));

		// Token: 0x04000079 RID: 121
		internal static readonly HygienePropertyDefinition OrganizationalUnitRootProperty = AsyncQueueCommonSchema.OrganizationalUnitRootProperty;

		// Token: 0x0400007A RID: 122
		internal static readonly HygienePropertyDefinition RequestIdProperty = AsyncQueueCommonSchema.RequestIdProperty;

		// Token: 0x0400007B RID: 123
		internal static readonly HygienePropertyDefinition ProcessStartDatetimeProperty = AsyncQueueLogSchema.ProcessStartDatetimeProperty;

		// Token: 0x0400007C RID: 124
		internal static readonly HygienePropertyDefinition ProcessEndDatetimeProperty = AsyncQueueLogSchema.ProcessEndDatetimeProperty;
	}
}
