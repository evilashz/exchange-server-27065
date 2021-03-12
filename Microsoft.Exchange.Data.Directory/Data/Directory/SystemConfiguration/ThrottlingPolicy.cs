using System;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005E6 RID: 1510
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class ThrottlingPolicy : ADConfigurationObject
	{
		// Token: 0x0600470E RID: 18190 RVA: 0x00107D20 File Offset: 0x00105F20
		internal static ThrottlingPolicy GetDefaultOrganizationEffectiveThrottlingPolicy()
		{
			ThrottlingPolicy throttlingPolicy = EffectiveThrottlingPolicy.ReadGlobalThrottlingPolicyFromAD();
			throttlingPolicy.ConvertToEffectiveThrottlingPolicy(false);
			return throttlingPolicy;
		}

		// Token: 0x17001743 RID: 5955
		// (get) Token: 0x0600470F RID: 18191 RVA: 0x00107D3B File Offset: 0x00105F3B
		private LegacyThrottlingPolicy LegacyThrottlingPolicy
		{
			get
			{
				if (this.legacyThrottlingPolicy == null)
				{
					this.legacyThrottlingPolicy = LegacyThrottlingPolicy.GetLegacyThrottlingPolicy(this);
				}
				return this.legacyThrottlingPolicy;
			}
		}

		// Token: 0x17001744 RID: 5956
		// (get) Token: 0x06004710 RID: 18192 RVA: 0x00107D57 File Offset: 0x00105F57
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x17001745 RID: 5957
		// (get) Token: 0x06004711 RID: 18193 RVA: 0x00107D5E File Offset: 0x00105F5E
		internal override bool IsShareable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001746 RID: 5958
		// (get) Token: 0x06004712 RID: 18194 RVA: 0x00107D61 File Offset: 0x00105F61
		internal override ADObjectSchema Schema
		{
			get
			{
				return ThrottlingPolicy.schema;
			}
		}

		// Token: 0x17001747 RID: 5959
		// (get) Token: 0x06004713 RID: 18195 RVA: 0x00107D68 File Offset: 0x00105F68
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ThrottlingPolicy.mostDerivedClass;
			}
		}

		// Token: 0x17001748 RID: 5960
		// (get) Token: 0x06004714 RID: 18196 RVA: 0x00107D6F File Offset: 0x00105F6F
		// (set) Token: 0x06004715 RID: 18197 RVA: 0x00107D81 File Offset: 0x00105F81
		public ThrottlingPolicyScopeType ThrottlingPolicyScope
		{
			get
			{
				return (ThrottlingPolicyScopeType)this[ThrottlingPolicySchema.ThrottlingPolicyScope];
			}
			internal set
			{
				this[ThrottlingPolicySchema.ThrottlingPolicyScope] = value;
			}
		}

		// Token: 0x17001749 RID: 5961
		// (get) Token: 0x06004716 RID: 18198 RVA: 0x00107D94 File Offset: 0x00105F94
		// (set) Token: 0x06004717 RID: 18199 RVA: 0x00107DA6 File Offset: 0x00105FA6
		public bool IsServiceAccount
		{
			get
			{
				return (bool)this[ThrottlingPolicySchema.IsServiceAccount];
			}
			internal set
			{
				this[ThrottlingPolicySchema.IsServiceAccount] = value;
			}
		}

		// Token: 0x1700174A RID: 5962
		// (get) Token: 0x06004718 RID: 18200 RVA: 0x00107DB9 File Offset: 0x00105FB9
		// (set) Token: 0x06004719 RID: 18201 RVA: 0x00107DCB File Offset: 0x00105FCB
		public Unlimited<uint>? AnonymousMaxConcurrency
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.AnonymousMaxConcurrency];
			}
			set
			{
				this[ThrottlingPolicySchema.AnonymousMaxConcurrency] = value;
			}
		}

		// Token: 0x1700174B RID: 5963
		// (get) Token: 0x0600471A RID: 18202 RVA: 0x00107DDE File Offset: 0x00105FDE
		// (set) Token: 0x0600471B RID: 18203 RVA: 0x00107DF0 File Offset: 0x00105FF0
		public Unlimited<uint>? AnonymousMaxBurst
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.AnonymousMaxBurst];
			}
			set
			{
				this[ThrottlingPolicySchema.AnonymousMaxBurst] = value;
			}
		}

		// Token: 0x1700174C RID: 5964
		// (get) Token: 0x0600471C RID: 18204 RVA: 0x00107E03 File Offset: 0x00106003
		// (set) Token: 0x0600471D RID: 18205 RVA: 0x00107E15 File Offset: 0x00106015
		public Unlimited<uint>? AnonymousRechargeRate
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.AnonymousRechargeRate];
			}
			set
			{
				this[ThrottlingPolicySchema.AnonymousRechargeRate] = value;
			}
		}

		// Token: 0x1700174D RID: 5965
		// (get) Token: 0x0600471E RID: 18206 RVA: 0x00107E28 File Offset: 0x00106028
		// (set) Token: 0x0600471F RID: 18207 RVA: 0x00107E3A File Offset: 0x0010603A
		public Unlimited<uint>? AnonymousCutoffBalance
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.AnonymousCutoffBalance];
			}
			set
			{
				this[ThrottlingPolicySchema.AnonymousCutoffBalance] = value;
			}
		}

		// Token: 0x1700174E RID: 5966
		// (get) Token: 0x06004720 RID: 18208 RVA: 0x00107E4D File Offset: 0x0010604D
		// (set) Token: 0x06004721 RID: 18209 RVA: 0x00107E5F File Offset: 0x0010605F
		public Unlimited<uint>? EasMaxConcurrency
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.EasMaxConcurrency];
			}
			set
			{
				this[ThrottlingPolicySchema.EasMaxConcurrency] = value;
			}
		}

		// Token: 0x1700174F RID: 5967
		// (get) Token: 0x06004722 RID: 18210 RVA: 0x00107E72 File Offset: 0x00106072
		// (set) Token: 0x06004723 RID: 18211 RVA: 0x00107E84 File Offset: 0x00106084
		public Unlimited<uint>? EasMaxBurst
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.EasMaxBurst];
			}
			set
			{
				this[ThrottlingPolicySchema.EasMaxBurst] = value;
			}
		}

		// Token: 0x17001750 RID: 5968
		// (get) Token: 0x06004724 RID: 18212 RVA: 0x00107E97 File Offset: 0x00106097
		// (set) Token: 0x06004725 RID: 18213 RVA: 0x00107EA9 File Offset: 0x001060A9
		public Unlimited<uint>? EasRechargeRate
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.EasRechargeRate];
			}
			set
			{
				this[ThrottlingPolicySchema.EasRechargeRate] = value;
			}
		}

		// Token: 0x17001751 RID: 5969
		// (get) Token: 0x06004726 RID: 18214 RVA: 0x00107EBC File Offset: 0x001060BC
		// (set) Token: 0x06004727 RID: 18215 RVA: 0x00107ECE File Offset: 0x001060CE
		public Unlimited<uint>? EasCutoffBalance
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.EasCutoffBalance];
			}
			set
			{
				this[ThrottlingPolicySchema.EasCutoffBalance] = value;
			}
		}

		// Token: 0x17001752 RID: 5970
		// (get) Token: 0x06004728 RID: 18216 RVA: 0x00107EE1 File Offset: 0x001060E1
		// (set) Token: 0x06004729 RID: 18217 RVA: 0x00107EF3 File Offset: 0x001060F3
		public Unlimited<uint>? EasMaxDevices
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.EasMaxDevices];
			}
			set
			{
				this[ThrottlingPolicySchema.EasMaxDevices] = value;
			}
		}

		// Token: 0x17001753 RID: 5971
		// (get) Token: 0x0600472A RID: 18218 RVA: 0x00107F06 File Offset: 0x00106106
		// (set) Token: 0x0600472B RID: 18219 RVA: 0x00107F18 File Offset: 0x00106118
		public Unlimited<uint>? EasMaxDeviceDeletesPerMonth
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.EasMaxDeviceDeletesPerMonth];
			}
			set
			{
				this[ThrottlingPolicySchema.EasMaxDeviceDeletesPerMonth] = value;
			}
		}

		// Token: 0x17001754 RID: 5972
		// (get) Token: 0x0600472C RID: 18220 RVA: 0x00107F2B File Offset: 0x0010612B
		// (set) Token: 0x0600472D RID: 18221 RVA: 0x00107F3D File Offset: 0x0010613D
		public Unlimited<uint>? EasMaxInactivityForDeviceCleanup
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.EasMaxInactivityForDeviceCleanup];
			}
			set
			{
				this[ThrottlingPolicySchema.EasMaxInactivityForDeviceCleanup] = value;
			}
		}

		// Token: 0x17001755 RID: 5973
		// (get) Token: 0x0600472E RID: 18222 RVA: 0x00107F50 File Offset: 0x00106150
		// (set) Token: 0x0600472F RID: 18223 RVA: 0x00107F62 File Offset: 0x00106162
		public Unlimited<uint>? EwsMaxConcurrency
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.EwsMaxConcurrency];
			}
			set
			{
				this[ThrottlingPolicySchema.EwsMaxConcurrency] = value;
			}
		}

		// Token: 0x17001756 RID: 5974
		// (get) Token: 0x06004730 RID: 18224 RVA: 0x00107F75 File Offset: 0x00106175
		// (set) Token: 0x06004731 RID: 18225 RVA: 0x00107F87 File Offset: 0x00106187
		public Unlimited<uint>? EwsMaxBurst
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.EwsMaxBurst];
			}
			set
			{
				this[ThrottlingPolicySchema.EwsMaxBurst] = value;
			}
		}

		// Token: 0x17001757 RID: 5975
		// (get) Token: 0x06004732 RID: 18226 RVA: 0x00107F9A File Offset: 0x0010619A
		// (set) Token: 0x06004733 RID: 18227 RVA: 0x00107FAC File Offset: 0x001061AC
		public Unlimited<uint>? EwsRechargeRate
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.EwsRechargeRate];
			}
			set
			{
				this[ThrottlingPolicySchema.EwsRechargeRate] = value;
			}
		}

		// Token: 0x17001758 RID: 5976
		// (get) Token: 0x06004734 RID: 18228 RVA: 0x00107FBF File Offset: 0x001061BF
		// (set) Token: 0x06004735 RID: 18229 RVA: 0x00107FD1 File Offset: 0x001061D1
		public Unlimited<uint>? EwsCutoffBalance
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.EwsCutoffBalance];
			}
			set
			{
				this[ThrottlingPolicySchema.EwsCutoffBalance] = value;
			}
		}

		// Token: 0x17001759 RID: 5977
		// (get) Token: 0x06004736 RID: 18230 RVA: 0x00107FE4 File Offset: 0x001061E4
		// (set) Token: 0x06004737 RID: 18231 RVA: 0x00107FF6 File Offset: 0x001061F6
		public Unlimited<uint>? EwsMaxSubscriptions
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.EwsMaxSubscriptions];
			}
			set
			{
				this[ThrottlingPolicySchema.EwsMaxSubscriptions] = value;
			}
		}

		// Token: 0x1700175A RID: 5978
		// (get) Token: 0x06004738 RID: 18232 RVA: 0x00108009 File Offset: 0x00106209
		// (set) Token: 0x06004739 RID: 18233 RVA: 0x0010801B File Offset: 0x0010621B
		public Unlimited<uint>? ImapMaxConcurrency
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.ImapMaxConcurrency];
			}
			set
			{
				this[ThrottlingPolicySchema.ImapMaxConcurrency] = value;
			}
		}

		// Token: 0x1700175B RID: 5979
		// (get) Token: 0x0600473A RID: 18234 RVA: 0x0010802E File Offset: 0x0010622E
		// (set) Token: 0x0600473B RID: 18235 RVA: 0x00108040 File Offset: 0x00106240
		public Unlimited<uint>? ImapMaxBurst
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.ImapMaxBurst];
			}
			set
			{
				this[ThrottlingPolicySchema.ImapMaxBurst] = value;
			}
		}

		// Token: 0x1700175C RID: 5980
		// (get) Token: 0x0600473C RID: 18236 RVA: 0x00108053 File Offset: 0x00106253
		// (set) Token: 0x0600473D RID: 18237 RVA: 0x00108065 File Offset: 0x00106265
		public Unlimited<uint>? ImapRechargeRate
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.ImapRechargeRate];
			}
			set
			{
				this[ThrottlingPolicySchema.ImapRechargeRate] = value;
			}
		}

		// Token: 0x1700175D RID: 5981
		// (get) Token: 0x0600473E RID: 18238 RVA: 0x00108078 File Offset: 0x00106278
		// (set) Token: 0x0600473F RID: 18239 RVA: 0x0010808A File Offset: 0x0010628A
		public Unlimited<uint>? ImapCutoffBalance
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.ImapCutoffBalance];
			}
			set
			{
				this[ThrottlingPolicySchema.ImapCutoffBalance] = value;
			}
		}

		// Token: 0x1700175E RID: 5982
		// (get) Token: 0x06004740 RID: 18240 RVA: 0x0010809D File Offset: 0x0010629D
		// (set) Token: 0x06004741 RID: 18241 RVA: 0x001080AF File Offset: 0x001062AF
		public Unlimited<uint>? OutlookServiceMaxConcurrency
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.OutlookServiceMaxConcurrency];
			}
			set
			{
				this[ThrottlingPolicySchema.OutlookServiceMaxConcurrency] = value;
			}
		}

		// Token: 0x1700175F RID: 5983
		// (get) Token: 0x06004742 RID: 18242 RVA: 0x001080C2 File Offset: 0x001062C2
		// (set) Token: 0x06004743 RID: 18243 RVA: 0x001080D4 File Offset: 0x001062D4
		public Unlimited<uint>? OutlookServiceMaxBurst
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.OutlookServiceMaxBurst];
			}
			set
			{
				this[ThrottlingPolicySchema.OutlookServiceMaxBurst] = value;
			}
		}

		// Token: 0x17001760 RID: 5984
		// (get) Token: 0x06004744 RID: 18244 RVA: 0x001080E7 File Offset: 0x001062E7
		// (set) Token: 0x06004745 RID: 18245 RVA: 0x001080F9 File Offset: 0x001062F9
		public Unlimited<uint>? OutlookServiceRechargeRate
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.OutlookServiceRechargeRate];
			}
			set
			{
				this[ThrottlingPolicySchema.OutlookServiceRechargeRate] = value;
			}
		}

		// Token: 0x17001761 RID: 5985
		// (get) Token: 0x06004746 RID: 18246 RVA: 0x0010810C File Offset: 0x0010630C
		// (set) Token: 0x06004747 RID: 18247 RVA: 0x0010811E File Offset: 0x0010631E
		public Unlimited<uint>? OutlookServiceCutoffBalance
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.OutlookServiceCutoffBalance];
			}
			set
			{
				this[ThrottlingPolicySchema.OutlookServiceCutoffBalance] = value;
			}
		}

		// Token: 0x17001762 RID: 5986
		// (get) Token: 0x06004748 RID: 18248 RVA: 0x00108131 File Offset: 0x00106331
		// (set) Token: 0x06004749 RID: 18249 RVA: 0x00108143 File Offset: 0x00106343
		public Unlimited<uint>? OutlookServiceMaxSubscriptions
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.OutlookServiceMaxSubscriptions];
			}
			set
			{
				this[ThrottlingPolicySchema.OutlookServiceMaxSubscriptions] = value;
			}
		}

		// Token: 0x17001763 RID: 5987
		// (get) Token: 0x0600474A RID: 18250 RVA: 0x00108156 File Offset: 0x00106356
		// (set) Token: 0x0600474B RID: 18251 RVA: 0x00108168 File Offset: 0x00106368
		public Unlimited<uint>? OutlookServiceMaxSocketConnectionsPerDevice
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.OutlookServiceMaxSocketConnectionsPerDevice];
			}
			set
			{
				this[ThrottlingPolicySchema.OutlookServiceMaxSocketConnectionsPerDevice] = value;
			}
		}

		// Token: 0x17001764 RID: 5988
		// (get) Token: 0x0600474C RID: 18252 RVA: 0x0010817B File Offset: 0x0010637B
		// (set) Token: 0x0600474D RID: 18253 RVA: 0x0010818D File Offset: 0x0010638D
		public Unlimited<uint>? OutlookServiceMaxSocketConnectionsPerUser
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.OutlookServiceMaxSocketConnectionsPerUser];
			}
			set
			{
				this[ThrottlingPolicySchema.OutlookServiceMaxSocketConnectionsPerUser] = value;
			}
		}

		// Token: 0x17001765 RID: 5989
		// (get) Token: 0x0600474E RID: 18254 RVA: 0x001081A0 File Offset: 0x001063A0
		// (set) Token: 0x0600474F RID: 18255 RVA: 0x001081B2 File Offset: 0x001063B2
		public Unlimited<uint>? OwaMaxConcurrency
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.OwaMaxConcurrency];
			}
			set
			{
				this[ThrottlingPolicySchema.OwaMaxConcurrency] = value;
			}
		}

		// Token: 0x17001766 RID: 5990
		// (get) Token: 0x06004750 RID: 18256 RVA: 0x001081C5 File Offset: 0x001063C5
		// (set) Token: 0x06004751 RID: 18257 RVA: 0x001081D7 File Offset: 0x001063D7
		public Unlimited<uint>? OwaMaxBurst
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.OwaMaxBurst];
			}
			set
			{
				this[ThrottlingPolicySchema.OwaMaxBurst] = value;
			}
		}

		// Token: 0x17001767 RID: 5991
		// (get) Token: 0x06004752 RID: 18258 RVA: 0x001081EA File Offset: 0x001063EA
		// (set) Token: 0x06004753 RID: 18259 RVA: 0x001081FC File Offset: 0x001063FC
		public Unlimited<uint>? OwaRechargeRate
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.OwaRechargeRate];
			}
			set
			{
				this[ThrottlingPolicySchema.OwaRechargeRate] = value;
			}
		}

		// Token: 0x17001768 RID: 5992
		// (get) Token: 0x06004754 RID: 18260 RVA: 0x0010820F File Offset: 0x0010640F
		// (set) Token: 0x06004755 RID: 18261 RVA: 0x00108221 File Offset: 0x00106421
		public Unlimited<uint>? OwaCutoffBalance
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.OwaCutoffBalance];
			}
			set
			{
				this[ThrottlingPolicySchema.OwaCutoffBalance] = value;
			}
		}

		// Token: 0x17001769 RID: 5993
		// (get) Token: 0x06004756 RID: 18262 RVA: 0x00108234 File Offset: 0x00106434
		// (set) Token: 0x06004757 RID: 18263 RVA: 0x00108246 File Offset: 0x00106446
		public Unlimited<uint>? OwaVoiceMaxConcurrency
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.OwaVoiceMaxConcurrency];
			}
			set
			{
				this[ThrottlingPolicySchema.OwaVoiceMaxConcurrency] = value;
			}
		}

		// Token: 0x1700176A RID: 5994
		// (get) Token: 0x06004758 RID: 18264 RVA: 0x00108259 File Offset: 0x00106459
		// (set) Token: 0x06004759 RID: 18265 RVA: 0x0010826B File Offset: 0x0010646B
		public Unlimited<uint>? OwaVoiceMaxBurst
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.OwaVoiceMaxBurst];
			}
			set
			{
				this[ThrottlingPolicySchema.OwaVoiceMaxBurst] = value;
			}
		}

		// Token: 0x1700176B RID: 5995
		// (get) Token: 0x0600475A RID: 18266 RVA: 0x0010827E File Offset: 0x0010647E
		// (set) Token: 0x0600475B RID: 18267 RVA: 0x00108290 File Offset: 0x00106490
		public Unlimited<uint>? OwaVoiceRechargeRate
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.OwaVoiceRechargeRate];
			}
			set
			{
				this[ThrottlingPolicySchema.OwaVoiceRechargeRate] = value;
			}
		}

		// Token: 0x1700176C RID: 5996
		// (get) Token: 0x0600475C RID: 18268 RVA: 0x001082A3 File Offset: 0x001064A3
		// (set) Token: 0x0600475D RID: 18269 RVA: 0x001082B5 File Offset: 0x001064B5
		public Unlimited<uint>? OwaVoiceCutoffBalance
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.OwaVoiceCutoffBalance];
			}
			set
			{
				this[ThrottlingPolicySchema.OwaVoiceCutoffBalance] = value;
			}
		}

		// Token: 0x1700176D RID: 5997
		// (get) Token: 0x0600475E RID: 18270 RVA: 0x001082C8 File Offset: 0x001064C8
		// (set) Token: 0x0600475F RID: 18271 RVA: 0x001082DA File Offset: 0x001064DA
		public Unlimited<uint>? PopMaxConcurrency
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PopMaxConcurrency];
			}
			set
			{
				this[ThrottlingPolicySchema.PopMaxConcurrency] = value;
			}
		}

		// Token: 0x1700176E RID: 5998
		// (get) Token: 0x06004760 RID: 18272 RVA: 0x001082ED File Offset: 0x001064ED
		// (set) Token: 0x06004761 RID: 18273 RVA: 0x001082FF File Offset: 0x001064FF
		public Unlimited<uint>? PopMaxBurst
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PopMaxBurst];
			}
			set
			{
				this[ThrottlingPolicySchema.PopMaxBurst] = value;
			}
		}

		// Token: 0x1700176F RID: 5999
		// (get) Token: 0x06004762 RID: 18274 RVA: 0x00108312 File Offset: 0x00106512
		// (set) Token: 0x06004763 RID: 18275 RVA: 0x00108324 File Offset: 0x00106524
		public Unlimited<uint>? PopRechargeRate
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PopRechargeRate];
			}
			set
			{
				this[ThrottlingPolicySchema.PopRechargeRate] = value;
			}
		}

		// Token: 0x17001770 RID: 6000
		// (get) Token: 0x06004764 RID: 18276 RVA: 0x00108337 File Offset: 0x00106537
		// (set) Token: 0x06004765 RID: 18277 RVA: 0x00108349 File Offset: 0x00106549
		public Unlimited<uint>? PopCutoffBalance
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PopCutoffBalance];
			}
			set
			{
				this[ThrottlingPolicySchema.PopCutoffBalance] = value;
			}
		}

		// Token: 0x17001771 RID: 6001
		// (get) Token: 0x06004766 RID: 18278 RVA: 0x0010835C File Offset: 0x0010655C
		// (set) Token: 0x06004767 RID: 18279 RVA: 0x0010836E File Offset: 0x0010656E
		public Unlimited<uint>? PowerShellMaxConcurrency
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PowerShellMaxConcurrency];
			}
			set
			{
				this[ThrottlingPolicySchema.PowerShellMaxConcurrency] = value;
			}
		}

		// Token: 0x17001772 RID: 6002
		// (get) Token: 0x06004768 RID: 18280 RVA: 0x00108381 File Offset: 0x00106581
		// (set) Token: 0x06004769 RID: 18281 RVA: 0x00108393 File Offset: 0x00106593
		public Unlimited<uint>? PowerShellMaxBurst
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PowerShellMaxBurst];
			}
			set
			{
				this[ThrottlingPolicySchema.PowerShellMaxBurst] = value;
			}
		}

		// Token: 0x17001773 RID: 6003
		// (get) Token: 0x0600476A RID: 18282 RVA: 0x001083A6 File Offset: 0x001065A6
		// (set) Token: 0x0600476B RID: 18283 RVA: 0x001083B8 File Offset: 0x001065B8
		public Unlimited<uint>? PowerShellRechargeRate
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PowerShellRechargeRate];
			}
			set
			{
				this[ThrottlingPolicySchema.PowerShellRechargeRate] = value;
			}
		}

		// Token: 0x17001774 RID: 6004
		// (get) Token: 0x0600476C RID: 18284 RVA: 0x001083CB File Offset: 0x001065CB
		// (set) Token: 0x0600476D RID: 18285 RVA: 0x001083DD File Offset: 0x001065DD
		public Unlimited<uint>? PowerShellCutoffBalance
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PowerShellCutoffBalance];
			}
			set
			{
				this[ThrottlingPolicySchema.PowerShellCutoffBalance] = value;
			}
		}

		// Token: 0x17001775 RID: 6005
		// (get) Token: 0x0600476E RID: 18286 RVA: 0x001083F0 File Offset: 0x001065F0
		// (set) Token: 0x0600476F RID: 18287 RVA: 0x0010841F File Offset: 0x0010661F
		public Unlimited<uint>? PowerShellMaxTenantConcurrency
		{
			get
			{
				if (this.ThrottlingPolicyScope == ThrottlingPolicyScopeType.Regular)
				{
					return null;
				}
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PowerShellMaxTenantConcurrency];
			}
			set
			{
				this[ThrottlingPolicySchema.PowerShellMaxTenantConcurrency] = value;
			}
		}

		// Token: 0x17001776 RID: 6006
		// (get) Token: 0x06004770 RID: 18288 RVA: 0x00108432 File Offset: 0x00106632
		// (set) Token: 0x06004771 RID: 18289 RVA: 0x00108444 File Offset: 0x00106644
		public Unlimited<uint>? PowerShellMaxOperations
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PowerShellMaxOperations];
			}
			set
			{
				this[ThrottlingPolicySchema.PowerShellMaxOperations] = value;
			}
		}

		// Token: 0x17001777 RID: 6007
		// (get) Token: 0x06004772 RID: 18290 RVA: 0x00108457 File Offset: 0x00106657
		// (set) Token: 0x06004773 RID: 18291 RVA: 0x00108469 File Offset: 0x00106669
		public Unlimited<uint>? PowerShellMaxCmdletsTimePeriod
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PowerShellMaxCmdletsTimePeriod];
			}
			set
			{
				this[ThrottlingPolicySchema.PowerShellMaxCmdletsTimePeriod] = value;
			}
		}

		// Token: 0x17001778 RID: 6008
		// (get) Token: 0x06004774 RID: 18292 RVA: 0x0010847C File Offset: 0x0010667C
		// (set) Token: 0x06004775 RID: 18293 RVA: 0x0010848E File Offset: 0x0010668E
		public Unlimited<uint>? ExchangeMaxCmdlets
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.ExchangeMaxCmdlets];
			}
			set
			{
				this[ThrottlingPolicySchema.ExchangeMaxCmdlets] = value;
			}
		}

		// Token: 0x17001779 RID: 6009
		// (get) Token: 0x06004776 RID: 18294 RVA: 0x001084A1 File Offset: 0x001066A1
		// (set) Token: 0x06004777 RID: 18295 RVA: 0x001084B3 File Offset: 0x001066B3
		public Unlimited<uint>? PowerShellMaxCmdletQueueDepth
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PowerShellMaxCmdletQueueDepth];
			}
			set
			{
				this[ThrottlingPolicySchema.PowerShellMaxCmdletQueueDepth] = value;
			}
		}

		// Token: 0x1700177A RID: 6010
		// (get) Token: 0x06004778 RID: 18296 RVA: 0x001084C6 File Offset: 0x001066C6
		// (set) Token: 0x06004779 RID: 18297 RVA: 0x001084D8 File Offset: 0x001066D8
		public Unlimited<uint>? PowerShellMaxDestructiveCmdlets
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PowerShellMaxDestructiveCmdlets];
			}
			set
			{
				this[ThrottlingPolicySchema.PowerShellMaxDestructiveCmdlets] = value;
			}
		}

		// Token: 0x1700177B RID: 6011
		// (get) Token: 0x0600477A RID: 18298 RVA: 0x001084EB File Offset: 0x001066EB
		// (set) Token: 0x0600477B RID: 18299 RVA: 0x001084FD File Offset: 0x001066FD
		public Unlimited<uint>? PowerShellMaxDestructiveCmdletsTimePeriod
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PowerShellMaxDestructiveCmdletsTimePeriod];
			}
			set
			{
				this[ThrottlingPolicySchema.PowerShellMaxDestructiveCmdletsTimePeriod] = value;
			}
		}

		// Token: 0x1700177C RID: 6012
		// (get) Token: 0x0600477C RID: 18300 RVA: 0x00108510 File Offset: 0x00106710
		// (set) Token: 0x0600477D RID: 18301 RVA: 0x00108522 File Offset: 0x00106722
		public Unlimited<uint>? PowerShellMaxCmdlets
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PowerShellMaxCmdlets];
			}
			set
			{
				this[ThrottlingPolicySchema.PowerShellMaxCmdlets] = value;
			}
		}

		// Token: 0x1700177D RID: 6013
		// (get) Token: 0x0600477E RID: 18302 RVA: 0x00108535 File Offset: 0x00106735
		// (set) Token: 0x0600477F RID: 18303 RVA: 0x00108547 File Offset: 0x00106747
		public Unlimited<uint>? PowerShellMaxRunspaces
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PowerShellMaxRunspaces];
			}
			set
			{
				this[ThrottlingPolicySchema.PowerShellMaxRunspaces] = value;
			}
		}

		// Token: 0x1700177E RID: 6014
		// (get) Token: 0x06004780 RID: 18304 RVA: 0x0010855C File Offset: 0x0010675C
		// (set) Token: 0x06004781 RID: 18305 RVA: 0x0010858B File Offset: 0x0010678B
		public Unlimited<uint>? PowerShellMaxTenantRunspaces
		{
			get
			{
				if (this.ThrottlingPolicyScope == ThrottlingPolicyScopeType.Regular)
				{
					return null;
				}
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PowerShellMaxTenantRunspaces];
			}
			set
			{
				this[ThrottlingPolicySchema.PowerShellMaxTenantRunspaces] = value;
			}
		}

		// Token: 0x1700177F RID: 6015
		// (get) Token: 0x06004782 RID: 18306 RVA: 0x0010859E File Offset: 0x0010679E
		// (set) Token: 0x06004783 RID: 18307 RVA: 0x001085B0 File Offset: 0x001067B0
		public Unlimited<uint>? PowerShellMaxRunspacesTimePeriod
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PowerShellMaxRunspacesTimePeriod];
			}
			set
			{
				this[ThrottlingPolicySchema.PowerShellMaxRunspacesTimePeriod] = value;
			}
		}

		// Token: 0x17001780 RID: 6016
		// (get) Token: 0x06004784 RID: 18308 RVA: 0x001085C3 File Offset: 0x001067C3
		// (set) Token: 0x06004785 RID: 18309 RVA: 0x001085D5 File Offset: 0x001067D5
		public Unlimited<uint>? PswsMaxConcurrency
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PswsMaxConcurrency];
			}
			set
			{
				this[ThrottlingPolicySchema.PswsMaxConcurrency] = value;
			}
		}

		// Token: 0x17001781 RID: 6017
		// (get) Token: 0x06004786 RID: 18310 RVA: 0x001085E8 File Offset: 0x001067E8
		// (set) Token: 0x06004787 RID: 18311 RVA: 0x001085FA File Offset: 0x001067FA
		public Unlimited<uint>? PswsMaxRequest
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PswsMaxRequest];
			}
			set
			{
				this[ThrottlingPolicySchema.PswsMaxRequest] = value;
			}
		}

		// Token: 0x17001782 RID: 6018
		// (get) Token: 0x06004788 RID: 18312 RVA: 0x0010860D File Offset: 0x0010680D
		// (set) Token: 0x06004789 RID: 18313 RVA: 0x0010861F File Offset: 0x0010681F
		public Unlimited<uint>? PswsMaxRequestTimePeriod
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PswsMaxRequestTimePeriod];
			}
			set
			{
				this[ThrottlingPolicySchema.PswsMaxRequestTimePeriod] = value;
			}
		}

		// Token: 0x17001783 RID: 6019
		// (get) Token: 0x0600478A RID: 18314 RVA: 0x00108632 File Offset: 0x00106832
		// (set) Token: 0x0600478B RID: 18315 RVA: 0x00108644 File Offset: 0x00106844
		public Unlimited<uint>? RcaMaxConcurrency
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.RcaMaxConcurrency];
			}
			set
			{
				this[ThrottlingPolicySchema.RcaMaxConcurrency] = value;
			}
		}

		// Token: 0x17001784 RID: 6020
		// (get) Token: 0x0600478C RID: 18316 RVA: 0x00108657 File Offset: 0x00106857
		// (set) Token: 0x0600478D RID: 18317 RVA: 0x00108669 File Offset: 0x00106869
		public Unlimited<uint>? RcaMaxBurst
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.RcaMaxBurst];
			}
			set
			{
				this[ThrottlingPolicySchema.RcaMaxBurst] = value;
			}
		}

		// Token: 0x17001785 RID: 6021
		// (get) Token: 0x0600478E RID: 18318 RVA: 0x0010867C File Offset: 0x0010687C
		// (set) Token: 0x0600478F RID: 18319 RVA: 0x0010868E File Offset: 0x0010688E
		public Unlimited<uint>? RcaRechargeRate
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.RcaRechargeRate];
			}
			set
			{
				this[ThrottlingPolicySchema.RcaRechargeRate] = value;
			}
		}

		// Token: 0x17001786 RID: 6022
		// (get) Token: 0x06004790 RID: 18320 RVA: 0x001086A1 File Offset: 0x001068A1
		// (set) Token: 0x06004791 RID: 18321 RVA: 0x001086B3 File Offset: 0x001068B3
		public Unlimited<uint>? RcaCutoffBalance
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.RcaCutoffBalance];
			}
			set
			{
				this[ThrottlingPolicySchema.RcaCutoffBalance] = value;
			}
		}

		// Token: 0x17001787 RID: 6023
		// (get) Token: 0x06004792 RID: 18322 RVA: 0x001086C6 File Offset: 0x001068C6
		// (set) Token: 0x06004793 RID: 18323 RVA: 0x001086D8 File Offset: 0x001068D8
		public Unlimited<uint>? CpaMaxConcurrency
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.CpaMaxConcurrency];
			}
			set
			{
				this[ThrottlingPolicySchema.CpaMaxConcurrency] = value;
			}
		}

		// Token: 0x17001788 RID: 6024
		// (get) Token: 0x06004794 RID: 18324 RVA: 0x001086EB File Offset: 0x001068EB
		// (set) Token: 0x06004795 RID: 18325 RVA: 0x001086FD File Offset: 0x001068FD
		public Unlimited<uint>? CpaMaxBurst
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.CpaMaxBurst];
			}
			set
			{
				this[ThrottlingPolicySchema.CpaMaxBurst] = value;
			}
		}

		// Token: 0x17001789 RID: 6025
		// (get) Token: 0x06004796 RID: 18326 RVA: 0x00108710 File Offset: 0x00106910
		// (set) Token: 0x06004797 RID: 18327 RVA: 0x00108722 File Offset: 0x00106922
		public Unlimited<uint>? CpaRechargeRate
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.CpaRechargeRate];
			}
			set
			{
				this[ThrottlingPolicySchema.CpaRechargeRate] = value;
			}
		}

		// Token: 0x1700178A RID: 6026
		// (get) Token: 0x06004798 RID: 18328 RVA: 0x00108735 File Offset: 0x00106935
		// (set) Token: 0x06004799 RID: 18329 RVA: 0x00108747 File Offset: 0x00106947
		public Unlimited<uint>? CpaCutoffBalance
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.CpaCutoffBalance];
			}
			set
			{
				this[ThrottlingPolicySchema.CpaCutoffBalance] = value;
			}
		}

		// Token: 0x1700178B RID: 6027
		// (get) Token: 0x0600479A RID: 18330 RVA: 0x0010875A File Offset: 0x0010695A
		// (set) Token: 0x0600479B RID: 18331 RVA: 0x0010876C File Offset: 0x0010696C
		public Unlimited<uint>? MessageRateLimit
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.MessageRateLimit];
			}
			set
			{
				this[ThrottlingPolicySchema.MessageRateLimit] = value;
			}
		}

		// Token: 0x1700178C RID: 6028
		// (get) Token: 0x0600479C RID: 18332 RVA: 0x0010877F File Offset: 0x0010697F
		// (set) Token: 0x0600479D RID: 18333 RVA: 0x00108791 File Offset: 0x00106991
		public Unlimited<uint>? RecipientRateLimit
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.RecipientRateLimit];
			}
			set
			{
				this[ThrottlingPolicySchema.RecipientRateLimit] = value;
			}
		}

		// Token: 0x1700178D RID: 6029
		// (get) Token: 0x0600479E RID: 18334 RVA: 0x001087A4 File Offset: 0x001069A4
		// (set) Token: 0x0600479F RID: 18335 RVA: 0x001087B6 File Offset: 0x001069B6
		public Unlimited<uint>? ForwardeeLimit
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.ForwardeeLimit];
			}
			set
			{
				this[ThrottlingPolicySchema.ForwardeeLimit] = value;
			}
		}

		// Token: 0x1700178E RID: 6030
		// (get) Token: 0x060047A0 RID: 18336 RVA: 0x001087C9 File Offset: 0x001069C9
		// (set) Token: 0x060047A1 RID: 18337 RVA: 0x001087DB File Offset: 0x001069DB
		public Unlimited<uint>? DiscoveryMaxConcurrency
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.DiscoveryMaxConcurrency];
			}
			set
			{
				this[ThrottlingPolicySchema.DiscoveryMaxConcurrency] = value;
			}
		}

		// Token: 0x1700178F RID: 6031
		// (get) Token: 0x060047A2 RID: 18338 RVA: 0x001087EE File Offset: 0x001069EE
		// (set) Token: 0x060047A3 RID: 18339 RVA: 0x00108800 File Offset: 0x00106A00
		public Unlimited<uint>? DiscoveryMaxMailboxes
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.DiscoveryMaxMailboxes];
			}
			set
			{
				this[ThrottlingPolicySchema.DiscoveryMaxMailboxes] = value;
			}
		}

		// Token: 0x17001790 RID: 6032
		// (get) Token: 0x060047A4 RID: 18340 RVA: 0x00108813 File Offset: 0x00106A13
		// (set) Token: 0x060047A5 RID: 18341 RVA: 0x00108825 File Offset: 0x00106A25
		public Unlimited<uint>? DiscoveryMaxKeywords
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.DiscoveryMaxKeywords];
			}
			set
			{
				this[ThrottlingPolicySchema.DiscoveryMaxKeywords] = value;
			}
		}

		// Token: 0x17001791 RID: 6033
		// (get) Token: 0x060047A6 RID: 18342 RVA: 0x00108838 File Offset: 0x00106A38
		// (set) Token: 0x060047A7 RID: 18343 RVA: 0x0010884A File Offset: 0x00106A4A
		public Unlimited<uint>? DiscoveryMaxPreviewSearchMailboxes
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.DiscoveryMaxPreviewSearchMailboxes];
			}
			set
			{
				this[ThrottlingPolicySchema.DiscoveryMaxPreviewSearchMailboxes] = value;
			}
		}

		// Token: 0x17001792 RID: 6034
		// (get) Token: 0x060047A8 RID: 18344 RVA: 0x0010885D File Offset: 0x00106A5D
		// (set) Token: 0x060047A9 RID: 18345 RVA: 0x0010886F File Offset: 0x00106A6F
		public Unlimited<uint>? DiscoveryMaxStatsSearchMailboxes
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.DiscoveryMaxStatsSearchMailboxes];
			}
			set
			{
				this[ThrottlingPolicySchema.DiscoveryMaxStatsSearchMailboxes] = value;
			}
		}

		// Token: 0x17001793 RID: 6035
		// (get) Token: 0x060047AA RID: 18346 RVA: 0x00108882 File Offset: 0x00106A82
		// (set) Token: 0x060047AB RID: 18347 RVA: 0x00108894 File Offset: 0x00106A94
		public Unlimited<uint>? DiscoveryPreviewSearchResultsPageSize
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.DiscoveryPreviewSearchResultsPageSize];
			}
			set
			{
				this[ThrottlingPolicySchema.DiscoveryPreviewSearchResultsPageSize] = value;
			}
		}

		// Token: 0x17001794 RID: 6036
		// (get) Token: 0x060047AC RID: 18348 RVA: 0x001088A7 File Offset: 0x00106AA7
		// (set) Token: 0x060047AD RID: 18349 RVA: 0x001088B9 File Offset: 0x00106AB9
		public Unlimited<uint>? DiscoveryMaxKeywordsPerPage
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.DiscoveryMaxKeywordsPerPage];
			}
			set
			{
				this[ThrottlingPolicySchema.DiscoveryMaxKeywordsPerPage] = value;
			}
		}

		// Token: 0x17001795 RID: 6037
		// (get) Token: 0x060047AE RID: 18350 RVA: 0x001088CC File Offset: 0x00106ACC
		// (set) Token: 0x060047AF RID: 18351 RVA: 0x001088DE File Offset: 0x00106ADE
		public Unlimited<uint>? DiscoveryMaxRefinerResults
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.DiscoveryMaxRefinerResults];
			}
			set
			{
				this[ThrottlingPolicySchema.DiscoveryMaxRefinerResults] = value;
			}
		}

		// Token: 0x17001796 RID: 6038
		// (get) Token: 0x060047B0 RID: 18352 RVA: 0x001088F1 File Offset: 0x00106AF1
		// (set) Token: 0x060047B1 RID: 18353 RVA: 0x00108903 File Offset: 0x00106B03
		public Unlimited<uint>? DiscoveryMaxSearchQueueDepth
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.DiscoveryMaxSearchQueueDepth];
			}
			set
			{
				this[ThrottlingPolicySchema.DiscoveryMaxSearchQueueDepth] = value;
			}
		}

		// Token: 0x17001797 RID: 6039
		// (get) Token: 0x060047B2 RID: 18354 RVA: 0x00108916 File Offset: 0x00106B16
		// (set) Token: 0x060047B3 RID: 18355 RVA: 0x00108928 File Offset: 0x00106B28
		public Unlimited<uint>? DiscoverySearchTimeoutPeriod
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.DiscoverySearchTimeoutPeriod];
			}
			set
			{
				this[ThrottlingPolicySchema.DiscoverySearchTimeoutPeriod] = value;
			}
		}

		// Token: 0x17001798 RID: 6040
		// (get) Token: 0x060047B4 RID: 18356 RVA: 0x0010893B File Offset: 0x00106B3B
		// (set) Token: 0x060047B5 RID: 18357 RVA: 0x0010894D File Offset: 0x00106B4D
		public Unlimited<uint>? PushNotificationMaxConcurrency
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PushNotificationMaxConcurrency];
			}
			set
			{
				this[ThrottlingPolicySchema.PushNotificationMaxConcurrency] = value;
			}
		}

		// Token: 0x17001799 RID: 6041
		// (get) Token: 0x060047B6 RID: 18358 RVA: 0x00108960 File Offset: 0x00106B60
		// (set) Token: 0x060047B7 RID: 18359 RVA: 0x00108972 File Offset: 0x00106B72
		public Unlimited<uint>? PushNotificationMaxBurst
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PushNotificationMaxBurst];
			}
			set
			{
				this[ThrottlingPolicySchema.PushNotificationMaxBurst] = value;
			}
		}

		// Token: 0x1700179A RID: 6042
		// (get) Token: 0x060047B8 RID: 18360 RVA: 0x00108985 File Offset: 0x00106B85
		// (set) Token: 0x060047B9 RID: 18361 RVA: 0x00108997 File Offset: 0x00106B97
		public Unlimited<uint>? PushNotificationRechargeRate
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PushNotificationRechargeRate];
			}
			set
			{
				this[ThrottlingPolicySchema.PushNotificationRechargeRate] = value;
			}
		}

		// Token: 0x1700179B RID: 6043
		// (get) Token: 0x060047BA RID: 18362 RVA: 0x001089AA File Offset: 0x00106BAA
		// (set) Token: 0x060047BB RID: 18363 RVA: 0x001089BC File Offset: 0x00106BBC
		public Unlimited<uint>? PushNotificationCutoffBalance
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PushNotificationCutoffBalance];
			}
			set
			{
				this[ThrottlingPolicySchema.PushNotificationCutoffBalance] = value;
			}
		}

		// Token: 0x1700179C RID: 6044
		// (get) Token: 0x060047BC RID: 18364 RVA: 0x001089CF File Offset: 0x00106BCF
		// (set) Token: 0x060047BD RID: 18365 RVA: 0x001089E1 File Offset: 0x00106BE1
		public Unlimited<uint>? PushNotificationMaxBurstPerDevice
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PushNotificationMaxBurstPerDevice];
			}
			set
			{
				this[ThrottlingPolicySchema.PushNotificationMaxBurstPerDevice] = value;
			}
		}

		// Token: 0x1700179D RID: 6045
		// (get) Token: 0x060047BE RID: 18366 RVA: 0x001089F4 File Offset: 0x00106BF4
		// (set) Token: 0x060047BF RID: 18367 RVA: 0x00108A06 File Offset: 0x00106C06
		public Unlimited<uint>? PushNotificationRechargeRatePerDevice
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PushNotificationRechargeRatePerDevice];
			}
			set
			{
				this[ThrottlingPolicySchema.PushNotificationRechargeRatePerDevice] = value;
			}
		}

		// Token: 0x1700179E RID: 6046
		// (get) Token: 0x060047C0 RID: 18368 RVA: 0x00108A19 File Offset: 0x00106C19
		// (set) Token: 0x060047C1 RID: 18369 RVA: 0x00108A2B File Offset: 0x00106C2B
		public Unlimited<uint>? PushNotificationSamplingPeriodPerDevice
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.PushNotificationSamplingPeriodPerDevice];
			}
			set
			{
				this[ThrottlingPolicySchema.PushNotificationSamplingPeriodPerDevice] = value;
			}
		}

		// Token: 0x1700179F RID: 6047
		// (get) Token: 0x060047C2 RID: 18370 RVA: 0x00108A3E File Offset: 0x00106C3E
		// (set) Token: 0x060047C3 RID: 18371 RVA: 0x00108A50 File Offset: 0x00106C50
		public Unlimited<uint>? EncryptionSenderMaxConcurrency
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.EncryptionSenderMaxConcurrency];
			}
			set
			{
				this[ThrottlingPolicySchema.EncryptionSenderMaxConcurrency] = value;
			}
		}

		// Token: 0x170017A0 RID: 6048
		// (get) Token: 0x060047C4 RID: 18372 RVA: 0x00108A63 File Offset: 0x00106C63
		// (set) Token: 0x060047C5 RID: 18373 RVA: 0x00108A75 File Offset: 0x00106C75
		public Unlimited<uint>? EncryptionSenderMaxBurst
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.EncryptionSenderMaxBurst];
			}
			set
			{
				this[ThrottlingPolicySchema.EncryptionSenderMaxBurst] = value;
			}
		}

		// Token: 0x170017A1 RID: 6049
		// (get) Token: 0x060047C6 RID: 18374 RVA: 0x00108A88 File Offset: 0x00106C88
		// (set) Token: 0x060047C7 RID: 18375 RVA: 0x00108A9A File Offset: 0x00106C9A
		public Unlimited<uint>? EncryptionSenderRechargeRate
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.EncryptionSenderRechargeRate];
			}
			set
			{
				this[ThrottlingPolicySchema.EncryptionSenderRechargeRate] = value;
			}
		}

		// Token: 0x170017A2 RID: 6050
		// (get) Token: 0x060047C8 RID: 18376 RVA: 0x00108AAD File Offset: 0x00106CAD
		// (set) Token: 0x060047C9 RID: 18377 RVA: 0x00108ABF File Offset: 0x00106CBF
		public Unlimited<uint>? EncryptionSenderCutoffBalance
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.EncryptionSenderCutoffBalance];
			}
			set
			{
				this[ThrottlingPolicySchema.EncryptionSenderCutoffBalance] = value;
			}
		}

		// Token: 0x170017A3 RID: 6051
		// (get) Token: 0x060047CA RID: 18378 RVA: 0x00108AD2 File Offset: 0x00106CD2
		// (set) Token: 0x060047CB RID: 18379 RVA: 0x00108AE4 File Offset: 0x00106CE4
		public Unlimited<uint>? EncryptionRecipientMaxConcurrency
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.EncryptionRecipientMaxConcurrency];
			}
			set
			{
				this[ThrottlingPolicySchema.EncryptionRecipientMaxConcurrency] = value;
			}
		}

		// Token: 0x170017A4 RID: 6052
		// (get) Token: 0x060047CC RID: 18380 RVA: 0x00108AF7 File Offset: 0x00106CF7
		// (set) Token: 0x060047CD RID: 18381 RVA: 0x00108B09 File Offset: 0x00106D09
		public Unlimited<uint>? EncryptionRecipientMaxBurst
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.EncryptionRecipientMaxBurst];
			}
			set
			{
				this[ThrottlingPolicySchema.EncryptionRecipientMaxBurst] = value;
			}
		}

		// Token: 0x170017A5 RID: 6053
		// (get) Token: 0x060047CE RID: 18382 RVA: 0x00108B1C File Offset: 0x00106D1C
		// (set) Token: 0x060047CF RID: 18383 RVA: 0x00108B2E File Offset: 0x00106D2E
		public Unlimited<uint>? EncryptionRecipientRechargeRate
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.EncryptionRecipientRechargeRate];
			}
			set
			{
				this[ThrottlingPolicySchema.EncryptionRecipientRechargeRate] = value;
			}
		}

		// Token: 0x170017A6 RID: 6054
		// (get) Token: 0x060047D0 RID: 18384 RVA: 0x00108B41 File Offset: 0x00106D41
		// (set) Token: 0x060047D1 RID: 18385 RVA: 0x00108B53 File Offset: 0x00106D53
		public Unlimited<uint>? EncryptionRecipientCutoffBalance
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.EncryptionRecipientCutoffBalance];
			}
			set
			{
				this[ThrottlingPolicySchema.EncryptionRecipientCutoffBalance] = value;
			}
		}

		// Token: 0x170017A7 RID: 6055
		// (get) Token: 0x060047D2 RID: 18386 RVA: 0x00108B66 File Offset: 0x00106D66
		// (set) Token: 0x060047D3 RID: 18387 RVA: 0x00108B78 File Offset: 0x00106D78
		public Unlimited<uint>? ComplianceMaxExpansionDGRecipients
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.ComplianceMaxExpansionDGRecipients];
			}
			set
			{
				this[ThrottlingPolicySchema.ComplianceMaxExpansionDGRecipients] = value;
			}
		}

		// Token: 0x170017A8 RID: 6056
		// (get) Token: 0x060047D4 RID: 18388 RVA: 0x00108B8B File Offset: 0x00106D8B
		// (set) Token: 0x060047D5 RID: 18389 RVA: 0x00108B9D File Offset: 0x00106D9D
		public Unlimited<uint>? ComplianceMaxExpansionNestedDGs
		{
			get
			{
				return (Unlimited<uint>?)this[ThrottlingPolicySchema.ComplianceMaxExpansionNestedDGs];
			}
			set
			{
				this[ThrottlingPolicySchema.ComplianceMaxExpansionNestedDGs] = value;
			}
		}

		// Token: 0x170017A9 RID: 6057
		// (get) Token: 0x060047D6 RID: 18390 RVA: 0x00108BB0 File Offset: 0x00106DB0
		public bool IsLegacyDefault
		{
			get
			{
				return this.LegacyThrottlingPolicy.IsDefault && !string.IsNullOrEmpty(this.LegacyThrottlingPolicy.ToString());
			}
		}

		// Token: 0x170017AA RID: 6058
		// (get) Token: 0x060047D7 RID: 18391 RVA: 0x00108BD4 File Offset: 0x00106DD4
		// (set) Token: 0x060047D8 RID: 18392 RVA: 0x00108C08 File Offset: 0x00106E08
		public string Diagnostics
		{
			get
			{
				if (this.diagnostics == null)
				{
					this.diagnostics = this.LegacyThrottlingPolicy.ToString();
					if (this.diagnostics == null)
					{
						this.diagnostics = string.Empty;
					}
				}
				return this.diagnostics;
			}
			set
			{
				this.diagnostics = (value ?? string.Empty);
			}
		}

		// Token: 0x060047D9 RID: 18393 RVA: 0x00108C1C File Offset: 0x00106E1C
		internal void CloneThrottlingSettingsFrom(IThrottlingPolicy policy)
		{
			this.AnonymousMaxConcurrency = new Unlimited<uint>?(policy.AnonymousMaxConcurrency);
			this.AnonymousMaxBurst = new Unlimited<uint>?(policy.AnonymousMaxBurst);
			this.AnonymousRechargeRate = new Unlimited<uint>?(policy.AnonymousRechargeRate);
			this.AnonymousCutoffBalance = new Unlimited<uint>?(policy.AnonymousCutoffBalance);
			this.EasMaxConcurrency = new Unlimited<uint>?(policy.EasMaxConcurrency);
			this.EasMaxBurst = new Unlimited<uint>?(policy.EasMaxBurst);
			this.EasRechargeRate = new Unlimited<uint>?(policy.EasRechargeRate);
			this.EasCutoffBalance = new Unlimited<uint>?(policy.EasCutoffBalance);
			this.EasMaxDevices = new Unlimited<uint>?(policy.EasMaxDevices);
			this.EasMaxDeviceDeletesPerMonth = new Unlimited<uint>?(policy.EasMaxDeviceDeletesPerMonth);
			this.EasMaxInactivityForDeviceCleanup = new Unlimited<uint>?(policy.EasMaxInactivityForDeviceCleanup);
			this.EwsMaxConcurrency = new Unlimited<uint>?(policy.EwsMaxConcurrency);
			this.EwsMaxBurst = new Unlimited<uint>?(policy.EwsMaxBurst);
			this.EwsRechargeRate = new Unlimited<uint>?(policy.EwsRechargeRate);
			this.EwsCutoffBalance = new Unlimited<uint>?(policy.EwsCutoffBalance);
			this.EwsMaxSubscriptions = new Unlimited<uint>?(policy.EwsMaxSubscriptions);
			this.ImapMaxConcurrency = new Unlimited<uint>?(policy.ImapMaxConcurrency);
			this.ImapMaxBurst = new Unlimited<uint>?(policy.ImapMaxBurst);
			this.ImapRechargeRate = new Unlimited<uint>?(policy.ImapRechargeRate);
			this.ImapCutoffBalance = new Unlimited<uint>?(policy.ImapCutoffBalance);
			this.OutlookServiceMaxConcurrency = new Unlimited<uint>?(policy.OutlookServiceMaxConcurrency);
			this.OutlookServiceMaxBurst = new Unlimited<uint>?(policy.OutlookServiceMaxBurst);
			this.OutlookServiceRechargeRate = new Unlimited<uint>?(policy.OutlookServiceRechargeRate);
			this.OutlookServiceCutoffBalance = new Unlimited<uint>?(policy.OutlookServiceCutoffBalance);
			this.OutlookServiceMaxSubscriptions = new Unlimited<uint>?(policy.OutlookServiceMaxSubscriptions);
			this.OutlookServiceMaxSocketConnectionsPerDevice = new Unlimited<uint>?(policy.OutlookServiceMaxSocketConnectionsPerDevice);
			this.OutlookServiceMaxSocketConnectionsPerUser = new Unlimited<uint>?(policy.OutlookServiceMaxSocketConnectionsPerUser);
			this.OwaMaxConcurrency = new Unlimited<uint>?(policy.OwaMaxConcurrency);
			this.OwaMaxBurst = new Unlimited<uint>?(policy.OwaMaxBurst);
			this.OwaRechargeRate = new Unlimited<uint>?(policy.OwaRechargeRate);
			this.OwaCutoffBalance = new Unlimited<uint>?(policy.OwaCutoffBalance);
			this.OwaVoiceMaxConcurrency = new Unlimited<uint>?(policy.OwaVoiceMaxConcurrency);
			this.OwaVoiceMaxBurst = new Unlimited<uint>?(policy.OwaVoiceMaxBurst);
			this.OwaVoiceRechargeRate = new Unlimited<uint>?(policy.OwaVoiceRechargeRate);
			this.OwaVoiceCutoffBalance = new Unlimited<uint>?(policy.OwaVoiceCutoffBalance);
			this.PopMaxConcurrency = new Unlimited<uint>?(policy.PopMaxConcurrency);
			this.PopMaxBurst = new Unlimited<uint>?(policy.PopMaxBurst);
			this.PopRechargeRate = new Unlimited<uint>?(policy.PopRechargeRate);
			this.PopCutoffBalance = new Unlimited<uint>?(policy.PopCutoffBalance);
			this.PowerShellMaxConcurrency = new Unlimited<uint>?(policy.PowerShellMaxConcurrency);
			this.PowerShellMaxBurst = new Unlimited<uint>?(policy.PowerShellMaxBurst);
			this.PowerShellRechargeRate = new Unlimited<uint>?(policy.PowerShellRechargeRate);
			this.PowerShellCutoffBalance = new Unlimited<uint>?(policy.PowerShellCutoffBalance);
			this.PowerShellMaxTenantConcurrency = new Unlimited<uint>?(policy.PowerShellMaxTenantConcurrency);
			this.PowerShellMaxOperations = new Unlimited<uint>?(policy.PowerShellMaxOperations);
			this.PowerShellMaxCmdletsTimePeriod = new Unlimited<uint>?(policy.PowerShellMaxCmdletsTimePeriod);
			this.ExchangeMaxCmdlets = new Unlimited<uint>?(policy.ExchangeMaxCmdlets);
			this.PowerShellMaxCmdletQueueDepth = new Unlimited<uint>?(policy.PowerShellMaxCmdletQueueDepth);
			this.PowerShellMaxDestructiveCmdlets = new Unlimited<uint>?(policy.PowerShellMaxDestructiveCmdlets);
			this.PowerShellMaxDestructiveCmdletsTimePeriod = new Unlimited<uint>?(policy.PowerShellMaxDestructiveCmdletsTimePeriod);
			this.PowerShellMaxCmdlets = new Unlimited<uint>?(policy.PowerShellMaxCmdlets);
			this.PowerShellMaxRunspaces = new Unlimited<uint>?(policy.PowerShellMaxRunspaces);
			this.PowerShellMaxTenantRunspaces = new Unlimited<uint>?(policy.PowerShellMaxTenantRunspaces);
			this.PowerShellMaxRunspacesTimePeriod = new Unlimited<uint>?(policy.PowerShellMaxRunspacesTimePeriod);
			this.PswsMaxConcurrency = new Unlimited<uint>?(policy.PswsMaxConcurrency);
			this.PswsMaxRequest = new Unlimited<uint>?(policy.PswsMaxRequest);
			this.PswsMaxRequestTimePeriod = new Unlimited<uint>?(policy.PswsMaxRequestTimePeriod);
			this.MessageRateLimit = new Unlimited<uint>?(policy.MessageRateLimit);
			this.RecipientRateLimit = new Unlimited<uint>?(policy.RecipientRateLimit);
			this.ForwardeeLimit = new Unlimited<uint>?(policy.ForwardeeLimit);
			this.DiscoveryMaxConcurrency = new Unlimited<uint>?(policy.DiscoveryMaxConcurrency);
			this.DiscoveryMaxMailboxes = new Unlimited<uint>?(policy.DiscoveryMaxMailboxes);
			this.DiscoveryMaxKeywords = new Unlimited<uint>?(policy.DiscoveryMaxKeywords);
			this.DiscoveryMaxPreviewSearchMailboxes = new Unlimited<uint>?(policy.DiscoveryMaxPreviewSearchMailboxes);
			this.DiscoveryMaxStatsSearchMailboxes = new Unlimited<uint>?(policy.DiscoveryMaxStatsSearchMailboxes);
			this.DiscoveryPreviewSearchResultsPageSize = new Unlimited<uint>?(policy.DiscoveryPreviewSearchResultsPageSize);
			this.DiscoveryMaxKeywordsPerPage = new Unlimited<uint>?(policy.DiscoveryMaxKeywordsPerPage);
			this.DiscoveryMaxRefinerResults = new Unlimited<uint>?(policy.DiscoveryMaxRefinerResults);
			this.DiscoveryMaxSearchQueueDepth = new Unlimited<uint>?(policy.DiscoveryMaxSearchQueueDepth);
			this.DiscoverySearchTimeoutPeriod = new Unlimited<uint>?(policy.DiscoverySearchTimeoutPeriod);
			this.PushNotificationCutoffBalance = new Unlimited<uint>?(policy.PushNotificationCutoffBalance);
			this.PushNotificationMaxBurst = new Unlimited<uint>?(policy.PushNotificationMaxBurst);
			this.PushNotificationMaxConcurrency = new Unlimited<uint>?(policy.PushNotificationMaxConcurrency);
			this.PushNotificationRechargeRate = new Unlimited<uint>?(policy.PushNotificationRechargeRate);
			this.PushNotificationMaxBurstPerDevice = new Unlimited<uint>?(policy.PushNotificationMaxBurstPerDevice);
			this.PushNotificationRechargeRatePerDevice = new Unlimited<uint>?(policy.PushNotificationRechargeRatePerDevice);
			this.PushNotificationSamplingPeriodPerDevice = new Unlimited<uint>?(policy.PushNotificationSamplingPeriodPerDevice);
			this.RcaMaxConcurrency = new Unlimited<uint>?(policy.RcaMaxConcurrency);
			this.RcaMaxBurst = new Unlimited<uint>?(policy.RcaMaxBurst);
			this.RcaRechargeRate = new Unlimited<uint>?(policy.RcaRechargeRate);
			this.RcaCutoffBalance = new Unlimited<uint>?(policy.RcaCutoffBalance);
			this.CpaMaxConcurrency = new Unlimited<uint>?(policy.CpaMaxConcurrency);
			this.CpaMaxBurst = new Unlimited<uint>?(policy.CpaMaxBurst);
			this.CpaRechargeRate = new Unlimited<uint>?(policy.CpaRechargeRate);
			this.CpaCutoffBalance = new Unlimited<uint>?(policy.CpaCutoffBalance);
			this.EncryptionSenderMaxConcurrency = new Unlimited<uint>?(policy.EncryptionSenderMaxConcurrency);
			this.EncryptionSenderMaxBurst = new Unlimited<uint>?(policy.EncryptionSenderMaxBurst);
			this.EncryptionSenderRechargeRate = new Unlimited<uint>?(policy.EncryptionSenderRechargeRate);
			this.EncryptionSenderCutoffBalance = new Unlimited<uint>?(policy.EncryptionSenderCutoffBalance);
			this.EncryptionRecipientMaxConcurrency = new Unlimited<uint>?(policy.EncryptionRecipientMaxConcurrency);
			this.EncryptionRecipientMaxBurst = new Unlimited<uint>?(policy.EncryptionRecipientMaxBurst);
			this.EncryptionRecipientRechargeRate = new Unlimited<uint>?(policy.EncryptionRecipientRechargeRate);
			this.EncryptionRecipientCutoffBalance = new Unlimited<uint>?(policy.EncryptionRecipientCutoffBalance);
			this.ComplianceMaxExpansionDGRecipients = new Unlimited<uint>?(policy.ComplianceMaxExpansionDGRecipients);
			this.ComplianceMaxExpansionNestedDGs = new Unlimited<uint>?(policy.ComplianceMaxExpansionNestedDGs);
		}

		// Token: 0x060047DA RID: 18394 RVA: 0x00109278 File Offset: 0x00107478
		internal void UpgradeFromLegacyThrottlingPolicy()
		{
			this.LegacyThrottlingPolicy.UpgradeThrottlingSettingsTo(this);
		}

		// Token: 0x060047DB RID: 18395 RVA: 0x00109286 File Offset: 0x00107486
		internal void SaveLegacyThrottlingPolicy(IConfigDataProvider session)
		{
			if (this.LegacyThrottlingPolicy.m_Session == null)
			{
				this.LegacyThrottlingPolicy.m_Session = (IDirectorySession)session;
			}
			session.Save(this.LegacyThrottlingPolicy);
		}

		// Token: 0x060047DC RID: 18396 RVA: 0x001092B2 File Offset: 0x001074B2
		internal new void SetId(IConfigurationSession session, ADObjectId parent, string cn)
		{
			base.SetId(session, parent, cn);
			this.LegacyThrottlingPolicy.SetIdAndName(this);
		}

		// Token: 0x060047DD RID: 18397 RVA: 0x001092C9 File Offset: 0x001074C9
		internal EffectiveThrottlingPolicy GetEffectiveThrottlingPolicy(bool useCacheToGetParent = false)
		{
			return new EffectiveThrottlingPolicy(this, useCacheToGetParent);
		}

		// Token: 0x060047DE RID: 18398 RVA: 0x001092D4 File Offset: 0x001074D4
		internal void ConvertToEffectiveThrottlingPolicy(bool useCacheToGetParent = false)
		{
			IThrottlingPolicy effectiveThrottlingPolicy = this.GetEffectiveThrottlingPolicy(useCacheToGetParent);
			this.CloneThrottlingSettingsFrom(effectiveThrottlingPolicy);
		}

		// Token: 0x0400317A RID: 12666
		public const string GlobalThrottlingPolicyNamePrefix = "GlobalThrottlingPolicy_";

		// Token: 0x0400317B RID: 12667
		public const string TenantHydrationThrottlingPolicyName = "TenantHydrationThrottlingPolicy";

		// Token: 0x0400317C RID: 12668
		public const string PartnerThrottlingPolicyName = "PartnerThrottlingPolicy";

		// Token: 0x0400317D RID: 12669
		public const string MSOSyncServiceThrottlingPolicyName = "MSOSyncServiceThrottlingPolicy";

		// Token: 0x0400317E RID: 12670
		public const string DiscoveryThrottlingPolicyName = "DiscoveryThrottlingPolicy";

		// Token: 0x0400317F RID: 12671
		public const string PushNotificationServiceThrottlingPolicy = "PushNotificationServiceThrottlingPolicy";

		// Token: 0x04003180 RID: 12672
		private static ThrottlingPolicySchema schema = ObjectSchema.GetInstance<ThrottlingPolicySchema>();

		// Token: 0x04003181 RID: 12673
		private static string mostDerivedClass = "msExchThrottlingPolicy";

		// Token: 0x04003182 RID: 12674
		private LegacyThrottlingPolicy legacyThrottlingPolicy;

		// Token: 0x04003183 RID: 12675
		private string diagnostics;
	}
}
