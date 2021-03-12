using System;

namespace Microsoft.Exchange.Diagnostics.Components.IPUriPuller
{
	// Token: 0x020003DF RID: 991
	public static class ExTraceGlobals
	{
		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x0600178A RID: 6026 RVA: 0x0005A230 File Offset: 0x00058430
		public static Trace PullerTracer
		{
			get
			{
				if (ExTraceGlobals.pullerTracer == null)
				{
					ExTraceGlobals.pullerTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.pullerTracer;
			}
		}

		// Token: 0x04001C38 RID: 7224
		private static Guid componentGuid = new Guid("D51325F9-4448-46B6-A151-148E015B1831");

		// Token: 0x04001C39 RID: 7225
		private static Trace pullerTracer = null;
	}
}
