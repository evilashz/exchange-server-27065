using System;

namespace Microsoft.Exchange.Diagnostics.Components.FFOEmailUtil
{
	// Token: 0x02000404 RID: 1028
	public static class ExTraceGlobals
	{
		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x0005CDBC File Offset: 0x0005AFBC
		public static Trace CommonTracer
		{
			get
			{
				if (ExTraceGlobals.commonTracer == null)
				{
					ExTraceGlobals.commonTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.commonTracer;
			}
		}

		// Token: 0x04001D7A RID: 7546
		private static Guid componentGuid = new Guid("ada8a02a-ed68-4c8f-9269-a96fa2e4654d");

		// Token: 0x04001D7B RID: 7547
		private static Trace commonTracer = null;
	}
}
