using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Agent.AntiSpam.Common
{
	// Token: 0x0200001A RID: 26
	public static class AgentsEventLogConstants
	{
		// Token: 0x0400006B RID: 107
		public const string EventSource = "MSExchange Antispam";

		// Token: 0x0400006C RID: 108
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AgentQueueFull = new ExEventLog.EventTuple(2147745892U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400006D RID: 109
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_DnsNotConfigured = new ExEventLog.EventTuple(2147745893U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0400006E RID: 110
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_ProtocolAnalysisBg = new ExEventLog.EventTuple(2147745992U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0400006F RID: 111
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ContentFilterInitialized = new ExEventLog.EventTuple(262444U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000070 RID: 112
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ContentFilterNotInitialized = new ExEventLog.EventTuple(3221487917U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000071 RID: 113
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ContentFilterQuarantineMailboxIsInvalid = new ExEventLog.EventTuple(2147746094U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Medium, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000072 RID: 114
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ContentFilterInitFailedUnauthorizedAccess = new ExEventLog.EventTuple(3221487919U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000073 RID: 115
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ContentFilterInitFailedBadImageFormat = new ExEventLog.EventTuple(3221487920U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000074 RID: 116
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ContentFilterInitFailedFSWatcherAlreadyInitialized = new ExEventLog.EventTuple(3221487921U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000075 RID: 117
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ContentFilterInitFailedInsufficientBuffer = new ExEventLog.EventTuple(3221487922U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000076 RID: 118
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ContentFilterWrapperNotResponding = new ExEventLog.EventTuple(3221487923U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000077 RID: 119
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ContentFilterWrapperBeingRecycled = new ExEventLog.EventTuple(2147746100U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000078 RID: 120
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ContentFilterWrapperSuccessfullyRecycled = new ExEventLog.EventTuple(262453U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000079 RID: 121
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ContentFilterWrapperRecycleTimedout = new ExEventLog.EventTuple(3221487926U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007A RID: 122
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ContentFilterWrapperRecycleError = new ExEventLog.EventTuple(3221487927U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007B RID: 123
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ContentFilterWrapperSendingPingRequest = new ExEventLog.EventTuple(2147746104U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007C RID: 124
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_ContentFilterWrapperErrorSubmittingMessage = new ExEventLog.EventTuple(2147746105U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Low, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400007D RID: 125
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ExSMimeFailedToInitialize = new ExEventLog.EventTuple(3221487930U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400007E RID: 126
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UnexpectedFailureScanningMessage = new ExEventLog.EventTuple(3221487931U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400007F RID: 127
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToReadAntispamUpdateMode = new ExEventLog.EventTuple(3221487932U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000080 RID: 128
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AntispamUpdateModeChanged = new ExEventLog.EventTuple(262461U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000081 RID: 129
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_ContentFilterInitFailedFileNotFound = new ExEventLog.EventTuple(3221487934U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000082 RID: 130
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_UpdateAgentFileNotLoaded = new ExEventLog.EventTuple(2147746192U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000083 RID: 131
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_FailedToReadConfiguration = new ExEventLog.EventTuple(3221488116U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000084 RID: 132
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_ConnectionFilteringDnsNotConfigured = new ExEventLog.EventTuple(3221488216U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000085 RID: 133
		[EventLogPeriod(Period = "LogOneTime")]
		public static readonly ExEventLog.EventTuple Tuple_SenderIdDnsNotConfigured = new ExEventLog.EventTuple(3221488316U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000086 RID: 134
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_PartnerConfigurationLoadingError = new ExEventLog.EventTuple(3221488416U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000087 RID: 135
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AgentLogPathIsNull = new ExEventLog.EventTuple(3221488516U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000088 RID: 136
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SenderRecipientThrottlingAgentMessageRejected = new ExEventLog.EventTuple(264145U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000089 RID: 137
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_SenderRecipientThrottlingAgentMessageAccepted = new ExEventLog.EventTuple(264146U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400008A RID: 138
		[EventLogPeriod(Period = "LogAlways")]
		public static readonly ExEventLog.EventTuple Tuple_AlertStringFormatExceptionGenerated = new ExEventLog.EventTuple(264147U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400008B RID: 139
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_AddressBookPolicyLoadingError = new ExEventLog.EventTuple(3221489716U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400008C RID: 140
		[EventLogPeriod(Period = "LogPeriodic")]
		public static readonly ExEventLog.EventTuple Tuple_SenderRecipientPairWithSubjectAutoNuked = new ExEventLog.EventTuple(3221489619U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0200001B RID: 27
		private enum Category : short
		{
			// Token: 0x0400008E RID: 142
			General = 1
		}

		// Token: 0x0200001C RID: 28
		internal enum Message : uint
		{
			// Token: 0x04000090 RID: 144
			AgentQueueFull = 2147745892U,
			// Token: 0x04000091 RID: 145
			DnsNotConfigured,
			// Token: 0x04000092 RID: 146
			ProtocolAnalysisBg = 2147745992U,
			// Token: 0x04000093 RID: 147
			ContentFilterInitialized = 262444U,
			// Token: 0x04000094 RID: 148
			ContentFilterNotInitialized = 3221487917U,
			// Token: 0x04000095 RID: 149
			ContentFilterQuarantineMailboxIsInvalid = 2147746094U,
			// Token: 0x04000096 RID: 150
			ContentFilterInitFailedUnauthorizedAccess = 3221487919U,
			// Token: 0x04000097 RID: 151
			ContentFilterInitFailedBadImageFormat,
			// Token: 0x04000098 RID: 152
			ContentFilterInitFailedFSWatcherAlreadyInitialized,
			// Token: 0x04000099 RID: 153
			ContentFilterInitFailedInsufficientBuffer,
			// Token: 0x0400009A RID: 154
			ContentFilterWrapperNotResponding,
			// Token: 0x0400009B RID: 155
			ContentFilterWrapperBeingRecycled = 2147746100U,
			// Token: 0x0400009C RID: 156
			ContentFilterWrapperSuccessfullyRecycled = 262453U,
			// Token: 0x0400009D RID: 157
			ContentFilterWrapperRecycleTimedout = 3221487926U,
			// Token: 0x0400009E RID: 158
			ContentFilterWrapperRecycleError,
			// Token: 0x0400009F RID: 159
			ContentFilterWrapperSendingPingRequest = 2147746104U,
			// Token: 0x040000A0 RID: 160
			ContentFilterWrapperErrorSubmittingMessage,
			// Token: 0x040000A1 RID: 161
			ExSMimeFailedToInitialize = 3221487930U,
			// Token: 0x040000A2 RID: 162
			UnexpectedFailureScanningMessage,
			// Token: 0x040000A3 RID: 163
			FailedToReadAntispamUpdateMode,
			// Token: 0x040000A4 RID: 164
			AntispamUpdateModeChanged = 262461U,
			// Token: 0x040000A5 RID: 165
			ContentFilterInitFailedFileNotFound = 3221487934U,
			// Token: 0x040000A6 RID: 166
			UpdateAgentFileNotLoaded = 2147746192U,
			// Token: 0x040000A7 RID: 167
			FailedToReadConfiguration = 3221488116U,
			// Token: 0x040000A8 RID: 168
			ConnectionFilteringDnsNotConfigured = 3221488216U,
			// Token: 0x040000A9 RID: 169
			SenderIdDnsNotConfigured = 3221488316U,
			// Token: 0x040000AA RID: 170
			PartnerConfigurationLoadingError = 3221488416U,
			// Token: 0x040000AB RID: 171
			AgentLogPathIsNull = 3221488516U,
			// Token: 0x040000AC RID: 172
			SenderRecipientThrottlingAgentMessageRejected = 264145U,
			// Token: 0x040000AD RID: 173
			SenderRecipientThrottlingAgentMessageAccepted,
			// Token: 0x040000AE RID: 174
			AlertStringFormatExceptionGenerated,
			// Token: 0x040000AF RID: 175
			AddressBookPolicyLoadingError = 3221489716U,
			// Token: 0x040000B0 RID: 176
			SenderRecipientPairWithSubjectAutoNuked = 3221489619U
		}
	}
}
