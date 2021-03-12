using System;

namespace Microsoft.Exchange.Diagnostics.Components.GlobalLocatorCache
{
	// Token: 0x020003A4 RID: 932
	public static class ExTraceGlobals
	{
		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x0005816B File Offset: 0x0005636B
		public static Trace ServiceTracer
		{
			get
			{
				if (ExTraceGlobals.serviceTracer == null)
				{
					ExTraceGlobals.serviceTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.serviceTracer;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x00058189 File Offset: 0x00056389
		public static Trace ClientTracer
		{
			get
			{
				if (ExTraceGlobals.clientTracer == null)
				{
					ExTraceGlobals.clientTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.clientTracer;
			}
		}

		// Token: 0x04001B32 RID: 6962
		private static Guid componentGuid = new Guid("24319d41-f580-49c1-82dc-045116f009f1");

		// Token: 0x04001B33 RID: 6963
		private static Trace serviceTracer = null;

		// Token: 0x04001B34 RID: 6964
		private static Trace clientTracer = null;
	}
}
