using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.BackgroundJobManager
{
	// Token: 0x020003D9 RID: 985
	public static class ExTraceGlobals
	{
		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06001773 RID: 6003 RVA: 0x00059F66 File Offset: 0x00058166
		public static Trace SDKTracer
		{
			get
			{
				if (ExTraceGlobals.sDKTracer == null)
				{
					ExTraceGlobals.sDKTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.sDKTracer;
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06001774 RID: 6004 RVA: 0x00059F84 File Offset: 0x00058184
		public static Trace ServiceTracer
		{
			get
			{
				if (ExTraceGlobals.serviceTracer == null)
				{
					ExTraceGlobals.serviceTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.serviceTracer;
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06001775 RID: 6005 RVA: 0x00059FA2 File Offset: 0x000581A2
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

		// Token: 0x04001C21 RID: 7201
		private static Guid componentGuid = new Guid("790DC26A-9CC8-400D-84B0-1CAA155054BE");

		// Token: 0x04001C22 RID: 7202
		private static Trace sDKTracer = null;

		// Token: 0x04001C23 RID: 7203
		private static Trace serviceTracer = null;

		// Token: 0x04001C24 RID: 7204
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
