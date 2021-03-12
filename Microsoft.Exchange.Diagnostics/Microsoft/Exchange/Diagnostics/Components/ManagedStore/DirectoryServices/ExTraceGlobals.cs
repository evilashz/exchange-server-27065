using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ManagedStore.DirectoryServices
{
	// Token: 0x02000391 RID: 913
	public static class ExTraceGlobals
	{
		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x060015AF RID: 5551 RVA: 0x000563AC File Offset: 0x000545AC
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

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x000563CA File Offset: 0x000545CA
		public static Trace ADCallsTracer
		{
			get
			{
				if (ExTraceGlobals.aDCallsTracer == null)
				{
					ExTraceGlobals.aDCallsTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.aDCallsTracer;
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x060015B1 RID: 5553 RVA: 0x000563E8 File Offset: 0x000545E8
		public static Trace CallStackTracer
		{
			get
			{
				if (ExTraceGlobals.callStackTracer == null)
				{
					ExTraceGlobals.callStackTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.callStackTracer;
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x00056406 File Offset: 0x00054606
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

		// Token: 0x04001A5D RID: 6749
		private static Guid componentGuid = new Guid("2d756daa-9cee-497d-b5a1-dc2f994b99ca");

		// Token: 0x04001A5E RID: 6750
		private static Trace generalTracer = null;

		// Token: 0x04001A5F RID: 6751
		private static Trace aDCallsTracer = null;

		// Token: 0x04001A60 RID: 6752
		private static Trace callStackTracer = null;

		// Token: 0x04001A61 RID: 6753
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
