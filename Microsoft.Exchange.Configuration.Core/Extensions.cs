using System;
using System.Collections.Specialized;
using System.Web;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x0200000A RID: 10
	internal static class Extensions
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002D53 File Offset: 0x00000F53
		public static string GetFriendlyName(this OrganizationId orgId)
		{
			if (orgId != null && orgId.OrganizationalUnit != null)
			{
				return orgId.OrganizationalUnit.Name;
			}
			return null;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002D74 File Offset: 0x00000F74
		internal static NameValueCollection GetUrlProperties(this Uri uri)
		{
			if (uri == null)
			{
				return null;
			}
			UriBuilder uriBuilder = new UriBuilder(uri);
			return HttpUtility.ParseQueryString(uriBuilder.Query.Replace(';', '&'));
		}
	}
}
