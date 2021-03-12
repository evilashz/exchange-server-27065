using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.RedirectionModule
{
	// Token: 0x0200036E RID: 878
	public static class ExTraceGlobals
	{
		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x00053DE2 File Offset: 0x00051FE2
		public static Trace RedirectionTracer
		{
			get
			{
				if (ExTraceGlobals.redirectionTracer == null)
				{
					ExTraceGlobals.redirectionTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.redirectionTracer;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x00053E00 File Offset: 0x00052000
		public static Trace ErrorReportingTracer
		{
			get
			{
				if (ExTraceGlobals.errorReportingTracer == null)
				{
					ExTraceGlobals.errorReportingTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.errorReportingTracer;
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x00053E1E File Offset: 0x0005201E
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x00053E3C File Offset: 0x0005203C
		public static Trace TenantMonitoringTracer
		{
			get
			{
				if (ExTraceGlobals.tenantMonitoringTracer == null)
				{
					ExTraceGlobals.tenantMonitoringTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.tenantMonitoringTracer;
			}
		}

		// Token: 0x04001943 RID: 6467
		private static Guid componentGuid = new Guid("62a46310-1793-40b2-9f61-74bf8637fce6");

		// Token: 0x04001944 RID: 6468
		private static Trace redirectionTracer = null;

		// Token: 0x04001945 RID: 6469
		private static Trace errorReportingTracer = null;

		// Token: 0x04001946 RID: 6470
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001947 RID: 6471
		private static Trace tenantMonitoringTracer = null;
	}
}
