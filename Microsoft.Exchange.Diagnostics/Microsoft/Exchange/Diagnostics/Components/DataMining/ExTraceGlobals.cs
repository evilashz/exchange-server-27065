using System;

namespace Microsoft.Exchange.Diagnostics.Components.DataMining
{
	// Token: 0x020003BE RID: 958
	public static class ExTraceGlobals
	{
		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06001702 RID: 5890 RVA: 0x0005915F File Offset: 0x0005735F
		public static Trace EventsTracer
		{
			get
			{
				if (ExTraceGlobals.eventsTracer == null)
				{
					ExTraceGlobals.eventsTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.eventsTracer;
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x0005917D File Offset: 0x0005737D
		public static Trace GeneralTracer
		{
			get
			{
				if (ExTraceGlobals.generalTracer == null)
				{
					ExTraceGlobals.generalTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.generalTracer;
			}
		}

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x0005919B File Offset: 0x0005739B
		public static Trace ConfigurationTracer
		{
			get
			{
				if (ExTraceGlobals.configurationTracer == null)
				{
					ExTraceGlobals.configurationTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.configurationTracer;
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x000591B9 File Offset: 0x000573B9
		public static Trace ConfigurationServiceTracer
		{
			get
			{
				if (ExTraceGlobals.configurationServiceTracer == null)
				{
					ExTraceGlobals.configurationServiceTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.configurationServiceTracer;
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x000591D7 File Offset: 0x000573D7
		public static Trace SchedulerTracer
		{
			get
			{
				if (ExTraceGlobals.schedulerTracer == null)
				{
					ExTraceGlobals.schedulerTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.schedulerTracer;
			}
		}

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x000591F5 File Offset: 0x000573F5
		public static Trace PumperTracer
		{
			get
			{
				if (ExTraceGlobals.pumperTracer == null)
				{
					ExTraceGlobals.pumperTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.pumperTracer;
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06001708 RID: 5896 RVA: 0x00059213 File Offset: 0x00057413
		public static Trace UploaderTracer
		{
			get
			{
				if (ExTraceGlobals.uploaderTracer == null)
				{
					ExTraceGlobals.uploaderTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.uploaderTracer;
			}
		}

		// Token: 0x04001BB0 RID: 7088
		private static Guid componentGuid = new Guid("{54300D03-CEA2-43CB-9522-2F6B1CD5DAA4}");

		// Token: 0x04001BB1 RID: 7089
		private static Trace eventsTracer = null;

		// Token: 0x04001BB2 RID: 7090
		private static Trace generalTracer = null;

		// Token: 0x04001BB3 RID: 7091
		private static Trace configurationTracer = null;

		// Token: 0x04001BB4 RID: 7092
		private static Trace configurationServiceTracer = null;

		// Token: 0x04001BB5 RID: 7093
		private static Trace schedulerTracer = null;

		// Token: 0x04001BB6 RID: 7094
		private static Trace pumperTracer = null;

		// Token: 0x04001BB7 RID: 7095
		private static Trace uploaderTracer = null;
	}
}
