using System;
using Microsoft.Exchange.AirSync;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x020000F8 RID: 248
	public sealed class VariantConfigurationActiveSyncComponent : VariantConfigurationComponent
	{
		// Token: 0x06000AB6 RID: 2742 RVA: 0x00018F98 File Offset: 0x00017198
		internal VariantConfigurationActiveSyncComponent() : base("ActiveSync")
		{
			base.Add(new VariantConfigurationSection("ActiveSync.settings.ini", "SyncStateOnDirectItems", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveSync.settings.ini", "MdmSupportedPlatforms", typeof(IMdmSupportedPlatformsSettings), true));
			base.Add(new VariantConfigurationSection("ActiveSync.settings.ini", "GlobalCriminalCompliance", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveSync.settings.ini", "ConsumerOrganizationUser", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveSync.settings.ini", "HDPhotos", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveSync.settings.ini", "MailboxLoggingVerboseMode", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveSync.settings.ini", "ActiveSyncClientAccessRulesEnabled", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveSync.settings.ini", "ForceSingleNameSpaceUsage", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveSync.settings.ini", "MdmNotification", typeof(IMdmNotificationSettings), true));
			base.Add(new VariantConfigurationSection("ActiveSync.settings.ini", "ActiveSyncDiagnosticsLogABQPeriodicEvent", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveSync.settings.ini", "RedirectForOnBoarding", typeof(IFeature), true));
			base.Add(new VariantConfigurationSection("ActiveSync.settings.ini", "CloudMdmEnrolled", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveSync.settings.ini", "UseOAuthMasterSidForSecurityContext", typeof(IFeature), true));
			base.Add(new VariantConfigurationSection("ActiveSync.settings.ini", "EnableV160", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveSync.settings.ini", "EasPartialIcsSync", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveSync.settings.ini", "DisableCharsetDetectionInCopyMessageContents", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveSync.settings.ini", "GetGoidFromCalendarItemForMeetingResponse", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("ActiveSync.settings.ini", "SyncStatusOnGlobalInfo", typeof(IFeature), false));
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x000191F0 File Offset: 0x000173F0
		public VariantConfigurationSection SyncStateOnDirectItems
		{
			get
			{
				return base["SyncStateOnDirectItems"];
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x000191FD File Offset: 0x000173FD
		public VariantConfigurationSection MdmSupportedPlatforms
		{
			get
			{
				return base["MdmSupportedPlatforms"];
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x0001920A File Offset: 0x0001740A
		public VariantConfigurationSection GlobalCriminalCompliance
		{
			get
			{
				return base["GlobalCriminalCompliance"];
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06000ABA RID: 2746 RVA: 0x00019217 File Offset: 0x00017417
		public VariantConfigurationSection ConsumerOrganizationUser
		{
			get
			{
				return base["ConsumerOrganizationUser"];
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x00019224 File Offset: 0x00017424
		public VariantConfigurationSection HDPhotos
		{
			get
			{
				return base["HDPhotos"];
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x00019231 File Offset: 0x00017431
		public VariantConfigurationSection MailboxLoggingVerboseMode
		{
			get
			{
				return base["MailboxLoggingVerboseMode"];
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0001923E File Offset: 0x0001743E
		public VariantConfigurationSection ActiveSyncClientAccessRulesEnabled
		{
			get
			{
				return base["ActiveSyncClientAccessRulesEnabled"];
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x0001924B File Offset: 0x0001744B
		public VariantConfigurationSection ForceSingleNameSpaceUsage
		{
			get
			{
				return base["ForceSingleNameSpaceUsage"];
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x00019258 File Offset: 0x00017458
		public VariantConfigurationSection MdmNotification
		{
			get
			{
				return base["MdmNotification"];
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x00019265 File Offset: 0x00017465
		public VariantConfigurationSection ActiveSyncDiagnosticsLogABQPeriodicEvent
		{
			get
			{
				return base["ActiveSyncDiagnosticsLogABQPeriodicEvent"];
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x00019272 File Offset: 0x00017472
		public VariantConfigurationSection RedirectForOnBoarding
		{
			get
			{
				return base["RedirectForOnBoarding"];
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x0001927F File Offset: 0x0001747F
		public VariantConfigurationSection CloudMdmEnrolled
		{
			get
			{
				return base["CloudMdmEnrolled"];
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06000AC3 RID: 2755 RVA: 0x0001928C File Offset: 0x0001748C
		public VariantConfigurationSection UseOAuthMasterSidForSecurityContext
		{
			get
			{
				return base["UseOAuthMasterSidForSecurityContext"];
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x00019299 File Offset: 0x00017499
		public VariantConfigurationSection EnableV160
		{
			get
			{
				return base["EnableV160"];
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x000192A6 File Offset: 0x000174A6
		public VariantConfigurationSection EasPartialIcsSync
		{
			get
			{
				return base["EasPartialIcsSync"];
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x000192B3 File Offset: 0x000174B3
		public VariantConfigurationSection DisableCharsetDetectionInCopyMessageContents
		{
			get
			{
				return base["DisableCharsetDetectionInCopyMessageContents"];
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x000192C0 File Offset: 0x000174C0
		public VariantConfigurationSection GetGoidFromCalendarItemForMeetingResponse
		{
			get
			{
				return base["GetGoidFromCalendarItemForMeetingResponse"];
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x000192CD File Offset: 0x000174CD
		public VariantConfigurationSection SyncStatusOnGlobalInfo
		{
			get
			{
				return base["SyncStatusOnGlobalInfo"];
			}
		}
	}
}
