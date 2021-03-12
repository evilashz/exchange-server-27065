using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.Pop3
{
	// Token: 0x02000333 RID: 819
	public static class ExTraceGlobals
	{
		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x0004DB11 File Offset: 0x0004BD11
		public static Trace ServerTracer
		{
			get
			{
				if (ExTraceGlobals.serverTracer == null)
				{
					ExTraceGlobals.serverTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.serverTracer;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x0004DB2F File Offset: 0x0004BD2F
		public static Trace SessionTracer
		{
			get
			{
				if (ExTraceGlobals.sessionTracer == null)
				{
					ExTraceGlobals.sessionTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.sessionTracer;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x0004DB4D File Offset: 0x0004BD4D
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

		// Token: 0x04001677 RID: 5751
		private static Guid componentGuid = new Guid("CE267B2B-B25F-4e73-BDDA-0C0734D8019B");

		// Token: 0x04001678 RID: 5752
		private static Trace serverTracer = null;

		// Token: 0x04001679 RID: 5753
		private static Trace sessionTracer = null;

		// Token: 0x0400167A RID: 5754
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
