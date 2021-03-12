using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.ProcessManager
{
	// Token: 0x0200080E RID: 2062
	internal abstract class ProcessManagerService : ExServiceBase, IDisposeTrackable, IDisposable
	{
		// Token: 0x06002B44 RID: 11076 RVA: 0x0005ED10 File Offset: 0x0005CF10
		public ProcessManagerService(string serviceName, string workerProcessPathName, string jobObjectName, bool canHandleConnectionsIfPassive, int workerProcessExitTimeout, bool runningAsService, Microsoft.Exchange.Diagnostics.Trace tracer, ExEventLog eventLogger)
		{
			ProcessManagerService.DropBreadCrumb(ProcessManagerService.ProcessManagerBreadcrumbs.InstanceCreated, ExDateTime.UtcNow);
			this.diagnostics = tracer;
			this.eventLogger = eventLogger;
			this.runningAsService = runningAsService;
			ProcessManagerService processManagerService = Interlocked.CompareExchange<ProcessManagerService>(ref ProcessManagerService.instance, this, null);
			if (processManagerService != null)
			{
				throw new InvalidOperationException("ProcessManagerService is a singleton.");
			}
			base.ServiceName = serviceName;
			base.CanStop = true;
			base.CanShutdown = true;
			base.AutoLog = false;
			this.workerProcessPathName = workerProcessPathName;
			this.jobObjectName = jobObjectName;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06002B45 RID: 11077
		public abstract bool CanHandleConnectionsIfPassive { get; }

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06002B46 RID: 11078 RVA: 0x0005EDA6 File Offset: 0x0005CFA6
		public virtual int MaxWorkerProcessExitTimeoutDefault
		{
			get
			{
				return 90;
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06002B47 RID: 11079 RVA: 0x0005EDAA File Offset: 0x0005CFAA
		public virtual int MaxWorkerProcessDumpTimeoutDefault
		{
			get
			{
				return 900;
			}
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06002B48 RID: 11080 RVA: 0x0005EDB1 File Offset: 0x0005CFB1
		internal ServiceConfiguration ServiceConfiguration
		{
			get
			{
				return this.serviceConfiguration;
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06002B49 RID: 11081 RVA: 0x0005EDB9 File Offset: 0x0005CFB9
		// (set) Token: 0x06002B4A RID: 11082 RVA: 0x0005EDC1 File Offset: 0x0005CFC1
		internal int MaxConnectionRate
		{
			get
			{
				return this.maxConnectionRate;
			}
			set
			{
				this.maxConnectionRate = value;
				if (this.tcpListener != null)
				{
					this.tcpListener.MaxConnectionRate = value;
				}
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06002B4B RID: 11083 RVA: 0x0005EDDE File Offset: 0x0005CFDE
		internal int StopListenerAndWorkerCalled
		{
			get
			{
				return this.stopListenerAndWorkerCalled;
			}
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x0005EDE8 File Offset: 0x0005CFE8
		public static bool StopService(bool canRetry, bool retryAlways, bool retryImmediately, WorkerInstance workerInstance)
		{
			ProcessManagerService.DropBreadCrumb(ProcessManagerService.ProcessManagerBreadcrumbs.StopService, canRetry ? 1 : 0);
			ProcessManagerService processManagerService = ProcessManagerService.instance;
			if (canRetry && processManagerService.startState == StartState.Started)
			{
				if (retryAlways || processManagerService.retryAttempts < processManagerService.serviceConfiguration.MaxProcessManagerRestartAttempts)
				{
					TimeSpan timeSpan = retryImmediately ? ProcessManagerService.ImmediateRestartWorkerProcessInterval : ProcessManagerService.RestartWorkerProcessInterval;
					processManagerService.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_WorkerProcessRestartScheduled, null, new object[]
					{
						(int)timeSpan.TotalMinutes
					});
					processManagerService.diagnostics.TraceError<int, int>(0L, "Restarting the worker process manager in {0} minutes. It has been restarted {1} times.", (int)timeSpan.TotalMinutes, processManagerService.retryAttempts);
					processManagerService.retryAttempts++;
					if (processManagerService.tcpListener != null)
					{
						processManagerService.tcpListener.StopListening();
					}
					processManagerService.workerProcessManager.Stop(processManagerService.serviceConfiguration.MaxWorkerProcessExitTimeout, processManagerService.serviceConfiguration.MaxWorkerProcessDumpTimeout);
					processManagerService.restartTimer = new Timer(new TimerCallback(processManagerService.RestartTimerCallback), null, (int)timeSpan.TotalMilliseconds, -1);
					return false;
				}
			}
			else if (canRetry && workerInstance != null && processManagerService.startState == StartState.Starting && processManagerService.retryAttempts < processManagerService.serviceConfiguration.MaxProcessRestartAttemptsWhileInStartingState)
			{
				processManagerService.retryAttempts++;
				if (processManagerService.workerProcessManager.RetryWorkerProcess(workerInstance))
				{
					return false;
				}
			}
			ProcessManagerService.StopService();
			return true;
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x0005EF3C File Offset: 0x0005D13C
		public static void StopService()
		{
			if (ProcessManagerService.instance == null)
			{
				throw new NullReferenceException("Instance of ProcessManagerService does not exist");
			}
			if (ProcessManagerService.instance.SetStopServiceCalled() == 1)
			{
				ProcessManagerService.instance.diagnostics.TraceDebug(0L, "StopService will be skipped because it is already called");
				return;
			}
			if (!ProcessManagerService.instance.runningAsService)
			{
				ProcessManagerService.instance.diagnostics.TraceDebug(0L, "StopService called (running interactive)");
				Environment.Exit(1);
				return;
			}
			if (ProcessManagerService.instance.startState != StartState.None)
			{
				ProcessManagerService.instance.diagnostics.TraceDebug(0L, "StopService called (running as a service)");
				ProcessManagerService.instance.Stop();
				return;
			}
			ProcessManagerService.instance.diagnostics.TraceDebug(0L, "StopService called (running as a service, not started yet)");
			Environment.Exit(1);
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x0005EFF1 File Offset: 0x0005D1F1
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.restartTimer != null)
				{
					this.restartTimer.Dispose();
					this.restartTimer = null;
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x0005F034 File Offset: 0x0005D234
		internal bool CheckCommonSwapThresholds(Process process, int refreshInterval, int maxThreads, long maxWorkingSet)
		{
			int num = -1;
			try
			{
				num = process.Id;
			}
			catch (InvalidOperationException)
			{
				return false;
			}
			if (!this.CheckRefreshIntervalOkay(process, num, refreshInterval))
			{
				return true;
			}
			if (maxThreads == 0 && maxWorkingSet == 0L)
			{
				return false;
			}
			Process process2 = null;
			bool result;
			try
			{
				try
				{
					process2 = Process.GetProcessById(num);
				}
				catch (ArgumentException)
				{
				}
				if (process2 == null)
				{
					this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_WorkerProcessRefreshProcessData, null, new object[]
					{
						num
					});
					this.diagnostics.TraceDebug<int>(0L, "Process {0}: failed to retrieve process data, so refresh", num);
					result = true;
				}
				else
				{
					int threadCount = 0;
					long workingSet = 0L;
					try
					{
						threadCount = process2.Threads.Count;
						workingSet = process2.WorkingSet64;
					}
					catch (InvalidOperationException ex)
					{
						this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_WorkerProcessRefreshProcessDataFetchFailed, null, new object[]
						{
							num,
							ex
						});
						this.diagnostics.TraceDebug<int, InvalidOperationException>(0L, "Process {0}: failed to retrieve thread or working set information, so refresh. Exception {1}", num, ex);
						return true;
					}
					if (!this.CheckMaxThreadsOkay(num, maxThreads, threadCount))
					{
						result = true;
					}
					else if (!this.CheckMaxWorkingSetOkay(num, maxWorkingSet, workingSet))
					{
						result = true;
					}
					else
					{
						result = false;
					}
				}
			}
			finally
			{
				if (process2 != null)
				{
					process2.Close();
				}
			}
			return result;
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x0005F188 File Offset: 0x0005D388
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ProcessManagerService>(this);
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x0005F190 File Offset: 0x0005D390
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
				this.disposeTracker = null;
			}
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x0005F1AC File Offset: 0x0005D3AC
		protected virtual bool Initialize()
		{
			ProcessManagerService.DropBreadCrumb(ProcessManagerService.ProcessManagerBreadcrumbs.Initialize, ExDateTime.UtcNow);
			try
			{
				this.serviceConfiguration = ServiceConfiguration.Load(this);
			}
			catch (ConfigurationErrorsException ex)
			{
				this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_AppConfigLoadFailed, null, new object[]
				{
					ex.ToString()
				});
				return false;
			}
			bool flag = this.CreateJobObject(this.jobObjectName);
			return flag && this.stopServiceCalled == 0;
		}

		// Token: 0x06002B53 RID: 11091
		protected abstract bool GetBindings(out IPEndPoint[] bindings);

		// Token: 0x06002B54 RID: 11092 RVA: 0x0005F22C File Offset: 0x0005D42C
		protected virtual void SetJobObjectExtendedInfo(ref NativeMethods.JOBOBJECT_EXTENDED_LIMIT_INFORMATION jobExtendedInfo)
		{
			jobExtendedInfo.ProcessMemoryLimit = UIntPtr.Zero;
			jobExtendedInfo.JobMemoryLimit = UIntPtr.Zero;
			jobExtendedInfo.PeakProcessMemoryUsed = UIntPtr.Zero;
			jobExtendedInfo.PeakJobMemoryUsed = UIntPtr.Zero;
			jobExtendedInfo.BasicLimitInformation.LimitFlags = 8192U;
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x0005F26A File Offset: 0x0005D46A
		protected virtual JobObjectUILimit GetJobObjectUIRestrictions()
		{
			return JobObjectUILimit.Handles | JobObjectUILimit.ReadClipboard | JobObjectUILimit.SystemParameters | JobObjectUILimit.WriteClipboard | JobObjectUILimit.Desktop | JobObjectUILimit.DisplaySettings | JobObjectUILimit.ExitWindows | JobObjectUILimit.GlobalAtoms;
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x0005F271 File Offset: 0x0005D471
		protected virtual bool TryReadServerConfig()
		{
			return true;
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x0005F274 File Offset: 0x0005D474
		protected virtual void RegisterWorkerEvents(WorkerProcessManager workerProcessManager)
		{
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x0005F276 File Offset: 0x0005D476
		protected virtual void UnregisterWorkerEvents(WorkerProcessManager workerProcessManager)
		{
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x0005F278 File Offset: 0x0005D478
		protected override void OnStartInternal(string[] args)
		{
			ProcessManagerService.DropBreadCrumb(ProcessManagerService.ProcessManagerBreadcrumbs.OnStartInternalEnter, ExDateTime.UtcNow);
			try
			{
				if (this.stopServiceCalled != 1)
				{
					this.startState = StartState.Starting;
					using (Process currentProcess = Process.GetCurrentProcess())
					{
						this.diagnostics.TracePfd<int, string, DateTime>(0L, "PFD ETS {0} Starting {1} ({2})", 24599, base.ServiceName, DateTime.UtcNow);
						this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_ServiceStartAttempt, null, new object[]
						{
							currentProcess.Id
						});
						if (this.runningAsService)
						{
							base.RequestAdditionalTime((int)ProcessManagerService.StartupTimeoutInterval.TotalMilliseconds - (int)TimeSpan.FromSeconds(30.0).TotalMilliseconds);
						}
						if (!this.TryReadServerConfig())
						{
							this.diagnostics.TraceError(0L, "TryReadServerConfig failed. The service is stopping.");
							ProcessManagerService.StopService();
						}
						else
						{
							this.SetMaxIOThreadsLimit();
							this.SetupWorkerProcessManager(this.workerProcessPathName);
							if (this.workerProcessManager == null)
							{
								this.diagnostics.TraceError(0L, "SetupWorkerProcessManager failed. The service is stopping.");
								ProcessManagerService.StopService();
							}
							else
							{
								ProcessManagerService.DropBreadCrumb(ProcessManagerService.ProcessManagerBreadcrumbs.WorkerProcessSetupSuccess, ExDateTime.UtcNow);
								while (!this.workerProcessManager.HasWorkerEverContacted)
								{
									if (this.workerProcessManager.IsStoppedOrStopping())
									{
										this.diagnostics.TraceError(0L, "WorkerProcessManager is stopped before start completed");
										return;
									}
									Thread.Sleep((int)ProcessManagerService.SleepInterval.TotalMilliseconds);
								}
								if (this.serviceConfiguration.ServiceListening)
								{
									this.tcpListener = new TcpListener(new TcpListener.HandleFailure(ProcessManagerService.OnTcpListenerFailure), new TcpListener.HandleConnection(this.workerProcessManager.HandleConnection), null, this.diagnostics, this.eventLogger, this.maxConnectionRate, false, false);
									this.GetAndSetBindings(true);
									this.tcpListener.StartListening(true);
								}
								this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_ServiceStartedSuccessFully, null, new object[]
								{
									currentProcess.Id
								});
								this.diagnostics.TracePfd<int, string>(0L, "PFD ETS {0} Started Successfully {1}", 19959, base.ServiceName);
								this.startState = StartState.Started;
							}
						}
					}
				}
			}
			finally
			{
				ProcessManagerService.DropBreadCrumb(ProcessManagerService.ProcessManagerBreadcrumbs.OnStartInternalExit, (int)this.startState, ExDateTime.UtcNow);
			}
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x0005F4E4 File Offset: 0x0005D6E4
		protected override void OnStopInternal()
		{
			ProcessManagerService.DropBreadCrumb(ProcessManagerService.ProcessManagerBreadcrumbs.OnStopInternalEnter, ExDateTime.UtcNow);
			try
			{
				this.diagnostics.TraceDebug(0L, "OnStopInternal is called");
				this.StopListenerAndWorker();
			}
			finally
			{
				ProcessManagerService.DropBreadCrumb(ProcessManagerService.ProcessManagerBreadcrumbs.OnStopInternalExit, ExDateTime.UtcNow);
			}
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x0005F53C File Offset: 0x0005D73C
		protected override void OnShutdownInternal()
		{
			ProcessManagerService.DropBreadCrumb(ProcessManagerService.ProcessManagerBreadcrumbs.OnShutdownInternalEnter, ExDateTime.UtcNow);
			try
			{
				this.diagnostics.TraceDebug(0L, "OnShutdownInternal is called");
				this.StopListenerAndWorker();
			}
			finally
			{
				ProcessManagerService.DropBreadCrumb(ProcessManagerService.ProcessManagerBreadcrumbs.OnShutdownInternalExit, ExDateTime.UtcNow);
			}
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x0005F594 File Offset: 0x0005D794
		protected override void OnCustomCommandInternal(int command)
		{
			ProcessManagerService.DropBreadCrumb(ProcessManagerService.ProcessManagerBreadcrumbs.OnCustomCommandInternalEnter, command);
			this.diagnostics.TraceDebug<int>(0L, "OnCustomCommandInternal is called, with command {0}.", command);
			if (this.workerProcessManager == null)
			{
				this.diagnostics.TraceError<int>(0L, "OnCustomCommandInternal: no worker process manager.", command);
				return;
			}
			switch (command)
			{
			case 200:
				if (this.workerProcessManager.IsRunning())
				{
					this.workerProcessManager.InitiateRefresh();
					return;
				}
				this.InitiateRestart();
				return;
			case 201:
				if (!this.workerProcessManager.IsRunning())
				{
					return;
				}
				this.OnConfigUpdate();
				return;
			case 202:
				if (!this.workerProcessManager.IsRunning())
				{
					return;
				}
				this.OnMemoryPressure();
				return;
			case 203:
				if (!this.workerProcessManager.IsRunning())
				{
					return;
				}
				this.OnClearConfigCache();
				return;
			case 204:
				if (!this.workerProcessManager.IsRunning())
				{
					return;
				}
				this.OnBlockedSubmissionQueue();
				return;
			case 205:
				if (!this.workerProcessManager.IsRunning())
				{
					return;
				}
				this.OnClearKerberosTicketCache();
				return;
			case 206:
				if (!this.workerProcessManager.IsRunning())
				{
					return;
				}
				this.OnLogFlush();
				return;
			case 207:
				if (!this.workerProcessManager.IsRunning())
				{
					return;
				}
				this.OnForcedCrash();
				return;
			default:
				return;
			}
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x0005F6BC File Offset: 0x0005D8BC
		protected virtual void OnConfigUpdate()
		{
			if (this.workerProcessManager != null)
			{
				this.workerProcessManager.InitiateConfigUpdate();
			}
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x0005F6D1 File Offset: 0x0005D8D1
		protected virtual void OnMemoryPressure()
		{
			if (this.workerProcessManager != null)
			{
				this.workerProcessManager.InitiateMemoryPressureHandler();
			}
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x0005F6E6 File Offset: 0x0005D8E6
		protected virtual void OnLogFlush()
		{
			if (this.workerProcessManager != null)
			{
				this.workerProcessManager.InitiateLogFlush();
			}
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x0005F6FB File Offset: 0x0005D8FB
		protected virtual void OnClearConfigCache()
		{
			if (this.workerProcessManager != null)
			{
				this.workerProcessManager.InitiateClearConfigCache();
			}
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x0005F710 File Offset: 0x0005D910
		protected virtual void OnBlockedSubmissionQueue()
		{
			if (this.workerProcessManager != null)
			{
				this.workerProcessManager.InitiateSubmissionQueueBlockedHandler();
			}
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x0005F725 File Offset: 0x0005D925
		protected virtual void OnForcedCrash()
		{
			if (this.workerProcessManager != null)
			{
				this.workerProcessManager.InitiateForcedCrash();
			}
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x0005F73A File Offset: 0x0005D93A
		protected virtual void OnClearKerberosTicketCache()
		{
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x0005F73C File Offset: 0x0005D93C
		protected override void OnPauseInternal()
		{
			if (this.workerProcessManager != null)
			{
				this.workerProcessManager.InitiatePause();
			}
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x0005F751 File Offset: 0x0005D951
		protected override void OnContinueInternal()
		{
			if (this.workerProcessManager != null)
			{
				this.workerProcessManager.InitiateContinue();
			}
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x0005F768 File Offset: 0x0005D968
		protected override void OnCommandTimeout()
		{
			if (this.workerProcessManager != null)
			{
				this.workerProcessManager.WorkerInstancesTriggerDumpAndWait(this.serviceConfiguration.MaxWorkerProcessDumpTimeout);
				if (this.startState == StartState.Starting && this.workerProcessManager.HasWorkerCrashed)
				{
					ProcessManagerService.instance.diagnostics.TraceError(0L, "Worker crashed during startup. No need to generate service dump.");
					Environment.Exit(1);
				}
			}
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x0005F7C8 File Offset: 0x0005D9C8
		protected void StopListenerAndWorker()
		{
			ProcessManagerService.DropBreadCrumb(ProcessManagerService.ProcessManagerBreadcrumbs.StopListenerAndWorkerEnter, ExDateTime.UtcNow);
			try
			{
				if (this.tcpListener != null)
				{
					this.tcpListener.ProcessStopping = true;
				}
				if (Interlocked.Exchange(ref this.stopListenerAndWorkerCalled, 1) == 1)
				{
					while (!this.stopListenerAndWorkerCompleted)
					{
						Thread.Sleep((int)ProcessManagerService.SleepInterval.TotalMilliseconds);
					}
				}
				else
				{
					using (Process currentProcess = Process.GetCurrentProcess())
					{
						this.diagnostics.TraceDebug<string>(0L, "Stopping {0}", base.ServiceName);
						this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_ServiceStopAttempt, null, new object[]
						{
							currentProcess.Id
						});
						if (this.tcpListener != null)
						{
							this.tcpListener.StopListening();
							this.tcpListener.Shutdown();
						}
						WorkerProcessManager workerProcessManager = this.workerProcessManager;
						if (workerProcessManager != null)
						{
							if (workerProcessManager.IsRunning())
							{
								workerProcessManager.Stop(this.serviceConfiguration.MaxWorkerProcessExitTimeout, this.serviceConfiguration.MaxWorkerProcessDumpTimeout);
							}
							this.UnregisterWorkerEvents(workerProcessManager);
						}
						this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_ServiceStopped, null, new object[]
						{
							currentProcess.Id
						});
						this.diagnostics.TraceDebug<string>(0L, "Stopped {0}", base.ServiceName);
						this.stopListenerAndWorkerCompleted = true;
					}
				}
			}
			finally
			{
				ProcessManagerService.DropBreadCrumb(ProcessManagerService.ProcessManagerBreadcrumbs.StopListenerAndWorkerExit, ExDateTime.UtcNow);
			}
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x0005F960 File Offset: 0x0005DB60
		protected void GetAndSetBindings(bool stopOnFailure)
		{
			IPEndPoint[] newBindings = null;
			if (!this.GetBindings(out newBindings))
			{
				if (stopOnFailure)
				{
					this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_BindingConfigNotFound, null, new object[0]);
					ProcessManagerService.StopService();
				}
				return;
			}
			if (this.tcpListener != null)
			{
				this.tcpListener.SetBindings(newBindings, stopOnFailure);
			}
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x0005F9B0 File Offset: 0x0005DBB0
		protected bool CreateJobObject(string jobObjectName)
		{
			this.jobObject = NativeMethods.CreateJobObject(IntPtr.Zero, jobObjectName);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (this.jobObject.IsInvalid)
			{
				this.diagnostics.TraceError<string, int>(0L, "CreateJobObject(name: {0}) failed: {1}", jobObjectName, lastWin32Error);
				this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_CreateJobObjectFailed, null, new object[]
				{
					lastWin32Error.ToString(),
					jobObjectName
				});
				return false;
			}
			if (lastWin32Error == 183)
			{
				this.diagnostics.TraceWarning<string>(0L, "CreateJobObject(name: {0}) returned existing jobObject", jobObjectName);
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_CreatedExistingJobObject, null, new object[]
					{
						jobObjectName,
						currentProcess.Id
					});
				}
			}
			try
			{
				NativeMethods.JOBOBJECT_EXTENDED_LIMIT_INFORMATION extendedLimits = default(NativeMethods.JOBOBJECT_EXTENDED_LIMIT_INFORMATION);
				this.SetJobObjectExtendedInfo(ref extendedLimits);
				this.jobObject.SetExtendedLimits(extendedLimits);
				JobObjectUILimit jobObjectUIRestrictions = this.GetJobObjectUIRestrictions();
				this.jobObject.SetUIRestrictions(jobObjectUIRestrictions);
			}
			catch (Win32Exception ex)
			{
				this.diagnostics.TraceError<int>(0L, "SetInformationJobObject() failed: {0}", ex.NativeErrorCode);
				this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_SetJobObjectFailed, null, new object[]
				{
					ex.Message,
					jobObjectName
				});
				this.jobObject.Close();
				return false;
			}
			return true;
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x0005FB28 File Offset: 0x0005DD28
		private void RestartTimerCallback(object obj)
		{
			this.diagnostics.TraceDebug(0L, "RestartTimerCallback invoked.");
			this.InitiateRestart();
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x0005FB44 File Offset: 0x0005DD44
		private void InitiateRestart()
		{
			ProcessManagerService.DropBreadCrumb(ProcessManagerService.ProcessManagerBreadcrumbs.InitiateRestartEnter, ExDateTime.UtcNow);
			try
			{
				if (this.stopListenerAndWorkerCalled == 1 || this.stopServiceCalled == 1)
				{
					this.diagnostics.TraceError(0L, "Ignore restart because stop service has been requested.");
				}
				else if (!this.workerProcessManager.ReInit())
				{
					this.diagnostics.TraceError(0L, "Unable to restart because the worker process manager is not stopped.");
				}
				else
				{
					this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_RestartWorkerProcess, null, new object[0]);
					this.diagnostics.TraceError(0L, "Restarting the worker process manager.");
					if (!this.workerProcessManager.Start())
					{
						this.diagnostics.TraceError(0L, "WorkerProcessManager.Start() failed. The service is stopping.");
						ProcessManagerService.StopService();
					}
					else
					{
						int i = 0;
						bool flag = false;
						while (i < (int)ProcessManagerService.StartupWaitInterval.TotalMilliseconds / (int)ProcessManagerService.SleepInterval.TotalMilliseconds)
						{
							flag = this.workerProcessManager.IsReady();
							if (flag)
							{
								break;
							}
							if (this.workerProcessManager.IsStoppedOrStopping())
							{
								this.diagnostics.TraceError(0L, "WorkerProcessManager is stopped before restart completed");
								return;
							}
							Thread.Sleep((int)ProcessManagerService.SleepInterval.TotalMilliseconds);
							i++;
						}
						if (!flag)
						{
							this.diagnostics.TraceError(0L, "InitiateRestart failed as the service did not become ready. The service is stopping.");
							ProcessManagerService.StopService();
						}
						else
						{
							this.diagnostics.TraceDebug(0L, "Restart is successful");
							if (this.tcpListener != null)
							{
								this.tcpListener.StartListening(false);
							}
							this.retryAttempts = 0;
						}
					}
				}
			}
			finally
			{
				ProcessManagerService.DropBreadCrumb(ProcessManagerService.ProcessManagerBreadcrumbs.InitiateRestartExit, ExDateTime.UtcNow);
			}
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x0005FCE8 File Offset: 0x0005DEE8
		private bool WorkerProcessManagerStopServiceHandler(bool canRetry, bool retryAlways, bool retryImmediately, WorkerInstance workerProcess)
		{
			this.diagnostics.TraceDebug(0L, "WorkerProcessManagerStopServiceHandler: service request to stop the service");
			return ProcessManagerService.StopService(canRetry, retryAlways, retryImmediately, workerProcess);
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x0005FD08 File Offset: 0x0005DF08
		private bool SetupWorkerProcessManager(string workerPath)
		{
			try
			{
				this.workerProcessManager = new WorkerProcessManager(workerPath, new WorkerProcessManager.ProcessNeedsSwapCheckDelegate(this.CheckProcessSwap), this.jobObject, this.diagnostics, this.eventLogger, new WorkerProcessManager.StopServiceHandler(this.WorkerProcessManagerStopServiceHandler));
			}
			catch (ArgumentException arg)
			{
				this.diagnostics.TraceError<ArgumentException>(0L, "Failed to create the WorkerProcessManager. Exception: {0}", arg);
				this.workerProcessManager = null;
			}
			if (this.workerProcessManager != null)
			{
				this.RegisterWorkerEvents(this.workerProcessManager);
				if (!this.workerProcessManager.Start())
				{
					this.workerProcessManager = null;
				}
			}
			return this.workerProcessManager != null;
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x0005FDB0 File Offset: 0x0005DFB0
		private void SetMaxIOThreadsLimit()
		{
			int workerThreads;
			int arg;
			ThreadPool.GetMaxThreads(out workerThreads, out arg);
			ThreadPool.SetMaxThreads(workerThreads, this.serviceConfiguration.MaxIOThreads);
			this.diagnostics.TraceDebug<int, int>(0L, "Setting max I/O threads from {0} to {1}", arg, this.serviceConfiguration.MaxIOThreads);
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x0005FDF6 File Offset: 0x0005DFF6
		private bool CheckProcessSwap(Process process)
		{
			return this.CheckCommonSwapThresholds(process, this.serviceConfiguration.MaxWorkerProcessRefreshInterval, this.serviceConfiguration.MaxWorkerProcessThreads, this.serviceConfiguration.MaxWorkerProcessWorkingSet);
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x0005FE20 File Offset: 0x0005E020
		private bool CheckRefreshIntervalOkay(Process process, int pid, int refreshInterval)
		{
			if (refreshInterval == 0)
			{
				return true;
			}
			DateTime localTime = ExDateTime.Now.LocalTime;
			DateTime dateTime = process.StartTime.AddSeconds((double)refreshInterval);
			if (dateTime > localTime)
			{
				return true;
			}
			this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_WorkerProcessRefreshInterval, null, new object[]
			{
				pid,
				refreshInterval.ToString()
			});
			this.diagnostics.TraceDebug(0L, "Process {0}: threshold 'RefreshInterval' exceeded: startTime={1}, retireTime=={2}, now={3}", new object[]
			{
				pid,
				process.StartTime,
				dateTime,
				localTime
			});
			return false;
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x0005FED8 File Offset: 0x0005E0D8
		private bool CheckMaxThreadsOkay(int pid, int maxThreads, int threadCount)
		{
			if (maxThreads == 0)
			{
				return true;
			}
			if (threadCount <= maxThreads)
			{
				return true;
			}
			this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_WorkerProcessRefreshMaxThread, null, new object[]
			{
				pid,
				threadCount.ToString(),
				maxThreads.ToString()
			});
			this.diagnostics.TraceDebug<int, int, int>(0L, "Process {0}: threshold 'MaxThreads' exceeded: threadCount={1}, maxThreads={2}", pid, threadCount, maxThreads);
			return false;
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x0005FF40 File Offset: 0x0005E140
		private bool CheckMaxWorkingSetOkay(int pid, long maxWorkingSet, long workingSet)
		{
			if (maxWorkingSet == 0L)
			{
				return true;
			}
			if (workingSet < maxWorkingSet)
			{
				return true;
			}
			this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_WorkerProcessRefreshMaxWorkingSet, null, new object[]
			{
				pid,
				workingSet.ToString(),
				maxWorkingSet.ToString()
			});
			this.diagnostics.TraceDebug<int, long, long>(0L, "Process {0}: threshold 'MaxWorkingSet' exceeded: workingSet={1}, MaxWorkingSet=={2}", pid, workingSet, maxWorkingSet);
			return false;
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x0005FFA7 File Offset: 0x0005E1A7
		private int SetStopServiceCalled()
		{
			return Interlocked.Exchange(ref this.stopServiceCalled, 1);
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x0005FFB5 File Offset: 0x0005E1B5
		private static void DropBreadCrumb(ProcessManagerService.ProcessManagerBreadcrumbs bc, int data)
		{
			if (ProcessManagerService.instance != null && ProcessManagerService.instance.diagnostics != null)
			{
				ProcessManagerService.instance.diagnostics.TraceDebug<ProcessManagerService.ProcessManagerBreadcrumbs, int>(0L, "ProcessManagerService.DropBreadCrumb: {0}, {1}", bc, data);
			}
			ProcessManagerService.DropBreadCrumb((ProcessManagerService.ProcessManagerBreadcrumbs)ProcessManagerService.GetEncodedInt((int)bc, data));
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x0005FFF0 File Offset: 0x0005E1F0
		private static void DropBreadCrumb(ProcessManagerService.ProcessManagerBreadcrumbs bc, int data, ExDateTime time)
		{
			if (ProcessManagerService.instance != null && ProcessManagerService.instance.diagnostics != null)
			{
				ProcessManagerService.instance.diagnostics.TraceDebug<ProcessManagerService.ProcessManagerBreadcrumbs, int, ExDateTime>(0L, "ProcessManagerService.DropBreadCrumb: {0}, {1}, {2}", bc, data, time);
			}
			ProcessManagerService.ProcessManagerBreadcrumbs encodedInt = (ProcessManagerService.ProcessManagerBreadcrumbs)ProcessManagerService.GetEncodedInt((int)bc, data);
			ProcessManagerService.DropBreadCrumb((ProcessManagerService.ProcessManagerBreadcrumbs)ProcessManagerService.GetEncodedDateTime((int)encodedInt, time));
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x0006003D File Offset: 0x0005E23D
		private static void DropBreadCrumb(ProcessManagerService.ProcessManagerBreadcrumbs bc, ExDateTime time)
		{
			if (ProcessManagerService.instance != null && ProcessManagerService.instance.diagnostics != null)
			{
				ProcessManagerService.instance.diagnostics.TraceDebug<ProcessManagerService.ProcessManagerBreadcrumbs, ExDateTime>(0L, "ProcessManagerService.DropBreadCrumb: {0}, {1}", bc, time);
			}
			ProcessManagerService.DropBreadCrumb((ProcessManagerService.ProcessManagerBreadcrumbs)ProcessManagerService.GetEncodedDateTime((int)bc, time));
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x00060076 File Offset: 0x0005E276
		private static void DropBreadCrumb(ProcessManagerService.ProcessManagerBreadcrumbs bc)
		{
			ProcessManagerService.breadcrumbs.Drop(bc);
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x00060083 File Offset: 0x0005E283
		public static int GetEncodedDateTime(int bc, ExDateTime time)
		{
			return bc + time.Minute * 100 + time.Second;
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x00060099 File Offset: 0x0005E299
		public static int GetEncodedInt(int bc, int data)
		{
			if (data < 0 || data >= 100)
			{
				return bc + 990000;
			}
			return bc + data * 10000;
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x000600B5 File Offset: 0x0005E2B5
		private static void OnTcpListenerFailure(bool addressAlreadyInUseFailure)
		{
			ProcessManagerService.StopService();
		}

		// Token: 0x040025A2 RID: 9634
		public const string TransportServiceName = "MSExchangeTransport";

		// Token: 0x040025A3 RID: 9635
		private const long TraceId = 0L;

		// Token: 0x040025A4 RID: 9636
		public const int NumberOfBreadcrumbs = 128;

		// Token: 0x040025A5 RID: 9637
		public static ProcessManagerService instance;

		// Token: 0x040025A6 RID: 9638
		protected Microsoft.Exchange.Diagnostics.Trace diagnostics;

		// Token: 0x040025A7 RID: 9639
		protected ExEventLog eventLogger;

		// Token: 0x040025A8 RID: 9640
		protected StartState startState;

		// Token: 0x040025A9 RID: 9641
		protected int stopServiceCalled;

		// Token: 0x040025AA RID: 9642
		private DisposeTracker disposeTracker;

		// Token: 0x040025AB RID: 9643
		private static readonly TimeSpan StartupTimeoutInterval = TimeSpan.FromMinutes(2.0);

		// Token: 0x040025AC RID: 9644
		private static readonly TimeSpan RestartWorkerProcessInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x040025AD RID: 9645
		private static readonly TimeSpan ImmediateRestartWorkerProcessInterval = TimeSpan.FromSeconds(5.0);

		// Token: 0x040025AE RID: 9646
		private static readonly TimeSpan StartupWaitInterval = TimeSpan.FromMinutes(2.0);

		// Token: 0x040025AF RID: 9647
		private static readonly TimeSpan SleepInterval = TimeSpan.FromMilliseconds(250.0);

		// Token: 0x040025B0 RID: 9648
		private string workerProcessPathName;

		// Token: 0x040025B1 RID: 9649
		private string jobObjectName;

		// Token: 0x040025B2 RID: 9650
		private Timer restartTimer;

		// Token: 0x040025B3 RID: 9651
		private int retryAttempts;

		// Token: 0x040025B4 RID: 9652
		private ServiceConfiguration serviceConfiguration;

		// Token: 0x040025B5 RID: 9653
		private WorkerProcessManager workerProcessManager;

		// Token: 0x040025B6 RID: 9654
		private TcpListener tcpListener;

		// Token: 0x040025B7 RID: 9655
		private bool runningAsService;

		// Token: 0x040025B8 RID: 9656
		private SafeJobHandle jobObject;

		// Token: 0x040025B9 RID: 9657
		private int maxConnectionRate = 1200;

		// Token: 0x040025BA RID: 9658
		private int stopListenerAndWorkerCalled;

		// Token: 0x040025BB RID: 9659
		private bool stopListenerAndWorkerCompleted;

		// Token: 0x040025BC RID: 9660
		private static Breadcrumbs<ProcessManagerService.ProcessManagerBreadcrumbs> breadcrumbs = new Breadcrumbs<ProcessManagerService.ProcessManagerBreadcrumbs>(128);

		// Token: 0x0200080F RID: 2063
		public enum CustomCommands
		{
			// Token: 0x040025BE RID: 9662
			InitiateRefresh = 200,
			// Token: 0x040025BF RID: 9663
			InitiateConfigUpdate,
			// Token: 0x040025C0 RID: 9664
			InitiateHandleMemoryPressure,
			// Token: 0x040025C1 RID: 9665
			InitiateClearConfigCache,
			// Token: 0x040025C2 RID: 9666
			InitiateHandleSubmissionQueueBlocked,
			// Token: 0x040025C3 RID: 9667
			InitiateClearKerberosTicketCache,
			// Token: 0x040025C4 RID: 9668
			InitiateLogFlush,
			// Token: 0x040025C5 RID: 9669
			InitiateForceCrash
		}

		// Token: 0x02000810 RID: 2064
		private enum ProcessManagerBreadcrumbs
		{
			// Token: 0x040025C7 RID: 9671
			InstanceCreated = 1000000,
			// Token: 0x040025C8 RID: 9672
			StopService = 2000000,
			// Token: 0x040025C9 RID: 9673
			Initialize = 3000000,
			// Token: 0x040025CA RID: 9674
			OnStartInternalEnter = 4000000,
			// Token: 0x040025CB RID: 9675
			WorkerProcessSetupSuccess = 5000000,
			// Token: 0x040025CC RID: 9676
			OnStartInternalExit = 6000000,
			// Token: 0x040025CD RID: 9677
			OnStopInternalEnter = 7000000,
			// Token: 0x040025CE RID: 9678
			OnStopInternalExit = 8000000,
			// Token: 0x040025CF RID: 9679
			OnCustomCommandInternalEnter = 9000000,
			// Token: 0x040025D0 RID: 9680
			InitiateRestartEnter = 10000000,
			// Token: 0x040025D1 RID: 9681
			InitiateRestartExit = 11000000,
			// Token: 0x040025D2 RID: 9682
			OnShutdownInternalEnter = 12000000,
			// Token: 0x040025D3 RID: 9683
			OnShutdownInternalExit = 13000000,
			// Token: 0x040025D4 RID: 9684
			StopListenerAndWorkerEnter = 14000000,
			// Token: 0x040025D5 RID: 9685
			StopListenerAndWorkerExit = 15000000
		}
	}
}
