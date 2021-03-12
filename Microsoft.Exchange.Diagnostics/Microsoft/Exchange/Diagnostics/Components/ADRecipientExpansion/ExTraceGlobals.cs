using System;

namespace Microsoft.Exchange.Diagnostics.Components.ADRecipientExpansion
{
	// Token: 0x02000325 RID: 805
	public static class ExTraceGlobals
	{
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001119 RID: 4377 RVA: 0x0004C2AD File Offset: 0x0004A4AD
		public static Trace ADExpansionTracer
		{
			get
			{
				if (ExTraceGlobals.aDExpansionTracer == null)
				{
					ExTraceGlobals.aDExpansionTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.aDExpansionTracer;
			}
		}

		// Token: 0x040015C7 RID: 5575
		private static Guid componentGuid = new Guid("9e0cc833-7761-49ac-80cc-e2b9cf4d5b94");

		// Token: 0x040015C8 RID: 5576
		private static Trace aDExpansionTracer = null;
	}
}
