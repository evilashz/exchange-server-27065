using System;

namespace Microsoft.Exchange.Diagnostics.Components.HygieneDarScheduler
{
	// Token: 0x020003C2 RID: 962
	public static class ExTraceGlobals
	{
		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x0600171D RID: 5917 RVA: 0x000594EA File Offset: 0x000576EA
		public static Trace HygieneDarSchedulerTracer
		{
			get
			{
				if (ExTraceGlobals.hygieneDarSchedulerTracer == null)
				{
					ExTraceGlobals.hygieneDarSchedulerTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.hygieneDarSchedulerTracer;
			}
		}

		// Token: 0x04001BCB RID: 7115
		private static Guid componentGuid = new Guid("C4515B23-59C8-422C-8B14-852E8C7B3268");

		// Token: 0x04001BCC RID: 7116
		private static Trace hygieneDarSchedulerTracer = null;
	}
}
