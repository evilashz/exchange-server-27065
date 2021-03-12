using System;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Security.Permissions;

namespace System.Threading.Tasks
{
	// Token: 0x02000521 RID: 1313
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class TaskFactory<TResult>
	{
		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06003EB2 RID: 16050 RVA: 0x000E8665 File Offset: 0x000E6865
		private TaskScheduler DefaultScheduler
		{
			get
			{
				if (this.m_defaultScheduler == null)
				{
					return TaskScheduler.Current;
				}
				return this.m_defaultScheduler;
			}
		}

		// Token: 0x06003EB3 RID: 16051 RVA: 0x000E867B File Offset: 0x000E687B
		private TaskScheduler GetDefaultScheduler(Task currTask)
		{
			if (this.m_defaultScheduler != null)
			{
				return this.m_defaultScheduler;
			}
			if (currTask != null && (currTask.CreationOptions & TaskCreationOptions.HideScheduler) == TaskCreationOptions.None)
			{
				return currTask.ExecutingTaskScheduler;
			}
			return TaskScheduler.Default;
		}

		// Token: 0x06003EB4 RID: 16052 RVA: 0x000E86A8 File Offset: 0x000E68A8
		[__DynamicallyInvokable]
		public TaskFactory() : this(default(CancellationToken), TaskCreationOptions.None, TaskContinuationOptions.None, null)
		{
		}

		// Token: 0x06003EB5 RID: 16053 RVA: 0x000E86C7 File Offset: 0x000E68C7
		[__DynamicallyInvokable]
		public TaskFactory(CancellationToken cancellationToken) : this(cancellationToken, TaskCreationOptions.None, TaskContinuationOptions.None, null)
		{
		}

		// Token: 0x06003EB6 RID: 16054 RVA: 0x000E86D4 File Offset: 0x000E68D4
		[__DynamicallyInvokable]
		public TaskFactory(TaskScheduler scheduler) : this(default(CancellationToken), TaskCreationOptions.None, TaskContinuationOptions.None, scheduler)
		{
		}

		// Token: 0x06003EB7 RID: 16055 RVA: 0x000E86F4 File Offset: 0x000E68F4
		[__DynamicallyInvokable]
		public TaskFactory(TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions) : this(default(CancellationToken), creationOptions, continuationOptions, null)
		{
		}

		// Token: 0x06003EB8 RID: 16056 RVA: 0x000E8713 File Offset: 0x000E6913
		[__DynamicallyInvokable]
		public TaskFactory(CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			TaskFactory.CheckCreationOptions(creationOptions);
			this.m_defaultCancellationToken = cancellationToken;
			this.m_defaultScheduler = scheduler;
			this.m_defaultCreationOptions = creationOptions;
			this.m_defaultContinuationOptions = continuationOptions;
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06003EB9 RID: 16057 RVA: 0x000E8744 File Offset: 0x000E6944
		[__DynamicallyInvokable]
		public CancellationToken CancellationToken
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_defaultCancellationToken;
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06003EBA RID: 16058 RVA: 0x000E874C File Offset: 0x000E694C
		[__DynamicallyInvokable]
		public TaskScheduler Scheduler
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_defaultScheduler;
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06003EBB RID: 16059 RVA: 0x000E8754 File Offset: 0x000E6954
		[__DynamicallyInvokable]
		public TaskCreationOptions CreationOptions
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_defaultCreationOptions;
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06003EBC RID: 16060 RVA: 0x000E875C File Offset: 0x000E695C
		[__DynamicallyInvokable]
		public TaskContinuationOptions ContinuationOptions
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_defaultContinuationOptions;
			}
		}

		// Token: 0x06003EBD RID: 16061 RVA: 0x000E8764 File Offset: 0x000E6964
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew(Func<TResult> function)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		// Token: 0x06003EBE RID: 16062 RVA: 0x000E8798 File Offset: 0x000E6998
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew(Func<TResult> function, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		// Token: 0x06003EBF RID: 16063 RVA: 0x000E87C8 File Offset: 0x000E69C8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew(Func<TResult> function, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		// Token: 0x06003EC0 RID: 16064 RVA: 0x000E87F8 File Offset: 0x000E69F8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew(Func<TResult> function, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler, ref stackCrawlMark);
		}

