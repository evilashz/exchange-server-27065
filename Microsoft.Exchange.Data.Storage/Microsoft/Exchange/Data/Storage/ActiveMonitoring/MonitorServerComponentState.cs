using System;

namespace Microsoft.Exchange.Data.Storage.ActiveMonitoring
{
	// Token: 0x02000325 RID: 805
	public enum MonitorServerComponentState
	{
		// Token: 0x0400156F RID: 5487
		Unknown,
		// Token: 0x04001570 RID: 5488
		NotApplicable,
		// Token: 0x04001571 RID: 5489
		Online,
		// Token: 0x04001572 RID: 5490
		PartiallyOnline,
		// Token: 0x04001573 RID: 5491
		Offline,
		// Token: 0x04001574 RID: 5492
		Functional,
		// Token: 0x04001575 RID: 5493
		Sidelined
	}
}
