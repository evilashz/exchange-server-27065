using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.Monitoring
{
	// Token: 0x0200037E RID: 894
	public static class ExTraceGlobals
	{
		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x0005523E File Offset: 0x0005343E
		public static Trace MonitoringServiceTracer
		{
			get
			{
				if (ExTraceGlobals.monitoringServiceTracer == null)
				{
					ExTraceGlobals.monitoringServiceTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.monitoringServiceTracer;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x0005525C File Offset: 0x0005345C
		public static Trace MonitoringRpcServerTracer
		{
			get
			{
				if (ExTraceGlobals.monitoringRpcServerTracer == null)
				{
					ExTraceGlobals.monitoringRpcServerTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.monitoringRpcServerTracer;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x0600152D RID: 5421 RVA: 0x0005527A File Offset: 0x0005347A
		public static Trace MonitoringTasksTracer
		{
			get
			{
				if (ExTraceGlobals.monitoringTasksTracer == null)
				{
					ExTraceGlobals.monitoringTasksTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.monitoringTasksTracer;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x00055298 File Offset: 0x00053498
		public static Trace MonitoringHelperTracer
		{
			get
			{
				if (ExTraceGlobals.monitoringHelperTracer == null)
				{
					ExTraceGlobals.monitoringHelperTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.monitoringHelperTracer;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x000552B6 File Offset: 0x000534B6
		public static Trace MonitoringDataTracer
		{
			get
			{
				if (ExTraceGlobals.monitoringDataTracer == null)
				{
					ExTraceGlobals.monitoringDataTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.monitoringDataTracer;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001530 RID: 5424 RVA: 0x000552D4 File Offset: 0x000534D4
		public static Trace CorrelationEngineTracer
		{
			get
			{
				if (ExTraceGlobals.correlationEngineTracer == null)
				{
					ExTraceGlobals.correlationEngineTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.correlationEngineTracer;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x000552F2 File Offset: 0x000534F2
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

		// Token: 0x040019D9 RID: 6617
		private static Guid componentGuid = new Guid("170506F7-64BA-4C74-A2A3-0A5CC247DB58");

		// Token: 0x040019DA RID: 6618
		private static Trace monitoringServiceTracer = null;

		// Token: 0x040019DB RID: 6619
		private static Trace monitoringRpcServerTracer = null;

		// Token: 0x040019DC RID: 6620
		private static Trace monitoringTasksTracer = null;

		// Token: 0x040019DD RID: 6621
		private static Trace monitoringHelperTracer = null;

		// Token: 0x040019DE RID: 6622
		private static Trace monitoringDataTracer = null;

		// Token: 0x040019DF RID: 6623
		private static Trace correlationEngineTracer = null;

		// Token: 0x040019E0 RID: 6624
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
