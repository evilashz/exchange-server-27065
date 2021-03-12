using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.Data.FileDistributionService
{
	// Token: 0x0200033E RID: 830
	public static class ExTraceGlobals
	{
		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x00050CDC File Offset: 0x0004EEDC
		public static Trace CustomCommandTracer
		{
			get
			{
				if (ExTraceGlobals.customCommandTracer == null)
				{
					ExTraceGlobals.customCommandTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.customCommandTracer;
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x00050CFA File Offset: 0x0004EEFA
		public static Trace FileReplicationTracer
		{
			get
			{
				if (ExTraceGlobals.fileReplicationTracer == null)
				{
					ExTraceGlobals.fileReplicationTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.fileReplicationTracer;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x00050D18 File Offset: 0x0004EF18
		public static Trace ADRequestsTracer
		{
			get
			{
				if (ExTraceGlobals.aDRequestsTracer == null)
				{
					ExTraceGlobals.aDRequestsTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.aDRequestsTracer;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001329 RID: 4905 RVA: 0x00050D36 File Offset: 0x0004EF36
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x040017D4 RID: 6100
		private static Guid componentGuid = new Guid("0f0a52f9-4d72-460d-9928-1da8215066d4");

		// Token: 0x040017D5 RID: 6101
		private static Trace customCommandTracer = null;

		// Token: 0x040017D6 RID: 6102
		private static Trace fileReplicationTracer = null;

		// Token: 0x040017D7 RID: 6103
		private static Trace aDRequestsTracer = null;

		// Token: 0x040017D8 RID: 6104
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
