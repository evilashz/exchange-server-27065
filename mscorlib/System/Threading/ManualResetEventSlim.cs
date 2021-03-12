using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x02000516 RID: 1302
	[ComVisible(false)]
	[DebuggerDisplay("Set = {IsSet}")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class ManualResetEventSlim : IDisposable
	{
		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06003E01 RID: 15873 RVA: 0x000E6604 File Offset: 0x000E4804
		[__DynamicallyInvokable]
		public WaitHandle WaitHandle
		{
			[__DynamicallyInvokable]
			get
			{
				this.ThrowIfDisposed();
				if (this.m_eventObj == null)
				{
					this.LazyInitializeEvent();
				}
				return this.m_eventObj;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06003E02 RID: 15874 RVA: 0x000E6625 File Offset: 0x000E4825
		// (set) Token: 0x06003E03 RID: 15875 RVA: 0x000E663C File Offset: 0x000E483C
		[__DynamicallyInvokable]
		public bool IsSet
		{
			[__DynamicallyInvokable]
			get
			{
				return ManualResetEventSlim.ExtractStatePortion(this.m_combinedState, int.MinValue) != 0;
			}
			private set
			{
				this.UpdateStateAtomically((value ? 1 : 0) << 31, int.MinValue);
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06003E04 RID: 15876 RVA: 0x000E6653 File Offset: 0x000E4853
		// (set) Token: 0x06003E05 RID: 15877 RVA: 0x000E6669 File Offset: 0x000E4869
		[__DynamicallyInvokable]
		public int SpinCount
		{
			[__DynamicallyInvokable]
			get
			{
				return ManualResetEventSlim.ExtractStatePortionAndShiftRight(this.m_combinedState, 1073217536, 19);
			}
			private set
			{
				this.m_combinedState = ((this.m_combinedState & -1073217537) | value << 19);
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06003E06 RID: 15878 RVA: 0x000E6686 File Offset: 0x000E4886
		// (set) Token: 0x06003E07 RID: 15879 RVA: 0x000E669B File Offset: 0x000E489B
		private int Waiters
		{
			get
			{
				return ManualResetEventSlim.ExtractStatePortionAndShiftRight(this.m_combinedState, 524287, 0);
			}
			set
			{
				if (value >= 524287)
				{
					throw new InvalidOperationException(string.Format(Environment.GetResourceString("ManualResetEventSlim_ctor_TooManyWaiters"), 524287));
				}
				this.UpdateStateAtomically(value, 524287);
			}
		}

		// Token: 0x06003E08 RID: 15880 RVA: 0x000E66D0 File Offset: 0x000E48D0
		[__DynamicallyInvokable]
		public ManualResetEventSlim() : this(false)
		{
		}

		// Token: 0x06003E09 RID: 15881 RVA: 0x000E66D9 File Offset: 0x000E48D9
		[__DynamicallyInvokable]
		public ManualResetEventSlim(bool initialState)
		{
			this.Initialize(initialState, 10);
		}

		// Token: 0x06003E0A RID: 15882 RVA: 0x000E66EC File Offset: 0x000E48EC
		[__DynamicallyInvokable]
		public ManualResetEventSlim(bool initialState, int spinCount)
		{
			if (spinCount < 0)
			{
				throw new ArgumentOutOfRangeException("spinCount");
			}
			if (spinCount > 2047)
			{
				throw new ArgumentOutOfRangeException("spinCount", string.Format(Environment.GetResourceString("ManualResetEventSlim_ctor_SpinCountOutOfRange"), 2047));
			}
			this.Initialize(initialState, spinCount);
		}

		// Token: 0x06003E0B RID: 15883 RVA: 0x000E6742 File Offset: 0x000E4942
		private void Initialize(bool initialState, int spinCount)
		{
			this.m_combinedState = (initialState ? int.MinValue : 0);
			this.SpinCount = (PlatformHelper.IsSingleProcessor ? 1 : spinCount);
		}

		// Token: 0x06003E0C RID: 15884 RVA: 0x000E6768 File Offset: 0x000E4968
		private void EnsureLockObjectCreated()
		{
			if (this.m_lock != null)
			{
				return;
			}
			object value = new object();
			Interlocked.CompareExchange(ref this.m_lock, value, null);
		}

		// Token: 0x06003E0D RID: 15885 RVA: 0x000E6794 File Offset: 0x000E4994
		private bool LazyInitializeEvent()
		{
			bool isSet = this.IsSet;
			ManualResetEvent manualResetEvent = new ManualResetEvent(isSet);
			if (Interlocked.CompareExchange<ManualResetEvent>(ref this.m_eventObj, manualResetEvent, null) != null)
			{
				manualResetEvent.Close();
				return false;
			}
			bool isSet2 = this.IsSet;
			if (isSet2 != isSet)
			{
				ManualResetEvent obj = manualResetEvent;
				lock (obj)
				{
					if (this.m_eventObj == manualResetEvent)
					{
						manualResetEvent.Set();
					}
				}
			}
			return true;
		}

		// Token: 0x06003E0E RID: 15886 RVA: 0x000E6810 File Offset: 0x000E4A10
		[__DynamicallyInvokable]
		public void Set()
		{
			this.Set(false);
		}

		// Token: 0x06003E0F RID: 15887 RVA: 0x000E681C File Offset: 0x000E4A1C
		private void Set(bool duringCancellation)
		{
			this.IsSet = true;
			if (this.Waiters > 0)
			{
				object @lock = this.m_lock;
				lock (@lock)
				{
					Monitor.PulseAll(this.m_lock);
				}
			}
			ManualResetEvent eventObj = this.m_eventObj;
			if (eventObj != null && !duringCancellation)
			{
				ManualResetEvent obj = eventObj;
				lock (obj)
				{
					if (this.m_eventObj != null)
					{
						this.m_eventObj.Set();
					}
				}
			}
		}

		// Token: 0x06003E10 RID: 15888 RVA: 0x000E68C4 File Offset: 0x000E4AC4
		[__DynamicallyInvokable]
		public void Reset()
		{
			this.ThrowIfDisposed();
			if (this.m_eventObj != null)
			{
				this.m_eventObj.Reset();
			}
			this.IsSet = false;
		}

		// Token: 0x06003E11 RID: 15889 RVA: 0x000E68EC File Offset: 0x000E4AEC
		[__DynamicallyInvokable]
		public void Wait()
		{
			this.Wait(-1, default(CancellationToken));
		}

		// Token: 0x06003E12 RID: 15890 RVA: 0x000E690A File Offset: 0x000E4B0A
		[__DynamicallyInvokable]
		public void Wait(CancellationToken cancellationToken)
		{
			this.Wait(-1, cancellationToken);
		}

		// Token: 0x06003E13 RID: 15891 RVA: 0x000E6918 File Offset: 0x000E4B18
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

		// Token: 0x06003E14 RID: 15892 RVA: 0x000E6958 File Offset: 0x000E4B58
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

		// Token: 0x06003E15 RID: 15893 RVA: 0x000E6990 File Offset: 0x000E4B90
		[__DynamicallyInvokable]
		public bool Wait(int millisecondsTimeout)
		{
			return this.Wait(millisecondsTimeout, default(CancellationToken));
		}

		// Token: 0x06003E16 RID: 15894 RVA: 0x000E69B0 File Offset: 0x000E4BB0
		[__DynamicallyInvokable]
		public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			this.ThrowIfDisposed();
			cancellationToken.ThrowIfCancellationRequested();
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			if (!this.IsSet)
			{
				if (millisecondsTimeout == 0)
				{
					return false;
				}
				uint startTime = 0U;
				bool flag = false;
				int num = millisecondsTimeout;
				if (millisecondsTimeout != -1)
				{
					startTime = TimeoutHelper.GetTime();
					flag = true;
				}
				int num2 = 10;
				int num3 = 5;
				int num4 = 20;
				int spinCount = this.SpinCount;
				for (int i = 0; i < spinCount; i++)
				{
					if (this.IsSet)
					{
						return true;
					}
					if (i < num2)
					{
						if (i == num2 / 2)
						{
							Thread.Yield();
						}
						else
						{
							Thread.SpinWait(4 << i);
						}
					}
					else if (i % num4 == 0)
					{
						Thread.Sleep(1);
					}
					else if (i % num3 == 0)
					{
						Thread.Sleep(0);
					}
					else
					{
						Thread.Yield();
					}
					if (i >= 100 && i % 10 == 0)
					{
						cancellationToken.ThrowIfCancellationRequested();
					}
				}
				this.EnsureLockObjectCreated();
				using (cancellationToken.InternalRegisterWithoutEC(ManualResetEventSlim.s_cancellationTokenCallback, this))
				{
					object @lock = this.m_lock;
					lock (@lock)
					{
						while (!this.IsSet)
						{
							cancellationToken.ThrowIfCancellationRequested();
							if (flag)
							{
								num = TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout);
								if (num <= 0)
								{
									return false;
								}
							}
							this.Waiters++;
							if (this.IsSet)
							{
								int waiters = this.Waiters;
								this.Waiters = waiters - 1;
								return true;
							}
							try
							{
								if (!Monitor.Wait(this.m_lock, num))
								{
									return false;
								}
							}
							finally
							{
								this.Waiters--;
							}
						}
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06003E17 RID: 15895 RVA: 0x000E6B70 File Offset: 0x000E4D70
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003E18 RID: 15896 RVA: 0x000E6B80 File Offset: 0x000E4D80
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			if ((this.m_combinedState & 1073741824) != 0)
			{
				return;
			}
			this.m_combinedState |= 1073741824;
			if (disposing)
			{
				ManualResetEvent eventObj = this.m_eventObj;
				if (eventObj != null)
				{
					ManualResetEvent obj = eventObj;
					lock (obj)
					{
						eventObj.Close();
						this.m_eventObj = null;
					}
				}
			}
		}

		// Token: 0x06003E19 RID: 15897 RVA: 0x000E6BFC File Offset: 0x000E4DFC
		private void ThrowIfDisposed()
		{
			if ((this.m_combinedState & 1073741824) != 0)
			{
				throw new ObjectDisposedException(Environment.GetResourceString("ManualResetEventSlim_Disposed"));
			}
		}

		// Token: 0x06003E1A RID: 15898 RVA: 0x000E6C20 File Offset: 0x000E4E20
		private static void CancellationTokenCallback(object obj)
		{
			ManualResetEventSlim manualResetEventSlim = obj as ManualResetEventSlim;
			object @lock = manualResetEventSlim.m_lock;
			lock (@lock)
			{
				Monitor.PulseAll(manualResetEventSlim.m_lock);
			}
		}

		// Token: 0x06003E1B RID: 15899 RVA: 0x000E6C70 File Offset: 0x000E4E70
		private void UpdateStateAtomically(int newBits, int updateBitsMask)
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int combinedState = this.m_combinedState;
				int value = (combinedState & ~updateBitsMask) | newBits;
				if (Interlocked.CompareExchange(ref this.m_combinedState, value, combinedState) == combinedState)
				{
					break;
				}
				spinWait.SpinOnce();
			}
		}

		// Token: 0x06003E1C RID: 15900 RVA: 0x000E6CAE File Offset: 0x000E4EAE
		private static int ExtractStatePortionAndShiftRight(int state, int mask, int rightBitShiftCount)
		{
			return (int)((uint)(state & mask) >> rightBitShiftCount);
		}

		// Token: 0x06003E1D RID: 15901 RVA: 0x000E6CB8 File Offset: 0x000E4EB8
		private static int ExtractStatePortion(int state, int mask)
		{
			return state & mask;
		}

		// Token: 0x040019CF RID: 6607
		private const int DEFAULT_SPIN_SP = 1;

		// Token: 0x040019D0 RID: 6608
		private const int DEFAULT_SPIN_MP = 10;

		// Token: 0x040019D1 RID: 6609
		private volatile object m_lock;

		// Token: 0x040019D2 RID: 6610
		private volatile ManualResetEvent m_eventObj;

		// Token: 0x040019D3 RID: 6611
		private volatile int m_combinedState;

		// Token: 0x040019D4 RID: 6612
		private const int SignalledState_BitMask = -2147483648;

		// Token: 0x040019D5 RID: 6613
		private const int SignalledState_ShiftCount = 31;

		// Token: 0x040019D6 RID: 6614
		private const int Dispose_BitMask = 1073741824;

		// Token: 0x040019D7 RID: 6615
		private const int SpinCountState_BitMask = 1073217536;

		// Token: 0x040019D8 RID: 6616
		private const int SpinCountState_ShiftCount = 19;

		// Token: 0x040019D9 RID: 6617
		private const int SpinCountState_MaxValue = 2047;

		// Token: 0x040019DA RID: 6618
		private const int NumWaitersState_BitMask = 524287;

		// Token: 0x040019DB RID: 6619
		private const int NumWaitersState_ShiftCount = 0;

		// Token: 0x040019DC RID: 6620
		private const int NumWaitersState_MaxValue = 524287;

		// Token: 0x040019DD RID: 6621
		private static Action<object> s_cancellationTokenCallback = new Action<object>(ManualResetEventSlim.CancellationTokenCallback);
	}
}
