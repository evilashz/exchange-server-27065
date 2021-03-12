using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.PopImap.Core
{
	// Token: 0x02000331 RID: 817
	public static class ExTraceGlobals
	{
		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x0004DA5F File Offset: 0x0004BC5F
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

		// Token: 0x04001671 RID: 5745
		private static Guid componentGuid = new Guid("EFEA6219-825A-4d56-9B26-7393EF24D917");

		// Token: 0x04001672 RID: 5746
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
