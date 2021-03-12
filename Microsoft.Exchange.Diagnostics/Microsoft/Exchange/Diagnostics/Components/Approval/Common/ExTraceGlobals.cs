using System;

namespace Microsoft.Exchange.Diagnostics.Components.Approval.Common
{
	// Token: 0x0200035D RID: 861
	public static class ExTraceGlobals
	{
		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x000525B9 File Offset: 0x000507B9
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

		// Token: 0x04001892 RID: 6290
		private static Guid componentGuid = new Guid("DEFD64F3-201F-4cf5-A1A4-B949C647C287");

		// Token: 0x04001893 RID: 6291
		private static Trace generalTracer = null;
	}
}
