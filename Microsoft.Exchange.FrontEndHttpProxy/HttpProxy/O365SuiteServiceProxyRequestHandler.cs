using System;
using System.Net;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000BB RID: 187
	internal class O365SuiteServiceProxyRequestHandler : BEServerCookieProxyRequestHandler<WebServicesService>
	{
		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0002AD1E File Offset: 0x00028F1E
		protected override ClientAccessType ClientAccessType
		{
			get
			{
				return ClientAccessType.Internal;
			}
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0002AD24 File Offset: 0x00028F24
		protected override void AddProtocolSpecificHeadersToServerRequest(WebHeaderCollection headers)
		{
			headers["RPSPUID"] = (string)base.HttpContext.Items["RPSPUID"];
			headers["RPSOrgIdPUID"] = (string)base.HttpContext.Items["RPSOrgIdPUID"];
			base.AddProtocolSpecificHeadersToServerRequest(headers);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0002AD84 File Offset: 0x00028F84
		protected override Uri GetTargetBackEndServerUrl()
		{
			Uri targetBackEndServerUrl = base.GetTargetBackEndServerUrl();
			return UrlUtilities.FixIntegratedAuthUrlForBackEnd(targetBackEndServerUrl);
		}
	}
}
