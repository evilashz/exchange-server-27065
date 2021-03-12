using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000020 RID: 32
	public class CheckStaleScheduledTasks : FfoBGDTaskProbeBase
	{
		// Token: 0x06000109 RID: 265 RVA: 0x00008A68 File Offset: 0x00006C68
		protected override void DoWork(CancellationToken cancellationToken)
		{
			try
			{
				this.Initialize();
				this.ReadProbeParameter();
				List<Exception> list = new List<Exception>();
				if (!cancellationToken.IsCancellationRequested)
				{
					foreach (RoleDefinition roleDefinition in this.roleDefinitions)
					{
						base.RoleId = roleDefinition.RoleId;
						BackgroundJobMgrInstance[] source = base.BackgroundJobSession.FindBackgroundJobMgrInstances(base.RoleId, Environment.MachineName);
						base.ManagerInstance = source.FirstOrDefault<BackgroundJobMgrInstance>();
						if (base.ManagerInstance != null)
						{
							ScheduleItem[] array2 = base.BackgroundJobSession.FindScheduleItems(base.RoleId, null, new bool?(true), new int?((int)base.ManagerInstance.Region), null, null);
							foreach (ScheduleItem scheduleItem in array2)
							{
								if (scheduleItem.SchedulingType == SchedulingType.Scheduled && scheduleItem.SchedulingInterval != 0 && !this.IsTheLastTaskRunning(scheduleItem))
								{
									DateTime lastScheduledTime = scheduleItem.HasLastScheduledTime ? scheduleItem.LastScheduledTime : scheduleItem.CreatedDatetime;
									TaskItem taskItem = this.FindLatestTask(scheduleItem);
									DateTime nextScheduleTime = this.GetNextScheduleTime(taskItem, scheduleItem.ChangedDatetime, lastScheduledTime, scheduleItem.SchedulingInterval);
									if (DateTime.UtcNow - nextScheduleTime > this.timeOfScheduledTaskHasNotScheduled)
									{
										list.Add(new Exception(string.Format("The BGD (scheduled) task (schedule id: {0}) should be created at {1}, but it hasn't been created in {2} minutes. The threshold is {3} minutes.", new object[]
										{
											scheduleItem.ScheduleId,
											nextScheduleTime,
											(DateTime.UtcNow - nextScheduleTime).TotalMinutes,
											this.timeOfScheduledTaskHasNotScheduled.TotalMinutes
										})));
										if (taskItem != null)
										{
											base.Log("The last task information:\n   Schedule id: {0}\n   Task id: {1}\n   Task executable state: {2}", new object[]
											{
												scheduleItem.ScheduleId,
												taskItem.TaskId,
												taskItem.TaskExecutionState
											});
											base.DisplayBGDScheduleAndTaskCommands(taskItem);
										}
										else
										{
											base.Log("No task is created for this BGD schedule (schedule id : {0})", new object[]
											{
												scheduleItem.ScheduleId
											});
										}
									}
									else if (taskItem != null && taskItem.TaskExecutionState == TaskExecutionStateType.NotStarted && DateTime.UtcNow - taskItem.InsertTimeStamp > base.TimeOfTaskInNotStartedStatus)
									{
										list.Add(new Exception(string.Format("The current task (Task id: {0}) of the schedule (Schedule id: {1}) has been in Not Started state longer than {2} minutes. The threshold {3}. Please check probe 'CheckTaskInNotStartedStatusProbe' result! ", new object[]
										{
											taskItem.TaskId,
											taskItem.ScheduleId,
											(DateTime.UtcNow - taskItem.InsertTimeStamp).TotalMinutes,
											base.TimeOfTaskInNotStartedStatus
										})));
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

		// Token: 0x0600010A RID: 266 RVA: 0x00008DB4 File Offset: 0x00006FB4
		protected override void ReadProbeParameter()
		{
			this.timeOfScheduledTaskHasNotScheduled = TimeSpan.FromMinutes((double)FfoBGDTaskProbeBase.ReadProbeParameters<int>(base.Definition.Attributes, "NumberOfMinutesOfScheduledTaskHasNotScheduled"));
			base.TimeOfTaskInNotStartedStatus = TimeSpan.FromMinutes((double)FfoBGDTaskProbeBase.ReadProbeParameters<int>(base.Definition.Attributes, "NumberOfMinutesOfTaskInNotStartedStatus"));
			if (this.timeOfScheduledTaskHasNotScheduled < TimeSpan.Zero)
			{
				throw new ArgumentException(string.Format("The value of the parameter NumberOfMinutesOfScheduledTaskHasNotScheduled ({0}) should be larger than 0.", this.timeOfScheduledTaskHasNotScheduled));
			}
			if (base.TimeOfTaskInNotStartedStatus < TimeSpan.Zero)
			{
				throw new ArgumentException(string.Format("The value of the parameter NumberOfMinutesOfTaskInNotStartedStatus ({0}) should be larger than 0.", base.TimeOfTaskInNotStartedStatus));
			}
			base.Log("The parameter value NumberOfMinutesOfScheduledTaskHasNotScheduled is {0} minutes. ", new object[]
			{
				this.timeOfScheduledTaskHasNotScheduled,
				Environment.NewLine
			});
			base.Log("The parameter value NumberOfMinutesOfTaskInNotStartedStatus is {0} minutes. ", new object[]
			{
				base.TimeOfTaskInNotStartedStatus,
				Environment.NewLine
			});
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00008EB0 File Offset: 0x000070B0
		private void Initialize()
		{
			if (base.BackgroundJobSession == null)
			{
				BackgroundJobBackendSession backgroundJobBackendSession = new BackgroundJobBackendSession();
				this.roleDefinitions = backgroundJobBackendSession.FindRoleByNameVersion(null, null);
				base.BackgroundJobSession = backgroundJobBackendSession;
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00008EE0 File Offset: 0x000070E0
		private bool IsTheLastTaskRunning(ScheduleItem sched)
		{
			TaskItem[] array = base.BackgroundJobSession.FindTasks(base.RoleId, null, new TaskExecutionStateType?(TaskExecutionStateType.Running), null, null, new Guid?(sched.ScheduleId), null, null, null, null, null);
			return array != null && array.Length != 0;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00008F68 File Offset: 0x00007168
		private TaskItem FindLatestTask(ScheduleItem sched)
		{
			TaskItem[] array = base.BackgroundJobSession.FindTasks(base.RoleId, null, null, null, new SchedulingType?(SchedulingType.Scheduled), null, new Guid?(sched.ScheduleId), null, null, null, null, null);
			if (array == null)
			{
				return null;
			}
			return (from ti in array
			orderby ti.InsertTimeStamp
			select ti).LastOrDefault<TaskItem>();
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00009004 File Offset: 0x00007204
		private DateTime GetNextScheduleTime(TaskItem latestTaskItem, DateTime scheduleChangedTime, DateTime lastScheduledTime, int scheduleInterval)
		{
			double value;
			if (latestTaskItem != null && latestTaskItem.HasEndTime)
			{
				if (scheduleChangedTime > latestTaskItem.EndTime)
				{
					value = ((scheduleChangedTime - lastScheduledTime).TotalSeconds / (double)scheduleInterval + 1.0) * (double)scheduleInterval;
				}
				else
				{
					value = ((latestTaskItem.EndTime - lastScheduledTime).TotalSeconds / (double)scheduleInterval + 1.0) * (double)scheduleInterval;
				}
			}
			else if (scheduleChangedTime > lastScheduledTime)
			{
				value = ((scheduleChangedTime - lastScheduledTime).TotalSeconds / (double)scheduleInterval + 1.0) * (double)scheduleInterval;
			}
			else
			{
				value = (double)scheduleInterval;
			}
			return lastScheduledTime.AddSeconds(value);
		}

		// Token: 0x040000BA RID: 186
		private TimeSpan timeOfScheduledTaskHasNotScheduled;

		// Token: 0x040000BB RID: 187
		private RoleDefinition[] roleDefinitions;
	}
}
