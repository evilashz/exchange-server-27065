using System;

namespace Microsoft.Exchange.Diagnostics.Components.LogSearch
{
	// Token: 0x0200031A RID: 794
	public static class ExTraceGlobals
	{
		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x0004B59C File Offset: 0x0004979C
		public static Trace ServiceTracer
		{
			get
			{
				if (ExTraceGlobals.serviceTracer == null)
				{
					ExTraceGlobals.serviceTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.serviceTracer;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060010B9 RID: 4281 RVA: 0x0004B5BA File Offset: 0x000497BA
		public static Trace CommonTracer
		{
			get
			{
				if (ExTraceGlobals.commonTracer == null)
				{
					ExTraceGlobals.commonTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.commonTracer;
			}
		}

		// Token: 0x04001566 RID: 5478
		private static Guid componentGuid = new Guid("6510D9BF-20C7-4c2e-A346-E1B4D1112527");

		// Token: 0x04001567 RID: 5479
		private static Trace serviceTracer = null;

		// Token: 0x04001568 RID: 5480
		private static Trace commonTracer = null;
	}
}
