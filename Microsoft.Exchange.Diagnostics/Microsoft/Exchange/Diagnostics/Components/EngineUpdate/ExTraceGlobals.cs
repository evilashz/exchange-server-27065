using System;

namespace Microsoft.Exchange.Diagnostics.Components.EngineUpdate
{
	// Token: 0x020003E5 RID: 997
	public static class ExTraceGlobals
	{
		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x0005B4CA File Offset: 0x000596CA
		public static Trace LoggingLibTracer
		{
			get
			{
				if (ExTraceGlobals.loggingLibTracer == null)
				{
					ExTraceGlobals.loggingLibTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.loggingLibTracer;
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x0005B4E8 File Offset: 0x000596E8
		public static Trace LoggerTracer
		{
			get
			{
				if (ExTraceGlobals.loggerTracer == null)
				{
					ExTraceGlobals.loggerTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.loggerTracer;
			}
		}

		// Token: 0x04001CBC RID: 7356
		private static Guid componentGuid = new Guid("4A97B20E-92F6-4960-8273-E069E7785615");

		// Token: 0x04001CBD RID: 7357
		private static Trace loggingLibTracer = null;

		// Token: 0x04001CBE RID: 7358
		private static Trace loggerTracer = null;
	}
}
