using System;

namespace Microsoft.Exchange.Diagnostics.Components.MapiHttpHandler
{
	// Token: 0x020003A6 RID: 934
	public static class ExTraceGlobals
	{
		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x00058576 File Offset: 0x00056776
		public static Trace AsyncOperationTracer
		{
			get
			{
				if (ExTraceGlobals.asyncOperationTracer == null)
				{
					ExTraceGlobals.asyncOperationTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.asyncOperationTracer;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x00058594 File Offset: 0x00056794
		public static Trace SessionContextTracer
		{
			get
			{
				if (ExTraceGlobals.sessionContextTracer == null)
				{
					ExTraceGlobals.sessionContextTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.sessionContextTracer;
			}
		}

		// Token: 0x04001B4F RID: 6991
		private static Guid componentGuid = new Guid("F998D96B-10B7-4D4F-94FA-D1A019D62326");

		// Token: 0x04001B50 RID: 6992
		private static Trace asyncOperationTracer = null;

		// Token: 0x04001B51 RID: 6993
		private static Trace sessionContextTracer = null;
	}
}
