using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.MessagingPolicies.TransportRuleAgent
{
	// Token: 0x0200032D RID: 813
	public static class ExTraceGlobals
	{
		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x0004CEAC File Offset: 0x0004B0AC
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

		// Token: 0x0400161E RID: 5662
		private static Guid componentGuid = new Guid("B14FEEA6-0168-427D-AB88-5F88812B99C1");

		// Token: 0x0400161F RID: 5663
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
