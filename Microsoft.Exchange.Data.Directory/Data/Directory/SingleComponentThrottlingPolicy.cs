using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009CF RID: 2511
	internal class SingleComponentThrottlingPolicy
	{
		// Token: 0x06007437 RID: 29751 RVA: 0x0017F760 File Offset: 0x0017D960
		internal SingleComponentThrottlingPolicy(BudgetType budgetType, IThrottlingPolicy fullPolicy)
		{
			this.budgetType = budgetType;
			if (fullPolicy == null)
			{
				throw new ArgumentNullException("fullPolicy");
			}
			this.fullPolicy = fullPolicy;
		}

		// Token: 0x17002965 RID: 10597
		// (get) Token: 0x06007438 RID: 29752 RVA: 0x0017F784 File Offset: 0x0017D984
		// (set) Token: 0x06007439 RID: 29753 RVA: 0x0017F78B File Offset: 0x0017D98B
		public static uint BulkOperationConcurrencyCap
		{
			get
			{
				return SingleComponentThrottlingPolicy.bulkOperationConcurrencyCap;
			}
			set
			{
				SingleComponentThrottlingPolicy.bulkOperationConcurrencyCap = value;
			}
		}

		// Token: 0x17002966 RID: 10598
		// (get) Token: 0x0600743A RID: 29754 RVA: 0x0017F793 File Offset: 0x0017D993
		// (set) Token: 0x0600743B RID: 29755 RVA: 0x0017F79A File Offset: 0x0017D99A
		public static uint NonInteractiveOperationConcurrencyCap
		{
			get
			{
				return SingleComponentThrottlingPolicy.nonInteractiveOperationConcurrencyCap;
			}
			set
			{
				SingleComponentThrottlingPolicy.nonInteractiveOperationConcurrencyCap = value;
			}
		}

		// Token: 0x17002967 RID: 10599
		// (get) Token: 0x0600743C RID: 29756 RVA: 0x0017F7A4 File Offset: 0x0017D9A4
		public virtual Unlimited<uint> MaxConcurrency
		{
			get
			{
				switch (this.budgetType)
				{
				case BudgetType.Owa:
					return this.fullPolicy.OwaMaxConcurrency;
				case BudgetType.Ews:
					return this.fullPolicy.EwsMaxConcurrency;
				case BudgetType.Eas:
					return this.fullPolicy.EasMaxConcurrency;
				case BudgetType.Pop:
					return this.fullPolicy.PopMaxConcurrency;
				case BudgetType.Imap:
					return this.fullPolicy.ImapMaxConcurrency;
				case BudgetType.PowerShell:
				case BudgetType.WSMan:
					return this.fullPolicy.PowerShellMaxConcurrency;
				case BudgetType.Rca:
					return this.fullPolicy.RcaMaxConcurrency;
				case BudgetType.Cpa:
					return this.fullPolicy.CpaMaxConcurrency;
				case BudgetType.Anonymous:
					return this.fullPolicy.AnonymousMaxConcurrency;
				case BudgetType.ResourceTracking:
					return Unlimited<uint>.UnlimitedValue;
				case BudgetType.WSManTenant:
					return this.fullPolicy.PowerShellMaxTenantConcurrency;
				case BudgetType.OwaVoice:
					return this.fullPolicy.OwaVoiceMaxConcurrency;
				case BudgetType.PushNotificationTenant:
					return this.fullPolicy.PushNotificationMaxConcurrency;
				case BudgetType.EwsBulkOperation:
					return SingleComponentThrottlingPolicy.BulkOperationConcurrencyCap;
				case BudgetType.OwaBulkOperation:
					return SingleComponentThrottlingPolicy.BulkOperationConcurrencyCap;
				case BudgetType.OwaNonInteractiveOperation:
					return SingleComponentThrottlingPolicy.NonInteractiveOperationConcurrencyCap;
				case BudgetType.E4eSender:
					return this.fullPolicy.EncryptionSenderMaxConcurrency;
				case BudgetType.E4eRecipient:
					return this.fullPolicy.EncryptionRecipientMaxConcurrency;
				case BudgetType.OutlookService:
					return this.fullPolicy.OutlookServiceMaxConcurrency;
				default:
					return Unlimited<uint>.UnlimitedValue;
				}
			}
		}

		// Token: 0x17002968 RID: 10600
		// (get) Token: 0x0600743D RID: 29757 RVA: 0x0017F8F4 File Offset: 0x0017DAF4
		public virtual Unlimited<uint> MaxBurst
		{
			get
			{
				switch (this.budgetType)
				{
				case BudgetType.Owa:
				case BudgetType.OwaBulkOperation:
				case BudgetType.OwaNonInteractiveOperation:
					return this.fullPolicy.OwaMaxBurst;
				case BudgetType.Ews:
				case BudgetType.EwsBulkOperation:
					return this.fullPolicy.EwsMaxBurst;
				case BudgetType.Eas:
					return this.fullPolicy.EasMaxBurst;
				case BudgetType.Pop:
					return this.fullPolicy.PopMaxBurst;
				case BudgetType.Imap:
					return this.fullPolicy.ImapMaxBurst;
				case BudgetType.PowerShell:
				case BudgetType.WSMan:
				case BudgetType.WSManTenant:
					return this.fullPolicy.PowerShellMaxBurst;
				case BudgetType.Rca:
					return this.fullPolicy.RcaMaxBurst;
				case BudgetType.Cpa:
					return this.fullPolicy.CpaMaxBurst;
				case BudgetType.Anonymous:
					return this.fullPolicy.AnonymousMaxBurst;
				case BudgetType.ResourceTracking:
					return Unlimited<uint>.UnlimitedValue;
				case BudgetType.OwaVoice:
					return this.fullPolicy.OwaVoiceMaxBurst;
				case BudgetType.PushNotificationTenant:
					return this.fullPolicy.PushNotificationMaxBurst;
				case BudgetType.E4eSender:
					return this.fullPolicy.EncryptionSenderMaxBurst;
				case BudgetType.E4eRecipient:
					return this.fullPolicy.EncryptionRecipientMaxBurst;
				case BudgetType.OutlookService:
					return this.fullPolicy.OutlookServiceMaxBurst;
				default:
					return Unlimited<uint>.UnlimitedValue;
				}
			}
		}

		// Token: 0x17002969 RID: 10601
		// (get) Token: 0x0600743E RID: 29758 RVA: 0x0017FA18 File Offset: 0x0017DC18
		public virtual Unlimited<uint> RechargeRate
		{
			get
			{
				switch (this.budgetType)
				{
				case BudgetType.Owa:
				case BudgetType.OwaBulkOperation:
				case BudgetType.OwaNonInteractiveOperation:
					return this.fullPolicy.OwaRechargeRate;
				case BudgetType.Ews:
				case BudgetType.EwsBulkOperation:
					return this.fullPolicy.EwsRechargeRate;
				case BudgetType.Eas:
					return this.fullPolicy.EasRechargeRate;
				case BudgetType.Pop:
					return this.fullPolicy.PopRechargeRate;
				case BudgetType.Imap:
					return this.fullPolicy.ImapRechargeRate;
				case BudgetType.PowerShell:
				case BudgetType.WSMan:
				case BudgetType.WSManTenant:
					return this.fullPolicy.PowerShellRechargeRate;
				case BudgetType.Rca:
					return this.fullPolicy.RcaRechargeRate;
				case BudgetType.Cpa:
					return this.fullPolicy.CpaRechargeRate;
				case BudgetType.Anonymous:
					return this.fullPolicy.AnonymousRechargeRate;
				case BudgetType.ResourceTracking:
					return Unlimited<uint>.UnlimitedValue;
				case BudgetType.OwaVoice:
					return this.fullPolicy.OwaVoiceRechargeRate;
				case BudgetType.PushNotificationTenant:
					return this.fullPolicy.PushNotificationRechargeRate;
				case BudgetType.E4eSender:
					return this.fullPolicy.EncryptionSenderRechargeRate;
				case BudgetType.E4eRecipient:
					return this.fullPolicy.EncryptionRecipientRechargeRate;
				case BudgetType.OutlookService:
					return this.fullPolicy.OutlookServiceRechargeRate;
				default:
					return Unlimited<uint>.UnlimitedValue;
				}
			}
		}

		// Token: 0x1700296A RID: 10602
		// (get) Token: 0x0600743F RID: 29759 RVA: 0x0017FB3C File Offset: 0x0017DD3C
		public virtual Unlimited<uint> CutoffBalance
		{
			get
			{
				switch (this.budgetType)
				{
				case BudgetType.Owa:
				case BudgetType.OwaBulkOperation:
				case BudgetType.OwaNonInteractiveOperation:
					return this.fullPolicy.OwaCutoffBalance;
				case BudgetType.Ews:
				case BudgetType.EwsBulkOperation:
					return this.fullPolicy.EwsCutoffBalance;
				case BudgetType.Eas:
					return this.fullPolicy.EasCutoffBalance;
				case BudgetType.Pop:
					return this.fullPolicy.PopCutoffBalance;
				case BudgetType.Imap:
					return this.fullPolicy.ImapCutoffBalance;
				case BudgetType.PowerShell:
				case BudgetType.WSMan:
				case BudgetType.WSManTenant:
					return this.fullPolicy.PowerShellCutoffBalance;
				case BudgetType.Rca:
					return this.fullPolicy.RcaCutoffBalance;
				case BudgetType.Cpa:
					return this.fullPolicy.CpaCutoffBalance;
				case BudgetType.Anonymous:
					return this.fullPolicy.AnonymousCutoffBalance;
				case BudgetType.ResourceTracking:
					return Unlimited<uint>.UnlimitedValue;
				case BudgetType.OwaVoice:
					return this.fullPolicy.OwaVoiceCutoffBalance;
				case BudgetType.PushNotificationTenant:
					return this.fullPolicy.PushNotificationCutoffBalance;
				case BudgetType.E4eSender:
					return this.fullPolicy.EncryptionSenderCutoffBalance;
				case BudgetType.E4eRecipient:
					return this.fullPolicy.EncryptionRecipientCutoffBalance;
				case BudgetType.OutlookService:
					return this.fullPolicy.OutlookServiceCutoffBalance;
				default:
					return Unlimited<uint>.UnlimitedValue;
				}
			}
		}

		// Token: 0x1700296B RID: 10603
		// (get) Token: 0x06007440 RID: 29760 RVA: 0x0017FC5E File Offset: 0x0017DE5E
		public Unlimited<uint> PowerShellMaxTenantConcurrency
		{
			get
			{
				return this.fullPolicy.PowerShellMaxTenantConcurrency;
			}
		}

		// Token: 0x1700296C RID: 10604
		// (get) Token: 0x06007441 RID: 29761 RVA: 0x0017FC6B File Offset: 0x0017DE6B
		public Unlimited<uint> PowerShellMaxOperations
		{
			get
			{
				return this.fullPolicy.PowerShellMaxOperations;
			}
		}

		// Token: 0x1700296D RID: 10605
		// (get) Token: 0x06007442 RID: 29762 RVA: 0x0017FC78 File Offset: 0x0017DE78
		public Unlimited<uint> PowerShellMaxBurst
		{
			get
			{
				return this.fullPolicy.PowerShellMaxBurst;
			}
		}

		// Token: 0x1700296E RID: 10606
		// (get) Token: 0x06007443 RID: 29763 RVA: 0x0017FC85 File Offset: 0x0017DE85
		public Unlimited<uint> PowerShellRechargeRate
		{
			get
			{
				return this.fullPolicy.PowerShellRechargeRate;
			}
		}

		// Token: 0x1700296F RID: 10607
		// (get) Token: 0x06007444 RID: 29764 RVA: 0x0017FC92 File Offset: 0x0017DE92
		public Unlimited<uint> PowerShellCutoffBalance
		{
			get
			{
				return this.fullPolicy.PowerShellCutoffBalance;
			}
		}

		// Token: 0x17002970 RID: 10608
		// (get) Token: 0x06007445 RID: 29765 RVA: 0x0017FC9F File Offset: 0x0017DE9F
		public Unlimited<uint> PowerShellMaxCmdletsTimePeriod
		{
			get
			{
				return this.fullPolicy.PowerShellMaxCmdletsTimePeriod;
			}
		}

		// Token: 0x17002971 RID: 10609
		// (get) Token: 0x06007446 RID: 29766 RVA: 0x0017FCAC File Offset: 0x0017DEAC
		public Unlimited<uint> ExchangeMaxCmdlets
		{
			get
			{
				return this.fullPolicy.ExchangeMaxCmdlets;
			}
		}

		// Token: 0x17002972 RID: 10610
		// (get) Token: 0x06007447 RID: 29767 RVA: 0x0017FCB9 File Offset: 0x0017DEB9
		public Unlimited<uint> PowerShellMaxCmdletQueueDepth
		{
			get
			{
				return this.fullPolicy.PowerShellMaxCmdletQueueDepth;
			}
		}

		// Token: 0x17002973 RID: 10611
		// (get) Token: 0x06007448 RID: 29768 RVA: 0x0017FCC6 File Offset: 0x0017DEC6
		public Unlimited<uint> PowerShellMaxDestructiveCmdlets
		{
			get
			{
				return this.fullPolicy.PowerShellMaxDestructiveCmdlets;
			}
		}

		// Token: 0x17002974 RID: 10612
		// (get) Token: 0x06007449 RID: 29769 RVA: 0x0017FCD3 File Offset: 0x0017DED3
		public Unlimited<uint> PowerShellMaxDestructiveCmdletsTimePeriod
		{
			get
			{
				return this.fullPolicy.PowerShellMaxDestructiveCmdletsTimePeriod;
			}
		}

		// Token: 0x17002975 RID: 10613
		// (get) Token: 0x0600744A RID: 29770 RVA: 0x0017FCE0 File Offset: 0x0017DEE0
		public Unlimited<uint> PowerShellMaxCmdlets
		{
			get
			{
				return this.fullPolicy.PowerShellMaxCmdlets;
			}
		}

		// Token: 0x17002976 RID: 10614
		// (get) Token: 0x0600744B RID: 29771 RVA: 0x0017FCED File Offset: 0x0017DEED
		public Unlimited<uint> PowerShellMaxRunspaces
		{
			get
			{
				return this.fullPolicy.PowerShellMaxRunspaces;
			}
		}

		// Token: 0x17002977 RID: 10615
		// (get) Token: 0x0600744C RID: 29772 RVA: 0x0017FCFA File Offset: 0x0017DEFA
		public Unlimited<uint> PowerShellMaxTenantRunspaces
		{
			get
			{
				return this.fullPolicy.PowerShellMaxTenantRunspaces;
			}
		}

		// Token: 0x17002978 RID: 10616
		// (get) Token: 0x0600744D RID: 29773 RVA: 0x0017FD07 File Offset: 0x0017DF07
		public Unlimited<uint> PowerShellMaxRunspacesTimePeriod
		{
			get
			{
				return this.fullPolicy.PowerShellMaxRunspacesTimePeriod;
			}
		}

		// Token: 0x17002979 RID: 10617
		// (get) Token: 0x0600744E RID: 29774 RVA: 0x0017FD14 File Offset: 0x0017DF14
		public Unlimited<uint> PswsMaxConcurrency
		{
			get
			{
				return this.fullPolicy.PswsMaxConcurrency;
			}
		}

		// Token: 0x1700297A RID: 10618
		// (get) Token: 0x0600744F RID: 29775 RVA: 0x0017FD21 File Offset: 0x0017DF21
		public Unlimited<uint> PswsMaxRequest
		{
			get
			{
				return this.fullPolicy.PswsMaxRequest;
			}
		}

		// Token: 0x1700297B RID: 10619
		// (get) Token: 0x06007450 RID: 29776 RVA: 0x0017FD2E File Offset: 0x0017DF2E
		public Unlimited<uint> PswsMaxRequestTimePeriod
		{
			get
			{
				return this.fullPolicy.PswsMaxRequestTimePeriod;
			}
		}

		// Token: 0x1700297C RID: 10620
		// (get) Token: 0x06007451 RID: 29777 RVA: 0x0017FD3B File Offset: 0x0017DF3B
		public Unlimited<uint> EasMaxDevices
		{
			get
			{
				return this.fullPolicy.EasMaxDevices;
			}
		}

		// Token: 0x1700297D RID: 10621
		// (get) Token: 0x06007452 RID: 29778 RVA: 0x0017FD48 File Offset: 0x0017DF48
		public Unlimited<uint> EasMaxDeviceDeletesPerMonth
		{
			get
			{
				return this.fullPolicy.EasMaxDeviceDeletesPerMonth;
			}
		}

		// Token: 0x1700297E RID: 10622
		// (get) Token: 0x06007453 RID: 29779 RVA: 0x0017FD55 File Offset: 0x0017DF55
		public Unlimited<uint> EasMaxInactivityForDeviceCleanup
		{
			get
			{
				return this.fullPolicy.EasMaxInactivityForDeviceCleanup;
			}
		}

		// Token: 0x1700297F RID: 10623
		// (get) Token: 0x06007454 RID: 29780 RVA: 0x0017FD62 File Offset: 0x0017DF62
		public Unlimited<uint> DiscoveryMaxConcurrency
		{
			get
			{
				return this.fullPolicy.DiscoveryMaxConcurrency;
			}
		}

		// Token: 0x17002980 RID: 10624
		// (get) Token: 0x06007455 RID: 29781 RVA: 0x0017FD6F File Offset: 0x0017DF6F
		public Unlimited<uint> DiscoveryMaxMailboxes
		{
			get
			{
				return this.fullPolicy.DiscoveryMaxMailboxes;
			}
		}

		// Token: 0x17002981 RID: 10625
		// (get) Token: 0x06007456 RID: 29782 RVA: 0x0017FD7C File Offset: 0x0017DF7C
		public Unlimited<uint> DiscoveryMaxKeywords
		{
			get
			{
				return this.fullPolicy.DiscoveryMaxKeywords;
			}
		}

		// Token: 0x17002982 RID: 10626
		// (get) Token: 0x06007457 RID: 29783 RVA: 0x0017FD89 File Offset: 0x0017DF89
		public Unlimited<uint> DiscoveryMaxPreviewSearchMailboxes
		{
			get
			{
				return this.fullPolicy.DiscoveryMaxPreviewSearchMailboxes;
			}
		}

		// Token: 0x17002983 RID: 10627
		// (get) Token: 0x06007458 RID: 29784 RVA: 0x0017FD96 File Offset: 0x0017DF96
		public Unlimited<uint> DiscoveryMaxStatsSearchMailboxes
		{
			get
			{
				return this.fullPolicy.DiscoveryMaxStatsSearchMailboxes;
			}
		}

		// Token: 0x17002984 RID: 10628
		// (get) Token: 0x06007459 RID: 29785 RVA: 0x0017FDA3 File Offset: 0x0017DFA3
		public Unlimited<uint> DiscoveryPreviewSearchResultsPageSize
		{
			get
			{
				return this.fullPolicy.DiscoveryPreviewSearchResultsPageSize;
			}
		}

		// Token: 0x17002985 RID: 10629
		// (get) Token: 0x0600745A RID: 29786 RVA: 0x0017FDB0 File Offset: 0x0017DFB0
		public Unlimited<uint> DiscoveryMaxKeywordsPerPage
		{
			get
			{
				return this.fullPolicy.DiscoveryMaxKeywordsPerPage;
			}
		}

		// Token: 0x17002986 RID: 10630
		// (get) Token: 0x0600745B RID: 29787 RVA: 0x0017FDBD File Offset: 0x0017DFBD
		public Unlimited<uint> DiscoveryMaxRefinerResults
		{
			get
			{
				return this.fullPolicy.DiscoveryMaxRefinerResults;
			}
		}

		// Token: 0x17002987 RID: 10631
		// (get) Token: 0x0600745C RID: 29788 RVA: 0x0017FDCA File Offset: 0x0017DFCA
		public Unlimited<uint> DiscoveryMaxSearchQueueDepth
		{
			get
			{
				return this.fullPolicy.DiscoveryMaxSearchQueueDepth;
			}
		}

		// Token: 0x17002988 RID: 10632
		// (get) Token: 0x0600745D RID: 29789 RVA: 0x0017FDD7 File Offset: 0x0017DFD7
		public Unlimited<uint> DiscoverySearchTimeoutPeriod
		{
			get
			{
				return this.fullPolicy.DiscoverySearchTimeoutPeriod;
			}
		}

		// Token: 0x17002989 RID: 10633
		// (get) Token: 0x0600745E RID: 29790 RVA: 0x0017FDE4 File Offset: 0x0017DFE4
		public Unlimited<uint> ComplianceMaxExpansionDGRecipients
		{
			get
			{
				return this.fullPolicy.ComplianceMaxExpansionDGRecipients;
			}
		}

		// Token: 0x1700298A RID: 10634
		// (get) Token: 0x0600745F RID: 29791 RVA: 0x0017FDF1 File Offset: 0x0017DFF1
		public Unlimited<uint> ComplianceMaxExpansionNestedDGs
		{
			get
			{
				return this.fullPolicy.ComplianceMaxExpansionNestedDGs;
			}
		}

		// Token: 0x1700298B RID: 10635
		// (get) Token: 0x06007460 RID: 29792 RVA: 0x0017FDFE File Offset: 0x0017DFFE
		public Unlimited<uint> PushNotificationMaxConcurrency
		{
			get
			{
				return this.MaxConcurrency;
			}
		}

		// Token: 0x1700298C RID: 10636
		// (get) Token: 0x06007461 RID: 29793 RVA: 0x0017FE06 File Offset: 0x0017E006
		public Unlimited<uint> OutlookServiceMaxSubscriptions
		{
			get
			{
				return this.fullPolicy.OutlookServiceMaxSubscriptions;
			}
		}

		// Token: 0x1700298D RID: 10637
		// (get) Token: 0x06007462 RID: 29794 RVA: 0x0017FE13 File Offset: 0x0017E013
		public Unlimited<uint> OutlookServiceMaxSocketConnectionsPerDevice
		{
			get
			{
				return this.fullPolicy.OutlookServiceMaxSocketConnectionsPerDevice;
			}
		}

		// Token: 0x1700298E RID: 10638
		// (get) Token: 0x06007463 RID: 29795 RVA: 0x0017FE20 File Offset: 0x0017E020
		public Unlimited<uint> OutlookServiceMaxSocketConnectionsPerUser
		{
			get
			{
				return this.fullPolicy.OutlookServiceMaxSocketConnectionsPerUser;
			}
		}

		// Token: 0x1700298F RID: 10639
		// (get) Token: 0x06007464 RID: 29796 RVA: 0x0017FE2D File Offset: 0x0017E02D
		internal IThrottlingPolicy FullPolicy
		{
			get
			{
				return this.fullPolicy;
			}
		}

		// Token: 0x04004B0C RID: 19212
		private BudgetType budgetType;

		// Token: 0x04004B0D RID: 19213
		private IThrottlingPolicy fullPolicy;

		// Token: 0x04004B0E RID: 19214
		private static uint bulkOperationConcurrencyCap = 2U;

		// Token: 0x04004B0F RID: 19215
		private static uint nonInteractiveOperationConcurrencyCap = 2U;
	}
}
