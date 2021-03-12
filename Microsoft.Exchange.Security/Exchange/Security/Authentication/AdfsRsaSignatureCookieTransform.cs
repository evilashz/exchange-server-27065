using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Web;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x0200001E RID: 30
	public class AdfsRsaSignatureCookieTransform : AdfsCookieTransform
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00007758 File Offset: 0x00005958
		public AdfsRsaSignatureCookieTransform(X509Certificate2[] certificates) : base(certificates)
		{
			for (int i = 0; i < certificates.Length; i++)
			{
				this.Transforms[i] = new RsaSignatureCookieTransform(certificates[i]);
			}
		}
	}
}
