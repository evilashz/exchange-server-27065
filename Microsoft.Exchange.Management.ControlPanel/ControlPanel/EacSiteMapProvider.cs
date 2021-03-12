using System;
using System.Web;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001F2 RID: 498
	public class EacSiteMapProvider : XmlSiteMapProvider
	{
		// Token: 0x0600264C RID: 9804 RVA: 0x00075FC0 File Offset: 0x000741C0
		public override bool IsAccessibleToUser(HttpContext context, SiteMapNode node)
		{
			string relativePathToAppRoot = EcpUrl.GetRelativePathToAppRoot(node.Url);
			if (relativePathToAppRoot != null)
			{
				string rewriteUrl = EacFlightProvider.Instance.GetRewriteUrl(relativePathToAppRoot);
				if (!EacFlightProvider.Instance.IsUrlEnabled(rewriteUrl ?? relativePathToAppRoot))
				{
					return false;
				}
				if (rewriteUrl != null)
				{
					node = node.Clone();
					node.Url = EcpUrl.ReplaceRelativePath(node.Url, rewriteUrl, true);
				}
			}
			return base.IsAccessibleToUser(context, node);
		}
	}
}
