using System;
using System.Globalization;
using System.Web;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000C4 RID: 196
	internal class OwaResourceProxyRequestHandler : ProxyRequestHandler
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0002B70A File Offset: 0x0002990A
		protected override bool WillAddProtocolSpecificCookiesToClientResponse
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0002B710 File Offset: 0x00029910
		internal static bool CanHandle(HttpRequest httpRequest)
		{
			HttpCookie httpCookie = httpRequest.Cookies[Constants.AnonResource];
			return httpCookie != null && string.Compare(httpCookie.Value, "true", CultureInfo.InvariantCulture, CompareOptions.IgnoreCase) == 0 && BEResourceRequestHandler.IsResourceRequest(httpRequest.Url.LocalPath);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0002B760 File Offset: 0x00029960
		protected override AnchorMailbox ResolveAnchorMailbox()
		{
			HttpCookie httpCookie = base.ClientRequest.Cookies[Constants.AnonResourceBackend];
			if (httpCookie != null)
			{
				this.savedBackendServer = httpCookie.Value;
			}
			if (!string.IsNullOrEmpty(this.savedBackendServer))
			{
				base.Logger.Set(HttpProxyMetadata.RoutingHint, Constants.AnonResourceBackend + "-Cookie");
				ExTraceGlobals.VerboseTracer.TraceDebug<HttpCookie, int>((long)this.GetHashCode(), "[OwaResourceProxyRequestHandler::ResolveAnchorMailbox]: AnonResourceBackend cookie used: {0}; context {1}.", httpCookie, base.TraceContext);
				return new ServerInfoAnchorMailbox(BackEndServer.FromString(this.savedBackendServer), this);
			}
			return new AnonymousAnchorMailbox(this);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0002B7F5 File Offset: 0x000299F5
		protected override bool ShouldBackendRequestBeAnonymous()
		{
			return true;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0002B7F8 File Offset: 0x000299F8
		protected override void CopySupplementalCookiesToClientResponse()
		{
			string text = null;
			if (base.AnchoredRoutingTarget != null && base.AnchoredRoutingTarget.BackEndServer != null)
			{
				text = base.AnchoredRoutingTarget.BackEndServer.ToString();
			}
			if (!string.IsNullOrEmpty(text) && this.savedBackendServer != text)
			{
				HttpCookie httpCookie = new HttpCookie(Constants.AnonResourceBackend, text);
				httpCookie.HttpOnly = true;
				httpCookie.Secure = base.ClientRequest.IsSecureConnection;
				base.ClientResponse.Cookies.Add(httpCookie);
			}
			base.CopySupplementalCookiesToClientResponse();
		}

		// Token: 0x040004A4 RID: 1188
		private string savedBackendServer;
	}
}
