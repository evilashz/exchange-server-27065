using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Data.HA
{
	// Token: 0x02000003 RID: 3
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ManualOneShotEvent : DisposeTrackableBase
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002603 File Offset: 0x00000803
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ReplayApiTracer;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000260A File Offset: 0x0000080A
		public ManualOneShotEvent(string debugName)
		{
			this.m_name = debugName;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002625 File Offset: 0x00000825
		public bool IsSignaled
		{
			get
			{
				return this.m_firstEventCompletedSignaled;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000262D File Offset: 0x0000082D
		public ManualOneShotEvent.Result WaitOne()
		{
			return this.WaitOne(InvokeWithTimeout.InfiniteTimeSpan);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000263C File Offset: 0x0000083C
		public ManualOneShotEvent.Result WaitOne(TimeSpan timeout)
		{
			ManualOneShotEvent.IncrementResult incrementResult = this.IncrementRefCountIfNecessary();
			if (incrementResult == ManualOneShotEvent.IncrementResult.NotIncrementedAlreadySignaled)
			{
				return ManualOneShotEvent.Result.Success;
			}
			if (incrementResult == ManualOneShotEvent.IncrementResult.NotIncrementedShuttingDown)
			{
				return ManualOneShotEvent.Result.ShuttingDown;
			}
			ManualOneShotEvent.Result result;
			try
			{
				if (!this.m_firstEventCompleted.WaitOne(timeout))
				{
					ManualOneShotEvent.Tracer.TraceError<string, double>((long)this.GetHashCode(), "ManualOneShotEvent.WaitOne(): {0}: Waiting for event timed out after {1} msecs.", this.m_name, timeout.TotalMilliseconds);
					result = ManualOneShotEvent.Result.WaitTimedOut;
				}
				else
				{
					result = ManualOneShotEvent.Result.Success;
				}
			}
			finally
			{
				this.DecrementRefCountAndCloseIfNecessary();
			}
			return result;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000026D4 File Offset: 0x000008D4
		public static int WaitAny(object[] waitHandles, TimeSpan timeout)
		{
			if (waitHandles == null)
			{
				throw new ArgumentNullException("waitHandles");
			}
			if (waitHandles.Length == 0)
			{
				throw new ArgumentException("Empty waitHandles should not be passed in.");
			}
			int num = -1;
			List<WaitHandle> list = new List<WaitHandle>(waitHandles.Length);
			ManualOneShotEvent.ManualOneShotInfo[] array = new ManualOneShotEvent.ManualOneShotInfo[waitHandles.Length];
			try
			{
				for (int i = 0; i < waitHandles.Length; i++)
				{
					object obj = waitHandles[i];
					if (obj == null)
					{
						throw new ArgumentNullException("An array element of waitHandles should not be null.");
					}
					array[i] = new ManualOneShotEvent.ManualOneShotInfo();
					if (obj is ManualOneShotEvent)
					{
						ManualOneShotEvent.ManualOneShotInfo manualOneShotInfo = array[i];
						ManualOneShotEvent manualOneShotEvent = obj as ManualOneShotEvent;
						manualOneShotInfo.IsManualOneShotEvent = true;
						manualOneShotInfo.ManualOneShotEvent = manualOneShotEvent;
						ManualOneShotEvent.IncrementResult incrementResult = manualOneShotEvent.IncrementRefCountIfNecessary();
						if (incrementResult == ManualOneShotEvent.IncrementResult.NotIncrementedShuttingDown)
						{
							manualOneShotInfo.IsClosed = true;
						}
						else if (incrementResult == ManualOneShotEvent.IncrementResult.NotIncrementedAlreadySignaled)
						{
							manualOneShotInfo.IsPreSignaled = true;
						}
						else
						{
							manualOneShotInfo.IsWaitRegistered = true;
							list.Add(manualOneShotEvent.m_firstEventCompleted);
						}
					}
					else
					{
						if (!(obj is WaitHandle))
						{
							throw new ArgumentException("An object of type {0} was passed in to the array. It must be either of type ManualOneShotEvent or WaitHandle.", obj.GetType().ToString());
						}
						list.Add(obj as WaitHandle);
					}
				}
				if (array.All((ManualOneShotEvent.ManualOneShotInfo info) => info.IsManualOneShotEvent && info.IsClosed))
				{
					ManualOneShotEvent.Tracer.TraceError(0L, "ManualOneShotEvent.WaitAny(): Every event is a ManualOneShotEvent and is already closed! Returning WaitTimeout.");
					return 258;
				}
				if (array.Any((ManualOneShotEvent.ManualOneShotInfo info) => info.IsManualOneShotEvent && info.IsPreSignaled))
				{
					int num2 = -1;
					for (int j = 0; j < array.Length; j++)
					{
						ManualOneShotEvent.ManualOneShotInfo manualOneShotInfo2 = array[j];
						if (manualOneShotInfo2.IsManualOneShotEvent && manualOneShotInfo2.IsPreSignaled && num2 == -1)
						{
							num2 = j;
						}
						if (manualOneShotInfo2.IsManualOneShotEvent && manualOneShotInfo2.IsWaitRegistered)
						{
							manualOneShotInfo2.ManualOneShotEvent.DecrementRefCountAndCloseIfNecessary();
							manualOneShotInfo2.IsWaitUnregistered = true;
						}
					}
					return num2;
				}
				num = WaitHandle.WaitAny(list.ToArray(), timeout);
			}
			finally
			{
				foreach (ManualOneShotEvent.ManualOneShotInfo manualOneShotInfo3 in array)
				{
					if (manualOneShotInfo3 != null && manualOneShotInfo3.IsManualOneShotEvent && manualOneShotInfo3.IsWaitRegistered && !manualOneShotInfo3.IsWaitUnregistered)
					{
						manualOneShotInfo3.ManualOneShotEvent.DecrementRefCountAndCloseIfNecessary();
						manualOneShotInfo3.IsWaitUnregistered = true;
					}
				}
			}
			if (num == 258)
			{
				ManualOneShotEvent.Tracer.TraceError<TimeSpan>(0L, "ManualOneShotEvent.WaitAny(): Wait timed out after {0}!", timeout);
				return num;
			}
			ManualOneShotEvent.Tracer.TraceDebug<int>(0L, "ManualOneShotEvent.WaitAny(): Event with *internal* index {0} was signaled.", num);
			int num3 = -1;
			int num4 = 0;
			for (int l = 0; l < array.Length; l++)
			{
				ManualOneShotEvent.ManualOneShotInfo manualOneShotInfo4 = array[l];
				if (!manualOneShotInfo4.IsManualOneShotEvent || !manualOneShotInfo4.IsClosed)
				{
					num4++;
				}
				if (num == num4 - 1)
				{
					num3 = l;
					break;
				}
			}
			ManualOneShotEvent.Tracer.TraceDebug<int>(0L, "ManualOneShotEvent.WaitAny(): Event with index {0} was signaled. Returning {0}.", num3);
			return num3;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000029A4 File Offset: 0x00000BA4
		public void Set()
		{
			if (this.m_firstEventCompletedSignaled)
			{
				return;
			}
			lock (this)
			{
				if (!this.m_firstEventCompletedSignaled && this.m_firstEventCompleted != null && !this.m_disposeCalled)
				{
					this.m_firstEventCompleted.Set();
					this.m_firstEventCompletedSignaled = true;
					ManualOneShotEvent.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ManualOneShotEvent.Set(): {0}: First event completed!", this.m_name);
					this.Close();
				}
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002A30 File Offset: 0x00000C30
		public void Close()
		{
			lock (this)
			{
				if (this.m_disposeCalled)
				{
					return;
				}
				this.m_closeRequested = true;
				if (this.m_threadsWaiting > 0)
				{
					return;
				}
			}
			base.Dispose();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002A88 File Offset: 0x00000C88
		public new void Dispose()
		{
			this.Close();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002A90 File Offset: 0x00000C90
		protected override void InternalDispose(bool disposing)
		{
			lock (this)
			{
				if (!this.m_disposeCalled)
				{
					if (disposing)
					{
						this.m_firstEventCompleted.Close();
						this.m_firstEventCompleted = null;
					}
					this.m_disposeCalled = true;
				}
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002AEC File Offset: 0x00000CEC
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ManualOneShotEvent>(this);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002AF4 File Offset: 0x00000CF4
		private ManualOneShotEvent.IncrementResult IncrementRefCountIfNecessary()
		{
			if (this.m_firstEventCompletedSignaled)
			{
				return ManualOneShotEvent.IncrementResult.NotIncrementedAlreadySignaled;
			}
			lock (this)
			{
				if (this.m_firstEventCompletedSignaled)
				{
					return ManualOneShotEvent.IncrementResult.NotIncrementedAlreadySignaled;
				}
				if (this.m_closeRequested)
				{
					ManualOneShotEvent.Tracer.TraceError<string>((long)this.GetHashCode(), "ManualOneShotEvent.IncrementRefCountIfNecessary(): {0}: m_closeRequested is true, which means the object is shutting down!", this.m_name);
					return ManualOneShotEvent.IncrementResult.NotIncrementedShuttingDown;
				}
				this.m_threadsWaiting++;
				ManualOneShotEvent.Tracer.TraceDebug<string, int>((long)this.GetHashCode(), "ManualOneShotEvent.IncrementRefCountIfNecessary(): {0}: Successfully registered a Waiter. Ref count is now: {1}", this.m_name, this.m_threadsWaiting);
			}
			return ManualOneShotEvent.IncrementResult.Incremented;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002B9C File Offset: 0x00000D9C
		private void DecrementRefCountAndCloseIfNecessary()
		{
			lock (this)
			{
				this.m_threadsWaiting--;
				if (this.m_closeRequested)
				{
					this.Close();
				}
			}
		}

		// Token: 0x04000007 RID: 7
		private readonly string m_name;

		// Token: 0x04000008 RID: 8
		private ManualResetEvent m_firstEventCompleted = new ManualResetEvent(false);

		// Token: 0x04000009 RID: 9
		private bool m_firstEventCompletedSignaled;

		// Token: 0x0400000A RID: 10
		private bool m_closeRequested;

		// Token: 0x0400000B RID: 11
		private bool m_disposeCalled;

		// Token: 0x0400000C RID: 12
		private int m_threadsWaiting;

		// Token: 0x02000004 RID: 4
		internal enum Result
		{
			// Token: 0x04000010 RID: 16
			Success,
			// Token: 0x04000011 RID: 17
			WaitTimedOut,
			// Token: 0x04000012 RID: 18
			ShuttingDown
		}

		// Token: 0x02000005 RID: 5
		private enum IncrementResult
		{
			// Token: 0x04000014 RID: 20
			Incremented,
			// Token: 0x04000015 RID: 21
			NotIncrementedShuttingDown,
			// Token: 0x04000016 RID: 22
			NotIncrementedAlreadySignaled
		}

		// Token: 0x02000006 RID: 6
		private class ManualOneShotInfo
		{
			// Token: 0x17000003 RID: 3
			// (get) Token: 0x0600001A RID: 26 RVA: 0x00002BF0 File Offset: 0x00000DF0
			// (set) Token: 0x0600001B RID: 27 RVA: 0x00002BF8 File Offset: 0x00000DF8
			public ManualOneShotEvent ManualOneShotEvent { get; set; }

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x0600001C RID: 28 RVA: 0x00002C01 File Offset: 0x00000E01
			// (set) Token: 0x0600001D RID: 29 RVA: 0x00002C09 File Offset: 0x00000E09
			public bool IsManualOneShotEvent { get; set; }

			// Token: 0x17000005 RID: 5
			// (get) Token: 0x0600001E RID: 30 RVA: 0x00002C12 File Offset: 0x00000E12
			// (set) Token: 0x0600001F RID: 31 RVA: 0x00002C1A File Offset: 0x00000E1A
			public bool IsPreSignaled { get; set; }

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000020 RID: 32 RVA: 0x00002C23 File Offset: 0x00000E23
			// (set) Token: 0x06000021 RID: 33 RVA: 0x00002C2B File Offset: 0x00000E2B
			public bool IsWaitRegistered { get; set; }

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x06000022 RID: 34 RVA: 0x00002C34 File Offset: 0x00000E34
			// (set) Token: 0x06000023 RID: 35 RVA: 0x00002C3C File Offset: 0x00000E3C
			public bool IsWaitUnregistered { get; set; }

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000024 RID: 36 RVA: 0x00002C45 File Offset: 0x00000E45
			// (set) Token: 0x06000025 RID: 37 RVA: 0x00002C4D File Offset: 0x00000E4D
			public bool IsClosed { get; set; }
		}
	}
}
