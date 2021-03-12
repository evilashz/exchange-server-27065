using System;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x02000038 RID: 56
	public class NoopCommandEventArgs : ReceiveCommandEventArgs
	{
		// Token: 0x0600015D RID: 349 RVA: 0x000060AB File Offset: 0x000042AB
		internal NoopCommandEventArgs()
		{
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000060B3 File Offset: 0x000042B3
		internal NoopCommandEventArgs(SmtpSession smtpSession) : base(smtpSession)
		{
		}
	}
}
