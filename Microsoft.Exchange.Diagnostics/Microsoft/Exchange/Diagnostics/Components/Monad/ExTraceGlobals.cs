using System;

namespace Microsoft.Exchange.Diagnostics.Components.Monad
{
	// Token: 0x0200038D RID: 909
	public static class ExTraceGlobals
	{
		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06001593 RID: 5523 RVA: 0x00055FF7 File Offset: 0x000541F7
		public static Trace DefaultTracer
		{
			get
			{
				if (ExTraceGlobals.defaultTracer == null)
				{
					ExTraceGlobals.defaultTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.defaultTracer;
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06001594 RID: 5524 RVA: 0x00056015 File Offset: 0x00054215
		public static Trace IntegrationTracer
		{
			get
			{
				if (ExTraceGlobals.integrationTracer == null)
				{
					ExTraceGlobals.integrationTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.integrationTracer;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06001595 RID: 5525 RVA: 0x00056033 File Offset: 0x00054233
		public static Trace VerboseTracer
		{
			get
			{
				if (ExTraceGlobals.verboseTracer == null)
				{
					ExTraceGlobals.verboseTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.verboseTracer;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x00056051 File Offset: 0x00054251
		public static Trace DataTracer
		{
			get
			{
				if (ExTraceGlobals.dataTracer == null)
				{
					ExTraceGlobals.dataTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.dataTracer;
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06001597 RID: 5527 RVA: 0x0005606F File Offset: 0x0005426F
		public static Trace HostTracer
		{
			get
			{
				if (ExTraceGlobals.hostTracer == null)
				{
					ExTraceGlobals.hostTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.hostTracer;
			}
		}

		// Token: 0x04001A41 RID: 6721
		private static Guid componentGuid = new Guid("b47bd400-78af-479f-aeff-39d4d6c54559");

		// Token: 0x04001A42 RID: 6722
		private static Trace defaultTracer = null;

		// Token: 0x04001A43 RID: 6723
		private static Trace integrationTracer = null;

		// Token: 0x04001A44 RID: 6724
		private static Trace verboseTracer = null;

		// Token: 0x04001A45 RID: 6725
		private static Trace dataTracer = null;

		// Token: 0x04001A46 RID: 6726
		private static Trace hostTracer = null;
	}
}
