using System;

namespace Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.DirectoryProcessorAssistant.GroupMetricsGenerator
{
	// Token: 0x02000357 RID: 855
	public static class ExTraceGlobals
	{
		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x000522EF File Offset: 0x000504EF
		public static Trace GroupMetricsTracer
		{
			get
			{
				if (ExTraceGlobals.groupMetricsTracer == null)
				{
					ExTraceGlobals.groupMetricsTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.groupMetricsTracer;
			}
		}

		// Token: 0x0400187B RID: 6267
		private static Guid componentGuid = new Guid("a968ca69-504c-4430-bef4-84f9480d8937");

		// Token: 0x0400187C RID: 6268
		private static Trace groupMetricsTracer = null;
	}
}
