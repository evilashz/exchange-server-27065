using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Web;
using System.Web.Configuration;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data.ApplicationLogic.Extension;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;
using Microsoft.Exchange.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Online.BOX.Shell;
using Microsoft.Online.BOX.UI.Shell;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000247 RID: 583
	public abstract class NavBarClientBase
	{
		// Token: 0x06002859 RID: 10329 RVA: 0x0007E724 File Offset: 0x0007C924
		public NavBarClientBase(bool showAdminFeature, bool fallbackMode, bool forceReload)
		{
			this.showAdminFeature = showAdminFeature;
			this.fallbackMode = fallbackMode;
			this.forceReload = forceReload;
			string authNInfo = NavBarClientBase.GetAuthNInfo("RPSOrgIdPUID");
			this.userPuid = (string.IsNullOrEmpty(authNInfo) ? NavBarClientBase.GetAuthNInfo("RPSPUID") : authNInfo);
			this.userPrincipalName = NavBarClientBase.GetAuthNInfo("RPSMemberName");
			this.cultureName = CultureInfo.CurrentCulture.IetfLanguageTag;
			this.rbacPrincipal = RbacPrincipal.Current;
			if (this.rbacPrincipal.IsInRole("Impersonated") || this.rbacPrincipal.IsInRole("DelegatedAdmin") || this.rbacPrincipal.IsInRole("ByoidAdmin"))
			{
				this.fallbackMode = true;
			}
			this.isGallatin = this.rbacPrincipal.IsInRole("IsGallatin");
			ExTraceGlobals.WebServiceTracer.TraceInformation(0, 0L, "Create shell client for user: {0}, UserPuid: {1}, locale: {2}, forceReload: {3}", new object[]
			{
				this.userPrincipalName,
				this.userPuid,
				this.cultureName,
				forceReload
			});
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x0007E830 File Offset: 0x0007CA30
		private static string GetAuthNInfo(string key)
		{
			string text = HttpContext.Current.Request.Headers[key];
			if (string.IsNullOrEmpty(text))
			{
				text = (HttpContext.Current.Items[key] as string);
			}
			return text;
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x0007E874 File Offset: 0x0007CA74
		public static NavBarClientBase Create(bool showAdminFeature, bool fallbackMode, bool forceReload = false)
		{
			NavBarClientBase.EnsureInitialized();
			NavBarClientBase result = null;
			if (NavBarClientBase.loadConfigSuccess)
			{
				if (NavBarClientBase.IsGeminiShellEnabled)
				{
					result = new ShellClient(showAdminFeature, fallbackMode, forceReload);
				}
				else
				{
					result = new NavBarClient(showAdminFeature, fallbackMode, forceReload);
				}
			}
			else
			{
				ExTraceGlobals.WebServiceTracer.TraceInformation<string>(0, 0L, "NavBarClientBase return null for user '{0}' due to loadConfigSuccess is false.", RbacPrincipal.Current.NameForEventLog);
			}
			return result;
		}

		// Token: 0x17001C57 RID: 7255
		// (get) Token: 0x0600285C RID: 10332 RVA: 0x0007E8C9 File Offset: 0x0007CAC9
		protected virtual bool UseNavBarPackCache
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C58 RID: 7256
		// (get) Token: 0x0600285D RID: 10333 RVA: 0x0007E8CC File Offset: 0x0007CACC
		protected virtual string Office365Copyright
		{
			get
			{
				return Strings.Office365Copyright;
			}
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x0007E8D8 File Offset: 0x0007CAD8
		public void PrepareNavBarPack()
		{
			if (this.userPuid != null && !this.fallbackMode)
			{
				if (this.forceReload || !this.UseNavBarPackCache)
				{
					ShellServiceClient shellServiceClient = new ShellServiceClient(NavBarClientBase.endPointConfiguration);
					shellServiceClient.ClientCredentials.ClientCertificate.Certificate = NavBarClientBase.certificate;
					NavBarInfoRequest navBarInfoRequest = this.CreateRequest();
					ExTraceGlobals.WebServiceTracer.TraceInformation(0, 0L, string.Format("NavBarInfoRequest created. TrackingGuid: {0}, WorkloadID: {1}", navBarInfoRequest.TrackingGuid, navBarInfoRequest.WorkloadId.ToString()));
					this.BeginGetNavBarPack(shellServiceClient, navBarInfoRequest);
					return;
				}
				this.navBarPack = this.TryGetNavBarPackFromCache();
			}
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x0007E974 File Offset: 0x0007CB74
		public NavBarPack GetNavBarPack()
		{
			if (this.userPuid != null && !this.fallbackMode && (this.forceReload || !this.UseNavBarPackCache))
			{
				this.navBarPack = this.EndGetNavBarPack();
			}
			bool isMockObject = false;
			if (this.navBarPack == null && !this.forceReload)
			{
				this.navBarPack = this.GetMockNavBarPack();
				NavBarData navBarData = this.navBarPack.NavBarData;
				navBarData.UserDisplayName = this.rbacPrincipal.Name;
				if (NavigationUtil.ShouldRenderLogoutLink(this.rbacPrincipal))
				{
					navBarData.SignOutLink = MockNavBar.CreateLink("signout_O365L", Strings.SignOff, "logoff.aspx?src=exch", null);
				}
				if (!this.isGallatin)
				{
					navBarData.FooterCopyrightText = this.Office365Copyright;
					navBarData.LegalLink = MockNavBar.CreateLink("legal_O365L", Strings.Legal, "http://g.microsoftonline.com/1BX10en/20", "_blank");
					navBarData.CommunityLink = MockNavBar.CreateLink("community_O365L", Strings.Community, "http://g.microsoftonline.com/1BX10en/142", "_blank");
					navBarData.PrivacyLink = MockNavBar.CreateLink("privacy_O365L", Strings.Privacy, "http://g.microsoftonline.com/1BX10en/11", "_blank");
				}
				navBarData.HelpLink = MockNavBar.CreateLink("help_O365L", Strings.Help, null, null);
				isMockObject = true;
				if (this.fallbackMode)
				{
					this.navBarPack.IsFresh = true;
				}
			}
			if (this.navBarPack != null)
			{
				this.UpdateAppsLinks();
				this.AddCustomSubLinks(isMockObject);
			}
			return this.navBarPack;
		}

		// Token: 0x06002860 RID: 10336
		protected abstract NavBarInfoRequest CreateRequest();

		// Token: 0x06002861 RID: 10337 RVA: 0x0007EAE8 File Offset: 0x0007CCE8
		protected virtual void BeginGetNavBarPack(ShellServiceClient client, NavBarInfoRequest request)
		{
			this.GetNavBarInfoAndHandleException(client, request);
		}

		// Token: 0x06002862 RID: 10338
		protected abstract NavBarPack EndGetNavBarPack();

		// Token: 0x06002863 RID: 10339 RVA: 0x0007EAF4 File Offset: 0x0007CCF4
		protected void GetNavBarInfoAndHandleException(ShellServiceClient client, NavBarInfoRequest request)
		{
			try
			{
				this.CallShellService(client, request);
			}
			catch (EndpointNotFoundException ex)
			{
				this.shellServiceException = ex;
			}
			catch (ActionNotSupportedException ex2)
			{
				this.shellServiceException = ex2;
			}
			catch (FaultException ex3)
			{
				this.shellServiceException = ex3;
			}
			catch (MessageSecurityException ex4)
			{
				this.shellServiceException = ex4;
			}
			finally
			{
				client.Close();
			}
		}

		// Token: 0x06002864 RID: 10340
		protected abstract void CallShellService(ShellServiceClient client, NavBarInfoRequest request);

		// Token: 0x06002865 RID: 10341 RVA: 0x0007EB7C File Offset: 0x0007CD7C
		protected virtual NavBarPack TryGetNavBarPackFromCache()
		{
			return null;
		}

		// Token: 0x06002866 RID: 10342
		protected abstract NavBarPack GetMockNavBarPack();

		// Token: 0x06002867 RID: 10343 RVA: 0x0007EB80 File Offset: 0x0007CD80
		protected bool LogException()
		{
			if (this.shellServiceException != null)
			{
				NavBarDiagnosticsDetails navBarDiagnosticsDetails = (NavBarDiagnosticsDetails)HttpContext.Current.Cache["NavBarDiagnostics.Details"];
				if (navBarDiagnosticsDetails != null)
				{
					navBarDiagnosticsDetails.Exception = this.shellServiceException;
				}
				string nameForEventLog = this.rbacPrincipal.NameForEventLog;
				string text = this.shellServiceException.ToString();
				ExTraceGlobals.WebServiceTracer.TraceError<string, string>(0, 0L, "NavBarHelper catch exception when try to connect to Shell Service. User: {0}. Exception: {1}.", nameForEventLog, text);
				EcpEventLogConstants.Tuple_Office365ShellServiceFailed.LogPeriodicEvent(EcpEventLogExtensions.GetPeriodicKeyPerUser(), new object[]
				{
					EcpEventLogExtensions.GetUserNameToLog(),
					text
				});
				return true;
			}
			return false;
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x0007EC10 File Offset: 0x0007CE10
		protected static NavBarPack GetNavBarPackFromInfo(NavBarInfo info, string culture)
		{
			return new NavBarPack
			{
				NavBarData = info.NavBarDataJson.JsonDeserialize(null),
				Culture = culture,
				SharedCSSUrl = info.SharedCSSUrl,
				SharedJSUrl = info.SharedJSUrl,
				Version = info.Version
			};
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x0007EC64 File Offset: 0x0007CE64
		private void UpdateAppsLinks()
		{
			if (this.navBarPack.NavBarData.AppsLinks == null)
			{
				return;
			}
			List<NavBarLinkData> list = new List<NavBarLinkData>(this.navBarPack.NavBarData.AppsLinks.Length);
			NavBarLinkData[] appsLinks = this.navBarPack.NavBarData.AppsLinks;
			int i = 0;
			while (i < appsLinks.Length)
			{
				NavBarLinkData navBarLinkData = appsLinks[i];
				if ("ShellMarketplace".Equals(navBarLinkData.Id, StringComparison.Ordinal))
				{
					ExchangeRunspaceConfiguration rbacConfiguration = RbacPrincipal.Current.RbacConfiguration;
					if (rbacConfiguration.HasRoleOfType(RoleType.MyMarketplaceApps) && OfficeStoreAvailableQueryProcessor.IsOfficeStoreAvailable)
					{
						navBarLinkData.Url = ExtensionData.GetClientExtensionMarketplaceUrl(CultureInfo.CurrentCulture.LCID, rbacConfiguration.HasRoleOfType(RoleType.MyReadWriteMailboxApps), this.showAdminFeature ? ExtensionUtility.UrlEncodedOfficeCallBackUrlForOrg : ExtensionUtility.UrlEncodedOfficeCallBackUrl, ExtensionUtility.DeploymentId, null);
						navBarLinkData.TargetWindow = "_blank";
						goto IL_DA;
					}
				}
				else
				{
					if ("ShellOfficeDotCom".Equals(navBarLinkData.Id, StringComparison.Ordinal))
					{
						navBarLinkData.TargetWindow = "_blank";
						goto IL_DA;
					}
					goto IL_DA;
				}
				IL_E1:
				i++;
				continue;
				IL_DA:
				list.Add(navBarLinkData);
				goto IL_E1;
			}
			this.navBarPack.NavBarData.AppsLinks = list.ToArray();
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x0007ED78 File Offset: 0x0007CF78
		private void AddCustomSubLinks(bool isMockObject)
		{
			NavBarLinkData[] currentWorkloadUserSubLinks = null;
			if (this.showAdminFeature)
			{
				this.navBarPack.FeatureSet = "myorg";
				this.navBarPack.NavBarData.CurrentMainLinkElementID = "ShellAdmin";
				if (this.rbacPrincipal.IsInRole("UserOptions+OrgMgmControlPanel"))
				{
					currentWorkloadUserSubLinks = new NavBarLinkData[]
					{
						MockNavBar.CreateLink("eso_O365L", Strings.EntryOnBehalfOf, null, null)
					};
				}
			}
			else
			{
				this.navBarPack.FeatureSet = "myself";
				this.navBarPack.NavBarData.CurrentMainLinkElementID = "ShellOutlook";
				if (isMockObject && NavigationUtil.ShouldRenderOwaLink(this.rbacPrincipal, this.showAdminFeature))
				{
					NavBarLinkData[] workloadLinks = new NavBarLinkData[]
					{
						MockNavBar.CreateLink("ShellOutlook", Strings.NavBarMail, EcpUrl.OwaVDir, null)
					};
					this.navBarPack.NavBarData.WorkloadLinks = workloadLinks;
				}
			}
			this.navBarPack.NavBarData.CurrentWorkloadUserSubLinks = currentWorkloadUserSubLinks;
			List<NavBarLinkData> list = new List<NavBarLinkData>();
			string flightName = this.navBarPack.NavBarData.FlightName;
			if (!string.IsNullOrEmpty(flightName) && flightName.Contains("GeminiShellUX"))
			{
				this.navBarPack.HelpParameters = this.navBarPack.NavBarData.HelpLink.Url;
				this.navBarPack.NavBarData.HelpLink.Id = (this.showAdminFeature ? "openHelp_O365L" : "openOptionHelp_O365L");
				this.navBarPack.NavBarData.HelpLink.Url = HelpUtil.BuildEhcHref(this.showAdminFeature ? EACHelpId.Default.ToString() : OptionsHelpId.OwaOptionsDefault.ToString());
			}
			else if (this.showAdminFeature)
			{
				list.Add(MockNavBar.CreateLink("openHelp_O365L", Strings.Help, HelpUtil.BuildEhcHref(EACHelpId.Default.ToString()), null));
			}
			else
			{
				list.Add(MockNavBar.CreateLink("openOptionHelp_O365L", Strings.Help, HelpUtil.BuildEhcHref(OptionsHelpId.OwaOptionsDefault.ToString()), null));
			}
			HttpCookie httpCookie = HttpContext.Current.Request.Cookies["msExchEcpFvaHelp"];
			bool flag = httpCookie != null && httpCookie.Value != null && httpCookie.Value.StartsWith("0");
			list.Add(MockNavBar.CreateLink("toggleFVA_O365L", flag ? ClientStrings.EnableFVA : ClientStrings.DisableFVA, null, null));
			if (NavBarClientBase.showPerfConsole)
			{
				list.Add(MockNavBar.CreateLink("perfConsole_O365L", Strings.PerformanceConsole, null, null));
			}
			if (this.showAdminFeature && EacFlightUtility.GetSnapshotForCurrentUser().Eac.CmdletLogging.Enabled)
			{
				list.Add(MockNavBar.CreateLink("cmdletLogging_O365L", Strings.CmdLogButtonText, null, null));
			}
			this.navBarPack.NavBarData.CurrentWorkloadHelpSubLinks = list.ToArray();
			if (this.isGallatin && this.navBarPack.NavBarData.FooterICPLink != null)
			{
				if (!string.IsNullOrEmpty(this.navBarPack.NavBarData.FooterICPLink.Text))
				{
					this.navBarPack.NavBarData.FooterICPLink.Text = this.navBarPack.NavBarData.FooterICPLink.Text.Replace("-10", "-22");
				}
				if (!string.IsNullOrEmpty(this.navBarPack.NavBarData.FooterICPLink.Title))
				{
					this.navBarPack.NavBarData.FooterICPLink.Title = this.navBarPack.NavBarData.FooterICPLink.Title.Replace("-10", "-22");
				}
			}
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x0007F124 File Offset: 0x0007D324
		private static void EnsureInitialized()
		{
			if (Util.IsMicrosoftHostedOnly && NavBarClientBase.endPointConfiguration == null)
			{
				NavBarClientBase.endPointConfiguration = ConfigurationManager.AppSettings["ShellServiceEndPointConfiguration"];
				NavBarClientBase.showPerfConsole = StringComparer.OrdinalIgnoreCase.Equals("true", WebConfigurationManager.AppSettings["ShowPerformanceConsole"]);
				NavBarClientBase.certificateSubject = ConfigurationManager.AppSettings["MsOnlineShellService_CertSubject"];
				NavBarClientBase.certificateThumbprint = ConfigurationManager.AppSettings["MsOnlineShellService_CertThumbprint"];
				string text = string.Format("{0}{1}/scripts/o365shared.js", EcpUrl.EcpVDirForStaticResource, Util.ApplicationVersion);
				string theme = ((PagesSection)ConfigurationManager.GetSection("system.web/pages")).Theme;
				string text2 = string.Format("{0}{1}/themes/{2}/o365shared.css", EcpUrl.EcpVDirForStaticResource, Util.ApplicationVersion, theme);
				string text3 = string.Format("{0}{1}/themes/{2}/o365shared-rtl.css", EcpUrl.EcpVDirForStaticResource, Util.ApplicationVersion, theme);
				string text4 = string.Format("{0}{1}/themes/{2}/o365shared.png", EcpUrl.EcpVDirForStaticResource, Util.ApplicationVersion, theme);
				string text5 = string.Format("{0}{1}/scripts/CoreShellBundle.js", EcpUrl.EcpVDirForStaticResource, Util.ApplicationVersion);
				string text6 = string.Format("{0}{1}/themes/{2}/O365ShellCore.css", EcpUrl.EcpVDirForStaticResource, Util.ApplicationVersion, theme);
				string text7 = string.Format("{0}{1}/themes/{2}/O365ShellCore-rtl.css", EcpUrl.EcpVDirForStaticResource, Util.ApplicationVersion, theme);
				string text8 = string.Format("{0}{1}/scripts/O365ShellPlusTestExtension.js", EcpUrl.EcpVDirForStaticResource, Util.ApplicationVersion);
				string text9 = string.Format("{0}{1}/themes/{2}/O365ShellPlus.css", EcpUrl.EcpVDirForStaticResource, Util.ApplicationVersion, theme);
				string text10 = string.Format("{0}{1}/themes/{2}/O365ShellPlus-rtl.css", EcpUrl.EcpVDirForStaticResource, Util.ApplicationVersion, theme);
				string text11 = ConfigurationManager.AppSettings["O365Url"];
				MockNavBar.Initialize(text11, text, text2, text3, text4, text5, text6, text7, text8, text9, text10);
				if (NavBarClientBase.endPointConfiguration != null && !string.IsNullOrEmpty(NavBarClientBase.certificateThumbprint))
				{
					NavBarClientBase.certificate = TlsCertificateInfo.FindCertByThumbprint(NavBarClientBase.certificateThumbprint);
					NavBarClientBase.loadConfigSuccess = (NavBarClientBase.certificate != null);
				}
				ExTraceGlobals.WebServiceTracer.TraceInformation(0, 0L, "NavBarHelper load config success: {0}. EndPointConfiguration: '{1}'; ShellJs: '{2}'; Certicate: '{3}' {4}.", new object[]
				{
					NavBarClientBase.loadConfigSuccess,
					NavBarClientBase.endPointConfiguration,
					text,
					NavBarClientBase.certificateSubject,
					(NavBarClientBase.certificate != null) ? "found" : "not found"
				});
				if (!NavBarClientBase.loadConfigSuccess)
				{
					EcpEventLogConstants.Tuple_Office365NavBarLoadConfigFailed.LogEvent(new object[]
					{
						NavBarClientBase.loadConfigSuccess,
						NavBarClientBase.endPointConfiguration,
						text,
						NavBarClientBase.certificateSubject
					});
				}
			}
		}

		// Token: 0x17001C59 RID: 7257
		// (get) Token: 0x0600286C RID: 10348 RVA: 0x0007F38C File Offset: 0x0007D58C
		internal static bool IsGeminiShellEnabled
		{
			get
			{
				VariantConfigurationSnapshot snapshotForCurrentUser = EacFlightUtility.GetSnapshotForCurrentUser();
				return snapshotForCurrentUser != null && snapshotForCurrentUser.Eac.GeminiShell.Enabled;
			}
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x0007F3BC File Offset: 0x0007D5BC
		internal static void RenderConfigInformation(TextWriter output)
		{
			NavBarClientBase.EnsureInitialized();
			DiagnosticsPage.Write(output, "End point configuration:", NavBarClientBase.endPointConfiguration, null);
			DiagnosticsPage.Write(output, "Certificate:", NavBarClientBase.certificateSubject, null);
			DiagnosticsPage.Write(output, "Certificate loaded:", NavBarClientBase.certificate != null, null);
			DiagnosticsPage.Write(output, "Configuration loaded success:", NavBarClientBase.loadConfigSuccess, null);
		}

		// Token: 0x0400204C RID: 8268
		internal const string FlightCookie = "flight";

		// Token: 0x0400204D RID: 8269
		internal const string FlightGeminiShellUx = "geminishellux";

		// Token: 0x0400204E RID: 8270
		internal const string FvaHelpCookieName = "msExchEcpFvaHelp";

		// Token: 0x0400204F RID: 8271
		private static string endPointConfiguration;

		// Token: 0x04002050 RID: 8272
		private static string certificateSubject;

		// Token: 0x04002051 RID: 8273
		private static string certificateThumbprint;

		// Token: 0x04002052 RID: 8274
		private static bool showPerfConsole;

		// Token: 0x04002053 RID: 8275
		private static bool loadConfigSuccess;

		// Token: 0x04002054 RID: 8276
		private static X509Certificate2 certificate;

		// Token: 0x04002055 RID: 8277
		protected string userPuid;

		// Token: 0x04002056 RID: 8278
		protected string userPrincipalName;

		// Token: 0x04002057 RID: 8279
		protected string cultureName;

		// Token: 0x04002058 RID: 8280
		protected bool showAdminFeature;

		// Token: 0x04002059 RID: 8281
		protected bool fallbackMode;

		// Token: 0x0400205A RID: 8282
		protected bool forceReload;

		// Token: 0x0400205B RID: 8283
		protected bool isGallatin;

		// Token: 0x0400205C RID: 8284
		internal RbacPrincipal rbacPrincipal;

		// Token: 0x0400205D RID: 8285
		private NavBarPack navBarPack;

		// Token: 0x0400205E RID: 8286
		private Exception shellServiceException;
	}
}
