using System;
using System.Web;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HttpProxy.Common
{
	// Token: 0x02000004 RID: 4
	internal static class AspNetHelper
	{
		// Token: 0x0600000C RID: 12 RVA: 0x0000235C File Offset: 0x0000055C
		public static string GetClientIpAsProxyHeader(HttpRequest httpRequest)
		{
			ArgumentValidator.ThrowIfNull("httpRequest", httpRequest);
			string text = httpRequest.Headers[Constants.OriginatingClientIpHeader];
			if (text == null || !HttpProxySettings.TrustClientXForwardedFor.Value)
			{
				text = httpRequest.UserHostAddress;
			}
			else
			{
				text = string.Format("{0},{1}", text, httpRequest.UserHostAddress);
			}
			return text;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023B0 File Offset: 0x000005B0
		public static string GetClientPortAsProxyHeader(HttpContext httpContext)
		{
			ArgumentValidator.ThrowIfNull("httpContext", httpContext);
			string text = httpContext.Request.Headers[Constants.OriginatingClientPortHeader];
			string text2 = GccUtils.GetClientPort(httpContext);
			if (!string.IsNullOrEmpty(text))
			{
				text2 = string.Format("{0},{1}", text, text2);
			}
			return text2;
		}
	}
}
