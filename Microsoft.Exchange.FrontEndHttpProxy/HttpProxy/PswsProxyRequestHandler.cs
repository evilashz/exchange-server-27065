using System;
using System.Net;
using System.Web;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000C7 RID: 199
	internal class PswsProxyRequestHandler : RwsPswsProxyRequestHandlerBase<WebServicesService>
	{
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x0002BBC5 File Offset: 0x00029DC5
		protected override string ServiceName
		{
			get
			{
				return "PowerShell Web Service";
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0002BBCC File Offset: 0x00029DCC
		protected override bool ShouldCopyHeaderToServerRequest(string headerName)
		{
			return string.Equals(headerName, "client-request-id", StringComparison.OrdinalIgnoreCase) || (!string.Equals(headerName, WellKnownHeader.CmdletProxyIsOn, StringComparison.OrdinalIgnoreCase) && base.ShouldCopyHeaderToServerRequest(headerName));
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0002BBF5 File Offset: 0x00029DF5
		protected override void AddProtocolSpecificHeadersToServerRequest(WebHeaderCollection headers)
		{
			headers.Add("public-server-uri", base.ClientRequest.Url.GetLeftPart(UriPartial.Authority));
			base.AddProtocolSpecificHeadersToServerRequest(headers);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0002BC1C File Offset: 0x00029E1C
		protected override void DoProtocolSpecificBeginProcess()
		{
			base.DoProtocolSpecificBeginProcess();
			string message;
			if (!this.AuthorizeOAuthRequest(out message))
			{
				throw new HttpException(403, message);
			}
			string domain;
			if (base.TryGetTenantDomain("organization", out domain))
			{
				base.IsDomainBasedRequest = true;
				base.Domain = domain;
			}
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0002BC64 File Offset: 0x00029E64
		private bool AuthorizeOAuthRequest(out string errorMsg)
		{
			OAuthIdentity oauthIdentity = base.HttpContext.User.Identity as OAuthIdentity;
			string empty = string.Empty;
			errorMsg = string.Empty;
			if (oauthIdentity != null && base.TryGetTenantDomain("organization", out empty))
			{
				string text = string.Empty;
				if (oauthIdentity.OrganizationId != null)
				{
					text = oauthIdentity.OrganizationId.ConfigurationUnit.ToString();
				}
				if (!string.IsNullOrEmpty(text) && string.Compare(text, empty, true) != 0)
				{
					errorMsg = string.Format("{0} is not a authorized tenant. The authorized tenant is {1}", empty, text);
					return false;
				}
			}
			return true;
		}

		// Token: 0x040004A9 RID: 1193
		private const string TenantParameterName = "organization";
	}
}
