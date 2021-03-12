using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004A6 RID: 1190
	internal sealed class MailboxDatabaseSchema : DatabaseSchema
	{
		// Token: 0x040024AD RID: 9389
		public static readonly ADPropertyDefinition JournalRecipient = SharedPropertyDefinitions.JournalRecipient;

		// Token: 0x040024AE RID: 9390
		public static readonly ADPropertyDefinition MailboxRetention = new ADPropertyDefinition("MailboxRetention", ExchangeObjectVersion.Exchange2003, typeof(EnhancedTimeSpan), "msExchMailboxRetentionPeriod", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromSeconds(2592000.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.Zero, EnhancedTimeSpan.FromSeconds(2147483647.0)),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040024AF RID: 9391
		public static readonly ADPropertyDefinition OfflineAddressBook = SharedPropertyDefinitions.OfflineAddressBook;

		// Token: 0x040024B0 RID: 9392
		public static readonly ADPropertyDefinition OriginalDatabase = SharedPropertyDefinitions.OriginalDatabase;

		// Token: 0x040024B1 RID: 9393
		public static readonly ADPropertyDefinition PublicFolderDatabase = SharedPropertyDefinitions.MailboxPublicFolderDatabase;

		// Token: 0x040024B2 RID: 9394
		public static readonly ADPropertyDefinition ProhibitSendReceiveQuota = new ADPropertyDefinition("ProhibitSendReceiveQuota", ExchangeObjectVersion.Exchange2003, typeof(Unlimited<ByteQuantifiedSize>), ByteQuantifiedSize.KilobyteQuantifierProvider, "mDBOverHardQuotaLimit", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(0UL), ByteQuantifiedSize.FromKB(2147483647UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040024B3 RID: 9395
		public static readonly ADPropertyDefinition ProhibitSendQuota = new ADPropertyDefinition("ProhibitSendQuota", ExchangeObjectVersion.Exchange2003, typeof(Unlimited<ByteQuantifiedSize>), ByteQuantifiedSize.KilobyteQuantifierProvider, "mDBOverQuotaLimit", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(0UL), ByteQuantifiedSize.FromKB(2147483647UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040024B4 RID: 9396
		public static readonly ADPropertyDefinition RecoverableItemsQuota = new ADPropertyDefinition("RecoverableItemsQuota", ExchangeObjectVersion.Exchange2010, typeof(Unlimited<ByteQuantifiedSize>), ByteQuantifiedSize.KilobyteQuantifierProvider, "msExchDumpsterQuota", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(0UL), ByteQuantifiedSize.FromKB(2147483647UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040024B5 RID: 9397
		public static readonly ADPropertyDefinition RecoverableItemsWarningQuota = new ADPropertyDefinition("RecoverableItemsWarningQuota", ExchangeObjectVersion.Exchange2010, typeof(Unlimited<ByteQuantifiedSize>), ByteQuantifiedSize.KilobyteQuantifierProvider, "msExchDumpsterWarningQuota", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(0UL), ByteQuantifiedSize.FromKB(2147483647UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040024B6 RID: 9398
		public static readonly ADPropertyDefinition CalendarLoggingQuota = new ADPropertyDefinition("CalendarLoggingQuota", ExchangeObjectVersion.Exchange2010, typeof(Unlimited<ByteQuantifiedSize>), ByteQuantifiedSize.KilobyteQuantifierProvider, "msExchCalendarLoggingQuota", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(0UL), ByteQuantifiedSize.FromKB(2147483647UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040024B7 RID: 9399
		public static readonly ADPropertyDefinition IndexEnabled = new ADPropertyDefinition("IndexEnabled", ExchangeObjectVersion.Exchange2003, typeof(bool), "msExchCIAvailable", ADPropertyDefinitionFlags.PersistDefaultValue, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040024B8 RID: 9400
		public new static readonly ADPropertyDefinition Name = DatabaseSchema.Name;

		// Token: 0x040024B9 RID: 9401
		public static readonly ADPropertyDefinition ProvisioningFlags = SharedPropertyDefinitions.ProvisioningFlags;

		// Token: 0x040024BA RID: 9402
		public static readonly ADPropertyDefinition ReservedFlag = new ADPropertyDefinition("ReservedFlag", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MailboxDatabaseSchema.ProvisioningFlags
		}, null, ADObject.FlagGetterDelegate(MailboxDatabaseSchema.ProvisioningFlags, 1), ADObject.FlagSetterDelegate(MailboxDatabaseSchema.ProvisioningFlags, 1), null, null);

		// Token: 0x040024BB RID: 9403
		public static readonly ADPropertyDefinition IsExcludedFromProvisioning = new ADPropertyDefinition("IsExcludedFromProvisioning", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MailboxDatabaseSchema.ProvisioningFlags
		}, new CustomFilterBuilderDelegate(MailboxDatabase.IsExcludedFromProvisioningFilterBuilder), ADObject.FlagGetterDelegate(MailboxDatabaseSchema.ProvisioningFlags, 2), ADObject.FlagSetterDelegate(MailboxDatabaseSchema.ProvisioningFlags, 2), null, null);

		// Token: 0x040024BC RID: 9404
		public static readonly ADPropertyDefinition IsSuspendedFromProvisioning = new ADPropertyDefinition("IsSuspendedFromProvisioning", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MailboxDatabaseSchema.ProvisioningFlags
		}, new CustomFilterBuilderDelegate(MailboxDatabase.IsSuspendedFromProvisioningFilterBuilder), ADObject.FlagGetterDelegate(MailboxDatabaseSchema.ProvisioningFlags, 4), ADObject.FlagSetterDelegate(MailboxDatabaseSchema.ProvisioningFlags, 4), null, null);

		// Token: 0x040024BD RID: 9405
		public static readonly ADPropertyDefinition IsExcludedFromInitialProvisioning = new ADPropertyDefinition("IsExcludedFromInitialProvisioning", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MailboxDatabaseSchema.ProvisioningFlags
		}, (SinglePropertyFilter filter) => ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(MailboxDatabaseSchema.ProvisioningFlags, 16UL)), ADObject.FlagGetterDelegate(MailboxDatabaseSchema.ProvisioningFlags, 16), ADObject.FlagSetterDelegate(MailboxDatabaseSchema.ProvisioningFlags, 16), null, null);

		// Token: 0x040024BE RID: 9406
		public static readonly ADPropertyDefinition IsExcludedFromProvisioningBySpaceMonitoring = new ADPropertyDefinition("IsExcludedFromProvisioningBySpaceMonitoring", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MailboxDatabaseSchema.ProvisioningFlags
		}, (SinglePropertyFilter filter) => ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(MailboxDatabaseSchema.ProvisioningFlags, 32UL)), ADObject.FlagGetterDelegate(MailboxDatabaseSchema.ProvisioningFlags, 32), ADObject.FlagSetterDelegate(MailboxDatabaseSchema.ProvisioningFlags, 32), null, null);

		// Token: 0x040024BF RID: 9407
		public static readonly ADPropertyDefinition IsExcludedFromProvisioningBySchemaVersionMonitoring = new ADPropertyDefinition("IsExcludedFromProvisioningBySchemaVersionMonitoring", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MailboxDatabaseSchema.ProvisioningFlags
		}, (SinglePropertyFilter filter) => ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(MailboxDatabaseSchema.ProvisioningFlags, 64UL)), ADObject.FlagGetterDelegate(MailboxDatabaseSchema.ProvisioningFlags, 64), ADObject.FlagSetterDelegate(MailboxDatabaseSchema.ProvisioningFlags, 64), null, null);

		// Token: 0x040024C0 RID: 9408
		public static readonly ADPropertyDefinition MailboxLoadBalanceMaximumEdbFileSize = XMLSerializableBase.ConfigXmlProperty<DatabaseConfigXml, ByteQuantifiedSize?>("MailboxLoadBalanceMaximumEdbFileSize", ExchangeObjectVersion.Exchange2007, DatabaseSchema.ConfigurationXML, null, (DatabaseConfigXml configXml) => configXml.MailboxLoadBalanceMaximumEdbFileSize, delegate(DatabaseConfigXml configXml, ByteQuantifiedSize? value)
		{
			configXml.MailboxLoadBalanceMaximumEdbFileSize = value;
		}, null, null);

		// Token: 0x040024C1 RID: 9409
		public static readonly ADPropertyDefinition MailboxLoadBalanceRelativeLoadCapacity = XMLSerializableBase.ConfigXmlProperty<DatabaseConfigXml, int?>("MailboxLoadBalanceRelativeLoadCapacity", ExchangeObjectVersion.Exchange2007, DatabaseSchema.ConfigurationXML, null, (DatabaseConfigXml configXml) => configXml.MailboxLoadBalanceRelativeCapacity, delegate(DatabaseConfigXml configXml, int? value)
		{
			configXml.MailboxLoadBalanceRelativeCapacity = value;
		}, null, null);

		// Token: 0x040024C2 RID: 9410
		public static readonly ADPropertyDefinition MailboxLoadBalanceOverloadedThreshold = XMLSerializableBase.ConfigXmlProperty<DatabaseConfigXml, int?>("MailboxLoadBalanceOverloadedThreshold", ExchangeObjectVersion.Exchange2007, DatabaseSchema.ConfigurationXML, null, (DatabaseConfigXml configXml) => configXml.MailboxLoadBalanceOverloadThreshold, delegate(DatabaseConfigXml configXml, int? value)
		{
			configXml.MailboxLoadBalanceOverloadThreshold = value;
		}, null, null);

		// Token: 0x040024C3 RID: 9411
		public static readonly ADPropertyDefinition MailboxLoadBalanceUnderloadedThreshold = XMLSerializableBase.ConfigXmlProperty<DatabaseConfigXml, int?>("MailboxLoadBalanceUnderloadedThreshold", ExchangeObjectVersion.Exchange2007, DatabaseSchema.ConfigurationXML, null, (DatabaseConfigXml configXml) => configXml.MailboxLoadBalanceMinimumBalancingThreshold, delegate(DatabaseConfigXml configXml, int? value)
		{
			configXml.MailboxLoadBalanceMinimumBalancingThreshold = value;
		}, null, null);

		// Token: 0x040024C4 RID: 9412
		public static readonly ADPropertyDefinition MailboxLoadBalanceEnabled = XMLSerializableBase.ConfigXmlProperty<DatabaseConfigXml, bool?>("MailboxLoadBalanceEnabled", ExchangeObjectVersion.Exchange2007, DatabaseSchema.ConfigurationXML, null, (DatabaseConfigXml configXml) => configXml.MailboxLoadBalanceEnabled, delegate(DatabaseConfigXml configXml, bool? value)
		{
			configXml.MailboxLoadBalanceEnabled = value;
		}, null, null);
	}
}
