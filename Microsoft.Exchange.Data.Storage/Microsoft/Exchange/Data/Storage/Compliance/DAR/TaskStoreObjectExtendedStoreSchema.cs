using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Compliance.DAR
{
	// Token: 0x0200045A RID: 1114
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TaskStoreObjectExtendedStoreSchema : ObjectSchema
	{
		// Token: 0x04001ACB RID: 6859
		public static readonly ExtendedPropertyDefinition Priority = new ExtendedPropertyDefinition(WellKnownPropertySet.Compliance, "Priority", 14);

		// Token: 0x04001ACC RID: 6860
		public static readonly ExtendedPropertyDefinition Category = new ExtendedPropertyDefinition(WellKnownPropertySet.Compliance, "Category", 14);

		// Token: 0x04001ACD RID: 6861
		public static readonly ExtendedPropertyDefinition TaskState = new ExtendedPropertyDefinition(WellKnownPropertySet.Compliance, "TaskState", 14);

		// Token: 0x04001ACE RID: 6862
		public static readonly ExtendedPropertyDefinition TaskType = new ExtendedPropertyDefinition(WellKnownPropertySet.Compliance, "TaskType", 25);

		// Token: 0x04001ACF RID: 6863
		public static readonly ExtendedPropertyDefinition SerializedTaskData = new ExtendedPropertyDefinition(WellKnownPropertySet.Compliance, "SerializedTaskData", 25);

		// Token: 0x04001AD0 RID: 6864
		public static readonly ExtendedPropertyDefinition TenantId = new ExtendedPropertyDefinition(WellKnownPropertySet.Compliance, "TenantId", 2);

		// Token: 0x04001AD1 RID: 6865
		public static readonly ExtendedPropertyDefinition MinTaskScheduleTime = new ExtendedPropertyDefinition(WellKnownPropertySet.Compliance, "MinTaskScheduleTime", 23);

		// Token: 0x04001AD2 RID: 6866
		public static readonly ExtendedPropertyDefinition TaskCompletionTime = new ExtendedPropertyDefinition(WellKnownPropertySet.Compliance, "TaskCompletionTime", 23);

		// Token: 0x04001AD3 RID: 6867
		public static readonly ExtendedPropertyDefinition TaskExecutionStartTime = new ExtendedPropertyDefinition(WellKnownPropertySet.Compliance, "TaskExecutionStartTime", 23);

		// Token: 0x04001AD4 RID: 6868
		public static readonly ExtendedPropertyDefinition TaskQueuedTime = new ExtendedPropertyDefinition(WellKnownPropertySet.Compliance, "TaskQueuedTime", 23);

		// Token: 0x04001AD5 RID: 6869
		public static readonly ExtendedPropertyDefinition TaskScheduledTime = new ExtendedPropertyDefinition(WellKnownPropertySet.Compliance, "TaskScheduledTime", 23);

		// Token: 0x04001AD6 RID: 6870
		public static readonly ExtendedPropertyDefinition TaskRetryInterval = new ExtendedPropertyDefinition(WellKnownPropertySet.Compliance, "TaskRetryInterval", 25);

		// Token: 0x04001AD7 RID: 6871
		public static readonly ExtendedPropertyDefinition TaskRetryCurrentCount = new ExtendedPropertyDefinition(WellKnownPropertySet.Compliance, "TaskRetryCurrentCount", 14);

		// Token: 0x04001AD8 RID: 6872
		public static readonly ExtendedPropertyDefinition TaskRetryTotalCount = new ExtendedPropertyDefinition(WellKnownPropertySet.Compliance, "TaskRetryTotalCount", 14);

		// Token: 0x04001AD9 RID: 6873
		public static readonly ExtendedPropertyDefinition TaskSynchronizationOption = new ExtendedPropertyDefinition(WellKnownPropertySet.Compliance, "TaskSynchronizationOption", 14);

		// Token: 0x04001ADA RID: 6874
		public static readonly ExtendedPropertyDefinition TaskSynchronizationKey = new ExtendedPropertyDefinition(WellKnownPropertySet.Compliance, "TaskSynchronizationKey", 25);

		// Token: 0x04001ADB RID: 6875
		public static readonly ExtendedPropertyDefinition ExecutionContainer = new ExtendedPropertyDefinition(WellKnownPropertySet.Compliance, "ExecutionContainer", 25);

		// Token: 0x04001ADC RID: 6876
		public static readonly ExtendedPropertyDefinition ExecutionTarget = new ExtendedPropertyDefinition(WellKnownPropertySet.Compliance, "ExecutionTarget", 25);

		// Token: 0x04001ADD RID: 6877
		public static readonly ExtendedPropertyDefinition ExecutionLockExpiryTime = new ExtendedPropertyDefinition(WellKnownPropertySet.Compliance, "ExecutionLockExpiryTime", 23);

		// Token: 0x04001ADE RID: 6878
		public static readonly ExtendedPropertyDefinition SchemaVersion = new ExtendedPropertyDefinition(WellKnownPropertySet.Compliance, "SchemaVersion", 14);
	}
}
