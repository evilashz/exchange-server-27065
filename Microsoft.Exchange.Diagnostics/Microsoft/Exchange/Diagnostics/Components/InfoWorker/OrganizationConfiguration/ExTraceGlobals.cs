using System;

namespace Microsoft.Exchange.Diagnostics.Components.InfoWorker.OrganizationConfiguration
{
	// Token: 0x02000356 RID: 854
	public static class ExTraceGlobals
	{
		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x000522BA File Offset: 0x000504BA
		public static Trace OrganizationConfigurationTracer
		{
			get
			{
				if (ExTraceGlobals.organizationConfigurationTracer == null)
				{
					ExTraceGlobals.organizationConfigurationTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.organizationConfigurationTracer;
			}
		}

		// Token: 0x04001879 RID: 6265
		private static Guid componentGuid = new Guid("2D64C97D-8957-48fb-B734-A6D176C1EA05");

		// Token: 0x0400187A RID: 6266
		private static Trace organizationConfigurationTracer = null;
	}
}
