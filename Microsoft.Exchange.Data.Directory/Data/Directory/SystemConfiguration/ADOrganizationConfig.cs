using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000527 RID: 1319
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class ADOrganizationConfig : Organization
	{
		// Token: 0x17001247 RID: 4679
		// (get) Token: 0x06003A20 RID: 14880 RVA: 0x000E0773 File Offset: 0x000DE973
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADOrganizationConfig.schema;
			}
		}

		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x06003A21 RID: 14881 RVA: 0x000E077C File Offset: 0x000DE97C
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new OrFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, Organization.MostDerivedClass),
					new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, ExchangeConfigurationUnit.MostDerivedClass)
				});
			}
		}

		// Token: 0x17001249 RID: 4681
		// (get) Token: 0x06003A22 RID: 14882 RVA: 0x000E07BC File Offset: 0x000DE9BC
		// (set) Token: 0x06003A23 RID: 14883 RVA: 0x000E07CE File Offset: 0x000DE9CE
		[Parameter(Mandatory = false)]
		public bool PublicFoldersLockedForMigration
		{
			get
			{
				return (bool)this[OrganizationSchema.PublicFoldersLockedForMigration];
			}
			set
			{
				this[OrganizationSchema.PublicFoldersLockedForMigration] = value;
			}
		}

		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x06003A24 RID: 14884 RVA: 0x000E07E1 File Offset: 0x000DE9E1
		// (set) Token: 0x06003A25 RID: 14885 RVA: 0x000E07F3 File Offset: 0x000DE9F3
		[Parameter(Mandatory = false)]
		public bool PublicFolderMigrationComplete
		{
			get
			{
				return (bool)this[OrganizationSchema.PublicFolderMigrationComplete];
			}
			set
			{
				this[OrganizationSchema.PublicFolderMigrationComplete] = value;
			}
		}

		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x06003A26 RID: 14886 RVA: 0x000E0806 File Offset: 0x000DEA06
		// (set) Token: 0x06003A27 RID: 14887 RVA: 0x000E0818 File Offset: 0x000DEA18
		[Parameter(Mandatory = false)]
		public bool PublicFolderMailboxesLockedForNewConnections
		{
			get
			{
				return (bool)this[OrganizationSchema.PublicFolderMailboxesLockedForNewConnections];
			}
			set
			{
				this[OrganizationSchema.PublicFolderMailboxesLockedForNewConnections] = value;
			}
		}

		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x06003A28 RID: 14888 RVA: 0x000E082B File Offset: 0x000DEA2B
		// (set) Token: 0x06003A29 RID: 14889 RVA: 0x000E083D File Offset: 0x000DEA3D
		[Parameter(Mandatory = false)]
		public bool PublicFolderMailboxesMigrationComplete
		{
			get
			{
				return (bool)this[OrganizationSchema.PublicFolderMailboxesMigrationComplete];
			}
			set
			{
				this[OrganizationSchema.PublicFolderMailboxesMigrationComplete] = value;
			}
		}

		// Token: 0x1700124D RID: 4685
		// (get) Token: 0x06003A2A RID: 14890 RVA: 0x000E0850 File Offset: 0x000DEA50
		// (set) Token: 0x06003A2B RID: 14891 RVA: 0x000E0862 File Offset: 0x000DEA62
		public string ServicePlan
		{
			get
			{
				return (string)this[ADOrganizationConfigSchema.ServicePlan];
			}
			internal set
			{
				this[ADOrganizationConfigSchema.ServicePlan] = value;
			}
		}

		// Token: 0x1700124E RID: 4686
		// (get) Token: 0x06003A2C RID: 14892 RVA: 0x000E0870 File Offset: 0x000DEA70
		// (set) Token: 0x06003A2D RID: 14893 RVA: 0x000E0882 File Offset: 0x000DEA82
		public string TargetServicePlan
		{
			get
			{
				return (string)this[ADOrganizationConfigSchema.TargetServicePlan];
			}
			internal set
			{
				this[ADOrganizationConfigSchema.TargetServicePlan] = value;
			}
		}

		// Token: 0x1700124F RID: 4687
		// (get) Token: 0x06003A2E RID: 14894 RVA: 0x000E0890 File Offset: 0x000DEA90
		// (set) Token: 0x06003A2F RID: 14895 RVA: 0x000E08A2 File Offset: 0x000DEAA2
		[Parameter(Mandatory = false)]
		public bool PublicComputersDetectionEnabled
		{
			get
			{
				return (bool)this[OrganizationSchema.PublicComputersDetectionEnabled];
			}
			set
			{
				this[OrganizationSchema.PublicComputersDetectionEnabled] = value;
			}
		}

		// Token: 0x17001250 RID: 4688
		// (get) Token: 0x06003A30 RID: 14896 RVA: 0x000E08B5 File Offset: 0x000DEAB5
		// (set) Token: 0x06003A31 RID: 14897 RVA: 0x000E08C7 File Offset: 0x000DEAC7
		[Parameter(Mandatory = false)]
		public RmsoSubscriptionStatusFlags RmsoSubscriptionStatus
		{
			get
			{
				return (RmsoSubscriptionStatusFlags)this[OrganizationSchema.RmsoSubscriptionStatus];
			}
			set
			{
				this[OrganizationSchema.RmsoSubscriptionStatus] = (int)value;
			}
		}

		// Token: 0x17001251 RID: 4689
		// (get) Token: 0x06003A32 RID: 14898 RVA: 0x000E08DA File Offset: 0x000DEADA
		// (set) Token: 0x06003A33 RID: 14899 RVA: 0x000E08EC File Offset: 0x000DEAEC
		[Parameter(Mandatory = false)]
		public ReleaseTrack? ReleaseTrack
		{
			get
			{
				return (ReleaseTrack?)this[OrganizationSchema.ReleaseTrack];
			}
			set
			{
				this[OrganizationSchema.ReleaseTrack] = value;
			}
		}

		// Token: 0x17001252 RID: 4690
		// (get) Token: 0x06003A34 RID: 14900 RVA: 0x000E08FF File Offset: 0x000DEAFF
		// (set) Token: 0x06003A35 RID: 14901 RVA: 0x000E0911 File Offset: 0x000DEB11
		[Parameter(Mandatory = false)]
		public Uri SharePointUrl
		{
			get
			{
				return (Uri)this[ADOrganizationConfigSchema.SharePointUrl];
			}
			set
			{
				this[ADOrganizationConfigSchema.SharePointUrl] = value;
			}
		}

		// Token: 0x17001253 RID: 4691
		// (get) Token: 0x06003A36 RID: 14902 RVA: 0x000E091F File Offset: 0x000DEB1F
		// (set) Token: 0x06003A37 RID: 14903 RVA: 0x000E0931 File Offset: 0x000DEB31
		[Parameter(Mandatory = false)]
		public override Uri SiteMailboxCreationURL
		{
			get
			{
				return (Uri)this[OrganizationSchema.SiteMailboxCreationURL];
			}
			set
			{
				this[OrganizationSchema.SiteMailboxCreationURL] = value;
			}
		}

		// Token: 0x17001254 RID: 4692
		// (get) Token: 0x06003A38 RID: 14904 RVA: 0x000E093F File Offset: 0x000DEB3F
		// (set) Token: 0x06003A39 RID: 14905 RVA: 0x000E0947 File Offset: 0x000DEB47
		[Parameter(Mandatory = false)]
		public override bool? CustomerFeedbackEnabled
		{
			get
			{
				return base.CustomerFeedbackEnabled;
			}
			set
			{
				base.CustomerFeedbackEnabled = value;
			}
		}

		// Token: 0x17001255 RID: 4693
		// (get) Token: 0x06003A3A RID: 14906 RVA: 0x000E0950 File Offset: 0x000DEB50
		// (set) Token: 0x06003A3B RID: 14907 RVA: 0x000E0958 File Offset: 0x000DEB58
		[Parameter(Mandatory = false)]
		public override IndustryType Industry
		{
			get
			{
				return base.Industry;
			}
			set
			{
				base.Industry = value;
			}
		}

		// Token: 0x17001256 RID: 4694
		// (get) Token: 0x06003A3C RID: 14908 RVA: 0x000E0961 File Offset: 0x000DEB61
		// (set) Token: 0x06003A3D RID: 14909 RVA: 0x000E0969 File Offset: 0x000DEB69
		[Parameter(Mandatory = false)]
		public override string ManagedFolderHomepage
		{
			get
			{
				return base.ManagedFolderHomepage;
			}
			set
			{
				base.ManagedFolderHomepage = value;
			}
		}

		// Token: 0x17001257 RID: 4695
		// (get) Token: 0x06003A3E RID: 14910 RVA: 0x000E0972 File Offset: 0x000DEB72
		// (set) Token: 0x06003A3F RID: 14911 RVA: 0x000E097A File Offset: 0x000DEB7A
		[Parameter(Mandatory = false)]
		public override EnhancedTimeSpan? DefaultPublicFolderAgeLimit
		{
			get
			{
				return base.DefaultPublicFolderAgeLimit;
			}
			set
			{
				base.DefaultPublicFolderAgeLimit = value;
			}
		}

		// Token: 0x17001258 RID: 4696
		// (get) Token: 0x06003A40 RID: 14912 RVA: 0x000E0983 File Offset: 0x000DEB83
		// (set) Token: 0x06003A41 RID: 14913 RVA: 0x000E098B File Offset: 0x000DEB8B
		[Parameter(Mandatory = false)]
		public override Unlimited<ByteQuantifiedSize> DefaultPublicFolderIssueWarningQuota
		{
			get
			{
				return base.DefaultPublicFolderIssueWarningQuota;
			}
			set
			{
				base.DefaultPublicFolderIssueWarningQuota = value;
			}
		}

		// Token: 0x17001259 RID: 4697
		// (get) Token: 0x06003A42 RID: 14914 RVA: 0x000E0994 File Offset: 0x000DEB94
		// (set) Token: 0x06003A43 RID: 14915 RVA: 0x000E099C File Offset: 0x000DEB9C
		[Parameter(Mandatory = false)]
		public override Unlimited<ByteQuantifiedSize> DefaultPublicFolderProhibitPostQuota
		{
			get
			{
				return base.DefaultPublicFolderProhibitPostQuota;
			}
			set
			{
				base.DefaultPublicFolderProhibitPostQuota = value;
			}
		}

		// Token: 0x1700125A RID: 4698
		// (get) Token: 0x06003A44 RID: 14916 RVA: 0x000E09A5 File Offset: 0x000DEBA5
		// (set) Token: 0x06003A45 RID: 14917 RVA: 0x000E09AD File Offset: 0x000DEBAD
		[Parameter(Mandatory = false)]
		public override Unlimited<ByteQuantifiedSize> DefaultPublicFolderMaxItemSize
		{
			get
			{
				return base.DefaultPublicFolderMaxItemSize;
			}
			set
			{
				base.DefaultPublicFolderMaxItemSize = value;
			}
		}

		// Token: 0x1700125B RID: 4699
		// (get) Token: 0x06003A46 RID: 14918 RVA: 0x000E09B6 File Offset: 0x000DEBB6
		// (set) Token: 0x06003A47 RID: 14919 RVA: 0x000E09BE File Offset: 0x000DEBBE
		[Parameter(Mandatory = false)]
		public override EnhancedTimeSpan? DefaultPublicFolderDeletedItemRetention
		{
			get
			{
				return base.DefaultPublicFolderDeletedItemRetention;
			}
			set
			{
				base.DefaultPublicFolderDeletedItemRetention = value;
			}
		}

		// Token: 0x1700125C RID: 4700
		// (get) Token: 0x06003A48 RID: 14920 RVA: 0x000E09C7 File Offset: 0x000DEBC7
		// (set) Token: 0x06003A49 RID: 14921 RVA: 0x000E09CF File Offset: 0x000DEBCF
		[Parameter(Mandatory = false)]
		public override EnhancedTimeSpan? DefaultPublicFolderMovedItemRetention
		{
			get
			{
				return base.DefaultPublicFolderMovedItemRetention;
			}
			set
			{
				base.DefaultPublicFolderMovedItemRetention = value;
			}
		}

		// Token: 0x1700125D RID: 4701
		// (get) Token: 0x06003A4A RID: 14922 RVA: 0x000E09D8 File Offset: 0x000DEBD8
		// (set) Token: 0x06003A4B RID: 14923 RVA: 0x000E09E0 File Offset: 0x000DEBE0
		[Parameter(Mandatory = false)]
		public override MultiValuedProperty<OrganizationSummaryEntry> OrganizationSummary
		{
			get
			{
				return base.OrganizationSummary;
			}
			set
			{
				base.OrganizationSummary = value;
			}
		}

		// Token: 0x1700125E RID: 4702
		// (get) Token: 0x06003A4C RID: 14924 RVA: 0x000E09E9 File Offset: 0x000DEBE9
		// (set) Token: 0x06003A4D RID: 14925 RVA: 0x000E09F1 File Offset: 0x000DEBF1
		[Parameter(Mandatory = false)]
		public override bool ForwardSyncLiveIdBusinessInstance
		{
			get
			{
				return base.ForwardSyncLiveIdBusinessInstance;
			}
			set
			{
				base.ForwardSyncLiveIdBusinessInstance = value;
			}
		}

		// Token: 0x1700125F RID: 4703
		// (get) Token: 0x06003A4E RID: 14926 RVA: 0x000E09FA File Offset: 0x000DEBFA
		// (set) Token: 0x06003A4F RID: 14927 RVA: 0x000E0A02 File Offset: 0x000DEC02
		[Parameter(Mandatory = false)]
		public override ProxyAddressCollection MicrosoftExchangeRecipientEmailAddresses
		{
			get
			{
				return base.MicrosoftExchangeRecipientEmailAddresses;
			}
			set
			{
				base.MicrosoftExchangeRecipientEmailAddresses = value;
			}
		}

		// Token: 0x17001260 RID: 4704
		// (get) Token: 0x06003A50 RID: 14928 RVA: 0x000E0A0B File Offset: 0x000DEC0B
		// (set) Token: 0x06003A51 RID: 14929 RVA: 0x000E0A13 File Offset: 0x000DEC13
		[Parameter(Mandatory = false)]
		public override SmtpAddress MicrosoftExchangeRecipientPrimarySmtpAddress
		{
			get
			{
				return base.MicrosoftExchangeRecipientPrimarySmtpAddress;
			}
			set
			{
				base.MicrosoftExchangeRecipientPrimarySmtpAddress = value;
			}
		}

		// Token: 0x17001261 RID: 4705
		// (get) Token: 0x06003A52 RID: 14930 RVA: 0x000E0A1C File Offset: 0x000DEC1C
		// (set) Token: 0x06003A53 RID: 14931 RVA: 0x000E0A24 File Offset: 0x000DEC24
		[Parameter(Mandatory = false)]
		public override bool MicrosoftExchangeRecipientEmailAddressPolicyEnabled
		{
			get
			{
				return base.MicrosoftExchangeRecipientEmailAddressPolicyEnabled;
			}
			set
			{
				base.MicrosoftExchangeRecipientEmailAddressPolicyEnabled = value;
			}
		}

		// Token: 0x17001262 RID: 4706
		// (get) Token: 0x06003A54 RID: 14932 RVA: 0x000E0A2D File Offset: 0x000DEC2D
		// (set) Token: 0x06003A55 RID: 14933 RVA: 0x000E0A35 File Offset: 0x000DEC35
		[Parameter(Mandatory = false)]
		public override bool MailTipsExternalRecipientsTipsEnabled
		{
			get
			{
				return base.MailTipsExternalRecipientsTipsEnabled;
			}
			set
			{
				base.MailTipsExternalRecipientsTipsEnabled = value;
			}
		}

		// Token: 0x17001263 RID: 4707
		// (get) Token: 0x06003A56 RID: 14934 RVA: 0x000E0A3E File Offset: 0x000DEC3E
		// (set) Token: 0x06003A57 RID: 14935 RVA: 0x000E0A46 File Offset: 0x000DEC46
		[Parameter(Mandatory = false)]
		public override uint MailTipsLargeAudienceThreshold
		{
			get
			{
				return base.MailTipsLargeAudienceThreshold;
			}
			set
			{
				base.MailTipsLargeAudienceThreshold = value;
			}
		}

		// Token: 0x17001264 RID: 4708
		// (get) Token: 0x06003A58 RID: 14936 RVA: 0x000E0A4F File Offset: 0x000DEC4F
		// (set) Token: 0x06003A59 RID: 14937 RVA: 0x000E0A57 File Offset: 0x000DEC57
		[Parameter(Mandatory = false)]
		public override PublicFoldersDeployment PublicFoldersEnabled
		{
			get
			{
				return base.PublicFoldersEnabled;
			}
			set
			{
				base.PublicFoldersEnabled = value;
			}
		}

		// Token: 0x17001265 RID: 4709
		// (get) Token: 0x06003A5A RID: 14938 RVA: 0x000E0A60 File Offset: 0x000DEC60
		// (set) Token: 0x06003A5B RID: 14939 RVA: 0x000E0A68 File Offset: 0x000DEC68
		[Parameter(Mandatory = false)]
		public override bool MailTipsMailboxSourcedTipsEnabled
		{
			get
			{
				return base.MailTipsMailboxSourcedTipsEnabled;
			}
			set
			{
				base.MailTipsMailboxSourcedTipsEnabled = value;
			}
		}

		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x06003A5C RID: 14940 RVA: 0x000E0A71 File Offset: 0x000DEC71
		// (set) Token: 0x06003A5D RID: 14941 RVA: 0x000E0A79 File Offset: 0x000DEC79
		[Parameter(Mandatory = false)]
		public override bool MailTipsGroupMetricsEnabled
		{
			get
			{
				return base.MailTipsGroupMetricsEnabled;
			}
			set
			{
				base.MailTipsGroupMetricsEnabled = value;
			}
		}

		// Token: 0x17001267 RID: 4711
		// (get) Token: 0x06003A5E RID: 14942 RVA: 0x000E0A82 File Offset: 0x000DEC82
		// (set) Token: 0x06003A5F RID: 14943 RVA: 0x000E0A8A File Offset: 0x000DEC8A
		[Parameter(Mandatory = false)]
		public override bool MailTipsAllTipsEnabled
		{
			get
			{
				return base.MailTipsAllTipsEnabled;
			}
			set
			{
				base.MailTipsAllTipsEnabled = value;
			}
		}

		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x06003A60 RID: 14944 RVA: 0x000E0A93 File Offset: 0x000DEC93
		// (set) Token: 0x06003A61 RID: 14945 RVA: 0x000E0A9B File Offset: 0x000DEC9B
		[Parameter(Mandatory = false)]
		public override bool ReadTrackingEnabled
		{
			get
			{
				return base.ReadTrackingEnabled;
			}
			set
			{
				base.ReadTrackingEnabled = value;
			}
		}

		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x06003A62 RID: 14946 RVA: 0x000E0AA4 File Offset: 0x000DECA4
		// (set) Token: 0x06003A63 RID: 14947 RVA: 0x000E0AAC File Offset: 0x000DECAC
		[Parameter(Mandatory = false)]
		public override MultiValuedProperty<string> DistributionGroupNameBlockedWordsList
		{
			get
			{
				return base.DistributionGroupNameBlockedWordsList;
			}
			set
			{
				base.DistributionGroupNameBlockedWordsList = value;
			}
		}

		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x06003A64 RID: 14948 RVA: 0x000E0AB5 File Offset: 0x000DECB5
		// (set) Token: 0x06003A65 RID: 14949 RVA: 0x000E0ABD File Offset: 0x000DECBD
		[Parameter(Mandatory = false)]
		public override DistributionGroupNamingPolicy DistributionGroupNamingPolicy
		{
			get
			{
				return base.DistributionGroupNamingPolicy;
			}
			set
			{
				base.DistributionGroupNamingPolicy = value;
			}
		}

		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x06003A66 RID: 14950 RVA: 0x000E0AC6 File Offset: 0x000DECC6
		// (set) Token: 0x06003A67 RID: 14951 RVA: 0x000E0ACE File Offset: 0x000DECCE
		[Parameter(Mandatory = false)]
		public override ProtocolConnectionSettings AVAuthenticationService
		{
			get
			{
				return base.AVAuthenticationService;
			}
			set
			{
				base.AVAuthenticationService = value;
			}
		}

		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x06003A68 RID: 14952 RVA: 0x000E0AD7 File Offset: 0x000DECD7
		// (set) Token: 0x06003A69 RID: 14953 RVA: 0x000E0ADF File Offset: 0x000DECDF
		[Parameter(Mandatory = false)]
		public override ProtocolConnectionSettings SIPAccessService
		{
			get
			{
				return base.SIPAccessService;
			}
			set
			{
				base.SIPAccessService = value;
			}
		}

		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x06003A6A RID: 14954 RVA: 0x000E0AE8 File Offset: 0x000DECE8
		// (set) Token: 0x06003A6B RID: 14955 RVA: 0x000E0AF0 File Offset: 0x000DECF0
		[Parameter(Mandatory = false)]
		public override ProtocolConnectionSettings SIPSessionBorderController
		{
			get
			{
				return base.SIPSessionBorderController;
			}
			set
			{
				base.SIPSessionBorderController = value;
			}
		}

		// Token: 0x1700126E RID: 4718
		// (get) Token: 0x06003A6C RID: 14956 RVA: 0x000E0AF9 File Offset: 0x000DECF9
		// (set) Token: 0x06003A6D RID: 14957 RVA: 0x000E0B01 File Offset: 0x000DED01
		[Parameter(Mandatory = false)]
		public override bool ExchangeNotificationEnabled
		{
			get
			{
				return base.ExchangeNotificationEnabled;
			}
			set
			{
				base.ExchangeNotificationEnabled = value;
			}
		}

		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x06003A6E RID: 14958 RVA: 0x000E0B0A File Offset: 0x000DED0A
		// (set) Token: 0x06003A6F RID: 14959 RVA: 0x000E0B12 File Offset: 0x000DED12
		[Parameter(Mandatory = false)]
		public override EnhancedTimeSpan ActivityBasedAuthenticationTimeoutInterval
		{
			get
			{
				return base.ActivityBasedAuthenticationTimeoutInterval;
			}
			set
			{
				base.ActivityBasedAuthenticationTimeoutInterval = value;
			}
		}

		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x06003A70 RID: 14960 RVA: 0x000E0B1B File Offset: 0x000DED1B
		// (set) Token: 0x06003A71 RID: 14961 RVA: 0x000E0B23 File Offset: 0x000DED23
		[Parameter(Mandatory = false)]
		public override bool ActivityBasedAuthenticationTimeoutEnabled
		{
			get
			{
				return base.ActivityBasedAuthenticationTimeoutEnabled;
			}
			set
			{
				base.ActivityBasedAuthenticationTimeoutEnabled = value;
			}
		}

		// Token: 0x17001271 RID: 4721
		// (get) Token: 0x06003A72 RID: 14962 RVA: 0x000E0B2C File Offset: 0x000DED2C
		// (set) Token: 0x06003A73 RID: 14963 RVA: 0x000E0B34 File Offset: 0x000DED34
		[Parameter(Mandatory = false)]
		public override bool ActivityBasedAuthenticationTimeoutWithSingleSignOnEnabled
		{
			get
			{
				return base.ActivityBasedAuthenticationTimeoutWithSingleSignOnEnabled;
			}
			set
			{
				base.ActivityBasedAuthenticationTimeoutWithSingleSignOnEnabled = value;
			}
		}

		// Token: 0x17001272 RID: 4722
		// (get) Token: 0x06003A74 RID: 14964 RVA: 0x000E0B3D File Offset: 0x000DED3D
		// (set) Token: 0x06003A75 RID: 14965 RVA: 0x000E0B48 File Offset: 0x000DED48
		[Parameter(Mandatory = false)]
		public override string WACDiscoveryEndpoint
		{
			get
			{
				return base.WACDiscoveryEndpoint;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					base.WACDiscoveryEndpoint = string.Empty;
					return;
				}
				Uri uri;
				if (!Uri.TryCreate(value, UriKind.Absolute, out uri))
				{
					throw new ArgumentException(DirectoryStrings.WACDiscoveryEndpointShouldBeAbsoluteUri(value));
				}
				base.WACDiscoveryEndpoint = value;
			}
		}

		// Token: 0x17001273 RID: 4723
		// (get) Token: 0x06003A76 RID: 14966 RVA: 0x000E0B8C File Offset: 0x000DED8C
		// (set) Token: 0x06003A77 RID: 14967 RVA: 0x000E0B94 File Offset: 0x000DED94
		[Parameter(Mandatory = false)]
		public override bool IsExcludedFromOnboardMigration
		{
			get
			{
				return base.IsExcludedFromOnboardMigration;
			}
			set
			{
				base.IsExcludedFromOnboardMigration = value;
			}
		}

		// Token: 0x17001274 RID: 4724
		// (get) Token: 0x06003A78 RID: 14968 RVA: 0x000E0B9D File Offset: 0x000DED9D
		// (set) Token: 0x06003A79 RID: 14969 RVA: 0x000E0BA5 File Offset: 0x000DEDA5
		[Parameter(Mandatory = false)]
		public override bool IsExcludedFromOffboardMigration
		{
			get
			{
				return base.IsExcludedFromOffboardMigration;
			}
			set
			{
				base.IsExcludedFromOffboardMigration = value;
			}
		}

		// Token: 0x17001275 RID: 4725
		// (get) Token: 0x06003A7A RID: 14970 RVA: 0x000E0BAE File Offset: 0x000DEDAE
		// (set) Token: 0x06003A7B RID: 14971 RVA: 0x000E0BB6 File Offset: 0x000DEDB6
		[Parameter(Mandatory = false)]
		public override bool IsFfoMigrationInProgress
		{
			get
			{
				return base.IsFfoMigrationInProgress;
			}
			set
			{
				base.IsFfoMigrationInProgress = value;
			}
		}

		// Token: 0x17001276 RID: 4726
		// (get) Token: 0x06003A7C RID: 14972 RVA: 0x000E0BBF File Offset: 0x000DEDBF
		// (set) Token: 0x06003A7D RID: 14973 RVA: 0x000E0BC7 File Offset: 0x000DEDC7
		[Parameter(Mandatory = false)]
		public override bool TenantRelocationsAllowed
		{
			get
			{
				return base.TenantRelocationsAllowed;
			}
			set
			{
				base.TenantRelocationsAllowed = value;
			}
		}

		// Token: 0x17001277 RID: 4727
		// (get) Token: 0x06003A7E RID: 14974 RVA: 0x000E0BD0 File Offset: 0x000DEDD0
		// (set) Token: 0x06003A7F RID: 14975 RVA: 0x000E0BD8 File Offset: 0x000DEDD8
		[Parameter(Mandatory = false)]
		public override Unlimited<int> MaxConcurrentMigrations
		{
			get
			{
				return base.MaxConcurrentMigrations;
			}
			set
			{
				base.MaxConcurrentMigrations = value;
			}
		}

		// Token: 0x17001278 RID: 4728
		// (get) Token: 0x06003A80 RID: 14976 RVA: 0x000E0BE1 File Offset: 0x000DEDE1
		// (set) Token: 0x06003A81 RID: 14977 RVA: 0x000E0BE9 File Offset: 0x000DEDE9
		[Parameter(Mandatory = false)]
		public override bool IsProcessEhaMigratedMessagesEnabled
		{
			get
			{
				return base.IsProcessEhaMigratedMessagesEnabled;
			}
			set
			{
				base.IsProcessEhaMigratedMessagesEnabled = value;
			}
		}

		// Token: 0x17001279 RID: 4729
		// (get) Token: 0x06003A82 RID: 14978 RVA: 0x000E0BF2 File Offset: 0x000DEDF2
		// (set) Token: 0x06003A83 RID: 14979 RVA: 0x000E0BFA File Offset: 0x000DEDFA
		[Parameter(Mandatory = false)]
		public override bool AppsForOfficeEnabled
		{
			get
			{
				return base.AppsForOfficeEnabled;
			}
			set
			{
				base.AppsForOfficeEnabled = value;
			}
		}

		// Token: 0x1700127A RID: 4730
		// (get) Token: 0x06003A84 RID: 14980 RVA: 0x000E0C03 File Offset: 0x000DEE03
		// (set) Token: 0x06003A85 RID: 14981 RVA: 0x000E0C0B File Offset: 0x000DEE0B
		[Parameter(Mandatory = false)]
		public override bool? EwsEnabled
		{
			get
			{
				return base.EwsEnabled;
			}
			set
			{
				base.EwsEnabled = value;
			}
		}

		// Token: 0x1700127B RID: 4731
		// (get) Token: 0x06003A86 RID: 14982 RVA: 0x000E0C14 File Offset: 0x000DEE14
		// (set) Token: 0x06003A87 RID: 14983 RVA: 0x000E0C1C File Offset: 0x000DEE1C
		[Parameter(Mandatory = false)]
		public override bool? EwsAllowOutlook
		{
			get
			{
				return base.EwsAllowOutlook;
			}
			set
			{
				base.EwsAllowOutlook = value;
			}
		}

		// Token: 0x1700127C RID: 4732
		// (get) Token: 0x06003A88 RID: 14984 RVA: 0x000E0C25 File Offset: 0x000DEE25
		// (set) Token: 0x06003A89 RID: 14985 RVA: 0x000E0C2D File Offset: 0x000DEE2D
		[Parameter(Mandatory = false)]
		public override bool? EwsAllowMacOutlook
		{
			get
			{
				return base.EwsAllowMacOutlook;
			}
			set
			{
				base.EwsAllowMacOutlook = value;
			}
		}

		// Token: 0x1700127D RID: 4733
		// (get) Token: 0x06003A8A RID: 14986 RVA: 0x000E0C36 File Offset: 0x000DEE36
		// (set) Token: 0x06003A8B RID: 14987 RVA: 0x000E0C3E File Offset: 0x000DEE3E
		[Parameter(Mandatory = false)]
		public override bool? EwsAllowEntourage
		{
			get
			{
				return base.EwsAllowEntourage;
			}
			set
			{
				base.EwsAllowEntourage = value;
			}
		}

		// Token: 0x1700127E RID: 4734
		// (get) Token: 0x06003A8C RID: 14988 RVA: 0x000E0C47 File Offset: 0x000DEE47
		// (set) Token: 0x06003A8D RID: 14989 RVA: 0x000E0C4F File Offset: 0x000DEE4F
		[Parameter(Mandatory = false)]
		public override EwsApplicationAccessPolicy? EwsApplicationAccessPolicy
		{
			get
			{
				return base.EwsApplicationAccessPolicy;
			}
			set
			{
				base.EwsApplicationAccessPolicy = value;
			}
		}

		// Token: 0x1700127F RID: 4735
		// (get) Token: 0x06003A8E RID: 14990 RVA: 0x000E0C58 File Offset: 0x000DEE58
		// (set) Token: 0x06003A8F RID: 14991 RVA: 0x000E0C60 File Offset: 0x000DEE60
		[Parameter(Mandatory = false)]
		public override MultiValuedProperty<string> EwsAllowList
		{
			get
			{
				return base.EwsAllowList;
			}
			set
			{
				base.EwsAllowList = value;
			}
		}

		// Token: 0x17001280 RID: 4736
		// (get) Token: 0x06003A90 RID: 14992 RVA: 0x000E0C69 File Offset: 0x000DEE69
		// (set) Token: 0x06003A91 RID: 14993 RVA: 0x000E0C71 File Offset: 0x000DEE71
		[Parameter(Mandatory = false)]
		public override MultiValuedProperty<string> EwsBlockList
		{
			get
			{
				return base.EwsBlockList;
			}
			set
			{
				base.EwsBlockList = value;
			}
		}

		// Token: 0x17001281 RID: 4737
		// (get) Token: 0x06003A92 RID: 14994 RVA: 0x000E0C7A File Offset: 0x000DEE7A
		// (set) Token: 0x06003A93 RID: 14995 RVA: 0x000E0C82 File Offset: 0x000DEE82
		[Parameter(Mandatory = false)]
		public override bool CalendarVersionStoreEnabled
		{
			get
			{
				return base.CalendarVersionStoreEnabled;
			}
			set
			{
				base.CalendarVersionStoreEnabled = value;
			}
		}

		// Token: 0x17001282 RID: 4738
		// (get) Token: 0x06003A94 RID: 14996 RVA: 0x000E0C8B File Offset: 0x000DEE8B
		// (set) Token: 0x06003A95 RID: 14997 RVA: 0x000E0C93 File Offset: 0x000DEE93
		[Parameter(Mandatory = false)]
		public override bool IsGuidPrefixedLegacyDnDisabled
		{
			get
			{
				return base.IsGuidPrefixedLegacyDnDisabled;
			}
			set
			{
				base.IsGuidPrefixedLegacyDnDisabled = value;
			}
		}

		// Token: 0x17001283 RID: 4739
		// (get) Token: 0x06003A96 RID: 14998 RVA: 0x000E0C9C File Offset: 0x000DEE9C
		// (set) Token: 0x06003A97 RID: 14999 RVA: 0x000E0CA4 File Offset: 0x000DEEA4
		[Parameter(Mandatory = false)]
		public override MultiValuedProperty<UMLanguage> UMAvailableLanguages
		{
			get
			{
				return base.UMAvailableLanguages;
			}
			set
			{
				base.UMAvailableLanguages = value;
			}
		}

		// Token: 0x17001284 RID: 4740
		// (get) Token: 0x06003A98 RID: 15000 RVA: 0x000E0CAD File Offset: 0x000DEEAD
		// (set) Token: 0x06003A99 RID: 15001 RVA: 0x000E0CB5 File Offset: 0x000DEEB5
		[Parameter(Mandatory = false)]
		public override bool IsMailboxForcedReplicationDisabled
		{
			get
			{
				return base.IsMailboxForcedReplicationDisabled;
			}
			set
			{
				base.IsMailboxForcedReplicationDisabled = value;
			}
		}

		// Token: 0x17001285 RID: 4741
		// (get) Token: 0x06003A9A RID: 15002 RVA: 0x000E0CBE File Offset: 0x000DEEBE
		// (set) Token: 0x06003A9B RID: 15003 RVA: 0x000E0CC6 File Offset: 0x000DEEC6
		[Parameter(Mandatory = false, ParameterSetName = "AdfsAuthenticationRawConfiguration")]
		public override string AdfsAuthenticationConfiguration
		{
			get
			{
				return base.AdfsAuthenticationConfiguration;
			}
			set
			{
				base.AdfsAuthenticationConfiguration = value;
			}
		}

		// Token: 0x17001286 RID: 4742
		// (get) Token: 0x06003A9C RID: 15004 RVA: 0x000E0CCF File Offset: 0x000DEECF
		// (set) Token: 0x06003A9D RID: 15005 RVA: 0x000E0CD7 File Offset: 0x000DEED7
		[Parameter(Mandatory = false)]
		public override int PreferredInternetCodePageForShiftJis
		{
			get
			{
				return base.PreferredInternetCodePageForShiftJis;
			}
			set
			{
				base.PreferredInternetCodePageForShiftJis = value;
			}
		}

		// Token: 0x17001287 RID: 4743
		// (get) Token: 0x06003A9E RID: 15006 RVA: 0x000E0CE0 File Offset: 0x000DEEE0
		// (set) Token: 0x06003A9F RID: 15007 RVA: 0x000E0CE8 File Offset: 0x000DEEE8
		[Parameter(Mandatory = false)]
		public override int RequiredCharsetCoverage
		{
			get
			{
				return base.RequiredCharsetCoverage;
			}
			set
			{
				base.RequiredCharsetCoverage = value;
			}
		}

		// Token: 0x17001288 RID: 4744
		// (get) Token: 0x06003AA0 RID: 15008 RVA: 0x000E0CF1 File Offset: 0x000DEEF1
		// (set) Token: 0x06003AA1 RID: 15009 RVA: 0x000E0CF9 File Offset: 0x000DEEF9
		[Parameter(Mandatory = false)]
		public override int ByteEncoderTypeFor7BitCharsets
		{
			get
			{
				return base.ByteEncoderTypeFor7BitCharsets;
			}
			set
			{
				base.ByteEncoderTypeFor7BitCharsets = value;
			}
		}

		// Token: 0x17001289 RID: 4745
		// (get) Token: 0x06003AA2 RID: 15010 RVA: 0x000E0D02 File Offset: 0x000DEF02
		// (set) Token: 0x06003AA3 RID: 15011 RVA: 0x000E0D0A File Offset: 0x000DEF0A
		[Parameter(Mandatory = false, ParameterSetName = "AdfsAuthenticationParameter")]
		public override Uri AdfsIssuer
		{
			get
			{
				return base.AdfsIssuer;
			}
			set
			{
				base.AdfsIssuer = value;
			}
		}

		// Token: 0x1700128A RID: 4746
		// (get) Token: 0x06003AA4 RID: 15012 RVA: 0x000E0D13 File Offset: 0x000DEF13
		// (set) Token: 0x06003AA5 RID: 15013 RVA: 0x000E0D1B File Offset: 0x000DEF1B
		[Parameter(Mandatory = false, ParameterSetName = "AdfsAuthenticationParameter")]
		public override MultiValuedProperty<Uri> AdfsAudienceUris
		{
			get
			{
				return base.AdfsAudienceUris;
			}
			set
			{
				base.AdfsAudienceUris = value;
			}
		}

		// Token: 0x1700128B RID: 4747
		// (get) Token: 0x06003AA6 RID: 15014 RVA: 0x000E0D24 File Offset: 0x000DEF24
		// (set) Token: 0x06003AA7 RID: 15015 RVA: 0x000E0D2C File Offset: 0x000DEF2C
		[Parameter(Mandatory = false, ParameterSetName = "AdfsAuthenticationParameter")]
		public override MultiValuedProperty<string> AdfsSignCertificateThumbprints
		{
			get
			{
				return base.AdfsSignCertificateThumbprints;
			}
			set
			{
				base.AdfsSignCertificateThumbprints = value;
			}
		}

		// Token: 0x1700128C RID: 4748
		// (get) Token: 0x06003AA8 RID: 15016 RVA: 0x000E0D35 File Offset: 0x000DEF35
		// (set) Token: 0x06003AA9 RID: 15017 RVA: 0x000E0D3D File Offset: 0x000DEF3D
		[Parameter(Mandatory = false, ParameterSetName = "AdfsAuthenticationParameter")]
		public override string AdfsEncryptCertificateThumbprint
		{
			get
			{
				return base.AdfsEncryptCertificateThumbprint;
			}
			set
			{
				base.AdfsEncryptCertificateThumbprint = value;
			}
		}

		// Token: 0x1700128D RID: 4749
		// (get) Token: 0x06003AAA RID: 15018 RVA: 0x000E0D46 File Offset: 0x000DEF46
		// (set) Token: 0x06003AAB RID: 15019 RVA: 0x000E0D4E File Offset: 0x000DEF4E
		[Parameter(Mandatory = false)]
		public override bool IsSyncPropertySetUpgradeAllowed
		{
			get
			{
				return base.IsSyncPropertySetUpgradeAllowed;
			}
			set
			{
				base.IsSyncPropertySetUpgradeAllowed = value;
			}
		}

		// Token: 0x1700128E RID: 4750
		// (get) Token: 0x06003AAC RID: 15020 RVA: 0x000E0D57 File Offset: 0x000DEF57
		// (set) Token: 0x06003AAD RID: 15021 RVA: 0x000E0D5F File Offset: 0x000DEF5F
		[Parameter(Mandatory = false)]
		public override bool MapiHttpEnabled
		{
			get
			{
				return base.MapiHttpEnabled;
			}
			set
			{
				base.MapiHttpEnabled = value;
			}
		}

		// Token: 0x1700128F RID: 4751
		// (get) Token: 0x06003AAE RID: 15022 RVA: 0x000E0D68 File Offset: 0x000DEF68
		// (set) Token: 0x06003AAF RID: 15023 RVA: 0x000E0D70 File Offset: 0x000DEF70
		[Parameter(Mandatory = false)]
		public override bool OAuth2ClientProfileEnabled
		{
			get
			{
				return base.OAuth2ClientProfileEnabled;
			}
			set
			{
				base.OAuth2ClientProfileEnabled = value;
			}
		}

		// Token: 0x17001290 RID: 4752
		// (get) Token: 0x06003AB0 RID: 15024 RVA: 0x000E0D79 File Offset: 0x000DEF79
		// (set) Token: 0x06003AB1 RID: 15025 RVA: 0x000E0D81 File Offset: 0x000DEF81
		[Parameter(Mandatory = false)]
		public override bool IntuneManagedStatus
		{
			get
			{
				return base.IntuneManagedStatus;
			}
			set
			{
				base.IntuneManagedStatus = value;
			}
		}

		// Token: 0x17001291 RID: 4753
		// (get) Token: 0x06003AB2 RID: 15026 RVA: 0x000E0D8A File Offset: 0x000DEF8A
		// (set) Token: 0x06003AB3 RID: 15027 RVA: 0x000E0D9C File Offset: 0x000DEF9C
		[Parameter(Mandatory = false)]
		public HybridConfigurationStatusFlags HybridConfigurationStatus
		{
			get
			{
				return (HybridConfigurationStatusFlags)this[OrganizationSchema.HybridConfigurationStatus];
			}
			set
			{
				this[OrganizationSchema.HybridConfigurationStatus] = (int)value;
			}
		}

		// Token: 0x17001292 RID: 4754
		// (get) Token: 0x06003AB4 RID: 15028 RVA: 0x000E0DAF File Offset: 0x000DEFAF
		// (set) Token: 0x06003AB5 RID: 15029 RVA: 0x000E0DC1 File Offset: 0x000DEFC1
		[Parameter(Mandatory = false)]
		public override bool ACLableSyncedObjectEnabled
		{
			get
			{
				return (bool)this[OrganizationSchema.ACLableSyncedObjectEnabled];
			}
			set
			{
				this[OrganizationSchema.ACLableSyncedObjectEnabled] = value;
			}
		}

		// Token: 0x040027D3 RID: 10195
		private const string AdfsAuthenticationRawConfiguration = "AdfsAuthenticationRawConfiguration";

		// Token: 0x040027D4 RID: 10196
		private const string AdfsAuthenticationParameter = "AdfsAuthenticationParameter";

		// Token: 0x040027D5 RID: 10197
		private static readonly ADOrganizationConfigSchema schema = ObjectSchema.GetInstance<ADOrganizationConfigSchema>();
	}
}
