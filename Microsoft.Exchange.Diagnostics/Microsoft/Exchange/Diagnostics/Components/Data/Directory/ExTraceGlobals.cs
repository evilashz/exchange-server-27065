using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.Data.Directory
{
	// Token: 0x0200033B RID: 827
	public static class ExTraceGlobals
	{
		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x00050403 File Offset: 0x0004E603
		public static Trace TopologyProviderTracer
		{
			get
			{
				if (ExTraceGlobals.topologyProviderTracer == null)
				{
					ExTraceGlobals.topologyProviderTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.topologyProviderTracer;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x00050421 File Offset: 0x0004E621
		public static Trace ADTopologyTracer
		{
			get
			{
				if (ExTraceGlobals.aDTopologyTracer == null)
				{
					ExTraceGlobals.aDTopologyTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.aDTopologyTracer;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x0005043F File Offset: 0x0004E63F
		public static Trace ConnectionTracer
		{
			get
			{
				if (ExTraceGlobals.connectionTracer == null)
				{
					ExTraceGlobals.connectionTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.connectionTracer;
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060012EA RID: 4842 RVA: 0x0005045D File Offset: 0x0004E65D
		public static Trace ConnectionDetailsTracer
		{
			get
			{
				if (ExTraceGlobals.connectionDetailsTracer == null)
				{
					ExTraceGlobals.connectionDetailsTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.connectionDetailsTracer;
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x0005047B File Offset: 0x0004E67B
		public static Trace GetConnectionTracer
		{
			get
			{
				if (ExTraceGlobals.getConnectionTracer == null)
				{
					ExTraceGlobals.getConnectionTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.getConnectionTracer;
			}
		}

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x00050499 File Offset: 0x0004E699
		public static Trace ADFindTracer
		{
			get
			{
				if (ExTraceGlobals.aDFindTracer == null)
				{
					ExTraceGlobals.aDFindTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.aDFindTracer;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x000504B7 File Offset: 0x0004E6B7
		public static Trace ADReadTracer
		{
			get
			{
				if (ExTraceGlobals.aDReadTracer == null)
				{
					ExTraceGlobals.aDReadTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.aDReadTracer;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x000504D5 File Offset: 0x0004E6D5
		public static Trace ADReadDetailsTracer
		{
			get
			{
				if (ExTraceGlobals.aDReadDetailsTracer == null)
				{
					ExTraceGlobals.aDReadDetailsTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.aDReadDetailsTracer;
			}
		}

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x000504F3 File Offset: 0x0004E6F3
		public static Trace ADSaveTracer
		{
			get
			{
				if (ExTraceGlobals.aDSaveTracer == null)
				{
					ExTraceGlobals.aDSaveTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.aDSaveTracer;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x00050511 File Offset: 0x0004E711
		public static Trace ADSaveDetailsTracer
		{
			get
			{
				if (ExTraceGlobals.aDSaveDetailsTracer == null)
				{
					ExTraceGlobals.aDSaveDetailsTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.aDSaveDetailsTracer;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x00050530 File Offset: 0x0004E730
		public static Trace ADDeleteTracer
		{
			get
			{
				if (ExTraceGlobals.aDDeleteTracer == null)
				{
					ExTraceGlobals.aDDeleteTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.aDDeleteTracer;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060012F2 RID: 4850 RVA: 0x0005054F File Offset: 0x0004E74F
		public static Trace ValidationTracer
		{
			get
			{
				if (ExTraceGlobals.validationTracer == null)
				{
					ExTraceGlobals.validationTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.validationTracer;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x0005056E File Offset: 0x0004E76E
		public static Trace ADNotificationsTracer
		{
			get
			{
				if (ExTraceGlobals.aDNotificationsTracer == null)
				{
					ExTraceGlobals.aDNotificationsTracer = new Trace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.aDNotificationsTracer;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060012F4 RID: 4852 RVA: 0x0005058D File Offset: 0x0004E78D
		public static Trace DirectoryExceptionTracer
		{
			get
			{
				if (ExTraceGlobals.directoryExceptionTracer == null)
				{
					ExTraceGlobals.directoryExceptionTracer = new Trace(ExTraceGlobals.componentGuid, 13);
				}
				return ExTraceGlobals.directoryExceptionTracer;
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060012F5 RID: 4853 RVA: 0x000505AC File Offset: 0x0004E7AC
		public static Trace LdapFilterBuilderTracer
		{
			get
			{
				if (ExTraceGlobals.ldapFilterBuilderTracer == null)
				{
					ExTraceGlobals.ldapFilterBuilderTracer = new Trace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.ldapFilterBuilderTracer;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060012F6 RID: 4854 RVA: 0x000505CB File Offset: 0x0004E7CB
		public static Trace ADPropertyRequestTracer
		{
			get
			{
				if (ExTraceGlobals.aDPropertyRequestTracer == null)
				{
					ExTraceGlobals.aDPropertyRequestTracer = new Trace(ExTraceGlobals.componentGuid, 15);
				}
				return ExTraceGlobals.aDPropertyRequestTracer;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x000505EA File Offset: 0x0004E7EA
		public static Trace ADObjectTracer
		{
			get
			{
				if (ExTraceGlobals.aDObjectTracer == null)
				{
					ExTraceGlobals.aDObjectTracer = new Trace(ExTraceGlobals.componentGuid, 17);
				}
				return ExTraceGlobals.aDObjectTracer;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x00050609 File Offset: 0x0004E809
		public static Trace ContentTypeMappingTracer
		{
			get
			{
				if (ExTraceGlobals.contentTypeMappingTracer == null)
				{
					ExTraceGlobals.contentTypeMappingTracer = new Trace(ExTraceGlobals.componentGuid, 19);
				}
				return ExTraceGlobals.contentTypeMappingTracer;
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x00050628 File Offset: 0x0004E828
		public static Trace LcidMapperTracer
		{
			get
			{
				if (ExTraceGlobals.lcidMapperTracer == null)
				{
					ExTraceGlobals.lcidMapperTracer = new Trace(ExTraceGlobals.componentGuid, 20);
				}
				return ExTraceGlobals.lcidMapperTracer;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060012FA RID: 4858 RVA: 0x00050647 File Offset: 0x0004E847
		public static Trace RecipientUpdateServiceTracer
		{
			get
			{
				if (ExTraceGlobals.recipientUpdateServiceTracer == null)
				{
					ExTraceGlobals.recipientUpdateServiceTracer = new Trace(ExTraceGlobals.componentGuid, 21);
				}
				return ExTraceGlobals.recipientUpdateServiceTracer;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x00050666 File Offset: 0x0004E866
		public static Trace UMAutoAttendantTracer
		{
			get
			{
				if (ExTraceGlobals.uMAutoAttendantTracer == null)
				{
					ExTraceGlobals.uMAutoAttendantTracer = new Trace(ExTraceGlobals.componentGuid, 22);
				}
				return ExTraceGlobals.uMAutoAttendantTracer;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060012FC RID: 4860 RVA: 0x00050685 File Offset: 0x0004E885
		public static Trace ExchangeTopologyTracer
		{
			get
			{
				if (ExTraceGlobals.exchangeTopologyTracer == null)
				{
					ExTraceGlobals.exchangeTopologyTracer = new Trace(ExTraceGlobals.componentGuid, 23);
				}
				return ExTraceGlobals.exchangeTopologyTracer;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x000506A4 File Offset: 0x0004E8A4
		public static Trace PerfCountersTracer
		{
			get
			{
				if (ExTraceGlobals.perfCountersTracer == null)
				{
					ExTraceGlobals.perfCountersTracer = new Trace(ExTraceGlobals.componentGuid, 24);
				}
				return ExTraceGlobals.perfCountersTracer;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060012FE RID: 4862 RVA: 0x000506C3 File Offset: 0x0004E8C3
		public static Trace ClientThrottlingTracer
		{
			get
			{
				if (ExTraceGlobals.clientThrottlingTracer == null)
				{
					ExTraceGlobals.clientThrottlingTracer = new Trace(ExTraceGlobals.componentGuid, 25);
				}
				return ExTraceGlobals.clientThrottlingTracer;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x000506E2 File Offset: 0x0004E8E2
		public static Trace ServerSettingsProviderTracer
		{
			get
			{
				if (ExTraceGlobals.serverSettingsProviderTracer == null)
				{
					ExTraceGlobals.serverSettingsProviderTracer = new Trace(ExTraceGlobals.componentGuid, 27);
				}
				return ExTraceGlobals.serverSettingsProviderTracer;
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x00050701 File Offset: 0x0004E901
		public static Trace RetryManagerTracer
		{
			get
			{
				if (ExTraceGlobals.retryManagerTracer == null)
				{
					ExTraceGlobals.retryManagerTracer = new Trace(ExTraceGlobals.componentGuid, 29);
				}
				return ExTraceGlobals.retryManagerTracer;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x00050720 File Offset: 0x0004E920
		public static Trace SystemConfigurationCacheTracer
		{
			get
			{
				if (ExTraceGlobals.systemConfigurationCacheTracer == null)
				{
					ExTraceGlobals.systemConfigurationCacheTracer = new Trace(ExTraceGlobals.componentGuid, 30);
				}
				return ExTraceGlobals.systemConfigurationCacheTracer;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x0005073F File Offset: 0x0004E93F
		public static Trace FederatedIdentityTracer
		{
			get
			{
				if (ExTraceGlobals.federatedIdentityTracer == null)
				{
					ExTraceGlobals.federatedIdentityTracer = new Trace(ExTraceGlobals.componentGuid, 31);
				}
				return ExTraceGlobals.federatedIdentityTracer;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x0005075E File Offset: 0x0004E95E
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 32);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x0005077D File Offset: 0x0004E97D
		public static Trace AddressListTracer
		{
			get
			{
				if (ExTraceGlobals.addressListTracer == null)
				{
					ExTraceGlobals.addressListTracer = new Trace(ExTraceGlobals.componentGuid, 33);
				}
				return ExTraceGlobals.addressListTracer;
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x0005079C File Offset: 0x0004E99C
		public static Trace NspiRpcClientConnectionTracer
		{
			get
			{
				if (ExTraceGlobals.nspiRpcClientConnectionTracer == null)
				{
					ExTraceGlobals.nspiRpcClientConnectionTracer = new Trace(ExTraceGlobals.componentGuid, 34);
				}
				return ExTraceGlobals.nspiRpcClientConnectionTracer;
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x000507BB File Offset: 0x0004E9BB
		public static Trace ScopeVerificationTracer
		{
			get
			{
				if (ExTraceGlobals.scopeVerificationTracer == null)
				{
					ExTraceGlobals.scopeVerificationTracer = new Trace(ExTraceGlobals.componentGuid, 35);
				}
				return ExTraceGlobals.scopeVerificationTracer;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x000507DA File Offset: 0x0004E9DA
		public static Trace SchemaInitializationTracer
		{
			get
			{
				if (ExTraceGlobals.schemaInitializationTracer == null)
				{
					ExTraceGlobals.schemaInitializationTracer = new Trace(ExTraceGlobals.componentGuid, 36);
				}
				return ExTraceGlobals.schemaInitializationTracer;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001308 RID: 4872 RVA: 0x000507F9 File Offset: 0x0004E9F9
		public static Trace IsMemberOfResolverTracer
		{
			get
			{
				if (ExTraceGlobals.isMemberOfResolverTracer == null)
				{
					ExTraceGlobals.isMemberOfResolverTracer = new Trace(ExTraceGlobals.componentGuid, 37);
				}
				return ExTraceGlobals.isMemberOfResolverTracer;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x00050818 File Offset: 0x0004EA18
		public static Trace OwaSegmentationTracer
		{
			get
			{
				if (ExTraceGlobals.owaSegmentationTracer == null)
				{
					ExTraceGlobals.owaSegmentationTracer = new Trace(ExTraceGlobals.componentGuid, 39);
				}
				return ExTraceGlobals.owaSegmentationTracer;
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600130A RID: 4874 RVA: 0x00050837 File Offset: 0x0004EA37
		public static Trace ADPerformanceTracer
		{
			get
			{
				if (ExTraceGlobals.aDPerformanceTracer == null)
				{
					ExTraceGlobals.aDPerformanceTracer = new Trace(ExTraceGlobals.componentGuid, 40);
				}
				return ExTraceGlobals.aDPerformanceTracer;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x00050856 File Offset: 0x0004EA56
		public static Trace ResourceHealthManagerTracer
		{
			get
			{
				if (ExTraceGlobals.resourceHealthManagerTracer == null)
				{
					ExTraceGlobals.resourceHealthManagerTracer = new Trace(ExTraceGlobals.componentGuid, 41);
				}
				return ExTraceGlobals.resourceHealthManagerTracer;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x00050875 File Offset: 0x0004EA75
		public static Trace BudgetDelayTracer
		{
			get
			{
				if (ExTraceGlobals.budgetDelayTracer == null)
				{
					ExTraceGlobals.budgetDelayTracer = new Trace(ExTraceGlobals.componentGuid, 42);
				}
				return ExTraceGlobals.budgetDelayTracer;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600130D RID: 4877 RVA: 0x00050894 File Offset: 0x0004EA94
		public static Trace GLSTracer
		{
			get
			{
				if (ExTraceGlobals.gLSTracer == null)
				{
					ExTraceGlobals.gLSTracer = new Trace(ExTraceGlobals.componentGuid, 43);
				}
				return ExTraceGlobals.gLSTracer;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x000508B3 File Offset: 0x0004EAB3
		public static Trace MServTracer
		{
			get
			{
				if (ExTraceGlobals.mServTracer == null)
				{
					ExTraceGlobals.mServTracer = new Trace(ExTraceGlobals.componentGuid, 44);
				}
				return ExTraceGlobals.mServTracer;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x0600130F RID: 4879 RVA: 0x000508D2 File Offset: 0x0004EAD2
		public static Trace TenantRelocationTracer
		{
			get
			{
				if (ExTraceGlobals.tenantRelocationTracer == null)
				{
					ExTraceGlobals.tenantRelocationTracer = new Trace(ExTraceGlobals.componentGuid, 45);
				}
				return ExTraceGlobals.tenantRelocationTracer;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001310 RID: 4880 RVA: 0x000508F1 File Offset: 0x0004EAF1
		public static Trace StateManagementTracer
		{
			get
			{
				if (ExTraceGlobals.stateManagementTracer == null)
				{
					ExTraceGlobals.stateManagementTracer = new Trace(ExTraceGlobals.componentGuid, 46);
				}
				return ExTraceGlobals.stateManagementTracer;
			}
		}

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001311 RID: 4881 RVA: 0x00050910 File Offset: 0x0004EB10
		public static Trace ServerComponentStateManagerTracer
		{
			get
			{
				if (ExTraceGlobals.serverComponentStateManagerTracer == null)
				{
					ExTraceGlobals.serverComponentStateManagerTracer = new Trace(ExTraceGlobals.componentGuid, 48);
				}
				return ExTraceGlobals.serverComponentStateManagerTracer;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x0005092F File Offset: 0x0004EB2F
		public static Trace SessionSettingsTracer
		{
			get
			{
				if (ExTraceGlobals.sessionSettingsTracer == null)
				{
					ExTraceGlobals.sessionSettingsTracer = new Trace(ExTraceGlobals.componentGuid, 49);
				}
				return ExTraceGlobals.sessionSettingsTracer;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001313 RID: 4883 RVA: 0x0005094E File Offset: 0x0004EB4E
		public static Trace ADConfigLoaderTracer
		{
			get
			{
				if (ExTraceGlobals.aDConfigLoaderTracer == null)
				{
					ExTraceGlobals.aDConfigLoaderTracer = new Trace(ExTraceGlobals.componentGuid, 50);
				}
				return ExTraceGlobals.aDConfigLoaderTracer;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001314 RID: 4884 RVA: 0x0005096D File Offset: 0x0004EB6D
		public static Trace SlimTenantTracer
		{
			get
			{
				if (ExTraceGlobals.slimTenantTracer == null)
				{
					ExTraceGlobals.slimTenantTracer = new Trace(ExTraceGlobals.componentGuid, 51);
				}
				return ExTraceGlobals.slimTenantTracer;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x0005098C File Offset: 0x0004EB8C
		public static Trace TenantUpgradeServiceletTracer
		{
			get
			{
				if (ExTraceGlobals.tenantUpgradeServiceletTracer == null)
				{
					ExTraceGlobals.tenantUpgradeServiceletTracer = new Trace(ExTraceGlobals.componentGuid, 52);
				}
				return ExTraceGlobals.tenantUpgradeServiceletTracer;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x000509AB File Offset: 0x0004EBAB
		public static Trace DirectoryTasksTracer
		{
			get
			{
				if (ExTraceGlobals.directoryTasksTracer == null)
				{
					ExTraceGlobals.directoryTasksTracer = new Trace(ExTraceGlobals.componentGuid, 53);
				}
				return ExTraceGlobals.directoryTasksTracer;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001317 RID: 4887 RVA: 0x000509CA File Offset: 0x0004EBCA
		public static Trace ComplianceTracer
		{
			get
			{
				if (ExTraceGlobals.complianceTracer == null)
				{
					ExTraceGlobals.complianceTracer = new Trace(ExTraceGlobals.componentGuid, 54);
				}
				return ExTraceGlobals.complianceTracer;
			}
		}

		// Token: 0x04001795 RID: 6037
		private static Guid componentGuid = new Guid("0c6a4049-bb65-4ea6-9f0c-12808260c2f1");

		// Token: 0x04001796 RID: 6038
		private static Trace topologyProviderTracer = null;

		// Token: 0x04001797 RID: 6039
		private static Trace aDTopologyTracer = null;

		// Token: 0x04001798 RID: 6040
		private static Trace connectionTracer = null;

		// Token: 0x04001799 RID: 6041
		private static Trace connectionDetailsTracer = null;

		// Token: 0x0400179A RID: 6042
		private static Trace getConnectionTracer = null;

		// Token: 0x0400179B RID: 6043
		private static Trace aDFindTracer = null;

		// Token: 0x0400179C RID: 6044
		private static Trace aDReadTracer = null;

		// Token: 0x0400179D RID: 6045
		private static Trace aDReadDetailsTracer = null;

		// Token: 0x0400179E RID: 6046
		private static Trace aDSaveTracer = null;

		// Token: 0x0400179F RID: 6047
		private static Trace aDSaveDetailsTracer = null;

		// Token: 0x040017A0 RID: 6048
		private static Trace aDDeleteTracer = null;

		// Token: 0x040017A1 RID: 6049
		private static Trace validationTracer = null;

		// Token: 0x040017A2 RID: 6050
		private static Trace aDNotificationsTracer = null;

		// Token: 0x040017A3 RID: 6051
		private static Trace directoryExceptionTracer = null;

		// Token: 0x040017A4 RID: 6052
		private static Trace ldapFilterBuilderTracer = null;

		// Token: 0x040017A5 RID: 6053
		private static Trace aDPropertyRequestTracer = null;

		// Token: 0x040017A6 RID: 6054
		private static Trace aDObjectTracer = null;

		// Token: 0x040017A7 RID: 6055
		private static Trace contentTypeMappingTracer = null;

		// Token: 0x040017A8 RID: 6056
		private static Trace lcidMapperTracer = null;

		// Token: 0x040017A9 RID: 6057
		private static Trace recipientUpdateServiceTracer = null;

		// Token: 0x040017AA RID: 6058
		private static Trace uMAutoAttendantTracer = null;

		// Token: 0x040017AB RID: 6059
		private static Trace exchangeTopologyTracer = null;

		// Token: 0x040017AC RID: 6060
		private static Trace perfCountersTracer = null;

		// Token: 0x040017AD RID: 6061
		private static Trace clientThrottlingTracer = null;

		// Token: 0x040017AE RID: 6062
		private static Trace serverSettingsProviderTracer = null;

		// Token: 0x040017AF RID: 6063
		private static Trace retryManagerTracer = null;

		// Token: 0x040017B0 RID: 6064
		private static Trace systemConfigurationCacheTracer = null;

		// Token: 0x040017B1 RID: 6065
		private static Trace federatedIdentityTracer = null;

		// Token: 0x040017B2 RID: 6066
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x040017B3 RID: 6067
		private static Trace addressListTracer = null;

		// Token: 0x040017B4 RID: 6068
		private static Trace nspiRpcClientConnectionTracer = null;

		// Token: 0x040017B5 RID: 6069
		private static Trace scopeVerificationTracer = null;

		// Token: 0x040017B6 RID: 6070
		private static Trace schemaInitializationTracer = null;

		// Token: 0x040017B7 RID: 6071
		private static Trace isMemberOfResolverTracer = null;

		// Token: 0x040017B8 RID: 6072
		private static Trace owaSegmentationTracer = null;

		// Token: 0x040017B9 RID: 6073
		private static Trace aDPerformanceTracer = null;

		// Token: 0x040017BA RID: 6074
		private static Trace resourceHealthManagerTracer = null;

		// Token: 0x040017BB RID: 6075
		private static Trace budgetDelayTracer = null;

		// Token: 0x040017BC RID: 6076
		private static Trace gLSTracer = null;

		// Token: 0x040017BD RID: 6077
		private static Trace mServTracer = null;

		// Token: 0x040017BE RID: 6078
		private static Trace tenantRelocationTracer = null;

		// Token: 0x040017BF RID: 6079
		private static Trace stateManagementTracer = null;

		// Token: 0x040017C0 RID: 6080
		private static Trace serverComponentStateManagerTracer = null;

		// Token: 0x040017C1 RID: 6081
		private static Trace sessionSettingsTracer = null;

		// Token: 0x040017C2 RID: 6082
		private static Trace aDConfigLoaderTracer = null;

		// Token: 0x040017C3 RID: 6083
		private static Trace slimTenantTracer = null;

		// Token: 0x040017C4 RID: 6084
		private static Trace tenantUpgradeServiceletTracer = null;

		// Token: 0x040017C5 RID: 6085
		private static Trace directoryTasksTracer = null;

		// Token: 0x040017C6 RID: 6086
		private static Trace complianceTracer = null;
	}
}
