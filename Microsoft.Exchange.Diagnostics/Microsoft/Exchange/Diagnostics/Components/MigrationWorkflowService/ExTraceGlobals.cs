using System;

namespace Microsoft.Exchange.Diagnostics.Components.MigrationWorkflowService
{
	// Token: 0x0200037B RID: 891
	public static class ExTraceGlobals
	{
		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001519 RID: 5401 RVA: 0x00054FE4 File Offset: 0x000531E4
		public static Trace MigrationWorkflowServiceTracer
		{
			get
			{
				if (ExTraceGlobals.migrationWorkflowServiceTracer == null)
				{
					ExTraceGlobals.migrationWorkflowServiceTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.migrationWorkflowServiceTracer;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x00055002 File Offset: 0x00053202
		public static Trace MailboxLoadBalanceTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxLoadBalanceTracer == null)
				{
					ExTraceGlobals.mailboxLoadBalanceTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.mailboxLoadBalanceTracer;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x0600151B RID: 5403 RVA: 0x00055020 File Offset: 0x00053220
		public static Trace InjectorServiceTracer
		{
			get
			{
				if (ExTraceGlobals.injectorServiceTracer == null)
				{
					ExTraceGlobals.injectorServiceTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.injectorServiceTracer;
			}
		}

		// Token: 0x040019C7 RID: 6599
		private static Guid componentGuid = new Guid("d58c9a14-d24d-45c5-9aac-14e2678adff8");

		// Token: 0x040019C8 RID: 6600
		private static Trace migrationWorkflowServiceTracer = null;

		// Token: 0x040019C9 RID: 6601
		private static Trace mailboxLoadBalanceTracer = null;

		// Token: 0x040019CA RID: 6602
		private static Trace injectorServiceTracer = null;
	}
}
