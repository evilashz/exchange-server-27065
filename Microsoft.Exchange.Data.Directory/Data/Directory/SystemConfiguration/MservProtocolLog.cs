using System;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002E6 RID: 742
	internal static class MservProtocolLog
	{
		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x060022CF RID: 8911 RVA: 0x000978C2 File Offset: 0x00095AC2
		// (set) Token: 0x060022D0 RID: 8912 RVA: 0x000978C9 File Offset: 0x00095AC9
		private static bool Enabled { get; set; }

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x060022D1 RID: 8913 RVA: 0x000978D1 File Offset: 0x00095AD1
		// (set) Token: 0x060022D2 RID: 8914 RVA: 0x000978D8 File Offset: 0x00095AD8
		private static bool Initialized { get; set; }

		// Token: 0x060022D3 RID: 8915 RVA: 0x000978E0 File Offset: 0x00095AE0
		private static int GetNextSequenceNumber()
		{
			int result;
			lock (MservProtocolLog.incrementLock)
			{
				if (MservProtocolLog.sequenceNumber == 2147483647)
				{
					MservProtocolLog.sequenceNumber = 0;
				}
				else
				{
					MservProtocolLog.sequenceNumber++;
				}
				result = MservProtocolLog.sequenceNumber;
			}
			return result;
		}

		// Token: 0x060022D4 RID: 8916 RVA: 0x00097940 File Offset: 0x00095B40
		private static void Initialize(ExDateTime serviceStartTime, string logFilePath, TimeSpan maxRetentionPeriond, ByteQuantifiedSize directorySizeQuota, ByteQuantifiedSize perFileSizeQuota, bool applyHourPrecision)
		{
			int registryInt;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange Mserv\\Parameters"))
			{
				MservProtocolLog.Enabled = MservProtocolLog.GetRegistryBool(registryKey, "ProtocolLoggingEnabled", true);
				registryInt = MservProtocolLog.GetRegistryInt(registryKey, "LogBufferSize", 65536);
			}
			if (MservProtocolLog.registryWatcher == null)
			{
				MservProtocolLog.registryWatcher = new RegistryWatcher("SYSTEM\\CurrentControlSet\\Services\\MSExchange Mserv\\Parameters", false);
			}
			if (MservProtocolLog.timer == null)
			{
				MservProtocolLog.timer = new Timer(new TimerCallback(MservProtocolLog.UpdateConfigIfChanged), null, 0, 300000);
			}
			if (MservProtocolLog.Enabled)
			{
				MservProtocolLog.log = new Log(MservProtocolLog.logFilePrefix, new LogHeaderFormatter(MservProtocolLog.schema, LogHeaderCsvOption.CsvCompatible), "MservLogs");
				MservProtocolLog.log.Configure(logFilePath, maxRetentionPeriond, (long)directorySizeQuota.ToBytes(), (long)perFileSizeQuota.ToBytes(), applyHourPrecision, registryInt, MservProtocolLog.defaultFlushInterval);
				AppDomain.CurrentDomain.ProcessExit += MservProtocolLog.CurrentDomain_ProcessExit;
			}
			MservProtocolLog.Initialized = true;
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x00097A3C File Offset: 0x00095C3C
		private static void UpdateConfigIfChanged(object state)
		{
			if (MservProtocolLog.registryWatcher.IsChanged())
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange Mserv\\Parameters"))
				{
					bool registryBool = MservProtocolLog.GetRegistryBool(registryKey, "ProtocolLoggingEnabled", true);
					if (registryBool != MservProtocolLog.Enabled)
					{
						lock (MservProtocolLog.logLock)
						{
							MservProtocolLog.Initialized = false;
							MservProtocolLog.Enabled = registryBool;
						}
					}
				}
			}
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x00097ACC File Offset: 0x00095CCC
		internal static void Append(string operation, string resultCode, long processingTime, string failure, string lookupKey, string partnerId, string ipAddress, string diagnosticHeader, string transactionId)
		{
			lock (MservProtocolLog.logLock)
			{
				if (!MservProtocolLog.Initialized)
				{
					MservProtocolLog.Initialize(ExDateTime.UtcNow, Path.Combine(MservProtocolLog.GetExchangeInstallPath(), "Logging\\Mserv\\"), MservProtocolLog.defaultMaxRetentionPeriod, MservProtocolLog.defaultDirectorySizeQuota, MservProtocolLog.defaultPerFileSizeQuota, true);
				}
			}
			if (MservProtocolLog.Enabled)
			{
				LogRowFormatter logRowFormatter = new LogRowFormatter(MservProtocolLog.schema);
				logRowFormatter[1] = MservProtocolLog.GetNextSequenceNumber();
				logRowFormatter[2] = Globals.ProcessName;
				logRowFormatter[4] = Globals.ProcessAppName;
				logRowFormatter[3] = Globals.ProcessId;
				logRowFormatter[5] = operation;
				logRowFormatter[7] = processingTime;
				logRowFormatter[6] = resultCode;
				logRowFormatter[8] = failure;
				logRowFormatter[9] = lookupKey;
				logRowFormatter[10] = partnerId;
				logRowFormatter[11] = ipAddress;
				logRowFormatter[12] = diagnosticHeader;
				logRowFormatter[13] = transactionId;
				MservProtocolLog.log.Append(logRowFormatter, 0);
			}
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x00097BE8 File Offset: 0x00095DE8
		internal static void BeginAppend(string operation, string resultCode, long processingTime, string failure, string lookupKey, string partnerId, string ipAddress, string diagnosticHeader, string transactionId)
		{
			MservProtocolLog.AppendDelegate appendDelegate = new MservProtocolLog.AppendDelegate(MservProtocolLog.Append);
			appendDelegate.BeginInvoke(operation, resultCode, processingTime, failure, lookupKey, partnerId, ipAddress, diagnosticHeader, transactionId, null, null);
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x00097C1C File Offset: 0x00095E1C
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

		// Token: 0x060022D9 RID: 8921 RVA: 0x00097C60 File Offset: 0x00095E60
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

		// Token: 0x060022DA RID: 8922 RVA: 0x00097C9C File Offset: 0x00095E9C
		private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
		{
			lock (MservProtocolLog.logLock)
			{
				MservProtocolLog.Enabled = false;
				MservProtocolLog.Initialized = false;
				MservProtocolLog.Shutdown();
			}
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x00097CE8 File Offset: 0x00095EE8
		private static void Shutdown()
		{
			if (MservProtocolLog.log != null)
			{
				MservProtocolLog.log.Close();
			}
			if (MservProtocolLog.timer != null)
			{
				MservProtocolLog.timer.Dispose();
				MservProtocolLog.timer = null;
			}
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x00097D14 File Offset: 0x00095F14
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

		// Token: 0x060022DD RID: 8925 RVA: 0x00097D80 File Offset: 0x00095F80
		private static string[] GetColumnArray()
		{
			string[] array = new string[MservProtocolLog.Fields.Length];
			for (int i = 0; i < MservProtocolLog.Fields.Length; i++)
			{
				array[i] = MservProtocolLog.Fields[i].ColumnName;
			}
			return array;
		}

		// Token: 0x04001598 RID: 5528
		private const string LogTypeName = "Mserv Logs";

		// Token: 0x04001599 RID: 5529
		private const string LogComponent = "MservLogs";

		// Token: 0x0400159A RID: 5530
		private const int DefaultLogBufferSize = 65536;

		// Token: 0x0400159B RID: 5531
		private const bool DefaultLoggingEnabled = true;

		// Token: 0x0400159C RID: 5532
		private const string RegistryParameters = "SYSTEM\\CurrentControlSet\\Services\\MSExchange Mserv\\Parameters";

		// Token: 0x0400159D RID: 5533
		private const string LoggingEnabledRegKeyName = "ProtocolLoggingEnabled";

		// Token: 0x0400159E RID: 5534
		private const string LogBufferSizeRegKeyName = "LogBufferSize";

		// Token: 0x0400159F RID: 5535
		private const string LoggingDirectoryUnderExchangeInstallPath = "Logging\\Mserv\\";

		// Token: 0x040015A0 RID: 5536
		internal static readonly MservProtocolLog.FieldInfo[] Fields = new MservProtocolLog.FieldInfo[]
		{
			new MservProtocolLog.FieldInfo(MservProtocolLog.Field.DateTime, "date-time"),
			new MservProtocolLog.FieldInfo(MservProtocolLog.Field.SequenceNumber, "seq-number"),
			new MservProtocolLog.FieldInfo(MservProtocolLog.Field.ClientName, "process-name"),
			new MservProtocolLog.FieldInfo(MservProtocolLog.Field.Pid, "process-id"),
			new MservProtocolLog.FieldInfo(MservProtocolLog.Field.AppName, "application-name"),
			new MservProtocolLog.FieldInfo(MservProtocolLog.Field.Operation, "operation"),
			new MservProtocolLog.FieldInfo(MservProtocolLog.Field.ResultCode, "result-code"),
			new MservProtocolLog.FieldInfo(MservProtocolLog.Field.ProcessingTime, "processing-time"),
			new MservProtocolLog.FieldInfo(MservProtocolLog.Field.Failures, "failures"),
			new MservProtocolLog.FieldInfo(MservProtocolLog.Field.LookupKey, "lookup-key"),
			new MservProtocolLog.FieldInfo(MservProtocolLog.Field.PartnerId, "partner-id"),
			new MservProtocolLog.FieldInfo(MservProtocolLog.Field.IpAddress, "ip-address"),
			new MservProtocolLog.FieldInfo(MservProtocolLog.Field.DiagnosticHeader, "diagnostic-header"),
			new MservProtocolLog.FieldInfo(MservProtocolLog.Field.TransactionId, "transaction-id")
		};

		// Token: 0x040015A1 RID: 5537
		private static readonly LogSchema schema = new LogSchema("Microsoft Exchange", "15.00.1497.010", "Mserv Logs", MservProtocolLog.GetColumnArray());

		// Token: 0x040015A2 RID: 5538
		private static TimeSpan defaultMaxRetentionPeriod = TimeSpan.FromHours(24.0);

		// Token: 0x040015A3 RID: 5539
		private static ByteQuantifiedSize defaultDirectorySizeQuota = ByteQuantifiedSize.Parse("200MB");

		// Token: 0x040015A4 RID: 5540
		private static ByteQuantifiedSize defaultPerFileSizeQuota = ByteQuantifiedSize.Parse("10MB");

		// Token: 0x040015A5 RID: 5541
		private static TimeSpan defaultFlushInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x040015A6 RID: 5542
		private static string logFilePrefix = Globals.ProcessName + "_" + Globals.ProcessAppName + "_";

		// Token: 0x040015A7 RID: 5543
		private static int sequenceNumber = 0;

		// Token: 0x040015A8 RID: 5544
		private static Timer timer;

		// Token: 0x040015A9 RID: 5545
		private static Log log;

		// Token: 0x040015AA RID: 5546
		private static object logLock = new object();

		// Token: 0x040015AB RID: 5547
		private static object incrementLock = new object();

		// Token: 0x040015AC RID: 5548
		private static RegistryWatcher registryWatcher;

		// Token: 0x020002E7 RID: 743
		internal enum Field
		{
			// Token: 0x040015B0 RID: 5552
			DateTime,
			// Token: 0x040015B1 RID: 5553
			SequenceNumber,
			// Token: 0x040015B2 RID: 5554
			ClientName,
			// Token: 0x040015B3 RID: 5555
			Pid,
			// Token: 0x040015B4 RID: 5556
			AppName,
			// Token: 0x040015B5 RID: 5557
			Operation,
			// Token: 0x040015B6 RID: 5558
			ResultCode,
			// Token: 0x040015B7 RID: 5559
			ProcessingTime,
			// Token: 0x040015B8 RID: 5560
			Failures,
			// Token: 0x040015B9 RID: 5561
			LookupKey,
			// Token: 0x040015BA RID: 5562
			PartnerId,
			// Token: 0x040015BB RID: 5563
			IpAddress,
			// Token: 0x040015BC RID: 5564
			DiagnosticHeader,
			// Token: 0x040015BD RID: 5565
			TransactionId
		}

		// Token: 0x020002E8 RID: 744
		// (Invoke) Token: 0x060022E0 RID: 8928
		internal delegate void AppendDelegate(string operation, string resultCode, long processingTime, string failure, string lookupKey, string partnerId, string ipAddress, string diagnosticHeader, string transactionId);

		// Token: 0x020002E9 RID: 745
		internal struct FieldInfo
		{
			// Token: 0x060022E3 RID: 8931 RVA: 0x00097FC9 File Offset: 0x000961C9
			public FieldInfo(MservProtocolLog.Field field, string columnName)
			{
				this.Field = field;
				this.ColumnName = columnName;
			}

			// Token: 0x040015BE RID: 5566
			internal readonly MservProtocolLog.Field Field;

			// Token: 0x040015BF RID: 5567
			internal readonly string ColumnName;
		}
	}
}
