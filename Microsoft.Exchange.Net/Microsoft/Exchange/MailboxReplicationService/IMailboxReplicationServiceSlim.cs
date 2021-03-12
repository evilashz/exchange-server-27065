using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200076E RID: 1902
	[ServiceContract(SessionMode = SessionMode.Required)]
	internal interface IMailboxReplicationServiceSlim
	{
		// Token: 0x0600259E RID: 9630
		[OperationContract]
		void SyncNow(List<SyncNowNotification> notifications);
	}
}
