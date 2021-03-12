using System;

namespace Microsoft.Exchange.Diagnostics.Components.E4E
{
	// Token: 0x02000338 RID: 824
	public static class ExTraceGlobals
	{
		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x0004FD74 File Offset: 0x0004DF74
		public static Trace CoreTracer
		{
			get
			{
				if (ExTraceGlobals.coreTracer == null)
				{
					ExTraceGlobals.coreTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.coreTracer;
			}
		}

		// Token: 0x04001766 RID: 5990
		private static Guid componentGuid = new Guid("13E154E9-F834-4B1C-8FD7-81AA1B37F6AE");

		// Token: 0x04001767 RID: 5991
		private static Trace coreTracer = null;
	}
}
