using System;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Compliance.DAR
{
	// Token: 0x0200045C RID: 1116
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TaskAggregateStoreObjectSchema : EwsStoreObjectSchema
	{
		// Token: 0x04001AE1 RID: 6881
		public static readonly EwsStoreObjectPropertyDefinition Id = EwsStoreObjectSchema.AlternativeId;

		// Token: 0x04001AE2 RID: 6882
		public static readonly EwsStoreObjectPropertyDefinition LastModifiedTime = TaskStoreObjectSchema.CreateEwsDefinitionFromServiceDefinition(ItemSchema.LastModifiedTime, "LastModifiedTime", null);

		// Token: 0x04001AE3 RID: 6883
		public static readonly EwsStoreObjectPropertyDefinition Enabled = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskAggregateStoreObjectExtendedStoreSchema.Enabled, null);

		// Token: 0x04001AE4 RID: 6884
		public static readonly EwsStoreObjectPropertyDefinition TaskType = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskStoreObjectExtendedStoreSchema.TaskType, null);

		// Token: 0x04001AE5 RID: 6885
		public static readonly EwsStoreObjectPropertyDefinition ScopeId = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskStoreObjectExtendedStoreSchema.TenantId, null);

		// Token: 0x04001AE6 RID: 6886
		public static readonly EwsStoreObjectPropertyDefinition MaxRunningTasks = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskAggregateStoreObjectExtendedStoreSchema.MaxRunningTasks, null);

		// Token: 0x04001AE7 RID: 6887
		public static readonly EwsStoreObjectPropertyDefinition RecurrenceType = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskAggregateStoreObjectExtendedStoreSchema.RecurrenceType, null);

		// Token: 0x04001AE8 RID: 6888
		public static readonly EwsStoreObjectPropertyDefinition RecurrenceFrequency = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskAggregateStoreObjectExtendedStoreSchema.RecurrenceFrequency, null);

		// Token: 0x04001AE9 RID: 6889
		public static readonly EwsStoreObjectPropertyDefinition RecurrenceInterval = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskAggregateStoreObjectExtendedStoreSchema.RecurrenceInterval, null);

		// Token: 0x04001AEA RID: 6890
		public static readonly EwsStoreObjectPropertyDefinition SchemaVersion = TaskStoreObjectSchema.CreateEwsPropertyDefinition(TaskStoreObjectExtendedStoreSchema.SchemaVersion, 0);
	}
}
