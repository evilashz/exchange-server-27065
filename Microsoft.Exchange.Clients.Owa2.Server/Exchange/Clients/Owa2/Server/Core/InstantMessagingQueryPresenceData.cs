using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000444 RID: 1092
	internal enum InstantMessagingQueryPresenceData
	{
		// Token: 0x040014CE RID: 5326
		[DisplayName("QP.LS")]
		LyncServer,
		// Token: 0x040014CF RID: 5327
		[DisplayName("QP.UC")]
		UserContext,
		// Token: 0x040014D0 RID: 5328
		[DisplayName("QP.UCS")]
		UCSMode,
		// Token: 0x040014D1 RID: 5329
		[DisplayName("QP.PVC")]
		PrivacyMode,
		// Token: 0x040014D2 RID: 5330
		[DisplayName("QP.QSIP")]
		QueriedSIPs,
		// Token: 0x040014D3 RID: 5331
		[DisplayName("QP.SSIP")]
		SkippedSIPs,
		// Token: 0x040014D4 RID: 5332
		[DisplayName("QP.INSIP")]
		InvalidSIPs,
		// Token: 0x040014D5 RID: 5333
		[DisplayName("QP.SSIP")]
		SuccessfulSIPs,
		// Token: 0x040014D6 RID: 5334
		[DisplayName("QP.FSIP")]
		FailedSIPs
	}
}
