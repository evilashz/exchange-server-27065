using System;
using System.Configuration;

namespace Microsoft.Exchange.ProcessManager
{
	// Token: 0x02000813 RID: 2067
	internal class ServiceConfiguration
	{
		// Token: 0x06002B84 RID: 11140 RVA: 0x00060305 File Offset: 0x0005E505
		private ServiceConfiguration()
		{
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06002B85 RID: 11141 RVA: 0x0006030D File Offset: 0x0005E50D
		public int CheckProcessHandleTimeOut
		{
			get
			{
				return this.checkProcessHandleTimeOut;
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06002B86 RID: 11142 RVA: 0x00060315 File Offset: 0x0005E515
		public bool DisconnectTransportPerformanceCounters
		{
			get
			{
				return this.disconnectTransportPerformanceCounters;
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06002B87 RID: 11143 RVA: 0x0006031D File Offset: 0x0005E51D
		public int MaxIOThreads
		{
			get
			{
				return this.maxIOThreads;
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06002B88 RID: 11144 RVA: 0x00060325 File Offset: 0x0005E525
		public int MaxWorkerProcessThreads
		{
			get
			{
				return this.maxWorkerProcessThreads;
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06002B89 RID: 11145 RVA: 0x0006032D File Offset: 0x0005E52D
		public long MaxWorkerProcessWorkingSet
		{
			get
			{
				return this.maxWorkerProcessWorkingSet;
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x06002B8A RID: 11146 RVA: 0x00060335 File Offset: 0x0005E535
		public int MaxWorkerProcessRefreshInterval
		{
			get
			{
				return this.maxWorkerProcessRefreshInterval;
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06002B8B RID: 11147 RVA: 0x0006033D File Offset: 0x0005E53D
		public int MaxWorkerProcessExitTimeout
		{
			get
			{
				return this.maxWorkerProcessExitTimeout;
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06002B8C RID: 11148 RVA: 0x00060345 File Offset: 0x0005E545
		public int MaxWorkerProcessDumpTimeout
		{
			get
			{
				return this.maxWorkerProcessDumpTimeout;
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06002B8D RID: 11149 RVA: 0x0006034D File Offset: 0x0005E54D
		public int MaxProcessManagerRestartAttempts
		{
			get
			{
				return this.maxProcessManagerRestartAttempts;
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06002B8E RID: 11150 RVA: 0x00060355 File Offset: 0x0005E555
		public bool ServiceListening
		{
			get
			{
				return this.serviceListening;
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06002B8F RID: 11151 RVA: 0x0006035D File Offset: 0x0005E55D
		public int MaxProcessRestartAttemptsWhileInStartingState
		{
			get
			{
				return this.maxProcessRestartAttemptsWhileInStartingState;
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06002B90 RID: 11152 RVA: 0x00060365 File Offset: 0x0005E565
		public int ThrashCrashMaximum
		{
			get
			{
				return this.thrashCrashMaximum;
			}
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x00060370 File Offset: 0x0005E570
		public static ServiceConfiguration Load(ProcessManagerService processManagerService)
		{
			ServiceConfiguration serviceConfiguration = new ServiceConfiguration();
			if (!ServiceConfiguration.LoadAppSettingsConfigInt32("CheckProcessHandleTimeOut", out serviceConfiguration.checkProcessHandleTimeOut))
			{
				serviceConfiguration.checkProcessHandleTimeOut = 30;
			}
			else if (serviceConfiguration.checkProcessHandleTimeOut < ServiceConfiguration.CheckProcessHandleTimeOutMinimum || serviceConfiguration.checkProcessHandleTimeOut > ServiceConfiguration.CheckProcessHandleTimeOutMaximum)
			{
				serviceConfiguration.checkProcessHandleTimeOut = 30;
			}
			if (!ServiceConfiguration.LoadAppSettingsConfigBool("DisconnectTransportPerformanceCounters", out serviceConfiguration.disconnectTransportPerformanceCounters))
			{
				serviceConfiguration.disconnectTransportPerformanceCounters = ServiceConfiguration.DisconnectTransportPerformanceCountersDefault;
			}
			if (!ServiceConfiguration.LoadAppSettingsConfigInt32("MaxIOThreads", out serviceConfiguration.maxIOThreads))
			{
				serviceConfiguration.maxIOThreads = 0;
			}
			if (serviceConfiguration.maxIOThreads < ServiceConfiguration.MaxIOThreadsMinimum || serviceConfiguration.maxIOThreads > ServiceConfiguration.MaxIOThreadsMaximum)
			{
				serviceConfiguration.maxIOThreads = 30;
			}
			if (!ServiceConfiguration.LoadAppSettingsConfigInt32("MaxWorkerProcessThreads", out serviceConfiguration.maxWorkerProcessThreads))
			{
				serviceConfiguration.maxWorkerProcessThreads = 0;
			}
			if (!ServiceConfiguration.LoadAppSettingsConfigInt64("MaxWorkerProcessWorkingSet", out serviceConfiguration.maxWorkerProcessWorkingSet))
			{
				serviceConfiguration.maxWorkerProcessWorkingSet = 0L;
			}
			if (!ServiceConfiguration.LoadAppSettingsConfigInt32("MaxWorkerProcessRefreshInterval", out serviceConfiguration.maxWorkerProcessRefreshInterval))
			{
				serviceConfiguration.maxWorkerProcessRefreshInterval = 0;
			}
			if (!ServiceConfiguration.LoadAppSettingsConfigInt32("MaxWorkerProcessExitTimeout", out serviceConfiguration.maxWorkerProcessExitTimeout))
			{
				serviceConfiguration.maxWorkerProcessExitTimeout = processManagerService.MaxWorkerProcessExitTimeoutDefault;
			}
			if (!ServiceConfiguration.LoadAppSettingsConfigInt32("MaxProcessManagerRestartAttempts", out serviceConfiguration.maxProcessManagerRestartAttempts))
			{
				serviceConfiguration.maxProcessManagerRestartAttempts = 4;
			}
			if (!ServiceConfiguration.LoadAppSettingsConfigInt32("MaxProcessRestartAttemptsWhileInStartingState", out serviceConfiguration.maxProcessRestartAttemptsWhileInStartingState))
			{
				serviceConfiguration.maxProcessRestartAttemptsWhileInStartingState = 1;
			}
			if (!ServiceConfiguration.LoadAppSettingsConfigInt32("ThrashCrashMaximum", out serviceConfiguration.thrashCrashMaximum))
			{
				serviceConfiguration.thrashCrashMaximum = 3;
			}
			if (!ServiceConfiguration.LoadAppSettingsConfigInt32("MaxWorkerProcessExitTimeout", out serviceConfiguration.maxWorkerProcessExitTimeout))
			{
				serviceConfiguration.maxWorkerProcessExitTimeout = processManagerService.MaxWorkerProcessExitTimeoutDefault;
			}
			if (!ServiceConfiguration.LoadAppSettingsConfigInt32("MaxWorkerProcessDumpTimeout", out serviceConfiguration.maxWorkerProcessDumpTimeout))
			{
				serviceConfiguration.maxWorkerProcessDumpTimeout = processManagerService.MaxWorkerProcessDumpTimeoutDefault;
			}
			if (!ServiceConfiguration.LoadAppSettingsConfigBool("serviceListening", out serviceConfiguration.serviceListening))
			{
				serviceConfiguration.serviceListening = true;
			}
			return serviceConfiguration;
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x00060524 File Offset: 0x0005E724
		private static bool LoadAppSettingsConfigBool(string configName, out bool configvalue)
		{
			bool result = false;
			configvalue = false;
			string text = ConfigurationManager.AppSettings[configName];
			bool flag;
			if (text != null && bool.TryParse(text, out flag))
			{
				configvalue = flag;
				result = true;
			}
			return result;
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x00060558 File Offset: 0x0005E758
		private static bool LoadAppSettingsConfigInt32(string configName, out int configvalue)
		{
			bool result = false;
			configvalue = 0;
			string text = ConfigurationManager.AppSettings[configName];
			int num;
			if (text != null && int.TryParse(text, out num))
			{
				configvalue = num;
				result = true;
			}
			return result;
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x0006058C File Offset: 0x0005E78C
		private static bool LoadAppSettingsConfigInt64(string configName, out long configvalue)
		{
			bool result = false;
			configvalue = 0L;
			string text = ConfigurationManager.AppSettings[configName];
			long num;
			if (text != null && long.TryParse(text, out num))
			{
				configvalue = num;
				result = true;
			}
			return result;
		}

		// Token: 0x040025D9 RID: 9689
		private const int CheckProcessHandleTimeOutDefault = 30;

		// Token: 0x040025DA RID: 9690
		private const int MaxIOThreadsDefault = 30;

		// Token: 0x040025DB RID: 9691
		private const int MaxProcessManagerRestartAttemptsDefault = 4;

		// Token: 0x040025DC RID: 9692
		private const int MaxProcessRestartAttemptsWhileInStartingStateDefault = 1;

		// Token: 0x040025DD RID: 9693
		private const int ThrashCrashMaximumDefault = 3;

		// Token: 0x040025DE RID: 9694
		private const bool ServiceListeningDefault = true;

		// Token: 0x040025DF RID: 9695
		private const string CheckProcessHandleTimeOutLabel = "CheckProcessHandleTimeOut";

		// Token: 0x040025E0 RID: 9696
		private const string DisconnectTransportPerformanceCountersLabel = "DisconnectTransportPerformanceCounters";

		// Token: 0x040025E1 RID: 9697
		private const string MaxIOThreadsLabel = "MaxIOThreads";

		// Token: 0x040025E2 RID: 9698
		private const string MaxWorkerProcessExitTimeoutLabel = "MaxWorkerProcessExitTimeout";

		// Token: 0x040025E3 RID: 9699
		private const string MaxWorkerProcessDumpTimeoutLabel = "MaxWorkerProcessDumpTimeout";

		// Token: 0x040025E4 RID: 9700
		private const string MaxWorkerProcessThreadsLabel = "MaxWorkerProcessThreads";

		// Token: 0x040025E5 RID: 9701
		private const string MaxWorkerProcessWorkingSetLabel = "MaxWorkerProcessWorkingSet";

		// Token: 0x040025E6 RID: 9702
		private const string MaxWorkerProcessRefreshIntervalLabel = "MaxWorkerProcessRefreshInterval";

		// Token: 0x040025E7 RID: 9703
		private const string MaxProcessManagerRestartAttemptsLabel = "MaxProcessManagerRestartAttempts";

		// Token: 0x040025E8 RID: 9704
		private const string MaxProcessRestartAttemptsWhileInStartingStateLabel = "MaxProcessRestartAttemptsWhileInStartingState";

		// Token: 0x040025E9 RID: 9705
		private const string ThrashCrashMaximumLabel = "ThrashCrashMaximum";

		// Token: 0x040025EA RID: 9706
		private const string serviceListeningLabel = "serviceListening";

		// Token: 0x040025EB RID: 9707
		private int checkProcessHandleTimeOut;

		// Token: 0x040025EC RID: 9708
		private bool disconnectTransportPerformanceCounters;

		// Token: 0x040025ED RID: 9709
		private int maxIOThreads;

		// Token: 0x040025EE RID: 9710
		private int maxWorkerProcessThreads;

		// Token: 0x040025EF RID: 9711
		private long maxWorkerProcessWorkingSet;

		// Token: 0x040025F0 RID: 9712
		private int maxWorkerProcessRefreshInterval;

		// Token: 0x040025F1 RID: 9713
		private int maxWorkerProcessExitTimeout;

		// Token: 0x040025F2 RID: 9714
		private int maxWorkerProcessDumpTimeout;

		// Token: 0x040025F3 RID: 9715
		private int maxProcessManagerRestartAttempts;

		// Token: 0x040025F4 RID: 9716
		private int maxProcessRestartAttemptsWhileInStartingState;

		// Token: 0x040025F5 RID: 9717
		private bool serviceListening;

		// Token: 0x040025F6 RID: 9718
		private static int CheckProcessHandleTimeOutMinimum = 0;

		// Token: 0x040025F7 RID: 9719
		private static int CheckProcessHandleTimeOutMaximum = 300;

		// Token: 0x040025F8 RID: 9720
		private static bool DisconnectTransportPerformanceCountersDefault = true;

		// Token: 0x040025F9 RID: 9721
		private static int MaxIOThreadsMinimum = Environment.ProcessorCount;

		// Token: 0x040025FA RID: 9722
		private static int MaxIOThreadsMaximum = Environment.ProcessorCount * 100;

		// Token: 0x040025FB RID: 9723
		private int thrashCrashMaximum;
	}
}
