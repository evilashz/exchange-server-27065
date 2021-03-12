using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000BF7 RID: 3063
	internal class DnsLog
	{
		// Token: 0x0600432B RID: 17195 RVA: 0x000B4078 File Offset: 0x000B2278
		public static void Start(string dnsLogPath, TimeSpan dnsLogmaxAge, long dnsLogMaxDirectorySize, long dnsLogMaxFileSize)
		{
			if (DnsLog.enabled)
			{
				throw new InvalidOperationException("DnsLogPath cannot be started twice");
			}
			ArgumentValidator.ThrowIfOutOfRange<long>("dnsLogMaxDirectorySize", dnsLogMaxDirectorySize, 0L, long.MaxValue);
			ArgumentValidator.ThrowIfOutOfRange<long>("dnsLogMaxFileSize", dnsLogMaxFileSize, 0L, long.MaxValue);
			if (string.IsNullOrEmpty(dnsLogPath))
			{
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				dnsLogPath = Path.Combine(Directory.GetParent(Path.GetDirectoryName(executingAssembly.Location)).FullName, "TransportRoles\\Logs\\Dns\\");
			}
			DnsLog.dnsLogSchema = new LogSchema("Microsoft Exchange Server", "15.00.1497.010", "DNS log", DnsLog.Fields);
			DnsLog.log = new AsyncLog("DnsLog", new LogHeaderFormatter(DnsLog.dnsLogSchema), "DnsLogs");
			DnsLog.log.Configure(dnsLogPath, dnsLogmaxAge, dnsLogMaxDirectorySize, dnsLogMaxFileSize, 5242880, TimeSpan.FromSeconds(60.0), TimeSpan.FromSeconds(15.0));
			DnsLog.enabled = true;
			DnsLog.LogServiceStart();
		}

		// Token: 0x0600432C RID: 17196 RVA: 0x000B4168 File Offset: 0x000B2368
		public static void LogServiceStart()
		{
			if (!DnsLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(DnsLog.dnsLogSchema);
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				logRowFormatter[3] = string.Format("DNS Logging for process {0} started.", currentProcess.ProcessName);
			}
			logRowFormatter[1] = DnsLogEventId.START;
			DnsLog.log.Append(logRowFormatter, 0);
		}

		// Token: 0x0600432D RID: 17197 RVA: 0x000B41DC File Offset: 0x000B23DC
		public static void Stop()
		{
			if (DnsLog.log != null)
			{
				DnsLog.enabled = false;
				DnsLog.log.Close();
				DnsLog.log = null;
			}
		}

		// Token: 0x0600432E RID: 17198 RVA: 0x000B41FC File Offset: 0x000B23FC
		private static string[] InitializeFields()
		{
			string[] array = new string[Enum.GetValues(typeof(DnsLog.DnsLogField)).Length];
			array[0] = "Timestamp";
			array[1] = "EventId";
			array[2] = "RequestId";
			array[3] = "Data";
			return array;
		}

		// Token: 0x0600432F RID: 17199 RVA: 0x000B4244 File Offset: 0x000B2444
		public static void Log(object request, string format, params object[] parameters)
		{
			if (!DnsLog.enabled)
			{
				return;
			}
			LogRowFormatter logRowFormatter = new LogRowFormatter(DnsLog.dnsLogSchema);
			logRowFormatter[3] = string.Format(format, parameters);
			logRowFormatter[2] = ((request == null) ? 0 : request.GetHashCode());
			DnsLog.Append(logRowFormatter);
		}

		// Token: 0x06004330 RID: 17200 RVA: 0x000B4290 File Offset: 0x000B2490
		private static void Append(LogRowFormatter row)
		{
			try
			{
				DnsLog.log.Append(row, 0);
			}
			catch (ObjectDisposedException)
			{
				ExTraceGlobals.DNSTracer.TraceError(0L, "DnsLog append failed with ObjectDisposedException");
			}
		}

		// Token: 0x0400392E RID: 14638
		private const string LogComponentName = "DnsLogs";

		// Token: 0x0400392F RID: 14639
		private static readonly string[] Fields = DnsLog.InitializeFields();

		// Token: 0x04003930 RID: 14640
		private static LogSchema dnsLogSchema;

		// Token: 0x04003931 RID: 14641
		private static AsyncLog log;

		// Token: 0x04003932 RID: 14642
		private static bool enabled;

		// Token: 0x02000BF8 RID: 3064
		internal enum DnsLogField
		{
			// Token: 0x04003934 RID: 14644
			Time,
			// Token: 0x04003935 RID: 14645
			EventId,
			// Token: 0x04003936 RID: 14646
			RequestId,
			// Token: 0x04003937 RID: 14647
			Data
		}
	}
}
