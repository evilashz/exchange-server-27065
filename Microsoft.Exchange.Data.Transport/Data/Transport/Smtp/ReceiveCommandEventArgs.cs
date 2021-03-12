using System;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x0200002C RID: 44
	public abstract class ReceiveCommandEventArgs : ReceiveEventArgs
	{
		// Token: 0x06000117 RID: 279 RVA: 0x00005CEB File Offset: 0x00003EEB
		internal ReceiveCommandEventArgs()
		{
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005CF3 File Offset: 0x00003EF3
		internal ReceiveCommandEventArgs(SmtpSession smtpSession) : base(smtpSession)
		{
		}
	}
}
