using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Hygiene.Data;
using Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x0200001B RID: 27
	public class CheckSingleInstanceTasks : ProbeWorkItem
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x0000757C File Offset: 0x0000577C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			try
			{
				this.InternalDoWork(cancellationToken);
			}
			catch (PermanentDALException ex)
			{
				ProbeResult result = base.Result;
				result.ExecutionContext += string.Format("Ignoring DAL exception {0} because it is outside the charter of this probe.", ex.Message);
			}
			catch (TransientDALException ex2)
			{
				ProbeResult result2 = base.Result;
				result2.ExecutionContext += string.Format("Ignoring DAL exception {0} because it is outside the charter of this probe.", ex2.Message);
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00007600 File Offset: 0x00005800
		private static void Log(ProbeResult result, string message)
		{
			result.ExecutionContext += string.Format(CultureInfo.InvariantCulture, "{0} - {1}.{2}", new object[]
			{
				DateTime.UtcNow.ToString("hh:mm:ss.ff", CultureInfo.InvariantCulture),
				message,
				Environment.NewLine
			});
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000076B0 File Offset: 0x000058B0
		private void InternalDoWork(CancellationToken cancellationToken)
		{
			this.Initialize();
			if (this.managerInstance == null)
			{
				return;
			}
			List<Exception> list = new List<Exception>();
			JobDefinition[] array = this.session.FindJobDefinitions(new Guid?(this.roleId), null, "Microsoft.Exchange.Hygiene.ForwardSyncDaemon.exe");
			JobDefinition[] array2 = array;
			int i = 0;
			while (i < array2.Length)
			{
				JobDefinition jobDefinition = array2[i];
				if (!cancellationToken.IsCancellationRequested)
				{
					CheckSingleInstanceTasks.Log(base.Result, "CheckSingleInstanceTasks started.");
					ScheduleItem[] array3 = this.session.FindScheduleItems(this.roleId, null, null, new int?((int)this.managerInstance.Region), null, new Guid?(jobDefinition.BackgroundJobId));
					foreach (ScheduleItem scheduleItem in array3)
					{
						if (cancellationToken.IsCancellationRequested)
						{
							return;
						}
						CheckSingleInstanceTasks.Log(base.Result, string.Format(CultureInfo.InvariantCulture, "Processing Job Name = {0}; Schedule Id = {1}.", new object[]
						{
							jobDefinition.Name,
							scheduleItem.ScheduleId
						}));
						List<TaskItem> list2 = new List<TaskItem>(this.session.FindTasks(this.roleId, null, new TaskExecutionStateType?(TaskExecutionStateType.Completed), new SchedulingType?(SchedulingType.Continuous), null, new Guid?(scheduleItem.ScheduleId), null, null, null, null, null));
						list2.RemoveAll((TaskItem item) => !item.HasStartTime || item.StartTime < this.lastTimeChecked.Subtract(TimeSpan.FromHours(1.0)));
						this.lastTimeChecked = DateTime.UtcNow;
						list2.Sort(Comparer<TaskItem>.Create((TaskItem ti1, TaskItem ti2) => ti1.StartTime.CompareTo(ti2.StartTime)));
						for (int k = 1; k < list2.Count; k++)
						{
							if (list2[k].StartTime < list2[k - 1].EndTime)
							{
								list.Add(new Exception(this.FormatTaskErrorMessage(jobDefinition, list2[k - 1], list2[k])));
							}
						}
					}
					i++;
					continue;
				}
				return;
			}
			CheckSingleInstanceTasks.Log(base.Result, "CheckSingleInstanceTasks finished.");
			if (list.Count > 0)
			{
				throw new AggregateException(list);
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000792C File Offset: 0x00005B2C
		private void Initialize()
		{
			if (this.session == null)
			{
				BackgroundJobBackendSession backgroundJobBackendSession = new BackgroundJobBackendSession();
				RoleDefinition[] source = backgroundJobBackendSession.FindRoleByNameVersion("Background", null);
				this.roleId = source.Single<RoleDefinition>().RoleId;
				BackgroundJobMgrInstance[] source2 = backgroundJobBackendSession.FindBackgroundJobMgrInstances(this.roleId, Environment.MachineName);
				this.managerInstance = source2.FirstOrDefault<BackgroundJobMgrInstance>();
				this.session = backgroundJobBackendSession;
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000798C File Offset: 0x00005B8C
		private string FormatTaskErrorMessage(JobDefinition job, TaskItem task1, TaskItem task2)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Job {0}-{1} Task items {2} and {3} overlapped\r\n", new object[]
			{
				job.Name,
				job.CommandLine,
				task1.TaskId,
				task2.TaskId
			});
			stringBuilder.AppendFormat("Task {0} ran on {1}. Start {2}. End {3}. Completion Status {4}\r\n", new object[]
			{
				task1.TaskId,
				this.GetMachine(task1),
				task1.StartTime,
				task1.EndTime,
				task1.TaskCompletionStatus.ToString()
			});
			stringBuilder.AppendFormat("Task {0} ran on {1}. Start {2}. End {3}. Completion Status {4}\r\n", new object[]
			{
				task2.TaskId,
				this.GetMachine(task2),
				task2.StartTime,
				task2.EndTime,
				task2.TaskCompletionStatus.ToString()
			});
			return stringBuilder.ToString();
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00007ACC File Offset: 0x00005CCC
		private string GetMachine(TaskItem task)
		{
			BackgroundJobMgrInstance[] array = this.session.FindBackgroundJobMgrInstances(this.roleId, null);
			BackgroundJobMgrInstance[] array2 = Array.FindAll<BackgroundJobMgrInstance>(array, (BackgroundJobMgrInstance m) => task.HasBJMOwnerId && m.MachineId == task.BJMOwnerId);
			if (array2.Length == 1)
			{
				return array2[0].MachineName;
			}
			if (!task.HasBJMOwnerId)
			{
				return Guid.Empty.ToString();
			}
			return task.BJMOwnerId.ToString();
		}

		// Token: 0x040000AB RID: 171
		private Guid roleId;

		// Token: 0x040000AC RID: 172
		private BackgroundJobMgrInstance managerInstance;

		// Token: 0x040000AD RID: 173
		private BackgroundJobBackendSession session;

		// Token: 0x040000AE RID: 174
		private DateTime lastTimeChecked = DateTime.UtcNow.Subtract(TimeSpan.FromHours(12.0));
	}
}
