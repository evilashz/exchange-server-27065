using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend
{
	// Token: 0x02000042 RID: 66
	internal sealed class SyncTaskStatusUpdate : BackgroundJobBackendBase
	{
		// Token: 0x0600025B RID: 603 RVA: 0x000085B4 File Offset: 0x000067B4
		public SyncTaskStatusUpdate(Guid backgroundJobId, Guid taskId, Guid ownerId, TaskExecutionStateType taskExecutionState, TaskExecutionStateType newTaskExecutionState, TaskCompletionStatusType newTaskCompletionStatus)
		{
			this[SyncTaskStatusUpdate.BackgroundJobIdProperty] = backgroundJobId;
			this[SyncTaskStatusUpdate.TaskIdProperty] = taskId;
			this[SyncTaskStatusUpdate.BJMOwnerIdProperty] = ownerId;
			this[SyncTaskStatusUpdate.TaskExecutionStateProperty] = taskExecutionState;
			this[SyncTaskStatusUpdate.NewTaskExecutionStateProperty] = newTaskExecutionState;
			this[SyncTaskStatusUpdate.NewTaskCompletionStatusProperty] = newTaskCompletionStatus;
			this.UpdatedTaskStatus = false;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00008637 File Offset: 0x00006837
		public Guid BackgroundJobId
		{
			get
			{
				return (Guid)this[SyncTaskStatusUpdate.BackgroundJobIdProperty];
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00008649 File Offset: 0x00006849
		public Guid TaskId
		{
			get
			{
				return (Guid)this[SyncTaskStatusUpdate.TaskIdProperty];
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000865B File Offset: 0x0000685B
		// (set) Token: 0x0600025F RID: 607 RVA: 0x0000866D File Offset: 0x0000686D
		public TaskExecutionStateType TaskExecutionState
		{
			get
			{
				return (TaskExecutionStateType)this[SyncTaskStatusUpdate.TaskExecutionStateProperty];
			}
			set
			{
				this[SyncTaskStatusUpdate.TaskExecutionStateProperty] = (byte)value;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00008680 File Offset: 0x00006880
		// (set) Token: 0x06000261 RID: 609 RVA: 0x00008692 File Offset: 0x00006892
		public TaskExecutionStateType NewTaskExecutionState
		{
			get
			{
				return (TaskExecutionStateType)this[SyncTaskStatusUpdate.NewTaskExecutionStateProperty];
			}
			set
			{
				this[SyncTaskStatusUpdate.NewTaskExecutionStateProperty] = (byte)value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000262 RID: 610 RVA: 0x000086A5 File Offset: 0x000068A5
		// (set) Token: 0x06000263 RID: 611 RVA: 0x000086B7 File Offset: 0x000068B7
		public TaskCompletionStatusType NewTaskCompletionStatus
		{
			get
			{
				return (TaskCompletionStatusType)this[SyncTaskStatusUpdate.NewTaskCompletionStatusProperty];
			}
			set
			{
				this[SyncTaskStatusUpdate.NewTaskCompletionStatusProperty] = (byte)value;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000264 RID: 612 RVA: 0x000086CA File Offset: 0x000068CA
		public Guid BJMOwnerId
		{
			get
			{
				return (Guid)this[SyncTaskStatusUpdate.BJMOwnerIdProperty];
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000265 RID: 613 RVA: 0x000086DC File Offset: 0x000068DC
		public bool HasEndTime
		{
			get
			{
				return this[SyncTaskStatusUpdate.EndTimeProperty] != null;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000266 RID: 614 RVA: 0x000086EF File Offset: 0x000068EF
		// (set) Token: 0x06000267 RID: 615 RVA: 0x00008701 File Offset: 0x00006901
		public DateTime EndTime
		{
			get
			{
				return (DateTime)this[SyncTaskStatusUpdate.EndTimeProperty];
			}
			set
			{
				this[SyncTaskStatusUpdate.EndTimeProperty] = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00008714 File Offset: 0x00006914
		public bool HasHeartBeat
		{
			get
			{
				return this[SyncTaskStatusUpdate.HeartBeatProperty] != null;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00008727 File Offset: 0x00006927
		// (set) Token: 0x0600026A RID: 618 RVA: 0x00008739 File Offset: 0x00006939
		public DateTime HeartBeat
		{
			get
			{
				return (DateTime)this[SyncTaskStatusUpdate.HeartBeatProperty];
			}
			set
			{
				this[SyncTaskStatusUpdate.HeartBeatProperty] = value;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000874C File Offset: 0x0000694C
		// (set) Token: 0x0600026C RID: 620 RVA: 0x0000875E File Offset: 0x0000695E
		public bool UpdatedTaskStatus
		{
			get
			{
				return (bool)this[SyncTaskStatusUpdate.UpdatedTaskStatusProperty];
			}
			set
			{
				this[SyncTaskStatusUpdate.UpdatedTaskStatusProperty] = value;
			}
		}

		// Token: 0x04000198 RID: 408
		internal static readonly BackgroundJobBackendPropertyDefinition BackgroundJobIdProperty = ScheduleItemProperties.BackgroundJobIdProperty;

		// Token: 0x04000199 RID: 409
		internal static readonly BackgroundJobBackendPropertyDefinition TaskIdProperty = TaskItemProperties.TaskIdProperty;

		// Token: 0x0400019A RID: 410
		internal static readonly BackgroundJobBackendPropertyDefinition TaskExecutionStateProperty = TaskItemProperties.TaskExecutionStateProperty;

		// Token: 0x0400019B RID: 411
		internal static readonly BackgroundJobBackendPropertyDefinition BJMOwnerIdProperty = TaskItemProperties.BJMOwnerIdProperty;

		// Token: 0x0400019C RID: 412
		internal static readonly BackgroundJobBackendPropertyDefinition HeartBeatProperty = TaskItemProperties.HeartBeatProperty;

		// Token: 0x0400019D RID: 413
		internal static readonly BackgroundJobBackendPropertyDefinition EndTimeProperty = TaskItemProperties.EndTimeProperty;

		// Token: 0x0400019E RID: 414
		internal static readonly BackgroundJobBackendPropertyDefinition NewTaskExecutionStateProperty = new BackgroundJobBackendPropertyDefinition("NewTaskExecutionState", typeof(byte), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, 0);

		// Token: 0x0400019F RID: 415
		internal static readonly BackgroundJobBackendPropertyDefinition NewTaskCompletionStatusProperty = new BackgroundJobBackendPropertyDefinition("NewTaskCompletionStatus", typeof(byte), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, 0);

		// Token: 0x040001A0 RID: 416
		internal static readonly BackgroundJobBackendPropertyDefinition UpdatedTaskStatusProperty = new BackgroundJobBackendPropertyDefinition("UpdatedTaskStatus", typeof(bool), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, false);
	}
}
