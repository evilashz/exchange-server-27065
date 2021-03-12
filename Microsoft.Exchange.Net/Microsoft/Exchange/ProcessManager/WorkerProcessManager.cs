using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Win32;
using Microsoft.Win32;

namespace Microsoft.Exchange.ProcessManager
{
	// Token: 0x0200081D RID: 2077
	internal class WorkerProcessManager : DisposeTrackableBase
	{
		// Token: 0x1400008F RID: 143
		// (add) Token: 0x06002BE5 RID: 11237 RVA: 0x00062260 File Offset: 0x00060460
		// (remove) Token: 0x06002BE6 RID: 11238 RVA: 0x00062298 File Offset: 0x00060498
		public event WorkerProcessManager.DisconnectPerformanceCountersHandler OnDisconnectPerformanceCounters;

		// Token: 0x14000090 RID: 144
		// (add) Token: 0x06002BE7 RID: 11239 RVA: 0x000622D0 File Offset: 0x000604D0
		// (remove) Token: 0x06002BE8 RID: 11240 RVA: 0x00062308 File Offset: 0x00060508
		public event WorkerProcessManager.WorkerEventHandler OnWorkerContacted;

		// Token: 0x14000091 RID: 145
		// (add) Token: 0x06002BE9 RID: 11241 RVA: 0x00062340 File Offset: 0x00060540
		// (remove) Token: 0x06002BEA RID: 11242 RVA: 0x00062378 File Offset: 0x00060578
		public event WorkerProcessManager.WorkerEventHandler OnWorkerExited;

