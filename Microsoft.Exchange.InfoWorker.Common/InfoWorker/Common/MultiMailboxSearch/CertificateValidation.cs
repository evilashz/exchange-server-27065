using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001BE RID: 446
	internal static class CertificateValidation
	{
		// Token: 0x06000C17 RID: 3095 RVA: 0x00034B78 File Offset: 0x00032D78
		internal static bool CertificateErrorHandler(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return sslPolicyErrors == SslPolicyErrors.None || sslPolicyErrors == SslPolicyErrors.RemoteCertificateNameMismatch || SslConfiguration.AllowInternalUntrustedCerts;
		}
	}
}
