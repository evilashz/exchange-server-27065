using System;

namespace Microsoft.Exchange.Diagnostics.Components.TenantMonitoring
{
	// Token: 0x020003BC RID: 956
	public static class ExTraceGlobals
	{
		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x060016FE RID: 5886 RVA: 0x000590F5 File Offset: 0x000572F5
		public static Trace DataProviderTracer
		{
			get
			{
				if (ExTraceGlobals.dataProviderTracer == null)
				{
					ExTraceGlobals.dataProviderTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.dataProviderTracer;
			}
		}

		// Token: 0x04001BAC RID: 7084
		private static Guid componentGuid = new Guid("756b7f3f-cd68-4cd8-8737-180b7ad456f8");

		// Token: 0x04001BAD RID: 7085
		private static Trace dataProviderTracer = null;
	}
}
