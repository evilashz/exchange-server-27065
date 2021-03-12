using System;

namespace Microsoft.Exchange.Diagnostics.Components.ServiceLocator
{
	// Token: 0x020003C7 RID: 967
	public static class ExTraceGlobals
	{
		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x0600173A RID: 5946 RVA: 0x000598AD File Offset: 0x00057AAD
		public static Trace FfoDnsServerCommonTracer
		{
			get
			{
				if (ExTraceGlobals.ffoDnsServerCommonTracer == null)
				{
					ExTraceGlobals.ffoDnsServerCommonTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.ffoDnsServerCommonTracer;
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x000598CB File Offset: 0x00057ACB
		public static Trace FfoDnsServerTracer
		{
			get
			{
				if (ExTraceGlobals.ffoDnsServerTracer == null)
				{
					ExTraceGlobals.ffoDnsServerTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.ffoDnsServerTracer;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x0600173C RID: 5948 RVA: 0x000598E9 File Offset: 0x00057AE9
		public static Trace FfoDnsServerDBPlugInTracer
		{
			get
			{
				if (ExTraceGlobals.ffoDnsServerDBPlugInTracer == null)
				{
					ExTraceGlobals.ffoDnsServerDBPlugInTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.ffoDnsServerDBPlugInTracer;
			}
		}

		// Token: 0x04001BE8 RID: 7144
		private static Guid componentGuid = new Guid("9CCAE37E-338A-403B-9EBB-2636514DEE9C");

		// Token: 0x04001BE9 RID: 7145
		private static Trace ffoDnsServerCommonTracer = null;

		// Token: 0x04001BEA RID: 7146
		private static Trace ffoDnsServerTracer = null;

		// Token: 0x04001BEB RID: 7147
		private static Trace ffoDnsServerDBPlugInTracer = null;
	}
}
