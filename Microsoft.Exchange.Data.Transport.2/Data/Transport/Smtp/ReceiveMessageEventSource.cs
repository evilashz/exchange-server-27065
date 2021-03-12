using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x02000043 RID: 67
	public abstract class ReceiveMessageEventSource : ReceiveEventSource
	{
		// Token: 0x0600018E RID: 398 RVA: 0x0000627E File Offset: 0x0000447E
		internal ReceiveMessageEventSource(SmtpSession smtpSession) : base(smtpSession)
		{
		}

		// Token: 0x0600018F RID: 399
		public abstract void RejectMessage(SmtpResponse response);

		// Token: 0x06000190 RID: 400
		public abstract void RejectMessage(SmtpResponse response, string trackingContext);

		// Token: 0x06000191 RID: 401
		public abstract void DiscardMessage(SmtpResponse response, string trackingContext);

		// Token: 0x06000192 RID: 402
		public abstract void Quarantine(IEnumerable<EnvelopeRecipient> recipients, string quarantineReason);
	}
}
