using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004CD RID: 1229
	internal interface IRoutingConfigProvider
	{
		// Token: 0x17001130 RID: 4400
		// (get) Token: 0x0600388F RID: 14479
		bool CheckDagSelectorHeader { get; }

		// Token: 0x17001131 RID: 4401
		// (get) Token: 0x06003890 RID: 14480
		bool LocalLoopDetectionEnabled { get; }

		// Token: 0x17001132 RID: 4402
		// (get) Token: 0x06003891 RID: 14481
		int LocalLoopDetectionSubDomainLeftToRightOffsetForPerfCounter { get; }

		// Token: 0x17001133 RID: 4403
		// (get) Token: 0x06003892 RID: 14482
		List<int> LocalLoopMessageDeferralIntervals { get; }

		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x06003893 RID: 14483
		int LocalLoopSubdomainDepth { get; }

		// Token: 0x17001135 RID: 4405
		// (get) Token: 0x06003894 RID: 14484
		int LoopDetectionNumberOfTransits { get; }
	}
}
