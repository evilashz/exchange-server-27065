using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004AC RID: 1196
	internal class ReceiveMessageEventSourceImpl : ReceiveMessageEventSource
	{
		// Token: 0x060035F7 RID: 13815 RVA: 0x000DDC09 File Offset: 0x000DBE09
		private ReceiveMessageEventSourceImpl(SmtpSession smtpSession, MailItem mailItem) : base(smtpSession)
		{
			this.mailItem = mailItem;
		}

		// Token: 0x060035F8 RID: 13816 RVA: 0x000DDC19 File Offset: 0x000DBE19
		public static ReceiveMessageEventSource Create(SmtpSession smtpSession, MailItem mailItem)
		{
			ArgumentValidator.ThrowIfNull("mailItem", mailItem);
			return new ReceiveMessageEventSourceImpl(smtpSession, mailItem);
		}

		// Token: 0x060035F9 RID: 13817 RVA: 0x000DDC2D File Offset: 0x000DBE2D
		public override void RejectMessage(SmtpResponse response)
		{
			this.RejectMessage(response, null);
		}

		// Token: 0x060035FA RID: 13818 RVA: 0x000DDC37 File Offset: 0x000DBE37
		public override void RejectMessage(SmtpResponse response, string context)
		{
			if (response.Equals(SmtpResponse.Empty))
			{
				throw new ArgumentException("Argument cannot be response.Empty", "response");
			}
			base.SmtpSession.RejectMessage(response, context);
		}

		// Token: 0x060035FB RID: 13819 RVA: 0x000DDC64 File Offset: 0x000DBE64
		public override void DiscardMessage(SmtpResponse response, string context)
		{
			base.SmtpSession.DiscardMessage(response, context);
		}

		// Token: 0x060035FC RID: 13820 RVA: 0x000DDC73 File Offset: 0x000DBE73
		public override void Disconnect()
		{
			base.SmtpSession.Disconnect();
		}

		// Token: 0x060035FD RID: 13821 RVA: 0x000DDC80 File Offset: 0x000DBE80
		public override CertificateValidationStatus ValidateCertificate()
		{
			return base.SmtpSession.ValidateCertificate();
		}

		// Token: 0x060035FE RID: 13822 RVA: 0x000DDC8D File Offset: 0x000DBE8D
		public override CertificateValidationStatus ValidateCertificate(string domain, out string matchedCertDomain)
		{
			return base.SmtpSession.ValidateCertificate(domain, out matchedCertDomain);
		}

		// Token: 0x060035FF RID: 13823 RVA: 0x000DDC9C File Offset: 0x000DBE9C
		public override void Quarantine(IEnumerable<EnvelopeRecipient> recipients, string quarantineReason)
		{
			HeaderList headers = this.mailItem.Message.MimeDocument.RootPart.Headers;
			if (headers.FindFirst("X-MS-Exchange-Organization-Quarantine") != null)
			{
				return;
			}
			if (recipients == null)
			{
				recipients = this.mailItem.Recipients;
			}
			SmtpResponse smtpResponse = new SmtpResponse("550", "5.2.1", new string[]
			{
				quarantineReason
			});
			IList<EnvelopeRecipient> list = (recipients as IList<EnvelopeRecipient>) ?? new List<EnvelopeRecipient>(recipients);
			for (int i = list.Count - 1; i >= 0; i--)
			{
				EnvelopeRecipient envelopeRecipient = list[i];
				MailRecipient mailRecipient = ((MailRecipientWrapper)envelopeRecipient).MailRecipient;
				mailRecipient.Ack(AckStatus.Quarantine, smtpResponse);
			}
		}

		// Token: 0x04001BA8 RID: 7080
		private readonly MailItem mailItem;
	}
}
