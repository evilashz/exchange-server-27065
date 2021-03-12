using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.WorkloadManagement;
using Microsoft.Win32;

namespace Microsoft.Exchange.Data.Directory.Diagnostics
{
	// Token: 0x020000BE RID: 190
	internal class DirectoryThrottlingLog : DisposeTrackableBase
	{
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x0002C378 File Offset: 0x0002A578
		internal static int CountOfDCsToLog
		{
			get
			{
				return DirectoryThrottlingLog.countOfDCsToLog;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x0002C37F File Offset: 0x0002A57F
		internal static bool LoggingEnabled
		{
			get
			{
				return DirectoryThrottlingLog.loggingEnabled.Value;
			}
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0002C38C File Offset: 0x0002A58C
		private DirectoryThrottlingLog()
		{
			this.logSchema = new LogSchema("Microsoft Exchange AD Throttling", Assembly.GetExecutingAssembly().GetName().Version.ToString(), "Directory Throttling Log", Enum.GetNames(typeof(DirectoryThrottlingLog.DirectoryThrottlingLogFields)));
			this.log = new Log(DirectoryThrottlingLog.FileNamePrefixName, new LogHeaderFormatter(this.logSchema, true), "DirectoryThrottling");
			DirectoryThrottlingLog.ReadConfigData();
			DirectoryThrottlingLog.registryWatcher = new RegistryWatcher("SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess\\Parameters", false);
			DirectoryThrottlingLog.timer = new Timer(new TimerCallback(DirectoryThrottlingLog.UpdateConfigIfChanged), null, 0, 300000);
			this.Configure();
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x0002C43B File Offset: 0x0002A63B
		public static DirectoryThrottlingLog Instance
		{
			get
			{
				return DirectoryThrottlingLog.instance;
			}
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0002C444 File Offset: 0x0002A644
		public void Configure()
		{
			if (!base.IsDisposed)
			{
				lock (this.logLock)
				{
					this.log.Configure(Path.Combine(DirectoryLogUtils.GetExchangeInstallPath(), "Logging\\DirectoryThrottling\\"), TimeSpan.FromDays(30.0), 104857600L, 104857600L, 10485760, TimeSpan.FromSeconds(10.0), true);
				}
			}
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0002C4D0 File Offset: 0x0002A6D0
		public void Close()
		{
			if (!base.IsDisposed && this.log != null)
			{
				this.log.Close();
				this.log = null;
			}
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0002C4F4 File Offset: 0x0002A6F4
		public void Log(string targetForest, ResourceLoadState resourceLoadState, int metricValue, Dictionary<string, ADServerMetrics> topDCsToLog)
		{
			if (DirectoryThrottlingLog.LoggingEnabled)
			{
				int count = topDCsToLog.Count;
				StringBuilder stringBuilder = new StringBuilder();
				foreach (KeyValuePair<string, ADServerMetrics> keyValuePair in topDCsToLog)
				{
					stringBuilder.AppendFormat("{0},{1},", keyValuePair.Key, keyValuePair.Value.IncomingDebt);
				}
				string topDomainControllersIncomingDebt = stringBuilder.ToString().Trim(new char[]
				{
					','
				});
				this.LogRow(Globals.ProcessName, Globals.ProcessId, Thread.CurrentThread.ManagedThreadId, targetForest, resourceLoadState, metricValue, topDomainControllersIncomingDebt);
			}
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0002C5B4 File Offset: 0x0002A7B4
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0002C5BF File Offset: 0x0002A7BF
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<DirectoryThrottlingLog>(this);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0002C5C8 File Offset: 0x0002A7C8
		private void LogRow(string processName, int processId, int threadId, string targetForest, ResourceLoadState resourceLoadState, int metricValue, string topDomainControllersIncomingDebt)
		{
			LogRowFormatter logRowFormatter = new LogRowFormatter(this.logSchema);
			logRowFormatter[1] = processName;
			logRowFormatter[2] = processId;
			logRowFormatter[3] = threadId;
			logRowFormatter[4] = targetForest;
			logRowFormatter[5] = resourceLoadState;
			logRowFormatter[6] = metricValue;
			logRowFormatter[7] = topDomainControllersIncomingDebt;
			this.AppendLogRow(logRowFormatter);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0002C638 File Offset: 0x0002A838
		private void AppendLogRow(LogRowFormatter row)
		{
			if (!base.IsDisposed)
			{
				this.log.Append(row, 0);
			}
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0002C650 File Offset: 0x0002A850
		private static void ReadConfigData()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess\\Parameters"))
			{
				DirectoryThrottlingLog.loggingEnabled = new bool?(DirectoryLogUtils.GetRegistryBool(registryKey, "DirectoryThrottlingLogEnabled", true));
				DirectoryThrottlingLog.countOfDCsToLog = DirectoryLogUtils.GetRegistryInt(registryKey, "DirectoryThrottlingNumberOfDCsToLog", 5);
			}
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0002C6B0 File Offset: 0x0002A8B0
		private static void UpdateConfigIfChanged(object state)
		{
			if (DirectoryThrottlingLog.registryWatcher.IsChanged())
			{
				DirectoryThrottlingLog.ReadConfigData();
			}
		}

		// Token: 0x04000399 RID: 921
		private const string LogTypeName = "Directory Throttling Log";

		// Token: 0x0400039A RID: 922
		private const string LogComponentName = "DirectoryThrottling";

		// Token: 0x0400039B RID: 923
		private const string SoftwareName = "Microsoft Exchange AD Throttling";

		// Token: 0x0400039C RID: 924
		protected const string RegistryParameters = "SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess\\Parameters";

		// Token: 0x0400039D RID: 925
		private const string LoggingEnabledRegKeyName = "DirectoryThrottlingLogEnabled";

		// Token: 0x0400039E RID: 926
		private const string NumberOfDCsToLogRegKeyName = "DirectoryThrottlingNumberOfDCsToLog";

		// Token: 0x0400039F RID: 927
		private const bool DefaultLogEnabled = true;

		// Token: 0x040003A0 RID: 928
		private const int DefaultNumberOfDCsToLog = 5;

		// Token: 0x040003A1 RID: 929
		private static readonly string FileNamePrefixName = Globals.ProcessName + "_";

		// Token: 0x040003A2 RID: 930
		private static readonly DirectoryThrottlingLog instance = new DirectoryThrottlingLog();

		// Token: 0x040003A3 RID: 931
		private static RegistryWatcher registryWatcher;

		// Token: 0x040003A4 RID: 932
		private Log log;

		// Token: 0x040003A5 RID: 933
		private LogSchema logSchema;

		// Token: 0x040003A6 RID: 934
		private static int countOfDCsToLog;

		// Token: 0x040003A7 RID: 935
		private static bool? loggingEnabled;

		// Token: 0x040003A8 RID: 936
		private static Timer timer;

		// Token: 0x040003A9 RID: 937
		private object logLock = new object();

		// Token: 0x020000BF RID: 191
		internal enum DirectoryThrottlingLogFields
		{
			// Token: 0x040003AB RID: 939
			Timestamp,
			// Token: 0x040003AC RID: 940
			ProcessName,
			// Token: 0x040003AD RID: 941
			ProcessId,
			// Token: 0x040003AE RID: 942
			ThreadId,
			// Token: 0x040003AF RID: 943
			TargetForest,
			// Token: 0x040003B0 RID: 944
			ResourceLoadState,
			// Token: 0x040003B1 RID: 945
			MetricValue,
			// Token: 0x040003B2 RID: 946
			TopDomainControllersIncomingDebt
		}
	}
}