		// Token: 0x06003EC1 RID: 16065 RVA: 0x000E881C File Offset: 0x000E6A1C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew(Func<object, TResult> function, object state)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		// Token: 0x06003EC2 RID: 16066 RVA: 0x000E8850 File Offset: 0x000E6A50
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew(Func<object, TResult> function, object state, CancellationToken cancellationToken)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, cancellationToken, this.m_defaultCreationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		// Token: 0x06003EC3 RID: 16067 RVA: 0x000E8880 File Offset: 0x000E6A80
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew(Func<object, TResult> function, object state, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			Task internalCurrent = Task.InternalCurrent;
			return Task<TResult>.StartNew(internalCurrent, function, state, this.m_defaultCancellationToken, creationOptions, InternalTaskOptions.None, this.GetDefaultScheduler(internalCurrent), ref stackCrawlMark);
		}

		// Token: 0x06003EC4 RID: 16068 RVA: 0x000E88B0 File Offset: 0x000E6AB0
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> StartNew(Func<object, TResult> function, object state, CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Task<TResult>.StartNew(Task.InternalCurrentIfAttached(creationOptions), function, state, cancellationToken, creationOptions, InternalTaskOptions.None, scheduler, ref stackCrawlMark);
		}

		// Token: 0x06003EC5 RID: 16069 RVA: 0x000E88D8 File Offset: 0x000E6AD8
		private static void FromAsyncCoreLogic(IAsyncResult iar, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, Task<TResult> promise, bool requiresSynchronization)
		{
			Exception ex = null;
			OperationCanceledException ex2 = null;
			TResult result = default(TResult);
			try
			{
				if (endFunction != null)
				{
					result = endFunction(iar);
				}
				else
				{
					endAction(iar);
				}
			}
			catch (OperationCanceledException ex3)
			{
				ex2 = ex3;
			}
			catch (Exception ex4)
			{
				ex = ex4;
			}
			finally
			{
				if (ex2 != null)
				{
					promise.TrySetCanceled(ex2.CancellationToken, ex2);
				}
				else if (ex != null)
				{
					bool flag = promise.TrySetException(ex);
					if (flag && ex is ThreadAbortException)
					{
						promise.m_contingentProperties.m_exceptionsHolder.MarkAsHandled(false);
					}
				}
				else
				{
					if (AsyncCausalityTracer.LoggingOn)
					{
						AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, promise.Id, AsyncCausalityStatus.Completed);
					}
					if (Task.s_asyncDebuggingEnabled)
					{
						Task.RemoveFromActiveTasks(promise.Id);
					}
					if (requiresSynchronization)
					{
						promise.TrySetResult(result);
					}
					else
					{
						promise.DangerousSetResult(result);
					}
				}
			}
		}

		// Token: 0x06003EC6 RID: 16070 RVA: 0x000E89C0 File Offset: 0x000E6BC0
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, this.m_defaultCreationOptions, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06003EC7 RID: 16071 RVA: 0x000E89E8 File Offset: 0x000E6BE8
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, creationOptions, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06003EC8 RID: 16072 RVA: 0x000E8A08 File Offset: 0x000E6C08
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> FromAsync(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endMethod, TaskCreationOptions creationOptions, TaskScheduler scheduler)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.FromAsyncImpl(asyncResult, endMethod, null, creationOptions, scheduler, ref stackCrawlMark);
		}

		// Token: 0x06003EC9 RID: 16073 RVA: 0x000E8A24 File Offset: 0x000E6C24
		internal static Task<TResult> FromAsyncImpl(IAsyncResult asyncResult, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TaskCreationOptions creationOptions, TaskScheduler scheduler, ref StackCrawlMark stackMark)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endMethod");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, false);
			Task<TResult> promise = new Task<TResult>(null, creationOptions);
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, promise.Id, "TaskFactory.FromAsync", 0UL);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(promise);
			}
			Task t = new Task(delegate(object <p0>)
			{
				TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, true);
			}, null, null, default(CancellationToken), TaskCreationOptions.None, InternalTaskOptions.None, null, ref stackMark);
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Verbose, t.Id, "TaskFactory.FromAsync Callback", 0UL);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(t);
			}
			if (asyncResult.IsCompleted)
			{
				try
				{
					t.InternalRunSynchronously(scheduler, false);
					goto IL_154;
				}
				catch (Exception exceptionObject)
				{
					promise.TrySetException(exceptionObject);
					goto IL_154;
				}
			}
			ThreadPool.RegisterWaitForSingleObject(asyncResult.AsyncWaitHandle, delegate(object <p0>, bool <p1>)
			{
				try
				{
					t.InternalRunSynchronously(scheduler, false);
				}
				catch (Exception exceptionObject2)
				{
					promise.TrySetException(exceptionObject2);
				}
			}, null, -1, true);
			IL_154:
			return promise;
		}

		// Token: 0x06003ECA RID: 16074 RVA: 0x000E8B9C File Offset: 0x000E6D9C
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, null, state, this.m_defaultCreationOptions);
		}

		// Token: 0x06003ECB RID: 16075 RVA: 0x000E8BAD File Offset: 0x000E6DAD
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl(beginMethod, endMethod, null, state, creationOptions);
		}

		// Token: 0x06003ECC RID: 16076 RVA: 0x000E8BBC File Offset: 0x000E6DBC
		internal static Task<TResult> FromAsyncImpl(Func<AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				throw new ArgumentNullException("beginMethod");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endMethod");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, promise.Id, "TaskFactory.FromAsync: " + beginMethod.Method.Name, 0UL);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(promise);
			}
			try
			{
				if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
				{
					IAsyncResult asyncResult = beginMethod(delegate(IAsyncResult iar)
					{
						if (!iar.CompletedSynchronously)
						{
							TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
						}
					}, state);
					if (asyncResult.CompletedSynchronously)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
					}
				}
				else
				{
					IAsyncResult asyncResult2 = beginMethod(delegate(IAsyncResult iar)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}, state);
				}
			}
			catch
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, promise.Id, AsyncCausalityStatus.Error);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(promise.Id);
				}
				promise.TrySetResult(default(TResult));
				throw;
			}
			return promise;
		}

		// Token: 0x06003ECD RID: 16077 RVA: 0x000E8D14 File Offset: 0x000E6F14
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, null, arg1, state, this.m_defaultCreationOptions);
		}

		// Token: 0x06003ECE RID: 16078 RVA: 0x000E8D27 File Offset: 0x000E6F27
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1>(beginMethod, endMethod, null, arg1, state, creationOptions);
		}

		// Token: 0x06003ECF RID: 16079 RVA: 0x000E8D38 File Offset: 0x000E6F38
		internal static Task<TResult> FromAsyncImpl<TArg1>(Func<TArg1, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				throw new ArgumentNullException("beginMethod");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endFunction");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, promise.Id, "TaskFactory.FromAsync: " + beginMethod.Method.Name, 0UL);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(promise);
			}
			try
			{
				if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
				{
					IAsyncResult asyncResult = beginMethod(arg1, delegate(IAsyncResult iar)
					{
						if (!iar.CompletedSynchronously)
						{
							TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
						}
					}, state);
					if (asyncResult.CompletedSynchronously)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
					}
				}
				else
				{
					IAsyncResult asyncResult2 = beginMethod(arg1, delegate(IAsyncResult iar)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}, state);
				}
			}
			catch
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, promise.Id, AsyncCausalityStatus.Error);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(promise.Id);
				}
				promise.TrySetResult(default(TResult));
				throw;
			}
			return promise;
		}

		// Token: 0x06003ED0 RID: 16080 RVA: 0x000E8E94 File Offset: 0x000E7094
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, null, arg1, arg2, state, this.m_defaultCreationOptions);
		}

		// Token: 0x06003ED1 RID: 16081 RVA: 0x000E8EA9 File Offset: 0x000E70A9
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2>(beginMethod, endMethod, null, arg1, arg2, state, creationOptions);
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x000E8EBC File Offset: 0x000E70BC
		internal static Task<TResult> FromAsyncImpl<TArg1, TArg2>(Func<TArg1, TArg2, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, TArg2 arg2, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				throw new ArgumentNullException("beginMethod");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endMethod");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, promise.Id, "TaskFactory.FromAsync: " + beginMethod.Method.Name, 0UL);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(promise);
			}
			try
			{
				if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
				{
					IAsyncResult asyncResult = beginMethod(arg1, arg2, delegate(IAsyncResult iar)
					{
						if (!iar.CompletedSynchronously)
						{
							TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
						}
					}, state);
					if (asyncResult.CompletedSynchronously)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
					}
				}
				else
				{
					IAsyncResult asyncResult2 = beginMethod(arg1, arg2, delegate(IAsyncResult iar)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}, state);
				}
			}
			catch
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, promise.Id, AsyncCausalityStatus.Error);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(promise.Id);
				}
				promise.TrySetResult(default(TResult));
				throw;
			}
			return promise;
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x000E901C File Offset: 0x000E721C
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, null, arg1, arg2, arg3, state, this.m_defaultCreationOptions);
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x000E9033 File Offset: 0x000E7233
		[__DynamicallyInvokable]
		public Task<TResult> FromAsync<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endMethod, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
		{
			return TaskFactory<TResult>.FromAsyncImpl<TArg1, TArg2, TArg3>(beginMethod, endMethod, null, arg1, arg2, arg3, state, creationOptions);
		}

		// Token: 0x06003ED5 RID: 16085 RVA: 0x000E9048 File Offset: 0x000E7248
		internal static Task<TResult> FromAsyncImpl<TArg1, TArg2, TArg3>(Func<TArg1, TArg2, TArg3, AsyncCallback, object, IAsyncResult> beginMethod, Func<IAsyncResult, TResult> endFunction, Action<IAsyncResult> endAction, TArg1 arg1, TArg2 arg2, TArg3 arg3, object state, TaskCreationOptions creationOptions)
		{
			if (beginMethod == null)
			{
				throw new ArgumentNullException("beginMethod");
			}
			if (endFunction == null && endAction == null)
			{
				throw new ArgumentNullException("endMethod");
			}
			TaskFactory.CheckFromAsyncOptions(creationOptions, true);
			Task<TResult> promise = new Task<TResult>(state, creationOptions);
			if (AsyncCausalityTracer.LoggingOn)
			{
				AsyncCausalityTracer.TraceOperationCreation(CausalityTraceLevel.Required, promise.Id, "TaskFactory.FromAsync: " + beginMethod.Method.Name, 0UL);
			}
			if (Task.s_asyncDebuggingEnabled)
			{
				Task.AddToActiveTasks(promise);
			}
			try
			{
				if (BinaryCompatibility.TargetsAtLeast_Desktop_V4_5)
				{
					IAsyncResult asyncResult = beginMethod(arg1, arg2, arg3, delegate(IAsyncResult iar)
					{
						if (!iar.CompletedSynchronously)
						{
							TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
						}
					}, state);
					if (asyncResult.CompletedSynchronously)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(asyncResult, endFunction, endAction, promise, false);
					}
				}
				else
				{
					IAsyncResult asyncResult2 = beginMethod(arg1, arg2, arg3, delegate(IAsyncResult iar)
					{
						TaskFactory<TResult>.FromAsyncCoreLogic(iar, endFunction, endAction, promise, true);
					}, state);
				}
			}
			catch
			{
				if (AsyncCausalityTracer.LoggingOn)
				{
					AsyncCausalityTracer.TraceOperationCompletion(CausalityTraceLevel.Required, promise.Id, AsyncCausalityStatus.Error);
				}
				if (Task.s_asyncDebuggingEnabled)
				{
					Task.RemoveFromActiveTasks(promise.Id);
				}
				promise.TrySetResult(default(TResult));
				throw;
			}
			return promise;
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x000E91AC File Offset: 0x000E73AC
		internal static Task<TResult> FromAsyncTrim<TInstance, TArgs>(TInstance thisRef, TArgs args, Func<TInstance, TArgs, AsyncCallback, object, IAsyncResult> beginMethod, Func<TInstance, IAsyncResult, TResult> endMethod) where TInstance : class
		{
			TaskFactory<TResult>.FromAsyncTrimPromise<TInstance> fromAsyncTrimPromise = new TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>(thisRef, endMethod);
			IAsyncResult asyncResult = beginMethod(thisRef, args, TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>.s_completeFromAsyncResult, fromAsyncTrimPromise);
			if (asyncResult.CompletedSynchronously)
			{
				fromAsyncTrimPromise.Complete(thisRef, endMethod, asyncResult, false);
			}
			return fromAsyncTrimPromise;
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x000E91E4 File Offset: 0x000E73E4
		private static Task<TResult> CreateCanceledTask(TaskContinuationOptions continuationOptions, CancellationToken ct)
		{
			TaskCreationOptions creationOptions;
			InternalTaskOptions internalTaskOptions;
			Task.CreationOptionsFromContinuationOptions(continuationOptions, out creationOptions, out internalTaskOptions);
			return new Task<TResult>(true, default(TResult), creationOptions, ct);
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x000E920C File Offset: 0x000E740C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06003ED9 RID: 16089 RVA: 0x000E9248 File Offset: 0x000E7448
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06003EDA RID: 16090 RVA: 0x000E927C File Offset: 0x000E747C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06003EDB RID: 16091 RVA: 0x000E92B0 File Offset: 0x000E74B0
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll(Task[] tasks, Func<Task[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		// Token: 0x06003EDC RID: 16092 RVA: 0x000E92DC File Offset: 0x000E74DC
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06003EDD RID: 16093 RVA: 0x000E9318 File Offset: 0x000E7518
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06003EDE RID: 16094 RVA: 0x000E934C File Offset: 0x000E754C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06003EDF RID: 16095 RVA: 0x000E9380 File Offset: 0x000E7580
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAll<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAllImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		// Token: 0x06003EE0 RID: 16096 RVA: 0x000E93AC File Offset: 0x000E75AC
		internal static Task<TResult> ContinueWhenAllImpl<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>[], TResult> continuationFunction, Action<Task<TAntecedentResult>[]> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler, ref StackCrawlMark stackMark)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task<TAntecedentResult>[] tasksCopy = TaskFactory.CheckMultiContinuationTasksAndCopy<TAntecedentResult>(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			Task<Task<TAntecedentResult>[]> task = TaskFactory.CommonCWAllLogic<TAntecedentResult>(tasksCopy);
			if (continuationFunction != null)
			{
				return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAllFuncDelegate, continuationFunction, scheduler, cancellationToken, continuationOptions, ref stackMark);
			}
			return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAllActionDelegate, continuationAction, scheduler, cancellationToken, continuationOptions, ref stackMark);
		}

		// Token: 0x06003EE1 RID: 16097 RVA: 0x000E942C File Offset: 0x000E762C
		internal static Task<TResult> ContinueWhenAllImpl(Task[] tasks, Func<Task[], TResult> continuationFunction, Action<Task[]> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler, ref StackCrawlMark stackMark)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task[] tasksCopy = TaskFactory.CheckMultiContinuationTasksAndCopy(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			Task<Task[]> task = TaskFactory.CommonCWAllLogic(tasksCopy);
			if (continuationFunction != null)
			{
				return task.ContinueWith<TResult>(delegate(Task<Task[]> completedTasks, object state)
				{
					completedTasks.NotifyDebuggerOfWaitCompletionIfNecessary();
					return ((Func<Task[], TResult>)state)(completedTasks.Result);
				}, continuationFunction, scheduler, cancellationToken, continuationOptions, ref stackMark);
			}
			return task.ContinueWith<TResult>(delegate(Task<Task[]> completedTasks, object state)
			{
				completedTasks.NotifyDebuggerOfWaitCompletionIfNecessary();
				((Action<Task[]>)state)(completedTasks.Result);
				return default(TResult);
			}, continuationAction, scheduler, cancellationToken, continuationOptions, ref stackMark);
		}

		// Token: 0x06003EE2 RID: 16098 RVA: 0x000E94E0 File Offset: 0x000E76E0
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06003EE3 RID: 16099 RVA: 0x000E951C File Offset: 0x000E771C
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06003EE4 RID: 16100 RVA: 0x000E9550 File Offset: 0x000E7750
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06003EE5 RID: 16101 RVA: 0x000E9584 File Offset: 0x000E7784
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny(Task[] tasks, Func<Task, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		// Token: 0x06003EE6 RID: 16102 RVA: 0x000E95B0 File Offset: 0x000E77B0
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06003EE7 RID: 16103 RVA: 0x000E95EC File Offset: 0x000E77EC
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, this.m_defaultContinuationOptions, cancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06003EE8 RID: 16104 RVA: 0x000E9620 File Offset: 0x000E7820
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, TaskContinuationOptions continuationOptions)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, this.m_defaultCancellationToken, this.DefaultScheduler, ref stackCrawlMark);
		}

		// Token: 0x06003EE9 RID: 16105 RVA: 0x000E9654 File Offset: 0x000E7854
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public Task<TResult> ContinueWhenAny<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, CancellationToken cancellationToken, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
		{
			if (continuationFunction == null)
			{
				throw new ArgumentNullException("continuationFunction");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TaskFactory<TResult>.ContinueWhenAnyImpl<TAntecedentResult>(tasks, continuationFunction, null, continuationOptions, cancellationToken, scheduler, ref stackCrawlMark);
		}

		// Token: 0x06003EEA RID: 16106 RVA: 0x000E9680 File Offset: 0x000E7880
		internal static Task<TResult> ContinueWhenAnyImpl(Task[] tasks, Func<Task, TResult> continuationFunction, Action<Task> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler, ref StackCrawlMark stackMark)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_EmptyTaskList"), "tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task<Task> task = TaskFactory.CommonCWAnyLogic(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			if (continuationFunction != null)
			{
				return task.ContinueWith<TResult>((Task<Task> completedTask, object state) => ((Func<Task, TResult>)state)(completedTask.Result), continuationFunction, scheduler, cancellationToken, continuationOptions, ref stackMark);
			}
			return task.ContinueWith<TResult>(delegate(Task<Task> completedTask, object state)
			{
				((Action<Task>)state)(completedTask.Result);
				return default(TResult);
			}, continuationAction, scheduler, cancellationToken, continuationOptions, ref stackMark);
		}

		// Token: 0x06003EEB RID: 16107 RVA: 0x000E9748 File Offset: 0x000E7948
		internal static Task<TResult> ContinueWhenAnyImpl<TAntecedentResult>(Task<TAntecedentResult>[] tasks, Func<Task<TAntecedentResult>, TResult> continuationFunction, Action<Task<TAntecedentResult>> continuationAction, TaskContinuationOptions continuationOptions, CancellationToken cancellationToken, TaskScheduler scheduler, ref StackCrawlMark stackMark)
		{
			TaskFactory.CheckMultiTaskContinuationOptions(continuationOptions);
			if (tasks == null)
			{
				throw new ArgumentNullException("tasks");
			}
			if (tasks.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Task_MultiTaskContinuation_EmptyTaskList"), "tasks");
			}
			if (scheduler == null)
			{
				throw new ArgumentNullException("scheduler");
			}
			Task<Task> task = TaskFactory.CommonCWAnyLogic(tasks);
			if (cancellationToken.IsCancellationRequested && (continuationOptions & TaskContinuationOptions.LazyCancellation) == TaskContinuationOptions.None)
			{
				return TaskFactory<TResult>.CreateCanceledTask(continuationOptions, cancellationToken);
			}
			if (continuationFunction != null)
			{
				return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAnyFuncDelegate, continuationFunction, scheduler, cancellationToken, continuationOptions, ref stackMark);
			}
			return task.ContinueWith<TResult>(GenericDelegateCache<TAntecedentResult, TResult>.CWAnyActionDelegate, continuationAction, scheduler, cancellationToken, continuationOptions, ref stackMark);
		}

		// Token: 0x04001A07 RID: 6663
		private CancellationToken m_defaultCancellationToken;

		// Token: 0x04001A08 RID: 6664
		private TaskScheduler m_defaultScheduler;

		// Token: 0x04001A09 RID: 6665
		private TaskCreationOptions m_defaultCreationOptions;

		// Token: 0x04001A0A RID: 6666
		private TaskContinuationOptions m_defaultContinuationOptions;

		// Token: 0x02000BCC RID: 3020
		private sealed class FromAsyncTrimPromise<TInstance> : Task<TResult> where TInstance : class
		{
			// Token: 0x06006E6A RID: 28266 RVA: 0x0017BFCF File Offset: 0x0017A1CF
			internal FromAsyncTrimPromise(TInstance thisRef, Func<TInstance, IAsyncResult, TResult> endMethod)
			{
				this.m_thisRef = thisRef;
				this.m_endMethod = endMethod;
			}

			// Token: 0x06006E6B RID: 28267 RVA: 0x0017BFE8 File Offset: 0x0017A1E8
			internal static void CompleteFromAsyncResult(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				TaskFactory<TResult>.FromAsyncTrimPromise<TInstance> fromAsyncTrimPromise = asyncResult.AsyncState as TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>;
				if (fromAsyncTrimPromise == null)
				{
					throw new ArgumentException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndCalledMultiple"), "asyncResult");
				}
				TInstance thisRef = fromAsyncTrimPromise.m_thisRef;
				Func<TInstance, IAsyncResult, TResult> endMethod = fromAsyncTrimPromise.m_endMethod;
				fromAsyncTrimPromise.m_thisRef = default(TInstance);
				fromAsyncTrimPromise.m_endMethod = null;
				if (endMethod == null)
				{
					throw new ArgumentException(Environment.GetResourceString("InvalidOperation_WrongAsyncResultOrEndCalledMultiple"), "asyncResult");
				}
				if (!asyncResult.CompletedSynchronously)
				{
					fromAsyncTrimPromise.Complete(thisRef, endMethod, asyncResult, true);
				}
			}

			// Token: 0x06006E6C RID: 28268 RVA: 0x0017C074 File Offset: 0x0017A274
			internal void Complete(TInstance thisRef, Func<TInstance, IAsyncResult, TResult> endMethod, IAsyncResult asyncResult, bool requiresSynchronization)
			{
				try
				{
					TResult result = endMethod(thisRef, asyncResult);
					if (requiresSynchronization)
					{
						bool flag = base.TrySetResult(result);
					}
					else
					{
						base.DangerousSetResult(result);
					}
				}
				catch (OperationCanceledException ex)
				{
					bool flag = base.TrySetCanceled(ex.CancellationToken, ex);
				}
				catch (Exception exceptionObject)
				{
					bool flag = base.TrySetException(exceptionObject);
				}
			}

			// Token: 0x04003572 RID: 13682
			internal static readonly AsyncCallback s_completeFromAsyncResult = new AsyncCallback(TaskFactory<TResult>.FromAsyncTrimPromise<TInstance>.CompleteFromAsyncResult);

			// Token: 0x04003573 RID: 13683
			private TInstance m_thisRef;

			// Token: 0x04003574 RID: 13684
			private Func<TInstance, IAsyncResult, TResult> m_endMethod;
		}
	}
}
