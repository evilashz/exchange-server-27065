using System;
using System.Threading;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000014 RID: 20
	public class Task<T> : Task
	{
		// Token: 0x06000235 RID: 565 RVA: 0x00004E68 File Offset: 0x00003068
		public Task(Task<T>.TaskCallback callback, T context, bool autoStart) : this(callback, context, ThreadPriority.Normal, 0, (TaskFlags)(2 | (autoStart ? 1 : 0)))
		{
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00004E80 File Offset: 0x00003080
		public Task(Task<T>.TaskCallback callback, T context, ThreadPriority priority, int stackSizeInKilobytes, TaskFlags taskFlags)
		{
			this.callback = callback;
			this.context = context;
			this.priority = priority;
			this.stackSizeInKilobytes = stackSizeInKilobytes;
			this.taskFlags = taskFlags;
			this.taskComplete = new ManualResetEvent(true);
			this.state = Task<T>.TaskState.Ready;
			if ((byte)(taskFlags & TaskFlags.AutoStart) != 0)
			{
				this.Start();
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00004EEE File Offset: 0x000030EE
		// (set) Token: 0x06000238 RID: 568 RVA: 0x00004EF6 File Offset: 0x000030F6
		protected Task<T>.TaskCallback CallbackDelegate
		{
			get
			{
				return this.callback;
			}
			set
			{
				this.callback = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00004EFF File Offset: 0x000030FF
		// (set) Token: 0x0600023A RID: 570 RVA: 0x00004F07 File Offset: 0x00003107
		protected T Context
		{
			get
			{
				return this.context;
			}
			set
			{
				this.context = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00004F10 File Offset: 0x00003110
		protected object StateLock
		{
			get
			{
				return this.stateLockObject;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00004F18 File Offset: 0x00003118
		internal uint ExecutionCount
		{
			get
			{
				return this.executionCount;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00004F20 File Offset: 0x00003120
		// (set) Token: 0x0600023E RID: 574 RVA: 0x00004F28 File Offset: 0x00003128
		protected Task<T>.TaskState State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
				switch (value)
				{
				case Task<T>.TaskState.Ready:
					this.taskComplete.Set();
					return;
				case Task<T>.TaskState.Starting:
					this.taskComplete.Reset();
					return;
				case Task<T>.TaskState.Running:
					this.taskComplete.Reset();
					return;
				case Task<T>.TaskState.StopRequested:
				case Task<T>.TaskState.DisposingStopRequested:
					this.taskComplete.Reset();
					return;
				case Task<T>.TaskState.Complete:
				case Task<T>.TaskState.DisposingComplete:
					this.taskComplete.Set();
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00004FAC File Offset: 0x000031AC
		public override void Start()
		{
			base.CheckDisposed();
			FaultInjection.InjectFault(this.StartLockEnterTestHook);
			using (LockManager.Lock(this.StateLock))
			{
				FaultInjection.InjectFault(this.StartLockEnteredTestHook);
				if ((this.State == Task<T>.TaskState.Ready || this.State == Task<T>.TaskState.Complete) && (!this.RunOnceOnly() || this.executionCount == 0U))
				{
					this.State = Task<T>.TaskState.Starting;
					if ((byte)(this.taskFlags & TaskFlags.UseThreadPoolThread) != 0)
					{
						WaitCallback callBack = delegate(object stateParameter)
						{
							this.Worker();
						};
						if (!ThreadPool.QueueUserWorkItem(callBack))
						{
							this.State = Task<T>.TaskState.Ready;
						}
					}
					else
					{
						ThreadStart start = new ThreadStart(this.Worker);
						Thread thread;
						if (this.stackSizeInKilobytes == 0)
						{
							thread = new Thread(start);
						}
						else
						{
							thread = new Thread(start, this.stackSizeInKilobytes * 1024);
						}
						thread.Start();
					}
				}
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00005094 File Offset: 0x00003294
		public override void Stop()
		{
			base.CheckDisposed();
			FaultInjection.InjectFault(this.StopLockEnterTestHook);
			using (LockManager.Lock(this.StateLock))
			{
				FaultInjection.InjectFault(this.StopLockEnteredTestHook);
				if (this.State == Task<T>.TaskState.Starting || this.State == Task<T>.TaskState.Running)
				{
					this.State = Task<T>.TaskState.StopRequested;
				}
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00005104 File Offset: 0x00003304
		public bool RunOnceOnly()
		{
			return (byte)(this.taskFlags & TaskFlags.RunOnceOnly) == 4;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00005112 File Offset: 0x00003312
		public override bool WaitForCompletion(TimeSpan delay)
		{
			base.CheckDisposed();
			return this.taskComplete == null || this.taskComplete.WaitOne(delay);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00005130 File Offset: 0x00003330
		public override bool Finished()
		{
			return this.WaitForCompletion(Task.NoDelay);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000513D File Offset: 0x0000333D
		public bool ShouldCallbackContinue()
		{
			return this.ShouldCallbackContinueImplementation();
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00005148 File Offset: 0x00003348
		protected virtual bool ShouldCallbackContinueImplementation()
		{
			base.CheckDisposed();
			bool result;
			using (LockManager.Lock(this.StateLock))
			{
				result = (this.State == Task<T>.TaskState.Starting || this.State == Task<T>.TaskState.Running);
			}
			return result;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000051A0 File Offset: 0x000033A0
		protected virtual void Invoke()
		{
			LockManager.AssertNoLocksHeld();
			if (!Task.testDisabledInvoke)
			{
				Interlocked.Increment(ref Task.invokeCount);
				try
				{
					using (this.executionDiagnosticsContext.NewDiagnosticsScope())
					{
						FaultInjection.InjectFault(Task.invokeTestHook);
						FaultInjection.InjectFault(this.InvokeLock1EnterTestHook);
						using (LockManager.Lock(this.StateLock))
						{
							FaultInjection.InjectFault(this.InvokeLock1EnteredTestHook);
							switch (this.State)
							{
							case Task<T>.TaskState.Starting:
								this.State = Task<T>.TaskState.Running;
								goto IL_AD;
							case Task<T>.TaskState.StopRequested:
								this.State = Task<T>.TaskState.Complete;
								break;
							case Task<T>.TaskState.DisposingStopRequested:
								this.State = Task<T>.TaskState.DisposingComplete;
								break;
							}
							return;
						}
						IL_AD:
						base.CheckDisposed();
						Thread currentThread = Thread.CurrentThread;
						ThreadPriority threadPriority = currentThread.Priority;
						try
						{
							if (currentThread.Priority != this.priority)
							{
								currentThread.Priority = this.priority;
							}
						}
						catch (ThreadStateException exception)
						{
							NullExecutionDiagnostics.Instance.OnExceptionCatch(exception);
						}
						this.executionCount += 1U;
						try
						{
							using (ThreadManager.NewMethodFrame(this.CallbackDelegate))
							{
								this.CallbackDelegate(this.executionDiagnosticsContext, this.context, new Func<bool>(this.ShouldCallbackContinue));
							}
						}
						finally
						{
							try
							{
								if (currentThread.Priority != threadPriority)
								{
									currentThread.Priority = threadPriority;
								}
							}
							catch (ThreadStateException exception2)
							{
								NullExecutionDiagnostics.Instance.OnExceptionCatch(exception2);
							}
							FaultInjection.InjectFault(this.InvokeLock2EnterTestHook);
							using (LockManager.Lock(this.StateLock))
							{
								FaultInjection.InjectFault(this.InvokeLock2EnteredTestHook);
								switch (this.State)
								{
								case Task<T>.TaskState.Running:
								case Task<T>.TaskState.StopRequested:
									this.State = Task<T>.TaskState.Complete;
									break;
								case Task<T>.TaskState.DisposingStopRequested:
									this.State = Task<T>.TaskState.DisposingComplete;
									break;
								}
							}
						}
					}
				}
				finally
				{
					Interlocked.Decrement(ref Task.invokeCount);
					LockManager.AssertNoLocksHeld();
				}
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00005454 File Offset: 0x00003654
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<Task<T>>(this);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000545C File Offset: 0x0000365C
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				FaultInjection.InjectFault(this.DisposeLock1EnterTestHook);
				using (LockManager.Lock(this.StateLock))
				{
					FaultInjection.InjectFault(this.DisposeLock1EnteredTestHook);
					Task<T>.TaskState taskState = this.State;
					if (taskState != Task<T>.TaskState.Ready)
					{
						switch (taskState)
						{
						case Task<T>.TaskState.Complete:
							break;
						case Task<T>.TaskState.DisposingComplete:
							goto IL_55;
						default:
							this.State = Task<T>.TaskState.DisposingStopRequested;
							goto IL_55;
						}
					}
					this.State = Task<T>.TaskState.DisposingComplete;
					IL_55:;
				}
				base.WaitForCompletion();
				FaultInjection.InjectFault(this.DisposeLock2EnterTestHook);
				using (LockManager.Lock(this.StateLock))
				{
					FaultInjection.InjectFault(this.DisposeLock2EnteredTestHook);
					if (this.taskComplete != null)
					{
						this.taskComplete.Dispose();
						this.taskComplete = null;
					}
				}
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00005580 File Offset: 0x00003780
		protected void Worker()
		{
			WatsonOnUnhandledException.Guard(this.executionDiagnosticsContext, new TryDelegate(this, (UIntPtr)ldftn(<Worker>b__2)));
		}

		// Token: 0x040002EB RID: 747
		private Task<T>.TaskState state;

		// Token: 0x040002EC RID: 748
		private TaskFlags taskFlags;

		// Token: 0x040002ED RID: 749
		private ManualResetEvent taskComplete;

		// Token: 0x040002EE RID: 750
		private T context;

		// Token: 0x040002EF RID: 751
		private Task<T>.TaskCallback callback;

		// Token: 0x040002F0 RID: 752
		private ThreadPriority priority;

		// Token: 0x040002F1 RID: 753
		private int stackSizeInKilobytes;

		// Token: 0x040002F2 RID: 754
		private object stateLockObject = new object();

		// Token: 0x040002F3 RID: 755
		private uint executionCount;

		// Token: 0x040002F4 RID: 756
		private TaskExecutionDiagnosticsProxy executionDiagnosticsContext = new TaskExecutionDiagnosticsProxy();

		// Token: 0x02000015 RID: 21
		// (Invoke) Token: 0x0600024D RID: 589
		public delegate void Callback(T context, Func<bool> shouldCallbackContinue);

		// Token: 0x02000016 RID: 22
		// (Invoke) Token: 0x06000251 RID: 593
		public delegate void TaskCallback(TaskExecutionDiagnosticsProxy diagnosticsContext, T context, Func<bool> shouldCallbackContinue);

		// Token: 0x02000017 RID: 23
		public enum TaskState
		{
			// Token: 0x040002F6 RID: 758
			Ready,
			// Token: 0x040002F7 RID: 759
			Starting,
			// Token: 0x040002F8 RID: 760
			Running,
			// Token: 0x040002F9 RID: 761
			StopRequested,
			// Token: 0x040002FA RID: 762
			DisposingStopRequested,
			// Token: 0x040002FB RID: 763
			Complete,
			// Token: 0x040002FC RID: 764
			DisposingComplete
		}
	}
}
