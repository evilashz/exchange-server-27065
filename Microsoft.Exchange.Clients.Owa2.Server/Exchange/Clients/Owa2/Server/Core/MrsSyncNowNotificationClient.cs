using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200018A RID: 394
	internal class MrsSyncNowNotificationClient : ISyncNowNotificationClient
	{
		// Token: 0x06000E1F RID: 3615 RVA: 0x000359B8 File Offset: 0x00033BB8
		void ISyncNowNotificationClient.NotifyOWALogonTriggeredSyncNowNeeded(Guid mailboxGuid, Guid mdbGuid, string mailboxServer)
		{
			this.SyncNow(mailboxGuid, mdbGuid);
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x000359C2 File Offset: 0x00033BC2
		void ISyncNowNotificationClient.NotifyOWARefreshButtonTriggeredSyncNowNeeded(Guid mailboxGuid, Guid mdbGuid, string mailboxServer)
		{
			this.SyncNow(mailboxGuid, mdbGuid);
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x000359CC File Offset: 0x00033BCC
		void ISyncNowNotificationClient.NotifyOWAActivityTriggeredSyncNowNeeded(Guid mailboxGuid, Guid mdbGuid, string mailboxServer)
		{
			this.SyncNow(mailboxGuid, mdbGuid);
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x000359D6 File Offset: 0x00033BD6
		private void SyncNow(Guid mailboxGuid, Guid mdbGuid)
		{
			MailboxReplicationServiceClientSlim.NotifyToSync(SyncNowNotificationFlags.ActivateJob, mailboxGuid, mdbGuid);
		}
	}
}
