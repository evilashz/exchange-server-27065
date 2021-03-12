using System;
using System.IO;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.Inference.Common.Diagnostics;
using Microsoft.Exchange.Inference.DataAnalysis;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.InferenceDataCollection
{
	// Token: 0x02000214 RID: 532
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class Configuration : BaseConfiguration
	{
		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x00075357 File Offset: 0x00073557
		// (set) Token: 0x0600144E RID: 5198 RVA: 0x0007535F File Offset: 0x0007355F
		public int ModuloNumberToRandomizeForGroups { get; protected set; }

		// Token: 0x0600144F RID: 5199 RVA: 0x00075368 File Offset: 0x00073568
		public Configuration()
		{
			bool configBoolValue = AppConfigLoader.GetConfigBoolValue("InferenceDataCollectionIsLoggingEnabled", true);
			LoggingLevel configEnumValue = AppConfigLoader.GetConfigEnumValue<LoggingLevel>("InferenceDataCollectionLoggingLevel", LoggingLevel.Debug);
			string configStringValue = AppConfigLoader.GetConfigStringValue("InferenceDataCollectionLogPath", Configuration.DefaultLogPath);
			TimeSpan configTimeSpanValue = AppConfigLoader.GetConfigTimeSpanValue("InferenceDataCollectionMaxLogAge", TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromDays(30.0));
			ByteQuantifiedSize byteQuantifiedSize = ConfigurationUtils.ReadByteQuantifiedSizeValue("InferenceDataCollectionMaxLogDirectorySize", ByteQuantifiedSize.FromGB(1UL));
			ByteQuantifiedSize byteQuantifiedSize2 = ConfigurationUtils.ReadByteQuantifiedSizeValue("InferenceDataCollectionMaxLogFileSize", ByteQuantifiedSize.FromMB(10UL));
			base.MetadataLogConfig = new LogConfig(configBoolValue, "InferenceMetadata", "InferenceMetadata", Path.Combine(configStringValue, "Metadata"), new ulong?(byteQuantifiedSize.ToBytes()), new ulong?(byteQuantifiedSize2.ToBytes()), new TimeSpan?(configTimeSpanValue), 4096);
			base.DiagnosticLogConfig = new DiagnosticLogConfig(configBoolValue, "InferenceDataCollection", "InferenceDataCollection", configStringValue, new ulong?(byteQuantifiedSize.ToBytes()), new ulong?(byteQuantifiedSize2.ToBytes()), new TimeSpan?(configTimeSpanValue), configEnumValue);
			base.MailboxReprocessAge = AppConfigLoader.GetConfigTimeSpanValue("InferenceDataCollectionMailboxReprocessAge", TimeSpan.Zero, TimeSpan.MaxValue, TimeSpan.FromDays(30.0));
			base.ModuloNumberToRandomize = AppConfigLoader.GetConfigIntValue("InferenceDataCollectionModuloNumberToRandomize", 1, int.MaxValue, 500);
			this.ModuloNumberToRandomizeForGroups = AppConfigLoader.GetConfigIntValue("InferenceDataCollectionModuloNumberToRandomizeForGroups", 1, int.MaxValue, 10);
			base.BlackListedFolders = ConfigurationUtils.ReadCommaSeperatedStringValue("InferenceDataCollectionBlackListedFolders", null);
			base.WhiteListedFolders = ConfigurationUtils.ReadCommaSeperatedStringValue("InferenceDataCollectionWhiteListedFolders", null);
			base.MinimumItemCountInMailbox = AppConfigLoader.GetConfigIntValue("InferenceDataCollectionMinimumItemCountInMailbox", 0, int.MaxValue, 0);
			base.MinimumSentItemsCount = AppConfigLoader.GetConfigIntValue("InferenceDataCollectionMinimumSentItemsCount", 0, int.MaxValue, 0);
			base.NumberOfItemsPerFolderToProcess = AppConfigLoader.GetConfigIntValue("InferenceDataCollectionNumberOfItemsPerFolderToProcess", 0, int.MaxValue, int.MaxValue);
			base.MinimumSentItemsPercentage = AppConfigLoader.GetConfigIntValue("InferenceDataCollectionMinimumSentItemsPercentage", 0, 100, 0);
			base.IsOutputSanitized = AppConfigLoader.GetConfigBoolValue("InferenceDataCollectionIsOutputSanitized", true);
			base.QueryPageSize = AppConfigLoader.GetConfigIntValue("InferenceDataCollectionQueryPageSize", 0, 32767, 100);
			base.CollectMessageBodyProps = AppConfigLoader.GetConfigBoolValue("InferenceDataCollectionCollectMessageBodyProps", false);
			base.ChunkSize = AppConfigLoader.GetConfigIntValue("InferenceDataCollectionChunkSize", 1, int.MaxValue, 1000);
			base.ItemMaxAttemptCount = AppConfigLoader.GetConfigIntValue("InferenceDataCollectionItemMaxAttemptCount", 1, 10, 3);
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x000755AA File Offset: 0x000737AA
		public static Hookable<Configuration> Instance
		{
			get
			{
				return Configuration.instance;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x000755B1 File Offset: 0x000737B1
		public static Configuration Current
		{
			get
			{
				return Configuration.instance.Value;
			}
		}

		// Token: 0x04000C42 RID: 3138
		private static readonly string DefaultLogPath = Path.Combine(ExchangeSetupContext.InstallPath, "Logging\\InferenceDataCollection");

		// Token: 0x04000C43 RID: 3139
		private static readonly Hookable<Configuration> instance = Hookable<Configuration>.Create(true, new Configuration());
	}
}
