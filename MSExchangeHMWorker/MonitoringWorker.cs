using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.ServiceModel.Security;
using System.Threading;
using Microsoft.Exchange.ActiveMonitoring.EventLog;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.ProcessManager;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Exchange.ActiveMonitoring
{
	// Token: 0x02000002 RID: 2
	internal sealed class MonitoringWorker : ControlObject.TransportWorker, IWorkerProcess
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public void Retire()
		{
			WTFLogger.Instance.LogDebug(WTFLog.Core, TracingContext.Default, "Retire command received", "Retire", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Local\\WorkerProcess\\Program.cs", 165);
			MonitoringWorker.eventLogger.LogEvent(MSExchangeHMEventLogConstants.Tuple_HealthManagerWorkerRetiring, null, new object[]
			{
				MonitoringWorker.processId
			});
			this.StopActiveMonitoringWorker();
			this.DisposeCentralEventLogWatcher();
			this.DisposeEventLogNotification();
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002140 File Offset: 0x00000340
		public void Stop()
		{
			WTFLogger.Instance.LogDebug(WTFLog.Core, TracingContext.Default, "Stop command received", "Stop", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Local\\WorkerProcess\\Program.cs", 180);
			WTFLogger.Instance.Flush();
			if (Interlocked.Exchange(ref this.stopCount, 1) > 0)
			{
				WTFLogger.Instance.LogDebug(WTFLog.Core, TracingContext.Default, "Stop has already been called. Ignore this one", "Stop", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Local\\WorkerProcess\\Program.cs", 186);
				return;
			}
			MonitoringWorker.eventLogger.LogEvent(MSExchangeHMEventLogConstants.Tuple_HealthManagerWorkerStopping, null, new object[]
			{
				MonitoringWorker.processId
			});
			this.StopActiveMonitoringWorker();
			this.DisposeCentralEventLogWatcher();
			this.DisposeEventLogNotification();
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000021F0 File Offset: 0x000003F0
		public void Activate()
		{
			WTFLogger.Instance.LogDebug(WTFLog.Core, TracingContext.Default, "Activate command received", "Activate", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Local\\WorkerProcess\\Program.cs", 204);
			WTFLogger.Instance.Flush();
			if (Interlocked.Exchange(ref this.initialized, 1) == 0)
			{
				ExchangeRpcConfiguration exchangeRpcConfiguration = new ExchangeRpcConfiguration();
				exchangeRpcConfiguration.Initialize();
				RecoveryActionHelper.CommunicateWorkerProcessInfoToHostProcess(false);
				WTFLogger.Instance.LogDebug(WTFLog.Core, TracingContext.Default, "Initializing local data access from Crimson channel", "Activate", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Local\\WorkerProcess\\Program.cs", 225);
				LocalDataAccess.Initialize(LocalEndpointManager.Instance.EndpointWorkitems);
			}
			this.activeMonitoringWorker.Start();
			MonitoringWorker.eventLogger.LogEvent(MSExchangeHMEventLogConstants.Tuple_HealthManagerWorkerActivated, null, new object[]
			{
				MonitoringWorker.processId
			});
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000022B8 File Offset: 0x000004B8
		public void Pause()
		{
			WTFLogger.Instance.LogDebug(WTFLog.Core, TracingContext.Default, "Pause command received", "Pause", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Local\\WorkerProcess\\Program.cs", 239);
			this.activeMonitoringWorker.Stop();
			try
			{
				this.activeMonitoringWorker.Wait();
			}
			catch (AggregateException ex)
			{
				MonitoringWorker.eventLogger.LogEvent(MSExchangeHMEventLogConstants.Tuple_WorkerExitWithException, null, new object[]
				{
					MonitoringWorker.processId,
					ex.ToString()
				});
			}
			MonitoringWorker.eventLogger.LogEvent(MSExchangeHMEventLogConstants.Tuple_HealthManagerWorkerPaused, null, new object[]
			{
				MonitoringWorker.processId
			});
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002370 File Offset: 0x00000570
		public void Continue()
		{
			WTFLogger.Instance.LogDebug(WTFLog.Core, TracingContext.Default, "Continue command received", "Continue", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Local\\WorkerProcess\\Program.cs", 263);
			this.activeMonitoringWorker.Start();
			MonitoringWorker.eventLogger.LogEvent(MSExchangeHMEventLogConstants.Tuple_HealthManagerWorkerResumed, null, new object[]
			{
				MonitoringWorker.processId
			});
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000023D6 File Offset: 0x000005D6
		public void ConfigUpdate()
		{
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000023D8 File Offset: 0x000005D8
		public void HandleMemoryPressure()
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000023DA File Offset: 0x000005DA
		public void HandleConnection(Socket clientConnection)
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000023DC File Offset: 0x000005DC
		public void ClearConfigCache()
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000023DE File Offset: 0x000005DE
		public void HandleBlockedSubmissionQueue()
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000023E0 File Offset: 0x000005E0
		public void HandleLogFlush()
		{
			WTFLogger.Instance.Flush();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023EC File Offset: 0x000005EC
		public void HandleForceCrash()
		{
			WTFLogger.Instance.Flush();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000023F8 File Offset: 0x000005F8
		internal static void Main(string[] args)
		{
			ExWatson.Register();
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				MonitoringWorker.processId = currentProcess.Id;
			}
			MonitoringWorker.worker.Run(args);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002444 File Offset: 0x00000644
		private static void PrintUsageAndExit()
		{
			WTFLogger.Instance.Flush();
			Console.WriteLine(Strings.UsageText(Assembly.GetExecutingAssembly().GetName().Name));
			Environment.Exit(0);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002474 File Offset: 0x00000674
		private static Semaphore OpenSemaphore(string name, string semaphoreLabel)
		{
			Semaphore semaphore = null;
			if (!string.IsNullOrEmpty(name))
			{
				try
				{
					semaphore = Semaphore.OpenExisting(name);
				}
				catch (WaitHandleCannotBeOpenedException)
				{
				}
				catch (UnauthorizedAccessException)
				{
				}
				if (semaphore == null)
				{
					MonitoringWorker.eventLogger.LogEvent(MSExchangeHMEventLogConstants.Tuple_OpenSemaphoreFailed, null, new object[]
					{
						MonitoringWorker.processId
					});
					Environment.Exit(1);
				}
			}
			return semaphore;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000024E8 File Offset: 0x000006E8
		private void Run(string[] args)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			string text = null;
			string name = null;
			string name2 = null;
			string s = null;
			foreach (string text2 in args)
			{
				if (text2.StartsWith("-?", StringComparison.OrdinalIgnoreCase))
				{
					MonitoringWorker.PrintUsageAndExit();
				}
				else if (text2.StartsWith("-console", StringComparison.OrdinalIgnoreCase))
				{
					flag3 = true;
				}
				else if (text2.StartsWith("-stopkey:", StringComparison.OrdinalIgnoreCase))
				{
					text = text2.Remove(0, "-stopkey:".Length);
				}
				else if (text2.StartsWith("-hangkey:", StringComparison.OrdinalIgnoreCase))
				{
					name = text2.Remove(0, "-hangkey:".Length);
				}
				else if (text2.StartsWith("-resetkey:", StringComparison.OrdinalIgnoreCase))
				{
					text2.Remove(0, "-resetkey:".Length);
				}
				else if (text2.StartsWith("-readykey:", StringComparison.OrdinalIgnoreCase))
				{
					name2 = text2.Remove(0, "-readykey:".Length);
				}
				else if (text2.StartsWith("-pipe:", StringComparison.OrdinalIgnoreCase))
				{
					s = text2.Remove(0, "-pipe:".Length);
				}
				else if (text2.StartsWith("-paused", StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
				}
				else if (text2.StartsWith("-passive", StringComparison.OrdinalIgnoreCase))
				{
					flag2 = true;
				}
				else if (text2.StartsWith("-wait", StringComparison.OrdinalIgnoreCase))
				{
					flag4 = true;
				}
			}
			this.serviceControlled = !string.IsNullOrEmpty(text);
			if (!this.serviceControlled)
			{
				if (!flag3)
				{
					MonitoringWorker.PrintUsageAndExit();
				}
				Console.WriteLine("Starting {0}, running in console mode.", Assembly.GetExecutingAssembly().GetName().Name);
				if (flag4)
				{
					Console.WriteLine("Press ENTER to continue.");
					Console.ReadLine();
				}
			}
			this.stopHandle = MonitoringWorker.OpenSemaphore(text, "stop");
			this.hangHandle = MonitoringWorker.OpenSemaphore(name, "hang");
			this.readyHandle = MonitoringWorker.OpenSemaphore(name2, "ready");
			if (this.hangHandle != null)
			{
				new Thread(new ThreadStart(this.WaitForHangSignal))
				{
					IsBackground = true
				}.Start();
			}
			if (CriticalDependencyAlertEscalator.RunningInMicrosoftDatacenter())
			{
				bool flag5 = true;
				string name3 = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\CriticalDependencyVerification\\";
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name3))
				{
					if (registryKey != null)
					{
						object value = registryKey.GetValue("Enabled");
						if (value != null && value is int && (int)value == 0)
						{
							flag5 = false;
						}
					}
				}
				if (flag5)
				{
					WTFLogger.Instance.LogDebug(WTFLog.Core, TracingContext.Default, "Attempting to verify critical dependencies before continuing with initialization.", "Run", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Local\\WorkerProcess\\Program.cs", 543);
					CriticalDependencyVerification criticalDependencyVerification = new CriticalDependencyVerification(ExTraceGlobals.CommonComponentsTracer, TracingContext.Default);
					if (!criticalDependencyVerification.VerifyDependencies())
					{
						WTFLogger.Instance.LogError(WTFLog.Core, TracingContext.Default, "Failed to verify one or more critical dependencies. We will try to continue running, but may hang, crash, or exit unexpectedly.", "Run", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Local\\WorkerProcess\\Program.cs", 551);
					}
					else
					{
						WTFLogger.Instance.LogDebug(WTFLog.Core, TracingContext.Default, "Successfully verified all critical dependencies. Continuing with initialization.", "Run", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Local\\WorkerProcess\\Program.cs", 555);
					}
				}
			}
			if (!ServiceTopologyProvider.IsAdTopologyServiceInstalled())
			{
				ADSession.SetAdminTopologyMode();
			}
			Globals.InitializeMultiPerfCounterInstance("ExHMWorker");
			LocalEndpointManager.UseMaintenanceWorkItem = true;
			WTFLogger.Instance.LogDebug(WTFLog.Core, TracingContext.Default, "Initializing global override lists", "Run", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Local\\WorkerProcess\\Program.cs", 573);
			try
			{
				DirectoryAccessor.Instance.LoadGlobalOverrides();
			}
			catch (Exception arg)
			{
				ExTraceGlobals.WorkerTracer.TraceError<Exception>(0L, "Initializing global override lists failed with exception {0}", arg);
			}
			WTFLogger.Instance.LogDebug(WTFLog.Core, TracingContext.Default, "Initializing local override lists", "Run", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Local\\WorkerProcess\\Program.cs", 586);
			LocalOverrideManager.LoadLocalOverrides();
			Settings.RemoveAllOverrides();
			if (MaintenanceDefinition.GlobalOverrides != null)
			{
				foreach (WorkDefinitionOverride o in MaintenanceDefinition.GlobalOverrides)
				{
					this.ApplyConfigurationOverrideIfNecessary(o);
				}
			}
			if (MaintenanceDefinition.LocalOverrides != null)
			{
				foreach (WorkDefinitionOverride o2 in MaintenanceDefinition.LocalOverrides)
				{
					this.ApplyConfigurationOverrideIfNecessary(o2);
				}
			}
			Assembly assembly = typeof(LocalEndpointManager).Assembly;
			WorkItemFactory.DefaultAssemblies["Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.Components.dll"] = assembly;
			WorkItemFactory.DefaultAssemblies[assembly.Location] = assembly;
			WorkItemFactory factory = new WorkItemFactory();
			bool flag6 = true;
			bool flag7 = true;
			try
			{
				flag6 = ServerComponentStateManager.IsOnline(ServerComponentEnum.Monitoring);
			}
			catch (Exception ex)
			{
				MonitoringWorker.eventLogger.LogEvent(MSExchangeHMEventLogConstants.Tuple_FailedToGetIsOnlineState, null, new object[]
				{
					MonitoringWorker.processId,
					ServerComponentEnum.Monitoring.ToString(),
					flag6.ToString(),
					ex.ToString()
				});
			}
			try
			{
				flag7 = ServerComponentStateManager.IsOnline(ServerComponentEnum.RecoveryActionsEnabled);
			}
			catch (Exception ex2)
			{
				MonitoringWorker.eventLogger.LogEvent(MSExchangeHMEventLogConstants.Tuple_FailedToGetIsOnlineState, null, new object[]
				{
					MonitoringWorker.processId,
					ServerComponentEnum.RecoveryActionsEnabled.ToString(),
					flag7.ToString(),
					ex2.ToString()
				});
			}
			if (flag6 && flag7)
			{
				this.activeMonitoringWorker = new Worker(new WorkBroker[]
				{
					new ProbeWorkBroker<LocalDataAccess>(factory),
					new MonitorWorkBroker<LocalDataAccess>(factory),
					new ResponderWorkBroker<LocalDataAccess>(factory),
					new MaintenanceWorkBroker<LocalDataAccess>(factory)
				}, new Action(this.OnExitCallback), true);
				MonitoringWorker.eventLogger.LogEvent(MSExchangeHMEventLogConstants.Tuple_HealthManagerWorkerStarted, null, new object[]
				{
					MonitoringWorker.processId,
					Strings.StartedWithAllWorkBrokers(flag6, flag7)
				});
			}
			else if (flag6 && !flag7)
			{
				this.activeMonitoringWorker = new Worker(new WorkBroker[]
				{
					new ProbeWorkBroker<LocalDataAccess>(factory),
					new MonitorWorkBroker<LocalDataAccess>(factory),
					new MaintenanceWorkBroker<LocalDataAccess>(factory)
				}, new Action(this.OnExitCallback), true);
				MonitoringWorker.eventLogger.LogEvent(MSExchangeHMEventLogConstants.Tuple_HealthManagerWorkerStarted, null, new object[]
				{
					MonitoringWorker.processId,
					Strings.StartedWithAllWorkBrokersExceptResponder(flag6, flag7)
				});
			}
			else
			{
				this.activeMonitoringWorker = new Worker(new WorkBroker[]
				{
					new MaintenanceWorkBroker<LocalDataAccess>(factory)
				}, new Action(this.OnExitCallback), true);
				MonitoringWorker.eventLogger.LogEvent(MSExchangeHMEventLogConstants.Tuple_HealthManagerWorkerStarted, null, new object[]
				{
					MonitoringWorker.processId,
					Strings.StartedWithMaintenanceWorkBrokerOnly(flag6, flag7)
				});
			}
			try
			{
				if (this.serviceControlled)
				{
					SafeFileHandle handle = new SafeFileHandle(new IntPtr(long.Parse(s)), true);
					this.listenPipeStream = new PipeStream(handle, FileAccess.Read, true);
					this.controlObject = new ControlObject(this.listenPipeStream, this);
					if (this.controlObject.Initialize())
					{
						if (this.readyHandle != null)
						{
							WTFLogger.Instance.LogDebug(WTFLog.Core, TracingContext.Default, "Signal the process is ready", "Run", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Local\\WorkerProcess\\Program.cs", 726);
							this.readyHandle.Release();
							this.readyHandle.Close();
							this.readyHandle = null;
						}
						if (!flag && !flag2)
						{
							WTFLogger.Instance.LogDebug(WTFLog.Core, TracingContext.Default, "Start processing work items", "Run", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Local\\WorkerProcess\\Program.cs", 736);
							this.Activate();
						}
						WTFLogger.Instance.LogDebug(WTFLog.Core, TracingContext.Default, "Waiting for shutdown signal to exit.", "Run", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Local\\WorkerProcess\\Program.cs", 742);
						this.stopHandle.WaitOne();
					}
				}
				else
				{
					if (!flag)
					{
						Console.WriteLine("Start processing work items");
						this.Activate();
					}
					Console.WriteLine("Press ENTER to exit ");
					bool flag8 = false;
					while (!flag8)
					{
						string text3 = Console.ReadLine();
						if (string.IsNullOrEmpty(text3))
						{
							Console.WriteLine("Exiting.");
							flag8 = true;
						}
						else if (text3.Equals("p"))
						{
							Console.WriteLine("Pause.");
							this.Pause();
						}
						else if (text3.Equals("c"))
						{
							Console.WriteLine("Continue.");
							this.Continue();
						}
						else if (text3.Equals("s"))
						{
							Console.WriteLine("Stop.");
							this.Stop();
						}
						else
						{
							Console.WriteLine("Unknown command.");
						}
					}
				}
			}
			catch (Exception ex3)
			{
				MonitoringWorker.eventLogger.LogEvent(MSExchangeHMEventLogConstants.Tuple_WorkerRestartOnUnknown, null, new object[]
				{
					MonitoringWorker.processId,
					ex3.ToString()
				});
				if (!this.serviceControlled && ((ex3 is Win32Exception && ((Win32Exception)ex3).NativeErrorCode == 5) || ex3 is UnauthorizedAccessException || ex3 is SecurityAccessDeniedException))
				{
					Console.WriteLine("You must run the worker process with elevated privileges");
				}
			}
			WTFLogger.Instance.LogDebug(WTFLog.Core, TracingContext.Default, "Received a signal to shut down.  Closing control pipe.", "Run", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Local\\WorkerProcess\\Program.cs", 814);
			this.DoCleanUp();
			this.SavePersistentState();
			WTFLogger.Instance.LogDebug(WTFLog.Core, TracingContext.Default, "Monitoring worker process stopped.", "Run", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Local\\WorkerProcess\\Program.cs", 820);
			WTFLogger.Instance.Flush();
			MonitoringWorker.eventLogger.LogEvent(MSExchangeHMEventLogConstants.Tuple_HealthManagerWorkerStopped, null, new object[]
			{
				MonitoringWorker.processId
			});
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002F08 File Offset: 0x00001108
		private void ApplyConfigurationOverrideIfNecessary(WorkDefinitionOverride o)
		{
			if (string.Compare(o.ServiceName, ExchangeComponent.Monitoring.Name, true, CultureInfo.InvariantCulture) == 0 && string.Compare(o.WorkDefinitionName, "Configuration", true, CultureInfo.InvariantCulture) == 0 && !string.IsNullOrWhiteSpace(o.PropertyName) && o.NewPropertyValue != null)
			{
				Settings.ApplyOverride(o.PropertyName, o.NewPropertyValue);
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002F70 File Offset: 0x00001170
		private void OnExitCallback()
		{
			if (this.activeMonitoringWorker.RestartRequest == null)
			{
				MonitoringWorker.eventLogger.LogEvent(MSExchangeHMEventLogConstants.Tuple_WorkerExitGracefully, null, new object[]
				{
					MonitoringWorker.processId
				});
				return;
			}
			Exception exception = this.activeMonitoringWorker.RestartRequest.Exception;
			switch (this.activeMonitoringWorker.RestartRequest.Reason)
			{
			case RestartRequestReason.DataAccessError:
				MonitoringWorker.eventLogger.LogEvent(MSExchangeHMEventLogConstants.Tuple_WorkerRestartOnDataAccessError, null, new object[]
				{
					MonitoringWorker.processId,
					(exception == null) ? string.Empty : exception.ToString()
				});
				break;
			case RestartRequestReason.PoisonResult:
				MonitoringWorker.eventLogger.LogEvent(MSExchangeHMEventLogConstants.Tuple_WorkerRestartOnPoisonResult, null, new object[]
				{
					MonitoringWorker.processId,
					this.activeMonitoringWorker.RestartRequest.ResultName,
					this.activeMonitoringWorker.RestartRequest.Timestamp,
					this.activeMonitoringWorker.RestartRequest.ResultId
				});
				break;
			case RestartRequestReason.Maintenance:
				MonitoringWorker.eventLogger.LogEvent(MSExchangeHMEventLogConstants.Tuple_WorkerRestartOnMaintenance, null, new object[]
				{
					MonitoringWorker.processId,
					this.activeMonitoringWorker.RestartRequest.ResultName,
					this.activeMonitoringWorker.RestartRequest.Timestamp
				});
				break;
			case RestartRequestReason.Unknown:
				MonitoringWorker.eventLogger.LogEvent(MSExchangeHMEventLogConstants.Tuple_WorkerRestartOnUnknown, null, new object[]
				{
					MonitoringWorker.processId,
					(exception == null) ? string.Empty : exception.ToString()
				});
				break;
			}
			this.Stop();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000313B File Offset: 0x0000133B
		private void WaitForHangSignal()
		{
			this.hangHandle.WaitOne();
			Environment.Exit(1);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00003150 File Offset: 0x00001350
		private void TryReleaseStopHandle()
		{
			if (this.serviceControlled)
			{
				try
				{
					this.stopHandle.Release();
					return;
				}
				catch (SemaphoreFullException)
				{
					WTFLogger.Instance.LogDebug(WTFLog.Core, TracingContext.Default, "StopHandle already signaled. The process is already stopping", "TryReleaseStopHandle", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Local\\WorkerProcess\\Program.cs", 916);
					return;
				}
			}
			WTFLogger.Instance.LogDebug(WTFLog.Core, TracingContext.Default, "TryReleaseStopHandle() got called but we have no StopHandle to signal. This can happen when the worker runs independently of the HM service, such as in a console or debugger session.", "TryReleaseStopHandle", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Local\\WorkerProcess\\Program.cs", 921);
			Console.WriteLine("The Worker Task Framework would like to restart the worker process. Please press ENTER to shut down this worker.");
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000031E0 File Offset: 0x000013E0
		private void DoCleanUp()
		{
			PipeStream pipeStream = this.listenPipeStream;
			this.listenPipeStream = null;
			if (pipeStream != null)
			{
				try
				{
					pipeStream.Close();
				}
				catch (IOException)
				{
				}
				catch (ObjectDisposedException)
				{
				}
				catch (OperationCanceledException)
				{
				}
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00003238 File Offset: 0x00001438
		private void SavePersistentState()
		{
			try
			{
				WTFLogger.Instance.LogDebug(WTFLog.Core, TracingContext.Default, "Write PersistentState", "SavePersistentState", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Local\\WorkerProcess\\Program.cs", 957);
				LocalDataAccess.WriteAllPersistentResults(CancellationToken.None);
				WTFLogger.Instance.LogDebug(WTFLog.Core, TracingContext.Default, "Succeed to write PersistentState", "SavePersistentState", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Local\\WorkerProcess\\Program.cs", 961);
			}
			catch (Exception exception)
			{
				WTFLogger.Instance.LogException(WTFLog.Core, TracingContext.Default, exception, "SavePersistentState", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Local\\WorkerProcess\\Program.cs", 965);
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000032D8 File Offset: 0x000014D8
		private void StopActiveMonitoringWorker()
		{
			WTFLogger.Instance.Flush();
			this.activeMonitoringWorker.Stop();
			try
			{
				this.activeMonitoringWorker.Wait();
			}
			catch (AggregateException ex)
			{
				MonitoringWorker.eventLogger.LogEvent(MSExchangeHMEventLogConstants.Tuple_WorkerExitWithException, null, new object[]
				{
					MonitoringWorker.processId,
					ex.ToString()
				});
			}
			finally
			{
				this.TryReleaseStopHandle();
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00003360 File Offset: 0x00001560
		private void DisposeCentralEventLogWatcher()
		{
			try
			{
				CentralEventLogWatcher.Instance.Dispose();
			}
			catch (Exception ex)
			{
				MonitoringWorker.eventLogger.LogEvent(MSExchangeHMEventLogConstants.Tuple_WorkerExitWithException, null, new object[]
				{
					MonitoringWorker.processId,
					ex.ToString()
				});
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000033BC File Offset: 0x000015BC
		private void DisposeEventLogNotification()
		{
			try
			{
				EventLogNotification.Instance.Dispose();
			}
			catch (Exception ex)
			{
				MonitoringWorker.eventLogger.LogEvent(MSExchangeHMEventLogConstants.Tuple_WorkerExitWithException, null, new object[]
				{
					MonitoringWorker.processId,
					ex.ToString()
				});
			}
		}

		// Token: 0x04000001 RID: 1
		private const string HelpOption = "-?";

		// Token: 0x04000002 RID: 2
		private const string ConsoleOption = "-console";

		// Token: 0x04000003 RID: 3
		private const string WaitToContinueOption = "-wait";

		// Token: 0x04000004 RID: 4
		private const string StopKeyOption = "-stopkey:";

		// Token: 0x04000005 RID: 5
		private const string HangKeyOption = "-hangkey:";

		// Token: 0x04000006 RID: 6
		private const string ResetKeyOption = "-resetkey:";

		// Token: 0x04000007 RID: 7
		private const string ReadyKeyOption = "-readykey:";

		// Token: 0x04000008 RID: 8
		private const string PipeOption = "-pipe:";

		// Token: 0x04000009 RID: 9
		private const string PausedOption = "-paused";

		// Token: 0x0400000A RID: 10
		private const string PassiveOption = "-passive";

		// Token: 0x0400000B RID: 11
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.WorkerTracer.Category, "MSExchangeHM");

		// Token: 0x0400000C RID: 12
		private static MonitoringWorker worker = new MonitoringWorker();

		// Token: 0x0400000D RID: 13
		private static int processId;

		// Token: 0x0400000E RID: 14
		private Semaphore stopHandle;

		// Token: 0x0400000F RID: 15
		private Semaphore hangHandle;

		// Token: 0x04000010 RID: 16
		private Semaphore readyHandle;

		// Token: 0x04000011 RID: 17
		private ControlObject controlObject;

		// Token: 0x04000012 RID: 18
		private PipeStream listenPipeStream;

		// Token: 0x04000013 RID: 19
		private Worker activeMonitoringWorker;

		// Token: 0x04000014 RID: 20
		private int stopCount;

		// Token: 0x04000015 RID: 21
		private int initialized;

		// Token: 0x04000016 RID: 22
		private bool serviceControlled;
	}
}
