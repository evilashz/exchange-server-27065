using System;
using System.Configuration;
using System.Web;
using Microsoft.Exchange.Configuration.Authorization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000386 RID: 902
	internal class OfficeStoreAvailableQueryProcessor : RbacQuery.RbacQueryProcessor
	{
		// Token: 0x17001F36 RID: 7990
		// (get) Token: 0x0600306A RID: 12394 RVA: 0x000938A8 File Offset: 0x00091AA8
		public static bool IsOfficeStoreAvailable
		{
			get
			{
				string text = ConfigurationManager.AppSettings["OfficeStoreUnavailable"];
				string a = HttpContext.Current.Request.QueryString["appStore"];
				return !string.Equals(a, "OSX", StringComparison.OrdinalIgnoreCase) && (string.IsNullOrWhiteSpace(text) || StringComparer.OrdinalIgnoreCase.Equals("false", text));
			}
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x0009390B File Offset: 0x00091B0B
		public override bool? TryIsInRole(ExchangeRunspaceConfiguration rbacConfiguration)
		{
			return new bool?(OfficeStoreAvailableQueryProcessor.IsOfficeStoreAvailable);
		}

		// Token: 0x0400236D RID: 9069
		internal const string RoleName = "OfficeStoreAvailable";
	}
}
