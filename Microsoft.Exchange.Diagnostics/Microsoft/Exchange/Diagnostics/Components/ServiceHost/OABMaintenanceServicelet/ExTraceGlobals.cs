using System;

namespace Microsoft.Exchange.Diagnostics.Components.ServiceHost.OABMaintenanceServicelet
{
	// Token: 0x020003AE RID: 942
	public static class ExTraceGlobals
	{
		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x00058A36 File Offset: 0x00056C36
		public static Trace ServiceletFrameworkTracer
		{
			get
			{
				if (ExTraceGlobals.serviceletFrameworkTracer == null)
				{
					ExTraceGlobals.serviceletFrameworkTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.serviceletFrameworkTracer;
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x060016C8 RID: 5832 RVA: 0x00058A54 File Offset: 0x00056C54
		public static Trace RecoverOrphanedOABsTaskTracer
		{
			get
			{
				if (ExTraceGlobals.recoverOrphanedOABsTaskTracer == null)
				{
					ExTraceGlobals.recoverOrphanedOABsTaskTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.recoverOrphanedOABsTaskTracer;
			}
		}

		// Token: 0x04001B75 RID: 7029
		private static Guid componentGuid = new Guid("26A265F6-54FB-4345-A280-DBD4A1A62EE4");

		// Token: 0x04001B76 RID: 7030
		private static Trace serviceletFrameworkTracer = null;

		// Token: 0x04001B77 RID: 7031
		private static Trace recoverOrphanedOABsTaskTracer = null;
	}
}
