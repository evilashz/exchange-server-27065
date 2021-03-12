using System;
using System.Configuration;
using System.Threading;
using Microsoft.Exchange.LogUploaderProxy;
using Microsoft.Office.Compliance.Audit.Common;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000022 RID: 34
	internal class ServiceLogger : DisposableBase
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000943F File Offset: 0x0000763F
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00009446 File Offset: 0x00007646
		public static TimeSpan MaximumLogAge { get; private set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0000944E File Offset: 0x0000764E
		// (set) Token: 0x060001CB RID: 459 RVA: 0x00009455 File Offset: 0x00007655
		public static long MaximumLogDirectorySize { get; private set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000945D File Offset: 0x0000765D
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00009464 File Offset: 0x00007664
		public static long MaximumLogFileSize { get; private set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000946C File Offset: 0x0000766C
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00009473 File Offset: 0x00007673
		public static string FileLocation { get; private set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000947B File Offset: 0x0000767B
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00009482 File Offset: 0x00007682
		public static ServiceLogger.LogLevel ServiceLogLevel { get; private set; }

		// Token: 0x060001D2 RID: 466 RVA: 0x0000948C File Offset: 0x0000768C
		public static void ReadConfiguration()
		{
			string fileLocation = "d:\\MessageTracingServiceLogs";
			ServiceLogger.ServiceLogLevel = ServiceLogger.LogLevel.Error;
			ServiceLogger.MaximumLogAge = TimeSpan.Parse("5.00:00:00");
			ServiceLogger.MaximumLogDirectorySize = 500000000L;
			ServiceLogger.MaximumLogFileSize = 5000000L;
			try
			{
				fileLocation = ConfigurationManager.AppSettings["LogFilePath"].Trim();
				ServiceLogger.ServiceLogLevel = (ServiceLogger.LogLevel)Convert.ToInt32(ConfigurationManager.AppSettings["LogLevel"]);
				ServiceLogger.MaximumLogAge = TimeSpan.Parse(ConfigurationManager.AppSettings["LogFileMaximumLogAge"]);
				ServiceLogger.MaximumLogDirectorySize = Convert.ToInt64(ConfigurationManager.AppSettings["LogFileMaximumLogDirectorySize"]);
				ServiceLogger.MaximumLogFileSize = Convert.ToInt64(ConfigurationManager.AppSettings["LogFileMaximumLogFileSize"]);
			}
			catch (Exception ex)
			{
				string text = string.Format("Fail to read config value, default values are used. The error is {0}", ex.ToString());
				EventLogger.Logger.LogEvent(LogUploaderEventLogConstants.Tuple_ConfigSettingNotFound, Thread.CurrentThread.Name, new object[]
				{
					text
				});
				EventNotificationItem.Publish(ExchangeComponent.Name, "BadServiceLoggerConfig", null, text, ResultSeverityLevel.Error, false);
				if (RetryHelper.IsSystemFatal(ex))
				{
					throw;
				}
			}
			finally
			{
				ServiceLogger.FileLocation = fileLocation;
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000095C0 File Offset: 0x000077C0
		public static void Initialize(ILogWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			ServiceLogger.serviceLog = writer;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000095D6 File Offset: 0x000077D6
		public static void LogDebug(ServiceLogger.Component componentName, LogUploaderEventLogConstants.Message message, string customData = "", string logFileType = "", string logFilePath = "")
		{
			ServiceLogger.Log(ServiceLogger.LogLevel.Debug, componentName, message, customData, logFileType, logFilePath);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000095E4 File Offset: 0x000077E4
		public static void LogInfo(ServiceLogger.Component componentName, LogUploaderEventLogConstants.Message message, string customData = "", string logFileType = "", string logFilePath = "")
		{
			ServiceLogger.Log(ServiceLogger.LogLevel.Info, componentName, message, customData, logFileType, logFilePath);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000095F2 File Offset: 0x000077F2
		public static void LogWarning(ServiceLogger.Component componentName, LogUploaderEventLogConstants.Message message, string customData = "", string logFileType = "", string logFilePath = "")
		{
			ServiceLogger.Log(ServiceLogger.LogLevel.Warning, componentName, message, customData, logFileType, logFilePath);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00009600 File Offset: 0x00007800
		public static void LogError(ServiceLogger.Component componentName, LogUploaderEventLogConstants.Message message, string customData = "", string logFileType = "", string logFilePath = "")
		{
			ServiceLogger.Log(ServiceLogger.LogLevel.Error, componentName, message, customData, logFileType, logFilePath);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000960E File Offset: 0x0000780E
		public static void Log(ServiceLogger.LogLevel logLevel, ServiceLogger.Component componentName, LogUploaderEventLogConstants.Message message, string customData, string logFileType, string logFilePath)
		{
			ServiceLogger.LogCommon(logLevel, message.ToString(), customData, componentName, logFileType, logFilePath);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00009628 File Offset: 0x00007828
		public static void LogCommon(ServiceLogger.LogLevel logLevel, string message, string customData, ServiceLogger.Component componentName = ServiceLogger.Component.None, string logFileType = "", string logFilePath = "")
		{
			if (ServiceLogger.serviceLog == null)
			{
				ServiceLogger.InitializeMtrtLog();
			}
			if (ServiceLogger.ServiceLogLevel == ServiceLogger.LogLevel.None || logLevel < ServiceLogger.ServiceLogLevel)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(ServiceLogger.MessageTracingServiceLogSchema);
			logRowFormatter[1] = logLevel.ToString();
			if (!string.IsNullOrEmpty(logFileType))
			{
				string[] array = logFileType.Split(new char[]
				{
					'_'
				});
				logRowFormatter[2] = array[0];
			}
			if (!string.IsNullOrEmpty(logFilePath))
			{
				logRowFormatter[3] = logFilePath;
			}
			logRowFormatter[4] = componentName.ToString();
			logRowFormatter[5] = message;
			if (!string.IsNullOrEmpty(customData))
			{
				logRowFormatter[6] = customData;
			}
			ServiceLogger.serviceLog.Append(logRowFormatter, 0);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000096DE File Offset: 0x000078DE
		protected override IDisposable InternalGetDisposeTracker()
		{
			return DisposeTrackerFactory.Get<ServiceLogger>(this);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000096E6 File Offset: 0x000078E6
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && ServiceLogger.serviceLog != null)
			{
				ServiceLogger.serviceLog.Close();
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000096FC File Offset: 0x000078FC
		private static void InitializeMtrtLog()
		{
			try
			{
				ServiceLogger.ReadConfiguration();
			}
			finally
			{
				Log log = new Log("MessageTracingService_", new LogHeaderFormatter(ServiceLogger.MessageTracingServiceLogSchema), "Microsoft.ForeFront.Hygiene.MessageTracing");
				log.Configure(ServiceLogger.FileLocation, ServiceLogger.MaximumLogAge, ServiceLogger.MaximumLogDirectorySize, ServiceLogger.MaximumLogFileSize, false, 0, TimeSpan.MaxValue, LogFileRollOver.Hourly);
				ServiceLogger.Initialize(log);
			}
		}

		// Token: 0x040000E7 RID: 231
		private const string FormattedLogPrefix = "MessageTracingService";

		// Token: 0x040000E8 RID: 232
		private const string Version = "1.0";

		// Token: 0x040000E9 RID: 233
		private const string LogComponentName = "Microsoft.ForeFront.Hygiene.MessageTracing";

		// Token: 0x040000EA RID: 234
		private static readonly string[] Fields = new string[]
		{
			ServiceLogger.MessageTracingServiceLogFields.Timestamp.ToString(),
			ServiceLogger.MessageTracingServiceLogFields.LogLevel.ToString(),
			ServiceLogger.MessageTracingServiceLogFields.LogFileType.ToString(),
			ServiceLogger.MessageTracingServiceLogFields.LogFilePath.ToString(),
			ServiceLogger.MessageTracingServiceLogFields.ComponentName.ToString(),
			ServiceLogger.MessageTracingServiceLogFields.Message.ToString(),
			ServiceLogger.MessageTracingServiceLogFields.CustomData.ToString()
		};

		// Token: 0x040000EB RID: 235
		private static readonly LogSchema MessageTracingServiceLogSchema = new LogSchema("Microsoft.ForeFront.Hygiene.MessageTracing", "1.0", "Message Tracing Service Log", ServiceLogger.Fields);

		// Token: 0x040000EC RID: 236
		private static ILogWriter serviceLog;

		// Token: 0x02000023 RID: 35
		public enum LogLevel
		{
			// Token: 0x040000F3 RID: 243
			None,
			// Token: 0x040000F4 RID: 244
			Debug,
			// Token: 0x040000F5 RID: 245
			Info,
			// Token: 0x040000F6 RID: 246
			Warning,
			// Token: 0x040000F7 RID: 247
			Error
		}

		// Token: 0x02000024 RID: 36
		public enum Component
		{
			// Token: 0x040000F9 RID: 249
			ADConfigReader,
			// Token: 0x040000FA RID: 250
			AsyncQueueDBWriter,
			// Token: 0x040000FB RID: 251
			DatabaseWriter,
			// Token: 0x040000FC RID: 252
			DualWriteTenantSettingBatchDBWriter,
			// Token: 0x040000FD RID: 253
			GlobalLocationService,
			// Token: 0x040000FE RID: 254
			InputBuffer,
			// Token: 0x040000FF RID: 255
			LogDataBatch,
			// Token: 0x04000100 RID: 256
			LogFileInfo,
			// Token: 0x04000101 RID: 257
			LogMonitor,
			// Token: 0x04000102 RID: 258
			LogParser,
			// Token: 0x04000103 RID: 259
			LogReader,
			// Token: 0x04000104 RID: 260
			Message,
			// Token: 0x04000105 RID: 261
			MessageBatchDBWriter,
			// Token: 0x04000106 RID: 262
			PersistentStoreDetails,
			// Token: 0x04000107 RID: 263
			SpamDigestDBWriter,
			// Token: 0x04000108 RID: 264
			SplitLogMonitor,
			// Token: 0x04000109 RID: 265
			System,
			// Token: 0x0400010A RID: 266
			TenantSettingBatchDBWriter,
			// Token: 0x0400010B RID: 267
			TransferRawDataToFFOFileWriter,
			// Token: 0x0400010C RID: 268
			TransportQueueLogDatabaseWriter,
			// Token: 0x0400010D RID: 269
			TransportQueueLogDataBatch,
			// Token: 0x0400010E RID: 270
			WatermarkFile,
			// Token: 0x0400010F RID: 271
			WebServiceWriter,
			// Token: 0x04000110 RID: 272
			SpamEngineOpticsWriter,
			// Token: 0x04000111 RID: 273
			None
		}

		// Token: 0x02000025 RID: 37
		public enum MessageTracingServiceLogFields
		{
			// Token: 0x04000113 RID: 275
			Timestamp,
			// Token: 0x04000114 RID: 276
			LogLevel,
			// Token: 0x04000115 RID: 277
			LogFileType,
			// Token: 0x04000116 RID: 278
			LogFilePath,
			// Token: 0x04000117 RID: 279
			ComponentName,
			// Token: 0x04000118 RID: 280
			Message,
			// Token: 0x04000119 RID: 281
			CustomData
		}
	}
}