		// Token: 0x06002BEB RID: 11243 RVA: 0x000623B0 File Offset: 0x000605B0
		internal WorkerProcessManager(string workerPath, WorkerProcessManager.ProcessNeedsSwapCheckDelegate processNeedsSwapCheckDelegate, SafeJobHandle jobObject, Microsoft.Exchange.Diagnostics.Trace tracer, ExEventLog eventLogger, WorkerProcessManager.StopServiceHandler stopServiceHandler)
		{
			this.diag = tracer;
			this.eventLogger = eventLogger;
			this.processNeedsSwapCheckDelegate = processNeedsSwapCheckDelegate;
			this.jobObject = jobObject;
			this.stopServiceHandler = stopServiceHandler;
			if (workerPath == null || !File.Exists(workerPath))
			{
				this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_WorkerImagePathNotExist, null, new object[]
				{
					workerPath
				});
				this.diag.TraceError<string>(0L, "Path to worker process is invalid: {0}", workerPath);
				throw new ArgumentException("Path to worker process is invalid", "workerPath");
			}
			this.workerPath = workerPath;
			this.socketInformationMemoryStream = new MemoryStream();
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06002BEC RID: 11244 RVA: 0x0006246A File Offset: 0x0006066A
		internal bool HasWorkerCrashed
		{
			get
			{
				return this.hasWorkerCrashed;
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06002BED RID: 11245 RVA: 0x00062472 File Offset: 0x00060672
		internal bool HasWorkerEverContacted
		{
			get
			{
				return this.hasWorkerEverContacted;
			}
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x0006247A File Offset: 0x0006067A
		private bool SetState(WorkerProcessManager.States newState)
		{
			this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.SetState, (int)newState);
			if (this.state == WorkerProcessManager.States.Stopped && newState != WorkerProcessManager.States.Init)
			{
				return false;
			}
			this.state = newState;
			return true;
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x000624A0 File Offset: 0x000606A0
		internal bool ReInit()
		{
			this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.ReInit, ExDateTime.UtcNow);
			bool result;
			lock (this)
			{
				if (this.state != WorkerProcessManager.States.Stopped)
				{
					result = false;
				}
				else if (ProcessManagerService.instance.StopListenerAndWorkerCalled == 1)
				{
					result = false;
				}
				else
				{
					result = this.SetState(WorkerProcessManager.States.Init);
				}
			}
			return result;
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x0006250C File Offset: 0x0006070C
		internal bool Start()
		{
			this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.StartEnter, ExDateTime.UtcNow);
			bool result;
			try
			{
				this.diag.TracePfd<int>(0L, "PFD ETS {0} Starting an instance of Worker Process", 20503);
				lock (this)
				{
					if (this.state != WorkerProcessManager.States.Init)
					{
						this.diag.TraceDebug<int>(0L, "Cannot start process manager because state is not idle (state={0})", (int)this.state);
						result = false;
					}
					else if (!this.SetState(WorkerProcessManager.States.Idle))
					{
						result = false;
					}
					else
					{
						GC.Collect();
						GC.WaitForPendingFinalizers();
						this.activeWorker = this.StartWorkerInstance(0, false);
						if (this.monitorTimer == null)
						{
							this.monitorTimer = new Timer(new TimerCallback(this.MonitorInstancesTimer), null, 60000, 60000);
						}
						result = (this.activeWorker != null);
					}
				}
			}
			finally
			{
				this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.StartExit, ExDateTime.UtcNow);
			}
			return result;
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x00062608 File Offset: 0x00060808
		internal void StopInstance(WorkerInstance workerInstance)
		{
			this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.StopInstanceEnter, ExDateTime.UtcNow);
			this.diag.TraceDebug(0L, "WorkerProcessManager.StopInstance invoked.");
			try
			{
				workerInstance.Stop();
				int maxWorkerProcessExitTimeout = ProcessManagerService.instance.ServiceConfiguration.MaxWorkerProcessExitTimeout;
				int num = 0;
				int num2 = 1;
				bool flag = false;
				while (!workerInstance.Exited)
				{
					if (maxWorkerProcessExitTimeout > 0 && !flag && num >= maxWorkerProcessExitTimeout)
					{
						workerInstance.SignalHang();
						flag = true;
					}
					num += num2;
					Thread.Sleep(num2 * 1000);
				}
			}
			finally
			{
				this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.StopInstanceExit, ExDateTime.UtcNow);
			}
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x000626A4 File Offset: 0x000608A4
		internal void Stop(int workerProcessExitTimeout, int workerProcessDumpTimeout)
		{
			this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.StopEnter, ExDateTime.UtcNow);
			try
			{
				this.diag.TraceDebug<int>(0L, "Stop (state={0})", (int)this.state);
				this.TraceWorkers();
				List<WorkerInstance> list = new List<WorkerInstance>();
				lock (this)
				{
					if (!this.SetState(WorkerProcessManager.States.Stopping))
					{
						return;
					}
					this.WaitForWorkerReady(this.activeWorker);
					this.WaitForWorkerReady(this.passiveWorker);
					if (this.activeWorker != null && this.activeWorker.SignaledReady && this.activeWorker.IsConnected)
					{
						this.activeWorker.Stop();
						list.Add(this.activeWorker);
					}
					if (this.passiveWorker != null && this.passiveWorker.SignaledReady && this.passiveWorker.IsConnected)
					{
						this.passiveWorker.Stop();
						list.Add(this.passiveWorker);
					}
					if (this.retiredWorker != null && this.retiredWorker.SignaledReady && this.retiredWorker.IsConnected)
					{
						this.retiredWorker.Stop();
						list.Add(this.retiredWorker);
					}
				}
				int num = 0;
				int num2 = 1;
				bool flag2 = false;
				while (this.WorkerInstancesExist(list) && num < workerProcessDumpTimeout)
				{
					if (workerProcessExitTimeout > 0 && !flag2 && num >= workerProcessExitTimeout)
					{
						this.WorkerInstancesTriggerDump(list);
						flag2 = true;
					}
					num += num2;
					Thread.Sleep(num2 * 1000);
				}
				if (num >= workerProcessDumpTimeout)
				{
					this.TryKillWorkerInstances(list);
				}
				lock (this)
				{
					this.SetState(WorkerProcessManager.States.Stopped);
				}
			}
			finally
			{
				this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.StopExit, ExDateTime.UtcNow);
			}
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x0006289C File Offset: 0x00060A9C
		internal void WorkerInstancesTriggerDumpAndWait(int timeout)
		{
			this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.WorkerInstancesTriggerDumpAndWaitEnter, ExDateTime.UtcNow);
			List<WorkerInstance> list = new List<WorkerInstance>();
			WorkerInstance workerInstance = this.activeWorker;
			if (workerInstance != null)
			{
				list.Add(this.activeWorker);
			}
			workerInstance = this.passiveWorker;
			if (workerInstance != null)
			{
				list.Add(this.passiveWorker);
			}
			workerInstance = this.retiredWorker;
			if (workerInstance != null)
			{
				list.Add(this.retiredWorker);
			}
			this.WorkerInstancesTriggerDump(list);
			Stopwatch stopwatch = Stopwatch.StartNew();
			while (this.WorkerInstancesExist(list) && stopwatch.Elapsed.TotalSeconds < (double)timeout)
			{
				Thread.Sleep(1000);
			}
			if (stopwatch.Elapsed.TotalSeconds >= (double)timeout)
			{
				this.TryKillWorkerInstances(list);
			}
			this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.WorkerInstancesTriggerDumpAndWaitExit, ExDateTime.UtcNow);
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x00062960 File Offset: 0x00060B60
		internal void InitiateRefresh()
		{
			WorkerInstance workerInstance = this.activeWorker;
			int arg = (workerInstance != null) ? workerInstance.Pid : 0;
			if (this.state != WorkerProcessManager.States.Idle)
			{
				this.diag.TraceDebug<int, int>(0L, "Ignore Refresh because state is not Idle (state={0}, active pid={1})", (int)this.state, arg);
				return;
			}
			this.diag.TraceDebug<int, int>(0L, "Initiate Refresh (state={0}, active pid={1})", (int)this.state, arg);
			this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_WorkerProcessRefreshControlCommand, null, new object[]
			{
				arg.ToString()
			});
			this.RefreshStart();
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x000629E8 File Offset: 0x00060BE8
		internal void InitiatePause()
		{
			this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.InitiatePauseEnter, ExDateTime.UtcNow);
			lock (this)
			{
				if (!this.isPaused)
				{
					this.isPaused = true;
					int arg = (this.activeWorker != null) ? this.activeWorker.Pid : 0;
					this.diag.TraceDebug<int>(0L, "Initiate Pause (active pid={0})", arg);
					this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_WorkerProcessPauseCommand, null, new object[]
					{
						arg.ToString()
					});
					this.SendCommand(WorkerProcessManager.ServiceToWorkerCommands.Pause);
				}
			}
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x00062A98 File Offset: 0x00060C98
		internal void InitiateContinue()
		{
			this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.InitiateContinueEnter, ExDateTime.UtcNow);
			lock (this)
			{
				if (this.isPaused)
				{
					this.isPaused = false;
					int arg = (this.activeWorker != null) ? this.activeWorker.Pid : 0;
					this.diag.TraceDebug<int>(0L, "Initiate Continue (active pid={0})", arg);
					this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_WorkerProcessContinueCommand, null, new object[]
					{
						arg.ToString()
					});
					this.SendCommand(WorkerProcessManager.ServiceToWorkerCommands.Continue);
				}
			}
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x00062B48 File Offset: 0x00060D48
		internal void InitiateConfigUpdate()
		{
			lock (this)
			{
				this.refreshEnabled = true;
				this.SendCommand(WorkerProcessManager.ServiceToWorkerCommands.ConfigUpdate);
			}
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x00062B90 File Offset: 0x00060D90
		internal void InitiateMemoryPressureHandler()
		{
			this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.InitiateMemoryPressureHandlerEnter, ExDateTime.UtcNow);
			lock (this)
			{
				int num = (this.activeWorker != null) ? this.activeWorker.Pid : 0;
				this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_WorkerProcessForcedWatsonException, null, new object[]
				{
					num.ToString(),
					202
				});
				this.SendCommand(WorkerProcessManager.ServiceToWorkerCommands.HandleMemoryPressure);
			}
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x00062C28 File Offset: 0x00060E28
		internal void InitiateLogFlush()
		{
			this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.InitiateLogFlush, ExDateTime.UtcNow);
			lock (this)
			{
				if (this.activeWorker != null)
				{
					int pid = this.activeWorker.Pid;
				}
				this.SendCommand(WorkerProcessManager.ServiceToWorkerCommands.LogFlush);
			}
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x00062C8C File Offset: 0x00060E8C
		internal void InitiateClearConfigCache()
		{
			lock (this)
			{
				int num = (this.activeWorker != null) ? this.activeWorker.Pid : 0;
				this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_WorkerProcessClearConfigCache, null, new object[]
				{
					num.ToString(),
					203
				});
				this.SendCommand(WorkerProcessManager.ServiceToWorkerCommands.ClearConfigCache);
			}
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x00062D14 File Offset: 0x00060F14
		internal void InitiateSubmissionQueueBlockedHandler()
		{
			this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.InitiateSubmissionQueueBlockedHandlerEnter, ExDateTime.UtcNow);
			lock (this)
			{
				int num = (this.activeWorker != null) ? this.activeWorker.Pid : 0;
				this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_WorkerProcessForcedWatsonException, null, new object[]
				{
					num.ToString(),
					204
				});
				this.SendCommand(WorkerProcessManager.ServiceToWorkerCommands.HandleSubmissionQueueBlocked);
			}
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x00062DAC File Offset: 0x00060FAC
		internal void InitiateForcedCrash()
		{
			this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.ForcedCrash, ExDateTime.UtcNow);
			lock (this)
			{
				int num = (this.activeWorker != null) ? this.activeWorker.Pid : 0;
				this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_WorkerProcessForcedWatsonException, null, new object[]
				{
					num.ToString(),
					207
				});
				this.SendCommand(WorkerProcessManager.ServiceToWorkerCommands.ForceCrash);
			}
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x00062E44 File Offset: 0x00061044
		internal bool HandleConnection(Socket connection)
		{
			int num = 0;
			int num2 = 0;
			bool flag = false;
			bool flag2 = false;
			this.diag.TraceDebug(0L, "HandleConnection");
			try
			{
				while (num++ < 120)
				{
					WorkerInstance workerInstance = this.activeWorker;
					if (workerInstance != null)
					{
						num2 = workerInstance.Pid;
					}
					else
					{
						flag2 = true;
					}
					if (!flag2 && workerInstance != null && workerInstance.IsConnected)
					{
						this.diag.TraceDebug<int>(0L, "Try sending connection to pid={0}", num2);
						if (this.HandOverConnection(connection, num2, workerInstance))
						{
							flag = true;
							return true;
						}
						this.diag.TraceDebug<int>(0L, "Failed to duplicate handle into pid={0}", num2);
						this.StopInstance(workerInstance);
						return false;
					}
					else
					{
						if (this.IsStoppedOrStopping())
						{
							break;
						}
						if (num == 1)
						{
							Thread.Sleep(200);
						}
						else
						{
							Thread.Sleep(500);
						}
						flag2 = false;
					}
				}
			}
			finally
			{
				if (!flag)
				{
					this.diag.TraceDebug<IntPtr>(0L, "Close socket handle {0}", connection.Handle);
					connection.Close();
				}
			}
			return false;
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x00062F4C File Offset: 0x0006114C
		internal bool IsReady()
		{
			WorkerInstance workerInstance = this.activeWorker;
			return workerInstance != null && workerInstance.IsConnected;
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x00062F6B File Offset: 0x0006116B
		internal bool IsStoppedOrStopping()
		{
			return this.state == WorkerProcessManager.States.Stopping || this.state == WorkerProcessManager.States.Stopped;
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x00062F81 File Offset: 0x00061181
		internal bool IsRunning()
		{
			return this.state != WorkerProcessManager.States.Init && this.state != WorkerProcessManager.States.Stopping && this.state != WorkerProcessManager.States.Stopped;
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x00062FA2 File Offset: 0x000611A2
		internal bool RetryWorkerProcess(WorkerInstance workerInstance)
		{
			if (this.activeWorker == workerInstance || this.activeWorker == null)
			{
				this.CleanWorkerInstance(workerInstance);
				this.RestartActiveWorkerInstance(workerInstance);
				return true;
			}
			return false;
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x00062FC8 File Offset: 0x000611C8
		private bool WorkerInstancesExist(List<WorkerInstance> workers)
		{
			foreach (WorkerInstance workerInstance in workers)
			{
				if (!workerInstance.Exited)
				{
					this.diag.TraceDebug<int>(0L, "Worker instance pid={0} didn't exit yet", workerInstance.Pid);
					return true;
				}
				this.diag.TraceDebug<int>(0L, "Worker instance pid={0} exited", workerInstance.Pid);
			}
			return false;
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x00063050 File Offset: 0x00061250
		internal void WorkerInstancesTriggerDump(List<WorkerInstance> workers)
		{
			foreach (WorkerInstance workerInstance in workers)
			{
				workerInstance.SignalHang();
			}
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x000630A0 File Offset: 0x000612A0
		private bool IsStateRefesh()
		{
			return this.state == WorkerProcessManager.States.RefreshWaitingPassiveConnected || this.state == WorkerProcessManager.States.RefreshWaitingRetiredExit;
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x000630B8 File Offset: 0x000612B8
		private void InternalReset()
		{
			this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.InternalResetEnter, ExDateTime.UtcNow);
			try
			{
				lock (this)
				{
					if (!this.IsStoppedOrStopping())
					{
						if (this.retiredWorker != null)
						{
							WorkerInstance workerInstance = this.retiredWorker;
							this.retiredWorker = null;
							try
							{
								workerInstance.Process.Kill();
							}
							catch (InvalidOperationException)
							{
							}
						}
						if (this.passiveWorker != null)
						{
							WorkerInstance workerInstance = this.passiveWorker;
							this.passiveWorker = null;
							try
							{
								workerInstance.Process.Kill();
							}
							catch (InvalidOperationException)
							{
							}
						}
						if (this.activeWorker != null)
						{
							WorkerInstance workerInstance = this.activeWorker;
							this.activeWorker = null;
							try
							{
								workerInstance.Process.Kill();
							}
							catch (InvalidOperationException)
							{
							}
						}
						if (this.SetState(WorkerProcessManager.States.Idle))
						{
							GC.Collect();
							GC.WaitForPendingFinalizers();
							this.activeWorker = this.StartWorkerInstance(0, false);
						}
					}
				}
			}
			finally
			{
				this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.InternalResetExit, ExDateTime.UtcNow);
			}
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x000631E4 File Offset: 0x000613E4
		private void MonitorInstancesTimer(object state)
		{
			if (Interlocked.Exchange(ref this.monitorBusy, 1) != 0)
			{
				return;
			}
			try
			{
				lock (this)
				{
					int arg = 0;
					WorkerInstance workerInstance = this.activeWorker;
					if (workerInstance != null)
					{
						arg = workerInstance.Pid;
					}
					this.diag.TraceDebug<int>(0L, "Monitor the worker instance (pid={0})", arg);
					if (this.refreshEnabled && !this.IsStateRefesh() && workerInstance != null && workerInstance.Process != null && this.processNeedsSwapCheckDelegate != null && this.processNeedsSwapCheckDelegate(workerInstance.Process))
					{
						this.diag.TraceDebug(0L, "Start refresh");
						this.RefreshStart();
					}
				}
			}
			finally
			{
				Interlocked.Exchange(ref this.monitorBusy, 0);
			}
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x000632B8 File Offset: 0x000614B8
		private WorkerInstance StartWorkerInstance(int thrashCrashCount, bool startAsPassive)
		{
			this.HandlePerformanceCountersDisconnection();
			WorkerInstance workerInstance = new WorkerInstance(startAsPassive, new WorkerInstance.WorkerContacted(this.HandleWorkerContacted), new WorkerInstance.WorkerExited(this.HandleWorkerExited), thrashCrashCount, this.diag);
			if (!workerInstance.Start(this.workerPath, this.isPaused, ProcessManagerService.instance.ServiceConfiguration.ServiceListening, this.jobObject))
			{
				workerInstance = null;
				this.diag.TraceError(0L, "Failed to start the worker process");
				this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_WorkerStartFailed, null, new object[]
				{
					this.workerPath
				});
				ProcessManagerService.StopService();
			}
			else
			{
				this.diag.TracePfd<int, int>(0L, "PFD ETS {0} Started a new instance pid={1}", 28695, workerInstance.Pid);
			}
			return workerInstance;
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x00063378 File Offset: 0x00061578
		private void AsyncStartActiveWorker(object context)
		{
			this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.AsyncStartActiveWorkerEnter, ExDateTime.UtcNow);
			WorkerInstance workerInstance = (WorkerInstance)context;
			if (!this.WaitForProcessHandleClose(workerInstance.Pid))
			{
				this.diag.TraceDebug(0L, "Cannot verify the old handle is closed.");
			}
			lock (this)
			{
				if (this.IsStoppedOrStopping())
				{
					this.diag.TraceDebug(0L, "AsyncStartActiveWorker: Skipping because process manager is stopped.");
				}
				else
				{
					this.diag.TraceDebug(0L, "AsyncStartActiveWorker: prepare to force GC collection and then start active worker instance");
					int thrashCrashCount = 0;
					if (!workerInstance.IsConnected)
					{
						thrashCrashCount = workerInstance.ThrashCrashCount + 1;
					}
					this.activeWorker = this.StartWorkerInstance(thrashCrashCount, false);
				}
			}
		}

		// Token: 0x06002C09 RID: 11273 RVA: 0x00063434 File Offset: 0x00061634
		private void HandlePerformanceCountersDisconnection()
		{
			if (ProcessManagerService.instance.ServiceConfiguration.DisconnectTransportPerformanceCounters)
			{
				WorkerProcessManager.DisconnectPerformanceCountersHandler onDisconnectPerformanceCounters = this.OnDisconnectPerformanceCounters;
				if (onDisconnectPerformanceCounters != null)
				{
					onDisconnectPerformanceCounters();
				}
			}
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x00063464 File Offset: 0x00061664
		private void RestartActiveWorkerInstance(WorkerInstance workerInstance)
		{
			this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.RestartActiveWorkerInstanceEnter, workerInstance.ThrashCrashCount, ExDateTime.UtcNow);
			this.diag.TraceDebug<string>(0L, "RestartWorkerInstance worker instance: {0}", this.workerPath);
			int num = 0;
			if (!workerInstance.IsConnected)
			{
				num = workerInstance.ThrashCrashCount + 1;
				this.diag.TraceDebug(0L, "Worker instance crashed immediately: pid={0} thrashCrashCount={1}, start time={2}, connected={3}", new object[]
				{
					workerInstance.Pid,
					num,
					workerInstance.StartTime,
					workerInstance.IsConnected
				});
			}
			int thrashCrashMaximum = ProcessManagerService.instance.ServiceConfiguration.ThrashCrashMaximum;
			if (num > thrashCrashMaximum)
			{
				this.diag.TraceError<string, int, int>(0L, "Worker instance keeps crashing on startup: {0}, thrashCrashCount={1}, maximum={2}", this.workerPath, num, thrashCrashMaximum);
				this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_WorkerStartThrashCount, null, new object[]
				{
					this.workerPath
				});
				ProcessManagerService.StopService();
				return;
			}
			this.diag.TraceError(0L, "Schedule a AsyncStartActiveWorker callback");
			this.asyncRestartTimer = new Timer(new TimerCallback(this.AsyncStartActiveWorker), workerInstance, 250, -1);
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x00063588 File Offset: 0x00061788
		private void TryKillWorkerInstances(List<WorkerInstance> workers)
		{
			foreach (WorkerInstance workerInstance in workers)
			{
				if (!workerInstance.Exited)
				{
					this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_AttemptToKillWorkerProcess, null, new object[]
					{
						workerInstance.Pid
					});
					try
					{
						workerInstance.Process.Kill();
					}
					catch (Win32Exception exception)
					{
						this.LogFailedKill(workerInstance.Pid, exception);
					}
					catch (NotSupportedException exception2)
					{
						this.LogFailedKill(workerInstance.Pid, exception2);
					}
					catch (InvalidOperationException exception3)
					{
						this.LogFailedKill(workerInstance.Pid, exception3);
					}
				}
			}
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x00063668 File Offset: 0x00061868
		private void LogFailedKill(int pid, Exception exception)
		{
			this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_FailedToKillWorkerProcess, null, new object[]
			{
				pid,
				exception.Message
			});
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x000636A4 File Offset: 0x000618A4
		private void TraceWorkers()
		{
			string arg = "";
			WorkerInstance workerInstance = this.activeWorker;
			if (workerInstance != null)
			{
				arg = string.Format("pid={0}", workerInstance.Pid);
			}
			this.diag.TraceDebug<string>(0L, "active worker: {0}", arg);
			arg = "";
			workerInstance = this.passiveWorker;
			if (workerInstance != null)
			{
				arg = string.Format("pid={0}", workerInstance.Pid);
			}
			this.diag.TraceDebug<string>(0L, "passive worker: {0}", arg);
			arg = "";
			workerInstance = this.retiredWorker;
			if (workerInstance != null)
			{
				arg = string.Format("pid={0}", workerInstance.Pid);
			}
			this.diag.TraceDebug<string>(0L, "retired worker: {0}", arg);
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x0006375C File Offset: 0x0006195C
		private void RefreshStart()
		{
			this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.RefreshStartEnter, ExDateTime.UtcNow);
			try
			{
				this.diag.TraceDebug<int>(0L, "RefreshStart (state={0})", (int)this.state);
				this.TraceWorkers();
				lock (this)
				{
					if (!this.IsStoppedOrStopping())
					{
						if (this.activeWorker == null)
						{
							this.diag.TraceDebug(0L, "Skipping refresh because there is no active worker");
						}
						else if (this.SetState(WorkerProcessManager.States.RefreshWaitingPassiveConnected))
						{
							this.passiveWorker = this.StartWorkerInstance(0, true);
							this.TraceWorkers();
						}
					}
				}
			}
			finally
			{
				this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.RefreshStartExit, ExDateTime.UtcNow);
			}
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x00063824 File Offset: 0x00061A24
		private void RefreshWaitingPassiveConnected()
		{
			this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.RefreshWaitingPassiveConnected, ExDateTime.UtcNow);
			this.diag.TraceDebug<int>(0L, "RefreshWaitingPassiveConnected (state={0})", (int)this.state);
			lock (this)
			{
				this.TraceWorkers();
				if (!this.IsStoppedOrStopping())
				{
					WorkerInstance workerInstance = this.activeWorker;
					if (workerInstance != null)
					{
						this.SendMessage(workerInstance, WorkerProcessManager.ServiceToWorkerCommands.Retire, 0, WorkerProcessManager.ServiceToWorkerCommands.Retire.Length);
						this.retiredWorker = workerInstance;
						this.retiredWorker.IsActive = false;
						this.activeWorker = null;
						if (this.SetState(WorkerProcessManager.States.RefreshWaitingRetiredExit))
						{
							this.TraceWorkers();
						}
					}
					else
					{
						this.retiredWorker = null;
						this.RefreshWaitingRetiredExit();
					}
				}
			}
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x000638EC File Offset: 0x00061AEC
		private void AsyncRefreshWaitingRetiredExit(object context)
		{
			this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.AsyncRefreshWaitingRetiredExitEnter, ExDateTime.UtcNow);
			WorkerInstance workerInstance = (WorkerInstance)context;
			if (!this.WaitForProcessHandleClose(workerInstance.Pid))
			{
				this.diag.TraceDebug(0L, "Cannot verify the old handle is closed.");
			}
			lock (this)
			{
				if (this.IsStoppedOrStopping())
				{
					this.diag.TraceDebug(0L, "AsyncRefreshWaitingRetiredExit: Skipping because process manager is stopped.");
				}
				else
				{
					this.diag.TraceDebug(0L, "AsyncRefreshWaitingRetiredExit: prepare to force GC collection and then start active worker instance");
					this.RefreshWaitingRetiredExit();
				}
			}
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x0006398C File Offset: 0x00061B8C
		private void RefreshWaitingRetiredExit()
		{
			this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.RefreshWaitingRetiredExitEnter, ExDateTime.UtcNow);
			this.diag.TraceDebug<int>(0L, "RefreshWaitingRetiredExit (state={0})", (int)this.state);
			this.TraceWorkers();
			lock (this)
			{
				if (!this.IsStoppedOrStopping())
				{
					WorkerInstance workerInstance = this.passiveWorker;
					this.passiveWorker = null;
					this.SendMessage(workerInstance, WorkerProcessManager.ServiceToWorkerCommands.Activate, 0, WorkerProcessManager.ServiceToWorkerCommands.Activate.Length);
					workerInstance.IsActive = true;
					this.activeWorker = workerInstance;
					if (this.SetState(WorkerProcessManager.States.Idle))
					{
						this.diag.TraceDebug<int>(0L, "Refresh done (state={0})", (int)this.state);
						this.TraceWorkers();
					}
				}
			}
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x00063A54 File Offset: 0x00061C54
		private void RefreshExited(WorkerInstance workerInstance)
		{
			this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.RefreshExitedEnter, (int)this.state, ExDateTime.UtcNow);
			try
			{
				this.diag.TraceDebug<int, int>(0L, "RefreshExited (state={0}, pid={1})", (int)this.state, workerInstance.Pid);
				this.TraceWorkers();
				if (this.state == WorkerProcessManager.States.RefreshWaitingRetiredExit)
				{
					if (workerInstance == this.retiredWorker)
					{
						this.retiredWorker = null;
						this.diag.TraceError(0L, "Schedule a AsyncRefreshWaitingRetiredExit callback");
						this.asyncRestartTimer = new Timer(new TimerCallback(this.AsyncRefreshWaitingRetiredExit), workerInstance, 250, -1);
					}
					else if (this.activeWorker == null && this.passiveWorker == workerInstance)
					{
						lock (this)
						{
							if (!this.IsStoppedOrStopping())
							{
								if (this.SetState(WorkerProcessManager.States.Idle))
								{
									this.activeWorker = this.StartWorkerInstance(0, true);
								}
							}
						}
					}
				}
				else if (workerInstance == this.passiveWorker)
				{
					if (this.state == WorkerProcessManager.States.RefreshWaitingRetiredExit)
					{
						this.diag.TraceDebug<int>(0L, "Unhandled case is RefreshExited (state={0}): reset", (int)this.state);
						this.InternalReset();
					}
					else if (this.state == WorkerProcessManager.States.RefreshWaitingPassiveConnected)
					{
						this.diag.TraceDebug<int>(0L, "Unhandled case is RefreshExited (state={0}): disable refresh", (int)this.state);
						lock (this)
						{
							if (!this.IsStoppedOrStopping())
							{
								if (this.SetState(WorkerProcessManager.States.Idle))
								{
									this.refreshEnabled = false;
								}
							}
						}
					}
				}
			}
			finally
			{
				this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.RefreshExitedExit, (int)this.state, ExDateTime.UtcNow);
			}
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x00063C2C File Offset: 0x00061E2C
		private void HandleWorkerContacted(WorkerInstance workerInstance)
		{
			this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.HandleWorkerContactedEnter, ExDateTime.UtcNow);
			this.diag.TraceDebug<int>(0L, "Worker process (pid={0}) contacted.", workerInstance.Pid);
			this.hasWorkerEverContacted = true;
			if (this.state == WorkerProcessManager.States.RefreshWaitingPassiveConnected)
			{
				this.RefreshWaitingPassiveConnected();
			}
			if (this.OnWorkerContacted != null)
			{
				this.OnWorkerContacted();
			}
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x00063C8C File Offset: 0x00061E8C
		private void SaveLastWorkerProcessId(WorkerInstance workerInstance)
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Transport"))
				{
					if (registryKey.GetValue("LastWorkerProcessId") != null)
					{
						registryKey.DeleteValue("LastWorkerProcessId", false);
					}
					registryKey.SetValue("LastWorkerProcessId", workerInstance.Pid, RegistryValueKind.DWord);
				}
			}
			catch (Exception ex)
			{
				this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_FailedToStoreLastWorkerProcessId, null, new object[]
				{
					ex.ToString()
				});
			}
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x00063D2C File Offset: 0x00061F2C
		private void HandleWorkerExited(WorkerInstance workerInstance, bool resetRequested)
		{
			this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.HandleWorkerExitedEnter, resetRequested ? 1 : 0, ExDateTime.UtcNow);
			try
			{
				string stdOutText = workerInstance.StdOutText;
				string stdErrText = workerInstance.StdErrText;
				this.diag.TraceDebug(0L, "Worker process (pid={0}) {1}.\nStdOut: {2}\nStdErr: {3}", new object[]
				{
					workerInstance.Pid,
					resetRequested ? "requested reset (crashed)" : "exited",
					stdOutText,
					stdErrText
				});
				this.SaveLastWorkerProcessId(workerInstance);
				int num = 0;
				if (workerInstance.Process != null && workerInstance.Process.HasExited)
				{
					num = workerInstance.Process.ExitCode;
					this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.WorkerProcessExitCode, num);
				}
				if (!resetRequested)
				{
					string text;
					workerInstance.CloseProcess(out text);
					this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_ExistingWorkerProcessHasExitedValue, null, new object[]
					{
						text
					});
				}
				if (num == 196)
				{
					this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_WorkerProcessExitServiceTerminateWithUnhandledException, null, new object[]
					{
						workerInstance.Pid.ToString(CultureInfo.InvariantCulture)
					});
					throw new WorkerProcessRequestedAbnormalTerminationException("Worker process requested that the service terminate with unhandled exception on machine " + Environment.MachineName);
				}
				if (num == 199)
				{
					this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_WorkerProcessExitServiceStop, null, new object[]
					{
						workerInstance.Pid.ToString(CultureInfo.InvariantCulture)
					});
					if (this.stopServiceHandler != null)
					{
						this.stopServiceHandler(false, false, false, workerInstance);
					}
					Environment.Exit(0);
				}
				else
				{
					if (this.activeWorker == workerInstance && (num == 198 || num == 197 || num == 200))
					{
						this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_WorkerProcessExitServiceStop, null, new object[]
						{
							workerInstance.Pid.ToString(CultureInfo.InvariantCulture)
						});
						lock (this)
						{
							this.CleanWorkerInstance(workerInstance);
						}
						if (this.stopServiceHandler != null && this.stopServiceHandler(true, num == 197 || num == 200, num == 200, workerInstance))
						{
							Environment.Exit(0);
						}
						return;
					}
					if (resetRequested)
					{
						this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_WorkerProcessReplace, null, new object[]
						{
							workerInstance.Pid.ToString(CultureInfo.InvariantCulture)
						});
					}
					else
					{
						this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_WorkerProcessExit, null, new object[]
						{
							workerInstance.Pid.ToString(CultureInfo.InvariantCulture)
						});
					}
				}
				lock (this)
				{
					if (this.IsStoppedOrStopping())
					{
						this.CleanWorkerInstance(workerInstance);
						return;
					}
					if (resetRequested && this.crashingWorker != null)
					{
						this.diag.TraceDebug<int, int>(0L, "Worker process (pid={0}) requested reset, but another reset is still ongoing (pid={1}). Ignore request.", workerInstance.Pid, this.crashingWorker.Pid);
						return;
					}
					if (!resetRequested && this.crashingWorker == workerInstance)
					{
						this.diag.TraceDebug<int>(0L, "Worker process (pid={0}) finished reset.", workerInstance.Pid);
						this.crashingWorker = null;
					}
					if (resetRequested)
					{
						this.diag.TraceDebug<int>(0L, "Worker process (pid={0}) starting reset.", workerInstance.Pid);
						this.crashingWorker = workerInstance;
					}
					if (this.state == WorkerProcessManager.States.RefreshWaitingPassiveConnected || this.state == WorkerProcessManager.States.RefreshWaitingRetiredExit)
					{
						this.RefreshExited(workerInstance);
					}
					else if (this.activeWorker == workerInstance || this.activeWorker == null)
					{
						this.CleanWorkerInstance(workerInstance);
						this.hasWorkerCrashed = true;
						this.RestartActiveWorkerInstance(workerInstance);
					}
					this.CleanWorkerInstance(workerInstance);
					this.TraceWorkers();
				}
				if (this.OnWorkerExited != null)
				{
					this.OnWorkerExited();
				}
			}
			finally
			{
				this.DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs.HandleWorkerExitedExit, ExDateTime.UtcNow);
			}
		}

		// Token: 0x06002C16 RID: 11286 RVA: 0x00064150 File Offset: 0x00062350
		private void CleanWorkerInstance(WorkerInstance workerInstance)
		{
			lock (this)
			{
				if (workerInstance == this.activeWorker)
				{
					this.activeWorker = null;
				}
				else if (workerInstance == this.passiveWorker)
				{
					this.passiveWorker = null;
				}
				else if (workerInstance == this.retiredWorker)
				{
					this.retiredWorker = null;
				}
			}
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x000641BC File Offset: 0x000623BC
		private void SendCommand(byte[] command)
		{
			this.diag.TraceDebug<byte[]>(0L, "Sending Command {0}", command);
			lock (this)
			{
				if (this.activeWorker != null)
				{
					this.SendMessage(this.activeWorker, command, 0, command.Length);
				}
				if (this.passiveWorker != null)
				{
					this.SendMessage(this.passiveWorker, command, 0, command.Length);
				}
			}
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x00064238 File Offset: 0x00062438
		private bool SendMessage(WorkerInstance workerInstance, byte[] buffer, int offset, int count)
		{
			bool result = true;
			try
			{
				Monitor.Enter(workerInstance);
				if (!workerInstance.SendMessage(buffer, offset, count))
				{
					result = false;
				}
			}
			finally
			{
				if (Monitor.IsEntered(workerInstance))
				{
					Monitor.Exit(workerInstance);
				}
			}
			return result;
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x00064280 File Offset: 0x00062480
		private bool HandOverConnection(Socket connection, int destinationPid, WorkerInstance workerInstance)
		{
			SocketInformation socketInformation;
			try
			{
				socketInformation = connection.DuplicateAndClose(destinationPid);
			}
			catch (SocketException ex)
			{
				this.diag.TraceDebug<int>(0L, "DuplicateAndCloseHandle failed with error code: {0}", ex.ErrorCode);
				return false;
			}
			bool result;
			lock (this.socketInformationMemoryStream)
			{
				this.socketInformationMemoryStream.Position = 0L;
				this.socketInformationMemoryStream.WriteByte(78);
				this.socketInfoFormatter.Serialize(this.socketInformationMemoryStream, socketInformation);
				result = this.SendMessage(workerInstance, this.socketInformationMemoryStream.GetBuffer(), 0, (int)this.socketInformationMemoryStream.Position);
			}
			return result;
		}

		// Token: 0x06002C1A RID: 11290 RVA: 0x00064344 File Offset: 0x00062544
		private bool WaitForProcessHandleClose(int pid)
		{
			for (int i = 0; i < WorkerProcessManager.CheckProcessHandleMaxAttempts; i++)
			{
				lock (this)
				{
					if (this.IsStoppedOrStopping())
					{
						return false;
					}
				}
				if (this.VerifyProcessHandleClosed(pid))
				{
					this.diag.TraceDebug<int>(0L, "Handle for process {0} is closed", pid);
					return true;
				}
				this.diag.TraceDebug<int>(0L, "Handle for process {0} is not closed. Wait for 0.25 seconds.", pid);
				Thread.Sleep(250);
			}
			this.eventLogger.LogEvent(ProcessManagerServiceEventLogConstants.Tuple_WaitForProcessHandleCloseTimedOut, null, new object[]
			{
				pid.ToString(),
				ProcessManagerService.instance.ServiceConfiguration.CheckProcessHandleTimeOut
			});
			return false;
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x00064418 File Offset: 0x00062618
		private void WaitForWorkerReady(WorkerInstance worker)
		{
			if (worker == null)
			{
				return;
			}
			for (int i = 0; i < 30; i++)
			{
				if (worker.SignaledReady)
				{
					this.diag.TraceDebug<int>(0L, "Process {0} has already signaled ready.", worker.Pid);
					return;
				}
				this.diag.TraceDebug<int>(0L, "Wait for process {0} to signal ready. Sleep 1 second", worker.Pid);
				Thread.Sleep(1000);
			}
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x0006447C File Offset: 0x0006267C
		private bool VerifyProcessHandleClosed(int pid)
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();
			bool result;
			using (SafeProcessHandle safeProcessHandle = NativeMethods.OpenProcess(NativeMethods.ProcessAccess.QueryInformation, false, (uint)pid))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				this.diag.TraceDebug<int>(0L, "OpenProcess finishes with error code: {0}", lastWin32Error);
				if (lastWin32Error == 87 && (safeProcessHandle == null || safeProcessHandle.IsInvalid))
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06002C1D RID: 11293 RVA: 0x000644EC File Offset: 0x000626EC
		private void DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs bc, int data)
		{
			this.DropBreadCrumb((WorkerProcessManager.WorkerProcessManagerBreadcrumbs)ProcessManagerService.GetEncodedInt((int)bc, data));
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x000644FC File Offset: 0x000626FC
		private void DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs bc, int data, ExDateTime time)
		{
			int encodedInt = ProcessManagerService.GetEncodedInt((int)bc, data);
			WorkerProcessManager.WorkerProcessManagerBreadcrumbs encodedDateTime = (WorkerProcessManager.WorkerProcessManagerBreadcrumbs)ProcessManagerService.GetEncodedDateTime(encodedInt, time);
			this.DropBreadCrumb(encodedDateTime);
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x00064520 File Offset: 0x00062720
		private void DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs bc, ExDateTime time)
		{
			WorkerProcessManager.WorkerProcessManagerBreadcrumbs encodedDateTime = (WorkerProcessManager.WorkerProcessManagerBreadcrumbs)ProcessManagerService.GetEncodedDateTime((int)bc, time);
			this.DropBreadCrumb(encodedDateTime);
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x0006453C File Offset: 0x0006273C
		private void DropBreadCrumb(WorkerProcessManager.WorkerProcessManagerBreadcrumbs bc)
		{
			this.breadcrumbs.Drop(bc);
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x0006454A File Offset: 0x0006274A
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.asyncRestartTimer != null)
			{
				this.asyncRestartTimer.Dispose();
				this.asyncRestartTimer = null;
			}
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x00064569 File Offset: 0x00062769
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<WorkerProcessManager>(this);
		}

		// Token: 0x0400262C RID: 9772
		private const int CheckProcessHandleSleepInterval = 250;

		// Token: 0x0400262D RID: 9773
		private const long TraceId = 0L;

		// Token: 0x0400262E RID: 9774
		private const int FirstSendRetryInterval = 200;

		// Token: 0x0400262F RID: 9775
		private const int SendRetryInterval = 500;

		// Token: 0x04002630 RID: 9776
		private const int MaxSendRetryCount = 120;

		// Token: 0x04002631 RID: 9777
		private const int MonitorIntervalSec = 60;

		// Token: 0x04002632 RID: 9778
		private const int ShutdownWorkerReadyWaitSec = 30;

		// Token: 0x04002633 RID: 9779
		public const int TerminateServiceWithException = 196;

		// Token: 0x04002634 RID: 9780
		public const int RetryServiceAlwaysStopProcessExitCode = 197;

		// Token: 0x04002635 RID: 9781
		public const int RetryableServiceStopProcessExitCode = 198;

		// Token: 0x04002636 RID: 9782
		public const int ServiceStopProcessExitCode = 199;

		// Token: 0x04002637 RID: 9783
		public const int ImmediateRetryServiceAlwaysStopProcessExitCode = 200;

		// Token: 0x0400263B RID: 9787
		private WorkerProcessManager.StopServiceHandler stopServiceHandler;

		// Token: 0x0400263C RID: 9788
		private static readonly int CheckProcessHandleMaxAttempts = ProcessManagerService.instance.ServiceConfiguration.CheckProcessHandleTimeOut * 1000 / 250;

		// Token: 0x0400263D RID: 9789
		private WorkerProcessManager.ProcessNeedsSwapCheckDelegate processNeedsSwapCheckDelegate;

		// Token: 0x0400263E RID: 9790
		private IFormatter socketInfoFormatter = ExchangeBinaryFormatterFactory.CreateBinaryFormatter(null);

		// Token: 0x0400263F RID: 9791
		private WorkerProcessManager.States state;

		// Token: 0x04002640 RID: 9792
		private WorkerInstance activeWorker;

		// Token: 0x04002641 RID: 9793
		private WorkerInstance passiveWorker;

		// Token: 0x04002642 RID: 9794
		private WorkerInstance retiredWorker;

		// Token: 0x04002643 RID: 9795
		private WorkerInstance crashingWorker;

		// Token: 0x04002644 RID: 9796
		private bool refreshEnabled = true;

		// Token: 0x04002645 RID: 9797
		private bool isPaused;

		// Token: 0x04002646 RID: 9798
		private string workerPath;

		// Token: 0x04002647 RID: 9799
		private SafeJobHandle jobObject;

		// Token: 0x04002648 RID: 9800
		private Microsoft.Exchange.Diagnostics.Trace diag;

		// Token: 0x04002649 RID: 9801
		private ExEventLog eventLogger;

		// Token: 0x0400264A RID: 9802
		private MemoryStream socketInformationMemoryStream;

		// Token: 0x0400264B RID: 9803
		private Timer monitorTimer;

		// Token: 0x0400264C RID: 9804
		private int monitorBusy;

		// Token: 0x0400264D RID: 9805
		private Timer asyncRestartTimer;

		// Token: 0x0400264E RID: 9806
		private bool hasWorkerCrashed;

		// Token: 0x0400264F RID: 9807
		private bool hasWorkerEverContacted;

		// Token: 0x04002650 RID: 9808
		private Breadcrumbs<WorkerProcessManager.WorkerProcessManagerBreadcrumbs> breadcrumbs = new Breadcrumbs<WorkerProcessManager.WorkerProcessManagerBreadcrumbs>(128);

		// Token: 0x0200081E RID: 2078
		// (Invoke) Token: 0x06002C25 RID: 11301
		public delegate bool ProcessNeedsSwapCheckDelegate(Process process);

		// Token: 0x0200081F RID: 2079
		// (Invoke) Token: 0x06002C29 RID: 11305
		public delegate bool StopServiceHandler(bool canRetry, bool retryAlways, bool retryImmediately, WorkerInstance workerProcess);

		// Token: 0x02000820 RID: 2080
		// (Invoke) Token: 0x06002C2D RID: 11309
		public delegate void DisconnectPerformanceCountersHandler();

		// Token: 0x02000821 RID: 2081
		// (Invoke) Token: 0x06002C31 RID: 11313
		public delegate void WorkerEventHandler();

		// Token: 0x02000822 RID: 2082
		private enum States
		{
			// Token: 0x04002652 RID: 9810
			Init,
			// Token: 0x04002653 RID: 9811
			Idle,
			// Token: 0x04002654 RID: 9812
			RefreshWaitingPassiveConnected,
			// Token: 0x04002655 RID: 9813
			RefreshWaitingRetiredExit,
			// Token: 0x04002656 RID: 9814
			Stopping,
			// Token: 0x04002657 RID: 9815
			Stopped
		}

		// Token: 0x02000823 RID: 2083
		private enum WorkerProcessManagerBreadcrumbs
		{
			// Token: 0x04002659 RID: 9817
			SetState = 1000000,
			// Token: 0x0400265A RID: 9818
			ReInit = 2000000,
			// Token: 0x0400265B RID: 9819
			StartEnter = 3000000,
			// Token: 0x0400265C RID: 9820
			StartExit = 4000000,
			// Token: 0x0400265D RID: 9821
			StopInstanceEnter = 5000000,
			// Token: 0x0400265E RID: 9822
			StopInstanceExit = 6000000,
			// Token: 0x0400265F RID: 9823
			StopEnter = 7000000,
			// Token: 0x04002660 RID: 9824
			StopExit = 8000000,
			// Token: 0x04002661 RID: 9825
			WorkerInstancesTriggerDumpAndWaitEnter = 9000000,
			// Token: 0x04002662 RID: 9826
			WorkerInstancesTriggerDumpAndWaitExit = 10000000,
			// Token: 0x04002663 RID: 9827
			InitiatePauseEnter = 11000000,
			// Token: 0x04002664 RID: 9828
			InitiateContinueEnter = 12000000,
			// Token: 0x04002665 RID: 9829
			InitiateMemoryPressureHandlerEnter = 13000000,
			// Token: 0x04002666 RID: 9830
			InitiateSubmissionQueueBlockedHandlerEnter = 14000000,
			// Token: 0x04002667 RID: 9831
			RestartActiveWorkerInstanceEnter = 15000000,
			// Token: 0x04002668 RID: 9832
			RefreshStartEnter = 16000000,
			// Token: 0x04002669 RID: 9833
			RefreshStartExit = 17000000,
			// Token: 0x0400266A RID: 9834
			RefreshWaitingPassiveConnected = 18000000,
			// Token: 0x0400266B RID: 9835
			AsyncRefreshWaitingRetiredExitEnter = 19000000,
			// Token: 0x0400266C RID: 9836
			RefreshWaitingRetiredExitEnter = 20000000,
			// Token: 0x0400266D RID: 9837
			RefreshExitedEnter = 21000000,
			// Token: 0x0400266E RID: 9838
			RefreshExitedExit = 22000000,
			// Token: 0x0400266F RID: 9839
			HandleWorkerExitedEnter = 23000000,
			// Token: 0x04002670 RID: 9840
			WorkerProcessExitCode = 24000000,
			// Token: 0x04002671 RID: 9841
			HandleWorkerExitedExit = 25000000,
			// Token: 0x04002672 RID: 9842
			InternalResetEnter = 26000000,
			// Token: 0x04002673 RID: 9843
			InternalResetExit = 27000000,
			// Token: 0x04002674 RID: 9844
			AsyncStartActiveWorkerEnter = 28000000,
			// Token: 0x04002675 RID: 9845
			HandleWorkerContactedEnter = 29000000,
			// Token: 0x04002676 RID: 9846
			InitiateLogFlush = 30000000,
			// Token: 0x04002677 RID: 9847
			ForcedCrash = 31000000
		}

		// Token: 0x02000824 RID: 2084
		private class ServiceToWorkerCommands
		{
			// Token: 0x04002678 RID: 9848
			public const byte NewConnection = 78;

			// Token: 0x04002679 RID: 9849
			public static readonly byte[] Retire = new byte[]
			{
				82
			};

			// Token: 0x0400267A RID: 9850
			public static readonly byte[] Activate = new byte[]
			{
				65
			};

			// Token: 0x0400267B RID: 9851
			public static readonly byte[] ConfigUpdate = new byte[]
			{
				85
			};

			// Token: 0x0400267C RID: 9852
			public static readonly byte[] Pause = new byte[]
			{
				80
			};

			// Token: 0x0400267D RID: 9853
			public static readonly byte[] Continue = new byte[]
			{
				67
			};

			// Token: 0x0400267E RID: 9854
			public static readonly byte[] HandleMemoryPressure = new byte[]
			{
				77
			};

			// Token: 0x0400267F RID: 9855
			public static readonly byte[] HandleSubmissionQueueBlocked = new byte[]
			{
				81
			};

			// Token: 0x04002680 RID: 9856
			public static readonly byte[] ClearConfigCache = new byte[]
			{
				76
			};

			// Token: 0x04002681 RID: 9857
			public static readonly byte[] ForceCrash = new byte[]
			{
				87
			};

			// Token: 0x04002682 RID: 9858
			public static readonly byte[] LogFlush = new byte[]
			{
				70
			};
		}
	}
}
