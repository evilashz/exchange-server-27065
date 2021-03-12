using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Web;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200001D RID: 29
	public class AdfsRsaEncryptionCookieTransform : AdfsCookieTransform
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x00007724 File Offset: 0x00005924
		public AdfsRsaEncryptionCookieTransform(X509Certificate2[] certificates) : base(certificates)
		{
			for (int i = 0; i < certificates.Length; i++)
			{
				this.Transforms[i] = new RsaEncryptionCookieTransform(certificates[i]);
			}
		}
	}
}
