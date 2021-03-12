using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend
{
	// Token: 0x02000045 RID: 69
	internal static class TaskItemProperties
	{
		// Token: 0x040001B8 RID: 440
		internal static readonly BackgroundJobBackendPropertyDefinition ActiveJobIdProperty = new BackgroundJobBackendPropertyDefinition("ActiveJobId", typeof(Guid), PropertyDefinitionFlags.Mandatory, Guid.Empty);

		// Token: 0x040001B9 RID: 441
		internal static readonly BackgroundJobBackendPropertyDefinition TaskIdProperty = new BackgroundJobBackendPropertyDefinition("TaskId", typeof(Guid), PropertyDefinitionFlags.Mandatory, Guid.Empty);

		// Token: 0x040001BA RID: 442
		internal static readonly BackgroundJobBackendPropertyDefinition ScheduleIdProperty = new BackgroundJobBackendPropertyDefinition("ScheduleId", typeof(Guid), PropertyDefinitionFlags.Mandatory, Guid.Empty);

		// Token: 0x040001BB RID: 443
		internal static readonly BackgroundJobBackendPropertyDefinition BackgroundJobIdProperty = new BackgroundJobBackendPropertyDefinition("BackgroundJobId", typeof(Guid), PropertyDefinitionFlags.Mandatory, Guid.Empty);

		// Token: 0x040001BC RID: 444
		internal static readonly BackgroundJobBackendPropertyDefinition RoleIdProperty = new BackgroundJobBackendPropertyDefinition("RoleId", typeof(Guid), PropertyDefinitionFlags.Mandatory, Guid.Empty);

		// Token: 0x040001BD RID: 445
		internal static readonly BackgroundJobBackendPropertyDefinition InstanceIdProperty = new BackgroundJobBackendPropertyDefinition("InstanceId", typeof(int), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, 0);

		// Token: 0x040001BE RID: 446
		internal static readonly BackgroundJobBackendPropertyDefinition TaskExecutionStateProperty = new BackgroundJobBackendPropertyDefinition("TaskExecutionState", typeof(byte), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, 0);

		// Token: 0x040001BF RID: 447
		internal static readonly BackgroundJobBackendPropertyDefinition TaskCompletionStatusProperty = new BackgroundJobBackendPropertyDefinition("TaskCompletionStatus", typeof(byte?), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x040001C0 RID: 448
		internal static readonly BackgroundJobBackendPropertyDefinition ParentTaskIdProperty = new BackgroundJobBackendPropertyDefinition("ParentTaskId", typeof(Guid), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, Guid.Empty);

		// Token: 0x040001C1 RID: 449
		internal static readonly BackgroundJobBackendPropertyDefinition ExecutionAttemptProperty = new BackgroundJobBackendPropertyDefinition("ExecutionAttempt", typeof(short), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, 0);

		// Token: 0x040001C2 RID: 450
		internal static readonly BackgroundJobBackendPropertyDefinition BJMOwnerIdProperty = new BackgroundJobBackendPropertyDefinition("BJMOwnerId", typeof(Guid?), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x040001C3 RID: 451
		internal static readonly BackgroundJobBackendPropertyDefinition OwnerFitnessScoreProperty = new BackgroundJobBackendPropertyDefinition("OwnerFitnessScore", typeof(int?), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x040001C4 RID: 452
		internal static readonly BackgroundJobBackendPropertyDefinition StartTimeProperty = new BackgroundJobBackendPropertyDefinition("StartTime", typeof(DateTime?), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x040001C5 RID: 453
		internal static readonly BackgroundJobBackendPropertyDefinition EndTimeProperty = new BackgroundJobBackendPropertyDefinition("EndTime", typeof(DateTime?), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x040001C6 RID: 454
		internal static readonly BackgroundJobBackendPropertyDefinition HeartBeatProperty = new BackgroundJobBackendPropertyDefinition("Heartbeat", typeof(DateTime?), PropertyDefinitionFlags.Mandatory, null);

		// Token: 0x040001C7 RID: 455
		internal static readonly BackgroundJobBackendPropertyDefinition InsertTimeStampProperty = new BackgroundJobBackendPropertyDefinition("InsertTimeStamp", typeof(DateTime), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, new DateTime(0L));
	}
}
