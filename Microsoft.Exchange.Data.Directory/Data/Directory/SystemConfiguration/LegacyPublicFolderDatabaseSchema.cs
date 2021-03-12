using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200049D RID: 1181
	internal sealed class LegacyPublicFolderDatabaseSchema : LegacyDatabaseSchema
	{
		// Token: 0x0400245B RID: 9307
		public static readonly ADPropertyDefinition Alias = ADRecipientSchema.Alias;

		// Token: 0x0400245C RID: 9308
		public static readonly ADPropertyDefinition EmailAddresses = ADRecipientSchema.EmailAddresses;

		// Token: 0x0400245D RID: 9309
		public static readonly ADPropertyDefinition FirstInstance = new ADPropertyDefinition("FirstInstance", ExchangeObjectVersion.Exchange2003, typeof(bool), "msExchFirstInstance", ADPropertyDefinitionFlags.PersistDefaultValue, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400245E RID: 9310
		public static readonly ADPropertyDefinition HomeMta = ADRecipientSchema.HomeMTA;

		// Token: 0x0400245F RID: 9311
		public static readonly ADPropertyDefinition MaxItemSize = new ADPropertyDefinition("MaxItemSize", ExchangeObjectVersion.Exchange2003, typeof(Unlimited<ByteQuantifiedSize>), ByteQuantifiedSize.KilobyteQuantifierProvider, "messageSizeLimit", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(0UL), ByteQuantifiedSize.FromKB(2097151UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002460 RID: 9312
		public static readonly ADPropertyDefinition ItemRetentionPeriod = new ADPropertyDefinition("ItemRetentionPeriod", ExchangeObjectVersion.Exchange2003, typeof(Unlimited<EnhancedTimeSpan>), "msExchOverallAgeLimit", ADPropertyDefinitionFlags.None, Unlimited<EnhancedTimeSpan>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new UnlimitedEnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002461 RID: 9313
		public static readonly ADPropertyDefinition ReplicationPeriod = new ADPropertyDefinition("ReplicationPeriod", ExchangeObjectVersion.Exchange2003, typeof(uint), "msExchPollInterval", ADPropertyDefinitionFlags.PersistDefaultValue, 15U, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<uint>(1U, 2147483647U)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002462 RID: 9314
		public static readonly ADPropertyDefinition PoliciesIncluded = ADRecipientSchema.PoliciesIncluded;

		// Token: 0x04002463 RID: 9315
		public static readonly ADPropertyDefinition PublicFolderHierarchy = SharedPropertyDefinitions.PublicFolderHierarchy;

		// Token: 0x04002464 RID: 9316
		public static readonly ADPropertyDefinition ReplicationMessageSize = new ADPropertyDefinition("ReplicationMessageSize", ExchangeObjectVersion.Exchange2003, typeof(ByteQuantifiedSize), ByteQuantifiedSize.KilobyteQuantifierProvider, "msExchReplicationMsgSize", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.FromKB(300UL), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(1UL), ByteQuantifiedSize.FromKB(2097151UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002465 RID: 9317
		public static readonly ADPropertyDefinition ReplicationMode = new ADPropertyDefinition("ReplicationMode", ExchangeObjectVersion.Exchange2003, typeof(ScheduleMode), "msExchReplicationStyle", ADPropertyDefinitionFlags.PersistDefaultValue, ScheduleMode.Always, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(ScheduleMode))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002466 RID: 9318
		public static readonly ADPropertyDefinition ReplicationScheduleBitmaps = SharedPropertyDefinitions.ReplicationScheduleBitmaps;

		// Token: 0x04002467 RID: 9319
		public static readonly ADPropertyDefinition ProhibitPostQuota = new ADPropertyDefinition("ProhibitPostQuota", ExchangeObjectVersion.Exchange2003, typeof(Unlimited<ByteQuantifiedSize>), ByteQuantifiedSize.KilobyteQuantifierProvider, "mDBOverQuotaLimit", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(0UL), ByteQuantifiedSize.FromKB(2147483647UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002468 RID: 9320
		public static readonly ADPropertyDefinition UseCustomReferralServerList = new ADPropertyDefinition("UseCustomReferralServerList", ExchangeObjectVersion.Exchange2003, typeof(bool), null, ADPropertyDefinitionFlags.TaskPopulated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002469 RID: 9321
		public static readonly ADPropertyDefinition CustomReferralServerList = new ADPropertyDefinition("CustomReferralServerList", ExchangeObjectVersion.Exchange2003, typeof(ServerCostPair), null, ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400246A RID: 9322
		public static readonly ADPropertyDefinition ReplicationSchedule = new ADPropertyDefinition("ReplicationSchedule", ExchangeObjectVersion.Exchange2003, typeof(Schedule), null, ADPropertyDefinitionFlags.Calculated, Schedule.Always, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			LegacyPublicFolderDatabaseSchema.ReplicationScheduleBitmaps,
			LegacyPublicFolderDatabaseSchema.ReplicationMode
		}, null, new GetterDelegate(LegacyPublicFolderDatabase.ReplicationScheduleGetter), new SetterDelegate(LegacyPublicFolderDatabase.ReplicationScheduleSetter), null, null);

		// Token: 0x0400246B RID: 9323
		public static readonly ADPropertyDefinition WindowsEmailAddress = ADRecipientSchema.WindowsEmailAddress;

		// Token: 0x0400246C RID: 9324
		public new static readonly ADPropertyDefinition Name = LegacyDatabaseSchema.Name;
	}
}
