using System;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x02000023 RID: 35
	public enum SmtpResponseType
	{
		// Token: 0x0400004B RID: 75
		Unknown,
		// Token: 0x0400004C RID: 76
		Success,
		// Token: 0x0400004D RID: 77
		IntermediateSuccess,
		// Token: 0x0400004E RID: 78
		TransientError,
		// Token: 0x0400004F RID: 79
		PermanentError
	}
}
