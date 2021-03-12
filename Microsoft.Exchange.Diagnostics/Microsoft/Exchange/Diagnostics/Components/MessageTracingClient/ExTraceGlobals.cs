using System;

namespace Microsoft.Exchange.Diagnostics.Components.MessageTracingClient
{
	// Token: 0x020003C8 RID: 968
	public static class ExTraceGlobals
	{
		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x0600173E RID: 5950 RVA: 0x0005992A File Offset: 0x00057B2A
		public static Trace ParserTracer
		{
			get
			{
				if (ExTraceGlobals.parserTracer == null)
				{
					ExTraceGlobals.parserTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.parserTracer;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x0600173F RID: 5951 RVA: 0x00059948 File Offset: 0x00057B48
		public static Trace WriterTracer
		{
			get
			{
				if (ExTraceGlobals.writerTracer == null)
				{
					ExTraceGlobals.writerTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.writerTracer;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06001740 RID: 5952 RVA: 0x00059966 File Offset: 0x00057B66
		public static Trace ReaderTracer
		{
			get
			{
				if (ExTraceGlobals.readerTracer == null)
				{
					ExTraceGlobals.readerTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.readerTracer;
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06001741 RID: 5953 RVA: 0x00059984 File Offset: 0x00057B84
		public static Trace LogMonitorTracer
		{
			get
			{
				if (ExTraceGlobals.logMonitorTracer == null)
				{
					ExTraceGlobals.logMonitorTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.logMonitorTracer;
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x000599A2 File Offset: 0x00057BA2
		public static Trace GeneralTracer
		{
			get
			{
				if (ExTraceGlobals.generalTracer == null)
				{
					ExTraceGlobals.generalTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.generalTracer;
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06001743 RID: 5955 RVA: 0x000599C0 File Offset: 0x00057BC0
		public static Trace TransportQueueTracer
		{
			get
			{
				if (ExTraceGlobals.transportQueueTracer == null)
				{
					ExTraceGlobals.transportQueueTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.transportQueueTracer;
			}
		}

		// Token: 0x04001BEC RID: 7148
		private static Guid componentGuid = new Guid("0402AB9A-3D53-4353-AC55-9A9491E5A22A");

		// Token: 0x04001BED RID: 7149
		private static Trace parserTracer = null;

		// Token: 0x04001BEE RID: 7150
		private static Trace writerTracer = null;

		// Token: 0x04001BEF RID: 7151
		private static Trace readerTracer = null;

		// Token: 0x04001BF0 RID: 7152
		private static Trace logMonitorTracer = null;

		// Token: 0x04001BF1 RID: 7153
		private static Trace generalTracer = null;

		// Token: 0x04001BF2 RID: 7154
		private static Trace transportQueueTracer = null;
	}
}
