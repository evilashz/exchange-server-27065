using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.JobQueues;

namespace Microsoft.Exchange.Data.Storage.LinkedFolder
{
	// Token: 0x0200096D RID: 2413
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DocumentSyncJobQueue : TeamMailboxSyncJobQueue
	{
		// Token: 0x06005984 RID: 22916 RVA: 0x00172B60 File Offset: 0x00170D60
		public DocumentSyncJobQueue(TeamMailboxSyncConfiguration config, IResourceMonitorFactory resourceMonitorFactory, IOAuthCredentialFactory oauthCredentialFactory, bool createTeamMailboxSyncInfoCache = true) : base(QueueType.TeamMailboxDocumentSync, "TeamMailboxDocumentLastSyncCycleLog", config, resourceMonitorFactory, oauthCredentialFactory, createTeamMailboxSyncInfoCache)
		{
		}

		// Token: 0x06005985 RID: 22917 RVA: 0x00172B73 File Offset: 0x00170D73
		protected override Job InternalCreateJob(TeamMailboxSyncInfo info, string clientString, SyncOption syncOption)
		{
			return new DocumentSyncJob(this, this.config, info, clientString, syncOption);
		}
	}
}
