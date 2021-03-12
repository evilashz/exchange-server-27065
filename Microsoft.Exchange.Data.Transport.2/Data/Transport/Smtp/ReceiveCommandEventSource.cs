using System;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x02000042 RID: 66
	public abstract class ReceiveCommandEventSource : ReceiveEventSource
	{
		// Token: 0x0600018B RID: 395 RVA: 0x00006275 File Offset: 0x00004475
		internal ReceiveCommandEventSource(SmtpSession smtpSession) : base(smtpSession)
		{
		}

		// Token: 0x0600018C RID: 396
		public abstract void RejectCommand(SmtpResponse response);

		// Token: 0x0600018D RID: 397
		public abstract void RejectCommand(SmtpResponse response, string trackingContext);
	}
}
