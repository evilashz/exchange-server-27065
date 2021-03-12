using System;

namespace Microsoft.Exchange.Diagnostics.Components.VisualizationFramework
{
	// Token: 0x020003E9 RID: 1001
	public static class ExTraceGlobals
	{
		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x0600181A RID: 6170 RVA: 0x0005B62E File Offset: 0x0005982E
		public static Trace VisualizationFrameworkTracer
		{
			get
			{
				if (ExTraceGlobals.visualizationFrameworkTracer == null)
				{
					ExTraceGlobals.visualizationFrameworkTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.visualizationFrameworkTracer;
			}
		}

		// Token: 0x04001CC8 RID: 7368
		private static Guid componentGuid = new Guid("1896490C-BFB1-473E-A9b8-40E1b86A880C");

		// Token: 0x04001CC9 RID: 7369
		private static Trace visualizationFrameworkTracer = null;
	}
}
