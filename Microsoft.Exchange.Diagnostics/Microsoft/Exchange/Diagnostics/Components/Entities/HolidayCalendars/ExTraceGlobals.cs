using System;

namespace Microsoft.Exchange.Diagnostics.Components.Entities.HolidayCalendars
{
	// Token: 0x02000402 RID: 1026
	public static class ExTraceGlobals
	{
		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x0005CD2E File Offset: 0x0005AF2E
		public static Trace ConfigurationCacheTracer
		{
			get
			{
				if (ExTraceGlobals.configurationCacheTracer == null)
				{
					ExTraceGlobals.configurationCacheTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.configurationCacheTracer;
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x0005CD4C File Offset: 0x0005AF4C
		public static Trace EndpointConfigurationRetrieverTracer
		{
			get
			{
				if (ExTraceGlobals.endpointConfigurationRetrieverTracer == null)
				{
					ExTraceGlobals.endpointConfigurationRetrieverTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.endpointConfigurationRetrieverTracer;
			}
		}

		// Token: 0x04001D75 RID: 7541
		private static Guid componentGuid = new Guid("B8764FA6-79B0-42B6-8209-17E80F43CA43");

		// Token: 0x04001D76 RID: 7542
		private static Trace configurationCacheTracer = null;

		// Token: 0x04001D77 RID: 7543
		private static Trace endpointConfigurationRetrieverTracer = null;
	}
}
