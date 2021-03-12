using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Office.CompliancePolicy.Dar;
using Microsoft.Office.CompliancePolicy.Exchange.Dar.Diagnostics;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Office.CompliancePolicy.Exchange.Dar.Execution
{
	// Token: 0x0200000D RID: 13
	internal class Scheduler : SystemWorkloadBase
	{
		// Token: 0x06000072 RID: 114 RVA: 0x0000360C File Offset: 0x0000180C
		public Scheduler(InstanceManager instance)
		{
			this.provider = new ExDarServiceProvider();
			this.taskManager = new DarTaskManager(this.provider);
			this.prioritizedCategories = from DarTaskCategory t in Enum.GetValues(typeof(DarTaskCategory))
			orderby (int)t descending
			select t;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003682 File Offset: 0x00001882
		public override int BlockedTaskCount
		{
			get
			{
				return InstanceManager.Current.GetActiveTaskList(null).Count<DarTask>() - InstanceManager.Current.GetReadyTaskList().Count<DarTask>();
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000036A4 File Offset: 0x000018A4
		public override string Id
		{
			get
			{
				return WorkloadType.DarRuntime.ToString();
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000036B2 File Offset: 0x000018B2
		public override int TaskCount
		{
			get
			{
				return InstanceManager.Current.GetReadyTaskList().Count<DarTask>();
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000036C3 File Offset: 0x000018C3
		public override WorkloadType WorkloadType
		{
			get
			{
				return WorkloadType.DarRuntime;
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000036C7 File Offset: 0x000018C7
		public void Start()
		{
			if (SystemWorkloadManager.Status == WorkloadExecutionStatus.NotInitialized)
			{
				SystemWorkloadManager.Initialize(this.provider.ExecutionLog as IWorkloadLogger);
			}
			SystemWorkloadManager.RegisterWorkload(this);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000036EB File Offset: 0x000018EB
		public void Stop()
		{
			SystemWorkloadManager.UnregisterWorkload(this);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000036F4 File Offset: 0x000018F4
		protected override SystemTaskBase GetTask(ResourceReservationContext context)
		{
			SystemTaskBase result;
			lock (this.instanceLock)
			{
				foreach (DarTaskCategory category in this.prioritizedCategories)
				{
					DarTask darTask = this.taskManager.Dequeue(1, category, context).FirstOrDefault<DarTask>();
					if (darTask != null)
					{
						ResourceKey resourceKey;
						ResourceReservation resourceReservation = this.GetResourceReservation(darTask, context, out resourceKey);
						if (resourceReservation != null)
						{
							darTask.TaskState = DarTaskState.Running;
							return new TaskWrapper(darTask, this.taskManager, this, resourceReservation);
						}
						LogItem.Publish("Scheduler", "ResourcesNotReseved", ("Throttled resource: " + resourceKey != null) ? resourceKey.ToString() : "null", darTask.CorrelationId, ResultSeverityLevel.Warning);
					}
				}
				base.Pause();
				result = null;
			}
			return result;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000037E8 File Offset: 0x000019E8
		private ResourceReservation GetResourceReservation(DarTask task, ResourceReservationContext reservationContext, out ResourceKey throttledResource)
		{
			if (reservationContext == null)
			{
				throw new ArgumentNullException("reservationContext");
			}
			string taskType = task.TaskType;
			ResourceKey[] resources = new ResourceKey[]
			{
				ProcessorResourceKey.Local
			};
			return reservationContext.GetReservation(this, resources, out throttledResource);
		}

		// Token: 0x04000027 RID: 39
		private ExDarServiceProvider provider;

		// Token: 0x04000028 RID: 40
		private DarTaskManager taskManager;

		// Token: 0x04000029 RID: 41
		private IEnumerable<DarTaskCategory> prioritizedCategories;

		// Token: 0x0400002A RID: 42
		private object instanceLock = new object();
	}
}
