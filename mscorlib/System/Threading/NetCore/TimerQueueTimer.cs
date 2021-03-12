using System;
using System.Diagnostics.Tracing;
using System.Security;
using Microsoft.Win32;

namespace System.Threading.NetCore
{
	// Token: 0x0200055E RID: 1374
	internal sealed class TimerQueueTimer : IThreadPoolWorkItem
	{
		// Token: 0x06004198 RID: 16792 RVA: 0x000F4384 File Offset: 0x000F2584
		[SecuritySafeCritical]
		internal TimerQueueTimer(TimerCallback timerCallback, object state, uint dueTime, uint period, bool flowExecutionContext, ref StackCrawlMark stackMark)
		{
			this.m_timerCallback = timerCallback;
			this.m_state = state;
			this.m_dueTime = uint.MaxValue;
			this.m_period = uint.MaxValue;
			if (flowExecutionContext)
			{
				this.m_executionContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
			}
			this.m_associatedTimerQueue = TimerQueue.Instances[Thread.GetCurrentProcessorId() % TimerQueue.Instances.Length];
			if (dueTime != 4294967295U)
			{
				this.Change(dueTime, period);
			}
		}

		// Token: 0x06004199 RID: 16793 RVA: 0x000F43EC File Offset: 0x000F25EC
		internal bool Change(uint dueTime, uint period)
		{
			TimerQueue associatedTimerQueue = this.m_associatedTimerQueue;
			bool result;
			lock (associatedTimerQueue)
			{
				if (this.m_canceled)
				{
					throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_Generic"));
				}
				try
				{
				}
				finally
				{
					this.m_period = period;
					if (dueTime == 4294967295U)
					{
						this.m_associatedTimerQueue.DeleteTimer(this);
						result = true;
					}
					else
					{
						if (FrameworkEventSource.IsInitialized && FrameworkEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords)16L))
						{
							FrameworkEventSource.Log.ThreadTransferSendObj(this, 1, string.Empty, true);
						}
						result = this.m_associatedTimerQueue.UpdateTimer(this, dueTime, period);
					}
				}
			}
			return result;
		}

		// Token: 0x0600419A RID: 16794 RVA: 0x000F44A4 File Offset: 0x000F26A4
		public void Close()
		{
			TimerQueue associatedTimerQueue = this.m_associatedTimerQueue;
			lock (associatedTimerQueue)
			{
				try
				{
				}
				finally
				{
					if (!this.m_canceled)
					{
						this.m_canceled = true;
						this.m_associatedTimerQueue.DeleteTimer(this);
					}
				}
			}
		}

		// Token: 0x0600419B RID: 16795 RVA: 0x000F450C File Offset: 0x000F270C
		public bool Close(WaitHandle toSignal)
		{
			bool flag = false;
			TimerQueue associatedTimerQueue = this.m_associatedTimerQueue;
			bool result;
			lock (associatedTimerQueue)
			{
				try
				{
				}
				finally
				{
					if (this.m_canceled)
					{
						result = false;
					}
					else
					{
						this.m_canceled = true;
						this.m_notifyWhenNoCallbacksRunning = toSignal;
						this.m_associatedTimerQueue.DeleteTimer(this);
						flag = (this.m_callbacksRunning == 0);
						result = true;
					}
				}
			}
			if (flag)
			{
				this.SignalNoCallbacksRunning();
			}
			return result;
		}

		// Token: 0x0600419C RID: 16796 RVA: 0x000F4598 File Offset: 0x000F2798
		[SecurityCritical]
		void IThreadPoolWorkItem.ExecuteWorkItem()
		{
			this.Fire();
		}

		// Token: 0x0600419D RID: 16797 RVA: 0x000F45A0 File Offset: 0x000F27A0
		[SecurityCritical]
		void IThreadPoolWorkItem.MarkAborted(ThreadAbortException tae)
		{
		}

		// Token: 0x0600419E RID: 16798 RVA: 0x000F45A4 File Offset: 0x000F27A4
		internal void Fire()
		{
			bool flag = false;
			TimerQueue associatedTimerQueue = this.m_associatedTimerQueue;
			lock (associatedTimerQueue)
			{
				try
				{
				}
				finally
				{
					flag = this.m_canceled;
					if (!flag)
					{
						this.m_callbacksRunning++;
					}
				}
			}
			if (flag)
			{
				return;
			}
			this.CallCallback();
			bool flag3 = false;
			TimerQueue associatedTimerQueue2 = this.m_associatedTimerQueue;
			lock (associatedTimerQueue2)
			{
				try
				{
				}
				finally
				{
					this.m_callbacksRunning--;
					if (this.m_canceled && this.m_callbacksRunning == 0 && this.m_notifyWhenNoCallbacksRunning != null)
					{
						flag3 = true;
					}
				}
			}
			if (flag3)
			{
				this.SignalNoCallbacksRunning();
			}
		}

		// Token: 0x0600419F RID: 16799 RVA: 0x000F4684 File Offset: 0x000F2884
		[SecuritySafeCritical]
		internal void SignalNoCallbacksRunning()
		{
			Win32Native.SetEvent(this.m_notifyWhenNoCallbacksRunning.SafeWaitHandle);
		}

		// Token: 0x060041A0 RID: 16800 RVA: 0x000F469C File Offset: 0x000F289C
		[SecuritySafeCritical]
		internal void CallCallback()
		{
			if (FrameworkEventSource.IsInitialized && FrameworkEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords)16L))
			{
				FrameworkEventSource.Log.ThreadTransferReceiveObj(this, 1, string.Empty);
			}
			ExecutionContext executionContext = this.m_executionContext;
			if (executionContext == null)
			{
				this.m_timerCallback(this.m_state);
				return;
			}
			ExecutionContext executionContext2;
			executionContext = (executionContext2 = (executionContext.IsPreAllocatedDefault ? executionContext : executionContext.CreateCopy()));
			try
			{
				if (TimerQueueTimer.s_callCallbackInContext == null)
				{
					TimerQueueTimer.s_callCallbackInContext = new ContextCallback(TimerQueueTimer.CallCallbackInContext);
				}
				ExecutionContext.Run(executionContext, TimerQueueTimer.s_callCallbackInContext, this, true);
			}
			finally
			{
				if (executionContext2 != null)
				{
					((IDisposable)executionContext2).Dispose();
				}
			}
		}

		// Token: 0x060041A1 RID: 16801 RVA: 0x000F4748 File Offset: 0x000F2948
		[SecurityCritical]
		private static void CallCallbackInContext(object state)
		{
			TimerQueueTimer timerQueueTimer = (TimerQueueTimer)state;
			timerQueueTimer.m_timerCallback(timerQueueTimer.m_state);
		}

		// Token: 0x04001B0D RID: 6925
		private readonly TimerQueue m_associatedTimerQueue;

		// Token: 0x04001B0E RID: 6926
		internal TimerQueueTimer m_next;

		// Token: 0x04001B0F RID: 6927
		internal TimerQueueTimer m_prev;

		// Token: 0x04001B10 RID: 6928
		internal bool m_short;

		// Token: 0x04001B11 RID: 6929
		internal long m_startTicks;

		// Token: 0x04001B12 RID: 6930
		internal uint m_dueTime;

		// Token: 0x04001B13 RID: 6931
		internal uint m_period;

		// Token: 0x04001B14 RID: 6932
		private readonly TimerCallback m_timerCallback;

		// Token: 0x04001B15 RID: 6933
		private readonly object m_state;

		// Token: 0x04001B16 RID: 6934
		private readonly ExecutionContext m_executionContext;

		// Token: 0x04001B17 RID: 6935
		private int m_callbacksRunning;

		// Token: 0x04001B18 RID: 6936
		private volatile bool m_canceled;

		// Token: 0x04001B19 RID: 6937
		private volatile WaitHandle m_notifyWhenNoCallbacksRunning;

		// Token: 0x04001B1A RID: 6938
		[SecurityCritical]
		private static ContextCallback s_callCallbackInContext;
	}
}
