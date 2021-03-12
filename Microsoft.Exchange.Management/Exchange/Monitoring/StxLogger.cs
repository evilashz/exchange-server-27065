using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000048 RID: 72
	internal static class StxLogger
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00006A21 File Offset: 0x00004C21
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x00006A28 File Offset: 0x00004C28
		private static bool Enabled { get; set; }

		// Token: 0x060001A5 RID: 421 RVA: 0x00006A30 File Offset: 0x00004C30
		private static void Initialize(StxLoggerBase stxLogger)
		{
			stxLogger.Initialized = true;
			int registryInt;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\Monitoring\\Parameters"))
			{
				StxLogger.Enabled = StxLogger.GetRegistryBool(registryKey, "ProtocolLoggingEnabled", true);
				registryInt = StxLogger.GetRegistryInt(registryKey, "LogBufferSize", 256);
			}
			if (StxLogger.registryWatcher == null)
			{
				StxLogger.registryWatcher = new RegistryWatcher("SYSTEM\\CurrentControlSet\\Services\\Monitoring\\Parameters", false);
			}
			if (StxLogger.timer == null)
			{
				StxLogger.timer = new Timer(new TimerCallback(StxLogger.UpdateConfigIfChanged), null, 0, 300000);
			}
			if (StxLogger.Enabled)
			{
				stxLogger.Log.Configure(Path.Combine(StxLogger.GetExchangeInstallPath(), "Logging\\MonitoringLogs\\STx"), StxLogger.defaultMaxRetentionPeriod, (long)StxLogger.defaultDirectorySizeQuota.ToBytes(), (long)StxLogger.defaultPerFileSizeQuota.ToBytes(), true, registryInt, StxLogger.defaultFlushInterval);
				AppDomain.CurrentDomain.ProcessExit += StxLogger.CurrentDomain_ProcessExit;
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00006B24 File Offset: 0x00004D24
		private static void UpdateConfigIfChanged(object state)
		{
			if (StxLogger.registryWatcher.IsChanged())
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\Monitoring\\Parameters"))
				{
					StxLogger.Enabled = StxLogger.GetRegistryBool(registryKey, "ProtocolLoggingEnabled", true);
				}
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006B7C File Offset: 0x00004D7C
		internal static void Append(StxLoggerBase stxLogger, LogRowFormatter row)
		{
			lock (StxLogger.logLock)
			{
				if (!stxLogger.Initialized)
				{
					StxLogger.Initialize(stxLogger);
				}
			}
			if (StxLogger.Enabled)
			{
				stxLogger.Log.Append(row, stxLogger.DateTimeField);
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00006BDC File Offset: 0x00004DDC
		internal static void BeginAppend(StxLoggerBase stxLogger, LogRowFormatter row)
		{
			StxLogger.AppendDelegate appendDelegate = new StxLogger.AppendDelegate(StxLogger.Append);
			appendDelegate.BeginInvoke(stxLogger, row, null, null);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00006C04 File Offset: 0x00004E04
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

		// Token: 0x060001AA RID: 426 RVA: 0x00006C48 File Offset: 0x00004E48
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

		// Token: 0x060001AB RID: 427 RVA: 0x00006C84 File Offset: 0x00004E84
		private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
		{
			StxLogger.Shutdown();
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00006C8C File Offset: 0x00004E8C
		private static void Shutdown()
		{
			foreach (KeyValuePair<StxLogType, StxLoggerBase> keyValuePair in StxLoggerBase.LogDictionary)
			{
				if (keyValuePair.Value != null)
				{
					keyValuePair.Value.Log.Close();
					keyValuePair.Value.Initialized = false;
				}
			}
			if (StxLogger.timer != null)
			{
				StxLogger.timer.Dispose();
				StxLogger.timer = null;
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00006D10 File Offset: 0x00004F10
		private static string GetExchangeInstallPath()
		{
			string result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup"))
			{
				string text = Path.Combine("D:", "Exchange");
				if (registryKey == null)
				{
					result = text;
				}
				else
				{
					object value = registryKey.GetValue("MsiInstallPath");
					registryKey.Close();
					if (value == null)
					{
						result = text;
					}
					else
					{
						result = value.ToString();
					}
				}
			}
			return result;
		}

		// Token: 0x04000102 RID: 258
		private const int DefaultLogBufferSize = 256;

		// Token: 0x04000103 RID: 259
		private const bool DefaultLoggingEnabled = true;

		// Token: 0x04000104 RID: 260
		private const string RegistryParameters = "SYSTEM\\CurrentControlSet\\Services\\Monitoring\\Parameters";

		// Token: 0x04000105 RID: 261
		private const string LoggingEnabledRegKeyName = "ProtocolLoggingEnabled";

		// Token: 0x04000106 RID: 262
		private const string LogBufferSizeRegKeyName = "LogBufferSize";

		// Token: 0x04000107 RID: 263
		private const string LoggingDirectoryUnderExchangeInstallPath = "Logging\\MonitoringLogs\\STx";

		// Token: 0x04000108 RID: 264
		private static TimeSpan defaultMaxRetentionPeriod = TimeSpan.FromHours(720.0);

		// Token: 0x04000109 RID: 265
		private static ByteQuantifiedSize defaultDirectorySizeQuota = ByteQuantifiedSize.Parse("5GB");

		// Token: 0x0400010A RID: 266
		private static ByteQuantifiedSize defaultPerFileSizeQuota = ByteQuantifiedSize.Parse("10MB");

		// Token: 0x0400010B RID: 267
		private static TimeSpan defaultFlushInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x0400010C RID: 268
		private static Timer timer;

		// Token: 0x0400010D RID: 269
		private static object logLock = new object();

		// Token: 0x0400010E RID: 270
		private static RegistryWatcher registryWatcher;

		// Token: 0x02000049 RID: 73
		// (Invoke) Token: 0x060001B0 RID: 432
		internal delegate void AppendDelegate(StxLoggerBase stxLogger, LogRowFormatter row);
	}
}
