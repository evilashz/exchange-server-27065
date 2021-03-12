using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x0200001E RID: 30
	public class CheckStaleContinousTasks : FfoBGDTaskProbeBase
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00007EC0 File Offset: 0x000060C0
		protected override void DoWork(CancellationToken cancellationToken)
		{
			try
			{
				this.Initialize();
				this.ReadProbeParameter();
				List<Exception> list = new List<Exception>();
				if (!cancellationToken.IsCancellationRequested)
				{
					foreach (RoleDefinition roleDefinition in base.RoleDefinitions)
					{
						base.RoleId = roleDefinition.RoleId;
						BackgroundJobMgrInstance[] source = base.BackgroundJobSession.FindBackgroundJobMgrInstances(base.RoleId, Environment.MachineName);
						base.ManagerInstance = source.FirstOrDefault<BackgroundJobMgrInstance>();
						if (base.ManagerInstance != null)
						{
							ScheduleItem[] array = base.BackgroundJobSession.FindScheduleItems(base.RoleId, null, new bool?(true), new int?((int)base.ManagerInstance.Region), null, null);
							foreach (ScheduleItem scheduleItem in array)
							{
								if (scheduleItem.SchedulingType == SchedulingType.Continuous)
								{
									TaskItem[] array3 = base.BackgroundJobSession.FindTasks(base.RoleId, null, CheckStaleContinousTasks.goodStates, null, null, null, new Guid?(scheduleItem.ScheduleId), null, null, null, null, null);
									if (array3.Length <= 0)
									{
										array3 = base.BackgroundJobSession.FindTasks(base.RoleId, null, CheckStaleContinousTasks.badStates, null, null, null, new Guid?(scheduleItem.ScheduleId), null, null, null, null, null);
										if (array3 == null || array3.Length == 0)
										{
											if (DateTime.UtcNow - scheduleItem.CreatedDatetime > this.timeOfContinuousTaskHasNotScheduled && DateTime.UtcNow - scheduleItem.ChangedDatetime > this.timeOfContinuousTaskHasNotScheduled)
											{
												list.Add(new Exception(string.Format("No task has been created for the continuous Job (Schedule id: {0}). The schedule was created at {1}. The schedule was changed at {2}. Threshold is {3} minutes.", new object[]
												{
													scheduleItem.ScheduleId,
													scheduleItem.CreatedDatetime,
													scheduleItem.ChangedDatetime,
													this.timeOfContinuousTaskHasNotScheduled.TotalMinutes
												})));
												base.Log("The job schedule information:\n   Schedule id: {0}", new object[]
												{
													scheduleItem.ScheduleId
												});
											}
										}
										else
										{
											TaskItem taskItem = (from ti in array3
											orderby ti.EndTime
											select ti).LastOrDefault<TaskItem>();
											if (DateTime.UtcNow - taskItem.EndTime > this.timeOfContinuousTaskHasNotScheduled && DateTime.UtcNow - scheduleItem.ChangedDatetime > this.timeOfContinuousTaskHasNotScheduled)
											{
												list.Add(new Exception(string.Format("The last task of the continuous job (schedule id: {0} , task id: {1}) is in {2} state. The new task hasn't been scheduled more than {3} minutes. The threshold is {4} minutes.", new object[]
												{
													taskItem.ScheduleId,
													taskItem.TaskId,
													taskItem.TaskExecutionState,
													(DateTime.UtcNow - ((taskItem.EndTime > scheduleItem.ChangedDatetime) ? taskItem.EndTime : scheduleItem.ChangedDatetime)).TotalMinutes,
													this.timeOfContinuousTaskHasNotScheduled.TotalMinutes
												})));
												base.Log("The last task information:\n   Schedule id: {0}\n   Task id: {1}\n   Task Start Time: {2}\n   End time: {3}\n   Task executable state: {4}\n   Task completion state is: {5}", new object[]
												{
													scheduleItem.ScheduleId,
													taskItem.TaskId,
													taskItem.StartTime,
													taskItem.EndTime,
													taskItem.TaskExecutionState,
													taskItem.TaskCompletionStatus
												});
												base.DisplayBGDScheduleAndTaskCommands(taskItem);
											}
										}
									}
								}
							}
						}
					}
					if (list.Count > 0)
					{
						throw new AggregateException(list);
					}
				}
			}
			catch (ArgumentException ex)
			{
				base.Log(string.Format("Probe parameter is invalid: {0}", ex.Message), new object[0]);
				throw;
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00008358 File Offset: 0x00006558
		protected override void ReadProbeParameter()
		{
			this.timeOfContinuousTaskHasNotScheduled = TimeSpan.FromMinutes((double)FfoBGDTaskProbeBase.ReadProbeParameters<int>(base.Definition.Attributes, "NumberOfMinutesOfContinuousTaskHasNotScheduled"));
			if (this.timeOfContinuousTaskHasNotScheduled < TimeSpan.Zero)
			{
				throw new ArgumentException(string.Format("The value of the parameter NumberOfMinutesOfContinuousTaskHasNotScheduled ({0}) should be larger than 0.", this.timeOfContinuousTaskHasNotScheduled));
			}
			base.Log("The parameter value NumberOfMinutesOfContinuousTaskHasNotScheduled is {0} minutes. ", new object[]
			{
				this.timeOfContinuousTaskHasNotScheduled,
				Environment.NewLine
			});
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000083DC File Offset: 0x000065DC
		private void Initialize()
		{
			if (base.BackgroundJobSession == null)
			{
				BackgroundJobBackendSession backgroundJobBackendSession = new BackgroundJobBackendSession();
				base.RoleDefinitions = backgroundJobBackendSession.FindRoleByNameVersion(null, null);
				base.BackgroundJobSession = backgroundJobBackendSession;
			}
		}

		// Token: 0x040000B5 RID: 181
		private static readonly TaskExecutionStateType[] goodStates = new TaskExecutionStateType[]
		{
			TaskExecutionStateType.NotStarted,
			TaskExecutionStateType.Running,
			TaskExecutionStateType.Started
		};

		// Token: 0x040000B6 RID: 182
		private static readonly TaskExecutionStateType[] badStates = new TaskExecutionStateType[]
		{
			TaskExecutionStateType.Failed,
			TaskExecutionStateType.Failover,
			TaskExecutionStateType.Completed
		};

		// Token: 0x040000B7 RID: 183
		private TimeSpan timeOfContinuousTaskHasNotScheduled;
	}
}
