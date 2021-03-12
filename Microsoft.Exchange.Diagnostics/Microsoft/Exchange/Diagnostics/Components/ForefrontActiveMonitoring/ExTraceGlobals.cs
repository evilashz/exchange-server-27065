using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring
{
	// Token: 0x020003E4 RID: 996
	public static class ExTraceGlobals
	{
		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x0005B0CF File Offset: 0x000592CF
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

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x0005B0ED File Offset: 0x000592ED
		public static Trace SMTPTracer
		{
			get
			{
				if (ExTraceGlobals.sMTPTracer == null)
				{
					ExTraceGlobals.sMTPTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.sMTPTracer;
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x0005B10B File Offset: 0x0005930B
		public static Trace SMTPConnectionTracer
		{
			get
			{
				if (ExTraceGlobals.sMTPConnectionTracer == null)
				{
					ExTraceGlobals.sMTPConnectionTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.sMTPConnectionTracer;
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x0005B129 File Offset: 0x00059329
		public static Trace SMTPMonitorTracer
		{
			get
			{
				if (ExTraceGlobals.sMTPMonitorTracer == null)
				{
					ExTraceGlobals.sMTPMonitorTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.sMTPMonitorTracer;
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x060017F6 RID: 6134 RVA: 0x0005B147 File Offset: 0x00059347
		public static Trace WebServiceTracer
		{
			get
			{
				if (ExTraceGlobals.webServiceTracer == null)
				{
					ExTraceGlobals.webServiceTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.webServiceTracer;
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x060017F7 RID: 6135 RVA: 0x0005B165 File Offset: 0x00059365
		public static Trace HTTPTracer
		{
			get
			{
				if (ExTraceGlobals.hTTPTracer == null)
				{
					ExTraceGlobals.hTTPTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.hTTPTracer;
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x0005B183 File Offset: 0x00059383
		public static Trace ResponderTracer
		{
			get
			{
				if (ExTraceGlobals.responderTracer == null)
				{
					ExTraceGlobals.responderTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.responderTracer;
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x0005B1A1 File Offset: 0x000593A1
		public static Trace DNSTracer
		{
			get
			{
				if (ExTraceGlobals.dNSTracer == null)
				{
					ExTraceGlobals.dNSTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.dNSTracer;
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x060017FA RID: 6138 RVA: 0x0005B1BF File Offset: 0x000593BF
		public static Trace AntiSpamTracer
		{
			get
			{
				if (ExTraceGlobals.antiSpamTracer == null)
				{
					ExTraceGlobals.antiSpamTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.antiSpamTracer;
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x0005B1DD File Offset: 0x000593DD
		public static Trace BackgroundTracer
		{
			get
			{
				if (ExTraceGlobals.backgroundTracer == null)
				{
					ExTraceGlobals.backgroundTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.backgroundTracer;
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x0005B1FC File Offset: 0x000593FC
		public static Trace DALTracer
		{
			get
			{
				if (ExTraceGlobals.dALTracer == null)
				{
					ExTraceGlobals.dALTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.dALTracer;
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x060017FD RID: 6141 RVA: 0x0005B21B File Offset: 0x0005941B
		public static Trace DeploymentTracer
		{
			get
			{
				if (ExTraceGlobals.deploymentTracer == null)
				{
					ExTraceGlobals.deploymentTracer = new Trace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.deploymentTracer;
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x060017FE RID: 6142 RVA: 0x0005B23A File Offset: 0x0005943A
		public static Trace MonitoringTracer
		{
			get
			{
				if (ExTraceGlobals.monitoringTracer == null)
				{
					ExTraceGlobals.monitoringTracer = new Trace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.monitoringTracer;
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x060017FF RID: 6143 RVA: 0x0005B259 File Offset: 0x00059459
		public static Trace ProvisioningTracer
		{
			get
			{
				if (ExTraceGlobals.provisioningTracer == null)
				{
					ExTraceGlobals.provisioningTracer = new Trace(ExTraceGlobals.componentGuid, 15);
				}
				return ExTraceGlobals.provisioningTracer;
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x0005B278 File Offset: 0x00059478
		public static Trace TransportTracer
		{
			get
			{
				if (ExTraceGlobals.transportTracer == null)
				{
					ExTraceGlobals.transportTracer = new Trace(ExTraceGlobals.componentGuid, 16);
				}
				return ExTraceGlobals.transportTracer;
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x0005B297 File Offset: 0x00059497
		public static Trace WebStoreTracer
		{
			get
			{
				if (ExTraceGlobals.webStoreTracer == null)
				{
					ExTraceGlobals.webStoreTracer = new Trace(ExTraceGlobals.componentGuid, 17);
				}
				return ExTraceGlobals.webStoreTracer;
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x0005B2B6 File Offset: 0x000594B6
		public static Trace CmdletTracer
		{
			get
			{
				if (ExTraceGlobals.cmdletTracer == null)
				{
					ExTraceGlobals.cmdletTracer = new Trace(ExTraceGlobals.componentGuid, 18);
				}
				return ExTraceGlobals.cmdletTracer;
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x0005B2D5 File Offset: 0x000594D5
		public static Trace GenericHelperTracer
		{
			get
			{
				if (ExTraceGlobals.genericHelperTracer == null)
				{
					ExTraceGlobals.genericHelperTracer = new Trace(ExTraceGlobals.componentGuid, 19);
				}
				return ExTraceGlobals.genericHelperTracer;
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x0005B2F4 File Offset: 0x000594F4
		public static Trace DataminingTracer
		{
			get
			{
				if (ExTraceGlobals.dataminingTracer == null)
				{
					ExTraceGlobals.dataminingTracer = new Trace(ExTraceGlobals.componentGuid, 20);
				}
				return ExTraceGlobals.dataminingTracer;
			}
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x0005B313 File Offset: 0x00059513
		public static Trace ShadowRedundancyTracer
		{
			get
			{
				if (ExTraceGlobals.shadowRedundancyTracer == null)
				{
					ExTraceGlobals.shadowRedundancyTracer = new Trace(ExTraceGlobals.componentGuid, 21);
				}
				return ExTraceGlobals.shadowRedundancyTracer;
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x0005B332 File Offset: 0x00059532
		public static Trace RWSTracer
		{
			get
			{
				if (ExTraceGlobals.rWSTracer == null)
				{
					ExTraceGlobals.rWSTracer = new Trace(ExTraceGlobals.componentGuid, 22);
				}
				return ExTraceGlobals.rWSTracer;
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x0005B351 File Offset: 0x00059551
		public static Trace MessageTracingTracer
		{
			get
			{
				if (ExTraceGlobals.messageTracingTracer == null)
				{
					ExTraceGlobals.messageTracingTracer = new Trace(ExTraceGlobals.componentGuid, 23);
				}
				return ExTraceGlobals.messageTracingTracer;
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x0005B370 File Offset: 0x00059570
		public static Trace AsyncEngineTracer
		{
			get
			{
				if (ExTraceGlobals.asyncEngineTracer == null)
				{
					ExTraceGlobals.asyncEngineTracer = new Trace(ExTraceGlobals.componentGuid, 24);
				}
				return ExTraceGlobals.asyncEngineTracer;
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x0005B38F File Offset: 0x0005958F
		public static Trace FFOMigration1415Tracer
		{
			get
			{
				if (ExTraceGlobals.fFOMigration1415Tracer == null)
				{
					ExTraceGlobals.fFOMigration1415Tracer = new Trace(ExTraceGlobals.componentGuid, 25);
				}
				return ExTraceGlobals.fFOMigration1415Tracer;
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x0600180A RID: 6154 RVA: 0x0005B3AE File Offset: 0x000595AE
		public static Trace QueueDigestTracer
		{
			get
			{
				if (ExTraceGlobals.queueDigestTracer == null)
				{
					ExTraceGlobals.queueDigestTracer = new Trace(ExTraceGlobals.componentGuid, 26);
				}
				return ExTraceGlobals.queueDigestTracer;
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x0005B3CD File Offset: 0x000595CD
		public static Trace CentralAdminTracer
		{
			get
			{
				if (ExTraceGlobals.centralAdminTracer == null)
				{
					ExTraceGlobals.centralAdminTracer = new Trace(ExTraceGlobals.componentGuid, 27);
				}
				return ExTraceGlobals.centralAdminTracer;
			}
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x0600180C RID: 6156 RVA: 0x0005B3EC File Offset: 0x000595EC
		public static Trace RPSTracer
		{
			get
			{
				if (ExTraceGlobals.rPSTracer == null)
				{
					ExTraceGlobals.rPSTracer = new Trace(ExTraceGlobals.componentGuid, 28);
				}
				return ExTraceGlobals.rPSTracer;
			}
		}

		// Token: 0x04001CA0 RID: 7328
		private static Guid componentGuid = new Guid("94FBFACE-D4CE-4A9F-B2C6-64646394868F");

		// Token: 0x04001CA1 RID: 7329
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001CA2 RID: 7330
		private static Trace sMTPTracer = null;

		// Token: 0x04001CA3 RID: 7331
		private static Trace sMTPConnectionTracer = null;

		// Token: 0x04001CA4 RID: 7332
		private static Trace sMTPMonitorTracer = null;

		// Token: 0x04001CA5 RID: 7333
		private static Trace webServiceTracer = null;

		// Token: 0x04001CA6 RID: 7334
		private static Trace hTTPTracer = null;

		// Token: 0x04001CA7 RID: 7335
		private static Trace responderTracer = null;

		// Token: 0x04001CA8 RID: 7336
		private static Trace dNSTracer = null;

		// Token: 0x04001CA9 RID: 7337
		private static Trace antiSpamTracer = null;

		// Token: 0x04001CAA RID: 7338
		private static Trace backgroundTracer = null;

		// Token: 0x04001CAB RID: 7339
		private static Trace dALTracer = null;

		// Token: 0x04001CAC RID: 7340
		private static Trace deploymentTracer = null;

		// Token: 0x04001CAD RID: 7341
		private static Trace monitoringTracer = null;

		// Token: 0x04001CAE RID: 7342
		private static Trace provisioningTracer = null;

		// Token: 0x04001CAF RID: 7343
		private static Trace transportTracer = null;

		// Token: 0x04001CB0 RID: 7344
		private static Trace webStoreTracer = null;

		// Token: 0x04001CB1 RID: 7345
		private static Trace cmdletTracer = null;

		// Token: 0x04001CB2 RID: 7346
		private static Trace genericHelperTracer = null;

		// Token: 0x04001CB3 RID: 7347
		private static Trace dataminingTracer = null;

		// Token: 0x04001CB4 RID: 7348
		private static Trace shadowRedundancyTracer = null;

		// Token: 0x04001CB5 RID: 7349
		private static Trace rWSTracer = null;

		// Token: 0x04001CB6 RID: 7350
		private static Trace messageTracingTracer = null;

		// Token: 0x04001CB7 RID: 7351
		private static Trace asyncEngineTracer = null;

		// Token: 0x04001CB8 RID: 7352
		private static Trace fFOMigration1415Tracer = null;

		// Token: 0x04001CB9 RID: 7353
		private static Trace queueDigestTracer = null;

		// Token: 0x04001CBA RID: 7354
		private static Trace centralAdminTracer = null;

		// Token: 0x04001CBB RID: 7355
		private static Trace rPSTracer = null;
	}
}
