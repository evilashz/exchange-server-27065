using System;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020002F4 RID: 756
	[Flags]
	internal enum LogonExtendedRequestFlags : uint
	{
		// Token: 0x0400097E RID: 2430
		None = 0U,
		// Token: 0x0400097F RID: 2431
		UseLocaleInfo = 1U,
		// Token: 0x04000980 RID: 2432
		SetAuthContext = 2U,
		// Token: 0x04000981 RID: 2433
		AuthContextCompressed = 4U,
		// Token: 0x04000982 RID: 2434
		ApplicationId = 8U,
		// Token: 0x04000983 RID: 2435
		ReturnLocaleInfo = 16U,
		// Token: 0x04000984 RID: 2436
		TenantHint = 32U,
		// Token: 0x04000985 RID: 2437
		UnifiedLogon = 64U
	}
}
