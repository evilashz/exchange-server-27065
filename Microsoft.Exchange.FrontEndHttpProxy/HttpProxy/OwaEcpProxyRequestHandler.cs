using System;
using System.IO;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000AD RID: 173
	internal abstract class OwaEcpProxyRequestHandler<ServiceType> : BEServerCookieProxyRequestHandler<ServiceType> where ServiceType : HttpService
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00026116 File Offset: 0x00024316
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x0002611E File Offset: 0x0002431E
		protected bool IsExplicitSignOn { get; set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x00026127 File Offset: 0x00024327
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x0002612F File Offset: 0x0002432F
		protected string ExplicitSignOnAddress { get; set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x00026138 File Offset: 0x00024338
		// (set) Token: 0x060005FD RID: 1533 RVA: 0x00026140 File Offset: 0x00024340
		protected string ExplicitSignOnDomain { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060005FE RID: 1534
		protected abstract string ProxyLogonUri { get; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x00026149 File Offset: 0x00024349
		protected virtual string ProxyLogonQueryString
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x0002614C File Offset: 0x0002434C
		protected override bool WillAddProtocolSpecificCookiesToServerRequest
		{
			get
			{
				return this.proxyLogonResponseCookies != null;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x0002615A File Offset: 0x0002435A
		protected override bool ImplementsOutOfBandProxyLogon
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0002615D File Offset: 0x0002435D
		protected override HttpStatusCode StatusCodeSignifyingOutOfBandProxyLogonNeeded
		{
			get
			{
				return (HttpStatusCode)441;
			}
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00026164 File Offset: 0x00024364
		protected static bool IsServerPageRequest(string localPath)
		{
			return localPath.EndsWith(".asax", StringComparison.OrdinalIgnoreCase) || localPath.EndsWith(".ascx", StringComparison.OrdinalIgnoreCase) || localPath.EndsWith(".ashx", StringComparison.OrdinalIgnoreCase) || localPath.EndsWith(".asmx", StringComparison.OrdinalIgnoreCase) || localPath.EndsWith(".aspx", StringComparison.OrdinalIgnoreCase) || localPath.EndsWith(".browser", StringComparison.OrdinalIgnoreCase) || localPath.EndsWith(".config", StringComparison.OrdinalIgnoreCase) || localPath.EndsWith(".eas", StringComparison.OrdinalIgnoreCase) || localPath.EndsWith(".ics", StringComparison.OrdinalIgnoreCase) || localPath.EndsWith(".lex", StringComparison.OrdinalIgnoreCase) || localPath.EndsWith(".manifest", StringComparison.OrdinalIgnoreCase) || localPath.EndsWith(".master", StringComparison.OrdinalIgnoreCase) || localPath.EndsWith(".owa", StringComparison.OrdinalIgnoreCase) || localPath.EndsWith(".owa2", StringComparison.OrdinalIgnoreCase) || localPath.EndsWith(".svc", StringComparison.OrdinalIgnoreCase) || localPath.EndsWith(".template", StringComparison.OrdinalIgnoreCase) || localPath.EndsWith(".wsdl", StringComparison.OrdinalIgnoreCase) || localPath.EndsWith(".xaml", StringComparison.OrdinalIgnoreCase) || localPath.EndsWith(".xap", StringComparison.OrdinalIgnoreCase) || localPath.EndsWith(".xml", StringComparison.OrdinalIgnoreCase) || localPath.EndsWith(".xsd", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000262B8 File Offset: 0x000244B8
		protected override DatacenterRedirectStrategy CreateDatacenterRedirectStrategy()
		{
			return new OwaEcpRedirectStrategy(this);
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x000262C0 File Offset: 0x000244C0
		protected virtual void SetProtocolSpecificProxyLogonRequestParameters(HttpWebRequest request)
		{
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x000262C4 File Offset: 0x000244C4
		protected override void StartOutOfBandProxyLogon(object extraData)
		{
			lock (base.LockObject)
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<int>((long)this.GetHashCode(), "[OwaEcpProxyRequestHandler::StartOutOfBandProxyLogon]: Context {0}; Remote server returned 441, this means we need to attempt to do a proxy logon", base.TraceContext);
				UriBuilder uriBuilder = new UriBuilder(this.GetTargetBackEndServerUrl());
				uriBuilder.Scheme = Uri.UriSchemeHttps;
				uriBuilder.Path = base.ClientRequest.ApplicationPath + "/" + this.ProxyLogonUri;
				uriBuilder.Query = this.ProxyLogonQueryString;
				base.Logger.AppendGenericInfo("ProxyLogon", uriBuilder.Uri.ToString());
				this.proxyLogonRequest = (HttpWebRequest)WebRequest.Create(uriBuilder.Uri);
				this.proxyLogonRequest.ServicePoint.ConnectionLimit = HttpProxySettings.ServicePointConnectionLimit.Value;
				this.proxyLogonRequest.Method = "POST";
				base.PrepareServerRequest(this.proxyLogonRequest);
				this.SetProtocolSpecificProxyLogonRequestParameters(this.proxyLogonRequest);
				base.PfdTracer.TraceRequest("ProxyLogonRequest", this.proxyLogonRequest);
				UTF8Encoding utf8Encoding = new UTF8Encoding(true, true);
				this.proxyLogonCSC = utf8Encoding.GetBytes(base.HttpContext.GetSerializedAccessTokenString());
				this.proxyLogonRequest.ContentLength = (long)this.proxyLogonCSC.Length;
				this.proxyLogonRequest.BeginGetRequestStream(new AsyncCallback(OwaEcpProxyRequestHandler<ServiceType>.ProxyLogonRequestStreamReadyCallback), base.ServerAsyncState);
				base.State = ProxyRequestHandler.ProxyState.WaitForProxyLogonRequestStream;
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0002644C File Offset: 0x0002464C
		protected bool IsResourceRequest()
		{
			string localPath = base.ClientRequest.Url.LocalPath;
			return BEResourceRequestHandler.IsResourceRequest(localPath);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00026470 File Offset: 0x00024670
		protected override bool ShouldCopyCookieToClientResponse(Cookie cookie)
		{
			if (FbaModule.IsCadataCookie(cookie.Name))
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<int, string>((long)this.GetHashCode(), "[OwaEcpProxyRequestHandler::ShouldCopyCookieToClientResponse]: Context {0}; Unexpected cadata cookie {1} from BE", base.TraceContext, cookie.Name);
				return false;
			}
			return true;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x000264A4 File Offset: 0x000246A4
		protected override void CopySupplementalCookiesToClientResponse()
		{
			if (this.proxyLogonResponseCookies != null)
			{
				foreach (object obj in this.proxyLogonResponseCookies)
				{
					Cookie cookie = (Cookie)obj;
					if (FbaModule.IsCadataCookie(cookie.Name))
					{
						ExTraceGlobals.VerboseTracer.TraceDebug<int, string>((long)this.GetHashCode(), "[OwaEcpProxyRequestHandler::CopySupplementalCookiesToClientResponse]: Context {0}; Unexpected cadata cookie {1} in proxy logon response from BE", base.TraceContext, cookie.Name);
					}
					else
					{
						base.CopyServerCookieToClientResponse(cookie);
					}
				}
			}
			base.CopySupplementalCookiesToClientResponse();
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0002653C File Offset: 0x0002473C
		protected override bool TryHandleProtocolSpecificResponseErrors(WebException e)
		{
			if (e.Status == WebExceptionStatus.ProtocolError)
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)e.Response;
				int statusCode = (int)httpWebResponse.StatusCode;
				if (statusCode == 442)
				{
					this.RedirectOn442Response();
					return true;
				}
			}
			return base.TryHandleProtocolSpecificResponseErrors(e);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0002657C File Offset: 0x0002477C
		protected override void AddProtocolSpecificCookiesToServerRequest(CookieContainer cookieContainer)
		{
			cookieContainer.Add(this.proxyLogonResponseCookies);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0002658C File Offset: 0x0002478C
		protected override void Cleanup()
		{
			if (this.proxyLogonRequestStream != null)
			{
				this.proxyLogonRequestStream.Flush();
				this.proxyLogonRequestStream.Dispose();
				this.proxyLogonRequestStream = null;
			}
			if (this.proxyLogonResponse != null)
			{
				this.proxyLogonResponse.Close();
				this.proxyLogonResponse = null;
			}
			base.Cleanup();
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x000265E0 File Offset: 0x000247E0
		protected override Uri UpdateExternalRedirectUrl(Uri originalRedirectUrl)
		{
			UriBuilder uriBuilder = new UriBuilder(originalRedirectUrl);
			if (!string.IsNullOrEmpty(this.ExplicitSignOnAddress))
			{
				uriBuilder.Path = UrlUtilities.GetPathWithExplictLogonHint(originalRedirectUrl, this.ExplicitSignOnAddress);
			}
			return uriBuilder.Uri;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0002661C File Offset: 0x0002481C
		protected override bool ShouldExcludeFromExplicitLogonParsing()
		{
			bool flag = this.IsResourceRequest();
			Uri url = base.ClientRequest.Url;
			string text = (url.Segments.Length > 2) ? url.Segments[2] : string.Empty;
			string text2 = (url.Segments.Length > 3) ? url.Segments[3] : string.Empty;
			string text3 = base.ClientRequest.Headers["X-OWA-ExplicitLogonUser"];
			bool flag2 = (!string.IsNullOrEmpty(text3) && SmtpAddress.IsValidSmtpAddress(text3)) || text.Contains("@") || text2.Contains("@");
			ExTraceGlobals.VerboseTracer.TraceDebug((long)this.GetHashCode(), "ShouldExcludeFromExplicitLogonParsing: {0}/{1} resource:{2} explicit:{3}", new object[]
			{
				text,
				text2,
				flag ? "T" : "F",
				flag2 ? "T" : "F"
			});
			return flag && !flag2;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00026714 File Offset: 0x00024914
		protected override bool IsValidExplicitLogonNode(string node, bool nodeIsLast)
		{
			string text = string.Format("IsValidExplicitLogonNode: {0} last:{1} ", node, nodeIsLast ? "y" : "n");
			bool result;
			if (string.IsNullOrEmpty(node))
			{
				text += "1-F";
				result = false;
			}
			else if (!node.Contains("@") && !node.Contains("."))
			{
				text += "2-F";
				result = false;
			}
			else if (nodeIsLast && OwaEcpProxyRequestHandler<HttpService>.IsServerPageRequest(node))
			{
				ExTraceGlobals.VerboseTracer.TraceDebug<int, string>((long)this.GetHashCode(), "[OwaEcpProxyRequestHandler::IsValidExplicitLogonNode]: Context {0}; rejected explicit logon node: {1}", base.TraceContext, node);
				text += "3-F";
				result = false;
			}
			else
			{
				text += "4-T";
				result = true;
			}
			ExTraceGlobals.VerboseTracer.TraceDebug((long)this.GetHashCode(), text);
			return result;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x000267D8 File Offset: 0x000249D8
		protected override UriBuilder GetClientUrlForProxy()
		{
			UriBuilder uriBuilder = new UriBuilder(base.ClientRequest.Url.OriginalString);
			if (this.IsExplicitSignOn)
			{
				string text = HttpUtility.UrlDecode(base.ClientRequest.Url.AbsolutePath);
				string str = HttpUtility.UrlDecode(this.ExplicitSignOnAddress);
				uriBuilder.Path = text.Replace("/" + str, string.Empty);
			}
			return uriBuilder;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x000268C8 File Offset: 0x00024AC8
		protected override void RedirectIfNeeded(BackEndServer mailboxServer)
		{
			if (mailboxServer == null)
			{
				throw new ArgumentNullException("mailboxServer");
			}
			if (!Utilities.IsPartnerHostedOnly && !VariantConfiguration.InvariantNoFlightingSnapshot.Cafe.NoCrossSiteRedirect.Enabled)
			{
				ServiceTopology currentServiceTopology = ServiceTopology.GetCurrentServiceTopology("f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\RequestHandlers\\OwaEcpProxyRequestHandler.cs", "RedirectIfNeeded", 537);
				Site targetSite = currentServiceTopology.GetSite(mailboxServer.Fqdn, "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\RequestHandlers\\OwaEcpProxyRequestHandler.cs", "RedirectIfNeeded", 538);
				ADSite localSite = LocalSiteCache.LocalSite;
				if (!localSite.DistinguishedName.Equals(targetSite.DistinguishedName) && (!this.IsLocalRequest(LocalServerCache.LocalServerFqdn) || !this.IsLAMUserAgent(base.ClientRequest.UserAgent)))
				{
					HttpService targetService = currentServiceTopology.FindAny<ServiceType>(ClientAccessType.Internal, (ServiceType internalService) => internalService != null && internalService.IsFrontEnd && internalService.Site.DistinguishedName.Equals(targetSite.DistinguishedName), "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\RequestHandlers\\OwaEcpProxyRequestHandler.cs", "RedirectIfNeeded", 550);
					if (!this.ShouldExecuteSSORedirect(targetService))
					{
						HttpService httpService = currentServiceTopology.FindAny<ServiceType>(ClientAccessType.External, (ServiceType externalService) => externalService != null && externalService.IsFrontEnd && externalService.Site.DistinguishedName.Equals(targetSite.DistinguishedName), "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\RequestHandlers\\OwaEcpProxyRequestHandler.cs", "RedirectIfNeeded", 561);
						if (httpService != null)
						{
							Uri url = httpService.Url;
							if (Uri.Compare(url, base.ClientRequest.Url, UriComponents.Host, UriFormat.UriEscaped, StringComparison.OrdinalIgnoreCase) != 0)
							{
								UriBuilder uriBuilder = new UriBuilder(base.ClientRequest.Url);
								uriBuilder.Host = url.Host;
								ExTraceGlobals.VerboseTracer.TraceDebug<string>((long)this.GetHashCode(), "[OwaEcpProxyRequestHandler::RedirectIfNeeded]: Stop processing and redirect to {0}.", uriBuilder.Uri.AbsoluteUri);
								throw new ServerSideTransferException(uriBuilder.Uri.AbsoluteUri, LegacyRedirectTypeOptions.Silent);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00026A78 File Offset: 0x00024C78
		private static void ProxyLogonRequestStreamReadyCallback(IAsyncResult result)
		{
			OwaEcpProxyRequestHandler<ServiceType> owaEcpProxyRequestHandler = AsyncStateHolder.Unwrap<OwaEcpProxyRequestHandler<ServiceType>>(result);
			ExTraceGlobals.VerboseTracer.TraceDebug<int>((long)owaEcpProxyRequestHandler.GetHashCode(), "[OwaEcpProxyRequestHandler::ProxyLogonRequestStreamReadyCallback]: Context {0}", owaEcpProxyRequestHandler.TraceContext);
			if (result.CompletedSynchronously)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(owaEcpProxyRequestHandler.OnProxyLogonRequestStreamReady), result);
				return;
			}
			owaEcpProxyRequestHandler.OnProxyLogonRequestStreamReady(result);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00026ACC File Offset: 0x00024CCC
		private static void ProxyLogonResponseReadyCallback(IAsyncResult result)
		{
			OwaEcpProxyRequestHandler<ServiceType> owaEcpProxyRequestHandler = AsyncStateHolder.Unwrap<OwaEcpProxyRequestHandler<ServiceType>>(result);
			ExTraceGlobals.VerboseTracer.TraceDebug<int>((long)owaEcpProxyRequestHandler.GetHashCode(), "[OwaEcpProxyRequestHandler::ProxyLogonResponseReadyCallback]: Context {0}", owaEcpProxyRequestHandler.TraceContext);
			if (result.CompletedSynchronously)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(owaEcpProxyRequestHandler.OnProxyLogonResponseReady), result);
				return;
			}
			owaEcpProxyRequestHandler.OnProxyLogonResponseReady(result);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00026B1F File Offset: 0x00024D1F
		private bool ShouldExecuteSSORedirect(HttpService targetService)
		{
			return !FbaFormPostProxyRequestHandler.DisableSSORedirects && (VdirConfiguration.Instance.InternalAuthenticationMethod & AuthenticationMethod.Fba) == AuthenticationMethod.Fba && (targetService == null || (targetService.AuthenticationMethod & AuthenticationMethod.Fba) == AuthenticationMethod.Fba);
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00026D3C File Offset: 0x00024F3C
		private void OnProxyLogonRequestStreamReady(object extraData)
		{
			base.CallThreadEntranceMethod(delegate
			{
				IAsyncResult asyncResult = extraData as IAsyncResult;
				lock (this.LockObject)
				{
					try
					{
						this.proxyLogonRequestStream = this.proxyLogonRequest.EndGetRequestStream(asyncResult);
						this.proxyLogonRequestStream.Write(this.proxyLogonCSC, 0, this.proxyLogonCSC.Length);
						this.proxyLogonRequestStream.Flush();
						this.proxyLogonRequestStream.Dispose();
						this.proxyLogonRequestStream = null;
						try
						{
							ConcurrencyGuardHelper.IncrementTargetBackendDagAndForest(this);
							this.proxyLogonRequest.BeginGetResponse(new AsyncCallback(OwaEcpProxyRequestHandler<ServiceType>.ProxyLogonResponseReadyCallback), this.ServerAsyncState);
							this.State = ProxyRequestHandler.ProxyState.WaitForProxyLogonResponse;
						}
						catch (Exception)
						{
							ConcurrencyGuardHelper.DecrementTargetBackendDagAndForest(this);
							throw;
						}
					}
					catch (WebException ex)
					{
						this.CompleteWithError(ex, "[OwaEcpProxyRequestHandler::OnProxyLogonRequestStreamReady]");
					}
					catch (HttpException ex2)
					{
						this.CompleteWithError(ex2, "[OwaEcpProxyRequestHandler::OnProxyLogonRequestStreamReady]");
					}
					catch (HttpProxyException ex3)
					{
						this.CompleteWithError(ex3, "[OwaEcpProxyRequestHandler::OnProxyLogonRequestStreamReady]");
					}
					catch (IOException ex4)
					{
						this.CompleteWithError(ex4, "[OwaEcpProxyRequestHandler::OnProxyLogonRequestStreamReady]");
					}
				}
			});
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0002702C File Offset: 0x0002522C
		private void OnProxyLogonResponseReady(object extraData)
		{
			base.CallThreadEntranceMethod(delegate
			{
				IAsyncResult asyncResult = extraData as IAsyncResult;
				lock (this.LockObject)
				{
					try
					{
						ConcurrencyGuardHelper.DecrementTargetBackendDagAndForest(this);
						this.proxyLogonResponse = (HttpWebResponse)this.proxyLogonRequest.EndGetResponse(asyncResult);
						this.PfdTracer.TraceResponse("ProxyLogonResponse", this.proxyLogonResponse);
						this.proxyLogonResponseCookies = this.proxyLogonResponse.Cookies;
						this.proxyLogonResponse.Close();
						this.proxyLogonResponse = null;
						UserContextCookie userContextCookie = this.TryGetUserContextFromProxyLogonResponse();
						if (userContextCookie != null && userContextCookie.MailboxUniqueKey != null)
						{
							string mailboxUniqueKey = userContextCookie.MailboxUniqueKey;
							if (SmtpAddress.IsValidSmtpAddress(mailboxUniqueKey))
							{
								ProxyAddress proxyAddress = ProxyAddress.Parse("SMTP", mailboxUniqueKey);
								AnchorMailbox anchorMailbox = new SmtpAnchorMailbox(proxyAddress.AddressString, this);
								this.AnchoredRoutingTarget = new AnchoredRoutingTarget(anchorMailbox, this.AnchoredRoutingTarget.BackEndServer);
							}
							else
							{
								try
								{
									SecurityIdentifier sid = new SecurityIdentifier(mailboxUniqueKey);
									AnchorMailbox anchorMailbox2 = new SidAnchorMailbox(sid, this);
									this.AnchoredRoutingTarget = new AnchoredRoutingTarget(anchorMailbox2, this.AnchoredRoutingTarget.BackEndServer);
								}
								catch (ArgumentException)
								{
								}
							}
						}
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.BeginProxyRequest));
						this.State = ProxyRequestHandler.ProxyState.PrepareServerRequest;
					}
					catch (WebException ex)
					{
						if (ex.Status == WebExceptionStatus.ProtocolError && ((HttpWebResponse)ex.Response).StatusCode == (HttpStatusCode)442)
						{
							this.RedirectOn442Response();
						}
						else
						{
							this.CompleteWithError(ex, "[OwaEcpProxyRequestHandler::OnProxyLogonResponseReady]");
						}
					}
					catch (HttpException ex2)
					{
						this.CompleteWithError(ex2, "[OwaEcpProxyRequestHandler::OnProxyLogonResponseReady]");
					}
					catch (IOException ex3)
					{
						this.CompleteWithError(ex3, "[OwaEcpProxyRequestHandler::OnProxyLogonResponseReady]");
					}
					catch (HttpProxyException ex4)
					{
						this.CompleteWithError(ex4, "[OwaEcpProxyRequestHandler::OnProxyLogonResponseReady]");
					}
				}
			});
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00027060 File Offset: 0x00025260
		private UserContextCookie TryGetUserContextFromProxyLogonResponse()
		{
			foreach (object obj in this.proxyLogonResponseCookies)
			{
				Cookie cookie = (Cookie)obj;
				if (cookie.Name.StartsWith("UserContext"))
				{
					return UserContextCookie.TryCreateFromNetCookie(cookie);
				}
			}
			return null;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x000270D0 File Offset: 0x000252D0
		private void RedirectOn442Response()
		{
			ExTraceGlobals.VerboseTracer.TraceDebug<int>((long)this.GetHashCode(), "[OwaEcpProxyRequestHandler::RedirectOn442Response]: Context {0}; The proxy returned 442, this means the user's language or timezone are invalid", base.TraceContext);
			string text = OwaUrl.LanguagePage.GetExplicitUrl(base.ClientRequest).ToString();
			base.PfdTracer.TraceRedirect("EcpOwa442NeedLanguage", text);
			base.ClientResponse.Redirect(text, false);
			base.CompleteForRedirect(text);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00027134 File Offset: 0x00025334
		private bool IsLocalRequest(string machineFqdn)
		{
			string host = base.ClientRequest.Url.Host;
			return host.Equals("localhost", StringComparison.OrdinalIgnoreCase) || host.Equals("127.0.0.1", StringComparison.OrdinalIgnoreCase) || host.Equals(Environment.MachineName, StringComparison.OrdinalIgnoreCase) || host.Equals(machineFqdn, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00027189 File Offset: 0x00025389
		private bool IsLAMUserAgent(string requestUserAgent)
		{
			return requestUserAgent.Equals("AMPROBE/LOCAL/CLIENTACCESS", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0400044B RID: 1099
		private const int HttpStatusNeedIdentity = 441;

		// Token: 0x0400044C RID: 1100
		private const int HttpStatusNeedLanguage = 442;

		// Token: 0x0400044D RID: 1101
		private HttpWebRequest proxyLogonRequest;

		// Token: 0x0400044E RID: 1102
		private Stream proxyLogonRequestStream;

		// Token: 0x0400044F RID: 1103
		private HttpWebResponse proxyLogonResponse;

		// Token: 0x04000450 RID: 1104
		private CookieCollection proxyLogonResponseCookies;

		// Token: 0x04000451 RID: 1105
		private byte[] proxyLogonCSC;
	}
}
