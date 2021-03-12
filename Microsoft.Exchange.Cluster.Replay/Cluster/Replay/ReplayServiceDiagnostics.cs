using System;
using System.Diagnostics;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020002FA RID: 762
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReplayServiceDiagnostics
	{
		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06001EE5 RID: 7909 RVA: 0x0008BECF File Offset: 0x0008A0CF
		private static Microsoft.Exchange.Diagnostics.Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ReplayServiceDiagnosticsTracer;
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06001EE6 RID: 7910 RVA: 0x0008BED6 File Offset: 0x0008A0D6
		internal long ProcessThreadCount
		{
			get
			{
				return this.processThreadCount;
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06001EE7 RID: 7911 RVA: 0x0008BEDE File Offset: 0x0008A0DE
		internal long ProcessHandleCount
		{
			get
			{
				return this.processHandleCount;
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06001EE8 RID: 7912 RVA: 0x0008BEE6 File Offset: 0x0008A0E6
		internal long ProcessPrivateMemorySize
		{
			get
			{
				return this.processPrivateMemorySize;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x0008BEEE File Offset: 0x0008A0EE
		// (set) Token: 0x06001EEA RID: 7914 RVA: 0x0008BEF6 File Offset: 0x0008A0F6
		public bool LimitReached { get; private set; }

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06001EEB RID: 7915 RVA: 0x0008BEFF File Offset: 0x0008A0FF
		// (set) Token: 0x06001EEC RID: 7916 RVA: 0x0008BF07 File Offset: 0x0008A107
		public bool DisableCrash { get; set; }

		// Token: 0x06001EED RID: 7917 RVA: 0x0008BF10 File Offset: 0x0008A110
		public ReplayServiceDiagnostics()
		{
			this.ObtainLocalCopyCount = new Func<int>(this.GetLocalCopyCount);
			this.ProcessName = "msexchangerepl";
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06001EEE RID: 7918 RVA: 0x0008BF49 File Offset: 0x0008A149
		// (set) Token: 0x06001EEF RID: 7919 RVA: 0x0008BF51 File Offset: 0x0008A151
		public string ProcessName { get; set; }

		// Token: 0x06001EF0 RID: 7920 RVA: 0x0008BF5C File Offset: 0x0008A15C
		protected void SendReportAndCrash(Exception ex, string key)
		{
			this.LimitReached = true;
			ReplayEventLogConstants.Tuple_ProcessDiagnosticsTerminatingService.LogEvent(null, new object[]
			{
				ex.Message,
				key
			});
			if (!this.DisableCrash)
			{
				ExWatson.SendReportAndCrashOnAnotherThread(ex);
			}
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x0008BFA0 File Offset: 0x0008A1A0
		protected void SendReportWithoutDumpAndCrash(Exception ex, string key)
		{
			this.LimitReached = true;
			ReplayEventLogConstants.Tuple_ProcessDiagnosticsTerminatingServiceNoDump.LogEvent(null, new object[]
			{
				ex.Message,
				key
			});
			if (!this.DisableCrash)
			{
				ExWatson.SendReport(ex, ReportOptions.ReportTerminateAfterSend | ReportOptions.DoNotCollectDumps, "No Watson dump being taken.");
				ExWatson.TerminateCurrentProcess();
			}
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x0008BFF0 File Offset: 0x0008A1F0
		private int GetLocalCopyCount()
		{
			int result = 0;
			ReplicaInstanceManager replicaInstanceManager = Dependencies.ReplayCoreManager.ReplicaInstanceManager;
			if (replicaInstanceManager != null)
			{
				result = replicaInstanceManager.GetRICount();
			}
			return result;
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06001EF3 RID: 7923 RVA: 0x0008C015 File Offset: 0x0008A215
		// (set) Token: 0x06001EF4 RID: 7924 RVA: 0x0008C01D File Offset: 0x0008A21D
		public Func<int> ObtainLocalCopyCount { get; set; }

		// Token: 0x06001EF5 RID: 7925 RVA: 0x0008C028 File Offset: 0x0008A228
		private int MemoryLimitInMB()
		{
			int num = this.ObtainLocalCopyCount();
			return Math.Min(RegistryParameters.MaximumProcessPrivateMemoryMB, RegistryParameters.MemoryLimitBaseInMB + num * RegistryParameters.MemoryLimitPerDBInMB);
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x0008C05C File Offset: 0x0008A25C
		private string BuildMemoryLimitRegKeyString()
		{
			return string.Format("RegKeyBase={0};RegValues={1},{2},{3};EffectiveLimit=Min({1},{2}+nCopies*{3})", new object[]
			{
				"SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters",
				"MaximumProcessPrivateMemoryMB",
				"MemoryLimitBaseInMB",
				"MemoryLimitPerDBInMB"
			});
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x0008C09B File Offset: 0x0008A29B
		private long GetWarningLimit(long errorLimit)
		{
			return errorLimit * 80L / 100L;
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x0008C0A8 File Offset: 0x0008A2A8
		public void RunProcessDiagnostics()
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				this.processThreadCount = (long)currentProcess.Threads.Count;
				ReplayServiceDiagnostics.Tracer.TraceDebug<long>((long)this.GetHashCode(), "Replay service current number of threads: {0}", this.processThreadCount);
				long num = (long)RegistryParameters.MaximumProcessThreadCount;
				long warningLimit = this.GetWarningLimit(num);
				if (this.processThreadCount > warningLimit)
				{
					ReplayCrimsonEvents.ResourceWarningForThreadsReached.LogPeriodic<string, long, long, long, string>(Environment.MachineName, this.ResourceWarningPeriod, this.ProcessName, this.processThreadCount, warningLimit, num, "Threads");
					if (this.processThreadCount > num)
					{
						Exception ex = new ReplayServiceTooManyThreadsException(this.processThreadCount, (long)RegistryParameters.MaximumProcessThreadCount);
						this.SendReportAndCrash(ex, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters\\MaximumProcessThreadCount");
					}
				}
				this.processHandleCount = (long)currentProcess.HandleCount;
				ReplayServiceDiagnostics.Tracer.TraceDebug<long>((long)this.GetHashCode(), "Replay service current number of handles: {0}", this.processHandleCount);
				num = (long)RegistryParameters.MaximumProcessHandleCount;
				warningLimit = this.GetWarningLimit(num);
				if (this.processHandleCount > warningLimit)
				{
					ReplayCrimsonEvents.ResourceWarningForHandlesReached.LogPeriodic<string, long, long, long, string>(Environment.MachineName, this.ResourceWarningPeriod, this.ProcessName, this.processHandleCount, warningLimit, num, "OS Handles");
					if (this.processHandleCount > num)
					{
						Exception ex2 = new ReplayServiceTooManyHandlesException(this.processHandleCount, (long)RegistryParameters.MaximumProcessHandleCount);
						this.SendReportAndCrash(ex2, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters\\MaximumProcessHandleCount");
					}
				}
				this.processPrivateMemorySize = currentProcess.PrivateMemorySize64;
				double num2 = (double)this.processPrivateMemorySize / ReplayServiceDiagnostics.memorySizeMultiplier;
				ReplayServiceDiagnostics.Tracer.TraceDebug<double>((long)this.GetHashCode(), "Replay service current memory usage: {0} MiB", num2);
				long num3 = (long)num2;
				num = (long)this.MemoryLimitInMB();
				warningLimit = this.GetWarningLimit(num);
				if (num3 > warningLimit)
				{
					ReplayCrimsonEvents.ResourceWarningForMemoryReached.LogPeriodic<string, long, long, long, string>(Environment.MachineName, this.ResourceWarningPeriod, this.ProcessName, num3, warningLimit, num, "processPrivateMemorySize");
					if (num3 > num)
					{
						if (RegistryParameters.EnableWatsonDumpOnTooMuchMemory)
						{
							Exception ex3 = new ReplayServiceTooMuchMemoryException(num2, num);
							this.SendReportAndCrash(ex3, this.BuildMemoryLimitRegKeyString());
						}
						else
						{
							Exception ex4 = new ReplayServiceTooMuchMemoryNoDumpException(num2, num, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters\\EnableWatsonDumpOnTooMuchMemory");
							this.SendReportWithoutDumpAndCrash(ex4, this.BuildMemoryLimitRegKeyString());
						}
					}
				}
				if (RegistryParameters.MonitorGCHandleCount)
				{
					num = (long)RegistryParameters.MaximumGCHandleCount;
					warningLimit = this.GetWarningLimit(num);
					PerformanceCounter performanceCounter = null;
					try
					{
						performanceCounter = new PerformanceCounter(".NET CLR Memory", "# GC Handles", "msexchangerepl");
						long rawValue = performanceCounter.RawValue;
						if (rawValue > warningLimit)
						{
							ReplayCrimsonEvents.ResourceWarningForHandlesReached.LogPeriodic<string, long, long, long, string>(Environment.MachineName, this.ResourceWarningPeriod, this.ProcessName, rawValue, warningLimit, num, "GC Handles");
							if (rawValue > num)
							{
								Exception ex5 = new ReplayServiceTooManyHandlesException(rawValue, num);
								this.SendReportAndCrash(ex5, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters\\MaximumGCHandleCount");
							}
						}
					}
					catch (InvalidOperationException arg)
					{
						ReplayServiceDiagnostics.Tracer.TraceError<InvalidOperationException>((long)this.GetHashCode(), "Perf counters are broken, preventing the MaximumGCHandleCount check: {0}", arg);
					}
					finally
					{
						if (performanceCounter != null)
						{
							performanceCounter.Dispose();
							performanceCounter = null;
						}
					}
				}
			}
		}

		// Token: 0x04000CE2 RID: 3298
		private const long warningLimitPercentage = 80L;

		// Token: 0x04000CE3 RID: 3299
		private static double memorySizeMultiplier = 1048576.0;

		// Token: 0x04000CE4 RID: 3300
		private long processThreadCount;

		// Token: 0x04000CE5 RID: 3301
		private long processHandleCount;

		// Token: 0x04000CE6 RID: 3302
		private long processPrivateMemorySize;

		// Token: 0x04000CE7 RID: 3303
		private readonly TimeSpan ResourceWarningPeriod = TimeSpan.FromMinutes(60.0);
	}
}
