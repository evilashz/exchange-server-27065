using System;

namespace Microsoft.Exchange.Diagnostics.Components.Authentication
{
	// Token: 0x0200038C RID: 908
	public static class ExTraceGlobals
	{
		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x00055FC2 File Offset: 0x000541C2
		public static Trace DefaultTracer
		{
			get
			{
				if (ExTraceGlobals.defaultTracer == null)
				{
					ExTraceGlobals.defaultTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.defaultTracer;
			}
		}

		// Token: 0x04001A3F RID: 6719
		private static Guid componentGuid = new Guid("d7ac17d0-dee6-44c6-96e5-bdb65dd8efa3");

		// Token: 0x04001A40 RID: 6720
		private static Trace defaultTracer = null;
	}
}
