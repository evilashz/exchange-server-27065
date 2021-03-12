using System;

namespace Microsoft.Exchange.Diagnostics.Components.WorkingSet
{
	// Token: 0x02000363 RID: 867
	public static class ExTraceGlobals
	{
		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x00053A7B File Offset: 0x00051C7B
		public static Trace WorkingSetAgentTracer
		{
			get
			{
				if (ExTraceGlobals.workingSetAgentTracer == null)
				{
					ExTraceGlobals.workingSetAgentTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.workingSetAgentTracer;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x00053A99 File Offset: 0x00051C99
		public static Trace WorkingSetPublisherTracer
		{
			get
			{
				if (ExTraceGlobals.workingSetPublisherTracer == null)
				{
					ExTraceGlobals.workingSetPublisherTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.workingSetPublisherTracer;
			}
		}

		// Token: 0x04001925 RID: 6437
		private static Guid componentGuid = new Guid("144FC985-645D-4889-97CB-841A82F498F3");

		// Token: 0x04001926 RID: 6438
		private static Trace workingSetAgentTracer = null;

		// Token: 0x04001927 RID: 6439
		private static Trace workingSetPublisherTracer = null;
	}
}
