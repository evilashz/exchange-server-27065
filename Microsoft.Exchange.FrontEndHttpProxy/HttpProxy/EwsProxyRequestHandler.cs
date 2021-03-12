using System;
using System.IO;
using System.Net;
using System.Web;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000B3 RID: 179
	internal class EwsProxyRequestHandler : EwsAutodiscoverProxyRequestHandler
	{
		// Token: 0x0600065D RID: 1629 RVA: 0x00028F06 File Offset: 0x00027106
		internal EwsProxyRequestHandler() : this(false)
		{
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00028F0F File Offset: 0x0002710F
		internal EwsProxyRequestHandler(bool isOwa14EwsProxyRequest)
		{
			this.isOwa14EwsProxyRequest = isOwa14EwsProxyRequest;
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00028F1E File Offset: 0x0002711E
		protected override bool WillContentBeChangedDuringStreaming
		{
			get
			{
				return !base.IsWsSecurityRequest && base.ClientRequest.CanHaveBody() && (this.isOwa14EwsProxyRequest || base.ProxyToDownLevel || this.proxyForSameOrgExchangeOAuthCallToLowerVersion);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x00028F52 File Offset: 0x00027152
		protected override ClientAccessType ClientAccessType
		{
			get
			{
				return ClientAccessType.InternalNLBBypass;
			}
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00028F55 File Offset: 0x00027155
		protected override bool ShouldBlockCurrentOAuthRequest()
		{
			return !this.proxyForSameOrgExchangeOAuthCallToLowerVersion && base.ShouldBlockCurrentOAuthRequest();
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00028F67 File Offset: 0x00027167
		protected override void DoProtocolSpecificBeginRequestLogging()
		{
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00028F69 File Offset: 0x00027169
		protected override void AddProtocolSpecificHeadersToServerRequest(WebHeaderCollection headers)
		{
			base.AddProtocolSpecificHeadersToServerRequest(headers);
			if (this.proxyForSameOrgExchangeOAuthCallToLowerVersion)
			{
				headers.Remove("X-CommonAccessToken");
			}
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00028F88 File Offset: 0x00027188
		protected override StreamProxy BuildRequestStreamProxy(StreamProxy.StreamProxyType streamProxyType, Stream source, Stream target, byte[] buffer)
		{
			if (base.IsWsSecurityRequest || !base.ClientRequest.CanHaveBody())
			{
				return base.BuildRequestStreamProxy(streamProxyType, source, target, buffer);
			}
			if (!this.isOwa14EwsProxyRequest && !base.ProxyToDownLevel && !this.proxyForSameOrgExchangeOAuthCallToLowerVersion)
			{
				return base.BuildRequestStreamProxy(streamProxyType, source, target, buffer);
			}
			string requestVersionToAdd = null;
			if (this.isOwa14EwsProxyRequest)
			{
				if ("12.1".Equals(base.HttpContext.Request.QueryString["rv"]))
				{
					requestVersionToAdd = "Exchange2007_SP1";
				}
				else
				{
					requestVersionToAdd = "Exchange2010_SP1";
				}
			}
			return new EwsRequestStreamProxy(streamProxyType, source, target, buffer, this, base.ProxyToDownLevel || this.proxyForSameOrgExchangeOAuthCallToLowerVersion, this.proxyForSameOrgExchangeOAuthCallToLowerVersionWithNoSidUser, requestVersionToAdd);
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0002903C File Offset: 0x0002723C
		protected override Uri GetTargetBackEndServerUrl()
		{
			Uri targetBackEndServerUrl = base.GetTargetBackEndServerUrl();
			if (this.isOwa14EwsProxyRequest)
			{
				return new UriBuilder(targetBackEndServerUrl)
				{
					Path = "/ews/exchange.asmx",
					Query = string.Empty
				}.Uri;
			}
			if (targetBackEndServerUrl.AbsolutePath.IndexOf("ews/Nego2", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				return new UriBuilder(targetBackEndServerUrl)
				{
					Path = "/ews/exchange.asmx"
				}.Uri;
			}
			OAuthIdentity oauthIdentity = base.HttpContext.User.Identity as OAuthIdentity;
			if (oauthIdentity != null && !oauthIdentity.IsAppOnly && oauthIdentity.IsKnownFromSameOrgExchange && base.HttpContext.Request.UserAgent.StartsWith("ASProxy/CrossForest", StringComparison.InvariantCultureIgnoreCase))
			{
				if (FaultInjection.TraceTest<bool>((FaultInjection.LIDs)3548785981U))
				{
					throw new InvalidOAuthTokenException(OAuthErrors.TestOnlyExceptionDuringProxyDownLevelCheckNullSid, null, null);
				}
				this.proxyForSameOrgExchangeOAuthCallToLowerVersion = (base.ProxyToDownLevel || FaultInjection.TraceTest<bool>((FaultInjection.LIDs)2357603645U) || FaultInjection.TraceTest<bool>((FaultInjection.LIDs)3431345469U));
				if (this.proxyForSameOrgExchangeOAuthCallToLowerVersion || oauthIdentity.ActAsUser.IsUserVerified)
				{
					this.proxyForSameOrgExchangeOAuthCallToLowerVersionWithNoSidUser = (FaultInjection.TraceTest<bool>((FaultInjection.LIDs)3431345469U) || oauthIdentity.ActAsUser.Sid == null);
				}
			}
			return targetBackEndServerUrl;
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00029174 File Offset: 0x00027374
		protected override void OnInitializingHandler()
		{
			base.OnInitializingHandler();
			if (HttpProxyGlobals.ProtocolType == ProtocolType.Ews && !base.ClientRequest.IsAuthenticated)
			{
				base.IsWsSecurityRequest = base.ClientRequest.IsAnyWsSecurityRequest();
			}
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x000291A4 File Offset: 0x000273A4
		protected override AnchorMailbox ResolveAnchorMailbox()
		{
			string text = base.ClientRequest.Headers[Constants.PreferServerAffinityHeader];
			if (!string.IsNullOrEmpty(text) && text.Equals(bool.TrueString, StringComparison.OrdinalIgnoreCase))
			{
				HttpCookie httpCookie = base.ClientRequest.Cookies[Constants.BackEndOverrideCookieName];
				string text2 = (httpCookie == null) ? null : httpCookie.Value;
				if (!string.IsNullOrEmpty(text2))
				{
					try
					{
						BackEndServer backendServer = BackEndServer.FromString(text2);
						base.Logger.Set(HttpProxyMetadata.RoutingHint, Constants.BackEndOverrideCookieName);
						return new ServerInfoAnchorMailbox(backendServer, this);
					}
					catch (ArgumentException arg)
					{
						base.Logger.AppendGenericError("Unable to parse TargetServer: {0}", text2);
						ExTraceGlobals.ExceptionTracer.TraceDebug<string, ArgumentException>((long)this.GetHashCode(), "[EwsProxyRequestHandler::ResolveAnchorMailbox]: exception hit where target server was '{0}': {1}", text2, arg);
					}
				}
			}
			return base.ResolveAnchorMailbox();
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00029280 File Offset: 0x00027480
		protected override void CopySupplementalCookiesToClientResponse()
		{
			if (base.AnchoredRoutingTarget != null)
			{
				string value = base.ServerResponse.Headers["X-FromBackend-ServerAffinity"];
				if (!string.IsNullOrEmpty(value) && base.ClientRequest.Cookies[Constants.BackEndOverrideCookieName] == null)
				{
					HttpCookie httpCookie = new HttpCookie(Constants.BackEndOverrideCookieName, base.AnchoredRoutingTarget.BackEndServer.ToString());
					httpCookie.HttpOnly = true;
					httpCookie.Secure = base.ClientRequest.IsSecureConnection;
					base.ClientResponse.Cookies.Add(httpCookie);
				}
			}
			base.CopySupplementalCookiesToClientResponse();
		}

		// Token: 0x0400047C RID: 1148
		private const string Owa14EwsProxyRequestVersionHeader = "rv";

		// Token: 0x0400047D RID: 1149
		private const string Owa14EwsProxyE12SP1Version = "12.1";

		// Token: 0x0400047E RID: 1150
		private const string Exchange2007SP1Version = "Exchange2007_SP1";

		// Token: 0x0400047F RID: 1151
		private const string Exchange2010SP1Version = "Exchange2010_SP1";

		// Token: 0x04000480 RID: 1152
		private const string Nego2PathPrefix = "ews/Nego2";

		// Token: 0x04000481 RID: 1153
		private const string EwsPath = "/ews/exchange.asmx";

		// Token: 0x04000482 RID: 1154
		private readonly bool isOwa14EwsProxyRequest;

		// Token: 0x04000483 RID: 1155
		private bool proxyForSameOrgExchangeOAuthCallToLowerVersion;

		// Token: 0x04000484 RID: 1156
		private bool proxyForSameOrgExchangeOAuthCallToLowerVersionWithNoSidUser;
	}
}
