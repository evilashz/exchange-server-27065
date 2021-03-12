using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Parser;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x02000024 RID: 36
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class AsyncOperationExecutor : BaseObject, IAsyncOperationExecutor
	{
		// Token: 0x060001E2 RID: 482 RVA: 0x000114A4 File Offset: 0x0000F6A4
		public AsyncOperationExecutor(SegmentedRopOperation segmentedRopOperation, object progressToken, Action postExecution)
		{
			Util.ThrowOnNullArgument(segmentedRopOperation, "segmentedRopOperation");
			this.segmentedRopOperation = segmentedRopOperation;
			this.progressToken = progressToken;
			this.postExecution = postExecution;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000114F9 File Offset: 0x0000F6F9
		public void BeginOperation(bool useSameThread)
		{
			this.ChangeState(AsyncOperationExecutor.AsyncOperationState.Started);
			if (!useSameThread)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(AsyncOperationExecutor.SegmentedOperation), this);
				this.checkQuickAsyncOperationEvent.WaitOne(TimeSpan.FromSeconds(2.0));
				return;
			}
			AsyncOperationExecutor.SegmentedOperation(this);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0001153C File Offset: 0x0000F73C
		public void EndOperation()
		{
			bool flag = false;
			if (!this.IsCompleted)
			{
				flag = true;
				this.TraceDebug("The operation is not completed and is just canceled. Operation = {0}", new object[]
				{
					this
				});
				this.ChangeState(AsyncOperationExecutor.AsyncOperationState.Completed);
			}
			if (TestInterceptor.CountCondition != null)
			{
				TestInterceptor.CountCondition.Release(1);
			}
			this.WaitForStopped();
			if (flag)
			{
				this.segmentedRopOperation.ErrorCode = (ErrorCode)2147746067U;
				this.checkQuickAsyncOperationEvent.Set();
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000115AB File Offset: 0x0000F7AB
		public bool IsCompleted
		{
			get
			{
				return this.state == AsyncOperationExecutor.AsyncOperationState.Completed;
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x000115B8 File Offset: 0x0000F7B8
		public void WaitForStopped()
		{
			for (;;)
			{
				lock (this.lockAsyncThreadStopped)
				{
					if (this.state == AsyncOperationExecutor.AsyncOperationState.Completed)
					{
						break;
					}
				}
				Thread.Sleep(0);
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00011608 File Offset: 0x0000F808
		public void GetProgressInfo(out object progressToken, out ProgressInfo progressInfo)
		{
			if (this.IsCompleted && this.segmentedRopOperation.Exception != null)
			{
				ExTraceGlobals.AsyncRopHandlerTracer.TraceError<Exception>((long)this.GetHashCode(), "Exception thrown from segmentedRopOperation. Exception = {0}.", this.segmentedRopOperation.Exception);
				throw this.segmentedRopOperation.Exception;
			}
			progressToken = this.progressToken;
			progressInfo = new ProgressInfo
			{
				CompletedTaskCount = (uint)this.segmentedRopOperation.CompletedWork,
				TotalTaskCount = (uint)this.segmentedRopOperation.TotalWork,
				IsCompleted = this.IsCompleted,
				CreateCompleteResult = new Func<object, IProgressResultFactory, RopResult>(this.segmentedRopOperation.CreateCompleteResult),
				CreateCompleteResultForProgress = new Func<object, ProgressResultFactory, RopResult>(this.segmentedRopOperation.CreateCompleteResultForProgress)
			};
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000116D1 File Offset: 0x0000F8D1
		public override string ToString()
		{
			return string.Format("State = {0}, segmentedRopOperation = {1}", this.state, this.segmentedRopOperation);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000116EE File Offset: 0x0000F8EE
		internal void SuppressSendReport(Action<Exception> suppressExceptionDelegate)
		{
			this.suppressExceptionDelegate = suppressExceptionDelegate;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00011788 File Offset: 0x0000F988
		private static void SegmentedOperation(object state)
		{
			AsyncOperationExecutor executor = (AsyncOperationExecutor)state;
			bool flag = false;
			try
			{
				object obj;
				Monitor.Enter(obj = executor.lockAsyncThreadStopped, ref flag);
				bool moreWorkToDo = true;
				do
				{
					TestInterceptor.Intercept(TestInterceptorLocation.AsyncOperationExecutor_SegmentedOperation, new object[0]);
					executor.TraceDebug("The current state. State = {0}", new object[]
					{
						executor.state
					});
					if (executor.state == AsyncOperationExecutor.AsyncOperationState.Completed)
					{
						break;
					}
					try
					{
						executor.TraceDebug(">>> Starting a segmented batch operation. Operation = {0}", new object[]
						{
							executor
						});
						ExWatson.SendReportOnUnhandledException(delegate()
						{
							moreWorkToDo = executor.segmentedRopOperation.DoNextBatchOperation();
							if (executor.segmentedRopOperation.Exception != null)
							{
								moreWorkToDo = false;
							}
						}, delegate(object exception)
						{
							executor.TraceError("Unhandled exception. {0}", new object[]
							{
								exception
							});
							return executor.suppressExceptionDelegate == null && ExceptionTranslator.IsInterestingForInfoWatson(exception as Exception);
						}, ReportOptions.None);
					}
					catch (Exception ex)
					{
						if (executor.suppressExceptionDelegate == null)
						{
							executor.TraceDebug("The operation is completed. Operation = {0}, exception = {1}.", new object[]
							{
								executor,
								ex
							});
							executor.ChangeState(AsyncOperationExecutor.AsyncOperationState.Completed);
							throw;
						}
						executor.suppressExceptionDelegate(ex);
						moreWorkToDo = false;
					}
					executor.TraceDebug("<<< Completed a segmented batch operation. Operation = {0}", new object[]
					{
						executor
					});
				}
				while (moreWorkToDo);
				executor.TraceDebug("The operation is completed. Operation = {0}", new object[]
				{
					executor
				});
				executor.ChangeState(AsyncOperationExecutor.AsyncOperationState.Completed);
				executor.checkQuickAsyncOperationEvent.Set();
				if (executor.postExecution != null)
				{
					executor.TraceDebug("The operation is completed. postExecution is being executed. Operation = {0}", new object[]
					{
						executor
					});
					executor.postExecution();
				}
			}
			finally
			{
				if (flag)
				{
					object obj;
					Monitor.Exit(obj);
				}
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000119E0 File Offset: 0x0000FBE0
		private void ChangeState(AsyncOperationExecutor.AsyncOperationState newState)
		{
			bool flag2;
			lock (this.lockChangingState)
			{
				if ((newState == AsyncOperationExecutor.AsyncOperationState.Started && this.state != AsyncOperationExecutor.AsyncOperationState.NotStarted) || (newState == AsyncOperationExecutor.AsyncOperationState.Completed && this.state == AsyncOperationExecutor.AsyncOperationState.NotStarted))
				{
					flag2 = true;
				}
				else
				{
					flag2 = false;
					this.state = newState;
				}
			}
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("Invalid state transition. Current = {0}, NewState = {1}.", this.state, newState));
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00011A64 File Offset: 0x0000FC64
		private void TraceError(string formatString, params object[] objects)
		{
			ExTraceGlobals.FailedRopTracer.TraceDebug((long)this.GetHashCode(), formatString, objects);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00011A79 File Offset: 0x0000FC79
		private void TraceDebug(string formatString, params object[] objects)
		{
			ExTraceGlobals.AsyncRopHandlerTracer.TraceDebug((long)this.GetHashCode(), formatString, objects);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00011A8E File Offset: 0x0000FC8E
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<AsyncOperationExecutor>(this);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00011A96 File Offset: 0x0000FC96
		protected override void InternalDispose()
		{
			this.EndOperation();
			this.segmentedRopOperation.Dispose();
			base.InternalDispose();
		}

		// Token: 0x0400009D RID: 157
		private readonly SegmentedRopOperation segmentedRopOperation;

		// Token: 0x0400009E RID: 158
		private readonly object progressToken;

		// Token: 0x0400009F RID: 159
		private readonly object lockChangingState = new object();

		// Token: 0x040000A0 RID: 160
		private readonly object lockAsyncThreadStopped = new object();

		// Token: 0x040000A1 RID: 161
		private readonly Action postExecution;

		// Token: 0x040000A2 RID: 162
		private AutoResetEvent checkQuickAsyncOperationEvent = new AutoResetEvent(false);

		// Token: 0x040000A3 RID: 163
		private Action<Exception> suppressExceptionDelegate;

		// Token: 0x040000A4 RID: 164
		private AsyncOperationExecutor.AsyncOperationState state;

		// Token: 0x02000025 RID: 37
		private enum AsyncOperationState
		{
			// Token: 0x040000A6 RID: 166
			NotStarted,
			// Token: 0x040000A7 RID: 167
			Completed,
			// Token: 0x040000A8 RID: 168
			Started
		}
	}
}
