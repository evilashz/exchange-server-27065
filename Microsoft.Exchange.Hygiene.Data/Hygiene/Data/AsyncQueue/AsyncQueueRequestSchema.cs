using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.AsyncQueue
{
	// Token: 0x02000021 RID: 33
	internal class AsyncQueueRequestSchema
	{
		// Token: 0x0400007F RID: 127
		internal static readonly HygienePropertyDefinition PriorityProperty = AsyncQueueCommonSchema.PriorityProperty;

		// Token: 0x04000080 RID: 128
		internal static readonly HygienePropertyDefinition RequestIdProperty = AsyncQueueCommonSchema.RequestIdProperty;

		// Token: 0x04000081 RID: 129
		internal static readonly HygienePropertyDefinition FriendlyNameProperty = AsyncQueueCommonSchema.FriendlyNameProperty;

		// Token: 0x04000082 RID: 130
		internal static readonly HygienePropertyDefinition RequestFlagsProperty = AsyncQueueCommonSchema.RequestFlagsProperty;

		// Token: 0x04000083 RID: 131
		internal static readonly HygienePropertyDefinition OwnerIdProperty = AsyncQueueCommonSchema.OwnerIdProperty;

		// Token: 0x04000084 RID: 132
		internal static readonly HygienePropertyDefinition OrganizationalUnitRootProperty = AsyncQueueCommonSchema.OrganizationalUnitRootProperty;

		// Token: 0x04000085 RID: 133
		internal static readonly HygienePropertyDefinition DependantOrganizationalUnitRootProperty = AsyncQueueCommonSchema.DependantOrganizationalUnitRootProperty;

		// Token: 0x04000086 RID: 134
		internal static readonly HygienePropertyDefinition DependantRequestIdProperty = AsyncQueueCommonSchema.DependantRequestIdProperty;

		// Token: 0x04000087 RID: 135
		internal static readonly HygienePropertyDefinition RequestStatusProperty = AsyncQueueCommonSchema.RequestStatusProperty;

		// Token: 0x04000088 RID: 136
		internal static readonly HygienePropertyDefinition CreatedDatetimeProperty = AsyncQueueCommonSchema.CreatedDatetimeProperty;

		// Token: 0x04000089 RID: 137
		internal static readonly HygienePropertyDefinition LastModifiedDatetimeProperty = AsyncQueueCommonSchema.LastModifiedDatetimeProperty;

		// Token: 0x0400008A RID: 138
		internal static readonly HygienePropertyDefinition RejectIfExistsProperty = new HygienePropertyDefinition("RejectIfExists", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400008B RID: 139
		internal static readonly HygienePropertyDefinition FailIfDependencyMissingProperty = new HygienePropertyDefinition("FailIfDependencyMissing", typeof(bool), true, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400008C RID: 140
		internal static readonly HygienePropertyDefinition RequestCookieProperty = AsyncQueueCommonSchema.RequestCookieProperty;

		// Token: 0x0400008D RID: 141
		internal static readonly HygienePropertyDefinition ScheduleTimeProperty = new HygienePropertyDefinition("ScheduleDatetime", typeof(DateTime?));

		// Token: 0x0400008E RID: 142
		internal static readonly HygienePropertyDefinition AsyncQueueStepsProperty = new HygienePropertyDefinition("StepProperties", typeof(AsyncQueueStep), null, ADPropertyDefinitionFlags.MultiValued);
	}
}
