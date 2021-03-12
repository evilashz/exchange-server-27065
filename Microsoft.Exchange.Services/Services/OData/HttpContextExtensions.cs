using System;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.Protocols;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000DF3 RID: 3571
	internal static class HttpContextExtensions
	{
		// Token: 0x06005C89 RID: 23689 RVA: 0x00120710 File Offset: 0x0011E910
		internal static Uri GetServiceRootUri(this HttpContext httpContext)
		{
			Uri requestUri = httpContext.GetRequestUri();
			return new UriBuilder
			{
				Host = requestUri.Host,
				Scheme = requestUri.Scheme,
				Port = requestUri.Port,
				Path = "/EWS/OData/"
			}.Uri;
		}

		// Token: 0x06005C8A RID: 23690 RVA: 0x00120760 File Offset: 0x0011E960
		internal static Uri GetRequestUri(this HttpContext httpContext)
		{
			ArgumentValidator.ThrowIfNull("httpContext", httpContext);
			Uri result = httpContext.Request.Url;
			string text = httpContext.Request.Headers[WellKnownHeader.MsExchProxyUri];
			if (!string.IsNullOrEmpty(text))
			{
				result = new Uri(text);
			}
			return result;
		}
	}
}
