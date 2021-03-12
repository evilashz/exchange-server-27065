using System;
using System.Net;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000B2 RID: 178
	internal class EwsJsonProxyRequestHandler : OwaProxyRequestHandler
	{
		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x00028E31 File Offset: 0x00027031
		protected override ClientAccessType ClientAccessType
		{
			get
			{
				return ClientAccessType.Internal;
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00028E34 File Offset: 0x00027034
		protected override void AddProtocolSpecificHeadersToServerRequest(WebHeaderCollection headers)
		{
			headers["RPSPUID"] = (string)base.HttpContext.Items["RPSPUID"];
			headers["RPSOrgIdPUID"] = (string)base.HttpContext.Items["RPSOrgIdPUID"];
			base.AddProtocolSpecificHeadersToServerRequest(headers);
			if (base.ClientRequest != null && string.Equals(base.ClientRequest.QueryString["action"], "GetWacIframeUrl", StringComparison.OrdinalIgnoreCase))
			{
				OwaProxyRequestHandler.AddProxyUriHeader(base.ClientRequest, headers);
			}
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00028EC8 File Offset: 0x000270C8
		protected override bool ShouldCopyHeaderToServerRequest(string headerName)
		{
			return !string.Equals(headerName, "X-OWA-ProxyUri", StringComparison.OrdinalIgnoreCase) && base.ShouldCopyHeaderToServerRequest(headerName);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00028EE4 File Offset: 0x000270E4
		protected override Uri GetTargetBackEndServerUrl()
		{
			Uri targetBackEndServerUrl = base.GetTargetBackEndServerUrl();
			return UrlUtilities.FixIntegratedAuthUrlForBackEnd(targetBackEndServerUrl);
		}

		// Token: 0x0400047A RID: 1146
		private const string LiveIdPuid = "RPSPUID";

		// Token: 0x0400047B RID: 1147
		private const string OrgIdPuid = "RPSOrgIdPUID";
	}
}
