using System;

namespace Microsoft.Exchange.Diagnostics.Components.BackSync
{
	// Token: 0x020003B9 RID: 953
	public static class ExTraceGlobals
	{
		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x00058FA2 File Offset: 0x000571A2
		public static Trace BackSyncTracer
		{
			get
			{
				if (ExTraceGlobals.backSyncTracer == null)
				{
					ExTraceGlobals.backSyncTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.backSyncTracer;
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x00058FC0 File Offset: 0x000571C0
		public static Trace ActiveDirectoryTracer
		{
			get
			{
				if (ExTraceGlobals.activeDirectoryTracer == null)
				{
					ExTraceGlobals.activeDirectoryTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.activeDirectoryTracer;
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x00058FDE File Offset: 0x000571DE
		public static Trace TenantFullSyncTracer
		{
			get
			{
				if (ExTraceGlobals.tenantFullSyncTracer == null)
				{
					ExTraceGlobals.tenantFullSyncTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.tenantFullSyncTracer;
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x060016F6 RID: 5878 RVA: 0x00058FFC File Offset: 0x000571FC
		public static Trace MergeTracer
		{
			get
			{
				if (ExTraceGlobals.mergeTracer == null)
				{
					ExTraceGlobals.mergeTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.mergeTracer;
			}
		}

		// Token: 0x04001BA1 RID: 7073
		private static Guid componentGuid = new Guid("3C237538-546C-4659-AED9-F445236DFB91");

		// Token: 0x04001BA2 RID: 7074
		private static Trace backSyncTracer = null;

		// Token: 0x04001BA3 RID: 7075
		private static Trace activeDirectoryTracer = null;

		// Token: 0x04001BA4 RID: 7076
		private static Trace tenantFullSyncTracer = null;

		// Token: 0x04001BA5 RID: 7077
		private static Trace mergeTracer = null;
	}
}
