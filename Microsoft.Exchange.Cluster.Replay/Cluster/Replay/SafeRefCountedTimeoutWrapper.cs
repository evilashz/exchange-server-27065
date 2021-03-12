using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000118 RID: 280
	internal abstract class SafeRefCountedTimeoutWrapper : DisposeTrackableBase
	{
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x0002FDEA File Offset: 0x0002DFEA
		private static Trace Tracer
		{
			get
			{
				return SafeRefCountedTimeoutWrapper.s_trace;
			}
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0002FDF1 File Offset: 0x0002DFF1
		protected SafeRefCountedTimeoutWrapper(string debugName) : this(debugName, null)
		{
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0002FDFB File Offset: 0x0002DFFB
		protected SafeRefCountedTimeoutWrapper(string debugName, ManualOneShotEvent cancelEvent)
		{
			this.m_name = debugName;
			this.m_cancelEvent = cancelEvent;
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x0002FE1C File Offset: 0x0002E01C
		protected string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x0002FE24 File Offset: 0x0002E024
		protected ManualOneShotEvent CancelEvent
		{
			get
			{
				return this.m_cancelEvent;
			}
		}

		// Token: 0x06000AA8 RID: 2728
		protected abstract void InternalProtectedDispose();

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0002FE2C File Offset: 0x0002E02C
		protected virtual Exception GetOperationTimedOutException(string operationName, TimeoutException timeoutEx)
		{
			return timeoutEx;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0002FE2F File Offset: 0x0002E02F
		protected virtual Exception GetOperationCanceledException(string operationName, OperationAbortedException abortedEx)
		{
			return abortedEx;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0002FE32 File Offset: 0x0002E032
		protected void ProtectedCall(string operationName, Action operation)
		{
			this.ProtectedCallWithTimeout(operationName, InvokeWithTimeout.InfiniteTimeSpan, operation);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0002FE84 File Offset: 0x0002E084
		protected void ProtectedCallWithTimeout(string operationName, TimeSpan timeout, Action operation)
		{
			try
			{
				if (!this.IncrementRefCountIfNecessary())
				{
					throw this.GetOperationTimedOutException(operationName, new TimeoutException(string.Format("RefCount fails. Name={0}", this.Name)));
				}
				Action invokableAction = delegate()
				{
					try
					{
						operation();
					}
					finally
					{
						this.DecrementRefCountAndDisposeIfNecessary();
					}
				};
				InvokeWithTimeout.Invoke(invokableAction, timeout, this.m_cancelEvent);
			}
			catch (OperationAbortedException ex)
			{
				SafeRefCountedTimeoutWrapper.Tracer.TraceError<string, OperationAbortedException>((long)this.GetHashCode(), "SafeRefCountedTimeoutWrapper.ProtectedCallWithTimeout(): Operation '{0}' got canceled. Exception: {1}", operationName, ex);
				throw this.GetOperationCanceledException(operationName, ex);
			}
			catch (TimeoutException ex2)
			{
				SafeRefCountedTimeoutWrapper.Tracer.TraceError<string, TimeoutException>((long)this.GetHashCode(), "SafeRefCountedTimeoutWrapper.ProtectedCallWithTimeout(): Operation '{0}' timed out. Exception: {1}", operationName, ex2);
				throw this.GetOperationTimedOutException(operationName, ex2);
			}
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0002FF54 File Offset: 0x0002E154
		public override void Dispose()
		{
			lock (this.m_lockObj)
			{
				if (this.m_disposeCompleted)
				{
					SafeRefCountedTimeoutWrapper.Tracer.TraceDebug<string>((long)this.GetHashCode(), "SafeRefCountedTimeoutWrapper.Dispose(): {0}: Object has already been disposed. Doing nothing.", this.Name);
					return;
				}
				this.m_disposeRequested = true;
				if (this.m_threadsInProtectedCalls > 0)
				{
					SafeRefCountedTimeoutWrapper.Tracer.TraceDebug<string, int>((long)this.GetHashCode(), "SafeRefCountedTimeoutWrapper.Dispose(): {0}: There are currently {1} threads in protected calls. Delaying the dispose until the last thread completes.", this.Name, this.m_threadsInProtectedCalls);
					return;
				}
			}
			base.Dispose();
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0002FFF0 File Offset: 0x0002E1F0
		protected override void InternalDispose(bool disposing)
		{
			bool flag = false;
			lock (this.m_lockObj)
			{
				if (!this.m_disposeCompleted && !this.m_disposeStarted)
				{
					flag = true;
					this.m_disposeStarted = true;
				}
			}
			if (flag)
			{
				if (disposing)
				{
					this.m_dbgDisposeCalledUtc = DateTimeHelper.ToPersistedString(DateTime.UtcNow);
					this.InternalProtectedDispose();
				}
				this.m_disposeCompleted = true;
			}
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00030068 File Offset: 0x0002E268
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SafeRefCountedTimeoutWrapper>(this);
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00030070 File Offset: 0x0002E270
		protected bool IncrementRefCountIfNecessary()
		{
			bool result;
			lock (this.m_lockObj)
			{
				if (this.m_disposeRequested)
				{
					SafeRefCountedTimeoutWrapper.Tracer.TraceError<string>((long)this.GetHashCode(), "SafeRefCountedTimeoutWrapper.IncrementRefCountIfNecessary(): {0}: m_disposeRequested is true, so returning false to stop issuing new protected calls!", this.Name);
					result = false;
				}
				else
				{
					this.m_threadsInProtectedCalls++;
					SafeRefCountedTimeoutWrapper.Tracer.TraceDebug<string, int>((long)this.GetHashCode(), "SafeRefCountedTimeoutWrapper.IncrementRefCountIfNecessary(): {0}: Successfully recorded a new protected call. m_threadsInProtectedCalls = {1}", this.Name, this.m_threadsInProtectedCalls);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00030108 File Offset: 0x0002E308
		protected void DecrementRefCountAndDisposeIfNecessary()
		{
			bool flag = false;
			lock (this.m_lockObj)
			{
				this.m_threadsInProtectedCalls--;
				if (this.m_disposeRequested)
				{
					flag = true;
				}
			}
			if (flag)
			{
				this.Dispose();
			}
		}

		// Token: 0x0400047E RID: 1150
		private readonly string m_name;

		// Token: 0x0400047F RID: 1151
		private readonly object m_lockObj = new object();

		// Token: 0x04000480 RID: 1152
		private readonly ManualOneShotEvent m_cancelEvent;

		// Token: 0x04000481 RID: 1153
		private static Trace s_trace = ExTraceGlobals.ReplayApiTracer;

		// Token: 0x04000482 RID: 1154
		private bool m_disposeRequested;

		// Token: 0x04000483 RID: 1155
		private bool m_disposeCompleted;

		// Token: 0x04000484 RID: 1156
		private bool m_disposeStarted;

		// Token: 0x04000485 RID: 1157
		private int m_threadsInProtectedCalls;

		// Token: 0x04000486 RID: 1158
		private string m_dbgDisposeCalledUtc;
	}
}
