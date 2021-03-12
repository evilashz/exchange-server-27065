using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Core.Diagnostics
{
	// Token: 0x02000097 RID: 151
	internal interface IDiagnosticsSession
	{
		// Token: 0x17000105 RID: 261
		// (set) Token: 0x0600043B RID: 1083
		string ComponentName { set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600043C RID: 1084
		// (set) Token: 0x0600043D RID: 1085
		Trace Tracer { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600043E RID: 1086
		ExEventLog EventLog { get; }

		// Token: 0x0600043F RID: 1087
		string[] GetExtendedLoggingInformation();

		// Token: 0x06000440 RID: 1088
		void Assert(bool condition, string message, params object[] messageArgs);

		// Token: 0x06000441 RID: 1089
		void LogDiagnosticsInfo(DiagnosticsLoggingTag loggingTag, string operation, params object[] operationSpecificData);

		// Token: 0x06000442 RID: 1090
		void LogDiagnosticsInfo(DiagnosticsLoggingTag loggingTag, IIdentity documentId, string operation, params object[] operationSpecificData);

		// Token: 0x06000443 RID: 1091
		void LogCrawlerInfo(DiagnosticsLoggingTag loggingTag, string operationId, string databaseName, string mailboxGuid, string operation, params object[] operationSpecificData);

		// Token: 0x06000444 RID: 1092
		void LogGracefulDegradationInfo(DiagnosticsLoggingTag loggingTag, long totalMemory, long availableMemory, long actualMemoryUsage, long expectedMemoryUsage, float searchMemoryDriftRatio, string operation, params object[] operationSpecificData);

		// Token: 0x06000445 RID: 1093
		void LogDictionaryInfo(DiagnosticsLoggingTag loggingTag, int operationId, Guid correlationId, Guid database, Guid mailbox, string operation, params object[] operationSpecificData);

		// Token: 0x06000446 RID: 1094
		void LogEvent(ExEventLog.EventTuple eventId, params object[] messageArgs);

		// Token: 0x06000447 RID: 1095
		void LogPeriodicEvent(ExEventLog.EventTuple eventId, string periodicLogName, params object[] messageArgs);

		// Token: 0x06000448 RID: 1096
		void TraceDebug(string message, params object[] messageArgs);

		// Token: 0x06000449 RID: 1097
		void TraceDebug<T0>(string message, T0 arg0);

		// Token: 0x0600044A RID: 1098
		void TraceDebug<T0, T1>(string message, T0 arg0, T1 arg1);

		// Token: 0x0600044B RID: 1099
		void TraceDebug<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2);

		// Token: 0x0600044C RID: 1100
		void TraceError(string message, params object[] messageArgs);

		// Token: 0x0600044D RID: 1101
		void TraceError<T0>(string message, T0 arg0);

		// Token: 0x0600044E RID: 1102
		void TraceError<T0, T1>(string message, T0 arg0, T1 arg1);

		// Token: 0x0600044F RID: 1103
		void TraceError<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2);

		// Token: 0x06000450 RID: 1104
		bool IsTraceEnabled(TraceType traceType);

		// Token: 0x06000451 RID: 1105
		void SetCounterRawValue(IExPerformanceCounter counter, long value);

		// Token: 0x06000452 RID: 1106
		void DecrementCounter(IExPerformanceCounter counter);

		// Token: 0x06000453 RID: 1107
		void IncrementCounter(IExPerformanceCounter counter);

		// Token: 0x06000454 RID: 1108
		void IncrementCounterBy(IExPerformanceCounter counter, long incrementValue);

		// Token: 0x06000455 RID: 1109
		void SendInformationalWatsonReport(Exception exception, string additionalDetails);

		// Token: 0x06000456 RID: 1110
		void SendWatsonReport(Exception exception);

		// Token: 0x06000457 RID: 1111
		void SetDefaults(Guid eventLogComponentGuid, string serviceName, string logTypeName, string logFilePath, string logFilePrefix, string logComponent);
	}
}
