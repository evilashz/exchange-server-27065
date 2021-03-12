using System;

namespace Microsoft.Exchange.Diagnostics.Components.Sts
{
	// Token: 0x02000387 RID: 903
	public static class ExTraceGlobals
	{
		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x00055C49 File Offset: 0x00053E49
		public static Trace DatabaseTracer
		{
			get
			{
				if (ExTraceGlobals.databaseTracer == null)
				{
					ExTraceGlobals.databaseTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.databaseTracer;
			}
		}

		// Token: 0x04001A24 RID: 6692
		private static Guid componentGuid = new Guid("DEB97D6B-83F3-4002-8295-6BD0A2F71F18");

		// Token: 0x04001A25 RID: 6693
		private static Trace databaseTracer = null;
	}
}
