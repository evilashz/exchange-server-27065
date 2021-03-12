using System;
using System.Net;
using System.Web;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Net.Wopi;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000D2 RID: 210
	internal class WopiProxyRequestHandler : ProxyRequestHandler
	{
		// Token: 0x06000731 RID: 1841 RVA: 0x0002DE00 File Offset: 0x0002C000
		protected override AnchorMailbox ResolveAnchorMailbox()
		{
			UriBuilder uriBuilder = new UriBuilder(base.ClientRequest.Url);
			uriBuilder.Scheme = "https";
			uriBuilder.Port = 444;
			string userEmailAddress = WopiRequestPathHandler.GetUserEmailAddress(base.ClientRequest);
			if (string.IsNullOrEmpty(userEmailAddress))
			{
				return base.ResolveAnchorMailbox();
			}
			this.targetMailboxId = userEmailAddress;
			AnchorMailbox result;
			if (AnchorMailboxFactory.TryCreateFromMailboxGuid(this, userEmailAddress, out result))
			{
				return result;
			}
			base.Logger.Set(HttpProxyMetadata.RoutingHint, "Url-SMTP");
			return new SmtpAnchorMailbox(userEmailAddress, this);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0002DE84 File Offset: 0x0002C084
		protected override Uri GetTargetBackEndServerUrl()
		{
			Uri uri = base.GetTargetBackEndServerUrl();
			if (base.AnchoredRoutingTarget.BackEndServer.Version < Server.E15MinVersion)
			{
				throw new HttpException(500, string.Format("Version < E14 and a WOPI request?  Should not happen....  AnchorMailbox: {0}", base.AnchoredRoutingTarget.AnchorMailbox));
			}
			if (uri.Query.Length == 0)
			{
				throw new HttpException(400, "Unexpected query string format");
			}
			if (!string.IsNullOrEmpty(this.targetMailboxId))
			{
				UriBuilder uriBuilder = new UriBuilder(uri);
				uriBuilder.Path = WopiRequestPathHandler.StripEmailAddress(HttpUtility.UrlDecode(uriBuilder.Path), this.targetMailboxId);
				uriBuilder.Query = uri.Query.Substring(1) + "&UserEmail=" + HttpUtility.UrlEncode(this.targetMailboxId);
				uri = uriBuilder.Uri;
			}
			if (HttpProxySettings.DFPOWAVdirProxyEnabled.Value)
			{
				return UrlUtilities.FixDFPOWAVdirUrlForBackEnd(uri, HttpUtility.ParseQueryString(uri.Query)["vdir"]);
			}
			return uri;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0002DF73 File Offset: 0x0002C173
		protected override void AddProtocolSpecificHeadersToServerRequest(WebHeaderCollection headers)
		{
			OwaProxyRequestHandler.AddProxyUriHeader(base.ClientRequest, headers);
			base.AddProtocolSpecificHeadersToServerRequest(headers);
		}

		// Token: 0x040004C2 RID: 1218
		private string targetMailboxId;
	}
}
