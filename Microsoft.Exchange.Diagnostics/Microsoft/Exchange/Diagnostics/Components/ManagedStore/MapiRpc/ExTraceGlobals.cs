using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ManagedStore.MapiRpc
{
	// Token: 0x0200039B RID: 923
	public static class ExTraceGlobals
	{
		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x00057B46 File Offset: 0x00055D46
		public static Trace GeneralTracer
		{
			get
			{
				if (ExTraceGlobals.generalTracer == null)
				{
					ExTraceGlobals.generalTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.generalTracer;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06001655 RID: 5717 RVA: 0x00057B64 File Offset: 0x00055D64
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

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x00057B82 File Offset: 0x00055D82
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

		// Token: 0x04001B02 RID: 6914
		private static Guid componentGuid = new Guid("2B7F1123-5B0C-415b-8B74-B8563871D33D");

		// Token: 0x04001B03 RID: 6915
		private static Trace generalTracer = null;

		// Token: 0x04001B04 RID: 6916
		private static Trace rpcOperationTracer = null;

		// Token: 0x04001B05 RID: 6917
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
