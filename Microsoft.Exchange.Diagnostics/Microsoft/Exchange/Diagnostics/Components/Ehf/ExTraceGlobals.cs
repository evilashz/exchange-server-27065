using System;

namespace Microsoft.Exchange.Diagnostics.Components.Ehf
{
	// Token: 0x0200031E RID: 798
	public static class ExTraceGlobals
	{
		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001106 RID: 4358 RVA: 0x0004C086 File Offset: 0x0004A286
		public static Trace ProviderTracer
		{
			get
			{
				if (ExTraceGlobals.providerTracer == null)
				{
					ExTraceGlobals.providerTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.providerTracer;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001107 RID: 4359 RVA: 0x0004C0A4 File Offset: 0x0004A2A4
		public static Trace TargetConnectionTracer
		{
			get
			{
				if (ExTraceGlobals.targetConnectionTracer == null)
				{
					ExTraceGlobals.targetConnectionTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.targetConnectionTracer;
			}
		}

		// Token: 0x040015B4 RID: 5556
		private static Guid componentGuid = new Guid("0b50faa8-d032-4a04-b40e-d774edf00c31");

		// Token: 0x040015B5 RID: 5557
		private static Trace providerTracer = null;

		// Token: 0x040015B6 RID: 5558
		private static Trace targetConnectionTracer = null;
	}
}
