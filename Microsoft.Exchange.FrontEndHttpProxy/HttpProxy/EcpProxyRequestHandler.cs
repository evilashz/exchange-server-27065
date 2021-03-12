using System;
using System.Net;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.Security.Authentication;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000AF RID: 175
	internal class EcpProxyRequestHandler : OwaEcpProxyRequestHandler<EcpService>
	{
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x000273B8 File Offset: 0x000255B8
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x000273C0 File Offset: 0x000255C0
		internal bool IsCrossForestDelegated { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x000273CC File Offset: 0x000255CC
		protected override string ProxyLogonUri
		{
			get
			{
				string explicitPath = this.GetExplicitPath(base.ClientRequest.Path);
				if (explicitPath != null)
				{
					return explicitPath + "proxyLogon.ecp";
				}
				return "proxyLogon.ecp";
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x000273FF File Offset: 0x000255FF
		protected override ClientAccessType ClientAccessType
		{
			get
			{
				return ClientAccessType.Internal;
			}
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00027404 File Offset: 0x00025604
		internal static void AddDownLevelProxyHeaders(WebHeaderCollection headers, HttpContext context)
		{
			if (!context.Request.IsAuthenticated)
			{
				throw new HttpException(401, "Unable to proxy unauthenticated down-level requests.");
			}
			if (context.User != null)
			{
				IIdentity identity = context.User.Identity;
				if ((identity is WindowsIdentity || identity is ClientSecurityContextIdentity) && null != identity.GetSecurityIdentifier())
				{
					string value = identity.GetSecurityIdentifier().ToString();
					headers["msExchLogonAccount"] = value;
					headers["msExchLogonMailbox"] = value;
					headers["msExchTargetMailbox"] = value;
				}
			}
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00027494 File Offset: 0x00025694
		internal static bool IsCrossForestDelegatedRequest(HttpRequest request)
		{
			if (!string.IsNullOrEmpty(request.QueryString["SecurityToken"]))
			{
				return true;
			}
			HttpCookie httpCookie = request.Cookies["SecurityToken"];
			return httpCookie != null && !string.IsNullOrEmpty(httpCookie.Value);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x000274E0 File Offset: 0x000256E0
		protected override bool ShouldExcludeFromExplicitLogonParsing()
		{
			bool result = base.IsResourceRequest();
			ExTraceGlobals.VerboseTracer.TraceDebug<string>((long)this.GetHashCode(), "[EcpProxyRequestHandler::ShouldExcludeFromExplicitLogonParsing]: request is resource:{0}.", result.ToString());
			return result;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00027512 File Offset: 0x00025712
		protected override UriBuilder GetClientUrlForProxy()
		{
			return new UriBuilder(base.ClientRequest.Url);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00027524 File Offset: 0x00025724
		protected override void AddProtocolSpecificHeadersToServerRequest(WebHeaderCollection headers)
		{
			headers[Constants.LiveIdEnvironment] = (string)base.HttpContext.Items[Constants.LiveIdEnvironment];
			headers[Constants.LiveIdPuid] = (string)base.HttpContext.Items[Constants.LiveIdPuid];
			headers[Constants.OrgIdPuid] = (string)base.HttpContext.Items[Constants.OrgIdPuid];
			headers[Constants.LiveIdMemberName] = (string)base.HttpContext.Items[Constants.LiveIdMemberName];
			headers["msExchClientPath"] = base.ClientRequest.Path;
			if (this.isSyndicatedAdminManageDownLevelTarget)
			{
				headers["msExchCafeForceRouteToLogonAccount"] = "1";
			}
			if (!this.IsCrossForestDelegated && base.ProxyToDownLevel)
			{
				EcpProxyRequestHandler.AddDownLevelProxyHeaders(headers, base.HttpContext);
				if (base.IsExplicitSignOn)
				{
					string value = null;
					AnchoredRoutingTarget anchoredRoutingTarget = this.isSyndicatedAdminManageDownLevelTarget ? this.originalAnchoredRoutingTarget : base.AnchoredRoutingTarget;
					if (anchoredRoutingTarget != null)
					{
						UserBasedAnchorMailbox userBasedAnchorMailbox = anchoredRoutingTarget.AnchorMailbox as UserBasedAnchorMailbox;
						if (userBasedAnchorMailbox != null)
						{
							ADRawEntry adrawEntry = userBasedAnchorMailbox.GetADRawEntry();
							if (adrawEntry != null)
							{
								SecurityIdentifier securityIdentifier = adrawEntry[ADMailboxRecipientSchema.Sid] as SecurityIdentifier;
								if (securityIdentifier != null)
								{
									value = securityIdentifier.ToString();
								}
							}
						}
					}
					headers["msExchTargetMailbox"] = value;
				}
			}
			base.AddProtocolSpecificHeadersToServerRequest(headers);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00027688 File Offset: 0x00025888
		protected override bool ShouldCopyHeaderToServerRequest(string headerName)
		{
			bool flag = !string.Equals(headerName, Constants.MsExchProxyUri, StringComparison.OrdinalIgnoreCase) && !string.Equals(headerName, "msExchLogonAccount", StringComparison.OrdinalIgnoreCase) && !string.Equals(headerName, "msExchLogonMailbox", StringComparison.OrdinalIgnoreCase) && !string.Equals(headerName, "msExchTargetMailbox", StringComparison.OrdinalIgnoreCase) && !string.Equals(headerName, Constants.LiveIdPuid, StringComparison.OrdinalIgnoreCase) && !string.Equals(headerName, Constants.LiveIdMemberName, StringComparison.OrdinalIgnoreCase) && !string.Equals(headerName, "msExchCafeForceRouteToLogonAccount", StringComparison.OrdinalIgnoreCase) && !string.Equals(headerName, Constants.LiveIdEnvironment, StringComparison.OrdinalIgnoreCase) && base.ShouldCopyHeaderToServerRequest(headerName);
			ExTraceGlobals.VerboseTracer.TraceDebug<string, string>((long)this.GetHashCode(), "[EcpProxyRequestHandler::ShouldCopyHeaderToServerRequest]: {0} header '{1}'.", flag ? "copy" : "skip", headerName);
			return flag;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0002773C File Offset: 0x0002593C
		protected override void HandleLogoffRequest()
		{
			if (base.ClientRequest != null && base.ClientResponse != null && base.ClientRequest.Url.AbsolutePath.EndsWith("logoff.aspx", StringComparison.OrdinalIgnoreCase))
			{
				if (!Utilities.IsPartnerHostedOnly && !VariantConfiguration.InvariantNoFlightingSnapshot.Cafe.NoFormBasedAuthentication.Enabled)
				{
					FbaModule.InvalidateKeyCache(base.ClientRequest);
				}
				Utility.DeleteFbaAuthCookies(base.ClientRequest, base.ClientResponse);
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x000277B4 File Offset: 0x000259B4
		protected override AnchorMailbox ResolveAnchorMailbox()
		{
			if (base.State != ProxyRequestHandler.ProxyState.CalculateBackEndSecondRound)
			{
				if (!base.AuthBehavior.IsFullyAuthenticated())
				{
					base.HasPreemptivelyCheckedForRoutingHint = true;
					string text = base.HttpContext.Request.Headers["X-UpnAnchorMailbox"];
					if (!string.IsNullOrWhiteSpace(text))
					{
						ExTraceGlobals.VerboseTracer.TraceDebug<int>((long)this.GetHashCode(), "[EcpProxyRequestHandler::ResolveAnchorMailbox]: From Header Routing UPN Hint, context {1}.", base.TraceContext);
						base.Logger.SafeSet(HttpProxyMetadata.RoutingHint, "OwaEcpUpn");
						return new LiveIdMemberNameAnchorMailbox(text, null, this);
					}
					AnchorMailbox anchorMailbox = base.CreateAnchorMailboxFromRoutingHint();
					if (anchorMailbox != null)
					{
						ExTraceGlobals.VerboseTracer.TraceDebug<int>((long)this.GetHashCode(), "[EcpProxyRequestHandler::ResolveAnchorMailbox]: From Header Routing Hint, context {1}.", base.TraceContext);
						return anchorMailbox;
					}
				}
				string text2 = this.TryGetExplicitLogonNode(ExplicitLogonNode.Second);
				bool flag;
				if (!string.IsNullOrEmpty(text2))
				{
					if (SmtpAddress.IsValidSmtpAddress(text2))
					{
						base.IsExplicitSignOn = true;
						base.ExplicitSignOnAddress = text2;
						base.Logger.Set(HttpProxyMetadata.RoutingHint, "ExplicitSignOn-SMTP");
						ExTraceGlobals.VerboseTracer.TraceDebug<string, int>((long)this.GetHashCode(), "[EcpProxyRequestHandler::ResolveAnchorMailbox]: ExplicitSignOn-SMTP. Address {0}, context {1}.", text2, base.TraceContext);
						return new SmtpAnchorMailbox(text2, this);
					}
					if ((Utilities.IsPartnerHostedOnly || VariantConfiguration.InvariantNoFlightingSnapshot.Cafe.SyndicatedAdmin.Enabled) && text2.StartsWith("@"))
					{
						this.isSyndicatedAdmin = true;
						text2 = text2.Substring(1);
						if (SmtpAddress.IsValidDomain(text2))
						{
							string text3 = this.TryGetExplicitLogonNode(ExplicitLogonNode.Third);
							ExTraceGlobals.VerboseTracer.TraceDebug<string, string, int>((long)this.GetHashCode(), "[EcpProxyRequestHandler::ResolveAnchorMailbox]: SyndAdmin, domain {0}, SMTP {1}, context {2}.", text2, text3, base.TraceContext);
							if (!string.IsNullOrEmpty(text3))
							{
								base.IsExplicitSignOn = true;
								base.ExplicitSignOnAddress = text3;
								base.Logger.Set(HttpProxyMetadata.RoutingHint, "SyndAdmin-SMTP");
								return new SmtpAnchorMailbox(text3, this);
							}
							base.Logger.Set(HttpProxyMetadata.RoutingHint, "SyndAdmin-Domain");
							return new DomainAnchorMailbox(text2, this);
						}
					}
				}
				else if (!Utilities.IsPartnerHostedOnly && !VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
				{
					string text4 = this.TryGetBackendParameter("TargetServer", out flag);
					if (!string.IsNullOrEmpty(text4))
					{
						base.Logger.Set(HttpProxyMetadata.RoutingHint, "TargetServer" + (flag ? "-UrlQuery" : "-Cookie"));
						ExTraceGlobals.VerboseTracer.TraceDebug<string, string, int>((long)this.GetHashCode(), "[EcpProxyRequestHandler::ResolveAnchorMailbox]: On-Premise, TargetServer parameter {0}, from {1}, context {2}.", text4, flag ? "url query" : "cookie", base.TraceContext);
						return new ServerInfoAnchorMailbox(text4, this);
					}
				}
				string text5 = this.TryGetBackendParameter("ExchClientVer", out flag);
				if (!string.IsNullOrEmpty(text5))
				{
					string text6 = Utilities.NormalizeExchClientVer(text5);
					base.Logger.Set(HttpProxyMetadata.RoutingHint, "ExchClientVer" + (flag ? "-UrlQuery" : "-Cookie"));
					if (!Utilities.IsPartnerHostedOnly && !VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
					{
						ExTraceGlobals.VerboseTracer.TraceDebug<string, string, int>((long)this.GetHashCode(), "[EcpProxyRequestHandler::ResolveAnchorMailbox]: On-Premise, Version parameter {0}, from {1}, context {2}.", text5, flag ? "url query" : "cookie", base.TraceContext);
						return base.GetServerVersionAnchorMailbox(text6);
					}
					string text7 = (string)base.HttpContext.Items["AuthenticatedUserOrganization"];
					if (!string.IsNullOrEmpty(text7))
					{
						ExTraceGlobals.VerboseTracer.TraceDebug((long)this.GetHashCode(), "[EcpProxyRequestHandler::ResolveAnchorMailbox]: On-Cloud, Version parameter {0}, from {1}, domain {2}, context {3}.", new object[]
						{
							text6,
							flag ? "url query" : "cookie",
							text7,
							base.TraceContext
						});
						return VersionedDomainAnchorMailbox.GetAnchorMailbox(text7, text6, this);
					}
					ExTraceGlobals.VerboseTracer.TraceDebug<int>((long)this.GetHashCode(), "[EcpProxyRequestHandler::ResolveAnchorMailbox]: AuthenticatedUserOrganization is null. Context {0}.", base.TraceContext);
				}
			}
			ExTraceGlobals.VerboseTracer.TraceDebug<string, int>((long)this.GetHashCode(), "[EcpProxyRequestHandler::ResolveAnchorMailbox]: {0}, context {1}, call base method to do regular anchor mailbox calculation.", (base.State == ProxyRequestHandler.ProxyState.CalculateBackEndSecondRound) ? "Second round" : "Nothing special", base.TraceContext);
			return base.ResolveAnchorMailbox();
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00027BB0 File Offset: 0x00025DB0
		protected override void CopySupplementalCookiesToClientResponse()
		{
			if (this.backendServerFromUrlCookie != null)
			{
				base.CopyServerCookieToClientResponse(this.backendServerFromUrlCookie);
			}
			this.CopyBEResourcePathCookie();
			base.CopySupplementalCookiesToClientResponse();
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00027BD4 File Offset: 0x00025DD4
		protected override bool ShouldRecalculateProxyTarget()
		{
			bool result = false;
			if (this.isSyndicatedAdmin && !this.IsCrossForestDelegated && base.State == ProxyRequestHandler.ProxyState.CalculateBackEnd && base.AnchoredRoutingTarget.BackEndServer != null && base.AnchoredRoutingTarget.BackEndServer.Version < Server.E15MinVersion)
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<int>((long)this.GetHashCode(), "[EcpProxyRequestHandler::ShouldRecalculateProxyTarget]: context {0}, Syndicated admin request. Target tenant is down level, start 2nd round calculation.", base.TraceContext);
				this.isSyndicatedAdminManageDownLevelTarget = true;
				this.originalAnchoredRoutingTarget = base.AnchoredRoutingTarget;
				result = true;
			}
			else
			{
				ExTraceGlobals.VerboseTracer.TraceDebug((long)this.GetHashCode(), "[EcpProxyRequestHandler::ShouldRecalculateProxyTarget]: context {0}, no need to do 2nd round calculation: isSyndicatedAdmin {1}, cross forest {2}, state {3}, BEServer Version {4}, lower than E15MinVer {5}", new object[]
				{
					base.TraceContext,
					this.isSyndicatedAdmin,
					this.IsCrossForestDelegated,
					base.State,
					(base.AnchoredRoutingTarget.BackEndServer != null) ? base.AnchoredRoutingTarget.BackEndServer.Version : -1,
					Server.E15MinVersion
				});
			}
			return result;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00027CE4 File Offset: 0x00025EE4
		protected override void LogWebException(WebException exception)
		{
			base.LogWebException(exception);
			HttpWebResponse httpWebResponse = (HttpWebResponse)exception.Response;
			if (httpWebResponse != null && !string.IsNullOrEmpty(httpWebResponse.Headers["X-ECP-ERROR"]))
			{
				base.Logger.AppendGenericError("X-ECP-ERROR", httpWebResponse.Headers["X-ECP-ERROR"]);
			}
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00027D40 File Offset: 0x00025F40
		private string GetExplicitPath(string requestPath)
		{
			string result = null;
			int num = requestPath.IndexOf('@');
			if (num > 0)
			{
				int num2 = requestPath.IndexOf('@', num + 1);
				if (num2 < 0)
				{
					num2 = num;
				}
				int num3 = num;
				while (num3 > 0 && requestPath[num3] != '/')
				{
					num3--;
				}
				if (num3 > 0)
				{
					int num4 = requestPath.IndexOf('/', num2);
					if (num4 > num3)
					{
						result = requestPath.Substring(num3 + 1, num4 - num3);
					}
				}
			}
			return result;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00027DA8 File Offset: 0x00025FA8
		private string TryGetBackendParameter(string key, out bool isFromUrl)
		{
			string text = base.ClientRequest.QueryString[key];
			isFromUrl = false;
			if (string.IsNullOrEmpty(text))
			{
				HttpCookie httpCookie = base.ClientRequest.Cookies[key];
				text = ((httpCookie == null) ? null : httpCookie.Value);
			}
			else
			{
				isFromUrl = true;
				this.backendServerFromUrlCookie = new Cookie(key, text)
				{
					HttpOnly = false,
					Secure = true,
					Path = "/"
				};
			}
			return text;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00027E20 File Offset: 0x00026020
		private void CopyBEResourcePathCookie()
		{
			string text = base.ServerResponse.Headers[Constants.BEResourcePath];
			if (!string.IsNullOrEmpty(text) && base.AnchoredRoutingTarget != null)
			{
				HttpCookie httpCookie = new HttpCookie(Constants.BEResource, base.AnchoredRoutingTarget.BackEndServer.ToString());
				httpCookie.Path = text;
				httpCookie.HttpOnly = true;
				httpCookie.Secure = base.ClientRequest.IsSecureConnection;
				base.ClientResponse.Cookies.Add(httpCookie);
				return;
			}
			if (base.ClientRequest.Url.AbsolutePath.EndsWith(".aspx", StringComparison.OrdinalIgnoreCase) || base.ClientRequest.Url.AbsolutePath.EndsWith(".slab", StringComparison.OrdinalIgnoreCase))
			{
				ExTraceGlobals.VerboseTracer.TraceError<string, string, string>(0L, "[EcpProxyRequestHandler::CopyBEResourcePathCookie] Cannot add X-BEResource cookie to the response of {0}! Header from backend: {1}, backend server: {2}", base.ClientRequest.Url.ToString(), text, (base.AnchoredRoutingTarget == null) ? "null" : base.AnchoredRoutingTarget.BackEndServer.ToString());
			}
		}

		// Token: 0x0400045B RID: 1115
		public const string ClientPathHeaderKey = "msExchClientPath";

		// Token: 0x0400045C RID: 1116
		private const string LogonAccount = "msExchLogonAccount";

		// Token: 0x0400045D RID: 1117
		private const string LogonMailbox = "msExchLogonMailbox";

		// Token: 0x0400045E RID: 1118
		private const string TargetMailbox = "msExchTargetMailbox";

		// Token: 0x0400045F RID: 1119
		private const string CafeForceRouteToLogonAccountHeaderKey = "msExchCafeForceRouteToLogonAccount";

		// Token: 0x04000460 RID: 1120
		private const string EcpErrorHeaderName = "X-ECP-ERROR";

		// Token: 0x04000461 RID: 1121
		private const string EcpProxyLogonUri = "proxyLogon.ecp";

		// Token: 0x04000462 RID: 1122
		private const string LogoffPage = "logoff.aspx";

		// Token: 0x04000463 RID: 1123
		private const string SecurityTokenParamName = "SecurityToken";

		// Token: 0x04000464 RID: 1124
		private Cookie backendServerFromUrlCookie;

		// Token: 0x04000465 RID: 1125
		private bool isSyndicatedAdmin;

		// Token: 0x04000466 RID: 1126
		private bool isSyndicatedAdminManageDownLevelTarget;

		// Token: 0x04000467 RID: 1127
		private AnchoredRoutingTarget originalAnchoredRoutingTarget;
	}
}
