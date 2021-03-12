using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200015C RID: 348
	internal class MDBPerfCounterHelper
	{
		// Token: 0x06000C2B RID: 3115 RVA: 0x0001CFE0 File Offset: 0x0001B1E0
		public MDBPerfCounterHelper(string mdbName)
		{
			this.PerfCounter = MailboxReplicationServicePerMdbPerformanceCounters.GetInstance(mdbName);
			this.ReadTransferRate = new PerfCounterWithAverageRate(null, this.PerfCounter.ReadTransferRate, this.PerfCounter.ReadTransferRateBase, 1024, TimeSpan.FromSeconds(1.0));
			this.WriteTransferRate = new PerfCounterWithAverageRate(null, this.PerfCounter.WriteTransferRate, this.PerfCounter.WriteTransferRateBase, 1024, TimeSpan.FromSeconds(1.0));
			this.Completed = new PerfCounterWithAverageRate(this.PerfCounter.MoveRequestsCompleted, this.PerfCounter.MoveRequestsCompletedRate, this.PerfCounter.MoveRequestsCompletedRateBase, 1, TimeSpan.FromHours(1.0));
			this.CompletedWithWarnings = new PerfCounterWithAverageRate(this.PerfCounter.MoveRequestsCompletedWithWarnings, this.PerfCounter.MoveRequestsCompletedWithWarningsRate, this.PerfCounter.MoveRequestsCompletedWithWarningsRateBase, 1, TimeSpan.FromHours(1.0));
			this.Canceled = new PerfCounterWithAverageRate(this.PerfCounter.MoveRequestsCanceled, this.PerfCounter.MoveRequestsCanceledRate, this.PerfCounter.MoveRequestsCanceledRateBase, 1, TimeSpan.FromHours(1.0));
			this.FailTotal = new PerfCounterWithAverageRate(this.PerfCounter.MoveRequestsFailTotal, this.PerfCounter.MoveRequestsFailTotalRate, this.PerfCounter.MoveRequestsFailTotalRateBase, 1, TimeSpan.FromHours(1.0));
			this.FailBadItemLimit = new PerfCounterWithAverageRate(this.PerfCounter.MoveRequestsFailBadItemLimit, this.PerfCounter.MoveRequestsFailBadItemLimitRate, this.PerfCounter.MoveRequestsFailBadItemLimitRateBase, 1, TimeSpan.FromHours(1.0));
			this.FailNetwork = new PerfCounterWithAverageRate(this.PerfCounter.MoveRequestsFailNetwork, this.PerfCounter.MoveRequestsFailNetworkRate, this.PerfCounter.MoveRequestsFailNetworkRateBase, 1, TimeSpan.FromHours(1.0));
			this.FailStallCI = new PerfCounterWithAverageRate(this.PerfCounter.MoveRequestsFailStallCI, this.PerfCounter.MoveRequestsFailStallCIRate, this.PerfCounter.MoveRequestsFailStallCIRateBase, 1, TimeSpan.FromHours(1.0));
			this.FailStallHA = new PerfCounterWithAverageRate(this.PerfCounter.MoveRequestsFailStallHA, this.PerfCounter.MoveRequestsFailStallHARate, this.PerfCounter.MoveRequestsFailStallHARateBase, 1, TimeSpan.FromHours(1.0));
			this.FailMAPI = new PerfCounterWithAverageRate(this.PerfCounter.MoveRequestsFailMAPI, this.PerfCounter.MoveRequestsFailMAPIRate, this.PerfCounter.MoveRequestsFailMAPIRateBase, 1, TimeSpan.FromHours(1.0));
			this.FailOther = new PerfCounterWithAverageRate(this.PerfCounter.MoveRequestsFailOther, this.PerfCounter.MoveRequestsFailOtherRate, this.PerfCounter.MoveRequestsFailOtherRateBase, 1, TimeSpan.FromHours(1.0));
			this.TransientTotal = new PerfCounterWithAverageRate(this.PerfCounter.MoveRequestsTransientTotal, this.PerfCounter.MoveRequestsTransientTotalRate, this.PerfCounter.MoveRequestsTransientTotalRateBase, 1, TimeSpan.FromHours(1.0));
			this.NetworkFailures = new PerfCounterWithAverageRate(this.PerfCounter.MoveRequestsNetworkFailures, this.PerfCounter.MoveRequestsNetworkFailuresRate, this.PerfCounter.MoveRequestsNetworkFailuresRateBase, 1, TimeSpan.FromHours(1.0));
			this.ProxyBackoff = new PerfCounterWithAverageRate(this.PerfCounter.MoveRequestsProxyBackoff, this.PerfCounter.MoveRequestsProxyBackoffRate, this.PerfCounter.MoveRequestsProxyBackoffRateBase, 1, TimeSpan.FromHours(1.0));
			this.StallsTotal = new PerfCounterWithAverageRate(this.PerfCounter.MoveRequestsStallsTotal, this.PerfCounter.MoveRequestsStallsTotalRate, this.PerfCounter.MoveRequestsStallsTotalRateBase, 1, TimeSpan.FromHours(1.0));
			this.StallsHA = new PerfCounterWithAverageRate(this.PerfCounter.MoveRequestsStallsHA, this.PerfCounter.MoveRequestsStallsHARate, this.PerfCounter.MoveRequestsStallsHARateBase, 1, TimeSpan.FromHours(1.0));
			this.StallsCI = new PerfCounterWithAverageRate(this.PerfCounter.MoveRequestsStallsCI, this.PerfCounter.MoveRequestsStallsCIRate, this.PerfCounter.MoveRequestsStallsCIRateBase, 1, TimeSpan.FromHours(1.0));
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x0001D419 File Offset: 0x0001B619
		// (set) Token: 0x06000C2D RID: 3117 RVA: 0x0001D421 File Offset: 0x0001B621
		public MailboxReplicationServicePerMdbPerformanceCountersInstance PerfCounter { get; private set; }

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x0001D42A File Offset: 0x0001B62A
		// (set) Token: 0x06000C2F RID: 3119 RVA: 0x0001D432 File Offset: 0x0001B632
		public PerfCounterWithAverageRate Completed { get; private set; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x0001D43B File Offset: 0x0001B63B
		// (set) Token: 0x06000C31 RID: 3121 RVA: 0x0001D443 File Offset: 0x0001B643
		public PerfCounterWithAverageRate CompletedWithWarnings { get; private set; }

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0001D44C File Offset: 0x0001B64C
		// (set) Token: 0x06000C33 RID: 3123 RVA: 0x0001D454 File Offset: 0x0001B654
		public PerfCounterWithAverageRate Canceled { get; private set; }

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x0001D45D File Offset: 0x0001B65D
		// (set) Token: 0x06000C35 RID: 3125 RVA: 0x0001D465 File Offset: 0x0001B665
		public PerfCounterWithAverageRate FailTotal { get; private set; }

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x0001D46E File Offset: 0x0001B66E
		// (set) Token: 0x06000C37 RID: 3127 RVA: 0x0001D476 File Offset: 0x0001B676
		public PerfCounterWithAverageRate FailBadItemLimit { get; private set; }

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x0001D47F File Offset: 0x0001B67F
		// (set) Token: 0x06000C39 RID: 3129 RVA: 0x0001D487 File Offset: 0x0001B687
		public PerfCounterWithAverageRate FailNetwork { get; private set; }

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x0001D490 File Offset: 0x0001B690
		// (set) Token: 0x06000C3B RID: 3131 RVA: 0x0001D498 File Offset: 0x0001B698
		public PerfCounterWithAverageRate FailStallCI { get; private set; }

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x0001D4A1 File Offset: 0x0001B6A1
		// (set) Token: 0x06000C3D RID: 3133 RVA: 0x0001D4A9 File Offset: 0x0001B6A9
		public PerfCounterWithAverageRate FailStallHA { get; private set; }

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x0001D4B2 File Offset: 0x0001B6B2
		// (set) Token: 0x06000C3F RID: 3135 RVA: 0x0001D4BA File Offset: 0x0001B6BA
		public PerfCounterWithAverageRate FailMAPI { get; private set; }

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000C40 RID: 3136 RVA: 0x0001D4C3 File Offset: 0x0001B6C3
		// (set) Token: 0x06000C41 RID: 3137 RVA: 0x0001D4CB File Offset: 0x0001B6CB
		public PerfCounterWithAverageRate FailOther { get; private set; }

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x0001D4D4 File Offset: 0x0001B6D4
		// (set) Token: 0x06000C43 RID: 3139 RVA: 0x0001D4DC File Offset: 0x0001B6DC
		public PerfCounterWithAverageRate TransientTotal { get; private set; }

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x0001D4E5 File Offset: 0x0001B6E5
		// (set) Token: 0x06000C45 RID: 3141 RVA: 0x0001D4ED File Offset: 0x0001B6ED
		public PerfCounterWithAverageRate NetworkFailures { get; private set; }

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000C46 RID: 3142 RVA: 0x0001D4F6 File Offset: 0x0001B6F6
		// (set) Token: 0x06000C47 RID: 3143 RVA: 0x0001D4FE File Offset: 0x0001B6FE
		public PerfCounterWithAverageRate ProxyBackoff { get; private set; }

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x0001D507 File Offset: 0x0001B707
		// (set) Token: 0x06000C49 RID: 3145 RVA: 0x0001D50F File Offset: 0x0001B70F
		public PerfCounterWithAverageRate StallsTotal { get; private set; }

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x0001D518 File Offset: 0x0001B718
		// (set) Token: 0x06000C4B RID: 3147 RVA: 0x0001D520 File Offset: 0x0001B720
		public PerfCounterWithAverageRate StallsHA { get; private set; }

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x0001D529 File Offset: 0x0001B729
		// (set) Token: 0x06000C4D RID: 3149 RVA: 0x0001D531 File Offset: 0x0001B731
		public PerfCounterWithAverageRate StallsCI { get; private set; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000C4E RID: 3150 RVA: 0x0001D53A File Offset: 0x0001B73A
		// (set) Token: 0x06000C4F RID: 3151 RVA: 0x0001D542 File Offset: 0x0001B742
		public PerfCounterWithAverageRate ReadTransferRate { get; private set; }

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x0001D54B File Offset: 0x0001B74B
		// (set) Token: 0x06000C51 RID: 3153 RVA: 0x0001D553 File Offset: 0x0001B753
		public PerfCounterWithAverageRate WriteTransferRate { get; private set; }

		// Token: 0x06000C52 RID: 3154 RVA: 0x0001D55C File Offset: 0x0001B75C
		public void RemovePerfCounter()
		{
			MailboxReplicationServicePerMdbPerformanceCounters.RemoveInstance(this.PerfCounter.Name);
		}
	}
}
