using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000062 RID: 98
	public class AutodiscoverX509CertificateValidator : X509CertificateValidator
	{
		// Token: 0x060002C7 RID: 711 RVA: 0x00012830 File Offset: 0x00010A30
		public override void Validate(X509Certificate2 certificate)
		{
			try
			{
				X509CertificateValidator.ChainTrust.Validate(certificate);
			}
			catch (SecurityTokenValidationException)
			{
				PerformanceCounters.UpdateCertAuthRequestsFailed(HttpContext.Current.Request.UserAgent);
				throw;
			}
		}
	}
}
