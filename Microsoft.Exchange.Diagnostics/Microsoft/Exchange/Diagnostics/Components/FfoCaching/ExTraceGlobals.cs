using System;

namespace Microsoft.Exchange.Diagnostics.Components.FfoCaching
{
	// Token: 0x020003EE RID: 1006
	public static class ExTraceGlobals
	{
		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x0600182C RID: 6188 RVA: 0x0005B865 File Offset: 0x00059A65
		public static Trace PrimingThreadTracer
		{
			get
			{
				if (ExTraceGlobals.primingThreadTracer == null)
				{
					ExTraceGlobals.primingThreadTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.primingThreadTracer;
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x0005B883 File Offset: 0x00059A83
		public static Trace CachingProviderTracer
		{
			get
			{
				if (ExTraceGlobals.cachingProviderTracer == null)
				{
					ExTraceGlobals.cachingProviderTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.cachingProviderTracer;
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x0600182E RID: 6190 RVA: 0x0005B8A1 File Offset: 0x00059AA1
		public static Trace CompositeProviderTracer
		{
			get
			{
				if (ExTraceGlobals.compositeProviderTracer == null)
				{
					ExTraceGlobals.compositeProviderTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.compositeProviderTracer;
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x0600182F RID: 6191 RVA: 0x0005B8BF File Offset: 0x00059ABF
		public static Trace PrimingStateLocalCacheTracer
		{
			get
			{
				if (ExTraceGlobals.primingStateLocalCacheTracer == null)
				{
					ExTraceGlobals.primingStateLocalCacheTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.primingStateLocalCacheTracer;
			}
		}

		// Token: 0x04001CDA RID: 7386
		private static Guid componentGuid = new Guid("880B0BC2-765E-4B89-82A0-9FFBBA7B8BE1");

		// Token: 0x04001CDB RID: 7387
		private static Trace primingThreadTracer = null;

		// Token: 0x04001CDC RID: 7388
		private static Trace cachingProviderTracer = null;

		// Token: 0x04001CDD RID: 7389
		private static Trace compositeProviderTracer = null;

		// Token: 0x04001CDE RID: 7390
		private static Trace primingStateLocalCacheTracer = null;
	}
}
