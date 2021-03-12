using System;

namespace Microsoft.Exchange.Diagnostics.Components.ConnectionFiltering
{
	// Token: 0x02000381 RID: 897
	public static class ExTraceGlobals
	{
		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x000557E7 File Offset: 0x000539E7
		public static Trace ErrorTracer
		{
			get
			{
				if (ExTraceGlobals.errorTracer == null)
				{
					ExTraceGlobals.errorTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.errorTracer;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001555 RID: 5461 RVA: 0x00055805 File Offset: 0x00053A05
		public static Trace FactoryTracer
		{
			get
			{
				if (ExTraceGlobals.factoryTracer == null)
				{
					ExTraceGlobals.factoryTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.factoryTracer;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x00055823 File Offset: 0x00053A23
		public static Trace OnConnectTracer
		{
			get
			{
				if (ExTraceGlobals.onConnectTracer == null)
				{
					ExTraceGlobals.onConnectTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.onConnectTracer;
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x00055841 File Offset: 0x00053A41
		public static Trace OnMailFromTracer
		{
			get
			{
				if (ExTraceGlobals.onMailFromTracer == null)
				{
					ExTraceGlobals.onMailFromTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.onMailFromTracer;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x0005585F File Offset: 0x00053A5F
		public static Trace OnRcptToTracer
		{
			get
			{
				if (ExTraceGlobals.onRcptToTracer == null)
				{
					ExTraceGlobals.onRcptToTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.onRcptToTracer;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x0005587D File Offset: 0x00053A7D
		public static Trace OnEOHTracer
		{
			get
			{
				if (ExTraceGlobals.onEOHTracer == null)
				{
					ExTraceGlobals.onEOHTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.onEOHTracer;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x0600155A RID: 5466 RVA: 0x0005589B File Offset: 0x00053A9B
		public static Trace DNSTracer
		{
			get
			{
				if (ExTraceGlobals.dNSTracer == null)
				{
					ExTraceGlobals.dNSTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.dNSTracer;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x000558B9 File Offset: 0x00053AB9
		public static Trace IPAllowDenyTracer
		{
			get
			{
				if (ExTraceGlobals.iPAllowDenyTracer == null)
				{
					ExTraceGlobals.iPAllowDenyTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.iPAllowDenyTracer;
			}
		}

		// Token: 0x04001A02 RID: 6658
		private static Guid componentGuid = new Guid("F0A7EB4B-2EE5-478f-A589-5273CAC4E5EE");

		// Token: 0x04001A03 RID: 6659
		private static Trace errorTracer = null;

		// Token: 0x04001A04 RID: 6660
		private static Trace factoryTracer = null;

		// Token: 0x04001A05 RID: 6661
		private static Trace onConnectTracer = null;

		// Token: 0x04001A06 RID: 6662
		private static Trace onMailFromTracer = null;

		// Token: 0x04001A07 RID: 6663
		private static Trace onRcptToTracer = null;

		// Token: 0x04001A08 RID: 6664
		private static Trace onEOHTracer = null;

		// Token: 0x04001A09 RID: 6665
		private static Trace dNSTracer = null;

		// Token: 0x04001A0A RID: 6666
		private static Trace iPAllowDenyTracer = null;
	}
}
