using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Search.EventLog
{
	// Token: 0x020000CA RID: 202
	public static class MSExchangeFastSearchEventLogConstants
	{
		// Token: 0x040002DB RID: 731
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SearchServiceStarting = new ExEventLog.EventTuple(263144U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040002DC RID: 732
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SearchServiceStartSuccess = new ExEventLog.EventTuple(263145U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040002DD RID: 733
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SearchServiceStopping = new ExEventLog.EventTuple(263146U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040002DE RID: 734
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SearchServiceStopSuccess = new ExEventLog.EventTuple(263147U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040002DF RID: 735
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SearchServiceUnexpectedException = new ExEventLog.EventTuple(3221488620U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040002E0 RID: 736
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FastNodesAreNotReady = new ExEventLog.EventTuple(2147746797U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040002E1 RID: 737
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FastConnectionException = new ExEventLog.EventTuple(2147746798U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040002E2 RID: 738
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SearchIndexingStartSuccess = new ExEventLog.EventTuple(263151U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040002E3 RID: 739
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SearchIndexingStopSuccess = new ExEventLog.EventTuple(263152U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040002E4 RID: 740
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SearchIndexingUnexpectedException = new ExEventLog.EventTuple(2147746801U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040002E5 RID: 741
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FASTConnectionIssue = new ExEventLog.EventTuple(2147746802U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040002E6 RID: 742
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ComponentPoisoned = new ExEventLog.EventTuple(2147746803U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040002E7 RID: 743
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ComponentFailed = new ExEventLog.EventTuple(2147746804U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040002E8 RID: 744
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_CatalogResetDetected = new ExEventLog.EventTuple(2147746805U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040002E9 RID: 745
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_InvalidConfiguration = new ExEventLog.EventTuple(2147746806U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040002EA RID: 746
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ServiceStartupDelayed = new ExEventLog.EventTuple(2147746807U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040002EB RID: 747
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ItemProcessingTimeExceeded = new ExEventLog.EventTuple(2147747793U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040002EC RID: 748
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SevereDocumentFailure = new ExEventLog.EventTuple(2147747794U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040002ED RID: 749
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_RopNotSupported = new ExEventLog.EventTuple(2147747796U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040002EE RID: 750
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SchemaUpdateIsAvailable = new ExEventLog.EventTuple(264149U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040002EF RID: 751
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_InitiateSchemaUpdate = new ExEventLog.EventTuple(264150U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040002F0 RID: 752
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SuspendSchemaUpdate = new ExEventLog.EventTuple(264151U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040002F1 RID: 753
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ResumeSchemaUpdate = new ExEventLog.EventTuple(264152U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040002F2 RID: 754
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FinishSchemaUpdate = new ExEventLog.EventTuple(264153U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040002F3 RID: 755
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SetProcessorAffinityUnexpectedException = new ExEventLog.EventTuple(2147748793U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x020000CB RID: 203
		private enum Category : short
		{
			// Token: 0x040002F5 RID: 757
			General = 1
		}

		// Token: 0x020000CC RID: 204
		internal enum Message : uint
		{
			// Token: 0x040002F7 RID: 759
			SearchServiceStarting = 263144U,
			// Token: 0x040002F8 RID: 760
			SearchServiceStartSuccess,
			// Token: 0x040002F9 RID: 761
			SearchServiceStopping,
			// Token: 0x040002FA RID: 762
			SearchServiceStopSuccess,
			// Token: 0x040002FB RID: 763
			SearchServiceUnexpectedException = 3221488620U,
			// Token: 0x040002FC RID: 764
			FastNodesAreNotReady = 2147746797U,
			// Token: 0x040002FD RID: 765
			FastConnectionException,
			// Token: 0x040002FE RID: 766
			SearchIndexingStartSuccess = 263151U,
			// Token: 0x040002FF RID: 767
			SearchIndexingStopSuccess,
			// Token: 0x04000300 RID: 768
			SearchIndexingUnexpectedException = 2147746801U,
			// Token: 0x04000301 RID: 769
			FASTConnectionIssue,
			// Token: 0x04000302 RID: 770
			ComponentPoisoned,
			// Token: 0x04000303 RID: 771
			ComponentFailed,
			// Token: 0x04000304 RID: 772
			CatalogResetDetected,
			// Token: 0x04000305 RID: 773
			InvalidConfiguration,
			// Token: 0x04000306 RID: 774
			ServiceStartupDelayed,
			// Token: 0x04000307 RID: 775
			ItemProcessingTimeExceeded = 2147747793U,
			// Token: 0x04000308 RID: 776
			SevereDocumentFailure,
			// Token: 0x04000309 RID: 777
			RopNotSupported = 2147747796U,
			// Token: 0x0400030A RID: 778
			SchemaUpdateIsAvailable = 264149U,
			// Token: 0x0400030B RID: 779
			InitiateSchemaUpdate,
			// Token: 0x0400030C RID: 780
			SuspendSchemaUpdate,
			// Token: 0x0400030D RID: 781
			ResumeSchemaUpdate,
			// Token: 0x0400030E RID: 782
			FinishSchemaUpdate,
			// Token: 0x0400030F RID: 783
			SetProcessorAffinityUnexpectedException = 2147748793U
		}
	}
}
