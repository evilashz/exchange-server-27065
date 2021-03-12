using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ManagedStore.PhysicalAccess
{
	// Token: 0x02000395 RID: 917
	public static class ExTraceGlobals
	{
		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x000569B0 File Offset: 0x00054BB0
		public static Trace DbInteractionSummaryTracer
		{
			get
			{
				if (ExTraceGlobals.dbInteractionSummaryTracer == null)
				{
					ExTraceGlobals.dbInteractionSummaryTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.dbInteractionSummaryTracer;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x000569CE File Offset: 0x00054BCE
		public static Trace DbInteractionIntermediateTracer
		{
			get
			{
				if (ExTraceGlobals.dbInteractionIntermediateTracer == null)
				{
					ExTraceGlobals.dbInteractionIntermediateTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.dbInteractionIntermediateTracer;
			}
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x000569EC File Offset: 0x00054BEC
		public static Trace DbInteractionDetailTracer
		{
			get
			{
				if (ExTraceGlobals.dbInteractionDetailTracer == null)
				{
					ExTraceGlobals.dbInteractionDetailTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.dbInteractionDetailTracer;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x00056A0A File Offset: 0x00054C0A
		public static Trace DbInitializationTracer
		{
			get
			{
				if (ExTraceGlobals.dbInitializationTracer == null)
				{
					ExTraceGlobals.dbInitializationTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.dbInitializationTracer;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x060015DF RID: 5599 RVA: 0x00056A28 File Offset: 0x00054C28
		public static Trace DirtyObjectsTracer
		{
			get
			{
				if (ExTraceGlobals.dirtyObjectsTracer == null)
				{
					ExTraceGlobals.dirtyObjectsTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.dirtyObjectsTracer;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x00056A46 File Offset: 0x00054C46
		public static Trace DbIOTracer
		{
			get
			{
				if (ExTraceGlobals.dbIOTracer == null)
				{
					ExTraceGlobals.dbIOTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.dbIOTracer;
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x060015E1 RID: 5601 RVA: 0x00056A64 File Offset: 0x00054C64
		public static Trace BadPlanDetectionTracer
		{
			get
			{
				if (ExTraceGlobals.badPlanDetectionTracer == null)
				{
					ExTraceGlobals.badPlanDetectionTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.badPlanDetectionTracer;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x060015E2 RID: 5602 RVA: 0x00056A82 File Offset: 0x00054C82
		public static Trace CategorizedTableOperatorTracer
		{
			get
			{
				if (ExTraceGlobals.categorizedTableOperatorTracer == null)
				{
					ExTraceGlobals.categorizedTableOperatorTracer = new Trace(ExTraceGlobals.componentGuid, 15);
				}
				return ExTraceGlobals.categorizedTableOperatorTracer;
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x060015E3 RID: 5603 RVA: 0x00056AA1 File Offset: 0x00054CA1
		public static Trace SnapshotOperationTracer
		{
			get
			{
				if (ExTraceGlobals.snapshotOperationTracer == null)
				{
					ExTraceGlobals.snapshotOperationTracer = new Trace(ExTraceGlobals.componentGuid, 16);
				}
				return ExTraceGlobals.snapshotOperationTracer;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x00056AC0 File Offset: 0x00054CC0
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

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x00056ADF File Offset: 0x00054CDF
		public static Trace JetInformationTracer
		{
			get
			{
				if (ExTraceGlobals.jetInformationTracer == null)
				{
					ExTraceGlobals.jetInformationTracer = new Trace(ExTraceGlobals.componentGuid, 651);
				}
				return ExTraceGlobals.jetInformationTracer;
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x00056B01 File Offset: 0x00054D01
		public static Trace JetErrorsTracer
		{
			get
			{
				if (ExTraceGlobals.jetErrorsTracer == null)
				{
					ExTraceGlobals.jetErrorsTracer = new Trace(ExTraceGlobals.componentGuid, 652);
				}
				return ExTraceGlobals.jetErrorsTracer;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x060015E7 RID: 5607 RVA: 0x00056B23 File Offset: 0x00054D23
		public static Trace JetAssertsTracer
		{
			get
			{
				if (ExTraceGlobals.jetAssertsTracer == null)
				{
					ExTraceGlobals.jetAssertsTracer = new Trace(ExTraceGlobals.componentGuid, 653);
				}
				return ExTraceGlobals.jetAssertsTracer;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x00056B45 File Offset: 0x00054D45
		public static Trace JetAPITracer
		{
			get
			{
				if (ExTraceGlobals.jetAPITracer == null)
				{
					ExTraceGlobals.jetAPITracer = new Trace(ExTraceGlobals.componentGuid, 654);
				}
				return ExTraceGlobals.jetAPITracer;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x060015E9 RID: 5609 RVA: 0x00056B67 File Offset: 0x00054D67
		public static Trace JetInitTermTracer
		{
			get
			{
				if (ExTraceGlobals.jetInitTermTracer == null)
				{
					ExTraceGlobals.jetInitTermTracer = new Trace(ExTraceGlobals.componentGuid, 655);
				}
				return ExTraceGlobals.jetInitTermTracer;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x00056B89 File Offset: 0x00054D89
		public static Trace JetBufferManagerTracer
		{
			get
			{
				if (ExTraceGlobals.jetBufferManagerTracer == null)
				{
					ExTraceGlobals.jetBufferManagerTracer = new Trace(ExTraceGlobals.componentGuid, 656);
				}
				return ExTraceGlobals.jetBufferManagerTracer;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x060015EB RID: 5611 RVA: 0x00056BAB File Offset: 0x00054DAB
		public static Trace JetBufferManagerHashedLatchesTracer
		{
			get
			{
				if (ExTraceGlobals.jetBufferManagerHashedLatchesTracer == null)
				{
					ExTraceGlobals.jetBufferManagerHashedLatchesTracer = new Trace(ExTraceGlobals.componentGuid, 657);
				}
				return ExTraceGlobals.jetBufferManagerHashedLatchesTracer;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x060015EC RID: 5612 RVA: 0x00056BCD File Offset: 0x00054DCD
		public static Trace JetIOTracer
		{
			get
			{
				if (ExTraceGlobals.jetIOTracer == null)
				{
					ExTraceGlobals.jetIOTracer = new Trace(ExTraceGlobals.componentGuid, 658);
				}
				return ExTraceGlobals.jetIOTracer;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x060015ED RID: 5613 RVA: 0x00056BEF File Offset: 0x00054DEF
		public static Trace JetMemoryTracer
		{
			get
			{
				if (ExTraceGlobals.jetMemoryTracer == null)
				{
					ExTraceGlobals.jetMemoryTracer = new Trace(ExTraceGlobals.componentGuid, 659);
				}
				return ExTraceGlobals.jetMemoryTracer;
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x00056C11 File Offset: 0x00054E11
		public static Trace JetVersionStoreTracer
		{
			get
			{
				if (ExTraceGlobals.jetVersionStoreTracer == null)
				{
					ExTraceGlobals.jetVersionStoreTracer = new Trace(ExTraceGlobals.componentGuid, 660);
				}
				return ExTraceGlobals.jetVersionStoreTracer;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x060015EF RID: 5615 RVA: 0x00056C33 File Offset: 0x00054E33
		public static Trace JetVersionStoreOOMTracer
		{
			get
			{
				if (ExTraceGlobals.jetVersionStoreOOMTracer == null)
				{
					ExTraceGlobals.jetVersionStoreOOMTracer = new Trace(ExTraceGlobals.componentGuid, 661);
				}
				return ExTraceGlobals.jetVersionStoreOOMTracer;
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x00056C55 File Offset: 0x00054E55
		public static Trace JetVersionCleanupTracer
		{
			get
			{
				if (ExTraceGlobals.jetVersionCleanupTracer == null)
				{
					ExTraceGlobals.jetVersionCleanupTracer = new Trace(ExTraceGlobals.componentGuid, 662);
				}
				return ExTraceGlobals.jetVersionCleanupTracer;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x00056C77 File Offset: 0x00054E77
		public static Trace JetCatalogTracer
		{
			get
			{
				if (ExTraceGlobals.jetCatalogTracer == null)
				{
					ExTraceGlobals.jetCatalogTracer = new Trace(ExTraceGlobals.componentGuid, 663);
				}
				return ExTraceGlobals.jetCatalogTracer;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x00056C99 File Offset: 0x00054E99
		public static Trace JetDDLReadTracer
		{
			get
			{
				if (ExTraceGlobals.jetDDLReadTracer == null)
				{
					ExTraceGlobals.jetDDLReadTracer = new Trace(ExTraceGlobals.componentGuid, 664);
				}
				return ExTraceGlobals.jetDDLReadTracer;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x00056CBB File Offset: 0x00054EBB
		public static Trace JetDDLWriteTracer
		{
			get
			{
				if (ExTraceGlobals.jetDDLWriteTracer == null)
				{
					ExTraceGlobals.jetDDLWriteTracer = new Trace(ExTraceGlobals.componentGuid, 665);
				}
				return ExTraceGlobals.jetDDLWriteTracer;
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x00056CDD File Offset: 0x00054EDD
		public static Trace JetDMLReadTracer
		{
			get
			{
				if (ExTraceGlobals.jetDMLReadTracer == null)
				{
					ExTraceGlobals.jetDMLReadTracer = new Trace(ExTraceGlobals.componentGuid, 666);
				}
				return ExTraceGlobals.jetDMLReadTracer;
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x060015F5 RID: 5621 RVA: 0x00056CFF File Offset: 0x00054EFF
		public static Trace JetDMLWriteTracer
		{
			get
			{
				if (ExTraceGlobals.jetDMLWriteTracer == null)
				{
					ExTraceGlobals.jetDMLWriteTracer = new Trace(ExTraceGlobals.componentGuid, 667);
				}
				return ExTraceGlobals.jetDMLWriteTracer;
			}
		}

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x00056D21 File Offset: 0x00054F21
		public static Trace JetDMLConflictsTracer
		{
			get
			{
				if (ExTraceGlobals.jetDMLConflictsTracer == null)
				{
					ExTraceGlobals.jetDMLConflictsTracer = new Trace(ExTraceGlobals.componentGuid, 668);
				}
				return ExTraceGlobals.jetDMLConflictsTracer;
			}
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x060015F7 RID: 5623 RVA: 0x00056D43 File Offset: 0x00054F43
		public static Trace JetInstancesTracer
		{
			get
			{
				if (ExTraceGlobals.jetInstancesTracer == null)
				{
					ExTraceGlobals.jetInstancesTracer = new Trace(ExTraceGlobals.componentGuid, 669);
				}
				return ExTraceGlobals.jetInstancesTracer;
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x00056D65 File Offset: 0x00054F65
		public static Trace JetDatabasesTracer
		{
			get
			{
				if (ExTraceGlobals.jetDatabasesTracer == null)
				{
					ExTraceGlobals.jetDatabasesTracer = new Trace(ExTraceGlobals.componentGuid, 670);
				}
				return ExTraceGlobals.jetDatabasesTracer;
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x060015F9 RID: 5625 RVA: 0x00056D87 File Offset: 0x00054F87
		public static Trace JetSessionsTracer
		{
			get
			{
				if (ExTraceGlobals.jetSessionsTracer == null)
				{
					ExTraceGlobals.jetSessionsTracer = new Trace(ExTraceGlobals.componentGuid, 671);
				}
				return ExTraceGlobals.jetSessionsTracer;
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x00056DA9 File Offset: 0x00054FA9
		public static Trace JetCursorsTracer
		{
			get
			{
				if (ExTraceGlobals.jetCursorsTracer == null)
				{
					ExTraceGlobals.jetCursorsTracer = new Trace(ExTraceGlobals.componentGuid, 672);
				}
				return ExTraceGlobals.jetCursorsTracer;
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x060015FB RID: 5627 RVA: 0x00056DCB File Offset: 0x00054FCB
		public static Trace JetCursorNavigationTracer
		{
			get
			{
				if (ExTraceGlobals.jetCursorNavigationTracer == null)
				{
					ExTraceGlobals.jetCursorNavigationTracer = new Trace(ExTraceGlobals.componentGuid, 673);
				}
				return ExTraceGlobals.jetCursorNavigationTracer;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x00056DED File Offset: 0x00054FED
		public static Trace JetCursorPageRefsTracer
		{
			get
			{
				if (ExTraceGlobals.jetCursorPageRefsTracer == null)
				{
					ExTraceGlobals.jetCursorPageRefsTracer = new Trace(ExTraceGlobals.componentGuid, 674);
				}
				return ExTraceGlobals.jetCursorPageRefsTracer;
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x060015FD RID: 5629 RVA: 0x00056E0F File Offset: 0x0005500F
		public static Trace JetBtreeTracer
		{
			get
			{
				if (ExTraceGlobals.jetBtreeTracer == null)
				{
					ExTraceGlobals.jetBtreeTracer = new Trace(ExTraceGlobals.componentGuid, 675);
				}
				return ExTraceGlobals.jetBtreeTracer;
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x00056E31 File Offset: 0x00055031
		public static Trace JetSpaceTracer
		{
			get
			{
				if (ExTraceGlobals.jetSpaceTracer == null)
				{
					ExTraceGlobals.jetSpaceTracer = new Trace(ExTraceGlobals.componentGuid, 676);
				}
				return ExTraceGlobals.jetSpaceTracer;
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x00056E53 File Offset: 0x00055053
		public static Trace JetFCBsTracer
		{
			get
			{
				if (ExTraceGlobals.jetFCBsTracer == null)
				{
					ExTraceGlobals.jetFCBsTracer = new Trace(ExTraceGlobals.componentGuid, 677);
				}
				return ExTraceGlobals.jetFCBsTracer;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06001600 RID: 5632 RVA: 0x00056E75 File Offset: 0x00055075
		public static Trace JetTransactionsTracer
		{
			get
			{
				if (ExTraceGlobals.jetTransactionsTracer == null)
				{
					ExTraceGlobals.jetTransactionsTracer = new Trace(ExTraceGlobals.componentGuid, 678);
				}
				return ExTraceGlobals.jetTransactionsTracer;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x00056E97 File Offset: 0x00055097
		public static Trace JetLoggingTracer
		{
			get
			{
				if (ExTraceGlobals.jetLoggingTracer == null)
				{
					ExTraceGlobals.jetLoggingTracer = new Trace(ExTraceGlobals.componentGuid, 679);
				}
				return ExTraceGlobals.jetLoggingTracer;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06001602 RID: 5634 RVA: 0x00056EB9 File Offset: 0x000550B9
		public static Trace JetRecoveryTracer
		{
			get
			{
				if (ExTraceGlobals.jetRecoveryTracer == null)
				{
					ExTraceGlobals.jetRecoveryTracer = new Trace(ExTraceGlobals.componentGuid, 680);
				}
				return ExTraceGlobals.jetRecoveryTracer;
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x00056EDB File Offset: 0x000550DB
		public static Trace JetBackupTracer
		{
			get
			{
				if (ExTraceGlobals.jetBackupTracer == null)
				{
					ExTraceGlobals.jetBackupTracer = new Trace(ExTraceGlobals.componentGuid, 681);
				}
				return ExTraceGlobals.jetBackupTracer;
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06001604 RID: 5636 RVA: 0x00056EFD File Offset: 0x000550FD
		public static Trace JetRestoreTracer
		{
			get
			{
				if (ExTraceGlobals.jetRestoreTracer == null)
				{
					ExTraceGlobals.jetRestoreTracer = new Trace(ExTraceGlobals.componentGuid, 682);
				}
				return ExTraceGlobals.jetRestoreTracer;
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06001605 RID: 5637 RVA: 0x00056F1F File Offset: 0x0005511F
		public static Trace JetOLDTracer
		{
			get
			{
				if (ExTraceGlobals.jetOLDTracer == null)
				{
					ExTraceGlobals.jetOLDTracer = new Trace(ExTraceGlobals.componentGuid, 683);
				}
				return ExTraceGlobals.jetOLDTracer;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06001606 RID: 5638 RVA: 0x00056F41 File Offset: 0x00055141
		public static Trace JetEventlogTracer
		{
			get
			{
				if (ExTraceGlobals.jetEventlogTracer == null)
				{
					ExTraceGlobals.jetEventlogTracer = new Trace(ExTraceGlobals.componentGuid, 684);
				}
				return ExTraceGlobals.jetEventlogTracer;
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06001607 RID: 5639 RVA: 0x00056F63 File Offset: 0x00055163
		public static Trace JetBufferManagerMaintTasksTracer
		{
			get
			{
				if (ExTraceGlobals.jetBufferManagerMaintTasksTracer == null)
				{
					ExTraceGlobals.jetBufferManagerMaintTasksTracer = new Trace(ExTraceGlobals.componentGuid, 685);
				}
				return ExTraceGlobals.jetBufferManagerMaintTasksTracer;
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x00056F85 File Offset: 0x00055185
		public static Trace JetSpaceManagementTracer
		{
			get
			{
				if (ExTraceGlobals.jetSpaceManagementTracer == null)
				{
					ExTraceGlobals.jetSpaceManagementTracer = new Trace(ExTraceGlobals.componentGuid, 686);
				}
				return ExTraceGlobals.jetSpaceManagementTracer;
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x00056FA7 File Offset: 0x000551A7
		public static Trace JetSpaceInternalTracer
		{
			get
			{
				if (ExTraceGlobals.jetSpaceInternalTracer == null)
				{
					ExTraceGlobals.jetSpaceInternalTracer = new Trace(ExTraceGlobals.componentGuid, 687);
				}
				return ExTraceGlobals.jetSpaceInternalTracer;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x00056FC9 File Offset: 0x000551C9
		public static Trace JetIOQueueTracer
		{
			get
			{
				if (ExTraceGlobals.jetIOQueueTracer == null)
				{
					ExTraceGlobals.jetIOQueueTracer = new Trace(ExTraceGlobals.componentGuid, 688);
				}
				return ExTraceGlobals.jetIOQueueTracer;
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x00056FEB File Offset: 0x000551EB
		public static Trace JetDiskVolumeManagementTracer
		{
			get
			{
				if (ExTraceGlobals.jetDiskVolumeManagementTracer == null)
				{
					ExTraceGlobals.jetDiskVolumeManagementTracer = new Trace(ExTraceGlobals.componentGuid, 689);
				}
				return ExTraceGlobals.jetDiskVolumeManagementTracer;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x0005700D File Offset: 0x0005520D
		public static Trace JetCallbacksTracer
		{
			get
			{
				if (ExTraceGlobals.jetCallbacksTracer == null)
				{
					ExTraceGlobals.jetCallbacksTracer = new Trace(ExTraceGlobals.componentGuid, 690);
				}
				return ExTraceGlobals.jetCallbacksTracer;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x0005702F File Offset: 0x0005522F
		public static Trace JetIOProblemsTracer
		{
			get
			{
				if (ExTraceGlobals.jetIOProblemsTracer == null)
				{
					ExTraceGlobals.jetIOProblemsTracer = new Trace(ExTraceGlobals.componentGuid, 691);
				}
				return ExTraceGlobals.jetIOProblemsTracer;
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x0600160E RID: 5646 RVA: 0x00057051 File Offset: 0x00055251
		public static Trace JetUpgradeTracer
		{
			get
			{
				if (ExTraceGlobals.jetUpgradeTracer == null)
				{
					ExTraceGlobals.jetUpgradeTracer = new Trace(ExTraceGlobals.componentGuid, 692);
				}
				return ExTraceGlobals.jetUpgradeTracer;
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x00057073 File Offset: 0x00055273
		public static Trace JetBufMgrCacheStateTracer
		{
			get
			{
				if (ExTraceGlobals.jetBufMgrCacheStateTracer == null)
				{
					ExTraceGlobals.jetBufMgrCacheStateTracer = new Trace(ExTraceGlobals.componentGuid, 693);
				}
				return ExTraceGlobals.jetBufMgrCacheStateTracer;
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06001610 RID: 5648 RVA: 0x00057095 File Offset: 0x00055295
		public static Trace JetBufMgrDirtyStateTracer
		{
			get
			{
				if (ExTraceGlobals.jetBufMgrDirtyStateTracer == null)
				{
					ExTraceGlobals.jetBufMgrDirtyStateTracer = new Trace(ExTraceGlobals.componentGuid, 694);
				}
				return ExTraceGlobals.jetBufMgrDirtyStateTracer;
			}
		}

		// Token: 0x04001A89 RID: 6793
		private static Guid componentGuid = new Guid("40c22f16-f297-499a-b812-a5a352295610");

		// Token: 0x04001A8A RID: 6794
		private static Trace dbInteractionSummaryTracer = null;

		// Token: 0x04001A8B RID: 6795
		private static Trace dbInteractionIntermediateTracer = null;

		// Token: 0x04001A8C RID: 6796
		private static Trace dbInteractionDetailTracer = null;

		// Token: 0x04001A8D RID: 6797
		private static Trace dbInitializationTracer = null;

		// Token: 0x04001A8E RID: 6798
		private static Trace dirtyObjectsTracer = null;

		// Token: 0x04001A8F RID: 6799
		private static Trace dbIOTracer = null;

		// Token: 0x04001A90 RID: 6800
		private static Trace badPlanDetectionTracer = null;

		// Token: 0x04001A91 RID: 6801
		private static Trace categorizedTableOperatorTracer = null;

		// Token: 0x04001A92 RID: 6802
		private static Trace snapshotOperationTracer = null;

		// Token: 0x04001A93 RID: 6803
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001A94 RID: 6804
		private static Trace jetInformationTracer = null;

		// Token: 0x04001A95 RID: 6805
		private static Trace jetErrorsTracer = null;

		// Token: 0x04001A96 RID: 6806
		private static Trace jetAssertsTracer = null;

		// Token: 0x04001A97 RID: 6807
		private static Trace jetAPITracer = null;

		// Token: 0x04001A98 RID: 6808
		private static Trace jetInitTermTracer = null;

		// Token: 0x04001A99 RID: 6809
		private static Trace jetBufferManagerTracer = null;

		// Token: 0x04001A9A RID: 6810
		private static Trace jetBufferManagerHashedLatchesTracer = null;

		// Token: 0x04001A9B RID: 6811
		private static Trace jetIOTracer = null;

		// Token: 0x04001A9C RID: 6812
		private static Trace jetMemoryTracer = null;

		// Token: 0x04001A9D RID: 6813
		private static Trace jetVersionStoreTracer = null;

		// Token: 0x04001A9E RID: 6814
		private static Trace jetVersionStoreOOMTracer = null;

		// Token: 0x04001A9F RID: 6815
		private static Trace jetVersionCleanupTracer = null;

		// Token: 0x04001AA0 RID: 6816
		private static Trace jetCatalogTracer = null;

		// Token: 0x04001AA1 RID: 6817
		private static Trace jetDDLReadTracer = null;

		// Token: 0x04001AA2 RID: 6818
		private static Trace jetDDLWriteTracer = null;

		// Token: 0x04001AA3 RID: 6819
		private static Trace jetDMLReadTracer = null;

		// Token: 0x04001AA4 RID: 6820
		private static Trace jetDMLWriteTracer = null;

		// Token: 0x04001AA5 RID: 6821
		private static Trace jetDMLConflictsTracer = null;

		// Token: 0x04001AA6 RID: 6822
		private static Trace jetInstancesTracer = null;

		// Token: 0x04001AA7 RID: 6823
		private static Trace jetDatabasesTracer = null;

		// Token: 0x04001AA8 RID: 6824
		private static Trace jetSessionsTracer = null;

		// Token: 0x04001AA9 RID: 6825
		private static Trace jetCursorsTracer = null;

		// Token: 0x04001AAA RID: 6826
		private static Trace jetCursorNavigationTracer = null;

		// Token: 0x04001AAB RID: 6827
		private static Trace jetCursorPageRefsTracer = null;

		// Token: 0x04001AAC RID: 6828
		private static Trace jetBtreeTracer = null;

		// Token: 0x04001AAD RID: 6829
		private static Trace jetSpaceTracer = null;

		// Token: 0x04001AAE RID: 6830
		private static Trace jetFCBsTracer = null;

		// Token: 0x04001AAF RID: 6831
		private static Trace jetTransactionsTracer = null;

		// Token: 0x04001AB0 RID: 6832
		private static Trace jetLoggingTracer = null;

		// Token: 0x04001AB1 RID: 6833
		private static Trace jetRecoveryTracer = null;

		// Token: 0x04001AB2 RID: 6834
		private static Trace jetBackupTracer = null;

		// Token: 0x04001AB3 RID: 6835
		private static Trace jetRestoreTracer = null;

		// Token: 0x04001AB4 RID: 6836
		private static Trace jetOLDTracer = null;

		// Token: 0x04001AB5 RID: 6837
		private static Trace jetEventlogTracer = null;

		// Token: 0x04001AB6 RID: 6838
		private static Trace jetBufferManagerMaintTasksTracer = null;

		// Token: 0x04001AB7 RID: 6839
		private static Trace jetSpaceManagementTracer = null;

		// Token: 0x04001AB8 RID: 6840
		private static Trace jetSpaceInternalTracer = null;

		// Token: 0x04001AB9 RID: 6841
		private static Trace jetIOQueueTracer = null;

		// Token: 0x04001ABA RID: 6842
		private static Trace jetDiskVolumeManagementTracer = null;

		// Token: 0x04001ABB RID: 6843
		private static Trace jetCallbacksTracer = null;

		// Token: 0x04001ABC RID: 6844
		private static Trace jetIOProblemsTracer = null;

		// Token: 0x04001ABD RID: 6845
		private static Trace jetUpgradeTracer = null;

		// Token: 0x04001ABE RID: 6846
		private static Trace jetBufMgrCacheStateTracer = null;

		// Token: 0x04001ABF RID: 6847
		private static Trace jetBufMgrDirtyStateTracer = null;
	}
}
