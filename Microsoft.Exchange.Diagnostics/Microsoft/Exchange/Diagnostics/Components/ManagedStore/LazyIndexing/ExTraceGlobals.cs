using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ManagedStore.LazyIndexing
{
	// Token: 0x02000392 RID: 914
	public static class ExTraceGlobals
	{
		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x060015B4 RID: 5556 RVA: 0x0005644E File Offset: 0x0005464E
		public static Trace PseudoIndexTracer
		{
			get
			{
				if (ExTraceGlobals.pseudoIndexTracer == null)
				{
					ExTraceGlobals.pseudoIndexTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.pseudoIndexTracer;
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x0005646C File Offset: 0x0005466C
		public static Trace CategoryHeaderViewPopulationTracer
		{
			get
			{
				if (ExTraceGlobals.categoryHeaderViewPopulationTracer == null)
				{
					ExTraceGlobals.categoryHeaderViewPopulationTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.categoryHeaderViewPopulationTracer;
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x0005648A File Offset: 0x0005468A
		public static Trace CategoryHeaderViewMaintenanceTracer
		{
			get
			{
				if (ExTraceGlobals.categoryHeaderViewMaintenanceTracer == null)
				{
					ExTraceGlobals.categoryHeaderViewMaintenanceTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.categoryHeaderViewMaintenanceTracer;
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x000564A8 File Offset: 0x000546A8
		public static Trace CategorizedViewsTracer
		{
			get
			{
				if (ExTraceGlobals.categorizedViewsTracer == null)
				{
					ExTraceGlobals.categorizedViewsTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.categorizedViewsTracer;
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x060015B8 RID: 5560 RVA: 0x000564C6 File Offset: 0x000546C6
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

		// Token: 0x04001A62 RID: 6754
		private static Guid componentGuid = new Guid("0e12474e-7e64-471f-93f5-901f795c4ae0");

		// Token: 0x04001A63 RID: 6755
		private static Trace pseudoIndexTracer = null;

		// Token: 0x04001A64 RID: 6756
		private static Trace categoryHeaderViewPopulationTracer = null;

		// Token: 0x04001A65 RID: 6757
		private static Trace categoryHeaderViewMaintenanceTracer = null;

		// Token: 0x04001A66 RID: 6758
		private static Trace categorizedViewsTracer = null;

		// Token: 0x04001A67 RID: 6759
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
