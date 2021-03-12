using System;
using System.Web;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Protocols
{
	// Token: 0x02000826 RID: 2086
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class CafeHelper
	{
		// Token: 0x06002C38 RID: 11320 RVA: 0x00064682 File Offset: 0x00062882
		public static string GetSourceCafeServer(HttpRequest httpRequest)
		{
			if (httpRequest == null)
			{
				throw new ArgumentNullException("httpRequest");
			}
			return httpRequest.Headers[WellKnownHeader.XSourceCafeServer];
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x000646A2 File Offset: 0x000628A2
		public static string GetSourceCafeArrayUrl(HttpRequest httpRequest)
		{
			if (httpRequest == null)
			{
				throw new ArgumentNullException("httpRequest");
			}
			throw new NotImplementedException("OM: 1361029");
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x000646BC File Offset: 0x000628BC
		public static bool IsFromNativeProxy(HttpRequest httpRequest)
		{
			if (httpRequest == null)
			{
				throw new ArgumentNullException("httpRequest");
			}
			string b = httpRequest.Headers[CafeHelper.CafeProxyHandler];
			return string.Equals(CafeHelper.NativeHttpProxy, b, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04002683 RID: 9859
		public static readonly string CafeProxyHandler = "X-CafeProxyHandler";

		// Token: 0x04002684 RID: 9860
		public static readonly string NativeHttpProxy = "NativeHttpProxy";
	}
}
