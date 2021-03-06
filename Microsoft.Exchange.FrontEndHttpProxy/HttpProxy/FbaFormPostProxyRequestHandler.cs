using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security;
using System.Text;
using System.Web;
using System.Web.Configuration;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Cafe;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000B7 RID: 183
	internal class FbaFormPostProxyRequestHandler : OwaProxyRequestHandler
	{
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x00029534 File Offset: 0x00027734
		internal static bool DisableSSORedirects
		{
			get
			{
				if (FbaFormPostProxyRequestHandler.disableSSORedirects == null)
				{
					string value = WebConfigurationManager.AppSettings["DisableSSORedirects"];
					bool value2;
					if (!bool.TryParse(value, out value2))
					{
						value2 = false;
					}
					FbaFormPostProxyRequestHandler.disableSSORedirects = new bool?(value2);
				}
				return FbaFormPostProxyRequestHandler.disableSSORedirects.Value;
			}
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00029580 File Offset: 0x00027780
		public static char[] EncodeForSingleQuotedAttribute(char c)
		{
			char[] result = null;
			if (c == '&')
			{
				result = FbaFormPostProxyRequestHandler.EncodedAmpersand;
			}
			else if (c == '\'')
			{
				result = FbaFormPostProxyRequestHandler.EncodedApostrophe;
			}
			return result;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x000295A8 File Offset: 0x000277A8
		protected override AnchorMailbox ResolveAnchorMailbox()
		{
			string text = base.HttpContext.Items["destination"] as string;
			Uri uri;
			if (!Uri.TryCreate(text, UriKind.Absolute, out uri))
			{
				throw new HttpException(400, "destination value is not valid");
			}
			string text2 = null;
			bool flag2;
			string text3;
			bool flag = FbaFormPostProxyRequestHandler.IsExplicitLogon(HttpRuntime.AppDomainAppVirtualPath, uri.PathAndQuery, uri.OriginalString, out flag2, out text2, out text3);
			if (flag)
			{
				this.explicitLogonUser = text2;
			}
			AnchorMailbox anchorMailbox;
			if (!string.IsNullOrEmpty(this.explicitLogonUser))
			{
				anchorMailbox = new SmtpAnchorMailbox(this.explicitLogonUser, this);
			}
			else
			{
				anchorMailbox = AnchorMailboxFactory.CreateFromCaller(this);
			}
			UserBasedAnchorMailbox userBasedAnchorMailbox = anchorMailbox as UserBasedAnchorMailbox;
			if (userBasedAnchorMailbox != null)
			{
				if (UrlUtilities.IsEacUrl(text))
				{
					userBasedAnchorMailbox.CacheKeyPostfix = "_EAC";
				}
				else
				{
					userBasedAnchorMailbox.MissingDatabaseHandler = new Func<ADRawEntry, ADObjectId>(base.ResolveMailboxDatabase);
				}
			}
			return anchorMailbox;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00029674 File Offset: 0x00027874
		protected override BackEndServer GetDownLevelClientAccessServer(AnchorMailbox anchorMailbox, BackEndServer mailboxServer)
		{
			base.LogElapsedTime("E_GetDLCAS");
			Uri uri = null;
			BackEndServer downLevelClientAccessServer = DownLevelServerManager.Instance.GetDownLevelClientAccessServer<HttpService>(anchorMailbox, mailboxServer, this.ClientAccessType, base.Logger, HttpProxyGlobals.ProtocolType == ProtocolType.Owa || HttpProxyGlobals.ProtocolType == ProtocolType.OwaCalendar || HttpProxyGlobals.ProtocolType == ProtocolType.Ecp, out uri);
			base.LogElapsedTime("L_GetDLCAS");
			return downLevelClientAccessServer;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x000296D0 File Offset: 0x000278D0
		protected override bool ShouldContinueProxy()
		{
			this.HandleFbaFormPost(base.AnchoredRoutingTarget.BackEndServer);
			return true;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x000296E4 File Offset: 0x000278E4
		private static bool IsExplicitLogon(string appVdir, string requestVirtualPath, string requestRawUrl, out bool endsWithSlash, out string alternateMailboxSmtpAddress, out string updatedRequestUrl)
		{
			bool flag = false;
			alternateMailboxSmtpAddress = string.Empty;
			updatedRequestUrl = appVdir;
			int num = appVdir.Length + 1;
			endsWithSlash = false;
			int length = requestVirtualPath.Length;
			int num2 = length - 1;
			for (int i = num; i < length; i++)
			{
				if (i != num && requestVirtualPath[i] == '@')
				{
					flag = true;
				}
				if (requestVirtualPath[i] == '/')
				{
					endsWithSlash = true;
					num2 = i - 1;
					break;
				}
			}
			if (flag)
			{
				string text = appVdir;
				if (text.Length == 1 && text[0] == '/')
				{
					text = string.Empty;
				}
				if (endsWithSlash)
				{
					updatedRequestUrl = text + requestVirtualPath.Substring(num2 + 1);
				}
				alternateMailboxSmtpAddress = requestVirtualPath.Substring(num, num2 - num + 1);
			}
			return flag;
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00029799 File Offset: 0x00027999
		private static string CheckRedirectUrlForNewline(string destinationUrl)
		{
			if (destinationUrl.IndexOf('\n') >= 0)
			{
				destinationUrl = destinationUrl.Replace("\n", HttpUtility.UrlEncode("\n"));
			}
			return destinationUrl;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x000297C0 File Offset: 0x000279C0
		private static FbaFormPostProxyRequestHandler.LegacyRedirectFailureCause NeedCrossSiteRedirect(BackEndServer backEndServer, Site mailboxSite, Site currentServerSite, OwaServerVersion mailboxVersion, bool isEcpUrl, out Uri crossSiteRedirectUrl, out bool isSameAuthMethod)
		{
			isSameAuthMethod = false;
			crossSiteRedirectUrl = null;
			OwaServerVersion.CreateFromVersionString(HttpProxyGlobals.ApplicationVersion);
			FbaFormPostProxyRequestHandler.LegacyRedirectFailureCause result = FbaFormPostProxyRequestHandler.LegacyRedirectFailureCause.None;
			if (mailboxSite == null)
			{
				return result;
			}
			if (!mailboxSite.Equals(currentServerSite))
			{
				crossSiteRedirectUrl = FbaFormPostProxyRequestHandler.FindRedirectOwaUrlCrossSite(mailboxSite, mailboxVersion.Major, OwaVdirConfiguration.Instance.InternalAuthenticationMethod, OwaVdirConfiguration.Instance.ExternalAuthenticationMethod, backEndServer, out isSameAuthMethod, out result);
				if (isEcpUrl && crossSiteRedirectUrl != null)
				{
					crossSiteRedirectUrl = FbaFormPostProxyRequestHandler.FindRedirectEcpUrlCrossSite(mailboxSite, mailboxVersion.Major, out result);
				}
			}
			return result;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0002991C File Offset: 0x00027B1C
		private static Uri FindRedirectOwaUrlCrossSite(Site targetSite, int expectedMajorVersion, AuthenticationMethod internalAutheticationMethod, AuthenticationMethod externalAuthenticationMethod, BackEndServer backEndServer, out bool isSameAuthMethod, out FbaFormPostProxyRequestHandler.LegacyRedirectFailureCause failureCause)
		{
			failureCause = FbaFormPostProxyRequestHandler.LegacyRedirectFailureCause.None;
			isSameAuthMethod = false;
			bool isSameAuthExternalService = false;
			OwaService clientExternalService = null;
			ServiceTopology currentServiceTopology = ServiceTopology.GetCurrentServiceTopology("f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\RequestHandlers\\FbaFormPostProxyRequestHandler.cs", "FindRedirectOwaUrlCrossSite", 390);
			string mailboxServerFQDN = backEndServer.Fqdn;
			new List<OwaService>();
			currentServiceTopology.ForEach<OwaService>(delegate(OwaService owaService)
			{
				if (ServiceTopology.IsOnSite(owaService, targetSite, "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\RequestHandlers\\FbaFormPostProxyRequestHandler.cs", "FindRedirectOwaUrlCrossSite", 401) && owaService.ClientAccessType == ClientAccessType.External)
				{
					int major = OwaServerVersion.CreateFromVersionNumber(owaService.ServerVersionNumber).Major;
					if (major == expectedMajorVersion)
					{
						bool flag = false;
						if (owaService.AuthenticationMethod == internalAutheticationMethod || ((internalAutheticationMethod & AuthenticationMethod.Fba) != AuthenticationMethod.None && (owaService.AuthenticationMethod & AuthenticationMethod.Fba) != AuthenticationMethod.None))
						{
							flag = true;
							if (!isSameAuthExternalService)
							{
								clientExternalService = null;
								isSameAuthExternalService = true;
							}
						}
						if (flag || !isSameAuthExternalService)
						{
							if (clientExternalService == null)
							{
								clientExternalService = owaService;
								return;
							}
							if (ServiceTopology.CasMbxServicesFirst(owaService, clientExternalService, mailboxServerFQDN, "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\RequestHandlers\\FbaFormPostProxyRequestHandler.cs", "FindRedirectOwaUrlCrossSite", 432) < 0)
							{
								clientExternalService = owaService;
							}
						}
					}
				}
			}, "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\RequestHandlers\\FbaFormPostProxyRequestHandler.cs", "FindRedirectOwaUrlCrossSite", 396);
			if (clientExternalService != null)
			{
				isSameAuthMethod = isSameAuthExternalService;
				return clientExternalService.Url;
			}
			failureCause = FbaFormPostProxyRequestHandler.LegacyRedirectFailureCause.NoCasFound;
			return null;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00029A24 File Offset: 0x00027C24
		private static Uri FindRedirectEcpUrlCrossSite(Site targetSite, int expectedMajorVersion, out FbaFormPostProxyRequestHandler.LegacyRedirectFailureCause failureCause)
		{
			failureCause = FbaFormPostProxyRequestHandler.LegacyRedirectFailureCause.None;
			EcpService clientExternalService = null;
			ServiceTopology currentServiceTopology = ServiceTopology.GetCurrentServiceTopology("f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\RequestHandlers\\FbaFormPostProxyRequestHandler.cs", "FindRedirectEcpUrlCrossSite", 470);
			currentServiceTopology.ForEach<EcpService>(delegate(EcpService ecpService)
			{
				if (ServiceTopology.IsOnSite(ecpService, targetSite, "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\RequestHandlers\\FbaFormPostProxyRequestHandler.cs", "FindRedirectEcpUrlCrossSite", 476) && ecpService.ClientAccessType == ClientAccessType.External)
				{
					int major = OwaServerVersion.CreateFromVersionNumber(ecpService.ServerVersionNumber).Major;
					if (major == expectedMajorVersion)
					{
						clientExternalService = ecpService;
					}
				}
			}, "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\RequestHandlers\\FbaFormPostProxyRequestHandler.cs", "FindRedirectEcpUrlCrossSite", 473);
			if (clientExternalService != null)
			{
				return clientExternalService.Url;
			}
			failureCause = FbaFormPostProxyRequestHandler.LegacyRedirectFailureCause.NoCasFound;
			return null;
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00029AA0 File Offset: 0x00027CA0
		private static FbaFormPostProxyRequestHandler.LegacyRedirectFailureCause NeedOnSiteLegacyRedirect(BackEndServer backEndServer, Site mailboxSite, Site currentServerSite, OwaServerVersion mailboxVersion, out Uri legacyRedirectUrl, out bool isSameAuthMethod)
		{
			isSameAuthMethod = false;
			legacyRedirectUrl = null;
			OwaServerVersion owaServerVersion = OwaServerVersion.CreateFromVersionString(HttpProxyGlobals.ApplicationVersion);
			if (mailboxSite == null)
			{
				mailboxSite = currentServerSite;
			}
			FbaFormPostProxyRequestHandler.LegacyRedirectFailureCause result = FbaFormPostProxyRequestHandler.LegacyRedirectFailureCause.None;
			bool flag = mailboxSite.Equals(currentServerSite);
			if (flag && owaServerVersion.Major > mailboxVersion.Major && mailboxVersion.Major == (int)ExchangeObjectVersion.Exchange2007.ExchangeBuild.Major)
			{
				legacyRedirectUrl = FbaFormPostProxyRequestHandler.FindRedirectOwaUrlOnSiteForMismatchVersion(mailboxSite, mailboxVersion.Major, OwaVdirConfiguration.Instance.InternalAuthenticationMethod, OwaVdirConfiguration.Instance.ExternalAuthenticationMethod, backEndServer, out isSameAuthMethod, out result);
			}
			return result;
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00029CC0 File Offset: 0x00027EC0
		private static Uri FindRedirectOwaUrlOnSiteForMismatchVersion(Site targetSite, int expectedMajorVersion, AuthenticationMethod internalAutheticationMethod, AuthenticationMethod externalAuthenticationMethod, BackEndServer backEndServer, out bool isSameAuthMethod, out FbaFormPostProxyRequestHandler.LegacyRedirectFailureCause failureCause)
		{
			failureCause = FbaFormPostProxyRequestHandler.LegacyRedirectFailureCause.None;
			isSameAuthMethod = true;
			bool isSameAuthInternalService = false;
			bool isSameAuthExternalService = false;
			OwaService clientInternalService = null;
			OwaService clientExternalService = null;
			string mailboxServerFQDN = backEndServer.Fqdn;
			ServiceTopology currentServiceTopology = ServiceTopology.GetCurrentServiceTopology("f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\RequestHandlers\\FbaFormPostProxyRequestHandler.cs", "FindRedirectOwaUrlOnSiteForMismatchVersion", 577);
			new List<OwaService>();
			currentServiceTopology.ForEach<OwaService>(delegate(OwaService owaService)
			{
				if (ServiceTopology.IsOnSite(owaService, targetSite, "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\RequestHandlers\\FbaFormPostProxyRequestHandler.cs", "FindRedirectOwaUrlOnSiteForMismatchVersion", 586))
				{
					if (owaService.ClientAccessType == ClientAccessType.External)
					{
						int major = OwaServerVersion.CreateFromVersionNumber(owaService.ServerVersionNumber).Major;
						if (major == expectedMajorVersion)
						{
							bool flag = false;
							if (owaService.AuthenticationMethod == internalAutheticationMethod || ((internalAutheticationMethod & AuthenticationMethod.Fba) != AuthenticationMethod.None && (owaService.AuthenticationMethod & AuthenticationMethod.Fba) != AuthenticationMethod.None))
							{
								flag = true;
								if (!isSameAuthExternalService)
								{
									clientExternalService = null;
									isSameAuthExternalService = true;
								}
							}
							if (flag || !isSameAuthExternalService)
							{
								if (clientExternalService == null)
								{
									clientExternalService = owaService;
									return;
								}
								if (ServiceTopology.CasMbxServicesFirst(owaService, clientExternalService, mailboxServerFQDN, "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\RequestHandlers\\FbaFormPostProxyRequestHandler.cs", "FindRedirectOwaUrlOnSiteForMismatchVersion", 617) < 0)
								{
									clientExternalService = owaService;
									return;
								}
							}
						}
					}
					else if (owaService.ClientAccessType == ClientAccessType.Internal)
					{
						int major2 = OwaServerVersion.CreateFromVersionNumber(owaService.ServerVersionNumber).Major;
						if (major2 == expectedMajorVersion && clientExternalService == null)
						{
							bool flag = false;
							if (owaService.AuthenticationMethod == internalAutheticationMethod || ((internalAutheticationMethod & AuthenticationMethod.Fba) != AuthenticationMethod.None && (owaService.AuthenticationMethod & AuthenticationMethod.Fba) != AuthenticationMethod.None))
							{
								flag = true;
								if (!isSameAuthInternalService)
								{
									clientInternalService = null;
									isSameAuthInternalService = true;
								}
							}
							if (flag || !isSameAuthInternalService)
							{
								if (clientInternalService == null)
								{
									clientInternalService = owaService;
									return;
								}
								if (ServiceTopology.CasMbxServicesFirst(owaService, clientInternalService, mailboxServerFQDN, "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\RequestHandlers\\FbaFormPostProxyRequestHandler.cs", "FindRedirectOwaUrlOnSiteForMismatchVersion", 656) > 0)
								{
									clientInternalService = owaService;
								}
							}
						}
					}
				}
			}, "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\RequestHandlers\\FbaFormPostProxyRequestHandler.cs", "FindRedirectOwaUrlOnSiteForMismatchVersion", 581);
			if (clientExternalService != null)
			{
				isSameAuthMethod = isSameAuthExternalService;
				return clientExternalService.Url;
			}
			if (clientInternalService != null)
			{
				isSameAuthMethod = isSameAuthInternalService;
				return clientInternalService.Url;
			}
			failureCause = FbaFormPostProxyRequestHandler.LegacyRedirectFailureCause.NoCasFound;
			return null;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00029D94 File Offset: 0x00027F94
		private static bool IsOwaUrl(Uri requestUrl, OwaUrl owaUrl, bool exactMatch)
		{
			return FbaFormPostProxyRequestHandler.IsOwaUrl(requestUrl, owaUrl, exactMatch, true);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00029DA0 File Offset: 0x00027FA0
		private static bool IsOwaUrl(Uri requestUrl, OwaUrl owaUrl, bool exactMatch, bool useLocal)
		{
			int length = owaUrl.ImplicitUrl.Length;
			string text = useLocal ? requestUrl.LocalPath : requestUrl.PathAndQuery;
			bool flag = string.Compare(text, 0, owaUrl.ImplicitUrl, 0, length, StringComparison.OrdinalIgnoreCase) == 0;
			if (exactMatch)
			{
				flag = (flag && length == text.Length);
			}
			return flag;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00029DF4 File Offset: 0x00027FF4
		private static string GetNoScriptHtml()
		{
			string htmlEncoded = LocalizedStrings.GetHtmlEncoded(719849305);
			return string.Format(htmlEncoded, "<a href=\"http://www.microsoft.com/windows/ie/downloads/default.mspx\">", "</a>");
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00029E1C File Offset: 0x0002801C
		private static Uri AppendSmtpAddressToUrl(Uri url, string smtpAddress)
		{
			UriBuilder uriBuilder = new UriBuilder(url);
			if (!uriBuilder.Path.EndsWith("/", StringComparison.Ordinal))
			{
				UriBuilder uriBuilder2 = uriBuilder;
				uriBuilder2.Path += "/";
			}
			UriBuilder uriBuilder3 = uriBuilder;
			uriBuilder3.Path += smtpAddress;
			return uriBuilder.Uri;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00029E74 File Offset: 0x00028074
		private void HandleFbaFormPost(BackEndServer backEndServer)
		{
			HttpContext httpContext = base.HttpContext;
			HttpResponse response = httpContext.Response;
			Uri uri = null;
			string text = httpContext.Items["destination"] as string;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = true;
			string fqdn = backEndServer.Fqdn;
			int version = backEndServer.Version;
			OwaServerVersion owaServerVersion = null;
			bool flag4 = false;
			ServiceTopology currentServiceTopology = ServiceTopology.GetCurrentServiceTopology("f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\RequestHandlers\\FbaFormPostProxyRequestHandler.cs", "HandleFbaFormPost", 780);
			Site site = currentServiceTopology.GetSite(fqdn, "f:\\15.00.1497\\sources\\dev\\cafe\\src\\HttpProxy\\RequestHandlers\\FbaFormPostProxyRequestHandler.cs", "HandleFbaFormPost", 781);
			if (site != null && !site.Equals(HttpProxyGlobals.LocalSite.Member))
			{
				flag3 = false;
			}
			if (!FbaFormPostProxyRequestHandler.DisableSSORedirects)
			{
				owaServerVersion = OwaServerVersion.CreateFromVersionNumber(version);
				if (UrlUtilities.IsEcpUrl(text) && owaServerVersion.Major < (int)ExchangeObjectVersion.Exchange2010.ExchangeBuild.Major)
				{
					flag = false;
					flag2 = false;
				}
				else if (!flag3 && !UserAgentParser.IsMonitoringRequest(base.ClientRequest.UserAgent))
				{
					if (owaServerVersion.Major >= (int)ExchangeObjectVersion.Exchange2007.ExchangeBuild.Major)
					{
						FbaFormPostProxyRequestHandler.LegacyRedirectFailureCause legacyRedirectFailureCause = FbaFormPostProxyRequestHandler.NeedCrossSiteRedirect(backEndServer, site, HttpProxyGlobals.LocalSite.Member, owaServerVersion, UrlUtilities.IsEcpUrl(text), out uri, out flag4);
						string authority = base.ClientRequest.Url.Authority;
						string b = (uri == null) ? string.Empty : uri.Authority;
						flag2 = (legacyRedirectFailureCause != FbaFormPostProxyRequestHandler.LegacyRedirectFailureCause.NoCasFound && !string.Equals(authority, b, StringComparison.OrdinalIgnoreCase) && (legacyRedirectFailureCause != FbaFormPostProxyRequestHandler.LegacyRedirectFailureCause.None || uri != null));
						if (uri == null && owaServerVersion.Major == (int)ExchangeObjectVersion.Exchange2007.ExchangeBuild.Major)
						{
							flag = (FbaFormPostProxyRequestHandler.NeedOnSiteLegacyRedirect(backEndServer, null, HttpProxyGlobals.LocalSite.Member, owaServerVersion, out uri, out flag4) != FbaFormPostProxyRequestHandler.LegacyRedirectFailureCause.None || uri != null);
						}
					}
				}
				else
				{
					flag = (FbaFormPostProxyRequestHandler.NeedOnSiteLegacyRedirect(backEndServer, site, HttpProxyGlobals.LocalSite.Member, owaServerVersion, out uri, out flag4) != FbaFormPostProxyRequestHandler.LegacyRedirectFailureCause.None || uri != null);
				}
			}
			if (flag2 || flag)
			{
				if (uri != null)
				{
					string authority2 = base.ClientRequest.Url.Authority;
					string authority3 = uri.Authority;
					if (string.Compare(authority2, authority3, StringComparison.OrdinalIgnoreCase) == 0)
					{
						throw new HttpException(403, "Redirect loop detected");
					}
				}
				using (SecureNameValueCollection secureNameValueCollection = new SecureNameValueCollection())
				{
					int num = (int)base.HttpContext.Items["flags"];
					secureNameValueCollection.AddUnsecureNameValue("destination", base.HttpContext.Items["destination"] as string);
					secureNameValueCollection.AddUnsecureNameValue("username", base.HttpContext.Items["username"] as string);
					secureNameValueCollection.AddUnsecureNameValue("flags", num.ToString(CultureInfo.InvariantCulture));
					using (SecureString secureString = base.HttpContext.Items["password"] as SecureString)
					{
						secureNameValueCollection.AddSecureNameValue("password", secureString);
						if (flag)
						{
							if (uri == null)
							{
								AspNetHelper.TransferToErrorPage(httpContext, ErrorFE.FEErrorCodes.NoLegacyCAS);
							}
							else if (flag4)
							{
								if (uri.Scheme == Uri.UriSchemeHttps)
								{
									ExTraceGlobals.VerboseTracer.TraceDebug<string>((long)this.GetHashCode(), "FbaFormPostProxyRequestHandler - SSO redirecting to {0}", uri.ToString());
									this.RedirectUsingSSOFBA(secureNameValueCollection, uri, response, owaServerVersion.Major);
									response.End();
								}
								else
								{
									AspNetHelper.TransferToErrorPage(httpContext, ErrorFE.FEErrorCodes.NoFbaSSL);
								}
							}
							else
							{
								ExTraceGlobals.VerboseTracer.TraceDebug<string>((long)this.GetHashCode(), "FbaFormPostProxyRequestHandler - redirecting to {0}", uri.ToString());
								base.PfdTracer.TraceRedirect("FbaAuth", uri.ToString());
								response.Redirect(FbaFormPostProxyRequestHandler.CheckRedirectUrlForNewline(uri.ToString()));
							}
						}
						else if (flag2)
						{
							if (uri == null)
							{
								AspNetHelper.TransferToErrorPage(httpContext, ErrorFE.FEErrorCodes.NoLegacyCAS);
							}
							else
							{
								Uri uri2 = uri;
								if (this.explicitLogonUser != null)
								{
									uri2 = FbaFormPostProxyRequestHandler.AppendSmtpAddressToUrl(uri, this.explicitLogonUser);
								}
								if (flag4)
								{
									if (uri.Scheme == Uri.UriSchemeHttps)
									{
										ExTraceGlobals.VerboseTracer.TraceDebug<string>((long)this.GetHashCode(), "FbaFormPostProxyRequestHandler - SSO redirecting to {0}", uri.ToString());
										this.RedirectUsingSSOFBA(secureNameValueCollection, uri, response, owaServerVersion.Major);
										response.End();
									}
									else
									{
										AspNetHelper.TransferToErrorPage(httpContext, ErrorFE.FEErrorCodes.NoFbaSSL);
									}
								}
								else
								{
									ExTraceGlobals.VerboseTracer.TraceDebug<string>((long)this.GetHashCode(), "FbaFormPostProxyRequestHandler - redirecting to {0}", uri2.ToString());
									base.PfdTracer.TraceRedirect("FbaAuth", uri2.ToString());
									response.Redirect(FbaFormPostProxyRequestHandler.CheckRedirectUrlForNewline(uri2.ToString()));
								}
							}
						}
					}
					return;
				}
			}
			try
			{
				FbaModule.SetCadataCookies(base.HttpApplication);
			}
			catch (MissingSslCertificateException)
			{
				AspNetHelper.TransferToErrorPage(httpContext, ErrorFE.FEErrorCodes.NoFbaSSL);
			}
			ExTraceGlobals.VerboseTracer.TraceDebug<string>((long)this.GetHashCode(), "FbaFormPostProxyRequestHandler - redirecting to {0}", text);
			base.PfdTracer.TraceRedirect("FbaAuth", text);
			response.Redirect(FbaFormPostProxyRequestHandler.CheckRedirectUrlForNewline(text), false);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0002A3B0 File Offset: 0x000285B0
		private void RedirectUsingSSOFBA(SecureNameValueCollection collection, Uri redirectUrl, HttpResponse response, int majorCasVersion)
		{
			response.StatusCode = 200;
			response.Status = "200 - OK";
			response.BufferOutput = false;
			response.CacheControl = "no-cache";
			response.Cache.SetNoStore();
			HttpCookie httpCookie = new HttpCookie("PBack");
			httpCookie.Value = "1";
			response.Cookies.Add(httpCookie);
			response.Headers.Add("X-OWA-FEError", ErrorFE.FEErrorCodes.CasRedirect.ToString());
			using (SecureHttpBuffer secureHttpBuffer = new SecureHttpBuffer(1000, response))
			{
				this.CreateHtmlForSsoFba(secureHttpBuffer, collection, redirectUrl, majorCasVersion);
				secureHttpBuffer.Flush();
				response.End();
			}
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0002A46C File Offset: 0x0002866C
		private void CreateHtmlForSsoFba(SecureHttpBuffer buffer, SecureNameValueCollection collection, Uri redirectUrl, int majorCasVersion)
		{
			string noScriptHtml = FbaFormPostProxyRequestHandler.GetNoScriptHtml();
			buffer.CopyAtCurrentPosition("<html><noscript>");
			buffer.CopyAtCurrentPosition(noScriptHtml.ToString());
			buffer.CopyAtCurrentPosition("</noscript><head><title>Continue</title><script type='text/javascript'>function OnBack(){}function DoSubmit(){var subt=false;if(!subt){subt=true;document.logonForm.submit();}}</script></head><body onload='javascript:DoSubmit();'>");
			this.CreateFormHtmlForSsoFba(buffer, collection, redirectUrl, majorCasVersion);
			buffer.CopyAtCurrentPosition("</body></html>");
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0002A4B8 File Offset: 0x000286B8
		private void CreateFormHtmlForSsoFba(SecureHttpBuffer buffer, SecureNameValueCollection collection, Uri redirectUrl, int majorCasVersion)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(redirectUrl.Scheme);
			stringBuilder.Append(Uri.SchemeDelimiter);
			stringBuilder.Append(redirectUrl.Authority);
			stringBuilder.Append(OwaUrl.AuthDll.ImplicitUrl);
			buffer.CopyAtCurrentPosition("<form name='logonForm' id='logonForm' action='");
			buffer.CopyAtCurrentPosition(stringBuilder.ToString());
			buffer.CopyAtCurrentPosition("' method='post' target='_top'>");
			this.CreateInputHtmlCollection(collection, buffer, redirectUrl, majorCasVersion);
			buffer.CopyAtCurrentPosition("</form>");
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0002A53C File Offset: 0x0002873C
		private void CreateInputHtmlCollection(SecureNameValueCollection collection, SecureHttpBuffer buffer, Uri redirectUrl, int majorCasVersion)
		{
			foreach (string text in collection)
			{
				buffer.CopyAtCurrentPosition("<input type='hidden' name='");
				buffer.CopyAtCurrentPosition(text);
				buffer.CopyAtCurrentPosition("' value='");
				if (text == "password")
				{
					SecureString securePassword;
					collection.TryGetSecureValue(text, out securePassword);
					using (SecureArray<char> secureArray = securePassword.TransformToSecureCharArray(new CharTransformDelegate(FbaFormPostProxyRequestHandler.EncodeForSingleQuotedAttribute)))
					{
						buffer.CopyAtCurrentPosition(secureArray);
						goto IL_14B;
					}
					goto IL_72;
				}
				goto IL_72;
				IL_14B:
				buffer.CopyAtCurrentPosition("'>");
				continue;
				IL_72:
				string text2;
				if (!(text == "destination"))
				{
					collection.TryGetUnsecureValue(text, out text2);
					buffer.CopyAtCurrentPosition(EncodingUtilities.HtmlEncode(text2));
					goto IL_14B;
				}
				collection.TryGetUnsecureValue(text, out text2);
				Uri uri;
				if (!Uri.TryCreate(text2, UriKind.Absolute, out uri))
				{
					throw new HttpException(400, "destination value is not valid");
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(redirectUrl.Scheme);
				stringBuilder.Append(Uri.SchemeDelimiter);
				stringBuilder.Append(redirectUrl.Authority);
				if (FbaFormPostProxyRequestHandler.IsOwaUrl(uri, OwaUrl.AuthPost, true))
				{
					stringBuilder.Append(OwaUrl.ApplicationRoot.ImplicitUrl);
				}
				else if (string.IsNullOrEmpty(this.explicitLogonUser))
				{
					stringBuilder.Append(redirectUrl.PathAndQuery);
				}
				else
				{
					stringBuilder.Append(uri.PathAndQuery);
				}
				buffer.CopyAtCurrentPosition(stringBuilder.ToString());
				goto IL_14B;
			}
		}

		// Token: 0x04000486 RID: 1158
		private const string PostBackFFCookieName = "PBack";

		// Token: 0x04000487 RID: 1159
		private const string DisableSSORedirectsAppSetting = "DisableSSORedirects";

		// Token: 0x04000488 RID: 1160
		private static readonly char[] EncodedAmpersand = EncodingUtilities.HtmlEncode("&").ToCharArray();

		// Token: 0x04000489 RID: 1161
		private static readonly char[] EncodedApostrophe = EncodingUtilities.HtmlEncode("'").ToCharArray();

		// Token: 0x0400048A RID: 1162
		private static bool? disableSSORedirects = null;

		// Token: 0x0400048B RID: 1163
		private string explicitLogonUser;

		// Token: 0x020000B8 RID: 184
		private enum LegacyRedirectFailureCause
		{
			// Token: 0x0400048D RID: 1165
			None,
			// Token: 0x0400048E RID: 1166
			NoCasFound
		}
	}
}
