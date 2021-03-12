using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002C3 RID: 707
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractCalendarItemBase : AbstractItem, ICalendarItemBase, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06001E0D RID: 7693 RVA: 0x00085D50 File Offset: 0x00083F50
		// (set) Token: 0x06001E0E RID: 7694 RVA: 0x00085D57 File Offset: 0x00083F57
		public virtual double? Accuracy
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06001E0F RID: 7695 RVA: 0x00085D5E File Offset: 0x00083F5E
		public virtual bool AllowNewTimeProposal
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06001E10 RID: 7696 RVA: 0x00085D65 File Offset: 0x00083F65
		// (set) Token: 0x06001E11 RID: 7697 RVA: 0x00085D6C File Offset: 0x00083F6C
		public virtual double? Altitude
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06001E12 RID: 7698 RVA: 0x00085D73 File Offset: 0x00083F73
		// (set) Token: 0x06001E13 RID: 7699 RVA: 0x00085D7A File Offset: 0x00083F7A
		public virtual double? AltitudeAccuracy
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06001E14 RID: 7700 RVA: 0x00085D81 File Offset: 0x00083F81
		// (set) Token: 0x06001E15 RID: 7701 RVA: 0x00085D88 File Offset: 0x00083F88
		public virtual int AppointmentLastSequenceNumber
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06001E16 RID: 7702 RVA: 0x00085D8F File Offset: 0x00083F8F
		public virtual string AppointmentReplyName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06001E17 RID: 7703 RVA: 0x00085D96 File Offset: 0x00083F96
		public virtual ExDateTime AppointmentReplyTime
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06001E18 RID: 7704 RVA: 0x00085D9D File Offset: 0x00083F9D
		public virtual int AppointmentSequenceNumber
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06001E19 RID: 7705 RVA: 0x00085DA4 File Offset: 0x00083FA4
		public virtual IAttendeeCollection AttendeeCollection
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06001E1A RID: 7706 RVA: 0x00085DAB File Offset: 0x00083FAB
		public virtual ExDateTime AttendeeCriticalChangeTime
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06001E1B RID: 7707 RVA: 0x00085DB2 File Offset: 0x00083FB2
		public virtual bool AttendeesChanged
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06001E1C RID: 7708 RVA: 0x00085DB9 File Offset: 0x00083FB9
		public virtual CalendarItemType CalendarItemType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06001E1D RID: 7709 RVA: 0x00085DC0 File Offset: 0x00083FC0
		public virtual string CalendarOriginatorId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06001E1E RID: 7710 RVA: 0x00085DC7 File Offset: 0x00083FC7
		public virtual byte[] CleanGlobalObjectId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06001E1F RID: 7711 RVA: 0x00085DCE File Offset: 0x00083FCE
		// (set) Token: 0x06001E20 RID: 7712 RVA: 0x00085DD5 File Offset: 0x00083FD5
		public virtual ClientIntentFlags ClientIntent
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06001E21 RID: 7713 RVA: 0x00085DDC File Offset: 0x00083FDC
		// (set) Token: 0x06001E22 RID: 7714 RVA: 0x00085DE3 File Offset: 0x00083FE3
		public virtual string ConferenceInfo
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06001E23 RID: 7715 RVA: 0x00085DEA File Offset: 0x00083FEA
		// (set) Token: 0x06001E24 RID: 7716 RVA: 0x00085DF1 File Offset: 0x00083FF1
		public virtual string ConferenceTelURI
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06001E25 RID: 7717 RVA: 0x00085DF8 File Offset: 0x00083FF8
		// (set) Token: 0x06001E26 RID: 7718 RVA: 0x00085DFF File Offset: 0x00083FFF
		public virtual ExDateTime EndTime
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06001E27 RID: 7719 RVA: 0x00085E06 File Offset: 0x00084006
		// (set) Token: 0x06001E28 RID: 7720 RVA: 0x00085E0D File Offset: 0x0008400D
		public virtual ExTimeZone EndTimeZone
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06001E29 RID: 7721 RVA: 0x00085E14 File Offset: 0x00084014
		public virtual ExDateTime EndWallClock
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06001E2A RID: 7722 RVA: 0x00085E1B File Offset: 0x0008401B
		// (set) Token: 0x06001E2B RID: 7723 RVA: 0x00085E22 File Offset: 0x00084022
		public virtual Reminders<EventTimeBasedInboxReminder> EventTimeBasedInboxReminders
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06001E2C RID: 7724 RVA: 0x00085E29 File Offset: 0x00084029
		// (set) Token: 0x06001E2D RID: 7725 RVA: 0x00085E30 File Offset: 0x00084030
		public virtual BusyType FreeBusyStatus
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06001E2E RID: 7726 RVA: 0x00085E37 File Offset: 0x00084037
		public virtual GlobalObjectId GlobalObjectId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06001E2F RID: 7727 RVA: 0x00085E3E File Offset: 0x0008403E
		// (set) Token: 0x06001E30 RID: 7728 RVA: 0x00085E45 File Offset: 0x00084045
		public virtual bool IsAllDayEvent
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06001E31 RID: 7729 RVA: 0x00085E4C File Offset: 0x0008404C
		public virtual bool IsCalendarItemTypeOccurrenceOrException
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06001E32 RID: 7730 RVA: 0x00085E53 File Offset: 0x00084053
		public virtual bool IsCancelled
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06001E33 RID: 7731 RVA: 0x00085E5A File Offset: 0x0008405A
		public virtual bool IsEvent
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06001E34 RID: 7732 RVA: 0x00085E61 File Offset: 0x00084061
		public virtual bool IsForwardAllowed
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06001E35 RID: 7733 RVA: 0x00085E68 File Offset: 0x00084068
		// (set) Token: 0x06001E36 RID: 7734 RVA: 0x00085E6F File Offset: 0x0008406F
		public virtual bool IsMeeting
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06001E37 RID: 7735 RVA: 0x00085E76 File Offset: 0x00084076
		public virtual bool IsOrganizerExternal
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06001E38 RID: 7736 RVA: 0x00085E7D File Offset: 0x0008407D
		public string ItemClass
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06001E39 RID: 7737 RVA: 0x00085E84 File Offset: 0x00084084
		// (set) Token: 0x06001E3A RID: 7738 RVA: 0x00085E8B File Offset: 0x0008408B
		public virtual double? Latitude
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06001E3B RID: 7739 RVA: 0x00085E92 File Offset: 0x00084092
		// (set) Token: 0x06001E3C RID: 7740 RVA: 0x00085E99 File Offset: 0x00084099
		public virtual string Location
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06001E3D RID: 7741 RVA: 0x00085EA0 File Offset: 0x000840A0
		// (set) Token: 0x06001E3E RID: 7742 RVA: 0x00085EA7 File Offset: 0x000840A7
		public virtual string LocationAnnotation
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06001E3F RID: 7743 RVA: 0x00085EAE File Offset: 0x000840AE
		// (set) Token: 0x06001E40 RID: 7744 RVA: 0x00085EB5 File Offset: 0x000840B5
		public virtual string LocationCity
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06001E41 RID: 7745 RVA: 0x00085EBC File Offset: 0x000840BC
		// (set) Token: 0x06001E42 RID: 7746 RVA: 0x00085EC3 File Offset: 0x000840C3
		public virtual string LocationCountry
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06001E43 RID: 7747 RVA: 0x00085ECA File Offset: 0x000840CA
		// (set) Token: 0x06001E44 RID: 7748 RVA: 0x00085ED1 File Offset: 0x000840D1
		public virtual string LocationDisplayName
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06001E45 RID: 7749 RVA: 0x00085ED8 File Offset: 0x000840D8
		// (set) Token: 0x06001E46 RID: 7750 RVA: 0x00085EDF File Offset: 0x000840DF
		public virtual string LocationPostalCode
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06001E47 RID: 7751 RVA: 0x00085EE6 File Offset: 0x000840E6
		// (set) Token: 0x06001E48 RID: 7752 RVA: 0x00085EED File Offset: 0x000840ED
		public virtual LocationSource LocationSource
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06001E49 RID: 7753 RVA: 0x00085EF4 File Offset: 0x000840F4
		// (set) Token: 0x06001E4A RID: 7754 RVA: 0x00085EFB File Offset: 0x000840FB
		public virtual string LocationState
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06001E4B RID: 7755 RVA: 0x00085F02 File Offset: 0x00084102
		// (set) Token: 0x06001E4C RID: 7756 RVA: 0x00085F09 File Offset: 0x00084109
		public virtual string LocationStreet
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06001E4D RID: 7757 RVA: 0x00085F10 File Offset: 0x00084110
		// (set) Token: 0x06001E4E RID: 7758 RVA: 0x00085F17 File Offset: 0x00084117
		public virtual string LocationUri
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06001E4F RID: 7759 RVA: 0x00085F1E File Offset: 0x0008411E
		// (set) Token: 0x06001E50 RID: 7760 RVA: 0x00085F25 File Offset: 0x00084125
		public virtual double? Longitude
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06001E51 RID: 7761 RVA: 0x00085F2C File Offset: 0x0008412C
		public virtual bool MeetingRequestWasSent
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06001E52 RID: 7762 RVA: 0x00085F33 File Offset: 0x00084133
		// (set) Token: 0x06001E53 RID: 7763 RVA: 0x00085F3A File Offset: 0x0008413A
		public virtual string OnlineMeetingConfLink
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06001E54 RID: 7764 RVA: 0x00085F41 File Offset: 0x00084141
		// (set) Token: 0x06001E55 RID: 7765 RVA: 0x00085F48 File Offset: 0x00084148
		public virtual string OnlineMeetingExternalLink
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06001E56 RID: 7766 RVA: 0x00085F4F File Offset: 0x0008414F
		// (set) Token: 0x06001E57 RID: 7767 RVA: 0x00085F56 File Offset: 0x00084156
		public virtual string OnlineMeetingInternalLink
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06001E58 RID: 7768 RVA: 0x00085F5D File Offset: 0x0008415D
		public virtual Participant Organizer
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06001E59 RID: 7769 RVA: 0x00085F64 File Offset: 0x00084164
		// (set) Token: 0x06001E5A RID: 7770 RVA: 0x00085F6B File Offset: 0x0008416B
		public virtual byte[] OutlookUserPropsPropDefStream
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06001E5B RID: 7771 RVA: 0x00085F72 File Offset: 0x00084172
		public virtual int? OwnerAppointmentId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06001E5C RID: 7772 RVA: 0x00085F79 File Offset: 0x00084179
		public virtual ExDateTime OwnerCriticalChangeTime
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06001E5D RID: 7773 RVA: 0x00085F80 File Offset: 0x00084180
		// (set) Token: 0x06001E5E RID: 7774 RVA: 0x00085F87 File Offset: 0x00084187
		public virtual bool ResponseRequested
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06001E5F RID: 7775 RVA: 0x00085F8E File Offset: 0x0008418E
		// (set) Token: 0x06001E60 RID: 7776 RVA: 0x00085F95 File Offset: 0x00084195
		public virtual ResponseType ResponseType
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06001E61 RID: 7777 RVA: 0x00085F9C File Offset: 0x0008419C
		// (set) Token: 0x06001E62 RID: 7778 RVA: 0x00085FA3 File Offset: 0x000841A3
		public virtual string SeriesId
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06001E63 RID: 7779 RVA: 0x00085FAA File Offset: 0x000841AA
		// (set) Token: 0x06001E64 RID: 7780 RVA: 0x00085FB1 File Offset: 0x000841B1
		public virtual string ClientId
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06001E65 RID: 7781 RVA: 0x00085FB8 File Offset: 0x000841B8
		// (set) Token: 0x06001E66 RID: 7782 RVA: 0x00085FBF File Offset: 0x000841BF
		public virtual ExDateTime StartTime
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06001E67 RID: 7783 RVA: 0x00085FC6 File Offset: 0x000841C6
		// (set) Token: 0x06001E68 RID: 7784 RVA: 0x00085FCD File Offset: 0x000841CD
		public virtual ExTimeZone StartTimeZone
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06001E69 RID: 7785 RVA: 0x00085FD4 File Offset: 0x000841D4
		public virtual ExDateTime StartWallClock
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06001E6A RID: 7786 RVA: 0x00085FDB File Offset: 0x000841DB
		// (set) Token: 0x06001E6B RID: 7787 RVA: 0x00085FE2 File Offset: 0x000841E2
		public virtual string Subject
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06001E6C RID: 7788 RVA: 0x00085FE9 File Offset: 0x000841E9
		// (set) Token: 0x06001E6D RID: 7789 RVA: 0x00085FF0 File Offset: 0x000841F0
		public virtual string UCCapabilities
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06001E6E RID: 7790 RVA: 0x00085FF7 File Offset: 0x000841F7
		// (set) Token: 0x06001E6F RID: 7791 RVA: 0x00085FFE File Offset: 0x000841FE
		public virtual string UCInband
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06001E70 RID: 7792 RVA: 0x00086005 File Offset: 0x00084205
		// (set) Token: 0x06001E71 RID: 7793 RVA: 0x0008600C File Offset: 0x0008420C
		public virtual string UCMeetingSetting
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06001E72 RID: 7794 RVA: 0x00086013 File Offset: 0x00084213
		// (set) Token: 0x06001E73 RID: 7795 RVA: 0x0008601A File Offset: 0x0008421A
		public virtual string UCMeetingSettingSent
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06001E74 RID: 7796 RVA: 0x00086021 File Offset: 0x00084221
		// (set) Token: 0x06001E75 RID: 7797 RVA: 0x00086028 File Offset: 0x00084228
		public virtual string UCOpenedConferenceID
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06001E76 RID: 7798 RVA: 0x0008602F File Offset: 0x0008422F
		// (set) Token: 0x06001E77 RID: 7799 RVA: 0x00086036 File Offset: 0x00084236
		public virtual string When
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06001E78 RID: 7800 RVA: 0x0008603D File Offset: 0x0008423D
		// (set) Token: 0x06001E79 RID: 7801 RVA: 0x00086044 File Offset: 0x00084244
		public virtual bool IsReminderSet
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06001E7A RID: 7802 RVA: 0x0008604B File Offset: 0x0008424B
		// (set) Token: 0x06001E7B RID: 7803 RVA: 0x00086052 File Offset: 0x00084252
		public virtual int ReminderMinutesBeforeStart
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06001E7C RID: 7804 RVA: 0x00086059 File Offset: 0x00084259
		// (set) Token: 0x06001E7D RID: 7805 RVA: 0x00086060 File Offset: 0x00084260
		public virtual RemindersState<EventTimeBasedInboxReminderState> EventTimeBasedInboxRemindersState
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x00086067 File Offset: 0x00084267
		public virtual bool IsOrganizer()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x0008606E File Offset: 0x0008426E
		public virtual MeetingResponse RespondToMeetingRequest(ResponseType responseType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x00086075 File Offset: 0x00084275
		public virtual MeetingResponse RespondToMeetingRequest(ResponseType responseType, bool autoCaptureClientIntent, bool intendToSendResponse)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001E81 RID: 7809 RVA: 0x0008607C File Offset: 0x0008427C
		public virtual MeetingResponse RespondToMeetingRequest(ResponseType responseType, bool autoCaptureClientIntent, bool intendToSendResponse, ExDateTime? proposedStart, ExDateTime? proposedEnd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001E82 RID: 7810 RVA: 0x00086083 File Offset: 0x00084283
		public virtual MeetingResponse RespondToMeetingRequest(ResponseType responseType, string subjectPrefix, ExDateTime? proposedStart, ExDateTime? proposedEnd)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001E83 RID: 7811 RVA: 0x0008608A File Offset: 0x0008428A
		public virtual void SendMeetingMessages(bool isToAllAttendees, int? seriesSequenceNumber = null, bool autoCaptureClientIntent = false, bool copyToSentItems = true, string occurrencesViewPropertiesBlob = null, byte[] masterGoid = null)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x00086091 File Offset: 0x00084291
		public void SaveWithConflictCheck(SaveMode saveMode)
		{
			throw new NotImplementedException();
		}
	}
}
