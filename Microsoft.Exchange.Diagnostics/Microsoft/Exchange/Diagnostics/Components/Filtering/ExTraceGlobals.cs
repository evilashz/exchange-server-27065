using System;

namespace Microsoft.Exchange.Diagnostics.Components.Filtering
{
	// Token: 0x020003F8 RID: 1016
	public static class ExTraceGlobals
	{
		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06001864 RID: 6244 RVA: 0x0005BFA8 File Offset: 0x0005A1A8
		public static Trace ADConnectorTracer
		{
			get
			{
				if (ExTraceGlobals.aDConnectorTracer == null)
				{
					ExTraceGlobals.aDConnectorTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.aDConnectorTracer;
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06001865 RID: 6245 RVA: 0x0005BFC6 File Offset: 0x0005A1C6
		public static Trace FilteringServiceApiTracer
		{
			get
			{
				if (ExTraceGlobals.filteringServiceApiTracer == null)
				{
					ExTraceGlobals.filteringServiceApiTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.filteringServiceApiTracer;
			}
		}

		// Token: 0x04001D12 RID: 7442
		private static Guid componentGuid = new Guid("2D0C84FD-7C17-4091-8293-86745B18C1E8");

		// Token: 0x04001D13 RID: 7443
		private static Trace aDConnectorTracer = null;

		// Token: 0x04001D14 RID: 7444
		private static Trace filteringServiceApiTracer = null;
	}
}
