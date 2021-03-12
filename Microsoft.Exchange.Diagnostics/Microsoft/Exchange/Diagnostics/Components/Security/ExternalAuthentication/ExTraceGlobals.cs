using System;

namespace Microsoft.Exchange.Diagnostics.Components.Security.ExternalAuthentication
{
	// Token: 0x0200035E RID: 862
	public static class ExTraceGlobals
	{
		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x000525EE File Offset: 0x000507EE
		public static Trace ConfigurationTracer
		{
			get
			{
				if (ExTraceGlobals.configurationTracer == null)
				{
					ExTraceGlobals.configurationTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.configurationTracer;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x0005260C File Offset: 0x0005080C
		public static Trace TargetUriResolverTracer
		{
			get
			{
				if (ExTraceGlobals.targetUriResolverTracer == null)
				{
					ExTraceGlobals.targetUriResolverTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.targetUriResolverTracer;
			}
		}

		// Token: 0x04001894 RID: 6292
		private static Guid componentGuid = new Guid("D12C2E1E-4222-40e0-A9D4-BF4A5FA2ADC1");

		// Token: 0x04001895 RID: 6293
		private static Trace configurationTracer = null;

		// Token: 0x04001896 RID: 6294
		private static Trace targetUriResolverTracer = null;
	}
}
