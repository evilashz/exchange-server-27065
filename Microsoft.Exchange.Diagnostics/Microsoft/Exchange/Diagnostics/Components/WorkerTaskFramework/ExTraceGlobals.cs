using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.WorkerTaskFramework
{
	// Token: 0x020003E1 RID: 993
	public static class ExTraceGlobals
	{
		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06001790 RID: 6032 RVA: 0x0005A2E2 File Offset: 0x000584E2
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06001791 RID: 6033 RVA: 0x0005A300 File Offset: 0x00058500
		public static Trace CoreTracer
		{
			get
			{
				if (ExTraceGlobals.coreTracer == null)
				{
					ExTraceGlobals.coreTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.coreTracer;
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x0005A31E File Offset: 0x0005851E
		public static Trace DataAccessTracer
		{
			get
			{
				if (ExTraceGlobals.dataAccessTracer == null)
				{
					ExTraceGlobals.dataAccessTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.dataAccessTracer;
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06001793 RID: 6035 RVA: 0x0005A33C File Offset: 0x0005853C
		public static Trace WorkItemTracer
		{
			get
			{
				if (ExTraceGlobals.workItemTracer == null)
				{
					ExTraceGlobals.workItemTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.workItemTracer;
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x0005A35A File Offset: 0x0005855A
		public static Trace ManagedAvailabilityTracer
		{
			get
			{
				if (ExTraceGlobals.managedAvailabilityTracer == null)
				{
					ExTraceGlobals.managedAvailabilityTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.managedAvailabilityTracer;
			}
		}

		// Token: 0x04001C3E RID: 7230
		private static Guid componentGuid = new Guid("EAF36C57-87B9-4D84-B551-3537A14A62B8");

		// Token: 0x04001C3F RID: 7231
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001C40 RID: 7232
		private static Trace coreTracer = null;

		// Token: 0x04001C41 RID: 7233
		private static Trace dataAccessTracer = null;

		// Token: 0x04001C42 RID: 7234
		private static Trace workItemTracer = null;

		// Token: 0x04001C43 RID: 7235
		private static Trace managedAvailabilityTracer = null;
	}
}
