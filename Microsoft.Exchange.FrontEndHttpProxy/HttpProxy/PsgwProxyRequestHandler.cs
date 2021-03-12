using System;
using System.Net;
using System.Web;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000D5 RID: 213
	internal class PsgwProxyRequestHandler : ProxyRequestHandler
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0002E05E File Offset: 0x0002C25E
		protected override bool ProxyKerberosAuthentication
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0002E064 File Offset: 0x0002C264
		public static bool IsPsgwRequest(HttpRequest request)
		{
			return !string.IsNullOrEmpty(request.Url.AbsolutePath) && (string.Compare(request.Url.AbsolutePath, "/psgw", StringComparison.OrdinalIgnoreCase) == 0 || request.Url.AbsolutePath.IndexOf("/psgw/", StringComparison.OrdinalIgnoreCase) == 0);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0002E0B6 File Offset: 0x0002C2B6
		protected override void OnInitializingHandler()
		{
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0002E0B8 File Offset: 0x0002C2B8
		protected override void AddProtocolSpecificHeadersToServerRequest(WebHeaderCollection headers)
		{
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0002E0BA File Offset: 0x0002C2BA
		protected override bool ShouldCopyHeaderToServerRequest(string headerName)
		{
			return true;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0002E0C0 File Offset: 0x0002C2C0
		protected override Uri GetTargetBackEndServerUrl()
		{
			UriBuilder uriBuilder = new UriBuilder(base.ClientRequest.Url);
			if (uriBuilder.Path.EndsWith("/healthchecktarget.htm"))
			{
				uriBuilder.Path = "/powershell/healthcheck.htm";
			}
			else
			{
				uriBuilder.Path = "/powershell";
			}
			return uriBuilder.Uri;
		}
	}
}
