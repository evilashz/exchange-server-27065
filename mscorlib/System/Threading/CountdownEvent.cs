using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x0200050E RID: 1294
	[ComVisible(false)]
	[DebuggerDisplay("Initial Count={InitialCount}, Current Count={CurrentCount}")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class CountdownEvent : IDisposable
	{
		// Token: 0x06003DA6 RID: 15782 RVA: 0x000E5042 File Offset: 0x000E3242
		[__DynamicallyInvokable]
		public CountdownEvent(int initialCount)
		{
			if (initialCount < 0)
			{
				throw new ArgumentOutOfRangeException("initialCount");
			}
			this.m_initialCount = initialCount;
			this.m_currentCount = initialCount;
			this.m_event = new ManualResetEventSlim();
			if (initialCount == 0)
			{
				this.m_event.Set();
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06003DA7 RID: 15783 RVA: 0x000E5084 File Offset: 0x000E3284
		[__DynamicallyInvokable]
		public int CurrentCount
		{
			[__DynamicallyInvokable]
			get
			{
				int currentCount = this.m_currentCount;
				if (currentCount >= 0)
				{
					return currentCount;
				}
				return 0;
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06003DA8 RID: 15784 RVA: 0x000E50A1 File Offset: 0x000E32A1
		[__DynamicallyInvokable]
		public int InitialCount
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_initialCount;
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06003DA9 RID: 15785 RVA: 0x000E50A9 File Offset: 0x000E32A9
		[__DynamicallyInvokable]
		public bool IsSet
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_currentCount <= 0;
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06003DAA RID: 15786 RVA: 0x000E50B9 File Offset: 0x000E32B9
		[__DynamicallyInvokable]
		public WaitHandle WaitHandle
		{
			[__DynamicallyInvokable]
			get
			{
				this.ThrowIfDisposed();
				return this.m_event.WaitHandle;
			}
		}

		// Token: 0x06003DAB RID: 15787 RVA: 0x000E50CC File Offset: 0x000E32CC
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003DAC RID: 15788 RVA: 0x000E50DB File Offset: 0x000E32DB
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.m_event.Dispose();
				this.m_disposed = true;
			}
		}

		// Token: 0x06003DAD RID: 15789 RVA: 0x000E50F4 File Offset: 0x000E32F4
		[__DynamicallyInvokable]
		public bool Signal()
		{
			this.ThrowIfDisposed();
			if (this.m_currentCount <= 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("CountdownEvent_Decrement_BelowZero"));
			}
			int num = Interlocked.Decrement(ref this.m_currentCount);
			if (num == 0)
			{
				this.m_event.Set();
				return true;
			}
			if (num < 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("CountdownEvent_Decrement_BelowZero"));
			}
			return false;
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x000E5154 File Offset: 0x000E3354
		[__DynamicallyInvokable]
		public bool Signal(int signalCount)
		{
			if (signalCount <= 0)
			{
				throw new ArgumentOutOfRangeException("signalCount");
			}
			this.ThrowIfDisposed();
			SpinWait spinWait = default(SpinWait);
			int currentCount;
			for (;;)
			{
				currentCount = this.m_currentCount;
				if (currentCount < signalCount)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this.m_currentCount, currentCount - signalCount, currentCount) == currentCount)
				{
					goto IL_55;
				}
				spinWait.SpinOnce();
			}
			throw new InvalidOperationException(Environment.GetResourceString("CountdownEvent_Decrement_BelowZero"));
			IL_55:
			if (currentCount == signalCount)
			{
				this.m_event.Set();
				return true;
			}
			return false;
		}

		// Token: 0x06003DAF RID: 15791 RVA: 0x000E51C8 File Offset: 0x000E33C8
		[__DynamicallyInvokable]
		public void AddCount()
		{
			this.AddCount(1);
		}

		// Token: 0x06003DB0 RID: 15792 RVA: 0x000E51D1 File Offset: 0x000E33D1
		[__DynamicallyInvokable]
		public bool TryAddCount()
		{
			return this.TryAddCount(1);
		}

		// Token: 0x06003DB1 RID: 15793 RVA: 0x000E51DA File Offset: 0x000E33DA
		[__DynamicallyInvokable]
		public void AddCount(int signalCount)
		{
			if (!this.TryAddCount(signalCount))
			{
				throw new InvalidOperationException(Environment.GetResourceString("CountdownEvent_Increment_AlreadyZero"));
			}
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x000E51F8 File Offset: 0x000E33F8
		[__DynamicallyInvokable]
		public bool TryAddCount(int signalCount)
		{
			if (signalCount <= 0)
			{
				throw new ArgumentOutOfRangeException("signalCount");
			}
			this.ThrowIfDisposed();
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int currentCount = this.m_currentCount;
				if (currentCount <= 0)
				{
					break;
				}
				if (currentCount > 2147483647 - signalCount)
				{
					goto Block_3;
				}
				if (Interlocked.CompareExchange(ref this.m_currentCount, currentCount + signalCount, currentCount) == currentCount)
				{
					return true;
				}
				spinWait.SpinOnce();
			}
			return false;
			Block_3:
			throw new InvalidOperationException(Environment.GetResourceString("CountdownEvent_Increment_AlreadyMax"));
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x000E5267 File Offset: 0x000E3467
		[__DynamicallyInvokable]
		public void Reset()
		{
			this.Reset(this.m_initialCount);
		}

		// Token: 0x06003DB4 RID: 15796 RVA: 0x000E5278 File Offset: 0x000E3478
		[__DynamicallyInvokable]
		public void Reset(int count)
		{
			this.ThrowIfDisposed();
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.m_currentCount = count;
			this.m_initialCount = count;
			if (count == 0)
			{
				this.m_event.Set();
				return;
			}
			this.m_event.Reset();
		}

		// Token: 0x06003DB5 RID: 15797 RVA: 0x000E52C4 File Offset: 0x000E34C4
		[__DynamicallyInvokable]
		public void Wait()
		{
			this.Wait(-1, default(CancellationToken));
		}

		// Token: 0x06003DB6 RID: 15798 RVA: 0x000E52E2 File Offset: 0x000E34E2
		[__DynamicallyInvokable]
		public void Wait(CancellationToken cancellationToken)
		{
			this.Wait(-1, cancellationToken);
		}

		// Token: 0x06003DB7 RID: 15799 RVA: 0x000E52F0 File Offset: 0x000E34F0
		[__DynamicallyInvokable]
		public bool Wait(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, default(CancellationToken));
		}

		// Token: 0x06003DB8 RID: 15800 RVA: 0x000E5330 File Offset: 0x000E3530
		[__DynamicallyInvokable]
		public bool Wait(TimeSpan timeout, CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, cancellationToken);
		}

		// Token: 0x06003DB9 RID: 15801 RVA: 0x000E5368 File Offset: 0x000E3568
		[__DynamicallyInvokable]
		public bool Wait(int millisecondsTimeout)
		{
			return this.Wait(millisecondsTimeout, default(CancellationToken));
		}

		// Token: 0x06003DBA RID: 15802 RVA: 0x000E5388 File Offset: 0x000E3588
		[__DynamicallyInvokable]
		public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			this.ThrowIfDisposed();
			cancellationToken.ThrowIfCancellationRequested();
			bool flag = this.IsSet;
			if (!flag)
			{
				flag = this.m_event.Wait(millisecondsTimeout, cancellationToken);
			}
			return flag;
		}

		// Token: 0x06003DBB RID: 15803 RVA: 0x000E53CA File Offset: 0x000E35CA
		private void ThrowIfDisposed()
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("CountdownEvent");
			}
		}

		// Token: 0x040019AE RID: 6574
		private int m_initialCount;

		// Token: 0x040019AF RID: 6575
		private volatile int m_currentCount;

		// Token: 0x040019B0 RID: 6576
		private ManualResetEventSlim m_event;

		// Token: 0x040019B1 RID: 6577
		private volatile bool m_disposed;
	}
}
