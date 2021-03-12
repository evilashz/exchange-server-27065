using System;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x0200003C RID: 60
	public class RsetCommandEventArgs : ReceiveCommandEventArgs
	{
		// Token: 0x0600017B RID: 379 RVA: 0x00006207 File Offset: 0x00004407
		internal RsetCommandEventArgs()
		{
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000620F File Offset: 0x0000440F
		internal RsetCommandEventArgs(SmtpSession smtpSession) : base(smtpSession)
		{
		}
	}
}
