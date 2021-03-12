using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004A0 RID: 1184
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class LegacyThrottlingPolicy : ADConfigurationObject
	{
		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x06003611 RID: 13841 RVA: 0x000D4AF8 File Offset: 0x000D2CF8
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x06003612 RID: 13842 RVA: 0x000D4AFF File Offset: 0x000D2CFF
		internal override ADObjectSchema Schema
		{
			get
			{
				return LegacyThrottlingPolicy.schema;
			}
		}

		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x06003613 RID: 13843 RVA: 0x000D4B06 File Offset: 0x000D2D06
		internal override string MostDerivedObjectClass
		{
			get
			{
				return LegacyThrottlingPolicy.mostDerivedClass;
			}
		}

		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x06003614 RID: 13844 RVA: 0x000D4B10 File Offset: 0x000D2D10
		// (set) Token: 0x06003615 RID: 13845 RVA: 0x000D4B40 File Offset: 0x000D2D40
		internal bool IsDefault
		{
			get
			{
				return ((bool?)this[LegacyThrottlingPolicySchema.IsDefaultPolicy]) ?? false;
			}
			set
			{
				this[LegacyThrottlingPolicySchema.IsDefaultPolicy] = value;
			}
		}

		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x06003616 RID: 13846 RVA: 0x000D4B53 File Offset: 0x000D2D53
		// (set) Token: 0x06003617 RID: 13847 RVA: 0x000D4B65 File Offset: 0x000D2D65
		internal LegacyThrottlingPolicySettings AnonymousThrottlingPolicyStateSettings
		{
			get
			{
				return (LegacyThrottlingPolicySettings)this[LegacyThrottlingPolicySchema.AnonymousThrottlingPolicyStateSettings];
			}
			set
			{
				this[LegacyThrottlingPolicySchema.AnonymousThrottlingPolicyStateSettings] = value;
			}
		}

		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x06003618 RID: 13848 RVA: 0x000D4B73 File Offset: 0x000D2D73
		// (set) Token: 0x06003619 RID: 13849 RVA: 0x000D4B85 File Offset: 0x000D2D85
		internal LegacyThrottlingPolicySettings EasThrottlingPolicyStateSettings
		{
			get
			{
				return (LegacyThrottlingPolicySettings)this[LegacyThrottlingPolicySchema.EasThrottlingPolicyStateSettings];
			}
			set
			{
				this[LegacyThrottlingPolicySchema.EasThrottlingPolicyStateSettings] = value;
			}
		}

		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x0600361A RID: 13850 RVA: 0x000D4B93 File Offset: 0x000D2D93
		// (set) Token: 0x0600361B RID: 13851 RVA: 0x000D4BA5 File Offset: 0x000D2DA5
		internal LegacyThrottlingPolicySettings EwsThrottlingPolicyStateSettings
		{
			get
			{
				return (LegacyThrottlingPolicySettings)this[LegacyThrottlingPolicySchema.EwsThrottlingPolicyStateSettings];
			}
			set
			{
				this[LegacyThrottlingPolicySchema.EwsThrottlingPolicyStateSettings] = value;
			}
		}

		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x0600361C RID: 13852 RVA: 0x000D4BB3 File Offset: 0x000D2DB3
		// (set) Token: 0x0600361D RID: 13853 RVA: 0x000D4BC5 File Offset: 0x000D2DC5
		internal LegacyThrottlingPolicySettings ImapThrottlingPolicyStateSettings
		{
			get
			{
				return (LegacyThrottlingPolicySettings)this[LegacyThrottlingPolicySchema.ImapThrottlingPolicyStateSettings];
			}
			set
			{
				this[LegacyThrottlingPolicySchema.ImapThrottlingPolicyStateSettings] = value;
			}
		}

		// Token: 0x17001066 RID: 4198
		// (get) Token: 0x0600361E RID: 13854 RVA: 0x000D4BD3 File Offset: 0x000D2DD3
		// (set) Token: 0x0600361F RID: 13855 RVA: 0x000D4BE5 File Offset: 0x000D2DE5
		internal LegacyThrottlingPolicySettings OwaThrottlingPolicyStateSettings
		{
			get
			{
				return (LegacyThrottlingPolicySettings)this[LegacyThrottlingPolicySchema.OwaThrottlingPolicyStateSettings];
			}
			set
			{
				this[LegacyThrottlingPolicySchema.OwaThrottlingPolicyStateSettings] = value;
			}
		}

		// Token: 0x17001067 RID: 4199
		// (get) Token: 0x06003620 RID: 13856 RVA: 0x000D4BF3 File Offset: 0x000D2DF3
		// (set) Token: 0x06003621 RID: 13857 RVA: 0x000D4C05 File Offset: 0x000D2E05
		internal LegacyThrottlingPolicySettings PopThrottlingPolicyStateSettings
		{
			get
			{
				return (LegacyThrottlingPolicySettings)this[LegacyThrottlingPolicySchema.PopThrottlingPolicyStateSettings];
			}
			set
			{
				this[LegacyThrottlingPolicySchema.PopThrottlingPolicyStateSettings] = value;
			}
		}

		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x06003622 RID: 13858 RVA: 0x000D4C13 File Offset: 0x000D2E13
		// (set) Token: 0x06003623 RID: 13859 RVA: 0x000D4C25 File Offset: 0x000D2E25
		internal LegacyThrottlingPolicySettings PowershellThrottlingPolicyStateSettings
		{
			get
			{
				return (LegacyThrottlingPolicySettings)this[LegacyThrottlingPolicySchema.PowershellThrottlingPolicyStateSettings];
			}
			set
			{
				this[LegacyThrottlingPolicySchema.PowershellThrottlingPolicyStateSettings] = value;
			}
		}

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x06003624 RID: 13860 RVA: 0x000D4C33 File Offset: 0x000D2E33
		// (set) Token: 0x06003625 RID: 13861 RVA: 0x000D4C45 File Offset: 0x000D2E45
		internal LegacyThrottlingPolicySettings RcaThrottlingPolicyStateSettings
		{
			get
			{
				return (LegacyThrottlingPolicySettings)this[LegacyThrottlingPolicySchema.RcaThrottlingPolicyStateSettings];
			}
			set
			{
				this[LegacyThrottlingPolicySchema.RcaThrottlingPolicyStateSettings] = value;
			}
		}

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x06003626 RID: 13862 RVA: 0x000D4C53 File Offset: 0x000D2E53
		// (set) Token: 0x06003627 RID: 13863 RVA: 0x000D4C65 File Offset: 0x000D2E65
		internal LegacyThrottlingPolicySettings GeneralThrottlingPolicyStateSettings
		{
			get
			{
				return (LegacyThrottlingPolicySettings)this[LegacyThrottlingPolicySchema.GeneralThrottlingPolicyStateSettings];
			}
			set
			{
				this[LegacyThrottlingPolicySchema.GeneralThrottlingPolicyStateSettings] = value;
			}
		}

		// Token: 0x06003628 RID: 13864 RVA: 0x000D4C74 File Offset: 0x000D2E74
		internal static LegacyThrottlingPolicy GetLegacyThrottlingPolicy(ThrottlingPolicy policy)
		{
			LegacyThrottlingPolicy legacyThrottlingPolicy;
			if (policy.ObjectState == ObjectState.New)
			{
				legacyThrottlingPolicy = new LegacyThrottlingPolicy();
				legacyThrottlingPolicy[ADObjectSchema.ObjectState] = ObjectState.Unchanged;
				legacyThrottlingPolicy.ResetChangeTracking();
			}
			else
			{
				legacyThrottlingPolicy = LegacyThrottlingPolicy.LoadLegacyThrottlingPolicyFromAD(policy);
			}
			return legacyThrottlingPolicy;
		}

		// Token: 0x06003629 RID: 13865 RVA: 0x000D4CB4 File Offset: 0x000D2EB4
		internal void SetIdAndName(ThrottlingPolicy policy)
		{
			base.SetId(policy.Id);
			this.propertyBag.ResetChangeTracking(ADObjectSchema.Id);
			base.Name = policy.Id.Name;
			this.propertyBag.ResetChangeTracking(ADObjectSchema.Name);
			base.OrganizationId = policy.OrganizationId;
			this.propertyBag.ResetChangeTracking(ADObjectSchema.OrganizationId);
		}

		// Token: 0x0600362A RID: 13866 RVA: 0x000D4D1C File Offset: 0x000D2F1C
		private static LegacyThrottlingPolicy LoadLegacyThrottlingPolicyFromAD(ThrottlingPolicy policy)
		{
			LegacyThrottlingPolicy[] array = policy.Session.Find<LegacyThrottlingPolicy>(policy.Id, QueryScope.Base, null, null, 2);
			if (array == null || array.Length != 1)
			{
				throw new InvalidOperationException("Could not read throttling policy " + policy.Id);
			}
			return array[0];
		}

		// Token: 0x0600362B RID: 13867 RVA: 0x000D4D64 File Offset: 0x000D2F64
		private static void AppendSettingsString(StringBuilder sb, string name, LegacyThrottlingPolicySettings settings)
		{
			if (settings != null && !string.IsNullOrEmpty(settings.ToString()))
			{
				if (sb.Length > 0)
				{
					sb.Append(", ");
				}
				sb.Append("Legacy/" + name + ":" + settings.ToString());
			}
		}

		// Token: 0x0600362C RID: 13868 RVA: 0x000D4DB4 File Offset: 0x000D2FB4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			LegacyThrottlingPolicy.AppendSettingsString(stringBuilder, "Anonymous", this.AnonymousThrottlingPolicyStateSettings);
			LegacyThrottlingPolicy.AppendSettingsString(stringBuilder, "Eas", this.EasThrottlingPolicyStateSettings);
			LegacyThrottlingPolicy.AppendSettingsString(stringBuilder, "Ews", this.EwsThrottlingPolicyStateSettings);
			LegacyThrottlingPolicy.AppendSettingsString(stringBuilder, "Owa", this.OwaThrottlingPolicyStateSettings);
			LegacyThrottlingPolicy.AppendSettingsString(stringBuilder, "Pop", this.PopThrottlingPolicyStateSettings);
			LegacyThrottlingPolicy.AppendSettingsString(stringBuilder, "Imap", this.ImapThrottlingPolicyStateSettings);
			LegacyThrottlingPolicy.AppendSettingsString(stringBuilder, "PowerShell", this.PowershellThrottlingPolicyStateSettings);
			LegacyThrottlingPolicy.AppendSettingsString(stringBuilder, "Rca", this.RcaThrottlingPolicyStateSettings);
			LegacyThrottlingPolicy.AppendSettingsString(stringBuilder, "General", this.GeneralThrottlingPolicyStateSettings);
			return stringBuilder.ToString();
		}

		// Token: 0x0600362D RID: 13869 RVA: 0x000D4E68 File Offset: 0x000D3068
		internal void CloneThrottlingSettingsTo(LegacyThrottlingPolicy policy)
		{
			policy.AnonymousThrottlingPolicyStateSettings = this.AnonymousThrottlingPolicyStateSettings;
			policy.EasThrottlingPolicyStateSettings = this.EasThrottlingPolicyStateSettings;
			policy.EwsThrottlingPolicyStateSettings = this.EwsThrottlingPolicyStateSettings;
			policy.ImapThrottlingPolicyStateSettings = this.ImapThrottlingPolicyStateSettings;
			policy.OwaThrottlingPolicyStateSettings = this.OwaThrottlingPolicyStateSettings;
			policy.PopThrottlingPolicyStateSettings = this.PopThrottlingPolicyStateSettings;
			policy.PowershellThrottlingPolicyStateSettings = this.PowershellThrottlingPolicyStateSettings;
			policy.RcaThrottlingPolicyStateSettings = this.RcaThrottlingPolicyStateSettings;
			policy.GeneralThrottlingPolicyStateSettings = this.GeneralThrottlingPolicyStateSettings;
			if (policy.Session == null)
			{
				policy.m_Session = base.Session;
			}
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x000D4EF8 File Offset: 0x000D30F8
		internal void UpgradeThrottlingSettingsTo(ThrottlingPolicy policy)
		{
			foreach (KeyValuePair<ADPropertyDefinition, ADPropertyDefinition> keyValuePair in LegacyThrottlingPolicy.CommonThrottlingPolicySettingMapping)
			{
				this.CopyIfValueExists(policy, keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x0600362F RID: 13871 RVA: 0x000D4F58 File Offset: 0x000D3158
		private void CopyIfValueExists(ThrottlingPolicy policy, ADPropertyDefinition property, ADPropertyDefinition legacyProperty)
		{
			string text = (string)this[legacyProperty];
			if (!string.IsNullOrEmpty(text))
			{
				Unlimited<uint> unlimited = ThrottlingPolicyBaseSettings.ParseValue(text);
				policy[property] = unlimited;
			}
		}

		// Token: 0x04002493 RID: 9363
		private static readonly Dictionary<ADPropertyDefinition, ADPropertyDefinition> CommonThrottlingPolicySettingMapping = new Dictionary<ADPropertyDefinition, ADPropertyDefinition>
		{
			{
				ThrottlingPolicySchema.AnonymousMaxConcurrency,
				LegacyThrottlingPolicySchema.AnonymousMaxConcurrency
			},
			{
				ThrottlingPolicySchema.EasMaxConcurrency,
				LegacyThrottlingPolicySchema.EasMaxConcurrency
			},
			{
				ThrottlingPolicySchema.EasMaxDevices,
				LegacyThrottlingPolicySchema.EasMaxDevices
			},
			{
				ThrottlingPolicySchema.EasMaxDeviceDeletesPerMonth,
				LegacyThrottlingPolicySchema.EasMaxDeviceDeletesPerMonth
			},
			{
				ThrottlingPolicySchema.EwsMaxConcurrency,
				LegacyThrottlingPolicySchema.EwsMaxConcurrency
			},
			{
				ThrottlingPolicySchema.EwsMaxSubscriptions,
				LegacyThrottlingPolicySchema.EwsMaxSubscriptions
			},
			{
				ThrottlingPolicySchema.ImapMaxConcurrency,
				LegacyThrottlingPolicySchema.ImapMaxConcurrency
			},
			{
				ThrottlingPolicySchema.OwaMaxConcurrency,
				LegacyThrottlingPolicySchema.OwaMaxConcurrency
			},
			{
				ThrottlingPolicySchema.PopMaxConcurrency,
				LegacyThrottlingPolicySchema.PopMaxConcurrency
			},
			{
				ThrottlingPolicySchema.PowerShellMaxConcurrency,
				LegacyThrottlingPolicySchema.PowershellMaxConcurrency
			},
			{
				ThrottlingPolicySchema.PowerShellMaxTenantConcurrency,
				LegacyThrottlingPolicySchema.PowershellMaxTenantConcurrency
			},
			{
				ThrottlingPolicySchema.PowerShellMaxCmdlets,
				LegacyThrottlingPolicySchema.PowerShellMaxCmdlets
			},
			{
				ThrottlingPolicySchema.PowerShellMaxCmdletsTimePeriod,
				LegacyThrottlingPolicySchema.PowershellMaxCmdletsTimePeriod
			},
			{
				ThrottlingPolicySchema.PowerShellMaxCmdletQueueDepth,
				LegacyThrottlingPolicySchema.PowershellMaxCmdletQueueDepth
			},
			{
				ThrottlingPolicySchema.ExchangeMaxCmdlets,
				LegacyThrottlingPolicySchema.ExchangeMaxCmdlets
			},
			{
				ThrottlingPolicySchema.PowerShellMaxDestructiveCmdlets,
				LegacyThrottlingPolicySchema.PowershellMaxDestructiveCmdlets
			},
			{
				ThrottlingPolicySchema.PowerShellMaxDestructiveCmdletsTimePeriod,
				LegacyThrottlingPolicySchema.PowershellMaxDestructiveCmdletsTimePeriod
			},
			{
				ThrottlingPolicySchema.RcaMaxConcurrency,
				LegacyThrottlingPolicySchema.RcaMaxConcurrency
			},
			{
				ThrottlingPolicySchema.CpaMaxConcurrency,
				LegacyThrottlingPolicySchema.CpaMaxConcurrency
			},
			{
				ThrottlingPolicySchema.MessageRateLimit,
				LegacyThrottlingPolicySchema.MessageRateLimit
			},
			{
				ThrottlingPolicySchema.RecipientRateLimit,
				LegacyThrottlingPolicySchema.RecipientRateLimit
			},
			{
				ThrottlingPolicySchema.ForwardeeLimit,
				LegacyThrottlingPolicySchema.ForwardeeLimit
			},
			{
				ThrottlingPolicySchema.DiscoveryMaxConcurrency,
				LegacyThrottlingPolicySchema.DiscoveryMaxConcurrency
			},
			{
				ThrottlingPolicySchema.DiscoveryMaxMailboxes,
				LegacyThrottlingPolicySchema.DiscoveryMaxMailboxes
			},
			{
				ThrottlingPolicySchema.DiscoveryMaxKeywords,
				LegacyThrottlingPolicySchema.DiscoveryMaxKeywords
			}
		};

		// Token: 0x04002494 RID: 9364
		private static LegacyThrottlingPolicySchema schema = ObjectSchema.GetInstance<LegacyThrottlingPolicySchema>();

		// Token: 0x04002495 RID: 9365
		private static string mostDerivedClass = "msExchThrottlingPolicy";
	}
}
