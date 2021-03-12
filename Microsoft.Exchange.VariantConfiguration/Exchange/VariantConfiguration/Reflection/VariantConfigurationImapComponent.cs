using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x0200010B RID: 267
	public sealed class VariantConfigurationImapComponent : VariantConfigurationComponent
	{
		// Token: 0x06000C1E RID: 3102 RVA: 0x0001CD48 File Offset: 0x0001AF48
		internal VariantConfigurationImapComponent() : base("Imap")
		{
			base.Add(new VariantConfigurationSection("Imap.settings.ini", "RfcIDImap", typeof(IFeature), true));
			base.Add(new VariantConfigurationSection("Imap.settings.ini", "IgnoreNonProvisionedServers", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Imap.settings.ini", "UseSamAccountNameAsUsername", typeof(IFeature), true));
			base.Add(new VariantConfigurationSection("Imap.settings.ini", "SkipAuthOnCafe", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Imap.settings.ini", "AllowPlainTextConversionWithoutUsingSkeleton", typeof(IFeature), true));
			base.Add(new VariantConfigurationSection("Imap.settings.ini", "RfcIDImapCafe", typeof(IFeature), true));
			base.Add(new VariantConfigurationSection("Imap.settings.ini", "GlobalCriminalCompliance", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Imap.settings.ini", "CheckOnlyAuthenticationStatus", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Imap.settings.ini", "RfcMoveImap", typeof(IFeature), true));
			base.Add(new VariantConfigurationSection("Imap.settings.ini", "RefreshSearchFolderItems", typeof(IFeature), true));
			base.Add(new VariantConfigurationSection("Imap.settings.ini", "ReloadMailboxBeforeGettingSubscriptionList", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Imap.settings.ini", "EnforceLogsRetentionPolicy", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Imap.settings.ini", "AppendServerNameInBanner", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Imap.settings.ini", "UsePrimarySmtpAddress", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Imap.settings.ini", "ImapClientAccessRulesEnabled", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Imap.settings.ini", "DontReturnLastMessageForUInt32MaxValue", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Imap.settings.ini", "LrsLogging", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Imap.settings.ini", "AllowKerberosAuth", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Imap.settings.ini", "RfcMoveImapCafe", typeof(IFeature), true));
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x0001CFC0 File Offset: 0x0001B1C0
		public VariantConfigurationSection RfcIDImap
		{
			get
			{
				return base["RfcIDImap"];
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x0001CFCD File Offset: 0x0001B1CD
		public VariantConfigurationSection IgnoreNonProvisionedServers
		{
			get
			{
				return base["IgnoreNonProvisionedServers"];
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x0001CFDA File Offset: 0x0001B1DA
		public VariantConfigurationSection UseSamAccountNameAsUsername
		{
			get
			{
				return base["UseSamAccountNameAsUsername"];
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x0001CFE7 File Offset: 0x0001B1E7
		public VariantConfigurationSection SkipAuthOnCafe
		{
			get
			{
				return base["SkipAuthOnCafe"];
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x0001CFF4 File Offset: 0x0001B1F4
		public VariantConfigurationSection AllowPlainTextConversionWithoutUsingSkeleton
		{
			get
			{
				return base["AllowPlainTextConversionWithoutUsingSkeleton"];
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x0001D001 File Offset: 0x0001B201
		public VariantConfigurationSection RfcIDImapCafe
		{
			get
			{
				return base["RfcIDImapCafe"];
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0001D00E File Offset: 0x0001B20E
		public VariantConfigurationSection GlobalCriminalCompliance
		{
			get
			{
				return base["GlobalCriminalCompliance"];
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x0001D01B File Offset: 0x0001B21B
		public VariantConfigurationSection CheckOnlyAuthenticationStatus
		{
			get
			{
				return base["CheckOnlyAuthenticationStatus"];
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0001D028 File Offset: 0x0001B228
		public VariantConfigurationSection RfcMoveImap
		{
			get
			{
				return base["RfcMoveImap"];
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06000C28 RID: 3112 RVA: 0x0001D035 File Offset: 0x0001B235
		public VariantConfigurationSection RefreshSearchFolderItems
		{
			get
			{
				return base["RefreshSearchFolderItems"];
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x0001D042 File Offset: 0x0001B242
		public VariantConfigurationSection ReloadMailboxBeforeGettingSubscriptionList
		{
			get
			{
				return base["ReloadMailboxBeforeGettingSubscriptionList"];
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06000C2A RID: 3114 RVA: 0x0001D04F File Offset: 0x0001B24F
		public VariantConfigurationSection EnforceLogsRetentionPolicy
		{
			get
			{
				return base["EnforceLogsRetentionPolicy"];
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x0001D05C File Offset: 0x0001B25C
		public VariantConfigurationSection AppendServerNameInBanner
		{
			get
			{
				return base["AppendServerNameInBanner"];
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x0001D069 File Offset: 0x0001B269
		public VariantConfigurationSection UsePrimarySmtpAddress
		{
			get
			{
				return base["UsePrimarySmtpAddress"];
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06000C2D RID: 3117 RVA: 0x0001D076 File Offset: 0x0001B276
		public VariantConfigurationSection ImapClientAccessRulesEnabled
		{
			get
			{
				return base["ImapClientAccessRulesEnabled"];
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x0001D083 File Offset: 0x0001B283
		public VariantConfigurationSection DontReturnLastMessageForUInt32MaxValue
		{
			get
			{
				return base["DontReturnLastMessageForUInt32MaxValue"];
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0001D090 File Offset: 0x0001B290
		public VariantConfigurationSection LrsLogging
		{
			get
			{
				return base["LrsLogging"];
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x0001D09D File Offset: 0x0001B29D
		public VariantConfigurationSection AllowKerberosAuth
		{
			get
			{
				return base["AllowKerberosAuth"];
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0001D0AA File Offset: 0x0001B2AA
		public VariantConfigurationSection RfcMoveImapCafe
		{
			get
			{
				return base["RfcMoveImapCafe"];
			}
		}
	}
}
