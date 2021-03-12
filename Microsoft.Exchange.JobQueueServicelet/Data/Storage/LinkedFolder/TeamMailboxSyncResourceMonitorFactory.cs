using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000008 RID: 8
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TeamMailboxSyncResourceMonitorFactory : IResourceMonitorFactory
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002604 File Offset: 0x00000804
		public IResourceMonitor Create(Guid teamMailboxMdbGuid)
		{
			if (teamMailboxMdbGuid == Guid.Empty)
			{
				throw new ArgumentNullException("teamMailboxMdbGuid");
			}
			return new TeamMailboxSyncResourceMonitorFactory.TeamMailboxSyncResourceMonitor(new ResourceKey[]
			{
				ProcessorResourceKey.Local,
				new MdbReplicationResourceHealthMonitorKey(teamMailboxMdbGuid),
				new MdbResourceHealthMonitorKey(teamMailboxMdbGuid)
			}, StandardBudget.Acquire(new UnthrottledBudgetKey("TeamMailboxSync", BudgetType.ResourceTracking)));
		}

		// Token: 0x04000015 RID: 21
		private const string BudgetKey = "TeamMailboxSync";

		// Token: 0x02000009 RID: 9
		private class TeamMailboxSyncResourceMonitor : IResourceMonitor
		{
			// Token: 0x06000038 RID: 56 RVA: 0x00002669 File Offset: 0x00000869
			public TeamMailboxSyncResourceMonitor(ResourceKey[] resourcesToAccess, IBudget ibudget)
			{
				this.resourcesToAccess = resourcesToAccess;
				this.ibudget = ibudget;
			}

			// Token: 0x06000039 RID: 57 RVA: 0x0000267F File Offset: 0x0000087F
			public void CheckResourceHealth()
			{
				ResourceLoadDelayInfo.CheckResourceHealth(this.ibudget, TeamMailboxSyncResourceMonitorFactory.TeamMailboxSyncResourceMonitor.workloadSettings, this.resourcesToAccess);
			}

			// Token: 0x0600003A RID: 58 RVA: 0x00002697 File Offset: 0x00000897
			public DelayInfo GetDelay()
			{
				return ResourceLoadDelayInfo.GetDelay(this.ibudget, TeamMailboxSyncResourceMonitorFactory.TeamMailboxSyncResourceMonitor.workloadSettings, this.resourcesToAccess, true);
			}

			// Token: 0x0600003B RID: 59 RVA: 0x000026B0 File Offset: 0x000008B0
			public void StartChargingBudget()
			{
				this.ibudget.StartLocal("TeamMailboxSyncResourceMonitorFactory.StartChargingBudget", default(TimeSpan));
			}

			// Token: 0x0600003C RID: 60 RVA: 0x000026D6 File Offset: 0x000008D6
			public void ResetBudget()
			{
				this.ibudget.EndLocal();
			}

			// Token: 0x0600003D RID: 61 RVA: 0x000026E3 File Offset: 0x000008E3
			public IBudget GetBudget()
			{
				return this.ibudget;
			}

			// Token: 0x04000016 RID: 22
			private static readonly WorkloadSettings workloadSettings = new WorkloadSettings(WorkloadType.TeamMailboxSync, true);

			// Token: 0x04000017 RID: 23
			private readonly ResourceKey[] resourcesToAccess;

			// Token: 0x04000018 RID: 24
			private readonly IBudget ibudget;
		}
	}
}
