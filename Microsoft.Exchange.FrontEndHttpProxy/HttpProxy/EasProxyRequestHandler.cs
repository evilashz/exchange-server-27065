using System;
using System.Net;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000AE RID: 174
	internal class EasProxyRequestHandler : OwaEcpProxyRequestHandler<MobileSyncService>
	{
		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x0002719F File Offset: 0x0002539F
		protected override string ProxyLogonUri
		{
			get
			{
				return "Proxy/";
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x000271A6 File Offset: 0x000253A6
		protected override string ProxyLogonQueryString
		{
			get
			{
				return "cmd=ProxyLogin";
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x000271AD File Offset: 0x000253AD
		protected override ClientAccessType ClientAccessType
		{
			get
			{
				return ClientAccessType.Internal;
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x000271B0 File Offset: 0x000253B0
		protected override DatacenterRedirectStrategy CreateDatacenterRedirectStrategy()
		{
			return new DefaultRedirectStrategy(this);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x000271B8 File Offset: 0x000253B8
		protected override Uri GetTargetBackEndServerUrl()
		{
			return new UriBuilder(base.GetTargetBackEndServerUrl())
			{
				Path = base.ClientRequest.ApplicationPath + "/Proxy" + base.ClientRequest.Url.AbsolutePath.Substring(base.ClientRequest.ApplicationPath.Length)
			}.Uri;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00027217 File Offset: 0x00025417
		protected override void DoProtocolSpecificBeginRequestLogging()
		{
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0002721C File Offset: 0x0002541C
		protected override void AddProtocolSpecificHeadersToServerRequest(WebHeaderCollection headers)
		{
			IIdentity identity = base.HttpContext.User.Identity;
			if (identity is WindowsIdentity || identity is ClientSecurityContextIdentity)
			{
				headers["X-EAS-Proxy"] = identity.GetSecurityIdentifier().ToString() + "," + identity.GetSafeName(true);
			}
			base.AddProtocolSpecificHeadersToServerRequest(headers);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00027278 File Offset: 0x00025478
		protected override bool ShouldCopyHeaderToServerRequest(string headerName)
		{
			return !string.Equals(headerName, "X-EAS-Proxy", StringComparison.OrdinalIgnoreCase) && base.ShouldCopyHeaderToServerRequest(headerName);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00027291 File Offset: 0x00025491
		protected override void SetProtocolSpecificProxyLogonRequestParameters(HttpWebRequest request)
		{
			request.ContentType = "text/xml";
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x000272A0 File Offset: 0x000254A0
		protected override bool TryHandleProtocolSpecificRequestErrors(Exception ex)
		{
			HttpException ex2 = ex as HttpException;
			if (ex2 != null && ex2.WebEventCode == 3004)
			{
				string text = base.ClientRequest.Headers["MS-ASProtocolVersion"];
				base.Logger.AppendGenericError("RuntimeErrorPostTooLarge", ex.ToString());
				if (!string.IsNullOrEmpty(text) && (text == "14.0" || text == "14.1"))
				{
					base.ClientResponse.StatusCode = 200;
					if (base.ClientResponse.IsClientConnected)
					{
						base.ClientResponse.ContentType = "application/vnd.ms-sync.wbxml";
						base.ClientResponse.OutputStream.Write(EasProxyRequestHandler.easRequestSizeTooLargeResponseBytes, 0, EasProxyRequestHandler.easRequestSizeTooLargeResponseBytes.Length);
					}
				}
				else
				{
					base.ClientResponse.StatusCode = 500;
				}
				base.Complete();
				return true;
			}
			return base.TryHandleProtocolSpecificRequestErrors(ex);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00027383 File Offset: 0x00025583
		protected override void RedirectIfNeeded(BackEndServer mailboxServer)
		{
		}

		// Token: 0x04000455 RID: 1109
		private const string ProxyHeader = "X-EAS-Proxy";

		// Token: 0x04000456 RID: 1110
		private const string EASProtocolVersion = "MS-ASProtocolVersion";

		// Token: 0x04000457 RID: 1111
		private const string WbXmlContentType = "application/vnd.ms-sync.wbxml";

		// Token: 0x04000458 RID: 1112
		private const string EasProxyLogonUri = "Proxy/";

		// Token: 0x04000459 RID: 1113
		private const string EasProxyLogonQueryString = "cmd=ProxyLogin";

		// Token: 0x0400045A RID: 1114
		private static byte[] easRequestSizeTooLargeResponseBytes = new byte[]
		{
			3,
			1,
			106,
			0,
			0,
			21,
			69,
			82,
			3,
			49,
			49,
			53,
			0,
			1,
			1
		};
	}
}
