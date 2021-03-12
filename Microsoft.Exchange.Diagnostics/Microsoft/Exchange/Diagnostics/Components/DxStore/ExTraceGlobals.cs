using System;

namespace Microsoft.Exchange.Diagnostics.Components.DxStore
{
	// Token: 0x02000408 RID: 1032
	public static class ExTraceGlobals
	{
		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x0005CF8C File Offset: 0x0005B18C
		public static Trace AccessTracer
		{
			get
			{
				if (ExTraceGlobals.accessTracer == null)
				{
					ExTraceGlobals.accessTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.accessTracer;
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x060018DC RID: 6364 RVA: 0x0005CFAA File Offset: 0x0005B1AA
		public static Trace ManagerTracer
		{
			get
			{
				if (ExTraceGlobals.managerTracer == null)
				{
					ExTraceGlobals.managerTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.managerTracer;
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x0005CFC8 File Offset: 0x0005B1C8
		public static Trace InstanceTracer
		{
			get
			{
				if (ExTraceGlobals.instanceTracer == null)
				{
					ExTraceGlobals.instanceTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.instanceTracer;
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x060018DE RID: 6366 RVA: 0x0005CFE6 File Offset: 0x0005B1E6
		public static Trace PaxosMessageTracer
		{
			get
			{
				if (ExTraceGlobals.paxosMessageTracer == null)
				{
					ExTraceGlobals.paxosMessageTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.paxosMessageTracer;
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x060018DF RID: 6367 RVA: 0x0005D004 File Offset: 0x0005B204
		public static Trace HealthCheckerTracer
		{
			get
			{
				if (ExTraceGlobals.healthCheckerTracer == null)
				{
					ExTraceGlobals.healthCheckerTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.healthCheckerTracer;
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x060018E0 RID: 6368 RVA: 0x0005D022 File Offset: 0x0005B222
		public static Trace StateMachineTracer
		{
			get
			{
				if (ExTraceGlobals.stateMachineTracer == null)
				{
					ExTraceGlobals.stateMachineTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.stateMachineTracer;
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x060018E1 RID: 6369 RVA: 0x0005D040 File Offset: 0x0005B240
		public static Trace TruncatorTracer
		{
			get
			{
				if (ExTraceGlobals.truncatorTracer == null)
				{
					ExTraceGlobals.truncatorTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.truncatorTracer;
			}
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x0005D05E File Offset: 0x0005B25E
		public static Trace SnapshotTracer
		{
			get
			{
				if (ExTraceGlobals.snapshotTracer == null)
				{
					ExTraceGlobals.snapshotTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.snapshotTracer;
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x060018E3 RID: 6371 RVA: 0x0005D07C File Offset: 0x0005B27C
		public static Trace LocalStoreTracer
		{
			get
			{
				if (ExTraceGlobals.localStoreTracer == null)
				{
					ExTraceGlobals.localStoreTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.localStoreTracer;
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x060018E4 RID: 6372 RVA: 0x0005D09A File Offset: 0x0005B29A
		public static Trace UtilsTracer
		{
			get
			{
				if (ExTraceGlobals.utilsTracer == null)
				{
					ExTraceGlobals.utilsTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.utilsTracer;
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x060018E5 RID: 6373 RVA: 0x0005D0B9 File Offset: 0x0005B2B9
		public static Trace ConfigTracer
		{
			get
			{
				if (ExTraceGlobals.configTracer == null)
				{
					ExTraceGlobals.configTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.configTracer;
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x060018E6 RID: 6374 RVA: 0x0005D0D8 File Offset: 0x0005B2D8
		public static Trace MeshTracer
		{
			get
			{
				if (ExTraceGlobals.meshTracer == null)
				{
					ExTraceGlobals.meshTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.meshTracer;
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x060018E7 RID: 6375 RVA: 0x0005D0F7 File Offset: 0x0005B2F7
		public static Trace AccessClientTracer
		{
			get
			{
				if (ExTraceGlobals.accessClientTracer == null)
				{
					ExTraceGlobals.accessClientTracer = new Trace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.accessClientTracer;
			}
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x060018E8 RID: 6376 RVA: 0x0005D116 File Offset: 0x0005B316
		public static Trace ManagerClientTracer
		{
			get
			{
				if (ExTraceGlobals.managerClientTracer == null)
				{
					ExTraceGlobals.managerClientTracer = new Trace(ExTraceGlobals.componentGuid, 13);
				}
				return ExTraceGlobals.managerClientTracer;
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x060018E9 RID: 6377 RVA: 0x0005D135 File Offset: 0x0005B335
		public static Trace InstanceClientTracer
		{
			get
			{
				if (ExTraceGlobals.instanceClientTracer == null)
				{
					ExTraceGlobals.instanceClientTracer = new Trace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.instanceClientTracer;
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x0005D154 File Offset: 0x0005B354
		public static Trace StoreReadTracer
		{
			get
			{
				if (ExTraceGlobals.storeReadTracer == null)
				{
					ExTraceGlobals.storeReadTracer = new Trace(ExTraceGlobals.componentGuid, 15);
				}
				return ExTraceGlobals.storeReadTracer;
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x060018EB RID: 6379 RVA: 0x0005D173 File Offset: 0x0005B373
		public static Trace StoreWriteTracer
		{
			get
			{
				if (ExTraceGlobals.storeWriteTracer == null)
				{
					ExTraceGlobals.storeWriteTracer = new Trace(ExTraceGlobals.componentGuid, 16);
				}
				return ExTraceGlobals.storeWriteTracer;
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x060018EC RID: 6380 RVA: 0x0005D192 File Offset: 0x0005B392
		public static Trace AccessEntryPointTracer
		{
			get
			{
				if (ExTraceGlobals.accessEntryPointTracer == null)
				{
					ExTraceGlobals.accessEntryPointTracer = new Trace(ExTraceGlobals.componentGuid, 18);
				}
				return ExTraceGlobals.accessEntryPointTracer;
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x060018ED RID: 6381 RVA: 0x0005D1B1 File Offset: 0x0005B3B1
		public static Trace ManagerEntryPointTracer
		{
			get
			{
				if (ExTraceGlobals.managerEntryPointTracer == null)
				{
					ExTraceGlobals.managerEntryPointTracer = new Trace(ExTraceGlobals.componentGuid, 19);
				}
				return ExTraceGlobals.managerEntryPointTracer;
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x060018EE RID: 6382 RVA: 0x0005D1D0 File Offset: 0x0005B3D0
		public static Trace InstanceEntryPointTracer
		{
			get
			{
				if (ExTraceGlobals.instanceEntryPointTracer == null)
				{
					ExTraceGlobals.instanceEntryPointTracer = new Trace(ExTraceGlobals.componentGuid, 20);
				}
				return ExTraceGlobals.instanceEntryPointTracer;
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x060018EF RID: 6383 RVA: 0x0005D1EF File Offset: 0x0005B3EF
		public static Trace RunOperationTracer
		{
			get
			{
				if (ExTraceGlobals.runOperationTracer == null)
				{
					ExTraceGlobals.runOperationTracer = new Trace(ExTraceGlobals.componentGuid, 21);
				}
				return ExTraceGlobals.runOperationTracer;
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x060018F0 RID: 6384 RVA: 0x0005D20E File Offset: 0x0005B40E
		public static Trace CommitAckTracer
		{
			get
			{
				if (ExTraceGlobals.commitAckTracer == null)
				{
					ExTraceGlobals.commitAckTracer = new Trace(ExTraceGlobals.componentGuid, 22);
				}
				return ExTraceGlobals.commitAckTracer;
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x060018F1 RID: 6385 RVA: 0x0005D22D File Offset: 0x0005B42D
		public static Trace EventLoggerTracer
		{
			get
			{
				if (ExTraceGlobals.eventLoggerTracer == null)
				{
					ExTraceGlobals.eventLoggerTracer = new Trace(ExTraceGlobals.componentGuid, 23);
				}
				return ExTraceGlobals.eventLoggerTracer;
			}
		}

		// Token: 0x04001D89 RID: 7561
		private static Guid componentGuid = new Guid("{3C3F940E-234C-442E-A30B-A78F146F8C10}");

		// Token: 0x04001D8A RID: 7562
		private static Trace accessTracer = null;

		// Token: 0x04001D8B RID: 7563
		private static Trace managerTracer = null;

		// Token: 0x04001D8C RID: 7564
		private static Trace instanceTracer = null;

		// Token: 0x04001D8D RID: 7565
		private static Trace paxosMessageTracer = null;

		// Token: 0x04001D8E RID: 7566
		private static Trace healthCheckerTracer = null;

		// Token: 0x04001D8F RID: 7567
		private static Trace stateMachineTracer = null;

		// Token: 0x04001D90 RID: 7568
		private static Trace truncatorTracer = null;

		// Token: 0x04001D91 RID: 7569
		private static Trace snapshotTracer = null;

		// Token: 0x04001D92 RID: 7570
		private static Trace localStoreTracer = null;

		// Token: 0x04001D93 RID: 7571
		private static Trace utilsTracer = null;

		// Token: 0x04001D94 RID: 7572
		private static Trace configTracer = null;

		// Token: 0x04001D95 RID: 7573
		private static Trace meshTracer = null;

		// Token: 0x04001D96 RID: 7574
		private static Trace accessClientTracer = null;

		// Token: 0x04001D97 RID: 7575
		private static Trace managerClientTracer = null;

		// Token: 0x04001D98 RID: 7576
		private static Trace instanceClientTracer = null;

		// Token: 0x04001D99 RID: 7577
		private static Trace storeReadTracer = null;

		// Token: 0x04001D9A RID: 7578
		private static Trace storeWriteTracer = null;

		// Token: 0x04001D9B RID: 7579
		private static Trace accessEntryPointTracer = null;

		// Token: 0x04001D9C RID: 7580
		private static Trace managerEntryPointTracer = null;

		// Token: 0x04001D9D RID: 7581
		private static Trace instanceEntryPointTracer = null;

		// Token: 0x04001D9E RID: 7582
		private static Trace runOperationTracer = null;

		// Token: 0x04001D9F RID: 7583
		private static Trace commitAckTracer = null;

		// Token: 0x04001DA0 RID: 7584
		private static Trace eventLoggerTracer = null;
	}
}
