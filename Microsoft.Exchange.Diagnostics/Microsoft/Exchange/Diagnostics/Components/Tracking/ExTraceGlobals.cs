using System;

namespace Microsoft.Exchange.Diagnostics.Components.Tracking
{
	// Token: 0x020003A2 RID: 930
	public static class ExTraceGlobals
	{
		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x00057FBD File Offset: 0x000561BD
		public static Trace TaskTracer
		{
			get
			{
				if (ExTraceGlobals.taskTracer == null)
				{
					ExTraceGlobals.taskTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.taskTracer;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06001678 RID: 5752 RVA: 0x00057FDB File Offset: 0x000561DB
		public static Trace ServerStatusTracer
		{
			get
			{
				if (ExTraceGlobals.serverStatusTracer == null)
				{
					ExTraceGlobals.serverStatusTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.serverStatusTracer;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x00057FF9 File Offset: 0x000561F9
		public static Trace LogAnalysisTracer
		{
			get
			{
				if (ExTraceGlobals.logAnalysisTracer == null)
				{
					ExTraceGlobals.logAnalysisTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.logAnalysisTracer;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x0600167A RID: 5754 RVA: 0x00058017 File Offset: 0x00056217
		public static Trace SearchLibraryTracer
		{
			get
			{
				if (ExTraceGlobals.searchLibraryTracer == null)
				{
					ExTraceGlobals.searchLibraryTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.searchLibraryTracer;
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x00058035 File Offset: 0x00056235
		public static Trace WebServiceTracer
		{
			get
			{
				if (ExTraceGlobals.webServiceTracer == null)
				{
					ExTraceGlobals.webServiceTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.webServiceTracer;
			}
		}

		// Token: 0x04001B25 RID: 6949
		private static Guid componentGuid = new Guid("0B7BA732-EF67-4e7c-A68F-3D8593D9DC06");

		// Token: 0x04001B26 RID: 6950
		private static Trace taskTracer = null;

		// Token: 0x04001B27 RID: 6951
		private static Trace serverStatusTracer = null;

		// Token: 0x04001B28 RID: 6952
		private static Trace logAnalysisTracer = null;

		// Token: 0x04001B29 RID: 6953
		private static Trace searchLibraryTracer = null;

		// Token: 0x04001B2A RID: 6954
		private static Trace webServiceTracer = null;
	}
}
