using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005E5 RID: 1509
	internal sealed class ThrottlingPolicySchema : ADConfigurationObjectSchema
	{
		// Token: 0x06004647 RID: 17991 RVA: 0x00105218 File Offset: 0x00103418
		internal static GetterDelegate ThrottlingPolicyScopeGetterDelegate(ProviderPropertyDefinition propertyDefinition)
		{
			return delegate(IPropertyBag bag)
			{
				int num = (int)bag[propertyDefinition];
				if ((num & 4) != 0)
				{
					return ThrottlingPolicyScopeType.Global;
				}
				if ((num & 2) != 0)
				{
					return ThrottlingPolicyScopeType.Organization;
				}
				return ThrottlingPolicyScopeType.Regular;
			};
		}

		// Token: 0x06004648 RID: 17992 RVA: 0x001052E8 File Offset: 0x001034E8
		internal static SetterDelegate ThrottlingPolicyScopeSetterDelegate(ProviderPropertyDefinition propertyDefinition)
		{
			return delegate(object value, IPropertyBag bag)
			{
				int num = (int)bag[propertyDefinition];
				switch ((ThrottlingPolicyScopeType)value)
				{
				case ThrottlingPolicyScopeType.Regular:
					bag[propertyDefinition] = (num & -5 & -3);
					return;
				case ThrottlingPolicyScopeType.Organization:
					bag[propertyDefinition] = ((num & -5) | 2);
					return;
				case ThrottlingPolicyScopeType.Global:
					bag[propertyDefinition] = ((num & -3) | 4);
					return;
				default:
					throw new NotSupportedException(string.Format("Unsupported enum value {0}.", (ThrottlingPolicyScopeType)value));
				}
			};
		}

		// Token: 0x06004649 RID: 17993 RVA: 0x00105310 File Offset: 0x00103510
		internal static QueryFilter ThrottlingPolicyScopeFilterBuilder(SinglePropertyFilter filter)
		{
			if (!(filter is ComparisonFilter))
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedFilterForProperty(filter.Property.Name, filter.GetType(), typeof(ComparisonFilter)));
			}
			ComparisonFilter comparisonFilter = (ComparisonFilter)filter;
			if (comparisonFilter.ComparisonOperator != ComparisonOperator.Equal)
			{
				throw new ADFilterException(DirectoryStrings.ExceptionUnsupportedOperatorForProperty(comparisonFilter.Property.Name, comparisonFilter.ComparisonOperator.ToString()));
			}
			ThrottlingPolicyScopeType throttlingPolicyScopeType = (ThrottlingPolicyScopeType)comparisonFilter.PropertyValue;
			switch (throttlingPolicyScopeType)
			{
			case ThrottlingPolicyScopeType.Regular:
				return new NotFilter(new BitMaskOrFilter(ThrottlingPolicySchema.ThrottlingPolicyFlags, 6UL));
			case ThrottlingPolicyScopeType.Organization:
				return new BitMaskAndFilter(ThrottlingPolicySchema.ThrottlingPolicyFlags, 2UL);
			case ThrottlingPolicyScopeType.Global:
				return new BitMaskAndFilter(ThrottlingPolicySchema.ThrottlingPolicyFlags, 4UL);
			default:
				throw new NotSupportedException(string.Format("Unsupported enum value {0}.", throttlingPolicyScopeType));
			}
		}

		// Token: 0x0600464A RID: 17994 RVA: 0x001053E8 File Offset: 0x001035E8
		private static T GetSettingsFromPropertyBag<T>(IPropertyBag propertyBag, ADPropertyDefinition settingsProperty) where T : ThrottlingPolicyBaseSettings, new()
		{
			T t = (T)((object)propertyBag[settingsProperty]);
			if (t == null)
			{
				t = Activator.CreateInstance<T>();
			}
			return t;
		}

		// Token: 0x0600464B RID: 17995 RVA: 0x00105414 File Offset: 0x00103614
		private static ADPropertyDefinition BuildSettingsProperty<T>(string name, string ldapDisplayName) where T : ThrottlingPolicyBaseSettings, new()
		{
			return new ADPropertyDefinition(name, ExchangeObjectVersion.Exchange2010, typeof(T), ldapDisplayName, ADPropertyDefinitionFlags.None, Activator.CreateInstance<T>(), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);
		}

		// Token: 0x0600464C RID: 17996 RVA: 0x00105456 File Offset: 0x00103656
		private static ADPropertyDefinition BuildCalculatedSettingProperty<T>(string name, ADPropertyDefinition parentProperty, Func<T, Unlimited<uint>?> getterFunction, Action<T, object> setterAction) where T : ThrottlingPolicyBaseSettings, new()
		{
			return ThrottlingPolicySchema.BuildCalculatedSettingProperty<T>(name, PropertyDefinitionConstraint.None, parentProperty, getterFunction, setterAction);
		}

		// Token: 0x0600464D RID: 17997 RVA: 0x00105500 File Offset: 0x00103700
		private static ADPropertyDefinition BuildCalculatedSettingProperty<T>(string name, PropertyDefinitionConstraint[] writeConstraints, ADPropertyDefinition parentProperty, Func<T, Unlimited<uint>?> getterFunction, Action<T, object> setterAction) where T : ThrottlingPolicyBaseSettings, new()
		{
			return new ADPropertyDefinition(name, ExchangeObjectVersion.Exchange2010, typeof(Unlimited<uint>?), null, ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, writeConstraints, new ProviderPropertyDefinition[]
			{
				parentProperty
			}, null, delegate(IPropertyBag propertyBag)
			{
				T settingsFromPropertyBag = ThrottlingPolicySchema.GetSettingsFromPropertyBag<T>(propertyBag, parentProperty);
				return getterFunction(settingsFromPropertyBag);
			}, delegate(object value, IPropertyBag propertyBag)
			{
				T settingsFromPropertyBag = ThrottlingPolicySchema.GetSettingsFromPropertyBag<T>(propertyBag, parentProperty);
				T t = settingsFromPropertyBag.Clone<T>();
				setterAction(t, value);
				if (string.IsNullOrEmpty(t.ToString()))
				{
					t = default(T);
				}
				propertyBag[parentProperty] = t;
			}, null, null);
		}

		// Token: 0x04003040 RID: 12352
		internal const string AnonymousThrottlingPolicyStateSettingsName = "msExchAnonymousThrottlingPolicyStateEx";

		// Token: 0x04003041 RID: 12353
		internal const string EasThrottlingPolicyStateSettingsName = "msExchEasThrottlingPolicyStateEx";

		// Token: 0x04003042 RID: 12354
		internal const string EwsThrottlingPolicyStateSettingsName = "msExchEwsThrottlingPolicyStateEx";

		// Token: 0x04003043 RID: 12355
		internal const string ImapThrottlingPolicyStateSettingsName = "msExchImapThrottlingPolicyStateEx";

		// Token: 0x04003044 RID: 12356
		internal const string OwaThrottlingPolicyStateSettingsName = "msExchOwaThrottlingPolicyStateEx";

		// Token: 0x04003045 RID: 12357
		internal const string PopThrottlingPolicyStateSettingsName = "msExchPopThrottlingPolicyStateEx";

		// Token: 0x04003046 RID: 12358
		internal const string PowershellThrottlingPolicyStateSettingsName = "msExchPowershellThrottlingPolicyStateEx";

		// Token: 0x04003047 RID: 12359
		internal const string RcaThrottlingPolicyStateSettingsName = "msExchRcaThrottlingPolicyStateEx";

		// Token: 0x04003048 RID: 12360
		internal const string GeneralThrottlingPolicyStateSettingsName = "msExchGeneralThrottlingPolicyStateEx";

		// Token: 0x04003049 RID: 12361
		internal const string PushNotificationThrottlingPolicyStateSettingsName = "msExchPushNotificationsThrottlingPolicyStateEx";

		// Token: 0x0400304A RID: 12362
		internal const string E4eThrottlingPolicyStateSettingsName = "msExchEncryptionThrottlingPolicyStateEx";

		// Token: 0x0400304B RID: 12363
		private static readonly PropertyDefinitionConstraint[] RecipientRateLimitWriteConstraint = new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<uint>(1U, uint.MaxValue)
		};

		// Token: 0x0400304C RID: 12364
		private static readonly PropertyDefinitionConstraint[] ForwardeeLimitWriteConstraint = new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<uint>(1U, uint.MaxValue)
		};

		// Token: 0x0400304D RID: 12365
		private static readonly PropertyDefinitionConstraint[] MessageRateLimitWriteConstraint = new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<uint>(0U, 100000U)
		};

		// Token: 0x0400304E RID: 12366
		private static readonly ADPropertyDefinition AnonymousThrottlingPolicyStateSettings = ThrottlingPolicySchema.BuildSettingsProperty<ThrottlingPolicyAnonymousSettings>("AnonymousThrottlingPolicyStateSettings", "msExchAnonymousThrottlingPolicyStateEx");

		// Token: 0x0400304F RID: 12367
		public static readonly ADPropertyDefinition AnonymousMaxConcurrency = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyAnonymousSettings>("AnonymousMaxConcurrency", ThrottlingPolicySchema.AnonymousThrottlingPolicyStateSettings, (ThrottlingPolicyAnonymousSettings settings) => settings.MaxConcurrency, delegate(ThrottlingPolicyAnonymousSettings settings, object value)
		{
			settings.MaxConcurrency = (Unlimited<uint>?)value;
		});

		// Token: 0x04003050 RID: 12368
		public static readonly ADPropertyDefinition AnonymousMaxBurst = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyAnonymousSettings>("AnonymousMaxBurst", ThrottlingPolicySchema.AnonymousThrottlingPolicyStateSettings, (ThrottlingPolicyAnonymousSettings settings) => settings.MaxBurst, delegate(ThrottlingPolicyAnonymousSettings settings, object value)
		{
			settings.MaxBurst = (Unlimited<uint>?)value;
		});

		// Token: 0x04003051 RID: 12369
		public static readonly ADPropertyDefinition AnonymousRechargeRate = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyAnonymousSettings>("AnonymousRechargeRate", ThrottlingPolicySchema.AnonymousThrottlingPolicyStateSettings, (ThrottlingPolicyAnonymousSettings settings) => settings.RechargeRate, delegate(ThrottlingPolicyAnonymousSettings settings, object value)
		{
			settings.RechargeRate = (Unlimited<uint>?)value;
		});

		// Token: 0x04003052 RID: 12370
		public static readonly ADPropertyDefinition AnonymousCutoffBalance = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyAnonymousSettings>("AnonymousCutoffBalance", ThrottlingPolicySchema.AnonymousThrottlingPolicyStateSettings, (ThrottlingPolicyAnonymousSettings settings) => settings.CutoffBalance, delegate(ThrottlingPolicyAnonymousSettings settings, object value)
		{
			settings.CutoffBalance = (Unlimited<uint>?)value;
		});

		// Token: 0x04003053 RID: 12371
		private static readonly ADPropertyDefinition EasThrottlingPolicyStateSettings = ThrottlingPolicySchema.BuildSettingsProperty<ThrottlingPolicyEasSettings>("EasThrottlingPolicyStateSettings", "msExchEasThrottlingPolicyStateEx");

		// Token: 0x04003054 RID: 12372
		public static readonly ADPropertyDefinition EasMaxConcurrency = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyEasSettings>("EasMaxConcurrency", ThrottlingPolicySchema.EasThrottlingPolicyStateSettings, (ThrottlingPolicyEasSettings settings) => settings.MaxConcurrency, delegate(ThrottlingPolicyEasSettings settings, object value)
		{
			settings.MaxConcurrency = (Unlimited<uint>?)value;
		});

		// Token: 0x04003055 RID: 12373
		public static readonly ADPropertyDefinition EasMaxBurst = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyEasSettings>("EasMaxBurst", ThrottlingPolicySchema.EasThrottlingPolicyStateSettings, (ThrottlingPolicyEasSettings settings) => settings.MaxBurst, delegate(ThrottlingPolicyEasSettings settings, object value)
		{
			settings.MaxBurst = (Unlimited<uint>?)value;
		});

		// Token: 0x04003056 RID: 12374
		public static readonly ADPropertyDefinition EasRechargeRate = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyEasSettings>("EasRechargeRate", ThrottlingPolicySchema.EasThrottlingPolicyStateSettings, (ThrottlingPolicyEasSettings settings) => settings.RechargeRate, delegate(ThrottlingPolicyEasSettings settings, object value)
		{
			settings.RechargeRate = (Unlimited<uint>?)value;
		});

		// Token: 0x04003057 RID: 12375
		public static readonly ADPropertyDefinition EasCutoffBalance = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyEasSettings>("EasCutoffBalance", ThrottlingPolicySchema.EasThrottlingPolicyStateSettings, (ThrottlingPolicyEasSettings settings) => settings.CutoffBalance, delegate(ThrottlingPolicyEasSettings settings, object value)
		{
			settings.CutoffBalance = (Unlimited<uint>?)value;
		});

		// Token: 0x04003058 RID: 12376
		public static readonly ADPropertyDefinition EasMaxDevices = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyEasSettings>("EasMaxDevices", ThrottlingPolicySchema.EasThrottlingPolicyStateSettings, (ThrottlingPolicyEasSettings settings) => settings.MaxDevices, delegate(ThrottlingPolicyEasSettings settings, object value)
		{
			settings.MaxDevices = (Unlimited<uint>?)value;
		});

		// Token: 0x04003059 RID: 12377
		public static readonly ADPropertyDefinition EasMaxDeviceDeletesPerMonth = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyEasSettings>("EasMaxDeviceDeletesPerMonth", ThrottlingPolicySchema.EasThrottlingPolicyStateSettings, (ThrottlingPolicyEasSettings settings) => settings.MaxDeviceDeletesPerMonth, delegate(ThrottlingPolicyEasSettings settings, object value)
		{
			settings.MaxDeviceDeletesPerMonth = (Unlimited<uint>?)value;
		});

		// Token: 0x0400305A RID: 12378
		public static readonly ADPropertyDefinition EasMaxInactivityForDeviceCleanup = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyEasSettings>("EasMaxInactivityForDeviceCleanup", ThrottlingPolicySchema.EasThrottlingPolicyStateSettings, (ThrottlingPolicyEasSettings settings) => settings.MaxInactivityForDeviceCleanup, delegate(ThrottlingPolicyEasSettings settings, object value)
		{
			settings.MaxInactivityForDeviceCleanup = (Unlimited<uint>?)value;
		});

		// Token: 0x0400305B RID: 12379
		private static readonly ADPropertyDefinition EwsThrottlingPolicyStateSettings = ThrottlingPolicySchema.BuildSettingsProperty<ThrottlingPolicyEwsSettings>("EwsThrottlingPolicyStateSettings", "msExchEwsThrottlingPolicyStateEx");

		// Token: 0x0400305C RID: 12380
		public static readonly ADPropertyDefinition EwsMaxConcurrency = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyEwsSettings>("EwsMaxConcurrency", ThrottlingPolicySchema.EwsThrottlingPolicyStateSettings, (ThrottlingPolicyEwsSettings settings) => settings.MaxConcurrency, delegate(ThrottlingPolicyEwsSettings settings, object value)
		{
			settings.MaxConcurrency = (Unlimited<uint>?)value;
		});

		// Token: 0x0400305D RID: 12381
		public static readonly ADPropertyDefinition EwsMaxBurst = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyEwsSettings>("EwsMaxBurst", ThrottlingPolicySchema.EwsThrottlingPolicyStateSettings, (ThrottlingPolicyEwsSettings settings) => settings.MaxBurst, delegate(ThrottlingPolicyEwsSettings settings, object value)
		{
			settings.MaxBurst = (Unlimited<uint>?)value;
		});

		// Token: 0x0400305E RID: 12382
		public static readonly ADPropertyDefinition EwsRechargeRate = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyEwsSettings>("EwsRechargeRate", ThrottlingPolicySchema.EwsThrottlingPolicyStateSettings, (ThrottlingPolicyEwsSettings settings) => settings.RechargeRate, delegate(ThrottlingPolicyEwsSettings settings, object value)
		{
			settings.RechargeRate = (Unlimited<uint>?)value;
		});

		// Token: 0x0400305F RID: 12383
		public static readonly ADPropertyDefinition EwsCutoffBalance = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyEwsSettings>("EwsCutoffBalance", ThrottlingPolicySchema.EwsThrottlingPolicyStateSettings, (ThrottlingPolicyEwsSettings settings) => settings.CutoffBalance, delegate(ThrottlingPolicyEwsSettings settings, object value)
		{
			settings.CutoffBalance = (Unlimited<uint>?)value;
		});

		// Token: 0x04003060 RID: 12384
		public static readonly ADPropertyDefinition EwsMaxSubscriptions = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyEwsSettings>("EwsMaxSubscriptions", ThrottlingPolicySchema.EwsThrottlingPolicyStateSettings, (ThrottlingPolicyEwsSettings settings) => settings.MaxSubscriptions, delegate(ThrottlingPolicyEwsSettings settings, object value)
		{
			settings.MaxSubscriptions = (Unlimited<uint>?)value;
		});

		// Token: 0x04003061 RID: 12385
		public static readonly ADPropertyDefinition OutlookServiceMaxConcurrency = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyEwsSettings>("OutlookServiceMaxConcurrency", ThrottlingPolicySchema.EwsThrottlingPolicyStateSettings, (ThrottlingPolicyEwsSettings settings) => settings.OutlookServiceMaxConcurrency, delegate(ThrottlingPolicyEwsSettings settings, object value)
		{
			settings.OutlookServiceMaxConcurrency = (Unlimited<uint>?)value;
		});

		// Token: 0x04003062 RID: 12386
		public static readonly ADPropertyDefinition OutlookServiceMaxBurst = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyEwsSettings>("OutlookServiceMaxBurst", ThrottlingPolicySchema.EwsThrottlingPolicyStateSettings, (ThrottlingPolicyEwsSettings settings) => settings.OutlookServiceMaxBurst, delegate(ThrottlingPolicyEwsSettings settings, object value)
		{
			settings.OutlookServiceMaxBurst = (Unlimited<uint>?)value;
		});

		// Token: 0x04003063 RID: 12387
		public static readonly ADPropertyDefinition OutlookServiceRechargeRate = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyEwsSettings>("OutlookServiceRechargeRate", ThrottlingPolicySchema.EwsThrottlingPolicyStateSettings, (ThrottlingPolicyEwsSettings settings) => settings.OutlookServiceRechargeRate, delegate(ThrottlingPolicyEwsSettings settings, object value)
		{
			settings.OutlookServiceRechargeRate = (Unlimited<uint>?)value;
		});

		// Token: 0x04003064 RID: 12388
		public static readonly ADPropertyDefinition OutlookServiceCutoffBalance = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyEwsSettings>("OutlookServiceCutoffBalance", ThrottlingPolicySchema.EwsThrottlingPolicyStateSettings, (ThrottlingPolicyEwsSettings settings) => settings.OutlookServiceCutoffBalance, delegate(ThrottlingPolicyEwsSettings settings, object value)
		{
			settings.OutlookServiceCutoffBalance = (Unlimited<uint>?)value;
		});

		// Token: 0x04003065 RID: 12389
		public static readonly ADPropertyDefinition OutlookServiceMaxSubscriptions = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyEwsSettings>("OutlookServiceMaxSubscriptions", ThrottlingPolicySchema.EwsThrottlingPolicyStateSettings, (ThrottlingPolicyEwsSettings settings) => settings.OutlookServiceMaxSubscriptions, delegate(ThrottlingPolicyEwsSettings settings, object value)
		{
			settings.OutlookServiceMaxSubscriptions = (Unlimited<uint>?)value;
		});

		// Token: 0x04003066 RID: 12390
		public static readonly ADPropertyDefinition OutlookServiceMaxSocketConnectionsPerDevice = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyEwsSettings>("OutlookServiceMaxSocketConnectionsPerDevice", ThrottlingPolicySchema.EwsThrottlingPolicyStateSettings, (ThrottlingPolicyEwsSettings settings) => settings.OutlookServiceMaxSocketConnectionsPerDevice, delegate(ThrottlingPolicyEwsSettings settings, object value)
		{
			settings.OutlookServiceMaxSocketConnectionsPerDevice = (Unlimited<uint>?)value;
		});

		// Token: 0x04003067 RID: 12391
		public static readonly ADPropertyDefinition OutlookServiceMaxSocketConnectionsPerUser = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyEwsSettings>("OutlookServiceMaxSocketConnectionsPerUser", ThrottlingPolicySchema.EwsThrottlingPolicyStateSettings, (ThrottlingPolicyEwsSettings settings) => settings.OutlookServiceMaxSocketConnectionsPerUser, delegate(ThrottlingPolicyEwsSettings settings, object value)
		{
			settings.OutlookServiceMaxSocketConnectionsPerUser = (Unlimited<uint>?)value;
		});

		// Token: 0x04003068 RID: 12392
		private static readonly ADPropertyDefinition ImapThrottlingPolicyStateSettings = ThrottlingPolicySchema.BuildSettingsProperty<ThrottlingPolicyImapSettings>("ImapThrottlingPolicyStateSettings", "msExchImapThrottlingPolicyStateEx");

		// Token: 0x04003069 RID: 12393
		public static readonly ADPropertyDefinition ImapMaxConcurrency = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyImapSettings>("ImapMaxConcurrency", ThrottlingPolicySchema.ImapThrottlingPolicyStateSettings, (ThrottlingPolicyImapSettings settings) => settings.MaxConcurrency, delegate(ThrottlingPolicyImapSettings settings, object value)
		{
			settings.MaxConcurrency = (Unlimited<uint>?)value;
		});

		// Token: 0x0400306A RID: 12394
		public static readonly ADPropertyDefinition ImapMaxBurst = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyImapSettings>("ImapMaxBurst", ThrottlingPolicySchema.ImapThrottlingPolicyStateSettings, (ThrottlingPolicyImapSettings settings) => settings.MaxBurst, delegate(ThrottlingPolicyImapSettings settings, object value)
		{
			settings.MaxBurst = (Unlimited<uint>?)value;
		});

		// Token: 0x0400306B RID: 12395
		public static readonly ADPropertyDefinition ImapRechargeRate = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyImapSettings>("ImapRechargeRate", ThrottlingPolicySchema.ImapThrottlingPolicyStateSettings, (ThrottlingPolicyImapSettings settings) => settings.RechargeRate, delegate(ThrottlingPolicyImapSettings settings, object value)
		{
			settings.RechargeRate = (Unlimited<uint>?)value;
		});

		// Token: 0x0400306C RID: 12396
		public static readonly ADPropertyDefinition ImapCutoffBalance = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyImapSettings>("ImapCutoffBalance", ThrottlingPolicySchema.ImapThrottlingPolicyStateSettings, (ThrottlingPolicyImapSettings settings) => settings.CutoffBalance, delegate(ThrottlingPolicyImapSettings settings, object value)
		{
			settings.CutoffBalance = (Unlimited<uint>?)value;
		});

		// Token: 0x0400306D RID: 12397
		private static readonly ADPropertyDefinition OwaThrottlingPolicyStateSettings = ThrottlingPolicySchema.BuildSettingsProperty<ThrottlingPolicyOwaSettings>("OwaThrottlingPolicyStateSettings", "msExchOwaThrottlingPolicyStateEx");

		// Token: 0x0400306E RID: 12398
		public static readonly ADPropertyDefinition OwaMaxConcurrency = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyOwaSettings>("OwaMaxConcurrency", ThrottlingPolicySchema.OwaThrottlingPolicyStateSettings, (ThrottlingPolicyOwaSettings settings) => settings.MaxConcurrency, delegate(ThrottlingPolicyOwaSettings settings, object value)
		{
			settings.MaxConcurrency = (Unlimited<uint>?)value;
		});

		// Token: 0x0400306F RID: 12399
		public static readonly ADPropertyDefinition OwaMaxBurst = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyOwaSettings>("OwaMaxBurst", ThrottlingPolicySchema.OwaThrottlingPolicyStateSettings, (ThrottlingPolicyOwaSettings settings) => settings.MaxBurst, delegate(ThrottlingPolicyOwaSettings settings, object value)
		{
			settings.MaxBurst = (Unlimited<uint>?)value;
		});

		// Token: 0x04003070 RID: 12400
		public static readonly ADPropertyDefinition OwaRechargeRate = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyOwaSettings>("OwaRechargeRate", ThrottlingPolicySchema.OwaThrottlingPolicyStateSettings, (ThrottlingPolicyOwaSettings settings) => settings.RechargeRate, delegate(ThrottlingPolicyOwaSettings settings, object value)
		{
			settings.RechargeRate = (Unlimited<uint>?)value;
		});

		// Token: 0x04003071 RID: 12401
		public static readonly ADPropertyDefinition OwaCutoffBalance = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyOwaSettings>("OwaCutoffBalance", ThrottlingPolicySchema.OwaThrottlingPolicyStateSettings, (ThrottlingPolicyOwaSettings settings) => settings.CutoffBalance, delegate(ThrottlingPolicyOwaSettings settings, object value)
		{
			settings.CutoffBalance = (Unlimited<uint>?)value;
		});

		// Token: 0x04003072 RID: 12402
		public static readonly ADPropertyDefinition OwaVoiceMaxConcurrency = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyOwaSettings>("OwaVoiceMaxConcurrency", ThrottlingPolicySchema.OwaThrottlingPolicyStateSettings, (ThrottlingPolicyOwaSettings settings) => settings.VoiceMaxConcurrency, delegate(ThrottlingPolicyOwaSettings settings, object value)
		{
			settings.VoiceMaxConcurrency = (Unlimited<uint>?)value;
		});

		// Token: 0x04003073 RID: 12403
		public static readonly ADPropertyDefinition OwaVoiceMaxBurst = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyOwaSettings>("OwaVoiceMaxBurst", ThrottlingPolicySchema.OwaThrottlingPolicyStateSettings, (ThrottlingPolicyOwaSettings settings) => settings.VoiceMaxBurst, delegate(ThrottlingPolicyOwaSettings settings, object value)
		{
			settings.VoiceMaxBurst = (Unlimited<uint>?)value;
		});

		// Token: 0x04003074 RID: 12404
		public static readonly ADPropertyDefinition OwaVoiceRechargeRate = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyOwaSettings>("OwaVoiceRechargeRate", ThrottlingPolicySchema.OwaThrottlingPolicyStateSettings, (ThrottlingPolicyOwaSettings settings) => settings.VoiceRechargeRate, delegate(ThrottlingPolicyOwaSettings settings, object value)
		{
			settings.VoiceRechargeRate = (Unlimited<uint>?)value;
		});

		// Token: 0x04003075 RID: 12405
		public static readonly ADPropertyDefinition OwaVoiceCutoffBalance = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyOwaSettings>("OwaVoiceCutoffBalance", ThrottlingPolicySchema.OwaThrottlingPolicyStateSettings, (ThrottlingPolicyOwaSettings settings) => settings.VoiceCutoffBalance, delegate(ThrottlingPolicyOwaSettings settings, object value)
		{
			settings.VoiceCutoffBalance = (Unlimited<uint>?)value;
		});

		// Token: 0x04003076 RID: 12406
		private static readonly ADPropertyDefinition PopThrottlingPolicyStateSettings = ThrottlingPolicySchema.BuildSettingsProperty<ThrottlingPolicyPopSettings>("PopThrottlingPolicyStateSettings", "msExchPopThrottlingPolicyStateEx");

		// Token: 0x04003077 RID: 12407
		public static readonly ADPropertyDefinition PopMaxConcurrency = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPopSettings>("PopMaxConcurrency", ThrottlingPolicySchema.PopThrottlingPolicyStateSettings, (ThrottlingPolicyPopSettings settings) => settings.MaxConcurrency, delegate(ThrottlingPolicyPopSettings settings, object value)
		{
			settings.MaxConcurrency = (Unlimited<uint>?)value;
		});

		// Token: 0x04003078 RID: 12408
		public static readonly ADPropertyDefinition PopMaxBurst = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPopSettings>("PopMaxBurst", ThrottlingPolicySchema.PopThrottlingPolicyStateSettings, (ThrottlingPolicyPopSettings settings) => settings.MaxBurst, delegate(ThrottlingPolicyPopSettings settings, object value)
		{
			settings.MaxBurst = (Unlimited<uint>?)value;
		});

		// Token: 0x04003079 RID: 12409
		public static readonly ADPropertyDefinition PopRechargeRate = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPopSettings>("PopRechargeRate", ThrottlingPolicySchema.PopThrottlingPolicyStateSettings, (ThrottlingPolicyPopSettings settings) => settings.RechargeRate, delegate(ThrottlingPolicyPopSettings settings, object value)
		{
			settings.RechargeRate = (Unlimited<uint>?)value;
		});

		// Token: 0x0400307A RID: 12410
		public static readonly ADPropertyDefinition PopCutoffBalance = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPopSettings>("PopCutoffBalance", ThrottlingPolicySchema.PopThrottlingPolicyStateSettings, (ThrottlingPolicyPopSettings settings) => settings.CutoffBalance, delegate(ThrottlingPolicyPopSettings settings, object value)
		{
			settings.CutoffBalance = (Unlimited<uint>?)value;
		});

		// Token: 0x0400307B RID: 12411
		private static readonly ADPropertyDefinition PowerShellThrottlingPolicyStateSettings = ThrottlingPolicySchema.BuildSettingsProperty<ThrottlingPolicyPowerShellSettings>("PowerShellThrottlingPolicyStateSettings", "msExchPowershellThrottlingPolicyStateEx");

		// Token: 0x0400307C RID: 12412
		public static readonly ADPropertyDefinition PowerShellMaxConcurrency = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPowerShellSettings>("PowerShellMaxConcurrency", ThrottlingPolicySchema.PowerShellThrottlingPolicyStateSettings, (ThrottlingPolicyPowerShellSettings settings) => settings.MaxConcurrency, delegate(ThrottlingPolicyPowerShellSettings settings, object value)
		{
			settings.MaxConcurrency = (Unlimited<uint>?)value;
		});

		// Token: 0x0400307D RID: 12413
		public static readonly ADPropertyDefinition PowerShellMaxTenantConcurrency = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPowerShellSettings>("PowerShellMaxTenantConcurrency", ThrottlingPolicySchema.PowerShellThrottlingPolicyStateSettings, (ThrottlingPolicyPowerShellSettings settings) => settings.MaxTenantConcurrency, delegate(ThrottlingPolicyPowerShellSettings settings, object value)
		{
			settings.MaxTenantConcurrency = (Unlimited<uint>?)value;
		});

		// Token: 0x0400307E RID: 12414
		public static readonly ADPropertyDefinition PowerShellMaxBurst = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPowerShellSettings>("PowerShellMaxBurst", ThrottlingPolicySchema.PowerShellThrottlingPolicyStateSettings, (ThrottlingPolicyPowerShellSettings settings) => settings.MaxBurst, delegate(ThrottlingPolicyPowerShellSettings settings, object value)
		{
			settings.MaxBurst = (Unlimited<uint>?)value;
		});

		// Token: 0x0400307F RID: 12415
		public static readonly ADPropertyDefinition PowerShellRechargeRate = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPowerShellSettings>("PowerShellRechargeRate", ThrottlingPolicySchema.PowerShellThrottlingPolicyStateSettings, (ThrottlingPolicyPowerShellSettings settings) => settings.RechargeRate, delegate(ThrottlingPolicyPowerShellSettings settings, object value)
		{
			settings.RechargeRate = (Unlimited<uint>?)value;
		});

		// Token: 0x04003080 RID: 12416
		public static readonly ADPropertyDefinition PowerShellCutoffBalance = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPowerShellSettings>("PowerShellCutoffBalance", ThrottlingPolicySchema.PowerShellThrottlingPolicyStateSettings, (ThrottlingPolicyPowerShellSettings settings) => settings.CutoffBalance, delegate(ThrottlingPolicyPowerShellSettings settings, object value)
		{
			settings.CutoffBalance = (Unlimited<uint>?)value;
		});

		// Token: 0x04003081 RID: 12417
		public static readonly ADPropertyDefinition PowerShellMaxOperations = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPowerShellSettings>("PowerShellMaxOperations", ThrottlingPolicySchema.PowerShellThrottlingPolicyStateSettings, (ThrottlingPolicyPowerShellSettings settings) => settings.MaxOperations, delegate(ThrottlingPolicyPowerShellSettings settings, object value)
		{
			settings.MaxOperations = (Unlimited<uint>?)value;
		});

		// Token: 0x04003082 RID: 12418
		public static readonly ADPropertyDefinition PowerShellMaxCmdletsTimePeriod = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPowerShellSettings>("PowerShellMaxCmdletsTimePeriod", ThrottlingPolicySchema.PowerShellThrottlingPolicyStateSettings, (ThrottlingPolicyPowerShellSettings settings) => settings.MaxCmdletsTimePeriod, delegate(ThrottlingPolicyPowerShellSettings settings, object value)
		{
			settings.MaxCmdletsTimePeriod = (Unlimited<uint>?)value;
		});

		// Token: 0x04003083 RID: 12419
		public static readonly ADPropertyDefinition ExchangeMaxCmdlets = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPowerShellSettings>("ExchangeMaxCmdlets", ThrottlingPolicySchema.PowerShellThrottlingPolicyStateSettings, (ThrottlingPolicyPowerShellSettings settings) => settings.ExchangeMaxCmdlets, delegate(ThrottlingPolicyPowerShellSettings settings, object value)
		{
			settings.ExchangeMaxCmdlets = (Unlimited<uint>?)value;
		});

		// Token: 0x04003084 RID: 12420
		public static readonly ADPropertyDefinition PowerShellMaxCmdletQueueDepth = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPowerShellSettings>("PowerShellMaxCmdletQueueDepth", ThrottlingPolicySchema.PowerShellThrottlingPolicyStateSettings, (ThrottlingPolicyPowerShellSettings settings) => settings.MaxCmdletQueueDepth, delegate(ThrottlingPolicyPowerShellSettings settings, object value)
		{
			settings.MaxCmdletQueueDepth = (Unlimited<uint>?)value;
		});

		// Token: 0x04003085 RID: 12421
		public static readonly ADPropertyDefinition PowerShellMaxDestructiveCmdlets = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPowerShellSettings>("PowerShellMaxDestructiveCmdlets", ThrottlingPolicySchema.PowerShellThrottlingPolicyStateSettings, (ThrottlingPolicyPowerShellSettings settings) => settings.MaxDestructiveCmdlets, delegate(ThrottlingPolicyPowerShellSettings settings, object value)
		{
			settings.MaxDestructiveCmdlets = (Unlimited<uint>?)value;
		});

		// Token: 0x04003086 RID: 12422
		public static readonly ADPropertyDefinition PowerShellMaxDestructiveCmdletsTimePeriod = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPowerShellSettings>("PowerShellMaxDestructiveCmdletsTimePeriod", ThrottlingPolicySchema.PowerShellThrottlingPolicyStateSettings, (ThrottlingPolicyPowerShellSettings settings) => settings.MaxDestructiveCmdletsTimePeriod, delegate(ThrottlingPolicyPowerShellSettings settings, object value)
		{
			settings.MaxDestructiveCmdletsTimePeriod = (Unlimited<uint>?)value;
		});

		// Token: 0x04003087 RID: 12423
		public static readonly ADPropertyDefinition PowerShellMaxCmdlets = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPowerShellSettings>("PowerShellMaxCmdlets", ThrottlingPolicySchema.PowerShellThrottlingPolicyStateSettings, (ThrottlingPolicyPowerShellSettings settings) => settings.MaxCmdlets, delegate(ThrottlingPolicyPowerShellSettings settings, object value)
		{
			settings.MaxCmdlets = (Unlimited<uint>?)value;
		});

		// Token: 0x04003088 RID: 12424
		public static readonly ADPropertyDefinition PowerShellMaxRunspaces = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPowerShellSettings>("PowerShellMaxRunspaces", ThrottlingPolicySchema.PowerShellThrottlingPolicyStateSettings, (ThrottlingPolicyPowerShellSettings settings) => settings.MaxRunspaces, delegate(ThrottlingPolicyPowerShellSettings settings, object value)
		{
			settings.MaxRunspaces = (Unlimited<uint>?)value;
		});

		// Token: 0x04003089 RID: 12425
		public static readonly ADPropertyDefinition PowerShellMaxTenantRunspaces = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPowerShellSettings>("PowerShellMaxTenantRunspaces", ThrottlingPolicySchema.PowerShellThrottlingPolicyStateSettings, (ThrottlingPolicyPowerShellSettings settings) => settings.MaxTenantRunspaces, delegate(ThrottlingPolicyPowerShellSettings settings, object value)
		{
			settings.MaxTenantRunspaces = (Unlimited<uint>?)value;
		});

		// Token: 0x0400308A RID: 12426
		public static readonly ADPropertyDefinition PowerShellMaxRunspacesTimePeriod = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPowerShellSettings>("PowerShellMaxRunspacesTimePeriod", ThrottlingPolicySchema.PowerShellThrottlingPolicyStateSettings, (ThrottlingPolicyPowerShellSettings settings) => settings.MaxRunspacesTimePeriod, delegate(ThrottlingPolicyPowerShellSettings settings, object value)
		{
			settings.MaxRunspacesTimePeriod = (Unlimited<uint>?)value;
		});

		// Token: 0x0400308B RID: 12427
		public static readonly ADPropertyDefinition PowerShellThrottlingBackup = new ADPropertyDefinition("PowerShellThrottlingBackup", ExchangeObjectVersion.Exchange2003, typeof(string), "adminDescription", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400308C RID: 12428
		public static readonly ADPropertyDefinition PswsMaxConcurrency = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPowerShellSettings>("PswsMaxConcurrency", ThrottlingPolicySchema.PowerShellThrottlingPolicyStateSettings, (ThrottlingPolicyPowerShellSettings settings) => settings.PswsMaxConcurrency, delegate(ThrottlingPolicyPowerShellSettings settings, object value)
		{
			settings.PswsMaxConcurrency = (Unlimited<uint>?)value;
		});

		// Token: 0x0400308D RID: 12429
		public static readonly ADPropertyDefinition PswsMaxRequest = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPowerShellSettings>("PswsMaxRequest", ThrottlingPolicySchema.PowerShellThrottlingPolicyStateSettings, (ThrottlingPolicyPowerShellSettings settings) => settings.PswsMaxRequest, delegate(ThrottlingPolicyPowerShellSettings settings, object value)
		{
			settings.PswsMaxRequest = (Unlimited<uint>?)value;
		});

		// Token: 0x0400308E RID: 12430
		public static readonly ADPropertyDefinition PswsMaxRequestTimePeriod = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPowerShellSettings>("PswsMaxRequestTimePeriod", ThrottlingPolicySchema.PowerShellThrottlingPolicyStateSettings, (ThrottlingPolicyPowerShellSettings settings) => settings.PswsMaxRequestTimePeriod, delegate(ThrottlingPolicyPowerShellSettings settings, object value)
		{
			settings.PswsMaxRequestTimePeriod = (Unlimited<uint>?)value;
		});

		// Token: 0x0400308F RID: 12431
		private static readonly ADPropertyDefinition RcaThrottlingPolicyStateSettings = ThrottlingPolicySchema.BuildSettingsProperty<ThrottlingPolicyRcaSettings>("RcaThrottlingPolicyStateSettings", "msExchRcaThrottlingPolicyStateEx");

		// Token: 0x04003090 RID: 12432
		public static readonly ADPropertyDefinition RcaMaxConcurrency = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyRcaSettings>("RcaMaxConcurrency", ThrottlingPolicySchema.RcaThrottlingPolicyStateSettings, (ThrottlingPolicyRcaSettings settings) => settings.MaxConcurrency, delegate(ThrottlingPolicyRcaSettings settings, object value)
		{
			settings.MaxConcurrency = (Unlimited<uint>?)value;
		});

		// Token: 0x04003091 RID: 12433
		public static readonly ADPropertyDefinition RcaMaxBurst = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyRcaSettings>("RcaMaxBurst", ThrottlingPolicySchema.RcaThrottlingPolicyStateSettings, (ThrottlingPolicyRcaSettings settings) => settings.MaxBurst, delegate(ThrottlingPolicyRcaSettings settings, object value)
		{
			settings.MaxBurst = (Unlimited<uint>?)value;
		});

		// Token: 0x04003092 RID: 12434
		public static readonly ADPropertyDefinition RcaRechargeRate = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyRcaSettings>("RcaRechargeRate", ThrottlingPolicySchema.RcaThrottlingPolicyStateSettings, (ThrottlingPolicyRcaSettings settings) => settings.RechargeRate, delegate(ThrottlingPolicyRcaSettings settings, object value)
		{
			settings.RechargeRate = (Unlimited<uint>?)value;
		});

		// Token: 0x04003093 RID: 12435
		public static readonly ADPropertyDefinition RcaCutoffBalance = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyRcaSettings>("RcaCutoffBalance", ThrottlingPolicySchema.RcaThrottlingPolicyStateSettings, (ThrottlingPolicyRcaSettings settings) => settings.CutoffBalance, delegate(ThrottlingPolicyRcaSettings settings, object value)
		{
			settings.CutoffBalance = (Unlimited<uint>?)value;
		});

		// Token: 0x04003094 RID: 12436
		public static readonly ADPropertyDefinition CpaMaxConcurrency = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyRcaSettings>("CpaMaxConcurrency", ThrottlingPolicySchema.RcaThrottlingPolicyStateSettings, (ThrottlingPolicyRcaSettings settings) => settings.CpaMaxConcurrency, delegate(ThrottlingPolicyRcaSettings settings, object value)
		{
			settings.CpaMaxConcurrency = (Unlimited<uint>?)value;
		});

		// Token: 0x04003095 RID: 12437
		public static readonly ADPropertyDefinition CpaMaxBurst = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyRcaSettings>("CpaMaxBurst", ThrottlingPolicySchema.RcaThrottlingPolicyStateSettings, (ThrottlingPolicyRcaSettings settings) => settings.CpaMaxBurst, delegate(ThrottlingPolicyRcaSettings settings, object value)
		{
			settings.CpaMaxBurst = (Unlimited<uint>?)value;
		});

		// Token: 0x04003096 RID: 12438
		public static readonly ADPropertyDefinition CpaRechargeRate = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyRcaSettings>("CpaRechargeRate", ThrottlingPolicySchema.RcaThrottlingPolicyStateSettings, (ThrottlingPolicyRcaSettings settings) => settings.CpaRechargeRate, delegate(ThrottlingPolicyRcaSettings settings, object value)
		{
			settings.CpaRechargeRate = (Unlimited<uint>?)value;
		});

		// Token: 0x04003097 RID: 12439
		public static readonly ADPropertyDefinition CpaCutoffBalance = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyRcaSettings>("CpaCutoffBalance", ThrottlingPolicySchema.RcaThrottlingPolicyStateSettings, (ThrottlingPolicyRcaSettings settings) => settings.CpaCutoffBalance, delegate(ThrottlingPolicyRcaSettings settings, object value)
		{
			settings.CpaCutoffBalance = (Unlimited<uint>?)value;
		});

		// Token: 0x04003098 RID: 12440
		private static readonly ADPropertyDefinition GeneralThrottlingPolicyStateSettings = ThrottlingPolicySchema.BuildSettingsProperty<ThrottlingPolicyGeneralSettings>("GeneralThrottlingPolicyStateSettings", "msExchGeneralThrottlingPolicyStateEx");

		// Token: 0x04003099 RID: 12441
		public static readonly ADPropertyDefinition MessageRateLimit = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyGeneralSettings>("MessageRateLimit", ThrottlingPolicySchema.MessageRateLimitWriteConstraint, ThrottlingPolicySchema.GeneralThrottlingPolicyStateSettings, (ThrottlingPolicyGeneralSettings settings) => settings.MessageRateLimit, delegate(ThrottlingPolicyGeneralSettings settings, object value)
		{
			settings.MessageRateLimit = (Unlimited<uint>?)value;
		});

		// Token: 0x0400309A RID: 12442
		public static readonly ADPropertyDefinition RecipientRateLimit = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyGeneralSettings>("RecipientRateLimit", ThrottlingPolicySchema.RecipientRateLimitWriteConstraint, ThrottlingPolicySchema.GeneralThrottlingPolicyStateSettings, (ThrottlingPolicyGeneralSettings settings) => settings.RecipientRateLimit, delegate(ThrottlingPolicyGeneralSettings settings, object value)
		{
			settings.RecipientRateLimit = (Unlimited<uint>?)value;
		});

		// Token: 0x0400309B RID: 12443
		public static readonly ADPropertyDefinition ForwardeeLimit = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyGeneralSettings>("ForwardeeLimit", ThrottlingPolicySchema.ForwardeeLimitWriteConstraint, ThrottlingPolicySchema.GeneralThrottlingPolicyStateSettings, (ThrottlingPolicyGeneralSettings settings) => settings.ForwardeeLimit, delegate(ThrottlingPolicyGeneralSettings settings, object value)
		{
			settings.ForwardeeLimit = (Unlimited<uint>?)value;
		});

		// Token: 0x0400309C RID: 12444
		public static readonly ADPropertyDefinition DiscoveryMaxConcurrency = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyGeneralSettings>("DiscoveryMaxConcurrency", ThrottlingPolicySchema.GeneralThrottlingPolicyStateSettings, (ThrottlingPolicyGeneralSettings settings) => settings.DiscoveryMaxConcurrency, delegate(ThrottlingPolicyGeneralSettings settings, object value)
		{
			settings.DiscoveryMaxConcurrency = (Unlimited<uint>?)value;
		});

		// Token: 0x0400309D RID: 12445
		public static readonly ADPropertyDefinition DiscoveryMaxMailboxes = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyGeneralSettings>("DiscoveryMaxMailboxes", ThrottlingPolicySchema.GeneralThrottlingPolicyStateSettings, (ThrottlingPolicyGeneralSettings settings) => settings.DiscoveryMaxMailboxes, delegate(ThrottlingPolicyGeneralSettings settings, object value)
		{
			settings.DiscoveryMaxMailboxes = (Unlimited<uint>?)value;
		});

		// Token: 0x0400309E RID: 12446
		public static readonly ADPropertyDefinition DiscoveryMaxKeywords = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyGeneralSettings>("DiscoveryMaxKeywords", ThrottlingPolicySchema.GeneralThrottlingPolicyStateSettings, (ThrottlingPolicyGeneralSettings settings) => settings.DiscoveryMaxKeywords, delegate(ThrottlingPolicyGeneralSettings settings, object value)
		{
			settings.DiscoveryMaxKeywords = (Unlimited<uint>?)value;
		});

		// Token: 0x0400309F RID: 12447
		public static readonly ADPropertyDefinition DiscoveryMaxPreviewSearchMailboxes = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyGeneralSettings>("DiscoveryMaxPreviewSearchMailboxes", ThrottlingPolicySchema.GeneralThrottlingPolicyStateSettings, (ThrottlingPolicyGeneralSettings settings) => settings.DiscoveryMaxPreviewSearchMailboxes, delegate(ThrottlingPolicyGeneralSettings settings, object value)
		{
			settings.DiscoveryMaxPreviewSearchMailboxes = (Unlimited<uint>?)value;
		});

		// Token: 0x040030A0 RID: 12448
		public static readonly ADPropertyDefinition DiscoveryMaxStatsSearchMailboxes = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyGeneralSettings>("DiscoveryMaxStatsSearchMailboxes", ThrottlingPolicySchema.GeneralThrottlingPolicyStateSettings, (ThrottlingPolicyGeneralSettings settings) => settings.DiscoveryMaxStatsSearchMailboxes, delegate(ThrottlingPolicyGeneralSettings settings, object value)
		{
			settings.DiscoveryMaxStatsSearchMailboxes = (Unlimited<uint>?)value;
		});

		// Token: 0x040030A1 RID: 12449
		public static readonly ADPropertyDefinition DiscoveryPreviewSearchResultsPageSize = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyGeneralSettings>("DiscoveryPreviewSearchResultsPageSize", ThrottlingPolicySchema.GeneralThrottlingPolicyStateSettings, (ThrottlingPolicyGeneralSettings settings) => settings.DiscoveryPreviewSearchResultsPageSize, delegate(ThrottlingPolicyGeneralSettings settings, object value)
		{
			settings.DiscoveryPreviewSearchResultsPageSize = (Unlimited<uint>?)value;
		});

		// Token: 0x040030A2 RID: 12450
		public static readonly ADPropertyDefinition DiscoveryMaxKeywordsPerPage = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyGeneralSettings>("DiscoveryMaxKeywordsPerPage", ThrottlingPolicySchema.GeneralThrottlingPolicyStateSettings, (ThrottlingPolicyGeneralSettings settings) => settings.DiscoveryMaxKeywordsPerPage, delegate(ThrottlingPolicyGeneralSettings settings, object value)
		{
			settings.DiscoveryMaxKeywordsPerPage = (Unlimited<uint>?)value;
		});

		// Token: 0x040030A3 RID: 12451
		public static readonly ADPropertyDefinition DiscoveryMaxRefinerResults = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyGeneralSettings>("DiscoveryMaxRefinerResults", ThrottlingPolicySchema.GeneralThrottlingPolicyStateSettings, (ThrottlingPolicyGeneralSettings settings) => settings.DiscoveryMaxRefinerResults, delegate(ThrottlingPolicyGeneralSettings settings, object value)
		{
			settings.DiscoveryMaxRefinerResults = (Unlimited<uint>?)value;
		});

		// Token: 0x040030A4 RID: 12452
		public static readonly ADPropertyDefinition DiscoveryMaxSearchQueueDepth = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyGeneralSettings>("DiscoveryMaxSearchQueueDepth", ThrottlingPolicySchema.GeneralThrottlingPolicyStateSettings, (ThrottlingPolicyGeneralSettings settings) => settings.DiscoveryMaxSearchQueueDepth, delegate(ThrottlingPolicyGeneralSettings settings, object value)
		{
			settings.DiscoveryMaxSearchQueueDepth = (Unlimited<uint>?)value;
		});

		// Token: 0x040030A5 RID: 12453
		public static readonly ADPropertyDefinition DiscoverySearchTimeoutPeriod = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyGeneralSettings>("DiscoverySearchTimeoutPeriod", ThrottlingPolicySchema.GeneralThrottlingPolicyStateSettings, (ThrottlingPolicyGeneralSettings settings) => settings.DiscoverySearchTimeoutPeriod, delegate(ThrottlingPolicyGeneralSettings settings, object value)
		{
			settings.DiscoverySearchTimeoutPeriod = (Unlimited<uint>?)value;
		});

		// Token: 0x040030A6 RID: 12454
		public static readonly ADPropertyDefinition ComplianceMaxExpansionDGRecipients = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyGeneralSettings>("ComplianceMaxExpansionDGRecipients", ThrottlingPolicySchema.GeneralThrottlingPolicyStateSettings, (ThrottlingPolicyGeneralSettings settings) => settings.ComplianceMaxExpansionDGRecipients, delegate(ThrottlingPolicyGeneralSettings settings, object value)
		{
			settings.ComplianceMaxExpansionDGRecipients = (Unlimited<uint>?)value;
		});

		// Token: 0x040030A7 RID: 12455
		public static readonly ADPropertyDefinition ComplianceMaxExpansionNestedDGs = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyGeneralSettings>("ComplianceMaxExpansionNestedDGs", ThrottlingPolicySchema.GeneralThrottlingPolicyStateSettings, (ThrottlingPolicyGeneralSettings settings) => settings.ComplianceMaxExpansionNestedDGs, delegate(ThrottlingPolicyGeneralSettings settings, object value)
		{
			settings.ComplianceMaxExpansionNestedDGs = (Unlimited<uint>?)value;
		});

		// Token: 0x040030A8 RID: 12456
		private static readonly ADPropertyDefinition PushNotificationThrottlingPolicyStateSettings = ThrottlingPolicySchema.BuildSettingsProperty<ThrottlingPolicyPushNotificationSettings>("PushNotificationThrottlingPolicyStateSettings", "msExchPushNotificationsThrottlingPolicyStateEx");

		// Token: 0x040030A9 RID: 12457
		public static readonly ADPropertyDefinition PushNotificationMaxConcurrency = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPushNotificationSettings>("PushNotificationMaxConcurrency", ThrottlingPolicySchema.PushNotificationThrottlingPolicyStateSettings, (ThrottlingPolicyPushNotificationSettings settings) => settings.MaxConcurrency, delegate(ThrottlingPolicyPushNotificationSettings settings, object value)
		{
			settings.MaxConcurrency = (Unlimited<uint>?)value;
		});

		// Token: 0x040030AA RID: 12458
		public static readonly ADPropertyDefinition PushNotificationMaxBurst = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPushNotificationSettings>("PushNotificationMaxBurst", ThrottlingPolicySchema.PushNotificationThrottlingPolicyStateSettings, (ThrottlingPolicyPushNotificationSettings settings) => settings.MaxBurst, delegate(ThrottlingPolicyPushNotificationSettings settings, object value)
		{
			settings.MaxBurst = (Unlimited<uint>?)value;
		});

		// Token: 0x040030AB RID: 12459
		public static readonly ADPropertyDefinition PushNotificationRechargeRate = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPushNotificationSettings>("PushNotificationRechargeRate", ThrottlingPolicySchema.PushNotificationThrottlingPolicyStateSettings, (ThrottlingPolicyPushNotificationSettings settings) => settings.RechargeRate, delegate(ThrottlingPolicyPushNotificationSettings settings, object value)
		{
			settings.RechargeRate = (Unlimited<uint>?)value;
		});

		// Token: 0x040030AC RID: 12460
		public static readonly ADPropertyDefinition PushNotificationCutoffBalance = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPushNotificationSettings>("PushNotificationCutoffBalance", ThrottlingPolicySchema.PushNotificationThrottlingPolicyStateSettings, (ThrottlingPolicyPushNotificationSettings settings) => settings.CutoffBalance, delegate(ThrottlingPolicyPushNotificationSettings settings, object value)
		{
			settings.CutoffBalance = (Unlimited<uint>?)value;
		});

		// Token: 0x040030AD RID: 12461
		public static readonly ADPropertyDefinition PushNotificationMaxBurstPerDevice = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPushNotificationSettings>("PushNotificationMaximumLimitPerDevice", ThrottlingPolicySchema.PushNotificationThrottlingPolicyStateSettings, (ThrottlingPolicyPushNotificationSettings settings) => settings.PushNotificationMaxBurstPerDevice, delegate(ThrottlingPolicyPushNotificationSettings settings, object value)
		{
			settings.PushNotificationMaxBurstPerDevice = (Unlimited<uint>?)value;
		});

		// Token: 0x040030AE RID: 12462
		public static readonly ADPropertyDefinition PushNotificationRechargeRatePerDevice = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPushNotificationSettings>("PushNotificationRechargeRatePerDevice", ThrottlingPolicySchema.PushNotificationThrottlingPolicyStateSettings, (ThrottlingPolicyPushNotificationSettings settings) => settings.PushNotificationRechargeRatePerDevice, delegate(ThrottlingPolicyPushNotificationSettings settings, object value)
		{
			settings.PushNotificationRechargeRatePerDevice = (Unlimited<uint>?)value;
		});

		// Token: 0x040030AF RID: 12463
		public static readonly ADPropertyDefinition PushNotificationSamplingPeriodPerDevice = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyPushNotificationSettings>("PushNotificationSamplingPeriodPerDevice", ThrottlingPolicySchema.PushNotificationThrottlingPolicyStateSettings, (ThrottlingPolicyPushNotificationSettings settings) => settings.PushNotificationSamplingPeriodPerDevice, delegate(ThrottlingPolicyPushNotificationSettings settings, object value)
		{
			settings.PushNotificationSamplingPeriodPerDevice = (Unlimited<uint>?)value;
		});

		// Token: 0x040030B0 RID: 12464
		private static readonly ADPropertyDefinition E4eThrottlingPolicyStateSettings = ThrottlingPolicySchema.BuildSettingsProperty<ThrottlingPolicyE4eSettings>("E4eThrottlingPolicyStateSettings", "msExchEncryptionThrottlingPolicyStateEx");

		// Token: 0x040030B1 RID: 12465
		public static readonly ADPropertyDefinition EncryptionSenderMaxConcurrency = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyE4eSettings>("EncryptionSenderMaxConcurrency", ThrottlingPolicySchema.E4eThrottlingPolicyStateSettings, (ThrottlingPolicyE4eSettings settings) => settings.EncryptionSenderMaxConcurrency, delegate(ThrottlingPolicyE4eSettings settings, object value)
		{
			settings.EncryptionSenderMaxConcurrency = (Unlimited<uint>?)value;
		});

		// Token: 0x040030B2 RID: 12466
		public static readonly ADPropertyDefinition EncryptionSenderMaxBurst = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyE4eSettings>("EncryptionSenderMaxBurst", ThrottlingPolicySchema.E4eThrottlingPolicyStateSettings, (ThrottlingPolicyE4eSettings settings) => settings.EncryptionSenderMaxBurst, delegate(ThrottlingPolicyE4eSettings settings, object value)
		{
			settings.EncryptionSenderMaxBurst = (Unlimited<uint>?)value;
		});

		// Token: 0x040030B3 RID: 12467
		public static readonly ADPropertyDefinition EncryptionSenderRechargeRate = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyE4eSettings>("EncryptionSenderRechargeRate", ThrottlingPolicySchema.E4eThrottlingPolicyStateSettings, (ThrottlingPolicyE4eSettings settings) => settings.EncryptionSenderRechargeRate, delegate(ThrottlingPolicyE4eSettings settings, object value)
		{
			settings.EncryptionSenderRechargeRate = (Unlimited<uint>?)value;
		});

		// Token: 0x040030B4 RID: 12468
		public static readonly ADPropertyDefinition EncryptionSenderCutoffBalance = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyE4eSettings>("EncryptionSenderCutoffBalance", ThrottlingPolicySchema.E4eThrottlingPolicyStateSettings, (ThrottlingPolicyE4eSettings settings) => settings.EncryptionSenderCutoffBalance, delegate(ThrottlingPolicyE4eSettings settings, object value)
		{
			settings.EncryptionSenderCutoffBalance = (Unlimited<uint>?)value;
		});

		// Token: 0x040030B5 RID: 12469
		public static readonly ADPropertyDefinition EncryptionRecipientMaxConcurrency = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyE4eSettings>("EncryptionRecipientMaxConcurrency", ThrottlingPolicySchema.E4eThrottlingPolicyStateSettings, (ThrottlingPolicyE4eSettings settings) => settings.EncryptionRecipientMaxConcurrency, delegate(ThrottlingPolicyE4eSettings settings, object value)
		{
			settings.EncryptionRecipientMaxConcurrency = (Unlimited<uint>?)value;
		});

		// Token: 0x040030B6 RID: 12470
		public static readonly ADPropertyDefinition EncryptionRecipientMaxBurst = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyE4eSettings>("EncryptionRecipientMaxBurst", ThrottlingPolicySchema.E4eThrottlingPolicyStateSettings, (ThrottlingPolicyE4eSettings settings) => settings.EncryptionRecipientMaxBurst, delegate(ThrottlingPolicyE4eSettings settings, object value)
		{
			settings.EncryptionRecipientMaxBurst = (Unlimited<uint>?)value;
		});

		// Token: 0x040030B7 RID: 12471
		public static readonly ADPropertyDefinition EncryptionRecipientRechargeRate = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyE4eSettings>("EncryptionRecipientRechargeRate", ThrottlingPolicySchema.E4eThrottlingPolicyStateSettings, (ThrottlingPolicyE4eSettings settings) => settings.EncryptionRecipientRechargeRate, delegate(ThrottlingPolicyE4eSettings settings, object value)
		{
			settings.EncryptionRecipientRechargeRate = (Unlimited<uint>?)value;
		});

		// Token: 0x040030B8 RID: 12472
		public static readonly ADPropertyDefinition EncryptionRecipientCutoffBalance = ThrottlingPolicySchema.BuildCalculatedSettingProperty<ThrottlingPolicyE4eSettings>("EncryptionRecipientCutoffBalance", ThrottlingPolicySchema.E4eThrottlingPolicyStateSettings, (ThrottlingPolicyE4eSettings settings) => settings.EncryptionRecipientCutoffBalance, delegate(ThrottlingPolicyE4eSettings settings, object value)
		{
			settings.EncryptionRecipientCutoffBalance = (Unlimited<uint>?)value;
		});

		// Token: 0x040030B9 RID: 12473
		public static readonly ADPropertyDefinition ThrottlingPolicyFlags = new ADPropertyDefinition("ThrottlingPolicyFlags", ExchangeObjectVersion.Exchange2010, typeof(int), "msExchThrottlingPolicyFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, new PropertyDefinitionConstraint[]
		{
			new DelegateConstraint(new ValidationDelegate(ConstraintDelegates.ValidateThrottlingPolicyFlags))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040030BA RID: 12474
		public static readonly ADPropertyDefinition IsServiceAccount = new ADPropertyDefinition("IsServiceAccount", ExchangeObjectVersion.Exchange2010, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ThrottlingPolicySchema.ThrottlingPolicyFlags
		}, null, ADObject.FlagGetterDelegate(1, ThrottlingPolicySchema.ThrottlingPolicyFlags), ADObject.FlagSetterDelegate(1, ThrottlingPolicySchema.ThrottlingPolicyFlags), null, null);

		// Token: 0x040030BB RID: 12475
		public static readonly ADPropertyDefinition ThrottlingPolicyScope = new ADPropertyDefinition("ThrottlingPolicyScope", ExchangeObjectVersion.Exchange2010, typeof(ThrottlingPolicyScopeType), null, ADPropertyDefinitionFlags.Calculated, ThrottlingPolicyScopeType.Regular, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ThrottlingPolicySchema.ThrottlingPolicyFlags
		}, new CustomFilterBuilderDelegate(ThrottlingPolicySchema.ThrottlingPolicyScopeFilterBuilder), ThrottlingPolicySchema.ThrottlingPolicyScopeGetterDelegate(ThrottlingPolicySchema.ThrottlingPolicyFlags), ThrottlingPolicySchema.ThrottlingPolicyScopeSetterDelegate(ThrottlingPolicySchema.ThrottlingPolicyFlags), null, null);
	}
}
