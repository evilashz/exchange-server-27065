using System;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000108 RID: 264
	internal interface IRedirectTargetChooser
	{
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000751 RID: 1873
		string SubscriberLogId { get; }

		// Token: 0x06000752 RID: 1874
		bool GetTargetServer(out string fqdn, out int port);

		// Token: 0x06000753 RID: 1875
		void HandleServerNotFound();
	}
}
