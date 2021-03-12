using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001B3 RID: 435
	internal class ThrottlingPolicyGeneralSettings : ThrottlingPolicyBaseSettings
	{
		// Token: 0x06000F1C RID: 3868 RVA: 0x0002EBB8 File Offset: 0x0002CDB8
		public ThrottlingPolicyGeneralSettings()
		{
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0002EBC0 File Offset: 0x0002CDC0
		private ThrottlingPolicyGeneralSettings(string value) : base(value)
		{
			Unlimited<uint>? messageRateLimit = this.MessageRateLimit;
			Unlimited<uint>? recipientRateLimit = this.RecipientRateLimit;
			Unlimited<uint>? forwardeeLimit = this.ForwardeeLimit;
			Unlimited<uint>? discoveryMaxConcurrency = this.DiscoveryMaxConcurrency;
			Unlimited<uint>? discoveryMaxMailboxes = this.DiscoveryMaxMailboxes;
			Unlimited<uint>? discoveryMaxKeywords = this.DiscoveryMaxKeywords;
			Unlimited<uint>? discoveryMaxPreviewSearchMailboxes = this.DiscoveryMaxPreviewSearchMailboxes;
			Unlimited<uint>? discoveryMaxStatsSearchMailboxes = this.DiscoveryMaxStatsSearchMailboxes;
			Unlimited<uint>? discoveryPreviewSearchResultsPageSize = this.DiscoveryPreviewSearchResultsPageSize;
			Unlimited<uint>? discoveryMaxKeywordsPerPage = this.DiscoveryMaxKeywordsPerPage;
			Unlimited<uint>? discoveryMaxRefinerResults = this.DiscoveryMaxRefinerResults;
			Unlimited<uint>? discoveryMaxSearchQueueDepth = this.DiscoveryMaxSearchQueueDepth;
			Unlimited<uint>? discoverySearchTimeoutPeriod = this.DiscoverySearchTimeoutPeriod;
			Unlimited<uint>? complianceMaxExpansionDGRecipients = this.ComplianceMaxExpansionDGRecipients;
			Unlimited<uint>? complianceMaxExpansionNestedDGs = this.ComplianceMaxExpansionNestedDGs;
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0002EC3D File Offset: 0x0002CE3D
		public static ThrottlingPolicyGeneralSettings Parse(string stateToParse)
		{
			return new ThrottlingPolicyGeneralSettings(stateToParse);
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x0002EC45 File Offset: 0x0002CE45
		// (set) Token: 0x06000F20 RID: 3872 RVA: 0x0002EC52 File Offset: 0x0002CE52
		internal Unlimited<uint>? MessageRateLimit
		{
			get
			{
				return base.GetValueFromPropertyBag("MsgRateLimit");
			}
			set
			{
				base.SetValueInPropertyBag("MsgRateLimit", value);
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x0002EC60 File Offset: 0x0002CE60
		// (set) Token: 0x06000F22 RID: 3874 RVA: 0x0002EC6D File Offset: 0x0002CE6D
		internal Unlimited<uint>? RecipientRateLimit
		{
			get
			{
				return base.GetValueFromPropertyBag("RecipRateLimit");
			}
			set
			{
				base.SetValueInPropertyBag("RecipRateLimit", value);
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x0002EC7B File Offset: 0x0002CE7B
		// (set) Token: 0x06000F24 RID: 3876 RVA: 0x0002EC88 File Offset: 0x0002CE88
		internal Unlimited<uint>? ForwardeeLimit
		{
			get
			{
				return base.GetValueFromPropertyBag("ForwardeeLimit");
			}
			set
			{
				base.SetValueInPropertyBag("ForwardeeLimit", value);
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x0002EC96 File Offset: 0x0002CE96
		// (set) Token: 0x06000F26 RID: 3878 RVA: 0x0002ECA3 File Offset: 0x0002CEA3
		internal Unlimited<uint>? DiscoveryMaxConcurrency
		{
			get
			{
				return base.GetValueFromPropertyBag("DiscoveryMaxConcurr");
			}
			set
			{
				base.SetValueInPropertyBag("DiscoveryMaxConcurr", value);
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x0002ECB1 File Offset: 0x0002CEB1
		// (set) Token: 0x06000F28 RID: 3880 RVA: 0x0002ECBE File Offset: 0x0002CEBE
		internal Unlimited<uint>? DiscoveryMaxMailboxes
		{
			get
			{
				return base.GetValueFromPropertyBag("DiscoveryMaxMailboxes");
			}
			set
			{
				base.SetValueInPropertyBag("DiscoveryMaxMailboxes", value);
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x0002ECCC File Offset: 0x0002CECC
		// (set) Token: 0x06000F2A RID: 3882 RVA: 0x0002ECD9 File Offset: 0x0002CED9
		internal Unlimited<uint>? DiscoveryMaxKeywords
		{
			get
			{
				return base.GetValueFromPropertyBag("DiscoveryMaxKeywords");
			}
			set
			{
				base.SetValueInPropertyBag("DiscoveryMaxKeywords", value);
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x0002ECE7 File Offset: 0x0002CEE7
		// (set) Token: 0x06000F2C RID: 3884 RVA: 0x0002ECF4 File Offset: 0x0002CEF4
		internal Unlimited<uint>? DiscoveryMaxPreviewSearchMailboxes
		{
			get
			{
				return base.GetValueFromPropertyBag("DiscoveryMaxPreviewMailboxes");
			}
			set
			{
				base.SetValueInPropertyBag("DiscoveryMaxPreviewMailboxes", value);
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x0002ED02 File Offset: 0x0002CF02
		// (set) Token: 0x06000F2E RID: 3886 RVA: 0x0002ED0F File Offset: 0x0002CF0F
		internal Unlimited<uint>? DiscoveryMaxStatsSearchMailboxes
		{
			get
			{
				return base.GetValueFromPropertyBag("DiscoveryMaxStatsMailboxes");
			}
			set
			{
				base.SetValueInPropertyBag("DiscoveryMaxStatsMailboxes", value);
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x0002ED1D File Offset: 0x0002CF1D
		// (set) Token: 0x06000F30 RID: 3888 RVA: 0x0002ED2A File Offset: 0x0002CF2A
		internal Unlimited<uint>? DiscoveryPreviewSearchResultsPageSize
		{
			get
			{
				return base.GetValueFromPropertyBag("DiscoveryPreviewSearchResultsPageSize");
			}
			set
			{
				base.SetValueInPropertyBag("DiscoveryPreviewSearchResultsPageSize", value);
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x0002ED38 File Offset: 0x0002CF38
		// (set) Token: 0x06000F32 RID: 3890 RVA: 0x0002ED45 File Offset: 0x0002CF45
		internal Unlimited<uint>? DiscoveryMaxKeywordsPerPage
		{
			get
			{
				return base.GetValueFromPropertyBag("DiscoveryMaxKeywordsPerPage");
			}
			set
			{
				base.SetValueInPropertyBag("DiscoveryMaxKeywordsPerPage", value);
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000F33 RID: 3891 RVA: 0x0002ED53 File Offset: 0x0002CF53
		// (set) Token: 0x06000F34 RID: 3892 RVA: 0x0002ED60 File Offset: 0x0002CF60
		internal Unlimited<uint>? DiscoveryMaxRefinerResults
		{
			get
			{
				return base.GetValueFromPropertyBag("DiscoveryMaxRefinerResults");
			}
			set
			{
				base.SetValueInPropertyBag("DiscoveryMaxRefinerResults", value);
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x0002ED6E File Offset: 0x0002CF6E
		// (set) Token: 0x06000F36 RID: 3894 RVA: 0x0002ED7B File Offset: 0x0002CF7B
		internal Unlimited<uint>? DiscoveryMaxSearchQueueDepth
		{
			get
			{
				return base.GetValueFromPropertyBag("DiscoveryMaxSearchQueueDepth");
			}
			set
			{
				base.SetValueInPropertyBag("DiscoveryMaxSearchQueueDepth", value);
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x0002ED89 File Offset: 0x0002CF89
		// (set) Token: 0x06000F38 RID: 3896 RVA: 0x0002ED96 File Offset: 0x0002CF96
		internal Unlimited<uint>? DiscoverySearchTimeoutPeriod
		{
			get
			{
				return base.GetValueFromPropertyBag("DiscoverySearchTimeoutPeriod");
			}
			set
			{
				base.SetValueInPropertyBag("DiscoverySearchTimeoutPeriod", value);
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x0002EDA4 File Offset: 0x0002CFA4
		// (set) Token: 0x06000F3A RID: 3898 RVA: 0x0002EDB1 File Offset: 0x0002CFB1
		internal Unlimited<uint>? ComplianceMaxExpansionDGRecipients
		{
			get
			{
				return base.GetValueFromPropertyBag("ComplianceMaxExpansionDGRecipients");
			}
			set
			{
				base.SetValueInPropertyBag("ComplianceMaxExpansionDGRecipients", value);
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000F3B RID: 3899 RVA: 0x0002EDBF File Offset: 0x0002CFBF
		// (set) Token: 0x06000F3C RID: 3900 RVA: 0x0002EDCC File Offset: 0x0002CFCC
		internal Unlimited<uint>? ComplianceMaxExpansionNestedDGs
		{
			get
			{
				return base.GetValueFromPropertyBag("ComplianceMaxExpansionNestedDGs");
			}
			set
			{
				base.SetValueInPropertyBag("ComplianceMaxExpansionNestedDGs", value);
			}
		}

		// Token: 0x04000912 RID: 2322
		private const string MessageRateLimitPrefix = "MsgRateLimit";

		// Token: 0x04000913 RID: 2323
		private const string RecipientRateLimitPrefix = "RecipRateLimit";

		// Token: 0x04000914 RID: 2324
		private const string ForwardeeLimitPrefix = "ForwardeeLimit";

		// Token: 0x04000915 RID: 2325
		private const string DiscoveryMaxConcurrencyPrefix = "DiscoveryMaxConcurr";

		// Token: 0x04000916 RID: 2326
		private const string DiscoveryMaxMailboxesPrefix = "DiscoveryMaxMailboxes";

		// Token: 0x04000917 RID: 2327
		private const string DiscoveryMaxPreviewSearchMailboxesPrefix = "DiscoveryMaxPreviewMailboxes";

		// Token: 0x04000918 RID: 2328
		private const string DiscoveryMaxStatsSearchMailboxesPrefix = "DiscoveryMaxStatsMailboxes";

		// Token: 0x04000919 RID: 2329
		private const string DiscoveryMaxKeywordsPrefix = "DiscoveryMaxKeywords";

		// Token: 0x0400091A RID: 2330
		private const string DiscoveryPreviewSearchResultsPageSizePrefix = "DiscoveryPreviewSearchResultsPageSize";

		// Token: 0x0400091B RID: 2331
		private const string DiscoveryMaxKeywordsPerPagePrefix = "DiscoveryMaxKeywordsPerPage";

		// Token: 0x0400091C RID: 2332
		private const string DiscoveryMaxRefinerResultsPrefix = "DiscoveryMaxRefinerResults";

		// Token: 0x0400091D RID: 2333
		private const string DiscoveryMaxSearchQueueDepthPrefix = "DiscoveryMaxSearchQueueDepth";

		// Token: 0x0400091E RID: 2334
		private const string DiscoverySearchTimeoutPeriodPrefix = "DiscoverySearchTimeoutPeriod";

		// Token: 0x0400091F RID: 2335
		private const string ComplianceMaxExpansionDGRecipientsPrefix = "ComplianceMaxExpansionDGRecipients";

		// Token: 0x04000920 RID: 2336
		private const string ComplianceMaxExpansionNestedDGsPrefix = "ComplianceMaxExpansionNestedDGs";
	}
}
