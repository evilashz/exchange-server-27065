using System;
using System.Configuration;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005CE RID: 1486
	internal class TenantRelocationConfigSchema : ConfigSchemaBase
	{
		// Token: 0x17001681 RID: 5761
		// (get) Token: 0x0600447E RID: 17534 RVA: 0x0010047A File Offset: 0x000FE67A
		public override string Name
		{
			get
			{
				return "TenantRelocation";
			}
		}

		// Token: 0x17001682 RID: 5762
		// (get) Token: 0x0600447F RID: 17535 RVA: 0x00100481 File Offset: 0x000FE681
		public override string SectionName
		{
			get
			{
				return "TenantRelocationConfiguration";
			}
		}

		// Token: 0x17001683 RID: 5763
		// (get) Token: 0x06004480 RID: 17536 RVA: 0x00100488 File Offset: 0x000FE688
		// (set) Token: 0x06004481 RID: 17537 RVA: 0x0010049A File Offset: 0x000FE69A
		[ConfigurationProperty("IsAllRelocationActivitiesHalted", DefaultValue = true)]
		public bool IsAllRelocationActivitiesHalted
		{
			get
			{
				return (bool)base["IsAllRelocationActivitiesHalted"];
			}
			set
			{
				base["IsAllRelocationActivitiesHalted"] = value;
			}
		}

		// Token: 0x17001684 RID: 5764
		// (get) Token: 0x06004482 RID: 17538 RVA: 0x001004AD File Offset: 0x000FE6AD
		// (set) Token: 0x06004483 RID: 17539 RVA: 0x001004BF File Offset: 0x000FE6BF
		[ConfigurationProperty("IsProcessingBrokerEnabled", DefaultValue = false)]
		public bool IsProcessingBrokerEnabled
		{
			get
			{
				return (bool)base["IsProcessingBrokerEnabled"];
			}
			set
			{
				base["IsProcessingBrokerEnabled"] = value;
			}
		}

		// Token: 0x17001685 RID: 5765
		// (get) Token: 0x06004484 RID: 17540 RVA: 0x001004D2 File Offset: 0x000FE6D2
		// (set) Token: 0x06004485 RID: 17541 RVA: 0x001004E4 File Offset: 0x000FE6E4
		[ConfigurationProperty("IsRollbackBrokerEnabled", DefaultValue = false)]
		public bool IsRollbackBrokerEnabled
		{
			get
			{
				return (bool)base["IsRollbackBrokerEnabled"];
			}
			set
			{
				base["IsRollbackBrokerEnabled"] = value;
			}
		}

		// Token: 0x17001686 RID: 5766
		// (get) Token: 0x06004486 RID: 17542 RVA: 0x001004F7 File Offset: 0x000FE6F7
		// (set) Token: 0x06004487 RID: 17543 RVA: 0x00100509 File Offset: 0x000FE709
		[ConfigurationProperty("IsCleanupBrokerEnabled", DefaultValue = false)]
		public bool IsCleanupBrokerEnabled
		{
			get
			{
				return (bool)base["IsCleanupBrokerEnabled"];
			}
			set
			{
				base["IsCleanupBrokerEnabled"] = value;
			}
		}

		// Token: 0x17001687 RID: 5767
		// (get) Token: 0x06004488 RID: 17544 RVA: 0x0010051C File Offset: 0x000FE71C
		// (set) Token: 0x06004489 RID: 17545 RVA: 0x0010052E File Offset: 0x000FE72E
		[ConfigurationProperty("IsOrchestratorEnabled", DefaultValue = false)]
		public bool IsOrchestratorEnabled
		{
			get
			{
				return (bool)base["IsOrchestratorEnabled"];
			}
			set
			{
				base["IsOrchestratorEnabled"] = value;
			}
		}

		// Token: 0x17001688 RID: 5768
		// (get) Token: 0x0600448A RID: 17546 RVA: 0x00100541 File Offset: 0x000FE741
		// (set) Token: 0x0600448B RID: 17547 RVA: 0x00100553 File Offset: 0x000FE753
		[ConfigurationProperty("DataSyncObjectsPerPageLimit", DefaultValue = "1000")]
		public uint DataSyncObjectsPerPageLimit
		{
			get
			{
				return (uint)base["DataSyncObjectsPerPageLimit"];
			}
			set
			{
				base["DataSyncObjectsPerPageLimit"] = value;
			}
		}

		// Token: 0x17001689 RID: 5769
		// (get) Token: 0x0600448C RID: 17548 RVA: 0x00100566 File Offset: 0x000FE766
		// (set) Token: 0x0600448D RID: 17549 RVA: 0x00100578 File Offset: 0x000FE778
		[ConfigurationProperty("DataSyncLinksPerPageLimit", DefaultValue = "1500")]
		public uint DataSyncLinksPerPageLimit
		{
			get
			{
				return (uint)base["DataSyncLinksPerPageLimit"];
			}
			set
			{
				base["DataSyncLinksPerPageLimit"] = value;
			}
		}

		// Token: 0x1700168A RID: 5770
		// (get) Token: 0x0600448E RID: 17550 RVA: 0x0010058B File Offset: 0x000FE78B
		// (set) Token: 0x0600448F RID: 17551 RVA: 0x0010059D File Offset: 0x000FE79D
		[ConfigurationProperty("DataSyncInitialLinkReadSize", DefaultValue = "1500")]
		public uint DataSyncInitialLinkReadSize
		{
			get
			{
				return (uint)base["DataSyncInitialLinkReadSize"];
			}
			set
			{
				base["DataSyncInitialLinkReadSize"] = value;
			}
		}

		// Token: 0x1700168B RID: 5771
		// (get) Token: 0x06004490 RID: 17552 RVA: 0x001005B0 File Offset: 0x000FE7B0
		// (set) Token: 0x06004491 RID: 17553 RVA: 0x001005C2 File Offset: 0x000FE7C2
		[ConfigurationProperty("DataSyncFailoverTimeoutInMinutes", DefaultValue = "30")]
		public uint DataSyncFailoverTimeoutInMinutes
		{
			get
			{
				return (uint)base["DataSyncFailoverTimeoutInMinutes"];
			}
			set
			{
				base["DataSyncFailoverTimeoutInMinutes"] = value;
			}
		}

		// Token: 0x1700168C RID: 5772
		// (get) Token: 0x06004492 RID: 17554 RVA: 0x001005D5 File Offset: 0x000FE7D5
		// (set) Token: 0x06004493 RID: 17555 RVA: 0x001005E7 File Offset: 0x000FE7E7
		[ConfigurationProperty("DataSyncLinksOverldapSize", DefaultValue = "100")]
		public uint DataSyncLinksOverldapSize
		{
			get
			{
				return (uint)base["DataSyncLinksOverldapSize"];
			}
			set
			{
				base["DataSyncLinksOverldapSize"] = value;
			}
		}

		// Token: 0x1700168D RID: 5773
		// (get) Token: 0x06004494 RID: 17556 RVA: 0x001005FA File Offset: 0x000FE7FA
		// (set) Token: 0x06004495 RID: 17557 RVA: 0x0010060C File Offset: 0x000FE80C
		[ConfigurationProperty("DeltaSyncUsnRangeLimit", DefaultValue = "1000000")]
		public long DeltaSyncUsnRangeLimit
		{
			get
			{
				return (long)base["DeltaSyncUsnRangeLimit"];
			}
			set
			{
				base["DeltaSyncUsnRangeLimit"] = value;
			}
		}

		// Token: 0x1700168E RID: 5774
		// (get) Token: 0x06004496 RID: 17558 RVA: 0x0010061F File Offset: 0x000FE81F
		// (set) Token: 0x06004497 RID: 17559 RVA: 0x00100631 File Offset: 0x000FE831
		[ConfigurationProperty("MaxConcurrentProcessingThreadsPerServer", DefaultValue = "20")]
		public uint MaxConcurrentProcessingThreadsPerServer
		{
			get
			{
				return (uint)base["MaxConcurrentProcessingThreadsPerServer"];
			}
			set
			{
				base["MaxConcurrentProcessingThreadsPerServer"] = value;
			}
		}

		// Token: 0x1700168F RID: 5775
		// (get) Token: 0x06004498 RID: 17560 RVA: 0x00100644 File Offset: 0x000FE844
		// (set) Token: 0x06004499 RID: 17561 RVA: 0x00100656 File Offset: 0x000FE856
		[ConfigurationProperty("MaxConcurrentRollbackThreadsPerServer", DefaultValue = "1")]
		public uint MaxConcurrentRollbackThreadsPerServer
		{
			get
			{
				return (uint)base["MaxConcurrentRollbackThreadsPerServer"];
			}
			set
			{
				base["MaxConcurrentRollbackThreadsPerServer"] = value;
			}
		}

		// Token: 0x17001690 RID: 5776
		// (get) Token: 0x0600449A RID: 17562 RVA: 0x00100669 File Offset: 0x000FE869
		// (set) Token: 0x0600449B RID: 17563 RVA: 0x0010067B File Offset: 0x000FE87B
		[ConfigurationProperty("MaxConcurrentCleanupThreadsPerServer", DefaultValue = "1")]
		public uint MaxConcurrentCleanupThreadsPerServer
		{
			get
			{
				return (uint)base["MaxConcurrentCleanupThreadsPerServer"];
			}
			set
			{
				base["MaxConcurrentCleanupThreadsPerServer"] = value;
			}
		}

		// Token: 0x17001691 RID: 5777
		// (get) Token: 0x0600449C RID: 17564 RVA: 0x0010068E File Offset: 0x000FE88E
		// (set) Token: 0x0600449D RID: 17565 RVA: 0x001006A0 File Offset: 0x000FE8A0
		[ConfigurationProperty("ProcessingBrokerPollIntervalInMinutes", DefaultValue = "5")]
		public uint ProcessingBrokerPollIntervalInMinutes
		{
			get
			{
				return (uint)base["ProcessingBrokerPollIntervalInMinutes"];
			}
			set
			{
				base["ProcessingBrokerPollIntervalInMinutes"] = value;
			}
		}

		// Token: 0x17001692 RID: 5778
		// (get) Token: 0x0600449E RID: 17566 RVA: 0x001006B3 File Offset: 0x000FE8B3
		// (set) Token: 0x0600449F RID: 17567 RVA: 0x001006C5 File Offset: 0x000FE8C5
		[ConfigurationProperty("RollbackBrokerPollIntervalInMinutes", DefaultValue = "5")]
		public uint RollbackBrokerPollIntervalInMinutes
		{
			get
			{
				return (uint)base["RollbackBrokerPollIntervalInMinutes"];
			}
			set
			{
				base["RollbackBrokerPollIntervalInMinutes"] = value;
			}
		}

		// Token: 0x17001693 RID: 5779
		// (get) Token: 0x060044A0 RID: 17568 RVA: 0x001006D8 File Offset: 0x000FE8D8
		// (set) Token: 0x060044A1 RID: 17569 RVA: 0x001006EA File Offset: 0x000FE8EA
		[ConfigurationProperty("IsUserExperienceTestEnabled", DefaultValue = false)]
		public bool IsUserExperienceTestEnabled
		{
			get
			{
				return (bool)base["IsUserExperienceTestEnabled"];
			}
			set
			{
				base["IsUserExperienceTestEnabled"] = value;
			}
		}

		// Token: 0x17001694 RID: 5780
		// (get) Token: 0x060044A2 RID: 17570 RVA: 0x001006FD File Offset: 0x000FE8FD
		// (set) Token: 0x060044A3 RID: 17571 RVA: 0x0010070F File Offset: 0x000FE90F
		[ConfigurationProperty("DisabledUXProbes", DefaultValue = "")]
		public string DisabledUXProbes
		{
			get
			{
				return (string)base["DisabledUXProbes"];
			}
			set
			{
				base["DisabledUXProbes"] = value;
			}
		}

		// Token: 0x17001695 RID: 5781
		// (get) Token: 0x060044A4 RID: 17572 RVA: 0x0010071D File Offset: 0x000FE91D
		// (set) Token: 0x060044A5 RID: 17573 RVA: 0x0010072F File Offset: 0x000FE92F
		[ConfigurationProperty("UXProbeRecurrenceIntervalSeconds", DefaultValue = "150")]
		public uint UXProbeRecurrenceIntervalSeconds
		{
			get
			{
				return (uint)base["UXProbeRecurrenceIntervalSeconds"];
			}
			set
			{
				base["UXProbeRecurrenceIntervalSeconds"] = value;
			}
		}

		// Token: 0x17001696 RID: 5782
		// (get) Token: 0x060044A6 RID: 17574 RVA: 0x00100742 File Offset: 0x000FE942
		// (set) Token: 0x060044A7 RID: 17575 RVA: 0x00100754 File Offset: 0x000FE954
		[ConfigurationProperty("UXMonitorConsecutiveProbeFailureCount", DefaultValue = "2")]
		public uint UXMonitorConsecutiveProbeFailureCount
		{
			get
			{
				return (uint)base["UXMonitorConsecutiveProbeFailureCount"];
			}
			set
			{
				base["UXMonitorConsecutiveProbeFailureCount"] = value;
			}
		}

		// Token: 0x17001697 RID: 5783
		// (get) Token: 0x060044A8 RID: 17576 RVA: 0x00100767 File Offset: 0x000FE967
		// (set) Token: 0x060044A9 RID: 17577 RVA: 0x00100779 File Offset: 0x000FE979
		[ConfigurationProperty("UXMonitorAccountExpiredDays", DefaultValue = "15")]
		public uint UXMonitorAccountExpiredDays
		{
			get
			{
				return (uint)base["UXMonitorAccountExpiredDays"];
			}
			set
			{
				base["UXMonitorAccountExpiredDays"] = value;
			}
		}

		// Token: 0x17001698 RID: 5784
		// (get) Token: 0x060044AA RID: 17578 RVA: 0x0010078C File Offset: 0x000FE98C
		// (set) Token: 0x060044AB RID: 17579 RVA: 0x0010079E File Offset: 0x000FE99E
		[ConfigurationProperty("RemoveUXMonitorAccountWaitReplicationMinutes", DefaultValue = "5")]
		public uint RemoveUXMonitorAccountWaitReplicationMinutes
		{
			get
			{
				return (uint)base["RemoveUXMonitorAccountWaitReplicationMinutes"];
			}
			set
			{
				base["RemoveUXMonitorAccountWaitReplicationMinutes"] = value;
			}
		}

		// Token: 0x17001699 RID: 5785
		// (get) Token: 0x060044AC RID: 17580 RVA: 0x001007B1 File Offset: 0x000FE9B1
		// (set) Token: 0x060044AD RID: 17581 RVA: 0x001007C3 File Offset: 0x000FE9C3
		[ConfigurationProperty("WaitUXFailureResultSeconds", DefaultValue = "30")]
		public uint WaitUXFailureResultSeconds
		{
			get
			{
				return (uint)base["WaitUXFailureResultSeconds"];
			}
			set
			{
				base["WaitUXFailureResultSeconds"] = value;
			}
		}

		// Token: 0x1700169A RID: 5786
		// (get) Token: 0x060044AE RID: 17582 RVA: 0x001007D6 File Offset: 0x000FE9D6
		// (set) Token: 0x060044AF RID: 17583 RVA: 0x001007E8 File Offset: 0x000FE9E8
		[ConfigurationProperty("UXTransportProbeSmtpServers", DefaultValue = "")]
		public string UXTransportProbeSmtpServers
		{
			get
			{
				return (string)base["UXTransportProbeSmtpServers"];
			}
			set
			{
				base["UXTransportProbeSmtpServers"] = value;
			}
		}

		// Token: 0x1700169B RID: 5787
		// (get) Token: 0x060044B0 RID: 17584 RVA: 0x001007F6 File Offset: 0x000FE9F6
		// (set) Token: 0x060044B1 RID: 17585 RVA: 0x00100808 File Offset: 0x000FEA08
		[ConfigurationProperty("UXTransportProbeSmtpPort", DefaultValue = "2525")]
		public uint UXTransportProbeSmtpPort
		{
			get
			{
				return (uint)base["UXTransportProbeSmtpPort"];
			}
			set
			{
				base["UXTransportProbeSmtpPort"] = value;
			}
		}

		// Token: 0x1700169C RID: 5788
		// (get) Token: 0x060044B2 RID: 17586 RVA: 0x0010081B File Offset: 0x000FEA1B
		// (set) Token: 0x060044B3 RID: 17587 RVA: 0x0010082D File Offset: 0x000FEA2D
		[ConfigurationProperty("UXTransportProbeSenderAddress", DefaultValue = "")]
		public string UXTransportProbeSenderAddress
		{
			get
			{
				return (string)base["UXTransportProbeSenderAddress"];
			}
			set
			{
				base["UXTransportProbeSenderAddress"] = value;
			}
		}

		// Token: 0x1700169D RID: 5789
		// (get) Token: 0x060044B4 RID: 17588 RVA: 0x0010083B File Offset: 0x000FEA3B
		// (set) Token: 0x060044B5 RID: 17589 RVA: 0x0010084D File Offset: 0x000FEA4D
		[ConfigurationProperty("UXTransportProbeSendMessageTimeout", DefaultValue = "15")]
		public uint UXTransportProbeSendMessageTimeout
		{
			get
			{
				return (uint)base["UXTransportProbeSendMessageTimeout"];
			}
			set
			{
				base["UXTransportProbeSendMessageTimeout"] = value;
			}
		}

		// Token: 0x1700169E RID: 5790
		// (get) Token: 0x060044B6 RID: 17590 RVA: 0x00100860 File Offset: 0x000FEA60
		// (set) Token: 0x060044B7 RID: 17591 RVA: 0x00100872 File Offset: 0x000FEA72
		[ConfigurationProperty("UXTransportProbeWaitMessageTimeout", DefaultValue = "90")]
		public uint UXTransportProbeWaitMessageTimeout
		{
			get
			{
				return (uint)base["UXTransportProbeWaitMessageTimeout"];
			}
			set
			{
				base["UXTransportProbeWaitMessageTimeout"] = value;
			}
		}

		// Token: 0x1700169F RID: 5791
		// (get) Token: 0x060044B8 RID: 17592 RVA: 0x00100885 File Offset: 0x000FEA85
		// (set) Token: 0x060044B9 RID: 17593 RVA: 0x00100897 File Offset: 0x000FEA97
		[ConfigurationProperty("CheckStaleRelocations", DefaultValue = true)]
		public bool CheckStaleRelocations
		{
			get
			{
				return (bool)base["CheckStaleRelocations"];
			}
			set
			{
				base["CheckStaleRelocations"] = value;
			}
		}

		// Token: 0x170016A0 RID: 5792
		// (get) Token: 0x060044BA RID: 17594 RVA: 0x001008AA File Offset: 0x000FEAAA
		// (set) Token: 0x060044BB RID: 17595 RVA: 0x001008BC File Offset: 0x000FEABC
		[ConfigurationProperty("SafeScheduleWindow", DefaultValue = "DailyFrom1AMTo5AM")]
		public string SafeScheduleWindow
		{
			get
			{
				return (string)base["SafeScheduleWindow"];
			}
			set
			{
				base["SafeScheduleWindow"] = value;
			}
		}

		// Token: 0x170016A1 RID: 5793
		// (get) Token: 0x060044BC RID: 17596 RVA: 0x001008CA File Offset: 0x000FEACA
		// (set) Token: 0x060044BD RID: 17597 RVA: 0x001008DC File Offset: 0x000FEADC
		[ConfigurationProperty("MaxRelocationInNonCriticalStage", DefaultValue = "20")]
		public uint MaxRelocationInNonCriticalStage
		{
			get
			{
				return (uint)base["MaxRelocationInNonCriticalStage"];
			}
			set
			{
				base["MaxRelocationInNonCriticalStage"] = value;
			}
		}

		// Token: 0x170016A2 RID: 5794
		// (get) Token: 0x060044BE RID: 17598 RVA: 0x001008EF File Offset: 0x000FEAEF
		// (set) Token: 0x060044BF RID: 17599 RVA: 0x00100901 File Offset: 0x000FEB01
		[ConfigurationProperty("MaxRelocationInCriticalStage", DefaultValue = "10")]
		public uint MaxRelocationInCriticalStage
		{
			get
			{
				return (uint)base["MaxRelocationInCriticalStage"];
			}
			set
			{
				base["MaxRelocationInCriticalStage"] = value;
			}
		}

		// Token: 0x170016A3 RID: 5795
		// (get) Token: 0x060044C0 RID: 17600 RVA: 0x00100914 File Offset: 0x000FEB14
		// (set) Token: 0x060044C1 RID: 17601 RVA: 0x00100926 File Offset: 0x000FEB26
		[ConfigurationProperty("MaxRelocationInCleanupStage", DefaultValue = "10")]
		public uint MaxRelocationInCleanupStage
		{
			get
			{
				return (uint)base["MaxRelocationInCleanupStage"];
			}
			set
			{
				base["MaxRelocationInCleanupStage"] = value;
			}
		}

		// Token: 0x170016A4 RID: 5796
		// (get) Token: 0x060044C2 RID: 17602 RVA: 0x00100939 File Offset: 0x000FEB39
		// (set) Token: 0x060044C3 RID: 17603 RVA: 0x0010094B File Offset: 0x000FEB4B
		[ConfigurationProperty("OrchestratorSleepIntervalBetweenRetriesInMinutes", DefaultValue = "60")]
		public uint OrchestratorSleepIntervalBetweenRetriesInMinutes
		{
			get
			{
				return (uint)base["OrchestratorSleepIntervalBetweenRetriesInMinutes"];
			}
			set
			{
				base["OrchestratorSleepIntervalBetweenRetriesInMinutes"] = value;
			}
		}

		// Token: 0x170016A5 RID: 5797
		// (get) Token: 0x060044C4 RID: 17604 RVA: 0x0010095E File Offset: 0x000FEB5E
		// (set) Token: 0x060044C5 RID: 17605 RVA: 0x00100970 File Offset: 0x000FEB70
		[ConfigurationProperty("ADDriverValidatorEnabled", DefaultValue = true)]
		public bool ADDriverValidatorEnabled
		{
			get
			{
				return (bool)base["ADDriverValidatorEnabled"];
			}
			set
			{
				base["ADDriverValidatorEnabled"] = value;
			}
		}

		// Token: 0x170016A6 RID: 5798
		// (get) Token: 0x060044C6 RID: 17606 RVA: 0x00100983 File Offset: 0x000FEB83
		// (set) Token: 0x060044C7 RID: 17607 RVA: 0x00100995 File Offset: 0x000FEB95
		[ConfigurationProperty("RemoveSourceForestLinkOnRetirement", DefaultValue = false)]
		public bool RemoveSourceForestLinkOnRetirement
		{
			get
			{
				return (bool)base["RemoveSourceForestLinkOnRetirement"];
			}
			set
			{
				base["RemoveSourceForestLinkOnRetirement"] = value;
			}
		}

		// Token: 0x170016A7 RID: 5799
		// (get) Token: 0x060044C8 RID: 17608 RVA: 0x001009A8 File Offset: 0x000FEBA8
		// (set) Token: 0x060044C9 RID: 17609 RVA: 0x001009BA File Offset: 0x000FEBBA
		[ConfigurationProperty("RemoveSourceForestLinkOnCleanup", DefaultValue = true)]
		public bool RemoveSourceForestLinkOnCleanup
		{
			get
			{
				return (bool)base["RemoveSourceForestLinkOnCleanup"];
			}
			set
			{
				base["RemoveSourceForestLinkOnCleanup"] = value;
			}
		}

		// Token: 0x170016A8 RID: 5800
		// (get) Token: 0x060044CA RID: 17610 RVA: 0x001009CD File Offset: 0x000FEBCD
		// (set) Token: 0x060044CB RID: 17611 RVA: 0x001009DF File Offset: 0x000FEBDF
		[ConfigurationProperty("TranslateSupportedSharedConfigurations", DefaultValue = true)]
		public bool TranslateSupportedSharedConfigurations
		{
			get
			{
				return (bool)base["TranslateSupportedSharedConfigurations"];
			}
			set
			{
				base["TranslateSupportedSharedConfigurations"] = value;
			}
		}

		// Token: 0x170016A9 RID: 5801
		// (get) Token: 0x060044CC RID: 17612 RVA: 0x001009F2 File Offset: 0x000FEBF2
		// (set) Token: 0x060044CD RID: 17613 RVA: 0x00100A04 File Offset: 0x000FEC04
		[ConfigurationProperty("IgnoreRelocationConstraintExpiration", DefaultValue = true)]
		public bool IgnoreRelocationConstraintExpiration
		{
			get
			{
				return (bool)base["IgnoreRelocationConstraintExpiration"];
			}
			set
			{
				base["IgnoreRelocationConstraintExpiration"] = value;
			}
		}

		// Token: 0x170016AA RID: 5802
		// (get) Token: 0x060044CE RID: 17614 RVA: 0x00100A17 File Offset: 0x000FEC17
		// (set) Token: 0x060044CF RID: 17615 RVA: 0x00100A29 File Offset: 0x000FEC29
		[ConfigurationProperty("AutoSelectTargetPartition", DefaultValue = true)]
		public bool AutoSelectTargetPartition
		{
			get
			{
				return (bool)base["AutoSelectTargetPartition"];
			}
			set
			{
				base["AutoSelectTargetPartition"] = value;
			}
		}

		// Token: 0x170016AB RID: 5803
		// (get) Token: 0x060044D0 RID: 17616 RVA: 0x00100A3C File Offset: 0x000FEC3C
		// (set) Token: 0x060044D1 RID: 17617 RVA: 0x00100A4E File Offset: 0x000FEC4E
		[ConfigurationProperty("DefaultRelocationCacheExpirationTimeInMinutes", DefaultValue = 60)]
		public int DefaultRelocationCacheExpirationTimeInMinutes
		{
			get
			{
				return (int)base["DefaultRelocationCacheExpirationTimeInMinutes"];
			}
			set
			{
				base["DefaultRelocationCacheExpirationTimeInMinutes"] = value;
			}
		}

		// Token: 0x170016AC RID: 5804
		// (get) Token: 0x060044D2 RID: 17618 RVA: 0x00100A61 File Offset: 0x000FEC61
		// (set) Token: 0x060044D3 RID: 17619 RVA: 0x00100A73 File Offset: 0x000FEC73
		[ConfigurationProperty("ModerateRelocationCacheExpirationTimeInMinutes", DefaultValue = 10)]
		public int ModerateRelocationCacheExpirationTimeInMinutes
		{
			get
			{
				return (int)base["ModerateRelocationCacheExpirationTimeInMinutes"];
			}
			set
			{
				base["ModerateRelocationCacheExpirationTimeInMinutes"] = value;
			}
		}

		// Token: 0x170016AD RID: 5805
		// (get) Token: 0x060044D4 RID: 17620 RVA: 0x00100A86 File Offset: 0x000FEC86
		// (set) Token: 0x060044D5 RID: 17621 RVA: 0x00100A98 File Offset: 0x000FEC98
		[ConfigurationProperty("AggressiveRelocationCacheExpirationTimeInMinutes", DefaultValue = 3)]
		public int AggressiveRelocationCacheExpirationTimeInMinutes
		{
			get
			{
				return (int)base["AggressiveRelocationCacheExpirationTimeInMinutes"];
			}
			set
			{
				base["AggressiveRelocationCacheExpirationTimeInMinutes"] = value;
			}
		}

		// Token: 0x170016AE RID: 5806
		// (get) Token: 0x060044D6 RID: 17622 RVA: 0x00100AAB File Offset: 0x000FECAB
		// (set) Token: 0x060044D7 RID: 17623 RVA: 0x00100ABD File Offset: 0x000FECBD
		[ConfigurationProperty("DedicatedOrchestrator", DefaultValue = "")]
		public string DedicatedOrchestrator
		{
			get
			{
				return (string)base["DedicatedOrchestrator"];
			}
			set
			{
				base["DedicatedOrchestrator"] = value;
			}
		}

		// Token: 0x170016AF RID: 5807
		// (get) Token: 0x060044D8 RID: 17624 RVA: 0x00100ACB File Offset: 0x000FECCB
		// (set) Token: 0x060044D9 RID: 17625 RVA: 0x00100ADD File Offset: 0x000FECDD
		[ConfigurationProperty("WaitForGlsCacheUpdateMinutes", DefaultValue = 5)]
		public int WaitForGlsCacheUpdateMinutes
		{
			get
			{
				return (int)base["WaitForGlsCacheUpdateMinutes"];
			}
			set
			{
				base["WaitForGlsCacheUpdateMinutes"] = value;
			}
		}

		// Token: 0x170016B0 RID: 5808
		// (get) Token: 0x060044DA RID: 17626 RVA: 0x00100AF0 File Offset: 0x000FECF0
		// (set) Token: 0x060044DB RID: 17627 RVA: 0x00100B02 File Offset: 0x000FED02
		[ConfigurationProperty("GlsReadRetries", DefaultValue = 6)]
		public int GlsReadRetries
		{
			get
			{
				return (int)base["GlsReadRetries"];
			}
			set
			{
				base["GlsReadRetries"] = value;
			}
		}

		// Token: 0x170016B1 RID: 5809
		// (get) Token: 0x060044DC RID: 17628 RVA: 0x00100B15 File Offset: 0x000FED15
		// (set) Token: 0x060044DD RID: 17629 RVA: 0x00100B27 File Offset: 0x000FED27
		[ConfigurationProperty("MaxAllowedReplicationLatencyInMinutes", DefaultValue = 5)]
		public int MaxAllowedReplicationLatencyInMinutes
		{
			get
			{
				return (int)base["MaxAllowedReplicationLatencyInMinutes"];
			}
			set
			{
				base["MaxAllowedReplicationLatencyInMinutes"] = value;
			}
		}

		// Token: 0x170016B2 RID: 5810
		// (get) Token: 0x060044DE RID: 17630 RVA: 0x00100B3A File Offset: 0x000FED3A
		// (set) Token: 0x060044DF RID: 17631 RVA: 0x00100B4C File Offset: 0x000FED4C
		[ConfigurationProperty("MaxTenantLockDownTimeInMinutes", DefaultValue = 60)]
		public int MaxTenantLockDownTimeInMinutes
		{
			get
			{
				return (int)base["MaxTenantLockDownTimeInMinutes"];
			}
			set
			{
				base["MaxTenantLockDownTimeInMinutes"] = value;
			}
		}

		// Token: 0x170016B3 RID: 5811
		// (get) Token: 0x060044E0 RID: 17632 RVA: 0x00100B5F File Offset: 0x000FED5F
		// (set) Token: 0x060044E1 RID: 17633 RVA: 0x00100B71 File Offset: 0x000FED71
		[ConfigurationProperty("DoValidationAfterFullSyncEnabled", DefaultValue = true)]
		public bool ValidationAfterFullSyncEnabled
		{
			get
			{
				return (bool)base["DoValidationAfterFullSyncEnabled"];
			}
			set
			{
				base["DoValidationAfterFullSyncEnabled"] = value;
			}
		}

		// Token: 0x170016B4 RID: 5812
		// (get) Token: 0x060044E2 RID: 17634 RVA: 0x00100B84 File Offset: 0x000FED84
		// (set) Token: 0x060044E3 RID: 17635 RVA: 0x00100B96 File Offset: 0x000FED96
		[ConfigurationProperty("MaxNumberOfTransitions", DefaultValue = 30)]
		public int MaxNumberOfTransitions
		{
			get
			{
				return (int)base["MaxNumberOfTransitions"];
			}
			set
			{
				base["MaxNumberOfTransitions"] = value;
			}
		}

		// Token: 0x170016B5 RID: 5813
		// (get) Token: 0x060044E4 RID: 17636 RVA: 0x00100BA9 File Offset: 0x000FEDA9
		// (set) Token: 0x060044E5 RID: 17637 RVA: 0x00100BBB File Offset: 0x000FEDBB
		[ConfigurationProperty("ValidateDomainRecordsInGls", DefaultValue = true)]
		public bool ValidateDomainRecordsInGls
		{
			get
			{
				return (bool)base["ValidateDomainRecordsInGls"];
			}
			set
			{
				base["ValidateDomainRecordsInGls"] = value;
			}
		}

		// Token: 0x170016B6 RID: 5814
		// (get) Token: 0x060044E6 RID: 17638 RVA: 0x00100BCE File Offset: 0x000FEDCE
		// (set) Token: 0x060044E7 RID: 17639 RVA: 0x00100BE0 File Offset: 0x000FEDE0
		[ConfigurationProperty("ValidateDomainRecordsInMServ", DefaultValue = true)]
		public bool ValidateDomainRecordsInMServ
		{
			get
			{
				return (bool)base["ValidateDomainRecordsInMServ"];
			}
			set
			{
				base["ValidateDomainRecordsInMServ"] = value;
			}
		}

		// Token: 0x170016B7 RID: 5815
		// (get) Token: 0x060044E8 RID: 17640 RVA: 0x00100BF3 File Offset: 0x000FEDF3
		// (set) Token: 0x060044E9 RID: 17641 RVA: 0x00100C05 File Offset: 0x000FEE05
		[ConfigurationProperty("ValidateMXRecordsInDNS", DefaultValue = true)]
		public bool ValidateMXRecordsInDNS
		{
			get
			{
				return (bool)base["ValidateMXRecordsInDNS"];
			}
			set
			{
				base["ValidateMXRecordsInDNS"] = value;
			}
		}

		// Token: 0x170016B8 RID: 5816
		// (get) Token: 0x060044EA RID: 17642 RVA: 0x00100C18 File Offset: 0x000FEE18
		// (set) Token: 0x060044EB RID: 17643 RVA: 0x00100C2A File Offset: 0x000FEE2A
		[ConfigurationProperty("MaxNumberOfRelocationsInRelocationPipeline", DefaultValue = 1000)]
		public int MaxNumberOfRelocationsInRelocationPipeline
		{
			get
			{
				return (int)base["MaxNumberOfRelocationsInRelocationPipeline"];
			}
			set
			{
				base["MaxNumberOfRelocationsInRelocationPipeline"] = value;
			}
		}

		// Token: 0x170016B9 RID: 5817
		// (get) Token: 0x060044EC RID: 17644 RVA: 0x00100C3D File Offset: 0x000FEE3D
		// (set) Token: 0x060044ED RID: 17645 RVA: 0x00100C4F File Offset: 0x000FEE4F
		[ConfigurationProperty("DoValidationAfterDeltaSyncEnabled", DefaultValue = true)]
		public bool DoValidationAfterDeltaSyncEnabled
		{
			get
			{
				return (bool)base["DoValidationAfterDeltaSyncEnabled"];
			}
			set
			{
				base["DoValidationAfterDeltaSyncEnabled"] = value;
			}
		}

		// Token: 0x170016BA RID: 5818
		// (get) Token: 0x060044EE RID: 17646 RVA: 0x00100C62 File Offset: 0x000FEE62
		// (set) Token: 0x060044EF RID: 17647 RVA: 0x00100C74 File Offset: 0x000FEE74
		[ConfigurationProperty("CleanupSchedule", DefaultValue = "From9AMTo5PMAtWeekDays")]
		public string CleanupSchedule
		{
			get
			{
				return (string)base["CleanupSchedule"];
			}
			set
			{
				base["CleanupSchedule"] = value;
			}
		}

		// Token: 0x170016BB RID: 5819
		// (get) Token: 0x060044F0 RID: 17648 RVA: 0x00100C82 File Offset: 0x000FEE82
		// (set) Token: 0x060044F1 RID: 17649 RVA: 0x00100C94 File Offset: 0x000FEE94
		[ConfigurationProperty("ADHealthSamplerPollIntervalInMinutes", DefaultValue = 15)]
		public int ADHealthSamplerPollIntervalInMinutes
		{
			get
			{
				return (int)base["ADHealthSamplerPollIntervalInMinutes"];
			}
			set
			{
				base["ADHealthSamplerPollIntervalInMinutes"] = value;
			}
		}

		// Token: 0x170016BC RID: 5820
		// (get) Token: 0x060044F2 RID: 17650 RVA: 0x00100CA7 File Offset: 0x000FEEA7
		// (set) Token: 0x060044F3 RID: 17651 RVA: 0x00100CB9 File Offset: 0x000FEEB9
		[ConfigurationProperty("ADReplicationHealthSamplerEnabled", DefaultValue = true)]
		public bool ADReplicationHealthSamplerEnabled
		{
			get
			{
				return (bool)base["ADReplicationHealthSamplerEnabled"];
			}
			set
			{
				base["ADReplicationHealthSamplerEnabled"] = value;
			}
		}

		// Token: 0x170016BD RID: 5821
		// (get) Token: 0x060044F4 RID: 17652 RVA: 0x00100CCC File Offset: 0x000FEECC
		// (set) Token: 0x060044F5 RID: 17653 RVA: 0x00100CDE File Offset: 0x000FEEDE
		[ConfigurationProperty("LoadStateNoDelayMs", DefaultValue = 0)]
		public int LoadStateNoDelayMs
		{
			get
			{
				return (int)base["LoadStateNoDelayMs"];
			}
			set
			{
				base["LoadStateNoDelayMs"] = value;
			}
		}

		// Token: 0x170016BE RID: 5822
		// (get) Token: 0x060044F6 RID: 17654 RVA: 0x00100CF1 File Offset: 0x000FEEF1
		// (set) Token: 0x060044F7 RID: 17655 RVA: 0x00100D03 File Offset: 0x000FEF03
		[ConfigurationProperty("LoadStateDefaultDelayMs", DefaultValue = 100)]
		public int LoadStateDefaultDelayMs
		{
			get
			{
				return (int)base["LoadStateDefaultDelayMs"];
			}
			set
			{
				base["LoadStateDefaultDelayMs"] = value;
			}
		}

		// Token: 0x170016BF RID: 5823
		// (get) Token: 0x060044F8 RID: 17656 RVA: 0x00100D16 File Offset: 0x000FEF16
		// (set) Token: 0x060044F9 RID: 17657 RVA: 0x00100D28 File Offset: 0x000FEF28
		[ConfigurationProperty("LoadStateOverloadedDelayMs", DefaultValue = 500)]
		public int LoadStateOverloadedDelayMs
		{
			get
			{
				return (int)base["LoadStateOverloadedDelayMs"];
			}
			set
			{
				base["LoadStateOverloadedDelayMs"] = value;
			}
		}

		// Token: 0x170016C0 RID: 5824
		// (get) Token: 0x060044FA RID: 17658 RVA: 0x00100D3B File Offset: 0x000FEF3B
		// (set) Token: 0x060044FB RID: 17659 RVA: 0x00100D4D File Offset: 0x000FEF4D
		[ConfigurationProperty("LoadStateCriticalDelayMs", DefaultValue = 1000)]
		public int LoadStateCriticalDelayMs
		{
			get
			{
				return (int)base["LoadStateCriticalDelayMs"];
			}
			set
			{
				base["LoadStateCriticalDelayMs"] = value;
			}
		}

		// Token: 0x170016C1 RID: 5825
		// (get) Token: 0x060044FC RID: 17660 RVA: 0x00100D60 File Offset: 0x000FEF60
		// (set) Token: 0x060044FD RID: 17661 RVA: 0x00100D72 File Offset: 0x000FEF72
		[ConfigurationProperty("CleanupDryRunEnabled", DefaultValue = false)]
		public bool CleanupDryRunEnabled
		{
			get
			{
				return (bool)base["CleanupDryRunEnabled"];
			}
			set
			{
				base["CleanupDryRunEnabled"] = value;
			}
		}

		// Token: 0x170016C2 RID: 5826
		// (get) Token: 0x060044FE RID: 17662 RVA: 0x00100D85 File Offset: 0x000FEF85
		// (set) Token: 0x060044FF RID: 17663 RVA: 0x00100D97 File Offset: 0x000FEF97
		[ConfigurationProperty("SuspendGlsCache", DefaultValue = true)]
		public bool SuspendGlsCache
		{
			get
			{
				return (bool)base["SuspendGlsCache"];
			}
			set
			{
				base["SuspendGlsCache"] = value;
			}
		}

		// Token: 0x06004500 RID: 17664 RVA: 0x00100DAA File Offset: 0x000FEFAA
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, string>(0L, "Unrecognized configuration attribute {0}={1}", name, value);
			return base.OnDeserializeUnrecognizedAttribute(name, value);
		}

		// Token: 0x020005CF RID: 1487
		public static class Setting
		{
			// Token: 0x04002EC2 RID: 11970
			public const string IsAllRelocationActivitiesHalted = "IsAllRelocationActivitiesHalted";

			// Token: 0x04002EC3 RID: 11971
			public const string IsProcessingBrokerEnabled = "IsProcessingBrokerEnabled";

			// Token: 0x04002EC4 RID: 11972
			public const string IsRollbackBrokerEnabled = "IsRollbackBrokerEnabled";

			// Token: 0x04002EC5 RID: 11973
			public const string IsCleanupBrokerEnabled = "IsCleanupBrokerEnabled";

			// Token: 0x04002EC6 RID: 11974
			public const string IsOrchestratorEnabled = "IsOrchestratorEnabled";

			// Token: 0x04002EC7 RID: 11975
			public const string DataSyncObjectsPerPageLimit = "DataSyncObjectsPerPageLimit";

			// Token: 0x04002EC8 RID: 11976
			public const string DataSyncLinksPerPageLimit = "DataSyncLinksPerPageLimit";

			// Token: 0x04002EC9 RID: 11977
			public const string DataSyncInitialLinkReadSize = "DataSyncInitialLinkReadSize";

			// Token: 0x04002ECA RID: 11978
			public const string DataSyncFailoverTimeoutInMinutes = "DataSyncFailoverTimeoutInMinutes";

			// Token: 0x04002ECB RID: 11979
			public const string DataSyncLinksOverldapSize = "DataSyncLinksOverldapSize";

			// Token: 0x04002ECC RID: 11980
			public const string DeltaSyncUsnRangeLimit = "DeltaSyncUsnRangeLimit";

			// Token: 0x04002ECD RID: 11981
			public const string MaxAllowedReplicationLatencyInMinutes = "MaxAllowedReplicationLatencyInMinutes";

			// Token: 0x04002ECE RID: 11982
			public const string MaxConcurrentProcessingThreadsPerServer = "MaxConcurrentProcessingThreadsPerServer";

			// Token: 0x04002ECF RID: 11983
			public const string MaxConcurrentRollbackThreadsPerServer = "MaxConcurrentRollbackThreadsPerServer";

			// Token: 0x04002ED0 RID: 11984
			public const string MaxConcurrentCleanupThreadsPerServer = "MaxConcurrentCleanupThreadsPerServer";

			// Token: 0x04002ED1 RID: 11985
			public const string ProcessingBrokerPollIntervalInMinutes = "ProcessingBrokerPollIntervalInMinutes";

			// Token: 0x04002ED2 RID: 11986
			public const string RollbackBrokerPollIntervalInMinutes = "RollbackBrokerPollIntervalInMinutes";

			// Token: 0x04002ED3 RID: 11987
			public const string IsUserExperienceTestEnabled = "IsUserExperienceTestEnabled";

			// Token: 0x04002ED4 RID: 11988
			public const string DisabledUXProbes = "DisabledUXProbes";

			// Token: 0x04002ED5 RID: 11989
			public const string UXProbeRecurrenceIntervalSeconds = "UXProbeRecurrenceIntervalSeconds";

			// Token: 0x04002ED6 RID: 11990
			public const string UXMonitorConsecutiveProbeFailureCount = "UXMonitorConsecutiveProbeFailureCount";

			// Token: 0x04002ED7 RID: 11991
			public const string UXMonitorAccountExpiredDays = "UXMonitorAccountExpiredDays";

			// Token: 0x04002ED8 RID: 11992
			public const string RemoveUXMonitorAccountWaitReplicationMinutes = "RemoveUXMonitorAccountWaitReplicationMinutes";

			// Token: 0x04002ED9 RID: 11993
			public const string WaitUXFailureResultSeconds = "WaitUXFailureResultSeconds";

			// Token: 0x04002EDA RID: 11994
			public const string UXTransportProbeSmtpServers = "UXTransportProbeSmtpServers";

			// Token: 0x04002EDB RID: 11995
			public const string UXTransportProbeSmtpPort = "UXTransportProbeSmtpPort";

			// Token: 0x04002EDC RID: 11996
			public const string UXTransportProbeSenderAddress = "UXTransportProbeSenderAddress";

			// Token: 0x04002EDD RID: 11997
			public const string UXTransportProbeSendMessageTimeout = "UXTransportProbeSendMessageTimeout";

			// Token: 0x04002EDE RID: 11998
			public const string UXTransportProbeWaitMessageTimeout = "UXTransportProbeWaitMessageTimeout";

			// Token: 0x04002EDF RID: 11999
			public const string CheckStaleRelocations = "CheckStaleRelocations";

			// Token: 0x04002EE0 RID: 12000
			public const string SafeScheduleWindow = "SafeScheduleWindow";

			// Token: 0x04002EE1 RID: 12001
			public const string MaxRelocationInNonCriticalStage = "MaxRelocationInNonCriticalStage";

			// Token: 0x04002EE2 RID: 12002
			public const string MaxRelocationInCriticalStage = "MaxRelocationInCriticalStage";

			// Token: 0x04002EE3 RID: 12003
			public const string MaxRelocationInCleanupStage = "MaxRelocationInCleanupStage";

			// Token: 0x04002EE4 RID: 12004
			public const string OrchestratorSleepIntervalBetweenRetriesInMinutes = "OrchestratorSleepIntervalBetweenRetriesInMinutes";

			// Token: 0x04002EE5 RID: 12005
			public const string ADDriverValidatorEnabled = "ADDriverValidatorEnabled";

			// Token: 0x04002EE6 RID: 12006
			public const string RemoveSourceForestLinkOnRetirement = "RemoveSourceForestLinkOnRetirement";

			// Token: 0x04002EE7 RID: 12007
			public const string RemoveSourceForestLinkOnCleanup = "RemoveSourceForestLinkOnCleanup";

			// Token: 0x04002EE8 RID: 12008
			public const string TranslateSupportedSharedConfigurations = "TranslateSupportedSharedConfigurations";

			// Token: 0x04002EE9 RID: 12009
			public const string IgnoreRelocationConstraintExpiration = "IgnoreRelocationConstraintExpiration";

			// Token: 0x04002EEA RID: 12010
			public const string AutoSelectTargetPartition = "AutoSelectTargetPartition";

			// Token: 0x04002EEB RID: 12011
			public const string DefaultRelocationCacheExpirationTimeInMinutes = "DefaultRelocationCacheExpirationTimeInMinutes";

			// Token: 0x04002EEC RID: 12012
			public const string ModerateRelocationCacheExpirationTimeInMinutes = "ModerateRelocationCacheExpirationTimeInMinutes";

			// Token: 0x04002EED RID: 12013
			public const string AggressiveRelocationCacheExpirationTimeInMinutes = "AggressiveRelocationCacheExpirationTimeInMinutes";

			// Token: 0x04002EEE RID: 12014
			public const string DedicatedOrchestrator = "DedicatedOrchestrator";

			// Token: 0x04002EEF RID: 12015
			public const string WaitForGlsCacheUpdateMinutes = "WaitForGlsCacheUpdateMinutes";

			// Token: 0x04002EF0 RID: 12016
			public const string GlsReadRetries = "GlsReadRetries";

			// Token: 0x04002EF1 RID: 12017
			public const string MaxTenantLockDownTimeInMinutes = "MaxTenantLockDownTimeInMinutes";

			// Token: 0x04002EF2 RID: 12018
			public const string DoValidationAfterFullSyncEnabled = "DoValidationAfterFullSyncEnabled";

			// Token: 0x04002EF3 RID: 12019
			public const string MaxNumberOfTransitions = "MaxNumberOfTransitions";

			// Token: 0x04002EF4 RID: 12020
			public const string ValidateDomainRecordsInGls = "ValidateDomainRecordsInGls";

			// Token: 0x04002EF5 RID: 12021
			public const string ValidateDomainRecordsInMServ = "ValidateDomainRecordsInMServ";

			// Token: 0x04002EF6 RID: 12022
			public const string ValidateMXRecordsInDNS = "ValidateMXRecordsInDNS";

			// Token: 0x04002EF7 RID: 12023
			public const string MaxNumberOfRelocationsInRelocationPipeline = "MaxNumberOfRelocationsInRelocationPipeline";

			// Token: 0x04002EF8 RID: 12024
			public const string DoValidationAfterDeltaSyncEnabled = "DoValidationAfterDeltaSyncEnabled";

			// Token: 0x04002EF9 RID: 12025
			public const string CleanupSchedule = "CleanupSchedule";

			// Token: 0x04002EFA RID: 12026
			public const string ADHealthSamplerPollIntervalInMinutes = "ADHealthSamplerPollIntervalInMinutes";

			// Token: 0x04002EFB RID: 12027
			public const string ADReplicationHealthSamplerEnabled = "ADReplicationHealthSamplerEnabled";

			// Token: 0x04002EFC RID: 12028
			public const string LoadStateNoDelayMs = "LoadStateNoDelayMs";

			// Token: 0x04002EFD RID: 12029
			public const string LoadStateDefaultDelayMs = "LoadStateDefaultDelayMs";

			// Token: 0x04002EFE RID: 12030
			public const string LoadStateOverloadedDelayMs = "LoadStateOverloadedDelayMs";

			// Token: 0x04002EFF RID: 12031
			public const string LoadStateCriticalDelayMs = "LoadStateCriticalDelayMs";

			// Token: 0x04002F00 RID: 12032
			public const string CleanupDryRunEnabled = "CleanupDryRunEnabled";

			// Token: 0x04002F01 RID: 12033
			public const string SuspendGlsCache = "SuspendGlsCache";
		}
	}
}
