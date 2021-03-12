using System;

namespace Microsoft.Exchange.Diagnostics.Components.StreamingOpticsHost
{
	// Token: 0x020003FB RID: 1019
	public static class ExTraceGlobals
	{
		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06001871 RID: 6257 RVA: 0x0005C143 File Offset: 0x0005A343
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

		// Token: 0x04001D1F RID: 7455
		private static Guid componentGuid = new Guid("05238676-6E91-4730-B8B6-2D891A4A0E85");

		// Token: 0x04001D20 RID: 7456
		private static Trace generalTracer = null;
	}
}
