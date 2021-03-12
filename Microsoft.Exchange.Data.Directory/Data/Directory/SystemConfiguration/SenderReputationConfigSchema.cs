using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000579 RID: 1401
	internal sealed class SenderReputationConfigSchema : MessageHygieneAgentConfigSchema
	{
		// Token: 0x04002A5D RID: 10845
		public static readonly ADPropertyDefinition MinMessagesPerDatabaseTransaction = new ADPropertyDefinition("MinMessagesPerDatabaseTransaction", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSenderReputationMinMessagesPerDatabaseTransaction", ADPropertyDefinitionFlags.PersistDefaultValue, 20, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A5E RID: 10846
		public static readonly ADPropertyDefinition SrlBlockThreshold = new ADPropertyDefinition("SrlBlockThreshold", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSenderReputationSrlBlockThreshold", ADPropertyDefinitionFlags.PersistDefaultValue, 7, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 9)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A5F RID: 10847
		public static readonly ADPropertyDefinition MinMessagesPerTimeSlice = new ADPropertyDefinition("MinMessagesPerTimeSlice", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSenderReputationMinMessagePerTimeSlice", ADPropertyDefinitionFlags.PersistDefaultValue, 100, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A60 RID: 10848
		public static readonly ADPropertyDefinition TimeSliceInterval = new ADPropertyDefinition("TimeSliceInterval", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSenderReputationTimeSliceInterval", ADPropertyDefinitionFlags.PersistDefaultValue, 48, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A61 RID: 10849
		public static readonly ADPropertyDefinition OpenProxyRescanInterval = new ADPropertyDefinition("OpenProxyRescanInterval", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSenderReputationOpenProxyRescanInterval", ADPropertyDefinitionFlags.PersistDefaultValue, 10, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A62 RID: 10850
		public static readonly ADPropertyDefinition OpenProxyFlags = new ADPropertyDefinition("OpenProxyFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSenderReputationOpenProxyFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 3, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A63 RID: 10851
		public static readonly ADPropertyDefinition MinReverseDnsQueryPeriod = new ADPropertyDefinition("MinReverseDnsQueryPeriod", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSenderReputationMinReverseDnsQueryPeriod", ADPropertyDefinitionFlags.PersistDefaultValue, 1, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A64 RID: 10852
		public static readonly ADPropertyDefinition SenderBlockingPeriod = new ADPropertyDefinition("SenderBlockingPeriod", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSenderReputationSenderBlockingPeriod", ADPropertyDefinitionFlags.PersistDefaultValue, 24, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 48)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A65 RID: 10853
		public static readonly ADPropertyDefinition MaxWorkQueueSize = new ADPropertyDefinition("MaxWorkQueueSize", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSenderReputationMaxWorkQueueSize", ADPropertyDefinitionFlags.PersistDefaultValue, 1000, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A66 RID: 10854
		public static readonly ADPropertyDefinition MaxIdleTime = new ADPropertyDefinition("MaxIdleTime", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSenderReputationMaxIdleTime", ADPropertyDefinitionFlags.PersistDefaultValue, 10, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A67 RID: 10855
		public static readonly ADPropertyDefinition Socks4Ports = new ADPropertyDefinition("Socks4Ports", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSenderReputationSocks4Ports", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A68 RID: 10856
		public static readonly ADPropertyDefinition Socks5Ports = new ADPropertyDefinition("Socks5Ports", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSenderReputationSocks5Ports", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A69 RID: 10857
		public static readonly ADPropertyDefinition HttpConnectPorts = new ADPropertyDefinition("HttpConnectPorts", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSenderReputationHttpConnectPorts", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A6A RID: 10858
		public static readonly ADPropertyDefinition HttpPostPorts = new ADPropertyDefinition("HttpPostPorts", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSenderReputationHttpPostPorts", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A6B RID: 10859
		public static readonly ADPropertyDefinition CiscoPorts = new ADPropertyDefinition("CiscoPorts", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSenderReputationCiscoPorts", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A6C RID: 10860
		public static readonly ADPropertyDefinition TelnetPorts = new ADPropertyDefinition("TelnetPorts", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSenderReputationTelnetPorts", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A6D RID: 10861
		public static readonly ADPropertyDefinition WingatePorts = new ADPropertyDefinition("WingatePorts", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSenderReputationWingatePorts", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A6E RID: 10862
		public static readonly ADPropertyDefinition TablePurgeInterval = new ADPropertyDefinition("TablePurgeInterval", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSenderReputationTablePurgeInterval", ADPropertyDefinitionFlags.PersistDefaultValue, 24, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A6F RID: 10863
		public static readonly ADPropertyDefinition MaxPendingOperations = new ADPropertyDefinition("MaxPendingOperations", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSenderReputationMaxPendingOperations", ADPropertyDefinitionFlags.PersistDefaultValue, 100, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A70 RID: 10864
		public static readonly ADPropertyDefinition ProxyServerIP = new ADPropertyDefinition("ProxyServerIP", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchSenderReputationProxyServerIP", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A71 RID: 10865
		public static readonly ADPropertyDefinition ProxyServerType = new ADPropertyDefinition("ProxyServerType", ExchangeObjectVersion.Exchange2007, typeof(ProxyType), "msExchSenderReputationProxyServerType", ADPropertyDefinitionFlags.PersistDefaultValue, ProxyType.None, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(ProxyType))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A72 RID: 10866
		public static readonly ADPropertyDefinition ProxyServerPort = new ADPropertyDefinition("ProxyServerPort", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSenderReputationProxyServerPort", ADPropertyDefinitionFlags.PersistDefaultValue, 0, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 9999)
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A73 RID: 10867
		public static readonly ADPropertyDefinition MinDownloadInterval = new ADPropertyDefinition("MinDownloadInterval", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSenderReputationMinDownloadInterval", ADPropertyDefinitionFlags.PersistDefaultValue, 10, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A74 RID: 10868
		public static readonly ADPropertyDefinition MaxDownloadInterval = new ADPropertyDefinition("MaxDownloadInterval", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchSenderReputationMaxDownloadInterval", ADPropertyDefinitionFlags.PersistDefaultValue, 100, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A75 RID: 10869
		public static readonly ADPropertyDefinition SrlSettingsDatabaseFileName = new ADPropertyDefinition("SrlSettingsDatabaseFileName", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchSenderReputationSrlSettingsDatabaseFileName", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002A76 RID: 10870
		public static readonly ADPropertyDefinition ReputationServiceUrl = new ADPropertyDefinition("ReputationServiceUrl", ExchangeObjectVersion.Exchange2007, typeof(string), "msExchSenderReputationServiceUrl", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
