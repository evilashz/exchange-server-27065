using System;
using System.Diagnostics.Tracing;
using System.Security;
using Microsoft.Win32;

namespace System.Threading
{
	// Token: 0x02000502 RID: 1282
	internal sealed class TimerQueueTimer
	{
		// Token: 0x06003D25 RID: 15653 RVA: 0x000E3758 File Offset: 0x000E1958
		[SecurityCritical]
		internal TimerQueueTimer(TimerCallback timerCallback, object state, uint dueTime, uint period, ref StackCrawlMark stackMark)
		{
			this.m_timerCallback = timerCallback;
			this.m_state = state;
			this.m_dueTime = uint.MaxValue;
			this.m_period = uint.MaxValue;
			if (!ExecutionContext.IsFlowSuppressed())
			{
				this.m_executionContext = ExecutionContext.Capture(ref stackMark, ExecutionContext.CaptureOptions.IgnoreSyncCtx | ExecutionContext.CaptureOptions.OptimizeDefaultCase);
			}
			if (dueTime != 4294967295U)
			{
				this.Change(dueTime, period);
			}
		}

		// Token: 0x06003D26 RID: 15654 RVA: 0x000E37AC File Offset: 0x000E19AC
		internal bool Change(uint dueTime, uint period)
		{
			TimerQueue instance = TimerQueue.Instance;
			bool result;
			lock (instance)
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
						TimerQueue.Instance.DeleteTimer(this);
						result = true;
					}
					else
					{
						if (FrameworkEventSource.IsInitialized && FrameworkEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords)16L))
						{
							FrameworkEventSource.Log.ThreadTransferSendObj(this, 1, string.Empty, true);
						}
						result = TimerQueue.Instance.UpdateTimer(this, dueTime, period);
					}
				}
			}
			return result;
		}

		// Token: 0x06003D27 RID: 15655 RVA: 0x000E3860 File Offset: 0x000E1A60
		public void Close()
		{
			TimerQueue instance = TimerQueue.Instance;
			lock (instance)
			{
				try
				{
				}
				finally
				{
					if (!this.m_canceled)
					{
						this.m_canceled = true;
						TimerQueue.Instance.DeleteTimer(this);
					}
				}
			}
		}

		// Token: 0x06003D28 RID: 15656 RVA: 0x000E38C4 File Offset: 0x000E1AC4
		public bool Close(WaitHandle toSignal)
		{
			bool flag = false;
			TimerQueue instance = TimerQueue.Instance;
			bool result;
			lock (instance)
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
						TimerQueue.Instance.DeleteTimer(this);
						if (this.m_callbacksRunning == 0)
						{
							flag = true;
						}
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

		// Token: 0x06003D29 RID: 15657 RVA: 0x000E3950 File Offset: 0x000E1B50
		internal void Fire()
		{
			bool flag = false;
			TimerQueue instance = TimerQueue.Instance;
			lock (instance)
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
			TimerQueue instance2 = TimerQueue.Instance;
			lock (instance2)
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

		// Token: 0x06003D2A RID: 15658 RVA: 0x000E3A30 File Offset: 0x000E1C30
		[SecuritySafeCritical]
		internal void SignalNoCallbacksRunning()
		{
			Win32Native.SetEvent(this.m_notifyWhenNoCallbacksRunning.SafeWaitHandle);
		}

		// Token: 0x06003D2B RID: 15659 RVA: 0x000E3A48 File Offset: 0x000E1C48
		[SecuritySafeCritical]
		internal void CallCallback()
		{
			if (FrameworkEventSource.IsInitialized && FrameworkEventSource.Log.IsEnabled(EventLevel.Informational, (EventKeywords)16L))
			{
				FrameworkEventSource.Log.ThreadTransferReceiveObj(this, 1, string.Empty);
			}
			if (this.m_executionContext == null)
			{
				this.m_timerCallback(this.m_state);
				return;
			}
			using (ExecutionContext executionContext = this.m_executionContext.IsPreAllocatedDefault ? this.m_executionContext : this.m_executionContext.CreateCopy())
			{
				ContextCallback contextCallback = TimerQueueTimer.s_callCallbackInContext;
				if (contextCallback == null)
				{
					contextCallback = (TimerQueueTimer.s_callCallbackInContext = new ContextCallback(TimerQueueTimer.CallCallbackInContext));
				}
				ExecutionContext.Run(executionContext, contextCallback, this, true);
			}
		}

		// Token: 0x06003D2C RID: 15660 RVA: 0x000E3AFC File Offset: 0x000E1CFC
		[SecurityCritical]
		private static void CallCallbackInContext(object state)
		{
			TimerQueueTimer timerQueueTimer = (TimerQueueTimer)state;
			timerQueueTimer.m_timerCallback(timerQueueTimer.m_state);
		}

		// Token: 0x0400197E RID: 6526
		internal TimerQueueTimer m_next;

		// Token: 0x0400197F RID: 6527
		internal TimerQueueTimer m_prev;

		// Token: 0x04001980 RID: 6528
		internal int m_startTicks;

		// Token: 0x04001981 RID: 6529
		internal uint m_dueTime;

		// Token: 0x04001982 RID: 6530
		internal uint m_period;

		// Token: 0x04001983 RID: 6531
		private readonly TimerCallback m_timerCallback;

		// Token: 0x04001984 RID: 6532
		private readonly object m_state;

		// Token: 0x04001985 RID: 6533
		private readonly ExecutionContext m_executionContext;

		// Token: 0x04001986 RID: 6534
		private int m_callbacksRunning;

		// Token: 0x04001987 RID: 6535
		private volatile bool m_canceled;

		// Token: 0x04001988 RID: 6536
		private volatile WaitHandle m_notifyWhenNoCallbacksRunning;

		// Token: 0x04001989 RID: 6537
		[SecurityCritical]
		private static ContextCallback s_callCallbackInContext;
	}
}
