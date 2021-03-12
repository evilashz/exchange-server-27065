using System;

namespace Microsoft.Exchange.Diagnostics.Components.DeliveryService
{
	// Token: 0x02000328 RID: 808
	public static class ExTraceGlobals
	{
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x0004C610 File Offset: 0x0004A810
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

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x0004C62E File Offset: 0x0004A82E
		public static Trace StoreDriverDeliveryTracer
		{
			get
			{
				if (ExTraceGlobals.storeDriverDeliveryTracer == null)
				{
					ExTraceGlobals.storeDriverDeliveryTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.storeDriverDeliveryTracer;
			}
		}

		// Token: 0x040015E0 RID: 5600
		private static Guid componentGuid = new Guid("AFADB38E-21D5-4937-B5A1-E30ED4615958");

		// Token: 0x040015E1 RID: 5601
		private static Trace serviceTracer = null;

		// Token: 0x040015E2 RID: 5602
		private static Trace storeDriverDeliveryTracer = null;
	}
}
