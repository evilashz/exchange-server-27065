using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200005D RID: 93
	internal class ADProtocolLog
	{
		// Token: 0x0600048A RID: 1162 RVA: 0x0001A040 File Offset: 0x00018240
		private ADProtocolLog()
		{
			this.rows = new List<LogRowFormatter>(32);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0001A060 File Offset: 0x00018260
		internal void Append(string operation, string dn, string filter, string scope, string dc, string port, string resultCode, long processingTime, string failure, int serverProcessingTime, int entriesVisted, int entriesReturned, Guid activityId, string userEmail, string newValue, string callerInfo)
		{
			if (!this.initialized)
			{
				lock (this.logLock)
				{
					if (!this.initialized)
					{
						this.Initialize(ExDateTime.UtcNow, Path.Combine(ADProtocolLog.GetExchangeInstallPath(), "Logging\\ADDriver\\"), ADProtocolLog.DefaultMaxRetentionPeriod, ADProtocolLog.DefaultDirectorySizeQuota, ADProtocolLog.DefaultPerFileSizeQuota, true);
					}
				}
			}
			if (this.enabled)
			{
				LogRowFormatter logRowFormatter = new LogRowFormatter(ADProtocolLog.Schema);
				logRowFormatter[1] = this.GetNextSequenceNumber();
				logRowFormatter[2] = Globals.ProcessName;
				logRowFormatter[4] = Globals.ProcessAppName;
				logRowFormatter[3] = Globals.ProcessId;
				logRowFormatter[5] = operation;
				logRowFormatter[7] = processingTime;
				logRowFormatter[8] = serverProcessingTime;
				logRowFormatter[10] = entriesReturned;
				logRowFormatter[9] = entriesVisted;
				logRowFormatter[11] = dn;
				logRowFormatter[12] = filter;
				logRowFormatter[13] = scope;
				logRowFormatter[15] = dc;
				logRowFormatter[16] = port;
				logRowFormatter[6] = resultCode;
				logRowFormatter[14] = failure;
				logRowFormatter[17] = activityId;
				logRowFormatter[20] = callerInfo;
				logRowFormatter[19] = newValue;
				logRowFormatter[18] = userEmail;
				lock (this.logLock)
				{
					this.rows.Add(logRowFormatter);
					if (this.flush == null)
					{
						this.flush = new ADProtocolLog.FlushDelegate(this.FlushRows);
						this.flush.BeginInvoke(null, null);
					}
				}
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0001A238 File Offset: 0x00018438
		private static bool GetRegistryBool(RegistryKey regkey, string key, bool defaultValue)
		{
			int? num = null;
			if (regkey != null)
			{
				num = (regkey.GetValue(key) as int?);
			}
			if (num == null)
			{
				return defaultValue;
			}
			return Convert.ToBoolean(num.Value);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001A27C File Offset: 0x0001847C
		private static int GetRegistryInt(RegistryKey regkey, string key, int defaultValue)
		{
			int? num = null;
			if (regkey != null)
			{
				num = (regkey.GetValue(key) as int?);
			}
			if (num == null)
			{
				return defaultValue;
			}
			return num.Value;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0001A2B8 File Offset: 0x000184B8
		private static string GetExchangeInstallPath()
		{
			string result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup"))
			{
				if (registryKey == null)
				{
					result = string.Empty;
				}
				else
				{
					object value = registryKey.GetValue("MsiInstallPath");
					registryKey.Close();
					if (value == null)
					{
						result = string.Empty;
					}
					else
					{
						result = value.ToString();
					}
				}
			}
			return result;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0001A324 File Offset: 0x00018524
		private static string[] GetColumnArray()
		{
			string[] array = new string[ADProtocolLog.Fields.Length];
			for (int i = 0; i < ADProtocolLog.Fields.Length; i++)
			{
				array[i] = ADProtocolLog.Fields[i].ColumnName;
			}
			return array;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0001A369 File Offset: 0x00018569
		private int GetNextSequenceNumber()
		{
			return Interlocked.Increment(ref this.sequenceNumber) & int.MaxValue;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0001A37C File Offset: 0x0001857C
		private void Initialize(ExDateTime serviceStartTime, string logFilePath, TimeSpan maxRetentionPeriod, ByteQuantifiedSize directorySizeQuota, ByteQuantifiedSize perFileSizeQuota, bool applyHourPrecision)
		{
			int registryInt;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess\\Parameters"))
			{
				this.enabled = ADProtocolLog.GetRegistryBool(registryKey, "ProtocolLoggingEnabled", true);
				registryInt = ADProtocolLog.GetRegistryInt(registryKey, "LogBufferSize", 524288);
				int registryInt2 = ADProtocolLog.GetRegistryInt(registryKey, "FlushIntervalInMinutes", 15);
				if (registryInt2 > 0)
				{
					ADProtocolLog.FlushInterval = TimeSpan.FromMinutes((double)registryInt2);
				}
			}
			if (this.registryWatcher == null)
			{
				this.registryWatcher = new RegistryWatcher("SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess\\Parameters", false);
			}
			if (this.timer == null)
			{
				this.timer = new Timer(new TimerCallback(this.UpdateConfigIfChanged), null, 0, 300000);
			}
			if (this.enabled)
			{
				this.log = new Log(ADProtocolLog.LogFilePrefix, new LogHeaderFormatter(ADProtocolLog.Schema, LogHeaderCsvOption.CsvCompatible), "ADDriverLogs");
				this.log.Configure(logFilePath, maxRetentionPeriod, (long)directorySizeQuota.ToBytes(), (long)perFileSizeQuota.ToBytes(), applyHourPrecision, registryInt, ADProtocolLog.FlushInterval, LogFileRollOver.Hourly);
				AppDomain.CurrentDomain.ProcessExit += this.CurrentDomain_ProcessExit;
			}
			this.initialized = true;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001A4A0 File Offset: 0x000186A0
		private void UpdateConfigIfChanged(object state)
		{
			if (this.registryWatcher.IsChanged())
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess\\Parameters"))
				{
					bool registryBool = ADProtocolLog.GetRegistryBool(registryKey, "ProtocolLoggingEnabled", true);
					if (registryBool != this.enabled)
					{
						lock (this.logLock)
						{
							this.initialized = false;
							this.enabled = registryBool;
						}
					}
				}
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001A534 File Offset: 0x00018734
		private void FlushRows()
		{
			List<LogRowFormatter> list;
			lock (this.logLock)
			{
				list = this.rows;
				this.rows = new List<LogRowFormatter>(32);
			}
			this.log.Append(list, 0);
			this.flush = null;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001A598 File Offset: 0x00018798
		private void CurrentDomain_ProcessExit(object sender, EventArgs e)
		{
			lock (this.logLock)
			{
				this.enabled = false;
				this.initialized = false;
				this.rows.Clear();
				this.Shutdown();
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0001A5F4 File Offset: 0x000187F4
		private void Shutdown()
		{
			if (this.log != null)
			{
				this.log.Close();
			}
			if (this.timer != null)
			{
				this.timer.Dispose();
				this.timer = null;
			}
		}

		// Token: 0x040001AB RID: 427
		private const string LogTypeName = "ADDriver Logs";

		// Token: 0x040001AC RID: 428
		private const string LogComponent = "ADDriverLogs";

		// Token: 0x040001AD RID: 429
		private const int DefaultLogBufferSize = 524288;

		// Token: 0x040001AE RID: 430
		private const bool DefaultLoggingEnabled = true;

		// Token: 0x040001AF RID: 431
		private const string RegistryParameters = "SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess\\Parameters";

		// Token: 0x040001B0 RID: 432
		private const string LoggingEnabledRegKeyName = "ProtocolLoggingEnabled";

		// Token: 0x040001B1 RID: 433
		private const string LogBufferSizeRegKeyName = "LogBufferSize";

		// Token: 0x040001B2 RID: 434
		private const string LogFlushIntervalRegKeyName = "FlushIntervalInMinutes";

		// Token: 0x040001B3 RID: 435
		private const string LoggingDirectoryUnderExchangeInstallPath = "Logging\\ADDriver\\";

		// Token: 0x040001B4 RID: 436
		private const int DefaultRowCount = 32;

		// Token: 0x040001B5 RID: 437
		public static readonly ADProtocolLog Instance = new ADProtocolLog();

		// Token: 0x040001B6 RID: 438
		internal static readonly ADProtocolLog.FieldInfo[] Fields = new ADProtocolLog.FieldInfo[]
		{
			new ADProtocolLog.FieldInfo(ADProtocolLog.Field.DateTime, "date-time"),
			new ADProtocolLog.FieldInfo(ADProtocolLog.Field.SequenceNumber, "seq-number"),
			new ADProtocolLog.FieldInfo(ADProtocolLog.Field.ClientName, "process-name"),
			new ADProtocolLog.FieldInfo(ADProtocolLog.Field.Pid, "process-id"),
			new ADProtocolLog.FieldInfo(ADProtocolLog.Field.AppName, "application-name"),
			new ADProtocolLog.FieldInfo(ADProtocolLog.Field.Operation, "operation"),
			new ADProtocolLog.FieldInfo(ADProtocolLog.Field.ResultCode, "result-code"),
			new ADProtocolLog.FieldInfo(ADProtocolLog.Field.ProcessingTime, "processing-time"),
			new ADProtocolLog.FieldInfo(ADProtocolLog.Field.ServerProcessingTime, "serverprocessing-time"),
			new ADProtocolLog.FieldInfo(ADProtocolLog.Field.EntriesVisited, "entries-visited"),
			new ADProtocolLog.FieldInfo(ADProtocolLog.Field.EntriesReturned, "entries-returned"),
			new ADProtocolLog.FieldInfo(ADProtocolLog.Field.DN, "distinguished-name"),
			new ADProtocolLog.FieldInfo(ADProtocolLog.Field.Filter, "filter"),
			new ADProtocolLog.FieldInfo(ADProtocolLog.Field.Scope, "scope"),
			new ADProtocolLog.FieldInfo(ADProtocolLog.Field.Failures, "failures"),
			new ADProtocolLog.FieldInfo(ADProtocolLog.Field.DC, "domaincontroller"),
			new ADProtocolLog.FieldInfo(ADProtocolLog.Field.Port, "port"),
			new ADProtocolLog.FieldInfo(ADProtocolLog.Field.ActivityId, "activity-id"),
			new ADProtocolLog.FieldInfo(ADProtocolLog.Field.UserEmail, "user-email"),
			new ADProtocolLog.FieldInfo(ADProtocolLog.Field.NewValue, "new-value"),
			new ADProtocolLog.FieldInfo(ADProtocolLog.Field.CallerInfo, "caller-info")
		};

		// Token: 0x040001B7 RID: 439
		private static readonly LogSchema Schema = new LogSchema("Microsoft Exchange", "15.00.1497.010", "ADDriver Logs", ADProtocolLog.GetColumnArray());

		// Token: 0x040001B8 RID: 440
		private static readonly TimeSpan RowTimeLimit = TimeSpan.FromSeconds(5.0);

		// Token: 0x040001B9 RID: 441
		private static readonly TimeSpan DefaultMaxRetentionPeriod = TimeSpan.FromHours(8.0);

		// Token: 0x040001BA RID: 442
		private static readonly ByteQuantifiedSize DefaultDirectorySizeQuota = ByteQuantifiedSize.Parse("200MB");

		// Token: 0x040001BB RID: 443
		private static readonly ByteQuantifiedSize DefaultPerFileSizeQuota = ByteQuantifiedSize.Parse("10MB");

		// Token: 0x040001BC RID: 444
		private static TimeSpan FlushInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x040001BD RID: 445
		private static readonly string LogFilePrefix = Globals.ProcessName + "_" + Globals.ProcessAppName + "_";

		// Token: 0x040001BE RID: 446
		private int sequenceNumber;

		// Token: 0x040001BF RID: 447
		private Timer timer;

		// Token: 0x040001C0 RID: 448
		private Log log;

		// Token: 0x040001C1 RID: 449
		private object logLock = new object();

		// Token: 0x040001C2 RID: 450
		private bool enabled;

		// Token: 0x040001C3 RID: 451
		private bool initialized;

		// Token: 0x040001C4 RID: 452
		private RegistryWatcher registryWatcher;

		// Token: 0x040001C5 RID: 453
		private List<LogRowFormatter> rows;

		// Token: 0x040001C6 RID: 454
		private ADProtocolLog.FlushDelegate flush;

		// Token: 0x0200005E RID: 94
		// (Invoke) Token: 0x06000498 RID: 1176
		internal delegate void FlushDelegate();

		// Token: 0x0200005F RID: 95
		internal enum Field
		{
			// Token: 0x040001C8 RID: 456
			DateTime,
			// Token: 0x040001C9 RID: 457
			SequenceNumber,
			// Token: 0x040001CA RID: 458
			ClientName,
			// Token: 0x040001CB RID: 459
			Pid,
			// Token: 0x040001CC RID: 460
			AppName,
			// Token: 0x040001CD RID: 461
			Operation,
			// Token: 0x040001CE RID: 462
			ResultCode,
			// Token: 0x040001CF RID: 463
			ProcessingTime,
			// Token: 0x040001D0 RID: 464
			ServerProcessingTime,
			// Token: 0x040001D1 RID: 465
			EntriesVisited,
			// Token: 0x040001D2 RID: 466
			EntriesReturned,
			// Token: 0x040001D3 RID: 467
			DN,
			// Token: 0x040001D4 RID: 468
			Filter,
			// Token: 0x040001D5 RID: 469
			Scope,
			// Token: 0x040001D6 RID: 470
			Failures,
			// Token: 0x040001D7 RID: 471
			DC,
			// Token: 0x040001D8 RID: 472
			Port,
			// Token: 0x040001D9 RID: 473
			ActivityId,
			// Token: 0x040001DA RID: 474
			UserEmail,
			// Token: 0x040001DB RID: 475
			NewValue,
			// Token: 0x040001DC RID: 476
			CallerInfo
		}

		// Token: 0x02000060 RID: 96
		internal struct FieldInfo
		{
			// Token: 0x0600049B RID: 1179 RVA: 0x0001A8D7 File Offset: 0x00018AD7
			public FieldInfo(ADProtocolLog.Field field, string columnName)
			{
				this.Field = field;
				this.ColumnName = columnName;
			}

			// Token: 0x040001DD RID: 477
			internal readonly ADProtocolLog.Field Field;

			// Token: 0x040001DE RID: 478
			internal readonly string ColumnName;
		}
	}
}
