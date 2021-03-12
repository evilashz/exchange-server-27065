using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004A7 RID: 1191
	internal class ConnectEventSourceImpl : ConnectEventSource
	{
		// Token: 0x060035D8 RID: 13784 RVA: 0x000DD9A6 File Offset: 0x000DBBA6
		private ConnectEventSourceImpl(SmtpSession smtpSession) : base(smtpSession)
		{
		}

		// Token: 0x060035D9 RID: 13785 RVA: 0x000DD9AF File Offset: 0x000DBBAF
		public static ConnectEventSource Create(SmtpSession smtpSession)
		{
			return new ConnectEventSourceImpl(smtpSession);
		}

		// Token: 0x060035DA RID: 13786 RVA: 0x000DD9B7 File Offset: 0x000DBBB7
		public override void RejectConnection(SmtpResponse response)
		{
			if (response.Equals(SmtpResponse.Empty))
			{
				throw new ArgumentException("Argument cannot be response.Empty", "response");
			}
			base.SmtpSession.SmtpResponse = response;
			this.Disconnect();
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x000DD9E9 File Offset: 0x000DBBE9
		public override void Disconnect()
		{
			base.SmtpSession.Disconnect();
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x000DD9F6 File Offset: 0x000DBBF6
		public override CertificateValidationStatus ValidateCertificate()
		{
			return base.SmtpSession.ValidateCertificate();
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x000DDA03 File Offset: 0x000DBC03
		public override CertificateValidationStatus ValidateCertificate(string domain, out string matchedCertDomain)
		{
			return base.SmtpSession.ValidateCertificate(domain, out matchedCertDomain);
		}
	}
}
