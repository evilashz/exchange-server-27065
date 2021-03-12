using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.WSTrust
{
	// Token: 0x02000B5B RID: 2907
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class CustomX509CertificateValidator : X509CertificateValidator
	{
		// Token: 0x06003E60 RID: 15968 RVA: 0x000A2C9B File Offset: 0x000A0E9B
		internal CustomX509CertificateValidator(IEnumerable<X509Certificate2> trustedCertificates)
		{
			this.trustedCertificates = trustedCertificates;
		}

		// Token: 0x06003E61 RID: 15969 RVA: 0x000A2CAC File Offset: 0x000A0EAC
		public override void Validate(X509Certificate2 certificate)
		{
			foreach (X509Certificate2 b in this.trustedCertificates)
			{
				if (this.IsSameRawCertData(certificate, b))
				{
					return;
				}
			}
			X509CertificateValidator.ChainTrust.Validate(certificate);
		}

		// Token: 0x06003E62 RID: 15970 RVA: 0x000A2D0C File Offset: 0x000A0F0C
		private bool IsSameRawCertData(X509Certificate2 a, X509Certificate2 b)
		{
			byte[] rawCertData = a.GetRawCertData();
			byte[] rawCertData2 = b.GetRawCertData();
			if (rawCertData.Length != rawCertData2.Length)
			{
				return false;
			}
			for (int i = 0; i < rawCertData.Length; i++)
			{
				if (rawCertData[i] != rawCertData2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04003659 RID: 13913
		private IEnumerable<X509Certificate2> trustedCertificates;
	}
}
