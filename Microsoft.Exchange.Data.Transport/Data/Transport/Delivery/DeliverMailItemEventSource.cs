using System;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Data.Transport.Delivery
{
	// Token: 0x02000059 RID: 89
	public abstract class DeliverMailItemEventSource
	{
		// Token: 0x06000206 RID: 518
		public abstract void FailQueue(SmtpResponse smtpResponse);

		// Token: 0x06000207 RID: 519
		public abstract void DeferQueue(SmtpResponse smtpResponse);

		// Token: 0x06000208 RID: 520
		public abstract void DeferQueue(SmtpResponse smtpResponse, TimeSpan interval);

		// Token: 0x06000209 RID: 521
		public abstract void AckMailItemSuccess(SmtpResponse smtpResponse);

		// Token: 0x0600020A RID: 522
		public abstract void AckMailItemDefer(SmtpResponse smtpResponse);

		// Token: 0x0600020B RID: 523
		public abstract void AckMailItemFail(SmtpResponse smtpResponse);

		// Token: 0x0600020C RID: 524
		public abstract void AckRecipientSuccess(EnvelopeRecipient recipient, SmtpResponse smtpResponse);

		// Token: 0x0600020D RID: 525
		public abstract void AckRecipientDefer(EnvelopeRecipient recipient, SmtpResponse smtpResponse);

		// Token: 0x0600020E RID: 526
		public abstract void AckRecipientFail(EnvelopeRecipient recipient, SmtpResponse smtpResponse);

		// Token: 0x0600020F RID: 527
		internal abstract void AddDsnParameters(string key, object value);

		// Token: 0x06000210 RID: 528
		internal abstract bool TryGetDsnParameters(string key, out object value);

		// Token: 0x06000211 RID: 529
		internal abstract void AddDsnParameters(EnvelopeRecipient recipient, string key, object value);

		// Token: 0x06000212 RID: 530
		internal abstract bool TryGetDsnParameters(EnvelopeRecipient recipient, string key, out object value);

		// Token: 0x06000213 RID: 531
		public abstract void UnregisterConnection(SmtpResponse smtpResponse);
	}
}
