using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.CertificateAuthentication
{
	// Token: 0x02000371 RID: 881
	public static class ExTraceGlobals
	{
		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x00053F7D File Offset: 0x0005217D
		public static Trace CertAuthTracer
		{
			get
			{
				if (ExTraceGlobals.certAuthTracer == null)
				{
					ExTraceGlobals.certAuthTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.certAuthTracer;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x00053F9B File Offset: 0x0005219B
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x04001950 RID: 6480
		private static Guid componentGuid = new Guid("39942ef4-b83c-426d-b492-ba040437091a");

		// Token: 0x04001951 RID: 6481
		private static Trace certAuthTracer = null;

		// Token: 0x04001952 RID: 6482
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
