using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009BA RID: 2490
	internal class FallbackThrottlingPolicy : IThrottlingPolicy
	{
		// Token: 0x0600732D RID: 29485 RVA: 0x0017D27C File Offset: 0x0017B47C
		private FallbackThrottlingPolicy()
		{
		}

		// Token: 0x0600732E RID: 29486 RVA: 0x0017D284 File Offset: 0x0017B484
		public static FallbackThrottlingPolicy GetSingleton()
		{
			return FallbackThrottlingPolicy.singleton;
		}

		// Token: 0x170028D4 RID: 10452
		// (get) Token: 0x0600732F RID: 29487 RVA: 0x0017D28B File Offset: 0x0017B48B
		public bool IsFallback
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170028D5 RID: 10453
		// (get) Token: 0x06007330 RID: 29488 RVA: 0x0017D28E File Offset: 0x0017B48E
		public ThrottlingPolicyScopeType ThrottlingPolicyScope
		{
			get
			{
				return ThrottlingPolicyScopeType.Regular;
			}
		}

		// Token: 0x170028D6 RID: 10454
		// (get) Token: 0x06007331 RID: 29489 RVA: 0x0017D291 File Offset: 0x0017B491
		public bool IsServiceAccount
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170028D7 RID: 10455
		// (get) Token: 0x06007332 RID: 29490 RVA: 0x0017D294 File Offset: 0x0017B494
		public bool IsUnthrottled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170028D8 RID: 10456
		// (get) Token: 0x06007333 RID: 29491 RVA: 0x0017D297 File Offset: 0x0017B497
		public Unlimited<uint> AnonymousMaxConcurrency
		{
			get
			{
				return ThrottlingPolicyDefaults.AnonymousMaxConcurrency;
			}
		}

		// Token: 0x170028D9 RID: 10457
		// (get) Token: 0x06007334 RID: 29492 RVA: 0x0017D29E File Offset: 0x0017B49E
		public Unlimited<uint> AnonymousMaxBurst
		{
			get
			{
				return ThrottlingPolicyDefaults.AnonymousMaxBurst;
			}
		}

		// Token: 0x170028DA RID: 10458
		// (get) Token: 0x06007335 RID: 29493 RVA: 0x0017D2A5 File Offset: 0x0017B4A5
		public Unlimited<uint> AnonymousRechargeRate
		{
			get
			{
				return ThrottlingPolicyDefaults.AnonymousRechargeRate;
			}
		}

		// Token: 0x170028DB RID: 10459
		// (get) Token: 0x06007336 RID: 29494 RVA: 0x0017D2AC File Offset: 0x0017B4AC
		public Unlimited<uint> AnonymousCutoffBalance
		{
			get
			{
				return ThrottlingPolicyDefaults.AnonymousCutoffBalance;
			}
		}

		// Token: 0x170028DC RID: 10460
		// (get) Token: 0x06007337 RID: 29495 RVA: 0x0017D2B3 File Offset: 0x0017B4B3
		public Unlimited<uint> EasMaxConcurrency
		{
			get
			{
				return ThrottlingPolicyDefaults.EasMaxConcurrency;
			}
		}

		// Token: 0x170028DD RID: 10461
		// (get) Token: 0x06007338 RID: 29496 RVA: 0x0017D2BA File Offset: 0x0017B4BA
		public Unlimited<uint> EasMaxBurst
		{
			get
			{
				return ThrottlingPolicyDefaults.EasMaxBurst;
			}
		}

		// Token: 0x170028DE RID: 10462
		// (get) Token: 0x06007339 RID: 29497 RVA: 0x0017D2C1 File Offset: 0x0017B4C1
		public Unlimited<uint> EasRechargeRate
		{
			get
			{
				return ThrottlingPolicyDefaults.EasRechargeRate;
			}
		}

		// Token: 0x170028DF RID: 10463
		// (get) Token: 0x0600733A RID: 29498 RVA: 0x0017D2C8 File Offset: 0x0017B4C8
		public Unlimited<uint> EasCutoffBalance
		{
			get
			{
				return ThrottlingPolicyDefaults.EasCutoffBalance;
			}
		}

		// Token: 0x170028E0 RID: 10464
		// (get) Token: 0x0600733B RID: 29499 RVA: 0x0017D2CF File Offset: 0x0017B4CF
		public Unlimited<uint> EasMaxDevices
		{
			get
			{
				return ThrottlingPolicyDefaults.EasMaxDevices;
			}
		}

		// Token: 0x170028E1 RID: 10465
		// (get) Token: 0x0600733C RID: 29500 RVA: 0x0017D2D6 File Offset: 0x0017B4D6
		public Unlimited<uint> EasMaxDeviceDeletesPerMonth
		{
			get
			{
				return ThrottlingPolicyDefaults.EasMaxDeviceDeletesPerMonth;
			}
		}

		// Token: 0x170028E2 RID: 10466
		// (get) Token: 0x0600733D RID: 29501 RVA: 0x0017D2DD File Offset: 0x0017B4DD
		public Unlimited<uint> EasMaxInactivityForDeviceCleanup
		{
			get
			{
				return ThrottlingPolicyDefaults.EasMaxInactivityForDeviceCleanup;
			}
		}

		// Token: 0x170028E3 RID: 10467
		// (get) Token: 0x0600733E RID: 29502 RVA: 0x0017D2E4 File Offset: 0x0017B4E4
		public Unlimited<uint> EwsMaxConcurrency
		{
			get
			{
				return ThrottlingPolicyDefaults.EwsMaxConcurrency;
			}
		}

		// Token: 0x170028E4 RID: 10468
		// (get) Token: 0x0600733F RID: 29503 RVA: 0x0017D2EB File Offset: 0x0017B4EB
		public Unlimited<uint> EwsMaxBurst
		{
			get
			{
				return ThrottlingPolicyDefaults.EwsMaxBurst;
			}
		}

		// Token: 0x170028E5 RID: 10469
		// (get) Token: 0x06007340 RID: 29504 RVA: 0x0017D2F2 File Offset: 0x0017B4F2
		public Unlimited<uint> EwsRechargeRate
		{
			get
			{
				return ThrottlingPolicyDefaults.EwsRechargeRate;
			}
		}

		// Token: 0x170028E6 RID: 10470
		// (get) Token: 0x06007341 RID: 29505 RVA: 0x0017D2F9 File Offset: 0x0017B4F9
		public Unlimited<uint> EwsCutoffBalance
		{
			get
			{
				return ThrottlingPolicyDefaults.EwsCutoffBalance;
			}
		}

		// Token: 0x170028E7 RID: 10471
		// (get) Token: 0x06007342 RID: 29506 RVA: 0x0017D300 File Offset: 0x0017B500
		public Unlimited<uint> EwsMaxSubscriptions
		{
			get
			{
				return ThrottlingPolicyDefaults.EwsMaxSubscriptions;
			}
		}

		// Token: 0x170028E8 RID: 10472
		// (get) Token: 0x06007343 RID: 29507 RVA: 0x0017D307 File Offset: 0x0017B507
		public Unlimited<uint> ImapMaxConcurrency
		{
			get
			{
				return ThrottlingPolicyDefaults.ImapMaxConcurrency;
			}
		}

		// Token: 0x170028E9 RID: 10473
		// (get) Token: 0x06007344 RID: 29508 RVA: 0x0017D30E File Offset: 0x0017B50E
		public Unlimited<uint> ImapMaxBurst
		{
			get
			{
				return ThrottlingPolicyDefaults.ImapMaxBurst;
			}
		}

		// Token: 0x170028EA RID: 10474
		// (get) Token: 0x06007345 RID: 29509 RVA: 0x0017D315 File Offset: 0x0017B515
		public Unlimited<uint> ImapRechargeRate
		{
			get
			{
				return ThrottlingPolicyDefaults.ImapRechargeRate;
			}
		}

		// Token: 0x170028EB RID: 10475
		// (get) Token: 0x06007346 RID: 29510 RVA: 0x0017D31C File Offset: 0x0017B51C
		public Unlimited<uint> ImapCutoffBalance
		{
			get
			{
				return ThrottlingPolicyDefaults.ImapCutoffBalance;
			}
		}

		// Token: 0x170028EC RID: 10476
		// (get) Token: 0x06007347 RID: 29511 RVA: 0x0017D323 File Offset: 0x0017B523
		public Unlimited<uint> OutlookServiceMaxConcurrency
		{
			get
			{
				return ThrottlingPolicyDefaults.OutlookServiceMaxConcurrency;
			}
		}

		// Token: 0x170028ED RID: 10477
		// (get) Token: 0x06007348 RID: 29512 RVA: 0x0017D32A File Offset: 0x0017B52A
		public Unlimited<uint> OutlookServiceMaxBurst
		{
			get
			{
				return ThrottlingPolicyDefaults.OutlookServiceMaxBurst;
			}
		}

		// Token: 0x170028EE RID: 10478
		// (get) Token: 0x06007349 RID: 29513 RVA: 0x0017D331 File Offset: 0x0017B531
		public Unlimited<uint> OutlookServiceRechargeRate
		{
			get
			{
				return ThrottlingPolicyDefaults.OutlookServiceRechargeRate;
			}
		}

		// Token: 0x170028EF RID: 10479
		// (get) Token: 0x0600734A RID: 29514 RVA: 0x0017D338 File Offset: 0x0017B538
		public Unlimited<uint> OutlookServiceCutoffBalance
		{
			get
			{
				return ThrottlingPolicyDefaults.OutlookServiceCutoffBalance;
			}
		}

		// Token: 0x170028F0 RID: 10480
		// (get) Token: 0x0600734B RID: 29515 RVA: 0x0017D33F File Offset: 0x0017B53F
		public Unlimited<uint> OutlookServiceMaxSubscriptions
		{
			get
			{
				return ThrottlingPolicyDefaults.OutlookServiceMaxSubscriptions;
			}
		}

		// Token: 0x170028F1 RID: 10481
		// (get) Token: 0x0600734C RID: 29516 RVA: 0x0017D346 File Offset: 0x0017B546
		public Unlimited<uint> OutlookServiceMaxSocketConnectionsPerDevice
		{
			get
			{
				return ThrottlingPolicyDefaults.OutlookServiceMaxSocketConnectionsPerDevice;
			}
		}

		// Token: 0x170028F2 RID: 10482
		// (get) Token: 0x0600734D RID: 29517 RVA: 0x0017D34D File Offset: 0x0017B54D
		public Unlimited<uint> OutlookServiceMaxSocketConnectionsPerUser
		{
			get
			{
				return ThrottlingPolicyDefaults.OutlookServiceMaxSocketConnectionsPerUser;
			}
		}

		// Token: 0x170028F3 RID: 10483
		// (get) Token: 0x0600734E RID: 29518 RVA: 0x0017D354 File Offset: 0x0017B554
		public Unlimited<uint> OwaMaxConcurrency
		{
			get
			{
				return ThrottlingPolicyDefaults.OwaMaxConcurrency;
			}
		}

		// Token: 0x170028F4 RID: 10484
		// (get) Token: 0x0600734F RID: 29519 RVA: 0x0017D35B File Offset: 0x0017B55B
		public Unlimited<uint> OwaMaxBurst
		{
			get
			{
				return ThrottlingPolicyDefaults.OwaMaxBurst;
			}
		}

		// Token: 0x170028F5 RID: 10485
		// (get) Token: 0x06007350 RID: 29520 RVA: 0x0017D362 File Offset: 0x0017B562
		public Unlimited<uint> OwaRechargeRate
		{
			get
			{
				return ThrottlingPolicyDefaults.OwaRechargeRate;
			}
		}

		// Token: 0x170028F6 RID: 10486
		// (get) Token: 0x06007351 RID: 29521 RVA: 0x0017D369 File Offset: 0x0017B569
		public Unlimited<uint> OwaCutoffBalance
		{
			get
			{
				return ThrottlingPolicyDefaults.OwaCutoffBalance;
			}
		}

		// Token: 0x170028F7 RID: 10487
		// (get) Token: 0x06007352 RID: 29522 RVA: 0x0017D370 File Offset: 0x0017B570
		public Unlimited<uint> OwaVoiceMaxConcurrency
		{
			get
			{
				return ThrottlingPolicyDefaults.OwaVoiceMaxConcurrency;
			}
		}

		// Token: 0x170028F8 RID: 10488
		// (get) Token: 0x06007353 RID: 29523 RVA: 0x0017D377 File Offset: 0x0017B577
		public Unlimited<uint> OwaVoiceMaxBurst
		{
			get
			{
				return ThrottlingPolicyDefaults.OwaVoiceMaxBurst;
			}
		}

		// Token: 0x170028F9 RID: 10489
		// (get) Token: 0x06007354 RID: 29524 RVA: 0x0017D37E File Offset: 0x0017B57E
		public Unlimited<uint> OwaVoiceRechargeRate
		{
			get
			{
				return ThrottlingPolicyDefaults.OwaVoiceRechargeRate;
			}
		}

		// Token: 0x170028FA RID: 10490
		// (get) Token: 0x06007355 RID: 29525 RVA: 0x0017D385 File Offset: 0x0017B585
		public Unlimited<uint> OwaVoiceCutoffBalance
		{
			get
			{
				return ThrottlingPolicyDefaults.OwaVoiceCutoffBalance;
			}
		}

		// Token: 0x170028FB RID: 10491
		// (get) Token: 0x06007356 RID: 29526 RVA: 0x0017D38C File Offset: 0x0017B58C
		public Unlimited<uint> PopMaxConcurrency
		{
			get
			{
				return ThrottlingPolicyDefaults.PopMaxConcurrency;
			}
		}

		// Token: 0x170028FC RID: 10492
		// (get) Token: 0x06007357 RID: 29527 RVA: 0x0017D393 File Offset: 0x0017B593
		public Unlimited<uint> PopMaxBurst
		{
			get
			{
				return ThrottlingPolicyDefaults.PopMaxBurst;
			}
		}

		// Token: 0x170028FD RID: 10493
		// (get) Token: 0x06007358 RID: 29528 RVA: 0x0017D39A File Offset: 0x0017B59A
		public Unlimited<uint> PopRechargeRate
		{
			get
			{
				return ThrottlingPolicyDefaults.PopRechargeRate;
			}
		}

		// Token: 0x170028FE RID: 10494
		// (get) Token: 0x06007359 RID: 29529 RVA: 0x0017D3A1 File Offset: 0x0017B5A1
		public Unlimited<uint> PopCutoffBalance
		{
			get
			{
				return ThrottlingPolicyDefaults.PopCutoffBalance;
			}
		}

		// Token: 0x170028FF RID: 10495
		// (get) Token: 0x0600735A RID: 29530 RVA: 0x0017D3A8 File Offset: 0x0017B5A8
		public Unlimited<uint> PowerShellMaxConcurrency
		{
			get
			{
				return ThrottlingPolicyDefaults.PowerShellMaxConcurrency;
			}
		}

		// Token: 0x17002900 RID: 10496
		// (get) Token: 0x0600735B RID: 29531 RVA: 0x0017D3AF File Offset: 0x0017B5AF
		public Unlimited<uint> PowerShellMaxBurst
		{
			get
			{
				return ThrottlingPolicyDefaults.PowerShellMaxBurst;
			}
		}

		// Token: 0x17002901 RID: 10497
		// (get) Token: 0x0600735C RID: 29532 RVA: 0x0017D3B6 File Offset: 0x0017B5B6
		public Unlimited<uint> PowerShellRechargeRate
		{
			get
			{
				return ThrottlingPolicyDefaults.PowerShellRechargeRate;
			}
		}

		// Token: 0x17002902 RID: 10498
		// (get) Token: 0x0600735D RID: 29533 RVA: 0x0017D3BD File Offset: 0x0017B5BD
		public Unlimited<uint> PowerShellCutoffBalance
		{
			get
			{
				return ThrottlingPolicyDefaults.PowerShellCutoffBalance;
			}
		}

		// Token: 0x17002903 RID: 10499
		// (get) Token: 0x0600735E RID: 29534 RVA: 0x0017D3C4 File Offset: 0x0017B5C4
		public Unlimited<uint> PowerShellMaxTenantConcurrency
		{
			get
			{
				return ThrottlingPolicyDefaults.PowerShellMaxTenantConcurrency;
			}
		}

		// Token: 0x17002904 RID: 10500
		// (get) Token: 0x0600735F RID: 29535 RVA: 0x0017D3CB File Offset: 0x0017B5CB
		public Unlimited<uint> PowerShellMaxOperations
		{
			get
			{
				return ThrottlingPolicyDefaults.PowerShellMaxOperations;
			}
		}

		// Token: 0x17002905 RID: 10501
		// (get) Token: 0x06007360 RID: 29536 RVA: 0x0017D3D2 File Offset: 0x0017B5D2
		public Unlimited<uint> PowerShellMaxCmdletsTimePeriod
		{
			get
			{
				return ThrottlingPolicyDefaults.PowerShellMaxCmdletsTimePeriod;
			}
		}

		// Token: 0x17002906 RID: 10502
		// (get) Token: 0x06007361 RID: 29537 RVA: 0x0017D3D9 File Offset: 0x0017B5D9
		public Unlimited<uint> ExchangeMaxCmdlets
		{
			get
			{
				return ThrottlingPolicyDefaults.ExchangeMaxCmdlets;
			}
		}

		// Token: 0x17002907 RID: 10503
		// (get) Token: 0x06007362 RID: 29538 RVA: 0x0017D3E0 File Offset: 0x0017B5E0
		public Unlimited<uint> PowerShellMaxCmdletQueueDepth
		{
			get
			{
				return ThrottlingPolicyDefaults.PowerShellMaxCmdletQueueDepth;
			}
		}

		// Token: 0x17002908 RID: 10504
		// (get) Token: 0x06007363 RID: 29539 RVA: 0x0017D3E7 File Offset: 0x0017B5E7
		public Unlimited<uint> PowerShellMaxDestructiveCmdlets
		{
			get
			{
				return ThrottlingPolicyDefaults.PowerShellMaxDestructiveCmdlets;
			}
		}

		// Token: 0x17002909 RID: 10505
		// (get) Token: 0x06007364 RID: 29540 RVA: 0x0017D3EE File Offset: 0x0017B5EE
		public Unlimited<uint> PowerShellMaxDestructiveCmdletsTimePeriod
		{
			get
			{
				return ThrottlingPolicyDefaults.PowerShellMaxDestructiveCmdletsTimePeriod;
			}
		}

		// Token: 0x1700290A RID: 10506
		// (get) Token: 0x06007365 RID: 29541 RVA: 0x0017D3F5 File Offset: 0x0017B5F5
		public Unlimited<uint> PowerShellMaxCmdlets
		{
			get
			{
				return ThrottlingPolicyDefaults.PowerShellMaxCmdlets;
			}
		}

		// Token: 0x1700290B RID: 10507
		// (get) Token: 0x06007366 RID: 29542 RVA: 0x0017D3FC File Offset: 0x0017B5FC
		public Unlimited<uint> PowerShellMaxRunspaces
		{
			get
			{
				return ThrottlingPolicyDefaults.PowerShellMaxRunspaces;
			}
		}

		// Token: 0x1700290C RID: 10508
		// (get) Token: 0x06007367 RID: 29543 RVA: 0x0017D403 File Offset: 0x0017B603
		public Unlimited<uint> PowerShellMaxTenantRunspaces
		{
			get
			{
				return ThrottlingPolicyDefaults.PowerShellMaxTenantRunspaces;
			}
		}

		// Token: 0x1700290D RID: 10509
		// (get) Token: 0x06007368 RID: 29544 RVA: 0x0017D40A File Offset: 0x0017B60A
		public Unlimited<uint> PowerShellMaxRunspacesTimePeriod
		{
			get
			{
				return ThrottlingPolicyDefaults.PowerShellMaxRunspacesTimePeriod;
			}
		}

		// Token: 0x1700290E RID: 10510
		// (get) Token: 0x06007369 RID: 29545 RVA: 0x0017D411 File Offset: 0x0017B611
		public Unlimited<uint> PswsMaxConcurrency
		{
			get
			{
				return ThrottlingPolicyDefaults.PswsMaxConcurrency;
			}
		}

		// Token: 0x1700290F RID: 10511
		// (get) Token: 0x0600736A RID: 29546 RVA: 0x0017D418 File Offset: 0x0017B618
		public Unlimited<uint> PswsMaxRequest
		{
			get
			{
				return ThrottlingPolicyDefaults.PswsMaxRequest;
			}
		}

		// Token: 0x17002910 RID: 10512
		// (get) Token: 0x0600736B RID: 29547 RVA: 0x0017D41F File Offset: 0x0017B61F
		public Unlimited<uint> PswsMaxRequestTimePeriod
		{
			get
			{
				return ThrottlingPolicyDefaults.PswsMaxRequestTimePeriod;
			}
		}

		// Token: 0x17002911 RID: 10513
		// (get) Token: 0x0600736C RID: 29548 RVA: 0x0017D426 File Offset: 0x0017B626
		public Unlimited<uint> RcaMaxConcurrency
		{
			get
			{
				return ThrottlingPolicyDefaults.RcaMaxConcurrency;
			}
		}

		// Token: 0x17002912 RID: 10514
		// (get) Token: 0x0600736D RID: 29549 RVA: 0x0017D42D File Offset: 0x0017B62D
		public Unlimited<uint> RcaMaxBurst
		{
			get
			{
				return ThrottlingPolicyDefaults.RcaMaxBurst;
			}
		}

		// Token: 0x17002913 RID: 10515
		// (get) Token: 0x0600736E RID: 29550 RVA: 0x0017D434 File Offset: 0x0017B634
		public Unlimited<uint> RcaRechargeRate
		{
			get
			{
				return ThrottlingPolicyDefaults.RcaRechargeRate;
			}
		}

		// Token: 0x17002914 RID: 10516
		// (get) Token: 0x0600736F RID: 29551 RVA: 0x0017D43B File Offset: 0x0017B63B
		public Unlimited<uint> RcaCutoffBalance
		{
			get
			{
				return ThrottlingPolicyDefaults.RcaCutoffBalance;
			}
		}

		// Token: 0x17002915 RID: 10517
		// (get) Token: 0x06007370 RID: 29552 RVA: 0x0017D442 File Offset: 0x0017B642
		public Unlimited<uint> CpaMaxConcurrency
		{
			get
			{
				return ThrottlingPolicyDefaults.CpaMaxConcurrency;
			}
		}

		// Token: 0x17002916 RID: 10518
		// (get) Token: 0x06007371 RID: 29553 RVA: 0x0017D449 File Offset: 0x0017B649
		public Unlimited<uint> CpaMaxBurst
		{
			get
			{
				return ThrottlingPolicyDefaults.CpaMaxBurst;
			}
		}

		// Token: 0x17002917 RID: 10519
		// (get) Token: 0x06007372 RID: 29554 RVA: 0x0017D450 File Offset: 0x0017B650
		public Unlimited<uint> CpaRechargeRate
		{
			get
			{
				return ThrottlingPolicyDefaults.CpaRechargeRate;
			}
		}

		// Token: 0x17002918 RID: 10520
		// (get) Token: 0x06007373 RID: 29555 RVA: 0x0017D457 File Offset: 0x0017B657
		public Unlimited<uint> CpaCutoffBalance
		{
			get
			{
				return ThrottlingPolicyDefaults.CpaCutoffBalance;
			}
		}

		// Token: 0x17002919 RID: 10521
		// (get) Token: 0x06007374 RID: 29556 RVA: 0x0017D45E File Offset: 0x0017B65E
		public Unlimited<uint> MessageRateLimit
		{
			get
			{
				return ThrottlingPolicyDefaults.MessageRateLimit;
			}
		}

		// Token: 0x1700291A RID: 10522
		// (get) Token: 0x06007375 RID: 29557 RVA: 0x0017D465 File Offset: 0x0017B665
		public Unlimited<uint> RecipientRateLimit
		{
			get
			{
				return ThrottlingPolicyDefaults.RecipientRateLimit;
			}
		}

		// Token: 0x1700291B RID: 10523
		// (get) Token: 0x06007376 RID: 29558 RVA: 0x0017D46C File Offset: 0x0017B66C
		public Unlimited<uint> ForwardeeLimit
		{
			get
			{
				return ThrottlingPolicyDefaults.ForwardeeLimit;
			}
		}

		// Token: 0x1700291C RID: 10524
		// (get) Token: 0x06007377 RID: 29559 RVA: 0x0017D473 File Offset: 0x0017B673
		public Unlimited<uint> DiscoveryMaxConcurrency
		{
			get
			{
				return ThrottlingPolicyDefaults.DiscoveryMaxConcurrency;
			}
		}

		// Token: 0x1700291D RID: 10525
		// (get) Token: 0x06007378 RID: 29560 RVA: 0x0017D47A File Offset: 0x0017B67A
		public Unlimited<uint> DiscoveryMaxMailboxes
		{
			get
			{
				return ThrottlingPolicyDefaults.DiscoveryMaxMailboxes;
			}
		}

		// Token: 0x1700291E RID: 10526
		// (get) Token: 0x06007379 RID: 29561 RVA: 0x0017D481 File Offset: 0x0017B681
		public Unlimited<uint> DiscoveryMaxKeywords
		{
			get
			{
				return ThrottlingPolicyDefaults.DiscoveryMaxKeywords;
			}
		}

		// Token: 0x1700291F RID: 10527
		// (get) Token: 0x0600737A RID: 29562 RVA: 0x0017D488 File Offset: 0x0017B688
		public Unlimited<uint> DiscoveryMaxStatsSearchMailboxes
		{
			get
			{
				return ThrottlingPolicyDefaults.DiscoveryMaxStatsSearchMailboxes;
			}
		}

		// Token: 0x17002920 RID: 10528
		// (get) Token: 0x0600737B RID: 29563 RVA: 0x0017D48F File Offset: 0x0017B68F
		public Unlimited<uint> DiscoveryMaxPreviewSearchMailboxes
		{
			get
			{
				return ThrottlingPolicyDefaults.DiscoveryMaxPreviewSearchMailboxes;
			}
		}

		// Token: 0x17002921 RID: 10529
		// (get) Token: 0x0600737C RID: 29564 RVA: 0x0017D496 File Offset: 0x0017B696
		public Unlimited<uint> DiscoveryPreviewSearchResultsPageSize
		{
			get
			{
				return ThrottlingPolicyDefaults.DiscoveryPreviewSearchResultsPageSize;
			}
		}

		// Token: 0x17002922 RID: 10530
		// (get) Token: 0x0600737D RID: 29565 RVA: 0x0017D49D File Offset: 0x0017B69D
		public Unlimited<uint> DiscoveryMaxKeywordsPerPage
		{
			get
			{
				return ThrottlingPolicyDefaults.DiscoveryMaxKeywordsPerPage;
			}
		}

		// Token: 0x17002923 RID: 10531
		// (get) Token: 0x0600737E RID: 29566 RVA: 0x0017D4A4 File Offset: 0x0017B6A4
		public Unlimited<uint> DiscoveryMaxRefinerResults
		{
			get
			{
				return ThrottlingPolicyDefaults.DiscoveryMaxRefinerResults;
			}
		}

		// Token: 0x17002924 RID: 10532
		// (get) Token: 0x0600737F RID: 29567 RVA: 0x0017D4AB File Offset: 0x0017B6AB
		public Unlimited<uint> DiscoveryMaxSearchQueueDepth
		{
			get
			{
				return ThrottlingPolicyDefaults.DiscoveryMaxSearchQueueDepth;
			}
		}

		// Token: 0x17002925 RID: 10533
		// (get) Token: 0x06007380 RID: 29568 RVA: 0x0017D4B2 File Offset: 0x0017B6B2
		public Unlimited<uint> DiscoverySearchTimeoutPeriod
		{
			get
			{
				return ThrottlingPolicyDefaults.DiscoverySearchTimeoutPeriod;
			}
		}

		// Token: 0x17002926 RID: 10534
		// (get) Token: 0x06007381 RID: 29569 RVA: 0x0017D4B9 File Offset: 0x0017B6B9
		public Unlimited<uint> PushNotificationMaxConcurrency
		{
			get
			{
				return ThrottlingPolicyDefaults.PushNotificationMaxConcurrency;
			}
		}

		// Token: 0x17002927 RID: 10535
		// (get) Token: 0x06007382 RID: 29570 RVA: 0x0017D4C0 File Offset: 0x0017B6C0
		public Unlimited<uint> PushNotificationMaxBurst
		{
			get
			{
				return ThrottlingPolicyDefaults.PushNotificationMaxBurst;
			}
		}

		// Token: 0x17002928 RID: 10536
		// (get) Token: 0x06007383 RID: 29571 RVA: 0x0017D4C7 File Offset: 0x0017B6C7
		public Unlimited<uint> PushNotificationRechargeRate
		{
			get
			{
				return ThrottlingPolicyDefaults.PushNotificationRechargeRate;
			}
		}

		// Token: 0x17002929 RID: 10537
		// (get) Token: 0x06007384 RID: 29572 RVA: 0x0017D4CE File Offset: 0x0017B6CE
		public Unlimited<uint> PushNotificationCutoffBalance
		{
			get
			{
				return ThrottlingPolicyDefaults.PushNotificationCutoffBalance;
			}
		}

		// Token: 0x1700292A RID: 10538
		// (get) Token: 0x06007385 RID: 29573 RVA: 0x0017D4D5 File Offset: 0x0017B6D5
		public Unlimited<uint> PushNotificationMaxBurstPerDevice
		{
			get
			{
				return ThrottlingPolicyDefaults.PushNotificationMaxBurstPerDevice;
			}
		}

		// Token: 0x1700292B RID: 10539
		// (get) Token: 0x06007386 RID: 29574 RVA: 0x0017D4DC File Offset: 0x0017B6DC
		public Unlimited<uint> PushNotificationRechargeRatePerDevice
		{
			get
			{
				return ThrottlingPolicyDefaults.PushNotificationRechargeRatePerDevice;
			}
		}

		// Token: 0x1700292C RID: 10540
		// (get) Token: 0x06007387 RID: 29575 RVA: 0x0017D4E3 File Offset: 0x0017B6E3
		public Unlimited<uint> PushNotificationSamplingPeriodPerDevice
		{
			get
			{
				return ThrottlingPolicyDefaults.PushNotificationSamplingPeriodPerDevice;
			}
		}

		// Token: 0x1700292D RID: 10541
		// (get) Token: 0x06007388 RID: 29576 RVA: 0x0017D4EA File Offset: 0x0017B6EA
		public Unlimited<uint> EncryptionSenderMaxConcurrency
		{
			get
			{
				return ThrottlingPolicyDefaults.EncryptionSenderMaxConcurrency;
			}
		}

		// Token: 0x1700292E RID: 10542
		// (get) Token: 0x06007389 RID: 29577 RVA: 0x0017D4F1 File Offset: 0x0017B6F1
		public Unlimited<uint> EncryptionSenderMaxBurst
		{
			get
			{
				return ThrottlingPolicyDefaults.EncryptionSenderMaxBurst;
			}
		}

		// Token: 0x1700292F RID: 10543
		// (get) Token: 0x0600738A RID: 29578 RVA: 0x0017D4F8 File Offset: 0x0017B6F8
		public Unlimited<uint> EncryptionSenderRechargeRate
		{
			get
			{
				return ThrottlingPolicyDefaults.EncryptionSenderRechargeRate;
			}
		}

		// Token: 0x17002930 RID: 10544
		// (get) Token: 0x0600738B RID: 29579 RVA: 0x0017D4FF File Offset: 0x0017B6FF
		public Unlimited<uint> EncryptionSenderCutoffBalance
		{
			get
			{
				return ThrottlingPolicyDefaults.EncryptionSenderCutoffBalance;
			}
		}

		// Token: 0x17002931 RID: 10545
		// (get) Token: 0x0600738C RID: 29580 RVA: 0x0017D506 File Offset: 0x0017B706
		public Unlimited<uint> EncryptionRecipientMaxConcurrency
		{
			get
			{
				return ThrottlingPolicyDefaults.EncryptionRecipientMaxConcurrency;
			}
		}

		// Token: 0x17002932 RID: 10546
		// (get) Token: 0x0600738D RID: 29581 RVA: 0x0017D50D File Offset: 0x0017B70D
		public Unlimited<uint> EncryptionRecipientMaxBurst
		{
			get
			{
				return ThrottlingPolicyDefaults.EncryptionRecipientMaxBurst;
			}
		}

		// Token: 0x17002933 RID: 10547
		// (get) Token: 0x0600738E RID: 29582 RVA: 0x0017D514 File Offset: 0x0017B714
		public Unlimited<uint> EncryptionRecipientRechargeRate
		{
			get
			{
				return ThrottlingPolicyDefaults.EncryptionRecipientRechargeRate;
			}
		}

		// Token: 0x17002934 RID: 10548
		// (get) Token: 0x0600738F RID: 29583 RVA: 0x0017D51B File Offset: 0x0017B71B
		public Unlimited<uint> EncryptionRecipientCutoffBalance
		{
			get
			{
				return ThrottlingPolicyDefaults.EncryptionRecipientCutoffBalance;
			}
		}

		// Token: 0x17002935 RID: 10549
		// (get) Token: 0x06007390 RID: 29584 RVA: 0x0017D522 File Offset: 0x0017B722
		public Unlimited<uint> ComplianceMaxExpansionDGRecipients
		{
			get
			{
				return ThrottlingPolicyDefaults.ComplianceMaxExpansionDGRecipients;
			}
		}

		// Token: 0x17002936 RID: 10550
		// (get) Token: 0x06007391 RID: 29585 RVA: 0x0017D529 File Offset: 0x0017B729
		public Unlimited<uint> ComplianceMaxExpansionNestedDGs
		{
			get
			{
				return ThrottlingPolicyDefaults.ComplianceMaxExpansionNestedDGs;
			}
		}

		// Token: 0x06007392 RID: 29586 RVA: 0x0017D530 File Offset: 0x0017B730
		public string GetIdentityString()
		{
			return "[Fallback]";
		}

		// Token: 0x06007393 RID: 29587 RVA: 0x0017D537 File Offset: 0x0017B737
		public string GetShortIdentityString()
		{
			return "[Fallback]";
		}

		// Token: 0x04004AB1 RID: 19121
		private const string FallbackIdentity = "[Fallback]";

		// Token: 0x04004AB2 RID: 19122
		private static FallbackThrottlingPolicy singleton = new FallbackThrottlingPolicy();
	}
}
