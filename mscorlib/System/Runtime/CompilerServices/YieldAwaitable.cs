using System;
using System.Diagnostics.Tracing;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008CD RID: 2253
	[__DynamicallyInvokable]
	public struct YieldAwaitable
	{
		// Token: 0x06005D32 RID: 23858 RVA: 0x001469B8 File Offset: 0x00144BB8
		[__DynamicallyInvokable]
		public YieldAwaitable.YieldAwaiter GetAwaiter()
		{
			return default(YieldAwaitable.YieldAwaiter);
		}

		// Token: 0x02000C61 RID: 3169
		[__DynamicallyInvokable]
		[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
		public struct YieldAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x17001347 RID: 4935
			// (get) Token: 0x06006FDD RID: 28637 RVA: 0x00180595 File Offset: 0x0017E795
			[__DynamicallyInvokable]
			public bool IsCompleted
			{
				[__DynamicallyInvokable]
				get
				{
					return false;
				}
			}

			// Token: 0x06006FDE RID: 28638 RVA: 0x00180598 File Offset: 0x0017E798
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			public void OnCompleted(Action continuation)
			{
				YieldAwaitable.YieldAwaiter.QueueContinuation(continuation, true);
			}

			// Token: 0x06006FDF RID: 28639 RVA: 0x001805A1 File Offset: 0x0017E7A1
			[SecurityCritical]
			[__DynamicallyInvokable]
			public void UnsafeOnCompleted(Action continuation)
			{
				YieldAwaitable.YieldAwaiter.QueueContinuation(continuation, false);
			}

			// Token: 0x06006FE0 RID: 28640 RVA: 0x001805AC File Offset: 0x0017E7AC
			[SecurityCritical]
			private static void QueueContinuation(Action continuation, bool flowContext)
			{
				if (continuation == null)
				{
					throw new ArgumentNullException("continuation");
				}
				if (TplEtwProvider.Log.IsEnabled())
				{
					continuation = YieldAwaitable.YieldAwaiter.OutputCorrelationEtwEvent(continuation);
				}
				SynchronizationContext currentNoFlow = SynchronizationContext.CurrentNoFlow;
				if (currentNoFlow != null && currentNoFlow.GetType() != typeof(SynchronizationContext))
				{
					currentNoFlow.Post(YieldAwaitable.YieldAwaiter.s_sendOrPostCallbackRunAction, continuation);
					return;
				}
				TaskScheduler taskScheduler = TaskScheduler.Current;
				if (taskScheduler != TaskScheduler.Default)
				{
					Task.Factory.StartNew(continuation, default(CancellationToken), TaskCreationOptions.PreferFairness, taskScheduler);
					return;
				}
				if (flowContext)
				{
					ThreadPool.QueueUserWorkItem(YieldAwaitable.YieldAwaiter.s_waitCallbackRunAction, continuation);
					return;
				}
				ThreadPool.UnsafeQueueUserWorkItem(YieldAwaitable.YieldAwaiter.s_waitCallbackRunAction, continuation);
			}

			// Token: 0x06006FE1 RID: 28641 RVA: 0x0018064C File Offset: 0x0017E84C
			private static Action OutputCorrelationEtwEvent(Action continuation)
			{
				int continuationId = Task.NewId();
				Task internalCurrent = Task.InternalCurrent;
				TplEtwProvider.Log.AwaitTaskContinuationScheduled(TaskScheduler.Current.Id, (internalCurrent != null) ? internalCurrent.Id : 0, continuationId);
				return AsyncMethodBuilderCore.CreateContinuationWrapper(continuation, delegate
				{
					TplEtwProvider log = TplEtwProvider.Log;
					log.TaskWaitContinuationStarted(continuationId);
					Guid currentThreadActivityId = default(Guid);
					if (log.TasksSetActivityIds)
					{
						EventSource.SetCurrentThreadActivityId(TplEtwProvider.CreateGuidForTaskID(continuationId), out currentThreadActivityId);
					}
					continuation();
					if (log.TasksSetActivityIds)
					{
						EventSource.SetCurrentThreadActivityId(currentThreadActivityId);
					}
					log.TaskWaitContinuationComplete(continuationId);
				}, null);
			}

			// Token: 0x06006FE2 RID: 28642 RVA: 0x001806B5 File Offset: 0x0017E8B5
			private static void RunAction(object state)
			{
				((Action)state)();
			}

			// Token: 0x06006FE3 RID: 28643 RVA: 0x001806C2 File Offset: 0x0017E8C2
			[__DynamicallyInvokable]
			public void GetResult()
			{
			}

			// Token: 0x0400376C RID: 14188
			private static readonly WaitCallback s_waitCallbackRunAction = new WaitCallback(YieldAwaitable.YieldAwaiter.RunAction);

			// Token: 0x0400376D RID: 14189
			private static readonly SendOrPostCallback s_sendOrPostCallbackRunAction = new SendOrPostCallback(YieldAwaitable.YieldAwaiter.RunAction);
		}
	}
}
