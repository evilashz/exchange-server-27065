using System;
using System.Configuration;
using System.Globalization;
using System.Management.Automation;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Win32;

namespace Microsoft.Exchange.CommonHelpProvider
{
	// Token: 0x02000002 RID: 2
	public static class HelpProvider
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static void Initialize(HelpProvider.HelpAppName appName)
		{
			HelpProvider.LoadBaseUrl(appName);
			HelpProvider.initializedViaCmdlet = false;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020DE File Offset: 0x000002DE
		public static void InitializeViaCmdlet(HelpProvider.HelpAppName appName, RunspaceServerSettingsPresentationObject runspaceServerSettings, MonadConnectionInfo monadConnectionInfo)
		{
			HelpProvider.LoadBaseUrlViaCmdlet(appName, runspaceServerSettings, monadConnectionInfo);
			HelpProvider.initializedViaCmdlet = true;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020F0 File Offset: 0x000002F0
		public static Uri ConstructHelpRenderingUrl()
		{
			return HelpProvider.UrlConstructHelper("default", new string[]
			{
				HelpProvider.GetLoginInfo()
			});
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002118 File Offset: 0x00000318
		public static Uri ConstructHelpRenderingUrl(string helpAttributeId)
		{
			string contentId = HelpProvider.GetAppQualifier() + helpAttributeId;
			return HelpProvider.UrlConstructHelper(contentId, new string[]
			{
				HelpProvider.GetLoginInfo()
			});
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002148 File Offset: 0x00000348
		public static Uri ConstructHelpRenderingUrl(ErrorRecord errorRecord)
		{
			string text = "e=" + "ms.exch.err." + errorRecord.Exception.GetType();
			return HelpProvider.UrlConstructHelper("ms.exch.err.default", new string[]
			{
				text,
				HelpProvider.GetLoginInfo()
			});
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002190 File Offset: 0x00000390
		public static Uri ConstructHelpRenderingUrl(int lcid, HelpProvider.OwaHelpExperience owaExp, string helpId, HelpProvider.RenderingMode mode, string optionalServerParams, OrganizationProperties organizationProperties)
		{
			Uri owaBaseUrl = HelpProvider.GetOwaBaseUrl(owaExp);
			string owaAppQualifier = HelpProvider.GetOwaAppQualifier(owaExp);
			string text = optionalServerParams;
			if (!string.IsNullOrEmpty(text) && !text.StartsWith("&"))
			{
				text = "&" + text;
			}
			string text2 = HelpProvider.OwaLightNamespace;
			if (owaExp != HelpProvider.OwaHelpExperience.Light)
			{
				if (organizationProperties == null || string.IsNullOrEmpty(organizationProperties.ServicePlan))
				{
					text2 = HelpProvider.OwaPremiumNamespace;
				}
				else if (organizationProperties.ServicePlan.StartsWith(HelpProvider.OwaMsoProfessionalServicePlanPrefix, StringComparison.InvariantCultureIgnoreCase))
				{
					text2 = (HelpProvider.IsGallatin() ? HelpProvider.OwaMsoProfessionalGallatinNamespace : HelpProvider.OwaMsoProfessionalNamespace);
				}
				else
				{
					text2 = (HelpProvider.IsGallatin() ? HelpProvider.OwaMsoEnterpriseGallatinNamespace : HelpProvider.OwaMsoEnterpriseNamespace);
				}
			}
			else
			{
				text = string.Empty;
			}
			string text3 = string.Empty;
			if (helpId != null)
			{
				text3 = "&helpid=" + owaAppQualifier + (helpId.Equals(string.Empty) ? "{0}" : helpId);
			}
			string text4 = "15";
			int num = HelpProvider.applicationVersion.IndexOf('.');
			if (num > 0)
			{
				text4 = HelpProvider.applicationVersion.Substring(0, num);
			}
			string uriString = string.Format("{0}?p1={1}&clid={2}&ver={3}&v={4}&mode={5}{6}{7}", new object[]
			{
				owaBaseUrl.ToString(),
				text2,
				lcid.ToString(),
				text4,
				HelpProvider.applicationVersion,
				HelpProvider.officeRedirModes[(int)mode],
				text3,
				text
			});
			return new Uri(uriString);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000022E9 File Offset: 0x000004E9
		public static Uri ConstructHelpRenderingUrlWithQualifierHelpId(string appQualifier, string helpId)
		{
			if (string.IsNullOrEmpty(appQualifier))
			{
				return null;
			}
			if (string.IsNullOrEmpty(helpId))
			{
				return null;
			}
			return new Uri(string.Format(HelpProvider.GetBaseUrl().ToString(), appQualifier, helpId));
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002318 File Offset: 0x00000518
		public static Uri ConstructTenantEventUrl(string source, string eventId)
		{
			return HelpProvider.UrlConstructHelper("ms.exch.evt.default", new string[]
			{
				HelpProvider.GetEventParam(source, string.Empty, eventId),
				HelpProvider.GetLoginInfo()
			});
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002350 File Offset: 0x00000550
		public static Uri ConstructTenantEventUrl(string source, string category, string eventId)
		{
			return HelpProvider.UrlConstructHelper("ms.exch.evt.default", new string[]
			{
				HelpProvider.GetEventParam(source, category, eventId),
				HelpProvider.GetLoginInfo()
			});
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002384 File Offset: 0x00000584
		public static Uri ConstructTenantEventUrl(string source, string category, string eventId, string locale)
		{
			return HelpProvider.ConstructFinalHelpUrl(HelpProvider.baseUrl, "ms.exch.evt.default", locale, new string[]
			{
				HelpProvider.GetEventParam(source, category, eventId),
				HelpProvider.GetLoginInfo()
			});
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000023BC File Offset: 0x000005BC
		public static bool TryGetErrorAssistanceUrl(LocalizedException exception, out Uri helpUrl)
		{
			helpUrl = null;
			if (!HelpProvider.ShouldConstructHelpUrlForException(exception))
			{
				return false;
			}
			helpUrl = HelpProvider.UrlConstructHelper("ms.exch.err.default", new string[]
			{
				HelpProvider.GetErrorParam(exception),
				HelpProvider.GetLoginInfo()
			});
			return true;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023FC File Offset: 0x000005FC
		public static Uri GetBaseUrl()
		{
			return HelpProvider.baseUrl;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002403 File Offset: 0x00000603
		public static Uri GetManagementConsoleFeedbackUrl()
		{
			return HelpProvider.managementConsoleFeedbackUrl;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000240A File Offset: 0x0000060A
		public static Uri GetPrivacyStatementUrl()
		{
			return HelpProvider.privacyStatementUrl;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002414 File Offset: 0x00000614
		public static Uri GetPrivacyStatementUrl(OrganizationId organizationId)
		{
			if (organizationId == OrganizationId.ForestWideOrgId)
			{
				return HelpProvider.privacyStatementUrl;
			}
			Uri result = null;
			HelpProviderCache.Item item = HelpProviderCache.Instance.Get(organizationId);
			if (item != null)
			{
				result = item.PrivacyStatementUrl;
			}
			return result;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000244D File Offset: 0x0000064D
		public static Uri GetExchange2013PrivacyStatementUrl()
		{
			return HelpProvider.privacyStatementUrlExchange2013;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002454 File Offset: 0x00000654
		public static Uri GetMSOnlinePrivacyStatementUrl()
		{
			return HelpProvider.privacyStatementUrlMSOnline;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000245C File Offset: 0x0000065C
		public static bool? GetPrivacyLinkDisplayEnabled(OrganizationId organizationId)
		{
			if (organizationId == OrganizationId.ForestWideOrgId)
			{
				return new bool?(true);
			}
			bool? result = null;
			HelpProviderCache.Item item = HelpProviderCache.Instance.Get(organizationId);
			if (item != null)
			{
				result = item.PrivacyLinkDisplayEnabled;
			}
			return result;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000249C File Offset: 0x0000069C
		public static bool TryGetPrivacyStatementUrl(OrganizationId organizationId, out Uri orgPrivacyStatementUrl)
		{
			orgPrivacyStatementUrl = HelpProvider.GetPrivacyStatementUrl(organizationId);
			return orgPrivacyStatementUrl != null;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000024B3 File Offset: 0x000006B3
		public static Uri GetLiveAccountUrl()
		{
			return HelpProvider.windowsLiveAccountUrl;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024BA File Offset: 0x000006BA
		public static Uri GetCommunityUrl()
		{
			return HelpProvider.communityUrl;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024C4 File Offset: 0x000006C4
		public static Uri GetCommunityUrl(OrganizationId organizationId)
		{
			if (organizationId == OrganizationId.ForestWideOrgId)
			{
				return HelpProvider.communityUrl;
			}
			Uri result = null;
			HelpProviderCache.Item item = HelpProviderCache.Instance.Get(organizationId);
			if (item != null)
			{
				result = item.CommunityUrl;
			}
			return result;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024FD File Offset: 0x000006FD
		public static bool TryGetCommunityUrl(OrganizationId organizationId, out Uri orgCommunityUrl)
		{
			orgCommunityUrl = HelpProvider.GetCommunityUrl(organizationId);
			return orgCommunityUrl != null;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002514 File Offset: 0x00000714
		public static Uri GetProductFeedbackUrl()
		{
			if (HelpProvider.initializedViaCmdlet)
			{
				return HelpProvider.managementConsoleFeedbackUrl;
			}
			HelpProvider.HelpAppName helpAppName = HelpProvider.callingAppName;
			if (helpAppName == HelpProvider.HelpAppName.Ecp)
			{
				return HelpProvider.controlPanelFeedbackUrl;
			}
			throw new InvalidOperationException(string.Format("callingAppName {0} is not a valid HelpAppName enum. Check caller of Initialize how we get this value.", HelpProvider.callingAppName.ToString()));
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000255C File Offset: 0x0000075C
		public static Uri GetProductFeedbackUrl(HelpProvider.OwaHelpExperience owaExp)
		{
			switch (owaExp)
			{
			case HelpProvider.OwaHelpExperience.Light:
				return HelpProvider.owaLightFeedbackUrl;
			case HelpProvider.OwaHelpExperience.Premium:
				return HelpProvider.owaPremiumFeedbackUrl;
			default:
				throw new InvalidOperationException("owaExp is not a valid OwaHelpExperience enum. Check caller of Initialize how we get this value.");
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002594 File Offset: 0x00000794
		internal static Uri ConstructHelpRenderingUrl(ExchangeRunspaceConfiguration rbacConfiguration)
		{
			return HelpProvider.UrlConstructHelper("default", new string[]
			{
				HelpProvider.GetLoginInfo(),
				HelpProvider.GetServicePlanInfo(rbacConfiguration)
			});
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000025C4 File Offset: 0x000007C4
		internal static Uri ConstructHelpRenderingUrl(string helpAttributeId, ExchangeRunspaceConfiguration rbacConfiguration)
		{
			string contentId = HelpProvider.GetAppQualifier() + helpAttributeId;
			return HelpProvider.UrlConstructHelper(contentId, new string[]
			{
				HelpProvider.GetLoginInfo(),
				HelpProvider.GetServicePlanInfo(rbacConfiguration)
			});
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000025FC File Offset: 0x000007FC
		internal static Uri ConstructHelpRenderingUrl(ErrorRecord errorRecord, ExchangeRunspaceConfiguration rbacConfiguration)
		{
			string text = "e=" + "ms.exch.err." + errorRecord.Exception.GetType();
			return HelpProvider.UrlConstructHelper("ms.exch.err.default", new string[]
			{
				text,
				HelpProvider.GetLoginInfo(),
				HelpProvider.GetServicePlanInfo(rbacConfiguration)
			});
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000264C File Offset: 0x0000084C
		internal static bool TryGetErrorAssistanceUrl(LocalizedException exception, ExchangeRunspaceConfiguration rbacConfiguration, out Uri helpUrl)
		{
			helpUrl = null;
			if (!HelpProvider.ShouldConstructHelpUrlForException(exception))
			{
				return false;
			}
			helpUrl = HelpProvider.UrlConstructHelper("ms.exch.err.default", new string[]
			{
				HelpProvider.GetErrorParam(exception),
				HelpProvider.GetLoginInfo(),
				HelpProvider.GetServicePlanInfo(rbacConfiguration)
			});
			return true;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002698 File Offset: 0x00000898
		internal static bool TryGetErrorAssistanceUrl(LocalizedException exception, OrganizationProperties organizationProperties, out Uri helpUrl)
		{
			helpUrl = null;
			if (!HelpProvider.ShouldConstructHelpUrlForException(exception))
			{
				return false;
			}
			helpUrl = HelpProvider.UrlConstructHelper("ms.exch.err.default", new string[]
			{
				HelpProvider.GetErrorParam(exception),
				HelpProvider.GetLoginInfo(),
				(organizationProperties == null) ? string.Empty : HelpProvider.ConstructServicePlanInfo(organizationProperties.ServicePlan)
			});
			return true;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000026F0 File Offset: 0x000008F0
		internal static ExchangeAssistance GetExchangeAssistanceObjectFromAD(OrganizationId organizationId)
		{
			ExchangeAssistance result = null;
			try
			{
				IConfigurationSession configurationSession;
				ADObjectId adobjectId;
				if (organizationId == OrganizationId.ForestWideOrgId)
				{
					configurationSession = HelpProvider.GetOrganizationConfigurationSession(organizationId);
					adobjectId = configurationSession.GetOrgContainerId();
				}
				else
				{
					SharedConfiguration sharedConfiguration = SharedConfiguration.GetSharedConfiguration(organizationId);
					if (sharedConfiguration != null)
					{
						adobjectId = sharedConfiguration.SharedConfigurationCU.Id;
						configurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, sharedConfiguration.GetSharedConfigurationSessionSettings(), 955, "GetExchangeAssistanceObjectFromAD", "f:\\15.00.1497\\sources\\dev\\UA\\src\\HelpProvider\\HelpProvider.cs");
					}
					else
					{
						adobjectId = organizationId.ConfigurationUnit;
						configurationSession = HelpProvider.GetOrganizationConfigurationSession(organizationId);
					}
				}
				ADObjectId childId = adobjectId.GetChildId("ExchangeAssistance").GetChildId(HelpProvider.CurrentVersionExchangeAssistanceContainerName);
				result = configurationSession.Read<ExchangeAssistance>(childId);
			}
			catch (ADTransientException ex)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "ADTransient Exception in LoadBaseURL: {0}", ex.Message);
			}
			catch (ADOperationException ex2)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "ADOperationException in LoadBaseURL: {0}", ex2.Message);
			}
			return result;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000027E0 File Offset: 0x000009E0
		internal static void LoadSetupBaseUrlFromRegistry()
		{
			string baseUrlFromRegistry = HelpProvider.GetBaseUrlFromRegistry(HelpProvider.baseSetupUrlFromRegistryPath, "baseSetupHelpUrl");
			string arg = string.IsNullOrEmpty(baseUrlFromRegistry) ? "http://technet.microsoft.com/library(EXCHG.150)" : baseUrlFromRegistry;
			HelpProvider.baseUrl = new Uri(string.Format("{0}/{{0}}{{1}}.aspx", arg));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002824 File Offset: 0x00000A24
		internal static void LoadBpaBaseUrlFromRegistry()
		{
			string baseUrlFromRegistry = HelpProvider.GetBaseUrlFromRegistry(HelpProvider.baseBpaUrlFromRegistryPath, "baseBpaHelpUrl");
			string uriString = string.IsNullOrEmpty(baseUrlFromRegistry) ? "http://technet.microsoft.com/library(EXCHG.150)" : baseUrlFromRegistry;
			HelpProvider.baseUrl = Utilities.NormalizeUrl(new Uri(uriString));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002862 File Offset: 0x00000A62
		internal static void LoadComplianceBaseUrl()
		{
			HelpProvider.baseUrl = Utilities.NormalizeUrl(new Uri("http://technet.microsoft.com/library"));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002878 File Offset: 0x00000A78
		internal static string GetBaseUrlFromRegistry(string localMachinePath, string keyName)
		{
			if (string.IsNullOrEmpty(localMachinePath) || string.IsNullOrEmpty(keyName))
			{
				return null;
			}
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(localMachinePath))
				{
					if (registryKey != null)
					{
						return (string)registryKey.GetValue(keyName);
					}
				}
			}
			catch (Exception)
			{
			}
			return null;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000028E8 File Offset: 0x00000AE8
		private static IConfigurationSession GetOrganizationConfigurationSession(OrganizationId organizationId)
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), organizationId, organizationId, false), 1064, "GetOrganizationConfigurationSession", "f:\\15.00.1497\\sources\\dev\\UA\\src\\HelpProvider\\HelpProvider.cs");
			tenantOrTopologyConfigurationSession.SessionSettings.IsSharedConfigChecked = true;
			return tenantOrTopologyConfigurationSession;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000292C File Offset: 0x00000B2C
		private static string GetAppQualifier()
		{
			switch (HelpProvider.callingAppName)
			{
			case HelpProvider.HelpAppName.Ecp:
				return "ms.exch.eac.";
			case HelpProvider.HelpAppName.Toolbox:
				return "ms.exch.toolbox.";
			case HelpProvider.HelpAppName.TenantMonitoring:
				return "ms.exch.evt.";
			case HelpProvider.HelpAppName.Eap:
				return "ms.exch.err.";
			case HelpProvider.HelpAppName.Bpa:
				return "ms.o365.bpa.";
			case HelpProvider.HelpAppName.Compliance:
				return "ms.o365.cc.";
			}
			throw new InvalidOperationException(string.Format("callingAppName {0} is not a valid HelpAppName enum. Check caller of Initialize how we get this value.", HelpProvider.callingAppName.ToString()));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000029AC File Offset: 0x00000BAC
		private static string GetOwaAppQualifier(HelpProvider.OwaHelpExperience appExp)
		{
			switch (appExp)
			{
			case HelpProvider.OwaHelpExperience.Light:
				return "ms.exch.owal.";
			case HelpProvider.OwaHelpExperience.Premium:
				return "ms.exch.owap.";
			case HelpProvider.OwaHelpExperience.Options:
				return "ms.exch.owao.";
			default:
				throw new ArgumentOutOfRangeException("appExp");
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000029EB File Offset: 0x00000BEB
		private static Uri GetOwaBaseUrl(HelpProvider.OwaHelpExperience appExp)
		{
			if (appExp != HelpProvider.OwaHelpExperience.Light)
			{
				return HelpProvider.baseOwaPremiumUrl;
			}
			return HelpProvider.baseOwaLightUrl;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000029FC File Offset: 0x00000BFC
		private static string GetUserHelpLanguage()
		{
			CultureInfo userCulture = HelpProvider.GetUserCulture();
			return userCulture.Name;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002A15 File Offset: 0x00000C15
		private static CultureInfo GetUserCulture()
		{
			return Thread.CurrentThread.CurrentUICulture;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002A24 File Offset: 0x00000C24
		private static string GetLoginInfo()
		{
			string result = string.Empty;
			if (!HelpProvider.initializedViaCmdlet)
			{
				result = "l=" + (Utilities.IsMicrosoftHostedOnly() ? "1" : "0");
			}
			return result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002A5D File Offset: 0x00000C5D
		private static string GetServicePlanInfo(ExchangeRunspaceConfiguration rbacConfiguration)
		{
			return HelpProvider.ConstructServicePlanInfo(Utilities.GetServicePlanName(rbacConfiguration));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002A6A File Offset: 0x00000C6A
		private static string ConstructServicePlanInfo(string servicePlan)
		{
			if (string.IsNullOrEmpty(servicePlan))
			{
				return string.Empty;
			}
			return "s=" + servicePlan;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002A88 File Offset: 0x00000C88
		private static void LoadBaseUrlViaADSession(HelpProvider.HelpAppName appName)
		{
			ExchangeAssistance exchangeAssistanceObjectFromAD = HelpProvider.GetExchangeAssistanceObjectFromAD(OrganizationId.ForestWideOrgId);
			if (exchangeAssistanceObjectFromAD != null)
			{
				HelpProvider.RegisterChangeNotification(exchangeAssistanceObjectFromAD.Id);
				if (appName.Equals(HelpProvider.HelpAppName.Ecp) || appName.Equals(HelpProvider.HelpAppName.TenantMonitoring))
				{
					if (exchangeAssistanceObjectFromAD.ControlPanelHelpURL != null)
					{
						HelpProvider.baseUrl = Utilities.NormalizeUrl(exchangeAssistanceObjectFromAD.ControlPanelHelpURL);
					}
					if (exchangeAssistanceObjectFromAD.ControlPanelFeedbackEnabled)
					{
						HelpProvider.controlPanelFeedbackUrl = exchangeAssistanceObjectFromAD.ControlPanelFeedbackURL;
					}
					else
					{
						HelpProvider.controlPanelFeedbackUrl = null;
					}
				}
				else if (appName.Equals(HelpProvider.HelpAppName.Owa))
				{
					if (exchangeAssistanceObjectFromAD.OWALightHelpURL != null)
					{
						HelpProvider.baseOwaLightUrl = exchangeAssistanceObjectFromAD.OWALightHelpURL;
					}
					if (exchangeAssistanceObjectFromAD.OWAHelpURL != null)
					{
						HelpProvider.baseOwaPremiumUrl = exchangeAssistanceObjectFromAD.OWAHelpURL;
					}
					if (exchangeAssistanceObjectFromAD.OWAFeedbackEnabled)
					{
						HelpProvider.owaPremiumFeedbackUrl = exchangeAssistanceObjectFromAD.OWAFeedbackURL;
					}
					else
					{
						HelpProvider.owaPremiumFeedbackUrl = null;
					}
					if (exchangeAssistanceObjectFromAD.OWALightFeedbackEnabled)
					{
						HelpProvider.owaLightFeedbackUrl = exchangeAssistanceObjectFromAD.OWALightFeedbackURL;
					}
					else
					{
						HelpProvider.owaLightFeedbackUrl = null;
					}
				}
				if (exchangeAssistanceObjectFromAD.PrivacyLinkDisplayEnabled)
				{
					HelpProvider.privacyStatementUrl = exchangeAssistanceObjectFromAD.PrivacyStatementURL;
				}
				else
				{
					HelpProvider.privacyStatementUrl = null;
				}
				if (exchangeAssistanceObjectFromAD.WindowsLiveAccountURLEnabled)
				{
					HelpProvider.windowsLiveAccountUrl = exchangeAssistanceObjectFromAD.WindowsLiveAccountPageURL;
				}
				else
				{
					HelpProvider.windowsLiveAccountUrl = null;
				}
				if (exchangeAssistanceObjectFromAD.CommunityLinkDisplayEnabled)
				{
					HelpProvider.communityUrl = exchangeAssistanceObjectFromAD.CommunityURL;
					return;
				}
				HelpProvider.communityUrl = null;
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002BE0 File Offset: 0x00000DE0
		private static void LoadBaseUrlViaCmdlet(HelpProvider.HelpAppName appName, RunspaceServerSettingsPresentationObject runspaceServerSettings, MonadConnectionInfo monadConnectionInfo)
		{
			if (appName != HelpProvider.HelpAppName.Toolbox)
			{
				if (appName != HelpProvider.HelpAppName.Eap)
				{
					throw new InvalidOperationException("appName is not a valid HelpAppName enum. Check caller of Initialize how we get this value.");
				}
			}
			try
			{
				ExchangeAssistance exchangeAssistance = null;
				MonadConnection connection = new MonadConnection("timeout=30", new CommandInteractionHandler(), runspaceServerSettings, monadConnectionInfo);
				using (new OpenConnection(connection))
				{
					using (MonadCommand monadCommand = new MonadCommand("Get-ExchangeAssistanceConfig", connection))
					{
						object[] array = monadCommand.Execute();
						if (array != null && array.Length != 0)
						{
							exchangeAssistance = (array[0] as ExchangeAssistance);
						}
					}
				}
				if (exchangeAssistance != null)
				{
					if (exchangeAssistance.ManagementConsoleHelpURL != null)
					{
						HelpProvider.baseUrl = Utilities.NormalizeUrl(exchangeAssistance.ManagementConsoleHelpURL);
					}
					if (exchangeAssistance.ManagementConsoleFeedbackEnabled)
					{
						HelpProvider.managementConsoleFeedbackUrl = exchangeAssistance.ManagementConsoleFeedbackURL;
					}
					else
					{
						HelpProvider.managementConsoleFeedbackUrl = null;
					}
					if (exchangeAssistance.PrivacyLinkDisplayEnabled)
					{
						HelpProvider.privacyStatementUrl = exchangeAssistance.PrivacyStatementURL;
					}
					else
					{
						HelpProvider.privacyStatementUrl = null;
					}
					if (exchangeAssistance.WindowsLiveAccountURLEnabled)
					{
						HelpProvider.windowsLiveAccountUrl = exchangeAssistance.WindowsLiveAccountPageURL;
					}
					else
					{
						HelpProvider.windowsLiveAccountUrl = null;
					}
					if (exchangeAssistance.CommunityLinkDisplayEnabled)
					{
						HelpProvider.communityUrl = exchangeAssistance.CommunityURL;
					}
					else
					{
						HelpProvider.communityUrl = null;
					}
				}
			}
			catch (CommandExecutionException ex)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "CommandExecution Exception in LoadBaseURL: {0}", ex.Message);
			}
			catch (CmdletInvocationException ex2)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "CmdletInvocationException Exception in LoadBaseURL: {0}", ex2.Message);
			}
			catch (PipelineStoppedException ex3)
			{
				ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "PipelineStopped Exception in LoadBaseURL: {0}", ex3.Message);
			}
			HelpProvider.callingAppName = appName;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002D8C File Offset: 0x00000F8C
		private static void LoadBaseUrl(HelpProvider.HelpAppName appName)
		{
			switch (appName)
			{
			case HelpProvider.HelpAppName.Ecp:
			case HelpProvider.HelpAppName.Owa:
			case HelpProvider.HelpAppName.TenantMonitoring:
				HelpProvider.LoadBaseUrlViaADSession(appName);
				goto IL_52;
			case HelpProvider.HelpAppName.Setup:
				HelpProvider.LoadSetupBaseUrlFromRegistry();
				goto IL_52;
			case HelpProvider.HelpAppName.Bpa:
				HelpProvider.LoadBpaBaseUrlFromRegistry();
				goto IL_52;
			case HelpProvider.HelpAppName.Compliance:
				HelpProvider.LoadComplianceBaseUrl();
				goto IL_52;
			}
			throw new InvalidOperationException("appName is not a valid HelpAppName enum. Check caller of Initialize how we get this value.");
			IL_52:
			HelpProvider.callingAppName = appName;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002DF4 File Offset: 0x00000FF4
		private static Uri ConstructFinalHelpUrl(Uri baseUrl, string contentId, string locale, params string[] queryParams)
		{
			string text = string.Format(baseUrl.ToString(), locale, contentId);
			StringBuilder stringBuilder = new StringBuilder(text.ToString(), HelpProvider.initialCapacity);
			stringBuilder.Append("?");
			stringBuilder.Append("v=");
			stringBuilder.Append(HelpProvider.applicationVersion);
			foreach (string value in queryParams)
			{
				if (!string.IsNullOrEmpty(value))
				{
					stringBuilder.Append("&");
					stringBuilder.Append(value);
				}
			}
			return new Uri(stringBuilder.ToString());
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002E84 File Offset: 0x00001084
		private static Uri UrlConstructHelper(string contentId, params string[] queryParams)
		{
			return HelpProvider.ConstructFinalHelpUrl(HelpProvider.baseUrl, contentId, HelpProvider.GetUserHelpLanguage(), queryParams);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002E97 File Offset: 0x00001097
		private static Uri LocaleBasedUrlConstructHelper(string contentId, string locale, params string[] queryParams)
		{
			return HelpProvider.ConstructFinalHelpUrl(HelpProvider.baseUrl, contentId, locale, queryParams);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002EA6 File Offset: 0x000010A6
		private static Uri UrlConstructHelperOwa(string contentId, HelpProvider.OwaHelpExperience owaExp, params string[] queryParams)
		{
			return HelpProvider.ConstructFinalHelpUrl(HelpProvider.GetOwaBaseUrl(owaExp), contentId, HelpProvider.GetUserHelpLanguage(), queryParams);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002EBC File Offset: 0x000010BC
		private static string GetEventParam(string source, string category, string eventId)
		{
			if (string.IsNullOrEmpty(category))
			{
				return string.Format("{0}{1}{2}.{3}", new object[]
				{
					"ev=",
					"ms.exch.evt.",
					source.Replace(" ", string.Empty),
					eventId
				});
			}
			return string.Format("{0}{1}{2}.{3}.{4}", new object[]
			{
				"ev=",
				"ms.exch.evt.",
				source.Replace(" ", string.Empty),
				category,
				eventId
			});
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002F48 File Offset: 0x00001148
		private static string GetErrorParam(LocalizedException exception)
		{
			return "e=" + "ms.exch.err." + exception.StringId;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002F60 File Offset: 0x00001160
		private static bool ShouldConstructHelpUrlForException(LocalizedException exception)
		{
			return exception.LocalizedString.ShowAssistanceInfoInUIIfError && !string.IsNullOrEmpty(exception.StringId);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002F8D File Offset: 0x0000118D
		private static void CurrentDomain_DomainUnload(object sender, EventArgs e)
		{
			if (HelpProvider.notificationRequestCookie != null)
			{
				ADNotificationAdapter.UnregisterChangeNotification(HelpProvider.notificationRequestCookie);
				HelpProvider.notificationRequestCookie = null;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002FA8 File Offset: 0x000011A8
		private static void RegisterChangeNotification(ADObjectId adObjectId)
		{
			if (HelpProvider.notificationRequestCookie == null)
			{
				ADNotificationCallback callback = new ADNotificationCallback(HelpProvider.OnExchangeAssiatnceConfigChanged);
				HelpProvider.notificationRequestCookie = ADNotificationAdapter.RegisterChangeNotification<ExchangeAssistance>(adObjectId, callback);
				AppDomain.CurrentDomain.DomainUnload += HelpProvider.CurrentDomain_DomainUnload;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002FEC File Offset: 0x000011EC
		private static void OnExchangeAssiatnceConfigChanged(ADNotificationEventArgs args)
		{
			ExchangeAssistance exchangeAssistanceObjectFromADWithRetry = HelpProvider.GetExchangeAssistanceObjectFromADWithRetry(OrganizationId.ForestWideOrgId);
			if (exchangeAssistanceObjectFromADWithRetry != null)
			{
				switch (HelpProvider.callingAppName)
				{
				case HelpProvider.HelpAppName.Ecp:
					if (exchangeAssistanceObjectFromADWithRetry.ControlPanelHelpURL != null)
					{
						Interlocked.Exchange<Uri>(ref HelpProvider.baseUrl, Utilities.NormalizeUrl(exchangeAssistanceObjectFromADWithRetry.ControlPanelHelpURL));
						return;
					}
					break;
				case HelpProvider.HelpAppName.Owa:
					if (exchangeAssistanceObjectFromADWithRetry.OWAHelpURL != null)
					{
						Interlocked.Exchange<Uri>(ref HelpProvider.baseOwaPremiumUrl, exchangeAssistanceObjectFromADWithRetry.OWAHelpURL);
					}
					if (exchangeAssistanceObjectFromADWithRetry.OWALightHelpURL != null)
					{
						Interlocked.Exchange<Uri>(ref HelpProvider.baseOwaLightUrl, exchangeAssistanceObjectFromADWithRetry.OWALightHelpURL);
					}
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003080 File Offset: 0x00001280
		private static ExchangeAssistance GetExchangeAssistanceObjectFromADWithRetry(OrganizationId organizationId)
		{
			int num = 0;
			ExchangeAssistance exchangeAssistance = null;
			while (exchangeAssistance == null && num < 6)
			{
				exchangeAssistance = HelpProvider.GetExchangeAssistanceObjectFromAD(organizationId);
				if (exchangeAssistance == null)
				{
					num++;
					Thread.Sleep(10000);
				}
			}
			return exchangeAssistance;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000030B4 File Offset: 0x000012B4
		private static bool IsGallatin()
		{
			bool flag = false;
			return bool.TryParse(ConfigurationManager.AppSettings["IsGallatin"], out flag) && flag;
		}

		// Token: 0x04000001 RID: 1
		public const string SetupQualifier = "ms.exch.setup.";

		// Token: 0x04000002 RID: 2
		public const string SetupReadinessQualifier = "ms.exch.setupreadiness.";

		// Token: 0x04000003 RID: 3
		public const string BPAQualifier = "ms.o365.bpa.";

		// Token: 0x04000004 RID: 4
		public const string ComplianceQualifier = "ms.o365.cc.";

		// Token: 0x04000005 RID: 5
		private const string DefaultRegistryBaseUrl = "http://technet.microsoft.com/library(EXCHG.150)";

		// Token: 0x04000006 RID: 6
		private const string BaseSetupUrlFromRegistryKeyName = "baseSetupHelpUrl";

		// Token: 0x04000007 RID: 7
		private const string BaseBpaUrlFromRegistryKeyName = "baseBpaHelpUrl";

		// Token: 0x04000008 RID: 8
		private const string ECPQualifier = "ms.exch.eac.";

		// Token: 0x04000009 RID: 9
		private const string OWALightQualifier = "ms.exch.owal.";

		// Token: 0x0400000A RID: 10
		private const string OWAPremiumQualifier = "ms.exch.owap.";

		// Token: 0x0400000B RID: 11
		private const string OWAOptionsQualifier = "ms.exch.owao.";

		// Token: 0x0400000C RID: 12
		private const string ToolboxQualifier = "ms.exch.toolbox.";

		// Token: 0x0400000D RID: 13
		private const string ERRQualifier = "ms.exch.err.";

		// Token: 0x0400000E RID: 14
		private const string EventQualifier = "ms.exch.evt.";

		// Token: 0x0400000F RID: 15
		private const string QueryParamErrorID = "e=";

		// Token: 0x04000010 RID: 16
		private const string QueryParamLoginInfo = "l=";

		// Token: 0x04000011 RID: 17
		private const string QueryParamAppVersion = "v=";

		// Token: 0x04000012 RID: 18
		private const string QueryParamServicePlan = "s=";

		// Token: 0x04000013 RID: 19
		private const string QueryParamF1Type = "t=";

		// Token: 0x04000014 RID: 20
		private const string QueryParamEventID = "ev=";

		// Token: 0x04000015 RID: 21
		private const string EHCERRPage = "ms.exch.err.default";

		// Token: 0x04000016 RID: 22
		private const string EHCEVTPage = "ms.exch.evt.default";

		// Token: 0x04000017 RID: 23
		private const int MaxRetryAttempts = 6;

		// Token: 0x04000018 RID: 24
		private const int RetryInterval = 10000;

		// Token: 0x04000019 RID: 25
		private const string ExchangeAssistanceContainerName = "ExchangeAssistance";

		// Token: 0x0400001A RID: 26
		private const string IsGallatinConfigKey = "IsGallatin";

		// Token: 0x0400001B RID: 27
		private const string DefaultComplianceBaseUrl = "http://technet.microsoft.com/library";

		// Token: 0x0400001C RID: 28
		private static readonly string[] officeRedirModes = new string[]
		{
			"Desktop",
			"Metro",
			"Mobile"
		};

		// Token: 0x0400001D RID: 29
		private static readonly string OwaLightNamespace = "OLWALIGHT";

		// Token: 0x0400001E RID: 30
		private static readonly string OwaPremiumNamespace = "OLWAENDUSER";

		// Token: 0x0400001F RID: 31
		private static readonly string OwaMsoProfessionalNamespace = "OLWAO365P";

		// Token: 0x04000020 RID: 32
		private static readonly string OwaMsoEnterpriseNamespace = "OLWAO365E";

		// Token: 0x04000021 RID: 33
		private static readonly string OwaMsoProfessionalGallatinNamespace = "OLWAO365Pg";

		// Token: 0x04000022 RID: 34
		private static readonly string OwaMsoEnterpriseGallatinNamespace = "OLWO365Eg";

		// Token: 0x04000023 RID: 35
		private static readonly string OwaMsoProfessionalServicePlanPrefix = "BPOS_L";

		// Token: 0x04000024 RID: 36
		private static readonly string CurrentVersionExchangeAssistanceContainerName = "ExchangeAssistance" + 15;

		// Token: 0x04000025 RID: 37
		private static readonly string applicationVersion = typeof(HelpProvider).GetApplicationVersion();

		// Token: 0x04000026 RID: 38
		private static readonly string baseSetupUrlFromRegistryPath = string.Format("SOFTWARE\\Microsoft\\ExchangeServer\\{0}\\{1}", "v15", "Setup");

		// Token: 0x04000027 RID: 39
		private static readonly string baseBpaUrlFromRegistryPath = "SOFTWARE\\Microsoft\\ExchangeServer\\BPA";

		// Token: 0x04000028 RID: 40
		private static bool initializedViaCmdlet = false;

		// Token: 0x04000029 RID: 41
		private static int initialCapacity = 2000;

		// Token: 0x0400002A RID: 42
		private static Uri baseUrl = new Uri("http://technet.microsoft.com/{0}/library/{1}(EXCHG.150).aspx");

		// Token: 0x0400002B RID: 43
		private static Uri baseOwaLightUrl = new Uri("http://o15.officeredir.microsoft.com/r/rlidOfficeWebHelp");

		// Token: 0x0400002C RID: 44
		private static Uri baseOwaPremiumUrl = new Uri("http://o15.officeredir.microsoft.com/r/rlidOfficeWebHelp");

		// Token: 0x0400002D RID: 45
		private static Uri privacyStatementUrl = new Uri("http://go.microsoft.com/fwlink/p/?LinkId=253085");

		// Token: 0x0400002E RID: 46
		private static Uri privacyStatementUrlExchange2013 = new Uri("http://go.microsoft.com/fwlink/?LinkId=260597");

		// Token: 0x0400002F RID: 47
		private static Uri privacyStatementUrlMSOnline = new Uri("http://go.microsoft.com/fwlink/p/?LinkId=259417");

		// Token: 0x04000030 RID: 48
		private static Uri windowsLiveAccountUrl = new Uri("http://go.microsoft.com/fwlink/p/?LinkId=253084");

		// Token: 0x04000031 RID: 49
		private static Uri controlPanelFeedbackUrl = new Uri("http://go.microsoft.com/fwlink/p/?LinkId=253080");

		// Token: 0x04000032 RID: 50
		private static Uri managementConsoleFeedbackUrl = new Uri("http://go.microsoft.com/fwlink/p/?LinkId=253081");

		// Token: 0x04000033 RID: 51
		private static Uri owaPremiumFeedbackUrl = new Uri("http://go.microsoft.com/fwlink/p/?LinkId=253083");

		// Token: 0x04000034 RID: 52
		private static Uri owaLightFeedbackUrl = new Uri("http://go.microsoft.com/fwlink/p/?LinkId=253087");

		// Token: 0x04000035 RID: 53
		private static Uri communityUrl = new Uri("http://go.microsoft.com/fwlink/p/?LinkId=253086");

		// Token: 0x04000036 RID: 54
		private static HelpProvider.HelpAppName callingAppName;

		// Token: 0x04000037 RID: 55
		private static ADNotificationRequestCookie notificationRequestCookie;

		// Token: 0x02000003 RID: 3
		public enum HelpAppName
		{
			// Token: 0x04000039 RID: 57
			Ecp,
			// Token: 0x0400003A RID: 58
			Owa,
			// Token: 0x0400003B RID: 59
			Toolbox,
			// Token: 0x0400003C RID: 60
			TenantMonitoring,
			// Token: 0x0400003D RID: 61
			Setup,
			// Token: 0x0400003E RID: 62
			Eap,
			// Token: 0x0400003F RID: 63
			Bpa,
			// Token: 0x04000040 RID: 64
			Compliance
		}

		// Token: 0x02000004 RID: 4
		public enum OwaHelpExperience
		{
			// Token: 0x04000042 RID: 66
			Light,
			// Token: 0x04000043 RID: 67
			Premium,
			// Token: 0x04000044 RID: 68
			Options
		}

		// Token: 0x02000005 RID: 5
		public enum RenderingMode
		{
			// Token: 0x04000046 RID: 70
			Mouse,
			// Token: 0x04000047 RID: 71
			TWide,
			// Token: 0x04000048 RID: 72
			TNarrow
		}
	}
}
