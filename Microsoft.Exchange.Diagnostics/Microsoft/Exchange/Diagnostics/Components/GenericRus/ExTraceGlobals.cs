using System;

namespace Microsoft.Exchange.Diagnostics.Components.GenericRus
{
	// Token: 0x020003F2 RID: 1010
	public static class ExTraceGlobals
	{
		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06001851 RID: 6225 RVA: 0x0005BD6E File Offset: 0x00059F6E
		public static Trace CommonTracer
		{
			get
			{
				if (ExTraceGlobals.commonTracer == null)
				{
					ExTraceGlobals.commonTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.commonTracer;
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06001852 RID: 6226 RVA: 0x0005BD8C File Offset: 0x00059F8C
		public static Trace RusClientTracer
		{
			get
			{
				if (ExTraceGlobals.rusClientTracer == null)
				{
					ExTraceGlobals.rusClientTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.rusClientTracer;
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06001853 RID: 6227 RVA: 0x0005BDAA File Offset: 0x00059FAA
		public static Trace RusServiceTracer
		{
			get
			{
				if (ExTraceGlobals.rusServiceTracer == null)
				{
					ExTraceGlobals.rusServiceTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.rusServiceTracer;
			}
		}

		// Token: 0x04001CFF RID: 7423
		private static Guid componentGuid = new Guid("F6193284-D059-4D1B-AB4B-C2A778A8BAB9");

		// Token: 0x04001D00 RID: 7424
		private static Trace commonTracer = null;

		// Token: 0x04001D01 RID: 7425
		private static Trace rusClientTracer = null;

		// Token: 0x04001D02 RID: 7426
		private static Trace rusServiceTracer = null;
	}
}
