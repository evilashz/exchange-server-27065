using System;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.Protocols;

namespace Microsoft.Exchange.HttpProxy.Common
{
	// Token: 0x02000005 RID: 5
	internal static class Extensions
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000023FC File Offset: 0x000005FC
		public static string GetFullRawUrl(this HttpRequest httpRequest)
		{
			ArgumentValidator.ThrowIfNull("httpRequest", httpRequest);
			string text = httpRequest.Url.IsDefaultPort ? string.Empty : (":" + httpRequest.Url.Port.ToString());
			return string.Concat(new string[]
			{
				httpRequest.Url.Scheme,
				"://",
				httpRequest.Url.Host,
				text,
				httpRequest.RawUrl
			});
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002484 File Offset: 0x00000684
		public static HttpRequestBase GetHttpRequestBase(this HttpRequest request)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			return new HttpRequestWrapper(request);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002498 File Offset: 0x00000698
		public static bool IsProbeRequest(this HttpRequestBase request)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			return request.IsProxyTestProbeRequest() || request.IsOutlookProbeRequest() || (!string.IsNullOrEmpty(request.UserAgent) && (request.UserAgent.IndexOf(Constants.MsExchMonString, StringComparison.OrdinalIgnoreCase) >= 0 || request.UserAgent.IndexOf(Constants.ActiveMonProbe, StringComparison.OrdinalIgnoreCase) >= 0 || request.UserAgent.IndexOf(Constants.LamProbe, StringComparison.OrdinalIgnoreCase) >= 0 || request.UserAgent.IndexOf(Constants.EasProbe, StringComparison.OrdinalIgnoreCase) >= 0));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002528 File Offset: 0x00000728
		public static bool IsProxyTestProbeRequest(this HttpRequestBase request)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			string text = request.Headers[Constants.ProbeHeaderName];
			return !string.IsNullOrEmpty(text) && string.Equals(text.Trim(), WellKnownHeader.LocalProbeHeaderValue, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000256C File Offset: 0x0000076C
		public static bool IsOutlookProbeRequest(this HttpRequestBase request)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			if (!string.IsNullOrEmpty(request.UserAgent) && request.UserAgent.IndexOf("MSRPC", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				string text = request.Headers["cookie"];
				return !string.IsNullOrEmpty(text) && text.IndexOf(Constants.ActiveMonProbe, StringComparison.OrdinalIgnoreCase) >= 0;
			}
			string text2 = request.Headers["X-ClientApplication"];
			return !string.IsNullOrEmpty(text2) && text2.IndexOf("MapiHttpClient", StringComparison.OrdinalIgnoreCase) >= 0;
		}
	}
}
