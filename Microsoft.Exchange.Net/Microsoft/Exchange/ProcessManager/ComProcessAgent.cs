using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.ProcessManager
{
	// Token: 0x020007F7 RID: 2039
	internal class ComProcessAgent<IComInterface> : DisposeTrackableBase, IComWorker<IComInterface>
	{
		// Token: 0x1400008E RID: 142
		// (add) Token: 0x06002AC7 RID: 10951 RVA: 0x0005D12C File Offset: 0x0005B32C
		// (remove) Token: 0x06002AC8 RID: 10952 RVA: 0x0005D164 File Offset: 0x0005B364
		private event EventHandler<EventArgs> WorkerProcessTerminatedEvent;

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06002AC9 RID: 10953 RVA: 0x0005D199 File Offset: 0x0005B399
		public IComInterface Worker
		{
			get
			{
				base.CheckDisposed();
				return this.worker;
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06002ACA RID: 10954 RVA: 0x0005D1A7 File Offset: 0x0005B3A7
		public SafeProcessHandle SafeProcessHandle
		{
			get
			{
				base.CheckDisposed();
				return this.workerProcess;
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06002ACB RID: 10955 RVA: 0x0005D1B5 File Offset: 0x0005B3B5
		public int ProcessId
		{
			get
			{
				base.CheckDisposed();
				return this.workerProcessId;
			}
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06002ACC RID: 10956 RVA: 0x0005D1C4 File Offset: 0x0005B3C4
		internal bool IsValid
		{
			get
			{
				return !base.IsDisposed && this.worker != null && this.workerProcess != null && !this.workerProcess.IsInvalid && !this.workerProcess.HasExited;
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06002ACD RID: 10957 RVA: 0x0005D211 File Offset: 0x0005B411
		// (set) Token: 0x06002ACE RID: 10958 RVA: 0x0005D219 File Offset: 0x0005B419
		internal bool IsWorkerBeyondMemoryLimit
		{
			get
			{
				return this.isWorkerBeyondMemoryLimit;
			}
			set
			{
				this.isWorkerBeyondMemoryLimit = value;
			}
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x0005D222 File Offset: 0x0005B422
		internal ComProcessAgent(ComWorkerConfiguration configuration, JobObjectManager jobObjectManger, SafeProcessHandle currentProcessHandle, SafeUserTokenHandle workerProcessToken, ComProcessManager<IComInterface>.OnCreateWorker createWorker, object requestParameter, EventHandler<EventArgs> workerProcessTerminatedEventHandler, Trace tracer)
		{
			this.configuration = configuration;
			this.jobObjectManger = jobObjectManger;
			this.currentProcessHandle = currentProcessHandle;
			this.workerProcessToken = workerProcessToken;
			this.WorkerProcessTerminatedEvent += workerProcessTerminatedEventHandler;
			this.tracer = tracer;
			this.LaunchWorkerProcess(createWorker, requestParameter);
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x0005D264 File Offset: 0x0005B464
		private void LaunchWorkerProcess(ComProcessManager<IComInterface>.OnCreateWorker createWorker, object requestParameters)
		{
			bool flag = false;
			try
			{
				this.InternalLaunchWorkProcess(createWorker, requestParameters);
				flag = true;
			}
			catch (Win32Exception e)
			{
				this.LaunchWorkProcessFailed(e);
			}
			catch (ArgumentException e2)
			{
				this.LaunchWorkProcessFailed(e2);
			}
			catch (NotSupportedException e3)
			{
				this.LaunchWorkProcessFailed(e3);
			}
			catch (MemberAccessException e4)
			{
				this.LaunchWorkProcessFailed(e4);
			}
			catch (InvalidComObjectException e5)
			{
				this.LaunchWorkProcessFailed(e5);
			}
			catch (COMException e6)
			{
				this.LaunchWorkProcessFailed(e6);
			}
			catch (TypeLoadException e7)
			{
				this.LaunchWorkProcessFailed(e7);
			}
			finally
			{
				if (!flag)
				{
					this.TerminateWorkerProcess(true);
				}
			}
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x0005D340 File Offset: 0x0005B540
		private void InternalLaunchWorkProcess(ComProcessManager<IComInterface>.OnCreateWorker createWorker, object requestParameters)
		{
			Guid guid = Guid.Empty;
			string workerProcessPath = this.WorkerConfiguration.WorkerProcessPath;
			EventWaitHandle eventWaitHandle = null;
			EventWaitHandle eventWaitHandle2 = null;
			guid = Guid.NewGuid();
			string eventName = string.Format("Local\\Local_{0}-AddToJobObject", guid);
			eventWaitHandle = this.InitializeEvent(eventName);
			eventName = string.Format("Local\\Local_{0}-ComRegister", guid);
			eventWaitHandle2 = this.InitializeEvent(eventName);
			if (eventWaitHandle == null || eventWaitHandle2 == null)
			{
				throw new ComInterfaceInitializeException("Can't Create an unique wait events for worker process!");
			}
			string text = string.Format("-EventID {0} -PID {1}", guid, this.currentProcessHandle.DangerousGetHandle());
			if (this.WorkerConfiguration.ExtraCommandLineParameters != null)
			{
				text = text + " " + this.WorkerConfiguration.ExtraCommandLineParameters;
			}
			if (this.ProcessLaunchMutex != null)
			{
				try
				{
					this.ProcessLaunchMutex.WaitOne();
				}
				catch (AbandonedMutexException ex)
				{
					this.TraceError(this, "AbandonedMutexException caught: {0}", new object[]
					{
						ex
					});
				}
			}
			try
			{
				if (this.WorkerConfiguration.RunAsLocalService)
				{
					this.workerProcess = SafeProcessHandle.CreateProcessAsUser(this.workerProcessToken, workerProcessPath, string.Format("{0} {1}", workerProcessPath, text));
				}
				else
				{
					this.workerProcess = SafeProcessHandle.CreateProcess(workerProcessPath, string.Format("{0} {1}", workerProcessPath, text));
				}
				this.workerProcessId = this.workerProcess.GetProcessId();
				this.jobObject = this.jobObjectManger.CreateJobObject(this.workerProcess, this.WorkerConfiguration.MayRunUnderAnotherJobObject);
				eventWaitHandle.Set();
				IntPtr[] array = new IntPtr[]
				{
					eventWaitHandle2.SafeWaitHandle.DangerousGetHandle(),
					this.workerProcess.DangerousGetHandle()
				};
				switch (NativeMethods.WaitForMultipleObjects((uint)array.Length, array, false, (uint)ComProcessAgent<IComInterface>.MaxWaitingTimeForWorkerProcessRegister.TotalMilliseconds))
				{
				case 0U:
					try
					{
						Type workerType = this.WorkerConfiguration.WorkerType;
						this.worker = (IComInterface)((object)Activator.CreateInstance(workerType));
					}
					catch (COMException inner)
					{
						throw new ComInterfaceInitializeException("Active worker object failed", inner);
					}
					break;
				case 1U:
					throw new ComInterfaceInitializeException("Work process exit before class id registered!");
				default:
					throw new ComInterfaceInitializeException("Wait too long to register the class id");
				}
			}
			finally
			{
				if (this.ProcessLaunchMutex != null)
				{
					this.ProcessLaunchMutex.ReleaseMutex();
				}
				if (eventWaitHandle != null)
				{
					eventWaitHandle.Close();
				}
				if (eventWaitHandle2 != null)
				{
					eventWaitHandle2.Close();
				}
			}
			if (createWorker != null)
			{
				createWorker(this, requestParameters);
			}
			DateTime localTime = ExDateTime.Now.LocalTime;
			this.transactionCount = 0;
			this.ResetProcessExpirationTime(new DateTime?(localTime));
			this.ResetIdleExpirationTime(new DateTime?(localTime));
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x0005D614 File Offset: 0x0005B814
		internal bool ExecuteRequest(ComProcessManager<IComInterface>.OnExecuteRequest requestDelegate, object requestParameters)
		{
			ComProcessAgent<IComInterface>.TransactionState transactionState = new ComProcessAgent<IComInterface>.TransactionState(this.transactionCount);
			Timer timer = new Timer(new TimerCallback(this.TimerProc), transactionState, this.WorkerConfiguration.TransactionTimeout, -1);
			bool flag = false;
			Exception ex = null;
			try
			{
				flag = requestDelegate(this, requestParameters);
			}
			catch (Exception ex2)
			{
				this.TraceError(this, "Request {0} terminated with an exception {1}", new object[]
				{
					transactionState.TransactionNumber,
					ex2
				});
				ex = ex2;
				throw;
			}
			finally
			{
				lock (transactionState)
				{
					this.transactionCount++;
				}
				if (transactionState.TimedOut)
				{
					string text = string.Format("Request {0} has timed out", transactionState.TransactionNumber);
					this.TraceError(this, text, new object[0]);
					throw new ComProcessTimeoutException(text, ex);
				}
				if (this.isWorkerBeyondMemoryLimit)
				{
					string text2 = string.Format("Worker process (PID = {0}) reaches the memory limit.", this.workerProcessId);
					this.TraceError(this, text2, new object[0]);
					throw new ComProcessBeyondMemoryLimitException(text2, ex);
				}
				if (ex != null)
				{
					this.TerminateWorkerProcess(true);
				}
				else if (!flag)
				{
					this.TerminateWorkerProcess(false);
				}
				this.ResetIdleExpirationTime(new DateTime?(ExDateTime.Now.LocalTime));
				timer.Dispose();
			}
			return flag;
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x0005D78C File Offset: 0x0005B98C
		private void TimerProc(object state)
		{
			ComProcessAgent<IComInterface>.TransactionState transactionState = (ComProcessAgent<IComInterface>.TransactionState)state;
			lock (transactionState)
			{
				if (this.transactionCount > transactionState.TransactionNumber)
				{
					return;
				}
				transactionState.SetTimedOut();
			}
			this.TerminateWorkerProcess(true);
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x0005D7E8 File Offset: 0x0005B9E8
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				if (this.workerProcess != null)
				{
					this.workerProcess.Dispose();
					this.workerProcess = null;
				}
				if (this.jobObject != null)
				{
					this.jobObject.Dispose();
				}
			}
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x0005D81A File Offset: 0x0005BA1A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ComProcessAgent<IComInterface>>(this);
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x0005D824 File Offset: 0x0005BA24
		private void LaunchWorkProcessFailed(Exception e)
		{
			this.TraceError(this, "Launch work process failed! error is {0}", new object[]
			{
				e
			});
			throw new ComInterfaceInitializeException("Launch work process failed", e);
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x0005D854 File Offset: 0x0005BA54
		private EventWaitHandle InitializeEvent(string eventName)
		{
			bool flag = false;
			EventWaitHandle eventWaitHandle = null;
			EventWaitHandleSecurity eventWaitHandleSecurity = null;
			if (this.WorkerConfiguration.RunAsLocalService)
			{
				eventWaitHandleSecurity = new EventWaitHandleSecurity();
				EventWaitHandleAccessRule rule = new EventWaitHandleAccessRule("NT AUTHORITY\\LocalService", EventWaitHandleRights.FullControl, AccessControlType.Allow);
				eventWaitHandleSecurity.AddAccessRule(rule);
			}
			try
			{
				eventWaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset, eventName, ref flag, eventWaitHandleSecurity);
			}
			catch (UnauthorizedAccessException)
			{
				return null;
			}
			catch (WaitHandleCannotBeOpenedException)
			{
				return null;
			}
			if (!flag && eventWaitHandle != null)
			{
				eventWaitHandle.Close();
				eventWaitHandle = null;
			}
			return eventWaitHandle;
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x0005D8DC File Offset: 0x0005BADC
		public void TerminateWorkerProcess(bool forceTermination)
		{
			if (base.IsDisposed)
			{
				return;
			}
			try
			{
				if (!forceTermination && this.worker != null)
				{
					try
					{
						Marshal.ReleaseComObject(this.worker);
					}
					catch (SystemException)
					{
					}
				}
				this.KillProcess(this.workerProcess);
				if (this.WorkerProcessTerminatedEvent != null)
				{
					this.WorkerProcessTerminatedEvent(this, null);
				}
			}
			finally
			{
				this.worker = default(IComInterface);
			}
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x0005D964 File Offset: 0x0005BB64
		private void KillProcess(SafeProcessHandle process)
		{
			if (process != null)
			{
				try
				{
					process.TerminateProcess(0U);
				}
				catch (Win32Exception ex)
				{
					this.TraceError(this, "Kill process failed reason is {0}", new object[]
					{
						ex.ToString()
					});
				}
				catch (InvalidOperationException ex2)
				{
					this.TraceError(this, "Target process is invalid. {0}", new object[]
					{
						ex2.ToString()
					});
				}
			}
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x0005D9DC File Offset: 0x0005BBDC
		internal bool IsExpiredLifetimeOrIdleTime(DateTime now)
		{
			return (this.processExpirationTime != null && DateTime.Compare(this.processExpirationTime.Value, now) < 0) || (this.idleExpirationTime != null && DateTime.Compare(this.idleExpirationTime.Value, now) < 0);
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x0005DA30 File Offset: 0x0005BC30
		internal bool IsExpiredLifetimeOrTransactionCount(DateTime now)
		{
			return this.transactionCount >= this.WorkerConfiguration.MaxTransactionsPerProcess || (this.processExpirationTime != null && DateTime.Compare(this.processExpirationTime.Value, now) < 0);
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x0005DA6C File Offset: 0x0005BC6C
		private void ResetProcessExpirationTime(DateTime? fromTime)
		{
			if (fromTime == null)
			{
				this.processExpirationTime = null;
				return;
			}
			if (this.WorkerConfiguration.WorkerLifetimeLimit != 0)
			{
				this.processExpirationTime = new DateTime?(fromTime.Value.AddMilliseconds((double)this.WorkerConfiguration.WorkerLifetimeLimit));
			}
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x0005DAC4 File Offset: 0x0005BCC4
		private void ResetIdleExpirationTime(DateTime? fromTime)
		{
			if (fromTime == null)
			{
				this.idleExpirationTime = null;
				return;
			}
			if (this.WorkerConfiguration.WorkerIdleTimeout != 0)
			{
				this.idleExpirationTime = new DateTime?(fromTime.Value.AddMilliseconds((double)this.WorkerConfiguration.WorkerIdleTimeout));
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06002ADE RID: 10974 RVA: 0x0005DB1A File Offset: 0x0005BD1A
		private ComWorkerConfiguration WorkerConfiguration
		{
			get
			{
				return this.configuration;
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06002ADF RID: 10975 RVA: 0x0005DB22 File Offset: 0x0005BD22
		private Mutex ProcessLaunchMutex
		{
			get
			{
				return this.WorkerConfiguration.ProcessLaunchMutex;
			}
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x0005DB2F File Offset: 0x0005BD2F
		private void TraceError(object target, string formatString, params object[] args)
		{
			if (this.tracer != null)
			{
				this.tracer.TraceError((long)((target != null) ? target.GetHashCode() : 0), formatString, args);
			}
		}

		// Token: 0x04002555 RID: 9557
		private static readonly TimeSpan MaxWaitingTimeForWorkerProcessRegister = TimeSpan.FromSeconds(10.0);

		// Token: 0x04002556 RID: 9558
		private IDisposable jobObject;

		// Token: 0x04002557 RID: 9559
		private JobObjectManager jobObjectManger;

		// Token: 0x04002558 RID: 9560
		private SafeProcessHandle currentProcessHandle;

		// Token: 0x04002559 RID: 9561
		private SafeUserTokenHandle workerProcessToken;

		// Token: 0x0400255A RID: 9562
		private ComWorkerConfiguration configuration;

		// Token: 0x0400255B RID: 9563
		private SafeProcessHandle workerProcess;

		// Token: 0x0400255C RID: 9564
		private int workerProcessId;

		// Token: 0x0400255D RID: 9565
		private IComInterface worker;

		// Token: 0x0400255E RID: 9566
		private int transactionCount;

		// Token: 0x0400255F RID: 9567
		private DateTime? processExpirationTime;

		// Token: 0x04002560 RID: 9568
		private DateTime? idleExpirationTime;

		// Token: 0x04002561 RID: 9569
		private Trace tracer;

		// Token: 0x04002563 RID: 9571
		private bool isWorkerBeyondMemoryLimit;

		// Token: 0x020007F8 RID: 2040
		internal class TransactionState
		{
			// Token: 0x06002AE2 RID: 10978 RVA: 0x0005DB68 File Offset: 0x0005BD68
			internal TransactionState(int transactionNumber)
			{
				this.transactionNumber = transactionNumber;
				this.isTimedOut = false;
			}

			// Token: 0x17000B5A RID: 2906
			// (get) Token: 0x06002AE3 RID: 10979 RVA: 0x0005DB7E File Offset: 0x0005BD7E
			internal bool TimedOut
			{
				get
				{
					return this.isTimedOut;
				}
			}

			// Token: 0x17000B5B RID: 2907
			// (get) Token: 0x06002AE4 RID: 10980 RVA: 0x0005DB86 File Offset: 0x0005BD86
			internal int TransactionNumber
			{
				get
				{
					return this.transactionNumber;
				}
			}

			// Token: 0x06002AE5 RID: 10981 RVA: 0x0005DB8E File Offset: 0x0005BD8E
			internal void SetTimedOut()
			{
				this.isTimedOut = true;
			}

			// Token: 0x04002564 RID: 9572
			private int transactionNumber;

			// Token: 0x04002565 RID: 9573
			private bool isTimedOut;
		}
	}
}
