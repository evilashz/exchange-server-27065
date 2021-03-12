using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.AsyncQueue
{
	// Token: 0x02000017 RID: 23
	internal class AsyncQueueLogSchema
	{
		// Token: 0x04000049 RID: 73
		internal static readonly HygienePropertyDefinition StepTransactionIdProperty = new HygienePropertyDefinition("StepTransactionId", typeof(Guid));

		// Token: 0x0400004A RID: 74
		internal static readonly HygienePropertyDefinition PriorityProperty = AsyncQueueCommonSchema.PriorityProperty;

		// Token: 0x0400004B RID: 75
		internal static readonly HygienePropertyDefinition RequestStepIdProperty = AsyncQueueCommonSchema.RequestStepIdProperty;

		// Token: 0x0400004C RID: 76
		internal static readonly HygienePropertyDefinition RequestIdProperty = AsyncQueueCommonSchema.RequestIdProperty;

		// Token: 0x0400004D RID: 77
		internal static readonly HygienePropertyDefinition DependantRequestIdProperty = AsyncQueueCommonSchema.DependantRequestIdProperty;

		// Token: 0x0400004E RID: 78
		internal static readonly HygienePropertyDefinition OwnerIdProperty = AsyncQueueCommonSchema.OwnerIdProperty;

		// Token: 0x0400004F RID: 79
		internal static readonly HygienePropertyDefinition FriendlyNameProperty = AsyncQueueCommonSchema.FriendlyNameProperty;

		// Token: 0x04000050 RID: 80
		internal static readonly HygienePropertyDefinition OrganizationalUnitRootProperty = AsyncQueueCommonSchema.OrganizationalUnitRootProperty;

		// Token: 0x04000051 RID: 81
		internal static readonly HygienePropertyDefinition StepNumberProperty = AsyncQueueCommonSchema.StepNumberProperty;

		// Token: 0x04000052 RID: 82
		internal static readonly HygienePropertyDefinition StepNameProperty = AsyncQueueCommonSchema.StepNameProperty;

		// Token: 0x04000053 RID: 83
		internal static readonly HygienePropertyDefinition StepOrdinalProperty = AsyncQueueCommonSchema.StepOrdinalProperty;

		// Token: 0x04000054 RID: 84
		internal static readonly HygienePropertyDefinition StepFromStatusProperty = new HygienePropertyDefinition("FromStatus", typeof(AsyncQueueStatus));

		// Token: 0x04000055 RID: 85
		internal static readonly HygienePropertyDefinition StepStatusProperty = new HygienePropertyDefinition("Status", typeof(AsyncQueueStatus));

		// Token: 0x04000056 RID: 86
		internal static readonly HygienePropertyDefinition FetchCountProperty = AsyncQueueCommonSchema.FetchCountProperty;

		// Token: 0x04000057 RID: 87
		internal static readonly HygienePropertyDefinition ErrorCountProperty = AsyncQueueCommonSchema.ErrorCountProperty;

		// Token: 0x04000058 RID: 88
		internal static readonly HygienePropertyDefinition ProcessInstanceNameProperty = AsyncQueueCommonSchema.ProcessInstanceNameProperty;

		// Token: 0x04000059 RID: 89
		internal static readonly HygienePropertyDefinition ProcessStartDatetimeProperty = new HygienePropertyDefinition("ProcessStartDatetime", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400005A RID: 90
		internal static readonly HygienePropertyDefinition ProcessEndDatetimeProperty = new HygienePropertyDefinition("ProcessEndDatetime", typeof(DateTime?));
	}
}
