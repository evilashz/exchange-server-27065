using System;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common
{
	// Token: 0x02000340 RID: 832
	public static class ExTraceGlobals
	{
		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001342 RID: 4930 RVA: 0x000510C0 File Offset: 0x0004F2C0
		public static Trace SingleInstanceItemTracer
		{
			get
			{
				if (ExTraceGlobals.singleInstanceItemTracer == null)
				{
					ExTraceGlobals.singleInstanceItemTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.singleInstanceItemTracer;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x000510DE File Offset: 0x0004F2DE
		public static Trace MeetingSuggestionsTracer
		{
			get
			{
				if (ExTraceGlobals.meetingSuggestionsTracer == null)
				{
					ExTraceGlobals.meetingSuggestionsTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.meetingSuggestionsTracer;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001344 RID: 4932 RVA: 0x000510FC File Offset: 0x0004F2FC
		public static Trace WorkingHoursTracer
		{
			get
			{
				if (ExTraceGlobals.workingHoursTracer == null)
				{
					ExTraceGlobals.workingHoursTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.workingHoursTracer;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x0005111A File Offset: 0x0004F31A
		public static Trace OOFTracer
		{
			get
			{
				if (ExTraceGlobals.oOFTracer == null)
				{
					ExTraceGlobals.oOFTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.oOFTracer;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x00051138 File Offset: 0x0004F338
		public static Trace ELCTracer
		{
			get
			{
				if (ExTraceGlobals.eLCTracer == null)
				{
					ExTraceGlobals.eLCTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.eLCTracer;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x00051156 File Offset: 0x0004F356
		public static Trace ResourceBookingTracer
		{
			get
			{
				if (ExTraceGlobals.resourceBookingTracer == null)
				{
					ExTraceGlobals.resourceBookingTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.resourceBookingTracer;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x00051174 File Offset: 0x0004F374
		public static Trace PFDTracer
		{
			get
			{
				if (ExTraceGlobals.pFDTracer == null)
				{
					ExTraceGlobals.pFDTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.pFDTracer;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x00051192 File Offset: 0x0004F392
		public static Trace TraceContextTracer
		{
			get
			{
				if (ExTraceGlobals.traceContextTracer == null)
				{
					ExTraceGlobals.traceContextTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.traceContextTracer;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x0600134A RID: 4938 RVA: 0x000511B0 File Offset: 0x0004F3B0
		public static Trace AutoTaggingTracer
		{
			get
			{
				if (ExTraceGlobals.autoTaggingTracer == null)
				{
					ExTraceGlobals.autoTaggingTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.autoTaggingTracer;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x0600134B RID: 4939 RVA: 0x000511CE File Offset: 0x0004F3CE
		public static Trace SearchTracer
		{
			get
			{
				if (ExTraceGlobals.searchTracer == null)
				{
					ExTraceGlobals.searchTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.searchTracer;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x0600134C RID: 4940 RVA: 0x000511ED File Offset: 0x0004F3ED
		public static Trace MWITracer
		{
			get
			{
				if (ExTraceGlobals.mWITracer == null)
				{
					ExTraceGlobals.mWITracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.mWITracer;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x0005120C File Offset: 0x0004F40C
		public static Trace TopNTracer
		{
			get
			{
				if (ExTraceGlobals.topNTracer == null)
				{
					ExTraceGlobals.topNTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.topNTracer;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x0005122B File Offset: 0x0004F42B
		public static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (ExTraceGlobals.faultInjectionTracer == null)
				{
					ExTraceGlobals.faultInjectionTracer = new FaultInjectionTrace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.faultInjectionTracer;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x0005124A File Offset: 0x0004F44A
		public static Trace PublicFolderFreeBusyDataTracer
		{
			get
			{
				if (ExTraceGlobals.publicFolderFreeBusyDataTracer == null)
				{
					ExTraceGlobals.publicFolderFreeBusyDataTracer = new Trace(ExTraceGlobals.componentGuid, 13);
				}
				return ExTraceGlobals.publicFolderFreeBusyDataTracer;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001350 RID: 4944 RVA: 0x00051269 File Offset: 0x0004F469
		public static Trace UserPhotosTracer
		{
			get
			{
				if (ExTraceGlobals.userPhotosTracer == null)
				{
					ExTraceGlobals.userPhotosTracer = new Trace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.userPhotosTracer;
			}
		}

		// Token: 0x040017F0 RID: 6128
		private static Guid componentGuid = new Guid("3A8BB7C6-6298-45eb-BE95-1A3AF02F7FFA");

		// Token: 0x040017F1 RID: 6129
		private static Trace singleInstanceItemTracer = null;

		// Token: 0x040017F2 RID: 6130
		private static Trace meetingSuggestionsTracer = null;

		// Token: 0x040017F3 RID: 6131
		private static Trace workingHoursTracer = null;

		// Token: 0x040017F4 RID: 6132
		private static Trace oOFTracer = null;

		// Token: 0x040017F5 RID: 6133
		private static Trace eLCTracer = null;

		// Token: 0x040017F6 RID: 6134
		private static Trace resourceBookingTracer = null;

		// Token: 0x040017F7 RID: 6135
		private static Trace pFDTracer = null;

		// Token: 0x040017F8 RID: 6136
		private static Trace traceContextTracer = null;

		// Token: 0x040017F9 RID: 6137
		private static Trace autoTaggingTracer = null;

		// Token: 0x040017FA RID: 6138
		private static Trace searchTracer = null;

		// Token: 0x040017FB RID: 6139
		private static Trace mWITracer = null;

		// Token: 0x040017FC RID: 6140
		private static Trace topNTracer = null;

		// Token: 0x040017FD RID: 6141
		private static FaultInjectionTrace faultInjectionTracer = null;

		// Token: 0x040017FE RID: 6142
		private static Trace publicFolderFreeBusyDataTracer = null;

		// Token: 0x040017FF RID: 6143
		private static Trace userPhotosTracer = null;
	}
}
