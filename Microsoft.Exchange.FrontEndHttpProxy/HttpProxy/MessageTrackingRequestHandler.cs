using System;
using System.Web;
using Microsoft.Exchange.Net.Protocols;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000D4 RID: 212
	internal class MessageTrackingRequestHandler : EwsProxyRequestHandler
	{
		// Token: 0x06000738 RID: 1848 RVA: 0x0002E021 File Offset: 0x0002C221
		internal static bool IsMessageTrackingRequest(HttpRequest request)
		{
			return request.UserAgent != null && request.UserAgent.StartsWith(MessageTrackingRequestHandler.MessageTrackingUserAgentString);
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0002E03D File Offset: 0x0002C23D
		protected override AnchorMailbox ResolveAnchorMailbox()
		{
			return new LocalSiteAnchorMailbox(this);
		}

		// Token: 0x040004C3 RID: 1219
		private static readonly string MessageTrackingUserAgentString = WellKnownUserAgent.GetEwsNegoAuthUserAgent("Microsoft.Exchange.InfoWorker.Common.MessageTracking");
	}
}
