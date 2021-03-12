using System;
using System.Web;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x02000007 RID: 7
	internal static class BrandingUtilities
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00004281 File Offset: 0x00002481
		internal static bool HasMHCookie()
		{
			return HttpContext.Current.Request.Cookies["MH"] != null;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000042A4 File Offset: 0x000024A4
		internal static bool IsBranded()
		{
			return VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaDeployment.IsBranded.Enabled && BrandingUtilities.HasMHCookie();
		}

		// Token: 0x040001B1 RID: 433
		internal const string MHCookieName = "MH";
	}
}
