using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.BackSync.Configuration
{
	// Token: 0x020000A3 RID: 163
	internal class ObjectFullSyncPerformanceCounterSession : PerformanceCounterSession
	{
		// Token: 0x06000553 RID: 1363 RVA: 0x00016343 File Offset: 0x00014543
		public ObjectFullSyncPerformanceCounterSession(bool enablePerformanceCounters) : base(enablePerformanceCounters)
		{
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x0001634C File Offset: 0x0001454C
		protected override ExPerformanceCounter RequestTime
		{
			get
			{
				return BackSyncPerfCounters.ObjectFullSyncTime;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x00016353 File Offset: 0x00014553
		protected override ExPerformanceCounter RequestCount
		{
			get
			{
				return BackSyncPerfCounters.ObjectFullSyncCount;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x0001635A File Offset: 0x0001455A
		protected override ExPerformanceCounter TimeSinceLast
		{
			get
			{
				return BackSyncPerfCounters.ObjectFullSyncTimeSinceLast;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00016361 File Offset: 0x00014561
		protected override PerformanceCounterSession.HitRatePerformanceCounters Success
		{
			get
			{
				return new PerformanceCounterSession.HitRatePerformanceCounters(BackSyncPerfCounters.ObjectFullSyncResultSuccess, BackSyncPerfCounters.ObjectFullSyncSuccessRate, BackSyncPerfCounters.ObjectFullSyncSuccessBase);
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x00016377 File Offset: 0x00014577
		protected override PerformanceCounterSession.HitRatePerformanceCounters SystemError
		{
			get
			{
				return new PerformanceCounterSession.HitRatePerformanceCounters(BackSyncPerfCounters.ObjectFullSyncResultSystemError, BackSyncPerfCounters.ObjectFullSyncSystemErrorRate, BackSyncPerfCounters.ObjectFullSyncSystemErrorBase);
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0001638D File Offset: 0x0001458D
		protected override PerformanceCounterSession.HitRatePerformanceCounters UserError
		{
			get
			{
				return new PerformanceCounterSession.HitRatePerformanceCounters(BackSyncPerfCounters.ObjectFullSyncResultUserError, BackSyncPerfCounters.ObjectFullSyncUserErrorRate, BackSyncPerfCounters.ObjectFullSyncUserErrorBase);
			}
		}
	}
}
