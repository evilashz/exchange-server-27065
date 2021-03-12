using System;
using System.Web;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000C0 RID: 192
	internal class OwaCobrandingRedirProxyRequestHandler : ProxyRequestHandler
	{
		// Token: 0x060006B8 RID: 1720 RVA: 0x0002B3E4 File Offset: 0x000295E4
		internal static bool IsCobrandingRedirRequest(HttpRequest request)
		{
			return request.Url.LocalPath.EndsWith("cobrandingredir.aspx", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0002B3FC File Offset: 0x000295FC
		protected override AnchoredRoutingTarget TryDirectTargetCalculation()
		{
			BackEndServer randomDownLevelClientAccessServer = DownLevelServerManager.Instance.GetRandomDownLevelClientAccessServer();
			return new AnchoredRoutingTarget(new AnonymousAnchorMailbox(this), randomDownLevelClientAccessServer);
		}
	}
}
