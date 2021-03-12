using System;

namespace Microsoft.Exchange.Diagnostics.Components.AntiSpamDataMigration
{
	// Token: 0x020003CF RID: 975
	public static class ExTraceGlobals
	{
		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x00059CE8 File Offset: 0x00057EE8
		public static Trace AntiSpamDataMigrationTracer
		{
			get
			{
				if (ExTraceGlobals.antiSpamDataMigrationTracer == null)
				{
					ExTraceGlobals.antiSpamDataMigrationTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.antiSpamDataMigrationTracer;
			}
		}

		// Token: 0x04001C0A RID: 7178
		private static Guid componentGuid = new Guid("6991CBA3-1062-49EE-8AD4-A160A8205EF8");

		// Token: 0x04001C0B RID: 7179
		private static Trace antiSpamDataMigrationTracer = null;
	}
}
