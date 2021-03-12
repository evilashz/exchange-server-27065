using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ProvisioningAgent
{
	// Token: 0x020003A1 RID: 929
	public static class ExTraceGlobals
	{
		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x00057F40 File Offset: 0x00056140
		public static Trace RusTracer
		{
			get
			{
				if (ExTraceGlobals.rusTracer == null)
				{
					ExTraceGlobals.rusTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.rusTracer;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06001674 RID: 5748 RVA: 0x00057F5E File Offset: 0x0005615E
		public static Trace AdminAuditLogTracer
		{
			get
			{
				if (ExTraceGlobals.adminAuditLogTracer == null)
				{
					ExTraceGlobals.adminAuditLogTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.adminAuditLogTracer;
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x00057F7C File Offset: 0x0005617C
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

		// Token: 0x04001B21 RID: 6945
		private static Guid componentGuid = new Guid("f0fd0248-ef90-4fad-8a53-a6a21ac5528c");

		// Token: 0x04001B22 RID: 6946
		private static Trace rusTracer = null;

		// Token: 0x04001B23 RID: 6947
		private static Trace adminAuditLogTracer = null;

		// Token: 0x04001B24 RID: 6948
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
