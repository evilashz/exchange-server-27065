using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.AsyncQueue
{
	// Token: 0x0200001B RID: 27
	internal class AsyncQueueProbeSchema
	{
		// Token: 0x04000065 RID: 101
		internal static readonly HygienePropertyDefinition OwnerID = AsyncQueueCommonSchema.OwnerIdProperty;

		// Token: 0x04000066 RID: 102
		internal static readonly HygienePropertyDefinition Priority = AsyncQueueCommonSchema.PriorityProperty;

		// Token: 0x04000067 RID: 103
		internal static readonly HygienePropertyDefinition RequestId = new HygienePropertyDefinition("RequestId", typeof(Guid));

		// Token: 0x04000068 RID: 104
		internal static readonly HygienePropertyDefinition StepName = AsyncQueueCommonSchema.StepNameProperty;

		// Token: 0x04000069 RID: 105
		internal static readonly HygienePropertyDefinition StepNumber = AsyncQueueCommonSchema.StepNumberProperty;

		// Token: 0x0400006A RID: 106
		internal static readonly HygienePropertyDefinition BitFlags = AsyncQueueCommonSchema.StepFlagsProperty;

		// Token: 0x0400006B RID: 107
		internal static readonly HygienePropertyDefinition Ordinal = AsyncQueueCommonSchema.StepOrdinalProperty;

		// Token: 0x0400006C RID: 108
		internal static readonly HygienePropertyDefinition Status = AsyncQueueCommonSchema.StepStatusProperty;

		// Token: 0x0400006D RID: 109
		internal static readonly HygienePropertyDefinition FetchCount = AsyncQueueCommonSchema.FetchCountProperty;

		// Token: 0x0400006E RID: 110
		internal static readonly HygienePropertyDefinition ErrorCount = AsyncQueueCommonSchema.ErrorCountProperty;

		// Token: 0x0400006F RID: 111
		internal static readonly HygienePropertyDefinition NextFetchDatetime = new HygienePropertyDefinition("NextFetchDatetime", typeof(DateTime?));

		// Token: 0x04000070 RID: 112
		internal static readonly HygienePropertyDefinition CreatedDatetime = new HygienePropertyDefinition("CreatedDatetime", typeof(DateTime?));

		// Token: 0x04000071 RID: 113
		internal static readonly HygienePropertyDefinition ChangedDatetime = new HygienePropertyDefinition("ChangedDatetime", typeof(DateTime?));

		// Token: 0x04000072 RID: 114
		internal static readonly HygienePropertyDefinition InprogressBatchSize = new HygienePropertyDefinition("InprogressBatchSize", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000073 RID: 115
		internal static readonly HygienePropertyDefinition BatchSize = new HygienePropertyDefinition("BatchSize", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000074 RID: 116
		internal static readonly HygienePropertyDefinition ProocessInprogressBackInSeconds = new HygienePropertyDefinition("ProocessInprogressBackInSeconds", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000075 RID: 117
		internal static readonly HygienePropertyDefinition ProocessBackInSeconds = new HygienePropertyDefinition("ProocessBackInSeconds", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
