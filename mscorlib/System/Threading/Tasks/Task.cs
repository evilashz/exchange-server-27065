using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
	// Token: 0x0200051F RID: 1311
	[DebuggerTypeProxy(typeof(SystemThreadingTasks_FutureDebugView<>))]
	[DebuggerDisplay("Id = {Id}, Status = {Status}, Method = {DebuggerDisplayMethodDescription}, Result = {DebuggerDisplayResultDescription}")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class Task<TResult> : Task
	{
		// Token: 0x06003E71 RID: 15985 RVA: 0x000E7C3D File Offset: 0x000E5E3D
		internal Task()
		{
		}

		// Token: 0x06003E72 RID: 15986 RVA: 0x000E7C45 File Offset: 0x000E5E45
		internal Task(object state, TaskCreationOptions options) : base(state, options, true)
		{
		}

		// Token: 0x06003E73 RID: 15987 RVA: 0x000E7C50 File Offset: 0x000E5E50
		internal Task(TResult result) : base(false, TaskCreationOptions.None, default(CancellationToken))
		{
			this.m_result = result;
		}

		// Token: 0x06003E74 RID: 15988 RVA: 0x000E7C75 File Offset: 0x000E5E75
		internal Task(bool canceled, TResult result, TaskCreationOptions creationOptions, CancellationToken ct) : base(canceled, creationOptions, ct)
		{
			if (!canceled)
			{
				this.m_result = result;
			}
		}

		// Token: 0x06003E75 RID: 15989 RVA: 0x000E7C8C File Offset: 0x000E5E8C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task(Func<TResult> function) : this(function, null, default(CancellationToken), TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			base.PossiblyCaptureContext(ref stackCrawlMark);
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x000E7CB8 File Offset: 0x000E5EB8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task(Func<TResult> function, CancellationToken cancellationToken) : this(function, null, cancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			base.PossiblyCaptureContext(ref stackCrawlMark);
		}

		// Token: 0x06003E77 RID: 15991 RVA: 0x000E7CDC File Offset: 0x000E5EDC
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task(Func<TResult> function, TaskCreationOptions creationOptions) : this(function, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, InternalTaskOptions.None, null)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			base.PossiblyCaptureContext(ref stackCrawlMark);
		}

		// Token: 0x06003E78 RID: 15992 RVA: 0x000E7D0C File Offset: 0x000E5F0C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task(Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions) : this(function, Task.InternalCurrentIfAttached(creationOptions), cancellationToken, creationOptions, InternalTaskOptions.None, null)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			base.PossiblyCaptureContext(ref stackCrawlMark);
		}

		// Token: 0x06003E79 RID: 15993 RVA: 0x000E7D34 File Offset: 0x000E5F34
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task(Func<object, TResult> function, object state) : this(function, state, null, default(CancellationToken), TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			base.PossiblyCaptureContext(ref stackCrawlMark);
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x000E7D60 File Offset: 0x000E5F60
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task(Func<object, TResult> function, object state, CancellationToken cancellationToken) : this(function, state, null, cancellationToken, TaskCreationOptions.None, InternalTaskOptions.None, null)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			base.PossiblyCaptureContext(ref stackCrawlMark);
		}

		// Token: 0x06003E7B RID: 15995 RVA: 0x000E7D84 File Offset: 0x000E5F84
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task(Func<object, TResult> function, object state, TaskCreationOptions creationOptions) : this(function, state, Task.InternalCurrentIfAttached(creationOptions), default(CancellationToken), creationOptions, InternalTaskOptions.None, null)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			base.PossiblyCaptureContext(ref stackCrawlMark);
		}

		// Token: 0x06003E7C RID: 15996 RVA: 0x000E7DB8 File Offset: 0x000E5FB8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task(Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions) : this(function, state, Task.InternalCurrentIfAttached(creationOptions), cancellationToken, creationOptions, InternalTaskOptions.None, null)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			base.PossiblyCaptureContext(ref stackCrawlMark);
		}

		// Token: 0x06003E7D RID: 15997 RVA: 0x000E7DE3 File Offset: 0x000E5FE3
		internal Task(Func<TResult> valueSelector, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler, ref StackCrawlMark stackMark) : this(valueSelector, parent, cancellationToken, creationOptions, internalOptions, scheduler)
		{
			base.PossiblyCaptureContext(ref stackMark);
		}

		// Token: 0x06003E7E RID: 15998 RVA: 0x000E7DFC File Offset: 0x000E5FFC
		internal Task(Func<TResult> valueSelector, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler) : base(valueSelector, null, parent, cancellationToken, creationOptions, internalOptions, scheduler)
		{
			if ((internalOptions & InternalTaskOptions.SelfReplicating) != InternalTaskOptions.None)
			{
				throw new ArgumentOutOfRangeException("creationOptions", Environment.GetResourceString("TaskT_ctor_SelfReplicating"));
			}
		}

		// Token: 0x06003E7F RID: 15999 RVA: 0x000E7E2D File Offset: 0x000E602D
		internal Task(Func<object, TResult> valueSelector, object state, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler, ref StackCrawlMark stackMark) : this(valueSelector, state, parent, cancellationToken, creationOptions, internalOptions, scheduler)
		{
			base.PossiblyCaptureContext(ref stackMark);
		}

		// Token: 0x06003E80 RID: 16000 RVA: 0x000E7E48 File Offset: 0x000E6048
		internal Task(Delegate valueSelector, object state, Task parent, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler) : base(valueSelector, state, parent, cancellationToken, creationOptions, internalOptions, scheduler)
		{
			if ((internalOptions & InternalTaskOptions.SelfReplicating) != InternalTaskOptions.None)
			{
				throw new ArgumentOutOfRangeException("creationOptions", Environment.GetResourceString("TaskT_ctor_SelfReplicating"));
			}
		}

		// Token: 0x06003E81 RID: 16001 RVA: 0x000E7E7C File Offset: 0x000E607C
		internal static Task<TResult> StartNew(Task parent, Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler, ref StackCrawlMark stackMark)
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			if ((internalOptions & InternalTaskOptions.SelfReplicating) != InternalTaskOptions.None)
			{
				throw new ArgumentOutOfRangeException("creationOptions", Environment.GetResourceString("TaskT_ctor_SelfReplicating"));
			}
			Task<TResult> task = new Task<TResult>(function, parent, cancellationToken, creationOptions, internalOptions | InternalTaskOptions.QueuedByRuntime, scheduler, ref stackMark);
			task.ScheduleAndStart(false);
			return task;
		}

		// Token: 0x06003E82 RID: 16002 RVA: 0x000E7EE4 File Offset: 0x000E60E4
		internal static Task<TResult> StartNew(Task parent, Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, InternalTaskOptions internalOptions, TaskScheduler scheduler, ref StackCrawlMark stackMark)
		{
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			if ((internalOptions & InternalTaskOptions.SelfReplicating) != InternalTaskOptions.None)
			{
				throw new ArgumentOutOfRangeException("creationOptions", Environment.GetResourceString("TaskT_ctor_SelfReplicating"));
			}
			Task<TResult> task = new Task<TResult>(function, state, parent, cancellationToken, creationOptions, internalOptions | InternalTaskOptions.QueuedByRuntime, scheduler, ref stackMark);
			task.ScheduleAndStart(false);
			return task;
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06003E83 RID: 16003 RVA: 0x000E7F4D File Offset: 0x000E614D
		private string DebuggerDisplayResultDescription
		{
			get
			{
				if (!base.IsRanToCompletion)
				{
					return Environment.GetResourceString("TaskT_DebuggerNoResult");
				}
				return string.Concat(this.m_result);
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06003E84 RID: 16004 RVA: 0x000E7F74 File Offset: 0x000E6174
		private string DebuggerDisplayMethodDescription
		{
			get
			{
				Delegate @delegate = (Delegate)this.m_action;
				if (@delegate == null)
				{
					return "{null}";
				}
				return @delegate.Method.ToString();
			}
		}

		// Token: 0x06003E85 RID: 16005 RVA: 0x000E7FA4 File Offset: 0x000E61A4
		internal bool TrySetResult(TResult result)
		{
			if (base.IsCompleted)
			{
				return false;
			}
			if (base.AtomicStateUpdate(67108864, 90177536))
			{
				this.m_result = result;
				Interlocked.Exchange(ref this.m_stateFlags, this.m_stateFlags | 16777216);
				Task.ContingentProperties contingentProperties = this.m_contingentProperties;
				if (contingentProperties != null)
				{
					contingentProperties.SetCompleted();
				}
				base.FinishStageThree();
				return true;
			}
			return false;
		}

		// Token: 0x06003E86 RID: 16006 RVA: 0x000E800C File Offset: 0x000E620C
		internal void DangerousSetResult(TResult result)
		{
			if (this.m_parent != null)
			{
				bool flag = this.TrySetResult(result);
				return;
			}
			this.m_result = result;
			this.m_stateFlags |= 16777216;
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06003E87 RID: 16007 RVA: 0x000E8047 File Offset: 0x000E6247
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[__DynamicallyInvokable]
		public TResult Result
		{
			[__DynamicallyInvokable]
			get
			{
				if (!base.IsWaitNotificationEnabledOrNotRanToCompletion)
				{
					return this.m_result;
				}
				return this.GetResultCore(true);
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06003E88 RID: 16008 RVA: 0x000E805F File Offset: 0x000E625F
		internal TResult ResultOnSuccess
		{
			get
			{
				return this.m_result;
			}
		}

		// Token: 0x06003E89 RID: 16009 RVA: 0x000E8068 File Offset: 0x000E6268
		internal TResult GetResultCore(bool waitCompletionNotification)
		{
			if (!base.IsCompleted)
			{
				base.InternalWait(-1, default(CancellationToken));
			}
			if (waitCompletionNotification)
			{
				base.NotifyDebuggerOfWaitCompletionIfNecessary();
			}
			if (!base.IsRanToCompletion)
			{
				base.ThrowIfExceptional(true);
			}
			return this.m_result;
		}

		// Token: 0x06003E8A RID: 16010 RVA: 0x000E80B0 File Offset: 0x000E62B0
		internal bool TrySetException(object exceptionObject)
		{
			bool result = false;
			base.EnsureContingentPropertiesInitialized(true);
			if (base.AtomicStateUpdate(67108864, 90177536))
			{
				base.AddException(exceptionObject);
				base.Finish(false);
				result = true;
			}
			return result;
		}

		// Token: 0x06003E8B RID: 16011 RVA: 0x000E80EA File Offset: 0x000E62EA
		internal bool TrySetCanceled(CancellationToken tokenToRecord)
		{
			return this.TrySetCanceled(tokenToRecord, null);
		}

		// Token: 0x06003E8C RID: 16012 RVA: 0x000E80F4 File Offset: 0x000E62F4
		internal bool TrySetCanceled(CancellationToken tokenToRecord, object cancellationException)
		{
			bool result = false;
			if (base.AtomicStateUpdate(67108864, 90177536))
			{
				base.RecordInternalCancellationRequest(tokenToRecord, cancellationException);
				base.CancellationCleanupLogic();
				result = true;
			}
			return result;
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06003E8D RID: 16013 RVA: 0x000E8126 File Offset: 0x000E6326
		[__DynamicallyInvokable]
		public new static TaskFactory<TResult> Factory
		{
			[__DynamicallyInvokable]
			get
			{
				return Task<TResult>.s_Factory;
			}
		}

		// Token: 0x06003E8E RID: 16014 RVA: 0x000E8130 File Offset: 0x000E6330
		internal override void InnerInvoke()
		{
			Func<TResult> func = this.m_action as Func<TResult>;
			if (func != null)
			{
				this.m_result = func();
				return;
			}
			Func<object, TResult> func2 = this.m_action as Func<object, TResult>;
			if (func2 != null)
			{
				this.m_result = func2(this.m_stateObject);
				return;
			}
		}

		// Token: 0x06003E8F RID: 16015 RVA: 0x000E817B File Offset: 0x000E637B
		[__DynamicallyInvokable]
		public new TaskAwaiter<TResult> GetAwaiter()
		{
			return new TaskAwaiter<TResult>(this);
		}

		// Token: 0x06003E90 RID: 16016 RVA: 0x000E8183 File Offset: 0x000E6383
		[__DynamicallyInvokable]
		public new ConfiguredTaskAwaitable<TResult> ConfigureAwait(bool continueOnCapturedContext)
		{
			return new ConfiguredTaskAwaitable<TResult>(this, continueOnCapturedContext);
		}

		// Token: 0x06003E91 RID: 16017 RVA: 0x000E818C File Offset: 0x000E638C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task<TResult>> continuationAction)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None, ref stackCrawlMark);
		}

		// Token: 0x06003E92 RID: 16018 RVA: 0x000E81B4 File Offset: 0x000E63B4
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task<TResult>> continuationAction, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackCrawlMark);
		}

		// Token: 0x06003E93 RID: 16019 RVA: 0x000E81D4 File Offset: 0x000E63D4
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task<TResult>> continuationAction, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, scheduler, default(CancellationToken), TaskContinuationOptions.None, ref stackCrawlMark);
		}

		// Token: 0x06003E94 RID: 16020 RVA: 0x000E81F8 File Offset: 0x000E63F8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task<TResult>> continuationAction, TaskContinuationOptions continuationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, TaskScheduler.Current, default(CancellationToken), continuationOptions, ref stackCrawlMark);
		}

		// Token: 0x06003E95 RID: 16021 RVA: 0x000E8220 File Offset: 0x000E6420
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task<TResult>> continuationAction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, scheduler, cancellationToken, continuationOptions, ref stackCrawlMark);
		}

		// Token: 0x06003E96 RID: 16022 RVA: 0x000E823C File Offset: 0x000E643C
		internal Task ContinueWith(Action<Task<TResult>> continuationAction, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskCreationOptions creationOptions;
			InternalTaskOptions internalOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
			Task task = new ContinuationTaskFromResultTask<TResult>(this, continuationAction, null, creationOptions, internalOptions, ref stackMark);
			base.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		// Token: 0x06003E97 RID: 16023 RVA: 0x000E828C File Offset: 0x000E648C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task<TResult>, object> continuationAction, object state)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, state, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None, ref stackCrawlMark);
		}

		// Token: 0x06003E98 RID: 16024 RVA: 0x000E82B4 File Offset: 0x000E64B4
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task<TResult>, object> continuationAction, object state, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, state, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackCrawlMark);
		}

		// Token: 0x06003E99 RID: 16025 RVA: 0x000E82D4 File Offset: 0x000E64D4
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task<TResult>, object> continuationAction, object state, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, state, scheduler, default(CancellationToken), TaskContinuationOptions.None, ref stackCrawlMark);
		}

		// Token: 0x06003E9A RID: 16026 RVA: 0x000E82F8 File Offset: 0x000E64F8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task<TResult>, object> continuationAction, object state, TaskContinuationOptions continuationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, state, TaskScheduler.Current, default(CancellationToken), continuationOptions, ref stackCrawlMark);
		}

		// Token: 0x06003E9B RID: 16027 RVA: 0x000E8320 File Offset: 0x000E6520
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task ContinueWith(Action<Task<TResult>, object> continuationAction, object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith(continuationAction, state, scheduler, cancellationToken, continuationOptions, ref stackCrawlMark);
		}

		// Token: 0x06003E9C RID: 16028 RVA: 0x000E8340 File Offset: 0x000E6540
		internal Task ContinueWith(Action<Task<TResult>, object> continuationAction, object state, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
		{
			if (continuationAction == null)
			{
				throw new ArgumentNullException("continuationAction");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskCreationOptions creationOptions;
			InternalTaskOptions internalOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
			Task task = new ContinuationTaskFromResultTask<TResult>(this, continuationAction, state, creationOptions, internalOptions, ref stackMark);
			base.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		// Token: 0x06003E9D RID: 16029 RVA: 0x000E8390 File Offset: 0x000E6590
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TNewResult>(continuationFunction, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None, ref stackCrawlMark);
		}

		// Token: 0x06003E9E RID: 16030 RVA: 0x000E83B8 File Offset: 0x000E65B8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TNewResult>(continuationFunction, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackCrawlMark);
		}

		// Token: 0x06003E9F RID: 16031 RVA: 0x000E83D8 File Offset: 0x000E65D8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TNewResult>(continuationFunction, scheduler, default(CancellationToken), TaskContinuationOptions.None, ref stackCrawlMark);
		}

		// Token: 0x06003EA0 RID: 16032 RVA: 0x000E83FC File Offset: 0x000E65FC
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TNewResult>(continuationFunction, TaskScheduler.Current, default(CancellationToken), continuationOptions, ref stackCrawlMark);
		}

		// Token: 0x06003EA1 RID: 16033 RVA: 0x000E8424 File Offset: 0x000E6624
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TNewResult>(continuationFunction, scheduler, cancellationToken, continuationOptions, ref stackCrawlMark);
		}

		// Token: 0x06003EA2 RID: 16034 RVA: 0x000E8440 File Offset: 0x000E6640
		internal Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, TNewResult> continuationFunction, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskCreationOptions creationOptions;
			InternalTaskOptions internalOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
			Task<TNewResult> task = new ContinuationResultTaskFromResultTask<TResult, TNewResult>(this, continuationFunction, null, creationOptions, internalOptions, ref stackMark);
			base.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		// Token: 0x06003EA3 RID: 16035 RVA: 0x000E8490 File Offset: 0x000E6690
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TNewResult>(continuationFunction, state, TaskScheduler.Current, default(CancellationToken), TaskContinuationOptions.None, ref stackCrawlMark);
		}

		// Token: 0x06003EA4 RID: 16036 RVA: 0x000E84B8 File Offset: 0x000E66B8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TNewResult>(continuationFunction, state, TaskScheduler.Current, cancellationToken, TaskContinuationOptions.None, ref stackCrawlMark);
		}

		// Token: 0x06003EA5 RID: 16037 RVA: 0x000E84D8 File Offset: 0x000E66D8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TNewResult>(continuationFunction, state, scheduler, default(CancellationToken), TaskContinuationOptions.None, ref stackCrawlMark);
		}

		// Token: 0x06003EA6 RID: 16038 RVA: 0x000E84FC File Offset: 0x000E66FC
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state, TaskContinuationOptions continuationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TNewResult>(continuationFunction, state, TaskScheduler.Current, default(CancellationToken), continuationOptions, ref stackCrawlMark);
		}

		// Token: 0x06003EA7 RID: 16039 RVA: 0x000E8524 File Offset: 0x000E6724
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return this.ContinueWith<TNewResult>(continuationFunction, state, scheduler, cancellationToken, continuationOptions, ref stackCrawlMark);
		}

		// Token: 0x06003EA8 RID: 16040 RVA: 0x000E8544 File Offset: 0x000E6744
		internal Task<TNewResult> ContinueWith<TNewResult>(Func<Task<TResult>, object, TNewResult> continuationFunction, object state, TaskScheduler scheduler, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, ref StackCrawlMark stackMark)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskCreationOptions creationOptions;
			InternalTaskOptions internalOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalOptions);
			Task<TNewResult> task = new ContinuationResultTaskFromResultTask<TResult, TNewResult>(this, continuationFunction, state, creationOptions, internalOptions, ref stackMark);
			base.ContinueWithCore(task, scheduler, cancellationToken, continuationOptions);
			return task;
		}

		// Token: 0x04001A03 RID: 6659
		internal TResult m_result;

		// Token: 0x04001A04 RID: 6660
		private static readonly TaskFactory<TResult> s_Factory = new TaskFactory<TResult>();

		// Token: 0x04001A05 RID: 6661
		internal static readonly Func<Task<Task>, Task<TResult>> TaskWhenAnyCast = (Task<Task> completed) => (Task<TResult>)completed.Result;
	}
}
