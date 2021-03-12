using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Timers;
using Microsoft.Exchange.Diagnostics.PerformanceLogger;
using Microsoft.Exchange.Diagnostics.Service.Common;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service
{
	// Token: 0x0200001E RID: 30
	public class RetentionAgent : IDisposable
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00007090 File Offset: 0x00005290
		public RetentionAgent(string enforcedDirectory, TimeSpan retentionPeriod, int maxDirectorySizeMBytes, TimeSpan checkInterval, bool logDataLossMessage)
		{
			Logger.LogInformationMessage("RetentionAgent: A retention agent instance is created - enforcedDirectory: '{0}' retentionPeriod: '{1}' maxDirectorySizeMBytes: '{2}' checkInterval: '{3}'", new object[]
			{
				enforcedDirectory,
				retentionPeriod,
				maxDirectorySizeMBytes,
				checkInterval
			});
			Directory.CreateDirectory(enforcedDirectory);
			this.enforcedDirectory = enforcedDirectory;
			this.retentionPeriod = retentionPeriod;
			this.maxDirectorySizeInMBytes = maxDirectorySizeMBytes;
			this.checkInterval = checkInterval;
			this.lockObject = new object();
			this.logDataLossMessage = logDataLossMessage;
			this.timer = new Timer(1.0);
			this.timer.Elapsed += this.OnTimedEvent;
			this.timer.SynchronizingObject = null;
			this.timer.Start();
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000714E File Offset: 0x0000534E
		public string EnforcedDirectory
		{
			get
			{
				return this.enforcedDirectory;
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00007156 File Offset: 0x00005356
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				this.InternalDispose(true);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000716E File Offset: 0x0000536E
		protected virtual void InternalDispose(bool disposing)
		{
			if (disposing && this.timer != null)
			{
				this.timer.Enabled = false;
				this.timer.Dispose();
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00007194 File Offset: 0x00005394
		private void OnTimedEvent(object source, ElapsedEventArgs arg)
		{
			lock (this.lockObject)
			{
				if (this.timer.Interval != this.checkInterval.TotalMilliseconds)
				{
					this.timer.Interval = this.checkInterval.TotalMilliseconds;
				}
				try
				{
					this.Purge();
				}
				catch (Exception arg2)
				{
					string text = string.Format("RetentionAgent: Unhandled Exception. Enforced Directory: '{0}' '{1}'", this.enforcedDirectory, arg2);
					Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_RetentionAgentUnhandledException, new object[]
					{
						text
					});
					Logger.LogErrorMessage("{0}", new object[]
					{
						text
					});
					throw;
				}
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00007268 File Offset: 0x00005468
		private void Purge()
		{
			Logger.LogInformationMessage("RetentionAgent: Retention Agent is waking up, analyzing the directory: {0}", new object[]
			{
				this.enforcedDirectory
			});
			IEnumerable<FileInfo> enumerable = null;
			try
			{
				IEnumerable<FileInfo> files = new DirectoryInfo(this.enforcedDirectory).GetFiles();
				enumerable = from file in files
				orderby file.CreationTimeUtc descending
				select file;
			}
			catch (IOException ex)
			{
				Logger.LogErrorMessage("RetentionAgent: Failed to read/sort files in the directory: {0} Will try again next time. {1}", new object[]
				{
					this.enforcedDirectory,
					ex
				});
				return;
			}
			HashSet<FileInfo> hashSet = new HashSet<FileInfo>();
			long num = (long)this.maxDirectorySizeInMBytes * 1024L * 1024L;
			long num2 = 0L;
			foreach (FileInfo fileInfo in enumerable)
			{
				num2 += fileInfo.Length;
				if (num2 > num || DateTime.UtcNow - fileInfo.CreationTimeUtc > this.retentionPeriod)
				{
					hashSet.Add(fileInfo);
				}
			}
			if (this.logDataLossMessage)
			{
				if (num2 <= num)
				{
					string text = null;
					if ((double)num2 > (double)num * 0.95)
					{
						text = string.Format("RetentionAgent: Warning: Potential data loss. The size of this folder {0} has reached 95% of max size allowed - {1} MB. Some data will be purged once it reaches the max limit.", this.enforcedDirectory, this.maxDirectorySizeInMBytes);
					}
					else if ((double)num2 > (double)num * 0.7)
					{
						text = string.Format("RetentionAgent: Warning: Potential data loss. The size of this folder {0} has reached 70% of max size allowed - {1} MB. Some data will be purged once it reaches the max limit.", this.enforcedDirectory, this.maxDirectorySizeInMBytes);
					}
					if (text != null)
					{
						Logger.LogWarningMessage("{0}", new object[]
						{
							text
						});
						Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_RetentionAgentPotentialDataLossWarning, new object[]
						{
							text
						});
					}
				}
				else
				{
					string text2 = string.Format("RetentionAgent: Data loss occurred. The size of this folder {0} has reached the max size allowed - {1} MB. Some files will be purged.", this.enforcedDirectory, this.maxDirectorySizeInMBytes);
					Logger.LogErrorMessage("{0}", new object[]
					{
						text2
					});
					Logger.LogEvent(MSExchangeDiagnosticsEventLogConstants.Tuple_RetentionAgentDataLossOccurred, new object[]
					{
						text2
					});
				}
			}
			foreach (FileInfo fileInfo2 in hashSet)
			{
				try
				{
					File.Delete(fileInfo2.FullName);
					if (this.logDataLossMessage)
					{
						Logger.LogErrorMessage("RetentionAgent: File deleted: {0}", new object[]
						{
							fileInfo2.FullName
						});
					}
					else
					{
						Logger.LogInformationMessage("RetentionAgent: File deleted: {0}", new object[]
						{
							fileInfo2.FullName
						});
					}
				}
				catch (IOException)
				{
					Logger.LogErrorMessage("RetentionAgent: Failed to delete. Will try again next timer event: {0}", new object[]
					{
						fileInfo2.FullName
					});
				}
			}
		}

		// Token: 0x04000062 RID: 98
		private readonly Timer timer;

		// Token: 0x04000063 RID: 99
		private readonly object lockObject;

		// Token: 0x04000064 RID: 100
		private readonly string enforcedDirectory;

		// Token: 0x04000065 RID: 101
		private readonly TimeSpan retentionPeriod;

		// Token: 0x04000066 RID: 102
		private readonly int maxDirectorySizeInMBytes;

		// Token: 0x04000067 RID: 103
		private readonly bool logDataLossMessage;

		// Token: 0x04000068 RID: 104
		private readonly TimeSpan checkInterval;

		// Token: 0x04000069 RID: 105
		private bool disposed;

		// Token: 0x0200001F RID: 31
		public class PerformanceLogAgent : RetentionAgent
		{
			// Token: 0x06000097 RID: 151 RVA: 0x000075FC File Offset: 0x000057FC
			public PerformanceLogAgent(string enforcedDirectory, TimeSpan retentionPeriod, int maxDirectorySizeMBytes, TimeSpan checkInterval, bool logDataLossMessage) : base(enforcedDirectory, retentionPeriod, maxDirectorySizeMBytes, checkInterval, logDataLossMessage)
			{
				this.performanceLogDirectory = enforcedDirectory;
				this.dailyPerformanceLogDirectory = enforcedDirectory.Replace("PerformanceLogsToBeProcessed", "DailyPerformanceLogs");
				this.relogManager = new RelogManager(enforcedDirectory, TimeSpan.FromMinutes(2.0));
				object value;
				if (CommonUtils.TryGetRegistryValue(DiagnosticsService.DiagnosticsRegistryKey, "PerformanceLogEnabled", null, out value) && !Convert.ToBoolean(value))
				{
					Logger.LogInformationMessage("Performance logging is disabled.", new object[0]);
					return;
				}
				Logger.LogInformationMessage("Starting performance log monitor.", new object[0]);
				string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				string performanceCounterList = Configuration.GetConfigString("PerformanceCounterCollectionList", null);
				if (string.IsNullOrEmpty(performanceCounterList))
				{
					performanceCounterList = Path.Combine(directoryName, "PerformanceCounterConfiguration.xml");
				}
				string dailyPerformanceCounterList = Path.Combine(directoryName, "PerformanceCounterConfigurationDaily.xml");
				if (!File.Exists(performanceCounterList))
				{
					string text = string.Format("The performance counter list file '{0}' does not exist", performanceCounterList);
					Logger.LogErrorMessage("{0}", new object[]
					{
						text
					});
					throw new ApplicationException(text);
				}
				Logger.LogInformationMessage("Adding '{0}' log to the monitor set", new object[]
				{
					performanceCounterList
				});
				if (!File.Exists(dailyPerformanceCounterList))
				{
					string text2 = string.Format("The performance counter list file '{0}' does not exist", dailyPerformanceCounterList);
					Logger.LogErrorMessage("{0}", new object[]
					{
						text2
					});
					throw new ApplicationException(text2);
				}
				TimeSpan configTimeSpan = Configuration.GetConfigTimeSpan("PerformanceLogMonitorInterval", TimeSpan.FromSeconds(30.0), TimeSpan.MaxValue, TimeSpan.FromMinutes(5.0));
				Logger.LogInformationMessage("Adding '{0}' log to the monitor set", new object[]
				{
					dailyPerformanceCounterList
				});
				this.logMonitor = new PerformanceLogMonitor(configTimeSpan, delegate(PerformanceLogMonitor logMonitor)
				{
					logMonitor.AddPerflog(new PerformanceLogSet(performanceCounterList, "ExchangeDiagnosticsPerformanceLog", this.performanceLogDirectory, new PerformanceLogSet.PerformanceLogFormat?(PerformanceLogSet.PerformanceLogFormat.BIN)), null);
					logMonitor.AddPerflog(new PerformanceLogSet(dailyPerformanceCounterList, "ExchangeDiagnosticsDailyPerformanceLog", this.dailyPerformanceLogDirectory, new PerformanceLogSet.PerformanceLogFormat?(PerformanceLogSet.PerformanceLogFormat.BIN), new PerformanceLogSet.FileNameFormatPattern?(PerformanceLogSet.FileNameFormatPattern.MMddHHmm), TimeSpan.FromMinutes(1.0), TimeSpan.FromHours(24.0)), null);
				});
				this.logMonitor.StartMonitor();
				Logger.LogInformationMessage("Started performance log monitor.", new object[0]);
			}

			// Token: 0x06000098 RID: 152 RVA: 0x0000780D File Offset: 0x00005A0D
			protected override void InternalDispose(bool disposing)
			{
				if (disposing)
				{
					if (this.relogManager != null)
					{
						this.relogManager.Dispose();
					}
					if (this.logMonitor != null)
					{
						this.logMonitor.StopMonitor();
					}
				}
				base.InternalDispose(disposing);
			}

			// Token: 0x0400006B RID: 107
			private readonly string performanceLogDirectory;

			// Token: 0x0400006C RID: 108
			private readonly string dailyPerformanceLogDirectory;

			// Token: 0x0400006D RID: 109
			private readonly RelogManager relogManager;

			// Token: 0x0400006E RID: 110
			private readonly PerformanceLogMonitor logMonitor;
		}
	}
}
