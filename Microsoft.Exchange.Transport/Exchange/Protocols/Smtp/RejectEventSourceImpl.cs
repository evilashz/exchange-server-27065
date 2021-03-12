using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004AD RID: 1197
	internal class RejectEventSourceImpl : RejectEventSource
	{
		// Token: 0x06003600 RID: 13824 RVA: 0x000DDD46 File Offset: 0x000DBF46
		private RejectEventSourceImpl(SmtpSession smtpSession) : base(smtpSession)
		{
		}

		// Token: 0x06003601 RID: 13825 RVA: 0x000DDD4F File Offset: 0x000DBF4F
		public static RejectEventSource Create(SmtpSession smtpSession)
		{
			return new RejectEventSourceImpl(smtpSession);
		}

		// Token: 0x06003602 RID: 13826 RVA: 0x000DDD57 File Offset: 0x000DBF57
		public override void Disconnect()
		{
			base.SmtpSession.Disconnect();
		}

		// Token: 0x06003603 RID: 13827 RVA: 0x000DDD64 File Offset: 0x000DBF64
		public override CertificateValidationStatus ValidateCertificate()
		{
			return base.SmtpSession.ValidateCertificate();
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x000DDD71 File Offset: 0x000DBF71
		public override CertificateValidationStatus ValidateCertificate(string domain, out string matchedCertDomain)
		{
			return base.SmtpSession.ValidateCertificate(domain, out matchedCertDomain);
		}
	}
}
