using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Data.Directory.GlobalLocatorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Win32;
using schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000118 RID: 280
	internal static class GLSLogger
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x000358FA File Offset: 0x00033AFA
		// (set) Token: 0x06000BBC RID: 3004 RVA: 0x00035901 File Offset: 0x00033B01
		private static bool Enabled { get; set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x00035909 File Offset: 0x00033B09
		// (set) Token: 0x06000BBE RID: 3006 RVA: 0x00035910 File Offset: 0x00033B10
		private static bool Initialized { get; set; }

		// Token: 0x06000BBF RID: 3007 RVA: 0x00035918 File Offset: 0x00033B18
		private static int GetNextSequenceNumber()
		{
			int result;
			lock (GLSLogger.incrementLock)
			{
				if (GLSLogger.sequenceNumber == 2147483647)
				{
					GLSLogger.sequenceNumber = 0;
				}
				else
				{
					GLSLogger.sequenceNumber++;
				}
				result = GLSLogger.sequenceNumber;
			}
			return result;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00035978 File Offset: 0x00033B78
		private static void Initialize(ExDateTime serviceStartTime, string logFilePath, TimeSpan maxRetentionPeriond, ByteQuantifiedSize directorySizeQuota, ByteQuantifiedSize perFileSizeQuota, bool applyHourPrecision)
		{
			int registryInt;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess\\Parameters"))
			{
				GLSLogger.Enabled = GLSLogger.GetRegistryBool(registryKey, "ProtocolLoggingEnabled", true);
				registryInt = GLSLogger.GetRegistryInt(registryKey, "LogBufferSize", 65536);
			}
			if (GLSLogger.registryWatcher == null)
			{
				GLSLogger.registryWatcher = new RegistryWatcher("SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess\\Parameters", false);
			}
			if (GLSLogger.timer == null)
			{
				GLSLogger.timer = new Timer(new TimerCallback(GLSLogger.UpdateConfigIfChanged), null, 0, 300000);
			}
			if (GLSLogger.Enabled)
			{
				GLSLogger.log = new Log(GLSLogger.logFilePrefix, new LogHeaderFormatter(GLSLogger.schema, LogHeaderCsvOption.CsvCompatible), "GLSLogs");
				GLSLogger.log.Configure(logFilePath, maxRetentionPeriond, (long)directorySizeQuota.ToBytes(), (long)perFileSizeQuota.ToBytes(), applyHourPrecision, registryInt, GLSLogger.defaultFlushInterval);
				AppDomain.CurrentDomain.ProcessExit += GLSLogger.CurrentDomain_ProcessExit;
			}
			GLSLogger.Initialized = true;
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x00035A74 File Offset: 0x00033C74
		private static void UpdateConfigIfChanged(object state)
		{
			if (GLSLogger.registryWatcher.IsChanged())
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess\\Parameters"))
				{
					bool registryBool = GLSLogger.GetRegistryBool(registryKey, "ProtocolLoggingEnabled", true);
					if (registryBool != GLSLogger.Enabled)
					{
						lock (GLSLogger.logLock)
						{
							GLSLogger.Initialized = false;
							GLSLogger.Enabled = registryBool;
						}
					}
				}
			}
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00035B04 File Offset: 0x00033D04
		public static void LogResponse(GlsLoggerContext context, GLSLogger.StatusCode statusCode, ResponseBase response, GlsRawResponse rawResponse)
		{
			int num = Environment.TickCount - context.TickStart;
			string resultCode;
			switch (statusCode)
			{
			case GLSLogger.StatusCode.Found:
				resultCode = "Found";
				break;
			case GLSLogger.StatusCode.NotFound:
				resultCode = "NotFound";
				break;
			case GLSLogger.StatusCode.WriteSuccess:
				resultCode = "Success";
				break;
			default:
				throw new ArgumentException("statusCode");
			}
			GLSLogger.BeginAppend(context.MethodName, context.ParameterValue, context.ResolveEndpointToIpAddress(false), resultCode, rawResponse, (long)num, string.Empty, response.TransactionID, context.ConnectionId, response.Diagnostics);
			ADProviderPerf.UpdateGlsCallLatency(context.MethodName, context.IsRead, num, true);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x00035BA0 File Offset: 0x00033DA0
		public static void LogException(GlsLoggerContext context, Exception ex)
		{
			int num = Environment.TickCount - context.TickStart;
			StackTrace stackTrace = new StackTrace(false);
			GLSLogger.BeginAppend(context.MethodName, context.ParameterValue, context.ResolveEndpointToIpAddress(true), "Exception", null, (long)num, ex.Message + stackTrace.ToString(), context.RequestTrackingGuid.ToString(), context.ConnectionId, string.Empty);
			ADProviderPerf.UpdateGlsCallLatency(context.MethodName, context.IsRead, num, false);
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00035C24 File Offset: 0x00033E24
		public static T LoggingWrapper<T>(LocatorServiceClientAdapter glsClientAdapter, string parameter, string connectionId, Func<T> method) where T : ResponseBase
		{
			string ipAddress = glsClientAdapter.ResolveEndpointToIpAddress(false);
			string diagnostics = string.Empty;
			string transactionId = string.Empty;
			string text = string.Empty;
			int tickCount = Environment.TickCount;
			T result;
			try
			{
				GLSLogger.FaultInjectionTrace();
				result = method();
				diagnostics = result.Diagnostics;
				transactionId = result.TransactionID;
			}
			catch (Exception ex)
			{
				text = ex.Message;
				ipAddress = glsClientAdapter.ResolveEndpointToIpAddress(true);
				throw;
			}
			finally
			{
				int num = Environment.TickCount - tickCount;
				string resultCode = string.IsNullOrEmpty(text) ? "Success" : "Exception";
				GLSLogger.BeginAppend(method.Method.Name, parameter, ipAddress, resultCode, null, (long)num, text, transactionId, connectionId, diagnostics);
				bool isRead;
				string apiName = GLSLogger.ApiNameFromReturnType<T>(out isRead);
				ADProviderPerf.UpdateGlsCallLatency(apiName, isRead, num, string.IsNullOrEmpty(text));
			}
			return result;
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00035D10 File Offset: 0x00033F10
		private static string ApiNameFromReturnType<T>(out bool isRead) where T : ResponseBase
		{
			Type typeFromHandle = typeof(T);
			isRead = true;
			if (typeFromHandle == typeof(FindTenantResponse))
			{
				return "FindTenant";
			}
			if (typeFromHandle == typeof(FindDomainResponse))
			{
				return "FindDomain";
			}
			if (typeFromHandle == typeof(FindDomainsResponse))
			{
				return "FindDomains";
			}
			isRead = false;
			if (typeFromHandle == typeof(SaveTenantResponse))
			{
				return "SaveTenant";
			}
			if (typeFromHandle == typeof(SaveDomainResponse))
			{
				return "SaveDomain";
			}
			if (typeFromHandle == typeof(DeleteTenantResponse))
			{
				return "DeleteTenant";
			}
			if (typeFromHandle == typeof(DeleteDomainResponse))
			{
				return "DeleteDomain";
			}
			throw new ArgumentException("Unknown response type " + typeFromHandle.Name);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00035DEC File Offset: 0x00033FEC
		internal static void Append(string operation, string parameter, string ipAddress, string resultCode, GlsRawResponse rawResponse, long processingTime, string failure, string transactionid, string connectionid, string diagnostics)
		{
			Guid activityId = Guid.Empty;
			try
			{
				IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
				if (currentActivityScope != null && currentActivityScope.Status == ActivityContextStatus.ActivityStarted)
				{
					activityId = currentActivityScope.ActivityId;
				}
			}
			catch (Exception ex)
			{
				diagnostics += ex.ToString();
			}
			GLSLogger.AppendInternal(operation, parameter, ipAddress, resultCode, rawResponse, processingTime, failure, transactionid, connectionid, diagnostics, activityId);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x00035E54 File Offset: 0x00034054
		private static void AppendInternal(string operation, string parameter, string ipAddress, string resultCode, GlsRawResponse rawResponse, long processingTime, string failure, string transactionid, string connectionid, string diagnostics, Guid activityId)
		{
			if (rawResponse == null)
			{
				rawResponse = new GlsRawResponse();
			}
			if (!GLSLogger.Initialized)
			{
				lock (GLSLogger.logLock)
				{
					if (!GLSLogger.Initialized)
					{
						GLSLogger.Initialize(ExDateTime.UtcNow, Path.Combine(GLSLogger.GetExchangeInstallPath(), "Logging\\GLS\\"), GLSLogger.defaultMaxRetentionPeriod, GLSLogger.defaultDirectorySizeQuota, GLSLogger.defaultPerFileSizeQuota, true);
					}
				}
			}
			if (GLSLogger.Enabled)
			{
				LogRowFormatter logRowFormatter = new LogRowFormatter(GLSLogger.schema);
				logRowFormatter[1] = GLSLogger.GetNextSequenceNumber();
				logRowFormatter[2] = Globals.ProcessName;
				logRowFormatter[3] = Globals.ProcessId;
				logRowFormatter[4] = Globals.ProcessAppName;
				logRowFormatter[5] = Environment.CurrentManagedThreadId;
				logRowFormatter[6] = operation;
				logRowFormatter[7] = parameter;
				logRowFormatter[9] = processingTime;
				logRowFormatter[8] = resultCode;
				logRowFormatter[10] = ipAddress;
				logRowFormatter[11] = rawResponse.ResourceForest;
				logRowFormatter[12] = rawResponse.AccountForest;
				logRowFormatter[13] = rawResponse.TenantContainerCN;
				logRowFormatter[14] = rawResponse.TenantId;
				logRowFormatter[15] = rawResponse.SmtpNextHopDomain;
				logRowFormatter[16] = rawResponse.TenantFlags;
				logRowFormatter[17] = rawResponse.DomainName;
				logRowFormatter[18] = rawResponse.DomainInUse;
				logRowFormatter[19] = rawResponse.DomainFlags;
				logRowFormatter[20] = failure;
				logRowFormatter[21] = transactionid;
				logRowFormatter[22] = connectionid;
				logRowFormatter[23] = diagnostics;
				logRowFormatter[24] = activityId;
				GLSLogger.log.Append(logRowFormatter, 0);
			}
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0003602C File Offset: 0x0003422C
		internal static void BeginAppend(string operation, string parameter, string ipAddress, string resultCode, GlsRawResponse rawResponse, long processingTime, string failure, string transactionId, string connectionId, string diagnostics)
		{
			GLSLogger.AppendDelegate appendDelegate = new GLSLogger.AppendDelegate(GLSLogger.AppendInternal);
			Guid activityId = Guid.Empty;
			try
			{
				IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
				if (currentActivityScope != null && currentActivityScope.Status == ActivityContextStatus.ActivityStarted)
				{
					activityId = currentActivityScope.ActivityId;
				}
			}
			catch (Exception ex)
			{
				diagnostics += ex.ToString();
			}
			appendDelegate.BeginInvoke(operation, parameter, ipAddress, resultCode, rawResponse, processingTime, failure, transactionId, connectionId, diagnostics, activityId, null, null);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x000360A4 File Offset: 0x000342A4
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

		// Token: 0x06000BCA RID: 3018 RVA: 0x000360E8 File Offset: 0x000342E8
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

		// Token: 0x06000BCB RID: 3019 RVA: 0x00036124 File Offset: 0x00034324
		private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
		{
			lock (GLSLogger.logLock)
			{
				GLSLogger.Enabled = false;
				GLSLogger.Initialized = false;
				GLSLogger.Shutdown();
			}
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00036170 File Offset: 0x00034370
		private static void Shutdown()
		{
			if (GLSLogger.log != null)
			{
				GLSLogger.log.Close();
			}
			if (GLSLogger.timer != null)
			{
				GLSLogger.timer.Dispose();
				GLSLogger.timer = null;
			}
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0003619C File Offset: 0x0003439C
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

		// Token: 0x06000BCE RID: 3022 RVA: 0x00036208 File Offset: 0x00034408
		private static string[] GetColumnArray()
		{
			string[] array = new string[GLSLogger.Fields.Length];
			for (int i = 0; i < GLSLogger.Fields.Length; i++)
			{
				array[i] = GLSLogger.Fields[i].ColumnName;
			}
			return array;
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x00036250 File Offset: 0x00034450
		internal static void FaultInjectionTrace()
		{
			try
			{
				ExTraceGlobals.FaultInjectionTracer.TraceTest(2785422653U);
			}
			catch (TimeoutException)
			{
			}
			ExTraceGlobals.FaultInjectionTracer.TraceTest(3620089149U);
			ExTraceGlobals.FaultInjectionTracer.TraceTest(2814782781U);
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x000362A0 File Offset: 0x000344A0
		internal static void FaultInjectionDelayTraceForAsync()
		{
			try
			{
				ExTraceGlobals.FaultInjectionTracer.TraceTest(3624283453U);
			}
			catch (TimeoutException)
			{
			}
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x000362D4 File Offset: 0x000344D4
		internal static void FaultInjectionTraceForAsync()
		{
			ExTraceGlobals.FaultInjectionTracer.TraceTest(4161154365U);
			ExTraceGlobals.FaultInjectionTracer.TraceTest(2550541629U);
		}

		// Token: 0x040005E3 RID: 1507
		private const uint TransientGlsExceptionLid = 3620089149U;

		// Token: 0x040005E4 RID: 1508
		private const uint PermanentGlsExceptionLid = 2814782781U;

		// Token: 0x040005E5 RID: 1509
		private const uint GlsDelayLid = 2785422653U;

		// Token: 0x040005E6 RID: 1510
		private const uint AsyncTransientGlsExceptionLid = 4161154365U;

		// Token: 0x040005E7 RID: 1511
		private const uint AsyncPermanentGlsExceptionLid = 2550541629U;

		// Token: 0x040005E8 RID: 1512
		private const uint AsyncGlsDelayLid = 3624283453U;

		// Token: 0x040005E9 RID: 1513
		private const string LogTypeName = "GLS Logs";

		// Token: 0x040005EA RID: 1514
		private const string LogComponent = "GLSLogs";

		// Token: 0x040005EB RID: 1515
		private const int DefaultLogBufferSize = 65536;

		// Token: 0x040005EC RID: 1516
		private const bool DefaultLoggingEnabled = true;

		// Token: 0x040005ED RID: 1517
		private const string RegistryParameters = "SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess\\Parameters";

		// Token: 0x040005EE RID: 1518
		private const string LoggingEnabledRegKeyName = "ProtocolLoggingEnabled";

		// Token: 0x040005EF RID: 1519
		private const string LogBufferSizeRegKeyName = "LogBufferSize";

		// Token: 0x040005F0 RID: 1520
		private const string LoggingDirectoryUnderExchangeInstallPath = "Logging\\GLS\\";

		// Token: 0x040005F1 RID: 1521
		internal static readonly GLSLogger.FieldInfo[] Fields = new GLSLogger.FieldInfo[]
		{
			new GLSLogger.FieldInfo(GLSLogger.Field.DateTime, "date-time"),
			new GLSLogger.FieldInfo(GLSLogger.Field.SequenceNumber, "seq-number"),
			new GLSLogger.FieldInfo(GLSLogger.Field.ClientName, "process-name"),
			new GLSLogger.FieldInfo(GLSLogger.Field.Pid, "process-id"),
			new GLSLogger.FieldInfo(GLSLogger.Field.AppName, "application-name"),
			new GLSLogger.FieldInfo(GLSLogger.Field.ThreadId, "thread-id"),
			new GLSLogger.FieldInfo(GLSLogger.Field.Operation, "operation"),
			new GLSLogger.FieldInfo(GLSLogger.Field.Parameter, "parameter"),
			new GLSLogger.FieldInfo(GLSLogger.Field.ResultCode, "result-code"),
			new GLSLogger.FieldInfo(GLSLogger.Field.ProcessingTime, "processing-time"),
			new GLSLogger.FieldInfo(GLSLogger.Field.IPAddress, "ip-address"),
			new GLSLogger.FieldInfo(GLSLogger.Field.ResourceForest, "resource-forest"),
			new GLSLogger.FieldInfo(GLSLogger.Field.AccountForest, "account-forest"),
			new GLSLogger.FieldInfo(GLSLogger.Field.TenantContainerCN, "tenant-container-CN"),
			new GLSLogger.FieldInfo(GLSLogger.Field.TenantId, "tenant-id"),
			new GLSLogger.FieldInfo(GLSLogger.Field.SmtpNextHopDomain, "smtp-next-hop-domain"),
			new GLSLogger.FieldInfo(GLSLogger.Field.TenantFlags, "tenant-flags"),
			new GLSLogger.FieldInfo(GLSLogger.Field.DomainName, "domain-name"),
			new GLSLogger.FieldInfo(GLSLogger.Field.DomainInUse, "domain-in-use"),
			new GLSLogger.FieldInfo(GLSLogger.Field.DomainFlags, "domain-flags"),
			new GLSLogger.FieldInfo(GLSLogger.Field.Failures, "failures"),
			new GLSLogger.FieldInfo(GLSLogger.Field.TransactionId, "transaction-id"),
			new GLSLogger.FieldInfo(GLSLogger.Field.ConnectionId, "connection-id"),
			new GLSLogger.FieldInfo(GLSLogger.Field.Diagnostics, "diagnostics"),
			new GLSLogger.FieldInfo(GLSLogger.Field.ActivityId, "activity-id")
		};

		// Token: 0x040005F2 RID: 1522
		internal static IList<TenantProperty> TenantPropertiesToLog = new TenantProperty[]
		{
			TenantProperty.EXOResourceForest,
			TenantProperty.EXOAccountForest,
			TenantProperty.EXOSmtpNextHopDomain,
			TenantProperty.EXOTenantContainerCN
		};

		// Token: 0x040005F3 RID: 1523
		private static readonly LogSchema schema = new LogSchema("Microsoft Exchange", "15.00.1497.010", "GLS Logs", GLSLogger.GetColumnArray());

		// Token: 0x040005F4 RID: 1524
		private static TimeSpan defaultMaxRetentionPeriod = TimeSpan.FromHours(8.0);

		// Token: 0x040005F5 RID: 1525
		private static ByteQuantifiedSize defaultDirectorySizeQuota = ByteQuantifiedSize.Parse("200MB");

		// Token: 0x040005F6 RID: 1526
		private static ByteQuantifiedSize defaultPerFileSizeQuota = ByteQuantifiedSize.Parse("10MB");

		// Token: 0x040005F7 RID: 1527
		private static TimeSpan defaultFlushInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x040005F8 RID: 1528
		private static string logFilePrefix = Globals.ProcessName + "_";

		// Token: 0x040005F9 RID: 1529
		private static int sequenceNumber = 0;

		// Token: 0x040005FA RID: 1530
		private static Timer timer;

		// Token: 0x040005FB RID: 1531
		private static Log log;

		// Token: 0x040005FC RID: 1532
		private static object logLock = new object();

		// Token: 0x040005FD RID: 1533
		private static object incrementLock = new object();

		// Token: 0x040005FE RID: 1534
		private static RegistryWatcher registryWatcher;

		// Token: 0x02000119 RID: 281
		internal enum Field
		{
			// Token: 0x04000602 RID: 1538
			DateTime,
			// Token: 0x04000603 RID: 1539
			SequenceNumber,
			// Token: 0x04000604 RID: 1540
			ClientName,
			// Token: 0x04000605 RID: 1541
			Pid,
			// Token: 0x04000606 RID: 1542
			AppName,
			// Token: 0x04000607 RID: 1543
			ThreadId,
			// Token: 0x04000608 RID: 1544
			Operation,
			// Token: 0x04000609 RID: 1545
			Parameter,
			// Token: 0x0400060A RID: 1546
			ResultCode,
			// Token: 0x0400060B RID: 1547
			ProcessingTime,
			// Token: 0x0400060C RID: 1548
			IPAddress,
			// Token: 0x0400060D RID: 1549
			ResourceForest,
			// Token: 0x0400060E RID: 1550
			AccountForest,
			// Token: 0x0400060F RID: 1551
			TenantContainerCN,
			// Token: 0x04000610 RID: 1552
			TenantId,
			// Token: 0x04000611 RID: 1553
			SmtpNextHopDomain,
			// Token: 0x04000612 RID: 1554
			TenantFlags,
			// Token: 0x04000613 RID: 1555
			DomainName,
			// Token: 0x04000614 RID: 1556
			DomainInUse,
			// Token: 0x04000615 RID: 1557
			DomainFlags,
			// Token: 0x04000616 RID: 1558
			Failures,
			// Token: 0x04000617 RID: 1559
			TransactionId,
			// Token: 0x04000618 RID: 1560
			ConnectionId,
			// Token: 0x04000619 RID: 1561
			Diagnostics,
			// Token: 0x0400061A RID: 1562
			ActivityId
		}

		// Token: 0x0200011A RID: 282
		internal enum StatusCode
		{
			// Token: 0x0400061C RID: 1564
			Found,
			// Token: 0x0400061D RID: 1565
			NotFound,
			// Token: 0x0400061E RID: 1566
			WriteSuccess
		}

		// Token: 0x0200011B RID: 283
		// (Invoke) Token: 0x06000BD4 RID: 3028
		internal delegate void AppendDelegate(string operation, string parameter, string ipAddress, string resultCode, GlsRawResponse rawResponse, long processingTime, string failure, string transactionid, string connectionid, string diagnostics, Guid activityId);

		// Token: 0x0200011C RID: 284
		internal struct FieldInfo
		{
			// Token: 0x06000BD7 RID: 3031 RVA: 0x0003662B File Offset: 0x0003482B
			public FieldInfo(GLSLogger.Field field, string columnName)
			{
				this.Field = field;
				this.ColumnName = columnName;
			}

			// Token: 0x0400061F RID: 1567
			internal readonly GLSLogger.Field Field;

			// Token: 0x04000620 RID: 1568
			internal readonly string ColumnName;
		}
	}
}
