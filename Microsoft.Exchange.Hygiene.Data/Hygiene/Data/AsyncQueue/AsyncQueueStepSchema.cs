using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.AsyncQueue
{
	// Token: 0x0200002B RID: 43
	internal class AsyncQueueStepSchema
	{
		// Token: 0x040000C1 RID: 193
		internal static readonly HygienePropertyDefinition PriorityProperty = AsyncQueueCommonSchema.PriorityProperty;

		// Token: 0x040000C2 RID: 194
		internal static readonly HygienePropertyDefinition RequestIdProperty = AsyncQueueCommonSchema.RequestIdProperty;

		// Token: 0x040000C3 RID: 195
		internal static readonly HygienePropertyDefinition DependantRequestIdProperty = AsyncQueueCommonSchema.DependantRequestIdProperty;

		// Token: 0x040000C4 RID: 196
		internal static readonly HygienePropertyDefinition RequestCookieProperty = AsyncQueueCommonSchema.RequestCookieProperty;

		// Token: 0x040000C5 RID: 197
		internal static readonly HygienePropertyDefinition RequestStepIdProperty = AsyncQueueCommonSchema.RequestStepIdProperty;

		// Token: 0x040000C6 RID: 198
		internal static readonly HygienePropertyDefinition OwnerIdProperty = AsyncQueueCommonSchema.OwnerIdProperty;

		// Token: 0x040000C7 RID: 199
		internal static readonly HygienePropertyDefinition FriendlyNameProperty = AsyncQueueCommonSchema.FriendlyNameProperty;

		// Token: 0x040000C8 RID: 200
		internal static readonly HygienePropertyDefinition OrganizationalUnitRootProperty = AsyncQueueCommonSchema.OrganizationalUnitRootProperty;

		// Token: 0x040000C9 RID: 201
		internal static readonly HygienePropertyDefinition StepNumberProperty = AsyncQueueCommonSchema.StepNumberProperty;

		// Token: 0x040000CA RID: 202
		internal static readonly HygienePropertyDefinition StepNameProperty = AsyncQueueCommonSchema.StepNameProperty;

		// Token: 0x040000CB RID: 203
		internal static readonly HygienePropertyDefinition StepOrdinalProperty = AsyncQueueCommonSchema.StepOrdinalProperty;

		// Token: 0x040000CC RID: 204
		internal static readonly HygienePropertyDefinition FlagsProperty = AsyncQueueCommonSchema.StepFlagsProperty;

		// Token: 0x040000CD RID: 205
		internal static readonly HygienePropertyDefinition StepStatusProperty = AsyncQueueCommonSchema.StepStatusProperty;

		// Token: 0x040000CE RID: 206
		internal static readonly HygienePropertyDefinition FetchCountProperty = AsyncQueueCommonSchema.FetchCountProperty;

		// Token: 0x040000CF RID: 207
		internal static readonly HygienePropertyDefinition ErrorCountProperty = AsyncQueueCommonSchema.ErrorCountProperty;

		// Token: 0x040000D0 RID: 208
		internal static readonly HygienePropertyDefinition ProcessInstanceNameProperty = AsyncQueueCommonSchema.ProcessInstanceNameProperty;

		// Token: 0x040000D1 RID: 209
		internal static readonly HygienePropertyDefinition MaxExecutionTimeProperty = AsyncQueueCommonSchema.MaxExecutionTimeProperty;

		// Token: 0x040000D2 RID: 210
		internal static readonly HygienePropertyDefinition CookieProperty = AsyncQueueCommonSchema.CookieProperty;

		// Token: 0x040000D3 RID: 211
		internal static readonly HygienePropertyDefinition ExecutionStateProperty = new HygienePropertyDefinition("ExecutionState", typeof(short), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040000D4 RID: 212
		internal static readonly HygienePropertyDefinition NextFetchDatetimeProperty = new HygienePropertyDefinition("NextFetchDatetime", typeof(DateTime?));
	}
}
