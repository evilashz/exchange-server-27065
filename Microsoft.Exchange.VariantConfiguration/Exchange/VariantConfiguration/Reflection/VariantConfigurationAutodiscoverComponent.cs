using System;
using Microsoft.Exchange.AutoDiscover;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x020000FA RID: 250
	public sealed class VariantConfigurationAutodiscoverComponent : VariantConfigurationComponent
	{
		// Token: 0x06000ACF RID: 2767 RVA: 0x000193D8 File Offset: 0x000175D8
		internal VariantConfigurationAutodiscoverComponent() : base("Autodiscover")
		{
			base.Add(new VariantConfigurationSection("Autodiscover.settings.ini", "AnonymousAuthentication", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Autodiscover.settings.ini", "EnableMobileSyncRedirectBypass", typeof(IFeature), true));
			base.Add(new VariantConfigurationSection("Autodiscover.settings.ini", "ParseBinarySecretHeader", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Autodiscover.settings.ini", "SkipServiceTopologyDiscovery", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Autodiscover.settings.ini", "StreamInsightUploader", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Autodiscover.settings.ini", "LoadNegoExSspNames", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Autodiscover.settings.ini", "NoADLookupForUser", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Autodiscover.settings.ini", "NoCrossForestDiscover", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Autodiscover.settings.ini", "EcpInternalExternalUrl", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Autodiscover.settings.ini", "MapiHttpForOutlook14", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Autodiscover.settings.ini", "OWAUrl", typeof(IOWAUrl), true));
			base.Add(new VariantConfigurationSection("Autodiscover.settings.ini", "AccountInCloud", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Autodiscover.settings.ini", "ConfigurePerformanceCounters", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Autodiscover.settings.ini", "RedirectOutlookClient", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Autodiscover.settings.ini", "WsSecurityEndpoint", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Autodiscover.settings.ini", "UseMapiHttpADSetting", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Autodiscover.settings.ini", "NoAuthenticationTokenToNego", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Autodiscover.settings.ini", "RestrictedSettings", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Autodiscover.settings.ini", "MapiHttp", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Autodiscover.settings.ini", "LogonViaStandardTokens", typeof(IFeature), false));
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x00019670 File Offset: 0x00017870
		public VariantConfigurationSection AnonymousAuthentication
		{
			get
			{
				return base["AnonymousAuthentication"];
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x0001967D File Offset: 0x0001787D
		public VariantConfigurationSection EnableMobileSyncRedirectBypass
		{
			get
			{
				return base["EnableMobileSyncRedirectBypass"];
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x0001968A File Offset: 0x0001788A
		public VariantConfigurationSection ParseBinarySecretHeader
		{
			get
			{
				return base["ParseBinarySecretHeader"];
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x00019697 File Offset: 0x00017897
		public VariantConfigurationSection SkipServiceTopologyDiscovery
		{
			get
			{
				return base["SkipServiceTopologyDiscovery"];
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x000196A4 File Offset: 0x000178A4
		public VariantConfigurationSection StreamInsightUploader
		{
			get
			{
				return base["StreamInsightUploader"];
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x000196B1 File Offset: 0x000178B1
		public VariantConfigurationSection LoadNegoExSspNames
		{
			get
			{
				return base["LoadNegoExSspNames"];
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x000196BE File Offset: 0x000178BE
		public VariantConfigurationSection NoADLookupForUser
		{
			get
			{
				return base["NoADLookupForUser"];
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x000196CB File Offset: 0x000178CB
		public VariantConfigurationSection NoCrossForestDiscover
		{
			get
			{
				return base["NoCrossForestDiscover"];
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x000196D8 File Offset: 0x000178D8
		public VariantConfigurationSection EcpInternalExternalUrl
		{
			get
			{
				return base["EcpInternalExternalUrl"];
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x000196E5 File Offset: 0x000178E5
		public VariantConfigurationSection MapiHttpForOutlook14
		{
			get
			{
				return base["MapiHttpForOutlook14"];
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x000196F2 File Offset: 0x000178F2
		public VariantConfigurationSection OWAUrl
		{
			get
			{
				return base["OWAUrl"];
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x000196FF File Offset: 0x000178FF
		public VariantConfigurationSection AccountInCloud
		{
			get
			{
				return base["AccountInCloud"];
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0001970C File Offset: 0x0001790C
		public VariantConfigurationSection ConfigurePerformanceCounters
		{
			get
			{
				return base["ConfigurePerformanceCounters"];
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x00019719 File Offset: 0x00017919
		public VariantConfigurationSection RedirectOutlookClient
		{
			get
			{
				return base["RedirectOutlookClient"];
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x00019726 File Offset: 0x00017926
		public VariantConfigurationSection WsSecurityEndpoint
		{
			get
			{
				return base["WsSecurityEndpoint"];
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x00019733 File Offset: 0x00017933
		public VariantConfigurationSection UseMapiHttpADSetting
		{
			get
			{
				return base["UseMapiHttpADSetting"];
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x00019740 File Offset: 0x00017940
		public VariantConfigurationSection NoAuthenticationTokenToNego
		{
			get
			{
				return base["NoAuthenticationTokenToNego"];
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0001974D File Offset: 0x0001794D
		public VariantConfigurationSection RestrictedSettings
		{
			get
			{
				return base["RestrictedSettings"];
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x0001975A File Offset: 0x0001795A
		public VariantConfigurationSection MapiHttp
		{
			get
			{
				return base["MapiHttp"];
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x00019767 File Offset: 0x00017967
		public VariantConfigurationSection LogonViaStandardTokens
		{
			get
			{
				return base["LogonViaStandardTokens"];
			}
		}
	}
}
