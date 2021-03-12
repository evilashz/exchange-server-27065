using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ManagedStore.FullTextIndex
{
	// Token: 0x02000397 RID: 919
	public static class ExTraceGlobals
	{
		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x0600162E RID: 5678 RVA: 0x00057612 File Offset: 0x00055812
		public static Trace FullTextIndexTracer
		{
			get
			{
				if (ExTraceGlobals.fullTextIndexTracer == null)
				{
					ExTraceGlobals.fullTextIndexTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.fullTextIndexTracer;
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x0600162F RID: 5679 RVA: 0x00057630 File Offset: 0x00055830
		public static Trace CriteriaFullTextFlavorTracer
		{
			get
			{
				if (ExTraceGlobals.criteriaFullTextFlavorTracer == null)
				{
					ExTraceGlobals.criteriaFullTextFlavorTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.criteriaFullTextFlavorTracer;
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06001630 RID: 5680 RVA: 0x0005764E File Offset: 0x0005584E
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 20);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x04001ADC RID: 6876
		private static Guid componentGuid = new Guid("58B1ADEC-A4DF-4762-A6C6-92D8877B408C");

		// Token: 0x04001ADD RID: 6877
		private static Trace fullTextIndexTracer = null;

		// Token: 0x04001ADE RID: 6878
		private static Trace criteriaFullTextFlavorTracer = null;

		// Token: 0x04001ADF RID: 6879
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
