using System;

namespace Microsoft.Exchange.Diagnostics.Components.FfoSelfRecoveryFx
{
	// Token: 0x020003F0 RID: 1008
	public static class ExTraceGlobals
	{
		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06001843 RID: 6211 RVA: 0x0005BB8E File Offset: 0x00059D8E
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

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x0005BBAC File Offset: 0x00059DAC
		public static Trace RAAServiceTracer
		{
			get
			{
				if (ExTraceGlobals.rAAServiceTracer == null)
				{
					ExTraceGlobals.rAAServiceTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.rAAServiceTracer;
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06001845 RID: 6213 RVA: 0x0005BBCA File Offset: 0x00059DCA
		public static Trace RAANetworkTracer
		{
			get
			{
				if (ExTraceGlobals.rAANetworkTracer == null)
				{
					ExTraceGlobals.rAANetworkTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.rAANetworkTracer;
			}
		}

		// Token: 0x04001CF1 RID: 7409
		private static Guid componentGuid = new Guid("2D55856F-B3DB-4318-9CB2-6B8921CEBFCB");

		// Token: 0x04001CF2 RID: 7410
		private static Trace commonTracer = null;

		// Token: 0x04001CF3 RID: 7411
		private static Trace rAAServiceTracer = null;

		// Token: 0x04001CF4 RID: 7412
		private static Trace rAANetworkTracer = null;
	}
}
