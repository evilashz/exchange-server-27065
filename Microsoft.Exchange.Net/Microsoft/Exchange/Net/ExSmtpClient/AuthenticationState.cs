using System;

namespace Microsoft.Exchange.Net.ExSmtpClient
{
	// Token: 0x020006FE RID: 1790
	internal enum AuthenticationState
	{
		// Token: 0x0400203D RID: 8253
		UnInitialized,
		// Token: 0x0400203E RID: 8254
		Initialized,
		// Token: 0x0400203F RID: 8255
		Negotiating,
		// Token: 0x04002040 RID: 8256
		Secured
	}
}
