using System;

namespace Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel
{
	// Token: 0x0200037D RID: 893
	public static class ExTraceGlobals
	{
		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x000550DE File Offset: 0x000532DE
		public static Trace EventLogTracer
		{
			get
			{
				if (ExTraceGlobals.eventLogTracer == null)
				{
					ExTraceGlobals.eventLogTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.eventLogTracer;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x000550FC File Offset: 0x000532FC
		public static Trace RBACTracer
		{
			get
			{
				if (ExTraceGlobals.rBACTracer == null)
				{
					ExTraceGlobals.rBACTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.rBACTracer;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x0005511A File Offset: 0x0005331A
		public static Trace ProxyTracer
		{
			get
			{
				if (ExTraceGlobals.proxyTracer == null)
				{
					ExTraceGlobals.proxyTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.proxyTracer;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x00055138 File Offset: 0x00053338
		public static Trace RedirectTracer
		{
			get
			{
				if (ExTraceGlobals.redirectTracer == null)
				{
					ExTraceGlobals.redirectTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.redirectTracer;
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x00055156 File Offset: 0x00053356
		public static Trace WebServiceTracer
		{
			get
			{
				if (ExTraceGlobals.webServiceTracer == null)
				{
					ExTraceGlobals.webServiceTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.webServiceTracer;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x00055174 File Offset: 0x00053374
		public static Trace PerformanceTracer
		{
			get
			{
				if (ExTraceGlobals.performanceTracer == null)
				{
					ExTraceGlobals.performanceTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.performanceTracer;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001527 RID: 5415 RVA: 0x00055192 File Offset: 0x00053392
		public static Trace UserPhotosTracer
		{
			get
			{
				if (ExTraceGlobals.userPhotosTracer == null)
				{
					ExTraceGlobals.userPhotosTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.userPhotosTracer;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x000551B0 File Offset: 0x000533B0
		public static Trace DDITracer
		{
			get
			{
				if (ExTraceGlobals.dDITracer == null)
				{
					ExTraceGlobals.dDITracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.dDITracer;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x000551CE File Offset: 0x000533CE
		public static Trace LinkedInTracer
		{
			get
			{
				if (ExTraceGlobals.linkedInTracer == null)
				{
					ExTraceGlobals.linkedInTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.linkedInTracer;
			}
		}

		// Token: 0x040019CF RID: 6607
		private static Guid componentGuid = new Guid("EDD5672C-EB31-485A-9880-6E1F3BFCE4EB");

		// Token: 0x040019D0 RID: 6608
		private static Trace eventLogTracer = null;

		// Token: 0x040019D1 RID: 6609
		private static Trace rBACTracer = null;

		// Token: 0x040019D2 RID: 6610
		private static Trace proxyTracer = null;

		// Token: 0x040019D3 RID: 6611
		private static Trace redirectTracer = null;

		// Token: 0x040019D4 RID: 6612
		private static Trace webServiceTracer = null;

		// Token: 0x040019D5 RID: 6613
		private static Trace performanceTracer = null;

		// Token: 0x040019D6 RID: 6614
		private static Trace userPhotosTracer = null;

		// Token: 0x040019D7 RID: 6615
		private static Trace dDITracer = null;

		// Token: 0x040019D8 RID: 6616
		private static Trace linkedInTracer = null;
	}
}
