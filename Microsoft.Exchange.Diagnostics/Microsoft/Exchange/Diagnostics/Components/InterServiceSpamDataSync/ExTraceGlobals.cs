using System;

namespace Microsoft.Exchange.Diagnostics.Components.InterServiceSpamDataSync
{
	// Token: 0x020003D6 RID: 982
	public static class ExTraceGlobals
	{
		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x0600176D RID: 5997 RVA: 0x00059EC7 File Offset: 0x000580C7
		public static Trace InterServiceSpamDataSyncTracer
		{
			get
			{
				if (ExTraceGlobals.interServiceSpamDataSyncTracer == null)
				{
					ExTraceGlobals.interServiceSpamDataSyncTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.interServiceSpamDataSyncTracer;
			}
		}

		// Token: 0x04001C1B RID: 7195
		private static Guid componentGuid = new Guid("D9830C82-1661-41A3-8F68-674D885D055E");

		// Token: 0x04001C1C RID: 7196
		private static Trace interServiceSpamDataSyncTracer = null;
	}
}
