using System;

namespace Microsoft.Exchange.Diagnostics.Components.ServiceHost.ServiceAccount
{
	// Token: 0x020003B7 RID: 951
	public static class ExTraceGlobals
	{
		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x00058DC3 File Offset: 0x00056FC3
		public static Trace GeneralTracer
		{
			get
			{
				if (ExTraceGlobals.generalTracer == null)
				{
					ExTraceGlobals.generalTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.generalTracer;
			}
		}

		// Token: 0x04001B93 RID: 7059
		private static Guid componentGuid = new Guid("76986af5-10f0-40d2-aac4-62e85132c65a");

		// Token: 0x04001B94 RID: 7060
		private static Trace generalTracer = null;
	}
}
