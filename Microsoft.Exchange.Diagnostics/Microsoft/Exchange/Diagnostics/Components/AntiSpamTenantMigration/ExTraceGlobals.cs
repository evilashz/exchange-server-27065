using System;

namespace Microsoft.Exchange.Diagnostics.Components.AntiSpamTenantMigration
{
	// Token: 0x020003D0 RID: 976
	public static class ExTraceGlobals
	{
		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x00059D1D File Offset: 0x00057F1D
		public static Trace AntiSpamTenantMigrationTracer
		{
			get
			{
				if (ExTraceGlobals.antiSpamTenantMigrationTracer == null)
				{
					ExTraceGlobals.antiSpamTenantMigrationTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.antiSpamTenantMigrationTracer;
			}
		}

		// Token: 0x04001C0C RID: 7180
		private static Guid componentGuid = new Guid("8B9CF4C9-EFC6-44E5-B0C0-795992D39DB9");

		// Token: 0x04001C0D RID: 7181
		private static Trace antiSpamTenantMigrationTracer = null;
	}
}
