using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002C2 RID: 706
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ICalendarItemBase : IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06001D96 RID: 7574
		// (set) Token: 0x06001D97 RID: 7575
		double? Accuracy { get; set; }

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06001D98 RID: 7576
		bool AllowNewTimeProposal { get; }

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06001D99 RID: 7577
		// (set) Token: 0x06001D9A RID: 7578
		double? Altitude { get; set; }

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06001D9B RID: 7579
		// (set) Token: 0x06001D9C RID: 7580
		double? AltitudeAccuracy { get; set; }

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06001D9D RID: 7581
		// (set) Token: 0x06001D9E RID: 7582
		int AppointmentLastSequenceNumber { get; set; }

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06001D9F RID: 7583
		string AppointmentReplyName { get; }

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06001DA0 RID: 7584
		ExDateTime AppointmentReplyTime { get; }

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06001DA1 RID: 7585
		int AppointmentSequenceNumber { get; }

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06001DA2 RID: 7586
		IAttendeeCollection AttendeeCollection { get; }

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06001DA3 RID: 7587
		ExDateTime AttendeeCriticalChangeTime { get; }

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06001DA4 RID: 7588
		bool AttendeesChanged { get; }

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06001DA5 RID: 7589
		CalendarItemType CalendarItemType { get; }

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06001DA6 RID: 7590
		string CalendarOriginatorId { get; }

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06001DA7 RID: 7591
		byte[] CleanGlobalObjectId { get; }

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06001DA8 RID: 7592
		// (set) Token: 0x06001DA9 RID: 7593
		ClientIntentFlags ClientIntent { get; set; }

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06001DAA RID: 7594
		// (set) Token: 0x06001DAB RID: 7595
		string ConferenceInfo { get; set; }

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06001DAC RID: 7596
		// (set) Token: 0x06001DAD RID: 7597
		string ConferenceTelURI { get; set; }

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06001DAE RID: 7598
		// (set) Token: 0x06001DAF RID: 7599
		ExDateTime EndTime { get; set; }

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06001DB0 RID: 7600
		// (set) Token: 0x06001DB1 RID: 7601
		ExTimeZone EndTimeZone { get; set; }

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06001DB2 RID: 7602
		ExDateTime EndWallClock { get; }

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06001DB3 RID: 7603
		// (set) Token: 0x06001DB4 RID: 7604
		Reminders<EventTimeBasedInboxReminder> EventTimeBasedInboxReminders { get; set; }

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06001DB5 RID: 7605
		// (set) Token: 0x06001DB6 RID: 7606
		BusyType FreeBusyStatus { get; set; }

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06001DB7 RID: 7607
		GlobalObjectId GlobalObjectId { get; }

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06001DB8 RID: 7608
		// (set) Token: 0x06001DB9 RID: 7609
		bool IsAllDayEvent { get; set; }

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06001DBA RID: 7610
		bool IsCalendarItemTypeOccurrenceOrException { get; }

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06001DBB RID: 7611
		bool IsCancelled { get; }

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06001DBC RID: 7612
		bool IsEvent { get; }

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06001DBD RID: 7613
		bool IsForwardAllowed { get; }

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06001DBE RID: 7614
		// (set) Token: 0x06001DBF RID: 7615
		bool IsMeeting { get; set; }

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06001DC0 RID: 7616
		bool IsOrganizerExternal { get; }

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06001DC1 RID: 7617
		// (set) Token: 0x06001DC2 RID: 7618
		double? Latitude { get; set; }

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06001DC3 RID: 7619
		// (set) Token: 0x06001DC4 RID: 7620
		string Location { get; set; }

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06001DC5 RID: 7621
		// (set) Token: 0x06001DC6 RID: 7622
		string LocationAnnotation { get; set; }

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06001DC7 RID: 7623
		// (set) Token: 0x06001DC8 RID: 7624
		string LocationCity { get; set; }

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06001DC9 RID: 7625
		// (set) Token: 0x06001DCA RID: 7626
		string LocationCountry { get; set; }

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06001DCB RID: 7627
		// (set) Token: 0x06001DCC RID: 7628
		string LocationDisplayName { get; set; }

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06001DCD RID: 7629
		// (set) Token: 0x06001DCE RID: 7630
		string LocationPostalCode { get; set; }

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06001DCF RID: 7631
		// (set) Token: 0x06001DD0 RID: 7632
		LocationSource LocationSource { get; set; }

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06001DD1 RID: 7633
		// (set) Token: 0x06001DD2 RID: 7634
		string LocationState { get; set; }

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06001DD3 RID: 7635
		// (set) Token: 0x06001DD4 RID: 7636
		string LocationStreet { get; set; }

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06001DD5 RID: 7637
		// (set) Token: 0x06001DD6 RID: 7638
		string LocationUri { get; set; }

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06001DD7 RID: 7639
		// (set) Token: 0x06001DD8 RID: 7640
		double? Longitude { get; set; }

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06001DD9 RID: 7641
		bool MeetingRequestWasSent { get; }

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06001DDA RID: 7642
		// (set) Token: 0x06001DDB RID: 7643
		string OnlineMeetingConfLink { get; set; }

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06001DDC RID: 7644
		// (set) Token: 0x06001DDD RID: 7645
		string OnlineMeetingExternalLink { get; set; }

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06001DDE RID: 7646
		// (set) Token: 0x06001DDF RID: 7647
		string OnlineMeetingInternalLink { get; set; }

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06001DE0 RID: 7648
		Participant Organizer { get; }

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06001DE1 RID: 7649
		// (set) Token: 0x06001DE2 RID: 7650
		byte[] OutlookUserPropsPropDefStream { get; set; }

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06001DE3 RID: 7651
		int? OwnerAppointmentId { get; }

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06001DE4 RID: 7652
		ExDateTime OwnerCriticalChangeTime { get; }

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06001DE5 RID: 7653
		// (set) Token: 0x06001DE6 RID: 7654
		bool ResponseRequested { get; set; }

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06001DE7 RID: 7655
		// (set) Token: 0x06001DE8 RID: 7656
		ResponseType ResponseType { get; set; }

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06001DE9 RID: 7657
		// (set) Token: 0x06001DEA RID: 7658
		string SeriesId { get; set; }

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06001DEB RID: 7659
		// (set) Token: 0x06001DEC RID: 7660
		string ClientId { get; set; }

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06001DED RID: 7661
		// (set) Token: 0x06001DEE RID: 7662
		ExDateTime StartTime { get; set; }

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06001DEF RID: 7663
		// (set) Token: 0x06001DF0 RID: 7664
		ExTimeZone StartTimeZone { get; set; }

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06001DF1 RID: 7665
		ExDateTime StartWallClock { get; }

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06001DF2 RID: 7666
		// (set) Token: 0x06001DF3 RID: 7667
		string Subject { get; set; }

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06001DF4 RID: 7668
		// (set) Token: 0x06001DF5 RID: 7669
		string UCCapabilities { get; set; }

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06001DF6 RID: 7670
		// (set) Token: 0x06001DF7 RID: 7671
		string UCInband { get; set; }

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06001DF8 RID: 7672
		// (set) Token: 0x06001DF9 RID: 7673
		string UCMeetingSetting { get; set; }

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06001DFA RID: 7674
		// (set) Token: 0x06001DFB RID: 7675
		string UCMeetingSettingSent { get; set; }

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06001DFC RID: 7676
		// (set) Token: 0x06001DFD RID: 7677
		string UCOpenedConferenceID { get; set; }

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06001DFE RID: 7678
		// (set) Token: 0x06001DFF RID: 7679
		string When { get; set; }

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06001E00 RID: 7680
		// (set) Token: 0x06001E01 RID: 7681
		bool IsReminderSet { get; set; }

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06001E02 RID: 7682
		// (set) Token: 0x06001E03 RID: 7683
		int ReminderMinutesBeforeStart { get; set; }

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06001E04 RID: 7684
		// (set) Token: 0x06001E05 RID: 7685
		RemindersState<EventTimeBasedInboxReminderState> EventTimeBasedInboxRemindersState { get; set; }

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06001E06 RID: 7686
		string ItemClass { get; }

		// Token: 0x06001E07 RID: 7687
		bool IsOrganizer();

		// Token: 0x06001E08 RID: 7688
		MeetingResponse RespondToMeetingRequest(ResponseType responseType);

		// Token: 0x06001E09 RID: 7689
		MeetingResponse RespondToMeetingRequest(ResponseType responseType, bool autoCaptureClientIntent, bool intendToSendResponse, ExDateTime? proposedStart = null, ExDateTime? proposedEnd = null);

		// Token: 0x06001E0A RID: 7690
		MeetingResponse RespondToMeetingRequest(ResponseType responseType, string subjectPrefix, ExDateTime? proposedStart = null, ExDateTime? proposedEnd = null);

		// Token: 0x06001E0B RID: 7691
		void SendMeetingMessages(bool isToAllAttendees, int? seriesSequenceNumber = null, bool autoCaptureClientIntent = false, bool copyToSentItems = true, string occurrencesViewPropertiesBlob = null, byte[] masterGoid = null);

		// Token: 0x06001E0C RID: 7692
		void SaveWithConflictCheck(SaveMode saveMode);
	}
}
