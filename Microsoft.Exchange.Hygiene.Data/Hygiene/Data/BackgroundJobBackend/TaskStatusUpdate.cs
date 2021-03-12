using System;

namespace Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend
{
	// Token: 0x02000046 RID: 70
	internal sealed class TaskStatusUpdate : BackgroundJobBackendBase
	{
		// Token: 0x060002A1 RID: 673 RVA: 0x00008F30 File Offset: 0x00007130
		public TaskStatusUpdate(Guid backgroundJobId, Guid taskId, TaskExecutionStateType taskExecutionState, TaskCompletionStatusType taskCompletionStatus)
		{
			this[TaskStatusUpdate.BackgroundJobIdProperty] = backgroundJobId;
			this[TaskStatusUpdate.TaskIdProperty] = taskId;
			this[TaskStatusUpdate.TaskExecutionStateProperty] = taskExecutionState;
			this[TaskStatusUpdate.TaskCompletionStatusProperty] = taskCompletionStatus;
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00008F88 File Offset: 0x00007188
		public Guid BackgroundJobId
		{
			get
			{
				return (Guid)this[TaskStatusUpdate.BackgroundJobIdProperty];
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00008F9A File Offset: 0x0000719A
		public Guid TaskId
		{
			get
			{
				return (Guid)this[TaskStatusUpdate.TaskIdProperty];
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00008FAC File Offset: 0x000071AC
		public TaskExecutionStateType TaskExecutionState
		{
			get
			{
				return (TaskExecutionStateType)this[TaskStatusUpdate.TaskExecutionStateProperty];
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00008FBE File Offset: 0x000071BE
		public TaskCompletionStatusType TaskCompletionStatus
		{
			get
			{
				return (TaskCompletionStatusType)this[TaskStatusUpdate.TaskCompletionStatusProperty];
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00008FD0 File Offset: 0x000071D0
		public bool HasEndTime
		{
			get
			{
				return this[TaskStatusUpdate.EndTimeProperty] != null;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00008FE3 File Offset: 0x000071E3
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x00008FF5 File Offset: 0x000071F5
		public DateTime EndTime
		{
			get
			{
				return (DateTime)this[TaskStatusUpdate.EndTimeProperty];
			}
			set
			{
				this[TaskStatusUpdate.EndTimeProperty] = value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00009008 File Offset: 0x00007208
		public bool HasHeartBeat
		{
			get
			{
				return this[TaskStatusUpdate.HeartBeatProperty] != null;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000901B File Offset: 0x0000721B
		// (set) Token: 0x060002AB RID: 683 RVA: 0x0000902D File Offset: 0x0000722D
		public DateTime HeartBeat
		{
			get
			{
				return (DateTime)this[TaskStatusUpdate.HeartBeatProperty];
			}
			set
			{
				this[TaskStatusUpdate.HeartBeatProperty] = value;
			}
		}

		// Token: 0x040001C8 RID: 456
		internal static readonly BackgroundJobBackendPropertyDefinition BackgroundJobIdProperty = ScheduleItemProperties.BackgroundJobIdProperty;

		// Token: 0x040001C9 RID: 457
		internal static readonly BackgroundJobBackendPropertyDefinition TaskIdProperty = TaskItemProperties.TaskIdProperty;

		// Token: 0x040001CA RID: 458
		internal static readonly BackgroundJobBackendPropertyDefinition TaskExecutionStateProperty = TaskItemProperties.TaskExecutionStateProperty;

		// Token: 0x040001CB RID: 459
		internal static readonly BackgroundJobBackendPropertyDefinition TaskCompletionStatusProperty = TaskItemProperties.TaskCompletionStatusProperty;

		// Token: 0x040001CC RID: 460
		internal static readonly BackgroundJobBackendPropertyDefinition HeartBeatProperty = TaskItemProperties.HeartBeatProperty;

		// Token: 0x040001CD RID: 461
		internal static readonly BackgroundJobBackendPropertyDefinition EndTimeProperty = TaskItemProperties.EndTimeProperty;
	}
}
