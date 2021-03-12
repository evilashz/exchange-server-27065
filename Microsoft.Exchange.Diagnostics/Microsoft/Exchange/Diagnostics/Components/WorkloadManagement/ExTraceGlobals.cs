using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.WorkloadManagement
{
	// Token: 0x020003BF RID: 959
	public static class ExTraceGlobals
	{
		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x0600170A RID: 5898 RVA: 0x0005926C File Offset: 0x0005746C
		public static Trace CommonTracer
		{
			get
			{
				if (ExTraceGlobals.commonTracer == null)
				{
					ExTraceGlobals.commonTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.commonTracer;
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x0600170B RID: 5899 RVA: 0x0005928A File Offset: 0x0005748A
		public static Trace ExecutionTracer
		{
			get
			{
				if (ExTraceGlobals.executionTracer == null)
				{
					ExTraceGlobals.executionTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.executionTracer;
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x0600170C RID: 5900 RVA: 0x000592A8 File Offset: 0x000574A8
		public static Trace SchedulerTracer
		{
			get
			{
				if (ExTraceGlobals.schedulerTracer == null)
				{
					ExTraceGlobals.schedulerTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.schedulerTracer;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x000592C6 File Offset: 0x000574C6
		public static Trace PoliciesTracer
		{
			get
			{
				if (ExTraceGlobals.policiesTracer == null)
				{
					ExTraceGlobals.policiesTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.policiesTracer;
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x0600170E RID: 5902 RVA: 0x000592E4 File Offset: 0x000574E4
		public static Trace ActivityContextTracer
		{
			get
			{
				if (ExTraceGlobals.activityContextTracer == null)
				{
					ExTraceGlobals.activityContextTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.activityContextTracer;
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x00059302 File Offset: 0x00057502
		public static Trace UserWorkloadManagerTracer
		{
			get
			{
				if (ExTraceGlobals.userWorkloadManagerTracer == null)
				{
					ExTraceGlobals.userWorkloadManagerTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.userWorkloadManagerTracer;
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x00059320 File Offset: 0x00057520
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x0005933E File Offset: 0x0005753E
		public static Trace AdmissionControlTracer
		{
			get
			{
				if (ExTraceGlobals.admissionControlTracer == null)
				{
					ExTraceGlobals.admissionControlTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.admissionControlTracer;
			}
		}

		// Token: 0x04001BB8 RID: 7096
		private static Guid componentGuid = new Guid("488b469c-d752-4650-8655-28590e044606");

		// Token: 0x04001BB9 RID: 7097
		private static Trace commonTracer = null;

		// Token: 0x04001BBA RID: 7098
		private static Trace executionTracer = null;

		// Token: 0x04001BBB RID: 7099
		private static Trace schedulerTracer = null;

		// Token: 0x04001BBC RID: 7100
		private static Trace policiesTracer = null;

		// Token: 0x04001BBD RID: 7101
		private static Trace activityContextTracer = null;

		// Token: 0x04001BBE RID: 7102
		private static Trace userWorkloadManagerTracer = null;

		// Token: 0x04001BBF RID: 7103
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001BC0 RID: 7104
		private static Trace admissionControlTracer = null;
	}
}
