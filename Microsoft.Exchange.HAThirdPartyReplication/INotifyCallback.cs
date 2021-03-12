using System;

namespace Microsoft.Exchange.ThirdPartyReplication
{
	// Token: 0x0200000D RID: 13
	public interface INotifyCallback
	{
		// Token: 0x06000039 RID: 57
		void BecomePame();

		// Token: 0x0600003A RID: 58
		void RevokePame();

		// Token: 0x0600003B RID: 59
		NotificationResponse DatabaseMoveNeeded(Guid dbId, string currentActiveFqdn, bool mountDesired);
	}
}
