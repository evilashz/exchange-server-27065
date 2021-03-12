using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.ExSmtpClient
{
	// Token: 0x02000706 RID: 1798
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class SmtpConstants
	{
		// Token: 0x0400206F RID: 8303
		internal static readonly string EhloCommand = "EHLO ";

		// Token: 0x04002070 RID: 8304
		internal static readonly string XAnonymousTlsCommand = "X-ANONYMOUSTLS";

		// Token: 0x04002071 RID: 8305
		internal static readonly string StartTlsCommand = "STARTTLS";

		// Token: 0x04002072 RID: 8306
		internal static readonly string AuthCommand = "X-EXPS EXCHANGEAUTH SHA256 ";

		// Token: 0x04002073 RID: 8307
		internal static readonly string AuthLoginCommand = "AUTH LOGIN ";

		// Token: 0x04002074 RID: 8308
		internal static readonly string MailFromCommand = "MAIL FROM: ";

		// Token: 0x04002075 RID: 8309
		internal static readonly string RcptToCommand = "RCPT TO: ";

		// Token: 0x04002076 RID: 8310
		internal static readonly string BdatCommand = "BDAT {0} LAST\r\n";

		// Token: 0x04002077 RID: 8311
		internal static readonly string RsetCommand = "RSET";

		// Token: 0x04002078 RID: 8312
		internal static readonly string QuitCommand = "QUIT";

		// Token: 0x04002079 RID: 8313
		internal static readonly string NoNDR = " NOTIFY=NEVER";

		// Token: 0x0400207A RID: 8314
		internal static readonly string NDRForFailure = " NOTIFY=FAILURE";

		// Token: 0x0400207B RID: 8315
		internal static readonly string XSHADOW = "XSHADOW ";

		// Token: 0x0400207C RID: 8316
		internal static readonly string CrLf = "\r\n";

		// Token: 0x0400207D RID: 8317
		internal static readonly string Cr = "\r";

		// Token: 0x0400207E RID: 8318
		internal static readonly string Lf = "\n";

		// Token: 0x0400207F RID: 8319
		internal static readonly string TargetSpn = "smtpsvc/{0}";

		// Token: 0x04002080 RID: 8320
		internal static readonly int BufferSize = 16384;

		// Token: 0x04002081 RID: 8321
		internal static readonly int NetworkingTimeout = 60000;
	}
}
