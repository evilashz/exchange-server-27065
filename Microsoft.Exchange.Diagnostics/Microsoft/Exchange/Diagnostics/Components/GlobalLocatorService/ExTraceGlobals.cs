using System;

namespace Microsoft.Exchange.Diagnostics.Components.GlobalLocatorService
{
	// Token: 0x020003DB RID: 987
	public static class ExTraceGlobals
	{
		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x0005A03C File Offset: 0x0005823C
		public static Trace APITracer
		{
			get
			{
				if (ExTraceGlobals.aPITracer == null)
				{
					ExTraceGlobals.aPITracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.aPITracer;
			}
		}

		// Token: 0x04001C28 RID: 7208
		private static Guid componentGuid = new Guid("11E1750A-9A85-4d08-BFDB-4BCFD5BA8645");

		// Token: 0x04001C29 RID: 7209
		private static Trace aPITracer = null;
	}
}
