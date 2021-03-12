using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage.Compliance.DAR;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Office.CompliancePolicy.Dar;
using Microsoft.Office.CompliancePolicy.Exchange.Dar.Diagnostics;
using Microsoft.Office.CompliancePolicy.Exchange.Dar.Execution;
using Microsoft.Office.CompliancePolicy.Exchange.Dar.Utility;

namespace Microsoft.Office.CompliancePolicy.Exchange.Dar
{
	// Token: 0x02000005 RID: 5
	internal class ExDarTaskQueue : DarTaskQueue
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000022DC File Offset: 0x000004DC
		public ExDarTaskQueue(DarServiceProvider provider)
		{
			this.provider = provider;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022EC File Offset: 0x000004EC
		public override void DeleteCompletedTask(DateTime maxCompletionTime, string taskType = null, string tenantId = null)
		{
			if (tenantId == null)
			{
				throw new ArgumentNullException("tenantId must be specified for exchange workload");
			}
			SearchFilter completedTaskFilter = TaskHelper.GetCompletedTaskFilter(taskType, null, new DateTime?(maxCompletionTime), null);
			TenantStore.DeleteTasks(tenantId, completedTaskFilter, OperationContext.CorrelationId);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000232C File Offset: 0x0000052C
		public override void DeleteTask(DarTask task)
		{
			if (task == null)
			{
				throw new ArgumentNullException("Task must be specified to delete");
			}
			SearchFilter completedTaskFilter = TaskHelper.GetCompletedTaskFilter(null, null, null, task.Id);
			TenantStore.DeleteTasks(task.TenantId, completedTaskFilter, OperationContext.CorrelationId);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002388 File Offset: 0x00000588
		public override IEnumerable<DarTask> GetCompletedTasks(DateTime minCompletionTime, string taskType = null, string tenantId = null)
		{
			if (tenantId == null)
			{
				throw new ArgumentNullException("tenantId must be specified for exchange workload");
			}
			SearchFilter completedTaskFilter = TaskHelper.GetCompletedTaskFilter(taskType, new DateTime?(minCompletionTime), null, null);
			return from task in TenantStore.Find<TaskStoreObject>(tenantId, completedTaskFilter, false, OperationContext.CorrelationId)
			select task.ToDarTask(this.provider);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023E8 File Offset: 0x000005E8
		public override IEnumerable<DarTask> GetTasks(string tenantId, string taskType = null, DarTaskState? taskState = null, DateTime? minScheduledTime = null, DateTime? maxScheduledTime = null, DateTime? minCompletedTime = null, DateTime? maxCompletedTime = null)
		{
			if (tenantId == null)
			{
				throw new ArgumentNullException("tenantId must be specified for exchange workload");
			}
			SearchFilter taskFilter = TaskHelper.GetTaskFilter(taskType, taskState, minScheduledTime, maxScheduledTime, minCompletedTime, maxCompletedTime);
			return from task in TenantStore.Find<TaskStoreObject>(tenantId, taskFilter, false, OperationContext.CorrelationId)
			select task.ToDarTask(this.provider);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000245C File Offset: 0x0000065C
		public override IEnumerable<DarTask> Dequeue(int count, DarTaskCategory category, object availableResource = null)
		{
			return (from t in (from t in InstanceManager.Current.GetReadyTaskList()
			orderby t.Priority
			select t).ThenBy((DarTask darTask) => darTask.TaskLastExecutionTime)
			where t.Category == category
			select t).Take(count);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024DB File Offset: 0x000006DB
		public override void Enqueue(DarTask darTask)
		{
			TaskHelper.Validate(darTask, this.provider);
			InstanceManager.Current.TaskAggregates.ValidateEnqueue(darTask, darTask.CorrelationId);
			darTask.TaskState = DarTaskState.Ready;
			TenantStore.SaveTask(darTask, darTask.CorrelationId);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000257C File Offset: 0x0000077C
		public override IEnumerable<DarTask> GetRunningTasks(DateTime minExecutionStartTime, DateTime maxExecutionStartTime, string taskType = null, string tenantId = null)
		{
			return from t in InstanceManager.Current.GetActiveTaskList(tenantId)
			where t.TaskExecutionStartTime > minExecutionStartTime && t.TaskExecutionStartTime < maxExecutionStartTime && t.TaskState == DarTaskState.Running && (string.IsNullOrEmpty(taskType) || taskType.Equals(taskType, StringComparison.InvariantCultureIgnoreCase))
			select t;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000025C1 File Offset: 0x000007C1
		public override void UpdateTask(DarTask darTask)
		{
			TenantStore.SaveTask(darTask, darTask.CorrelationId);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000025CF File Offset: 0x000007CF
		public override IEnumerable<DarTask> GetLastScheduledTasks(string tenantId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000005 RID: 5
		private DarServiceProvider provider;
	}
}
