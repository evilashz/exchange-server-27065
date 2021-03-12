using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000CC RID: 204
	internal class RpcHttpProxyRequestHandler : ProxyRequestHandler
	{
		// Token: 0x060006FF RID: 1791 RVA: 0x0002C959 File Offset: 0x0002AB59
		internal RpcHttpProxyRequestHandler() : this(RpcHttpProxyRules.DefaultRpcHttpProxyRules)
		{
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0002C966 File Offset: 0x0002AB66
		internal RpcHttpProxyRequestHandler(RpcHttpProxyRules rule)
		{
			this.proxyRules = rule;
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x0002C975 File Offset: 0x0002AB75
		protected override bool ShouldForceUnbufferedClientResponseOutput
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0002C978 File Offset: 0x0002AB78
		protected override bool IsBackendServerCacheValidationEnabled
		{
			get
			{
				return RpcHttpProxyRequestHandler.RpcHttpHeadRequestEnabled.Value && base.ClientRequest != null && base.ClientRequest.HttpMethod == "RPC_IN_DATA";
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x0002C9A5 File Offset: 0x0002ABA5
		protected override bool UseSmartBufferSizing
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0002C9A8 File Offset: 0x0002ABA8
		protected override void BeginValidateBackendServerCache()
		{
			Exception ex = null;
			try
			{
				Uri targetBackEndServerUrl = this.GetTargetBackEndServerUrl();
				this.headRequest = base.CreateServerRequest(targetBackEndServerUrl);
				this.headRequest.Method = "HEAD";
				this.headRequest.Timeout = RpcHttpProxyRequestHandler.RpcHttpHeadRequestTimeout.Value;
				this.headRequest.KeepAlive = false;
				this.headRequest.ContentLength = 0L;
				this.headRequest.SendChunked = false;
				this.headRequest.BeginGetResponse(new AsyncCallback(RpcHttpProxyRequestHandler.ValidateBackendServerCacheCallback), base.ServerAsyncState);
				base.Logger.LogCurrentTime("H-BeginGetResponse");
			}
			catch (WebException ex2)
			{
				ex = ex2;
			}
			catch (HttpException ex3)
			{
				ex = ex3;
			}
			catch (IOException ex4)
			{
				ex = ex4;
			}
			catch (SocketException ex5)
			{
				ex = ex5;
			}
			if (ex != null)
			{
				ExTraceGlobals.VerboseTracer.TraceError<Exception, int, ProxyRequestHandler.ProxyState>((long)this.GetHashCode(), "[ProxyRequestHandler::BeginValidateBackendServerCache]: An error occurred while trying to send head request: {0}; Context {1}; State {2}", ex, base.TraceContext, base.State);
				this.headRequest = null;
				base.BeginProxyRequestOrRecalculate();
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0002CB18 File Offset: 0x0002AD18
		protected override StreamProxy BuildResponseStreamProxy(StreamProxy.StreamProxyType streamProxyType, Stream source, Stream target, byte[] buffer)
		{
			return this.BuildResponseStream(() => new RpcHttpOutDataResponseStreamProxy(streamProxyType, source, target, buffer, this), () => this.<>n__FabricatedMethod4(streamProxyType, source, target, buffer));
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0002CBC8 File Offset: 0x0002ADC8
		protected override StreamProxy BuildResponseStreamProxySmartSizing(StreamProxy.StreamProxyType streamProxyType, Stream source, Stream target)
		{
			return this.BuildResponseStream(() => new RpcHttpOutDataResponseStreamProxy(streamProxyType, source, target, HttpProxySettings.ResponseBufferSize.Value, HttpProxySettings.MinimumResponseBufferSize.Value, this), () => this.<>n__FabricatedMethod9(streamProxyType, source, target));
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0002CC15 File Offset: 0x0002AE15
		protected override void DoProtocolSpecificBeginProcess()
		{
			if (base.ClientRequest.HttpMethod.Equals("RPC_IN_DATA"))
			{
				base.ParseClientRequest<bool>(new Func<Stream, bool>(this.ParseOutAssociationGuid), 104);
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0002CC44 File Offset: 0x0002AE44
		protected override AnchorMailbox ResolveAnchorMailbox()
		{
			UriBuilder uriBuilder = new UriBuilder(base.ClientRequest.Url);
			if (string.IsNullOrEmpty(base.ClientRequest.Url.Query))
			{
				throw new HttpProxyException(HttpStatusCode.NotFound, HttpProxySubErrorCode.EndpointNotFound, "No proxy destination is specified!");
			}
			RpcHttpQueryString rpcHttpQueryString = new RpcHttpQueryString(uriBuilder.Query);
			this.rpcServerTarget = rpcHttpQueryString.RcaServer;
			if (SmtpAddress.IsValidSmtpAddress(this.rpcServerTarget))
			{
				Guid mailboxGuid = Guid.Empty;
				string text = string.Empty;
				try
				{
					SmtpAddress smtpAddress = new SmtpAddress(this.rpcServerTarget);
					mailboxGuid = new Guid(smtpAddress.Local);
					text = smtpAddress.Domain;
				}
				catch (FormatException)
				{
					return this.ResolveToDefaultAnchorMailbox(this.rpcServerTarget, "InvalidFormat");
				}
				base.Logger.Set(HttpProxyMetadata.RoutingHint, "MailboxGuidWithDomain");
				if (!text.Contains("."))
				{
					string text2 = base.HttpContext.Items[Constants.WLIDMemberName] as string;
					if (!string.IsNullOrEmpty(text2))
					{
						SmtpAddress smtpAddress2 = new SmtpAddress(text2);
						string domain = smtpAddress2.Domain;
						if (domain != null && !string.Equals(domain, text, StringComparison.OrdinalIgnoreCase))
						{
							ExTraceGlobals.BriefTracer.TraceError((long)this.GetHashCode(), "[RpcHttpProxyRequestHandler::ResolveAnchorMailbox]: Fixing up invalid domain name from client: {0} to {1}; Context {2}; State {3}", new object[]
							{
								text,
								domain,
								base.TraceContext,
								base.State
							});
							text = domain;
							this.rpcServerTarget = ExchangeRpcClientAccess.CreatePersonalizedServer(mailboxGuid, text);
							base.Logger.AppendString(HttpProxyMetadata.RoutingHint, "-ChangedToUserDomain");
						}
					}
				}
				this.updateRpcServer = true;
				return new MailboxGuidAnchorMailbox(mailboxGuid, text, this);
			}
			ProxyDestination proxyDestination;
			if (this.proxyRules.TryGetProxyDestination(this.rpcServerTarget, out proxyDestination))
			{
				string text3 = proxyDestination.GetHostName(this.GetKeyForCasAffinity());
				if (proxyDestination.IsDynamicTarget)
				{
					try
					{
						text3 = DownLevelServerManager.Instance.GetDownLevelClientAccessServerWithPreferredServer<RpcHttpService>(new ServerInfoAnchorMailbox(text3, this), text3, ClientAccessType.External, base.Logger, proxyDestination.Version).Fqdn;
					}
					catch (NoAvailableDownLevelBackEndException)
					{
						throw new HttpProxyException(HttpStatusCode.NotFound, HttpProxySubErrorCode.EndpointNotFound, string.Format("Cannot find a healthy E12 or E14 CAS to proxy to: {0}", this.rpcServerTarget));
					}
				}
				uriBuilder.Host = text3;
				uriBuilder.Port = proxyDestination.Port;
				uriBuilder.Scheme = Uri.UriSchemeHttps;
				base.Logger.Set(HttpProxyMetadata.RoutingHint, "RpcHttpProxyRules");
				this.updateRpcServer = false;
				return new UrlAnchorMailbox(uriBuilder.Uri, this);
			}
			return this.ResolveToDefaultAnchorMailbox(this.rpcServerTarget, "UnknownServerName");
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0002CEE8 File Offset: 0x0002B0E8
		protected override Uri GetTargetBackEndServerUrl()
		{
			UriBuilder uriBuilder = new UriBuilder(base.GetTargetBackEndServerUrl());
			if (this.updateRpcServer)
			{
				RpcHttpQueryString rpcHttpQueryString = new RpcHttpQueryString(uriBuilder.Query);
				if (string.IsNullOrEmpty(rpcHttpQueryString.RcaServerPort))
				{
					uriBuilder.Query = uriBuilder.Host + rpcHttpQueryString.AdditionalParameters;
				}
				else
				{
					uriBuilder.Query = uriBuilder.Host + ":" + rpcHttpQueryString.RcaServerPort + rpcHttpQueryString.AdditionalParameters;
				}
			}
			return uriBuilder.Uri;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0002CF63 File Offset: 0x0002B163
		protected override void SetProtocolSpecificServerRequestParameters(HttpWebRequest serverRequest)
		{
			serverRequest.AllowWriteStreamBuffering = false;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0002CF6C File Offset: 0x0002B16C
		protected override bool ShouldCopyHeaderToServerRequest(string headerName)
		{
			return !RpcHttpProxyRequestHandler.ProtectedHeaderNames.Contains(headerName, StringComparer.OrdinalIgnoreCase) && base.ShouldCopyHeaderToServerRequest(headerName);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0002CF89 File Offset: 0x0002B189
		protected override bool ShouldLogClientDisconnectError(Exception ex)
		{
			return false;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0002CF8C File Offset: 0x0002B18C
		protected override void DoProtocolSpecificBeginRequestLogging()
		{
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0002CF90 File Offset: 0x0002B190
		protected override void AddProtocolSpecificHeadersToServerRequest(WebHeaderCollection headers)
		{
			headers[WellKnownHeader.RpcHttpProxyLogonUserName] = EncodingUtilities.EncodeToBase64(base.HttpContext.User.Identity.GetSafeName(true));
			headers[WellKnownHeader.RpcHttpProxyServerTarget] = this.rpcServerTarget;
			if (this.associationGuid != Guid.Empty)
			{
				headers[WellKnownHeader.RpcHttpProxyAssociationGuid] = this.associationGuid.ToString();
			}
			DatabaseBasedAnchorMailbox databaseBasedAnchorMailbox = base.AnchoredRoutingTarget.AnchorMailbox as DatabaseBasedAnchorMailbox;
			if (databaseBasedAnchorMailbox != null)
			{
				ADObjectId database = databaseBasedAnchorMailbox.GetDatabase();
				if (database != null)
				{
					headers[WellKnownHeader.MailboxDatabaseGuid] = database.ObjectGuid.ToString();
				}
			}
			base.AddProtocolSpecificHeadersToServerRequest(headers);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0002D048 File Offset: 0x0002B248
		private static void ValidateBackendServerCacheCallback(IAsyncResult result)
		{
			RpcHttpProxyRequestHandler rpcHttpProxyRequestHandler = AsyncStateHolder.Unwrap<RpcHttpProxyRequestHandler>(result);
			if (result.CompletedSynchronously)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(rpcHttpProxyRequestHandler.OnValidateBackendServerCacheCompleted), result);
				return;
			}
			rpcHttpProxyRequestHandler.OnValidateBackendServerCacheCompleted(result);
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0002D210 File Offset: 0x0002B410
		private void OnValidateBackendServerCacheCompleted(object extraData)
		{
			base.CallThreadEntranceMethod(delegate
			{
				IAsyncResult asyncResult = extraData as IAsyncResult;
				HttpWebResponse httpWebResponse = null;
				Exception ex = null;
				try
				{
					this.Logger.LogCurrentTime("H-OnResponseReady");
					httpWebResponse = (HttpWebResponse)this.headRequest.EndGetResponse(asyncResult);
					this.ThrowWebExceptionForRetryOnErrorTest(httpWebResponse, new int[]
					{
						0,
						1,
						2
					});
				}
				catch (WebException ex2)
				{
					ex = ex2;
				}
				catch (HttpException ex3)
				{
					ex = ex3;
				}
				catch (IOException ex4)
				{
					ex = ex4;
				}
				catch (SocketException ex5)
				{
					ex = ex5;
				}
				finally
				{
					this.Logger.LogCurrentTime("H-EndGetResponse");
					if (httpWebResponse != null)
					{
						httpWebResponse.Close();
					}
					this.headRequest = null;
				}
				if (ex != null)
				{
					ExTraceGlobals.VerboseTracer.TraceError<Exception, int, ProxyRequestHandler.ProxyState>((long)this.GetHashCode(), "[ProxyRequestHandler::OnValidateBackendServerCacheCompleted]: Head response error: {0}; Context {1}; State {2}", ex, this.TraceContext, this.State);
				}
				WebException ex6 = ex as WebException;
				bool flag = true;
				if (ex6 != null)
				{
					this.LogWebException(ex6);
					if (this.HandleWebExceptionConnectivityError(ex6))
					{
						flag = false;
					}
				}
				if (flag && this.ShouldRecalculateBackendOnHead(ex6) && this.RecalculateTargetBackend())
				{
					flag = false;
				}
				if (flag)
				{
					this.BeginProxyRequestOrRecalculate();
				}
			});
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0002D243 File Offset: 0x0002B443
		private StreamProxy BuildResponseStream(Func<StreamProxy> outDataResponseStreamFactory, Func<StreamProxy> defaultResponseStreamFactory)
		{
			if (base.ClientRequest.HttpMethod.Equals("RPC_OUT_DATA") && base.ClientRequest.ContentLength == 76)
			{
				return outDataResponseStreamFactory();
			}
			return defaultResponseStreamFactory();
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0002D278 File Offset: 0x0002B478
		private bool ShouldRecalculateBackendOnHead(WebException webException)
		{
			bool flag;
			return webException != null && webException.Response != null && ((base.AuthBehavior.AuthState != AuthState.BackEndFullAuth && base.IsAuthenticationChallengeFromBackend(webException) && base.TryFindKerberosChallenge(webException.Response.Headers[Constants.AuthenticationHeader], out flag)) || base.HandleRoutingError((HttpWebResponse)webException.Response));
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0002D2DC File Offset: 0x0002B4DC
		private bool ParseOutAssociationGuid(Stream stream)
		{
			byte[] array = new byte[104];
			stream.Read(array, 0, 20);
			if (array[2] == 20 && array[8] == 104 && array[18] == 6)
			{
				stream.Read(array, 20, 84);
				byte[] array2 = new byte[16];
				Array.Copy(array, 88, array2, 0, 16);
				this.associationGuid = new Guid(array2);
			}
			return true;
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0002D340 File Offset: 0x0002B540
		private int GetKeyForCasAffinity()
		{
			IIdentity identity = base.HttpContext.User.Identity;
			if (identity is WindowsIdentity || identity is ClientSecurityContextIdentity)
			{
				return identity.GetSecurityIdentifier().GetHashCode();
			}
			return identity.Name.GetHashCode();
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0002D388 File Offset: 0x0002B588
		private AnchorMailbox ResolveToDefaultAnchorMailbox(string originalRpcServerName, string reason)
		{
			string text = base.HttpContext.Items[Constants.WLIDMemberName] as string;
			if (!string.IsNullOrEmpty(text))
			{
				AnchorMailbox anchorMailbox = base.ResolveAnchorMailbox();
				if (anchorMailbox != null)
				{
					base.Logger.AppendString(HttpProxyMetadata.RoutingHint, "-" + reason);
					ExTraceGlobals.BriefTracer.TraceError((long)this.GetHashCode(), "[RpcHttpProxyRequestHandler::ResolveToDefaultAnchorMailbox]: Invalid explicit RPC server name from client: {0}; Defaulting to authenticated user {1} for routing; Context {2}; State {3}", new object[]
					{
						originalRpcServerName,
						text,
						base.TraceContext,
						base.State
					});
					this.rpcServerTarget = text;
					return anchorMailbox;
				}
			}
			throw new HttpProxyException(HttpStatusCode.NotFound, HttpProxySubErrorCode.MailboxGuidWithDomainNotFound, string.Format("RPC server name passed in by client could not be resolved: {0}", originalRpcServerName));
		}

		// Token: 0x040004B2 RID: 1202
		internal const string PreAuthRequestHeaderValue = "true";

		// Token: 0x040004B3 RID: 1203
		private const int LookAheadBufferSize = 104;

		// Token: 0x040004B4 RID: 1204
		private static readonly string[] ProtectedHeaderNames = new string[]
		{
			WellKnownHeader.RpcHttpProxyServerTarget,
			WellKnownHeader.RpcHttpProxyAssociationGuid,
			WellKnownHeader.MailboxDatabaseGuid
		};

		// Token: 0x040004B5 RID: 1205
		private static readonly IntAppSettingsEntry RpcHttpHeadRequestTimeout = new IntAppSettingsEntry(HttpProxySettings.Prefix("RpcHttpHeadRequestTimeout"), 5000, ExTraceGlobals.VerboseTracer);

		// Token: 0x040004B6 RID: 1206
		private static readonly BoolAppSettingsEntry RpcHttpHeadRequestEnabled = new BoolAppSettingsEntry(HttpProxySettings.Prefix("RpcHttpHeadRequestEnabled"), false, ExTraceGlobals.VerboseTracer);

		// Token: 0x040004B7 RID: 1207
		private readonly RpcHttpProxyRules proxyRules;

		// Token: 0x040004B8 RID: 1208
		private HttpWebRequest headRequest;

		// Token: 0x040004B9 RID: 1209
		private string rpcServerTarget;

		// Token: 0x040004BA RID: 1210
		private Guid associationGuid;

		// Token: 0x040004BB RID: 1211
		private bool updateRpcServer;
	}
}
