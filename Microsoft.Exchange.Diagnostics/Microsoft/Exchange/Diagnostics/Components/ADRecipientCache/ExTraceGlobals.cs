using System;

namespace Microsoft.Exchange.Diagnostics.Components.ADRecipientCache
{
	// Token: 0x02000324 RID: 804
	public static class ExTraceGlobals
	{
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x0004C254 File Offset: 0x0004A454
		public static Trace ADLookupTracer
		{
			get
			{
				if (ExTraceGlobals.aDLookupTracer == null)
				{
					ExTraceGlobals.aDLookupTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.aDLookupTracer;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x0004C272 File Offset: 0x0004A472
		public static Trace CacheLookupTracer
		{
			get
			{
				if (ExTraceGlobals.cacheLookupTracer == null)
				{
					ExTraceGlobals.cacheLookupTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.cacheLookupTracer;
			}
		}

		// Token: 0x040015C4 RID: 5572
		private static Guid componentGuid = new Guid("48868D1B-4502-4c8e-8293-E81776D01BCE");

		// Token: 0x040015C5 RID: 5573
		private static Trace aDLookupTracer = null;

		// Token: 0x040015C6 RID: 5574
		private static Trace cacheLookupTracer = null;
	}
}
