using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000445 RID: 1093
	internal enum InstantMessagingSubscriptionMetadata
	{
		// Token: 0x040014D8 RID: 5336
		[DisplayName("IMSUB.LS")]
		LyncServer,
		// Token: 0x040014D9 RID: 5337
		[DisplayName("IMSUB.UC")]
		UserContext,
		// Token: 0x040014DA RID: 5338
		[DisplayName("IMSUB.UCS")]
		UCSMode,
		// Token: 0x040014DB RID: 5339
		[DisplayName("IMSUB.PVC")]
		PrivacyMode,
		// Token: 0x040014DC RID: 5340
		[DisplayName("IMSUB.SUBSIP")]
		SubscribedSIPs,
		// Token: 0x040014DD RID: 5341
		[DisplayName("IMSUB.UNSIP")]
		UnSubscribedSIP
	}
}
