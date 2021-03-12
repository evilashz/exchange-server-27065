using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Service.Common;

namespace Microsoft.Exchange.Diagnostics.PerformanceLogger
{
	// Token: 0x02000002 RID: 2
	public class PerformanceLogMonitor
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public PerformanceLogMonitor(TimeSpan monitorInterval, Action<PerformanceLogMonitor> createLogSets)
		{
			this.threadDone = new ManualResetEvent(false);
			this.monitorInterval = monitorInterval;
			this.monitoredSets = new List<PerformanceLogMonitor.MonitoredLogSet>();
			this.createLogSets = createLogSets;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x0000211E File Offset: 0x0000031E
		public TimeSpan MonitorInterval
		{
			get
			{
				return this.monitorInterval;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002128 File Offset: 0x00000328
		public void AddPerflog(PerformanceLogSet performanceLog, TimeSpan? restartInterval)
		{
			lock (this.performanceLogListLock)
			{
				PerformanceLogMonitor.MonitoredLogSet item = new PerformanceLogMonitor.MonitoredLogSet(performanceLog, restartInterval);
				this.monitoredSets.Add(item);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002178 File Offset: 0x00000378
		public void StartMonitor()
		{
			if (!this.isStarted)
			{
				lock (this.monitorControlLock)
				{
					if (!this.isStarted)
					{
						this.statusThread = new Thread(new ThreadStart(this.CheckStatusThread));
						this.statusThread.Name = "PerformanceLogMonitor";
						this.statusThread.Start();
						this.isStarted = true;
					}
				}
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021FC File Offset: 0x000003FC
		public void StopMonitor()
		{
			if (this.isStarted)
			{
				lock (this.monitorControlLock)
				{
					if (this.isStarted)
					{
						this.threadDone.Set();
						this.statusThread.Join();
						this.threadDone.Reset();
						this.isStarted = false;
						this.statusThread = null;
					}
				}
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002278 File Offset: 0x00000478
		private void CheckStatusThread()
		{
			this.createLogSets(this);
			do
			{
				this.CheckPerflogStatus();
			}
			while (!this.threadDone.WaitOne(this.MonitorInterval));
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000022A0 File Offset: 0x000004A0
		private void CheckPerflogStatus()
		{
			lock (this.performanceLogListLock)
			{
				foreach (PerformanceLogMonitor.MonitoredLogSet monitoredLogSet in this.monitoredSets)
				{
					try
					{
						if (monitoredLogSet.LastRestart == DateTime.MinValue)
						{
							monitoredLogSet.CounterSet.StopLog(true);
							monitoredLogSet.CounterSet.CreateLogSettings();
						}
						PerformanceLogSet.PerformanceLogSetStatus status = monitoredLogSet.CounterSet.Status;
						bool flag2 = status != PerformanceLogSet.PerformanceLogSetStatus.Running;
						if (status == PerformanceLogSet.PerformanceLogSetStatus.DoesNotExist)
						{
							monitoredLogSet.CounterSet.CreateLogSettings();
						}
						if (!flag2 && monitoredLogSet.RestartInterval != null && DateTime.UtcNow - monitoredLogSet.LastRestart > monitoredLogSet.RestartInterval)
						{
							monitoredLogSet.CounterSet.StopLog(true);
							flag2 = true;
						}
						if (flag2)
						{
							monitoredLogSet.CounterSet.StartLog(false);
							monitoredLogSet.LastRestart = DateTime.UtcNow;
						}
						monitoredLogSet.FailureCount = 0;
					}
					catch (Exception ex)
					{
						Logger.LogWarningMessage("Check PerflogStatus hit exception {0} for performance log set {1}", new object[]
						{
							ex,
							monitoredLogSet.CounterSet.CounterSetName
						});
						if (5 <= monitoredLogSet.FailureCount)
						{
							Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_PerformanceLogError, new object[]
							{
								ex,
								monitoredLogSet.CounterSet.CounterSetName
							});
							throw;
						}
						monitoredLogSet.FailureCount++;
						if (!(ex is ArgumentException) && !(ex is COMException))
						{
							throw;
						}
						string text = Path.Combine(Environment.GetEnvironmentVariable("windir"), "System32\\Tasks\\Microsoft\\Windows\\PLA", monitoredLogSet.CounterSet.CounterSetName);
						Logger.LogWarningMessage("PerflogStatus deleting logset info at '{0}'", new object[]
						{
							text
						});
						PerformanceLogSet.DeleteTask(monitoredLogSet.CounterSet.CounterSetName);
						if (File.Exists(text))
						{
							File.Delete(text);
						}
					}
				}
			}
		}

		// Token: 0x04000001 RID: 1
		private const int MaximumFailureCount = 5;

		// Token: 0x04000002 RID: 2
		private readonly List<PerformanceLogMonitor.MonitoredLogSet> monitoredSets;

		// Token: 0x04000003 RID: 3
		private readonly ManualResetEvent threadDone;

		// Token: 0x04000004 RID: 4
		private readonly TimeSpan monitorInterval;

		// Token: 0x04000005 RID: 5
		private readonly object monitorControlLock = new object();

		// Token: 0x04000006 RID: 6
		private readonly object performanceLogListLock = new object();

		// Token: 0x04000007 RID: 7
		private readonly Action<PerformanceLogMonitor> createLogSets;

		// Token: 0x04000008 RID: 8
		private Thread statusThread;

		// Token: 0x04000009 RID: 9
		private bool isStarted;

		// Token: 0x02000003 RID: 3
		internal class MonitoredLogSet
		{
			// Token: 0x06000008 RID: 8 RVA: 0x000024F8 File Offset: 0x000006F8
			internal MonitoredLogSet(PerformanceLogSet counterSet, TimeSpan? restartInterval)
			{
				this.counterSet = counterSet;
				this.restartInterval = restartInterval;
				this.lastRestart = DateTime.MinValue;
				this.failureCount = 0;
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000009 RID: 9 RVA: 0x00002520 File Offset: 0x00000720
			internal PerformanceLogSet CounterSet
			{
				get
				{
					return this.counterSet;
				}
			}

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x0600000A RID: 10 RVA: 0x00002528 File Offset: 0x00000728
			internal TimeSpan? RestartInterval
			{
				get
				{
					return this.restartInterval;
				}
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x0600000B RID: 11 RVA: 0x00002530 File Offset: 0x00000730
			// (set) Token: 0x0600000C RID: 12 RVA: 0x00002538 File Offset: 0x00000738
			internal DateTime LastRestart
			{
				get
				{
					return this.lastRestart;
				}
				set
				{
					this.lastRestart = value;
				}
			}

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x0600000D RID: 13 RVA: 0x00002541 File Offset: 0x00000741
			// (set) Token: 0x0600000E RID: 14 RVA: 0x00002549 File Offset: 0x00000749
			internal int FailureCount
			{
				get
				{
					return this.failureCount;
				}
				set
				{
					this.failureCount = value;
				}
			}

			// Token: 0x0400000A RID: 10
			private readonly PerformanceLogSet counterSet;

			// Token: 0x0400000B RID: 11
			private readonly TimeSpan? restartInterval;

			// Token: 0x0400000C RID: 12
			private DateTime lastRestart;

			// Token: 0x0400000D RID: 13
			private int failureCount;
		}
	}
}
