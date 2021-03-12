using System;

namespace Microsoft.Exchange.Diagnostics.Components.Entities.Calendaring
{
	// Token: 0x02000400 RID: 1024
	public static class ExTraceGlobals
	{
		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x0005C5CC File Offset: 0x0005A7CC
		public static Trace EventDataProviderTracer
		{
			get
			{
				if (ExTraceGlobals.eventDataProviderTracer == null)
				{
					ExTraceGlobals.eventDataProviderTracer = new Trace(ExTraceGlobals.componentGuid, 0);
				}
				return ExTraceGlobals.eventDataProviderTracer;
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x0005C5EA File Offset: 0x0005A7EA
		public static Trace ReadEventTracer
		{
			get
			{
				if (ExTraceGlobals.readEventTracer == null)
				{
					ExTraceGlobals.readEventTracer = new Trace(ExTraceGlobals.componentGuid, 1);
				}
				return ExTraceGlobals.readEventTracer;
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06001895 RID: 6293 RVA: 0x0005C608 File Offset: 0x0005A808
		public static Trace CreateEventTracer
		{
			get
			{
				if (ExTraceGlobals.createEventTracer == null)
				{
					ExTraceGlobals.createEventTracer = new Trace(ExTraceGlobals.componentGuid, 2);
				}
				return ExTraceGlobals.createEventTracer;
			}
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06001896 RID: 6294 RVA: 0x0005C626 File Offset: 0x0005A826
		public static Trace UpdateEventTracer
		{
			get
			{
				if (ExTraceGlobals.updateEventTracer == null)
				{
					ExTraceGlobals.updateEventTracer = new Trace(ExTraceGlobals.componentGuid, 3);
				}
				return ExTraceGlobals.updateEventTracer;
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06001897 RID: 6295 RVA: 0x0005C644 File Offset: 0x0005A844
		public static Trace DeleteEventTracer
		{
			get
			{
				if (ExTraceGlobals.deleteEventTracer == null)
				{
					ExTraceGlobals.deleteEventTracer = new Trace(ExTraceGlobals.componentGuid, 4);
				}
				return ExTraceGlobals.deleteEventTracer;
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06001898 RID: 6296 RVA: 0x0005C662 File Offset: 0x0005A862
		public static Trace FindEventsTracer
		{
			get
			{
				if (ExTraceGlobals.findEventsTracer == null)
				{
					ExTraceGlobals.findEventsTracer = new Trace(ExTraceGlobals.componentGuid, 5);
				}
				return ExTraceGlobals.findEventsTracer;
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06001899 RID: 6297 RVA: 0x0005C680 File Offset: 0x0005A880
		public static Trace CalendarDataProviderTracer
		{
			get
			{
				if (ExTraceGlobals.calendarDataProviderTracer == null)
				{
					ExTraceGlobals.calendarDataProviderTracer = new Trace(ExTraceGlobals.componentGuid, 6);
				}
				return ExTraceGlobals.calendarDataProviderTracer;
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x0600189A RID: 6298 RVA: 0x0005C69E File Offset: 0x0005A89E
		public static Trace ReadCalendarTracer
		{
			get
			{
				if (ExTraceGlobals.readCalendarTracer == null)
				{
					ExTraceGlobals.readCalendarTracer = new Trace(ExTraceGlobals.componentGuid, 7);
				}
				return ExTraceGlobals.readCalendarTracer;
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x0600189B RID: 6299 RVA: 0x0005C6BC File Offset: 0x0005A8BC
		public static Trace CreateCalendarTracer
		{
			get
			{
				if (ExTraceGlobals.createCalendarTracer == null)
				{
					ExTraceGlobals.createCalendarTracer = new Trace(ExTraceGlobals.componentGuid, 8);
				}
				return ExTraceGlobals.createCalendarTracer;
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x0600189C RID: 6300 RVA: 0x0005C6DA File Offset: 0x0005A8DA
		public static Trace UpdateCalendarTracer
		{
			get
			{
				if (ExTraceGlobals.updateCalendarTracer == null)
				{
					ExTraceGlobals.updateCalendarTracer = new Trace(ExTraceGlobals.componentGuid, 9);
				}
				return ExTraceGlobals.updateCalendarTracer;
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x0600189D RID: 6301 RVA: 0x0005C6F9 File Offset: 0x0005A8F9
		public static Trace DeleteCalendarTracer
		{
			get
			{
				if (ExTraceGlobals.deleteCalendarTracer == null)
				{
					ExTraceGlobals.deleteCalendarTracer = new Trace(ExTraceGlobals.componentGuid, 10);
				}
				return ExTraceGlobals.deleteCalendarTracer;
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x0600189E RID: 6302 RVA: 0x0005C718 File Offset: 0x0005A918
		public static Trace FindCalendarsTracer
		{
			get
			{
				if (ExTraceGlobals.findCalendarsTracer == null)
				{
					ExTraceGlobals.findCalendarsTracer = new Trace(ExTraceGlobals.componentGuid, 11);
				}
				return ExTraceGlobals.findCalendarsTracer;
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x0600189F RID: 6303 RVA: 0x0005C737 File Offset: 0x0005A937
		public static Trace CancelEventTracer
		{
			get
			{
				if (ExTraceGlobals.cancelEventTracer == null)
				{
					ExTraceGlobals.cancelEventTracer = new Trace(ExTraceGlobals.componentGuid, 12);
				}
				return ExTraceGlobals.cancelEventTracer;
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x060018A0 RID: 6304 RVA: 0x0005C756 File Offset: 0x0005A956
		public static Trace RespondToEventTracer
		{
			get
			{
				if (ExTraceGlobals.respondToEventTracer == null)
				{
					ExTraceGlobals.respondToEventTracer = new Trace(ExTraceGlobals.componentGuid, 13);
				}
				return ExTraceGlobals.respondToEventTracer;
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x060018A1 RID: 6305 RVA: 0x0005C775 File Offset: 0x0005A975
		public static Trace InstancesQueryTracer
		{
			get
			{
				if (ExTraceGlobals.instancesQueryTracer == null)
				{
					ExTraceGlobals.instancesQueryTracer = new Trace(ExTraceGlobals.componentGuid, 14);
				}
				return ExTraceGlobals.instancesQueryTracer;
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x060018A2 RID: 6306 RVA: 0x0005C794 File Offset: 0x0005A994
		public static Trace CalendarInteropTracer
		{
			get
			{
				if (ExTraceGlobals.calendarInteropTracer == null)
				{
					ExTraceGlobals.calendarInteropTracer = new Trace(ExTraceGlobals.componentGuid, 15);
				}
				return ExTraceGlobals.calendarInteropTracer;
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x0005C7B3 File Offset: 0x0005A9B3
		public static Trace CreateSeriesTracer
		{
			get
			{
				if (ExTraceGlobals.createSeriesTracer == null)
				{
					ExTraceGlobals.createSeriesTracer = new Trace(ExTraceGlobals.componentGuid, 16);
				}
				return ExTraceGlobals.createSeriesTracer;
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x060018A4 RID: 6308 RVA: 0x0005C7D2 File Offset: 0x0005A9D2
		public static Trace CancelSeriesTracer
		{
			get
			{
				if (ExTraceGlobals.cancelSeriesTracer == null)
				{
					ExTraceGlobals.cancelSeriesTracer = new Trace(ExTraceGlobals.componentGuid, 17);
				}
				return ExTraceGlobals.cancelSeriesTracer;
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x0005C7F1 File Offset: 0x0005A9F1
		public static Trace UpdateSeriesTracer
		{
			get
			{
				if (ExTraceGlobals.updateSeriesTracer == null)
				{
					ExTraceGlobals.updateSeriesTracer = new Trace(ExTraceGlobals.componentGuid, 18);
				}
				return ExTraceGlobals.updateSeriesTracer;
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x060018A6 RID: 6310 RVA: 0x0005C810 File Offset: 0x0005AA10
		public static Trace SeriesPendingActionsInteropTracer
		{
			get
			{
				if (ExTraceGlobals.seriesPendingActionsInteropTracer == null)
				{
					ExTraceGlobals.seriesPendingActionsInteropTracer = new Trace(ExTraceGlobals.componentGuid, 19);
				}
				return ExTraceGlobals.seriesPendingActionsInteropTracer;
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x060018A7 RID: 6311 RVA: 0x0005C82F File Offset: 0x0005AA2F
		public static Trace SeriesInlineInteropTracer
		{
			get
			{
				if (ExTraceGlobals.seriesInlineInteropTracer == null)
				{
					ExTraceGlobals.seriesInlineInteropTracer = new Trace(ExTraceGlobals.componentGuid, 20);
				}
				return ExTraceGlobals.seriesInlineInteropTracer;
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x060018A8 RID: 6312 RVA: 0x0005C84E File Offset: 0x0005AA4E
		public static Trace CreateOccurrenceTracer
		{
			get
			{
				if (ExTraceGlobals.createOccurrenceTracer == null)
				{
					ExTraceGlobals.createOccurrenceTracer = new Trace(ExTraceGlobals.componentGuid, 21);
				}
				return ExTraceGlobals.createOccurrenceTracer;
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x060018A9 RID: 6313 RVA: 0x0005C86D File Offset: 0x0005AA6D
		public static Trace ReadCalendarGroupTracer
		{
			get
			{
				if (ExTraceGlobals.readCalendarGroupTracer == null)
				{
					ExTraceGlobals.readCalendarGroupTracer = new Trace(ExTraceGlobals.componentGuid, 22);
				}
				return ExTraceGlobals.readCalendarGroupTracer;
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x060018AA RID: 6314 RVA: 0x0005C88C File Offset: 0x0005AA8C
		public static Trace CreateCalendarGroupTracer
		{
			get
			{
				if (ExTraceGlobals.createCalendarGroupTracer == null)
				{
					ExTraceGlobals.createCalendarGroupTracer = new Trace(ExTraceGlobals.componentGuid, 23);
				}
				return ExTraceGlobals.createCalendarGroupTracer;
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x060018AB RID: 6315 RVA: 0x0005C8AB File Offset: 0x0005AAAB
		public static Trace UpdateCalendarGroupTracer
		{
			get
			{
				if (ExTraceGlobals.updateCalendarGroupTracer == null)
				{
					ExTraceGlobals.updateCalendarGroupTracer = new Trace(ExTraceGlobals.componentGuid, 24);
				}
				return ExTraceGlobals.updateCalendarGroupTracer;
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x0005C8CA File Offset: 0x0005AACA
		public static Trace DeleteCalendarGroupTracer
		{
			get
			{
				if (ExTraceGlobals.deleteCalendarGroupTracer == null)
				{
					ExTraceGlobals.deleteCalendarGroupTracer = new Trace(ExTraceGlobals.componentGuid, 25);
				}
				return ExTraceGlobals.deleteCalendarGroupTracer;
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x060018AD RID: 6317 RVA: 0x0005C8E9 File Offset: 0x0005AAE9
		public static Trace FindCalendarGroupsTracer
		{
			get
			{
				if (ExTraceGlobals.findCalendarGroupsTracer == null)
				{
					ExTraceGlobals.findCalendarGroupsTracer = new Trace(ExTraceGlobals.componentGuid, 26);
				}
				return ExTraceGlobals.findCalendarGroupsTracer;
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x060018AE RID: 6318 RVA: 0x0005C908 File Offset: 0x0005AB08
		public static Trace MeetingMessageProcessingTracer
		{
			get
			{
				if (ExTraceGlobals.meetingMessageProcessingTracer == null)
				{
					ExTraceGlobals.meetingMessageProcessingTracer = new Trace(ExTraceGlobals.componentGuid, 27);
				}
				return ExTraceGlobals.meetingMessageProcessingTracer;
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x0005C927 File Offset: 0x0005AB27
		public static Trace CreateReceivedSeriesTracer
		{
			get
			{
				if (ExTraceGlobals.createReceivedSeriesTracer == null)
				{
					ExTraceGlobals.createReceivedSeriesTracer = new Trace(ExTraceGlobals.componentGuid, 28);
				}
				return ExTraceGlobals.createReceivedSeriesTracer;
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x060018B0 RID: 6320 RVA: 0x0005C946 File Offset: 0x0005AB46
		public static Trace RespondToSeriesTracer
		{
			get
			{
				if (ExTraceGlobals.respondToSeriesTracer == null)
				{
					ExTraceGlobals.respondToSeriesTracer = new Trace(ExTraceGlobals.componentGuid, 29);
				}
				return ExTraceGlobals.respondToSeriesTracer;
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x0005C965 File Offset: 0x0005AB65
		public static Trace ForwardEventTracer
		{
			get
			{
				if (ExTraceGlobals.forwardEventTracer == null)
				{
					ExTraceGlobals.forwardEventTracer = new Trace(ExTraceGlobals.componentGuid, 30);
				}
				return ExTraceGlobals.forwardEventTracer;
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x0005C984 File Offset: 0x0005AB84
		public static Trace ForwardSeriesTracer
		{
			get
			{
				if (ExTraceGlobals.forwardSeriesTracer == null)
				{
					ExTraceGlobals.forwardSeriesTracer = new Trace(ExTraceGlobals.componentGuid, 31);
				}
				return ExTraceGlobals.forwardSeriesTracer;
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x060018B3 RID: 6323 RVA: 0x0005C9A3 File Offset: 0x0005ABA3
		public static Trace SeriesActionParserTracer
		{
			get
			{
				if (ExTraceGlobals.seriesActionParserTracer == null)
				{
					ExTraceGlobals.seriesActionParserTracer = new Trace(ExTraceGlobals.componentGuid, 32);
				}
				return ExTraceGlobals.seriesActionParserTracer;
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x060018B4 RID: 6324 RVA: 0x0005C9C2 File Offset: 0x0005ABC2
		public static Trace ExpandSeriesTracer
		{
			get
			{
				if (ExTraceGlobals.expandSeriesTracer == null)
				{
					ExTraceGlobals.expandSeriesTracer = new Trace(ExTraceGlobals.componentGuid, 33);
				}
				return ExTraceGlobals.expandSeriesTracer;
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x060018B5 RID: 6325 RVA: 0x0005C9E1 File Offset: 0x0005ABE1
		public static Trace MeetingRequestMessageDataProviderTracer
		{
			get
			{
				if (ExTraceGlobals.meetingRequestMessageDataProviderTracer == null)
				{
					ExTraceGlobals.meetingRequestMessageDataProviderTracer = new Trace(ExTraceGlobals.componentGuid, 34);
				}
				return ExTraceGlobals.meetingRequestMessageDataProviderTracer;
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x060018B6 RID: 6326 RVA: 0x0005CA00 File Offset: 0x0005AC00
		public static Trace RespondToMeetingRequestTracer
		{
			get
			{
				if (ExTraceGlobals.respondToMeetingRequestTracer == null)
				{
					ExTraceGlobals.respondToMeetingRequestTracer = new Trace(ExTraceGlobals.componentGuid, 35);
				}
				return ExTraceGlobals.respondToMeetingRequestTracer;
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x0005CA1F File Offset: 0x0005AC1F
		public static Trace ConvertSingleEventToNprSeriesTracer
		{
			get
			{
				if (ExTraceGlobals.convertSingleEventToNprSeriesTracer == null)
				{
					ExTraceGlobals.convertSingleEventToNprSeriesTracer = new Trace(ExTraceGlobals.componentGuid, 36);
				}
				return ExTraceGlobals.convertSingleEventToNprSeriesTracer;
			}
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x0005CA3E File Offset: 0x0005AC3E
		public static Trace GetCalendarViewTracer
		{
			get
			{
				if (ExTraceGlobals.getCalendarViewTracer == null)
				{
					ExTraceGlobals.getCalendarViewTracer = new Trace(ExTraceGlobals.componentGuid, 37);
				}
				return ExTraceGlobals.getCalendarViewTracer;
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x060018B9 RID: 6329 RVA: 0x0005CA5D File Offset: 0x0005AC5D
		public static Trace DeleteSeriesTracer
		{
			get
			{
				if (ExTraceGlobals.deleteSeriesTracer == null)
				{
					ExTraceGlobals.deleteSeriesTracer = new Trace(ExTraceGlobals.componentGuid, 38);
				}
				return ExTraceGlobals.deleteSeriesTracer;
			}
		}

		// Token: 0x04001D41 RID: 7489
		private static Guid componentGuid = new Guid("6B844120-1AE2-4E8C-ABDB-F3D7F3E95388");

		// Token: 0x04001D42 RID: 7490
		private static Trace eventDataProviderTracer = null;

		// Token: 0x04001D43 RID: 7491
		private static Trace readEventTracer = null;

		// Token: 0x04001D44 RID: 7492
		private static Trace createEventTracer = null;

		// Token: 0x04001D45 RID: 7493
		private static Trace updateEventTracer = null;

		// Token: 0x04001D46 RID: 7494
		private static Trace deleteEventTracer = null;

		// Token: 0x04001D47 RID: 7495
		private static Trace findEventsTracer = null;

		// Token: 0x04001D48 RID: 7496
		private static Trace calendarDataProviderTracer = null;

		// Token: 0x04001D49 RID: 7497
		private static Trace readCalendarTracer = null;

		// Token: 0x04001D4A RID: 7498
		private static Trace createCalendarTracer = null;

		// Token: 0x04001D4B RID: 7499
		private static Trace updateCalendarTracer = null;

		// Token: 0x04001D4C RID: 7500
		private static Trace deleteCalendarTracer = null;

		// Token: 0x04001D4D RID: 7501
		private static Trace findCalendarsTracer = null;

		// Token: 0x04001D4E RID: 7502
		private static Trace cancelEventTracer = null;

		// Token: 0x04001D4F RID: 7503
		private static Trace respondToEventTracer = null;

		// Token: 0x04001D50 RID: 7504
		private static Trace instancesQueryTracer = null;

		// Token: 0x04001D51 RID: 7505
		private static Trace calendarInteropTracer = null;

		// Token: 0x04001D52 RID: 7506
		private static Trace createSeriesTracer = null;

		// Token: 0x04001D53 RID: 7507
		private static Trace cancelSeriesTracer = null;

		// Token: 0x04001D54 RID: 7508
		private static Trace updateSeriesTracer = null;

		// Token: 0x04001D55 RID: 7509
		private static Trace seriesPendingActionsInteropTracer = null;

		// Token: 0x04001D56 RID: 7510
		private static Trace seriesInlineInteropTracer = null;

		// Token: 0x04001D57 RID: 7511
		private static Trace createOccurrenceTracer = null;

		// Token: 0x04001D58 RID: 7512
		private static Trace readCalendarGroupTracer = null;

		// Token: 0x04001D59 RID: 7513
		private static Trace createCalendarGroupTracer = null;

		// Token: 0x04001D5A RID: 7514
		private static Trace updateCalendarGroupTracer = null;

		// Token: 0x04001D5B RID: 7515
		private static Trace deleteCalendarGroupTracer = null;

		// Token: 0x04001D5C RID: 7516
		private static Trace findCalendarGroupsTracer = null;

		// Token: 0x04001D5D RID: 7517
		private static Trace meetingMessageProcessingTracer = null;

		// Token: 0x04001D5E RID: 7518
		private static Trace createReceivedSeriesTracer = null;

		// Token: 0x04001D5F RID: 7519
		private static Trace respondToSeriesTracer = null;

		// Token: 0x04001D60 RID: 7520
		private static Trace forwardEventTracer = null;

		// Token: 0x04001D61 RID: 7521
		private static Trace forwardSeriesTracer = null;

		// Token: 0x04001D62 RID: 7522
		private static Trace seriesActionParserTracer = null;

		// Token: 0x04001D63 RID: 7523
		private static Trace expandSeriesTracer = null;

		// Token: 0x04001D64 RID: 7524
		private static Trace meetingRequestMessageDataProviderTracer = null;

		// Token: 0x04001D65 RID: 7525
		private static Trace respondToMeetingRequestTracer = null;

		// Token: 0x04001D66 RID: 7526
		private static Trace convertSingleEventToNprSeriesTracer = null;

		// Token: 0x04001D67 RID: 7527
		private static Trace getCalendarViewTracer = null;

		// Token: 0x04001D68 RID: 7528
		private static Trace deleteSeriesTracer = null;
	}
}
