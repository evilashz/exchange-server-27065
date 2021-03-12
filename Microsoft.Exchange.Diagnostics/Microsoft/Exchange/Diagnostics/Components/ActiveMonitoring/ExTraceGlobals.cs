using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring
{
	// Token: 0x020003E2 RID: 994
	public static class ExTraceGlobals
	{
		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06001796 RID: 6038 RVA: 0x0005A3A7 File Offset: 0x000585A7
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

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06001797 RID: 6039 RVA: 0x0005A3C5 File Offset: 0x000585C5
		public static Trace AzureTracer
		{
			get
			{
				if (ExTraceGlobals.azureTracer == null)
				{
					ExTraceGlobals.azureTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.azureTracer;
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06001798 RID: 6040 RVA: 0x0005A3E3 File Offset: 0x000585E3
		public static Trace CommonComponentsTracer
		{
			get
			{
				if (ExTraceGlobals.commonComponentsTracer == null)
				{
					ExTraceGlobals.commonComponentsTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.commonComponentsTracer;
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06001799 RID: 6041 RVA: 0x0005A401 File Offset: 0x00058601
		public static Trace HeartbeatTracer
		{
			get
			{
				if (ExTraceGlobals.heartbeatTracer == null)
				{
					ExTraceGlobals.heartbeatTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.heartbeatTracer;
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x0005A41F File Offset: 0x0005861F
		public static Trace HTTPTracer
		{
			get
			{
				if (ExTraceGlobals.hTTPTracer == null)
				{
					ExTraceGlobals.hTTPTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.hTTPTracer;
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x0600179B RID: 6043 RVA: 0x0005A43D File Offset: 0x0005863D
		public static Trace OWATracer
		{
			get
			{
				if (ExTraceGlobals.oWATracer == null)
				{
					ExTraceGlobals.oWATracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.oWATracer;
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x0600179C RID: 6044 RVA: 0x0005A45B File Offset: 0x0005865B
		public static Trace RPCHTTPTracer
		{
			get
			{
				if (ExTraceGlobals.rPCHTTPTracer == null)
				{
					ExTraceGlobals.rPCHTTPTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.rPCHTTPTracer;
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x0600179D RID: 6045 RVA: 0x0005A479 File Offset: 0x00058679
		public static Trace ActiveSyncTracer
		{
			get
			{
				if (ExTraceGlobals.activeSyncTracer == null)
				{
					ExTraceGlobals.activeSyncTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.activeSyncTracer;
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x0600179E RID: 6046 RVA: 0x0005A497 File Offset: 0x00058697
		public static Trace EWSTracer
		{
			get
			{
				if (ExTraceGlobals.eWSTracer == null)
				{
					ExTraceGlobals.eWSTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.eWSTracer;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x0600179F RID: 6047 RVA: 0x0005A4B5 File Offset: 0x000586B5
		public static Trace AutoDiscoverTracer
		{
			get
			{
				if (ExTraceGlobals.autoDiscoverTracer == null)
				{
					ExTraceGlobals.autoDiscoverTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.autoDiscoverTracer;
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x0005A4D4 File Offset: 0x000586D4
		public static Trace LiveIdTracer
		{
			get
			{
				if (ExTraceGlobals.liveIdTracer == null)
				{
					ExTraceGlobals.liveIdTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.liveIdTracer;
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x060017A1 RID: 6049 RVA: 0x0005A4F3 File Offset: 0x000586F3
		public static Trace RIMTracer
		{
			get
			{
				if (ExTraceGlobals.rIMTracer == null)
				{
					ExTraceGlobals.rIMTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.rIMTracer;
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x060017A2 RID: 6050 RVA: 0x0005A512 File Offset: 0x00058712
		public static Trace ServiceTracer
		{
			get
			{
				if (ExTraceGlobals.serviceTracer == null)
				{
					ExTraceGlobals.serviceTracer = new Trace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.serviceTracer;
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x060017A3 RID: 6051 RVA: 0x0005A531 File Offset: 0x00058731
		public static Trace WorkerTracer
		{
			get
			{
				if (ExTraceGlobals.workerTracer == null)
				{
					ExTraceGlobals.workerTracer = new Trace(ExTraceGlobals.componentGuid, 13);
				}
				return ExTraceGlobals.workerTracer;
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x0005A550 File Offset: 0x00058750
		public static Trace OABTracer
		{
			get
			{
				if (ExTraceGlobals.oABTracer == null)
				{
					ExTraceGlobals.oABTracer = new Trace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.oABTracer;
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x060017A5 RID: 6053 RVA: 0x0005A56F File Offset: 0x0005876F
		public static Trace StoreTracer
		{
			get
			{
				if (ExTraceGlobals.storeTracer == null)
				{
					ExTraceGlobals.storeTracer = new Trace(ExTraceGlobals.componentGuid, 15);
				}
				return ExTraceGlobals.storeTracer;
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x060017A6 RID: 6054 RVA: 0x0005A58E File Offset: 0x0005878E
		public static Trace AvailabilityServiceTracer
		{
			get
			{
				if (ExTraceGlobals.availabilityServiceTracer == null)
				{
					ExTraceGlobals.availabilityServiceTracer = new Trace(ExTraceGlobals.componentGuid, 16);
				}
				return ExTraceGlobals.availabilityServiceTracer;
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x0005A5AD File Offset: 0x000587AD
		public static Trace NetworkTracer
		{
			get
			{
				if (ExTraceGlobals.networkTracer == null)
				{
					ExTraceGlobals.networkTracer = new Trace(ExTraceGlobals.componentGuid, 17);
				}
				return ExTraceGlobals.networkTracer;
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x060017A8 RID: 6056 RVA: 0x0005A5CC File Offset: 0x000587CC
		public static Trace UnifiedMessagingTracer
		{
			get
			{
				if (ExTraceGlobals.unifiedMessagingTracer == null)
				{
					ExTraceGlobals.unifiedMessagingTracer = new Trace(ExTraceGlobals.componentGuid, 18);
				}
				return ExTraceGlobals.unifiedMessagingTracer;
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x060017A9 RID: 6057 RVA: 0x0005A5EB File Offset: 0x000587EB
		public static Trace RPSTracer
		{
			get
			{
				if (ExTraceGlobals.rPSTracer == null)
				{
					ExTraceGlobals.rPSTracer = new Trace(ExTraceGlobals.componentGuid, 19);
				}
				return ExTraceGlobals.rPSTracer;
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x0005A60A File Offset: 0x0005880A
		public static Trace OfficeTracer
		{
			get
			{
				if (ExTraceGlobals.officeTracer == null)
				{
					ExTraceGlobals.officeTracer = new Trace(ExTraceGlobals.componentGuid, 20);
				}
				return ExTraceGlobals.officeTracer;
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x060017AB RID: 6059 RVA: 0x0005A629 File Offset: 0x00058829
		public static Trace IMAP4Tracer
		{
			get
			{
				if (ExTraceGlobals.iMAP4Tracer == null)
				{
					ExTraceGlobals.iMAP4Tracer = new Trace(ExTraceGlobals.componentGuid, 21);
				}
				return ExTraceGlobals.iMAP4Tracer;
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x0005A648 File Offset: 0x00058848
		public static Trace POP3Tracer
		{
			get
			{
				if (ExTraceGlobals.pOP3Tracer == null)
				{
					ExTraceGlobals.pOP3Tracer = new Trace(ExTraceGlobals.componentGuid, 22);
				}
				return ExTraceGlobals.pOP3Tracer;
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x060017AD RID: 6061 RVA: 0x0005A667 File Offset: 0x00058867
		public static Trace SearchTracer
		{
			get
			{
				if (ExTraceGlobals.searchTracer == null)
				{
					ExTraceGlobals.searchTracer = new Trace(ExTraceGlobals.componentGuid, 23);
				}
				return ExTraceGlobals.searchTracer;
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x0005A686 File Offset: 0x00058886
		public static Trace MigrationTracer
		{
			get
			{
				if (ExTraceGlobals.migrationTracer == null)
				{
					ExTraceGlobals.migrationTracer = new Trace(ExTraceGlobals.componentGuid, 24);
				}
				return ExTraceGlobals.migrationTracer;
			}
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x060017AF RID: 6063 RVA: 0x0005A6A5 File Offset: 0x000588A5
		public static Trace DirectoryTracer
		{
			get
			{
				if (ExTraceGlobals.directoryTracer == null)
				{
					ExTraceGlobals.directoryTracer = new Trace(ExTraceGlobals.componentGuid, 25);
				}
				return ExTraceGlobals.directoryTracer;
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x0005A6C4 File Offset: 0x000588C4
		public static Trace HighAvailabilityTracer
		{
			get
			{
				if (ExTraceGlobals.highAvailabilityTracer == null)
				{
					ExTraceGlobals.highAvailabilityTracer = new Trace(ExTraceGlobals.componentGuid, 26);
				}
				return ExTraceGlobals.highAvailabilityTracer;
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x060017B1 RID: 6065 RVA: 0x0005A6E3 File Offset: 0x000588E3
		public static Trace ProvisioningTracer
		{
			get
			{
				if (ExTraceGlobals.provisioningTracer == null)
				{
					ExTraceGlobals.provisioningTracer = new Trace(ExTraceGlobals.componentGuid, 27);
				}
				return ExTraceGlobals.provisioningTracer;
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x060017B2 RID: 6066 RVA: 0x0005A702 File Offset: 0x00058902
		public static Trace TransportTracer
		{
			get
			{
				if (ExTraceGlobals.transportTracer == null)
				{
					ExTraceGlobals.transportTracer = new Trace(ExTraceGlobals.componentGuid, 28);
				}
				return ExTraceGlobals.transportTracer;
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x0005A721 File Offset: 0x00058921
		public static Trace MonitoringTracer
		{
			get
			{
				if (ExTraceGlobals.monitoringTracer == null)
				{
					ExTraceGlobals.monitoringTracer = new Trace(ExTraceGlobals.componentGuid, 29);
				}
				return ExTraceGlobals.monitoringTracer;
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x060017B4 RID: 6068 RVA: 0x0005A740 File Offset: 0x00058940
		public static Trace CalendarSharingTracer
		{
			get
			{
				if (ExTraceGlobals.calendarSharingTracer == null)
				{
					ExTraceGlobals.calendarSharingTracer = new Trace(ExTraceGlobals.componentGuid, 30);
				}
				return ExTraceGlobals.calendarSharingTracer;
			}
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x0005A75F File Offset: 0x0005895F
		public static Trace CafeTracer
		{
			get
			{
				if (ExTraceGlobals.cafeTracer == null)
				{
					ExTraceGlobals.cafeTracer = new Trace(ExTraceGlobals.componentGuid, 31);
				}
				return ExTraceGlobals.cafeTracer;
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x0005A77E File Offset: 0x0005897E
		public static Trace EventAssistantsTracer
		{
			get
			{
				if (ExTraceGlobals.eventAssistantsTracer == null)
				{
					ExTraceGlobals.eventAssistantsTracer = new Trace(ExTraceGlobals.componentGuid, 32);
				}
				return ExTraceGlobals.eventAssistantsTracer;
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x0005A79D File Offset: 0x0005899D
		public static Trace FIPSTracer
		{
			get
			{
				if (ExTraceGlobals.fIPSTracer == null)
				{
					ExTraceGlobals.fIPSTracer = new Trace(ExTraceGlobals.componentGuid, 33);
				}
				return ExTraceGlobals.fIPSTracer;
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x060017B8 RID: 6072 RVA: 0x0005A7BC File Offset: 0x000589BC
		public static Trace AntimalwareTracer
		{
			get
			{
				if (ExTraceGlobals.antimalwareTracer == null)
				{
					ExTraceGlobals.antimalwareTracer = new Trace(ExTraceGlobals.componentGuid, 34);
				}
				return ExTraceGlobals.antimalwareTracer;
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x0005A7DB File Offset: 0x000589DB
		public static Trace TransportSyncTracer
		{
			get
			{
				if (ExTraceGlobals.transportSyncTracer == null)
				{
					ExTraceGlobals.transportSyncTracer = new Trace(ExTraceGlobals.componentGuid, 35);
				}
				return ExTraceGlobals.transportSyncTracer;
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x060017BA RID: 6074 RVA: 0x0005A7FA File Offset: 0x000589FA
		public static Trace ECPTracer
		{
			get
			{
				if (ExTraceGlobals.eCPTracer == null)
				{
					ExTraceGlobals.eCPTracer = new Trace(ExTraceGlobals.componentGuid, 36);
				}
				return ExTraceGlobals.eCPTracer;
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x0005A819 File Offset: 0x00058A19
		public static Trace SecurityTracer
		{
			get
			{
				if (ExTraceGlobals.securityTracer == null)
				{
					ExTraceGlobals.securityTracer = new Trace(ExTraceGlobals.componentGuid, 37);
				}
				return ExTraceGlobals.securityTracer;
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x0005A838 File Offset: 0x00058A38
		public static Trace RWSTracer
		{
			get
			{
				if (ExTraceGlobals.rWSTracer == null)
				{
					ExTraceGlobals.rWSTracer = new Trace(ExTraceGlobals.componentGuid, 38);
				}
				return ExTraceGlobals.rWSTracer;
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x0005A857 File Offset: 0x00058A57
		public static Trace EDSTracer
		{
			get
			{
				if (ExTraceGlobals.eDSTracer == null)
				{
					ExTraceGlobals.eDSTracer = new Trace(ExTraceGlobals.componentGuid, 39);
				}
				return ExTraceGlobals.eDSTracer;
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x0005A876 File Offset: 0x00058A76
		public static Trace ProcessIsolationTracer
		{
			get
			{
				if (ExTraceGlobals.processIsolationTracer == null)
				{
					ExTraceGlobals.processIsolationTracer = new Trace(ExTraceGlobals.componentGuid, 40);
				}
				return ExTraceGlobals.processIsolationTracer;
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x0005A895 File Offset: 0x00058A95
		public static Trace ActiveMonitoringRpcTracer
		{
			get
			{
				if (ExTraceGlobals.activeMonitoringRpcTracer == null)
				{
					ExTraceGlobals.activeMonitoringRpcTracer = new Trace(ExTraceGlobals.componentGuid, 41);
				}
				return ExTraceGlobals.activeMonitoringRpcTracer;
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x0005A8B4 File Offset: 0x00058AB4
		public static Trace RecoveryActionTracer
		{
			get
			{
				if (ExTraceGlobals.recoveryActionTracer == null)
				{
					ExTraceGlobals.recoveryActionTracer = new Trace(ExTraceGlobals.componentGuid, 42);
				}
				return ExTraceGlobals.recoveryActionTracer;
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x0005A8D3 File Offset: 0x00058AD3
		public static Trace GenericRpcTracer
		{
			get
			{
				if (ExTraceGlobals.genericRpcTracer == null)
				{
					ExTraceGlobals.genericRpcTracer = new Trace(ExTraceGlobals.componentGuid, 43);
				}
				return ExTraceGlobals.genericRpcTracer;
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x060017C2 RID: 6082 RVA: 0x0005A8F2 File Offset: 0x00058AF2
		public static Trace MapiSubmitLAMTracer
		{
			get
			{
				if (ExTraceGlobals.mapiSubmitLAMTracer == null)
				{
					ExTraceGlobals.mapiSubmitLAMTracer = new Trace(ExTraceGlobals.componentGuid, 44);
				}
				return ExTraceGlobals.mapiSubmitLAMTracer;
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x060017C3 RID: 6083 RVA: 0x0005A911 File Offset: 0x00058B11
		public static Trace PublicFoldersTracer
		{
			get
			{
				if (ExTraceGlobals.publicFoldersTracer == null)
				{
					ExTraceGlobals.publicFoldersTracer = new Trace(ExTraceGlobals.componentGuid, 45);
				}
				return ExTraceGlobals.publicFoldersTracer;
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x060017C4 RID: 6084 RVA: 0x0005A930 File Offset: 0x00058B30
		public static Trace SiteMailboxTracer
		{
			get
			{
				if (ExTraceGlobals.siteMailboxTracer == null)
				{
					ExTraceGlobals.siteMailboxTracer = new Trace(ExTraceGlobals.componentGuid, 46);
				}
				return ExTraceGlobals.siteMailboxTracer;
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x060017C5 RID: 6085 RVA: 0x0005A94F File Offset: 0x00058B4F
		public static Trace MailboxTransportTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxTransportTracer == null)
				{
					ExTraceGlobals.mailboxTransportTracer = new Trace(ExTraceGlobals.componentGuid, 47);
				}
				return ExTraceGlobals.mailboxTransportTracer;
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x0005A96E File Offset: 0x00058B6E
		public static Trace WACTracer
		{
			get
			{
				if (ExTraceGlobals.wACTracer == null)
				{
					ExTraceGlobals.wACTracer = new Trace(ExTraceGlobals.componentGuid, 48);
				}
				return ExTraceGlobals.wACTracer;
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x060017C7 RID: 6087 RVA: 0x0005A98D File Offset: 0x00058B8D
		public static Trace ClassificationTracer
		{
			get
			{
				if (ExTraceGlobals.classificationTracer == null)
				{
					ExTraceGlobals.classificationTracer = new Trace(ExTraceGlobals.componentGuid, 49);
				}
				return ExTraceGlobals.classificationTracer;
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x0005A9AC File Offset: 0x00058BAC
		public static Trace ResultCacheTracer
		{
			get
			{
				if (ExTraceGlobals.resultCacheTracer == null)
				{
					ExTraceGlobals.resultCacheTracer = new Trace(ExTraceGlobals.componentGuid, 50);
				}
				return ExTraceGlobals.resultCacheTracer;
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x060017C9 RID: 6089 RVA: 0x0005A9CB File Offset: 0x00058BCB
		public static Trace CentralAdminTracer
		{
			get
			{
				if (ExTraceGlobals.centralAdminTracer == null)
				{
					ExTraceGlobals.centralAdminTracer = new Trace(ExTraceGlobals.componentGuid, 51);
				}
				return ExTraceGlobals.centralAdminTracer;
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x0005A9EA File Offset: 0x00058BEA
		public static Trace DeploymentTracer
		{
			get
			{
				if (ExTraceGlobals.deploymentTracer == null)
				{
					ExTraceGlobals.deploymentTracer = new Trace(ExTraceGlobals.componentGuid, 52);
				}
				return ExTraceGlobals.deploymentTracer;
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x060017CB RID: 6091 RVA: 0x0005AA09 File Offset: 0x00058C09
		public static Trace HDPhotoTracer
		{
			get
			{
				if (ExTraceGlobals.hDPhotoTracer == null)
				{
					ExTraceGlobals.hDPhotoTracer = new Trace(ExTraceGlobals.componentGuid, 54);
				}
				return ExTraceGlobals.hDPhotoTracer;
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x0005AA28 File Offset: 0x00058C28
		public static Trace RBATracer
		{
			get
			{
				if (ExTraceGlobals.rBATracer == null)
				{
					ExTraceGlobals.rBATracer = new Trace(ExTraceGlobals.componentGuid, 55);
				}
				return ExTraceGlobals.rBATracer;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x060017CD RID: 6093 RVA: 0x0005AA47 File Offset: 0x00058C47
		public static Trace UserThrottlingTracer
		{
			get
			{
				if (ExTraceGlobals.userThrottlingTracer == null)
				{
					ExTraceGlobals.userThrottlingTracer = new Trace(ExTraceGlobals.componentGuid, 56);
				}
				return ExTraceGlobals.userThrottlingTracer;
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x060017CE RID: 6094 RVA: 0x0005AA66 File Offset: 0x00058C66
		public static Trace InferenceTracer
		{
			get
			{
				if (ExTraceGlobals.inferenceTracer == null)
				{
					ExTraceGlobals.inferenceTracer = new Trace(ExTraceGlobals.componentGuid, 57);
				}
				return ExTraceGlobals.inferenceTracer;
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x060017CF RID: 6095 RVA: 0x0005AA85 File Offset: 0x00058C85
		public static Trace PswsTracer
		{
			get
			{
				if (ExTraceGlobals.pswsTracer == null)
				{
					ExTraceGlobals.pswsTracer = new Trace(ExTraceGlobals.componentGuid, 58);
				}
				return ExTraceGlobals.pswsTracer;
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x0005AAA4 File Offset: 0x00058CA4
		public static Trace PeopleConnectTracer
		{
			get
			{
				if (ExTraceGlobals.peopleConnectTracer == null)
				{
					ExTraceGlobals.peopleConnectTracer = new Trace(ExTraceGlobals.componentGuid, 59);
				}
				return ExTraceGlobals.peopleConnectTracer;
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x060017D1 RID: 6097 RVA: 0x0005AAC3 File Offset: 0x00058CC3
		public static Trace CrossPremiseTracer
		{
			get
			{
				if (ExTraceGlobals.crossPremiseTracer == null)
				{
					ExTraceGlobals.crossPremiseTracer = new Trace(ExTraceGlobals.componentGuid, 60);
				}
				return ExTraceGlobals.crossPremiseTracer;
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x0005AAE2 File Offset: 0x00058CE2
		public static Trace E15InterruptionTracer
		{
			get
			{
				if (ExTraceGlobals.e15InterruptionTracer == null)
				{
					ExTraceGlobals.e15InterruptionTracer = new Trace(ExTraceGlobals.componentGuid, 61);
				}
				return ExTraceGlobals.e15InterruptionTracer;
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x060017D3 RID: 6099 RVA: 0x0005AB01 File Offset: 0x00058D01
		public static Trace FEPTracer
		{
			get
			{
				if (ExTraceGlobals.fEPTracer == null)
				{
					ExTraceGlobals.fEPTracer = new Trace(ExTraceGlobals.componentGuid, 62);
				}
				return ExTraceGlobals.fEPTracer;
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x0005AB20 File Offset: 0x00058D20
		public static Trace EdiscoveryTracer
		{
			get
			{
				if (ExTraceGlobals.ediscoveryTracer == null)
				{
					ExTraceGlobals.ediscoveryTracer = new Trace(ExTraceGlobals.componentGuid, 63);
				}
				return ExTraceGlobals.ediscoveryTracer;
			}
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x060017D5 RID: 6101 RVA: 0x0005AB3F File Offset: 0x00058D3F
		public static Trace OnlineMeetingTracer
		{
			get
			{
				if (ExTraceGlobals.onlineMeetingTracer == null)
				{
					ExTraceGlobals.onlineMeetingTracer = new Trace(ExTraceGlobals.componentGuid, 64);
				}
				return ExTraceGlobals.onlineMeetingTracer;
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x0005AB5E File Offset: 0x00058D5E
		public static Trace MailboxSpaceTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxSpaceTracer == null)
				{
					ExTraceGlobals.mailboxSpaceTracer = new Trace(ExTraceGlobals.componentGuid, 65);
				}
				return ExTraceGlobals.mailboxSpaceTracer;
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x060017D7 RID: 6103 RVA: 0x0005AB7D File Offset: 0x00058D7D
		public static Trace PushNotificationTracer
		{
			get
			{
				if (ExTraceGlobals.pushNotificationTracer == null)
				{
					ExTraceGlobals.pushNotificationTracer = new Trace(ExTraceGlobals.componentGuid, 66);
				}
				return ExTraceGlobals.pushNotificationTracer;
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x0005AB9C File Offset: 0x00058D9C
		public static Trace E4ETracer
		{
			get
			{
				if (ExTraceGlobals.e4ETracer == null)
				{
					ExTraceGlobals.e4ETracer = new Trace(ExTraceGlobals.componentGuid, 67);
				}
				return ExTraceGlobals.e4ETracer;
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x060017D9 RID: 6105 RVA: 0x0005ABBB File Offset: 0x00058DBB
		public static Trace LockBoxTracer
		{
			get
			{
				if (ExTraceGlobals.lockBoxTracer == null)
				{
					ExTraceGlobals.lockBoxTracer = new Trace(ExTraceGlobals.componentGuid, 68);
				}
				return ExTraceGlobals.lockBoxTracer;
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x0005ABDA File Offset: 0x00058DDA
		public static Trace PersistentStateTracer
		{
			get
			{
				if (ExTraceGlobals.persistentStateTracer == null)
				{
					ExTraceGlobals.persistentStateTracer = new Trace(ExTraceGlobals.componentGuid, 69);
				}
				return ExTraceGlobals.persistentStateTracer;
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x060017DB RID: 6107 RVA: 0x0005ABF9 File Offset: 0x00058DF9
		public static Trace AuditTracer
		{
			get
			{
				if (ExTraceGlobals.auditTracer == null)
				{
					ExTraceGlobals.auditTracer = new Trace(ExTraceGlobals.componentGuid, 70);
				}
				return ExTraceGlobals.auditTracer;
			}
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x060017DC RID: 6108 RVA: 0x0005AC18 File Offset: 0x00058E18
		public static Trace EndpointTracer
		{
			get
			{
				if (ExTraceGlobals.endpointTracer == null)
				{
					ExTraceGlobals.endpointTracer = new Trace(ExTraceGlobals.componentGuid, 71);
				}
				return ExTraceGlobals.endpointTracer;
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x060017DD RID: 6109 RVA: 0x0005AC37 File Offset: 0x00058E37
		public static Trace EndpointMaintenanceTracer
		{
			get
			{
				if (ExTraceGlobals.endpointMaintenanceTracer == null)
				{
					ExTraceGlobals.endpointMaintenanceTracer = new Trace(ExTraceGlobals.componentGuid, 72);
				}
				return ExTraceGlobals.endpointMaintenanceTracer;
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x060017DE RID: 6110 RVA: 0x0005AC56 File Offset: 0x00058E56
		public static Trace MonitoringEndpointTracer
		{
			get
			{
				if (ExTraceGlobals.monitoringEndpointTracer == null)
				{
					ExTraceGlobals.monitoringEndpointTracer = new Trace(ExTraceGlobals.componentGuid, 73);
				}
				return ExTraceGlobals.monitoringEndpointTracer;
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x060017DF RID: 6111 RVA: 0x0005AC75 File Offset: 0x00058E75
		public static Trace LocalEndpointManagerTracer
		{
			get
			{
				if (ExTraceGlobals.localEndpointManagerTracer == null)
				{
					ExTraceGlobals.localEndpointManagerTracer = new Trace(ExTraceGlobals.componentGuid, 74);
				}
				return ExTraceGlobals.localEndpointManagerTracer;
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x060017E0 RID: 6112 RVA: 0x0005AC94 File Offset: 0x00058E94
		public static Trace MailboxDatabaseEndpointTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxDatabaseEndpointTracer == null)
				{
					ExTraceGlobals.mailboxDatabaseEndpointTracer = new Trace(ExTraceGlobals.componentGuid, 75);
				}
				return ExTraceGlobals.mailboxDatabaseEndpointTracer;
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x060017E1 RID: 6113 RVA: 0x0005ACB3 File Offset: 0x00058EB3
		public static Trace OfflineAddressBookEndpointTracer
		{
			get
			{
				if (ExTraceGlobals.offlineAddressBookEndpointTracer == null)
				{
					ExTraceGlobals.offlineAddressBookEndpointTracer = new Trace(ExTraceGlobals.componentGuid, 76);
				}
				return ExTraceGlobals.offlineAddressBookEndpointTracer;
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x060017E2 RID: 6114 RVA: 0x0005ACD2 File Offset: 0x00058ED2
		public static Trace OverrideEndpointTracer
		{
			get
			{
				if (ExTraceGlobals.overrideEndpointTracer == null)
				{
					ExTraceGlobals.overrideEndpointTracer = new Trace(ExTraceGlobals.componentGuid, 77);
				}
				return ExTraceGlobals.overrideEndpointTracer;
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x060017E3 RID: 6115 RVA: 0x0005ACF1 File Offset: 0x00058EF1
		public static Trace RecoveryActionsEnabledEndpointTracer
		{
			get
			{
				if (ExTraceGlobals.recoveryActionsEnabledEndpointTracer == null)
				{
					ExTraceGlobals.recoveryActionsEnabledEndpointTracer = new Trace(ExTraceGlobals.componentGuid, 78);
				}
				return ExTraceGlobals.recoveryActionsEnabledEndpointTracer;
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x0005AD10 File Offset: 0x00058F10
		public static Trace ExchangeServerRoleEndpointTracer
		{
			get
			{
				if (ExTraceGlobals.exchangeServerRoleEndpointTracer == null)
				{
					ExTraceGlobals.exchangeServerRoleEndpointTracer = new Trace(ExTraceGlobals.componentGuid, 79);
				}
				return ExTraceGlobals.exchangeServerRoleEndpointTracer;
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x060017E5 RID: 6117 RVA: 0x0005AD2F File Offset: 0x00058F2F
		public static Trace SubjectListEndpointTracer
		{
			get
			{
				if (ExTraceGlobals.subjectListEndpointTracer == null)
				{
					ExTraceGlobals.subjectListEndpointTracer = new Trace(ExTraceGlobals.componentGuid, 80);
				}
				return ExTraceGlobals.subjectListEndpointTracer;
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x0005AD4E File Offset: 0x00058F4E
		public static Trace UnifiedMessagingEndpointTracer
		{
			get
			{
				if (ExTraceGlobals.unifiedMessagingEndpointTracer == null)
				{
					ExTraceGlobals.unifiedMessagingEndpointTracer = new Trace(ExTraceGlobals.componentGuid, 81);
				}
				return ExTraceGlobals.unifiedMessagingEndpointTracer;
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x0005AD6D File Offset: 0x00058F6D
		public static Trace WindowsServerRoleEndpointTracer
		{
			get
			{
				if (ExTraceGlobals.windowsServerRoleEndpointTracer == null)
				{
					ExTraceGlobals.windowsServerRoleEndpointTracer = new Trace(ExTraceGlobals.componentGuid, 82);
				}
				return ExTraceGlobals.windowsServerRoleEndpointTracer;
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x060017E8 RID: 6120 RVA: 0x0005AD8C File Offset: 0x00058F8C
		public static Trace ScopeMappingLocalEndpointTracer
		{
			get
			{
				if (ExTraceGlobals.scopeMappingLocalEndpointTracer == null)
				{
					ExTraceGlobals.scopeMappingLocalEndpointTracer = new Trace(ExTraceGlobals.componentGuid, 83);
				}
				return ExTraceGlobals.scopeMappingLocalEndpointTracer;
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x0005ADAB File Offset: 0x00058FAB
		public static Trace TimeBasedAssistantsTracer
		{
			get
			{
				if (ExTraceGlobals.timeBasedAssistantsTracer == null)
				{
					ExTraceGlobals.timeBasedAssistantsTracer = new Trace(ExTraceGlobals.componentGuid, 84);
				}
				return ExTraceGlobals.timeBasedAssistantsTracer;
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x060017EA RID: 6122 RVA: 0x0005ADCA File Offset: 0x00058FCA
		public static Trace RemoteStoreTracer
		{
			get
			{
				if (ExTraceGlobals.remoteStoreTracer == null)
				{
					ExTraceGlobals.remoteStoreTracer = new Trace(ExTraceGlobals.componentGuid, 85);
				}
				return ExTraceGlobals.remoteStoreTracer;
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x0005ADE9 File Offset: 0x00058FE9
		public static Trace WasclTracer
		{
			get
			{
				if (ExTraceGlobals.wasclTracer == null)
				{
					ExTraceGlobals.wasclTracer = new Trace(ExTraceGlobals.componentGuid, 86);
				}
				return ExTraceGlobals.wasclTracer;
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x060017EC RID: 6124 RVA: 0x0005AE08 File Offset: 0x00059008
		public static Trace GenericRusTracer
		{
			get
			{
				if (ExTraceGlobals.genericRusTracer == null)
				{
					ExTraceGlobals.genericRusTracer = new Trace(ExTraceGlobals.componentGuid, 87);
				}
				return ExTraceGlobals.genericRusTracer;
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x0005AE27 File Offset: 0x00059027
		public static Trace OfficeGraphTracer
		{
			get
			{
				if (ExTraceGlobals.officeGraphTracer == null)
				{
					ExTraceGlobals.officeGraphTracer = new Trace(ExTraceGlobals.componentGuid, 88);
				}
				return ExTraceGlobals.officeGraphTracer;
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x0005AE46 File Offset: 0x00059046
		public static Trace PUMTracer
		{
			get
			{
				if (ExTraceGlobals.pUMTracer == null)
				{
					ExTraceGlobals.pUMTracer = new Trace(ExTraceGlobals.componentGuid, 89);
				}
				return ExTraceGlobals.pUMTracer;
			}
		}

		// Token: 0x04001C44 RID: 7236
		private static Guid componentGuid = new Guid("EAF36C57-87B9-4D84-B551-3537A14A62B9");

		// Token: 0x04001C45 RID: 7237
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001C46 RID: 7238
		private static Trace azureTracer = null;

		// Token: 0x04001C47 RID: 7239
		private static Trace commonComponentsTracer = null;

		// Token: 0x04001C48 RID: 7240
		private static Trace heartbeatTracer = null;

		// Token: 0x04001C49 RID: 7241
		private static Trace hTTPTracer = null;

		// Token: 0x04001C4A RID: 7242
		private static Trace oWATracer = null;

		// Token: 0x04001C4B RID: 7243
		private static Trace rPCHTTPTracer = null;

		// Token: 0x04001C4C RID: 7244
		private static Trace activeSyncTracer = null;

		// Token: 0x04001C4D RID: 7245
		private static Trace eWSTracer = null;

		// Token: 0x04001C4E RID: 7246
		private static Trace autoDiscoverTracer = null;

		// Token: 0x04001C4F RID: 7247
		private static Trace liveIdTracer = null;

		// Token: 0x04001C50 RID: 7248
		private static Trace rIMTracer = null;

		// Token: 0x04001C51 RID: 7249
		private static Trace serviceTracer = null;

		// Token: 0x04001C52 RID: 7250
		private static Trace workerTracer = null;

		// Token: 0x04001C53 RID: 7251
		private static Trace oABTracer = null;

		// Token: 0x04001C54 RID: 7252
		private static Trace storeTracer = null;

		// Token: 0x04001C55 RID: 7253
		private static Trace availabilityServiceTracer = null;

		// Token: 0x04001C56 RID: 7254
		private static Trace networkTracer = null;

		// Token: 0x04001C57 RID: 7255
		private static Trace unifiedMessagingTracer = null;

		// Token: 0x04001C58 RID: 7256
		private static Trace rPSTracer = null;

		// Token: 0x04001C59 RID: 7257
		private static Trace officeTracer = null;

		// Token: 0x04001C5A RID: 7258
		private static Trace iMAP4Tracer = null;

		// Token: 0x04001C5B RID: 7259
		private static Trace pOP3Tracer = null;

		// Token: 0x04001C5C RID: 7260
		private static Trace searchTracer = null;

		// Token: 0x04001C5D RID: 7261
		private static Trace migrationTracer = null;

		// Token: 0x04001C5E RID: 7262
		private static Trace directoryTracer = null;

		// Token: 0x04001C5F RID: 7263
		private static Trace highAvailabilityTracer = null;

		// Token: 0x04001C60 RID: 7264
		private static Trace provisioningTracer = null;

		// Token: 0x04001C61 RID: 7265
		private static Trace transportTracer = null;

		// Token: 0x04001C62 RID: 7266
		private static Trace monitoringTracer = null;

		// Token: 0x04001C63 RID: 7267
		private static Trace calendarSharingTracer = null;

		// Token: 0x04001C64 RID: 7268
		private static Trace cafeTracer = null;

		// Token: 0x04001C65 RID: 7269
		private static Trace eventAssistantsTracer = null;

		// Token: 0x04001C66 RID: 7270
		private static Trace fIPSTracer = null;

		// Token: 0x04001C67 RID: 7271
		private static Trace antimalwareTracer = null;

		// Token: 0x04001C68 RID: 7272
		private static Trace transportSyncTracer = null;

		// Token: 0x04001C69 RID: 7273
		private static Trace eCPTracer = null;

		// Token: 0x04001C6A RID: 7274
		private static Trace securityTracer = null;

		// Token: 0x04001C6B RID: 7275
		private static Trace rWSTracer = null;

		// Token: 0x04001C6C RID: 7276
		private static Trace eDSTracer = null;

		// Token: 0x04001C6D RID: 7277
		private static Trace processIsolationTracer = null;

		// Token: 0x04001C6E RID: 7278
		private static Trace activeMonitoringRpcTracer = null;

		// Token: 0x04001C6F RID: 7279
		private static Trace recoveryActionTracer = null;

		// Token: 0x04001C70 RID: 7280
		private static Trace genericRpcTracer = null;

		// Token: 0x04001C71 RID: 7281
		private static Trace mapiSubmitLAMTracer = null;

		// Token: 0x04001C72 RID: 7282
		private static Trace publicFoldersTracer = null;

		// Token: 0x04001C73 RID: 7283
		private static Trace siteMailboxTracer = null;

		// Token: 0x04001C74 RID: 7284
		private static Trace mailboxTransportTracer = null;

		// Token: 0x04001C75 RID: 7285
		private static Trace wACTracer = null;

		// Token: 0x04001C76 RID: 7286
		private static Trace classificationTracer = null;

		// Token: 0x04001C77 RID: 7287
		private static Trace resultCacheTracer = null;

		// Token: 0x04001C78 RID: 7288
		private static Trace centralAdminTracer = null;

		// Token: 0x04001C79 RID: 7289
		private static Trace deploymentTracer = null;

		// Token: 0x04001C7A RID: 7290
		private static Trace hDPhotoTracer = null;

		// Token: 0x04001C7B RID: 7291
		private static Trace rBATracer = null;

		// Token: 0x04001C7C RID: 7292
		private static Trace userThrottlingTracer = null;

		// Token: 0x04001C7D RID: 7293
		private static Trace inferenceTracer = null;

		// Token: 0x04001C7E RID: 7294
		private static Trace pswsTracer = null;

		// Token: 0x04001C7F RID: 7295
		private static Trace peopleConnectTracer = null;

		// Token: 0x04001C80 RID: 7296
		private static Trace crossPremiseTracer = null;

		// Token: 0x04001C81 RID: 7297
		private static Trace e15InterruptionTracer = null;

		// Token: 0x04001C82 RID: 7298
		private static Trace fEPTracer = null;

		// Token: 0x04001C83 RID: 7299
		private static Trace ediscoveryTracer = null;

		// Token: 0x04001C84 RID: 7300
		private static Trace onlineMeetingTracer = null;

		// Token: 0x04001C85 RID: 7301
		private static Trace mailboxSpaceTracer = null;

		// Token: 0x04001C86 RID: 7302
		private static Trace pushNotificationTracer = null;

		// Token: 0x04001C87 RID: 7303
		private static Trace e4ETracer = null;

		// Token: 0x04001C88 RID: 7304
		private static Trace lockBoxTracer = null;

		// Token: 0x04001C89 RID: 7305
		private static Trace persistentStateTracer = null;

		// Token: 0x04001C8A RID: 7306
		private static Trace auditTracer = null;

		// Token: 0x04001C8B RID: 7307
		private static Trace endpointTracer = null;

		// Token: 0x04001C8C RID: 7308
		private static Trace endpointMaintenanceTracer = null;

		// Token: 0x04001C8D RID: 7309
		private static Trace monitoringEndpointTracer = null;

		// Token: 0x04001C8E RID: 7310
		private static Trace localEndpointManagerTracer = null;

		// Token: 0x04001C8F RID: 7311
		private static Trace mailboxDatabaseEndpointTracer = null;

		// Token: 0x04001C90 RID: 7312
		private static Trace offlineAddressBookEndpointTracer = null;

		// Token: 0x04001C91 RID: 7313
		private static Trace overrideEndpointTracer = null;

		// Token: 0x04001C92 RID: 7314
		private static Trace recoveryActionsEnabledEndpointTracer = null;

		// Token: 0x04001C93 RID: 7315
		private static Trace exchangeServerRoleEndpointTracer = null;

		// Token: 0x04001C94 RID: 7316
		private static Trace subjectListEndpointTracer = null;

		// Token: 0x04001C95 RID: 7317
		private static Trace unifiedMessagingEndpointTracer = null;

		// Token: 0x04001C96 RID: 7318
		private static Trace windowsServerRoleEndpointTracer = null;

		// Token: 0x04001C97 RID: 7319
		private static Trace scopeMappingLocalEndpointTracer = null;

		// Token: 0x04001C98 RID: 7320
		private static Trace timeBasedAssistantsTracer = null;

		// Token: 0x04001C99 RID: 7321
		private static Trace remoteStoreTracer = null;

		// Token: 0x04001C9A RID: 7322
		private static Trace wasclTracer = null;

		// Token: 0x04001C9B RID: 7323
		private static Trace genericRusTracer = null;

		// Token: 0x04001C9C RID: 7324
		private static Trace officeGraphTracer = null;

		// Token: 0x04001C9D RID: 7325
		private static Trace pUMTracer = null;
	}
}
