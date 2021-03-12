using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Common
{
	// Token: 0x02000042 RID: 66
	public abstract class ExServiceBase : ServiceBase
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00007784 File Offset: 0x00005984
		protected virtual TimeSpan StartTimeout
		{
			get
			{
				return ExServiceBase.DefaultTimeout;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000778B File Offset: 0x0000598B
		protected virtual TimeSpan StopTimeout
		{
			get
			{
				return ExServiceBase.DefaultTimeout;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00007792 File Offset: 0x00005992
		protected virtual TimeSpan PauseTimeout
		{
			get
			{
				return ExServiceBase.DefaultTimeout;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00007799 File Offset: 0x00005999
		protected virtual TimeSpan ContinueTimeout
		{
			get
			{
				return ExServiceBase.DefaultTimeout;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000077A0 File Offset: 0x000059A0
		protected virtual TimeSpan CustomCommandTimeout
		{
			get
			{
				return ExServiceBase.DefaultTimeout;
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000077A7 File Offset: 0x000059A7
		public static void RunAsConsole(ExServiceBase service)
		{
			Console.WriteLine("Starting...");
			service.OnStart(null);
			Console.WriteLine("Started. Type ENTER to stop.");
			Console.ReadLine();
			Console.WriteLine("Stopping...");
			service.OnStop();
			Console.WriteLine("Stopped");
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000077E4 File Offset: 0x000059E4
		public void ExRequestAdditionalTime(int milliseconds)
		{
			if (!Environment.UserInteractive)
			{
				base.RequestAdditionalTime(milliseconds);
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000077F4 File Offset: 0x000059F4
		protected static string GetProcessesInfo()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("Processes running are: ");
			try
			{
				Process[] processes = Process.GetProcesses();
				if (processes != null)
				{
					foreach (Process process in processes)
					{
						stringBuilder.AppendLine(process.Id + " " + process.ProcessName);
					}
				}
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine("An Exception occurred when getting running processes: " + ex.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000078F0 File Offset: 0x00005AF0
		protected sealed override void OnStart(string[] args)
		{
			this.SendWatsonReportOnTimeout((string message) => new ExServiceBase.ServiceStartTimeoutException(message), this.StartTimeout, delegate
			{
				ExWatson.SendReportOnUnhandledException(delegate()
				{
					try
					{
						this.OnStartInternal(args);
					}
					catch (ExServiceBase.GracefulServiceStartupFailureException)
					{
						this.Stop();
					}
				});
			});
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00007969 File Offset: 0x00005B69
		protected sealed override void OnStop()
		{
			this.SendWatsonReportOnTimeout((string message) => new ExServiceBase.ServiceStopTimeoutException(message), this.StopTimeout, delegate
			{
				ExWatson.SendReportOnUnhandledException(delegate()
				{
					this.OnStopInternal();
				});
			});
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000079C3 File Offset: 0x00005BC3
		protected sealed override void OnShutdown()
		{
			this.SendWatsonReportOnTimeout((string message) => new ExServiceBase.ServiceShutdownTimeoutException(message), this.StopTimeout, delegate
			{
				ExWatson.SendReportOnUnhandledException(delegate()
				{
					this.OnShutdownInternal();
				});
			});
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00007A1D File Offset: 0x00005C1D
		protected sealed override void OnPause()
		{
			this.SendWatsonReportOnTimeout((string message) => new ExServiceBase.ServicePauseTimeoutException(message), this.PauseTimeout, delegate
			{
				ExWatson.SendReportOnUnhandledException(delegate()
				{
					this.OnPauseInternal();
				});
			});
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00007A77 File Offset: 0x00005C77
		protected sealed override void OnContinue()
		{
			this.SendWatsonReportOnTimeout((string message) => new ExServiceBase.ServiceContinueTimeoutException(message), this.ContinueTimeout, delegate
			{
				ExWatson.SendReportOnUnhandledException(delegate()
				{
					this.OnContinueInternal();
				});
			});
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00007AE4 File Offset: 0x00005CE4
		protected sealed override void OnCustomCommand(int command)
		{
			this.SendWatsonReportOnTimeout((string message) => new ExServiceBase.ServiceCustomCommandTimeoutException(message), this.CustomCommandTimeout, delegate
			{
				ExWatson.SendReportOnUnhandledException(delegate()
				{
					this.OnCustomCommandInternal(command);
				});
			});
		}

		// Token: 0x06000162 RID: 354
		protected abstract void OnStartInternal(string[] args);

		// Token: 0x06000163 RID: 355
		protected abstract void OnStopInternal();

		// Token: 0x06000164 RID: 356 RVA: 0x00007B3A File Offset: 0x00005D3A
		protected virtual void OnShutdownInternal()
		{
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00007B3C File Offset: 0x00005D3C
		protected virtual void OnPauseInternal()
		{
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00007B3E File Offset: 0x00005D3E
		protected virtual void OnContinueInternal()
		{
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00007B40 File Offset: 0x00005D40
		protected virtual void OnCommandTimeout()
		{
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00007B42 File Offset: 0x00005D42
		protected virtual void OnCustomCommandInternal(int command)
		{
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00007B44 File Offset: 0x00005D44
		protected void GracefullyAbortStartup()
		{
			throw new ExServiceBase.GracefulServiceStartupFailureException();
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00007B4B File Offset: 0x00005D4B
		protected void LogExWatsonTimeoutServiceStateChangeInfo(string info)
		{
			if (this.serviceStateChanges == null)
			{
				this.serviceStateChanges = new StringBuilder();
			}
			this.serviceStateChanges.AppendFormat("{0} : {1}", DateTime.UtcNow, info);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00007B7C File Offset: 0x00005D7C
		private static bool IsSendEventLogsWithWatsonReportEnabled()
		{
			bool result;
			try
			{
				NameValueCollection appSettings = ConfigurationManager.AppSettings;
				string value = appSettings["SendEventLogsWithWatsonReport"];
				bool flag;
				if (string.IsNullOrEmpty(value))
				{
					result = false;
				}
				else if (bool.TryParse(value, out flag) && flag)
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch (ConfigurationErrorsException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00007BD4 File Offset: 0x00005DD4
		private static string GetLastEventLogEntries(TimeSpan timeSpan, int maxEntries)
		{
			DateTime t = DateTime.UtcNow - timeSpan;
			StringBuilder stringBuilder = new StringBuilder(5000);
			try
			{
				using (EventLog eventLog = new EventLog("Application"))
				{
					int num = 0;
					int i = eventLog.Entries.Count - 1;
					while (i >= 0)
					{
						using (EventLogEntry eventLogEntry = eventLog.Entries[i])
						{
							stringBuilder.AppendLine(string.Format("{0}: {1}: {2}: {3}", new object[]
							{
								eventLogEntry.TimeGenerated,
								eventLogEntry.EntryType,
								eventLogEntry.Source,
								eventLogEntry.Message
							}));
							if (eventLogEntry.TimeGenerated.ToUniversalTime() < t || num > maxEntries)
							{
								break;
							}
						}
						i--;
						num++;
					}
				}
			}
			catch (Exception ex)
			{
				stringBuilder.AppendLine(ex.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00007D38 File Offset: 0x00005F38
		private void SendWatsonReportOnTimeout(Func<string, ExServiceBase.ServiceTimeoutException> newException, TimeSpan timeout, ExServiceBase.UnderTimeoutDelegate underTimeoutDelegate)
		{
			string message = string.Concat(new object[]
			{
				"Started on thread ",
				Environment.CurrentManagedThreadId,
				" at UTC time: ",
				DateTime.UtcNow.ToString(),
				", but did not complete in alloted time of ",
				timeout.ToString()
			});
			Func<string, ExServiceBase.ServiceTimeoutException> newException2 = (string additionalInfo) => newException(message + additionalInfo);
			Task task = Task.Run(delegate()
			{
				underTimeoutDelegate();
			});
			try
			{
				task.Wait(timeout);
			}
			catch (AggregateException ex)
			{
				throw ex.InnerException;
			}
			if (!task.IsCompleted)
			{
				this.TimeoutHandler(newException2);
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00007E10 File Offset: 0x00006010
		private void TimeoutHandler(Func<string, ExServiceBase.ServiceTimeoutException> newException)
		{
			if (newException == null)
			{
				throw new ArgumentNullException("newException");
			}
			if (!Debugger.IsAttached)
			{
				this.OnCommandTimeout();
				ExServiceBase.ServiceTimeoutException exception = newException((this.serviceStateChanges != null) ? (Environment.NewLine + this.serviceStateChanges.ToString()) : string.Empty);
				if (ExServiceBase.IsSendEventLogsWithWatsonReportEnabled())
				{
					string lastEventLogEntries = ExServiceBase.GetLastEventLogEntries(TimeSpan.FromMinutes(10.0), 5000);
					ExWatson.SendReport(exception, ReportOptions.ReportTerminateAfterSend, lastEventLogEntries);
					return;
				}
				ExWatson.SendReport(exception);
			}
		}

		// Token: 0x0400016A RID: 362
		private const string SendEventLogsWithWatsonReport = "SendEventLogsWithWatsonReport";

		// Token: 0x0400016B RID: 363
		private static readonly TimeSpan DefaultTimeout = TimeSpan.FromMinutes(5.0);

		// Token: 0x0400016C RID: 364
		private StringBuilder serviceStateChanges;

		// Token: 0x02000043 RID: 67
		// (Invoke) Token: 0x06000180 RID: 384
		private delegate void UnderTimeoutDelegate();

		// Token: 0x02000044 RID: 68
		private abstract class ServiceTimeoutException : Exception
		{
			// Token: 0x06000183 RID: 387 RVA: 0x00007EAF File Offset: 0x000060AF
			public ServiceTimeoutException(string message) : base(message)
			{
			}
		}

		// Token: 0x02000045 RID: 69
		private sealed class ServiceStartTimeoutException : ExServiceBase.ServiceTimeoutException
		{
			// Token: 0x06000184 RID: 388 RVA: 0x00007EB8 File Offset: 0x000060B8
			public ServiceStartTimeoutException(string message) : base(message)
			{
			}
		}

		// Token: 0x02000046 RID: 70
		private sealed class ServiceStopTimeoutException : ExServiceBase.ServiceTimeoutException
		{
			// Token: 0x06000185 RID: 389 RVA: 0x00007EC1 File Offset: 0x000060C1
			public ServiceStopTimeoutException(string message) : base(message)
			{
			}
		}

		// Token: 0x02000047 RID: 71
		private sealed class ServiceShutdownTimeoutException : ExServiceBase.ServiceTimeoutException
		{
			// Token: 0x06000186 RID: 390 RVA: 0x00007ECA File Offset: 0x000060CA
			public ServiceShutdownTimeoutException(string message) : base(message)
			{
			}
		}

		// Token: 0x02000048 RID: 72
		private sealed class ServicePauseTimeoutException : ExServiceBase.ServiceTimeoutException
		{
			// Token: 0x06000187 RID: 391 RVA: 0x00007ED3 File Offset: 0x000060D3
			public ServicePauseTimeoutException(string message) : base(message)
			{
			}
		}

		// Token: 0x02000049 RID: 73
		private sealed class ServiceContinueTimeoutException : ExServiceBase.ServiceTimeoutException
		{
			// Token: 0x06000188 RID: 392 RVA: 0x00007EDC File Offset: 0x000060DC
			public ServiceContinueTimeoutException(string message) : base(message)
			{
			}
		}

		// Token: 0x0200004A RID: 74
		private sealed class ServiceCustomCommandTimeoutException : ExServiceBase.ServiceTimeoutException
		{
			// Token: 0x06000189 RID: 393 RVA: 0x00007EE5 File Offset: 0x000060E5
			public ServiceCustomCommandTimeoutException(string message) : base(message)
			{
			}
		}

		// Token: 0x0200004B RID: 75
		private class GracefulServiceStartupFailureException : Exception
		{
		}
	}
}
