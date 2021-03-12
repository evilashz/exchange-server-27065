using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.IdentityModel.Web;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000013 RID: 19
	public abstract class AdfsCookieTransform : CookieTransform
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00006539 File Offset: 0x00004739
		protected AdfsCookieTransform(X509Certificate2[] certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			if (certificates.Length < 1)
			{
				throw new ArgumentException("certificates list cannot be empty");
			}
			this.Transforms = new CookieTransform[certificates.Length];
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00006570 File Offset: 0x00004770
		public override byte[] Decode(byte[] encoded)
		{
			foreach (CookieTransform cookieTransform in this.Transforms)
			{
				try
				{
					return cookieTransform.Decode(encoded);
				}
				catch (CryptographicException ex)
				{
					ExTraceGlobals.AdfsAuthModuleTracer.TraceError(0, 0L, string.Format("[AdfsCookieTransform] Decode failed, trying next. Total transforms: {0}, Exception: {1}", this.Transforms.Length, ex.ToString()));
				}
			}
			string message = string.Format("Decode failed, giving up!. Total transforms: {0}", this.Transforms.Length);
			ExTraceGlobals.AdfsAuthModuleTracer.TraceError(0, 0L, message);
			throw new AdfsConfigurationException(AdfsConfigErrorReason.CertificatesMismatch, message);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00006614 File Offset: 0x00004814
		public override byte[] Encode(byte[] value)
		{
			if (this.Transforms.Length == 0 || this.Transforms[0] == null)
			{
				ExTraceGlobals.AdfsAuthModuleTracer.TraceError(0, 0L, "No transforms, cannot encode!");
				throw new AdfsConfigurationException(AdfsConfigErrorReason.NoCertificates, "No transforms, cannot encode!");
			}
			return this.Transforms[0].Encode(value);
		}

		// Token: 0x04000127 RID: 295
		protected CookieTransform[] Transforms;
	}
}
