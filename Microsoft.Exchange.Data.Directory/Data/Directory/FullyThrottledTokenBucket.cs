using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009BC RID: 2492
	internal class FullyThrottledTokenBucket : ITokenBucket
	{
		// Token: 0x060073A0 RID: 29600 RVA: 0x0017D54A File Offset: 0x0017B74A
		public FullyThrottledTokenBucket(ITokenBucket oldBucket)
		{
			if (oldBucket != null)
			{
				this.pendingCharges = oldBucket.PendingCharges;
			}
		}

		// Token: 0x1700293F RID: 10559
		// (get) Token: 0x060073A1 RID: 29601 RVA: 0x0017D561 File Offset: 0x0017B761
		public int PendingCharges
		{
			get
			{
				return this.pendingCharges;
			}
		}

		// Token: 0x17002940 RID: 10560
		// (get) Token: 0x060073A2 RID: 29602 RVA: 0x0017D569 File Offset: 0x0017B769
		public DateTime? LockedUntilUtc
		{
			get
			{
				return new DateTime?(DateTime.MaxValue);
			}
		}

		// Token: 0x17002941 RID: 10561
		// (get) Token: 0x060073A3 RID: 29603 RVA: 0x0017D575 File Offset: 0x0017B775
		public DateTime? LockedAt
		{
			get
			{
				return new DateTime?(TimeProvider.UtcNow);
			}
		}

		// Token: 0x17002942 RID: 10562
		// (get) Token: 0x060073A4 RID: 29604 RVA: 0x0017D581 File Offset: 0x0017B781
		public bool Locked
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002943 RID: 10563
		// (get) Token: 0x060073A5 RID: 29605 RVA: 0x0017D584 File Offset: 0x0017B784
		public int MaximumBalance
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17002944 RID: 10564
		// (get) Token: 0x060073A6 RID: 29606 RVA: 0x0017D587 File Offset: 0x0017B787
		public int MinimumBalance
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x17002945 RID: 10565
		// (get) Token: 0x060073A7 RID: 29607 RVA: 0x0017D58E File Offset: 0x0017B78E
		public int RechargeRate
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060073A8 RID: 29608 RVA: 0x0017D591 File Offset: 0x0017B791
		public float GetBalance()
		{
			return (float)this.MinimumBalance;
		}

		// Token: 0x17002946 RID: 10566
		// (get) Token: 0x060073A9 RID: 29609 RVA: 0x0017D59A File Offset: 0x0017B79A
		public DateTime LastUpdateUtc
		{
			get
			{
				return TimeProvider.UtcNow;
			}
		}

		// Token: 0x060073AA RID: 29610 RVA: 0x0017D5A1 File Offset: 0x0017B7A1
		public void Increment()
		{
			Interlocked.Increment(ref this.pendingCharges);
		}

		// Token: 0x060073AB RID: 29611 RVA: 0x0017D5AF File Offset: 0x0017B7AF
		public void Decrement(TimeSpan extraDuration = default(TimeSpan), bool reverseBudgetCharge = false)
		{
			Interlocked.Decrement(ref this.pendingCharges);
		}

		// Token: 0x04004AB3 RID: 19123
		private int pendingCharges;
	}
}
