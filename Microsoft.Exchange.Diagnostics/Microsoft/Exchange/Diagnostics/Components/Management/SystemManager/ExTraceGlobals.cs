using System;

namespace Microsoft.Exchange.Diagnostics.Components.Management.SystemManager
{
	// Token: 0x0200037C RID: 892
	public static class ExTraceGlobals
	{
		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x0600151D RID: 5405 RVA: 0x00055061 File Offset: 0x00053261
		public static Trace ProgramFlowTracer
		{
			get
			{
				if (ExTraceGlobals.programFlowTracer == null)
				{
					ExTraceGlobals.programFlowTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.programFlowTracer;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x0005507F File Offset: 0x0005327F
		public static Trace DataFlowTracer
		{
			get
			{
				if (ExTraceGlobals.dataFlowTracer == null)
				{
					ExTraceGlobals.dataFlowTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.dataFlowTracer;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x0005509D File Offset: 0x0005329D
		public static Trace LayoutTracer
		{
			get
			{
				if (ExTraceGlobals.layoutTracer == null)
				{
					ExTraceGlobals.layoutTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.layoutTracer;
			}
		}

		// Token: 0x040019CB RID: 6603
		private static Guid componentGuid = new Guid("D92207E8-DBF2-4a93-B4F9-4931434E5F96");

		// Token: 0x040019CC RID: 6604
		private static Trace programFlowTracer = null;

		// Token: 0x040019CD RID: 6605
		private static Trace dataFlowTracer = null;

		// Token: 0x040019CE RID: 6606
		private static Trace layoutTracer = null;
	}
}
