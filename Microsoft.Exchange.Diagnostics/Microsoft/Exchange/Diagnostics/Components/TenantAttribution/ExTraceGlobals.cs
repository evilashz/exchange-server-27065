using System;

namespace Microsoft.Exchange.Diagnostics.Components.TenantAttribution
{
	// Token: 0x02000388 RID: 904
	public static class ExTraceGlobals
	{
		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x00055C7E File Offset: 0x00053E7E
		public static Trace TenantAttributionInboundTracer
		{
			get
			{
				if (ExTraceGlobals.tenantAttributionInboundTracer == null)
				{
					ExTraceGlobals.tenantAttributionInboundTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.tenantAttributionInboundTracer;
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001579 RID: 5497 RVA: 0x00055C9C File Offset: 0x00053E9C
		public static Trace TenantAttributionOutboundTracer
		{
			get
			{
				if (ExTraceGlobals.tenantAttributionOutboundTracer == null)
				{
					ExTraceGlobals.tenantAttributionOutboundTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.tenantAttributionOutboundTracer;
			}
		}

		// Token: 0x04001A26 RID: 6694
		private static Guid componentGuid = new Guid("97680724-6FF7-4C3A-BD8F-6E329E54AF3A");

		// Token: 0x04001A27 RID: 6695
		private static Trace tenantAttributionInboundTracer = null;

		// Token: 0x04001A28 RID: 6696
		private static Trace tenantAttributionOutboundTracer = null;
	}
}
