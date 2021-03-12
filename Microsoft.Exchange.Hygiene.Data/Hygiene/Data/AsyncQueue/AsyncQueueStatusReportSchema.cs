using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.AsyncQueue
{
	// Token: 0x02000028 RID: 40
	internal class AsyncQueueStatusReportSchema
	{
		// Token: 0x040000B1 RID: 177
		internal static readonly HygienePropertyDefinition OrganizationalUnitRootProperty = AsyncQueueCommonSchema.OrganizationalUnitRootProperty;

		// Token: 0x040000B2 RID: 178
		internal static readonly HygienePropertyDefinition RequestIdProperty = AsyncQueueCommonSchema.RequestIdProperty;

		// Token: 0x040000B3 RID: 179
		internal static readonly HygienePropertyDefinition FriendlyNameProperty = AsyncQueueCommonSchema.FriendlyNameProperty;

		// Token: 0x040000B4 RID: 180
		internal static readonly HygienePropertyDefinition CreatedDatetimeProperty = AsyncQueueCommonSchema.CreatedDatetimeProperty;

		// Token: 0x040000B5 RID: 181
		internal static readonly HygienePropertyDefinition StepNameProperty = AsyncQueueCommonSchema.StepNameProperty;

		// Token: 0x040000B6 RID: 182
		internal static readonly HygienePropertyDefinition RequestStepIdProperty = AsyncQueueCommonSchema.RequestStepIdProperty;

		// Token: 0x040000B7 RID: 183
		internal static readonly HygienePropertyDefinition StepStatusProperty = AsyncQueueCommonSchema.StepStatusProperty;

		// Token: 0x040000B8 RID: 184
		internal static readonly HygienePropertyDefinition FetchCountProperty = AsyncQueueCommonSchema.FetchCountProperty;

		// Token: 0x040000B9 RID: 185
		internal static readonly HygienePropertyDefinition ErrorCountProperty = AsyncQueueCommonSchema.ErrorCountProperty;

		// Token: 0x040000BA RID: 186
		internal static readonly HygienePropertyDefinition NextFetchDatetimeProperty = AsyncQueueStepSchema.NextFetchDatetimeProperty;

		// Token: 0x040000BB RID: 187
		internal static readonly HygienePropertyDefinition MaxCreationDateTimeQueryProperty = AsyncQueueCommonSchema.MaxCreationDateTimeQueryProperty;

		// Token: 0x040000BC RID: 188
		internal static readonly HygienePropertyDefinition MinCreationDateTimeQueryProperty = AsyncQueueCommonSchema.MinCreationDateTimeQueryProperty;

		// Token: 0x040000BD RID: 189
		internal static readonly HygienePropertyDefinition MinFetchCountProperty = new HygienePropertyDefinition("MinFetchCount", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040000BE RID: 190
		internal static readonly HygienePropertyDefinition OwnerIdQueryProperty = AsyncQueueCommonSchema.OwnerIdProperty;

		// Token: 0x040000BF RID: 191
		internal static readonly HygienePropertyDefinition PageSizeQueryProperty = AsyncQueueCommonSchema.PageSizeQueryProperty;
	}
}
