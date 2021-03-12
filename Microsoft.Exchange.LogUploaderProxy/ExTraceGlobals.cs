using System;
using Microsoft.Exchange.Diagnostics.Components.MessageTracingClient;

namespace Microsoft.Exchange.LogUploaderProxy
{
	// Token: 0x0200001E RID: 30
	public static class ExTraceGlobals
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x0000314D File Offset: 0x0000134D
		public static Trace ParserTracer
		{
			get
			{
				if (ExTraceGlobals.parserTracer == null)
				{
					ExTraceGlobals.parserTracer = new Trace(ExTraceGlobals.ParserTracer);
				}
				return ExTraceGlobals.parserTracer;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x0000316A File Offset: 0x0000136A
		public static Trace WriterTracer
		{
			get
			{
				if (ExTraceGlobals.writerTracer == null)
				{
					ExTraceGlobals.writerTracer = new Trace(ExTraceGlobals.WriterTracer);
				}
				return ExTraceGlobals.writerTracer;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00003187 File Offset: 0x00001387
		public static Trace ReaderTracer
		{
			get
			{
				if (ExTraceGlobals.readerTracer == null)
				{
					ExTraceGlobals.readerTracer = new Trace(ExTraceGlobals.ReaderTracer);
				}
				return ExTraceGlobals.readerTracer;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000031A4 File Offset: 0x000013A4
		public static Trace LogMonitorTracer
		{
			get
			{
				if (ExTraceGlobals.logmonitorTracer == null)
				{
					ExTraceGlobals.logmonitorTracer = new Trace(ExTraceGlobals.LogMonitorTracer);
				}
				return ExTraceGlobals.logmonitorTracer;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000031C1 File Offset: 0x000013C1
		public static Trace GeneralTracer
		{
			get
			{
				if (ExTraceGlobals.generalTracer == null)
				{
					ExTraceGlobals.generalTracer = new Trace(ExTraceGlobals.GeneralTracer);
				}
				return ExTraceGlobals.generalTracer;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000031DE File Offset: 0x000013DE
		public static Trace TransportQueueTracer
		{
			get
			{
				if (ExTraceGlobals.transportqueueTracer == null)
				{
					ExTraceGlobals.transportqueueTracer = new Trace(ExTraceGlobals.TransportQueueTracer);
				}
				return ExTraceGlobals.transportqueueTracer;
			}
		}

		// Token: 0x04000056 RID: 86
		private static Trace parserTracer;

		// Token: 0x04000057 RID: 87
		private static Trace writerTracer;

		// Token: 0x04000058 RID: 88
		private static Trace readerTracer;

		// Token: 0x04000059 RID: 89
		private static Trace logmonitorTracer;

		// Token: 0x0400005A RID: 90
		private static Trace generalTracer;

		// Token: 0x0400005B RID: 91
		private static Trace transportqueueTracer;
	}
}
