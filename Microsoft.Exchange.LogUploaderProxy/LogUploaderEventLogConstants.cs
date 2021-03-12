using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.LogUploaderProxy
{
	// Token: 0x0200001F RID: 31
	public static class LogUploaderEventLogConstants
	{
		// Token: 0x0400005C RID: 92
		public const string EventSource = "LogUploader";

		// Token: 0x0400005D RID: 93
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_Startup_Impl = new ExEventLog.EventTuple(1073742824U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0400005E RID: 94
		public static readonly ExEventLog.EventTuple Tuple_Startup = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_Startup_Impl);

		// Token: 0x0400005F RID: 95
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_Shutdown_Impl = new ExEventLog.EventTuple(1073742825U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000060 RID: 96
		public static readonly ExEventLog.EventTuple Tuple_Shutdown = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_Shutdown_Impl);

		// Token: 0x04000061 RID: 97
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_ServiceDisabled_Impl = new ExEventLog.EventTuple(1073742826U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000062 RID: 98
		public static readonly ExEventLog.EventTuple Tuple_ServiceDisabled = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_ServiceDisabled_Impl);

		// Token: 0x04000063 RID: 99
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_ServiceEnabled_Impl = new ExEventLog.EventTuple(1073742827U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000064 RID: 100
		public static readonly ExEventLog.EventTuple Tuple_ServiceEnabled = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_ServiceEnabled_Impl);

		// Token: 0x04000065 RID: 101
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_FailToAccessADTemporarily_Impl = new ExEventLog.EventTuple(3221226476U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000066 RID: 102
		public static readonly ExEventLog.EventTuple Tuple_FailToAccessADTemporarily = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_FailToAccessADTemporarily_Impl);

		// Token: 0x04000067 RID: 103
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_FailedToInstantiateLogFileInfoFileNotExist_Impl = new ExEventLog.EventTuple(3221226477U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000068 RID: 104
		public static readonly ExEventLog.EventTuple Tuple_FailedToInstantiateLogFileInfoFileNotExist = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_FailedToInstantiateLogFileInfoFileNotExist_Impl);

		// Token: 0x04000069 RID: 105
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_ReadConfigFromADSucceeded_Impl = new ExEventLog.EventTuple(1073742830U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400006A RID: 106
		public static readonly ExEventLog.EventTuple Tuple_ReadConfigFromADSucceeded = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_ReadConfigFromADSucceeded_Impl);

		// Token: 0x0400006B RID: 107
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_ServerNotFoundInAD_Impl = new ExEventLog.EventTuple(3221226479U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400006C RID: 108
		public static readonly ExEventLog.EventTuple Tuple_ServerNotFoundInAD = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_ServerNotFoundInAD_Impl);

		// Token: 0x0400006D RID: 109
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_LogTypeNotFoundInConfigFile_Impl = new ExEventLog.EventTuple(3221226481U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0400006E RID: 110
		public static readonly ExEventLog.EventTuple Tuple_LogTypeNotFoundInConfigFile = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_LogTypeNotFoundInConfigFile_Impl);

		// Token: 0x0400006F RID: 111
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_ParamNotFoundInConfigFile_Impl = new ExEventLog.EventTuple(3221226482U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000070 RID: 112
		public static readonly ExEventLog.EventTuple Tuple_ParamNotFoundInConfigFile = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_ParamNotFoundInConfigFile_Impl);

		// Token: 0x04000071 RID: 113
		[EventLogPeriod(Period = "LogAlways")]
		private static readonly ExEventLog.EventTuple Tuple_FailedToReadConfigFile_Impl = new ExEventLog.EventTuple(3221226484U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000072 RID: 114
		public static readonly ExEventLog.EventTuple Tuple_FailedToReadConfigFile = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_FailedToReadConfigFile_Impl);

		// Token: 0x04000073 RID: 115
		[EventLogPeriod(Period = "LogAlways")]
		private static readonly ExEventLog.EventTuple Tuple_ServiceStartUnknownException_Impl = new ExEventLog.EventTuple(3221226485U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000074 RID: 116
		public static readonly ExEventLog.EventTuple Tuple_ServiceStartUnknownException = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_ServiceStartUnknownException_Impl);

		// Token: 0x04000075 RID: 117
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_FailedToGetConfigValueFromAD_Impl = new ExEventLog.EventTuple(3221226486U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000076 RID: 118
		public static readonly ExEventLog.EventTuple Tuple_FailedToGetConfigValueFromAD = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_FailedToGetConfigValueFromAD_Impl);

		// Token: 0x04000077 RID: 119
		[EventLogPeriod(Period = "LogAlways")]
		private static readonly ExEventLog.EventTuple Tuple_ServiceStartedForProcessingLogs_Impl = new ExEventLog.EventTuple(1073742839U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000078 RID: 120
		public static readonly ExEventLog.EventTuple Tuple_ServiceStartedForProcessingLogs = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_ServiceStartedForProcessingLogs_Impl);

		// Token: 0x04000079 RID: 121
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_NoLogsToProcess_Impl = new ExEventLog.EventTuple(3221226488U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0400007A RID: 122
		public static readonly ExEventLog.EventTuple Tuple_NoLogsToProcess = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_NoLogsToProcess_Impl);

		// Token: 0x0400007B RID: 123
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_LogDirIsDisabled_Impl = new ExEventLog.EventTuple(1073742842U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0400007C RID: 124
		public static readonly ExEventLog.EventTuple Tuple_LogDirIsDisabled = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_LogDirIsDisabled_Impl);

		// Token: 0x0400007D RID: 125
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_WorkerStartup_Impl = new ExEventLog.EventTuple(1073742843U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0400007E RID: 126
		public static readonly ExEventLog.EventTuple Tuple_WorkerStartup = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_WorkerStartup_Impl);

		// Token: 0x0400007F RID: 127
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_UnsupportedLogSchemaType_Impl = new ExEventLog.EventTuple(3221226492U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000080 RID: 128
		public static readonly ExEventLog.EventTuple Tuple_UnsupportedLogSchemaType = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_UnsupportedLogSchemaType_Impl);

		// Token: 0x04000081 RID: 129
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_FailedToRemoveMessagesForDomain_Impl = new ExEventLog.EventTuple(3221226494U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000082 RID: 130
		public static readonly ExEventLog.EventTuple Tuple_FailedToRemoveMessagesForDomain = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_FailedToRemoveMessagesForDomain_Impl);

		// Token: 0x04000083 RID: 131
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_ConfigSettingNotFound_Impl = new ExEventLog.EventTuple(2147484671U, 1, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000084 RID: 132
		public static readonly ExEventLog.EventTuple Tuple_ConfigSettingNotFound = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_ConfigSettingNotFound_Impl);

		// Token: 0x04000085 RID: 133
		[EventLogPeriod(Period = "LogAlways")]
		private static readonly ExEventLog.EventTuple Tuple_InconsistentPersistentStoreCopies_Impl = new ExEventLog.EventTuple(1074004992U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000086 RID: 134
		public static readonly ExEventLog.EventTuple Tuple_InconsistentPersistentStoreCopies = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_InconsistentPersistentStoreCopies_Impl);

		// Token: 0x04000087 RID: 135
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_FailedToGetLogPath_Impl = new ExEventLog.EventTuple(3221488641U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000088 RID: 136
		public static readonly ExEventLog.EventTuple Tuple_FailedToGetLogPath = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_FailedToGetLogPath_Impl);

		// Token: 0x04000089 RID: 137
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_MissingWatermark_Impl = new ExEventLog.EventTuple(3221227472U, 1, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400008A RID: 138
		public static readonly ExEventLog.EventTuple Tuple_MissingWatermark = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_MissingWatermark_Impl);

		// Token: 0x0400008B RID: 139
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_DatabaseWriterUnknownException_Impl = new ExEventLog.EventTuple(3221489617U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400008C RID: 140
		public static readonly ExEventLog.EventTuple Tuple_DatabaseWriterUnknownException = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_DatabaseWriterUnknownException_Impl);

		// Token: 0x0400008D RID: 141
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_DatabaseWriterPermanentException_Impl = new ExEventLog.EventTuple(3221489618U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400008E RID: 142
		public static readonly ExEventLog.EventTuple Tuple_DatabaseWriterPermanentException = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_DatabaseWriterPermanentException_Impl);

		// Token: 0x0400008F RID: 143
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_DatabaseWriterTransientException_Impl = new ExEventLog.EventTuple(3221489619U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000090 RID: 144
		public static readonly ExEventLog.EventTuple Tuple_DatabaseWriterTransientException = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_DatabaseWriterTransientException_Impl);

		// Token: 0x04000091 RID: 145
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_WritePerItemPermanentException_Impl = new ExEventLog.EventTuple(3221489620U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000092 RID: 146
		public static readonly ExEventLog.EventTuple Tuple_WritePerItemPermanentException = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_WritePerItemPermanentException_Impl);

		// Token: 0x04000093 RID: 147
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_WebServiceWriteException_Impl = new ExEventLog.EventTuple(3221489621U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000094 RID: 148
		public static readonly ExEventLog.EventTuple Tuple_WebServiceWriteException = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_WebServiceWriteException_Impl);

		// Token: 0x04000095 RID: 149
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_WritePerItemPermanentException2_Impl = new ExEventLog.EventTuple(3221489622U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000096 RID: 150
		public static readonly ExEventLog.EventTuple Tuple_WritePerItemPermanentException2 = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_WritePerItemPermanentException2_Impl);

		// Token: 0x04000097 RID: 151
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_DatabaseWriterTransientException2_Impl = new ExEventLog.EventTuple(3221489623U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000098 RID: 152
		public static readonly ExEventLog.EventTuple Tuple_DatabaseWriterTransientException2 = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_DatabaseWriterTransientException2_Impl);

		// Token: 0x04000099 RID: 153
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_FailedToGetTenantDomain_Impl = new ExEventLog.EventTuple(3221489625U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400009A RID: 154
		public static readonly ExEventLog.EventTuple Tuple_FailedToGetTenantDomain = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_FailedToGetTenantDomain_Impl);

		// Token: 0x0400009B RID: 155
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_TransportQueueDBWriteException_Impl = new ExEventLog.EventTuple(3221489626U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400009C RID: 156
		public static readonly ExEventLog.EventTuple Tuple_TransportQueueDBWriteException = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_TransportQueueDBWriteException_Impl);

		// Token: 0x0400009D RID: 157
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_TransportQueueDBWriteTreatingTransientAsPermanent_Impl = new ExEventLog.EventTuple(3221489627U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400009E RID: 158
		public static readonly ExEventLog.EventTuple Tuple_TransportQueueDBWriteTreatingTransientAsPermanent = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_TransportQueueDBWriteTreatingTransientAsPermanent_Impl);

		// Token: 0x0400009F RID: 159
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_AdTransientExceptionWhenWriteViaWebServiceDAL_Impl = new ExEventLog.EventTuple(3221489628U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000A0 RID: 160
		public static readonly ExEventLog.EventTuple Tuple_AdTransientExceptionWhenWriteViaWebServiceDAL = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_AdTransientExceptionWhenWriteViaWebServiceDAL_Impl);

		// Token: 0x040000A1 RID: 161
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_ADTopologyEndpointNotFound_Impl = new ExEventLog.EventTuple(3221489629U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000A2 RID: 162
		public static readonly ExEventLog.EventTuple Tuple_ADTopologyEndpointNotFound = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_ADTopologyEndpointNotFound_Impl);

		// Token: 0x040000A3 RID: 163
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_DatabaseServerBusyException_Impl = new ExEventLog.EventTuple(3221489630U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000A4 RID: 164
		public static readonly ExEventLog.EventTuple Tuple_DatabaseServerBusyException = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_DatabaseServerBusyException_Impl);

		// Token: 0x040000A5 RID: 165
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_ServerBusyExceptionWhenWriteViaWebServiceDAL_Impl = new ExEventLog.EventTuple(3221489631U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000A6 RID: 166
		public static readonly ExEventLog.EventTuple Tuple_ServerBusyExceptionWhenWriteViaWebServiceDAL = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_ServerBusyExceptionWhenWriteViaWebServiceDAL_Impl);

		// Token: 0x040000A7 RID: 167
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_TimeoutExceptionWhenWriteViaWebServiceDAL_Impl = new ExEventLog.EventTuple(3221489632U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000A8 RID: 168
		public static readonly ExEventLog.EventTuple Tuple_TimeoutExceptionWhenWriteViaWebServiceDAL = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_TimeoutExceptionWhenWriteViaWebServiceDAL_Impl);

		// Token: 0x040000A9 RID: 169
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_CommunicationExceptionWhenWriteViaWebServiceDAL_Impl = new ExEventLog.EventTuple(3221489633U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000AA RID: 170
		public static readonly ExEventLog.EventTuple Tuple_CommunicationExceptionWhenWriteViaWebServiceDAL = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_CommunicationExceptionWhenWriteViaWebServiceDAL_Impl);

		// Token: 0x040000AB RID: 171
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_FaultExceptionWhenWriteViaWebServiceDAL_Impl = new ExEventLog.EventTuple(3221489634U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000AC RID: 172
		public static readonly ExEventLog.EventTuple Tuple_FaultExceptionWhenWriteViaWebServiceDAL = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_FaultExceptionWhenWriteViaWebServiceDAL_Impl);

		// Token: 0x040000AD RID: 173
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_CertificateException_Impl = new ExEventLog.EventTuple(3221489635U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000AE RID: 174
		public static readonly ExEventLog.EventTuple Tuple_CertificateException = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_CertificateException_Impl);

		// Token: 0x040000AF RID: 175
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_LogMonitorRequestedStop_Impl = new ExEventLog.EventTuple(1073744825U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040000B0 RID: 176
		public static readonly ExEventLog.EventTuple Tuple_LogMonitorRequestedStop = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_LogMonitorRequestedStop_Impl);

		// Token: 0x040000B1 RID: 177
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_LogMonitorAllStopped_Impl = new ExEventLog.EventTuple(1073744826U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040000B2 RID: 178
		public static readonly ExEventLog.EventTuple Tuple_LogMonitorAllStopped = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_LogMonitorAllStopped_Impl);

		// Token: 0x040000B3 RID: 179
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_LogMonitorStopTimedOut_Impl = new ExEventLog.EventTuple(1073744827U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040000B4 RID: 180
		public static readonly ExEventLog.EventTuple Tuple_LogMonitorStopTimedOut = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_LogMonitorStopTimedOut_Impl);

		// Token: 0x040000B5 RID: 181
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_LogMonitorWatermarkCleanupFailed_Impl = new ExEventLog.EventTuple(3221228476U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000B6 RID: 182
		public static readonly ExEventLog.EventTuple Tuple_LogMonitorWatermarkCleanupFailed = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_LogMonitorWatermarkCleanupFailed_Impl);

		// Token: 0x040000B7 RID: 183
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_LogMonitorLogCompleted_Impl = new ExEventLog.EventTuple(1073744829U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040000B8 RID: 184
		public static readonly ExEventLog.EventTuple Tuple_LogMonitorLogCompleted = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_LogMonitorLogCompleted_Impl);

		// Token: 0x040000B9 RID: 185
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_LogMonitorDetectLogProcessingFallsBehind_Impl = new ExEventLog.EventTuple(3221228478U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000BA RID: 186
		public static readonly ExEventLog.EventTuple Tuple_LogMonitorDetectLogProcessingFallsBehind = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_LogMonitorDetectLogProcessingFallsBehind_Impl);

		// Token: 0x040000BB RID: 187
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_LogMonitorDetectNoStaleLog_Impl = new ExEventLog.EventTuple(1073744831U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000BC RID: 188
		public static readonly ExEventLog.EventTuple Tuple_LogMonitorDetectNoStaleLog = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_LogMonitorDetectNoStaleLog_Impl);

		// Token: 0x040000BD RID: 189
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_NonexistentLogDirectory_Impl = new ExEventLog.EventTuple(1073744832U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040000BE RID: 190
		public static readonly ExEventLog.EventTuple Tuple_NonexistentLogDirectory = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_NonexistentLogDirectory_Impl);

		// Token: 0x040000BF RID: 191
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_LogDirectoryChanged_Impl = new ExEventLog.EventTuple(1073744833U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000C0 RID: 192
		public static readonly ExEventLog.EventTuple Tuple_LogDirectoryChanged = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_LogDirectoryChanged_Impl);

		// Token: 0x040000C1 RID: 193
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_LogFileIsDeleted_Impl = new ExEventLog.EventTuple(2147486658U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040000C2 RID: 194
		public static readonly ExEventLog.EventTuple Tuple_LogFileIsDeleted = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_LogFileIsDeleted_Impl);

		// Token: 0x040000C3 RID: 195
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_CheckDirectoryCaughtException_Impl = new ExEventLog.EventTuple(3221228483U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000C4 RID: 196
		public static readonly ExEventLog.EventTuple Tuple_CheckDirectoryCaughtException = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_CheckDirectoryCaughtException_Impl);

		// Token: 0x040000C5 RID: 197
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_FailedToInstantiateLogFileInfo_Impl = new ExEventLog.EventTuple(2147486660U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000C6 RID: 198
		public static readonly ExEventLog.EventTuple Tuple_FailedToInstantiateLogFileInfo = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_FailedToInstantiateLogFileInfo_Impl);

		// Token: 0x040000C7 RID: 199
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_FileDeletedWhenCheckingItsCompletion_Impl = new ExEventLog.EventTuple(2147486661U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000C8 RID: 200
		public static readonly ExEventLog.EventTuple Tuple_FileDeletedWhenCheckingItsCompletion = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_FileDeletedWhenCheckingItsCompletion_Impl);

		// Token: 0x040000C9 RID: 201
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_TransportQueueMachineNotPartOfStampGroup_Impl = new ExEventLog.EventTuple(3221490630U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000CA RID: 202
		public static readonly ExEventLog.EventTuple Tuple_TransportQueueMachineNotPartOfStampGroup = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_TransportQueueMachineNotPartOfStampGroup_Impl);

		// Token: 0x040000CB RID: 203
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_TransportQueueServerInvalidState_Impl = new ExEventLog.EventTuple(3221490631U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000CC RID: 204
		public static readonly ExEventLog.EventTuple Tuple_TransportQueueServerInvalidState = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_TransportQueueServerInvalidState_Impl);

		// Token: 0x040000CD RID: 205
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_LogDisappearedFromKnownLogNameToLogFileMap_Impl = new ExEventLog.EventTuple(2147748808U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000CE RID: 206
		public static readonly ExEventLog.EventTuple Tuple_LogDisappearedFromKnownLogNameToLogFileMap = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_LogDisappearedFromKnownLogNameToLogFileMap_Impl);

		// Token: 0x040000CF RID: 207
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_PendingProcessLogFilesInfo_Impl = new ExEventLog.EventTuple(1074006985U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000D0 RID: 208
		public static readonly ExEventLog.EventTuple Tuple_PendingProcessLogFilesInfo = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_PendingProcessLogFilesInfo_Impl);

		// Token: 0x040000D1 RID: 209
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_SuppressedBacklogAlertBecauseDBOffline_Impl = new ExEventLog.EventTuple(1074006986U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000D2 RID: 210
		public static readonly ExEventLog.EventTuple Tuple_SuppressedBacklogAlertBecauseDBOffline = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_SuppressedBacklogAlertBecauseDBOffline_Impl);

		// Token: 0x040000D3 RID: 211
		[EventLogPeriod(Period = "LogAlways")]
		private static readonly ExEventLog.EventTuple Tuple_LogFileDeletedFromKnownLogNameToLogFileMap_Impl = new ExEventLog.EventTuple(1074006987U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000D4 RID: 212
		public static readonly ExEventLog.EventTuple Tuple_LogFileDeletedFromKnownLogNameToLogFileMap = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_LogFileDeletedFromKnownLogNameToLogFileMap_Impl);

		// Token: 0x040000D5 RID: 213
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_FailToDeleteOldAndProcessedLogFile_Impl = new ExEventLog.EventTuple(2147748812U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000D6 RID: 214
		public static readonly ExEventLog.EventTuple Tuple_FailToDeleteOldAndProcessedLogFile = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_FailToDeleteOldAndProcessedLogFile_Impl);

		// Token: 0x040000D7 RID: 215
		[EventLogPeriod(Period = "LogAlways")]
		private static readonly ExEventLog.EventTuple Tuple_LogReaderUnknownError_Impl = new ExEventLog.EventTuple(3221229474U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x040000D8 RID: 216
		public static readonly ExEventLog.EventTuple Tuple_LogReaderUnknownError = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_LogReaderUnknownError_Impl);

		// Token: 0x040000D9 RID: 217
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_LogReaderFileOpenFailed_Impl = new ExEventLog.EventTuple(3221229475U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040000DA RID: 218
		public static readonly ExEventLog.EventTuple Tuple_LogReaderFileOpenFailed = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_LogReaderFileOpenFailed_Impl);

		// Token: 0x040000DB RID: 219
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_LogReaderReadFailed_Impl = new ExEventLog.EventTuple(3221229476U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000DC RID: 220
		public static readonly ExEventLog.EventTuple Tuple_LogReaderReadFailed = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_LogReaderReadFailed_Impl);

		// Token: 0x040000DD RID: 221
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_LogReaderQueueFullException_Impl = new ExEventLog.EventTuple(1073745829U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000DE RID: 222
		public static readonly ExEventLog.EventTuple Tuple_LogReaderQueueFullException = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_LogReaderQueueFullException_Impl);

		// Token: 0x040000DF RID: 223
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_LogReaderQueueFull_Impl = new ExEventLog.EventTuple(2147487654U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000E0 RID: 224
		public static readonly ExEventLog.EventTuple Tuple_LogReaderQueueFull = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_LogReaderQueueFull_Impl);

		// Token: 0x040000E1 RID: 225
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_LogReaderStartedParsingLog_Impl = new ExEventLog.EventTuple(1073745831U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040000E2 RID: 226
		public static readonly ExEventLog.EventTuple Tuple_LogReaderStartedParsingLog = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_LogReaderStartedParsingLog_Impl);

		// Token: 0x040000E3 RID: 227
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_LogReaderFinishedParsingLog_Impl = new ExEventLog.EventTuple(1073745832U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040000E4 RID: 228
		public static readonly ExEventLog.EventTuple Tuple_LogReaderFinishedParsingLog = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_LogReaderFinishedParsingLog_Impl);

		// Token: 0x040000E5 RID: 229
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_LogReaderParseEmptyLog_Impl = new ExEventLog.EventTuple(2147487657U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040000E6 RID: 230
		public static readonly ExEventLog.EventTuple Tuple_LogReaderParseEmptyLog = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_LogReaderParseEmptyLog_Impl);

		// Token: 0x040000E7 RID: 231
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_LogReaderLogTooBig_Impl = new ExEventLog.EventTuple(3221229482U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040000E8 RID: 232
		public static readonly ExEventLog.EventTuple Tuple_LogReaderLogTooBig = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_LogReaderLogTooBig_Impl);

		// Token: 0x040000E9 RID: 233
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_LogReaderLogMissing_Impl = new ExEventLog.EventTuple(2147487659U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040000EA RID: 234
		public static readonly ExEventLog.EventTuple Tuple_LogReaderLogMissing = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_LogReaderLogMissing_Impl);

		// Token: 0x040000EB RID: 235
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_FailedToGetVersionFromLogHeader_Impl = new ExEventLog.EventTuple(2147487660U, 4, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000EC RID: 236
		public static readonly ExEventLog.EventTuple Tuple_FailedToGetVersionFromLogHeader = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_FailedToGetVersionFromLogHeader_Impl);

		// Token: 0x040000ED RID: 237
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_ReadLogCaughtIOException_Impl = new ExEventLog.EventTuple(3221229485U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000EE RID: 238
		public static readonly ExEventLog.EventTuple Tuple_ReadLogCaughtIOException = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_ReadLogCaughtIOException_Impl);

		// Token: 0x040000EF RID: 239
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_CsvParserFailedToParseLogLine_Impl = new ExEventLog.EventTuple(3221229487U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040000F0 RID: 240
		public static readonly ExEventLog.EventTuple Tuple_CsvParserFailedToParseLogLine = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_CsvParserFailedToParseLogLine_Impl);

		// Token: 0x040000F1 RID: 241
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_UnsupportedLogVersion_Impl = new ExEventLog.EventTuple(3221229488U, 4, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000F2 RID: 242
		public static readonly ExEventLog.EventTuple Tuple_UnsupportedLogVersion = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_UnsupportedLogVersion_Impl);

		// Token: 0x040000F3 RID: 243
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_LogLineParseError_Impl = new ExEventLog.EventTuple(3221230473U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000F4 RID: 244
		public static readonly ExEventLog.EventTuple Tuple_LogLineParseError = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_LogLineParseError_Impl);

		// Token: 0x040000F5 RID: 245
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_FailedToParseLatencyData_Impl = new ExEventLog.EventTuple(3221230474U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000F6 RID: 246
		public static readonly ExEventLog.EventTuple Tuple_FailedToParseLatencyData = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_FailedToParseLatencyData_Impl);

		// Token: 0x040000F7 RID: 247
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_InvalidProperty_Impl = new ExEventLog.EventTuple(2147488652U, 5, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000F8 RID: 248
		public static readonly ExEventLog.EventTuple Tuple_InvalidProperty = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_InvalidProperty_Impl);

		// Token: 0x040000F9 RID: 249
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_OneDomainHasDifferentTenantIds_Impl = new ExEventLog.EventTuple(2147488653U, 5, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x040000FA RID: 250
		public static readonly ExEventLog.EventTuple Tuple_OneDomainHasDifferentTenantIds = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_OneDomainHasDifferentTenantIds_Impl);

		// Token: 0x040000FB RID: 251
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_InvalidAgentInfoGroupName_Impl = new ExEventLog.EventTuple(3221230478U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040000FC RID: 252
		public static readonly ExEventLog.EventTuple Tuple_InvalidAgentInfoGroupName = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_InvalidAgentInfoGroupName_Impl);

		// Token: 0x040000FD RID: 253
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_InvalidSysprobeLogLine_Impl = new ExEventLog.EventTuple(3221230479U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x040000FE RID: 254
		public static readonly ExEventLog.EventTuple Tuple_InvalidSysprobeLogLine = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_InvalidSysprobeLogLine_Impl);

		// Token: 0x040000FF RID: 255
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_MissingPropertyInParse_Impl = new ExEventLog.EventTuple(3221230480U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000100 RID: 256
		public static readonly ExEventLog.EventTuple Tuple_MissingPropertyInParse = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_MissingPropertyInParse_Impl);

		// Token: 0x04000101 RID: 257
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_InvalidPropertyValueInParse_Impl = new ExEventLog.EventTuple(3221230481U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000102 RID: 258
		public static readonly ExEventLog.EventTuple Tuple_InvalidPropertyValueInParse = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_InvalidPropertyValueInParse_Impl);

		// Token: 0x04000103 RID: 259
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_InvalidCastInParse_Impl = new ExEventLog.EventTuple(3221230482U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000104 RID: 260
		public static readonly ExEventLog.EventTuple Tuple_InvalidCastInParse = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_InvalidCastInParse_Impl);

		// Token: 0x04000105 RID: 261
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_InvalidPropertyValue_Impl = new ExEventLog.EventTuple(3221230483U, 5, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000106 RID: 262
		public static readonly ExEventLog.EventTuple Tuple_InvalidPropertyValue = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_InvalidPropertyValue_Impl);

		// Token: 0x04000107 RID: 263
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_WatermarkFileMapRemoveFailed_Impl = new ExEventLog.EventTuple(3221231473U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000108 RID: 264
		public static readonly ExEventLog.EventTuple Tuple_WatermarkFileMapRemoveFailed = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_WatermarkFileMapRemoveFailed_Impl);

		// Token: 0x04000109 RID: 265
		[EventLogPeriod(Period = "LogAlways")]
		private static readonly ExEventLog.EventTuple Tuple_WatermarkFileDuplicateBlock_Impl = new ExEventLog.EventTuple(3221231474U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400010A RID: 266
		public static readonly ExEventLog.EventTuple Tuple_WatermarkFileDuplicateBlock = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_WatermarkFileDuplicateBlock_Impl);

		// Token: 0x0400010B RID: 267
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_WatermarkFileParseException_Impl = new ExEventLog.EventTuple(3221231475U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400010C RID: 268
		public static readonly ExEventLog.EventTuple Tuple_WatermarkFileParseException = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_WatermarkFileParseException_Impl);

		// Token: 0x0400010D RID: 269
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_OverlappingLogRangeInWatermarkFile_Impl = new ExEventLog.EventTuple(3221231476U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400010E RID: 270
		public static readonly ExEventLog.EventTuple Tuple_OverlappingLogRangeInWatermarkFile = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_OverlappingLogRangeInWatermarkFile_Impl);

		// Token: 0x0400010F RID: 271
		[EventLogPeriod(Period = "LogAlways")]
		private static readonly ExEventLog.EventTuple Tuple_DualWriteErrorEvent_Impl = new ExEventLog.EventTuple(3221231477U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000110 RID: 272
		public static readonly ExEventLog.EventTuple Tuple_DualWriteErrorEvent = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_DualWriteErrorEvent_Impl);

		// Token: 0x04000111 RID: 273
		[EventLogPeriod(Period = "LogAlways")]
		private static readonly ExEventLog.EventTuple Tuple_DualWriteWarningEvent_Impl = new ExEventLog.EventTuple(2147489654U, 2, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000112 RID: 274
		public static readonly ExEventLog.EventTuple Tuple_DualWriteWarningEvent = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_DualWriteWarningEvent_Impl);

		// Token: 0x04000113 RID: 275
		[EventLogPeriod(Period = "LogAlways")]
		private static readonly ExEventLog.EventTuple Tuple_WatermarkFileOverlappingBlock_Impl = new ExEventLog.EventTuple(3221231479U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x04000114 RID: 276
		public static readonly ExEventLog.EventTuple Tuple_WatermarkFileOverlappingBlock = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_WatermarkFileOverlappingBlock_Impl);

		// Token: 0x04000115 RID: 277
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_OpticsLogMonitorTopologyStart_Impl = new ExEventLog.EventTuple(1073747832U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000116 RID: 278
		public static readonly ExEventLog.EventTuple Tuple_OpticsLogMonitorTopologyStart = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_OpticsLogMonitorTopologyStart_Impl);

		// Token: 0x04000117 RID: 279
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_OpticsLogMonitorTopologyFailedToStart_Impl = new ExEventLog.EventTuple(3221231481U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000118 RID: 280
		public static readonly ExEventLog.EventTuple Tuple_OpticsLogMonitorTopologyFailedToStart = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_OpticsLogMonitorTopologyFailedToStart_Impl);

		// Token: 0x04000119 RID: 281
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_OpticsLogMonitorTopologyStop_Impl = new ExEventLog.EventTuple(1073747834U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0400011A RID: 282
		public static readonly ExEventLog.EventTuple Tuple_OpticsLogMonitorTopologyStop = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_OpticsLogMonitorTopologyStop_Impl);

		// Token: 0x0400011B RID: 283
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_OpticsLogMonitorTopologyFailedToStop_Impl = new ExEventLog.EventTuple(2147489659U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x0400011C RID: 284
		public static readonly ExEventLog.EventTuple Tuple_OpticsLogMonitorTopologyFailedToStop = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_OpticsLogMonitorTopologyFailedToStop_Impl);

		// Token: 0x0400011D RID: 285
		[EventLogPeriod(Period = "LogAlways")]
		private static readonly ExEventLog.EventTuple Tuple_OpticsWriteExtractionWarningEvent_Impl = new ExEventLog.EventTuple(2147489660U, 7, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogAlways);

		// Token: 0x0400011E RID: 286
		public static readonly ExEventLog.EventTuple Tuple_OpticsWriteExtractionWarningEvent = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_OpticsWriteExtractionWarningEvent_Impl);

		// Token: 0x0400011F RID: 287
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_OpticsDisabled_Impl = new ExEventLog.EventTuple(1073747837U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000120 RID: 288
		public static readonly ExEventLog.EventTuple Tuple_OpticsDisabled = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_OpticsDisabled_Impl);

		// Token: 0x04000121 RID: 289
		[EventLogPeriod(Period = "LogOneTime")]
		private static readonly ExEventLog.EventTuple Tuple_OpticsEnabled_Impl = new ExEventLog.EventTuple(1073747838U, 1, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogOneTime);

		// Token: 0x04000122 RID: 290
		public static readonly ExEventLog.EventTuple Tuple_OpticsEnabled = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_OpticsEnabled_Impl);

		// Token: 0x04000123 RID: 291
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_WatermarkFileIOException_Impl = new ExEventLog.EventTuple(3221231487U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000124 RID: 292
		public static readonly ExEventLog.EventTuple Tuple_WatermarkFileIOException = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_WatermarkFileIOException_Impl);

		// Token: 0x04000125 RID: 293
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_InactiveFileTurnsToActiveException_Impl = new ExEventLog.EventTuple(3221231488U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000126 RID: 294
		public static readonly ExEventLog.EventTuple Tuple_InactiveFileTurnsToActiveException = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_InactiveFileTurnsToActiveException_Impl);

		// Token: 0x04000127 RID: 295
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_FileDeleted_Impl = new ExEventLog.EventTuple(3221231489U, 3, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000128 RID: 296
		public static readonly ExEventLog.EventTuple Tuple_FileDeleted = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_FileDeleted_Impl);

		// Token: 0x04000129 RID: 297
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_WatermarkFileObjectNotFound_Impl = new ExEventLog.EventTuple(1073747842U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400012A RID: 298
		public static readonly ExEventLog.EventTuple Tuple_WatermarkFileObjectNotFound = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_WatermarkFileObjectNotFound_Impl);

		// Token: 0x0400012B RID: 299
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_DeletingFile_Impl = new ExEventLog.EventTuple(1073747843U, 3, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400012C RID: 300
		public static readonly ExEventLog.EventTuple Tuple_DeletingFile = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_DeletingFile_Impl);

		// Token: 0x0400012D RID: 301
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_LogBatchEnqueue_Impl = new ExEventLog.EventTuple(1073747844U, 4, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400012E RID: 302
		public static readonly ExEventLog.EventTuple Tuple_LogBatchEnqueue = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_LogBatchEnqueue_Impl);

		// Token: 0x0400012F RID: 303
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_ForcePickUnprocessedHoles_Impl = new ExEventLog.EventTuple(2147489669U, 3, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000130 RID: 304
		public static readonly ExEventLog.EventTuple Tuple_ForcePickUnprocessedHoles = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_ForcePickUnprocessedHoles_Impl);

		// Token: 0x04000131 RID: 305
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_PartitionIsNotHealthy_Impl = new ExEventLog.EventTuple(2147489670U, 2, EventLogEntryType.Warning, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000132 RID: 306
		public static readonly ExEventLog.EventTuple Tuple_PartitionIsNotHealthy = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_PartitionIsNotHealthy_Impl);

		// Token: 0x04000133 RID: 307
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_PartitionHealthyVerboseMessage_Impl = new ExEventLog.EventTuple(1073747847U, 2, EventLogEntryType.Information, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000134 RID: 308
		public static readonly ExEventLog.EventTuple Tuple_PartitionHealthyVerboseMessage = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_PartitionHealthyVerboseMessage_Impl);

		// Token: 0x04000135 RID: 309
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_WatermarkFileObjectDisposed_Impl = new ExEventLog.EventTuple(3221231496U, 6, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000136 RID: 310
		public static readonly ExEventLog.EventTuple Tuple_WatermarkFileObjectDisposed = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_WatermarkFileObjectDisposed_Impl);

		// Token: 0x04000137 RID: 311
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_ConfigFileNotFound_Impl = new ExEventLog.EventTuple(3221231497U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x04000138 RID: 312
		public static readonly ExEventLog.EventTuple Tuple_ConfigFileNotFound = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_ConfigFileNotFound_Impl);

		// Token: 0x04000139 RID: 313
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_InvalidConfigFile_Impl = new ExEventLog.EventTuple(3221231498U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400013A RID: 314
		public static readonly ExEventLog.EventTuple Tuple_InvalidConfigFile = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_InvalidConfigFile_Impl);

		// Token: 0x0400013B RID: 315
		[EventLogPeriod(Period = "LogPeriodic")]
		private static readonly ExEventLog.EventTuple Tuple_InvalidRuleCollection_Impl = new ExEventLog.EventTuple(3221231499U, 8, EventLogEntryType.Error, ExEventLog.EventLevel.Lowest, ExEventLog.EventPeriod.LogPeriodic);

		// Token: 0x0400013C RID: 316
		public static readonly ExEventLog.EventTuple Tuple_InvalidRuleCollection = new ExEventLog.EventTuple(LogUploaderEventLogConstants.Tuple_InvalidRuleCollection_Impl);

		// Token: 0x02000020 RID: 32
		private enum Category : short
		{
			// Token: 0x0400013E RID: 318
			General = 1,
			// Token: 0x0400013F RID: 319
			DatabaseWriter,
			// Token: 0x04000140 RID: 320
			LogMonitor,
			// Token: 0x04000141 RID: 321
			LogReader,
			// Token: 0x04000142 RID: 322
			Parser,
			// Token: 0x04000143 RID: 323
			WatermarkFile,
			// Token: 0x04000144 RID: 324
			Optics,
			// Token: 0x04000145 RID: 325
			UploaderConfig
		}

		// Token: 0x02000021 RID: 33
		internal enum Message : uint
		{
			// Token: 0x04000147 RID: 327
			Startup = 1073742824U,
			// Token: 0x04000148 RID: 328
			Shutdown,
			// Token: 0x04000149 RID: 329
			ServiceDisabled,
			// Token: 0x0400014A RID: 330
			ServiceEnabled,
			// Token: 0x0400014B RID: 331
			FailToAccessADTemporarily = 3221226476U,
			// Token: 0x0400014C RID: 332
			FailedToInstantiateLogFileInfoFileNotExist,
			// Token: 0x0400014D RID: 333
			ReadConfigFromADSucceeded = 1073742830U,
			// Token: 0x0400014E RID: 334
			ServerNotFoundInAD = 3221226479U,
			// Token: 0x0400014F RID: 335
			LogTypeNotFoundInConfigFile = 3221226481U,
			// Token: 0x04000150 RID: 336
			ParamNotFoundInConfigFile,
			// Token: 0x04000151 RID: 337
			FailedToReadConfigFile = 3221226484U,
			// Token: 0x04000152 RID: 338
			ServiceStartUnknownException,
			// Token: 0x04000153 RID: 339
			FailedToGetConfigValueFromAD,
			// Token: 0x04000154 RID: 340
			ServiceStartedForProcessingLogs = 1073742839U,
			// Token: 0x04000155 RID: 341
			NoLogsToProcess = 3221226488U,
			// Token: 0x04000156 RID: 342
			LogDirIsDisabled = 1073742842U,
			// Token: 0x04000157 RID: 343
			WorkerStartup,
			// Token: 0x04000158 RID: 344
			UnsupportedLogSchemaType = 3221226492U,
			// Token: 0x04000159 RID: 345
			FailedToRemoveMessagesForDomain = 3221226494U,
			// Token: 0x0400015A RID: 346
			ConfigSettingNotFound = 2147484671U,
			// Token: 0x0400015B RID: 347
			InconsistentPersistentStoreCopies = 1074004992U,
			// Token: 0x0400015C RID: 348
			FailedToGetLogPath = 3221488641U,
			// Token: 0x0400015D RID: 349
			MissingWatermark = 3221227472U,
			// Token: 0x0400015E RID: 350
			DatabaseWriterUnknownException = 3221489617U,
			// Token: 0x0400015F RID: 351
			DatabaseWriterPermanentException,
			// Token: 0x04000160 RID: 352
			DatabaseWriterTransientException,
			// Token: 0x04000161 RID: 353
			WritePerItemPermanentException,
			// Token: 0x04000162 RID: 354
			WebServiceWriteException,
			// Token: 0x04000163 RID: 355
			WritePerItemPermanentException2,
			// Token: 0x04000164 RID: 356
			DatabaseWriterTransientException2,
			// Token: 0x04000165 RID: 357
			FailedToGetTenantDomain = 3221489625U,
			// Token: 0x04000166 RID: 358
			TransportQueueDBWriteException,
			// Token: 0x04000167 RID: 359
			TransportQueueDBWriteTreatingTransientAsPermanent,
			// Token: 0x04000168 RID: 360
			AdTransientExceptionWhenWriteViaWebServiceDAL,
			// Token: 0x04000169 RID: 361
			ADTopologyEndpointNotFound,
			// Token: 0x0400016A RID: 362
			DatabaseServerBusyException,
			// Token: 0x0400016B RID: 363
			ServerBusyExceptionWhenWriteViaWebServiceDAL,
			// Token: 0x0400016C RID: 364
			TimeoutExceptionWhenWriteViaWebServiceDAL,
			// Token: 0x0400016D RID: 365
			CommunicationExceptionWhenWriteViaWebServiceDAL,
			// Token: 0x0400016E RID: 366
			FaultExceptionWhenWriteViaWebServiceDAL,
			// Token: 0x0400016F RID: 367
			CertificateException,
			// Token: 0x04000170 RID: 368
			LogMonitorRequestedStop = 1073744825U,
			// Token: 0x04000171 RID: 369
			LogMonitorAllStopped,
			// Token: 0x04000172 RID: 370
			LogMonitorStopTimedOut,
			// Token: 0x04000173 RID: 371
			LogMonitorWatermarkCleanupFailed = 3221228476U,
			// Token: 0x04000174 RID: 372
			LogMonitorLogCompleted = 1073744829U,
			// Token: 0x04000175 RID: 373
			LogMonitorDetectLogProcessingFallsBehind = 3221228478U,
			// Token: 0x04000176 RID: 374
			LogMonitorDetectNoStaleLog = 1073744831U,
			// Token: 0x04000177 RID: 375
			NonexistentLogDirectory,
			// Token: 0x04000178 RID: 376
			LogDirectoryChanged,
			// Token: 0x04000179 RID: 377
			LogFileIsDeleted = 2147486658U,
			// Token: 0x0400017A RID: 378
			CheckDirectoryCaughtException = 3221228483U,
			// Token: 0x0400017B RID: 379
			FailedToInstantiateLogFileInfo = 2147486660U,
			// Token: 0x0400017C RID: 380
			FileDeletedWhenCheckingItsCompletion,
			// Token: 0x0400017D RID: 381
			TransportQueueMachineNotPartOfStampGroup = 3221490630U,
			// Token: 0x0400017E RID: 382
			TransportQueueServerInvalidState,
			// Token: 0x0400017F RID: 383
			LogDisappearedFromKnownLogNameToLogFileMap = 2147748808U,
			// Token: 0x04000180 RID: 384
			PendingProcessLogFilesInfo = 1074006985U,
			// Token: 0x04000181 RID: 385
			SuppressedBacklogAlertBecauseDBOffline,
			// Token: 0x04000182 RID: 386
			LogFileDeletedFromKnownLogNameToLogFileMap,
			// Token: 0x04000183 RID: 387
			FailToDeleteOldAndProcessedLogFile = 2147748812U,
			// Token: 0x04000184 RID: 388
			LogReaderUnknownError = 3221229474U,
			// Token: 0x04000185 RID: 389
			LogReaderFileOpenFailed,
			// Token: 0x04000186 RID: 390
			LogReaderReadFailed,
			// Token: 0x04000187 RID: 391
			LogReaderQueueFullException = 1073745829U,
			// Token: 0x04000188 RID: 392
			LogReaderQueueFull = 2147487654U,
			// Token: 0x04000189 RID: 393
			LogReaderStartedParsingLog = 1073745831U,
			// Token: 0x0400018A RID: 394
			LogReaderFinishedParsingLog,
			// Token: 0x0400018B RID: 395
			LogReaderParseEmptyLog = 2147487657U,
			// Token: 0x0400018C RID: 396
			LogReaderLogTooBig = 3221229482U,
			// Token: 0x0400018D RID: 397
			LogReaderLogMissing = 2147487659U,
			// Token: 0x0400018E RID: 398
			FailedToGetVersionFromLogHeader,
			// Token: 0x0400018F RID: 399
			ReadLogCaughtIOException = 3221229485U,
			// Token: 0x04000190 RID: 400
			CsvParserFailedToParseLogLine = 3221229487U,
			// Token: 0x04000191 RID: 401
			UnsupportedLogVersion,
			// Token: 0x04000192 RID: 402
			LogLineParseError = 3221230473U,
			// Token: 0x04000193 RID: 403
			FailedToParseLatencyData,
			// Token: 0x04000194 RID: 404
			InvalidProperty = 2147488652U,
			// Token: 0x04000195 RID: 405
			OneDomainHasDifferentTenantIds,
			// Token: 0x04000196 RID: 406
			InvalidAgentInfoGroupName = 3221230478U,
			// Token: 0x04000197 RID: 407
			InvalidSysprobeLogLine,
			// Token: 0x04000198 RID: 408
			MissingPropertyInParse,
			// Token: 0x04000199 RID: 409
			InvalidPropertyValueInParse,
			// Token: 0x0400019A RID: 410
			InvalidCastInParse,
			// Token: 0x0400019B RID: 411
			InvalidPropertyValue,
			// Token: 0x0400019C RID: 412
			WatermarkFileMapRemoveFailed = 3221231473U,
			// Token: 0x0400019D RID: 413
			WatermarkFileDuplicateBlock,
			// Token: 0x0400019E RID: 414
			WatermarkFileParseException,
			// Token: 0x0400019F RID: 415
			OverlappingLogRangeInWatermarkFile,
			// Token: 0x040001A0 RID: 416
			DualWriteErrorEvent,
			// Token: 0x040001A1 RID: 417
			DualWriteWarningEvent = 2147489654U,
			// Token: 0x040001A2 RID: 418
			WatermarkFileOverlappingBlock = 3221231479U,
			// Token: 0x040001A3 RID: 419
			OpticsLogMonitorTopologyStart = 1073747832U,
			// Token: 0x040001A4 RID: 420
			OpticsLogMonitorTopologyFailedToStart = 3221231481U,
			// Token: 0x040001A5 RID: 421
			OpticsLogMonitorTopologyStop = 1073747834U,
			// Token: 0x040001A6 RID: 422
			OpticsLogMonitorTopologyFailedToStop = 2147489659U,
			// Token: 0x040001A7 RID: 423
			OpticsWriteExtractionWarningEvent,
			// Token: 0x040001A8 RID: 424
			OpticsDisabled = 1073747837U,
			// Token: 0x040001A9 RID: 425
			OpticsEnabled,
			// Token: 0x040001AA RID: 426
			WatermarkFileIOException = 3221231487U,
			// Token: 0x040001AB RID: 427
			InactiveFileTurnsToActiveException,
			// Token: 0x040001AC RID: 428
			FileDeleted,
			// Token: 0x040001AD RID: 429
			WatermarkFileObjectNotFound = 1073747842U,
			// Token: 0x040001AE RID: 430
			DeletingFile,
			// Token: 0x040001AF RID: 431
			LogBatchEnqueue,
			// Token: 0x040001B0 RID: 432
			ForcePickUnprocessedHoles = 2147489669U,
			// Token: 0x040001B1 RID: 433
			PartitionIsNotHealthy,
			// Token: 0x040001B2 RID: 434
			PartitionHealthyVerboseMessage = 1073747847U,
			// Token: 0x040001B3 RID: 435
			WatermarkFileObjectDisposed = 3221231496U,
			// Token: 0x040001B4 RID: 436
			ConfigFileNotFound,
			// Token: 0x040001B5 RID: 437
			InvalidConfigFile,
			// Token: 0x040001B6 RID: 438
			InvalidRuleCollection
		}
	}
}
