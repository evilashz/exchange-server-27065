using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ManagedStore.AdminInterface
{
	// Token: 0x0200038F RID: 911
	public static class ExTraceGlobals
	{
		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x000561CA File Offset: 0x000543CA
		public static Trace AdminRpcTracer
		{
			get
			{
				if (ExTraceGlobals.adminRpcTracer == null)
				{
					ExTraceGlobals.adminRpcTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.adminRpcTracer;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x000561E8 File Offset: 0x000543E8
		public static Trace MailboxSignatureTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxSignatureTracer == null)
				{
					ExTraceGlobals.mailboxSignatureTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.mailboxSignatureTracer;
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x060015A3 RID: 5539 RVA: 0x00056206 File Offset: 0x00054406
		public static Trace MultiMailboxSearchTracer
		{
			get
			{
				if (ExTraceGlobals.multiMailboxSearchTracer == null)
				{
					ExTraceGlobals.multiMailboxSearchTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.multiMailboxSearchTracer;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x00056224 File Offset: 0x00054424
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 20);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x04001A4F RID: 6735
		private static Guid componentGuid = new Guid("40a87a6b-f69b-4c8e-b8c9-1835d09acfe3");

		// Token: 0x04001A50 RID: 6736
		private static Trace adminRpcTracer = null;

		// Token: 0x04001A51 RID: 6737
		private static Trace mailboxSignatureTracer = null;

		// Token: 0x04001A52 RID: 6738
		private static Trace multiMailboxSearchTracer = null;

		// Token: 0x04001A53 RID: 6739
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
