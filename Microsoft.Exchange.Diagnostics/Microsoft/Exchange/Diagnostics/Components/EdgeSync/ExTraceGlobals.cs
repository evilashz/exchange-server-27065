using System;

namespace Microsoft.Exchange.Diagnostics.Components.EdgeSync
{
	// Token: 0x0200031C RID: 796
	public static class ExTraceGlobals
	{
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x0004BEB4 File Offset: 0x0004A0B4
		public static Trace ProcessTracer
		{
			get
			{
				if (ExTraceGlobals.processTracer == null)
				{
					ExTraceGlobals.processTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.processTracer;
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x0004BED2 File Offset: 0x0004A0D2
		public static Trace ConnectionTracer
		{
			get
			{
				if (ExTraceGlobals.connectionTracer == null)
				{
					ExTraceGlobals.connectionTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.connectionTracer;
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x0004BEF0 File Offset: 0x0004A0F0
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

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x0004BF0E File Offset: 0x0004A10E
		public static Trace SyncNowTracer
		{
			get
			{
				if (ExTraceGlobals.syncNowTracer == null)
				{
					ExTraceGlobals.syncNowTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.syncNowTracer;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x0004BF2C File Offset: 0x0004A12C
		public static Trace TopologyTracer
		{
			get
			{
				if (ExTraceGlobals.topologyTracer == null)
				{
					ExTraceGlobals.topologyTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.topologyTracer;
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x060010FD RID: 4349 RVA: 0x0004BF4A File Offset: 0x0004A14A
		public static Trace SynchronizationJobTracer
		{
			get
			{
				if (ExTraceGlobals.synchronizationJobTracer == null)
				{
					ExTraceGlobals.synchronizationJobTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.synchronizationJobTracer;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x0004BF68 File Offset: 0x0004A168
		public static Trace SubscriptionTracer
		{
			get
			{
				if (ExTraceGlobals.subscriptionTracer == null)
				{
					ExTraceGlobals.subscriptionTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.subscriptionTracer;
			}
		}

		// Token: 0x040015A6 RID: 5542
		private static Guid componentGuid = new Guid("AB9C28FE-50E0-4907-BB41-8F82D8E0C068");

		// Token: 0x040015A7 RID: 5543
		private static Trace processTracer = null;

		// Token: 0x040015A8 RID: 5544
		private static Trace connectionTracer = null;

		// Token: 0x040015A9 RID: 5545
		private static Trace schedulerTracer = null;

		// Token: 0x040015AA RID: 5546
		private static Trace syncNowTracer = null;

		// Token: 0x040015AB RID: 5547
		private static Trace topologyTracer = null;

		// Token: 0x040015AC RID: 5548
		private static Trace synchronizationJobTracer = null;

		// Token: 0x040015AD RID: 5549
		private static Trace subscriptionTracer = null;
	}
}
