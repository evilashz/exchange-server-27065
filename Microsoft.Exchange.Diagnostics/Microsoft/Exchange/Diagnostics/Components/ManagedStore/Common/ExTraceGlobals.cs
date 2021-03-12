using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ManagedStore.Common
{
	// Token: 0x02000399 RID: 921
	public static class ExTraceGlobals
	{
		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x0005770E File Offset: 0x0005590E
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

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06001637 RID: 5687 RVA: 0x0005772C File Offset: 0x0005592C
		public static Trace StatisticsTracer
		{
			get
			{
				if (ExTraceGlobals.statisticsTracer == null)
				{
					ExTraceGlobals.statisticsTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.statisticsTracer;
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x0005774A File Offset: 0x0005594A
		public static Trace ResetStatisticsTracer
		{
			get
			{
				if (ExTraceGlobals.resetStatisticsTracer == null)
				{
					ExTraceGlobals.resetStatisticsTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.resetStatisticsTracer;
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x00057768 File Offset: 0x00055968
		public static Trace LockWaitTimeTracer
		{
			get
			{
				if (ExTraceGlobals.lockWaitTimeTracer == null)
				{
					ExTraceGlobals.lockWaitTimeTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.lockWaitTimeTracer;
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x00057786 File Offset: 0x00055986
		public static Trace TasksTracer
		{
			get
			{
				if (ExTraceGlobals.tasksTracer == null)
				{
					ExTraceGlobals.tasksTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.tasksTracer;
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x000577A4 File Offset: 0x000559A4
		public static Trace ExceptionHandlerTracer
		{
			get
			{
				if (ExTraceGlobals.exceptionHandlerTracer == null)
				{
					ExTraceGlobals.exceptionHandlerTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.exceptionHandlerTracer;
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x000577C2 File Offset: 0x000559C2
		public static Trace ConfigurationTracer
		{
			get
			{
				if (ExTraceGlobals.configurationTracer == null)
				{
					ExTraceGlobals.configurationTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.configurationTracer;
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x0600163D RID: 5693 RVA: 0x000577E0 File Offset: 0x000559E0
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

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x000577FF File Offset: 0x000559FF
		public static Trace Buddy1Tracer
		{
			get
			{
				if (ExTraceGlobals.buddy1Tracer == null)
				{
					ExTraceGlobals.buddy1Tracer = new Trace(ExTraceGlobals.componentGuid, 50);
				}
				return ExTraceGlobals.buddy1Tracer;
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x0005781E File Offset: 0x00055A1E
		public static Trace Buddy2Tracer
		{
			get
			{
				if (ExTraceGlobals.buddy2Tracer == null)
				{
					ExTraceGlobals.buddy2Tracer = new Trace(ExTraceGlobals.componentGuid, 51);
				}
				return ExTraceGlobals.buddy2Tracer;
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x0005783D File Offset: 0x00055A3D
		public static Trace Buddy3Tracer
		{
			get
			{
				if (ExTraceGlobals.buddy3Tracer == null)
				{
					ExTraceGlobals.buddy3Tracer = new Trace(ExTraceGlobals.componentGuid, 52);
				}
				return ExTraceGlobals.buddy3Tracer;
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x0005785C File Offset: 0x00055A5C
		public static Trace Buddy4Tracer
		{
			get
			{
				if (ExTraceGlobals.buddy4Tracer == null)
				{
					ExTraceGlobals.buddy4Tracer = new Trace(ExTraceGlobals.componentGuid, 53);
				}
				return ExTraceGlobals.buddy4Tracer;
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x0005787B File Offset: 0x00055A7B
		public static Trace Buddy5Tracer
		{
			get
			{
				if (ExTraceGlobals.buddy5Tracer == null)
				{
					ExTraceGlobals.buddy5Tracer = new Trace(ExTraceGlobals.componentGuid, 54);
				}
				return ExTraceGlobals.buddy5Tracer;
			}
		}

		// Token: 0x04001AE4 RID: 6884
		private static Guid componentGuid = new Guid("b5c49b06-9eda-4e9d-b5b0-d696691fe1a7");

		// Token: 0x04001AE5 RID: 6885
		private static Trace generalTracer = null;

		// Token: 0x04001AE6 RID: 6886
		private static Trace statisticsTracer = null;

		// Token: 0x04001AE7 RID: 6887
		private static Trace resetStatisticsTracer = null;

		// Token: 0x04001AE8 RID: 6888
		private static Trace lockWaitTimeTracer = null;

		// Token: 0x04001AE9 RID: 6889
		private static Trace tasksTracer = null;

		// Token: 0x04001AEA RID: 6890
		private static Trace exceptionHandlerTracer = null;

		// Token: 0x04001AEB RID: 6891
		private static Trace configurationTracer = null;

		// Token: 0x04001AEC RID: 6892
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001AED RID: 6893
		private static Trace buddy1Tracer = null;

		// Token: 0x04001AEE RID: 6894
		private static Trace buddy2Tracer = null;

		// Token: 0x04001AEF RID: 6895
		private static Trace buddy3Tracer = null;

		// Token: 0x04001AF0 RID: 6896
		private static Trace buddy4Tracer = null;

		// Token: 0x04001AF1 RID: 6897
		private static Trace buddy5Tracer = null;
	}
}
