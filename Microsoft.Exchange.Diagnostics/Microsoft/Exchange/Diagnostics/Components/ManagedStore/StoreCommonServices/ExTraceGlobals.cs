using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ManagedStore.StoreCommonServices
{
	// Token: 0x02000396 RID: 918
	public static class ExTraceGlobals
	{
		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06001612 RID: 5650 RVA: 0x00057218 File Offset: 0x00055418
		public static Trace ContextTracer
		{
			get
			{
				if (ExTraceGlobals.contextTracer == null)
				{
					ExTraceGlobals.contextTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.contextTracer;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x00057236 File Offset: 0x00055436
		public static Trace MailboxTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxTracer == null)
				{
					ExTraceGlobals.mailboxTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.mailboxTracer;
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06001614 RID: 5652 RVA: 0x00057254 File Offset: 0x00055454
		public static Trace ExtendedPropsTracer
		{
			get
			{
				if (ExTraceGlobals.extendedPropsTracer == null)
				{
					ExTraceGlobals.extendedPropsTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.extendedPropsTracer;
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x00057272 File Offset: 0x00055472
		public static Trace QueryPlannerSummaryTracer
		{
			get
			{
				if (ExTraceGlobals.queryPlannerSummaryTracer == null)
				{
					ExTraceGlobals.queryPlannerSummaryTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.queryPlannerSummaryTracer;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06001616 RID: 5654 RVA: 0x00057290 File Offset: 0x00055490
		public static Trace QueryPlannerDetailTracer
		{
			get
			{
				if (ExTraceGlobals.queryPlannerDetailTracer == null)
				{
					ExTraceGlobals.queryPlannerDetailTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.queryPlannerDetailTracer;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06001617 RID: 5655 RVA: 0x000572AE File Offset: 0x000554AE
		public static Trace SecurityMailboxOwnerAccessTracer
		{
			get
			{
				if (ExTraceGlobals.securityMailboxOwnerAccessTracer == null)
				{
					ExTraceGlobals.securityMailboxOwnerAccessTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.securityMailboxOwnerAccessTracer;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06001618 RID: 5656 RVA: 0x000572CC File Offset: 0x000554CC
		public static Trace SecurityAdminAccessTracer
		{
			get
			{
				if (ExTraceGlobals.securityAdminAccessTracer == null)
				{
					ExTraceGlobals.securityAdminAccessTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.securityAdminAccessTracer;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06001619 RID: 5657 RVA: 0x000572EA File Offset: 0x000554EA
		public static Trace SecurityServiceAccessTracer
		{
			get
			{
				if (ExTraceGlobals.securityServiceAccessTracer == null)
				{
					ExTraceGlobals.securityServiceAccessTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.securityServiceAccessTracer;
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x0600161A RID: 5658 RVA: 0x00057308 File Offset: 0x00055508
		public static Trace SecuritySendAsAccessTracer
		{
			get
			{
				if (ExTraceGlobals.securitySendAsAccessTracer == null)
				{
					ExTraceGlobals.securitySendAsAccessTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.securitySendAsAccessTracer;
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x0600161B RID: 5659 RVA: 0x00057326 File Offset: 0x00055526
		public static Trace SecurityContextTracer
		{
			get
			{
				if (ExTraceGlobals.securityContextTracer == null)
				{
					ExTraceGlobals.securityContextTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.securityContextTracer;
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x0600161C RID: 5660 RVA: 0x00057345 File Offset: 0x00055545
		public static Trace SecurityDescriptorTracer
		{
			get
			{
				if (ExTraceGlobals.securityDescriptorTracer == null)
				{
					ExTraceGlobals.securityDescriptorTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.securityDescriptorTracer;
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x0600161D RID: 5661 RVA: 0x00057364 File Offset: 0x00055564
		public static Trace FullAccountNameTracer
		{
			get
			{
				if (ExTraceGlobals.fullAccountNameTracer == null)
				{
					ExTraceGlobals.fullAccountNameTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.fullAccountNameTracer;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x00057383 File Offset: 0x00055583
		public static Trace ExecutionDiagnosticsTracer
		{
			get
			{
				if (ExTraceGlobals.executionDiagnosticsTracer == null)
				{
					ExTraceGlobals.executionDiagnosticsTracer = new Trace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.executionDiagnosticsTracer;
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x0600161F RID: 5663 RVA: 0x000573A2 File Offset: 0x000555A2
		public static Trace FullTextIndexTracer
		{
			get
			{
				if (ExTraceGlobals.fullTextIndexTracer == null)
				{
					ExTraceGlobals.fullTextIndexTracer = new Trace(ExTraceGlobals.componentGuid, 13);
				}
				return ExTraceGlobals.fullTextIndexTracer;
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x000573C1 File Offset: 0x000555C1
		public static Trace MailboxQuarantineTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxQuarantineTracer == null)
				{
					ExTraceGlobals.mailboxQuarantineTracer = new Trace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.mailboxQuarantineTracer;
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x000573E0 File Offset: 0x000555E0
		public static Trace MailboxSignatureTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxSignatureTracer == null)
				{
					ExTraceGlobals.mailboxSignatureTracer = new Trace(ExTraceGlobals.componentGuid, 15);
				}
				return ExTraceGlobals.mailboxSignatureTracer;
			}
		}

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x000573FF File Offset: 0x000555FF
		public static Trace TimedEventsTracer
		{
			get
			{
				if (ExTraceGlobals.timedEventsTracer == null)
				{
					ExTraceGlobals.timedEventsTracer = new Trace(ExTraceGlobals.componentGuid, 16);
				}
				return ExTraceGlobals.timedEventsTracer;
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06001623 RID: 5667 RVA: 0x0005741E File Offset: 0x0005561E
		public static Trace MaintenanceTracer
		{
			get
			{
				if (ExTraceGlobals.maintenanceTracer == null)
				{
					ExTraceGlobals.maintenanceTracer = new Trace(ExTraceGlobals.componentGuid, 17);
				}
				return ExTraceGlobals.maintenanceTracer;
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x0005743D File Offset: 0x0005563D
		public static Trace MailboxLockTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxLockTracer == null)
				{
					ExTraceGlobals.mailboxLockTracer = new Trace(ExTraceGlobals.componentGuid, 18);
				}
				return ExTraceGlobals.mailboxLockTracer;
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06001625 RID: 5669 RVA: 0x0005745C File Offset: 0x0005565C
		public static Trace CriticalBlockTracer
		{
			get
			{
				if (ExTraceGlobals.criticalBlockTracer == null)
				{
					ExTraceGlobals.criticalBlockTracer = new Trace(ExTraceGlobals.componentGuid, 19);
				}
				return ExTraceGlobals.criticalBlockTracer;
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06001626 RID: 5670 RVA: 0x0005747B File Offset: 0x0005567B
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

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06001627 RID: 5671 RVA: 0x0005749A File Offset: 0x0005569A
		public static Trace NotificationTracer
		{
			get
			{
				if (ExTraceGlobals.notificationTracer == null)
				{
					ExTraceGlobals.notificationTracer = new Trace(ExTraceGlobals.componentGuid, 21);
				}
				return ExTraceGlobals.notificationTracer;
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06001628 RID: 5672 RVA: 0x000574B9 File Offset: 0x000556B9
		public static Trace StoreDatabaseTracer
		{
			get
			{
				if (ExTraceGlobals.storeDatabaseTracer == null)
				{
					ExTraceGlobals.storeDatabaseTracer = new Trace(ExTraceGlobals.componentGuid, 22);
				}
				return ExTraceGlobals.storeDatabaseTracer;
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06001629 RID: 5673 RVA: 0x000574D8 File Offset: 0x000556D8
		public static Trace MailboxCountersTracer
		{
			get
			{
				if (ExTraceGlobals.mailboxCountersTracer == null)
				{
					ExTraceGlobals.mailboxCountersTracer = new Trace(ExTraceGlobals.componentGuid, 23);
				}
				return ExTraceGlobals.mailboxCountersTracer;
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x0600162A RID: 5674 RVA: 0x000574F7 File Offset: 0x000556F7
		public static Trace ChunkingTracer
		{
			get
			{
				if (ExTraceGlobals.chunkingTracer == null)
				{
					ExTraceGlobals.chunkingTracer = new Trace(ExTraceGlobals.componentGuid, 24);
				}
				return ExTraceGlobals.chunkingTracer;
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x0600162B RID: 5675 RVA: 0x00057516 File Offset: 0x00055716
		public static Trace ViewTableFindRowTracer
		{
			get
			{
				if (ExTraceGlobals.viewTableFindRowTracer == null)
				{
					ExTraceGlobals.viewTableFindRowTracer = new Trace(ExTraceGlobals.componentGuid, 30);
				}
				return ExTraceGlobals.viewTableFindRowTracer;
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x0600162C RID: 5676 RVA: 0x00057535 File Offset: 0x00055735
		public static Trace SchemaUpgradeServiceTracer
		{
			get
			{
				if (ExTraceGlobals.schemaUpgradeServiceTracer == null)
				{
					ExTraceGlobals.schemaUpgradeServiceTracer = new Trace(ExTraceGlobals.componentGuid, 31);
				}
				return ExTraceGlobals.schemaUpgradeServiceTracer;
			}
		}

		// Token: 0x04001AC0 RID: 6848
		private static Guid componentGuid = new Guid("15744371-ee52-4dfc-97f9-940803cf462f");

		// Token: 0x04001AC1 RID: 6849
		private static Trace contextTracer = null;

		// Token: 0x04001AC2 RID: 6850
		private static Trace mailboxTracer = null;

		// Token: 0x04001AC3 RID: 6851
		private static Trace extendedPropsTracer = null;

		// Token: 0x04001AC4 RID: 6852
		private static Trace queryPlannerSummaryTracer = null;

		// Token: 0x04001AC5 RID: 6853
		private static Trace queryPlannerDetailTracer = null;

		// Token: 0x04001AC6 RID: 6854
		private static Trace securityMailboxOwnerAccessTracer = null;

		// Token: 0x04001AC7 RID: 6855
		private static Trace securityAdminAccessTracer = null;

		// Token: 0x04001AC8 RID: 6856
		private static Trace securityServiceAccessTracer = null;

		// Token: 0x04001AC9 RID: 6857
		private static Trace securitySendAsAccessTracer = null;

		// Token: 0x04001ACA RID: 6858
		private static Trace securityContextTracer = null;

		// Token: 0x04001ACB RID: 6859
		private static Trace securityDescriptorTracer = null;

		// Token: 0x04001ACC RID: 6860
		private static Trace fullAccountNameTracer = null;

		// Token: 0x04001ACD RID: 6861
		private static Trace executionDiagnosticsTracer = null;

		// Token: 0x04001ACE RID: 6862
		private static Trace fullTextIndexTracer = null;

		// Token: 0x04001ACF RID: 6863
		private static Trace mailboxQuarantineTracer = null;

		// Token: 0x04001AD0 RID: 6864
		private static Trace mailboxSignatureTracer = null;

		// Token: 0x04001AD1 RID: 6865
		private static Trace timedEventsTracer = null;

		// Token: 0x04001AD2 RID: 6866
		private static Trace maintenanceTracer = null;

		// Token: 0x04001AD3 RID: 6867
		private static Trace mailboxLockTracer = null;

		// Token: 0x04001AD4 RID: 6868
		private static Trace criticalBlockTracer = null;

		// Token: 0x04001AD5 RID: 6869
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x04001AD6 RID: 6870
		private static Trace notificationTracer = null;

		// Token: 0x04001AD7 RID: 6871
		private static Trace storeDatabaseTracer = null;

		// Token: 0x04001AD8 RID: 6872
		private static Trace mailboxCountersTracer = null;

		// Token: 0x04001AD9 RID: 6873
		private static Trace chunkingTracer = null;

		// Token: 0x04001ADA RID: 6874
		private static Trace viewTableFindRowTracer = null;

		// Token: 0x04001ADB RID: 6875
		private static Trace schemaUpgradeServiceTracer = null;
	}
}
