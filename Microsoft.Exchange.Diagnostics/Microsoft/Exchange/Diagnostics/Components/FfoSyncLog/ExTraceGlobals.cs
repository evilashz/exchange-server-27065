using System;

namespace Microsoft.Exchange.Diagnostics.Components.FfoSyncLog
{
	// Token: 0x020003F5 RID: 1013
	public static class ExTraceGlobals
	{
		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x0600185C RID: 6236 RVA: 0x0005BEC1 File Offset: 0x0005A0C1
		public static Trace LogGenTracer
		{
			get
			{
				if (ExTraceGlobals.logGenTracer == null)
				{
					ExTraceGlobals.logGenTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.logGenTracer;
			}
		}

		// Token: 0x04001D0A RID: 7434
		private static Guid componentGuid = new Guid("D277223A-D26E-49cc-B5AD-2446D3B89DF1");

		// Token: 0x04001D0B RID: 7435
		private static Trace logGenTracer = null;
	}
}
