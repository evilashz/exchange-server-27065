using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020009B9 RID: 2489
	internal class EffectiveThrottlingPolicy : IThrottlingPolicy
	{
		// Token: 0x060072BC RID: 29372 RVA: 0x0017BD5B File Offset: 0x00179F5B
		static EffectiveThrottlingPolicy()
		{
			EffectiveThrottlingPolicy.rootOrgConfigSession.SessionSettings.IsSharedConfigChecked = true;
		}

		// Token: 0x060072BD RID: 29373 RVA: 0x0017BD92 File Offset: 0x00179F92
		public EffectiveThrottlingPolicy(ThrottlingPolicy dataObject) : this(dataObject, false)
		{
		}

		// Token: 0x060072BE RID: 29374 RVA: 0x0017BD9C File Offset: 0x00179F9C
		public EffectiveThrottlingPolicy(ThrottlingPolicy dataObject, bool useCacheToGetParent)
		{
			if (dataObject == null)
			{
				throw new ArgumentNullException("dataObject");
			}
			this.ThrottlingPolicy = dataObject;
			IThrottlingPolicy parentThrottlingPolicy;
			if (useCacheToGetParent)
			{
				parentThrottlingPolicy = this.GetParentThrottlingPolicyFromCache(dataObject);
			}
			else
			{
				parentThrottlingPolicy = this.GetParentThrottlingPolicyFromAD(dataObject);
			}
			this.MergeValuesFromParentPolicy(parentThrottlingPolicy);
		}

		// Token: 0x060072BF RID: 29375 RVA: 0x0017BDE4 File Offset: 0x00179FE4
		private static ThrottlingPolicy ReadOrganizationThrottlingPolicyFromAD(OrganizationId orgId)
		{
			SharedConfiguration sharedConfiguration = SharedConfiguration.GetSharedConfiguration(orgId);
			if (sharedConfiguration != null)
			{
				orgId = sharedConfiguration.SharedConfigId;
			}
			ADSessionSettings sessionSettings = ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(orgId);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, sessionSettings, 224, "ReadOrganizationThrottlingPolicyFromAD", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\throttling\\EffectiveThrottlingPolicy.cs");
			tenantOrTopologyConfigurationSession.SessionSettings.IsSharedConfigChecked = true;
			return tenantOrTopologyConfigurationSession.GetOrganizationThrottlingPolicy(orgId);
		}

		// Token: 0x060072C0 RID: 29376 RVA: 0x0017BE3A File Offset: 0x0017A03A
		internal static ThrottlingPolicy ReadGlobalThrottlingPolicyFromAD()
		{
			return EffectiveThrottlingPolicy.rootOrgConfigSession.GetGlobalThrottlingPolicy(true);
		}

		// Token: 0x060072C1 RID: 29377 RVA: 0x0017BE48 File Offset: 0x0017A048
		private IThrottlingPolicy GetParentThrottlingPolicyFromCache(ThrottlingPolicy policy)
		{
			IThrottlingPolicy result;
			switch (policy.ThrottlingPolicyScope)
			{
			case ThrottlingPolicyScopeType.Regular:
				result = ThrottlingPolicyCache.Singleton.Get(this.ThrottlingPolicy.OrganizationId);
				break;
			case ThrottlingPolicyScopeType.Organization:
				result = ThrottlingPolicyCache.Singleton.GetGlobalThrottlingPolicy();
				break;
			case ThrottlingPolicyScopeType.Global:
				result = FallbackThrottlingPolicy.GetSingleton();
				break;
			default:
				throw new NotSupportedException(string.Format("Unsupported enum value {0}.", this.ThrottlingPolicy.ThrottlingPolicyScope));
			}
			return result;
		}

		// Token: 0x060072C2 RID: 29378 RVA: 0x0017BEC0 File Offset: 0x0017A0C0
		private IThrottlingPolicy GetParentThrottlingPolicyFromAD(ThrottlingPolicy policy)
		{
			IThrottlingPolicy result;
			switch (this.ThrottlingPolicy.ThrottlingPolicyScope)
			{
			case ThrottlingPolicyScopeType.Regular:
			{
				ThrottlingPolicy throttlingPolicy = EffectiveThrottlingPolicy.ReadOrganizationThrottlingPolicyFromAD(policy.OrganizationId);
				if (throttlingPolicy == null)
				{
					throttlingPolicy = EffectiveThrottlingPolicy.ReadGlobalThrottlingPolicyFromAD();
				}
				result = throttlingPolicy.GetEffectiveThrottlingPolicy(false);
				break;
			}
			case ThrottlingPolicyScopeType.Organization:
				result = EffectiveThrottlingPolicy.ReadGlobalThrottlingPolicyFromAD().GetEffectiveThrottlingPolicy(false);
				break;
			case ThrottlingPolicyScopeType.Global:
				result = FallbackThrottlingPolicy.GetSingleton();
				break;
			default:
				throw new NotSupportedException(string.Format("Unsupported enum value {0}.", this.ThrottlingPolicy.ThrottlingPolicyScope));
			}
			return result;
		}

		// Token: 0x1700286E RID: 10350
		// (get) Token: 0x060072C3 RID: 29379 RVA: 0x0017BF43 File Offset: 0x0017A143
		// (set) Token: 0x060072C4 RID: 29380 RVA: 0x0017BF4B File Offset: 0x0017A14B
		internal ThrottlingPolicy ThrottlingPolicy { get; private set; }

		// Token: 0x1700286F RID: 10351
		// (get) Token: 0x060072C5 RID: 29381 RVA: 0x0017BF54 File Offset: 0x0017A154
		public ADObjectId Id
		{
			get
			{
				return this.ThrottlingPolicy.Id;
			}
		}

		// Token: 0x17002870 RID: 10352
		// (get) Token: 0x060072C6 RID: 29382 RVA: 0x0017BF61 File Offset: 0x0017A161
		public OrganizationId OrganizationId
		{
			get
			{
				return this.ThrottlingPolicy.OrganizationId;
			}
		}

		// Token: 0x17002871 RID: 10353
		// (get) Token: 0x060072C7 RID: 29383 RVA: 0x0017BF6E File Offset: 0x0017A16E
		public bool IsFallback
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002872 RID: 10354
		// (get) Token: 0x060072C8 RID: 29384 RVA: 0x0017BF71 File Offset: 0x0017A171
		public bool IsUnthrottled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060072C9 RID: 29385 RVA: 0x0017BF74 File Offset: 0x0017A174
		string IThrottlingPolicy.GetIdentityString()
		{
			return this.ThrottlingPolicy.DistinguishedName;
		}

		// Token: 0x060072CA RID: 29386 RVA: 0x0017BF81 File Offset: 0x0017A181
		string IThrottlingPolicy.GetShortIdentityString()
		{
			return this.ThrottlingPolicy.Name;
		}

		// Token: 0x17002873 RID: 10355
		// (get) Token: 0x060072CB RID: 29387 RVA: 0x0017BF8E File Offset: 0x0017A18E
		public ThrottlingPolicyScopeType ThrottlingPolicyScope
		{
			get
			{
				return this.ThrottlingPolicy.ThrottlingPolicyScope;
			}
		}

		// Token: 0x17002874 RID: 10356
		// (get) Token: 0x060072CC RID: 29388 RVA: 0x0017BF9B File Offset: 0x0017A19B
		public bool IsServiceAccount
		{
			get
			{
				return this.ThrottlingPolicy.IsServiceAccount;
			}
		}

		// Token: 0x17002875 RID: 10357
		// (get) Token: 0x060072CD RID: 29389 RVA: 0x0017BFA8 File Offset: 0x0017A1A8
		public Unlimited<uint> AnonymousMaxConcurrency
		{
			get
			{
				return this.anonymousMaxConcurrency;
			}
		}

		// Token: 0x17002876 RID: 10358
		// (get) Token: 0x060072CE RID: 29390 RVA: 0x0017BFB0 File Offset: 0x0017A1B0
		public Unlimited<uint> AnonymousMaxBurst
		{
			get
			{
				return this.anonymousMaxBurst;
			}
		}

		// Token: 0x17002877 RID: 10359
		// (get) Token: 0x060072CF RID: 29391 RVA: 0x0017BFB8 File Offset: 0x0017A1B8
		public Unlimited<uint> AnonymousRechargeRate
		{
			get
			{
				return this.anonymousRechargeRate;
			}
		}

		// Token: 0x17002878 RID: 10360
		// (get) Token: 0x060072D0 RID: 29392 RVA: 0x0017BFC0 File Offset: 0x0017A1C0
		public Unlimited<uint> AnonymousCutoffBalance
		{
			get
			{
				return this.anonymousCutoffBalance;
			}
		}

		// Token: 0x17002879 RID: 10361
		// (get) Token: 0x060072D1 RID: 29393 RVA: 0x0017BFC8 File Offset: 0x0017A1C8
		public Unlimited<uint> EasMaxConcurrency
		{
			get
			{
				return this.easMaxConcurrency;
			}
		}

		// Token: 0x1700287A RID: 10362
		// (get) Token: 0x060072D2 RID: 29394 RVA: 0x0017BFD0 File Offset: 0x0017A1D0
		public Unlimited<uint> EasMaxBurst
		{
			get
			{
				return this.easMaxBurst;
			}
		}

		// Token: 0x1700287B RID: 10363
		// (get) Token: 0x060072D3 RID: 29395 RVA: 0x0017BFD8 File Offset: 0x0017A1D8
		public Unlimited<uint> EasRechargeRate
		{
			get
			{
				return this.easRechargeRate;
			}
		}

		// Token: 0x1700287C RID: 10364
		// (get) Token: 0x060072D4 RID: 29396 RVA: 0x0017BFE0 File Offset: 0x0017A1E0
		public Unlimited<uint> EasCutoffBalance
		{
			get
			{
				return this.easCutoffBalance;
			}
		}

		// Token: 0x1700287D RID: 10365
		// (get) Token: 0x060072D5 RID: 29397 RVA: 0x0017BFE8 File Offset: 0x0017A1E8
		public Unlimited<uint> EasMaxDevices
		{
			get
			{
				return this.easMaxDevices;
			}
		}

		// Token: 0x1700287E RID: 10366
		// (get) Token: 0x060072D6 RID: 29398 RVA: 0x0017BFF0 File Offset: 0x0017A1F0
		public Unlimited<uint> EasMaxDeviceDeletesPerMonth
		{
			get
			{
				return this.easMaxDeviceDeletesPerMonth;
			}
		}

		// Token: 0x1700287F RID: 10367
		// (get) Token: 0x060072D7 RID: 29399 RVA: 0x0017BFF8 File Offset: 0x0017A1F8
		public Unlimited<uint> EasMaxInactivityForDeviceCleanup
		{
			get
			{
				return this.easMaxInactivityForDeviceCleanup;
			}
		}

		// Token: 0x17002880 RID: 10368
		// (get) Token: 0x060072D8 RID: 29400 RVA: 0x0017C000 File Offset: 0x0017A200
		public Unlimited<uint> EwsMaxConcurrency
		{
			get
			{
				return this.ewsMaxConcurrency;
			}
		}

		// Token: 0x17002881 RID: 10369
		// (get) Token: 0x060072D9 RID: 29401 RVA: 0x0017C008 File Offset: 0x0017A208
		public Unlimited<uint> EwsMaxBurst
		{
			get
			{
				return this.ewsMaxBurst;
			}
		}

		// Token: 0x17002882 RID: 10370
		// (get) Token: 0x060072DA RID: 29402 RVA: 0x0017C010 File Offset: 0x0017A210
		public Unlimited<uint> EwsRechargeRate
		{
			get
			{
				return this.ewsRechargeRate;
			}
		}

		// Token: 0x17002883 RID: 10371
		// (get) Token: 0x060072DB RID: 29403 RVA: 0x0017C018 File Offset: 0x0017A218
		public Unlimited<uint> EwsCutoffBalance
		{
			get
			{
				return this.ewsCutoffBalance;
			}
		}

		// Token: 0x17002884 RID: 10372
		// (get) Token: 0x060072DC RID: 29404 RVA: 0x0017C020 File Offset: 0x0017A220
		public Unlimited<uint> EwsMaxSubscriptions
		{
			get
			{
				return this.ewsMaxSubscriptions;
			}
		}

		// Token: 0x17002885 RID: 10373
		// (get) Token: 0x060072DD RID: 29405 RVA: 0x0017C028 File Offset: 0x0017A228
		public Unlimited<uint> ImapMaxConcurrency
		{
			get
			{
				return this.imapMaxConcurrency;
			}
		}

		// Token: 0x17002886 RID: 10374
		// (get) Token: 0x060072DE RID: 29406 RVA: 0x0017C030 File Offset: 0x0017A230
		public Unlimited<uint> ImapMaxBurst
		{
			get
			{
				return this.imapMaxBurst;
			}
		}

		// Token: 0x17002887 RID: 10375
		// (get) Token: 0x060072DF RID: 29407 RVA: 0x0017C038 File Offset: 0x0017A238
		public Unlimited<uint> ImapRechargeRate
		{
			get
			{
				return this.imapRechargeRate;
			}
		}

		// Token: 0x17002888 RID: 10376
		// (get) Token: 0x060072E0 RID: 29408 RVA: 0x0017C040 File Offset: 0x0017A240
		public Unlimited<uint> ImapCutoffBalance
		{
			get
			{
				return this.imapCutoffBalance;
			}
		}

		// Token: 0x17002889 RID: 10377
		// (get) Token: 0x060072E1 RID: 29409 RVA: 0x0017C048 File Offset: 0x0017A248
		public Unlimited<uint> OutlookServiceMaxConcurrency
		{
			get
			{
				return this.outlookServiceMaxConcurrency;
			}
		}

		// Token: 0x1700288A RID: 10378
		// (get) Token: 0x060072E2 RID: 29410 RVA: 0x0017C050 File Offset: 0x0017A250
		public Unlimited<uint> OutlookServiceMaxBurst
		{
			get
			{
				return this.outlookServiceMaxBurst;
			}
		}

		// Token: 0x1700288B RID: 10379
		// (get) Token: 0x060072E3 RID: 29411 RVA: 0x0017C058 File Offset: 0x0017A258
		public Unlimited<uint> OutlookServiceRechargeRate
		{
			get
			{
				return this.outlookServiceRechargeRate;
			}
		}

		// Token: 0x1700288C RID: 10380
		// (get) Token: 0x060072E4 RID: 29412 RVA: 0x0017C060 File Offset: 0x0017A260
		public Unlimited<uint> OutlookServiceCutoffBalance
		{
			get
			{
				return this.outlookServiceCutoffBalance;
			}
		}

		// Token: 0x1700288D RID: 10381
		// (get) Token: 0x060072E5 RID: 29413 RVA: 0x0017C068 File Offset: 0x0017A268
		public Unlimited<uint> OutlookServiceMaxSubscriptions
		{
			get
			{
				return this.outlookServiceMaxSubscriptions;
			}
		}

		// Token: 0x1700288E RID: 10382
		// (get) Token: 0x060072E6 RID: 29414 RVA: 0x0017C070 File Offset: 0x0017A270
		public Unlimited<uint> OutlookServiceMaxSocketConnectionsPerDevice
		{
			get
			{
				return this.outlookServiceMaxSocketConnectionsPerDevice;
			}
		}

		// Token: 0x1700288F RID: 10383
		// (get) Token: 0x060072E7 RID: 29415 RVA: 0x0017C078 File Offset: 0x0017A278
		public Unlimited<uint> OutlookServiceMaxSocketConnectionsPerUser
		{
			get
			{
				return this.outlookServiceMaxSocketConnectionsPerUser;
			}
		}

		// Token: 0x17002890 RID: 10384
		// (get) Token: 0x060072E8 RID: 29416 RVA: 0x0017C080 File Offset: 0x0017A280
		public Unlimited<uint> OwaMaxConcurrency
		{
			get
			{
				return this.owaMaxConcurrency;
			}
		}

		// Token: 0x17002891 RID: 10385
		// (get) Token: 0x060072E9 RID: 29417 RVA: 0x0017C088 File Offset: 0x0017A288
		public Unlimited<uint> OwaMaxBurst
		{
			get
			{
				return this.owaMaxBurst;
			}
		}

		// Token: 0x17002892 RID: 10386
		// (get) Token: 0x060072EA RID: 29418 RVA: 0x0017C090 File Offset: 0x0017A290
		public Unlimited<uint> OwaRechargeRate
		{
			get
			{
				return this.owaRechargeRate;
			}
		}

		// Token: 0x17002893 RID: 10387
		// (get) Token: 0x060072EB RID: 29419 RVA: 0x0017C098 File Offset: 0x0017A298
		public Unlimited<uint> OwaCutoffBalance
		{
			get
			{
				return this.owaCutoffBalance;
			}
		}

		// Token: 0x17002894 RID: 10388
		// (get) Token: 0x060072EC RID: 29420 RVA: 0x0017C0A0 File Offset: 0x0017A2A0
		public Unlimited<uint> OwaVoiceMaxConcurrency
		{
			get
			{
				return this.owaVoiceMaxConcurrency;
			}
		}

		// Token: 0x17002895 RID: 10389
		// (get) Token: 0x060072ED RID: 29421 RVA: 0x0017C0A8 File Offset: 0x0017A2A8
		public Unlimited<uint> OwaVoiceMaxBurst
		{
			get
			{
				return this.owaVoiceMaxBurst;
			}
		}

		// Token: 0x17002896 RID: 10390
		// (get) Token: 0x060072EE RID: 29422 RVA: 0x0017C0B0 File Offset: 0x0017A2B0
		public Unlimited<uint> OwaVoiceRechargeRate
		{
			get
			{
				return this.owaVoiceRechargeRate;
			}
		}

		// Token: 0x17002897 RID: 10391
		// (get) Token: 0x060072EF RID: 29423 RVA: 0x0017C0B8 File Offset: 0x0017A2B8
		public Unlimited<uint> OwaVoiceCutoffBalance
		{
			get
			{
				return this.owaVoiceCutoffBalance;
			}
		}

		// Token: 0x17002898 RID: 10392
		// (get) Token: 0x060072F0 RID: 29424 RVA: 0x0017C0C0 File Offset: 0x0017A2C0
		public Unlimited<uint> PopMaxConcurrency
		{
			get
			{
				return this.popMaxConcurrency;
			}
		}

		// Token: 0x17002899 RID: 10393
		// (get) Token: 0x060072F1 RID: 29425 RVA: 0x0017C0C8 File Offset: 0x0017A2C8
		public Unlimited<uint> PopMaxBurst
		{
			get
			{
				return this.popMaxBurst;
			}
		}

		// Token: 0x1700289A RID: 10394
		// (get) Token: 0x060072F2 RID: 29426 RVA: 0x0017C0D0 File Offset: 0x0017A2D0
		public Unlimited<uint> PopRechargeRate
		{
			get
			{
				return this.popRechargeRate;
			}
		}

		// Token: 0x1700289B RID: 10395
		// (get) Token: 0x060072F3 RID: 29427 RVA: 0x0017C0D8 File Offset: 0x0017A2D8
		public Unlimited<uint> PopCutoffBalance
		{
			get
			{
				return this.popCutoffBalance;
			}
		}

		// Token: 0x1700289C RID: 10396
		// (get) Token: 0x060072F4 RID: 29428 RVA: 0x0017C0E0 File Offset: 0x0017A2E0
		public Unlimited<uint> RcaMaxConcurrency
		{
			get
			{
				return this.rcaMaxConcurrency;
			}
		}

		// Token: 0x1700289D RID: 10397
		// (get) Token: 0x060072F5 RID: 29429 RVA: 0x0017C0E8 File Offset: 0x0017A2E8
		public Unlimited<uint> RcaMaxBurst
		{
			get
			{
				return this.rcaMaxBurst;
			}
		}

		// Token: 0x1700289E RID: 10398
		// (get) Token: 0x060072F6 RID: 29430 RVA: 0x0017C0F0 File Offset: 0x0017A2F0
		public Unlimited<uint> RcaRechargeRate
		{
			get
			{
				return this.rcaRechargeRate;
			}
		}

		// Token: 0x1700289F RID: 10399
		// (get) Token: 0x060072F7 RID: 29431 RVA: 0x0017C0F8 File Offset: 0x0017A2F8
		public Unlimited<uint> RcaCutoffBalance
		{
			get
			{
				return this.rcaCutoffBalance;
			}
		}

		// Token: 0x170028A0 RID: 10400
		// (get) Token: 0x060072F8 RID: 29432 RVA: 0x0017C100 File Offset: 0x0017A300
		public Unlimited<uint> CpaMaxConcurrency
		{
			get
			{
				return this.cpaMaxConcurrency;
			}
		}

		// Token: 0x170028A1 RID: 10401
		// (get) Token: 0x060072F9 RID: 29433 RVA: 0x0017C108 File Offset: 0x0017A308
		public Unlimited<uint> CpaMaxBurst
		{
			get
			{
				return this.cpaMaxBurst;
			}
		}

		// Token: 0x170028A2 RID: 10402
		// (get) Token: 0x060072FA RID: 29434 RVA: 0x0017C110 File Offset: 0x0017A310
		public Unlimited<uint> CpaRechargeRate
		{
			get
			{
				return this.cpaRechargeRate;
			}
		}

		// Token: 0x170028A3 RID: 10403
		// (get) Token: 0x060072FB RID: 29435 RVA: 0x0017C118 File Offset: 0x0017A318
		public Unlimited<uint> CpaCutoffBalance
		{
			get
			{
				return this.cpaCutoffBalance;
			}
		}

		// Token: 0x170028A4 RID: 10404
		// (get) Token: 0x060072FC RID: 29436 RVA: 0x0017C120 File Offset: 0x0017A320
		public Unlimited<uint> PowerShellMaxConcurrency
		{
			get
			{
				return this.powerShellMaxConcurrency;
			}
		}

		// Token: 0x170028A5 RID: 10405
		// (get) Token: 0x060072FD RID: 29437 RVA: 0x0017C128 File Offset: 0x0017A328
		public Unlimited<uint> PowerShellMaxBurst
		{
			get
			{
				return this.powerShellMaxBurst;
			}
		}

		// Token: 0x170028A6 RID: 10406
		// (get) Token: 0x060072FE RID: 29438 RVA: 0x0017C130 File Offset: 0x0017A330
		public Unlimited<uint> PowerShellRechargeRate
		{
			get
			{
				return this.powerShellRechargeRate;
			}
		}

		// Token: 0x170028A7 RID: 10407
		// (get) Token: 0x060072FF RID: 29439 RVA: 0x0017C138 File Offset: 0x0017A338
		public Unlimited<uint> PowerShellCutoffBalance
		{
			get
			{
				return this.powerShellCutoffBalance;
			}
		}

		// Token: 0x170028A8 RID: 10408
		// (get) Token: 0x06007300 RID: 29440 RVA: 0x0017C140 File Offset: 0x0017A340
		public Unlimited<uint> PowerShellMaxTenantConcurrency
		{
			get
			{
				return this.powerShellMaxTenantConcurrency;
			}
		}

		// Token: 0x170028A9 RID: 10409
		// (get) Token: 0x06007301 RID: 29441 RVA: 0x0017C148 File Offset: 0x0017A348
		public Unlimited<uint> PowerShellMaxOperations
		{
			get
			{
				return this.powerShellMaxOperations;
			}
		}

		// Token: 0x170028AA RID: 10410
		// (get) Token: 0x06007302 RID: 29442 RVA: 0x0017C150 File Offset: 0x0017A350
		public Unlimited<uint> PowerShellMaxCmdletsTimePeriod
		{
			get
			{
				return this.powerShellMaxCmdletsTimePeriod;
			}
		}

		// Token: 0x170028AB RID: 10411
		// (get) Token: 0x06007303 RID: 29443 RVA: 0x0017C158 File Offset: 0x0017A358
		public Unlimited<uint> PowerShellMaxCmdletQueueDepth
		{
			get
			{
				return this.powerShellMaxCmdletQueueDepth;
			}
		}

		// Token: 0x170028AC RID: 10412
		// (get) Token: 0x06007304 RID: 29444 RVA: 0x0017C160 File Offset: 0x0017A360
		public Unlimited<uint> ExchangeMaxCmdlets
		{
			get
			{
				return this.exchangeMaxCmdlets;
			}
		}

		// Token: 0x170028AD RID: 10413
		// (get) Token: 0x06007305 RID: 29445 RVA: 0x0017C168 File Offset: 0x0017A368
		public Unlimited<uint> PowerShellMaxDestructiveCmdlets
		{
			get
			{
				return this.powerShellMaxDestructiveCmdlets;
			}
		}

		// Token: 0x170028AE RID: 10414
		// (get) Token: 0x06007306 RID: 29446 RVA: 0x0017C170 File Offset: 0x0017A370
		public Unlimited<uint> PowerShellMaxDestructiveCmdletsTimePeriod
		{
			get
			{
				return this.powerShellMaxDestructiveCmdletsTimePeriod;
			}
		}

		// Token: 0x170028AF RID: 10415
		// (get) Token: 0x06007307 RID: 29447 RVA: 0x0017C178 File Offset: 0x0017A378
		public Unlimited<uint> PowerShellMaxCmdlets
		{
			get
			{
				return this.powerShellMaxCmdlets;
			}
		}

		// Token: 0x170028B0 RID: 10416
		// (get) Token: 0x06007308 RID: 29448 RVA: 0x0017C180 File Offset: 0x0017A380
		public Unlimited<uint> PowerShellMaxRunspaces
		{
			get
			{
				return this.powerShellMaxRunspaces;
			}
		}

		// Token: 0x170028B1 RID: 10417
		// (get) Token: 0x06007309 RID: 29449 RVA: 0x0017C188 File Offset: 0x0017A388
		public Unlimited<uint> PowerShellMaxTenantRunspaces
		{
			get
			{
				return this.powerShellMaxTenantRunspaces;
			}
		}

		// Token: 0x170028B2 RID: 10418
		// (get) Token: 0x0600730A RID: 29450 RVA: 0x0017C190 File Offset: 0x0017A390
		public Unlimited<uint> PowerShellMaxRunspacesTimePeriod
		{
			get
			{
				return this.powerShellMaxRunspacesTimePeriod;
			}
		}

		// Token: 0x170028B3 RID: 10419
		// (get) Token: 0x0600730B RID: 29451 RVA: 0x0017C198 File Offset: 0x0017A398
		public Unlimited<uint> PswsMaxConcurrency
		{
			get
			{
				return this.pswsMaxConcurrency;
			}
		}

		// Token: 0x170028B4 RID: 10420
		// (get) Token: 0x0600730C RID: 29452 RVA: 0x0017C1A0 File Offset: 0x0017A3A0
		public Unlimited<uint> PswsMaxRequest
		{
			get
			{
				return this.pswsMaxRequest;
			}
		}

		// Token: 0x170028B5 RID: 10421
		// (get) Token: 0x0600730D RID: 29453 RVA: 0x0017C1A8 File Offset: 0x0017A3A8
		public Unlimited<uint> PswsMaxRequestTimePeriod
		{
			get
			{
				return this.pswsMaxRequestTimePeriod;
			}
		}

		// Token: 0x170028B6 RID: 10422
		// (get) Token: 0x0600730E RID: 29454 RVA: 0x0017C1B0 File Offset: 0x0017A3B0
		public Unlimited<uint> MessageRateLimit
		{
			get
			{
				return this.messageRateLimit;
			}
		}

		// Token: 0x170028B7 RID: 10423
		// (get) Token: 0x0600730F RID: 29455 RVA: 0x0017C1B8 File Offset: 0x0017A3B8
		public Unlimited<uint> RecipientRateLimit
		{
			get
			{
				return this.recipientRateLimit;
			}
		}

		// Token: 0x170028B8 RID: 10424
		// (get) Token: 0x06007310 RID: 29456 RVA: 0x0017C1C0 File Offset: 0x0017A3C0
		public Unlimited<uint> ForwardeeLimit
		{
			get
			{
				return this.forwardeeLimit;
			}
		}

		// Token: 0x170028B9 RID: 10425
		// (get) Token: 0x06007311 RID: 29457 RVA: 0x0017C1C8 File Offset: 0x0017A3C8
		public Unlimited<uint> DiscoveryMaxConcurrency
		{
			get
			{
				return this.discoveryMaxConcurrency;
			}
		}

		// Token: 0x170028BA RID: 10426
		// (get) Token: 0x06007312 RID: 29458 RVA: 0x0017C1D0 File Offset: 0x0017A3D0
		public Unlimited<uint> DiscoveryMaxMailboxes
		{
			get
			{
				return this.discoveryMaxMailboxes;
			}
		}

		// Token: 0x170028BB RID: 10427
		// (get) Token: 0x06007313 RID: 29459 RVA: 0x0017C1D8 File Offset: 0x0017A3D8
		public Unlimited<uint> DiscoveryMaxKeywords
		{
			get
			{
				return this.discoveryMaxKeywords;
			}
		}

		// Token: 0x170028BC RID: 10428
		// (get) Token: 0x06007314 RID: 29460 RVA: 0x0017C1E0 File Offset: 0x0017A3E0
		public Unlimited<uint> DiscoveryMaxPreviewSearchMailboxes
		{
			get
			{
				return this.discoveryMaxPreviewSearchMailboxes;
			}
		}

		// Token: 0x170028BD RID: 10429
		// (get) Token: 0x06007315 RID: 29461 RVA: 0x0017C1E8 File Offset: 0x0017A3E8
		public Unlimited<uint> DiscoveryMaxStatsSearchMailboxes
		{
			get
			{
				return this.discoveryMaxStatsSearchMailboxes;
			}
		}

		// Token: 0x170028BE RID: 10430
		// (get) Token: 0x06007316 RID: 29462 RVA: 0x0017C1F0 File Offset: 0x0017A3F0
		public Unlimited<uint> DiscoveryPreviewSearchResultsPageSize
		{
			get
			{
				return this.discoveryPreviewSearchResultsPageSize;
			}
		}

		// Token: 0x170028BF RID: 10431
		// (get) Token: 0x06007317 RID: 29463 RVA: 0x0017C1F8 File Offset: 0x0017A3F8
		public Unlimited<uint> DiscoveryMaxKeywordsPerPage
		{
			get
			{
				return this.discoveryMaxKeywordsPerPage;
			}
		}

		// Token: 0x170028C0 RID: 10432
		// (get) Token: 0x06007318 RID: 29464 RVA: 0x0017C200 File Offset: 0x0017A400
		public Unlimited<uint> DiscoveryMaxRefinerResults
		{
			get
			{
				return this.discoveryMaxRefinerResults;
			}
		}

		// Token: 0x170028C1 RID: 10433
		// (get) Token: 0x06007319 RID: 29465 RVA: 0x0017C208 File Offset: 0x0017A408
		public Unlimited<uint> DiscoveryMaxSearchQueueDepth
		{
			get
			{
				return this.discoveryMaxSearchQueueDepth;
			}
		}

		// Token: 0x170028C2 RID: 10434
		// (get) Token: 0x0600731A RID: 29466 RVA: 0x0017C210 File Offset: 0x0017A410
		public Unlimited<uint> DiscoverySearchTimeoutPeriod
		{
			get
			{
				return this.discoverySearchTimeoutPeriod;
			}
		}

		// Token: 0x170028C3 RID: 10435
		// (get) Token: 0x0600731B RID: 29467 RVA: 0x0017C218 File Offset: 0x0017A418
		public Unlimited<uint> PushNotificationMaxConcurrency
		{
			get
			{
				return this.pushNotificationMaxConcurrency;
			}
		}

		// Token: 0x170028C4 RID: 10436
		// (get) Token: 0x0600731C RID: 29468 RVA: 0x0017C220 File Offset: 0x0017A420
		public Unlimited<uint> PushNotificationMaxBurst
		{
			get
			{
				return this.pushNotificationMaxBurst;
			}
		}

		// Token: 0x170028C5 RID: 10437
		// (get) Token: 0x0600731D RID: 29469 RVA: 0x0017C228 File Offset: 0x0017A428
		public Unlimited<uint> PushNotificationRechargeRate
		{
			get
			{
				return this.pushNotificationRechargeRate;
			}
		}

		// Token: 0x170028C6 RID: 10438
		// (get) Token: 0x0600731E RID: 29470 RVA: 0x0017C230 File Offset: 0x0017A430
		public Unlimited<uint> PushNotificationCutoffBalance
		{
			get
			{
				return this.pushNotificationCutoffBalance;
			}
		}

		// Token: 0x170028C7 RID: 10439
		// (get) Token: 0x0600731F RID: 29471 RVA: 0x0017C238 File Offset: 0x0017A438
		public Unlimited<uint> PushNotificationMaxBurstPerDevice
		{
			get
			{
				return this.pushNotificationMaxBurstPerDevice;
			}
		}

		// Token: 0x170028C8 RID: 10440
		// (get) Token: 0x06007320 RID: 29472 RVA: 0x0017C240 File Offset: 0x0017A440
		public Unlimited<uint> PushNotificationRechargeRatePerDevice
		{
			get
			{
				return this.pushNotificationRechargeRatePerDevice;
			}
		}

		// Token: 0x170028C9 RID: 10441
		// (get) Token: 0x06007321 RID: 29473 RVA: 0x0017C248 File Offset: 0x0017A448
		public Unlimited<uint> PushNotificationSamplingPeriodPerDevice
		{
			get
			{
				return this.pushNotificationSamplingPeriodPerDevice;
			}
		}

		// Token: 0x170028CA RID: 10442
		// (get) Token: 0x06007322 RID: 29474 RVA: 0x0017C250 File Offset: 0x0017A450
		public Unlimited<uint> EncryptionSenderMaxConcurrency
		{
			get
			{
				return this.encryptionSenderMaxConcurrency;
			}
		}

		// Token: 0x170028CB RID: 10443
		// (get) Token: 0x06007323 RID: 29475 RVA: 0x0017C258 File Offset: 0x0017A458
		public Unlimited<uint> EncryptionSenderMaxBurst
		{
			get
			{
				return this.encryptionSenderMaxBurst;
			}
		}

		// Token: 0x170028CC RID: 10444
		// (get) Token: 0x06007324 RID: 29476 RVA: 0x0017C260 File Offset: 0x0017A460
		public Unlimited<uint> EncryptionSenderRechargeRate
		{
			get
			{
				return this.encryptionSenderRechargeRate;
			}
		}

		// Token: 0x170028CD RID: 10445
		// (get) Token: 0x06007325 RID: 29477 RVA: 0x0017C268 File Offset: 0x0017A468
		public Unlimited<uint> EncryptionSenderCutoffBalance
		{
			get
			{
				return this.encryptionSenderCutoffBalance;
			}
		}

		// Token: 0x170028CE RID: 10446
		// (get) Token: 0x06007326 RID: 29478 RVA: 0x0017C270 File Offset: 0x0017A470
		public Unlimited<uint> EncryptionRecipientMaxConcurrency
		{
			get
			{
				return this.encryptionRecipientMaxConcurrency;
			}
		}

		// Token: 0x170028CF RID: 10447
		// (get) Token: 0x06007327 RID: 29479 RVA: 0x0017C278 File Offset: 0x0017A478
		public Unlimited<uint> EncryptionRecipientMaxBurst
		{
			get
			{
				return this.encryptionRecipientMaxBurst;
			}
		}

		// Token: 0x170028D0 RID: 10448
		// (get) Token: 0x06007328 RID: 29480 RVA: 0x0017C280 File Offset: 0x0017A480
		public Unlimited<uint> EncryptionRecipientRechargeRate
		{
			get
			{
				return this.encryptionRecipientRechargeRate;
			}
		}

		// Token: 0x170028D1 RID: 10449
		// (get) Token: 0x06007329 RID: 29481 RVA: 0x0017C288 File Offset: 0x0017A488
		public Unlimited<uint> EncryptionRecipientCutoffBalance
		{
			get
			{
				return this.encryptionRecipientCutoffBalance;
			}
		}

		// Token: 0x170028D2 RID: 10450
		// (get) Token: 0x0600732A RID: 29482 RVA: 0x0017C290 File Offset: 0x0017A490
		public Unlimited<uint> ComplianceMaxExpansionDGRecipients
		{
			get
			{
				return this.complianceMaxExpansionDGRecipients;
			}
		}

		// Token: 0x170028D3 RID: 10451
		// (get) Token: 0x0600732B RID: 29483 RVA: 0x0017C298 File Offset: 0x0017A498
		public Unlimited<uint> ComplianceMaxExpansionNestedDGs
		{
			get
			{
				return this.complianceMaxExpansionNestedDGs;
			}
		}

		// Token: 0x0600732C RID: 29484 RVA: 0x0017C2A0 File Offset: 0x0017A4A0
		private void MergeValuesFromParentPolicy(IThrottlingPolicy parentThrottlingPolicy)
		{
			this.anonymousMaxConcurrency = (this.ThrottlingPolicy.AnonymousMaxConcurrency ?? parentThrottlingPolicy.AnonymousMaxConcurrency);
			this.anonymousMaxBurst = (this.ThrottlingPolicy.AnonymousMaxBurst ?? parentThrottlingPolicy.AnonymousMaxBurst);
			this.anonymousRechargeRate = (this.ThrottlingPolicy.AnonymousRechargeRate ?? parentThrottlingPolicy.AnonymousRechargeRate);
			this.anonymousCutoffBalance = (this.ThrottlingPolicy.AnonymousCutoffBalance ?? parentThrottlingPolicy.AnonymousCutoffBalance);
			this.easMaxConcurrency = (this.ThrottlingPolicy.EasMaxConcurrency ?? parentThrottlingPolicy.EasMaxConcurrency);
			this.easMaxBurst = (this.ThrottlingPolicy.EasMaxBurst ?? parentThrottlingPolicy.EasMaxBurst);
			this.easRechargeRate = (this.ThrottlingPolicy.EasRechargeRate ?? parentThrottlingPolicy.EasRechargeRate);
			this.easCutoffBalance = (this.ThrottlingPolicy.EasCutoffBalance ?? parentThrottlingPolicy.EasCutoffBalance);
			this.easMaxDevices = (this.ThrottlingPolicy.EasMaxDevices ?? parentThrottlingPolicy.EasMaxDevices);
			this.easMaxDeviceDeletesPerMonth = (this.ThrottlingPolicy.EasMaxDeviceDeletesPerMonth ?? parentThrottlingPolicy.EasMaxDeviceDeletesPerMonth);
			this.easMaxInactivityForDeviceCleanup = (this.ThrottlingPolicy.EasMaxInactivityForDeviceCleanup ?? parentThrottlingPolicy.EasMaxInactivityForDeviceCleanup);
			this.ewsMaxConcurrency = (this.ThrottlingPolicy.EwsMaxConcurrency ?? parentThrottlingPolicy.EwsMaxConcurrency);
			this.ewsMaxBurst = (this.ThrottlingPolicy.EwsMaxBurst ?? parentThrottlingPolicy.EwsMaxBurst);
			this.ewsRechargeRate = (this.ThrottlingPolicy.EwsRechargeRate ?? parentThrottlingPolicy.EwsRechargeRate);
			this.ewsCutoffBalance = (this.ThrottlingPolicy.EwsCutoffBalance ?? parentThrottlingPolicy.EwsCutoffBalance);
			this.ewsMaxSubscriptions = (this.ThrottlingPolicy.EwsMaxSubscriptions ?? parentThrottlingPolicy.EwsMaxSubscriptions);
			this.imapMaxConcurrency = (this.ThrottlingPolicy.ImapMaxConcurrency ?? parentThrottlingPolicy.ImapMaxConcurrency);
			this.imapMaxBurst = (this.ThrottlingPolicy.ImapMaxBurst ?? parentThrottlingPolicy.ImapMaxBurst);
			this.imapRechargeRate = (this.ThrottlingPolicy.ImapRechargeRate ?? parentThrottlingPolicy.ImapRechargeRate);
			this.imapCutoffBalance = (this.ThrottlingPolicy.ImapCutoffBalance ?? parentThrottlingPolicy.ImapCutoffBalance);
			this.outlookServiceMaxConcurrency = (this.ThrottlingPolicy.OutlookServiceMaxConcurrency ?? parentThrottlingPolicy.OutlookServiceMaxConcurrency);
			this.outlookServiceMaxBurst = (this.ThrottlingPolicy.OutlookServiceMaxBurst ?? parentThrottlingPolicy.OutlookServiceMaxBurst);
			this.outlookServiceRechargeRate = (this.ThrottlingPolicy.OutlookServiceRechargeRate ?? parentThrottlingPolicy.OutlookServiceRechargeRate);
			this.outlookServiceCutoffBalance = (this.ThrottlingPolicy.OutlookServiceCutoffBalance ?? parentThrottlingPolicy.OutlookServiceCutoffBalance);
			this.outlookServiceMaxSubscriptions = (this.ThrottlingPolicy.OutlookServiceMaxSubscriptions ?? parentThrottlingPolicy.OutlookServiceMaxSubscriptions);
			this.outlookServiceMaxSocketConnectionsPerDevice = (this.ThrottlingPolicy.OutlookServiceMaxSocketConnectionsPerDevice ?? parentThrottlingPolicy.OutlookServiceMaxSocketConnectionsPerDevice);
			this.outlookServiceMaxSocketConnectionsPerUser = (this.ThrottlingPolicy.OutlookServiceMaxSocketConnectionsPerUser ?? parentThrottlingPolicy.OutlookServiceMaxSocketConnectionsPerUser);
			this.owaMaxConcurrency = (this.ThrottlingPolicy.OwaMaxConcurrency ?? parentThrottlingPolicy.OwaMaxConcurrency);
			this.owaMaxBurst = (this.ThrottlingPolicy.OwaMaxBurst ?? parentThrottlingPolicy.OwaMaxBurst);
			this.owaRechargeRate = (this.ThrottlingPolicy.OwaRechargeRate ?? parentThrottlingPolicy.OwaRechargeRate);
			this.owaCutoffBalance = (this.ThrottlingPolicy.OwaCutoffBalance ?? parentThrottlingPolicy.OwaCutoffBalance);
			this.owaVoiceMaxConcurrency = (this.ThrottlingPolicy.OwaVoiceMaxConcurrency ?? parentThrottlingPolicy.OwaVoiceMaxConcurrency);
			this.owaVoiceMaxBurst = (this.ThrottlingPolicy.OwaVoiceMaxBurst ?? parentThrottlingPolicy.OwaVoiceMaxBurst);
			this.owaVoiceRechargeRate = (this.ThrottlingPolicy.OwaVoiceRechargeRate ?? parentThrottlingPolicy.OwaVoiceRechargeRate);
			this.owaVoiceCutoffBalance = (this.ThrottlingPolicy.OwaVoiceCutoffBalance ?? parentThrottlingPolicy.OwaVoiceCutoffBalance);
			this.popMaxConcurrency = (this.ThrottlingPolicy.PopMaxConcurrency ?? parentThrottlingPolicy.PopMaxConcurrency);
			this.popMaxBurst = (this.ThrottlingPolicy.PopMaxBurst ?? parentThrottlingPolicy.PopMaxBurst);
			this.popRechargeRate = (this.ThrottlingPolicy.PopRechargeRate ?? parentThrottlingPolicy.PopRechargeRate);
			this.popCutoffBalance = (this.ThrottlingPolicy.PopCutoffBalance ?? parentThrottlingPolicy.PopCutoffBalance);
			this.powerShellMaxConcurrency = (this.ThrottlingPolicy.PowerShellMaxConcurrency ?? parentThrottlingPolicy.PowerShellMaxConcurrency);
			this.powerShellMaxBurst = (this.ThrottlingPolicy.PowerShellMaxBurst ?? parentThrottlingPolicy.PowerShellMaxBurst);
			this.powerShellRechargeRate = (this.ThrottlingPolicy.PowerShellRechargeRate ?? parentThrottlingPolicy.PowerShellRechargeRate);
			this.powerShellCutoffBalance = (this.ThrottlingPolicy.PowerShellCutoffBalance ?? parentThrottlingPolicy.PowerShellCutoffBalance);
			this.powerShellMaxTenantConcurrency = (this.ThrottlingPolicy.PowerShellMaxTenantConcurrency ?? parentThrottlingPolicy.PowerShellMaxTenantConcurrency);
			this.powerShellMaxOperations = (this.ThrottlingPolicy.PowerShellMaxOperations ?? parentThrottlingPolicy.PowerShellMaxOperations);
			this.powerShellMaxCmdletsTimePeriod = (this.ThrottlingPolicy.PowerShellMaxCmdletsTimePeriod ?? parentThrottlingPolicy.PowerShellMaxCmdletsTimePeriod);
			this.exchangeMaxCmdlets = (this.ThrottlingPolicy.ExchangeMaxCmdlets ?? parentThrottlingPolicy.ExchangeMaxCmdlets);
			this.powerShellMaxCmdletQueueDepth = (this.ThrottlingPolicy.PowerShellMaxCmdletQueueDepth ?? parentThrottlingPolicy.PowerShellMaxCmdletQueueDepth);
			this.powerShellMaxDestructiveCmdlets = (this.ThrottlingPolicy.PowerShellMaxDestructiveCmdlets ?? parentThrottlingPolicy.PowerShellMaxDestructiveCmdlets);
			this.powerShellMaxDestructiveCmdletsTimePeriod = (this.ThrottlingPolicy.PowerShellMaxDestructiveCmdletsTimePeriod ?? parentThrottlingPolicy.PowerShellMaxDestructiveCmdletsTimePeriod);
			this.powerShellMaxCmdlets = (this.ThrottlingPolicy.PowerShellMaxCmdlets ?? parentThrottlingPolicy.PowerShellMaxCmdlets);
			this.powerShellMaxRunspaces = (this.ThrottlingPolicy.PowerShellMaxRunspaces ?? parentThrottlingPolicy.PowerShellMaxRunspaces);
			this.powerShellMaxTenantRunspaces = (this.ThrottlingPolicy.PowerShellMaxTenantRunspaces ?? parentThrottlingPolicy.PowerShellMaxTenantRunspaces);
			this.powerShellMaxRunspacesTimePeriod = (this.ThrottlingPolicy.PowerShellMaxRunspacesTimePeriod ?? parentThrottlingPolicy.PowerShellMaxRunspacesTimePeriod);
			this.pswsMaxConcurrency = (this.ThrottlingPolicy.PswsMaxConcurrency ?? parentThrottlingPolicy.PswsMaxConcurrency);
			this.pswsMaxRequest = (this.ThrottlingPolicy.PswsMaxRequest ?? parentThrottlingPolicy.PswsMaxRequest);
			this.pswsMaxRequestTimePeriod = (this.ThrottlingPolicy.PswsMaxRequestTimePeriod ?? parentThrottlingPolicy.PswsMaxRequestTimePeriod);
			this.rcaMaxConcurrency = (this.ThrottlingPolicy.RcaMaxConcurrency ?? parentThrottlingPolicy.RcaMaxConcurrency);
			this.rcaMaxBurst = (this.ThrottlingPolicy.RcaMaxBurst ?? parentThrottlingPolicy.RcaMaxBurst);
			this.rcaRechargeRate = (this.ThrottlingPolicy.RcaRechargeRate ?? parentThrottlingPolicy.RcaRechargeRate);
			this.rcaCutoffBalance = (this.ThrottlingPolicy.RcaCutoffBalance ?? parentThrottlingPolicy.RcaCutoffBalance);
			this.cpaMaxConcurrency = (this.ThrottlingPolicy.CpaMaxConcurrency ?? parentThrottlingPolicy.CpaMaxConcurrency);
			this.cpaMaxBurst = (this.ThrottlingPolicy.CpaMaxBurst ?? parentThrottlingPolicy.CpaMaxBurst);
			this.cpaRechargeRate = (this.ThrottlingPolicy.CpaRechargeRate ?? parentThrottlingPolicy.CpaRechargeRate);
			this.cpaCutoffBalance = (this.ThrottlingPolicy.CpaCutoffBalance ?? parentThrottlingPolicy.CpaCutoffBalance);
			this.messageRateLimit = (this.ThrottlingPolicy.MessageRateLimit ?? parentThrottlingPolicy.MessageRateLimit);
			this.recipientRateLimit = (this.ThrottlingPolicy.RecipientRateLimit ?? parentThrottlingPolicy.RecipientRateLimit);
			this.forwardeeLimit = (this.ThrottlingPolicy.ForwardeeLimit ?? parentThrottlingPolicy.ForwardeeLimit);
			this.discoveryMaxConcurrency = (this.ThrottlingPolicy.DiscoveryMaxConcurrency ?? parentThrottlingPolicy.DiscoveryMaxConcurrency);
			this.discoveryMaxMailboxes = (this.ThrottlingPolicy.DiscoveryMaxMailboxes ?? parentThrottlingPolicy.DiscoveryMaxMailboxes);
			this.discoveryMaxKeywords = (this.ThrottlingPolicy.DiscoveryMaxKeywords ?? parentThrottlingPolicy.DiscoveryMaxKeywords);
			this.discoveryMaxPreviewSearchMailboxes = (this.ThrottlingPolicy.DiscoveryMaxPreviewSearchMailboxes ?? parentThrottlingPolicy.DiscoveryMaxPreviewSearchMailboxes);
			this.discoveryMaxStatsSearchMailboxes = (this.ThrottlingPolicy.DiscoveryMaxStatsSearchMailboxes ?? parentThrottlingPolicy.DiscoveryMaxStatsSearchMailboxes);
			this.discoveryPreviewSearchResultsPageSize = (this.ThrottlingPolicy.DiscoveryPreviewSearchResultsPageSize ?? parentThrottlingPolicy.DiscoveryPreviewSearchResultsPageSize);
			this.discoveryMaxKeywordsPerPage = (this.ThrottlingPolicy.DiscoveryMaxKeywordsPerPage ?? parentThrottlingPolicy.DiscoveryMaxKeywordsPerPage);
			this.discoveryMaxRefinerResults = (this.ThrottlingPolicy.DiscoveryMaxRefinerResults ?? parentThrottlingPolicy.DiscoveryMaxRefinerResults);
			this.discoveryMaxSearchQueueDepth = (this.ThrottlingPolicy.DiscoveryMaxSearchQueueDepth ?? parentThrottlingPolicy.DiscoveryMaxSearchQueueDepth);
			this.discoverySearchTimeoutPeriod = (this.ThrottlingPolicy.DiscoverySearchTimeoutPeriod ?? parentThrottlingPolicy.DiscoverySearchTimeoutPeriod);
			this.pushNotificationMaxConcurrency = (this.ThrottlingPolicy.PushNotificationMaxConcurrency ?? parentThrottlingPolicy.PushNotificationMaxConcurrency);
			this.pushNotificationMaxBurst = (this.ThrottlingPolicy.PushNotificationMaxBurst ?? parentThrottlingPolicy.PushNotificationMaxBurst);
			this.pushNotificationRechargeRate = (this.ThrottlingPolicy.PushNotificationRechargeRate ?? parentThrottlingPolicy.PushNotificationRechargeRate);
			this.pushNotificationCutoffBalance = (this.ThrottlingPolicy.PushNotificationCutoffBalance ?? parentThrottlingPolicy.PushNotificationCutoffBalance);
			this.pushNotificationMaxBurstPerDevice = (this.ThrottlingPolicy.PushNotificationMaxBurstPerDevice ?? parentThrottlingPolicy.PushNotificationMaxBurstPerDevice);
			this.pushNotificationRechargeRatePerDevice = (this.ThrottlingPolicy.PushNotificationRechargeRatePerDevice ?? parentThrottlingPolicy.PushNotificationRechargeRatePerDevice);
			this.pushNotificationSamplingPeriodPerDevice = (this.ThrottlingPolicy.PushNotificationSamplingPeriodPerDevice ?? parentThrottlingPolicy.PushNotificationSamplingPeriodPerDevice);
			this.encryptionSenderMaxConcurrency = (this.ThrottlingPolicy.EncryptionSenderMaxConcurrency ?? parentThrottlingPolicy.EncryptionSenderMaxConcurrency);
			this.encryptionSenderMaxBurst = (this.ThrottlingPolicy.EncryptionSenderMaxBurst ?? parentThrottlingPolicy.EncryptionSenderMaxBurst);
			this.encryptionSenderRechargeRate = (this.ThrottlingPolicy.EncryptionSenderRechargeRate ?? parentThrottlingPolicy.EncryptionSenderRechargeRate);
			this.encryptionSenderCutoffBalance = (this.ThrottlingPolicy.EncryptionSenderCutoffBalance ?? parentThrottlingPolicy.EncryptionSenderCutoffBalance);
			this.encryptionRecipientMaxConcurrency = (this.ThrottlingPolicy.EncryptionRecipientMaxConcurrency ?? parentThrottlingPolicy.EncryptionRecipientMaxConcurrency);
			this.encryptionRecipientMaxBurst = (this.ThrottlingPolicy.EncryptionRecipientMaxBurst ?? parentThrottlingPolicy.EncryptionRecipientMaxBurst);
			this.encryptionRecipientRechargeRate = (this.ThrottlingPolicy.EncryptionRecipientRechargeRate ?? parentThrottlingPolicy.EncryptionRecipientRechargeRate);
			this.encryptionRecipientCutoffBalance = (this.ThrottlingPolicy.EncryptionRecipientCutoffBalance ?? parentThrottlingPolicy.EncryptionRecipientCutoffBalance);
			this.complianceMaxExpansionDGRecipients = (this.ThrottlingPolicy.ComplianceMaxExpansionDGRecipients ?? parentThrottlingPolicy.ComplianceMaxExpansionDGRecipients);
			this.complianceMaxExpansionNestedDGs = (this.ThrottlingPolicy.ComplianceMaxExpansionNestedDGs ?? parentThrottlingPolicy.ComplianceMaxExpansionNestedDGs);
		}

		// Token: 0x04004A50 RID: 19024
		private static ITopologyConfigurationSession rootOrgConfigSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 156, ".cctor", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\throttling\\EffectiveThrottlingPolicy.cs");

		// Token: 0x04004A51 RID: 19025
		private Unlimited<uint> anonymousMaxConcurrency;

		// Token: 0x04004A52 RID: 19026
		private Unlimited<uint> anonymousMaxBurst;

		// Token: 0x04004A53 RID: 19027
		private Unlimited<uint> anonymousRechargeRate;

		// Token: 0x04004A54 RID: 19028
		private Unlimited<uint> anonymousCutoffBalance;

		// Token: 0x04004A55 RID: 19029
		private Unlimited<uint> easMaxConcurrency;

		// Token: 0x04004A56 RID: 19030
		private Unlimited<uint> easMaxBurst;

		// Token: 0x04004A57 RID: 19031
		private Unlimited<uint> easRechargeRate;

		// Token: 0x04004A58 RID: 19032
		private Unlimited<uint> easCutoffBalance;

		// Token: 0x04004A59 RID: 19033
		private Unlimited<uint> easMaxDevices;

		// Token: 0x04004A5A RID: 19034
		private Unlimited<uint> easMaxDeviceDeletesPerMonth;

		// Token: 0x04004A5B RID: 19035
		private Unlimited<uint> easMaxInactivityForDeviceCleanup;

		// Token: 0x04004A5C RID: 19036
		private Unlimited<uint> ewsMaxConcurrency;

		// Token: 0x04004A5D RID: 19037
		private Unlimited<uint> ewsMaxBurst;

		// Token: 0x04004A5E RID: 19038
		private Unlimited<uint> ewsRechargeRate;

		// Token: 0x04004A5F RID: 19039
		private Unlimited<uint> ewsCutoffBalance;

		// Token: 0x04004A60 RID: 19040
		private Unlimited<uint> ewsMaxSubscriptions;

		// Token: 0x04004A61 RID: 19041
		private Unlimited<uint> imapMaxConcurrency;

		// Token: 0x04004A62 RID: 19042
		private Unlimited<uint> imapMaxBurst;

		// Token: 0x04004A63 RID: 19043
		private Unlimited<uint> imapRechargeRate;

		// Token: 0x04004A64 RID: 19044
		private Unlimited<uint> imapCutoffBalance;

		// Token: 0x04004A65 RID: 19045
		private Unlimited<uint> outlookServiceMaxConcurrency;

		// Token: 0x04004A66 RID: 19046
		private Unlimited<uint> outlookServiceMaxBurst;

		// Token: 0x04004A67 RID: 19047
		private Unlimited<uint> outlookServiceRechargeRate;

		// Token: 0x04004A68 RID: 19048
		private Unlimited<uint> outlookServiceCutoffBalance;

		// Token: 0x04004A69 RID: 19049
		private Unlimited<uint> outlookServiceMaxSubscriptions;

		// Token: 0x04004A6A RID: 19050
		private Unlimited<uint> outlookServiceMaxSocketConnectionsPerDevice;

		// Token: 0x04004A6B RID: 19051
		private Unlimited<uint> outlookServiceMaxSocketConnectionsPerUser;

		// Token: 0x04004A6C RID: 19052
		private Unlimited<uint> owaMaxConcurrency;

		// Token: 0x04004A6D RID: 19053
		private Unlimited<uint> owaMaxBurst;

		// Token: 0x04004A6E RID: 19054
		private Unlimited<uint> owaRechargeRate;

		// Token: 0x04004A6F RID: 19055
		private Unlimited<uint> owaCutoffBalance;

		// Token: 0x04004A70 RID: 19056
		private Unlimited<uint> owaVoiceMaxConcurrency;

		// Token: 0x04004A71 RID: 19057
		private Unlimited<uint> owaVoiceMaxBurst;

		// Token: 0x04004A72 RID: 19058
		private Unlimited<uint> owaVoiceRechargeRate;

		// Token: 0x04004A73 RID: 19059
		private Unlimited<uint> owaVoiceCutoffBalance;

		// Token: 0x04004A74 RID: 19060
		private Unlimited<uint> popMaxConcurrency;

		// Token: 0x04004A75 RID: 19061
		private Unlimited<uint> popMaxBurst;

		// Token: 0x04004A76 RID: 19062
		private Unlimited<uint> popRechargeRate;

		// Token: 0x04004A77 RID: 19063
		private Unlimited<uint> popCutoffBalance;

		// Token: 0x04004A78 RID: 19064
		private Unlimited<uint> powerShellMaxConcurrency;

		// Token: 0x04004A79 RID: 19065
		private Unlimited<uint> powerShellMaxBurst;

		// Token: 0x04004A7A RID: 19066
		private Unlimited<uint> powerShellRechargeRate;

		// Token: 0x04004A7B RID: 19067
		private Unlimited<uint> powerShellCutoffBalance;

		// Token: 0x04004A7C RID: 19068
		private Unlimited<uint> powerShellMaxTenantConcurrency;

		// Token: 0x04004A7D RID: 19069
		private Unlimited<uint> powerShellMaxOperations;

		// Token: 0x04004A7E RID: 19070
		private Unlimited<uint> powerShellMaxCmdletsTimePeriod;

		// Token: 0x04004A7F RID: 19071
		private Unlimited<uint> exchangeMaxCmdlets;

		// Token: 0x04004A80 RID: 19072
		private Unlimited<uint> powerShellMaxCmdletQueueDepth;

		// Token: 0x04004A81 RID: 19073
		private Unlimited<uint> powerShellMaxDestructiveCmdlets;

		// Token: 0x04004A82 RID: 19074
		private Unlimited<uint> powerShellMaxDestructiveCmdletsTimePeriod;

		// Token: 0x04004A83 RID: 19075
		private Unlimited<uint> powerShellMaxCmdlets;

		// Token: 0x04004A84 RID: 19076
		private Unlimited<uint> powerShellMaxRunspaces;

		// Token: 0x04004A85 RID: 19077
		private Unlimited<uint> powerShellMaxTenantRunspaces;

		// Token: 0x04004A86 RID: 19078
		private Unlimited<uint> powerShellMaxRunspacesTimePeriod;

		// Token: 0x04004A87 RID: 19079
		private Unlimited<uint> pswsMaxConcurrency;

		// Token: 0x04004A88 RID: 19080
		private Unlimited<uint> pswsMaxRequest;

		// Token: 0x04004A89 RID: 19081
		private Unlimited<uint> pswsMaxRequestTimePeriod;

		// Token: 0x04004A8A RID: 19082
		private Unlimited<uint> rcaMaxConcurrency;

		// Token: 0x04004A8B RID: 19083
		private Unlimited<uint> rcaMaxBurst;

		// Token: 0x04004A8C RID: 19084
		private Unlimited<uint> rcaRechargeRate;

		// Token: 0x04004A8D RID: 19085
		private Unlimited<uint> rcaCutoffBalance;

		// Token: 0x04004A8E RID: 19086
		private Unlimited<uint> cpaMaxConcurrency;

		// Token: 0x04004A8F RID: 19087
		private Unlimited<uint> cpaMaxBurst;

		// Token: 0x04004A90 RID: 19088
		private Unlimited<uint> cpaRechargeRate;

		// Token: 0x04004A91 RID: 19089
		private Unlimited<uint> cpaCutoffBalance;

		// Token: 0x04004A92 RID: 19090
		private Unlimited<uint> messageRateLimit;

		// Token: 0x04004A93 RID: 19091
		private Unlimited<uint> recipientRateLimit;

		// Token: 0x04004A94 RID: 19092
		private Unlimited<uint> forwardeeLimit;

		// Token: 0x04004A95 RID: 19093
		private Unlimited<uint> discoveryMaxConcurrency;

		// Token: 0x04004A96 RID: 19094
		private Unlimited<uint> discoveryMaxMailboxes;

		// Token: 0x04004A97 RID: 19095
		private Unlimited<uint> discoveryMaxKeywords;

		// Token: 0x04004A98 RID: 19096
		private Unlimited<uint> discoveryMaxPreviewSearchMailboxes;

		// Token: 0x04004A99 RID: 19097
		private Unlimited<uint> discoveryMaxStatsSearchMailboxes;

		// Token: 0x04004A9A RID: 19098
		private Unlimited<uint> discoveryPreviewSearchResultsPageSize;

		// Token: 0x04004A9B RID: 19099
		private Unlimited<uint> discoveryMaxKeywordsPerPage;

		// Token: 0x04004A9C RID: 19100
		private Unlimited<uint> discoveryMaxRefinerResults;

		// Token: 0x04004A9D RID: 19101
		private Unlimited<uint> discoveryMaxSearchQueueDepth;

		// Token: 0x04004A9E RID: 19102
		private Unlimited<uint> discoverySearchTimeoutPeriod;

		// Token: 0x04004A9F RID: 19103
		private Unlimited<uint> pushNotificationMaxConcurrency;

		// Token: 0x04004AA0 RID: 19104
		private Unlimited<uint> pushNotificationMaxBurst;

		// Token: 0x04004AA1 RID: 19105
		private Unlimited<uint> pushNotificationRechargeRate;

		// Token: 0x04004AA2 RID: 19106
		private Unlimited<uint> pushNotificationCutoffBalance;

		// Token: 0x04004AA3 RID: 19107
		private Unlimited<uint> pushNotificationMaxBurstPerDevice;

		// Token: 0x04004AA4 RID: 19108
		private Unlimited<uint> pushNotificationRechargeRatePerDevice;

		// Token: 0x04004AA5 RID: 19109
		private Unlimited<uint> pushNotificationSamplingPeriodPerDevice;

		// Token: 0x04004AA6 RID: 19110
		private Unlimited<uint> encryptionSenderMaxConcurrency;

		// Token: 0x04004AA7 RID: 19111
		private Unlimited<uint> encryptionSenderMaxBurst;

		// Token: 0x04004AA8 RID: 19112
		private Unlimited<uint> encryptionSenderRechargeRate;

		// Token: 0x04004AA9 RID: 19113
		private Unlimited<uint> encryptionSenderCutoffBalance;

		// Token: 0x04004AAA RID: 19114
		private Unlimited<uint> encryptionRecipientMaxConcurrency;

		// Token: 0x04004AAB RID: 19115
		private Unlimited<uint> encryptionRecipientMaxBurst;

		// Token: 0x04004AAC RID: 19116
		private Unlimited<uint> encryptionRecipientRechargeRate;

		// Token: 0x04004AAD RID: 19117
		private Unlimited<uint> encryptionRecipientCutoffBalance;

		// Token: 0x04004AAE RID: 19118
		private Unlimited<uint> complianceMaxExpansionDGRecipients;

		// Token: 0x04004AAF RID: 19119
		private Unlimited<uint> complianceMaxExpansionNestedDGs;
	}
}
