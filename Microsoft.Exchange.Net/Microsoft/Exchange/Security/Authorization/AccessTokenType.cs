using System;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x02000644 RID: 1604
	public enum AccessTokenType
	{
		// Token: 0x04001D3F RID: 7487
		Windows,
		// Token: 0x04001D40 RID: 7488
		LiveId,
		// Token: 0x04001D41 RID: 7489
		LiveIdBasic,
		// Token: 0x04001D42 RID: 7490
		LiveIdNego2,
		// Token: 0x04001D43 RID: 7491
		OAuth,
		// Token: 0x04001D44 RID: 7492
		CompositeIdentity,
		// Token: 0x04001D45 RID: 7493
		Adfs,
		// Token: 0x04001D46 RID: 7494
		CertificateSid,
		// Token: 0x04001D47 RID: 7495
		RemotePowerShellDelegated,
		// Token: 0x04001D48 RID: 7496
		Anonymous
	}
}
