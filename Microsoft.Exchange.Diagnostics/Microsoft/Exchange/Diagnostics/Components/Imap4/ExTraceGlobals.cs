using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.Imap4
{
	// Token: 0x02000332 RID: 818
	public static class ExTraceGlobals
	{
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x0004DA94 File Offset: 0x0004BC94
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

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x0004DAB2 File Offset: 0x0004BCB2
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

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x0004DAD0 File Offset: 0x0004BCD0
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

		// Token: 0x04001673 RID: 5747
		private static Guid componentGuid = new Guid("B338D7C6-58F5-4523-B459-E387B7C956BA");

		// Token: 0x04001674 RID: 5748
		private static Trace serverTracer = null;

		// Token: 0x04001675 RID: 5749
		private static Trace sessionTracer = null;

		// Token: 0x04001676 RID: 5750
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
