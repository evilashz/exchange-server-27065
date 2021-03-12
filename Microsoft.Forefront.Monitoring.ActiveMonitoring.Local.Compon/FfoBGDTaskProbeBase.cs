using System;
using System.Collections.Generic;
using Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x0200001D RID: 29
	public abstract class FfoBGDTaskProbeBase : ProbeWorkItem
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00007D6D File Offset: 0x00005F6D
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00007D75 File Offset: 0x00005F75
		internal BackgroundJobBackendSession BackgroundJobSession { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00007D7E File Offset: 0x00005F7E
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00007D86 File Offset: 0x00005F86
		internal Guid RoleId { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00007D8F File Offset: 0x00005F8F
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00007D97 File Offset: 0x00005F97
		internal BackgroundJobMgrInstance ManagerInstance { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00007DA0 File Offset: 0x00005FA0
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00007DA8 File Offset: 0x00005FA8
		internal RoleDefinition[] RoleDefinitions { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00007DB1 File Offset: 0x00005FB1
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00007DB9 File Offset: 0x00005FB9
		internal TimeSpan TimeOfTaskInNotStartedStatus { get; set; }

		// Token: 0x060000F9 RID: 249 RVA: 0x00007DC4 File Offset: 0x00005FC4
		internal void DisplayBGDScheduleAndTaskCommands(TaskItem taskItem)
		{
			if (taskItem != null)
			{
				this.Log("For more the last task information, please execute 'Get-BackgroundJobTask -TaskId {0}'. ", new object[]
				{
					taskItem.TaskId
				});
				this.Log("For more BGD job schedule information, please execute 'Get-BackgroundJobSchedule -ScheduleId {0}'. ", new object[]
				{
					taskItem.ScheduleId
				});
				this.Log("To get the tasks list, please execute 'Get-BackgroundJobTask -ScheduleId {0} | Sort StartTime '.", new object[]
				{
					taskItem.ScheduleId
				});
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00007E38 File Offset: 0x00006038
		protected static T ReadProbeParameters<T>(Dictionary<string, string> parameters, string name)
		{
			string value;
			if (parameters.TryGetValue(name, out value))
			{
				return (T)((object)Convert.ChangeType(value, typeof(T)));
			}
			return default(T);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00007E6F File Offset: 0x0000606F
		protected void Log(string message, params object[] args)
		{
			ProbeResult result = base.Result;
			result.ExecutionContext += string.Format("{0}: {1}{2}", DateTime.UtcNow, string.Format(message, args), Environment.NewLine);
		}

		// Token: 0x060000FC RID: 252
		protected abstract void ReadProbeParameter();
	}
}
