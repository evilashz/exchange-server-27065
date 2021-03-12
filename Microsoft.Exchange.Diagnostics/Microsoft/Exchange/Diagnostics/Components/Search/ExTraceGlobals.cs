using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.Search
{
	// Token: 0x02000360 RID: 864
	public static class ExTraceGlobals
	{
		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x0005270C File Offset: 0x0005090C
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

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x0005272A File Offset: 0x0005092A
		public static Trace CatchUpNotificationCrawlerTracer
		{
			get
			{
				if (ExTraceGlobals.catchUpNotificationCrawlerTracer == null)
				{
					ExTraceGlobals.catchUpNotificationCrawlerTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.catchUpNotificationCrawlerTracer;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x00052748 File Offset: 0x00050948
		public static Trace ChunkSourceFunctionsTracer
		{
			get
			{
				if (ExTraceGlobals.chunkSourceFunctionsTracer == null)
				{
					ExTraceGlobals.chunkSourceFunctionsTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.chunkSourceFunctionsTracer;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x00052766 File Offset: 0x00050966
		public static Trace CrawlerTracer
		{
			get
			{
				if (ExTraceGlobals.crawlerTracer == null)
				{
					ExTraceGlobals.crawlerTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.crawlerTracer;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x00052784 File Offset: 0x00050984
		public static Trace CSrchProjectTracer
		{
			get
			{
				if (ExTraceGlobals.cSrchProjectTracer == null)
				{
					ExTraceGlobals.cSrchProjectTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.cSrchProjectTracer;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x000527A2 File Offset: 0x000509A2
		public static Trace DataSourceFunctionsTracer
		{
			get
			{
				if (ExTraceGlobals.dataSourceFunctionsTracer == null)
				{
					ExTraceGlobals.dataSourceFunctionsTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.dataSourceFunctionsTracer;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x000527C0 File Offset: 0x000509C0
		public static Trace DriverTracer
		{
			get
			{
				if (ExTraceGlobals.driverTracer == null)
				{
					ExTraceGlobals.driverTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.driverTracer;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x000527DE File Offset: 0x000509DE
		public static Trace FilterEnumeratorTracer
		{
			get
			{
				if (ExTraceGlobals.filterEnumeratorTracer == null)
				{
					ExTraceGlobals.filterEnumeratorTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.filterEnumeratorTracer;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x000527FC File Offset: 0x000509FC
		public static Trace FTEAdminComInteropTracer
		{
			get
			{
				if (ExTraceGlobals.fTEAdminComInteropTracer == null)
				{
					ExTraceGlobals.fTEAdminComInteropTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.fTEAdminComInteropTracer;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060013F8 RID: 5112 RVA: 0x0005281A File Offset: 0x00050A1A
		public static Trace IndexablePropertyCacheTracer
		{
			get
			{
				if (ExTraceGlobals.indexablePropertyCacheTracer == null)
				{
					ExTraceGlobals.indexablePropertyCacheTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.indexablePropertyCacheTracer;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x00052839 File Offset: 0x00050A39
		public static Trace MapiIteratorTracer
		{
			get
			{
				if (ExTraceGlobals.mapiIteratorTracer == null)
				{
					ExTraceGlobals.mapiIteratorTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.mapiIteratorTracer;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x00052858 File Offset: 0x00050A58
		public static Trace NotificationProcessingTracer
		{
			get
			{
				if (ExTraceGlobals.notificationProcessingTracer == null)
				{
					ExTraceGlobals.notificationProcessingTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.notificationProcessingTracer;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x00052877 File Offset: 0x00050A77
		public static Trace NotificationQueueTracer
		{
			get
			{
				if (ExTraceGlobals.notificationQueueTracer == null)
				{
					ExTraceGlobals.notificationQueueTracer = new Trace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.notificationQueueTracer;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x00052896 File Offset: 0x00050A96
		public static Trace NotificationWatcherTracer
		{
			get
			{
				if (ExTraceGlobals.notificationWatcherTracer == null)
				{
					ExTraceGlobals.notificationWatcherTracer = new Trace(ExTraceGlobals.componentGuid, 13);
				}
				return ExTraceGlobals.notificationWatcherTracer;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x000528B5 File Offset: 0x00050AB5
		public static Trace PHFunctionsTracer
		{
			get
			{
				if (ExTraceGlobals.pHFunctionsTracer == null)
				{
					ExTraceGlobals.pHFunctionsTracer = new Trace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.pHFunctionsTracer;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x000528D4 File Offset: 0x00050AD4
		public static Trace RetryEngineTracer
		{
			get
			{
				if (ExTraceGlobals.retryEngineTracer == null)
				{
					ExTraceGlobals.retryEngineTracer = new Trace(ExTraceGlobals.componentGuid, 16);
				}
				return ExTraceGlobals.retryEngineTracer;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x000528F3 File Offset: 0x00050AF3
		public static Trace ThrottleControllerTracer
		{
			get
			{
				if (ExTraceGlobals.throttleControllerTracer == null)
				{
					ExTraceGlobals.throttleControllerTracer = new Trace(ExTraceGlobals.componentGuid, 17);
				}
				return ExTraceGlobals.throttleControllerTracer;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x00052912 File Offset: 0x00050B12
		public static Trace PropertyStoreCacheTracer
		{
			get
			{
				if (ExTraceGlobals.propertyStoreCacheTracer == null)
				{
					ExTraceGlobals.propertyStoreCacheTracer = new Trace(ExTraceGlobals.componentGuid, 18);
				}
				return ExTraceGlobals.propertyStoreCacheTracer;
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x00052931 File Offset: 0x00050B31
		public static Trace ActiveManagerTracer
		{
			get
			{
				if (ExTraceGlobals.activeManagerTracer == null)
				{
					ExTraceGlobals.activeManagerTracer = new Trace(ExTraceGlobals.componentGuid, 19);
				}
				return ExTraceGlobals.activeManagerTracer;
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x00052950 File Offset: 0x00050B50
		public static Trace CatalogHealthTracer
		{
			get
			{
				if (ExTraceGlobals.catalogHealthTracer == null)
				{
					ExTraceGlobals.catalogHealthTracer = new Trace(ExTraceGlobals.componentGuid, 20);
				}
				return ExTraceGlobals.catalogHealthTracer;
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x0005296F File Offset: 0x00050B6F
		public static Trace SearchCatalogClientTracer
		{
			get
			{
				if (ExTraceGlobals.searchCatalogClientTracer == null)
				{
					ExTraceGlobals.searchCatalogClientTracer = new Trace(ExTraceGlobals.componentGuid, 21);
				}
				return ExTraceGlobals.searchCatalogClientTracer;
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x0005298E File Offset: 0x00050B8E
		public static Trace SearchCatalogServerTracer
		{
			get
			{
				if (ExTraceGlobals.searchCatalogServerTracer == null)
				{
					ExTraceGlobals.searchCatalogServerTracer = new Trace(ExTraceGlobals.componentGuid, 22);
				}
				return ExTraceGlobals.searchCatalogServerTracer;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x000529AD File Offset: 0x00050BAD
		public static Trace MailboxDeletionTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxDeletionTracer == null)
				{
					ExTraceGlobals.mailboxDeletionTracer = new Trace(ExTraceGlobals.componentGuid, 23);
				}
				return ExTraceGlobals.mailboxDeletionTracer;
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x000529CC File Offset: 0x00050BCC
		public static Trace PropertyStoreTracer
		{
			get
			{
				if (ExTraceGlobals.propertyStoreTracer == null)
				{
					ExTraceGlobals.propertyStoreTracer = new Trace(ExTraceGlobals.componentGuid, 24);
				}
				return ExTraceGlobals.propertyStoreTracer;
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x000529EB File Offset: 0x00050BEB
		public static Trace StoreMonitorTracer
		{
			get
			{
				if (ExTraceGlobals.storeMonitorTracer == null)
				{
					ExTraceGlobals.storeMonitorTracer = new Trace(ExTraceGlobals.componentGuid, 25);
				}
				return ExTraceGlobals.storeMonitorTracer;
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x00052A0A File Offset: 0x00050C0A
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 26);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x00052A29 File Offset: 0x00050C29
		public static Trace MailboxIndexingHelperTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxIndexingHelperTracer == null)
				{
					ExTraceGlobals.mailboxIndexingHelperTracer = new Trace(ExTraceGlobals.componentGuid, 27);
				}
				return ExTraceGlobals.mailboxIndexingHelperTracer;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x00052A48 File Offset: 0x00050C48
		public static Trace CatalogStateTracer
		{
			get
			{
				if (ExTraceGlobals.catalogStateTracer == null)
				{
					ExTraceGlobals.catalogStateTracer = new Trace(ExTraceGlobals.componentGuid, 28);
				}
				return ExTraceGlobals.catalogStateTracer;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x00052A67 File Offset: 0x00050C67
		public static Trace FileExtensionCacheTracer
		{
			get
			{
				if (ExTraceGlobals.fileExtensionCacheTracer == null)
				{
					ExTraceGlobals.fileExtensionCacheTracer = new Trace(ExTraceGlobals.componentGuid, 29);
				}
				return ExTraceGlobals.fileExtensionCacheTracer;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x00052A86 File Offset: 0x00050C86
		public static Trace MsFteSqlMonitorTracer
		{
			get
			{
				if (ExTraceGlobals.msFteSqlMonitorTracer == null)
				{
					ExTraceGlobals.msFteSqlMonitorTracer = new Trace(ExTraceGlobals.componentGuid, 30);
				}
				return ExTraceGlobals.msFteSqlMonitorTracer;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x00052AA5 File Offset: 0x00050CA5
		public static Trace ServerConnectionsTracer
		{
			get
			{
				if (ExTraceGlobals.serverConnectionsTracer == null)
				{
					ExTraceGlobals.serverConnectionsTracer = new Trace(ExTraceGlobals.componentGuid, 31);
				}
				return ExTraceGlobals.serverConnectionsTracer;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x00052AC4 File Offset: 0x00050CC4
		public static Trace LogonCacheTracer
		{
			get
			{
				if (ExTraceGlobals.logonCacheTracer == null)
				{
					ExTraceGlobals.logonCacheTracer = new Trace(ExTraceGlobals.componentGuid, 32);
				}
				return ExTraceGlobals.logonCacheTracer;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x00052AE3 File Offset: 0x00050CE3
		public static Trace LogonTracer
		{
			get
			{
				if (ExTraceGlobals.logonTracer == null)
				{
					ExTraceGlobals.logonTracer = new Trace(ExTraceGlobals.componentGuid, 33);
				}
				return ExTraceGlobals.logonTracer;
			}
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06001410 RID: 5136 RVA: 0x00052B02 File Offset: 0x00050D02
		public static Trace CatalogReconcilerTracer
		{
			get
			{
				if (ExTraceGlobals.catalogReconcilerTracer == null)
				{
					ExTraceGlobals.catalogReconcilerTracer = new Trace(ExTraceGlobals.componentGuid, 34);
				}
				return ExTraceGlobals.catalogReconcilerTracer;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x00052B21 File Offset: 0x00050D21
		public static Trace CatalogReconcileResultTracer
		{
			get
			{
				if (ExTraceGlobals.catalogReconcileResultTracer == null)
				{
					ExTraceGlobals.catalogReconcileResultTracer = new Trace(ExTraceGlobals.componentGuid, 35);
				}
				return ExTraceGlobals.catalogReconcileResultTracer;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x00052B40 File Offset: 0x00050D40
		public static Trace AllCatalogReconcilerTracer
		{
			get
			{
				if (ExTraceGlobals.allCatalogReconcilerTracer == null)
				{
					ExTraceGlobals.allCatalogReconcilerTracer = new Trace(ExTraceGlobals.componentGuid, 36);
				}
				return ExTraceGlobals.allCatalogReconcilerTracer;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x00052B5F File Offset: 0x00050D5F
		public static Trace MailboxReconcileResultTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxReconcileResultTracer == null)
				{
					ExTraceGlobals.mailboxReconcileResultTracer = new Trace(ExTraceGlobals.componentGuid, 37);
				}
				return ExTraceGlobals.mailboxReconcileResultTracer;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x00052B7E File Offset: 0x00050D7E
		public static Trace NewFilterMonitorTracer
		{
			get
			{
				if (ExTraceGlobals.newFilterMonitorTracer == null)
				{
					ExTraceGlobals.newFilterMonitorTracer = new Trace(ExTraceGlobals.componentGuid, 38);
				}
				return ExTraceGlobals.newFilterMonitorTracer;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x00052B9D File Offset: 0x00050D9D
		public static Trace InMemoryDefaultTracer
		{
			get
			{
				if (ExTraceGlobals.inMemoryDefaultTracer == null)
				{
					ExTraceGlobals.inMemoryDefaultTracer = new Trace(ExTraceGlobals.componentGuid, 39);
				}
				return ExTraceGlobals.inMemoryDefaultTracer;
			}
		}

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x00052BBC File Offset: 0x00050DBC
		public static Trace TestExchangeSearchTracer
		{
			get
			{
				if (ExTraceGlobals.testExchangeSearchTracer == null)
				{
					ExTraceGlobals.testExchangeSearchTracer = new Trace(ExTraceGlobals.componentGuid, 40);
				}
				return ExTraceGlobals.testExchangeSearchTracer;
			}
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x00052BDB File Offset: 0x00050DDB
		public static Trace BatchThrottlerTracer
		{
			get
			{
				if (ExTraceGlobals.batchThrottlerTracer == null)
				{
					ExTraceGlobals.batchThrottlerTracer = new Trace(ExTraceGlobals.componentGuid, 41);
				}
				return ExTraceGlobals.batchThrottlerTracer;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x00052BFA File Offset: 0x00050DFA
		public static Trace ThrottleParametersTracer
		{
			get
			{
				if (ExTraceGlobals.throttleParametersTracer == null)
				{
					ExTraceGlobals.throttleParametersTracer = new Trace(ExTraceGlobals.componentGuid, 42);
				}
				return ExTraceGlobals.throttleParametersTracer;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x00052C19 File Offset: 0x00050E19
		public static Trace ThrottleDataProviderTracer
		{
			get
			{
				if (ExTraceGlobals.throttleDataProviderTracer == null)
				{
					ExTraceGlobals.throttleDataProviderTracer = new Trace(ExTraceGlobals.componentGuid, 43);
				}
				return ExTraceGlobals.throttleDataProviderTracer;
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x0600141A RID: 5146 RVA: 0x00052C38 File Offset: 0x00050E38
		public static Trace RegistryParameterTracer
		{
			get
			{
				if (ExTraceGlobals.registryParameterTracer == null)
				{
					ExTraceGlobals.registryParameterTracer = new Trace(ExTraceGlobals.componentGuid, 44);
				}
				return ExTraceGlobals.registryParameterTracer;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x00052C57 File Offset: 0x00050E57
		public static Trace LatencySamplerTracer
		{
			get
			{
				if (ExTraceGlobals.latencySamplerTracer == null)
				{
					ExTraceGlobals.latencySamplerTracer = new Trace(ExTraceGlobals.componentGuid, 45);
				}
				return ExTraceGlobals.latencySamplerTracer;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x00052C76 File Offset: 0x00050E76
		public static Trace MovingAverageTracer
		{
			get
			{
				if (ExTraceGlobals.movingAverageTracer == null)
				{
					ExTraceGlobals.movingAverageTracer = new Trace(ExTraceGlobals.componentGuid, 46);
				}
				return ExTraceGlobals.movingAverageTracer;
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x00052C95 File Offset: 0x00050E95
		public static Trace CoreComponentTracer
		{
			get
			{
				if (ExTraceGlobals.coreComponentTracer == null)
				{
					ExTraceGlobals.coreComponentTracer = new Trace(ExTraceGlobals.componentGuid, 50);
				}
				return ExTraceGlobals.coreComponentTracer;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x00052CB4 File Offset: 0x00050EB4
		public static Trace CoreComponentRegistryTracer
		{
			get
			{
				if (ExTraceGlobals.coreComponentRegistryTracer == null)
				{
					ExTraceGlobals.coreComponentRegistryTracer = new Trace(ExTraceGlobals.componentGuid, 51);
				}
				return ExTraceGlobals.coreComponentRegistryTracer;
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x00052CD3 File Offset: 0x00050ED3
		public static Trace CoreGeneralTracer
		{
			get
			{
				if (ExTraceGlobals.coreGeneralTracer == null)
				{
					ExTraceGlobals.coreGeneralTracer = new Trace(ExTraceGlobals.componentGuid, 52);
				}
				return ExTraceGlobals.coreGeneralTracer;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x00052CF2 File Offset: 0x00050EF2
		public static Trace FastFeederTracer
		{
			get
			{
				if (ExTraceGlobals.fastFeederTracer == null)
				{
					ExTraceGlobals.fastFeederTracer = new Trace(ExTraceGlobals.componentGuid, 53);
				}
				return ExTraceGlobals.fastFeederTracer;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x00052D11 File Offset: 0x00050F11
		public static Trace MdbNotificationsFeederTracer
		{
			get
			{
				if (ExTraceGlobals.mdbNotificationsFeederTracer == null)
				{
					ExTraceGlobals.mdbNotificationsFeederTracer = new Trace(ExTraceGlobals.componentGuid, 54);
				}
				return ExTraceGlobals.mdbNotificationsFeederTracer;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x00052D30 File Offset: 0x00050F30
		public static Trace ServiceTracer
		{
			get
			{
				if (ExTraceGlobals.serviceTracer == null)
				{
					ExTraceGlobals.serviceTracer = new Trace(ExTraceGlobals.componentGuid, 55);
				}
				return ExTraceGlobals.serviceTracer;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x00052D4F File Offset: 0x00050F4F
		public static Trace EngineTracer
		{
			get
			{
				if (ExTraceGlobals.engineTracer == null)
				{
					ExTraceGlobals.engineTracer = new Trace(ExTraceGlobals.componentGuid, 56);
				}
				return ExTraceGlobals.engineTracer;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x00052D6E File Offset: 0x00050F6E
		public static Trace MdbFeedingControllerTracer
		{
			get
			{
				if (ExTraceGlobals.mdbFeedingControllerTracer == null)
				{
					ExTraceGlobals.mdbFeedingControllerTracer = new Trace(ExTraceGlobals.componentGuid, 57);
				}
				return ExTraceGlobals.mdbFeedingControllerTracer;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x00052D8D File Offset: 0x00050F8D
		public static Trace IndexManagementTracer
		{
			get
			{
				if (ExTraceGlobals.indexManagementTracer == null)
				{
					ExTraceGlobals.indexManagementTracer = new Trace(ExTraceGlobals.componentGuid, 58);
				}
				return ExTraceGlobals.indexManagementTracer;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x00052DAC File Offset: 0x00050FAC
		public static Trace CoreFailureMonitorTracer
		{
			get
			{
				if (ExTraceGlobals.coreFailureMonitorTracer == null)
				{
					ExTraceGlobals.coreFailureMonitorTracer = new Trace(ExTraceGlobals.componentGuid, 59);
				}
				return ExTraceGlobals.coreFailureMonitorTracer;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x00052DCB File Offset: 0x00050FCB
		public static Trace MdbCrawlerFeederTracer
		{
			get
			{
				if (ExTraceGlobals.mdbCrawlerFeederTracer == null)
				{
					ExTraceGlobals.mdbCrawlerFeederTracer = new Trace(ExTraceGlobals.componentGuid, 60);
				}
				return ExTraceGlobals.mdbCrawlerFeederTracer;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x00052DEA File Offset: 0x00050FEA
		public static Trace MdbDocumentAdapterTracer
		{
			get
			{
				if (ExTraceGlobals.mdbDocumentAdapterTracer == null)
				{
					ExTraceGlobals.mdbDocumentAdapterTracer = new Trace(ExTraceGlobals.componentGuid, 61);
				}
				return ExTraceGlobals.mdbDocumentAdapterTracer;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x00052E09 File Offset: 0x00051009
		public static Trace CoreDocumentModelTracer
		{
			get
			{
				if (ExTraceGlobals.coreDocumentModelTracer == null)
				{
					ExTraceGlobals.coreDocumentModelTracer = new Trace(ExTraceGlobals.componentGuid, 62);
				}
				return ExTraceGlobals.coreDocumentModelTracer;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x00052E28 File Offset: 0x00051028
		public static Trace PipelineLoaderTracer
		{
			get
			{
				if (ExTraceGlobals.pipelineLoaderTracer == null)
				{
					ExTraceGlobals.pipelineLoaderTracer = new Trace(ExTraceGlobals.componentGuid, 63);
				}
				return ExTraceGlobals.pipelineLoaderTracer;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x00052E47 File Offset: 0x00051047
		public static Trace CorePipelineTracer
		{
			get
			{
				if (ExTraceGlobals.corePipelineTracer == null)
				{
					ExTraceGlobals.corePipelineTracer = new Trace(ExTraceGlobals.componentGuid, 64);
				}
				return ExTraceGlobals.corePipelineTracer;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x00052E66 File Offset: 0x00051066
		public static Trace QueueManagerTracer
		{
			get
			{
				if (ExTraceGlobals.queueManagerTracer == null)
				{
					ExTraceGlobals.queueManagerTracer = new Trace(ExTraceGlobals.componentGuid, 65);
				}
				return ExTraceGlobals.queueManagerTracer;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x00052E85 File Offset: 0x00051085
		public static Trace CrawlerWatermarkManagerTracer
		{
			get
			{
				if (ExTraceGlobals.crawlerWatermarkManagerTracer == null)
				{
					ExTraceGlobals.crawlerWatermarkManagerTracer = new Trace(ExTraceGlobals.componentGuid, 66);
				}
				return ExTraceGlobals.crawlerWatermarkManagerTracer;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x00052EA4 File Offset: 0x000510A4
		public static Trace FailedItemStorageTracer
		{
			get
			{
				if (ExTraceGlobals.failedItemStorageTracer == null)
				{
					ExTraceGlobals.failedItemStorageTracer = new Trace(ExTraceGlobals.componentGuid, 67);
				}
				return ExTraceGlobals.failedItemStorageTracer;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x00052EC3 File Offset: 0x000510C3
		public static Trace MdbWatcherTracer
		{
			get
			{
				if (ExTraceGlobals.mdbWatcherTracer == null)
				{
					ExTraceGlobals.mdbWatcherTracer = new Trace(ExTraceGlobals.componentGuid, 68);
				}
				return ExTraceGlobals.mdbWatcherTracer;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x00052EE2 File Offset: 0x000510E2
		public static Trace MdbRetryFeederTracer
		{
			get
			{
				if (ExTraceGlobals.mdbRetryFeederTracer == null)
				{
					ExTraceGlobals.mdbRetryFeederTracer = new Trace(ExTraceGlobals.componentGuid, 69);
				}
				return ExTraceGlobals.mdbRetryFeederTracer;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x00052F01 File Offset: 0x00051101
		public static Trace MdbSessionCacheTracer
		{
			get
			{
				if (ExTraceGlobals.mdbSessionCacheTracer == null)
				{
					ExTraceGlobals.mdbSessionCacheTracer = new Trace(ExTraceGlobals.componentGuid, 70);
				}
				return ExTraceGlobals.mdbSessionCacheTracer;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x00052F20 File Offset: 0x00051120
		public static Trace RetrieverOperatorTracer
		{
			get
			{
				if (ExTraceGlobals.retrieverOperatorTracer == null)
				{
					ExTraceGlobals.retrieverOperatorTracer = new Trace(ExTraceGlobals.componentGuid, 71);
				}
				return ExTraceGlobals.retrieverOperatorTracer;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x00052F3F File Offset: 0x0005113F
		public static Trace StreamManagerTracer
		{
			get
			{
				if (ExTraceGlobals.streamManagerTracer == null)
				{
					ExTraceGlobals.streamManagerTracer = new Trace(ExTraceGlobals.componentGuid, 72);
				}
				return ExTraceGlobals.streamManagerTracer;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x00052F5E File Offset: 0x0005115E
		public static Trace StreamChannelTracer
		{
			get
			{
				if (ExTraceGlobals.streamChannelTracer == null)
				{
					ExTraceGlobals.streamChannelTracer = new Trace(ExTraceGlobals.componentGuid, 73);
				}
				return ExTraceGlobals.streamChannelTracer;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x00052F7D File Offset: 0x0005117D
		public static Trace AnnotationTokenTracer
		{
			get
			{
				if (ExTraceGlobals.annotationTokenTracer == null)
				{
					ExTraceGlobals.annotationTokenTracer = new Trace(ExTraceGlobals.componentGuid, 74);
				}
				return ExTraceGlobals.annotationTokenTracer;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x00052F9C File Offset: 0x0005119C
		public static Trace TransportOperatorTracer
		{
			get
			{
				if (ExTraceGlobals.transportOperatorTracer == null)
				{
					ExTraceGlobals.transportOperatorTracer = new Trace(ExTraceGlobals.componentGuid, 75);
				}
				return ExTraceGlobals.transportOperatorTracer;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x00052FBB File Offset: 0x000511BB
		public static Trace IndexRoutingAgentTracer
		{
			get
			{
				if (ExTraceGlobals.indexRoutingAgentTracer == null)
				{
					ExTraceGlobals.indexRoutingAgentTracer = new Trace(ExTraceGlobals.componentGuid, 76);
				}
				return ExTraceGlobals.indexRoutingAgentTracer;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x00052FDA File Offset: 0x000511DA
		public static Trace IndexDeliveryAgentTracer
		{
			get
			{
				if (ExTraceGlobals.indexDeliveryAgentTracer == null)
				{
					ExTraceGlobals.indexDeliveryAgentTracer = new Trace(ExTraceGlobals.componentGuid, 77);
				}
				return ExTraceGlobals.indexDeliveryAgentTracer;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x00052FF9 File Offset: 0x000511F9
		public static Trace TransportFlowFeederTracer
		{
			get
			{
				if (ExTraceGlobals.transportFlowFeederTracer == null)
				{
					ExTraceGlobals.transportFlowFeederTracer = new Trace(ExTraceGlobals.componentGuid, 78);
				}
				return ExTraceGlobals.transportFlowFeederTracer;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x00053018 File Offset: 0x00051218
		public static Trace QueryExecutorTracer
		{
			get
			{
				if (ExTraceGlobals.queryExecutorTracer == null)
				{
					ExTraceGlobals.queryExecutorTracer = new Trace(ExTraceGlobals.componentGuid, 79);
				}
				return ExTraceGlobals.queryExecutorTracer;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x00053037 File Offset: 0x00051237
		public static Trace ErrorOperatorTracer
		{
			get
			{
				if (ExTraceGlobals.errorOperatorTracer == null)
				{
					ExTraceGlobals.errorOperatorTracer = new Trace(ExTraceGlobals.componentGuid, 80);
				}
				return ExTraceGlobals.errorOperatorTracer;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x00053056 File Offset: 0x00051256
		public static Trace NotificationsWatermarkManagerTracer
		{
			get
			{
				if (ExTraceGlobals.notificationsWatermarkManagerTracer == null)
				{
					ExTraceGlobals.notificationsWatermarkManagerTracer = new Trace(ExTraceGlobals.componentGuid, 81);
				}
				return ExTraceGlobals.notificationsWatermarkManagerTracer;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x00053075 File Offset: 0x00051275
		public static Trace IndexStatusStoreTracer
		{
			get
			{
				if (ExTraceGlobals.indexStatusStoreTracer == null)
				{
					ExTraceGlobals.indexStatusStoreTracer = new Trace(ExTraceGlobals.componentGuid, 82);
				}
				return ExTraceGlobals.indexStatusStoreTracer;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x00053094 File Offset: 0x00051294
		public static Trace IndexStatusProviderTracer
		{
			get
			{
				if (ExTraceGlobals.indexStatusProviderTracer == null)
				{
					ExTraceGlobals.indexStatusProviderTracer = new Trace(ExTraceGlobals.componentGuid, 83);
				}
				return ExTraceGlobals.indexStatusProviderTracer;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x000530B3 File Offset: 0x000512B3
		public static Trace FastIoExtensionTracer
		{
			get
			{
				if (ExTraceGlobals.fastIoExtensionTracer == null)
				{
					ExTraceGlobals.fastIoExtensionTracer = new Trace(ExTraceGlobals.componentGuid, 84);
				}
				return ExTraceGlobals.fastIoExtensionTracer;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x000530D2 File Offset: 0x000512D2
		public static Trace XSOMailboxSessionTracer
		{
			get
			{
				if (ExTraceGlobals.xSOMailboxSessionTracer == null)
				{
					ExTraceGlobals.xSOMailboxSessionTracer = new Trace(ExTraceGlobals.componentGuid, 85);
				}
				return ExTraceGlobals.xSOMailboxSessionTracer;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x000530F1 File Offset: 0x000512F1
		public static Trace PostDocParserOperatorTracer
		{
			get
			{
				if (ExTraceGlobals.postDocParserOperatorTracer == null)
				{
					ExTraceGlobals.postDocParserOperatorTracer = new Trace(ExTraceGlobals.componentGuid, 86);
				}
				return ExTraceGlobals.postDocParserOperatorTracer;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x00053110 File Offset: 0x00051310
		public static Trace RecordManagerOperatorTracer
		{
			get
			{
				if (ExTraceGlobals.recordManagerOperatorTracer == null)
				{
					ExTraceGlobals.recordManagerOperatorTracer = new Trace(ExTraceGlobals.componentGuid, 87);
				}
				return ExTraceGlobals.recordManagerOperatorTracer;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x0005312F File Offset: 0x0005132F
		public static Trace OperatorDiagnosticsTracer
		{
			get
			{
				if (ExTraceGlobals.operatorDiagnosticsTracer == null)
				{
					ExTraceGlobals.operatorDiagnosticsTracer = new Trace(ExTraceGlobals.componentGuid, 88);
				}
				return ExTraceGlobals.operatorDiagnosticsTracer;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x0005314E File Offset: 0x0005134E
		public static Trace SearchRpcClientTracer
		{
			get
			{
				if (ExTraceGlobals.searchRpcClientTracer == null)
				{
					ExTraceGlobals.searchRpcClientTracer = new Trace(ExTraceGlobals.componentGuid, 89);
				}
				return ExTraceGlobals.searchRpcClientTracer;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x0005316D File Offset: 0x0005136D
		public static Trace SearchRpcServerTracer
		{
			get
			{
				if (ExTraceGlobals.searchRpcServerTracer == null)
				{
					ExTraceGlobals.searchRpcServerTracer = new Trace(ExTraceGlobals.componentGuid, 90);
				}
				return ExTraceGlobals.searchRpcServerTracer;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x0005318C File Offset: 0x0005138C
		public static Trace DocumentTrackerOperatorTracer
		{
			get
			{
				if (ExTraceGlobals.documentTrackerOperatorTracer == null)
				{
					ExTraceGlobals.documentTrackerOperatorTracer = new Trace(ExTraceGlobals.componentGuid, 91);
				}
				return ExTraceGlobals.documentTrackerOperatorTracer;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x000531AB File Offset: 0x000513AB
		public static Trace ErrorBypassOperatorTracer
		{
			get
			{
				if (ExTraceGlobals.errorBypassOperatorTracer == null)
				{
					ExTraceGlobals.errorBypassOperatorTracer = new Trace(ExTraceGlobals.componentGuid, 92);
				}
				return ExTraceGlobals.errorBypassOperatorTracer;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x000531CA File Offset: 0x000513CA
		public static Trace FeederThrottlingTracer
		{
			get
			{
				if (ExTraceGlobals.feederThrottlingTracer == null)
				{
					ExTraceGlobals.feederThrottlingTracer = new Trace(ExTraceGlobals.componentGuid, 93);
				}
				return ExTraceGlobals.feederThrottlingTracer;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x000531E9 File Offset: 0x000513E9
		public static Trace WatermarkStorageTracer
		{
			get
			{
				if (ExTraceGlobals.watermarkStorageTracer == null)
				{
					ExTraceGlobals.watermarkStorageTracer = new Trace(ExTraceGlobals.componentGuid, 94);
				}
				return ExTraceGlobals.watermarkStorageTracer;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x00053208 File Offset: 0x00051408
		public static Trace DiagnosticOperatorTracer
		{
			get
			{
				if (ExTraceGlobals.diagnosticOperatorTracer == null)
				{
					ExTraceGlobals.diagnosticOperatorTracer = new Trace(ExTraceGlobals.componentGuid, 95);
				}
				return ExTraceGlobals.diagnosticOperatorTracer;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x00053227 File Offset: 0x00051427
		public static Trace InstantSearchTracer
		{
			get
			{
				if (ExTraceGlobals.instantSearchTracer == null)
				{
					ExTraceGlobals.instantSearchTracer = new Trace(ExTraceGlobals.componentGuid, 96);
				}
				return ExTraceGlobals.instantSearchTracer;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x00053246 File Offset: 0x00051446
		public static Trace TopNManagementClientTracer
		{
			get
			{
				if (ExTraceGlobals.topNManagementClientTracer == null)
				{
					ExTraceGlobals.topNManagementClientTracer = new Trace(ExTraceGlobals.componentGuid, 97);
				}
				return ExTraceGlobals.topNManagementClientTracer;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x00053265 File Offset: 0x00051465
		public static Trace SearchDictionaryTracer
		{
			get
			{
				if (ExTraceGlobals.searchDictionaryTracer == null)
				{
					ExTraceGlobals.searchDictionaryTracer = new Trace(ExTraceGlobals.componentGuid, 98);
				}
				return ExTraceGlobals.searchDictionaryTracer;
			}
		}

		// Token: 0x0400189D RID: 6301
		private static Guid componentGuid = new Guid("c3ea5adf-c135-45e7-9dff-e1dc3bd67123");

		// Token: 0x0400189E RID: 6302
		private static Trace generalTracer = null;

		// Token: 0x0400189F RID: 6303
		private static Trace catchUpNotificationCrawlerTracer = null;

		// Token: 0x040018A0 RID: 6304
		private static Trace chunkSourceFunctionsTracer = null;

		// Token: 0x040018A1 RID: 6305
		private static Trace crawlerTracer = null;

		// Token: 0x040018A2 RID: 6306
		private static Trace cSrchProjectTracer = null;

		// Token: 0x040018A3 RID: 6307
		private static Trace dataSourceFunctionsTracer = null;

		// Token: 0x040018A4 RID: 6308
		private static Trace driverTracer = null;

		// Token: 0x040018A5 RID: 6309
		private static Trace filterEnumeratorTracer = null;

		// Token: 0x040018A6 RID: 6310
		private static Trace fTEAdminComInteropTracer = null;

		// Token: 0x040018A7 RID: 6311
		private static Trace indexablePropertyCacheTracer = null;

		// Token: 0x040018A8 RID: 6312
		private static Trace mapiIteratorTracer = null;

		// Token: 0x040018A9 RID: 6313
		private static Trace notificationProcessingTracer = null;

		// Token: 0x040018AA RID: 6314
		private static Trace notificationQueueTracer = null;

		// Token: 0x040018AB RID: 6315
		private static Trace notificationWatcherTracer = null;

		// Token: 0x040018AC RID: 6316
		private static Trace pHFunctionsTracer = null;

		// Token: 0x040018AD RID: 6317
		private static Trace retryEngineTracer = null;

		// Token: 0x040018AE RID: 6318
		private static Trace throttleControllerTracer = null;

		// Token: 0x040018AF RID: 6319
		private static Trace propertyStoreCacheTracer = null;

		// Token: 0x040018B0 RID: 6320
		private static Trace activeManagerTracer = null;

		// Token: 0x040018B1 RID: 6321
		private static Trace catalogHealthTracer = null;

		// Token: 0x040018B2 RID: 6322
		private static Trace searchCatalogClientTracer = null;

		// Token: 0x040018B3 RID: 6323
		private static Trace searchCatalogServerTracer = null;

		// Token: 0x040018B4 RID: 6324
		private static Trace mailboxDeletionTracer = null;

		// Token: 0x040018B5 RID: 6325
		private static Trace propertyStoreTracer = null;

		// Token: 0x040018B6 RID: 6326
		private static Trace storeMonitorTracer = null;

		// Token: 0x040018B7 RID: 6327
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x040018B8 RID: 6328
		private static Trace mailboxIndexingHelperTracer = null;

		// Token: 0x040018B9 RID: 6329
		private static Trace catalogStateTracer = null;

		// Token: 0x040018BA RID: 6330
		private static Trace fileExtensionCacheTracer = null;

		// Token: 0x040018BB RID: 6331
		private static Trace msFteSqlMonitorTracer = null;

		// Token: 0x040018BC RID: 6332
		private static Trace serverConnectionsTracer = null;

		// Token: 0x040018BD RID: 6333
		private static Trace logonCacheTracer = null;

		// Token: 0x040018BE RID: 6334
		private static Trace logonTracer = null;

		// Token: 0x040018BF RID: 6335
		private static Trace catalogReconcilerTracer = null;

		// Token: 0x040018C0 RID: 6336
		private static Trace catalogReconcileResultTracer = null;

		// Token: 0x040018C1 RID: 6337
		private static Trace allCatalogReconcilerTracer = null;

		// Token: 0x040018C2 RID: 6338
		private static Trace mailboxReconcileResultTracer = null;

		// Token: 0x040018C3 RID: 6339
		private static Trace newFilterMonitorTracer = null;

		// Token: 0x040018C4 RID: 6340
		private static Trace inMemoryDefaultTracer = null;

		// Token: 0x040018C5 RID: 6341
		private static Trace testExchangeSearchTracer = null;

		// Token: 0x040018C6 RID: 6342
		private static Trace batchThrottlerTracer = null;

		// Token: 0x040018C7 RID: 6343
		private static Trace throttleParametersTracer = null;

		// Token: 0x040018C8 RID: 6344
		private static Trace throttleDataProviderTracer = null;

		// Token: 0x040018C9 RID: 6345
		private static Trace registryParameterTracer = null;

		// Token: 0x040018CA RID: 6346
		private static Trace latencySamplerTracer = null;

		// Token: 0x040018CB RID: 6347
		private static Trace movingAverageTracer = null;

		// Token: 0x040018CC RID: 6348
		private static Trace coreComponentTracer = null;

		// Token: 0x040018CD RID: 6349
		private static Trace coreComponentRegistryTracer = null;

		// Token: 0x040018CE RID: 6350
		private static Trace coreGeneralTracer = null;

		// Token: 0x040018CF RID: 6351
		private static Trace fastFeederTracer = null;

		// Token: 0x040018D0 RID: 6352
		private static Trace mdbNotificationsFeederTracer = null;

		// Token: 0x040018D1 RID: 6353
		private static Trace serviceTracer = null;

		// Token: 0x040018D2 RID: 6354
		private static Trace engineTracer = null;

		// Token: 0x040018D3 RID: 6355
		private static Trace mdbFeedingControllerTracer = null;

		// Token: 0x040018D4 RID: 6356
		private static Trace indexManagementTracer = null;

		// Token: 0x040018D5 RID: 6357
		private static Trace coreFailureMonitorTracer = null;

		// Token: 0x040018D6 RID: 6358
		private static Trace mdbCrawlerFeederTracer = null;

		// Token: 0x040018D7 RID: 6359
		private static Trace mdbDocumentAdapterTracer = null;

		// Token: 0x040018D8 RID: 6360
		private static Trace coreDocumentModelTracer = null;

		// Token: 0x040018D9 RID: 6361
		private static Trace pipelineLoaderTracer = null;

		// Token: 0x040018DA RID: 6362
		private static Trace corePipelineTracer = null;

		// Token: 0x040018DB RID: 6363
		private static Trace queueManagerTracer = null;

		// Token: 0x040018DC RID: 6364
		private static Trace crawlerWatermarkManagerTracer = null;

		// Token: 0x040018DD RID: 6365
		private static Trace failedItemStorageTracer = null;

		// Token: 0x040018DE RID: 6366
		private static Trace mdbWatcherTracer = null;

		// Token: 0x040018DF RID: 6367
		private static Trace mdbRetryFeederTracer = null;

		// Token: 0x040018E0 RID: 6368
		private static Trace mdbSessionCacheTracer = null;

		// Token: 0x040018E1 RID: 6369
		private static Trace retrieverOperatorTracer = null;

		// Token: 0x040018E2 RID: 6370
		private static Trace streamManagerTracer = null;

		// Token: 0x040018E3 RID: 6371
		private static Trace streamChannelTracer = null;

		// Token: 0x040018E4 RID: 6372
		private static Trace annotationTokenTracer = null;

		// Token: 0x040018E5 RID: 6373
		private static Trace transportOperatorTracer = null;

		// Token: 0x040018E6 RID: 6374
		private static Trace indexRoutingAgentTracer = null;

		// Token: 0x040018E7 RID: 6375
		private static Trace indexDeliveryAgentTracer = null;

		// Token: 0x040018E8 RID: 6376
		private static Trace transportFlowFeederTracer = null;

		// Token: 0x040018E9 RID: 6377
		private static Trace queryExecutorTracer = null;

		// Token: 0x040018EA RID: 6378
		private static Trace errorOperatorTracer = null;

		// Token: 0x040018EB RID: 6379
		private static Trace notificationsWatermarkManagerTracer = null;

		// Token: 0x040018EC RID: 6380
		private static Trace indexStatusStoreTracer = null;

		// Token: 0x040018ED RID: 6381
		private static Trace indexStatusProviderTracer = null;

		// Token: 0x040018EE RID: 6382
		private static Trace fastIoExtensionTracer = null;

		// Token: 0x040018EF RID: 6383
		private static Trace xSOMailboxSessionTracer = null;

		// Token: 0x040018F0 RID: 6384
		private static Trace postDocParserOperatorTracer = null;

		// Token: 0x040018F1 RID: 6385
		private static Trace recordManagerOperatorTracer = null;

		// Token: 0x040018F2 RID: 6386
		private static Trace operatorDiagnosticsTracer = null;

		// Token: 0x040018F3 RID: 6387
		private static Trace searchRpcClientTracer = null;

		// Token: 0x040018F4 RID: 6388
		private static Trace searchRpcServerTracer = null;

		// Token: 0x040018F5 RID: 6389
		private static Trace documentTrackerOperatorTracer = null;

		// Token: 0x040018F6 RID: 6390
		private static Trace errorBypassOperatorTracer = null;

		// Token: 0x040018F7 RID: 6391
		private static Trace feederThrottlingTracer = null;

		// Token: 0x040018F8 RID: 6392
		private static Trace watermarkStorageTracer = null;

		// Token: 0x040018F9 RID: 6393
		private static Trace diagnosticOperatorTracer = null;

		// Token: 0x040018FA RID: 6394
		private static Trace instantSearchTracer = null;

		// Token: 0x040018FB RID: 6395
		private static Trace topNManagementClientTracer = null;

		// Token: 0x040018FC RID: 6396
		private static Trace searchDictionaryTracer = null;
	}
}
