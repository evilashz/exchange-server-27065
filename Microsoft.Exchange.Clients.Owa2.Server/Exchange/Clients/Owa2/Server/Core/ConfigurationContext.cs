using System;
using System.Collections.Specialized;
using System.Web;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Configuration;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000CD RID: 205
	internal class ConfigurationContext : ConfigurationContextBase, IConfigurationContext
	{
		// Token: 0x06000810 RID: 2064 RVA: 0x0001AACF File Offset: 0x00018CCF
		public ConfigurationContext(UserContext userContext) : this(userContext, null)
		{
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001AADC File Offset: 0x00018CDC
		public ConfigurationContext(UserContext userContext, UserConfigurationManager.IAggregationContext aggregationContext)
		{
			this.principal = userContext.ExchangePrincipal;
			this.allowedCapabilitiesFlags = userContext.AllowedCapabilitiesFlags;
			this.MySiteUrl = ((userContext.MailboxIdentity == null) ? null : (userContext.MailboxIdentity.GetOWAMiniRecipient()[ADRecipientSchema.WebPage] as string));
			this.aggregationContext = aggregationContext;
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x0001AB39 File Offset: 0x00018D39
		public override WebBeaconFilterLevels FilterWebBeaconsAndHtmlForms
		{
			get
			{
				return VdirConfiguration.Instance.FilterWebBeaconsAndHtmlForms;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x0001AB45 File Offset: 0x00018D45
		public override AttachmentPolicy AttachmentPolicy
		{
			get
			{
				return this.GetConfiguration().AttachmentPolicy;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x0001AB52 File Offset: 0x00018D52
		public ulong SegmentationFlags
		{
			get
			{
				return this.GetConfiguration().SegmentationFlags;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x0001AB5F File Offset: 0x00018D5F
		public string DefaultTheme
		{
			get
			{
				return this.GetConfiguration().DefaultTheme;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x0001AB6C File Offset: 0x00018D6C
		public bool UseGB18030
		{
			get
			{
				return this.GetConfiguration().UseGB18030;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x0001AB79 File Offset: 0x00018D79
		public bool UseISO885915
		{
			get
			{
				return this.GetConfiguration().UseISO885915;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x0001AB86 File Offset: 0x00018D86
		public OutboundCharsetOptions OutboundCharset
		{
			get
			{
				return this.GetConfiguration().OutboundCharset;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x0001AB93 File Offset: 0x00018D93
		public InstantMessagingTypeOptions InstantMessagingType
		{
			get
			{
				return this.GetConfiguration().InstantMessagingType;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x0001ABA0 File Offset: 0x00018DA0
		public bool PlacesEnabled
		{
			get
			{
				return this.GetConfiguration().PlacesEnabled;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x0001ABAD File Offset: 0x00018DAD
		public bool WeatherEnabled
		{
			get
			{
				return this.GetConfiguration().WeatherEnabled;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x0001ABBA File Offset: 0x00018DBA
		public bool AllowCopyContactsToDeviceAddressBook
		{
			get
			{
				return this.GetConfiguration().AllowCopyContactsToDeviceAddressBook;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0001ABC7 File Offset: 0x00018DC7
		public AllowOfflineOnEnum AllowOfflineOn
		{
			get
			{
				return this.GetConfiguration().AllowOfflineOn;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x0001ABD4 File Offset: 0x00018DD4
		public bool RecoverDeletedItemsEnabled
		{
			get
			{
				return this.GetConfiguration().RecoverDeletedItemsEnabled;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x0001ABE1 File Offset: 0x00018DE1
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x0001ABE9 File Offset: 0x00018DE9
		public string MySiteUrl { get; private set; }

		// Token: 0x06000821 RID: 2081 RVA: 0x0001ABF2 File Offset: 0x00018DF2
		public bool IsFeatureNotRestricted(ulong feature)
		{
			return this.allowedCapabilitiesFlags == 0UL || (this.allowedCapabilitiesFlags & feature) == feature;
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0001AC0B File Offset: 0x00018E0B
		public override bool IsFeatureEnabled(Feature feature)
		{
			return (feature & (Feature)this.GetConfiguration().SegmentationFlags) == feature && this.IsFeatureNotRestricted((ulong)feature);
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001AC28 File Offset: 0x00018E28
		public Feature GetEnabledFeatures()
		{
			Feature feature = (Feature)0UL;
			foreach (object obj in Enum.GetValues(typeof(Feature)))
			{
				if (this.IsFeatureEnabled((Feature)obj))
				{
					feature ^= (Feature)obj;
				}
			}
			return feature;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0001AC9C File Offset: 0x00018E9C
		public override ulong GetFeaturesEnabled(Feature feature)
		{
			ulong num = (ulong)feature;
			ulong num2 = (ulong)(feature & (Feature)this.GetConfiguration().SegmentationFlags);
			while ((num & 1UL) == 0UL && num != 0UL)
			{
				num >>= 1;
				num2 >>= 1;
			}
			return num2;
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0001AD40 File Offset: 0x00018F40
		private ConfigurationBase GetConfiguration()
		{
			ConfigurationBase result = this.configurationBase;
			if (result == null)
			{
				if (this.aggregationContext == null)
				{
					result = this.GetPolicyOrVdirConfiguration(null, null, null, null, null, new bool?(false));
				}
				else
				{
					NameValueCollection requestHeaders = null;
					string requestUserAgent = null;
					string rawUrl = null;
					Uri uri = null;
					string userHostAddress = null;
					bool? isLocal = null;
					if (HttpContext.Current != null && HttpContext.Current.Request != null)
					{
						requestHeaders = HttpContext.Current.Request.Headers;
						requestUserAgent = HttpContext.Current.Request.UserAgent;
						rawUrl = HttpContext.Current.Request.RawUrl;
						uri = HttpContext.Current.Request.Url;
						userHostAddress = HttpContext.Current.Request.UserHostAddress;
						isLocal = new bool?(HttpContext.Current.Request.IsLocal);
					}
					OwaConfigurationBaseData data = this.aggregationContext.ReadType<OwaConfigurationBaseData>("OWA.ConfigurationBase", delegate
					{
						result = this.GetPolicyOrVdirConfiguration(requestHeaders, requestUserAgent, rawUrl, uri, userHostAddress, isLocal);
						return AggregatedBaseConfiguration.DataFromConfiguration(result);
					});
					if (result == null)
					{
						result = AggregatedBaseConfiguration.ConfigurationFromData(data);
					}
				}
			}
			this.configurationBase = result;
			return result;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0001AEB8 File Offset: 0x000190B8
		private ConfigurationBase GetPolicyOrVdirConfiguration(NameValueCollection requestHeaders = null, string requestUserAgent = null, string rawUrl = null, Uri uri = null, string userHostAddress = null, bool? isLocal = false)
		{
			ConfigurationBase configurationBase = null;
			if (this.principal != null)
			{
				configurationBase = OwaMailboxPolicyCache.GetPolicyConfiguration(this.principal.MailboxInfo.Configuration.OwaMailboxPolicy, this.principal.MailboxInfo.OrganizationId);
			}
			return configurationBase ?? VdirConfiguration.GetInstance(requestHeaders, requestUserAgent, rawUrl, uri, userHostAddress, isLocal);
		}

		// Token: 0x04000489 RID: 1161
		private readonly ulong allowedCapabilitiesFlags;

		// Token: 0x0400048A RID: 1162
		private ExchangePrincipal principal;

		// Token: 0x0400048B RID: 1163
		private UserConfigurationManager.IAggregationContext aggregationContext;

		// Token: 0x0400048C RID: 1164
		private ConfigurationBase configurationBase;
	}
}
