using System;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Web;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000609 RID: 1545
	internal static class LoginUtil
	{
		// Token: 0x06004502 RID: 17666 RVA: 0x000D0B7C File Offset: 0x000CED7C
		static LoginUtil()
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection.Add("securityTrimmingEnabled", "true");
			LoginUtil.urlAuthSiteProvider.Initialize("urlAuthSiteProvider", nameValueCollection);
		}

		// Token: 0x06004503 RID: 17667 RVA: 0x000D0BBC File Offset: 0x000CEDBC
		public static bool CheckUrlAccess(string path)
		{
			if (!path.StartsWith("~"))
			{
				Uri uri = new Uri(HttpContext.Current.Request.Url, new Uri(path, UriKind.RelativeOrAbsolute));
				path = uri.PathAndQuery;
			}
			SiteMapNode siteMapNode = new SiteMapNode(LoginUtil.urlAuthSiteProvider, "authCheck", path);
			return siteMapNode.IsAccessibleToUser(HttpContext.Current);
		}

		// Token: 0x06004504 RID: 17668 RVA: 0x000D0C18 File Offset: 0x000CEE18
		public static bool IsInRoles(IPrincipal user, string[] roles)
		{
			for (int i = 0; i < roles.Length; i++)
			{
				if (user.IsInRole(roles[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04002E4C RID: 11852
		private static SiteMapProvider urlAuthSiteProvider = new EacSiteMapProvider();
	}
}
