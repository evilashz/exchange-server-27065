using System;

namespace Microsoft.Exchange.Diagnostics.Components.AnalystAlerting
{
	// Token: 0x020003D7 RID: 983
	public static class ExTraceGlobals
	{
		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x0600176F RID: 5999 RVA: 0x00059EFC File Offset: 0x000580FC
		public static Trace AnalystAlertingTracer
		{
			get
			{
				if (ExTraceGlobals.analystAlertingTracer == null)
				{
					ExTraceGlobals.analystAlertingTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.analystAlertingTracer;
			}
		}

		// Token: 0x04001C1D RID: 7197
		private static Guid componentGuid = new Guid("DB7CA5BC-B68B-46DC-89AC-85572FBD89DD");

		// Token: 0x04001C1E RID: 7198
		private static Trace analystAlertingTracer = null;
	}
}
