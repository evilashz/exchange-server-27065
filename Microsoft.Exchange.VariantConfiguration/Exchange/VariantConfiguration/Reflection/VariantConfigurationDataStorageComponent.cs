using System;
using Microsoft.Exchange.Calendar;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000101 RID: 257
	public sealed class VariantConfigurationDataStorageComponent : VariantConfigurationComponent
	{
		// Token: 0x06000BA6 RID: 2982 RVA: 0x0001B91C File Offset: 0x00019B1C
		internal VariantConfigurationDataStorageComponent() : base("DataStorage")
		{
			base.Add(new VariantConfigurationSection("DataStorage.settings.ini", "CheckForRemoteConnections", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("DataStorage.settings.ini", "PeopleCentricConversation", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("DataStorage.settings.ini", "UseOfflineRms", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("DataStorage.settings.ini", "CalendarUpgrade", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("DataStorage.settings.ini", "IgnoreInessentialMetaDataLoadErrors", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("DataStorage.settings.ini", "ModernMailInfra", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("DataStorage.settings.ini", "CalendarView", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("DataStorage.settings.ini", "GroupsForOlkDesktop", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("DataStorage.settings.ini", "FindOrgMailboxInMultiTenantEnvironment", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("DataStorage.settings.ini", "DeleteGroupConversation", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("DataStorage.settings.ini", "ModernConversationPrep", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("DataStorage.settings.ini", "CheckLicense", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("DataStorage.settings.ini", "LoadHostedMailboxLimits", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("DataStorage.settings.ini", "RepresentRemoteMailbox", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("DataStorage.settings.ini", "CalendarUpgradeSettings", typeof(ICalendarUpgradeSettings), false));
			base.Add(new VariantConfigurationSection("DataStorage.settings.ini", "CrossPremiseDelegate", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("DataStorage.settings.ini", "CalendarIcalConversionSettings", typeof(ICalendarIcalConversionSettings), false));
			base.Add(new VariantConfigurationSection("DataStorage.settings.ini", "CalendarViewPropertyRule", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("DataStorage.settings.ini", "CheckR3Coexistence", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("DataStorage.settings.ini", "XOWAConsumerSharing", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("DataStorage.settings.ini", "UserConfigurationAggregation", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("DataStorage.settings.ini", "StorageAttachmentImageAnalysis", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("DataStorage.settings.ini", "LogIpEndpoints", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("DataStorage.settings.ini", "CheckExternalAccess", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("DataStorage.settings.ini", "ThreadedConversation", typeof(IFeature), false));
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x0001BC54 File Offset: 0x00019E54
		public VariantConfigurationSection CheckForRemoteConnections
		{
			get
			{
				return base["CheckForRemoteConnections"];
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0001BC61 File Offset: 0x00019E61
		public VariantConfigurationSection PeopleCentricConversation
		{
			get
			{
				return base["PeopleCentricConversation"];
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x0001BC6E File Offset: 0x00019E6E
		public VariantConfigurationSection UseOfflineRms
		{
			get
			{
				return base["UseOfflineRms"];
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x0001BC7B File Offset: 0x00019E7B
		public VariantConfigurationSection CalendarUpgrade
		{
			get
			{
				return base["CalendarUpgrade"];
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x0001BC88 File Offset: 0x00019E88
		public VariantConfigurationSection IgnoreInessentialMetaDataLoadErrors
		{
			get
			{
				return base["IgnoreInessentialMetaDataLoadErrors"];
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x0001BC95 File Offset: 0x00019E95
		public VariantConfigurationSection ModernMailInfra
		{
			get
			{
				return base["ModernMailInfra"];
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x0001BCA2 File Offset: 0x00019EA2
		public VariantConfigurationSection CalendarView
		{
			get
			{
				return base["CalendarView"];
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x0001BCAF File Offset: 0x00019EAF
		public VariantConfigurationSection GroupsForOlkDesktop
		{
			get
			{
				return base["GroupsForOlkDesktop"];
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x0001BCBC File Offset: 0x00019EBC
		public VariantConfigurationSection FindOrgMailboxInMultiTenantEnvironment
		{
			get
			{
				return base["FindOrgMailboxInMultiTenantEnvironment"];
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x0001BCC9 File Offset: 0x00019EC9
		public VariantConfigurationSection DeleteGroupConversation
		{
			get
			{
				return base["DeleteGroupConversation"];
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x0001BCD6 File Offset: 0x00019ED6
		public VariantConfigurationSection ModernConversationPrep
		{
			get
			{
				return base["ModernConversationPrep"];
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x0001BCE3 File Offset: 0x00019EE3
		public VariantConfigurationSection CheckLicense
		{
			get
			{
				return base["CheckLicense"];
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x0001BCF0 File Offset: 0x00019EF0
		public VariantConfigurationSection LoadHostedMailboxLimits
		{
			get
			{
				return base["LoadHostedMailboxLimits"];
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x0001BCFD File Offset: 0x00019EFD
		public VariantConfigurationSection RepresentRemoteMailbox
		{
			get
			{
				return base["RepresentRemoteMailbox"];
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x0001BD0A File Offset: 0x00019F0A
		public VariantConfigurationSection CalendarUpgradeSettings
		{
			get
			{
				return base["CalendarUpgradeSettings"];
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x0001BD17 File Offset: 0x00019F17
		public VariantConfigurationSection CrossPremiseDelegate
		{
			get
			{
				return base["CrossPremiseDelegate"];
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x0001BD24 File Offset: 0x00019F24
		public VariantConfigurationSection CalendarIcalConversionSettings
		{
			get
			{
				return base["CalendarIcalConversionSettings"];
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x0001BD31 File Offset: 0x00019F31
		public VariantConfigurationSection CalendarViewPropertyRule
		{
			get
			{
				return base["CalendarViewPropertyRule"];
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x0001BD3E File Offset: 0x00019F3E
		public VariantConfigurationSection CheckR3Coexistence
		{
			get
			{
				return base["CheckR3Coexistence"];
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x0001BD4B File Offset: 0x00019F4B
		public VariantConfigurationSection XOWAConsumerSharing
		{
			get
			{
				return base["XOWAConsumerSharing"];
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0001BD58 File Offset: 0x00019F58
		public VariantConfigurationSection UserConfigurationAggregation
		{
			get
			{
				return base["UserConfigurationAggregation"];
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x0001BD65 File Offset: 0x00019F65
		public VariantConfigurationSection StorageAttachmentImageAnalysis
		{
			get
			{
				return base["StorageAttachmentImageAnalysis"];
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x0001BD72 File Offset: 0x00019F72
		public VariantConfigurationSection LogIpEndpoints
		{
			get
			{
				return base["LogIpEndpoints"];
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0001BD7F File Offset: 0x00019F7F
		public VariantConfigurationSection CheckExternalAccess
		{
			get
			{
				return base["CheckExternalAccess"];
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x0001BD8C File Offset: 0x00019F8C
		public VariantConfigurationSection ThreadedConversation
		{
			get
			{
				return base["ThreadedConversation"];
			}
		}
	}
}
