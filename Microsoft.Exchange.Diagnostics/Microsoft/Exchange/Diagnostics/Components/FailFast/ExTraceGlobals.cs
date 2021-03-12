using System;

namespace Microsoft.Exchange.Diagnostics.Components.FailFast
{
	// Token: 0x02000370 RID: 880
	public static class ExTraceGlobals
	{
		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x00053F00 File Offset: 0x00052100
		public static Trace FailFastCacheTracer
		{
			get
			{
				if (ExTraceGlobals.failFastCacheTracer == null)
				{
					ExTraceGlobals.failFastCacheTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.failFastCacheTracer;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x00053F1E File Offset: 0x0005211E
		public static Trace FailFastModuleTracer
		{
			get
			{
				if (ExTraceGlobals.failFastModuleTracer == null)
				{
					ExTraceGlobals.failFastModuleTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.failFastModuleTracer;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x00053F3C File Offset: 0x0005213C
		public static Trace FailureThrottlingTracer
		{
			get
			{
				if (ExTraceGlobals.failureThrottlingTracer == null)
				{
					ExTraceGlobals.failureThrottlingTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.failureThrottlingTracer;
			}
		}

		// Token: 0x0400194C RID: 6476
		private static Guid componentGuid = new Guid("04E8E535-4C59-49CC-B92D-4598368E5B36");

		// Token: 0x0400194D RID: 6477
		private static Trace failFastCacheTracer = null;

		// Token: 0x0400194E RID: 6478
		private static Trace failFastModuleTracer = null;

		// Token: 0x0400194F RID: 6479
		private static Trace failureThrottlingTracer = null;
	}
}
