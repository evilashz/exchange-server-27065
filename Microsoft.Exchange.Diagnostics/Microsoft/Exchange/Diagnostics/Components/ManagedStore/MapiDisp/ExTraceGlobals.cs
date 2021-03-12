using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiDisp
{
	// Token: 0x0200038E RID: 910
	public static class ExTraceGlobals
	{
		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06001599 RID: 5529 RVA: 0x000560BC File Offset: 0x000542BC
		public static Trace RpcBufferTracer
		{
			get
			{
				if (ExTraceGlobals.rpcBufferTracer == null)
				{
					ExTraceGlobals.rpcBufferTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.rpcBufferTracer;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x000560DA File Offset: 0x000542DA
		public static Trace RpcOperationTracer
		{
			get
			{
				if (ExTraceGlobals.rpcOperationTracer == null)
				{
					ExTraceGlobals.rpcOperationTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.rpcOperationTracer;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x000560F8 File Offset: 0x000542F8
		public static Trace RpcDetailTracer
		{
			get
			{
				if (ExTraceGlobals.rpcDetailTracer == null)
				{
					ExTraceGlobals.rpcDetailTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.rpcDetailTracer;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x0600159C RID: 5532 RVA: 0x00056116 File Offset: 0x00054316
		public static Trace RopTimingTracer
		{
			get
			{
				if (ExTraceGlobals.ropTimingTracer == null)
				{
					ExTraceGlobals.ropTimingTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.ropTimingTracer;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x00056134 File Offset: 0x00054334
		public static Trace RpcContextPoolTracer
		{
			get
			{
				if (ExTraceGlobals.rpcContextPoolTracer == null)
				{
					ExTraceGlobals.rpcContextPoolTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.rpcContextPoolTracer;
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x00056152 File Offset: 0x00054352
		public static Trace SyncMailboxWithDSTracer
		{
			get
			{
				if (ExTraceGlobals.syncMailboxWithDSTracer == null)
				{
					ExTraceGlobals.syncMailboxWithDSTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.syncMailboxWithDSTracer;
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x00056170 File Offset: 0x00054370
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

		// Token: 0x04001A47 RID: 6727
		private static Guid componentGuid = new Guid("0df8b91e-45ef-41d3-bb91-b60a4446bb35");

		// Token: 0x04001A48 RID: 6728
		private static Trace rpcBufferTracer = null;

		// Token: 0x04001A49 RID: 6729
		private static Trace rpcOperationTracer = null;

		// Token: 0x04001A4A RID: 6730
		private static Trace rpcDetailTracer = null;

		// Token: 0x04001A4B RID: 6731
		private static Trace ropTimingTracer = null;

		// Token: 0x04001A4C RID: 6732
		private static Trace rpcContextPoolTracer = null;

		// Token: 0x04001A4D RID: 6733
		private static Trace syncMailboxWithDSTracer = null;

		// Token: 0x04001A4E RID: 6734
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
