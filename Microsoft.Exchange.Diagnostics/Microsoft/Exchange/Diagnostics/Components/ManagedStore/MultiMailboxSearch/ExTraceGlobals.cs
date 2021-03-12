using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ManagedStore.MultiMailboxSearch
{
	// Token: 0x02000398 RID: 920
	public static class ExTraceGlobals
	{
		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06001632 RID: 5682 RVA: 0x00057690 File Offset: 0x00055890
		public static Trace FullTextIndexTracer
		{
			get
			{
				if (ExTraceGlobals.fullTextIndexTracer == null)
				{
					ExTraceGlobals.fullTextIndexTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.fullTextIndexTracer;
			}
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06001633 RID: 5683 RVA: 0x000576AE File Offset: 0x000558AE
		public static Trace SearchTracer
		{
			get
			{
				if (ExTraceGlobals.searchTracer == null)
				{
					ExTraceGlobals.searchTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.searchTracer;
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x000576CC File Offset: 0x000558CC
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 20);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x04001AE0 RID: 6880
		private static Guid componentGuid = new Guid("A1D513D5-5D1D-4c2d-A5D4-44005EA7DB83");

		// Token: 0x04001AE1 RID: 6881
		private static Trace fullTextIndexTracer = null;

		// Token: 0x04001AE2 RID: 6882
		private static Trace searchTracer = null;

		// Token: 0x04001AE3 RID: 6883
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
