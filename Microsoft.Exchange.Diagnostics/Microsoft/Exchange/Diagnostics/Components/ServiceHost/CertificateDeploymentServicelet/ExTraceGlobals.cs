using System;

namespace Microsoft.Exchange.Diagnostics.Components.ServiceHost.CertificateDeploymentServicelet
{
	// Token: 0x020003B0 RID: 944
	public static class ExTraceGlobals
	{
		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x060016CC RID: 5836 RVA: 0x00058AC4 File Offset: 0x00056CC4
		public static Trace ServiceletTracer
		{
			get
			{
				if (ExTraceGlobals.serviceletTracer == null)
				{
					ExTraceGlobals.serviceletTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.serviceletTracer;
			}
		}

		// Token: 0x04001B7A RID: 7034
		private static Guid componentGuid = new Guid("1772CAFB-E211-4b08-89C9-CF49BDACE986");

		// Token: 0x04001B7B RID: 7035
		private static Trace serviceletTracer = null;
	}
}
