using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x0200002B RID: 43
	public class WTFLogConfiguration : ILogConfiguration
	{
		// Token: 0x060002F6 RID: 758 RVA: 0x0000AD20 File Offset: 0x00008F20
		public WTFLogConfiguration()
		{
			this.IsLoggingEnabled = Settings.IsTraceLoggingEnabled;
			if (string.IsNullOrEmpty(Settings.DefaultTraceLogPath))
			{
				this.IsLoggingEnabled = false;
			}
			if (this.IsLoggingEnabled)
			{
				this.LogPath = WTFLogConfiguration.GetLogDirectoryPath();
				this.MaxLogAge = TimeSpan.FromDays((double)Settings.MaxLogAge);
				this.MaxLogDirectorySizeInBytes = Settings.MaxTraceLogsDirectorySizeInBytes;
				this.MaxLogFileSizeInBytes = Settings.MaxTraceLogFileSizeInBytes;
				this.LogBufferSizeInBytes = Settings.TraceLogBufferSizeInBytes;
				this.FlushIntervalInMinutes = new TimeSpan(0, Settings.TraceLogFlushIntervalInMinutes, 0);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000ADC2 File Offset: 0x00008FC2
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x0000ADCA File Offset: 0x00008FCA
		public bool IsLoggingEnabled { get; private set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000ADD3 File Offset: 0x00008FD3
		public bool IsActivityEventHandler
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000ADD6 File Offset: 0x00008FD6
		// (set) Token: 0x060002FB RID: 763 RVA: 0x0000ADDE File Offset: 0x00008FDE
		public string LogPath { get; private set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000ADE7 File Offset: 0x00008FE7
		// (set) Token: 0x060002FD RID: 765 RVA: 0x0000ADEF File Offset: 0x00008FEF
		public TimeSpan MaxLogAge { get; private set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000ADF8 File Offset: 0x00008FF8
		// (set) Token: 0x060002FF RID: 767 RVA: 0x0000AE00 File Offset: 0x00009000
		public long MaxLogDirectorySizeInBytes { get; private set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000AE09 File Offset: 0x00009009
		// (set) Token: 0x06000301 RID: 769 RVA: 0x0000AE11 File Offset: 0x00009011
		public long MaxLogFileSizeInBytes { get; private set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000AE1A File Offset: 0x0000901A
		public string LogComponent
		{
			get
			{
				return "ActiveMonitoringTraceLogs";
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000AE21 File Offset: 0x00009021
		public string LogPrefix
		{
			get
			{
				return "ActiveMonitoringTraceLogs";
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000AE28 File Offset: 0x00009028
		public string LogType
		{
			get
			{
				return "ActiveMonitoringTraceLogs";
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000AE2F File Offset: 0x0000902F
		// (set) Token: 0x06000306 RID: 774 RVA: 0x0000AE37 File Offset: 0x00009037
		public int LogBufferSizeInBytes { get; private set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000AE40 File Offset: 0x00009040
		// (set) Token: 0x06000308 RID: 776 RVA: 0x0000AE48 File Offset: 0x00009048
		public TimeSpan FlushIntervalInMinutes { get; private set; }

		// Token: 0x06000309 RID: 777 RVA: 0x0000AE54 File Offset: 0x00009054
		private static string GetLogDirectoryPath()
		{
			string processName = Process.GetCurrentProcess().ProcessName;
			return Path.Combine(Path.Combine(Settings.DefaultTraceLogPath, "Monitoring\\Monitoring"), processName, "ActiveMonitoringTraceLogs");
		}

		// Token: 0x04000103 RID: 259
		public const string SoftwareName = "Microsoft Exchange Server Active Monitoring";

		// Token: 0x04000104 RID: 260
		private const string LogName = "ActiveMonitoringTraceLogs";

		// Token: 0x04000105 RID: 261
		private const string LogNamePrefix = "ActiveMonitoringTraceLogs";

		// Token: 0x04000106 RID: 262
		private const string LogNameType = "ActiveMonitoringTraceLogs";

		// Token: 0x04000107 RID: 263
		private const string LogComponentValue = "ActiveMonitoringTraceLogs";

		// Token: 0x04000108 RID: 264
		public readonly string SoftwareVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
	}
}
