using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Win32;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200005E RID: 94
	public class LoggerManager : DisposableBase, ILoggerFactory, IDisposable
	{
		// Token: 0x0600053D RID: 1341 RVA: 0x0000E48C File Offset: 0x0000C68C
		private LoggerManager(IBinaryLogger longOperationLogger, IBinaryLogger ropSummaryLogger, IBinaryLogger fullTextIndexLogger, IBinaryLogger diagnosticQueryLogger, IBinaryLogger referenceDataLogger, IBinaryLogger heavyClientActivityLogger, IBinaryLogger breadCrumbsLogger, IBinaryLogger syntheticCountersLogger)
		{
			this.longOperationLogger = longOperationLogger;
			this.ropSummaryLogger = ropSummaryLogger;
			this.fullTextIndexLogger = fullTextIndexLogger;
			this.diagnosticQueryLogger = diagnosticQueryLogger;
			this.referenceDataLogger = referenceDataLogger;
			this.heavyClientActivityLogger = heavyClientActivityLogger;
			this.breadCrumbsLogger = breadCrumbsLogger;
			this.syntheticCountersLogger = syntheticCountersLogger;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x0000E4DC File Offset: 0x0000C6DC
		private static Hookable<ILoggerFactory> LoggerFactory
		{
			get
			{
				if (LoggerManager.loggerFactory == null)
				{
					using (LockManager.Lock(LoggerManager.loggerFactoryLockObject))
					{
						if (LoggerManager.loggerFactory == null)
						{
							Interlocked.Exchange<Hookable<ILoggerFactory>>(ref LoggerManager.loggerFactory, Hookable<ILoggerFactory>.Create(true, LoggerManager.Create()));
						}
					}
				}
				return LoggerManager.loggerFactory;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x0000E540 File Offset: 0x0000C740
		private static ILoggerFactory LoggerFactoryInstance
		{
			get
			{
				return LoggerManager.LoggerFactory.Value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x0000E54C File Offset: 0x0000C74C
		private IBinaryLogger LongOperation
		{
			get
			{
				return this.longOperationLogger;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0000E554 File Offset: 0x0000C754
		private IBinaryLogger RopSummary
		{
			get
			{
				return this.ropSummaryLogger;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x0000E55C File Offset: 0x0000C75C
		private IBinaryLogger FullTextIndex
		{
			get
			{
				return this.fullTextIndexLogger;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0000E564 File Offset: 0x0000C764
		private IBinaryLogger DiagnosticQuery
		{
			get
			{
				return this.diagnosticQueryLogger;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0000E56C File Offset: 0x0000C76C
		private IBinaryLogger ReferenceData
		{
			get
			{
				return this.referenceDataLogger;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0000E574 File Offset: 0x0000C774
		private IBinaryLogger HeavyClientActivity
		{
			get
			{
				return this.heavyClientActivityLogger;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0000E57C File Offset: 0x0000C77C
		private IBinaryLogger BreadCrumbs
		{
			get
			{
				return this.breadCrumbsLogger;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0000E584 File Offset: 0x0000C784
		private IBinaryLogger SyntheticCounters
		{
			get
			{
				return this.syntheticCountersLogger;
			}
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0000E58C File Offset: 0x0000C78C
		public static void Terminate()
		{
			LoggerManager.LoggerFactoryInstance.Dispose();
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0000E598 File Offset: 0x0000C798
		public static void DoTraceLogDirectoryMaintenance()
		{
			foreach (EtwLoggerDefinition etwLoggerDefinition in LoggerManager.LoggerDefinitions.All)
			{
				LoggerManager.InternalTraceLogDirectoryMaintenance(LoggerManager.GetLogPath(), etwLoggerDefinition.LogFilePrefixName, etwLoggerDefinition.MaximumTotalFilesSizeMB * 1024U * 1024U, etwLoggerDefinition.RetentionLimit);
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x0000E5F4 File Offset: 0x0000C7F4
		public static void StartAllTraceSessions()
		{
			foreach (EtwLoggerDefinition definition in LoggerManager.LoggerDefinitions.All)
			{
				if (LoggerManager.LoggerFactoryInstance.IsTracingEnabled(definition.LoggerType))
				{
					LoggerManager.StartTraceSession(LoggerManager.GetLogFileName(definition), definition.LogFilePrefixName, definition);
				}
			}
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x0000E648 File Offset: 0x0000C848
		public static void StopAllTraceSessions()
		{
			foreach (EtwLoggerDefinition etwLoggerDefinition in LoggerManager.LoggerDefinitions.All)
			{
				LoggerManager.StopTraceSession(etwLoggerDefinition.LogFilePrefixName);
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0000E684 File Offset: 0x0000C884
		public static IBinaryLogger GetLogger(LoggerType type)
		{
			switch (type)
			{
			case LoggerType.LongOperation:
			case LoggerType.RopSummary:
			case LoggerType.FullTextIndex:
			case LoggerType.DiagnosticQuery:
			case LoggerType.ReferenceData:
			case LoggerType.HeavyClientActivity:
			case LoggerType.BreadCrumbs:
			case LoggerType.SyntheticCounters:
				return LoggerManager.LoggerFactoryInstance.GetLoggerInstance(type);
			default:
				throw new StoreException((LID)40508U, ErrorCodeValue.CallFailed, "Invalid ETW logger type");
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0000E6E8 File Offset: 0x0000C8E8
		internal static void InternalTraceLogDirectoryMaintenance(string logPath, string filePrefix, uint maximumSizeBytes, TimeSpan maximumRetention)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(logPath);
			List<LoggerManager.TraceFileInfo> list = null;
			try
			{
				list = new List<LoggerManager.TraceFileInfo>(from info in directoryInfo.GetFiles(filePrefix + "*")
				select LoggerManager.TraceFileInfo.Create(info));
			}
			catch (UnauthorizedAccessException ex)
			{
				Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_TraceMaintenanceFailed, new object[]
				{
					DiagnosticsNativeMethods.GetCurrentProcessId(),
					filePrefix,
					ex
				});
				NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
			}
			if (list != null && list.Count > 0)
			{
				LoggerManager.InternalTraceLogDirectoryMaintenance(list, maximumSizeBytes, maximumRetention);
			}
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0000E7A8 File Offset: 0x0000C9A8
		internal static void InternalTraceLogDirectoryMaintenance(List<LoggerManager.TraceFileInfo> files, uint maximumSizeBytes, TimeSpan maximumRetention)
		{
			long num = 0L;
			DateTime dateTime = DateTime.MaxValue;
			DateTime t = DateTime.UtcNow - maximumRetention;
			foreach (LoggerManager.TraceFileInfo traceFileInfo in files)
			{
				num += traceFileInfo.Length;
				dateTime = ((traceFileInfo.LastWriteTimeUtc < dateTime) ? traceFileInfo.LastWriteTimeUtc : dateTime);
			}
			if (num <= (long)((ulong)maximumSizeBytes) && dateTime >= t)
			{
				return;
			}
			files.Sort((LoggerManager.TraceFileInfo first, LoggerManager.TraceFileInfo second) => DateTime.Compare(first.CreationTimeUtc, second.CreationTimeUtc));
			foreach (LoggerManager.TraceFileInfo traceFileInfo2 in files)
			{
				if (num <= (long)((ulong)maximumSizeBytes) && traceFileInfo2.LastWriteTimeUtc >= t)
				{
					break;
				}
				num -= traceFileInfo2.Length;
				try
				{
					File.Delete(traceFileInfo2.FullName);
				}
				catch (IOException exception)
				{
					NullExecutionDiagnostics.Instance.OnExceptionCatch(exception);
				}
				catch (UnauthorizedAccessException exception2)
				{
					NullExecutionDiagnostics.Instance.OnExceptionCatch(exception2);
				}
			}
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x0000E8F4 File Offset: 0x0000CAF4
		internal static IDisposable SetTestHook(ILoggerFactory factory)
		{
			return LoggerManager.LoggerFactory.SetTestHook(factory);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0000E901 File Offset: 0x0000CB01
		private static LoggerManager Create()
		{
			return new LoggerManager(LoggerManager.CreateLogger(LoggerType.LongOperation), LoggerManager.CreateLogger(LoggerType.RopSummary), LoggerManager.CreateLogger(LoggerType.FullTextIndex), LoggerManager.CreateLogger(LoggerType.DiagnosticQuery), LoggerManager.CreateLogger(LoggerType.ReferenceData), LoggerManager.CreateLogger(LoggerType.HeavyClientActivity), LoggerManager.CreateLogger(LoggerType.BreadCrumbs), LoggerManager.CreateLogger(LoggerType.SyntheticCounters));
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x0000E938 File Offset: 0x0000CB38
		private static IBinaryLogger CreateLogger(LoggerType type)
		{
			IBinaryLogger binaryLogger = null;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				binaryLogger = disposeGuard.Add<IBinaryLogger>(LoggerManager.InternalCreate(type));
				if (binaryLogger != null)
				{
					binaryLogger.Start();
				}
				disposeGuard.Success();
			}
			return binaryLogger;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0000E990 File Offset: 0x0000CB90
		private static IBinaryLogger InternalCreate(LoggerType type)
		{
			switch (type)
			{
			case LoggerType.LongOperation:
				return LoggerManager.InternalCreate(LoggerManager.LoggerDefinitions.LongOperation);
			case LoggerType.RopSummary:
				return LoggerManager.InternalCreate(LoggerManager.LoggerDefinitions.RopSummary);
			case LoggerType.FullTextIndex:
				return LoggerManager.InternalCreate(LoggerManager.LoggerDefinitions.FullTextIndex);
			case LoggerType.DiagnosticQuery:
				return LoggerManager.InternalCreate(LoggerManager.LoggerDefinitions.DiagnosticQuery);
			case LoggerType.ReferenceData:
				return LoggerManager.InternalCreate(LoggerManager.LoggerDefinitions.ReferenceData);
			case LoggerType.HeavyClientActivity:
				return LoggerManager.InternalCreate(LoggerManager.LoggerDefinitions.HeavyClientActivity);
			case LoggerType.BreadCrumbs:
				return LoggerManager.InternalCreate(LoggerManager.LoggerDefinitions.BreadCrumbs);
			case LoggerType.SyntheticCounters:
				return LoggerManager.InternalCreate(LoggerManager.LoggerDefinitions.SyntheticCounters);
			default:
				throw new ArgumentException("type");
			}
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0000EA29 File Offset: 0x0000CC29
		private static IBinaryLogger InternalCreate(EtwLoggerDefinition definition)
		{
			return EtwBinaryLogger.Create(definition.LogFilePrefixName, definition.ProviderGuid);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0000EA40 File Offset: 0x0000CC40
		private static string GetLogPath()
		{
			string oldValue = "%ExchangeInstallDir%";
			string value = ConfigurationSchema.LogPath.Value;
			string value2 = RegistryReader.Instance.GetValue<string>(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup", "MsiInstallPath", string.Empty);
			string text = value.Replace(oldValue, value2);
			Directory.CreateDirectory(text);
			return text;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0000EA90 File Offset: 0x0000CC90
		private static string GetLogFileName(EtwLoggerDefinition definition)
		{
			string str = definition.LogFilePrefixName + DateTime.UtcNow.ToString("_yyyyMMdd-HHmmss-fffffff");
			if (definition.FileModeCreateNew)
			{
				str += "_%d";
			}
			return Path.Combine(LoggerManager.GetLogPath(), str + ".etl");
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0000EAE8 File Offset: 0x0000CCE8
		private static DiagnosticsNativeMethods.EventTraceProperties CreateTraceProperties(string logFileName, string sessionName, EtwLoggerDefinition definition)
		{
			DiagnosticsNativeMethods.EventTraceProperties eventTraceProperties = default(DiagnosticsNativeMethods.EventTraceProperties);
			DiagnosticsNativeMethods.LogFileMode logFileMode = definition.FileModeCreateNew ? DiagnosticsNativeMethods.LogFileMode.EVENT_TRACE_FILE_MODE_NEWFILE : DiagnosticsNativeMethods.LogFileMode.EVENT_TRACE_FILE_MODE_CIRCULAR;
			eventTraceProperties.etp.wnode.bufferSize = (uint)Marshal.SizeOf(eventTraceProperties);
			eventTraceProperties.etp.wnode.guid = Guid.NewGuid();
			eventTraceProperties.etp.wnode.flags = 131072U;
			eventTraceProperties.etp.wnode.clientContext = 1U;
			eventTraceProperties.etp.bufferSize = definition.MemoryBufferSizeKB;
			eventTraceProperties.etp.minimumBuffers = definition.MinimumNumberOfMemoryBuffers;
			eventTraceProperties.etp.maximumBuffers = definition.NumberOfMemoryBuffers;
			eventTraceProperties.etp.maximumFileSize = definition.LogFileSizeMB;
			eventTraceProperties.etp.logFileMode = (uint)(logFileMode | DiagnosticsNativeMethods.LogFileMode.EVENT_TRACE_USE_PAGED_MEMORY | DiagnosticsNativeMethods.LogFileMode.EVENT_TRACE_USE_LOCAL_SEQUENCE);
			eventTraceProperties.etp.flushTimer = (uint)definition.FlushTimer.TotalSeconds;
			eventTraceProperties.etp.enableFlags = 0U;
			eventTraceProperties.etp.logFileNameOffset = (uint)((int)Marshal.OffsetOf(typeof(DiagnosticsNativeMethods.EventTraceProperties), "logFileName"));
			eventTraceProperties.etp.loggerNameOffset = (uint)((int)Marshal.OffsetOf(typeof(DiagnosticsNativeMethods.EventTraceProperties), "loggerName"));
			eventTraceProperties.logFileName = logFileName;
			eventTraceProperties.loggerName = sessionName;
			return eventTraceProperties;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0000EC4C File Offset: 0x0000CE4C
		public IBinaryLogger GetLoggerInstance(LoggerType type)
		{
			switch (type)
			{
			case LoggerType.LongOperation:
				return this.longOperationLogger;
			case LoggerType.RopSummary:
				return this.ropSummaryLogger;
			case LoggerType.FullTextIndex:
				return this.fullTextIndexLogger;
			case LoggerType.DiagnosticQuery:
				return this.diagnosticQueryLogger;
			case LoggerType.ReferenceData:
				return this.referenceDataLogger;
			case LoggerType.HeavyClientActivity:
				return this.heavyClientActivityLogger;
			case LoggerType.BreadCrumbs:
				return this.breadCrumbsLogger;
			case LoggerType.SyntheticCounters:
				return this.syntheticCountersLogger;
			default:
				throw new StoreException((LID)43728U, ErrorCodeValue.CallFailed, "Invalid ETW logger type");
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0000ECD4 File Offset: 0x0000CED4
		public bool IsTracingEnabled(LoggerType type)
		{
			switch (type)
			{
			case LoggerType.LongOperation:
				return ConfigurationSchema.EnableTraceLongOperation.Value;
			case LoggerType.RopSummary:
				return ConfigurationSchema.EnableTraceRopSummary.Value;
			case LoggerType.FullTextIndex:
				return ConfigurationSchema.EnableTraceFullTextIndexQuery.Value;
			case LoggerType.DiagnosticQuery:
				return ConfigurationSchema.EnableTraceDiagnosticQuery.Value;
			case LoggerType.ReferenceData:
				return ConfigurationSchema.EnableTraceReferenceData.Value;
			case LoggerType.HeavyClientActivity:
				return ConfigurationSchema.EnableTraceHeavyClientActivity.Value;
			case LoggerType.BreadCrumbs:
				return ConfigurationSchema.EnableTraceBreadCrumbs.Value;
			case LoggerType.SyntheticCounters:
				return ConfigurationSchema.EnableTraceSyntheticCounters.Value;
			default:
				throw new ArgumentException("type");
			}
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0000ED70 File Offset: 0x0000CF70
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (this.longOperationLogger != null)
				{
					this.longOperationLogger.Dispose();
					this.longOperationLogger = null;
				}
				if (this.ropSummaryLogger != null)
				{
					this.ropSummaryLogger.Dispose();
					this.ropSummaryLogger = null;
				}
				if (this.fullTextIndexLogger != null)
				{
					this.fullTextIndexLogger.Dispose();
					this.fullTextIndexLogger = null;
				}
				if (this.diagnosticQueryLogger != null)
				{
					this.diagnosticQueryLogger.Dispose();
					this.diagnosticQueryLogger = null;
				}
				if (this.referenceDataLogger != null)
				{
					this.referenceDataLogger.Dispose();
					this.referenceDataLogger = null;
				}
				if (this.heavyClientActivityLogger != null)
				{
					this.heavyClientActivityLogger.Dispose();
					this.heavyClientActivityLogger = null;
				}
				if (this.breadCrumbsLogger != null)
				{
					this.breadCrumbsLogger.Dispose();
					this.breadCrumbsLogger = null;
				}
				if (this.syntheticCountersLogger != null)
				{
					this.syntheticCountersLogger.Dispose();
					this.syntheticCountersLogger = null;
				}
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0000EE53 File Offset: 0x0000D053
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<LoggerManager>(this);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x0000EE5C File Offset: 0x0000D05C
		internal static void StartTraceSession(string logFileName, string sessionName, EtwLoggerDefinition definition)
		{
			int currentProcessId = DiagnosticsNativeMethods.GetCurrentProcessId();
			DiagnosticsNativeMethods.EventTraceProperties eventTraceProperties = LoggerManager.CreateTraceProperties(logFileName, sessionName, definition);
			long sessionHandle;
			uint num = DiagnosticsNativeMethods.StartTrace(out sessionHandle, sessionName, ref eventTraceProperties);
			if (num != 0U)
			{
				Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_StartTraceSessionFailed, new object[]
				{
					"StartTrace",
					sessionName,
					definition.ProviderGuid,
					currentProcessId,
					num
				});
				return;
			}
			Guid providerGuid = definition.ProviderGuid;
			num = DiagnosticsNativeMethods.EnableTrace(1U, 0U, 5U, ref providerGuid, sessionHandle);
			if (num != 0U)
			{
				Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_StartTraceSessionFailed, new object[]
				{
					"EnableTrace",
					sessionName,
					definition.ProviderGuid,
					currentProcessId,
					num
				});
			}
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0000EF2C File Offset: 0x0000D12C
		internal static void StopTraceSession(string sessionName)
		{
			DiagnosticsNativeMethods.EventTraceProperties eventTraceProperties = LoggerManager.CreateTraceProperties(string.Empty, sessionName, default(EtwLoggerDefinition));
			DiagnosticsNativeMethods.ControlTrace(0L, sessionName, ref eventTraceProperties, 1U);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x0000EF5C File Offset: 0x0000D15C
		internal static void FlushSessionLog(string logFileName, string sessionName)
		{
			DiagnosticsNativeMethods.EventTraceProperties eventTraceProperties = LoggerManager.CreateTraceProperties(logFileName, sessionName, default(EtwLoggerDefinition));
			uint num = DiagnosticsNativeMethods.ControlTrace(0L, sessionName, ref eventTraceProperties, 3U);
			if (num != 0U)
			{
				throw new StoreException((LID)62584U, ErrorCodeValue.CallFailed, string.Format("ControlTrace(FLUSH) failed: {0}", num));
			}
		}

		// Token: 0x04000569 RID: 1385
		private static object loggerFactoryLockObject = new object();

		// Token: 0x0400056A RID: 1386
		private static Hookable<ILoggerFactory> loggerFactory;

		// Token: 0x0400056B RID: 1387
		private IBinaryLogger longOperationLogger;

		// Token: 0x0400056C RID: 1388
		private IBinaryLogger ropSummaryLogger;

		// Token: 0x0400056D RID: 1389
		private IBinaryLogger fullTextIndexLogger;

		// Token: 0x0400056E RID: 1390
		private IBinaryLogger diagnosticQueryLogger;

		// Token: 0x0400056F RID: 1391
		private IBinaryLogger referenceDataLogger;

		// Token: 0x04000570 RID: 1392
		private IBinaryLogger heavyClientActivityLogger;

		// Token: 0x04000571 RID: 1393
		private IBinaryLogger breadCrumbsLogger;

		// Token: 0x04000572 RID: 1394
		private IBinaryLogger syntheticCountersLogger;

		// Token: 0x0200005F RID: 95
		internal static class TraceGuids
		{
			// Token: 0x04000575 RID: 1397
			public static readonly Guid LongOperationDetail = new Guid("{4edb6394-0bf6-4feb-ad88-5eb8a73ae5fe}");

			// Token: 0x04000576 RID: 1398
			public static readonly Guid LongOperationSummary = new Guid("{a7386ff4-27f7-4546-9573-05eac353d033}");

			// Token: 0x04000577 RID: 1399
			public static readonly Guid RopSummary = new Guid("{b91ae2e7-f3c7-4ad5-aa79-748b093b60ef}");

			// Token: 0x04000578 RID: 1400
			public static readonly Guid DiagnosticQuery = new Guid("{a3bdbaa4-d7be-4103-8930-4ce1637e6ae7}");

			// Token: 0x04000579 RID: 1401
			public static readonly Guid FullTextIndexQuery = new Guid("{969a0e45-d76e-4b62-9628-6ab4986ad6f0}");

			// Token: 0x0400057A RID: 1402
			public static readonly Guid ClientType = new Guid("{5e419c98-31b0-421e-8ac7-724c4bc308a1}");

			// Token: 0x0400057B RID: 1403
			public static readonly Guid DatabaseInfo = new Guid("{b3d00223-c4f4-4684-a7be-2d35125dbe7f}");

			// Token: 0x0400057C RID: 1404
			public static readonly Guid ErrorCode = new Guid("{af8c813c-1ad2-49ce-9346-29a119cd4a58}");

			// Token: 0x0400057D RID: 1405
			public static readonly Guid RopId = new Guid("{f829d186-4bc8-4a68-b324-baecac2484d5}");

			// Token: 0x0400057E RID: 1406
			public static readonly Guid MailboxInfo = new Guid("{b3739cd6-30a8-43a7-a9d8-621d7852953c}");

			// Token: 0x0400057F RID: 1407
			public static readonly Guid FullTextIndexDetail = new Guid("{b1c468c0-e08c-42dc-88ca-d4ee662582b9}");

			// Token: 0x04000580 RID: 1408
			public static readonly Guid FullTextIndexSingleLine = new Guid("{260accff-993c-478a-be98-191c6a8a353a}");

			// Token: 0x04000581 RID: 1409
			public static readonly Guid ActivityInfo = new Guid("{c9fe591e-0508-4431-ba5b-0f7e1460c9a2}");

			// Token: 0x04000582 RID: 1410
			public static readonly Guid HeavyClientActivityDetail = new Guid("{415858e9-d31c-4bff-b7ca-96a35939d9a0}");

			// Token: 0x04000583 RID: 1411
			public static readonly Guid HeavyClientActivitySummary = new Guid("{166c6b8f-e9b8-4084-b61e-3d44f1f66b6e}");

			// Token: 0x04000584 RID: 1412
			public static readonly Guid BreadCrumbs = new Guid("{d644df02-3a11-45b2-9970-a16c39b7b222}");

			// Token: 0x04000585 RID: 1413
			public static readonly Guid ServerInfo = new Guid("{29b93039-3837-4656-8496-2714244c9ed6}");

			// Token: 0x04000586 RID: 1414
			public static readonly Guid OperationType = new Guid("{9bb5db73-7b59-46ae-bf7c-3988da83983d}");

			// Token: 0x04000587 RID: 1415
			public static readonly Guid OperationDetail = new Guid("{afec1b6c-a307-4bd4-80e6-bc8d0190453f}");

			// Token: 0x04000588 RID: 1416
			public static readonly Guid TaskType = new Guid("{852004a4-f4ea-45a0-99d3-4c264126ee55}");

			// Token: 0x04000589 RID: 1417
			public static readonly Guid AdminMethod = new Guid("{5e6fa64a-892a-4877-8751-d829b3c6d35a}");

			// Token: 0x0400058A RID: 1418
			public static readonly Guid SyntheticCounters = new Guid("{061016d0-2d4d-4a46-a0dc-b5061303b605}");

			// Token: 0x0400058B RID: 1419
			public static readonly Guid MailboxStatus = new Guid("{9d28577c-6344-43bb-9457-329048432c2c}");

			// Token: 0x0400058C RID: 1420
			public static readonly Guid MailboxType = new Guid("{48f9bb9e-764e-4e2b-825d-f8cd4e8f9448}");

			// Token: 0x0400058D RID: 1421
			public static readonly Guid MailboxTypeDetail = new Guid("{626325ae-bd70-4026-bc95-c962dcf76e44}");

			// Token: 0x0400058E RID: 1422
			public static readonly Guid BreadCrumbKind = new Guid("{36a346c1-792b-447d-8e5f-f5c219537487}");

			// Token: 0x0400058F RID: 1423
			public static readonly Guid OperationSource = new Guid("{eb325a9f-ca07-4896-a117-c8326f95deac}");
		}

		// Token: 0x02000060 RID: 96
		private static class LoggerDefinitions
		{
			// Token: 0x17000115 RID: 277
			// (get) Token: 0x06000562 RID: 1378 RVA: 0x0000F15E File Offset: 0x0000D35E
			public static EtwLoggerDefinition LongOperation
			{
				get
				{
					return LoggerManager.LoggerDefinitions.longOperationDefinition;
				}
			}

			// Token: 0x17000116 RID: 278
			// (get) Token: 0x06000563 RID: 1379 RVA: 0x0000F165 File Offset: 0x0000D365
			public static EtwLoggerDefinition RopSummary
			{
				get
				{
					return LoggerManager.LoggerDefinitions.ropSummaryDefinition;
				}
			}

			// Token: 0x17000117 RID: 279
			// (get) Token: 0x06000564 RID: 1380 RVA: 0x0000F16C File Offset: 0x0000D36C
			public static EtwLoggerDefinition FullTextIndex
			{
				get
				{
					return LoggerManager.LoggerDefinitions.fullTextIndexDefinition;
				}
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x06000565 RID: 1381 RVA: 0x0000F173 File Offset: 0x0000D373
			public static EtwLoggerDefinition DiagnosticQuery
			{
				get
				{
					return LoggerManager.LoggerDefinitions.diagnosticQueryDefinition;
				}
			}

			// Token: 0x17000119 RID: 281
			// (get) Token: 0x06000566 RID: 1382 RVA: 0x0000F17A File Offset: 0x0000D37A
			public static EtwLoggerDefinition ReferenceData
			{
				get
				{
					return LoggerManager.LoggerDefinitions.referenceDataDefinition;
				}
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x06000567 RID: 1383 RVA: 0x0000F181 File Offset: 0x0000D381
			public static EtwLoggerDefinition HeavyClientActivity
			{
				get
				{
					return LoggerManager.LoggerDefinitions.heavyClientActivityDefinition;
				}
			}

			// Token: 0x1700011B RID: 283
			// (get) Token: 0x06000568 RID: 1384 RVA: 0x0000F188 File Offset: 0x0000D388
			public static EtwLoggerDefinition BreadCrumbs
			{
				get
				{
					return LoggerManager.LoggerDefinitions.breadCrumbsDefinition;
				}
			}

			// Token: 0x1700011C RID: 284
			// (get) Token: 0x06000569 RID: 1385 RVA: 0x0000F18F File Offset: 0x0000D38F
			public static EtwLoggerDefinition SyntheticCounters
			{
				get
				{
					return LoggerManager.LoggerDefinitions.syntheticCountersDefinition;
				}
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x0600056A RID: 1386 RVA: 0x0000F196 File Offset: 0x0000D396
			public static EtwLoggerDefinition[] All
			{
				get
				{
					return LoggerManager.LoggerDefinitions.allDefinitions;
				}
			}

			// Token: 0x04000590 RID: 1424
			private static EtwLoggerDefinition longOperationDefinition = new EtwLoggerDefinition
			{
				LoggerType = LoggerType.LongOperation,
				LogFilePrefixName = "LongOperation",
				ProviderGuid = new Guid("{6551ea1e-9124-4e76-a971-50ef868272f1}"),
				LogFileSizeMB = 10U,
				MemoryBufferSizeKB = 128U,
				MinimumNumberOfMemoryBuffers = 2U,
				NumberOfMemoryBuffers = 100U,
				MaximumTotalFilesSizeMB = 1000U,
				FileModeCreateNew = true,
				FlushTimer = TimeSpan.FromHours(1.0),
				RetentionLimit = TimeSpan.FromDays(30.0)
			};

			// Token: 0x04000591 RID: 1425
			private static EtwLoggerDefinition ropSummaryDefinition = new EtwLoggerDefinition
			{
				LoggerType = LoggerType.RopSummary,
				LogFilePrefixName = "RopSummary",
				ProviderGuid = new Guid("{A6EAE9C9-5A1C-452d-A1B2-F65BE3918D74}"),
				LogFileSizeMB = 10U,
				MemoryBufferSizeKB = 128U,
				MinimumNumberOfMemoryBuffers = 2U,
				NumberOfMemoryBuffers = 100U,
				MaximumTotalFilesSizeMB = 2500U,
				FileModeCreateNew = true,
				RetentionLimit = TimeSpan.FromDays(14.0)
			};

			// Token: 0x04000592 RID: 1426
			private static EtwLoggerDefinition fullTextIndexDefinition = new EtwLoggerDefinition
			{
				LoggerType = LoggerType.FullTextIndex,
				LogFilePrefixName = "FullTextIndexQuery",
				ProviderGuid = new Guid("{7609B5F1-F8ED-4798-913C-1541723BFF60}"),
				LogFileSizeMB = 10U,
				MemoryBufferSizeKB = 128U,
				MinimumNumberOfMemoryBuffers = 2U,
				NumberOfMemoryBuffers = 100U,
				MaximumTotalFilesSizeMB = 1000U,
				FileModeCreateNew = true,
				FlushTimer = TimeSpan.FromHours(1.0),
				RetentionLimit = TimeSpan.FromDays(30.0)
			};

			// Token: 0x04000593 RID: 1427
			private static EtwLoggerDefinition diagnosticQueryDefinition = new EtwLoggerDefinition
			{
				LoggerType = LoggerType.DiagnosticQuery,
				LogFilePrefixName = "DiagnosticQuery",
				ProviderGuid = new Guid("{1B54734A-5DD9-44B5-A0F4-B613624C2AC9}"),
				LogFileSizeMB = 10U,
				MemoryBufferSizeKB = 128U,
				MinimumNumberOfMemoryBuffers = 2U,
				NumberOfMemoryBuffers = 100U,
				MaximumTotalFilesSizeMB = 1000U,
				FileModeCreateNew = true,
				FlushTimer = TimeSpan.FromHours(1.0),
				RetentionLimit = TimeSpan.FromDays(365.0)
			};

			// Token: 0x04000594 RID: 1428
			private static EtwLoggerDefinition referenceDataDefinition = new EtwLoggerDefinition
			{
				LoggerType = LoggerType.ReferenceData,
				LogFilePrefixName = "ReferenceData",
				ProviderGuid = new Guid("{E434A360-74A6-4A3F-957B-1F69D1006302}"),
				LogFileSizeMB = 10U,
				MemoryBufferSizeKB = 128U,
				MinimumNumberOfMemoryBuffers = 2U,
				NumberOfMemoryBuffers = 100U,
				MaximumTotalFilesSizeMB = 1000U,
				FileModeCreateNew = true,
				FlushTimer = TimeSpan.FromHours(6.0),
				RetentionLimit = TimeSpan.FromDays(30.0)
			};

			// Token: 0x04000595 RID: 1429
			private static EtwLoggerDefinition heavyClientActivityDefinition = new EtwLoggerDefinition
			{
				LoggerType = LoggerType.HeavyClientActivity,
				LogFilePrefixName = "HeavyClientActivity",
				ProviderGuid = new Guid("{d2371af6-80ff-4c1a-8e2e-e7f12bced4ec}"),
				LogFileSizeMB = 10U,
				MemoryBufferSizeKB = 128U,
				MinimumNumberOfMemoryBuffers = 2U,
				NumberOfMemoryBuffers = 100U,
				MaximumTotalFilesSizeMB = 1000U,
				FileModeCreateNew = true,
				FlushTimer = TimeSpan.FromHours(1.0),
				RetentionLimit = TimeSpan.FromDays(30.0)
			};

			// Token: 0x04000596 RID: 1430
			private static EtwLoggerDefinition breadCrumbsDefinition = new EtwLoggerDefinition
			{
				LoggerType = LoggerType.BreadCrumbs,
				LogFilePrefixName = "BreadCrumbs",
				ProviderGuid = new Guid("{ee60aded-233a-41d7-98d7-6f72e2b74f32}"),
				LogFileSizeMB = 10U,
				MemoryBufferSizeKB = 128U,
				MinimumNumberOfMemoryBuffers = 2U,
				NumberOfMemoryBuffers = 100U,
				MaximumTotalFilesSizeMB = 2500U,
				FileModeCreateNew = true,
				FlushTimer = TimeSpan.FromHours(1.0),
				RetentionLimit = TimeSpan.FromDays(14.0)
			};

			// Token: 0x04000597 RID: 1431
			private static EtwLoggerDefinition syntheticCountersDefinition = new EtwLoggerDefinition
			{
				LoggerType = LoggerType.SyntheticCounters,
				LogFilePrefixName = "SyntheticCounters",
				ProviderGuid = new Guid("{55a1769d-5ac5-4f8e-b1c9-37d3a2bb1305}"),
				LogFileSizeMB = 10U,
				MemoryBufferSizeKB = 128U,
				MinimumNumberOfMemoryBuffers = 2U,
				NumberOfMemoryBuffers = 100U,
				MaximumTotalFilesSizeMB = 2500U,
				FileModeCreateNew = true,
				FlushTimer = TimeSpan.FromHours(1.0),
				RetentionLimit = TimeSpan.FromDays(14.0)
			};

			// Token: 0x04000598 RID: 1432
			private static EtwLoggerDefinition[] allDefinitions = new EtwLoggerDefinition[]
			{
				LoggerManager.LoggerDefinitions.longOperationDefinition,
				LoggerManager.LoggerDefinitions.ropSummaryDefinition,
				LoggerManager.LoggerDefinitions.fullTextIndexDefinition,
				LoggerManager.LoggerDefinitions.diagnosticQueryDefinition,
				LoggerManager.LoggerDefinitions.referenceDataDefinition,
				LoggerManager.LoggerDefinitions.heavyClientActivityDefinition,
				LoggerManager.LoggerDefinitions.breadCrumbsDefinition,
				LoggerManager.LoggerDefinitions.syntheticCountersDefinition
			};
		}

		// Token: 0x02000061 RID: 97
		internal class TraceFileInfo
		{
			// Token: 0x1700011E RID: 286
			// (get) Token: 0x0600056C RID: 1388 RVA: 0x0000F6F3 File Offset: 0x0000D8F3
			// (set) Token: 0x0600056D RID: 1389 RVA: 0x0000F6FB File Offset: 0x0000D8FB
			public DateTime CreationTimeUtc { get; internal set; }

			// Token: 0x1700011F RID: 287
			// (get) Token: 0x0600056E RID: 1390 RVA: 0x0000F704 File Offset: 0x0000D904
			// (set) Token: 0x0600056F RID: 1391 RVA: 0x0000F70C File Offset: 0x0000D90C
			public string FullName { get; private set; }

			// Token: 0x17000120 RID: 288
			// (get) Token: 0x06000570 RID: 1392 RVA: 0x0000F715 File Offset: 0x0000D915
			// (set) Token: 0x06000571 RID: 1393 RVA: 0x0000F71D File Offset: 0x0000D91D
			public DateTime LastWriteTimeUtc { get; internal set; }

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x06000572 RID: 1394 RVA: 0x0000F726 File Offset: 0x0000D926
			// (set) Token: 0x06000573 RID: 1395 RVA: 0x0000F72E File Offset: 0x0000D92E
			public long Length { get; private set; }

			// Token: 0x06000574 RID: 1396 RVA: 0x0000F738 File Offset: 0x0000D938
			public static LoggerManager.TraceFileInfo Create(FileInfo info)
			{
				return new LoggerManager.TraceFileInfo
				{
					CreationTimeUtc = info.CreationTimeUtc,
					FullName = info.FullName,
					LastWriteTimeUtc = info.LastWriteTimeUtc,
					Length = info.Length
				};
			}
		}
	}
}
