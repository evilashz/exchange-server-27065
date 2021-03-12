using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009BB RID: 2491
	internal interface ITokenBucket
	{
		// Token: 0x17002937 RID: 10551
		// (get) Token: 0x06007395 RID: 29589
		int PendingCharges { get; }

		// Token: 0x17002938 RID: 10552
		// (get) Token: 0x06007396 RID: 29590
		DateTime? LockedUntilUtc { get; }

		// Token: 0x17002939 RID: 10553
		// (get) Token: 0x06007397 RID: 29591
		bool Locked { get; }

		// Token: 0x1700293A RID: 10554
		// (get) Token: 0x06007398 RID: 29592
		DateTime? LockedAt { get; }

		// Token: 0x1700293B RID: 10555
		// (get) Token: 0x06007399 RID: 29593
		int MaximumBalance { get; }

		// Token: 0x1700293C RID: 10556
		// (get) Token: 0x0600739A RID: 29594
		int MinimumBalance { get; }

		// Token: 0x1700293D RID: 10557
		// (get) Token: 0x0600739B RID: 29595
		int RechargeRate { get; }

		// Token: 0x0600739C RID: 29596
		float GetBalance();

		// Token: 0x1700293E RID: 10558
		// (get) Token: 0x0600739D RID: 29597
		DateTime LastUpdateUtc { get; }

		// Token: 0x0600739E RID: 29598
		void Increment();

		// Token: 0x0600739F RID: 29599
		void Decrement(TimeSpan extraDuration = default(TimeSpan), bool reverseBudgetCharge = false);
	}
}
