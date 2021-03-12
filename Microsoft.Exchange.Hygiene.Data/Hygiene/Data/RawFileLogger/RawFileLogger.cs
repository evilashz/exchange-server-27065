using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.SharePointSignalStore;
using Microsoft.Exchange.Transport.LoggingCommon;
using Microsoft.Win32;

namespace Microsoft.Exchange.Hygiene.Data.RawFileLogger
{
	// Token: 0x020001B9 RID: 441
	internal class RawFileLogger : DisposeTrackableBase
	{
		// Token: 0x06001270 RID: 4720 RVA: 0x00038424 File Offset: 0x00036624
		private RawFileLogger(string logPrefixType, Version version, string logPrefix) : this(logPrefixType, version, logPrefix, RawFileLogger.GetLogPath(logPrefixType))
		{
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x00038438 File Offset: 0x00036638
		private RawFileLogger(string logPrefixType, Version version, string logPrefix, string logPath)
		{
			this.logSchemaMapping = RawFileLogger.GetLogSchema(logPrefixType, version);
			long maximumLogFileSize = RawFileLoggerConfiguration.Instance.MaximumLogFileSize;
			long maximumLogDirectorySize = RawFileLoggerConfiguration.Instance.MaximumLogDirectorySize;
			TimeSpan maximumLogAge = RawFileLoggerConfiguration.Instance.MaximumLogAge;
			int logBufferSize = RawFileLoggerConfiguration.Instance.LogBufferSize;
			TimeSpan logBufferFlushInterval = RawFileLoggerConfiguration.Instance.LogBufferFlushInterval;
			this.log = new Log(logPrefix, new LogHeaderFormatter(this.logSchemaMapping), "Microsoft.Exchange.Hygiene.Data.RawFileLogger", false);
			this.log.Configure(logPath, maximumLogAge, maximumLogDirectorySize, maximumLogFileSize, logBufferSize, logBufferFlushInterval);
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x000384C0 File Offset: 0x000366C0
		public static RawFileLogger GetNonCachedLogger(string logPrefixType, string logDirectoryPath, Version version)
		{
			return new RawFileLogger(logPrefixType, version, logPrefixType, logDirectoryPath);
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x000384CC File Offset: 0x000366CC
		public static RawFileLogger GetLogger(string logPrefixType, Version version)
		{
			string logPrefix = RawFileLogger.GetLogPrefix(logPrefixType, version);
			if (!RawFileLogger.instances.ContainsKey(logPrefix))
			{
				RawFileLogger rawFileLogger = new RawFileLogger(logPrefixType, version, logPrefix);
				if (!RawFileLogger.instances.TryAdd(logPrefix, rawFileLogger))
				{
					rawFileLogger.Dispose();
				}
			}
			return RawFileLogger.instances[logPrefix];
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x00038518 File Offset: 0x00036718
		public static RawFileLogger GetLogger(string logPrefixType, string logDirectoryPath, Version version)
		{
			string logPrefix = RawFileLogger.GetLogPrefix(logPrefixType, version);
			if (!RawFileLogger.instances.ContainsKey(logPrefix))
			{
				RawFileLogger rawFileLogger = new RawFileLogger(logPrefixType, version, logPrefix, logDirectoryPath);
				if (!RawFileLogger.instances.TryAdd(logPrefix, rawFileLogger))
				{
					rawFileLogger.Dispose();
				}
			}
			return RawFileLogger.instances[logPrefix];
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x00038564 File Offset: 0x00036764
		public static void Cleanup()
		{
			foreach (RawFileLogger rawFileLogger in RawFileLogger.instances.Values)
			{
				rawFileLogger.Dispose();
			}
			RawFileLogger.instances.Clear();
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x000385C0 File Offset: 0x000367C0
		public void WriteLogData(byte[][] logData)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.logSchemaMapping, true, false);
			for (int i = 0; i < logData.GetLength(0); i++)
			{
				logRowFormatter[i] = logData[i];
			}
			this.log.Append(logRowFormatter, -1);
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x00038604 File Offset: 0x00036804
		public void Flush()
		{
			this.log.Flush();
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00038611 File Offset: 0x00036811
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<RawFileLogger>(this);
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x00038619 File Offset: 0x00036819
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.log != null)
			{
				this.log.Flush();
				this.log.Close();
			}
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0003863C File Offset: 0x0003683C
		private static string GetLogPrefix(string logPrefixType, Version version)
		{
			return string.Format("{0}_{1}_{2}_{3}_{4}_", new object[]
			{
				logPrefixType,
				version.Major,
				version.Minor,
				version.MajorRevision,
				version.MinorRevision
			});
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x00038728 File Offset: 0x00036928
		private static LogSchema GetLogSchema(string logPrefixType, Version version)
		{
			string[] fields;
			if (logPrefixType.StartsWith("MSGTRK", StringComparison.OrdinalIgnoreCase) || logPrefixType.StartsWith("MSGTRKMD", StringComparison.OrdinalIgnoreCase) || logPrefixType.StartsWith("MSGTRKMS", StringComparison.OrdinalIgnoreCase) || logPrefixType.StartsWith("MSGTRACECOMBO", StringComparison.OrdinalIgnoreCase) || logPrefixType.StartsWith("SYSPRB", StringComparison.OrdinalIgnoreCase))
			{
				fields = (from field in MessageTrackingSchema.MessageTrackingEvent.Fields
				where field.BuildAdded <= version
				select field into csvfield
				select csvfield.Name).ToArray<string>();
			}
			else if (logPrefixType.StartsWith("ASYNCQUEUE", StringComparison.OrdinalIgnoreCase))
			{
				fields = (from field in AsyncQueueLogLineSchema.DefaultSchema.Fields
				where field.BuildAdded <= version
				select field into csvfield
				select csvfield.Name).ToArray<string>();
			}
			else if (logPrefixType.StartsWith("SYNCTR", StringComparison.OrdinalIgnoreCase) || logPrefixType.StartsWith("SYNCADCP", StringComparison.OrdinalIgnoreCase))
			{
				fields = (from field in TenantSettingSchema.Schema.Fields
				where field.BuildAdded <= version
				select field into csvfield
				select csvfield.Name).ToArray<string>();
			}
			else if (logPrefixType.StartsWith("SPAMDIGEST", StringComparison.OrdinalIgnoreCase))
			{
				fields = (from field in SpamDigestLogSchema.DefaultSchema.Fields
				where field.BuildAdded <= version
				select field into csvfield
				select csvfield.Name).ToArray<string>();
			}
			else
			{
				if (!logPrefixType.StartsWith("OFFICEGRAPH", StringComparison.OrdinalIgnoreCase))
				{
					throw new ArgumentException(string.Format("Unsupported log prefix: {0}", logPrefixType));
				}
				fields = (from field in OfficeGraphLogSchema.Schema.Fields
				where field.BuildAdded <= version
				select field into csvfield
				select csvfield.Name).ToArray<string>();
			}
			return new LogSchema("Microsoft.Exchange.Hygiene.Data.RawFileLogger", version.ToString(), "Raw File Logger", fields);
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x00038990 File Offset: 0x00036B90
		private static string GetLogPath(string logPrefixType)
		{
			bool flag = false;
			string text = null;
			string key;
			switch (key = logPrefixType.ToUpper())
			{
			case "MSGTRK":
			case "MSGTRKMD":
			case "MSGTRKMS":
			case "MSGTRACECOMBO":
			case "SYSPRB":
			case "SYNCTR":
			case "SYNCADCP":
			{
				string text2 = "ExoMessageTraceLogPath";
				flag = true;
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeLabs"))
				{
					if (registryKey != null)
					{
						object value = registryKey.GetValue(text2);
						if (value != null)
						{
							text = value.ToString();
						}
					}
				}
				if (string.IsNullOrWhiteSpace(text))
				{
					throw new ConfigurationErrorsException(string.Format("Log Path is not set in Registry: {0}[{1}]", "SOFTWARE\\Microsoft\\ExchangeLabs", text2));
				}
				if (flag)
				{
					text = Path.Combine(text, logPrefixType);
				}
				return text;
			}
			}
			throw new ArgumentException(string.Format("Unsupported log prefix: {0}", logPrefixType));
		}

		// Token: 0x040008D5 RID: 2261
		private const string LogComponentName = "Microsoft.Exchange.Hygiene.Data.RawFileLogger";

		// Token: 0x040008D6 RID: 2262
		private const string LogPathRegistryPath = "SOFTWARE\\Microsoft\\ExchangeLabs";

		// Token: 0x040008D7 RID: 2263
		private static ConcurrentDictionary<string, RawFileLogger> instances = new ConcurrentDictionary<string, RawFileLogger>();

		// Token: 0x040008D8 RID: 2264
		private readonly LogSchema logSchemaMapping;

		// Token: 0x040008D9 RID: 2265
		private readonly Log log;
	}
}
