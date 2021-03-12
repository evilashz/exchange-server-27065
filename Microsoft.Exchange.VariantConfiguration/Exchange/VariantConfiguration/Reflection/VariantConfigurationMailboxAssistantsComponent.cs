using System;
using Microsoft.Exchange.Assistants;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x0200010E RID: 270
	public sealed class VariantConfigurationMailboxAssistantsComponent : VariantConfigurationComponent
	{
		// Token: 0x06000C53 RID: 3155 RVA: 0x0001D660 File Offset: 0x0001B860
		internal VariantConfigurationMailboxAssistantsComponent() : base("MailboxAssistants")
		{
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "FlagPlus", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "ApprovalAssistantCheckRateLimit", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "StoreUrgentMaintenanceAssistant", typeof(IMailboxAssistantSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "SharePointSignalStoreAssistant", typeof(IMailboxAssistantSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "StoreOnlineIntegrityCheckAssistant", typeof(IMailboxAssistantSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "DirectoryProcessorTenantLogging", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "CalendarRepairAssistantLogging", typeof(ICalendarRepairLoggerSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "CalendarNotificationAssistantSkipUserSettingsUpdate", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "ElcAssistantTryProcessEhaMigratedMessages", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "InferenceDataCollectionAssistant", typeof(IMailboxAssistantSettings), false));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "SearchIndexRepairAssistant", typeof(IMailboxAssistantSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "TimeBasedAssistantsMonitoring", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "TestTBA", typeof(IMailboxAssistantSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "OrgMailboxCheckScaleRequirements", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "PeopleRelevanceAssistant", typeof(IMailboxAssistantSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "PublicFolderAssistant", typeof(IMailboxAssistantSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "ElcAssistant", typeof(IMailboxAssistantSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "ElcRemoteArchive", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "PublicFolderSplit", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "InferenceTrainingAssistant", typeof(IMailboxAssistantSettings), false));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "SharePointSignalStore", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "ProbeTimeBasedAssistant", typeof(IMailboxAssistantSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "SharePointSignalStoreInDatacenter", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "GenerateGroupPhoto", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "StoreMaintenanceAssistant", typeof(IMailboxAssistantSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "CalendarSyncAssistant", typeof(IMailboxAssistantSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "EmailReminders", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "DelegateRulesLogger", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "OABGeneratorAssistant", typeof(IMailboxAssistantSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "StoreScheduledIntegrityCheckAssistant", typeof(IMailboxAssistantSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "MwiAssistantGetUMEnabledUsersFromDatacenter", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "MailboxProcessorAssistant", typeof(IMailboxAssistantSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "PeopleCentricTriageAssistant", typeof(IMailboxAssistantSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "MailboxAssistantService", typeof(IMailboxAssistantServiceSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "ElcAssistantApplyLitigationHoldDuration", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "TopNWordsAssistant", typeof(IMailboxAssistantSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "SiteMailboxAssistant", typeof(IMailboxAssistantSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "UMReportingAssistant", typeof(IMailboxAssistantSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "DarTaskStoreAssistant", typeof(IMailboxAssistantSettings), false));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "GroupMailboxAssistant", typeof(IMailboxAssistantSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "QuickCapture", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "JunkEmailOptionsCommitterAssistant", typeof(IMailboxAssistantSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "DirectoryProcessorAssistant", typeof(IMailboxAssistantSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "CalendarRepairAssistant", typeof(IMailboxAssistantSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "MailboxAssociationReplicationAssistant", typeof(IMailboxAssistantSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "StoreDSMaintenanceAssistant", typeof(IMailboxAssistantSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "ElcAssistantDiscoveryHoldSynchronizer", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "SharingPolicyAssistant", typeof(IMailboxAssistantSettings), true));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "ElcAssistantAlwaysProcessMailbox", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "CalendarRepairAssistantReliabilityLogger", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "UnifiedPolicyHold", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("MailboxAssistants.settings.ini", "PerformRecipientDLExpansion", typeof(IFeature), false));
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x0001DCF8 File Offset: 0x0001BEF8
		public VariantConfigurationSection FlagPlus
		{
			get
			{
				return base["FlagPlus"];
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x0001DD05 File Offset: 0x0001BF05
		public VariantConfigurationSection ApprovalAssistantCheckRateLimit
		{
			get
			{
				return base["ApprovalAssistantCheckRateLimit"];
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x0001DD12 File Offset: 0x0001BF12
		public VariantConfigurationSection StoreUrgentMaintenanceAssistant
		{
			get
			{
				return base["StoreUrgentMaintenanceAssistant"];
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x0001DD1F File Offset: 0x0001BF1F
		public VariantConfigurationSection SharePointSignalStoreAssistant
		{
			get
			{
				return base["SharePointSignalStoreAssistant"];
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x0001DD2C File Offset: 0x0001BF2C
		public VariantConfigurationSection StoreOnlineIntegrityCheckAssistant
		{
			get
			{
				return base["StoreOnlineIntegrityCheckAssistant"];
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x0001DD39 File Offset: 0x0001BF39
		public VariantConfigurationSection DirectoryProcessorTenantLogging
		{
			get
			{
				return base["DirectoryProcessorTenantLogging"];
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x0001DD46 File Offset: 0x0001BF46
		public VariantConfigurationSection CalendarRepairAssistantLogging
		{
			get
			{
				return base["CalendarRepairAssistantLogging"];
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x0001DD53 File Offset: 0x0001BF53
		public VariantConfigurationSection CalendarNotificationAssistantSkipUserSettingsUpdate
		{
			get
			{
				return base["CalendarNotificationAssistantSkipUserSettingsUpdate"];
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x0001DD60 File Offset: 0x0001BF60
		public VariantConfigurationSection ElcAssistantTryProcessEhaMigratedMessages
		{
			get
			{
				return base["ElcAssistantTryProcessEhaMigratedMessages"];
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x0001DD6D File Offset: 0x0001BF6D
		public VariantConfigurationSection InferenceDataCollectionAssistant
		{
			get
			{
				return base["InferenceDataCollectionAssistant"];
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x0001DD7A File Offset: 0x0001BF7A
		public VariantConfigurationSection SearchIndexRepairAssistant
		{
			get
			{
				return base["SearchIndexRepairAssistant"];
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x0001DD87 File Offset: 0x0001BF87
		public VariantConfigurationSection TimeBasedAssistantsMonitoring
		{
			get
			{
				return base["TimeBasedAssistantsMonitoring"];
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x0001DD94 File Offset: 0x0001BF94
		public VariantConfigurationSection TestTBA
		{
			get
			{
				return base["TestTBA"];
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x0001DDA1 File Offset: 0x0001BFA1
		public VariantConfigurationSection OrgMailboxCheckScaleRequirements
		{
			get
			{
				return base["OrgMailboxCheckScaleRequirements"];
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x0001DDAE File Offset: 0x0001BFAE
		public VariantConfigurationSection PeopleRelevanceAssistant
		{
			get
			{
				return base["PeopleRelevanceAssistant"];
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0001DDBB File Offset: 0x0001BFBB
		public VariantConfigurationSection PublicFolderAssistant
		{
			get
			{
				return base["PublicFolderAssistant"];
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x0001DDC8 File Offset: 0x0001BFC8
		public VariantConfigurationSection ElcAssistant
		{
			get
			{
				return base["ElcAssistant"];
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x0001DDD5 File Offset: 0x0001BFD5
		public VariantConfigurationSection ElcRemoteArchive
		{
			get
			{
				return base["ElcRemoteArchive"];
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x0001DDE2 File Offset: 0x0001BFE2
		public VariantConfigurationSection PublicFolderSplit
		{
			get
			{
				return base["PublicFolderSplit"];
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x0001DDEF File Offset: 0x0001BFEF
		public VariantConfigurationSection InferenceTrainingAssistant
		{
			get
			{
				return base["InferenceTrainingAssistant"];
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x0001DDFC File Offset: 0x0001BFFC
		public VariantConfigurationSection SharePointSignalStore
		{
			get
			{
				return base["SharePointSignalStore"];
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x0001DE09 File Offset: 0x0001C009
		public VariantConfigurationSection ProbeTimeBasedAssistant
		{
			get
			{
				return base["ProbeTimeBasedAssistant"];
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x0001DE16 File Offset: 0x0001C016
		public VariantConfigurationSection SharePointSignalStoreInDatacenter
		{
			get
			{
				return base["SharePointSignalStoreInDatacenter"];
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x0001DE23 File Offset: 0x0001C023
		public VariantConfigurationSection GenerateGroupPhoto
		{
			get
			{
				return base["GenerateGroupPhoto"];
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x0001DE30 File Offset: 0x0001C030
		public VariantConfigurationSection StoreMaintenanceAssistant
		{
			get
			{
				return base["StoreMaintenanceAssistant"];
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x0001DE3D File Offset: 0x0001C03D
		public VariantConfigurationSection CalendarSyncAssistant
		{
			get
			{
				return base["CalendarSyncAssistant"];
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x0001DE4A File Offset: 0x0001C04A
		public VariantConfigurationSection EmailReminders
		{
			get
			{
				return base["EmailReminders"];
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x0001DE57 File Offset: 0x0001C057
		public VariantConfigurationSection DelegateRulesLogger
		{
			get
			{
				return base["DelegateRulesLogger"];
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x0001DE64 File Offset: 0x0001C064
		public VariantConfigurationSection OABGeneratorAssistant
		{
			get
			{
				return base["OABGeneratorAssistant"];
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x0001DE71 File Offset: 0x0001C071
		public VariantConfigurationSection StoreScheduledIntegrityCheckAssistant
		{
			get
			{
				return base["StoreScheduledIntegrityCheckAssistant"];
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x0001DE7E File Offset: 0x0001C07E
		public VariantConfigurationSection MwiAssistantGetUMEnabledUsersFromDatacenter
		{
			get
			{
				return base["MwiAssistantGetUMEnabledUsersFromDatacenter"];
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x0001DE8B File Offset: 0x0001C08B
		public VariantConfigurationSection MailboxProcessorAssistant
		{
			get
			{
				return base["MailboxProcessorAssistant"];
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0001DE98 File Offset: 0x0001C098
		public VariantConfigurationSection PeopleCentricTriageAssistant
		{
			get
			{
				return base["PeopleCentricTriageAssistant"];
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x0001DEA5 File Offset: 0x0001C0A5
		public VariantConfigurationSection MailboxAssistantService
		{
			get
			{
				return base["MailboxAssistantService"];
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0001DEB2 File Offset: 0x0001C0B2
		public VariantConfigurationSection ElcAssistantApplyLitigationHoldDuration
		{
			get
			{
				return base["ElcAssistantApplyLitigationHoldDuration"];
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x0001DEBF File Offset: 0x0001C0BF
		public VariantConfigurationSection TopNWordsAssistant
		{
			get
			{
				return base["TopNWordsAssistant"];
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x0001DECC File Offset: 0x0001C0CC
		public VariantConfigurationSection SiteMailboxAssistant
		{
			get
			{
				return base["SiteMailboxAssistant"];
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x0001DED9 File Offset: 0x0001C0D9
		public VariantConfigurationSection UMReportingAssistant
		{
			get
			{
				return base["UMReportingAssistant"];
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x0001DEE6 File Offset: 0x0001C0E6
		public VariantConfigurationSection DarTaskStoreAssistant
		{
			get
			{
				return base["DarTaskStoreAssistant"];
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x0001DEF3 File Offset: 0x0001C0F3
		public VariantConfigurationSection GroupMailboxAssistant
		{
			get
			{
				return base["GroupMailboxAssistant"];
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x0001DF00 File Offset: 0x0001C100
		public VariantConfigurationSection QuickCapture
		{
			get
			{
				return base["QuickCapture"];
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x0001DF0D File Offset: 0x0001C10D
		public VariantConfigurationSection JunkEmailOptionsCommitterAssistant
		{
			get
			{
				return base["JunkEmailOptionsCommitterAssistant"];
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0001DF1A File Offset: 0x0001C11A
		public VariantConfigurationSection DirectoryProcessorAssistant
		{
			get
			{
				return base["DirectoryProcessorAssistant"];
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x0001DF27 File Offset: 0x0001C127
		public VariantConfigurationSection CalendarRepairAssistant
		{
			get
			{
				return base["CalendarRepairAssistant"];
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0001DF34 File Offset: 0x0001C134
		public VariantConfigurationSection MailboxAssociationReplicationAssistant
		{
			get
			{
				return base["MailboxAssociationReplicationAssistant"];
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x0001DF41 File Offset: 0x0001C141
		public VariantConfigurationSection StoreDSMaintenanceAssistant
		{
			get
			{
				return base["StoreDSMaintenanceAssistant"];
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0001DF4E File Offset: 0x0001C14E
		public VariantConfigurationSection ElcAssistantDiscoveryHoldSynchronizer
		{
			get
			{
				return base["ElcAssistantDiscoveryHoldSynchronizer"];
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x0001DF5B File Offset: 0x0001C15B
		public VariantConfigurationSection SharingPolicyAssistant
		{
			get
			{
				return base["SharingPolicyAssistant"];
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0001DF68 File Offset: 0x0001C168
		public VariantConfigurationSection ElcAssistantAlwaysProcessMailbox
		{
			get
			{
				return base["ElcAssistantAlwaysProcessMailbox"];
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x0001DF75 File Offset: 0x0001C175
		public VariantConfigurationSection CalendarRepairAssistantReliabilityLogger
		{
			get
			{
				return base["CalendarRepairAssistantReliabilityLogger"];
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0001DF82 File Offset: 0x0001C182
		public VariantConfigurationSection UnifiedPolicyHold
		{
			get
			{
				return base["UnifiedPolicyHold"];
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x0001DF8F File Offset: 0x0001C18F
		public VariantConfigurationSection PerformRecipientDLExpansion
		{
			get
			{
				return base["PerformRecipientDLExpansion"];
			}
		}
	}
}
