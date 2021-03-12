using System;

namespace Microsoft.Exchange.Diagnostics.Components.OfficeGraph
{
	// Token: 0x02000364 RID: 868
	public static class ExTraceGlobals
	{
		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x00053AD4 File Offset: 0x00051CD4
		public static Trace OfficeGraphAgentTracer
		{
			get
			{
				if (ExTraceGlobals.officeGraphAgentTracer == null)
				{
					ExTraceGlobals.officeGraphAgentTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.officeGraphAgentTracer;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x00053AF2 File Offset: 0x00051CF2
		public static Trace OfficeGraphWriterTracer
		{
			get
			{
				if (ExTraceGlobals.officeGraphWriterTracer == null)
				{
					ExTraceGlobals.officeGraphWriterTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.officeGraphWriterTracer;
			}
		}

		// Token: 0x04001928 RID: 6440
		private static Guid componentGuid = new Guid("2F0CCE12-0EF1-4AA2-808D-7827E3E21306");

		// Token: 0x04001929 RID: 6441
		private static Trace officeGraphAgentTracer = null;

		// Token: 0x0400192A RID: 6442
		private static Trace officeGraphWriterTracer = null;
	}
}
