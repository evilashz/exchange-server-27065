using System;

namespace Microsoft.Exchange.Diagnostics.Components.Cluster.DagManagement
{
	// Token: 0x02000379 RID: 889
	public static class ExTraceGlobals
	{
		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x00054DBB File Offset: 0x00052FBB
		public static Trace ServiceTracer
		{
			get
			{
				if (ExTraceGlobals.serviceTracer == null)
				{
					ExTraceGlobals.serviceTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.serviceTracer;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x00054DD9 File Offset: 0x00052FD9
		public static Trace DatabaseHealthTrackerTracer
		{
			get
			{
				if (ExTraceGlobals.databaseHealthTrackerTracer == null)
				{
					ExTraceGlobals.databaseHealthTrackerTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.databaseHealthTrackerTracer;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x00054DF7 File Offset: 0x00052FF7
		public static Trace MonitoringWcfServiceTracer
		{
			get
			{
				if (ExTraceGlobals.monitoringWcfServiceTracer == null)
				{
					ExTraceGlobals.monitoringWcfServiceTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.monitoringWcfServiceTracer;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x00054E15 File Offset: 0x00053015
		public static Trace MonitoringWcfClientTracer
		{
			get
			{
				if (ExTraceGlobals.monitoringWcfClientTracer == null)
				{
					ExTraceGlobals.monitoringWcfClientTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.monitoringWcfClientTracer;
			}
		}

		// Token: 0x040019B7 RID: 6583
		private static Guid componentGuid = new Guid("3ce77de7-c264-4d85-96f6-d0c3b66d4a4b");

		// Token: 0x040019B8 RID: 6584
		private static Trace serviceTracer = null;

		// Token: 0x040019B9 RID: 6585
		private static Trace databaseHealthTrackerTracer = null;

		// Token: 0x040019BA RID: 6586
		private static Trace monitoringWcfServiceTracer = null;

		// Token: 0x040019BB RID: 6587
		private static Trace monitoringWcfClientTracer = null;
	}
}
