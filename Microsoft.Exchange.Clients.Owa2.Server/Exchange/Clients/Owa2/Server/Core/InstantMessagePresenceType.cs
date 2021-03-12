using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000146 RID: 326
	public enum InstantMessagePresenceType
	{
		// Token: 0x04000774 RID: 1908
		None,
		// Token: 0x04000775 RID: 1909
		Online = 3000,
		// Token: 0x04000776 RID: 1910
		IdleOnline = 4500,
		// Token: 0x04000777 RID: 1911
		Busy = 6000,
		// Token: 0x04000778 RID: 1912
		IdleBusy = 7500,
		// Token: 0x04000779 RID: 1913
		DoNotDisturb = 9000,
		// Token: 0x0400077A RID: 1914
		BeRightBack = 12000,
		// Token: 0x0400077B RID: 1915
		Away = 15000,
		// Token: 0x0400077C RID: 1916
		Offline = 18000
	}
}
