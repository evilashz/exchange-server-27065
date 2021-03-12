using System;

namespace Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend
{
	// Token: 0x02000044 RID: 68
	internal sealed class TaskItem : BackgroundJobBackendBase
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000278 RID: 632 RVA: 0x00008984 File Offset: 0x00006B84
		// (set) Token: 0x06000279 RID: 633 RVA: 0x00008996 File Offset: 0x00006B96
		public Guid ActiveJobId
		{
			get
			{
				return (Guid)this[TaskItem.ActiveJobIdProperty];
			}
			set
			{
				this[TaskItem.ActiveJobIdProperty] = value;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600027A RID: 634 RVA: 0x000089A9 File Offset: 0x00006BA9
		// (set) Token: 0x0600027B RID: 635 RVA: 0x000089BB File Offset: 0x00006BBB
		public Guid TaskId
		{
			get
			{
				return (Guid)this[TaskItem.TaskIdProperty];
			}
			set
			{
				this[TaskItem.TaskIdProperty] = value;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600027C RID: 636 RVA: 0x000089CE File Offset: 0x00006BCE
		// (set) Token: 0x0600027D RID: 637 RVA: 0x000089E0 File Offset: 0x00006BE0
		public Guid ScheduleId
		{
			get
			{
				return (Guid)this[TaskItem.ScheduleIdProperty];
			}
			set
			{
				this[TaskItem.ScheduleIdProperty] = value;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600027E RID: 638 RVA: 0x000089F3 File Offset: 0x00006BF3
		// (set) Token: 0x0600027F RID: 639 RVA: 0x00008A05 File Offset: 0x00006C05
		public Guid BackgroundJobId
		{
			get
			{
				return (Guid)this[TaskItem.BackgroundJobIdProperty];
			}
			set
			{
				this[TaskItem.BackgroundJobIdProperty] = value;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00008A18 File Offset: 0x00006C18
		// (set) Token: 0x06000281 RID: 641 RVA: 0x00008A2A File Offset: 0x00006C2A
		public Guid RoleId
		{
			get
			{
				return (Guid)this[TaskItem.RoleIdProperty];
			}
			set
			{
				this[TaskItem.RoleIdProperty] = value;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000282 RID: 642 RVA: 0x00008A3D File Offset: 0x00006C3D
		// (set) Token: 0x06000283 RID: 643 RVA: 0x00008A4F File Offset: 0x00006C4F
		public int InstanceId
		{
			get
			{
				return (int)this[TaskItem.InstanceIdProperty];
			}
			set
			{
				this[TaskItem.InstanceIdProperty] = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00008A62 File Offset: 0x00006C62
		// (set) Token: 0x06000285 RID: 645 RVA: 0x00008A74 File Offset: 0x00006C74
		public TaskExecutionStateType TaskExecutionState
		{
			get
			{
				return (TaskExecutionStateType)this[TaskItem.TaskExecutionStateProperty];
			}
			set
			{
				this[TaskItem.TaskExecutionStateProperty] = (byte)value;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00008A87 File Offset: 0x00006C87
		public bool HasTaskCompletionStatus
		{
			get
			{
				return this[TaskItem.TaskCompletionStatusProperty] != null;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00008A9A File Offset: 0x00006C9A
		// (set) Token: 0x06000288 RID: 648 RVA: 0x00008AAC File Offset: 0x00006CAC
		public TaskCompletionStatusType TaskCompletionStatus
		{
			get
			{
				return (TaskCompletionStatusType)this[TaskItem.TaskCompletionStatusProperty];
			}
			set
			{
				this[TaskItem.TaskCompletionStatusProperty] = (byte)value;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00008ABF File Offset: 0x00006CBF
		// (set) Token: 0x0600028A RID: 650 RVA: 0x00008AD1 File Offset: 0x00006CD1
		public Guid ParentTaskId
		{
			get
			{
				return (Guid)this[TaskItem.ParentTaskIdProperty];
			}
			set
			{
				this[TaskItem.ParentTaskIdProperty] = value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00008AE4 File Offset: 0x00006CE4
		// (set) Token: 0x0600028C RID: 652 RVA: 0x00008AF6 File Offset: 0x00006CF6
		public short ExecutionAttempt
		{
			get
			{
				return (short)this[TaskItem.ExecutionAttemptProperty];
			}
			set
			{
				this[TaskItem.ExecutionAttemptProperty] = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00008B09 File Offset: 0x00006D09
		public bool HasBJMOwnerId
		{
			get
			{
				return this[TaskItem.BJMOwnerIdProperty] != null;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00008B1C File Offset: 0x00006D1C
		// (set) Token: 0x0600028F RID: 655 RVA: 0x00008B2E File Offset: 0x00006D2E
		public Guid BJMOwnerId
		{
			get
			{
				return (Guid)this[TaskItem.BJMOwnerIdProperty];
			}
			set
			{
				this[TaskItem.BJMOwnerIdProperty] = value;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00008B41 File Offset: 0x00006D41
		public string TargetMachineName
		{
			get
			{
				return (string)this[TaskItem.TargetMachineNameProperty];
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00008B53 File Offset: 0x00006D53
		public bool HasOwnerFitnessScore
		{
			get
			{
				return this[TaskItem.OwnerFitnessScoreProperty] != null;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00008B66 File Offset: 0x00006D66
		// (set) Token: 0x06000293 RID: 659 RVA: 0x00008B78 File Offset: 0x00006D78
		public int OwnerFitnessScore
		{
			get
			{
				return (int)this[TaskItem.OwnerFitnessScoreProperty];
			}
			set
			{
				this[TaskItem.OwnerFitnessScoreProperty] = value;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000294 RID: 660 RVA: 0x00008B8B File Offset: 0x00006D8B
		public bool HasStartTime
		{
			get
			{
				return this[TaskItem.StartTimeProperty] != null;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00008B9E File Offset: 0x00006D9E
		// (set) Token: 0x06000296 RID: 662 RVA: 0x00008BB0 File Offset: 0x00006DB0
		public DateTime StartTime
		{
			get
			{
				return (DateTime)this[TaskItem.StartTimeProperty];
			}
			set
			{
				this[TaskItem.StartTimeProperty] = value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00008BC3 File Offset: 0x00006DC3
		public bool HasEndTime
		{
			get
			{
				return this[TaskItem.EndTimeProperty] != null;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00008BD6 File Offset: 0x00006DD6
		// (set) Token: 0x06000299 RID: 665 RVA: 0x00008BE8 File Offset: 0x00006DE8
		public DateTime EndTime
		{
			get
			{
				return (DateTime)this[TaskItem.EndTimeProperty];
			}
			set
			{
				this[TaskItem.EndTimeProperty] = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00008BFB File Offset: 0x00006DFB
		public bool HasHeartBeat
		{
			get
			{
				return this[TaskItem.HeartBeatProperty] != null;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00008C0E File Offset: 0x00006E0E
		// (set) Token: 0x0600029C RID: 668 RVA: 0x00008C20 File Offset: 0x00006E20
		public DateTime HeartBeat
		{
			get
			{
				return (DateTime)this[TaskItem.HeartBeatProperty];
			}
			set
			{
				this[TaskItem.HeartBeatProperty] = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00008C33 File Offset: 0x00006E33
		// (set) Token: 0x0600029E RID: 670 RVA: 0x00008C45 File Offset: 0x00006E45
		public DateTime InsertTimeStamp
		{
			get
			{
				return (DateTime)this[TaskItem.InsertTimeStampProperty];
			}
			set
			{
				this[TaskItem.InsertTimeStampProperty] = value;
			}
		}

		// Token: 0x040001A7 RID: 423
		internal static readonly BackgroundJobBackendPropertyDefinition ActiveJobIdProperty = TaskItemProperties.ActiveJobIdProperty;

		// Token: 0x040001A8 RID: 424
		internal static readonly BackgroundJobBackendPropertyDefinition TaskIdProperty = TaskItemProperties.TaskIdProperty;

		// Token: 0x040001A9 RID: 425
		internal static readonly BackgroundJobBackendPropertyDefinition ScheduleIdProperty = TaskItemProperties.ScheduleIdProperty;

		// Token: 0x040001AA RID: 426
		internal static readonly BackgroundJobBackendPropertyDefinition BackgroundJobIdProperty = TaskItemProperties.BackgroundJobIdProperty;

		// Token: 0x040001AB RID: 427
		internal static readonly BackgroundJobBackendPropertyDefinition RoleIdProperty = TaskItemProperties.RoleIdProperty;

		// Token: 0x040001AC RID: 428
		internal static readonly BackgroundJobBackendPropertyDefinition InstanceIdProperty = TaskItemProperties.InstanceIdProperty;

		// Token: 0x040001AD RID: 429
		internal static readonly BackgroundJobBackendPropertyDefinition TaskExecutionStateProperty = TaskItemProperties.TaskExecutionStateProperty;

		// Token: 0x040001AE RID: 430
		internal static readonly BackgroundJobBackendPropertyDefinition TaskCompletionStatusProperty = TaskItemProperties.TaskCompletionStatusProperty;

		// Token: 0x040001AF RID: 431
		internal static readonly BackgroundJobBackendPropertyDefinition ParentTaskIdProperty = TaskItemProperties.ParentTaskIdProperty;

		// Token: 0x040001B0 RID: 432
		internal static readonly BackgroundJobBackendPropertyDefinition TargetMachineNameProperty = ScheduleItemProperties.TargetMachineNameProperty;

		// Token: 0x040001B1 RID: 433
		internal static readonly BackgroundJobBackendPropertyDefinition ExecutionAttemptProperty = TaskItemProperties.ExecutionAttemptProperty;

		// Token: 0x040001B2 RID: 434
		internal static readonly BackgroundJobBackendPropertyDefinition BJMOwnerIdProperty = TaskItemProperties.BJMOwnerIdProperty;

		// Token: 0x040001B3 RID: 435
		internal static readonly BackgroundJobBackendPropertyDefinition OwnerFitnessScoreProperty = TaskItemProperties.OwnerFitnessScoreProperty;

		// Token: 0x040001B4 RID: 436
		internal static readonly BackgroundJobBackendPropertyDefinition StartTimeProperty = TaskItemProperties.StartTimeProperty;

		// Token: 0x040001B5 RID: 437
		internal static readonly BackgroundJobBackendPropertyDefinition EndTimeProperty = TaskItemProperties.EndTimeProperty;

		// Token: 0x040001B6 RID: 438
		internal static readonly BackgroundJobBackendPropertyDefinition HeartBeatProperty = TaskItemProperties.HeartBeatProperty;

		// Token: 0x040001B7 RID: 439
		internal static readonly BackgroundJobBackendPropertyDefinition InsertTimeStampProperty = TaskItemProperties.InsertTimeStampProperty;
	}
}
