using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Web;

namespace Microsoft.Exchange.Security.Authentication
{
	// Token: 0x02000020 RID: 32
	public class AdfsSessionSecurityTokenHandler : SessionSecurityTokenHandler
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00007E75 File Offset: 0x00006075
		public AdfsSessionSecurityTokenHandler() : base(AdfsSessionSecurityTokenHandler.CreateTransforms())
		{
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00007E84 File Offset: 0x00006084
		protected override byte[] ApplyTransforms(byte[] cookie, bool outbound)
		{
			byte[] result = cookie;
			try
			{
				result = base.ApplyTransforms(cookie, outbound);
			}
			catch (AdfsConfigurationException ex)
			{
				ExTraceGlobals.AdfsAuthModuleTracer.TraceError<AdfsConfigurationException>(0L, "[AdfsSessionAuthModule::ApplyTransforms]: Exception occurred: {0}.", ex);
				HttpContext.Current.Response.Redirect(string.Format(CultureInfo.InvariantCulture, "{0}?msg={1}", new object[]
				{
					"/owa/auth/errorfe.aspx",
					ex.Reason
				}), true);
			}
			return result;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00007F04 File Offset: 0x00006104
		private static ReadOnlyCollection<CookieTransform> CreateTransforms()
		{
			X509Certificate2[] certificates = Utility.GetCertificates();
			return new List<CookieTransform>
			{
				new DeflateCookieTransform(),
				new AdfsRsaSignatureCookieTransform(certificates),
				new AdfsRsaEncryptionCookieTransform(certificates)
			}.AsReadOnly();
		}
	}
}
