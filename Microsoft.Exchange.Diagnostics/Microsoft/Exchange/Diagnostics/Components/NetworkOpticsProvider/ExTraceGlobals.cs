using System;

namespace Microsoft.Exchange.Diagnostics.Components.NetworkOpticsProvider
{
	// Token: 0x020003FC RID: 1020
	public static class ExTraceGlobals
	{
		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06001873 RID: 6259 RVA: 0x0005C178 File Offset: 0x0005A378
		public static Trace NetworkOpticsProviderTracer
		{
			get
			{
				if (ExTraceGlobals.networkOpticsProviderTracer == null)
				{
					ExTraceGlobals.networkOpticsProviderTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.networkOpticsProviderTracer;
			}
		}

		// Token: 0x04001D21 RID: 7457
		private static Guid componentGuid = new Guid("AEAC8836-FB11-4be9-BB2E-D92EA3F4A358");

		// Token: 0x04001D22 RID: 7458
		private static Trace networkOpticsProviderTracer = null;
	}
}
