using System;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x0200050A RID: 1290
	[ComVisible(false)]
	[DebuggerTypeProxy(typeof(SpinLock.SystemThreading_SpinLockDebugView))]
	[DebuggerDisplay("IsHeld = {IsHeld}")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct SpinLock
	{
		// Token: 0x06003D8C RID: 15756 RVA: 0x000E48C1 File Offset: 0x000E2AC1
		[__DynamicallyInvokable]
		public SpinLock(bool enableThreadOwnerTracking)
		{
			this.m_owner = 0;
			if (!enableThreadOwnerTracking)
			{
				this.m_owner |= int.MinValue;
			}
		}

		// Token: 0x06003D8D RID: 15757 RVA: 0x000E48E8 File Offset: 0x000E2AE8
		[__DynamicallyInvokable]
		public void Enter(ref bool lockTaken)
		{
			Thread.BeginCriticalRegion();
			int owner = this.m_owner;
			if (lockTaken || (owner & -2147483647) != -2147483648 || Interlocked.CompareExchange(ref this.m_owner, owner | 1, owner, ref lockTaken) != owner)
			{
				this.ContinueTryEnter(-1, ref lockTaken);
			}
		}

		// Token: 0x06003D8E RID: 15758 RVA: 0x000E4930 File Offset: 0x000E2B30
		[__DynamicallyInvokable]
		public void TryEnter(ref bool lockTaken)
		{
			this.TryEnter(0, ref lockTaken);
		}

		// Token: 0x06003D8F RID: 15759 RVA: 0x000E493C File Offset: 0x000E2B3C
		[__DynamicallyInvokable]
		public void TryEnter(TimeSpan timeout, ref bool lockTaken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, Environment.GetResourceString("SpinLock_TryEnter_ArgumentOutOfRange"));
			}
			this.TryEnter((int)timeout.TotalMilliseconds, ref lockTaken);
		}

		// Token: 0x06003D90 RID: 15760 RVA: 0x000E498C File Offset: 0x000E2B8C
		[__DynamicallyInvokable]
		public void TryEnter(int millisecondsTimeout, ref bool lockTaken)
		{
			Thread.BeginCriticalRegion();
			int owner = this.m_owner;
			if ((millisecondsTimeout < -1 | lockTaken) || (owner & -2147483647) != -2147483648 || Interlocked.CompareExchange(ref this.m_owner, owner | 1, owner, ref lockTaken) != owner)
			{
				this.ContinueTryEnter(millisecondsTimeout, ref lockTaken);
			}
		}

		// Token: 0x06003D91 RID: 15761 RVA: 0x000E49DC File Offset: 0x000E2BDC
		private void ContinueTryEnter(int millisecondsTimeout, ref bool lockTaken)
		{
			Thread.EndCriticalRegion();
			if (lockTaken)
			{
				lockTaken = false;
				throw new ArgumentException(Environment.GetResourceString("SpinLock_TryReliableEnter_ArgumentException"));
			}
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", millisecondsTimeout, Environment.GetResourceString("SpinLock_TryEnter_ArgumentOutOfRange"));
			}
			uint startTime = 0U;
			if (millisecondsTimeout != -1 && millisecondsTimeout != 0)
			{
				startTime = TimeoutHelper.GetTime();
			}
			if (CdsSyncEtwBCLProvider.Log.IsEnabled())
			{
				CdsSyncEtwBCLProvider.Log.SpinLock_FastPathFailed(this.m_owner);
			}
			if (this.IsThreadOwnerTrackingEnabled)
			{
				this.ContinueTryEnterWithThreadTracking(millisecondsTimeout, startTime, ref lockTaken);
				return;
			}
			int num = int.MaxValue;
			int owner = this.m_owner;
			if ((owner & 1) == 0)
			{
				Thread.BeginCriticalRegion();
				if (Interlocked.CompareExchange(ref this.m_owner, owner | 1, owner, ref lockTaken) == owner)
				{
					return;
				}
				Thread.EndCriticalRegion();
			}
			else if ((owner & 2147483646) != SpinLock.MAXIMUM_WAITERS)
			{
				num = (Interlocked.Add(ref this.m_owner, 2) & 2147483646) >> 1;
			}
			if (millisecondsTimeout == 0 || (millisecondsTimeout != -1 && TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout) <= 0))
			{
				this.DecrementWaiters();
				return;
			}
			int processorCount = PlatformHelper.ProcessorCount;
			if (num < processorCount)
			{
				int num2 = 1;
				for (int i = 1; i <= num * 100; i++)
				{
					Thread.SpinWait((num + i) * 100 * num2);
					if (num2 < processorCount)
					{
						num2++;
					}
					owner = this.m_owner;
					if ((owner & 1) == 0)
					{
						Thread.BeginCriticalRegion();
						int value = ((owner & 2147483646) == 0) ? (owner | 1) : (owner - 2 | 1);
						if (Interlocked.CompareExchange(ref this.m_owner, value, owner, ref lockTaken) == owner)
						{
							return;
						}
						Thread.EndCriticalRegion();
					}
				}
			}
			if (millisecondsTimeout != -1 && TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout) <= 0)
			{
				this.DecrementWaiters();
				return;
			}
			int num3 = 0;
			for (;;)
			{
				owner = this.m_owner;
				if ((owner & 1) == 0)
				{
					Thread.BeginCriticalRegion();
					int value2 = ((owner & 2147483646) == 0) ? (owner | 1) : (owner - 2 | 1);
					if (Interlocked.CompareExchange(ref this.m_owner, value2, owner, ref lockTaken) == owner)
					{
						break;
					}
					Thread.EndCriticalRegion();
				}
				if (num3 % 40 == 0)
				{
					Thread.Sleep(1);
				}
				else if (num3 % 10 == 0)
				{
					Thread.Sleep(0);
				}
				else
				{
					Thread.Yield();
				}
				if (num3 % 10 == 0 && millisecondsTimeout != -1 && TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout) <= 0)
				{
					goto Block_26;
				}
				num3++;
			}
			return;
			Block_26:
			this.DecrementWaiters();
		}

		// Token: 0x06003D92 RID: 15762 RVA: 0x000E4BF0 File Offset: 0x000E2DF0
		private void DecrementWaiters()
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int owner = this.m_owner;
				if ((owner & 2147483646) == 0)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this.m_owner, owner - 2, owner) == owner)
				{
					return;
				}
				spinWait.SpinOnce();
			}
		}

		// Token: 0x06003D93 RID: 15763 RVA: 0x000E4C34 File Offset: 0x000E2E34
		private void ContinueTryEnterWithThreadTracking(int millisecondsTimeout, uint startTime, ref bool lockTaken)
		{
			int num = 0;
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			if (this.m_owner == managedThreadId)
			{
				throw new LockRecursionException(Environment.GetResourceString("SpinLock_TryEnter_LockRecursionException"));
			}
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				spinWait.SpinOnce();
				if (this.m_owner == num)
				{
					Thread.BeginCriticalRegion();
					if (Interlocked.CompareExchange(ref this.m_owner, managedThreadId, num, ref lockTaken) == num)
					{
						break;
					}
					Thread.EndCriticalRegion();
				}
				if (millisecondsTimeout == 0 || (millisecondsTimeout != -1 && spinWait.NextSpinWillYield && TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout) <= 0))
				{
					return;
				}
			}
		}

		// Token: 0x06003D94 RID: 15764 RVA: 0x000E4CB9 File Offset: 0x000E2EB9
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public void Exit()
		{
			if ((this.m_owner & -2147483648) == 0)
			{
				this.ExitSlowPath(true);
			}
			else
			{
				Interlocked.Decrement(ref this.m_owner);
			}
			Thread.EndCriticalRegion();
		}

		// Token: 0x06003D95 RID: 15765 RVA: 0x000E4CE8 File Offset: 0x000E2EE8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public void Exit(bool useMemoryBarrier)
		{
			if ((this.m_owner & -2147483648) != 0 && !useMemoryBarrier)
			{
				int owner = this.m_owner;
				this.m_owner = (owner & -2);
			}
			else
			{
				this.ExitSlowPath(useMemoryBarrier);
			}
			Thread.EndCriticalRegion();
		}

		// Token: 0x06003D96 RID: 15766 RVA: 0x000E4D2C File Offset: 0x000E2F2C
		private void ExitSlowPath(bool useMemoryBarrier)
		{
			bool flag = (this.m_owner & int.MinValue) == 0;
			if (flag && !this.IsHeldByCurrentThread)
			{
				throw new SynchronizationLockException(Environment.GetResourceString("SpinLock_Exit_SynchronizationLockException"));
			}
			if (useMemoryBarrier)
			{
				if (flag)
				{
					Interlocked.Exchange(ref this.m_owner, 0);
					return;
				}
				Interlocked.Decrement(ref this.m_owner);
				return;
			}
			else
			{
				if (flag)
				{
					this.m_owner = 0;
					return;
				}
				int owner = this.m_owner;
				this.m_owner = (owner & -2);
				return;
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06003D97 RID: 15767 RVA: 0x000E4DA9 File Offset: 0x000E2FA9
		[__DynamicallyInvokable]
		public bool IsHeld
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get
			{
				if (this.IsThreadOwnerTrackingEnabled)
				{
					return this.m_owner != 0;
				}
				return (this.m_owner & 1) != 0;
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06003D98 RID: 15768 RVA: 0x000E4DCC File Offset: 0x000E2FCC
		[__DynamicallyInvokable]
		public bool IsHeldByCurrentThread
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get
			{
				if (!this.IsThreadOwnerTrackingEnabled)
				{
					throw new InvalidOperationException(Environment.GetResourceString("SpinLock_IsHeldByCurrentThread"));
				}
				return (this.m_owner & int.MaxValue) == Thread.CurrentThread.ManagedThreadId;
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06003D99 RID: 15769 RVA: 0x000E4E00 File Offset: 0x000E3000
		[__DynamicallyInvokable]
		public bool IsThreadOwnerTrackingEnabled
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get
			{
				return (this.m_owner & int.MinValue) == 0;
			}
		}

		// Token: 0x0400199C RID: 6556
		private volatile int m_owner;

		// Token: 0x0400199D RID: 6557
		private const int SPINNING_FACTOR = 100;

		// Token: 0x0400199E RID: 6558
		private const int SLEEP_ONE_FREQUENCY = 40;

		// Token: 0x0400199F RID: 6559
		private const int SLEEP_ZERO_FREQUENCY = 10;

		// Token: 0x040019A0 RID: 6560
		private const int TIMEOUT_CHECK_FREQUENCY = 10;

		// Token: 0x040019A1 RID: 6561
		private const int LOCK_ID_DISABLE_MASK = -2147483648;

		// Token: 0x040019A2 RID: 6562
		private const int LOCK_ANONYMOUS_OWNED = 1;

		// Token: 0x040019A3 RID: 6563
		private const int WAITERS_MASK = 2147483646;

		// Token: 0x040019A4 RID: 6564
		private const int ID_DISABLED_AND_ANONYMOUS_OWNED = -2147483647;

		// Token: 0x040019A5 RID: 6565
		private const int LOCK_UNOWNED = 0;

		// Token: 0x040019A6 RID: 6566
		private static int MAXIMUM_WAITERS = 2147483646;

		// Token: 0x02000BC4 RID: 3012
		internal class SystemThreading_SpinLockDebugView
		{
			// Token: 0x06006E58 RID: 28248 RVA: 0x0017BA91 File Offset: 0x00179C91
			public SystemThreading_SpinLockDebugView(SpinLock spinLock)
			{
				this.m_spinLock = spinLock;
			}

			// Token: 0x1700130A RID: 4874
			// (get) Token: 0x06006E59 RID: 28249 RVA: 0x0017BAA0 File Offset: 0x00179CA0
			public bool? IsHeldByCurrentThread
			{
				get
				{
					bool? result;
					try
					{
						result = new bool?(this.m_spinLock.IsHeldByCurrentThread);
					}
					catch (InvalidOperationException)
					{
						result = null;
					}
					return result;
				}
			}

			// Token: 0x1700130B RID: 4875
			// (get) Token: 0x06006E5A RID: 28250 RVA: 0x0017BAE0 File Offset: 0x00179CE0
			public int? OwnerThreadID
			{
				get
				{
					if (this.m_spinLock.IsThreadOwnerTrackingEnabled)
					{
						return new int?(this.m_spinLock.m_owner);
					}
					return null;
				}
			}

			// Token: 0x1700130C RID: 4876
			// (get) Token: 0x06006E5B RID: 28251 RVA: 0x0017BB16 File Offset: 0x00179D16
			public bool IsHeld
			{
				get
				{
					return this.m_spinLock.IsHeld;
				}
			}

			// Token: 0x0400355B RID: 13659
			private SpinLock m_spinLock;
		}
	}
}
