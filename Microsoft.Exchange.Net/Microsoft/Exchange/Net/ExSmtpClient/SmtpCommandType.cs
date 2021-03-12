using System;

namespace Microsoft.Exchange.Net.ExSmtpClient
{
	// Token: 0x02000707 RID: 1799
	internal enum SmtpCommandType
	{
		// Token: 0x04002083 RID: 8323
		Connect,
		// Token: 0x04002084 RID: 8324
		Ehlo,
		// Token: 0x04002085 RID: 8325
		Mail,
		// Token: 0x04002086 RID: 8326
		Recipient,
		// Token: 0x04002087 RID: 8327
		BDAT,
		// Token: 0x04002088 RID: 8328
		Quit,
		// Token: 0x04002089 RID: 8329
		XAnonymousTls,
		// Token: 0x0400208A RID: 8330
		Custom,
		// Token: 0x0400208B RID: 8331
		UnInit,
		// Token: 0x0400208C RID: 8332
		XSHADOW,
		// Token: 0x0400208D RID: 8333
		STARTTLS
	}
}
