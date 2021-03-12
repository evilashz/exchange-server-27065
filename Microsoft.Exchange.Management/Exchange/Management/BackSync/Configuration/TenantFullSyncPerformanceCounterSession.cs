using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.BackSync.Configuration
{
	// Token: 0x020000A4 RID: 164
	internal class TenantFullSyncPerformanceCounterSession : PerformanceCounterSession
	{
		// Token: 0x0600055A RID: 1370 RVA: 0x000163A3 File Offset: 0x000145A3
		public TenantFullSyncPerformanceCounterSession(bool enablePerformanceCounters) : base(enablePerformanceCounters)
		{
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x000163AC File Offset: 0x000145AC
		protected override ExPerformanceCounter RequestTime
		{
			get
			{
				return BackSyncPerfCounters.TenantFullSyncTime;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x000163B3 File Offset: 0x000145B3
		protected override ExPerformanceCounter RequestCount
		{
			get
			{
				return BackSyncPerfCounters.TenantFullSyncCount;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x000163BA File Offset: 0x000145BA
		protected override ExPerformanceCounter TimeSinceLast
		{
			get
			{
				return BackSyncPerfCounters.TenantFullSyncTimeSinceLast;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x000163C1 File Offset: 0x000145C1
		protected override PerformanceCounterSession.HitRatePerformanceCounters Success
		{
			get
			{
				return new PerformanceCounterSession.HitRatePerformanceCounters(BackSyncPerfCounters.TenantFullSyncResultSuccess, BackSyncPerfCounters.TenantFullSyncSuccessRate, BackSyncPerfCounters.TenantFullSyncSuccessBase);
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x000163D7 File Offset: 0x000145D7
		protected override PerformanceCounterSession.HitRatePerformanceCounters SystemError
		{
			get
			{
				return new PerformanceCounterSession.HitRatePerformanceCounters(BackSyncPerfCounters.TenantFullSyncResultSystemError, BackSyncPerfCounters.TenantFullSyncSystemErrorRate, BackSyncPerfCounters.TenantFullSyncSystemErrorBase);
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x000163ED File Offset: 0x000145ED
		protected override PerformanceCounterSession.HitRatePerformanceCounters UserError
		{
			get
			{
				return new PerformanceCounterSession.HitRatePerformanceCounters(BackSyncPerfCounters.TenantFullSyncResultUserError, BackSyncPerfCounters.TenantFullSyncUserErrorRate, BackSyncPerfCounters.TenantFullSyncUserErrorBase);
			}
		}
	}
}
