using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.EventLogs;

namespace Microsoft.Exchange.Services.WCF
{
	// Token: 0x02000B83 RID: 2947
	public class EWSX509CertificateValidator : X509CertificateValidator
	{
		// Token: 0x060055DE RID: 21982 RVA: 0x00110B48 File Offset: 0x0010ED48
		public EWSX509CertificateValidator()
		{
			this.validator = X509CertificateValidator.CreateChainTrustValidator(true, new X509ChainPolicy
			{
				RevocationMode = X509RevocationMode.Online
			});
		}

		// Token: 0x060055DF RID: 21983 RVA: 0x00110B78 File Offset: 0x0010ED78
		public override void Validate(X509Certificate2 certificate)
		{
			Exception ex = null;
			try
			{
				this.validator.Validate(certificate);
			}
			catch (CryptographicException ex2)
			{
				ex = ex2;
			}
			catch (SecurityTokenValidationException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				ServiceDiagnostics.LogExceptionWithTrace(ServicesEventLogConstants.Tuple_X509CerticateValidatorException, ex.Message, ExTraceGlobals.AuthenticationTracer, this, "[EWSX509CertificateValidator.Validate] hit Exception: {0}.", ex);
				throw ex;
			}
		}

		// Token: 0x04002ED5 RID: 11989
		private readonly X509CertificateValidator validator;
	}
}
