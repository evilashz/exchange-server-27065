using System;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007EA RID: 2026
	internal class FailedMSOSyncObjectPresentationObjectSchema : ObjectSchema
	{
		// Token: 0x040042B9 RID: 17081
		public static readonly PropertyDefinition ObjectId = FailedMSOSyncObjectSchema.ObjectId;

		// Token: 0x040042BA RID: 17082
		public static readonly PropertyDefinition ContextId = FailedMSOSyncObjectSchema.ContextId;

		// Token: 0x040042BB RID: 17083
		public static readonly PropertyDefinition ExternalDirectoryObjectClass = FailedMSOSyncObjectSchema.ExternalDirectoryObjectClass;

		// Token: 0x040042BC RID: 17084
		public static readonly PropertyDefinition SyncObjectId = FailedMSOSyncObjectSchema.SyncObjectId;

		// Token: 0x040042BD RID: 17085
		public static readonly PropertyDefinition DivergenceTimestamp = FailedMSOSyncObjectSchema.DivergenceTimestamp;

		// Token: 0x040042BE RID: 17086
		public static readonly PropertyDefinition DivergenceCount = FailedMSOSyncObjectSchema.DivergenceCount;

		// Token: 0x040042BF RID: 17087
		public static readonly PropertyDefinition IsTemporary = FailedMSOSyncObjectSchema.IsTemporary;

		// Token: 0x040042C0 RID: 17088
		public static readonly PropertyDefinition IsIncrementalOnly = FailedMSOSyncObjectSchema.IsIncrementalOnly;

		// Token: 0x040042C1 RID: 17089
		public static readonly PropertyDefinition IsLinkRelated = FailedMSOSyncObjectSchema.IsLinkRelated;

		// Token: 0x040042C2 RID: 17090
		public static readonly PropertyDefinition IsIgnoredInHaltCondition = FailedMSOSyncObjectSchema.IsIgnoredInHaltCondition;

		// Token: 0x040042C3 RID: 17091
		public static readonly PropertyDefinition IsTenantWideDivergence = FailedMSOSyncObjectSchema.IsTenantWideDivergence;

		// Token: 0x040042C4 RID: 17092
		public static readonly PropertyDefinition IsRetriable = FailedMSOSyncObjectSchema.IsRetriable;

		// Token: 0x040042C5 RID: 17093
		public static readonly PropertyDefinition IsValidationDivergence = FailedMSOSyncObjectSchema.IsValidationDivergence;

		// Token: 0x040042C6 RID: 17094
		public static readonly PropertyDefinition Errors = FailedMSOSyncObjectSchema.Errors;

		// Token: 0x040042C7 RID: 17095
		public static readonly PropertyDefinition Comment = FailedMSOSyncObjectSchema.Comment;

		// Token: 0x040042C8 RID: 17096
		public static readonly PropertyDefinition WhenChanged = ADObjectSchema.WhenChanged;

		// Token: 0x040042C9 RID: 17097
		public static readonly PropertyDefinition WhenChangedUTC = ADObjectSchema.WhenChangedUTC;
	}
}
