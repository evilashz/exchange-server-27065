using System;

namespace Microsoft.Exchange.Diagnostics.Components.ProtocolAnalysisBg
{
	// Token: 0x0200038A RID: 906
	public static class ExTraceGlobals
	{
		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x00055E14 File Offset: 0x00054014
		public static Trace FactoryTracer
		{
			get
			{
				if (ExTraceGlobals.factoryTracer == null)
				{
					ExTraceGlobals.factoryTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.factoryTracer;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x00055E32 File Offset: 0x00054032
		public static Trace AgentTracer
		{
			get
			{
				if (ExTraceGlobals.agentTracer == null)
				{
					ExTraceGlobals.agentTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.agentTracer;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x00055E50 File Offset: 0x00054050
		public static Trace DatabaseTracer
		{
			get
			{
				if (ExTraceGlobals.databaseTracer == null)
				{
					ExTraceGlobals.databaseTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.databaseTracer;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x00055E6E File Offset: 0x0005406E
		public static Trace OnOpenProxyDetectionTracer
		{
			get
			{
				if (ExTraceGlobals.onOpenProxyDetectionTracer == null)
				{
					ExTraceGlobals.onOpenProxyDetectionTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.onOpenProxyDetectionTracer;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x00055E8C File Offset: 0x0005408C
		public static Trace OnDnsQueryTracer
		{
			get
			{
				if (ExTraceGlobals.onDnsQueryTracer == null)
				{
					ExTraceGlobals.onDnsQueryTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.onDnsQueryTracer;
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06001589 RID: 5513 RVA: 0x00055EAA File Offset: 0x000540AA
		public static Trace OnSenderBlockingTracer
		{
			get
			{
				if (ExTraceGlobals.onSenderBlockingTracer == null)
				{
					ExTraceGlobals.onSenderBlockingTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.onSenderBlockingTracer;
			}
		}

		// Token: 0x04001A32 RID: 6706
		private static Guid componentGuid = new Guid("E30C077B-EBAD-4AC8-B2BF-197C3329952F");

		// Token: 0x04001A33 RID: 6707
		private static Trace factoryTracer = null;

		// Token: 0x04001A34 RID: 6708
		private static Trace agentTracer = null;

		// Token: 0x04001A35 RID: 6709
		private static Trace databaseTracer = null;

		// Token: 0x04001A36 RID: 6710
		private static Trace onOpenProxyDetectionTracer = null;

		// Token: 0x04001A37 RID: 6711
		private static Trace onDnsQueryTracer = null;

		// Token: 0x04001A38 RID: 6712
		private static Trace onSenderBlockingTracer = null;
	}
}
