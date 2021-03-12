using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x0200010D RID: 269
	public sealed class VariantConfigurationIpaedComponent : VariantConfigurationComponent
	{
		// Token: 0x06000C40 RID: 3136 RVA: 0x0001D31C File Offset: 0x0001B51C
		internal VariantConfigurationIpaedComponent() : base("Ipaed")
		{
			base.Add(new VariantConfigurationSection("Ipaed.settings.ini", "ProcessedByUnjournal", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ipaed.settings.ini", "ProcessForestWideOrgJournal", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ipaed.settings.ini", "MoveDeletionsToPurges", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ipaed.settings.ini", "InternalJournaling", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ipaed.settings.ini", "IncreaseQuotaForOnHoldMailboxes", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ipaed.settings.ini", "AdminAuditLocalQueue", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ipaed.settings.ini", "AdminAuditCmdletBlockList", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ipaed.settings.ini", "AdminAuditEventLogThrottling", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ipaed.settings.ini", "AuditConfigFromUCCPolicy", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ipaed.settings.ini", "PartitionedMailboxAuditLogs", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ipaed.settings.ini", "MailboxAuditLocalQueue", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ipaed.settings.ini", "RemoveMailboxFromJournalRecipients", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ipaed.settings.ini", "MoveClearNrn", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ipaed.settings.ini", "FolderBindExtendedThrottling", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ipaed.settings.ini", "PartitionedAdminAuditLogs", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ipaed.settings.ini", "AdminAuditExternalAccessCheckOnDedicated", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ipaed.settings.ini", "LegacyJournaling", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Ipaed.settings.ini", "EHAJournaling", typeof(IFeature), false));
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x0001D574 File Offset: 0x0001B774
		public VariantConfigurationSection ProcessedByUnjournal
		{
			get
			{
				return base["ProcessedByUnjournal"];
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x0001D581 File Offset: 0x0001B781
		public VariantConfigurationSection ProcessForestWideOrgJournal
		{
			get
			{
				return base["ProcessForestWideOrgJournal"];
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x0001D58E File Offset: 0x0001B78E
		public VariantConfigurationSection MoveDeletionsToPurges
		{
			get
			{
				return base["MoveDeletionsToPurges"];
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x0001D59B File Offset: 0x0001B79B
		public VariantConfigurationSection InternalJournaling
		{
			get
			{
				return base["InternalJournaling"];
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x0001D5A8 File Offset: 0x0001B7A8
		public VariantConfigurationSection IncreaseQuotaForOnHoldMailboxes
		{
			get
			{
				return base["IncreaseQuotaForOnHoldMailboxes"];
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x0001D5B5 File Offset: 0x0001B7B5
		public VariantConfigurationSection AdminAuditLocalQueue
		{
			get
			{
				return base["AdminAuditLocalQueue"];
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x0001D5C2 File Offset: 0x0001B7C2
		public VariantConfigurationSection AdminAuditCmdletBlockList
		{
			get
			{
				return base["AdminAuditCmdletBlockList"];
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x0001D5CF File Offset: 0x0001B7CF
		public VariantConfigurationSection AdminAuditEventLogThrottling
		{
			get
			{
				return base["AdminAuditEventLogThrottling"];
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x0001D5DC File Offset: 0x0001B7DC
		public VariantConfigurationSection AuditConfigFromUCCPolicy
		{
			get
			{
				return base["AuditConfigFromUCCPolicy"];
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x0001D5E9 File Offset: 0x0001B7E9
		public VariantConfigurationSection PartitionedMailboxAuditLogs
		{
			get
			{
				return base["PartitionedMailboxAuditLogs"];
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x0001D5F6 File Offset: 0x0001B7F6
		public VariantConfigurationSection MailboxAuditLocalQueue
		{
			get
			{
				return base["MailboxAuditLocalQueue"];
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x0001D603 File Offset: 0x0001B803
		public VariantConfigurationSection RemoveMailboxFromJournalRecipients
		{
			get
			{
				return base["RemoveMailboxFromJournalRecipients"];
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x0001D610 File Offset: 0x0001B810
		public VariantConfigurationSection MoveClearNrn
		{
			get
			{
				return base["MoveClearNrn"];
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x0001D61D File Offset: 0x0001B81D
		public VariantConfigurationSection FolderBindExtendedThrottling
		{
			get
			{
				return base["FolderBindExtendedThrottling"];
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x0001D62A File Offset: 0x0001B82A
		public VariantConfigurationSection PartitionedAdminAuditLogs
		{
			get
			{
				return base["PartitionedAdminAuditLogs"];
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x0001D637 File Offset: 0x0001B837
		public VariantConfigurationSection AdminAuditExternalAccessCheckOnDedicated
		{
			get
			{
				return base["AdminAuditExternalAccessCheckOnDedicated"];
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x0001D644 File Offset: 0x0001B844
		public VariantConfigurationSection LegacyJournaling
		{
			get
			{
				return base["LegacyJournaling"];
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x0001D651 File Offset: 0x0001B851
		public VariantConfigurationSection EHAJournaling
		{
			get
			{
				return base["EHAJournaling"];
			}
		}
	}
}
