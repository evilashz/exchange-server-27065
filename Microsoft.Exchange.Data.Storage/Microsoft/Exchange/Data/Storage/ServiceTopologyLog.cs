using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D69 RID: 3433
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ServiceTopologyLog
	{
		// Token: 0x060076AB RID: 30379 RVA: 0x0020C958 File Offset: 0x0020AB58
		private ServiceTopologyLog()
		{
			this.rows = new List<LogRowFormatter>(32);
		}

		// Token: 0x060076AC RID: 30380 RVA: 0x0020C978 File Offset: 0x0020AB78
		internal void Append(string callerFilePath, string memberName, int callerFileLine)
		{
			if (!this.initialized)
			{
				lock (this.logLock)
				{
					if (!this.initialized)
					{
						this.Initialize(ExDateTime.UtcNow, Path.Combine(ServiceTopologyLog.GetExchangeInstallPath(), "Logging\\ServiceTopology\\"), ServiceTopologyLog.DefaultMaxRetentionPeriod, ServiceTopologyLog.DefaultDirectorySizeQuota, ServiceTopologyLog.DefaultPerFileSizeQuota, true);
					}
				}
			}
			if (this.enabled)
			{
				LogRowFormatter logRowFormatter = new LogRowFormatter(ServiceTopologyLog.Schema);
				logRowFormatter[1] = this.GetNextSequenceNumber();
				logRowFormatter[2] = Globals.ProcessName;
				logRowFormatter[3] = Globals.ProcessId;
				logRowFormatter[4] = string.Format("{0}: Method {1}: Line {2}", callerFilePath, memberName, callerFileLine);
				lock (this.logLock)
				{
					this.rows.Add(logRowFormatter);
					if (this.flush == null)
					{
						this.flush = new ServiceTopologyLog.FlushDelegate(this.FlushRows);
						this.flush.BeginInvoke(null, null);
					}
				}
			}
		}

		// Token: 0x060076AD RID: 30381 RVA: 0x0020CAAC File Offset: 0x0020ACAC
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

		// Token: 0x060076AE RID: 30382 RVA: 0x0020CAF0 File Offset: 0x0020ACF0
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

		// Token: 0x060076AF RID: 30383 RVA: 0x0020CB2C File Offset: 0x0020AD2C
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

		// Token: 0x060076B0 RID: 30384 RVA: 0x0020CB98 File Offset: 0x0020AD98
		private static string[] GetColumnArray()
		{
			string[] array = new string[ServiceTopologyLog.Fields.Length];
			for (int i = 0; i < ServiceTopologyLog.Fields.Length; i++)
			{
				array[i] = ServiceTopologyLog.Fields[i].ColumnName;
			}
			return array;
		}

		// Token: 0x060076B1 RID: 30385 RVA: 0x0020CBDD File Offset: 0x0020ADDD
		private int GetNextSequenceNumber()
		{
			return Interlocked.Increment(ref this.sequenceNumber) & int.MaxValue;
		}

		// Token: 0x060076B2 RID: 30386 RVA: 0x0020CBF0 File Offset: 0x0020ADF0
		private void Initialize(ExDateTime serviceStartTime, string logFilePath, TimeSpan maxRetentionPeriod, ByteQuantifiedSize directorySizeQuota, ByteQuantifiedSize perFileSizeQuota, bool applyHourPrecision)
		{
			int registryInt;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess\\Parameters"))
			{
				this.enabled = ServiceTopologyLog.GetRegistryBool(registryKey, "ServiceTopologyLoggingEnabled", false);
				registryInt = ServiceTopologyLog.GetRegistryInt(registryKey, "LogBufferSize", 524288);
				int registryInt2 = ServiceTopologyLog.GetRegistryInt(registryKey, "FlushIntervalInMinutes", 15);
				if (registryInt2 > 0)
				{
					ServiceTopologyLog.FlushInterval = TimeSpan.FromMinutes((double)registryInt2);
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
			this.log = new Log(ServiceTopologyLog.LogFilePrefix, new LogHeaderFormatter(ServiceTopologyLog.Schema, LogHeaderCsvOption.CsvCompatible), "ServiceTopologyLogs");
			this.log.Configure(logFilePath, maxRetentionPeriod, (long)directorySizeQuota.ToBytes(), (long)perFileSizeQuota.ToBytes(), applyHourPrecision, registryInt, ServiceTopologyLog.FlushInterval, LogFileRollOver.Hourly);
			AppDomain.CurrentDomain.ProcessExit += this.CurrentDomain_ProcessExit;
			this.initialized = true;
		}

		// Token: 0x060076B3 RID: 30387 RVA: 0x0020CD0C File Offset: 0x0020AF0C
		private void UpdateConfigIfChanged(object state)
		{
			if (this.registryWatcher.IsChanged())
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess\\Parameters"))
				{
					bool registryBool = ServiceTopologyLog.GetRegistryBool(registryKey, "ServiceTopologyLoggingEnabled", false);
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

		// Token: 0x060076B4 RID: 30388 RVA: 0x0020CDA0 File Offset: 0x0020AFA0
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

		// Token: 0x060076B5 RID: 30389 RVA: 0x0020CE04 File Offset: 0x0020B004
		private void CurrentDomain_ProcessExit(object sender, EventArgs e)
		{
			lock (this.logLock)
			{
				this.initialized = false;
				this.rows.Clear();
				this.Shutdown();
			}
		}

		// Token: 0x060076B6 RID: 30390 RVA: 0x0020CE58 File Offset: 0x0020B058
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

		// Token: 0x0400523D RID: 21053
		private const string LogTypeName = "ServiceTopology Logs";

		// Token: 0x0400523E RID: 21054
		private const string LogComponent = "ServiceTopologyLogs";

		// Token: 0x0400523F RID: 21055
		private const int DefaultLogBufferSize = 524288;

		// Token: 0x04005240 RID: 21056
		private const bool DefaultLoggingEnabled = false;

		// Token: 0x04005241 RID: 21057
		private const string RegistryParameters = "SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess\\Parameters";

		// Token: 0x04005242 RID: 21058
		private const string LoggingEnabledRegKeyName = "ServiceTopologyLoggingEnabled";

		// Token: 0x04005243 RID: 21059
		private const string LogBufferSizeRegKeyName = "LogBufferSize";

		// Token: 0x04005244 RID: 21060
		private const string LogFlushIntervalRegKeyName = "FlushIntervalInMinutes";

		// Token: 0x04005245 RID: 21061
		private const string LoggingDirectoryUnderExchangeInstallPath = "Logging\\ServiceTopology\\";

		// Token: 0x04005246 RID: 21062
		private const int DefaultRowCount = 32;

		// Token: 0x04005247 RID: 21063
		public static readonly ServiceTopologyLog Instance = new ServiceTopologyLog();

		// Token: 0x04005248 RID: 21064
		internal static readonly ServiceTopologyLog.FieldInfo[] Fields = new ServiceTopologyLog.FieldInfo[]
		{
			new ServiceTopologyLog.FieldInfo(ServiceTopologyLog.Field.DateTime, "date-time"),
			new ServiceTopologyLog.FieldInfo(ServiceTopologyLog.Field.SequenceNumber, "seq-number"),
			new ServiceTopologyLog.FieldInfo(ServiceTopologyLog.Field.ClientName, "process-name"),
			new ServiceTopologyLog.FieldInfo(ServiceTopologyLog.Field.Pid, "process-id"),
			new ServiceTopologyLog.FieldInfo(ServiceTopologyLog.Field.CallerInfo, "caller-info")
		};

		// Token: 0x04005249 RID: 21065
		private static readonly LogSchema Schema = new LogSchema("Microsoft Exchange", "15.00.1497.010", "ServiceTopology Logs", ServiceTopologyLog.GetColumnArray());

		// Token: 0x0400524A RID: 21066
		private static readonly TimeSpan DefaultMaxRetentionPeriod = TimeSpan.FromHours(8.0);

		// Token: 0x0400524B RID: 21067
		private static readonly ByteQuantifiedSize DefaultDirectorySizeQuota = ByteQuantifiedSize.Parse("200MB");

		// Token: 0x0400524C RID: 21068
		private static readonly ByteQuantifiedSize DefaultPerFileSizeQuota = ByteQuantifiedSize.Parse("10MB");

		// Token: 0x0400524D RID: 21069
		private static TimeSpan FlushInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x0400524E RID: 21070
		private static readonly string LogFilePrefix = Globals.ProcessName + "_" + Globals.ProcessAppName + "_";

		// Token: 0x0400524F RID: 21071
		private int sequenceNumber;

		// Token: 0x04005250 RID: 21072
		private Timer timer;

		// Token: 0x04005251 RID: 21073
		private Log log;

		// Token: 0x04005252 RID: 21074
		private readonly object logLock = new object();

		// Token: 0x04005253 RID: 21075
		private bool enabled;

		// Token: 0x04005254 RID: 21076
		private bool initialized;

		// Token: 0x04005255 RID: 21077
		private RegistryWatcher registryWatcher;

		// Token: 0x04005256 RID: 21078
		private List<LogRowFormatter> rows;

		// Token: 0x04005257 RID: 21079
		private ServiceTopologyLog.FlushDelegate flush;

		// Token: 0x02000D6A RID: 3434
		// (Invoke) Token: 0x060076B9 RID: 30393
		internal delegate void FlushDelegate();

		// Token: 0x02000D6B RID: 3435
		internal enum Field
		{
			// Token: 0x04005259 RID: 21081
			DateTime,
			// Token: 0x0400525A RID: 21082
			SequenceNumber,
			// Token: 0x0400525B RID: 21083
			ClientName,
			// Token: 0x0400525C RID: 21084
			Pid,
			// Token: 0x0400525D RID: 21085
			CallerInfo
		}

		// Token: 0x02000D6C RID: 3436
		internal struct FieldInfo
		{
			// Token: 0x060076BC RID: 30396 RVA: 0x0020CF9F File Offset: 0x0020B19F
			public FieldInfo(ServiceTopologyLog.Field field, string columnName)
			{
				this.Field = field;
				this.ColumnName = columnName;
			}

			// Token: 0x0400525E RID: 21086
			internal readonly ServiceTopologyLog.Field Field;

			// Token: 0x0400525F RID: 21087
			internal readonly string ColumnName;
		}
	}
}
