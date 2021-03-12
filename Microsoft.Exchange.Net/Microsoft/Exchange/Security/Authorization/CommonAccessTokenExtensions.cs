using System;
using System.Web;

namespace Microsoft.Exchange.Security.Authorization
{
	// Token: 0x02000649 RID: 1609
	internal static class CommonAccessTokenExtensions
	{
		// Token: 0x06001D2C RID: 7468 RVA: 0x0003528C File Offset: 0x0003348C
		public static string GetMemberName(this HttpContext httpContext)
		{
			if (httpContext != null && httpContext.Items != null)
			{
				CommonAccessToken commonAccessToken = httpContext.Items["Item-CommonAccessToken"] as CommonAccessToken;
				if (commonAccessToken != null && commonAccessToken.ExtensionData.ContainsKey("MemberName"))
				{
					return commonAccessToken.ExtensionData["MemberName"];
				}
			}
			return null;
		}
	}
}
