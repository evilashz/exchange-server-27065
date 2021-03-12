using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.FfoMigration
{
	// Token: 0x020003E0 RID: 992
	public static class ExTraceGlobals
	{
		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x0600178C RID: 6028 RVA: 0x0005A265 File Offset: 0x00058465
		public static Trace PowershellProviderTracer
		{
			get
			{
				if (ExTraceGlobals.powershellProviderTracer == null)
				{
					ExTraceGlobals.powershellProviderTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.powershellProviderTracer;
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x0600178D RID: 6029 RVA: 0x0005A283 File Offset: 0x00058483
		public static Trace MigrationWorkflowTracer
		{
			get
			{
				if (ExTraceGlobals.migrationWorkflowTracer == null)
				{
					ExTraceGlobals.migrationWorkflowTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.migrationWorkflowTracer;
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x0600178E RID: 6030 RVA: 0x0005A2A1 File Offset: 0x000584A1
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x04001C3A RID: 7226
		private static Guid componentGuid = new Guid("C7BDFB80-A905-4da5-B7AF-B36A79AD2182");

		// Token: 0x04001C3B RID: 7227
		private static Trace powershellProviderTracer = null;

		// Token: 0x04001C3C RID: 7228
		private static Trace migrationWorkflowTracer = null;

		// Token: 0x04001C3D RID: 7229
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
