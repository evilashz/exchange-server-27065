using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000EF RID: 239
	[Flags]
	public enum ClientSMimeControlStatus : uint
	{
		// Token: 0x040005A6 RID: 1446
		None = 0U,
		// Token: 0x040005A7 RID: 1447
		NotInstalled = 1U,
		// Token: 0x040005A8 RID: 1448
		MustUpdate = 2U,
		// Token: 0x040005A9 RID: 1449
		Outdated = 4U,
		// Token: 0x040005AA RID: 1450
		OK = 8U,
		// Token: 0x040005AB RID: 1451
		ConnectionIsSSL = 16U
	}
}
