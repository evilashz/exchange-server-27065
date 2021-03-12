using System;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Office.CompliancePolicy.Dar;

namespace Microsoft.Exchange.Data.Storage.Compliance.DAR
{
	// Token: 0x0200045B RID: 1115
	[Serializable]
	public class TaskAggregateStoreObject : EwsStoreObject, IStoreObject
	{
		// Token: 0x06003196 RID: 12694 RVA: 0x000CAB48 File Offset: 0x000C8D48
		public TaskAggregateStoreObject()
		{
			this.propertyBag.SetField(EwsStoreObjectSchema.Identity, new EwsStoreObjectId(Guid.NewGuid().ToString()));
		}

		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x06003197 RID: 12695 RVA: 0x000CAB84 File Offset: 0x000C8D84
		// (set) Token: 0x06003198 RID: 12696 RVA: 0x000CAB96 File Offset: 0x000C8D96
		public string Id
		{
			get
			{
				return (string)this[TaskAggregateStoreObjectSchema.Id];
			}
			set
			{
				this[TaskAggregateStoreObjectSchema.Id] = value;
			}
		}

		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x06003199 RID: 12697 RVA: 0x000CABA4 File Offset: 0x000C8DA4
		// (set) Token: 0x0600319A RID: 12698 RVA: 0x000CABB6 File Offset: 0x000C8DB6
		public byte[] ScopeId
		{
			get
			{
				return (byte[])this[TaskAggregateStoreObjectSchema.ScopeId];
			}
			set
			{
				this[TaskAggregateStoreObjectSchema.ScopeId] = value;
			}
		}

		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x0600319B RID: 12699 RVA: 0x000CABC4 File Offset: 0x000C8DC4
		// (set) Token: 0x0600319C RID: 12700 RVA: 0x000CABD6 File Offset: 0x000C8DD6
		public string TaskType
		{
			get
			{
				return (string)this[TaskAggregateStoreObjectSchema.TaskType];
			}
			set
			{
				this[TaskAggregateStoreObjectSchema.TaskType] = value;
			}
		}

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x0600319D RID: 12701 RVA: 0x000CABE4 File Offset: 0x000C8DE4
		// (set) Token: 0x0600319E RID: 12702 RVA: 0x000CABF6 File Offset: 0x000C8DF6
		public bool Enabled
		{
			get
			{
				return (bool)this[TaskAggregateStoreObjectSchema.Enabled];
			}
			set
			{
				this[TaskAggregateStoreObjectSchema.Enabled] = value;
			}
		}

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x0600319F RID: 12703 RVA: 0x000CAC09 File Offset: 0x000C8E09
		// (set) Token: 0x060031A0 RID: 12704 RVA: 0x000CAC1B File Offset: 0x000C8E1B
		public int MaxRunningTasks
		{
			get
			{
				return (int)this[TaskAggregateStoreObjectSchema.MaxRunningTasks];
			}
			set
			{
				this[TaskAggregateStoreObjectSchema.MaxRunningTasks] = value;
			}
		}

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x060031A1 RID: 12705 RVA: 0x000CAC2E File Offset: 0x000C8E2E
		// (set) Token: 0x060031A2 RID: 12706 RVA: 0x000CAC40 File Offset: 0x000C8E40
		public RecurrenceType RecurrenceType
		{
			get
			{
				return (RecurrenceType)((int)this[TaskAggregateStoreObjectSchema.RecurrenceType]);
			}
			set
			{
				this[TaskAggregateStoreObjectSchema.RecurrenceType] = (int)value;
			}
		}

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x060031A3 RID: 12707 RVA: 0x000CAC53 File Offset: 0x000C8E53
		// (set) Token: 0x060031A4 RID: 12708 RVA: 0x000CAC65 File Offset: 0x000C8E65
		public RecurrenceFrequency RecurrenceFrequency
		{
			get
			{
				return (RecurrenceFrequency)((int)this[TaskAggregateStoreObjectSchema.RecurrenceFrequency]);
			}
			set
			{
				this[TaskAggregateStoreObjectSchema.RecurrenceFrequency] = (int)value;
			}
		}

		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x060031A5 RID: 12709 RVA: 0x000CAC78 File Offset: 0x000C8E78
		// (set) Token: 0x060031A6 RID: 12710 RVA: 0x000CAC8A File Offset: 0x000C8E8A
		public int RecurrenceInterval
		{
			get
			{
				return (int)this[TaskAggregateStoreObjectSchema.RecurrenceInterval];
			}
			set
			{
				this[TaskAggregateStoreObjectSchema.RecurrenceInterval] = value;
			}
		}

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x060031A7 RID: 12711 RVA: 0x000CAC9D File Offset: 0x000C8E9D
		// (set) Token: 0x060031A8 RID: 12712 RVA: 0x000CACAF File Offset: 0x000C8EAF
		public int SchemaVersion
		{
			get
			{
				return (int)this[TaskAggregateStoreObjectSchema.SchemaVersion];
			}
			set
			{
				this[TaskAggregateStoreObjectSchema.SchemaVersion] = value;
			}
		}

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x060031A9 RID: 12713 RVA: 0x000CACC2 File Offset: 0x000C8EC2
		public DateTime LastModifiedTime
		{
			get
			{
				return (DateTime)this[TaskAggregateStoreObjectSchema.LastModifiedTime];
			}
		}

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x060031AA RID: 12714 RVA: 0x000CACD4 File Offset: 0x000C8ED4
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x060031AB RID: 12715 RVA: 0x000CACDB File Offset: 0x000C8EDB
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return TaskAggregateStoreObject.schema;
			}
		}

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x060031AC RID: 12716 RVA: 0x000CACE2 File Offset: 0x000C8EE2
		internal override string ItemClass
		{
			get
			{
				return "IPM.Configuration.DarTaskAggregate";
			}
		}

		// Token: 0x060031AD RID: 12717 RVA: 0x000CACEC File Offset: 0x000C8EEC
		internal static TaskAggregateStoreObject FromDarTaskAggregate(DarTaskAggregate taskAggregate, StoreObjectProvider objectProvider)
		{
			if (taskAggregate == null)
			{
				throw new ArgumentNullException("taskAggregate");
			}
			if (objectProvider == null)
			{
				throw new ArgumentNullException("objectProvider");
			}
			TaskAggregateStoreObject taskAggregateStoreObject = taskAggregate.WorkloadContext as TaskAggregateStoreObject;
			if (taskAggregateStoreObject == null)
			{
				taskAggregateStoreObject = objectProvider.FindByAlternativeId<TaskAggregateStoreObject>(taskAggregate.Id);
				if (taskAggregateStoreObject == null)
				{
					taskAggregateStoreObject = new TaskAggregateStoreObject();
				}
			}
			if (taskAggregateStoreObject != null)
			{
				taskAggregateStoreObject.UpdateFromDarTaskAggregate(taskAggregate);
			}
			return taskAggregateStoreObject;
		}

		// Token: 0x060031AE RID: 12718 RVA: 0x000CAD48 File Offset: 0x000C8F48
		internal static TaskAggregateStoreObject FromExistingObject(TaskAggregateStoreObject storeObject, StoreObjectProvider objectProvider)
		{
			TaskAggregateStoreObject taskAggregateStoreObject = objectProvider.FindByAlternativeId<TaskAggregateStoreObject>(storeObject.Id);
			taskAggregateStoreObject.Id = storeObject.Id;
			taskAggregateStoreObject.Enabled = storeObject.Enabled;
			taskAggregateStoreObject.TaskType = storeObject.TaskType;
			taskAggregateStoreObject.ScopeId = storeObject.ScopeId;
			taskAggregateStoreObject.MaxRunningTasks = storeObject.MaxRunningTasks;
			taskAggregateStoreObject.RecurrenceType = storeObject.RecurrenceType;
			taskAggregateStoreObject.RecurrenceFrequency = storeObject.RecurrenceFrequency;
			taskAggregateStoreObject.RecurrenceInterval = storeObject.RecurrenceInterval;
			return taskAggregateStoreObject;
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x000CADC4 File Offset: 0x000C8FC4
		public DarTaskAggregate ToDarTaskAggregate(DarServiceProvider serviceProvider)
		{
			if (serviceProvider == null)
			{
				throw new ArgumentNullException("serviceProvider");
			}
			DarTaskAggregate darTaskAggregate = serviceProvider.DarTaskFactory.CreateTaskAggregate(this.TaskType);
			this.UpdateDarTaskAggregate(darTaskAggregate);
			return darTaskAggregate;
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x000CADFC File Offset: 0x000C8FFC
		public void UpdateDarTaskAggregate(DarTaskAggregate taskAggregate)
		{
			taskAggregate.Id = this.Id;
			taskAggregate.Enabled = this.Enabled;
			taskAggregate.TaskType = this.TaskType;
			taskAggregate.ScopeId = Convert.ToBase64String(this.ScopeId);
			taskAggregate.MaxRunningTasks = this.MaxRunningTasks;
			taskAggregate.RecurrenceType = this.RecurrenceType;
			taskAggregate.RecurrenceFrequency = this.RecurrenceFrequency;
			taskAggregate.RecurrenceInterval = this.RecurrenceInterval;
			taskAggregate.WorkloadContext = this;
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x000CAE78 File Offset: 0x000C9078
		public void UpdateFromDarTaskAggregate(DarTaskAggregate taskAggregate)
		{
			if (taskAggregate == null)
			{
				throw new ArgumentNullException("task");
			}
			if (taskAggregate.WorkloadContext != null && taskAggregate.WorkloadContext != this)
			{
				throw new InvalidOperationException("Task is bound to another raw object use it to perform udpate");
			}
			this.Id = Helper.ToDefaultString(taskAggregate.Id, null);
			this.Enabled = taskAggregate.Enabled;
			this.TaskType = Helper.ToDefaultString(taskAggregate.TaskType, null);
			this.ScopeId = Convert.FromBase64String(taskAggregate.ScopeId);
			this.MaxRunningTasks = taskAggregate.MaxRunningTasks;
			this.RecurrenceType = taskAggregate.RecurrenceType;
			this.RecurrenceFrequency = taskAggregate.RecurrenceFrequency;
			this.RecurrenceInterval = taskAggregate.RecurrenceInterval;
			taskAggregate.WorkloadContext = this;
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x000CAF28 File Offset: 0x000C9128
		public override string ToString()
		{
			return string.Format("Id:{0}, Tenant:{1}, Type:{2}, Max:{3}", new object[]
			{
				this.Id,
				this.ScopeId,
				this.TaskType,
				this.MaxRunningTasks
			});
		}

		// Token: 0x04001ADF RID: 6879
		public const string ObjectClass = "IPM.Configuration.DarTaskAggregate";

		// Token: 0x04001AE0 RID: 6880
		private static readonly TaskAggregateStoreObjectSchema schema = new TaskAggregateStoreObjectSchema();
	}
}
