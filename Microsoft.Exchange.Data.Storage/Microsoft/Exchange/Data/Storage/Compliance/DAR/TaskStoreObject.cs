using System;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Office.CompliancePolicy.Dar;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Exchange.Data.Storage.Compliance.DAR
{
	// Token: 0x02000457 RID: 1111
	[Serializable]
	public class TaskStoreObject : EwsStoreObject, IStoreObject
	{
		// Token: 0x06003155 RID: 12629 RVA: 0x000C9E84 File Offset: 0x000C8084
		public TaskStoreObject()
		{
			this.propertyBag.SetField(EwsStoreObjectSchema.Identity, new EwsStoreObjectId(Guid.NewGuid().ToString()));
		}

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06003156 RID: 12630 RVA: 0x000C9EC0 File Offset: 0x000C80C0
		// (set) Token: 0x06003157 RID: 12631 RVA: 0x000C9ED2 File Offset: 0x000C80D2
		public string Id
		{
			get
			{
				return (string)this[TaskStoreObjectSchema.Id];
			}
			set
			{
				this[TaskStoreObjectSchema.Id] = value;
			}
		}

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06003158 RID: 12632 RVA: 0x000C9EE0 File Offset: 0x000C80E0
		// (set) Token: 0x06003159 RID: 12633 RVA: 0x000C9EF2 File Offset: 0x000C80F2
		public DarTaskCategory Category
		{
			get
			{
				return (DarTaskCategory)((int)this[TaskStoreObjectSchema.Category]);
			}
			set
			{
				this[TaskStoreObjectSchema.Category] = (int)value;
			}
		}

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x0600315A RID: 12634 RVA: 0x000C9F05 File Offset: 0x000C8105
		// (set) Token: 0x0600315B RID: 12635 RVA: 0x000C9F17 File Offset: 0x000C8117
		public int Priority
		{
			get
			{
				return (int)this[TaskStoreObjectSchema.Priority];
			}
			set
			{
				this[TaskStoreObjectSchema.Priority] = value;
			}
		}

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x0600315C RID: 12636 RVA: 0x000C9F2A File Offset: 0x000C812A
		// (set) Token: 0x0600315D RID: 12637 RVA: 0x000C9F3C File Offset: 0x000C813C
		public DarTaskState TaskState
		{
			get
			{
				return (DarTaskState)((int)this[TaskStoreObjectSchema.TaskState]);
			}
			set
			{
				this[TaskStoreObjectSchema.TaskState] = (int)value;
			}
		}

		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x0600315E RID: 12638 RVA: 0x000C9F4F File Offset: 0x000C814F
		// (set) Token: 0x0600315F RID: 12639 RVA: 0x000C9F61 File Offset: 0x000C8161
		public string TaskType
		{
			get
			{
				return (string)this[TaskStoreObjectSchema.TaskType];
			}
			set
			{
				this[TaskStoreObjectSchema.TaskType] = value;
			}
		}

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x06003160 RID: 12640 RVA: 0x000C9F6F File Offset: 0x000C816F
		// (set) Token: 0x06003161 RID: 12641 RVA: 0x000C9F81 File Offset: 0x000C8181
		public string SerializedTaskData
		{
			get
			{
				return (string)this[TaskStoreObjectSchema.SerializedTaskData];
			}
			set
			{
				this[TaskStoreObjectSchema.SerializedTaskData] = value;
			}
		}

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06003162 RID: 12642 RVA: 0x000C9F8F File Offset: 0x000C818F
		// (set) Token: 0x06003163 RID: 12643 RVA: 0x000C9FA1 File Offset: 0x000C81A1
		public byte[] TenantId
		{
			get
			{
				return (byte[])this[TaskStoreObjectSchema.TenantId];
			}
			set
			{
				this[TaskStoreObjectSchema.TenantId] = value;
			}
		}

		// Token: 0x17000F7A RID: 3962
		// (get) Token: 0x06003164 RID: 12644 RVA: 0x000C9FAF File Offset: 0x000C81AF
		// (set) Token: 0x06003165 RID: 12645 RVA: 0x000C9FC1 File Offset: 0x000C81C1
		public DateTime MinTaskScheduleTime
		{
			get
			{
				return (DateTime)this[TaskStoreObjectSchema.MinTaskScheduleTime];
			}
			set
			{
				this[TaskStoreObjectSchema.MinTaskScheduleTime] = value;
			}
		}

		// Token: 0x17000F7B RID: 3963
		// (get) Token: 0x06003166 RID: 12646 RVA: 0x000C9FD4 File Offset: 0x000C81D4
		// (set) Token: 0x06003167 RID: 12647 RVA: 0x000C9FE6 File Offset: 0x000C81E6
		public DateTime TaskCompletionTime
		{
			get
			{
				return (DateTime)this[TaskStoreObjectSchema.TaskCompletionTime];
			}
			set
			{
				this[TaskStoreObjectSchema.TaskCompletionTime] = value;
			}
		}

		// Token: 0x17000F7C RID: 3964
		// (get) Token: 0x06003168 RID: 12648 RVA: 0x000C9FF9 File Offset: 0x000C81F9
		// (set) Token: 0x06003169 RID: 12649 RVA: 0x000CA00B File Offset: 0x000C820B
		public DateTime TaskExecutionStartTime
		{
			get
			{
				return (DateTime)this[TaskStoreObjectSchema.TaskExecutionStartTime];
			}
			set
			{
				this[TaskStoreObjectSchema.TaskExecutionStartTime] = value;
			}
		}

		// Token: 0x17000F7D RID: 3965
		// (get) Token: 0x0600316A RID: 12650 RVA: 0x000CA01E File Offset: 0x000C821E
		// (set) Token: 0x0600316B RID: 12651 RVA: 0x000CA030 File Offset: 0x000C8230
		public DateTime TaskQueuedTime
		{
			get
			{
				return (DateTime)this[TaskStoreObjectSchema.TaskQueuedTime];
			}
			set
			{
				this[TaskStoreObjectSchema.TaskQueuedTime] = value;
			}
		}

		// Token: 0x17000F7E RID: 3966
		// (get) Token: 0x0600316C RID: 12652 RVA: 0x000CA043 File Offset: 0x000C8243
		// (set) Token: 0x0600316D RID: 12653 RVA: 0x000CA055 File Offset: 0x000C8255
		public DateTime TaskScheduledTime
		{
			get
			{
				return (DateTime)this[TaskStoreObjectSchema.TaskScheduledTime];
			}
			set
			{
				this[TaskStoreObjectSchema.TaskScheduledTime] = value;
			}
		}

		// Token: 0x17000F7F RID: 3967
		// (get) Token: 0x0600316E RID: 12654 RVA: 0x000CA068 File Offset: 0x000C8268
		// (set) Token: 0x0600316F RID: 12655 RVA: 0x000CA07F File Offset: 0x000C827F
		public TimeSpan TaskRetryInterval
		{
			get
			{
				return TimeSpan.Parse((string)this[TaskStoreObjectSchema.TaskRetryInterval]);
			}
			set
			{
				this[TaskStoreObjectSchema.TaskRetryInterval] = value.ToString();
			}
		}

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x06003170 RID: 12656 RVA: 0x000CA099 File Offset: 0x000C8299
		// (set) Token: 0x06003171 RID: 12657 RVA: 0x000CA0AB File Offset: 0x000C82AB
		public int TaskRetryCurrentCount
		{
			get
			{
				return (int)this[TaskStoreObjectSchema.TaskRetryCurrentCount];
			}
			set
			{
				this[TaskStoreObjectSchema.TaskRetryCurrentCount] = value;
			}
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06003172 RID: 12658 RVA: 0x000CA0BE File Offset: 0x000C82BE
		// (set) Token: 0x06003173 RID: 12659 RVA: 0x000CA0D0 File Offset: 0x000C82D0
		public int TaskRetryTotalCount
		{
			get
			{
				return (int)this[TaskStoreObjectSchema.TaskRetryTotalCount];
			}
			set
			{
				this[TaskStoreObjectSchema.TaskRetryTotalCount] = value;
			}
		}

		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x06003174 RID: 12660 RVA: 0x000CA0E3 File Offset: 0x000C82E3
		// (set) Token: 0x06003175 RID: 12661 RVA: 0x000CA0F5 File Offset: 0x000C82F5
		public TaskSynchronizationOption TaskSynchronizationOption
		{
			get
			{
				return (TaskSynchronizationOption)((int)this[TaskStoreObjectSchema.TaskSynchronizationOption]);
			}
			set
			{
				this[TaskStoreObjectSchema.TaskSynchronizationOption] = (int)value;
			}
		}

		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06003176 RID: 12662 RVA: 0x000CA108 File Offset: 0x000C8308
		// (set) Token: 0x06003177 RID: 12663 RVA: 0x000CA11A File Offset: 0x000C831A
		public string TaskSynchronizationKey
		{
			get
			{
				return (string)this[TaskStoreObjectSchema.TaskSynchronizationKey];
			}
			set
			{
				this[TaskStoreObjectSchema.TaskSynchronizationKey] = value;
			}
		}

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06003178 RID: 12664 RVA: 0x000CA128 File Offset: 0x000C8328
		// (set) Token: 0x06003179 RID: 12665 RVA: 0x000CA13A File Offset: 0x000C833A
		public string ExecutionContainer
		{
			get
			{
				return (string)this[TaskStoreObjectSchema.ExecutionContainer];
			}
			set
			{
				this[TaskStoreObjectSchema.ExecutionContainer] = value;
			}
		}

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x0600317A RID: 12666 RVA: 0x000CA148 File Offset: 0x000C8348
		// (set) Token: 0x0600317B RID: 12667 RVA: 0x000CA15A File Offset: 0x000C835A
		public string ExecutionTarget
		{
			get
			{
				return (string)this[TaskStoreObjectSchema.ExecutionTarget];
			}
			set
			{
				this[TaskStoreObjectSchema.ExecutionTarget] = value;
			}
		}

		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x0600317C RID: 12668 RVA: 0x000CA168 File Offset: 0x000C8368
		// (set) Token: 0x0600317D RID: 12669 RVA: 0x000CA17A File Offset: 0x000C837A
		public DateTime ExecutionLockExpiryTime
		{
			get
			{
				return (DateTime)this[TaskStoreObjectSchema.ExecutionLockExpiryTime];
			}
			set
			{
				this[TaskStoreObjectSchema.ExecutionLockExpiryTime] = value;
			}
		}

		// Token: 0x17000F87 RID: 3975
		// (get) Token: 0x0600317E RID: 12670 RVA: 0x000CA18D File Offset: 0x000C838D
		public DateTime LastModifiedTime
		{
			get
			{
				return (DateTime)this[TaskAggregateStoreObjectSchema.LastModifiedTime];
			}
		}

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x0600317F RID: 12671 RVA: 0x000CA19F File Offset: 0x000C839F
		// (set) Token: 0x06003180 RID: 12672 RVA: 0x000CA1B1 File Offset: 0x000C83B1
		public int SchemaVersion
		{
			get
			{
				return (int)this[TaskStoreObjectSchema.SchemaVersion];
			}
			set
			{
				this[TaskStoreObjectSchema.SchemaVersion] = value;
			}
		}

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x06003181 RID: 12673 RVA: 0x000CA1C4 File Offset: 0x000C83C4
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06003182 RID: 12674 RVA: 0x000CA1CB File Offset: 0x000C83CB
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return TaskStoreObject.schema;
			}
		}

		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x06003183 RID: 12675 RVA: 0x000CA1D2 File Offset: 0x000C83D2
		internal override string ItemClass
		{
			get
			{
				return "IPM.Configuration.DarTask";
			}
		}

		// Token: 0x06003184 RID: 12676 RVA: 0x000CA1DC File Offset: 0x000C83DC
		internal static TaskStoreObject FromDarTask(DarTask task, StoreObjectProvider objectProvider)
		{
			if (task == null)
			{
				throw new ArgumentNullException("task");
			}
			if (objectProvider == null)
			{
				throw new ArgumentNullException("objectProvider");
			}
			TaskStoreObject taskStoreObject = task.WorkloadContext as TaskStoreObject;
			if (taskStoreObject == null)
			{
				taskStoreObject = objectProvider.FindByAlternativeId<TaskStoreObject>(task.Id);
				if (taskStoreObject == null)
				{
					taskStoreObject = new TaskStoreObject();
				}
			}
			if (taskStoreObject != null)
			{
				taskStoreObject.UpdateFromDarTask(task);
			}
			return taskStoreObject;
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x000CA238 File Offset: 0x000C8438
		internal static TaskStoreObject FromExistingObject(TaskStoreObject storeObject, StoreObjectProvider objectProvider)
		{
			TaskStoreObject taskStoreObject = objectProvider.FindByAlternativeId<TaskStoreObject>(storeObject.Id);
			taskStoreObject.Id = storeObject.Id;
			taskStoreObject.Category = storeObject.Category;
			taskStoreObject.Priority = storeObject.Priority;
			taskStoreObject.TaskSynchronizationKey = Helper.ToDefaultString(storeObject.TaskSynchronizationKey, null);
			taskStoreObject.TaskSynchronizationOption = storeObject.TaskSynchronizationOption;
			taskStoreObject.TaskType = Helper.ToDefaultString(storeObject.TaskType, null);
			taskStoreObject.TenantId = storeObject.TenantId;
			taskStoreObject.TaskState = storeObject.TaskState;
			taskStoreObject.TaskQueuedTime = storeObject.TaskQueuedTime;
			taskStoreObject.MinTaskScheduleTime = storeObject.MinTaskScheduleTime;
			taskStoreObject.TaskScheduledTime = storeObject.TaskScheduledTime;
			taskStoreObject.TaskExecutionStartTime = storeObject.TaskExecutionStartTime;
			taskStoreObject.TaskCompletionTime = storeObject.TaskCompletionTime;
			taskStoreObject.TaskRetryTotalCount = storeObject.TaskRetryTotalCount;
			taskStoreObject.TaskRetryInterval = storeObject.TaskRetryInterval;
			taskStoreObject.TaskRetryCurrentCount = storeObject.TaskRetryCurrentCount;
			taskStoreObject.SerializedTaskData = Helper.ToDefaultString(storeObject.SerializedTaskData, null);
			return taskStoreObject;
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x000CA334 File Offset: 0x000C8534
		public DarTask ToDarTask(DarServiceProvider serviceProvider)
		{
			if (serviceProvider == null)
			{
				throw new ArgumentNullException("serviceProvider");
			}
			DarTask darTask = serviceProvider.DarTaskFactory.CreateTask(this.TaskType);
			this.UpdateDarTask(darTask);
			return darTask;
		}

		// Token: 0x06003187 RID: 12679 RVA: 0x000CA36C File Offset: 0x000C856C
		public void UpdateDarTask(DarTask task)
		{
			task.Id = this.Id;
			task.Category = this.Category;
			task.Priority = this.Priority;
			task.TaskSynchronizationKey = this.TaskSynchronizationKey;
			task.TaskSynchronizationOption = this.TaskSynchronizationOption;
			task.TenantId = Convert.ToBase64String(this.TenantId);
			task.TaskState = this.TaskState;
			task.TaskQueuedTime = this.TaskQueuedTime;
			task.MinTaskScheduleTime = this.MinTaskScheduleTime;
			task.TaskScheduledTime = this.TaskScheduledTime;
			task.TaskExecutionStartTime = this.TaskExecutionStartTime;
			task.TaskCompletionTime = this.TaskCompletionTime;
			task.TaskRetryTotalCount = this.TaskRetryTotalCount;
			task.TaskRetryInterval = this.TaskRetryInterval;
			task.TaskRetryCurrentCount = this.TaskRetryCurrentCount;
			task.SerializedTaskData = this.SerializedTaskData;
			task.WorkloadContext = this;
		}

		// Token: 0x06003188 RID: 12680 RVA: 0x000CA448 File Offset: 0x000C8648
		public void UpdateFromDarTask(DarTask task)
		{
			if (task == null)
			{
				throw new ArgumentNullException("task");
			}
			if (task.WorkloadContext != null && task.WorkloadContext != this)
			{
				throw new InvalidOperationException("Task is bound to another raw object use it to perform udpate");
			}
			this.Id = Helper.ToDefaultString(task.Id, null);
			this.Category = task.Category;
			this.Priority = task.Priority;
			this.TaskSynchronizationKey = Helper.ToDefaultString(task.TaskSynchronizationKey, null);
			this.TaskSynchronizationOption = task.TaskSynchronizationOption;
			this.TaskType = Helper.ToDefaultString(task.TaskType, null);
			this.TenantId = Convert.FromBase64String(task.TenantId);
			this.TaskState = task.TaskState;
			this.TaskQueuedTime = task.TaskQueuedTime;
			this.MinTaskScheduleTime = task.MinTaskScheduleTime;
			this.TaskScheduledTime = task.TaskScheduledTime;
			this.TaskExecutionStartTime = task.TaskExecutionStartTime;
			this.TaskCompletionTime = task.TaskCompletionTime;
			this.TaskRetryTotalCount = task.TaskRetryTotalCount;
			this.TaskRetryInterval = task.TaskRetryInterval;
			this.TaskRetryCurrentCount = task.TaskRetryCurrentCount;
			this.SerializedTaskData = Helper.ToDefaultString(task.SerializedTaskData, null);
			task.WorkloadContext = this;
		}

		// Token: 0x06003189 RID: 12681 RVA: 0x000CA570 File Offset: 0x000C8770
		public override string ToString()
		{
			return string.Format("Id:{0}, Tenant:{1}, Type:{2}, State:{3}, Retries:{4}", new object[]
			{
				this.Id,
				this.TenantId,
				this.TaskType,
				this.TaskState,
				this.TaskRetryCurrentCount
			});
		}

		// Token: 0x04001AAA RID: 6826
		public const string ObjectClass = "IPM.Configuration.DarTask";

		// Token: 0x04001AAB RID: 6827
		private static readonly TaskStoreObjectSchema schema = new TaskStoreObjectSchema();
	}
}
