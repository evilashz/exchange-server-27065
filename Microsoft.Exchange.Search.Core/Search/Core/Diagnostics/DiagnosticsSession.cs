using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Search.Core.Diagnostics
{
	// Token: 0x02000098 RID: 152
	internal class DiagnosticsSession : IDiagnosticsSession
	{
		// Token: 0x06000458 RID: 1112 RVA: 0x0000D5AC File Offset: 0x0000B7AC
		internal DiagnosticsSession(string componentName, string eventLogSourceName, Microsoft.Exchange.Diagnostics.Trace tracer, long traceContext, IIdentity documentId, DiagnosticsLogConfig.LogDefaults logDefaults, DiagnosticsLogConfig.LogDefaults crawlerLogDefaults) : this(componentName, eventLogSourceName, tracer, traceContext, documentId, logDefaults, crawlerLogDefaults, DiagnosticsSessionFactory.GracefulDegradationLogDefaults, DiagnosticsSessionFactory.DictionaryLogDefaults)
		{
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000D5D4 File Offset: 0x0000B7D4
		internal DiagnosticsSession(string componentName, string eventLogSourceName, Microsoft.Exchange.Diagnostics.Trace tracer, long traceContext, IIdentity documentId, DiagnosticsLogConfig.LogDefaults logDefaults, DiagnosticsLogConfig.LogDefaults crawlerLogDefaults, DiagnosticsLogConfig.LogDefaults gracefulDegradationLogDefaults, DiagnosticsLogConfig.LogDefaults dictionaryLogDefaults)
		{
			this.componentName = (string.IsNullOrEmpty(componentName) ? string.Empty : string.Format("{0}_{1}", componentName, traceContext));
			this.tracer = tracer;
			this.traceContext = traceContext;
			this.documentId = documentId;
			this.logDefaults = logDefaults;
			this.eventLogSourceName = eventLogSourceName;
			this.crawlerLogDefaults = crawlerLogDefaults;
			this.gracefulDegradationLogDefaults = gracefulDegradationLogDefaults;
			this.dictionaryLogDefaults = dictionaryLogDefaults;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0000D64C File Offset: 0x0000B84C
		public static bool CrashOnUnhandledException
		{
			get
			{
				return DiagnosticsSession.crashOnUnhandledException;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x0000D653 File Offset: 0x0000B853
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x0000D65B File Offset: 0x0000B85B
		public Microsoft.Exchange.Diagnostics.Trace Tracer
		{
			get
			{
				return this.tracer;
			}
			set
			{
				this.tracer = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x0000D664 File Offset: 0x0000B864
		public string ComponentName
		{
			set
			{
				this.componentName = value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0000D670 File Offset: 0x0000B870
		public ExEventLog EventLog
		{
			get
			{
				if (DiagnosticsSession.eventLog == null)
				{
					DiagnosticsSession.eventLog = new ExEventLog(this.Log.Config.EventLogComponentGuid, this.eventLogSourceName ?? this.Log.Config.ServiceName);
				}
				return DiagnosticsSession.eventLog;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000D6BD File Offset: 0x0000B8BD
		private static string AppName
		{
			get
			{
				if (DiagnosticsSession.appName == null)
				{
					DiagnosticsSession.appName = ExWatson.AppName;
				}
				return DiagnosticsSession.appName;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000D6D8 File Offset: 0x0000B8D8
		private static string AppVersion
		{
			get
			{
				if (DiagnosticsSession.appVersion == null)
				{
					using (Process currentProcess = Process.GetCurrentProcess())
					{
						Version version;
						if (ExWatson.TryGetRealApplicationVersion(currentProcess, out version))
						{
							DiagnosticsSession.appVersion = version.ToString();
						}
						else
						{
							DiagnosticsSession.appVersion = "0";
						}
					}
				}
				return DiagnosticsSession.appVersion;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0000D734 File Offset: 0x0000B934
		private DiagnosticsLog Log
		{
			get
			{
				if (this.diagnosticsLog == null)
				{
					this.diagnosticsLog = new DiagnosticsLog(new DiagnosticsLogConfig(this.logDefaults), DiagnosticsSession.schemaColumns);
				}
				return this.diagnosticsLog;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000D75F File Offset: 0x0000B95F
		private DiagnosticsLog CrawlerLog
		{
			get
			{
				if (this.crawlerLog == null)
				{
					this.crawlerLog = new DiagnosticsLog(new DiagnosticsLogConfig(this.crawlerLogDefaults), DiagnosticsSession.crawlerSchemaColumns);
				}
				return this.crawlerLog;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000D78A File Offset: 0x0000B98A
		private DiagnosticsLog GracefulDegradationLog
		{
			get
			{
				if (this.gracefulDegradationLog == null)
				{
					this.gracefulDegradationLog = new DiagnosticsLog(new DiagnosticsLogConfig(this.gracefulDegradationLogDefaults), DiagnosticsSession.gracefulDegradationSchemaColumns);
				}
				return this.gracefulDegradationLog;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0000D7B5 File Offset: 0x0000B9B5
		private DiagnosticsLog DictionaryLog
		{
			get
			{
				if (this.dictionaryLog == null)
				{
					this.dictionaryLog = new DiagnosticsLog(new DiagnosticsLogConfig(this.dictionaryLogDefaults), DiagnosticsSession.dictionarySchemaColumns);
				}
				return this.dictionaryLog;
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000D7E0 File Offset: 0x0000B9E0
		public static IDisposable SetFactoryTestHook(IDiagnosticsSessionFactory diagnosticsSessionFactory)
		{
			return DiagnosticsSession.hookableDiagnosticsSessionFactory.SetTestHook(diagnosticsSessionFactory);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000D7ED File Offset: 0x0000B9ED
		public static IDiagnosticsSession CreateComponentDiagnosticsSession(string componentName, Microsoft.Exchange.Diagnostics.Trace tracer, long traceContext)
		{
			return DiagnosticsSession.hookableDiagnosticsSessionFactory.Value.CreateComponentDiagnosticsSession(componentName, tracer, traceContext);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000D801 File Offset: 0x0000BA01
		public static IDiagnosticsSession CreateComponentDiagnosticsSession(string componentName, string eventLogSourceName, Microsoft.Exchange.Diagnostics.Trace tracer, long traceContext)
		{
			return DiagnosticsSession.hookableDiagnosticsSessionFactory.Value.CreateComponentDiagnosticsSession(componentName, eventLogSourceName, tracer, traceContext);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000D816 File Offset: 0x0000BA16
		public static IDiagnosticsSession CreateDocumentDiagnosticsSession(IIdentity documentId, Microsoft.Exchange.Diagnostics.Trace tracer)
		{
			return DiagnosticsSession.hookableDiagnosticsSessionFactory.Value.CreateDocumentDiagnosticsSession(documentId, tracer);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000D829 File Offset: 0x0000BA29
		public string[] GetExtendedLoggingInformation()
		{
			return this.Log.GetFormattedExtendedLogging();
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0000D836 File Offset: 0x0000BA36
		public void Assert(bool condition, string message, params object[] messageArgs)
		{
			ExAssert.RetailAssert(condition, message, messageArgs);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000D840 File Offset: 0x0000BA40
		public void LogDiagnosticsInfo(DiagnosticsLoggingTag loggingTag, string operation, params object[] operationSpecificData)
		{
			this.LogDiagnosticsInfo(loggingTag, this.documentId, operation, operationSpecificData);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0000D851 File Offset: 0x0000BA51
		public void LogDiagnosticsInfo(DiagnosticsLoggingTag loggingTag, IIdentity documentId, string operation, params object[] operationSpecificData)
		{
			this.LogDiagnosticsInfo(loggingTag, (documentId == null) ? null : documentId.ToString(), operation, operationSpecificData);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0000D86C File Offset: 0x0000BA6C
		public void LogDiagnosticsInfo(DiagnosticsLoggingTag loggingTag, string entryId, string operation, params object[] operationSpecificData)
		{
			if (!this.Log.IsEnabled || (loggingTag & this.Log.Config.DiagnosticsLoggingTag) == DiagnosticsLoggingTag.None)
			{
				return;
			}
			string text = string.Empty;
			if (operationSpecificData.Length > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object arg in operationSpecificData)
				{
					stringBuilder.AppendFormat("{0},", arg);
				}
				text = stringBuilder.ToString().TrimEnd(new char[]
				{
					','
				});
			}
			string text2 = (operationSpecificData == null || operationSpecificData.Length == 0) ? operation : string.Format(operation, operationSpecificData);
			this.LogLine(this.Log, loggingTag, new object[]
			{
				null,
				loggingTag.ToString(),
				1,
				this.componentName,
				entryId,
				text2,
				text
			});
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0000D954 File Offset: 0x0000BB54
		public void LogCrawlerInfo(DiagnosticsLoggingTag loggingTag, string operationId, string databaseName, string mailboxGuid, string operation, params object[] operationSpecificData)
		{
			if (!this.CrawlerLog.IsEnabled || (loggingTag & this.CrawlerLog.Config.DiagnosticsLoggingTag) == DiagnosticsLoggingTag.None)
			{
				return;
			}
			string text = (operationSpecificData == null || operationSpecificData.Length == 0) ? operation : string.Format(operation, operationSpecificData);
			this.LogLine(this.CrawlerLog, loggingTag, new object[]
			{
				null,
				this.componentName,
				3,
				operationId,
				databaseName,
				mailboxGuid,
				text
			});
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000D9D4 File Offset: 0x0000BBD4
		public void LogGracefulDegradationInfo(DiagnosticsLoggingTag loggingTag, long totalMemory, long availableMemory, long actualMemoryUsage, long expectedMemoryUsage, float searchMemoryDriftRatio, string operation, params object[] operationSpecificData)
		{
			if (!this.GracefulDegradationLog.IsEnabled || (loggingTag & this.GracefulDegradationLog.Config.DiagnosticsLoggingTag) == DiagnosticsLoggingTag.None)
			{
				return;
			}
			string text = (operationSpecificData == null || operationSpecificData.Length == 0) ? operation : string.Format(operation, operationSpecificData);
			this.LogLine(this.GracefulDegradationLog, loggingTag, new object[]
			{
				null,
				this.componentName,
				1,
				totalMemory,
				availableMemory,
				actualMemoryUsage,
				expectedMemoryUsage,
				searchMemoryDriftRatio.ToString("0.00"),
				text
			});
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0000DA7C File Offset: 0x0000BC7C
		public void LogDictionaryInfo(DiagnosticsLoggingTag loggingTag, int operationId, Guid correlationId, Guid database, Guid mailbox, string operation, params object[] operationSpecificData)
		{
			if (!this.DictionaryLog.IsEnabled || (loggingTag & this.DictionaryLog.Config.DiagnosticsLoggingTag) == DiagnosticsLoggingTag.None)
			{
				return;
			}
			string text = (operationSpecificData == null || operationSpecificData.Length == 0) ? operation : string.Format(operation, operationSpecificData);
			this.LogLine(this.DictionaryLog, loggingTag, new object[]
			{
				null,
				this.componentName,
				1,
				operationId,
				correlationId,
				database,
				mailbox,
				text
			});
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0000DB14 File Offset: 0x0000BD14
		public void LogPeriodicEvent(ExEventLog.EventTuple eventId, string periodicLogName, params object[] messageArgs)
		{
			Util.ThrowOnNullArgument(eventId, "eventId");
			this.EventLog.LogEvent(eventId, periodicLogName, messageArgs);
			Util.ThrowOnNullOrEmptyArgument(periodicLogName, "periodicLogName");
			Util.ThrowOnConditionFailed(eventId.Period == ExEventLog.EventPeriod.LogPeriodic, "LogPeriodicEvent method should only be called for LogPeriodic type events");
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000DB54 File Offset: 0x0000BD54
		public void LogEvent(ExEventLog.EventTuple eventId, params object[] messageArgs)
		{
			Util.ThrowOnNullArgument(eventId, "eventId");
			this.EventLog.LogEvent(eventId, string.Empty, messageArgs);
			Util.ThrowOnConditionFailed(eventId.Period == ExEventLog.EventPeriod.LogAlways, "LogEvent method should only be called for LogAlways type events");
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0000DB8D File Offset: 0x0000BD8D
		public void TraceDebug(string message, params object[] messageArgs)
		{
			this.tracer.TraceDebug(this.traceContext, message, messageArgs);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000DBA2 File Offset: 0x0000BDA2
		public void TraceDebug<T0>(string message, T0 arg0)
		{
			this.tracer.TraceDebug<T0>(this.traceContext, message, arg0);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000DBB7 File Offset: 0x0000BDB7
		public void TraceDebug<T0, T1>(string message, T0 arg0, T1 arg1)
		{
			this.tracer.TraceDebug<T0, T1>(this.traceContext, message, arg0, arg1);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000DBCD File Offset: 0x0000BDCD
		public void TraceDebug<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2)
		{
			this.tracer.TraceDebug<T0, T1, T2>(this.traceContext, message, arg0, arg1, arg2);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000DBE5 File Offset: 0x0000BDE5
		public void TraceError(string message, params object[] messageArgs)
		{
			this.tracer.TraceError(this.traceContext, message, messageArgs);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000DBFA File Offset: 0x0000BDFA
		public void TraceError<T0>(string message, T0 arg0)
		{
			this.tracer.TraceError<T0>(this.traceContext, message, arg0);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000DC0F File Offset: 0x0000BE0F
		public void TraceError<T0, T1>(string message, T0 arg0, T1 arg1)
		{
			this.tracer.TraceError<T0, T1>(this.traceContext, message, arg0, arg1);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000DC25 File Offset: 0x0000BE25
		public void TraceError<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2)
		{
			this.tracer.TraceError<T0, T1, T2>(this.traceContext, message, arg0, arg1, arg2);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000DC3D File Offset: 0x0000BE3D
		public bool IsTraceEnabled(TraceType traceType)
		{
			return this.tracer.IsTraceEnabled(traceType);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000DC4B File Offset: 0x0000BE4B
		public void SetCounterRawValue(IExPerformanceCounter counter, long value)
		{
			Util.ThrowOnNullArgument(counter, "counter");
			counter.RawValue = value;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000DC5F File Offset: 0x0000BE5F
		public void DecrementCounter(IExPerformanceCounter counter)
		{
			Util.ThrowOnNullArgument(counter, "counter");
			counter.Decrement();
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000DC73 File Offset: 0x0000BE73
		public void IncrementCounter(IExPerformanceCounter counter)
		{
			Util.ThrowOnNullArgument(counter, "counter");
			counter.Increment();
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000DC87 File Offset: 0x0000BE87
		public void IncrementCounterBy(IExPerformanceCounter counter, long incrementValue)
		{
			Util.ThrowOnNullArgument(counter, "counter");
			counter.IncrementBy(incrementValue);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000DC9C File Offset: 0x0000BE9C
		public void SendInformationalWatsonReport(Exception exception, string additionalDetails)
		{
			Util.ThrowOnNullArgument(exception, "exception");
			StackTrace stackTrace = new StackTrace(1);
			string name = exception.GetType().Name;
			MethodBase method = stackTrace.GetFrame(0).GetMethod();
			AssemblyName name2 = method.DeclaringType.Assembly.GetName();
			int hashCode = (method.Name + name).GetHashCode();
			string detailedExceptionInformation = string.Format("{0}{1}{2}{3}{4}", new object[]
			{
				additionalDetails ?? string.Empty,
				Environment.NewLine,
				exception.ToString(),
				Environment.NewLine,
				this.GetExtendedLoggingInformation()
			});
			ExWatson.SendGenericWatsonReport("E12", DiagnosticsSession.AppVersion, DiagnosticsSession.AppName, name2.Version.ToString(), name2.Name, name, stackTrace.ToString(), hashCode.ToString("x"), method.Name, detailedExceptionInformation);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000DD84 File Offset: 0x0000BF84
		public void SendWatsonReport(Exception exception)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string value in this.GetExtendedLoggingInformation())
			{
				if (!string.IsNullOrEmpty(value))
				{
					stringBuilder.AppendLine(value);
				}
			}
			DiagnosticsSession.SendWatsonReport(exception, stringBuilder.ToString(), DiagnosticsSession.CrashOnUnhandledException);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000DDD1 File Offset: 0x0000BFD1
		public void SetDefaults(Guid eventLogComponentGuid, string serviceName, string logTypeName, string logFilePath, string logFilePrefix, string logComponent)
		{
			this.logDefaults = new DiagnosticsLogConfig.LogDefaults(eventLogComponentGuid, serviceName, logTypeName, logFilePath, logFilePrefix, logComponent);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000DDE8 File Offset: 0x0000BFE8
		private static void SendWatsonReport(Exception exception, string extraData, bool terminateProccess)
		{
			ReportOptions reportOptions = ReportOptions.DoNotFreezeThreads;
			try
			{
				if (terminateProccess)
				{
					reportOptions |= ReportOptions.ReportTerminateAfterSend;
					ExWatson.SendReport(exception, reportOptions, extraData);
				}
				else
				{
					ExWatson.SendReportAndCrashOnAnotherThread(exception, reportOptions, null, extraData);
				}
			}
			finally
			{
				if (terminateProccess)
				{
					try
					{
						using (Process currentProcess = Process.GetCurrentProcess())
						{
							currentProcess.Kill();
						}
					}
					catch (Win32Exception)
					{
					}
					Environment.Exit(-559034355);
				}
			}
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000DE68 File Offset: 0x0000C068
		private void LogLine(DiagnosticsLog log, DiagnosticsLoggingTag tag, params object[] data)
		{
			string text = log.Append(data);
			if (text != string.Empty)
			{
				if (tag != DiagnosticsLoggingTag.Informational && tag == DiagnosticsLoggingTag.Failures)
				{
					this.TraceError(text, new object[0]);
					return;
				}
				this.TraceDebug(text, new object[0]);
			}
		}

		// Token: 0x040001EA RID: 490
		internal const int DiagnosticsSessionSchemaVersion = 1;

		// Token: 0x040001EB RID: 491
		private const int CrawlerSchemaVersion = 3;

		// Token: 0x040001EC RID: 492
		private const int GracefulDegradationSchemaVersion = 1;

		// Token: 0x040001ED RID: 493
		private const int DictionaryLogSchemaVersion = 1;

		// Token: 0x040001EE RID: 494
		private static string[] schemaColumns = new string[]
		{
			"date-time",
			"type",
			"version",
			"component",
			"entryId",
			"operation",
			"operation-specific"
		};

		// Token: 0x040001EF RID: 495
		private static string[] crawlerSchemaColumns = new string[]
		{
			"date-time",
			"component",
			"version",
			"operationId",
			"databaseName",
			"mailboxGuid",
			"operation"
		};

		// Token: 0x040001F0 RID: 496
		private static string[] gracefulDegradationSchemaColumns = new string[]
		{
			"date-time",
			"component",
			"version",
			"totalmemory",
			"availablememory",
			"memoryusage",
			"expectedmemoryusage",
			"searchmemorydriftratio",
			"operation"
		};

		// Token: 0x040001F1 RID: 497
		private static string[] dictionarySchemaColumns = new string[]
		{
			"date-time",
			"component",
			"version",
			"operationId",
			"correlationId",
			"databaseGuid",
			"mailboxGuid",
			"operation"
		};

		// Token: 0x040001F2 RID: 498
		private static Hookable<IDiagnosticsSessionFactory> hookableDiagnosticsSessionFactory = Hookable<IDiagnosticsSessionFactory>.Create(true, new DiagnosticsSessionFactory());

		// Token: 0x040001F3 RID: 499
		private static ExEventLog eventLog;

		// Token: 0x040001F4 RID: 500
		private static string appName;

		// Token: 0x040001F5 RID: 501
		private static string appVersion;

		// Token: 0x040001F6 RID: 502
		private static bool crashOnUnhandledException = true;

		// Token: 0x040001F7 RID: 503
		private readonly string eventLogSourceName;

		// Token: 0x040001F8 RID: 504
		private string componentName;

		// Token: 0x040001F9 RID: 505
		private IIdentity documentId;

		// Token: 0x040001FA RID: 506
		private Microsoft.Exchange.Diagnostics.Trace tracer;

		// Token: 0x040001FB RID: 507
		private long traceContext;

		// Token: 0x040001FC RID: 508
		private DiagnosticsLog diagnosticsLog;

		// Token: 0x040001FD RID: 509
		private DiagnosticsLog crawlerLog;

		// Token: 0x040001FE RID: 510
		private DiagnosticsLog gracefulDegradationLog;

		// Token: 0x040001FF RID: 511
		private DiagnosticsLog dictionaryLog;

		// Token: 0x04000200 RID: 512
		private DiagnosticsLogConfig.LogDefaults logDefaults;

		// Token: 0x04000201 RID: 513
		private DiagnosticsLogConfig.LogDefaults crawlerLogDefaults;

		// Token: 0x04000202 RID: 514
		private DiagnosticsLogConfig.LogDefaults gracefulDegradationLogDefaults;

		// Token: 0x04000203 RID: 515
		private DiagnosticsLogConfig.LogDefaults dictionaryLogDefaults;
	}
}
