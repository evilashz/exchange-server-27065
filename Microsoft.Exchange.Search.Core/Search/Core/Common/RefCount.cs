using System;
using System.Threading;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x0200007A RID: 122
	internal class RefCount
	{
		// Token: 0x06000313 RID: 787 RVA: 0x0000A6AD File Offset: 0x000088AD
		public RefCount()
		{
			this.releaseOnDispose = new RefCount.ReleaseOnDispose(this);
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000314 RID: 788 RVA: 0x0000A6C1 File Offset: 0x000088C1
		public bool IsDisabled
		{
			get
			{
				return (Thread.VolatileRead(ref this.referenceCount) & int.MinValue) != 0;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000A6DA File Offset: 0x000088DA
		public int Count
		{
			get
			{
				return Thread.VolatileRead(ref this.referenceCount) & int.MaxValue;
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000A6ED File Offset: 0x000088ED
		public IDisposable AcquireReference()
		{
			if (this.TryAddRef())
			{
				return this.releaseOnDispose;
			}
			return null;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000A700 File Offset: 0x00008900
		public bool TryAddRef()
		{
			int num = Thread.VolatileRead(ref this.referenceCount);
			while ((num & -2147483648) == 0)
			{
				int num2 = Interlocked.CompareExchange(ref this.referenceCount, num + 1, num);
				if (num == num2)
				{
					return true;
				}
				num = num2;
			}
			return false;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000A73D File Offset: 0x0000893D
		public void AddRef()
		{
			if (!this.TryAddRef())
			{
				throw new InvalidOperationException("RefCount has been disabled");
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000A754 File Offset: 0x00008954
		public void Release()
		{
			int num = Interlocked.Decrement(ref this.referenceCount);
			if (num == -2147483648)
			{
				this.disabledAndZeroEvent.Set();
			}
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000A784 File Offset: 0x00008984
		public bool DisableAddRef()
		{
			this.disabledAndZeroEvent = new ManualResetEvent(false);
			int num = Thread.VolatileRead(ref this.referenceCount);
			int num2;
			while ((num2 = Interlocked.CompareExchange(ref this.referenceCount, num | -2147483648, num)) != num)
			{
				num = num2;
			}
			if ((num2 & 2147483647) == 0)
			{
				this.disabledAndZeroEvent.Dispose();
				this.disabledAndZeroEvent = null;
				return true;
			}
			return false;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000A7E3 File Offset: 0x000089E3
		public bool TryWaitForZero(TimeSpan waitTime)
		{
			if (this.DisableAddRef())
			{
				return true;
			}
			if (!this.disabledAndZeroEvent.WaitOne(waitTime))
			{
				return false;
			}
			this.disabledAndZeroEvent.Dispose();
			this.disabledAndZeroEvent = null;
			return true;
		}

		// Token: 0x0400015E RID: 350
		private const int DisabledFlag = -2147483648;

		// Token: 0x0400015F RID: 351
		private const int LastReferenceAndDisabled = -2147483648;

		// Token: 0x04000160 RID: 352
		private readonly RefCount.ReleaseOnDispose releaseOnDispose;

		// Token: 0x04000161 RID: 353
		private int referenceCount;

		// Token: 0x04000162 RID: 354
		private ManualResetEvent disabledAndZeroEvent;

		// Token: 0x0200007B RID: 123
		private class ReleaseOnDispose : IDisposable
		{
			// Token: 0x0600031C RID: 796 RVA: 0x0000A812 File Offset: 0x00008A12
			public ReleaseOnDispose(RefCount refCount)
			{
				this.refCount = refCount;
			}

			// Token: 0x0600031D RID: 797 RVA: 0x0000A821 File Offset: 0x00008A21
			public void Dispose()
			{
				this.refCount.Release();
			}

			// Token: 0x04000163 RID: 355
			private readonly RefCount refCount;
		}
	}
}
