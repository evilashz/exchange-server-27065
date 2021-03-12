using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020009B5 RID: 2485
	internal class DelayEnforcementResults
	{
		// Token: 0x060072A0 RID: 29344 RVA: 0x0017B7CC File Offset: 0x001799CC
		public DelayEnforcementResults(DelayInfo innerDelay, TimeSpan delayedAmount) : this(innerDelay, true, null, delayedAmount)
		{
		}

		// Token: 0x060072A1 RID: 29345 RVA: 0x0017B7D8 File Offset: 0x001799D8
		public DelayEnforcementResults(DelayInfo innerDelay, string notEnforcedReason) : this(innerDelay, false, notEnforcedReason, TimeSpan.Zero)
		{
		}

		// Token: 0x060072A2 RID: 29346 RVA: 0x0017B7E8 File Offset: 0x001799E8
		private DelayEnforcementResults(DelayInfo innerDelay, bool enforced, string notEnforcedReason, TimeSpan delayedAmount)
		{
			this.DelayInfo = innerDelay;
			this.Enforced = enforced;
			this.NotEnforcedReason = notEnforcedReason;
			this.DelayedAmount = delayedAmount;
		}

		// Token: 0x17002868 RID: 10344
		// (get) Token: 0x060072A3 RID: 29347 RVA: 0x0017B80D File Offset: 0x00179A0D
		// (set) Token: 0x060072A4 RID: 29348 RVA: 0x0017B815 File Offset: 0x00179A15
		public DelayInfo DelayInfo { get; private set; }

		// Token: 0x17002869 RID: 10345
		// (get) Token: 0x060072A5 RID: 29349 RVA: 0x0017B81E File Offset: 0x00179A1E
		// (set) Token: 0x060072A6 RID: 29350 RVA: 0x0017B826 File Offset: 0x00179A26
		public bool Enforced { get; private set; }

		// Token: 0x1700286A RID: 10346
		// (get) Token: 0x060072A7 RID: 29351 RVA: 0x0017B82F File Offset: 0x00179A2F
		// (set) Token: 0x060072A8 RID: 29352 RVA: 0x0017B837 File Offset: 0x00179A37
		public TimeSpan DelayedAmount { get; private set; }

		// Token: 0x1700286B RID: 10347
		// (get) Token: 0x060072A9 RID: 29353 RVA: 0x0017B840 File Offset: 0x00179A40
		// (set) Token: 0x060072AA RID: 29354 RVA: 0x0017B848 File Offset: 0x00179A48
		public string NotEnforcedReason { get; private set; }

		// Token: 0x04004A40 RID: 19008
		public const string NotEnforcedReasonMaxDelayedThreads = "Max Delayed Threads Exceeded";

		// Token: 0x04004A41 RID: 19009
		public const string NotEnforcedReasonNoDelay = "No Delay Necessary";

		// Token: 0x04004A42 RID: 19010
		public const string NotEnforcedReasonStrict = "Strict Delay Exceeds Preferred Delay";

		// Token: 0x04004A43 RID: 19011
		public const string NotEnforcedReasonCanceled = "OnBeforeDelay delegate returned false";

		// Token: 0x04004A44 RID: 19012
		public const string NotEnforcedReasonTooLong = "Delay Too Long";
	}
}
