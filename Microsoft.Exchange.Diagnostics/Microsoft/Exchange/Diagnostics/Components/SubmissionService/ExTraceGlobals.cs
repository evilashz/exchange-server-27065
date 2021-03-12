using System;

namespace Microsoft.Exchange.Diagnostics.Components.SubmissionService
{
	// Token: 0x0200032A RID: 810
	public static class ExTraceGlobals
	{
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x0004C918 File Offset: 0x0004AB18
		public static Trace ServiceTracer
		{
			get
			{
				if (ExTraceGlobals.serviceTracer == null)
				{
					ExTraceGlobals.serviceTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.serviceTracer;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x0004C936 File Offset: 0x0004AB36
		public static Trace StoreDriverSubmissionTracer
		{
			get
			{
				if (ExTraceGlobals.storeDriverSubmissionTracer == null)
				{
					ExTraceGlobals.storeDriverSubmissionTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.storeDriverSubmissionTracer;
			}
		}

		// Token: 0x040015F6 RID: 5622
		private static Guid componentGuid = new Guid("ef777296-2ff4-4617-8abd-5490cfb2d5c6");

		// Token: 0x040015F7 RID: 5623
		private static Trace serviceTracer = null;

		// Token: 0x040015F8 RID: 5624
		private static Trace storeDriverSubmissionTracer = null;
	}
}
