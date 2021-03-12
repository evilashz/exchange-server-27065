using System;

namespace Microsoft.Exchange.Diagnostics.Components.ContentFilter
{
	// Token: 0x02000380 RID: 896
	public static class ExTraceGlobals
	{
		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x00055746 File Offset: 0x00053946
		public static Trace InitializationTracer
		{
			get
			{
				if (ExTraceGlobals.initializationTracer == null)
				{
					ExTraceGlobals.initializationTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.initializationTracer;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x00055764 File Offset: 0x00053964
		public static Trace ScanMessageTracer
		{
			get
			{
				if (ExTraceGlobals.scanMessageTracer == null)
				{
					ExTraceGlobals.scanMessageTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.scanMessageTracer;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x00055782 File Offset: 0x00053982
		public static Trace BypassedSendersTracer
		{
			get
			{
				if (ExTraceGlobals.bypassedSendersTracer == null)
				{
					ExTraceGlobals.bypassedSendersTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.bypassedSendersTracer;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x000557A0 File Offset: 0x000539A0
		public static Trace ComInteropTracer
		{
			get
			{
				if (ExTraceGlobals.comInteropTracer == null)
				{
					ExTraceGlobals.comInteropTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.comInteropTracer;
			}
		}

		// Token: 0x040019FD RID: 6653
		private static Guid componentGuid = new Guid("A1FD20D2-933F-4505-A0C4-C1FBFFCB9E62");

		// Token: 0x040019FE RID: 6654
		private static Trace initializationTracer = null;

		// Token: 0x040019FF RID: 6655
		private static Trace scanMessageTracer = null;

		// Token: 0x04001A00 RID: 6656
		private static Trace bypassedSendersTracer = null;

		// Token: 0x04001A01 RID: 6657
		private static Trace comInteropTracer = null;
	}
}
