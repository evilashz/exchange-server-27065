using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel
{
	// Token: 0x0200039A RID: 922
	public static class ExTraceGlobals
	{
		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x00057906 File Offset: 0x00055B06
		public static Trace FolderTracer
		{
			get
			{
				if (ExTraceGlobals.folderTracer == null)
				{
					ExTraceGlobals.folderTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.folderTracer;
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x00057924 File Offset: 0x00055B24
		public static Trace EventsTracer
		{
			get
			{
				if (ExTraceGlobals.eventsTracer == null)
				{
					ExTraceGlobals.eventsTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.eventsTracer;
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x00057942 File Offset: 0x00055B42
		public static Trace ConversationsSummaryTracer
		{
			get
			{
				if (ExTraceGlobals.conversationsSummaryTracer == null)
				{
					ExTraceGlobals.conversationsSummaryTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.conversationsSummaryTracer;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06001647 RID: 5703 RVA: 0x00057960 File Offset: 0x00055B60
		public static Trace ConversationsDetailedTracer
		{
			get
			{
				if (ExTraceGlobals.conversationsDetailedTracer == null)
				{
					ExTraceGlobals.conversationsDetailedTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.conversationsDetailedTracer;
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06001648 RID: 5704 RVA: 0x0005797E File Offset: 0x00055B7E
		public static Trace SearchFolderSearchCriteriaTracer
		{
			get
			{
				if (ExTraceGlobals.searchFolderSearchCriteriaTracer == null)
				{
					ExTraceGlobals.searchFolderSearchCriteriaTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.searchFolderSearchCriteriaTracer;
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x0005799C File Offset: 0x00055B9C
		public static Trace SearchFolderPopulationTracer
		{
			get
			{
				if (ExTraceGlobals.searchFolderPopulationTracer == null)
				{
					ExTraceGlobals.searchFolderPopulationTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.searchFolderPopulationTracer;
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x000579BA File Offset: 0x00055BBA
		public static Trace CategorizationsTracer
		{
			get
			{
				if (ExTraceGlobals.categorizationsTracer == null)
				{
					ExTraceGlobals.categorizationsTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.categorizationsTracer;
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x000579D8 File Offset: 0x00055BD8
		public static Trace GetViewsPropertiesTracer
		{
			get
			{
				if (ExTraceGlobals.getViewsPropertiesTracer == null)
				{
					ExTraceGlobals.getViewsPropertiesTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.getViewsPropertiesTracer;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x0600164C RID: 5708 RVA: 0x000579F6 File Offset: 0x00055BF6
		public static Trace DatabaseSizeCheckTracer
		{
			get
			{
				if (ExTraceGlobals.databaseSizeCheckTracer == null)
				{
					ExTraceGlobals.databaseSizeCheckTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.databaseSizeCheckTracer;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x0600164D RID: 5709 RVA: 0x00057A14 File Offset: 0x00055C14
		public static Trace SearchFolderAgeOutTracer
		{
			get
			{
				if (ExTraceGlobals.searchFolderAgeOutTracer == null)
				{
					ExTraceGlobals.searchFolderAgeOutTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.searchFolderAgeOutTracer;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x00057A33 File Offset: 0x00055C33
		public static Trace ReadEventsTracer
		{
			get
			{
				if (ExTraceGlobals.readEventsTracer == null)
				{
					ExTraceGlobals.readEventsTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.readEventsTracer;
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x0600164F RID: 5711 RVA: 0x00057A52 File Offset: 0x00055C52
		public static Trace EventCounterBoundsTracer
		{
			get
			{
				if (ExTraceGlobals.eventCounterBoundsTracer == null)
				{
					ExTraceGlobals.eventCounterBoundsTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.eventCounterBoundsTracer;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x00057A71 File Offset: 0x00055C71
		public static Trace QuotaTracer
		{
			get
			{
				if (ExTraceGlobals.quotaTracer == null)
				{
					ExTraceGlobals.quotaTracer = new Trace(ExTraceGlobals.componentGuid, 18);
				}
				return ExTraceGlobals.quotaTracer;
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x00057A90 File Offset: 0x00055C90
		public static Trace SubobjectCleanupTracer
		{
			get
			{
				if (ExTraceGlobals.subobjectCleanupTracer == null)
				{
					ExTraceGlobals.subobjectCleanupTracer = new Trace(ExTraceGlobals.componentGuid, 19);
				}
				return ExTraceGlobals.subobjectCleanupTracer;
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06001652 RID: 5714 RVA: 0x00057AAF File Offset: 0x00055CAF
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

		// Token: 0x04001AF2 RID: 6898
		private static Guid componentGuid = new Guid("702edbba-c134-43b8-b01d-6aed04823af3");

		// Token: 0x04001AF3 RID: 6899
		private static Trace folderTracer = null;

		// Token: 0x04001AF4 RID: 6900
		private static Trace eventsTracer = null;

		// Token: 0x04001AF5 RID: 6901
		private static Trace conversationsSummaryTracer = null;

		// Token: 0x04001AF6 RID: 6902
		private static Trace conversationsDetailedTracer = null;

		// Token: 0x04001AF7 RID: 6903
		private static Trace searchFolderSearchCriteriaTracer = null;

		// Token: 0x04001AF8 RID: 6904
		private static Trace searchFolderPopulationTracer = null;

		// Token: 0x04001AF9 RID: 6905
		private static Trace categorizationsTracer = null;

		// Token: 0x04001AFA RID: 6906
		private static Trace getViewsPropertiesTracer = null;

		// Token: 0x04001AFB RID: 6907
		private static Trace databaseSizeCheckTracer = null;

		// Token: 0x04001AFC RID: 6908
		private static Trace searchFolderAgeOutTracer = null;

		// Token: 0x04001AFD RID: 6909
		private static Trace readEventsTracer = null;

		// Token: 0x04001AFE RID: 6910
		private static Trace eventCounterBoundsTracer = null;

		// Token: 0x04001AFF RID: 6911
		private static Trace quotaTracer = null;

		// Token: 0x04001B00 RID: 6912
		private static Trace subobjectCleanupTracer = null;

		// Token: 0x04001B01 RID: 6913
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
