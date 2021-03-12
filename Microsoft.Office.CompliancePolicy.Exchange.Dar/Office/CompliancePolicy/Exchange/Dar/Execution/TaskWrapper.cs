using System;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Office.CompliancePolicy.Dar;
using Microsoft.Office.CompliancePolicy.Exchange.Dar.Diagnostics;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Office.CompliancePolicy.Exchange.Dar.Execution
{
	// Token: 0x0200000A RID: 10
	internal class TaskWrapper : SystemTaskBase
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002B23 File Offset: 0x00000D23
		public TaskWrapper(DarTask task, DarTaskManager taskManager, SystemWorkloadBase workload, ResourceReservation resourceReservation) : base(workload, resourceReservation)
		{
			if (task == null)
			{
				throw new ArgumentNullException("task");
			}
			if (taskManager == null)
			{
				throw new ArgumentNullException("taskManager");
			}
			this.Task = task;
			this.TaskManager = taskManager;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002B58 File Offset: 0x00000D58
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002B60 File Offset: 0x00000D60
		public DarTask Task { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002B69 File Offset: 0x00000D69
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002B71 File Offset: 0x00000D71
		public DarTaskManager TaskManager { get; private set; }

		// Token: 0x06000053 RID: 83 RVA: 0x00002BC8 File Offset: 0x00000DC8
		protected override TaskStepResult Execute()
		{
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					try
					{
						this.ExecuteInternal();
					}
					catch (AggregateException ex2)
					{
						LogItem.Publish("TaskLifeCycle", "TaskFatalErrorRunningAndUpdatingTask", ex2.ToString(), this.Task.CorrelationId, ResultSeverityLevel.Error);
					}
				});
			}
			catch (GrayException ex)
			{
				LogItem.Publish("TaskLifeCycle", "TaskFatalGrayErrorRunningAndUpdatingTask", ex.ToString(), this.Task.CorrelationId, ResultSeverityLevel.Error);
			}
			return TaskStepResult.Complete;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002C38 File Offset: 0x00000E38
		private void ExecuteInternal()
		{
			try
			{
				ExceptionHandler.Handle(delegate
				{
					this.Task.InvokeTask(this.TaskManager);
				}, new ExceptionGroupHandler(ExceptionGroupHandlers.Unhandled), new ExceptionHandlingOptions
				{
					ClientId = "TaskLifeCycle",
					Operation = "TaskRunning",
					CorrelationId = this.Task.ToString(),
					IsTimeoutEnabled = false
				});
			}
			catch (AggregateException ex)
			{
				string component = "TaskLifeCycle";
				string tag = "TaskFailedWithUnhandledException";
				string correlationId = this.Task.CorrelationId;
				LogItem.Publish(component, tag, ex.ToString(), correlationId, ResultSeverityLevel.Error);
				this.Task.TaskState = DarTaskState.Failed;
				this.TaskManager.UpdateTaskState(this.Task);
			}
		}
	}
}
