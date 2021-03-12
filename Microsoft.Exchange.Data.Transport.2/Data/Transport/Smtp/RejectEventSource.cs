using System;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x02000044 RID: 68
	public abstract class RejectEventSource : ReceiveEventSource
	{
		// Token: 0x06000193 RID: 403 RVA: 0x00006287 File Offset: 0x00004487
		internal RejectEventSource(SmtpSession smtpSession) : base(smtpSession)
		{
		}
	}
}
