using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000443 RID: 1091
	internal enum InstantMessagingBuddyMetadata
	{
		// Token: 0x040014C7 RID: 5319
		[DisplayName("BD.LS")]
		LyncServer,
		// Token: 0x040014C8 RID: 5320
		[DisplayName("BD.UC")]
		UserContext,
		// Token: 0x040014C9 RID: 5321
		[DisplayName("BD.UCS")]
		UCSMode,
		// Token: 0x040014CA RID: 5322
		[DisplayName("BD.PVC")]
		PrivacyMode,
		// Token: 0x040014CB RID: 5323
		[DisplayName("BD.SIP")]
		SIP,
		// Token: 0x040014CC RID: 5324
		[DisplayName("BD.CID")]
		ContactId
	}
}
