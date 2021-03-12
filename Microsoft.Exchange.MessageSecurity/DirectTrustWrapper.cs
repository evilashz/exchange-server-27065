using System;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.MessageSecurity
{
	// Token: 0x02000005 RID: 5
	internal class DirectTrustWrapper : IDirectTrust
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002684 File Offset: 0x00000884
		public void Load()
		{
			DirectTrust.Load();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000268B File Offset: 0x0000088B
		public void Unload()
		{
			DirectTrust.Unload();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002692 File Offset: 0x00000892
		public SecurityIdentifier MapCertToSecurityIdentifier(X509Certificate2 certificate)
		{
			ArgumentValidator.ThrowIfNull("certificate", certificate);
			return DirectTrust.MapCertToSecurityIdentifier(certificate);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000026A5 File Offset: 0x000008A5
		public SecurityIdentifier MapCertToSecurityIdentifier(IX509Certificate2 certificate)
		{
			ArgumentValidator.ThrowIfNull("certificate", certificate);
			return DirectTrust.MapCertToSecurityIdentifier(certificate.Certificate);
		}
	}
}
