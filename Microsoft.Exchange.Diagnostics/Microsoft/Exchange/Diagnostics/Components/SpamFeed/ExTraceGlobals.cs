using System;

namespace Microsoft.Exchange.Diagnostics.Components.SpamFeed
{
	// Token: 0x020003F4 RID: 1012
	public static class ExTraceGlobals
	{
		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06001858 RID: 6232 RVA: 0x0005BE44 File Offset: 0x0005A044
		public static Trace RoutingTracer
		{
			get
			{
				if (ExTraceGlobals.routingTracer == null)
				{
					ExTraceGlobals.routingTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.routingTracer;
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06001859 RID: 6233 RVA: 0x0005BE62 File Offset: 0x0005A062
		public static Trace DeliveryAgentTracer
		{
			get
			{
				if (ExTraceGlobals.deliveryAgentTracer == null)
				{
					ExTraceGlobals.deliveryAgentTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.deliveryAgentTracer;
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x0600185A RID: 6234 RVA: 0x0005BE80 File Offset: 0x0005A080
		public static Trace KEStoreTracer
		{
			get
			{
				if (ExTraceGlobals.kEStoreTracer == null)
				{
					ExTraceGlobals.kEStoreTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.kEStoreTracer;
			}
		}

		// Token: 0x04001D06 RID: 7430
		private static Guid componentGuid = new Guid("4A0B58AE-577F-415c-AD4F-4C577162EBDD");

		// Token: 0x04001D07 RID: 7431
		private static Trace routingTracer = null;

		// Token: 0x04001D08 RID: 7432
		private static Trace deliveryAgentTracer = null;

		// Token: 0x04001D09 RID: 7433
		private static Trace kEStoreTracer = null;
	}
}
