using System;

namespace Microsoft.Exchange.Data.Transport.Smtp
{
	// Token: 0x0200003E RID: 62
	public abstract class ReceiveEventSource
	{
		// Token: 0x06000181 RID: 385 RVA: 0x00006244 File Offset: 0x00004444
		internal ReceiveEventSource(SmtpSession smtpSession)
		{
			this.smtpSession = smtpSession;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00006253 File Offset: 0x00004453
		internal SmtpSession SmtpSession
		{
			get
			{
				return this.smtpSession;
			}
		}

		// Token: 0x06000183 RID: 387
		public abstract void Disconnect();

		// Token: 0x06000184 RID: 388
		public abstract CertificateValidationStatus ValidateCertificate();

		// Token: 0x06000185 RID: 389
		public abstract CertificateValidationStatus ValidateCertificate(string domain, out string matchedCertDomain);

		// Token: 0x04000164 RID: 356
		private readonly SmtpSession smtpSession;
	}
}
