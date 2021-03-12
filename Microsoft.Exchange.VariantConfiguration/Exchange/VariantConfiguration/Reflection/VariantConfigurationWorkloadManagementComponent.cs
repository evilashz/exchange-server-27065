using System;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000128 RID: 296
	public sealed class VariantConfigurationWorkloadManagementComponent : VariantConfigurationComponent
	{
		// Token: 0x06000DCA RID: 3530 RVA: 0x00021604 File Offset: 0x0001F804
		internal VariantConfigurationWorkloadManagementComponent() : base("WorkloadManagement")
		{
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "PowerShell", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "PowerShellForwardSync", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "Ews", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "Processor", typeof(IResourceSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "StoreUrgentMaintenanceAssistant", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "SharePointSignalStoreAssistant", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "StoreOnlineIntegrityCheckAssistant", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "PowerShellLowPriorityWorkFlow", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "E4eRecipient", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "Eas", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "Transport", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "InferenceDataCollectionAssistant", typeof(IWorkloadSettings), false));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "Owa", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "PeopleRelevanceAssistant", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "PublicFolderAssistant", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "TransportSync", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "OrgContactsSyncAssistant", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "InferenceTrainingAssistant", typeof(IWorkloadSettings), false));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "ProbeTimeBasedAssistant", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "DarRuntime", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "Blackout", typeof(IBlackoutSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "MailboxReplicationServiceHighPriority", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "MailboxReplicationServiceInteractive", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "StoreMaintenanceAssistant", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "CalendarSyncAssistant", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "DarTaskStoreTimeBasedAssistant", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "ELCAssistant", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "SystemWorkloadManager", typeof(ISystemWorkloadManagerSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "DiskLatency", typeof(IResourceSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "O365SuiteService", typeof(IWorkloadSettings), false));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "DiskLatencySettings", typeof(IDiskLatencyMonitorSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "PublicFolderMailboxSync", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "OABGeneratorAssistant", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "TeamMailboxSync", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "StoreScheduledIntegrityCheckAssistant", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "Domt", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "MailboxReplicationServiceInternalMaintenance", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "PowerShellGalSync", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "Momt", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "MailboxProcessorAssistant", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "SearchIndexRepairTimebasedAssistant", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "PeopleCentricTriageAssistant", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "TopNAssistant", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "OwaVoice", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "E4eSender", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "Imap", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "SiteMailboxAssistant", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "UMReportingAssistant", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "ActiveDirectoryReplicationLatency", typeof(IResourceSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "OutlookService", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "GroupMailboxAssistant", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "MdbAvailability", typeof(IResourceSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "MdbLatency", typeof(IResourceSettings), false));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "JunkEmailOptionsCommitterAssistant", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "DirectoryProcessorAssistant", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "CalendarRepairAssistant", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "MailboxAssociationReplicationAssistant", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "StoreDSMaintenanceAssistant", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "CiAgeOfLastNotification", typeof(IResourceSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "SharingPolicyAssistant", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "MdbReplication", typeof(IResourceSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "PowerShellBackSync", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "Pop", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "MailboxReplicationService", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "PushNotificationService", typeof(IWorkloadSettings), true));
			base.Add(new VariantConfigurationSection("WorkloadManagement.settings.ini", "PowerShellDiscretionaryWorkFlow", typeof(IWorkloadSettings), true));
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x00021E5C File Offset: 0x0002005C
		public VariantConfigurationSection PowerShell
		{
			get
			{
				return base["PowerShell"];
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x00021E69 File Offset: 0x00020069
		public VariantConfigurationSection PowerShellForwardSync
		{
			get
			{
				return base["PowerShellForwardSync"];
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x00021E76 File Offset: 0x00020076
		public VariantConfigurationSection Ews
		{
			get
			{
				return base["Ews"];
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x00021E83 File Offset: 0x00020083
		public VariantConfigurationSection Processor
		{
			get
			{
				return base["Processor"];
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x00021E90 File Offset: 0x00020090
		public VariantConfigurationSection StoreUrgentMaintenanceAssistant
		{
			get
			{
				return base["StoreUrgentMaintenanceAssistant"];
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x00021E9D File Offset: 0x0002009D
		public VariantConfigurationSection SharePointSignalStoreAssistant
		{
			get
			{
				return base["SharePointSignalStoreAssistant"];
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x00021EAA File Offset: 0x000200AA
		public VariantConfigurationSection StoreOnlineIntegrityCheckAssistant
		{
			get
			{
				return base["StoreOnlineIntegrityCheckAssistant"];
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x00021EB7 File Offset: 0x000200B7
		public VariantConfigurationSection PowerShellLowPriorityWorkFlow
		{
			get
			{
				return base["PowerShellLowPriorityWorkFlow"];
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x00021EC4 File Offset: 0x000200C4
		public VariantConfigurationSection E4eRecipient
		{
			get
			{
				return base["E4eRecipient"];
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x00021ED1 File Offset: 0x000200D1
		public VariantConfigurationSection Eas
		{
			get
			{
				return base["Eas"];
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x00021EDE File Offset: 0x000200DE
		public VariantConfigurationSection Transport
		{
			get
			{
				return base["Transport"];
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x00021EEB File Offset: 0x000200EB
		public VariantConfigurationSection InferenceDataCollectionAssistant
		{
			get
			{
				return base["InferenceDataCollectionAssistant"];
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x00021EF8 File Offset: 0x000200F8
		public VariantConfigurationSection Owa
		{
			get
			{
				return base["Owa"];
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x00021F05 File Offset: 0x00020105
		public VariantConfigurationSection PeopleRelevanceAssistant
		{
			get
			{
				return base["PeopleRelevanceAssistant"];
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x00021F12 File Offset: 0x00020112
		public VariantConfigurationSection PublicFolderAssistant
		{
			get
			{
				return base["PublicFolderAssistant"];
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x00021F1F File Offset: 0x0002011F
		public VariantConfigurationSection TransportSync
		{
			get
			{
				return base["TransportSync"];
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x00021F2C File Offset: 0x0002012C
		public VariantConfigurationSection OrgContactsSyncAssistant
		{
			get
			{
				return base["OrgContactsSyncAssistant"];
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x00021F39 File Offset: 0x00020139
		public VariantConfigurationSection InferenceTrainingAssistant
		{
			get
			{
				return base["InferenceTrainingAssistant"];
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x00021F46 File Offset: 0x00020146
		public VariantConfigurationSection ProbeTimeBasedAssistant
		{
			get
			{
				return base["ProbeTimeBasedAssistant"];
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x00021F53 File Offset: 0x00020153
		public VariantConfigurationSection DarRuntime
		{
			get
			{
				return base["DarRuntime"];
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x00021F60 File Offset: 0x00020160
		public VariantConfigurationSection Blackout
		{
			get
			{
				return base["Blackout"];
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x00021F6D File Offset: 0x0002016D
		public VariantConfigurationSection MailboxReplicationServiceHighPriority
		{
			get
			{
				return base["MailboxReplicationServiceHighPriority"];
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x00021F7A File Offset: 0x0002017A
		public VariantConfigurationSection MailboxReplicationServiceInteractive
		{
			get
			{
				return base["MailboxReplicationServiceInteractive"];
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x00021F87 File Offset: 0x00020187
		public VariantConfigurationSection StoreMaintenanceAssistant
		{
			get
			{
				return base["StoreMaintenanceAssistant"];
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x00021F94 File Offset: 0x00020194
		public VariantConfigurationSection CalendarSyncAssistant
		{
			get
			{
				return base["CalendarSyncAssistant"];
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x00021FA1 File Offset: 0x000201A1
		public VariantConfigurationSection DarTaskStoreTimeBasedAssistant
		{
			get
			{
				return base["DarTaskStoreTimeBasedAssistant"];
			}
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x00021FAE File Offset: 0x000201AE
		public VariantConfigurationSection ELCAssistant
		{
			get
			{
				return base["ELCAssistant"];
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x00021FBB File Offset: 0x000201BB
		public VariantConfigurationSection SystemWorkloadManager
		{
			get
			{
				return base["SystemWorkloadManager"];
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x00021FC8 File Offset: 0x000201C8
		public VariantConfigurationSection DiskLatency
		{
			get
			{
				return base["DiskLatency"];
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x00021FD5 File Offset: 0x000201D5
		public VariantConfigurationSection O365SuiteService
		{
			get
			{
				return base["O365SuiteService"];
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x00021FE2 File Offset: 0x000201E2
		public VariantConfigurationSection DiskLatencySettings
		{
			get
			{
				return base["DiskLatencySettings"];
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x00021FEF File Offset: 0x000201EF
		public VariantConfigurationSection PublicFolderMailboxSync
		{
			get
			{
				return base["PublicFolderMailboxSync"];
			}
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x00021FFC File Offset: 0x000201FC
		public VariantConfigurationSection OABGeneratorAssistant
		{
			get
			{
				return base["OABGeneratorAssistant"];
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x00022009 File Offset: 0x00020209
		public VariantConfigurationSection TeamMailboxSync
		{
			get
			{
				return base["TeamMailboxSync"];
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x00022016 File Offset: 0x00020216
		public VariantConfigurationSection StoreScheduledIntegrityCheckAssistant
		{
			get
			{
				return base["StoreScheduledIntegrityCheckAssistant"];
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x00022023 File Offset: 0x00020223
		public VariantConfigurationSection Domt
		{
			get
			{
				return base["Domt"];
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x00022030 File Offset: 0x00020230
		public VariantConfigurationSection MailboxReplicationServiceInternalMaintenance
		{
			get
			{
				return base["MailboxReplicationServiceInternalMaintenance"];
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x0002203D File Offset: 0x0002023D
		public VariantConfigurationSection PowerShellGalSync
		{
			get
			{
				return base["PowerShellGalSync"];
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x0002204A File Offset: 0x0002024A
		public VariantConfigurationSection Momt
		{
			get
			{
				return base["Momt"];
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x00022057 File Offset: 0x00020257
		public VariantConfigurationSection MailboxProcessorAssistant
		{
			get
			{
				return base["MailboxProcessorAssistant"];
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x00022064 File Offset: 0x00020264
		public VariantConfigurationSection SearchIndexRepairTimebasedAssistant
		{
			get
			{
				return base["SearchIndexRepairTimebasedAssistant"];
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x00022071 File Offset: 0x00020271
		public VariantConfigurationSection PeopleCentricTriageAssistant
		{
			get
			{
				return base["PeopleCentricTriageAssistant"];
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x0002207E File Offset: 0x0002027E
		public VariantConfigurationSection TopNAssistant
		{
			get
			{
				return base["TopNAssistant"];
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x0002208B File Offset: 0x0002028B
		public VariantConfigurationSection OwaVoice
		{
			get
			{
				return base["OwaVoice"];
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x00022098 File Offset: 0x00020298
		public VariantConfigurationSection E4eSender
		{
			get
			{
				return base["E4eSender"];
			}
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x000220A5 File Offset: 0x000202A5
		public VariantConfigurationSection Imap
		{
			get
			{
				return base["Imap"];
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x000220B2 File Offset: 0x000202B2
		public VariantConfigurationSection SiteMailboxAssistant
		{
			get
			{
				return base["SiteMailboxAssistant"];
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x000220BF File Offset: 0x000202BF
		public VariantConfigurationSection UMReportingAssistant
		{
			get
			{
				return base["UMReportingAssistant"];
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06000DFB RID: 3579 RVA: 0x000220CC File Offset: 0x000202CC
		public VariantConfigurationSection ActiveDirectoryReplicationLatency
		{
			get
			{
				return base["ActiveDirectoryReplicationLatency"];
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x000220D9 File Offset: 0x000202D9
		public VariantConfigurationSection OutlookService
		{
			get
			{
				return base["OutlookService"];
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x000220E6 File Offset: 0x000202E6
		public VariantConfigurationSection GroupMailboxAssistant
		{
			get
			{
				return base["GroupMailboxAssistant"];
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x000220F3 File Offset: 0x000202F3
		public VariantConfigurationSection MdbAvailability
		{
			get
			{
				return base["MdbAvailability"];
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06000DFF RID: 3583 RVA: 0x00022100 File Offset: 0x00020300
		public VariantConfigurationSection MdbLatency
		{
			get
			{
				return base["MdbLatency"];
			}
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x0002210D File Offset: 0x0002030D
		public VariantConfigurationSection JunkEmailOptionsCommitterAssistant
		{
			get
			{
				return base["JunkEmailOptionsCommitterAssistant"];
			}
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x0002211A File Offset: 0x0002031A
		public VariantConfigurationSection DirectoryProcessorAssistant
		{
			get
			{
				return base["DirectoryProcessorAssistant"];
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x00022127 File Offset: 0x00020327
		public VariantConfigurationSection CalendarRepairAssistant
		{
			get
			{
				return base["CalendarRepairAssistant"];
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x00022134 File Offset: 0x00020334
		public VariantConfigurationSection MailboxAssociationReplicationAssistant
		{
			get
			{
				return base["MailboxAssociationReplicationAssistant"];
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x00022141 File Offset: 0x00020341
		public VariantConfigurationSection StoreDSMaintenanceAssistant
		{
			get
			{
				return base["StoreDSMaintenanceAssistant"];
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x0002214E File Offset: 0x0002034E
		public VariantConfigurationSection CiAgeOfLastNotification
		{
			get
			{
				return base["CiAgeOfLastNotification"];
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x0002215B File Offset: 0x0002035B
		public VariantConfigurationSection SharingPolicyAssistant
		{
			get
			{
				return base["SharingPolicyAssistant"];
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x00022168 File Offset: 0x00020368
		public VariantConfigurationSection MdbReplication
		{
			get
			{
				return base["MdbReplication"];
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x00022175 File Offset: 0x00020375
		public VariantConfigurationSection PowerShellBackSync
		{
			get
			{
				return base["PowerShellBackSync"];
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x00022182 File Offset: 0x00020382
		public VariantConfigurationSection Pop
		{
			get
			{
				return base["Pop"];
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x0002218F File Offset: 0x0002038F
		public VariantConfigurationSection MailboxReplicationService
		{
			get
			{
				return base["MailboxReplicationService"];
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x0002219C File Offset: 0x0002039C
		public VariantConfigurationSection PushNotificationService
		{
			get
			{
				return base["PushNotificationService"];
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x000221A9 File Offset: 0x000203A9
		public VariantConfigurationSection PowerShellDiscretionaryWorkFlow
		{
			get
			{
				return base["PowerShellDiscretionaryWorkFlow"];
			}
		}
	}
}
