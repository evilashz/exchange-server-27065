using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Office.CompliancePolicy.Dar;
using Microsoft.Office.CompliancePolicy.Exchange.Dar.Utility;

namespace Microsoft.Office.CompliancePolicy.Exchange.Dar.Execution
{
	// Token: 0x02000008 RID: 8
	internal class InstanceManager
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002858 File Offset: 0x00000A58
		public InstanceManager()
		{
			this.Provider = new ExDarServiceProvider();
			this.Settings = new ExecutionSettings();
			this.Tenants = new ActiveTasks();
			this.TaskAggregates = new TaskAggregates();
			this.Scheduler = new Scheduler(this);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002898 File Offset: 0x00000A98
		public static InstanceManager Current
		{
			get
			{
				return InstanceManager.current;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000289F File Offset: 0x00000A9F
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000028A7 File Offset: 0x00000AA7
		public ExecutionSettings Settings { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000028B0 File Offset: 0x00000AB0
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000028B8 File Offset: 0x00000AB8
		public ActiveTasks Tenants { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000028C1 File Offset: 0x00000AC1
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000028C9 File Offset: 0x00000AC9
		public TaskAggregates TaskAggregates { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000028D2 File Offset: 0x00000AD2
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000028DA File Offset: 0x00000ADA
		public Scheduler Scheduler { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000028E3 File Offset: 0x00000AE3
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000028EB File Offset: 0x00000AEB
		public DarServiceProvider Provider { get; private set; }

		// Token: 0x06000036 RID: 54 RVA: 0x000028F4 File Offset: 0x00000AF4
		public void Start()
		{
			this.Scheduler.Start();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002901 File Offset: 0x00000B01
		public void Stop()
		{
			this.Scheduler.Stop();
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002910 File Offset: 0x00000B10
		public void NotifyTaskStoreChange(string tenantId, string correlationId)
		{
			if (tenantId == null)
			{
				throw new ArgumentNullException("tenantId");
			}
			if (this.currentSyncTask != null && !this.currentSyncTask.IsCompleted && this.prevSyncTask != null && !this.prevSyncTask.IsCompleted)
			{
				return;
			}
			if (this.currentSyncTask != null && !this.currentSyncTask.IsCompleted)
			{
				this.prevSyncTask = this.currentSyncTask;
			}
			this.currentSyncTask = TenantStore.SyncActiveTasks(tenantId, this.Tenants, correlationId);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000029B8 File Offset: 0x00000BB8
		public IEnumerable<DarTask> GetReadyTaskList()
		{
			DateTime now = DateTime.UtcNow;
			return from t in InstanceManager.Current.GetActiveTaskList(null)
			where t.TaskState == DarTaskState.Ready && t.MinTaskScheduleTime < now
			where TaskHelper.IsValid(t)
			select t;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002A14 File Offset: 0x00000C14
		public IEnumerable<DarTask> GetActiveTaskList(string tenantId = null)
		{
			return this.Tenants.GetByTenantOrAll(tenantId);
		}

		// Token: 0x0400000B RID: 11
		private static InstanceManager current = new InstanceManager();

		// Token: 0x0400000C RID: 12
		private Task currentSyncTask;

		// Token: 0x0400000D RID: 13
		private Task prevSyncTask;
	}
}
