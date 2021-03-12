using System;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x02000041 RID: 65
	public abstract class EndOfAuthenticationEventSource : ReceiveEventSource
	{
		// Token: 0x06000189 RID: 393 RVA: 0x0000626C File Offset: 0x0000446C
		internal EndOfAuthenticationEventSource(SmtpSession smtpSession) : base(smtpSession)
		{
		}

		// Token: 0x0600018A RID: 394
		public abstract void RejectAuthentication(SmtpResponse response);
	}
}
