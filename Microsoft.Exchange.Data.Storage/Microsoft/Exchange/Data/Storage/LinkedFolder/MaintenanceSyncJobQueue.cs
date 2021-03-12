using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.JobQueues;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x02000977 RID: 2423
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MaintenanceSyncJobQueue : TeamMailboxSyncJobQueue
	{
		// Token: 0x060059BB RID: 22971 RVA: 0x001736C2 File Offset: 0x001718C2
		public MaintenanceSyncJobQueue(TeamMailboxSyncConfiguration config, IResourceMonitorFactory resourceMonitorFactory, IOAuthCredentialFactory oauthCredentialFactory, bool createTeamMailboxSyncInfoCache = true) : base(QueueType.TeamMailboxMaintenanceSync, "TeamMailboxMaintenanceLastSyncCycleLog", config, resourceMonitorFactory, oauthCredentialFactory, createTeamMailboxSyncInfoCache)
		{
		}

		// Token: 0x060059BC RID: 22972 RVA: 0x001736D5 File Offset: 0x001718D5
		protected override Job InternalCreateJob(TeamMailboxSyncInfo info, string clientString, SyncOption syncOption)
		{
			return new MaintenanceSyncJob(this, this.config, info, clientString, syncOption);
		}
	}
}
