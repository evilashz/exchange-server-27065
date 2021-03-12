using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009E1 RID: 2529
	internal class UnthrottledTokenBucket : ITokenBucket
	{
		// Token: 0x0600757B RID: 30075 RVA: 0x001824C1 File Offset: 0x001806C1
		public UnthrottledTokenBucket(ITokenBucket oldBucket)
		{
			this.pendingCharges = ((oldBucket == null) ? 0 : oldBucket.PendingCharges);
		}

		// Token: 0x17002A0B RID: 10763
		// (get) Token: 0x0600757C RID: 30076 RVA: 0x001824DB File Offset: 0x001806DB
		public int PendingCharges
		{
			get
			{
				return this.pendingCharges;
			}
		}

		// Token: 0x17002A0C RID: 10764
		// (get) Token: 0x0600757D RID: 30077 RVA: 0x001824E4 File Offset: 0x001806E4
		public DateTime? LockedUntilUtc
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002A0D RID: 10765
		// (get) Token: 0x0600757E RID: 30078 RVA: 0x001824FC File Offset: 0x001806FC
		public DateTime? LockedAt
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17002A0E RID: 10766
		// (get) Token: 0x0600757F RID: 30079 RVA: 0x00182512 File Offset: 0x00180712
		public bool Locked
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002A0F RID: 10767
		// (get) Token: 0x06007580 RID: 30080 RVA: 0x00182515 File Offset: 0x00180715
		public int MaximumBalance
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x17002A10 RID: 10768
		// (get) Token: 0x06007581 RID: 30081 RVA: 0x0018251C File Offset: 0x0018071C
		public int MinimumBalance
		{
			get
			{
				return int.MinValue;
			}
		}

		// Token: 0x17002A11 RID: 10769
		// (get) Token: 0x06007582 RID: 30082 RVA: 0x00182523 File Offset: 0x00180723
		public int RechargeRate
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x06007583 RID: 30083 RVA: 0x0018252A File Offset: 0x0018072A
		public float GetBalance()
		{
			return (float)this.MaximumBalance;
		}

		// Token: 0x17002A12 RID: 10770
		// (get) Token: 0x06007584 RID: 30084 RVA: 0x00182533 File Offset: 0x00180733
		public DateTime LastUpdateUtc
		{
			get
			{
				return TimeProvider.UtcNow;
			}
		}

		// Token: 0x06007585 RID: 30085 RVA: 0x0018253A File Offset: 0x0018073A
		public void Increment()
		{
			Interlocked.Increment(ref this.pendingCharges);
		}

		// Token: 0x06007586 RID: 30086 RVA: 0x00182548 File Offset: 0x00180748
		public void Decrement(TimeSpan extraDuration = default(TimeSpan), bool reverseBudgetCharge = false)
		{
			Interlocked.Decrement(ref this.pendingCharges);
		}

		// Token: 0x04004B64 RID: 19300
		private int pendingCharges;
	}
}
