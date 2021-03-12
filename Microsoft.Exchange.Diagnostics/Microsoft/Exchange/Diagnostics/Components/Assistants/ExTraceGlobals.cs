using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.Assistants
{
	// Token: 0x0200033F RID: 831
	public static class ExTraceGlobals
	{
		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x0600132B RID: 4907 RVA: 0x00050D7D File Offset: 0x0004EF7D
		public static Trace AssistantsRpcServerTracer
		{
			get
			{
				if (ExTraceGlobals.assistantsRpcServerTracer == null)
				{
					ExTraceGlobals.assistantsRpcServerTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.assistantsRpcServerTracer;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x00050D9B File Offset: 0x0004EF9B
		public static Trace DatabaseInfoTracer
		{
			get
			{
				if (ExTraceGlobals.databaseInfoTracer == null)
				{
					ExTraceGlobals.databaseInfoTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.databaseInfoTracer;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x00050DB9 File Offset: 0x0004EFB9
		public static Trace DatabaseManagerTracer
		{
			get
			{
				if (ExTraceGlobals.databaseManagerTracer == null)
				{
					ExTraceGlobals.databaseManagerTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.databaseManagerTracer;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x00050DD7 File Offset: 0x0004EFD7
		public static Trace ErrorHandlerTracer
		{
			get
			{
				if (ExTraceGlobals.errorHandlerTracer == null)
				{
					ExTraceGlobals.errorHandlerTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.errorHandlerTracer;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x0600132F RID: 4911 RVA: 0x00050DF5 File Offset: 0x0004EFF5
		public static Trace EventAccessTracer
		{
			get
			{
				if (ExTraceGlobals.eventAccessTracer == null)
				{
					ExTraceGlobals.eventAccessTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.eventAccessTracer;
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x00050E13 File Offset: 0x0004F013
		public static Trace EventControllerTracer
		{
			get
			{
				if (ExTraceGlobals.eventControllerTracer == null)
				{
					ExTraceGlobals.eventControllerTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.eventControllerTracer;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001331 RID: 4913 RVA: 0x00050E31 File Offset: 0x0004F031
		public static Trace EventDispatcherTracer
		{
			get
			{
				if (ExTraceGlobals.eventDispatcherTracer == null)
				{
					ExTraceGlobals.eventDispatcherTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.eventDispatcherTracer;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06001332 RID: 4914 RVA: 0x00050E4F File Offset: 0x0004F04F
		public static Trace EventBasedAssistantCollectionTracer
		{
			get
			{
				if (ExTraceGlobals.eventBasedAssistantCollectionTracer == null)
				{
					ExTraceGlobals.eventBasedAssistantCollectionTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.eventBasedAssistantCollectionTracer;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001333 RID: 4915 RVA: 0x00050E6D File Offset: 0x0004F06D
		public static Trace TimeBasedAssistantControllerTracer
		{
			get
			{
				if (ExTraceGlobals.timeBasedAssistantControllerTracer == null)
				{
					ExTraceGlobals.timeBasedAssistantControllerTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.timeBasedAssistantControllerTracer;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001334 RID: 4916 RVA: 0x00050E8B File Offset: 0x0004F08B
		public static Trace TimeBasedDatabaseDriverTracer
		{
			get
			{
				if (ExTraceGlobals.timeBasedDatabaseDriverTracer == null)
				{
					ExTraceGlobals.timeBasedDatabaseDriverTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.timeBasedDatabaseDriverTracer;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001335 RID: 4917 RVA: 0x00050EAA File Offset: 0x0004F0AA
		public static Trace TimeBasedDatabaseJobTracer
		{
			get
			{
				if (ExTraceGlobals.timeBasedDatabaseJobTracer == null)
				{
					ExTraceGlobals.timeBasedDatabaseJobTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.timeBasedDatabaseJobTracer;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x00050EC9 File Offset: 0x0004F0C9
		public static Trace TimeBasedDatabaseWindowJobTracer
		{
			get
			{
				if (ExTraceGlobals.timeBasedDatabaseWindowJobTracer == null)
				{
					ExTraceGlobals.timeBasedDatabaseWindowJobTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.timeBasedDatabaseWindowJobTracer;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x00050EE8 File Offset: 0x0004F0E8
		public static Trace TimeBasedDatabaseDemandJobTracer
		{
			get
			{
				if (ExTraceGlobals.timeBasedDatabaseDemandJobTracer == null)
				{
					ExTraceGlobals.timeBasedDatabaseDemandJobTracer = new Trace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.timeBasedDatabaseDemandJobTracer;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x00050F07 File Offset: 0x0004F107
		public static Trace TimeBasedDriverManagerTracer
		{
			get
			{
				if (ExTraceGlobals.timeBasedDriverManagerTracer == null)
				{
					ExTraceGlobals.timeBasedDriverManagerTracer = new Trace(ExTraceGlobals.componentGuid, 13);
				}
				return ExTraceGlobals.timeBasedDriverManagerTracer;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001339 RID: 4921 RVA: 0x00050F26 File Offset: 0x0004F126
		public static Trace OnlineDatabaseTracer
		{
			get
			{
				if (ExTraceGlobals.onlineDatabaseTracer == null)
				{
					ExTraceGlobals.onlineDatabaseTracer = new Trace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.onlineDatabaseTracer;
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x00050F45 File Offset: 0x0004F145
		public static Trace PoisonControlTracer
		{
			get
			{
				if (ExTraceGlobals.poisonControlTracer == null)
				{
					ExTraceGlobals.poisonControlTracer = new Trace(ExTraceGlobals.componentGuid, 15);
				}
				return ExTraceGlobals.poisonControlTracer;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x00050F64 File Offset: 0x0004F164
		public static Trace ThrottleTracer
		{
			get
			{
				if (ExTraceGlobals.throttleTracer == null)
				{
					ExTraceGlobals.throttleTracer = new Trace(ExTraceGlobals.componentGuid, 16);
				}
				return ExTraceGlobals.throttleTracer;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x00050F83 File Offset: 0x0004F183
		public static Trace PFDTracer
		{
			get
			{
				if (ExTraceGlobals.pFDTracer == null)
				{
					ExTraceGlobals.pFDTracer = new Trace(ExTraceGlobals.componentGuid, 17);
				}
				return ExTraceGlobals.pFDTracer;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x00050FA2 File Offset: 0x0004F1A2
		public static Trace GovernorTracer
		{
			get
			{
				if (ExTraceGlobals.governorTracer == null)
				{
					ExTraceGlobals.governorTracer = new Trace(ExTraceGlobals.componentGuid, 18);
				}
				return ExTraceGlobals.governorTracer;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x00050FC1 File Offset: 0x0004F1C1
		public static Trace QueueProcessorTracer
		{
			get
			{
				if (ExTraceGlobals.queueProcessorTracer == null)
				{
					ExTraceGlobals.queueProcessorTracer = new Trace(ExTraceGlobals.componentGuid, 19);
				}
				return ExTraceGlobals.queueProcessorTracer;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x00050FE0 File Offset: 0x0004F1E0
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

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x00050FFF File Offset: 0x0004F1FF
		public static Trace ProbeTimeBasedAssistantTracer
		{
			get
			{
				if (ExTraceGlobals.probeTimeBasedAssistantTracer == null)
				{
					ExTraceGlobals.probeTimeBasedAssistantTracer = new Trace(ExTraceGlobals.componentGuid, 21);
				}
				return ExTraceGlobals.probeTimeBasedAssistantTracer;
			}
		}

		// Token: 0x040017D9 RID: 6105
		private static Guid componentGuid = new Guid("EDC33045-05FB-4abb-A608-AEE572BC3C5F");

		// Token: 0x040017DA RID: 6106
		private static Trace assistantsRpcServerTracer = null;

		// Token: 0x040017DB RID: 6107
		private static Trace databaseInfoTracer = null;

		// Token: 0x040017DC RID: 6108
		private static Trace databaseManagerTracer = null;

		// Token: 0x040017DD RID: 6109
		private static Trace errorHandlerTracer = null;

		// Token: 0x040017DE RID: 6110
		private static Trace eventAccessTracer = null;

		// Token: 0x040017DF RID: 6111
		private static Trace eventControllerTracer = null;

		// Token: 0x040017E0 RID: 6112
		private static Trace eventDispatcherTracer = null;

		// Token: 0x040017E1 RID: 6113
		private static Trace eventBasedAssistantCollectionTracer = null;

		// Token: 0x040017E2 RID: 6114
		private static Trace timeBasedAssistantControllerTracer = null;

		// Token: 0x040017E3 RID: 6115
		private static Trace timeBasedDatabaseDriverTracer = null;

		// Token: 0x040017E4 RID: 6116
		private static Trace timeBasedDatabaseJobTracer = null;

		// Token: 0x040017E5 RID: 6117
		private static Trace timeBasedDatabaseWindowJobTracer = null;

		// Token: 0x040017E6 RID: 6118
		private static Trace timeBasedDatabaseDemandJobTracer = null;

		// Token: 0x040017E7 RID: 6119
		private static Trace timeBasedDriverManagerTracer = null;

		// Token: 0x040017E8 RID: 6120
		private static Trace onlineDatabaseTracer = null;

		// Token: 0x040017E9 RID: 6121
		private static Trace poisonControlTracer = null;

		// Token: 0x040017EA RID: 6122
		private static Trace throttleTracer = null;

		// Token: 0x040017EB RID: 6123
		private static Trace pFDTracer = null;

		// Token: 0x040017EC RID: 6124
		private static Trace governorTracer = null;

		// Token: 0x040017ED RID: 6125
		private static Trace queueProcessorTracer = null;

		// Token: 0x040017EE RID: 6126
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x040017EF RID: 6127
		private static Trace probeTimeBasedAssistantTracer = null;
	}
}
