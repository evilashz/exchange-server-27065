using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.BackSync.Configuration
{
	// Token: 0x020000A2 RID: 162
	internal class IncrementalSyncPerformanceCounterSession : PerformanceCounterSession
	{
		// Token: 0x0600054A RID: 1354 RVA: 0x000162A8 File Offset: 0x000144A8
		public IncrementalSyncPerformanceCounterSession(bool enablePerformanceCounters) : base(enablePerformanceCounters)
		{
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x000162B1 File Offset: 0x000144B1
		protected override ExPerformanceCounter RequestTime
		{
			get
			{
				return BackSyncPerfCounters.DeltaSyncTime;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600054C RID: 1356 RVA: 0x000162B8 File Offset: 0x000144B8
		protected override ExPerformanceCounter RequestCount
		{
			get
			{
				return BackSyncPerfCounters.DeltaSyncCount;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x000162BF File Offset: 0x000144BF
		protected override ExPerformanceCounter TimeSinceLast
		{
			get
			{
				return BackSyncPerfCounters.DeltaSyncTimeSinceLast;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x000162C6 File Offset: 0x000144C6
		protected override PerformanceCounterSession.HitRatePerformanceCounters Success
		{
			get
			{
				return new PerformanceCounterSession.HitRatePerformanceCounters(BackSyncPerfCounters.DeltaSyncResultSuccess, BackSyncPerfCounters.DeltaSyncSuccessRate, BackSyncPerfCounters.DeltaSyncSuccessBase);
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x000162DC File Offset: 0x000144DC
		protected override PerformanceCounterSession.HitRatePerformanceCounters SystemError
		{
			get
			{
				return new PerformanceCounterSession.HitRatePerformanceCounters(BackSyncPerfCounters.DeltaSyncResultSystemError, BackSyncPerfCounters.DeltaSyncSystemErrorRate, BackSyncPerfCounters.DeltaSyncSystemErrorBase);
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x000162F2 File Offset: 0x000144F2
		protected override PerformanceCounterSession.HitRatePerformanceCounters UserError
		{
			get
			{
				return new PerformanceCounterSession.HitRatePerformanceCounters(BackSyncPerfCounters.DeltaSyncResultUserError, BackSyncPerfCounters.DeltaSyncUserErrorRate, BackSyncPerfCounters.DeltaSyncUserErrorBase);
			}
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00016308 File Offset: 0x00014508
		public override void ReportChangeCount(int changeCount)
		{
			if (base.EnablePerformanceCounters)
			{
				BackSyncPerfCounters.DeltaSyncChangeCount.RawValue = (long)changeCount;
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0001631E File Offset: 0x0001451E
		public override void ReportSameCookie(bool sameCookie)
		{
			if (base.EnablePerformanceCounters)
			{
				if (sameCookie)
				{
					BackSyncPerfCounters.DeltaSyncRetryCookieCount.Increment();
					return;
				}
				BackSyncPerfCounters.DeltaSyncRetryCookieCount.RawValue = 0L;
			}
		}
	}
}
