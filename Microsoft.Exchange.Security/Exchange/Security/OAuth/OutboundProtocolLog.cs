using System;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Win32;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000F5 RID: 245
	internal static class OutboundProtocolLog
	{
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x0003707E File Offset: 0x0003527E
		// (set) Token: 0x06000830 RID: 2096 RVA: 0x00037085 File Offset: 0x00035285
		private static bool Enabled { get; set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x0003708D File Offset: 0x0003528D
		// (set) Token: 0x06000832 RID: 2098 RVA: 0x00037094 File Offset: 0x00035294
		private static bool Initialized { get; set; }

		// Token: 0x06000833 RID: 2099 RVA: 0x0003709C File Offset: 0x0003529C
		private static int GetNextSequenceNumber()
		{
			int result;
			lock (OutboundProtocolLog.incrementLock)
			{
				if (OutboundProtocolLog.sequenceNumber == 2147483647)
				{
					OutboundProtocolLog.sequenceNumber = 0;
				}
				else
				{
					OutboundProtocolLog.sequenceNumber++;
				}
				result = OutboundProtocolLog.sequenceNumber;
			}
			return result;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x000370FC File Offset: 0x000352FC
		private static void Initialize(ExDateTime serviceStartTime, string logFilePath, TimeSpan maxRetentionPeriond, ByteQuantifiedSize directorySizeQuota, ByteQuantifiedSize perFileSizeQuota, bool applyHourPrecision)
		{
			int registryInt;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange OAuth\\Parameters"))
			{
				OutboundProtocolLog.Enabled = OutboundProtocolLog.GetRegistryBool(registryKey, "ProtocolLoggingEnabled", true);
				registryInt = OutboundProtocolLog.GetRegistryInt(registryKey, "LogBufferSize", 1048576);
			}
			if (OutboundProtocolLog.registryWatcher == null)
			{
				OutboundProtocolLog.registryWatcher = new RegistryWatcher("SYSTEM\\CurrentControlSet\\Services\\MSExchange OAuth\\Parameters", false);
			}
			if (OutboundProtocolLog.timer == null)
			{
				OutboundProtocolLog.timer = new Timer(new TimerCallback(OutboundProtocolLog.UpdateConfigIfChanged), null, 0, 300000);
			}
			if (OutboundProtocolLog.Enabled)
			{
				OutboundProtocolLog.log = new Log(OutboundProtocolLog.logFilePrefix, new LogHeaderFormatter(OutboundProtocolLog.schema, LogHeaderCsvOption.CsvCompatible), "OAuthOutbound");
				OutboundProtocolLog.log.Configure(logFilePath, maxRetentionPeriond, (long)directorySizeQuota.ToBytes(), (long)perFileSizeQuota.ToBytes(), applyHourPrecision, registryInt, OutboundProtocolLog.defaultFlushInterval, LogFileRollOver.Hourly);
				AppDomain.CurrentDomain.ProcessExit += OutboundProtocolLog.CurrentDomain_ProcessExit;
			}
			OutboundProtocolLog.Initialized = true;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x000371F8 File Offset: 0x000353F8
		private static void UpdateConfigIfChanged(object state)
		{
			if (OutboundProtocolLog.registryWatcher.IsChanged())
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange OAuth\\Parameters"))
				{
					bool registryBool = OutboundProtocolLog.GetRegistryBool(registryKey, "ProtocolLoggingEnabled", true);
					if (registryBool != OutboundProtocolLog.Enabled)
					{
						lock (OutboundProtocolLog.logLock)
						{
							OutboundProtocolLog.Initialized = false;
							OutboundProtocolLog.Enabled = registryBool;
						}
					}
				}
			}
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00037288 File Offset: 0x00035488
		internal static void Append(string operation, string resultCode, long processingTime, string userAgent, Guid? clientRequestId, string targetUri, string tenantInfo, string resource, string errorString, string errorDetail, string devMetrics, TimeSpan remainingLifetime, TokenResult tokenResult)
		{
			lock (OutboundProtocolLog.logLock)
			{
				if (!OutboundProtocolLog.Initialized)
				{
					OutboundProtocolLog.Initialize(ExDateTime.UtcNow, Path.Combine(OutboundProtocolLog.GetExchangeInstallPath(), "Logging\\OAuthOutbound\\"), OutboundProtocolLog.defaultMaxRetentionPeriod, OutboundProtocolLog.defaultDirectorySizeQuota, OutboundProtocolLog.defaultPerFileSizeQuota, true);
				}
			}
			if (OutboundProtocolLog.Enabled)
			{
				LogRowFormatter logRowFormatter = new LogRowFormatter(OutboundProtocolLog.schema);
				logRowFormatter[1] = OutboundProtocolLog.GetNextSequenceNumber();
				logRowFormatter[2] = Globals.ProcessId;
				logRowFormatter[3] = "15.00.1497.010";
				logRowFormatter[4] = operation;
				logRowFormatter[5] = resultCode;
				logRowFormatter[6] = processingTime;
				logRowFormatter[7] = userAgent;
				logRowFormatter[8] = ((clientRequestId != null) ? clientRequestId.Value.ToString() : null);
				logRowFormatter[9] = targetUri;
				logRowFormatter[10] = tenantInfo;
				logRowFormatter[11] = resource;
				logRowFormatter[12] = Globals.ProcessAppName + "_" + OAuthCommon.CurrentAppPoolName;
				logRowFormatter[13] = errorString;
				logRowFormatter[14] = ((errorDetail == null) ? null : errorDetail.Replace(",", " ").Replace("\r\n", "  "));
				logRowFormatter[15] = devMetrics;
				logRowFormatter[16] = (int)remainingLifetime.TotalSeconds;
				logRowFormatter[17] = ((tokenResult == null) ? string.Empty : tokenResult.Base64String);
				OutboundProtocolLog.log.Append(logRowFormatter, 0);
			}
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0003743C File Offset: 0x0003563C
		internal static void BeginAppend(string operation, string resultCode, long processingTime, string userAgent, Guid? clientRequestId, string targetUri, string tenantInfo, string resource, string errorString, string errorDetail, string devMetrics, TimeSpan remainingLifetime, TokenResult tokenResult)
		{
			OutboundProtocolLog.AppendDelegate appendDelegate = new OutboundProtocolLog.AppendDelegate(OutboundProtocolLog.Append);
			appendDelegate.BeginInvoke(operation, resultCode, processingTime, userAgent, clientRequestId, targetUri, tenantInfo, resource, errorString, errorDetail, devMetrics, remainingLifetime, tokenResult, null, null);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00037478 File Offset: 0x00035678
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

		// Token: 0x06000839 RID: 2105 RVA: 0x000374BC File Offset: 0x000356BC
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

		// Token: 0x0600083A RID: 2106 RVA: 0x000374F8 File Offset: 0x000356F8
		private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
		{
			lock (OutboundProtocolLog.logLock)
			{
				OutboundProtocolLog.Enabled = false;
				OutboundProtocolLog.Initialized = false;
				OutboundProtocolLog.Shutdown();
			}
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00037544 File Offset: 0x00035744
		private static void Shutdown()
		{
			if (OutboundProtocolLog.log != null)
			{
				OutboundProtocolLog.log.Close();
			}
			if (OutboundProtocolLog.timer != null)
			{
				OutboundProtocolLog.timer.Dispose();
				OutboundProtocolLog.timer = null;
			}
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00037570 File Offset: 0x00035770
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

		// Token: 0x0600083D RID: 2109 RVA: 0x000375DC File Offset: 0x000357DC
		private static string[] GetColumnArray()
		{
			string[] array = new string[OutboundProtocolLog.Fields.Length];
			for (int i = 0; i < OutboundProtocolLog.Fields.Length; i++)
			{
				array[i] = OutboundProtocolLog.Fields[i].ColumnName;
			}
			return array;
		}

		// Token: 0x0400078B RID: 1931
		private const string LogTypeName = "OAuth Outbound Logs";

		// Token: 0x0400078C RID: 1932
		private const string LogComponent = "OAuthOutbound";

		// Token: 0x0400078D RID: 1933
		private const int DefaultLogBufferSize = 1048576;

		// Token: 0x0400078E RID: 1934
		private const bool DefaultLoggingEnabled = true;

		// Token: 0x0400078F RID: 1935
		private const string RegistryParameters = "SYSTEM\\CurrentControlSet\\Services\\MSExchange OAuth\\Parameters";

		// Token: 0x04000790 RID: 1936
		private const string LoggingEnabledRegKeyName = "ProtocolLoggingEnabled";

		// Token: 0x04000791 RID: 1937
		private const string LogBufferSizeRegKeyName = "LogBufferSize";

		// Token: 0x04000792 RID: 1938
		private const string LoggingDirectoryUnderExchangeInstallPath = "Logging\\OAuthOutbound\\";

		// Token: 0x04000793 RID: 1939
		internal static readonly OutboundProtocolLog.FieldInfo[] Fields = new OutboundProtocolLog.FieldInfo[]
		{
			new OutboundProtocolLog.FieldInfo(OutboundProtocolLog.Field.DateTime, "date-time"),
			new OutboundProtocolLog.FieldInfo(OutboundProtocolLog.Field.SequenceNumber, "seq-number"),
			new OutboundProtocolLog.FieldInfo(OutboundProtocolLog.Field.Pid, "process-id"),
			new OutboundProtocolLog.FieldInfo(OutboundProtocolLog.Field.BuildNumber, "build-number"),
			new OutboundProtocolLog.FieldInfo(OutboundProtocolLog.Field.Operation, "operation"),
			new OutboundProtocolLog.FieldInfo(OutboundProtocolLog.Field.ResultCode, "result-code"),
			new OutboundProtocolLog.FieldInfo(OutboundProtocolLog.Field.ProcessingTime, "processing-time"),
			new OutboundProtocolLog.FieldInfo(OutboundProtocolLog.Field.UserAgent, "user-agent"),
			new OutboundProtocolLog.FieldInfo(OutboundProtocolLog.Field.ClientRequestId, "client-request-id"),
			new OutboundProtocolLog.FieldInfo(OutboundProtocolLog.Field.TargetUri, "target"),
			new OutboundProtocolLog.FieldInfo(OutboundProtocolLog.Field.TenantInfo, "tenant-info"),
			new OutboundProtocolLog.FieldInfo(OutboundProtocolLog.Field.Resource, "resource"),
			new OutboundProtocolLog.FieldInfo(OutboundProtocolLog.Field.ProcessName, "process-name"),
			new OutboundProtocolLog.FieldInfo(OutboundProtocolLog.Field.ErrorString, "error-string"),
			new OutboundProtocolLog.FieldInfo(OutboundProtocolLog.Field.ErrorDetail, "error-detail"),
			new OutboundProtocolLog.FieldInfo(OutboundProtocolLog.Field.DevMetrics, "dev-metrics"),
			new OutboundProtocolLog.FieldInfo(OutboundProtocolLog.Field.RemainingLifetime, "remaining-lifetime"),
			new OutboundProtocolLog.FieldInfo(OutboundProtocolLog.Field.TokenResult, "token-result")
		};

		// Token: 0x04000794 RID: 1940
		private static readonly LogSchema schema = new LogSchema("Microsoft Exchange", "15.00.1497.010", "OAuth Outbound Logs", OutboundProtocolLog.GetColumnArray());

		// Token: 0x04000795 RID: 1941
		private static TimeSpan defaultMaxRetentionPeriod = TimeSpan.FromHours(48.0);

		// Token: 0x04000796 RID: 1942
		private static ByteQuantifiedSize defaultDirectorySizeQuota = ByteQuantifiedSize.Parse("200MB");

		// Token: 0x04000797 RID: 1943
		private static ByteQuantifiedSize defaultPerFileSizeQuota = ByteQuantifiedSize.Parse("10MB");

		// Token: 0x04000798 RID: 1944
		private static TimeSpan defaultFlushInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000799 RID: 1945
		private static string logFilePrefix = Globals.ProcessName + "_" + Globals.ProcessAppName + "_";

		// Token: 0x0400079A RID: 1946
		private static int sequenceNumber = 0;

		// Token: 0x0400079B RID: 1947
		private static Timer timer;

		// Token: 0x0400079C RID: 1948
		private static Log log;

		// Token: 0x0400079D RID: 1949
		private static object logLock = new object();

		// Token: 0x0400079E RID: 1950
		private static object incrementLock = new object();

		// Token: 0x0400079F RID: 1951
		private static RegistryWatcher registryWatcher;

		// Token: 0x020000F6 RID: 246
		internal enum Field
		{
			// Token: 0x040007A3 RID: 1955
			DateTime,
			// Token: 0x040007A4 RID: 1956
			SequenceNumber,
			// Token: 0x040007A5 RID: 1957
			Pid,
			// Token: 0x040007A6 RID: 1958
			BuildNumber,
			// Token: 0x040007A7 RID: 1959
			Operation,
			// Token: 0x040007A8 RID: 1960
			ResultCode,
			// Token: 0x040007A9 RID: 1961
			ProcessingTime,
			// Token: 0x040007AA RID: 1962
			UserAgent,
			// Token: 0x040007AB RID: 1963
			ClientRequestId,
			// Token: 0x040007AC RID: 1964
			TargetUri,
			// Token: 0x040007AD RID: 1965
			TenantInfo,
			// Token: 0x040007AE RID: 1966
			Resource,
			// Token: 0x040007AF RID: 1967
			ProcessName,
			// Token: 0x040007B0 RID: 1968
			ErrorString,
			// Token: 0x040007B1 RID: 1969
			ErrorDetail,
			// Token: 0x040007B2 RID: 1970
			DevMetrics,
			// Token: 0x040007B3 RID: 1971
			RemainingLifetime,
			// Token: 0x040007B4 RID: 1972
			TokenResult
		}

		// Token: 0x020000F7 RID: 247
		// (Invoke) Token: 0x06000840 RID: 2112
		internal delegate void AppendDelegate(string operation, string resultCode, long processingTime, string userAgent, Guid? clientRequestId, string targetUri, string tenantInfo, string resource, string errorString, string errorDetail, string devMetrics, TimeSpan remainingLifetime, TokenResult tokenResult);

		// Token: 0x020000F8 RID: 248
		internal struct FieldInfo
		{
			// Token: 0x06000843 RID: 2115 RVA: 0x00037889 File Offset: 0x00035A89
			public FieldInfo(OutboundProtocolLog.Field field, string columnName)
			{
				this.Field = field;
				this.ColumnName = columnName;
			}

			// Token: 0x040007B5 RID: 1973
			internal readonly OutboundProtocolLog.Field Field;

			// Token: 0x040007B6 RID: 1974
			internal readonly string ColumnName;
		}
	}
}
