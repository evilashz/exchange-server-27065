using System;

namespace Microsoft.Exchange.Diagnostics.Components.Mserve
{
	// Token: 0x0200031D RID: 797
	public static class ExTraceGlobals
	{
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x0004BFC1 File Offset: 0x0004A1C1
		public static Trace ProviderTracer
		{
			get
			{
				if (ExTraceGlobals.providerTracer == null)
				{
					ExTraceGlobals.providerTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.providerTracer;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001101 RID: 4353 RVA: 0x0004BFDF File Offset: 0x0004A1DF
		public static Trace TargetConnectionTracer
		{
			get
			{
				if (ExTraceGlobals.targetConnectionTracer == null)
				{
					ExTraceGlobals.targetConnectionTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.targetConnectionTracer;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001102 RID: 4354 RVA: 0x0004BFFD File Offset: 0x0004A1FD
		public static Trace ConfigTracer
		{
			get
			{
				if (ExTraceGlobals.configTracer == null)
				{
					ExTraceGlobals.configTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.configTracer;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x0004C01B File Offset: 0x0004A21B
		public static Trace DeltaSyncAPITracer
		{
			get
			{
				if (ExTraceGlobals.deltaSyncAPITracer == null)
				{
					ExTraceGlobals.deltaSyncAPITracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.deltaSyncAPITracer;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001104 RID: 4356 RVA: 0x0004C039 File Offset: 0x0004A239
		public static Trace MserveCacheServiceTracer
		{
			get
			{
				if (ExTraceGlobals.mserveCacheServiceTracer == null)
				{
					ExTraceGlobals.mserveCacheServiceTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.mserveCacheServiceTracer;
			}
		}

		// Token: 0x040015AE RID: 5550
		private static Guid componentGuid = new Guid("86790e72-3e66-4b27-b3e1-66faaa21840f");

		// Token: 0x040015AF RID: 5551
		private static Trace providerTracer = null;

		// Token: 0x040015B0 RID: 5552
		private static Trace targetConnectionTracer = null;

		// Token: 0x040015B1 RID: 5553
		private static Trace configTracer = null;

		// Token: 0x040015B2 RID: 5554
		private static Trace deltaSyncAPITracer = null;

		// Token: 0x040015B3 RID: 5555
		private static Trace mserveCacheServiceTracer = null;
	}
}
