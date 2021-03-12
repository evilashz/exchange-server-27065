using System;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x0200050B RID: 1291
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public struct SpinWait
	{
		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06003D9B RID: 15771 RVA: 0x000E4E1F File Offset: 0x000E301F
		[__DynamicallyInvokable]
		public int Count
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_count;
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06003D9C RID: 15772 RVA: 0x000E4E27 File Offset: 0x000E3027
		[__DynamicallyInvokable]
		public bool NextSpinWillYield
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_count > 10 || PlatformHelper.IsSingleProcessor;
			}
		}

		// Token: 0x06003D9D RID: 15773 RVA: 0x000E4E3C File Offset: 0x000E303C
		[__DynamicallyInvokable]
		public void SpinOnce()
		{
			if (this.NextSpinWillYield)
			{
				CdsSyncEtwBCLProvider.Log.SpinWait_NextSpinWillYield();
				int num = (this.m_count >= 10) ? (this.m_count - 10) : this.m_count;
				if (num % 20 == 19)
				{
					Thread.Sleep(1);
				}
				else if (num % 5 == 4)
				{
					Thread.Sleep(0);
				}
				else
				{
					Thread.Yield();
				}
			}
			else
			{
				Thread.SpinWait(4 << this.m_count);
			}
			this.m_count = ((this.m_count == int.MaxValue) ? 10 : (this.m_count + 1));
		}

		// Token: 0x06003D9E RID: 15774 RVA: 0x000E4ECC File Offset: 0x000E30CC
		[__DynamicallyInvokable]
		public void Reset()
		{
			this.m_count = 0;
		}

		// Token: 0x06003D9F RID: 15775 RVA: 0x000E4ED5 File Offset: 0x000E30D5
		[__DynamicallyInvokable]
		public static void SpinUntil(Func<bool> condition)
		{
			SpinWait.SpinUntil(condition, -1);
		}

		// Token: 0x06003DA0 RID: 15776 RVA: 0x000E4EE0 File Offset: 0x000E30E0
		[__DynamicallyInvokable]
		public static bool SpinUntil(Func<bool> condition, TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, Environment.GetResourceString("SpinWait_SpinUntil_TimeoutWrong"));
			}
			return SpinWait.SpinUntil(condition, (int)timeout.TotalMilliseconds);
		}

		// Token: 0x06003DA1 RID: 15777 RVA: 0x000E4F30 File Offset: 0x000E3130
		[__DynamicallyInvokable]
		public static bool SpinUntil(Func<bool> condition, int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", millisecondsTimeout, Environment.GetResourceString("SpinWait_SpinUntil_TimeoutWrong"));
			}
			if (condition == null)
			{
				throw new ArgumentNullException("condition", Environment.GetResourceString("SpinWait_SpinUntil_ArgumentNull"));
			}
			uint num = 0U;
			if (millisecondsTimeout != 0 && millisecondsTimeout != -1)
			{
				num = TimeoutHelper.GetTime();
			}
			SpinWait spinWait = default(SpinWait);
			while (!condition())
			{
				if (millisecondsTimeout == 0)
				{
					return false;
				}
				spinWait.SpinOnce();
				if (millisecondsTimeout != -1 && spinWait.NextSpinWillYield && (long)millisecondsTimeout <= (long)((ulong)(TimeoutHelper.GetTime() - num)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040019A7 RID: 6567
		internal const int YIELD_THRESHOLD = 10;

		// Token: 0x040019A8 RID: 6568
		internal const int SLEEP_0_EVERY_HOW_MANY_TIMES = 5;

		// Token: 0x040019A9 RID: 6569
		internal const int SLEEP_1_EVERY_HOW_MANY_TIMES = 20;

		// Token: 0x040019AA RID: 6570
		private int m_count;
	}
}
