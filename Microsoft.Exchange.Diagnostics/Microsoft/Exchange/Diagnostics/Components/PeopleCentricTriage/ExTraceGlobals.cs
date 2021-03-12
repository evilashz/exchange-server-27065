using System;

namespace Microsoft.Exchange.Diagnostics.Components.PeopleCentricTriage
{
	// Token: 0x02000330 RID: 816
	public static class ExTraceGlobals
	{
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x0004DA2A File Offset: 0x0004BC2A
		public static Trace AssistantTracer
		{
			get
			{
				if (ExTraceGlobals.assistantTracer == null)
				{
					ExTraceGlobals.assistantTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.assistantTracer;
			}
		}

		// Token: 0x0400166F RID: 5743
		private static Guid componentGuid = new Guid("4D9E2E62-6125-4C65-9C1D-ACDF3EE2E42E");

		// Token: 0x04001670 RID: 5744
		private static Trace assistantTracer = null;
	}
}
