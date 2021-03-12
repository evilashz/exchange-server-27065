using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.ProcessManager
{
	// Token: 0x020007FF RID: 2047
	internal class ComProcessManager<IComInterface> : DisposeTrackableBase
	{
		// Token: 0x06002AF8 RID: 11000 RVA: 0x0005DC50 File Offset: 0x0005BE50
		public ComProcessManager(int maxWorkerProcessNumber, ComWorkerConfiguration workerConfiguration, Trace tracer)
		{
			if (maxWorkerProcessNumber <= 0)
			{
				throw new ArgumentException("Invalid max work process count", "maxWorkerProcessNumber");
			}
			if (workerConfiguration == null)
			{
				throw new ArgumentNullException("Invalid worker configuration", "workerConfiguration");
			}
			this.workerConfiguration = workerConfiguration;
			this.tracer = tracer;
			this.semaphore = new Semaphore(maxWorkerProcessNumber, maxWorkerProcessNumber);
			try
			{
				this.jobObjectManager = new JobObjectManager(this.workerConfiguration.WorkerMemoryLimit);
			}
			catch (Win32Exception inner)
			{
				throw new ComProcessManagerInitializationException("ComProcessManager initialization fails because fail to intialize job object manager.", inner);
			}
			this.jobObjectManager.Tracer = this.tracer;
			JobObjectManager jobObjectManager = this.jobObjectManager;
			jobObjectManager.CallbackMonitorEvent = (CallbackMontiorEvent)Delegate.Combine(jobObjectManager.CallbackMonitorEvent, new CallbackMontiorEvent(this.JobObjectMonitorEventCallback));
			this.currentProcessHandle = NativeMethods.OpenProcess(NativeMethods.ProcessAccess.Synchronize, true, NativeMethods.GetCurrentProcessId());
			if (workerConfiguration.RunAsLocalService)
			{
				bool flag = NativeMethods.LogonUser("LocalService", "NT AUTHORITY", null, 5, 0, out this.workerProcessToken);
				if (!flag || this.workerProcessToken.IsInvalid)
				{
					throw new ComProcessManagerInitializationException("ComProcessManager initialization fails because fail to logon LocalService account.", new Win32Exception());
				}
			}
			this.workerProcessMap = new Dictionary<int, ComProcessAgent<IComInterface>>(maxWorkerProcessNumber);
			if (this.workerConfiguration.WorkerAllocationTimeout != 0 || this.workerConfiguration.WorkerIdleTimeout != 0)
			{
				this.processMonitorTimer = new Timer(new TimerCallback(this.ProcessTimerCallback), this, 5000, 5000);
			}
			this.freeWorkers = new List<ComProcessAgent<IComInterface>>();
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x0005DDBC File Offset: 0x0005BFBC
		public bool ExecuteRequest(object requestParameters)
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("ComProcessManager");
			}
			ComProcessAgent<IComInterface> comProcessAgent = null;
			bool flag = true;
			if (!this.semaphore.WaitOne(this.workerConfiguration.WorkerAllocationTimeout, false))
			{
				this.TraceError(this, "Unable to obtain a worker to perform a request", new object[0]);
				throw new ComProcessBusyException("No workers available for execution");
			}
			bool result;
			try
			{
				lock (this.LockObject)
				{
					while (this.freeWorkers.Count > 0)
					{
						comProcessAgent = this.freeWorkers[0];
						this.freeWorkers.RemoveAt(0);
						if (comProcessAgent.IsValid)
						{
							break;
						}
						comProcessAgent.Dispose();
						comProcessAgent = null;
					}
				}
				if (comProcessAgent == null)
				{
					comProcessAgent = new ComProcessAgent<IComInterface>(this.workerConfiguration, this.jobObjectManager, this.currentProcessHandle, this.workerProcessToken, this.CreateWorkerCallback, requestParameters, new EventHandler<EventArgs>(this.OnWorkerProcessTerminated), this.tracer);
					lock (this.LockObject)
					{
						this.workerProcessMap[comProcessAgent.ProcessId] = comProcessAgent;
					}
				}
				bool flag4 = comProcessAgent.ExecuteRequest(this.ExecuteRequestCallback, requestParameters);
				flag = false;
				if (flag4)
				{
					if (comProcessAgent.IsExpiredLifetimeOrTransactionCount(ExDateTime.Now.LocalTime))
					{
						if (this.DestroyWorkerCallback != null)
						{
							this.DestroyWorkerCallback(comProcessAgent, requestParameters, false);
						}
						comProcessAgent.TerminateWorkerProcess(false);
					}
					else
					{
						lock (this.LockObject)
						{
							this.freeWorkers.Insert(0, comProcessAgent);
						}
					}
				}
				result = flag4;
			}
			finally
			{
				this.semaphore.Release();
				if (flag && this.DestroyWorkerCallback != null)
				{
					this.DestroyWorkerCallback(null, requestParameters, true);
				}
			}
			return result;
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x0005DFE4 File Offset: 0x0005C1E4
		private void JobObjectMonitorEventCallback(MonitorEvent monitorEvent, params object[] args)
		{
			switch (monitorEvent)
			{
			case MonitorEvent.MonitorStart:
				this.TraceInfo(this, "Memory monitor thread starting.", new object[0]);
				return;
			case MonitorEvent.MonitorStop:
				this.TraceInfo(this, "Memory monitor thread stopping.", new object[0]);
				return;
			case MonitorEvent.ReachMemoryLimitation:
			{
				ComProcessAgent<IComInterface> comProcessAgent = null;
				int num = (int)args[0];
				this.TraceError(this, "Process {0} exceeded the memory limitation, will be killed", new object[]
				{
					num
				});
				lock (this.LockObject)
				{
					if (this.workerProcessMap.TryGetValue(num, out comProcessAgent))
					{
						this.workerProcessMap.Remove(num);
					}
				}
				if (comProcessAgent != null)
				{
					comProcessAgent.IsWorkerBeyondMemoryLimit = true;
					comProcessAgent.TerminateWorkerProcess(true);
					return;
				}
				break;
			}
			default:
				this.TraceError(this, "Invalid Job object event {0}.", new object[]
				{
					monitorEvent
				});
				break;
			}
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x0005E0DC File Offset: 0x0005C2DC
		private void OnWorkerProcessTerminated(object sender, EventArgs args)
		{
			ComProcessAgent<IComInterface> comProcessAgent = (ComProcessAgent<IComInterface>)sender;
			if (comProcessAgent != null)
			{
				lock (this.LockObject)
				{
					this.workerProcessMap.Remove(comProcessAgent.ProcessId);
					this.freeWorkers.Remove(comProcessAgent);
				}
				comProcessAgent.Dispose();
			}
		}

		// Token: 0x06002AFC RID: 11004 RVA: 0x0005E148 File Offset: 0x0005C348
		private void ProcessTimerCallback(object state)
		{
			List<ComProcessAgent<IComInterface>> list = null;
			DateTime localTime = ExDateTime.Now.LocalTime;
			lock (this.LockObject)
			{
				for (int i = this.freeWorkers.Count - 1; i >= 0; i--)
				{
					ComProcessAgent<IComInterface> comProcessAgent = this.freeWorkers[i];
					if (comProcessAgent.IsExpiredLifetimeOrIdleTime(localTime))
					{
						this.freeWorkers.RemoveAt(i);
						if (list == null)
						{
							list = new List<ComProcessAgent<IComInterface>>();
						}
						list.Add(comProcessAgent);
					}
				}
				if (list != null)
				{
					foreach (ComProcessAgent<IComInterface> comProcessAgent2 in list)
					{
						if (this.DestroyWorkerCallback != null)
						{
							this.DestroyWorkerCallback(comProcessAgent2, null, false);
						}
						comProcessAgent2.TerminateWorkerProcess(false);
					}
				}
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06002AFD RID: 11005 RVA: 0x0005E23C File Offset: 0x0005C43C
		internal ComWorkerConfiguration WorkerConfiguration
		{
			get
			{
				return this.workerConfiguration;
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06002AFE RID: 11006 RVA: 0x0005E244 File Offset: 0x0005C444
		private object LockObject
		{
			get
			{
				return this.workerProcessMap;
			}
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x0005E24C File Offset: 0x0005C44C
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				for (int i = this.freeWorkers.Count - 1; i >= 0; i--)
				{
					this.freeWorkers[i].TerminateWorkerProcess(false);
				}
				if (this.jobObjectManager != null)
				{
					this.jobObjectManager.Dispose();
					this.jobObjectManager = null;
				}
				if (this.currentProcessHandle != null)
				{
					this.currentProcessHandle.Dispose();
					this.currentProcessHandle = null;
				}
				if (this.workerProcessToken != null)
				{
					this.workerProcessToken.Dispose();
					this.workerProcessToken = null;
				}
				if (this.semaphore != null)
				{
					this.semaphore.Close();
					this.semaphore = null;
				}
				if (this.processMonitorTimer != null)
				{
					this.processMonitorTimer.Dispose();
					this.processMonitorTimer = null;
				}
			}
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x0005E30B File Offset: 0x0005C50B
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ComProcessManager<IComInterface>>(this);
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x0005E313 File Offset: 0x0005C513
		internal void TraceInfo(object target, string formatString, params object[] args)
		{
			if (this.tracer != null)
			{
				this.tracer.Information((long)((target != null) ? target.GetHashCode() : 0), formatString, args);
			}
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x0005E337 File Offset: 0x0005C537
		private void TraceError(object target, string formatString, params object[] args)
		{
			if (this.tracer != null)
			{
				this.tracer.TraceError((long)((target != null) ? target.GetHashCode() : 0), formatString, args);
			}
		}

		// Token: 0x04002566 RID: 9574
		private const int ProcessMonitorTimerFrequency = 5000;

		// Token: 0x04002567 RID: 9575
		public ComProcessManager<IComInterface>.OnCreateWorker CreateWorkerCallback;

		// Token: 0x04002568 RID: 9576
		public ComProcessManager<IComInterface>.OnDestroyWorker DestroyWorkerCallback;

		// Token: 0x04002569 RID: 9577
		public ComProcessManager<IComInterface>.OnExecuteRequest ExecuteRequestCallback;

		// Token: 0x0400256A RID: 9578
		private JobObjectManager jobObjectManager;

		// Token: 0x0400256B RID: 9579
		private SafeProcessHandle currentProcessHandle;

		// Token: 0x0400256C RID: 9580
		private SafeUserTokenHandle workerProcessToken;

		// Token: 0x0400256D RID: 9581
		private Semaphore semaphore;

		// Token: 0x0400256E RID: 9582
		private List<ComProcessAgent<IComInterface>> freeWorkers;

		// Token: 0x0400256F RID: 9583
		private Dictionary<int, ComProcessAgent<IComInterface>> workerProcessMap;

		// Token: 0x04002570 RID: 9584
		private Trace tracer;

		// Token: 0x04002571 RID: 9585
		private ComWorkerConfiguration workerConfiguration;

		// Token: 0x04002572 RID: 9586
		private Timer processMonitorTimer;

		// Token: 0x02000800 RID: 2048
		// (Invoke) Token: 0x06002B04 RID: 11012
		public delegate void OnCreateWorker(IComWorker<IComInterface> worker, object requestParameters);

		// Token: 0x02000801 RID: 2049
		// (Invoke) Token: 0x06002B08 RID: 11016
		public delegate bool OnExecuteRequest(IComWorker<IComInterface> worker, object requestParameters);

		// Token: 0x02000802 RID: 2050
		// (Invoke) Token: 0x06002B0C RID: 11020
		public delegate void OnDestroyWorker(IComWorker<IComInterface> worker, object requestParameters, bool forcedTermination);
	}
}
