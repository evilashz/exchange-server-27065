using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.Security.Authentication;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000A3 RID: 163
	internal abstract class BEServerCookieProxyRequestHandler<ServiceType> : ProxyRequestHandler where ServiceType : HttpService
	{
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x00024104 File Offset: 0x00022304
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x0002410C File Offset: 0x0002230C
		internal string Domain { get; set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x00024115 File Offset: 0x00022315
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x0002411D File Offset: 0x0002231D
		protected bool IsWsSecurityRequest { get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x00024126 File Offset: 0x00022326
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x0002412E File Offset: 0x0002232E
		protected bool IsDomainBasedRequest { get; set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060005A3 RID: 1443
		protected abstract ClientAccessType ClientAccessType { get; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00024137 File Offset: 0x00022337
		protected override bool WillAddProtocolSpecificCookiesToClientResponse
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0002413A File Offset: 0x0002233A
		protected virtual int MaxBackEndCookieEntries
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0002413D File Offset: 0x0002233D
		protected virtual string[] BackEndCookieNames
		{
			get
			{
				return BEServerCookieProxyRequestHandler<ServiceType>.ClientSupportedBackEndCookieNames;
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00024144 File Offset: 0x00022344
		protected override bool ShouldBackendRequestBeAnonymous()
		{
			return this.IsWsSecurityRequest;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0002414C File Offset: 0x0002234C
		protected override BackEndServer GetDownLevelClientAccessServer(AnchorMailbox anchorMailbox, BackEndServer mailboxServer)
		{
			if (mailboxServer.Version < Server.E14MinVersion)
			{
				return this.GetE12TargetServer(mailboxServer);
			}
			Uri uri = null;
			BackEndServer downLevelClientAccessServer = DownLevelServerManager.Instance.GetDownLevelClientAccessServer<ServiceType>(anchorMailbox, mailboxServer, this.ClientAccessType, base.Logger, HttpProxyGlobals.ProtocolType == ProtocolType.Owa || HttpProxyGlobals.ProtocolType == ProtocolType.OwaCalendar || HttpProxyGlobals.ProtocolType == ProtocolType.Ecp, out uri);
			if (uri != null)
			{
				Uri uri2 = this.UpdateExternalRedirectUrl(uri);
				if (Uri.Compare(uri2, base.ClientRequest.Url, UriComponents.Host, UriFormat.Unescaped, StringComparison.Ordinal) != 0)
				{
					ExTraceGlobals.VerboseTracer.TraceDebug<string>((long)this.GetHashCode(), "[BEServerCookieProxyRequestHandler::GetDownLevelClientAccessServer]: Stop processing and redirect to {0}.", uri2.ToString());
					throw new HttpException(302, uri2.ToString());
				}
			}
			return downLevelClientAccessServer;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x000241FB File Offset: 0x000223FB
		protected override void ResetForRetryOnError()
		{
			this.haveSetBackEndCookie = false;
			this.removeBackEndCookieEntry = false;
			base.ResetForRetryOnError();
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00024211 File Offset: 0x00022411
		protected virtual BackEndServer GetE12TargetServer(BackEndServer mailboxServer)
		{
			return MailboxServerCache.Instance.GetRandomE15Server(this);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0002421E File Offset: 0x0002241E
		protected virtual Uri UpdateExternalRedirectUrl(Uri originalRedirectUrl)
		{
			return originalRedirectUrl;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00024221 File Offset: 0x00022421
		protected virtual bool ShouldExcludeFromExplicitLogonParsing()
		{
			return true;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00024224 File Offset: 0x00022424
		protected virtual bool IsValidExplicitLogonNode(string node, bool nodeIsLast)
		{
			return true;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00024244 File Offset: 0x00022444
		protected override bool ShouldCopyCookieToServerRequest(HttpCookie cookie)
		{
			return !FbaModule.IsCadataCookie(cookie.Name) && (base.AuthBehavior.AuthState == AuthState.BackEndFullAuth || (!string.Equals(cookie.Name, Constants.LiveIdRPSAuth, StringComparison.OrdinalIgnoreCase) && !string.Equals(cookie.Name, Constants.LiveIdRPSSecAuth, StringComparison.OrdinalIgnoreCase) && !string.Equals(cookie.Name, Constants.LiveIdRPSTAuth, StringComparison.OrdinalIgnoreCase))) && !this.BackEndCookieNames.Any((string cookieName) => string.Equals(cookie.Name, cookieName, StringComparison.OrdinalIgnoreCase)) && !string.Equals(cookie.Name, Constants.RPSBackEndServerCookieName, StringComparison.OrdinalIgnoreCase) && base.ShouldCopyCookieToServerRequest(cookie);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0002430B File Offset: 0x0002250B
		protected override void CopySupplementalCookiesToClientResponse()
		{
			this.SetBackEndCookie();
			base.CopySupplementalCookiesToClientResponse();
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0002431C File Offset: 0x0002251C
		protected override AnchorMailbox ResolveAnchorMailbox()
		{
			AnchorMailbox anchorMailbox = null;
			if (!base.HasPreemptivelyCheckedForRoutingHint)
			{
				anchorMailbox = base.CreateAnchorMailboxFromRoutingHint();
			}
			if (anchorMailbox != null)
			{
				return anchorMailbox;
			}
			anchorMailbox = this.TryGetAnchorMailboxFromWsSecurityRequest();
			if (anchorMailbox != null)
			{
				return anchorMailbox;
			}
			anchorMailbox = this.TryGetAnchorMailboxFromDomainBasedRequest();
			if (anchorMailbox != null)
			{
				return anchorMailbox;
			}
			return AnchorMailboxFactory.CreateFromCaller(this);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00024360 File Offset: 0x00022560
		protected override AnchoredRoutingTarget TryFastTargetCalculationByAnchorMailbox(AnchorMailbox anchorMailbox)
		{
			if (this.backEndCookie == null || !base.IsRetryOnErrorEnabled)
			{
				this.FetchBackEndServerCookie();
			}
			PerfCounters.HttpProxyCacheCountersInstance.CookieUseRateBase.Increment();
			PerfCounters.IncrementMovingPercentagePerformanceCounterBase(PerfCounters.HttpProxyCacheCountersInstance.MovingPercentageCookieUseRate);
			if (this.backEndCookie != null)
			{
				BackEndServer backEndServer = anchorMailbox.AcceptBackEndCookie(this.backEndCookie);
				if (backEndServer != null)
				{
					ExTraceGlobals.VerboseTracer.TraceDebug<AnchorMailbox, BackEndServer>((long)this.GetHashCode(), "[BEServerCookieProxyRequestHandler::TryFastTargetCalculationByAnchorMailbox]: Back end server {1} resolved from anchor mailbox {0}", anchorMailbox, backEndServer);
					base.Logger.AppendString(HttpProxyMetadata.RoutingHint, "-ServerCookie");
					return new AnchoredRoutingTarget(anchorMailbox, backEndServer);
				}
			}
			ExTraceGlobals.VerboseTracer.TraceDebug<AnchorMailbox>((long)this.GetHashCode(), "[BEServerCookieProxyRequestHandler::TryFastTargetCalculationByAnchorMailbox]: No cookie associated with anchor mailbox {0}", anchorMailbox);
			return base.TryFastTargetCalculationByAnchorMailbox(anchorMailbox);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00024410 File Offset: 0x00022610
		protected virtual string TryGetExplicitLogonNode(ExplicitLogonNode node)
		{
			if (this.ShouldExcludeFromExplicitLogonParsing())
			{
				return null;
			}
			string text = null;
			bool nodeIsLast;
			string explicitLogonNode = ProtocolHelper.GetExplicitLogonNode(base.ClientRequest.ApplicationPath, base.ClientRequest.FilePath, node, out nodeIsLast);
			if (!string.IsNullOrEmpty(explicitLogonNode) && this.IsValidExplicitLogonNode(explicitLogonNode, nodeIsLast))
			{
				text = explicitLogonNode;
				ExTraceGlobals.VerboseTracer.TraceDebug<int, string>((long)this.GetHashCode(), "[OwaEcpProxyRequestHandler::TryGetExplicitLogonNode]: Context {0}; candidate explicit logon node: {1}", base.TraceContext, text);
			}
			return text;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0002447C File Offset: 0x0002267C
		protected AnchorMailbox TryGetAnchorMailboxFromWsSecurityRequest()
		{
			if (!this.IsWsSecurityRequest)
			{
				return null;
			}
			ExTraceGlobals.VerboseTracer.TraceDebug<int>((long)this.GetHashCode(), "[BEServerCookieProxyRequestHandler::TryGetAnchorMailboxFromWsSecurityRequest]: Context {0}; WSSecurity request.", base.TraceContext);
			WsSecurityParser @object = new WsSecurityParser(base.TraceContext);
			bool flag = false;
			string address;
			if (base.ClientRequest.IsPartnerAuthRequest())
			{
				address = base.ParseClientRequest<string>(new Func<Stream, string>(@object.FindAddressFromPartnerAuthRequest), 73628);
			}
			else if (base.ClientRequest.IsX509CertAuthRequest())
			{
				address = base.ParseClientRequest<string>(new Func<Stream, string>(@object.FindAddressFromX509CertAuthRequest), 73628);
			}
			else
			{
				KeyValuePair<string, bool> keyValuePair = base.ParseClientRequest<KeyValuePair<string, bool>>(new Func<Stream, KeyValuePair<string, bool>>(@object.FindAddressFromWsSecurityRequest), 73628);
				flag = keyValuePair.Value;
				address = keyValuePair.Key;
			}
			if (flag)
			{
				base.Logger.Set(HttpProxyMetadata.RoutingHint, "WSSecurityRequest-DelegationToken-Random");
				return new AnonymousAnchorMailbox(this);
			}
			return AnchorMailboxFactory.CreateFromSamlTokenAddress(address, this);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0002455C File Offset: 0x0002275C
		protected AnchorMailbox TryGetAnchorMailboxFromDomainBasedRequest()
		{
			if (!this.IsDomainBasedRequest)
			{
				return null;
			}
			ExTraceGlobals.VerboseTracer.TraceDebug<int, string>((long)this.GetHashCode(), "[BEServerCookieProxyRequestHandler::ResolveAnchorMailbox]: Context {0}; Domain-based request with domain {1}.", base.TraceContext, this.Domain);
			if (!string.IsNullOrEmpty(this.Domain) && SmtpAddress.IsValidDomain(this.Domain))
			{
				base.Logger.Set(HttpProxyMetadata.RoutingHint, "DomainBaseRequest-Domain");
				return new DomainAnchorMailbox(this.Domain, this);
			}
			base.Logger.Set(HttpProxyMetadata.RoutingHint, "DomainBaseRequest-Random");
			return new AnonymousAnchorMailbox(this);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x000245F0 File Offset: 0x000227F0
		protected ServerVersionAnchorMailbox<ServiceType> GetServerVersionAnchorMailbox(string serverVersionString)
		{
			ServerVersion serverVersion = new ServerVersion(LocalServerCache.LocalServer.VersionNumber);
			if (!string.IsNullOrEmpty(serverVersionString))
			{
				Match match = Constants.ExchClientVerRegex.Match(serverVersionString);
				ServerVersion serverVersion2;
				if (match.Success && RegexUtilities.TryGetServerVersionFromRegexMatch(match, out serverVersion2) && serverVersion2.Major >= 14)
				{
					serverVersion = serverVersion2;
				}
			}
			int build = (serverVersion.Build > 0) ? (serverVersion.Build - 1) : serverVersion.Build;
			serverVersion = new ServerVersion(serverVersion.Major, serverVersion.Minor, build, serverVersion.Revision);
			return new ServerVersionAnchorMailbox<ServiceType>(serverVersion, this.ClientAccessType, false, this);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00024680 File Offset: 0x00022880
		protected override void UpdateOrInvalidateAnchorMailboxCache(Guid mdbGuid, string resourceForest)
		{
			this.removeBackEndCookieEntry = true;
			this.SetBackEndCookie();
			base.UpdateOrInvalidateAnchorMailboxCache(mdbGuid, resourceForest);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00024698 File Offset: 0x00022898
		protected override void OnDatabaseNotFound(AnchorMailbox anchorMailbox)
		{
			foreach (string name in this.BackEndCookieNames)
			{
				Utility.DeleteCookie(base.ClientRequest, base.ClientResponse, name, this.GetCookiePath(), false);
				Utility.DeleteCookie(base.ClientRequest, base.ClientResponse, name, null, true);
			}
			base.OnDatabaseNotFound(anchorMailbox);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000246F4 File Offset: 0x000228F4
		private void FetchBackEndServerCookie()
		{
			foreach (string text in this.BackEndCookieNames)
			{
				if (this.ShouldProcessBackEndCookie(text))
				{
					HttpCookie httpCookie = base.ClientRequest.Cookies[text];
					if (httpCookie != null && httpCookie.Values != null)
					{
						this.backEndCookie = httpCookie;
						if (ExTraceGlobals.VerboseTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							StringBuilder stringBuilder = new StringBuilder();
							foreach (string text2 in httpCookie.Values.AllKeys)
							{
								stringBuilder.AppendFormat("{0}:{1};", text2, httpCookie.Values[text2]);
							}
							ExTraceGlobals.VerboseTracer.TraceDebug<int, string>((long)this.GetHashCode(), "[BEServerCookieProxyRequestHandler::FetchBackEndServerCookie]: Context {0}; Recieving cookie {1}", base.TraceContext, stringBuilder.ToString());
							return;
						}
						break;
					}
				}
			}
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x000247D0 File Offset: 0x000229D0
		private void SanitizeCookie(HttpCookie backEndCookie)
		{
			if (backEndCookie == null)
			{
				return;
			}
			if (this.removeBackEndCookieEntry && base.AnchoredRoutingTarget != null)
			{
				string text = base.AnchoredRoutingTarget.AnchorMailbox.ToCookieKey();
				backEndCookie.Values.Remove(text);
				ExTraceGlobals.VerboseTracer.TraceDebug<int, string>((long)this.GetHashCode(), "[BEServerCookieProxyRequestHandler::SanitizeCookie]: Context {0}; Removed cookie entry with key {1}.", base.TraceContext, text);
			}
			ExDateTime t = ExDateTime.UtcNow.AddYears(-30);
			int num = 0;
			for (int i = backEndCookie.Values.Count - 1; i >= 0; i--)
			{
				bool flag = true;
				BackEndCookieEntryBase backEndCookieEntryBase = null;
				if (num < this.MaxBackEndCookieEntries)
				{
					string entryValue = backEndCookie.Values[i];
					if (BackEndCookieEntryParser.TryParse(entryValue, out backEndCookieEntryBase))
					{
						flag = backEndCookieEntryBase.Expired;
						if (!flag && this.removeBackEndCookieEntry && base.AnchoredRoutingTarget != null && backEndCookieEntryBase.ShouldInvalidate(base.AnchoredRoutingTarget.BackEndServer))
						{
							flag = true;
						}
					}
				}
				if (flag)
				{
					string key = backEndCookie.Values.GetKey(i);
					backEndCookie.Values.Remove(key);
					ExTraceGlobals.VerboseTracer.TraceDebug<int, string>((long)this.GetHashCode(), "[BEServerCookieProxyRequestHandler::SanitizeCookie]: Context {0}; Removed cookie entry with key {1}.", base.TraceContext, key);
				}
				else
				{
					num++;
					if (backEndCookieEntryBase.ExpiryTime > t)
					{
						t = backEndCookieEntryBase.ExpiryTime;
					}
				}
			}
			ExTraceGlobals.VerboseTracer.TraceDebug<int, string>((long)this.GetHashCode(), "[BEServerCookieProxyRequestHandler::SanitizeCookie]: Context {0}; {1}", base.TraceContext, (num == 0) ? "Marking current cookie as expired." : "Extending cookie expiration.");
			backEndCookie.Expires = t.UniversalTime;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00024950 File Offset: 0x00022B50
		private void SetBackEndCookie()
		{
			if (this.haveSetBackEndCookie)
			{
				return;
			}
			foreach (string text in this.BackEndCookieNames)
			{
				if (this.ShouldProcessBackEndCookie(text))
				{
					HttpCookie httpCookie = base.ClientRequest.Cookies[text];
					if (httpCookie == null)
					{
						ExTraceGlobals.VerboseTracer.TraceDebug<int>((long)this.GetHashCode(), "[BEServerCookieProxyRequestHandler::SetBackEndCookie]: Context {0}; Client request does not include back end cookie.", base.TraceContext);
						httpCookie = new HttpCookie(text);
					}
					httpCookie.HttpOnly = true;
					httpCookie.Secure = base.ClientRequest.IsSecureConnection;
					httpCookie.Path = this.GetCookiePath();
					if (base.AnchoredRoutingTarget != null)
					{
						string text2 = base.AnchoredRoutingTarget.AnchorMailbox.ToCookieKey();
						BackEndCookieEntryBase backEndCookieEntryBase = base.AnchoredRoutingTarget.AnchorMailbox.BuildCookieEntryForTarget(base.AnchoredRoutingTarget.BackEndServer, base.ProxyToDownLevel, this.ShouldBackEndCookieHaveResourceForest(text));
						if (backEndCookieEntryBase != null)
						{
							httpCookie.Values[text2] = backEndCookieEntryBase.ToObscureString();
							ExTraceGlobals.VerboseTracer.TraceDebug<int, string, BackEndCookieEntryBase>((long)this.GetHashCode(), "[BEServerCookieProxyRequestHandler::SetBackEndCookie]: Context {0}; Setting cookie entry {1}={2}.", base.TraceContext, text2, backEndCookieEntryBase);
						}
					}
					this.SanitizeCookie(httpCookie);
					base.ClientResponse.Cookies.Add(httpCookie);
					this.haveSetBackEndCookie = true;
				}
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00024A8C File Offset: 0x00022C8C
		private string GetCookiePath()
		{
			if (base.ClientRequest.ApplicationPath.Length < base.ClientRequest.Url.AbsolutePath.Length)
			{
				return base.ClientRequest.Url.AbsolutePath.Remove(base.ClientRequest.ApplicationPath.Length);
			}
			return base.ClientRequest.Url.AbsolutePath;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00024AF8 File Offset: 0x00022CF8
		private bool ShouldProcessBackEndCookie(string backEndCookieName)
		{
			return this.BackEndCookieNames.Length <= 1 || (((HttpProxySettings.SupportBackEndCookie.Value & ProxyRequestHandler.SupportBackEndCookie.V1) != (ProxyRequestHandler.SupportBackEndCookie)0 || !string.Equals(backEndCookieName, "X-BackEndCookie", StringComparison.OrdinalIgnoreCase)) && ((HttpProxySettings.SupportBackEndCookie.Value & ProxyRequestHandler.SupportBackEndCookie.V2) != (ProxyRequestHandler.SupportBackEndCookie)0 || !string.Equals(backEndCookieName, "X-BackEndCookie2", StringComparison.OrdinalIgnoreCase)));
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00024B4F File Offset: 0x00022D4F
		private bool ShouldBackEndCookieHaveResourceForest(string backEndCookieName)
		{
			if ((HttpProxySettings.SupportBackEndCookie.Value & ProxyRequestHandler.SupportBackEndCookie.V2) != (ProxyRequestHandler.SupportBackEndCookie)0)
			{
				if (this.BackEndCookieNames.Length <= 1)
				{
					return true;
				}
				if (string.Equals(backEndCookieName, "X-BackEndCookie2", StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000407 RID: 1031
		private const string BackEndCookie2Name = "X-BackEndCookie2";

		// Token: 0x04000408 RID: 1032
		private static readonly string[] ClientSupportedBackEndCookieNames = new string[]
		{
			"X-BackEndCookie2",
			"X-BackEndCookie"
		};

		// Token: 0x04000409 RID: 1033
		private bool haveSetBackEndCookie;

		// Token: 0x0400040A RID: 1034
		private bool removeBackEndCookieEntry;

		// Token: 0x0400040B RID: 1035
		private HttpCookie backEndCookie;
	}
}
