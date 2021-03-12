using System;

namespace Microsoft.Exchange.Diagnostics.Components.FFODataPumper
{
	// Token: 0x020003EB RID: 1003
	public static class ExTraceGlobals
	{
		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x06001826 RID: 6182 RVA: 0x0005B7C6 File Offset: 0x000599C6
		public static Trace FFODataPumperTracer
		{
			get
			{
				if (ExTraceGlobals.fFODataPumperTracer == null)
				{
					ExTraceGlobals.fFODataPumperTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.fFODataPumperTracer;
			}
		}

		// Token: 0x04001CD4 RID: 7380
		private static Guid componentGuid = new Guid("38489AA6-6BD4-448b-9FF9-7BFA6B80FC2B");

		// Token: 0x04001CD5 RID: 7381
		private static Trace fFODataPumperTracer = null;
	}
}
