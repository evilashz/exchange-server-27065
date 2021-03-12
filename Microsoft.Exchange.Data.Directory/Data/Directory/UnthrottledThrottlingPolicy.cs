using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009E0 RID: 2528
	internal class UnthrottledThrottlingPolicy : IThrottlingPolicy
	{
		// Token: 0x06007513 RID: 29971 RVA: 0x001821F3 File Offset: 0x001803F3
		public static UnthrottledThrottlingPolicy GetSingleton()
		{
			return UnthrottledThrottlingPolicy.singleton;
		}

		// Token: 0x170029A8 RID: 10664
		// (get) Token: 0x06007514 RID: 29972 RVA: 0x001821FA File Offset: 0x001803FA
		public bool IsFallback
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170029A9 RID: 10665
		// (get) Token: 0x06007515 RID: 29973 RVA: 0x001821FD File Offset: 0x001803FD
		public ThrottlingPolicyScopeType ThrottlingPolicyScope
		{
			get
			{
				return ThrottlingPolicyScopeType.Regular;
			}
		}

		// Token: 0x170029AA RID: 10666
		// (get) Token: 0x06007516 RID: 29974 RVA: 0x00182200 File Offset: 0x00180400
		public bool IsServiceAccount
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170029AB RID: 10667
		// (get) Token: 0x06007517 RID: 29975 RVA: 0x00182203 File Offset: 0x00180403
		public bool IsUnthrottled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170029AC RID: 10668
		// (get) Token: 0x06007518 RID: 29976 RVA: 0x00182206 File Offset: 0x00180406
		public Unlimited<uint> EasMaxConcurrency
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029AD RID: 10669
		// (get) Token: 0x06007519 RID: 29977 RVA: 0x0018220D File Offset: 0x0018040D
		public Unlimited<uint> EasMaxBurst
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029AE RID: 10670
		// (get) Token: 0x0600751A RID: 29978 RVA: 0x00182214 File Offset: 0x00180414
		public Unlimited<uint> EasRechargeRate
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029AF RID: 10671
		// (get) Token: 0x0600751B RID: 29979 RVA: 0x0018221B File Offset: 0x0018041B
		public Unlimited<uint> EasCutoffBalance
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029B0 RID: 10672
		// (get) Token: 0x0600751C RID: 29980 RVA: 0x00182222 File Offset: 0x00180422
		public Unlimited<uint> EasMaxDevices
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029B1 RID: 10673
		// (get) Token: 0x0600751D RID: 29981 RVA: 0x00182229 File Offset: 0x00180429
		public Unlimited<uint> EasMaxDeviceDeletesPerMonth
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029B2 RID: 10674
		// (get) Token: 0x0600751E RID: 29982 RVA: 0x00182230 File Offset: 0x00180430
		public Unlimited<uint> EasMaxInactivityForDeviceCleanup
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029B3 RID: 10675
		// (get) Token: 0x0600751F RID: 29983 RVA: 0x00182237 File Offset: 0x00180437
		public Unlimited<uint> EwsMaxConcurrency
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029B4 RID: 10676
		// (get) Token: 0x06007520 RID: 29984 RVA: 0x0018223E File Offset: 0x0018043E
		public Unlimited<uint> EwsMaxBurst
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029B5 RID: 10677
		// (get) Token: 0x06007521 RID: 29985 RVA: 0x00182245 File Offset: 0x00180445
		public Unlimited<uint> EwsRechargeRate
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029B6 RID: 10678
		// (get) Token: 0x06007522 RID: 29986 RVA: 0x0018224C File Offset: 0x0018044C
		public Unlimited<uint> EwsCutoffBalance
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029B7 RID: 10679
		// (get) Token: 0x06007523 RID: 29987 RVA: 0x00182253 File Offset: 0x00180453
		public Unlimited<uint> EwsMaxSubscriptions
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029B8 RID: 10680
		// (get) Token: 0x06007524 RID: 29988 RVA: 0x0018225A File Offset: 0x0018045A
		public Unlimited<uint> ImapMaxConcurrency
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029B9 RID: 10681
		// (get) Token: 0x06007525 RID: 29989 RVA: 0x00182261 File Offset: 0x00180461
		public Unlimited<uint> ImapMaxBurst
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029BA RID: 10682
		// (get) Token: 0x06007526 RID: 29990 RVA: 0x00182268 File Offset: 0x00180468
		public Unlimited<uint> ImapRechargeRate
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029BB RID: 10683
		// (get) Token: 0x06007527 RID: 29991 RVA: 0x0018226F File Offset: 0x0018046F
		public Unlimited<uint> ImapCutoffBalance
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029BC RID: 10684
		// (get) Token: 0x06007528 RID: 29992 RVA: 0x00182276 File Offset: 0x00180476
		public Unlimited<uint> OutlookServiceMaxConcurrency
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029BD RID: 10685
		// (get) Token: 0x06007529 RID: 29993 RVA: 0x0018227D File Offset: 0x0018047D
		public Unlimited<uint> OutlookServiceMaxBurst
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029BE RID: 10686
		// (get) Token: 0x0600752A RID: 29994 RVA: 0x00182284 File Offset: 0x00180484
		public Unlimited<uint> OutlookServiceRechargeRate
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029BF RID: 10687
		// (get) Token: 0x0600752B RID: 29995 RVA: 0x0018228B File Offset: 0x0018048B
		public Unlimited<uint> OutlookServiceCutoffBalance
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029C0 RID: 10688
		// (get) Token: 0x0600752C RID: 29996 RVA: 0x00182292 File Offset: 0x00180492
		public Unlimited<uint> OutlookServiceMaxSubscriptions
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029C1 RID: 10689
		// (get) Token: 0x0600752D RID: 29997 RVA: 0x00182299 File Offset: 0x00180499
		public Unlimited<uint> OutlookServiceMaxSocketConnectionsPerDevice
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029C2 RID: 10690
		// (get) Token: 0x0600752E RID: 29998 RVA: 0x001822A0 File Offset: 0x001804A0
		public Unlimited<uint> OutlookServiceMaxSocketConnectionsPerUser
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029C3 RID: 10691
		// (get) Token: 0x0600752F RID: 29999 RVA: 0x001822A7 File Offset: 0x001804A7
		public Unlimited<uint> OwaMaxConcurrency
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029C4 RID: 10692
		// (get) Token: 0x06007530 RID: 30000 RVA: 0x001822AE File Offset: 0x001804AE
		public Unlimited<uint> OwaMaxBurst
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029C5 RID: 10693
		// (get) Token: 0x06007531 RID: 30001 RVA: 0x001822B5 File Offset: 0x001804B5
		public Unlimited<uint> OwaRechargeRate
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029C6 RID: 10694
		// (get) Token: 0x06007532 RID: 30002 RVA: 0x001822BC File Offset: 0x001804BC
		public Unlimited<uint> OwaCutoffBalance
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029C7 RID: 10695
		// (get) Token: 0x06007533 RID: 30003 RVA: 0x001822C3 File Offset: 0x001804C3
		public Unlimited<uint> OwaVoiceMaxConcurrency
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029C8 RID: 10696
		// (get) Token: 0x06007534 RID: 30004 RVA: 0x001822CA File Offset: 0x001804CA
		public Unlimited<uint> OwaVoiceMaxBurst
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029C9 RID: 10697
		// (get) Token: 0x06007535 RID: 30005 RVA: 0x001822D1 File Offset: 0x001804D1
		public Unlimited<uint> OwaVoiceRechargeRate
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029CA RID: 10698
		// (get) Token: 0x06007536 RID: 30006 RVA: 0x001822D8 File Offset: 0x001804D8
		public Unlimited<uint> OwaVoiceCutoffBalance
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029CB RID: 10699
		// (get) Token: 0x06007537 RID: 30007 RVA: 0x001822DF File Offset: 0x001804DF
		public Unlimited<uint> PopMaxConcurrency
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029CC RID: 10700
		// (get) Token: 0x06007538 RID: 30008 RVA: 0x001822E6 File Offset: 0x001804E6
		public Unlimited<uint> PopMaxBurst
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029CD RID: 10701
		// (get) Token: 0x06007539 RID: 30009 RVA: 0x001822ED File Offset: 0x001804ED
		public Unlimited<uint> PopRechargeRate
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029CE RID: 10702
		// (get) Token: 0x0600753A RID: 30010 RVA: 0x001822F4 File Offset: 0x001804F4
		public Unlimited<uint> PopCutoffBalance
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029CF RID: 10703
		// (get) Token: 0x0600753B RID: 30011 RVA: 0x001822FB File Offset: 0x001804FB
		public Unlimited<uint> PowerShellMaxConcurrency
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029D0 RID: 10704
		// (get) Token: 0x0600753C RID: 30012 RVA: 0x00182302 File Offset: 0x00180502
		public Unlimited<uint> PowerShellMaxBurst
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029D1 RID: 10705
		// (get) Token: 0x0600753D RID: 30013 RVA: 0x00182309 File Offset: 0x00180509
		public Unlimited<uint> PowerShellRechargeRate
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029D2 RID: 10706
		// (get) Token: 0x0600753E RID: 30014 RVA: 0x00182310 File Offset: 0x00180510
		public Unlimited<uint> PowerShellCutoffBalance
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029D3 RID: 10707
		// (get) Token: 0x0600753F RID: 30015 RVA: 0x00182317 File Offset: 0x00180517
		public Unlimited<uint> PowerShellMaxTenantConcurrency
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029D4 RID: 10708
		// (get) Token: 0x06007540 RID: 30016 RVA: 0x0018231E File Offset: 0x0018051E
		public Unlimited<uint> PowerShellMaxOperations
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029D5 RID: 10709
		// (get) Token: 0x06007541 RID: 30017 RVA: 0x00182325 File Offset: 0x00180525
		public Unlimited<uint> PowerShellMaxCmdletsTimePeriod
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029D6 RID: 10710
		// (get) Token: 0x06007542 RID: 30018 RVA: 0x0018232C File Offset: 0x0018052C
		public Unlimited<uint> ExchangeMaxCmdlets
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029D7 RID: 10711
		// (get) Token: 0x06007543 RID: 30019 RVA: 0x00182333 File Offset: 0x00180533
		public Unlimited<uint> PowerShellMaxCmdletQueueDepth
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029D8 RID: 10712
		// (get) Token: 0x06007544 RID: 30020 RVA: 0x0018233A File Offset: 0x0018053A
		public Unlimited<uint> PowerShellMaxDestructiveCmdlets
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029D9 RID: 10713
		// (get) Token: 0x06007545 RID: 30021 RVA: 0x00182341 File Offset: 0x00180541
		public Unlimited<uint> PowerShellMaxDestructiveCmdletsTimePeriod
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029DA RID: 10714
		// (get) Token: 0x06007546 RID: 30022 RVA: 0x00182348 File Offset: 0x00180548
		public Unlimited<uint> PowerShellMaxCmdlets
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029DB RID: 10715
		// (get) Token: 0x06007547 RID: 30023 RVA: 0x0018234F File Offset: 0x0018054F
		public Unlimited<uint> PowerShellMaxRunspaces
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029DC RID: 10716
		// (get) Token: 0x06007548 RID: 30024 RVA: 0x00182356 File Offset: 0x00180556
		public Unlimited<uint> PowerShellMaxTenantRunspaces
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029DD RID: 10717
		// (get) Token: 0x06007549 RID: 30025 RVA: 0x0018235D File Offset: 0x0018055D
		public Unlimited<uint> PowerShellMaxRunspacesTimePeriod
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029DE RID: 10718
		// (get) Token: 0x0600754A RID: 30026 RVA: 0x00182364 File Offset: 0x00180564
		public Unlimited<uint> PswsMaxConcurrency
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029DF RID: 10719
		// (get) Token: 0x0600754B RID: 30027 RVA: 0x0018236B File Offset: 0x0018056B
		public Unlimited<uint> PswsMaxRequest
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029E0 RID: 10720
		// (get) Token: 0x0600754C RID: 30028 RVA: 0x00182372 File Offset: 0x00180572
		public Unlimited<uint> PswsMaxRequestTimePeriod
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029E1 RID: 10721
		// (get) Token: 0x0600754D RID: 30029 RVA: 0x00182379 File Offset: 0x00180579
		public Unlimited<uint> RcaMaxConcurrency
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029E2 RID: 10722
		// (get) Token: 0x0600754E RID: 30030 RVA: 0x00182380 File Offset: 0x00180580
		public Unlimited<uint> RcaMaxBurst
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029E3 RID: 10723
		// (get) Token: 0x0600754F RID: 30031 RVA: 0x00182387 File Offset: 0x00180587
		public Unlimited<uint> RcaRechargeRate
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029E4 RID: 10724
		// (get) Token: 0x06007550 RID: 30032 RVA: 0x0018238E File Offset: 0x0018058E
		public Unlimited<uint> RcaCutoffBalance
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029E5 RID: 10725
		// (get) Token: 0x06007551 RID: 30033 RVA: 0x00182395 File Offset: 0x00180595
		public Unlimited<uint> CpaMaxConcurrency
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029E6 RID: 10726
		// (get) Token: 0x06007552 RID: 30034 RVA: 0x0018239C File Offset: 0x0018059C
		public Unlimited<uint> CpaMaxBurst
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029E7 RID: 10727
		// (get) Token: 0x06007553 RID: 30035 RVA: 0x001823A3 File Offset: 0x001805A3
		public Unlimited<uint> CpaRechargeRate
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029E8 RID: 10728
		// (get) Token: 0x06007554 RID: 30036 RVA: 0x001823AA File Offset: 0x001805AA
		public Unlimited<uint> CpaCutoffBalance
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029E9 RID: 10729
		// (get) Token: 0x06007555 RID: 30037 RVA: 0x001823B1 File Offset: 0x001805B1
		public Unlimited<uint> DiscoveryMaxConcurrency
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029EA RID: 10730
		// (get) Token: 0x06007556 RID: 30038 RVA: 0x001823B8 File Offset: 0x001805B8
		public Unlimited<uint> DiscoveryMaxMailboxes
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029EB RID: 10731
		// (get) Token: 0x06007557 RID: 30039 RVA: 0x001823BF File Offset: 0x001805BF
		public Unlimited<uint> DiscoveryMaxKeywords
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029EC RID: 10732
		// (get) Token: 0x06007558 RID: 30040 RVA: 0x001823C6 File Offset: 0x001805C6
		public Unlimited<uint> DiscoveryMaxPreviewSearchMailboxes
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029ED RID: 10733
		// (get) Token: 0x06007559 RID: 30041 RVA: 0x001823CD File Offset: 0x001805CD
		public Unlimited<uint> DiscoveryMaxStatsSearchMailboxes
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029EE RID: 10734
		// (get) Token: 0x0600755A RID: 30042 RVA: 0x001823D4 File Offset: 0x001805D4
		public Unlimited<uint> DiscoveryPreviewSearchResultsPageSize
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029EF RID: 10735
		// (get) Token: 0x0600755B RID: 30043 RVA: 0x001823DB File Offset: 0x001805DB
		public Unlimited<uint> DiscoveryMaxKeywordsPerPage
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029F0 RID: 10736
		// (get) Token: 0x0600755C RID: 30044 RVA: 0x001823E2 File Offset: 0x001805E2
		public Unlimited<uint> DiscoveryMaxRefinerResults
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029F1 RID: 10737
		// (get) Token: 0x0600755D RID: 30045 RVA: 0x001823E9 File Offset: 0x001805E9
		public Unlimited<uint> DiscoveryMaxSearchQueueDepth
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029F2 RID: 10738
		// (get) Token: 0x0600755E RID: 30046 RVA: 0x001823F0 File Offset: 0x001805F0
		public Unlimited<uint> DiscoverySearchTimeoutPeriod
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029F3 RID: 10739
		// (get) Token: 0x0600755F RID: 30047 RVA: 0x001823F7 File Offset: 0x001805F7
		public Unlimited<uint> MessageRateLimit
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029F4 RID: 10740
		// (get) Token: 0x06007560 RID: 30048 RVA: 0x001823FE File Offset: 0x001805FE
		public Unlimited<uint> RecipientRateLimit
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029F5 RID: 10741
		// (get) Token: 0x06007561 RID: 30049 RVA: 0x00182405 File Offset: 0x00180605
		public Unlimited<uint> ForwardeeLimit
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029F6 RID: 10742
		// (get) Token: 0x06007562 RID: 30050 RVA: 0x0018240C File Offset: 0x0018060C
		public Unlimited<uint> ComplianceMaxExpansionDGRecipients
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029F7 RID: 10743
		// (get) Token: 0x06007563 RID: 30051 RVA: 0x00182413 File Offset: 0x00180613
		public Unlimited<uint> ComplianceMaxExpansionNestedDGs
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x06007564 RID: 30052 RVA: 0x0018241A File Offset: 0x0018061A
		public string GetIdentityString()
		{
			return "[Unthrottled]";
		}

		// Token: 0x06007565 RID: 30053 RVA: 0x00182421 File Offset: 0x00180621
		public string GetShortIdentityString()
		{
			return "[Unthrottled]";
		}

		// Token: 0x170029F8 RID: 10744
		// (get) Token: 0x06007566 RID: 30054 RVA: 0x00182428 File Offset: 0x00180628
		public Unlimited<uint> AnonymousMaxConcurrency
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029F9 RID: 10745
		// (get) Token: 0x06007567 RID: 30055 RVA: 0x0018242F File Offset: 0x0018062F
		public Unlimited<uint> AnonymousMaxBurst
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029FA RID: 10746
		// (get) Token: 0x06007568 RID: 30056 RVA: 0x00182436 File Offset: 0x00180636
		public Unlimited<uint> AnonymousRechargeRate
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029FB RID: 10747
		// (get) Token: 0x06007569 RID: 30057 RVA: 0x0018243D File Offset: 0x0018063D
		public Unlimited<uint> AnonymousCutoffBalance
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029FC RID: 10748
		// (get) Token: 0x0600756A RID: 30058 RVA: 0x00182444 File Offset: 0x00180644
		public Unlimited<uint> PushNotificationMaxConcurrency
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029FD RID: 10749
		// (get) Token: 0x0600756B RID: 30059 RVA: 0x0018244B File Offset: 0x0018064B
		public Unlimited<uint> PushNotificationMaxBurst
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029FE RID: 10750
		// (get) Token: 0x0600756C RID: 30060 RVA: 0x00182452 File Offset: 0x00180652
		public Unlimited<uint> PushNotificationRechargeRate
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x170029FF RID: 10751
		// (get) Token: 0x0600756D RID: 30061 RVA: 0x00182459 File Offset: 0x00180659
		public Unlimited<uint> PushNotificationCutoffBalance
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x17002A00 RID: 10752
		// (get) Token: 0x0600756E RID: 30062 RVA: 0x00182460 File Offset: 0x00180660
		public Unlimited<uint> PushNotificationMaxBurstPerDevice
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x17002A01 RID: 10753
		// (get) Token: 0x0600756F RID: 30063 RVA: 0x00182467 File Offset: 0x00180667
		public Unlimited<uint> PushNotificationRechargeRatePerDevice
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x17002A02 RID: 10754
		// (get) Token: 0x06007570 RID: 30064 RVA: 0x0018246E File Offset: 0x0018066E
		public Unlimited<uint> PushNotificationSamplingPeriodPerDevice
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x17002A03 RID: 10755
		// (get) Token: 0x06007571 RID: 30065 RVA: 0x00182475 File Offset: 0x00180675
		public Unlimited<uint> EncryptionSenderMaxConcurrency
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x17002A04 RID: 10756
		// (get) Token: 0x06007572 RID: 30066 RVA: 0x0018247C File Offset: 0x0018067C
		public Unlimited<uint> EncryptionSenderMaxBurst
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x17002A05 RID: 10757
		// (get) Token: 0x06007573 RID: 30067 RVA: 0x00182483 File Offset: 0x00180683
		public Unlimited<uint> EncryptionSenderRechargeRate
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x17002A06 RID: 10758
		// (get) Token: 0x06007574 RID: 30068 RVA: 0x0018248A File Offset: 0x0018068A
		public Unlimited<uint> EncryptionSenderCutoffBalance
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x17002A07 RID: 10759
		// (get) Token: 0x06007575 RID: 30069 RVA: 0x00182491 File Offset: 0x00180691
		public Unlimited<uint> EncryptionRecipientMaxConcurrency
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x17002A08 RID: 10760
		// (get) Token: 0x06007576 RID: 30070 RVA: 0x00182498 File Offset: 0x00180698
		public Unlimited<uint> EncryptionRecipientMaxBurst
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x17002A09 RID: 10761
		// (get) Token: 0x06007577 RID: 30071 RVA: 0x0018249F File Offset: 0x0018069F
		public Unlimited<uint> EncryptionRecipientRechargeRate
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x17002A0A RID: 10762
		// (get) Token: 0x06007578 RID: 30072 RVA: 0x001824A6 File Offset: 0x001806A6
		public Unlimited<uint> EncryptionRecipientCutoffBalance
		{
			get
			{
				return Unlimited<uint>.UnlimitedValue;
			}
		}

		// Token: 0x04004B62 RID: 19298
		private const string UnthrottledIdentity = "[Unthrottled]";

		// Token: 0x04004B63 RID: 19299
		private static UnthrottledThrottlingPolicy singleton = new UnthrottledThrottlingPolicy();
	}
}
