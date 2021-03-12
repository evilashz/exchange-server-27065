using System;

namespace Microsoft.Exchange.Diagnostics.Components.SharedCache
{
	// Token: 0x02000407 RID: 1031
	public static class ExTraceGlobals
	{
		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x060018D7 RID: 6359 RVA: 0x0005CF0F File Offset: 0x0005B10F
		public static Trace CacheTracer
		{
			get
			{
				if (ExTraceGlobals.cacheTracer == null)
				{
					ExTraceGlobals.cacheTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.cacheTracer;
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x060018D8 RID: 6360 RVA: 0x0005CF2D File Offset: 0x0005B12D
		public static Trace ServerTracer
		{
			get
			{
				if (ExTraceGlobals.serverTracer == null)
				{
					ExTraceGlobals.serverTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.serverTracer;
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x060018D9 RID: 6361 RVA: 0x0005CF4B File Offset: 0x0005B14B
		public static Trace ClientTracer
		{
			get
			{
				if (ExTraceGlobals.clientTracer == null)
				{
					ExTraceGlobals.clientTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.clientTracer;
			}
		}

		// Token: 0x04001D85 RID: 7557
		private static Guid componentGuid = new Guid("E71C276F-E35F-40CB-BC7E-559CE4A9B4B3");

		// Token: 0x04001D86 RID: 7558
		private static Trace cacheTracer = null;

		// Token: 0x04001D87 RID: 7559
		private static Trace serverTracer = null;

		// Token: 0x04001D88 RID: 7560
		private static Trace clientTracer = null;
	}
}
