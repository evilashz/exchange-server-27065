using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000439 RID: 1081
	internal class EdgeSyncServiceConfigSchema : ADContainerSchema
	{
		// Token: 0x0400209F RID: 8351
		private const string DefaultLogPath = "TransportRoles\\Logs\\EdgeSync\\";

		// Token: 0x040020A0 RID: 8352
		public static readonly ADPropertyDefinition ConfigurationSyncInterval = new ADPropertyDefinition("ConfigurationSyncInterval", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchEdgeSyncConfigurationSyncInterval", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromMinutes(3.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.OneSecond, EnhancedTimeSpan.MaxValue),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040020A1 RID: 8353
		public static readonly ADPropertyDefinition RecipientSyncInterval = new ADPropertyDefinition("RecipientSyncInterval", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchEdgeSyncRecipientSyncInterval", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromMinutes(5.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.OneSecond, EnhancedTimeSpan.MaxValue),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040020A2 RID: 8354
		public static readonly ADPropertyDefinition LockDuration = new ADPropertyDefinition("LockDuration", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchEdgeSyncLockDuration", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromMinutes(6.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.OneSecond, EnhancedTimeSpan.MaxValue),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040020A3 RID: 8355
		public static readonly ADPropertyDefinition LockRenewalDuration = new ADPropertyDefinition("LockRenewalDuration", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchEdgeSyncLockRenewalDuration", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromMinutes(4.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.OneSecond, EnhancedTimeSpan.MaxValue),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040020A4 RID: 8356
		public static readonly ADPropertyDefinition OptionDuration = new ADPropertyDefinition("OptionDuration", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchEdgeSyncOptionDuration", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromMinutes(30.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.OneSecond, EnhancedTimeSpan.MaxValue),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneSecond)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040020A5 RID: 8357
		public static readonly ADPropertyDefinition CookieValidDuration = new ADPropertyDefinition("CookieValidDuration", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchEdgeSyncCookieValidDuration", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(21.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.OneDay, EnhancedTimeSpan.MaxValue),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneDay)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040020A6 RID: 8358
		public static readonly ADPropertyDefinition FailoverDCInterval = new ADPropertyDefinition("FailoverDCInterval", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchEdgeSyncFailoverDCInterval", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromMinutes(5.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.OneMinute, EnhancedTimeSpan.MaxValue),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneMinute)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040020A7 RID: 8359
		public static readonly ADPropertyDefinition LogEnabled = new ADPropertyDefinition("LogEnabled", ExchangeObjectVersion.Exchange2007, typeof(bool), "msExchEdgeSyncLogEnabled", ADPropertyDefinitionFlags.PersistDefaultValue, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040020A8 RID: 8360
		public static readonly ADPropertyDefinition LogMaxAge = new ADPropertyDefinition("LogMaxAge", ExchangeObjectVersion.Exchange2007, typeof(EnhancedTimeSpan), "msExchEdgeSyncLogMaxAge", ADPropertyDefinitionFlags.PersistDefaultValue, EnhancedTimeSpan.FromDays(30.0), new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<EnhancedTimeSpan>(EnhancedTimeSpan.OneDay, EnhancedTimeSpan.MaxValue),
			new EnhancedTimeSpanUnitConstraint(EnhancedTimeSpan.OneDay)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040020A9 RID: 8361
		public static readonly ADPropertyDefinition LogMaxDirectorySize = new ADPropertyDefinition("LogMaxDirectorySize", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), "msExchEdgeSyncLogMaxDirectorySize", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040020AA RID: 8362
		public static readonly ADPropertyDefinition LogMaxFileSize = new ADPropertyDefinition("LogMaxFileSize", ExchangeObjectVersion.Exchange2007, typeof(Unlimited<ByteQuantifiedSize>), "msExchEdgeSyncLogMaxFileSize", ADPropertyDefinitionFlags.None, Unlimited<ByteQuantifiedSize>.UnlimitedValue, new PropertyDefinitionConstraint[]
		{
			new RangedUnlimitedConstraint<ByteQuantifiedSize>(ByteQuantifiedSize.FromBytes(1UL), ByteQuantifiedSize.FromBytes(9223372036854775807UL))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040020AB RID: 8363
		public static readonly ADPropertyDefinition LogLevel = new ADPropertyDefinition("LogLevel", ExchangeObjectVersion.Exchange2007, typeof(EdgeSyncLoggingLevel), "msExchEdgeSyncLogLevel", ADPropertyDefinitionFlags.None, EdgeSyncLoggingLevel.None, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(EdgeSyncLoggingLevel))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040020AC RID: 8364
		public static readonly ADPropertyDefinition LogPath = new ADPropertyDefinition("LogPath", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchEdgeSyncLogPath", ADPropertyDefinitionFlags.Mandatory | ADPropertyDefinitionFlags.PersistDefaultValue, "TransportRoles\\Logs\\EdgeSync\\", PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
