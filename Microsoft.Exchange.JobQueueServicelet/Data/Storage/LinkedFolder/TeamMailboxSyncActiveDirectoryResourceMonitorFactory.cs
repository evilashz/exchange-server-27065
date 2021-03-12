using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000006 RID: 6
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TeamMailboxSyncActiveDirectoryResourceMonitorFactory : IResourceMonitorFactory
	{
		// Token: 0x0600002E RID: 46 RVA: 0x000025DD File Offset: 0x000007DD
		public IResourceMonitor Create(Guid teamMailboxMdbGuid)
		{
			return new TeamMailboxSyncActiveDirectoryResourceMonitorFactory.TeamMailboxSyncActiveDirectoryResourceMonitor();
		}

		// Token: 0x02000007 RID: 7
		private class TeamMailboxSyncActiveDirectoryResourceMonitor : IResourceMonitor
		{
			// Token: 0x06000030 RID: 48 RVA: 0x000025EC File Offset: 0x000007EC
			public void CheckResourceHealth()
			{
			}

			// Token: 0x06000031 RID: 49 RVA: 0x000025EE File Offset: 0x000007EE
			public DelayInfo GetDelay()
			{
				return DelayInfo.NoDelay;
			}

			// Token: 0x06000032 RID: 50 RVA: 0x000025F5 File Offset: 0x000007F5
			public void StartChargingBudget()
			{
			}

			// Token: 0x06000033 RID: 51 RVA: 0x000025F7 File Offset: 0x000007F7
			public void ResetBudget()
			{
			}

			// Token: 0x06000034 RID: 52 RVA: 0x000025F9 File Offset: 0x000007F9
			public IBudget GetBudget()
			{
				return null;
			}
		}
	}
}
