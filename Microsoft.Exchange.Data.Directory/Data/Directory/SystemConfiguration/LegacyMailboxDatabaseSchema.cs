using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200049B RID: 1179
	internal sealed class LegacyMailboxDatabaseSchema : LegacyDatabaseSchema
	{
		// Token: 0x0400244F RID: 9295
		public static readonly ADPropertyDefinition JournalRecipient = SharedPropertyDefinitions.JournalRecipient;

		// Token: 0x04002450 RID: 9296
		public static readonly ADPropertyDefinition MailboxRetention = new ADPropertyDefinition("MailboxRetention", ExchangeObjectVersion.Exchange2003, typeof(EnhancedTimeSpan), "msExchMailboxRetentionPeriod", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromSeconds(2592000.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002451 RID: 9297
		public static readonly ADPropertyDefinition OfflineAddressBook = SharedPropertyDefinitions.OfflineAddressBook;

		// Token: 0x04002452 RID: 9298
		public static readonly ADPropertyDefinition OriginalDatabase = SharedPropertyDefinitions.OriginalDatabase;

		// Token: 0x04002453 RID: 9299
		public static readonly ADPropertyDefinition PublicFolderDatabase = SharedPropertyDefinitions.MailboxPublicFolderDatabase;

		// Token: 0x04002454 RID: 9300
		public static readonly ADPropertyDefinition ProhibitSendReceiveQuota = new ADPropertyDefinition("ProhibitSendReceiveQuota", ExchangeObjectVersion.Exchange2003, typeof(Unlimited<ByteQuantifiedSize>), ByteQuantifiedSize.KilobyteQuantifierProvider, "mDBOverHardQuotaLimit", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(0UL), ByteQuantifiedSize.FromKB(2147483647UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002455 RID: 9301
		public static readonly ADPropertyDefinition Recovery = new ADPropertyDefinition("Recovery", ExchangeObjectVersion.Exchange2003, typeof(bool), "msExchRestore", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002456 RID: 9302
		public static readonly ADPropertyDefinition ProhibitSendQuota = new ADPropertyDefinition("ProhibitSendQuota", ExchangeObjectVersion.Exchange2003, typeof(Unlimited<ByteQuantifiedSize>), ByteQuantifiedSize.KilobyteQuantifierProvider, "mDBOverQuotaLimit", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(0UL), ByteQuantifiedSize.FromKB(2147483647UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002457 RID: 9303
		public static readonly ADPropertyDefinition IndexEnabled = new ADPropertyDefinition("IndexEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool), "msExchCIAvailable", ADPropertyDefinitionFlags.PersistDefaultValue, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002458 RID: 9304
		public new static readonly ADPropertyDefinition Name = LegacyDatabaseSchema.Name;
	}
}
