using System;

namespace Microsoft.Exchange.Diagnostics.Components.OABAuth
{
	// Token: 0x020003AC RID: 940
	public static class ExTraceGlobals
	{
		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x000588F4 File Offset: 0x00056AF4
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

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x060016BE RID: 5822 RVA: 0x00058912 File Offset: 0x00056B12
		public static Trace RPCTracer
		{
			get
			{
				if (ExTraceGlobals.rPCTracer == null)
				{
					ExTraceGlobals.rPCTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.rPCTracer;
			}
		}

		// Token: 0x04001B6B RID: 7019
		private static Guid componentGuid = new Guid("a38f8e7a-27d6-4fee-a5a6-56c225bbd889");

		// Token: 0x04001B6C RID: 7020
		private static Trace generalTracer = null;

		// Token: 0x04001B6D RID: 7021
		private static Trace rPCTracer = null;
	}
}
