using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004A9 RID: 1193
	internal class EndOfAuthenticationEventSourceImpl : EndOfAuthenticationEventSource
	{
		// Token: 0x060035E0 RID: 13792 RVA: 0x000DDA21 File Offset: 0x000DBC21
		private EndOfAuthenticationEventSourceImpl(SmtpSession smtpSession) : base(smtpSession)
		{
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x000DDA2A File Offset: 0x000DBC2A
		public static EndOfAuthenticationEventSource Create(SmtpSession smtpSession)
		{
			return new EndOfAuthenticationEventSourceImpl(smtpSession);
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x000DDA32 File Offset: 0x000DBC32
		public override void RejectAuthentication(SmtpResponse response)
		{
			if (response.Equals(SmtpResponse.Empty))
			{
				throw new ArgumentException("Argument cannot be response.Empty", "response");
			}
			base.SmtpSession.SmtpResponse = response;
			base.SmtpSession.ExecutionControl.HaltExecution();
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x000DDA6E File Offset: 0x000DBC6E
		public override void Disconnect()
		{
			base.SmtpSession.Disconnect();
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x000DDA7B File Offset: 0x000DBC7B
		public override CertificateValidationStatus ValidateCertificate()
		{
			return base.SmtpSession.ValidateCertificate();
		}

		// Token: 0x060035E5 RID: 13797 RVA: 0x000DDA88 File Offset: 0x000DBC88
		public override CertificateValidationStatus ValidateCertificate(string domain, out string matchedCertDomain)
		{
			return base.SmtpSession.ValidateCertificate(domain, out matchedCertDomain);
		}
	}
}
