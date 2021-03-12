using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000229 RID: 553
	public interface ISyncNowNotificationClient
	{
		// Token: 0x06001340 RID: 4928
		void NotifyOWALogonTriggeredSyncNowNeeded(Guid mailboxGuid, Guid mdbGuid, string mailboxServer);

		// Token: 0x06001341 RID: 4929
		void NotifyOWARefreshButtonTriggeredSyncNowNeeded(Guid mailboxGuid, Guid mdbGuid, string mailboxServer);

		// Token: 0x06001342 RID: 4930
		void NotifyOWAActivityTriggeredSyncNowNeeded(Guid mailboxGuid, Guid mdbGuid, string mailboxServer);
	}
}
