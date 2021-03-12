using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.JobQueues;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x0200097C RID: 2428
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MembershipSyncJobQueue : TeamMailboxSyncJobQueue
	{
		// Token: 0x060059D6 RID: 22998 RVA: 0x0017495E File Offset: 0x00172B5E
		public MembershipSyncJobQueue(TeamMailboxSyncConfiguration config, ITeamMailboxSecurityRefresher teamMailboxSecurityRefresher, IResourceMonitorFactory resourceMonitorFactory, IOAuthCredentialFactory oauthCredentialFactory, bool createTeamMailboxSyncInfoCache = true) : base(QueueType.TeamMailboxMembershipSync, "TeamMailboxMembershipLastSyncCycleLog", config, resourceMonitorFactory, oauthCredentialFactory, createTeamMailboxSyncInfoCache)
		{
			if (teamMailboxSecurityRefresher == null)
			{
				throw new ArgumentNullException("teamMailboxSecurityRefresher");
			}
			this.teamMailboxSecurityRefresher = teamMailboxSecurityRefresher;
		}

		// Token: 0x060059D7 RID: 22999 RVA: 0x00174987 File Offset: 0x00172B87
		protected override Job InternalCreateJob(TeamMailboxSyncInfo info, string clientString, SyncOption syncOption)
		{
			return new MembershipSyncJob(this, this.config, this.teamMailboxSecurityRefresher, info, clientString, syncOption);
		}

		// Token: 0x04003159 RID: 12633
		private ITeamMailboxSecurityRefresher teamMailboxSecurityRefresher;
	}
}
