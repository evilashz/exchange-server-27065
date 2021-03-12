using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200049F RID: 1183
	internal sealed class LegacyThrottlingPolicySchema : ADConfigurationObjectSchema
	{
		// Token: 0x0600360D RID: 13837 RVA: 0x000D46A8 File Offset: 0x000D28A8
		private static ADPropertyDefinition BuildSettingsProperty(string name, string ldapDisplayName)
		{
			return new ADPropertyDefinition(name, ExchangeObjectVersion.Exchange2010, typeof(LegacyThrottlingPolicySettings), ldapDisplayName, ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);
		}

		// Token: 0x0600360E RID: 13838 RVA: 0x000D4724 File Offset: 0x000D2924
		private static ADPropertyDefinition BuildCalculatedReadOnlySettingProperty(string name, string key, ADPropertyDefinition parentProperty)
		{
			return new ADPropertyDefinition(name, ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
			{
				parentProperty
			}, null, delegate(IPropertyBag propertyBag)
			{
				LegacyThrottlingPolicySettings legacyThrottlingPolicySettings = (LegacyThrottlingPolicySettings)propertyBag[parentProperty];
				string result;
				if (legacyThrottlingPolicySettings != null && legacyThrottlingPolicySettings.TryGetValue(key, out result))
				{
					return result;
				}
				return null;
			}, null, null, null);
		}

		// Token: 0x0400246F RID: 9327
		private const string MaxConcurrencyKey = "con";

		// Token: 0x04002470 RID: 9328
		public static readonly ADPropertyDefinition IsDefaultPolicy = new ADPropertyDefinition("IsDefaultPolicy", ExchangeObjectVersion.Exchange2010, typeof(bool), "msExchThrottlingIsDefaultPolicy", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002471 RID: 9329
		public static readonly ADPropertyDefinition AnonymousThrottlingPolicyStateSettings = LegacyThrottlingPolicySchema.BuildSettingsProperty("AnonymousThrottlingPolicyStateSettings", "msExchAnonymousThrottlingPolicyState");

		// Token: 0x04002472 RID: 9330
		public static readonly ADPropertyDefinition EasThrottlingPolicyStateSettings = LegacyThrottlingPolicySchema.BuildSettingsProperty("EasThrottlingPolicyStateSettings", "msExchEasThrottlingPolicyState");

		// Token: 0x04002473 RID: 9331
		public static readonly ADPropertyDefinition EwsThrottlingPolicyStateSettings = LegacyThrottlingPolicySchema.BuildSettingsProperty("EwsThrottlingPolicyStateSettings", "msExchEwsThrottlingPolicyState");

		// Token: 0x04002474 RID: 9332
		public static readonly ADPropertyDefinition ImapThrottlingPolicyStateSettings = LegacyThrottlingPolicySchema.BuildSettingsProperty("ImapThrottlingPolicyStateSettings", "msExchImapThrottlingPolicyState");

		// Token: 0x04002475 RID: 9333
		public static readonly ADPropertyDefinition OwaThrottlingPolicyStateSettings = LegacyThrottlingPolicySchema.BuildSettingsProperty("OwaThrottlingPolicyStateSettings", "msExchOwaThrottlingPolicyState");

		// Token: 0x04002476 RID: 9334
		public static readonly ADPropertyDefinition PopThrottlingPolicyStateSettings = LegacyThrottlingPolicySchema.BuildSettingsProperty("PopThrottlingPolicyStateSettings", "msExchPopThrottlingPolicyState");

		// Token: 0x04002477 RID: 9335
		public static readonly ADPropertyDefinition PowershellThrottlingPolicyStateSettings = LegacyThrottlingPolicySchema.BuildSettingsProperty("PowershellThrottlingPolicyStateSettings", "msExchPowershellThrottlingPolicyState");

		// Token: 0x04002478 RID: 9336
		public static readonly ADPropertyDefinition RcaThrottlingPolicyStateSettings = LegacyThrottlingPolicySchema.BuildSettingsProperty("RcaThrottlingPolicyStateSettings", "msExchRcaThrottlingPolicyState");

		// Token: 0x04002479 RID: 9337
		public static readonly ADPropertyDefinition GeneralThrottlingPolicyStateSettings = LegacyThrottlingPolicySchema.BuildSettingsProperty("GeneralThrottlingPolicyStateSettings", "msExchGeneralThrottlingPolicyState");

		// Token: 0x0400247A RID: 9338
		public static readonly ADPropertyDefinition AnonymousMaxConcurrency = LegacyThrottlingPolicySchema.BuildCalculatedReadOnlySettingProperty("AnonymousMaxConcurrency", "con", LegacyThrottlingPolicySchema.AnonymousThrottlingPolicyStateSettings);

		// Token: 0x0400247B RID: 9339
		public static readonly ADPropertyDefinition EasMaxConcurrency = LegacyThrottlingPolicySchema.BuildCalculatedReadOnlySettingProperty("EasMaxConcurrency", "con", LegacyThrottlingPolicySchema.EasThrottlingPolicyStateSettings);

		// Token: 0x0400247C RID: 9340
		public static readonly ADPropertyDefinition EasMaxDevices = LegacyThrottlingPolicySchema.BuildCalculatedReadOnlySettingProperty("EasMaxDevices", "md", LegacyThrottlingPolicySchema.EasThrottlingPolicyStateSettings);

		// Token: 0x0400247D RID: 9341
		public static readonly ADPropertyDefinition EasMaxDeviceDeletesPerMonth = LegacyThrottlingPolicySchema.BuildCalculatedReadOnlySettingProperty("EasMaxDeviceDeletesPerMonth", "mddpm", LegacyThrottlingPolicySchema.EasThrottlingPolicyStateSettings);

		// Token: 0x0400247E RID: 9342
		public static readonly ADPropertyDefinition EwsMaxConcurrency = LegacyThrottlingPolicySchema.BuildCalculatedReadOnlySettingProperty("EwsMaxConcurrency", "con", LegacyThrottlingPolicySchema.EwsThrottlingPolicyStateSettings);

		// Token: 0x0400247F RID: 9343
		public static readonly ADPropertyDefinition EwsMaxSubscriptions = LegacyThrottlingPolicySchema.BuildCalculatedReadOnlySettingProperty("EwsMaxSubscriptions", "sub", LegacyThrottlingPolicySchema.EwsThrottlingPolicyStateSettings);

		// Token: 0x04002480 RID: 9344
		public static readonly ADPropertyDefinition ImapMaxConcurrency = LegacyThrottlingPolicySchema.BuildCalculatedReadOnlySettingProperty("ImapMaxConcurrency", "con", LegacyThrottlingPolicySchema.ImapThrottlingPolicyStateSettings);

		// Token: 0x04002481 RID: 9345
		public static readonly ADPropertyDefinition OwaMaxConcurrency = LegacyThrottlingPolicySchema.BuildCalculatedReadOnlySettingProperty("OwaMaxConcurrency", "con", LegacyThrottlingPolicySchema.OwaThrottlingPolicyStateSettings);

		// Token: 0x04002482 RID: 9346
		public static readonly ADPropertyDefinition PopMaxConcurrency = LegacyThrottlingPolicySchema.BuildCalculatedReadOnlySettingProperty("PopMaxConcurrency", "con", LegacyThrottlingPolicySchema.PopThrottlingPolicyStateSettings);

		// Token: 0x04002483 RID: 9347
		public static readonly ADPropertyDefinition PowershellMaxConcurrency = LegacyThrottlingPolicySchema.BuildCalculatedReadOnlySettingProperty("PowershellMaxConcurrency", "con", LegacyThrottlingPolicySchema.PowershellThrottlingPolicyStateSettings);

		// Token: 0x04002484 RID: 9348
		public static readonly ADPropertyDefinition PowershellMaxTenantConcurrency = LegacyThrottlingPolicySchema.BuildCalculatedReadOnlySettingProperty("PowershellMaxTenantConcurrency", "ten", LegacyThrottlingPolicySchema.PowershellThrottlingPolicyStateSettings);

		// Token: 0x04002485 RID: 9349
		public static readonly ADPropertyDefinition PowerShellMaxCmdlets = LegacyThrottlingPolicySchema.BuildCalculatedReadOnlySettingProperty("PowerShellMaxCmdlets", "cmds", LegacyThrottlingPolicySchema.PowershellThrottlingPolicyStateSettings);

		// Token: 0x04002486 RID: 9350
		public static readonly ADPropertyDefinition PowershellMaxCmdletsTimePeriod = LegacyThrottlingPolicySchema.BuildCalculatedReadOnlySettingProperty("PowershellMaxCmdletsTimePeriod", "per", LegacyThrottlingPolicySchema.PowershellThrottlingPolicyStateSettings);

		// Token: 0x04002487 RID: 9351
		public static readonly ADPropertyDefinition PowershellMaxCmdletQueueDepth = LegacyThrottlingPolicySchema.BuildCalculatedReadOnlySettingProperty("PowershellMaxCmdletQueueDepth", "que", LegacyThrottlingPolicySchema.PowershellThrottlingPolicyStateSettings);

		// Token: 0x04002488 RID: 9352
		public static readonly ADPropertyDefinition ExchangeMaxCmdlets = LegacyThrottlingPolicySchema.BuildCalculatedReadOnlySettingProperty("ExchangeMaxCmdlets", "excmds", LegacyThrottlingPolicySchema.PowershellThrottlingPolicyStateSettings);

		// Token: 0x04002489 RID: 9353
		public static readonly ADPropertyDefinition PowershellMaxDestructiveCmdlets = LegacyThrottlingPolicySchema.BuildCalculatedReadOnlySettingProperty("PowershellMaxDestructiveCmdlets", "dscmds", LegacyThrottlingPolicySchema.PowershellThrottlingPolicyStateSettings);

		// Token: 0x0400248A RID: 9354
		public static readonly ADPropertyDefinition PowershellMaxDestructiveCmdletsTimePeriod = LegacyThrottlingPolicySchema.BuildCalculatedReadOnlySettingProperty("PowershellMaxDestructiveCmdletsTimePeriod", "dsper", LegacyThrottlingPolicySchema.PowershellThrottlingPolicyStateSettings);

		// Token: 0x0400248B RID: 9355
		public static readonly ADPropertyDefinition RcaMaxConcurrency = LegacyThrottlingPolicySchema.BuildCalculatedReadOnlySettingProperty("RcaMaxConcurrency", "con", LegacyThrottlingPolicySchema.RcaThrottlingPolicyStateSettings);

		// Token: 0x0400248C RID: 9356
		public static readonly ADPropertyDefinition CpaMaxConcurrency = LegacyThrottlingPolicySchema.BuildCalculatedReadOnlySettingProperty("CpaMaxConcurrency", "xcon", LegacyThrottlingPolicySchema.RcaThrottlingPolicyStateSettings);

		// Token: 0x0400248D RID: 9357
		public static readonly ADPropertyDefinition MessageRateLimit = LegacyThrottlingPolicySchema.BuildCalculatedReadOnlySettingProperty("MessageRateLimit", "mrl", LegacyThrottlingPolicySchema.GeneralThrottlingPolicyStateSettings);

		// Token: 0x0400248E RID: 9358
		public static readonly ADPropertyDefinition RecipientRateLimit = LegacyThrottlingPolicySchema.BuildCalculatedReadOnlySettingProperty("RecipientRateLimit", "rrl", LegacyThrottlingPolicySchema.GeneralThrottlingPolicyStateSettings);

		// Token: 0x0400248F RID: 9359
		public static readonly ADPropertyDefinition ForwardeeLimit = LegacyThrottlingPolicySchema.BuildCalculatedReadOnlySettingProperty("ForwardeeLimit", "fl", LegacyThrottlingPolicySchema.GeneralThrottlingPolicyStateSettings);

		// Token: 0x04002490 RID: 9360
		public static readonly ADPropertyDefinition DiscoveryMaxConcurrency = LegacyThrottlingPolicySchema.BuildCalculatedReadOnlySettingProperty("DiscoveryMaxConcurrency", "dmc", LegacyThrottlingPolicySchema.GeneralThrottlingPolicyStateSettings);

		// Token: 0x04002491 RID: 9361
		public static readonly ADPropertyDefinition DiscoveryMaxMailboxes = LegacyThrottlingPolicySchema.BuildCalculatedReadOnlySettingProperty("DiscoveryMaxMailboxes", "dmm", LegacyThrottlingPolicySchema.GeneralThrottlingPolicyStateSettings);

		// Token: 0x04002492 RID: 9362
		public static readonly ADPropertyDefinition DiscoveryMaxKeywords = LegacyThrottlingPolicySchema.BuildCalculatedReadOnlySettingProperty("DiscoveryMaxKeywords", "dmk", LegacyThrottlingPolicySchema.GeneralThrottlingPolicyStateSettings);
	}
}
