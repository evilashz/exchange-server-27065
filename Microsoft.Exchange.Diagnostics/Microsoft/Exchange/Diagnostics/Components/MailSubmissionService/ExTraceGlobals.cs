using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.MailSubmissionService
{
	// Token: 0x020003BD RID: 957
	public static class ExTraceGlobals
	{
		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x0005912A File Offset: 0x0005732A
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x04001BAE RID: 7086
		private static Guid componentGuid = new Guid("{2C5100F4-4C95-499d-8388-F3F682460E81}");

		// Token: 0x04001BAF RID: 7087
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
