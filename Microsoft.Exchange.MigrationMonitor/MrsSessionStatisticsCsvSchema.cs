using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000029 RID: 41
	internal class MrsSessionStatisticsCsvSchema : BaseMrsMonitorCsvSchema
	{
		// Token: 0x06000165 RID: 357 RVA: 0x00007EE2 File Offset: 0x000060E2
		public MrsSessionStatisticsCsvSchema() : base(BaseMigMonCsvSchema.GetRequiredColumns(MrsSessionStatisticsCsvSchema.requiredColumnsIds, MrsSessionStatisticsCsvSchema.requiredColumnsAsIs, "Time"), BaseMigMonCsvSchema.GetOptionalColumns(MrsSessionStatisticsCsvSchema.optionalColumnsIds, MrsSessionStatisticsCsvSchema.optionalColumnsAsIs), null)
		{
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00007F0E File Offset: 0x0000610E
		public override List<ColumnDefinition<int>> GetRequiredColumnsIds()
		{
			return MrsSessionStatisticsCsvSchema.requiredColumnsIds;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00007F15 File Offset: 0x00006115
		public override List<IColumnDefinition> GetRequiredColumnsAsIs()
		{
			return MrsSessionStatisticsCsvSchema.requiredColumnsAsIs;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00007F1C File Offset: 0x0000611C
		public override List<ColumnDefinition<int>> GetOptionalColumnsIds()
		{
			return MrsSessionStatisticsCsvSchema.optionalColumnsIds;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00007F23 File Offset: 0x00006123
		public override List<IColumnDefinition> GetOptionalColumnsAsIs()
		{
			return MrsSessionStatisticsCsvSchema.optionalColumnsAsIs;
		}

		// Token: 0x040000F2 RID: 242
		public const string MaxProviderDurationMethodNameColumn = "MaxProviderDurationMethodName";

		// Token: 0x040000F3 RID: 243
		private static readonly List<ColumnDefinition<int>> requiredColumnsIds = new List<ColumnDefinition<int>>();

		// Token: 0x040000F4 RID: 244
		private static readonly List<IColumnDefinition> requiredColumnsAsIs = new List<IColumnDefinition>
		{
			new ColumnDefinition<Guid>("RequestGuid"),
			new ColumnDefinition<int>("SessionId"),
			new ColumnDefinition<int>("SessionId_Archive")
		};

		// Token: 0x040000F5 RID: 245
		private static readonly List<ColumnDefinition<int>> optionalColumnsIds = new List<ColumnDefinition<int>>
		{
			new ColumnDefinition<int>("MaxProviderDurationMethodName", "MaxProviderDurationMethodNameId", KnownStringType.MaxProviderDurationMethodName)
		};

		// Token: 0x040000F6 RID: 246
		private static readonly List<IColumnDefinition> optionalColumnsAsIs = new List<IColumnDefinition>
		{
			new ColumnDefinition<double>("MaxProviderDurationInMilliseconds"),
			new ColumnDefinition<double>("CI_TotalTimeInMilliseconds"),
			new ColumnDefinition<int>("SourceLatencyInMillisecondsCurrent"),
			new ColumnDefinition<int>("SourceLatencyInMillisecondsAverage"),
			new ColumnDefinition<int>("SourceLatencyInMillisecondsMin"),
			new ColumnDefinition<int>("SourceLatencyInMillisecondsMax"),
			new ColumnDefinition<int>("SourceLatencyNumberOfSamples"),
			new ColumnDefinition<int>("SourceLatencyTotalNumberOfRemoteCalls"),
			new ColumnDefinition<int>("DestinationLatencyInMillisecondsCurrent"),
			new ColumnDefinition<int>("DestinationLatencyInMillisecondsAverage"),
			new ColumnDefinition<int>("DestinationLatencyInMillisecondsMin"),
			new ColumnDefinition<int>("DestinationLatencyInMillisecondsMax"),
			new ColumnDefinition<int>("DestinationLatencyNumberOfSamples"),
			new ColumnDefinition<int>("DestinationLatencyTotalNumberOfRemoteCalls"),
			new ColumnDefinition<double>("SourceProvider_TotalDurationInMilliseconds"),
			new ColumnDefinition<double>("DestinationProvider_TotalDurationInMilliseconds"),
			new ColumnDefinition<double>("SourceDuration_ISourceMailbox.ExportMessages"),
			new ColumnDefinition<double>("DestinationDuration_IMapiFxProxy.ProcessRequest"),
			new ColumnDefinition<double>("CI_TotalTimeInMilliseconds_Archive"),
			new ColumnDefinition<int>("SourceLatencyInMillisecondsCurrent_Archive"),
			new ColumnDefinition<int>("SourceLatencyInMillisecondsAverage_Archive"),
			new ColumnDefinition<int>("SourceLatencyInMillisecondsMin_Archive"),
			new ColumnDefinition<int>("SourceLatencyInMillisecondsMax_Archive"),
			new ColumnDefinition<int>("SourceLatencyNumberOfSamples_Archive"),
			new ColumnDefinition<int>("SourceLatencyTotalNumberOfRemoteCalls_Archive"),
			new ColumnDefinition<int>("DestinationLatencyInMillisecondsCurrent_Archive"),
			new ColumnDefinition<int>("DestinationLatencyInMillisecondsAverage_Archive"),
			new ColumnDefinition<int>("DestinationLatencyInMillisecondsMin_Archive"),
			new ColumnDefinition<int>("DestinationLatencyInMillisecondsMax_Archive"),
			new ColumnDefinition<int>("DestinationLatencyNumberOfSamples_Archive"),
			new ColumnDefinition<int>("DestinationLatencyTotalNumberOfRemoteCalls_Archive"),
			new ColumnDefinition<double>("SourceProvider_TotalDurationInMilliseconds_Archive"),
			new ColumnDefinition<double>("DestinationProvider_TotalDurationInMilliseconds_Archive"),
			new ColumnDefinition<double>("SourceDuration_ISourceMailbox.ExportMessages_Archive"),
			new ColumnDefinition<double>("DestinationDuration_IMapiFxProxy.ProcessRequest_Archive")
		};
	}
}
