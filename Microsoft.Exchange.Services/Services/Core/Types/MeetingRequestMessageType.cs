using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005EF RID: 1519
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class MeetingRequestMessageType : MeetingMessageType
	{
		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06002E45 RID: 11845 RVA: 0x000B3154 File Offset: 0x000B1354
		// (set) Token: 0x06002E46 RID: 11846 RVA: 0x000B316B File Offset: 0x000B136B
		[IgnoreDataMember]
		[XmlElement("MeetingRequestType")]
		public RequestType MeetingRequestType
		{
			get
			{
				if (!this.MeetingRequestTypeSpecified)
				{
					return RequestType.None;
				}
				return EnumUtilities.Parse<RequestType>(this.MeetingRequestTypeString);
			}
			set
			{
				this.MeetingRequestTypeString = EnumUtilities.ToString<RequestType>(value);
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06002E47 RID: 11847 RVA: 0x000B3179 File Offset: 0x000B1379
		// (set) Token: 0x06002E48 RID: 11848 RVA: 0x000B3186 File Offset: 0x000B1386
		[XmlIgnore]
		[DataMember(Name = "MeetingRequestType", EmitDefaultValue = false, Order = 1)]
		public string MeetingRequestTypeString
		{
			get
			{
				return base.GetValueOrDefault<string>(MeetingRequestSchema.MeetingRequestType);
			}
			set
			{
				this[MeetingRequestSchema.MeetingRequestType] = value;
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06002E49 RID: 11849 RVA: 0x000B3194 File Offset: 0x000B1394
		// (set) Token: 0x06002E4A RID: 11850 RVA: 0x000B31A1 File Offset: 0x000B13A1
		[IgnoreDataMember]
		[XmlIgnore]
		public bool MeetingRequestTypeSpecified
		{
			get
			{
				return base.IsSet(MeetingRequestSchema.MeetingRequestType);
			}
			set
			{
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06002E4B RID: 11851 RVA: 0x000B31A3 File Offset: 0x000B13A3
		// (set) Token: 0x06002E4C RID: 11852 RVA: 0x000B31BA File Offset: 0x000B13BA
		[XmlElement("IntendedFreeBusyStatus")]
		[IgnoreDataMember]
		public Microsoft.Exchange.InfoWorker.Common.Availability.BusyType IntendedFreeBusyStatus
		{
			get
			{
				if (!this.IntendedFreeBusyStatusSpecified)
				{
					return Microsoft.Exchange.InfoWorker.Common.Availability.BusyType.Free;
				}
				return EnumUtilities.Parse<Microsoft.Exchange.InfoWorker.Common.Availability.BusyType>(this.IntendedFreeBusyStatusString);
			}
			set
			{
				this.IntendedFreeBusyStatusString = EnumUtilities.ToString<Microsoft.Exchange.InfoWorker.Common.Availability.BusyType>(value);
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06002E4D RID: 11853 RVA: 0x000B31C8 File Offset: 0x000B13C8
		// (set) Token: 0x06002E4E RID: 11854 RVA: 0x000B31D5 File Offset: 0x000B13D5
		[DataMember(Name = "IntendedFreeBusyStatus", EmitDefaultValue = false, Order = 2)]
		[XmlIgnore]
		public string IntendedFreeBusyStatusString
		{
			get
			{
				return base.GetValueOrDefault<string>(MeetingRequestSchema.IntendedFreeBusyStatus);
			}
			set
			{
				this[MeetingRequestSchema.IntendedFreeBusyStatus] = value;
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06002E4F RID: 11855 RVA: 0x000B31E3 File Offset: 0x000B13E3
		// (set) Token: 0x06002E50 RID: 11856 RVA: 0x000B31F0 File Offset: 0x000B13F0
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IntendedFreeBusyStatusSpecified
		{
			get
			{
				return base.IsSet(MeetingRequestSchema.IntendedFreeBusyStatus);
			}
			set
			{
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x06002E51 RID: 11857 RVA: 0x000B31F2 File Offset: 0x000B13F2
		// (set) Token: 0x06002E52 RID: 11858 RVA: 0x000B31FF File Offset: 0x000B13FF
		[DataMember(EmitDefaultValue = false, Order = 3)]
		[DateTimeString]
		public string Start
		{
			get
			{
				return base.GetValueOrDefault<string>(MeetingRequestSchema.Start);
			}
			set
			{
				this[MeetingRequestSchema.Start] = value;
			}
		}

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x06002E53 RID: 11859 RVA: 0x000B320D File Offset: 0x000B140D
		// (set) Token: 0x06002E54 RID: 11860 RVA: 0x000B321A File Offset: 0x000B141A
		[XmlIgnore]
		[IgnoreDataMember]
		public bool StartSpecified
		{
			get
			{
				return base.IsSet(MeetingRequestSchema.Start);
			}
			set
			{
			}
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06002E55 RID: 11861 RVA: 0x000B321C File Offset: 0x000B141C
		// (set) Token: 0x06002E56 RID: 11862 RVA: 0x000B3229 File Offset: 0x000B1429
		[DataMember(EmitDefaultValue = false, Order = 4)]
		[DateTimeString]
		public string End
		{
			get
			{
				return base.GetValueOrDefault<string>(MeetingRequestSchema.End);
			}
			set
			{
				this[MeetingRequestSchema.End] = value;
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06002E57 RID: 11863 RVA: 0x000B3237 File Offset: 0x000B1437
		// (set) Token: 0x06002E58 RID: 11864 RVA: 0x000B3244 File Offset: 0x000B1444
		[IgnoreDataMember]
		[XmlIgnore]
		public bool EndSpecified
		{
			get
			{
				return base.IsSet(MeetingRequestSchema.End);
			}
			set
			{
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06002E59 RID: 11865 RVA: 0x000B3246 File Offset: 0x000B1446
		// (set) Token: 0x06002E5A RID: 11866 RVA: 0x000B3253 File Offset: 0x000B1453
		[DataMember(EmitDefaultValue = false, Order = 5)]
		[DateTimeString]
		public string OriginalStart
		{
			get
			{
				return base.GetValueOrDefault<string>(CalendarItemSchema.OriginalStart);
			}
			set
			{
				this[CalendarItemSchema.OriginalStart] = value;
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06002E5B RID: 11867 RVA: 0x000B3261 File Offset: 0x000B1461
		// (set) Token: 0x06002E5C RID: 11868 RVA: 0x000B326E File Offset: 0x000B146E
		[IgnoreDataMember]
		[XmlIgnore]
		public bool OriginalStartSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.OriginalStart);
			}
			set
			{
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06002E5D RID: 11869 RVA: 0x000B3270 File Offset: 0x000B1470
		// (set) Token: 0x06002E5E RID: 11870 RVA: 0x000B3282 File Offset: 0x000B1482
		[DataMember(EmitDefaultValue = false, Order = 6)]
		public bool? IsAllDayEvent
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<bool?>(CalendarItemSchema.IsAllDayEvent);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.IsAllDayEvent] = value;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06002E5F RID: 11871 RVA: 0x000B329A File Offset: 0x000B149A
		// (set) Token: 0x06002E60 RID: 11872 RVA: 0x000B32A7 File Offset: 0x000B14A7
		[IgnoreDataMember]
		[XmlIgnore]
		public bool IsAllDayEventSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.IsAllDayEvent);
			}
			set
			{
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06002E61 RID: 11873 RVA: 0x000B32A9 File Offset: 0x000B14A9
		// (set) Token: 0x06002E62 RID: 11874 RVA: 0x000B32C0 File Offset: 0x000B14C0
		[IgnoreDataMember]
		[XmlElement]
		public Microsoft.Exchange.InfoWorker.Common.Availability.BusyType LegacyFreeBusyStatus
		{
			get
			{
				if (!this.LegacyFreeBusyStatusSpecified)
				{
					return Microsoft.Exchange.InfoWorker.Common.Availability.BusyType.Free;
				}
				return EnumUtilities.Parse<Microsoft.Exchange.InfoWorker.Common.Availability.BusyType>(this.LegacyFreeBusyStatusString);
			}
			set
			{
				this.LegacyFreeBusyStatusString = EnumUtilities.ToString<Microsoft.Exchange.InfoWorker.Common.Availability.BusyType>(value);
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06002E63 RID: 11875 RVA: 0x000B32CE File Offset: 0x000B14CE
		// (set) Token: 0x06002E64 RID: 11876 RVA: 0x000B32E0 File Offset: 0x000B14E0
		[XmlIgnore]
		[DataMember(Name = "FreeBusyType", EmitDefaultValue = false, Order = 7)]
		public string LegacyFreeBusyStatusString
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.LegacyFreeBusyStatus);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.LegacyFreeBusyStatus] = value;
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06002E65 RID: 11877 RVA: 0x000B32F3 File Offset: 0x000B14F3
		// (set) Token: 0x06002E66 RID: 11878 RVA: 0x000B3300 File Offset: 0x000B1500
		[IgnoreDataMember]
		[XmlIgnore]
		public bool LegacyFreeBusyStatusSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.LegacyFreeBusyStatus);
			}
			set
			{
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06002E67 RID: 11879 RVA: 0x000B3302 File Offset: 0x000B1502
		// (set) Token: 0x06002E68 RID: 11880 RVA: 0x000B3314 File Offset: 0x000B1514
		[IgnoreDataMember]
		public string Location
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(MeetingRequestSchema.Location);
			}
			set
			{
				base.PropertyBag[MeetingRequestSchema.Location] = value;
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06002E69 RID: 11881 RVA: 0x000B3327 File Offset: 0x000B1527
		// (set) Token: 0x06002E6A RID: 11882 RVA: 0x000B3339 File Offset: 0x000B1539
		[DataMember(EmitDefaultValue = false, Order = 8)]
		public string When
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.When);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.When] = value;
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06002E6B RID: 11883 RVA: 0x000B334C File Offset: 0x000B154C
		// (set) Token: 0x06002E6C RID: 11884 RVA: 0x000B335E File Offset: 0x000B155E
		[DataMember(EmitDefaultValue = false, Order = 9)]
		public bool? IsMeeting
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<bool?>(CalendarItemSchema.IsMeeting);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.IsMeeting] = value;
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06002E6D RID: 11885 RVA: 0x000B3376 File Offset: 0x000B1576
		// (set) Token: 0x06002E6E RID: 11886 RVA: 0x000B3383 File Offset: 0x000B1583
		[IgnoreDataMember]
		[XmlIgnore]
		public bool IsMeetingSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.IsMeeting);
			}
			set
			{
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06002E6F RID: 11887 RVA: 0x000B3385 File Offset: 0x000B1585
		// (set) Token: 0x06002E70 RID: 11888 RVA: 0x000B3397 File Offset: 0x000B1597
		[DataMember(EmitDefaultValue = false, Order = 10)]
		public bool? IsCancelled
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<bool?>(CalendarItemSchema.IsCancelled);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.IsCancelled] = value;
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06002E71 RID: 11889 RVA: 0x000B33AF File Offset: 0x000B15AF
		// (set) Token: 0x06002E72 RID: 11890 RVA: 0x000B33BC File Offset: 0x000B15BC
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IsCancelledSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.IsCancelled);
			}
			set
			{
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06002E73 RID: 11891 RVA: 0x000B33BE File Offset: 0x000B15BE
		// (set) Token: 0x06002E74 RID: 11892 RVA: 0x000B33D0 File Offset: 0x000B15D0
		[DataMember(EmitDefaultValue = false, Order = 11)]
		public bool? IsRecurring
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<bool?>(CalendarItemSchema.IsRecurring);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.IsRecurring] = value;
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06002E75 RID: 11893 RVA: 0x000B33E8 File Offset: 0x000B15E8
		// (set) Token: 0x06002E76 RID: 11894 RVA: 0x000B33F5 File Offset: 0x000B15F5
		[IgnoreDataMember]
		[XmlIgnore]
		public bool IsRecurringSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.IsRecurring);
			}
			set
			{
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06002E77 RID: 11895 RVA: 0x000B33F7 File Offset: 0x000B15F7
		// (set) Token: 0x06002E78 RID: 11896 RVA: 0x000B3409 File Offset: 0x000B1609
		[DataMember(EmitDefaultValue = false, Order = 12)]
		public bool? MeetingRequestWasSent
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<bool?>(CalendarItemSchema.MeetingRequestWasSent);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.MeetingRequestWasSent] = value;
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06002E79 RID: 11897 RVA: 0x000B3421 File Offset: 0x000B1621
		// (set) Token: 0x06002E7A RID: 11898 RVA: 0x000B342E File Offset: 0x000B162E
		[XmlIgnore]
		[IgnoreDataMember]
		public bool MeetingRequestWasSentSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.MeetingRequestWasSent);
			}
			set
			{
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06002E7B RID: 11899 RVA: 0x000B3430 File Offset: 0x000B1630
		// (set) Token: 0x06002E7C RID: 11900 RVA: 0x000B3447 File Offset: 0x000B1647
		[IgnoreDataMember]
		[XmlElement("CalendarItemType")]
		public CalendarItemTypeType CalendarItemType
		{
			get
			{
				if (!this.CalendarItemTypeSpecified)
				{
					return CalendarItemTypeType.Single;
				}
				return EnumUtilities.Parse<CalendarItemTypeType>(this.CalendarItemTypeString);
			}
			set
			{
				this.CalendarItemTypeString = EnumUtilities.ToString<CalendarItemTypeType>(value);
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06002E7D RID: 11901 RVA: 0x000B3455 File Offset: 0x000B1655
		// (set) Token: 0x06002E7E RID: 11902 RVA: 0x000B3467 File Offset: 0x000B1667
		[DataMember(Name = "CalendarItemType", EmitDefaultValue = false, Order = 13)]
		[XmlIgnore]
		public string CalendarItemTypeString
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.CalendarItemType);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.CalendarItemType] = value;
			}
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06002E7F RID: 11903 RVA: 0x000B347A File Offset: 0x000B167A
		// (set) Token: 0x06002E80 RID: 11904 RVA: 0x000B3487 File Offset: 0x000B1687
		[IgnoreDataMember]
		[XmlIgnore]
		public bool CalendarItemTypeSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.CalendarItemType);
			}
			set
			{
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06002E81 RID: 11905 RVA: 0x000B3489 File Offset: 0x000B1689
		// (set) Token: 0x06002E82 RID: 11906 RVA: 0x000B349B File Offset: 0x000B169B
		[XmlElement]
		[DataMember(Name = "MyResponseType", EmitDefaultValue = false, Order = 14)]
		public string MyResponseType
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.MyResponseType);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.MyResponseType] = value;
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06002E83 RID: 11907 RVA: 0x000B34AE File Offset: 0x000B16AE
		// (set) Token: 0x06002E84 RID: 11908 RVA: 0x000B34BB File Offset: 0x000B16BB
		[IgnoreDataMember]
		[XmlIgnore]
		public bool MyResponseTypeSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.MyResponseType);
			}
			set
			{
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06002E85 RID: 11909 RVA: 0x000B34BD File Offset: 0x000B16BD
		// (set) Token: 0x06002E86 RID: 11910 RVA: 0x000B34CF File Offset: 0x000B16CF
		[DataMember(EmitDefaultValue = false, Order = 15)]
		public SingleRecipientType Organizer
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<SingleRecipientType>(CalendarItemSchema.Organizer);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.Organizer] = value;
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06002E87 RID: 11911 RVA: 0x000B34E2 File Offset: 0x000B16E2
		// (set) Token: 0x06002E88 RID: 11912 RVA: 0x000B34F4 File Offset: 0x000B16F4
		[DataMember(EmitDefaultValue = false, Order = 16)]
		[XmlArrayItem("Attendee", IsNullable = false)]
		public EwsAttendeeType[] RequiredAttendees
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<EwsAttendeeType[]>(CalendarItemSchema.AttendeeSpecific.RequiredAttendees);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.AttendeeSpecific.RequiredAttendees] = value;
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06002E89 RID: 11913 RVA: 0x000B3507 File Offset: 0x000B1707
		// (set) Token: 0x06002E8A RID: 11914 RVA: 0x000B3514 File Offset: 0x000B1714
		[DataMember(EmitDefaultValue = false, Order = 17)]
		[XmlArrayItem("Attendee", IsNullable = false)]
		public EwsAttendeeType[] OptionalAttendees
		{
			get
			{
				return base.GetValueOrDefault<EwsAttendeeType[]>(CalendarItemSchema.AttendeeSpecific.OptionalAttendees);
			}
			set
			{
				this[CalendarItemSchema.AttendeeSpecific.OptionalAttendees] = value;
			}
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06002E8B RID: 11915 RVA: 0x000B3522 File Offset: 0x000B1722
		// (set) Token: 0x06002E8C RID: 11916 RVA: 0x000B3534 File Offset: 0x000B1734
		[DataMember(EmitDefaultValue = false, Order = 18)]
		[XmlArrayItem("Attendee", IsNullable = false)]
		public EwsAttendeeType[] Resources
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<EwsAttendeeType[]>(CalendarItemSchema.AttendeeSpecific.Resources);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.AttendeeSpecific.Resources] = value;
			}
		}

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06002E8D RID: 11917 RVA: 0x000B3547 File Offset: 0x000B1747
		// (set) Token: 0x06002E8E RID: 11918 RVA: 0x000B3559 File Offset: 0x000B1759
		[DataMember(EmitDefaultValue = false, Order = 19)]
		public int? ConflictingMeetingCount
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<int?>(CalendarItemSchema.ConflictingMeetingCount);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.ConflictingMeetingCount] = value;
			}
		}

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06002E8F RID: 11919 RVA: 0x000B3571 File Offset: 0x000B1771
		// (set) Token: 0x06002E90 RID: 11920 RVA: 0x000B357E File Offset: 0x000B177E
		[XmlIgnore]
		[IgnoreDataMember]
		public bool ConflictingMeetingCountSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.ConflictingMeetingCount);
			}
			set
			{
			}
		}

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06002E91 RID: 11921 RVA: 0x000B3580 File Offset: 0x000B1780
		// (set) Token: 0x06002E92 RID: 11922 RVA: 0x000B3592 File Offset: 0x000B1792
		[DataMember(EmitDefaultValue = false, Order = 20)]
		public int? AdjacentMeetingCount
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<int?>(CalendarItemSchema.AdjacentMeetingCount);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.AdjacentMeetingCount] = value;
			}
		}

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06002E93 RID: 11923 RVA: 0x000B35AA File Offset: 0x000B17AA
		// (set) Token: 0x06002E94 RID: 11924 RVA: 0x000B35B7 File Offset: 0x000B17B7
		[XmlIgnore]
		[IgnoreDataMember]
		public bool AdjacentMeetingCountSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.AdjacentMeetingCount);
			}
			set
			{
			}
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06002E95 RID: 11925 RVA: 0x000B35B9 File Offset: 0x000B17B9
		// (set) Token: 0x06002E96 RID: 11926 RVA: 0x000B35CB File Offset: 0x000B17CB
		[DataMember(EmitDefaultValue = false, Order = 21)]
		public NonEmptyArrayOfAllItemsType ConflictingMeetings
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<NonEmptyArrayOfAllItemsType>(CalendarItemSchema.ConflictingMeetings);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.ConflictingMeetings] = value;
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06002E97 RID: 11927 RVA: 0x000B35DE File Offset: 0x000B17DE
		// (set) Token: 0x06002E98 RID: 11928 RVA: 0x000B35F0 File Offset: 0x000B17F0
		[DataMember(EmitDefaultValue = false, Order = 22)]
		public NonEmptyArrayOfAllItemsType AdjacentMeetings
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<NonEmptyArrayOfAllItemsType>(CalendarItemSchema.AdjacentMeetings);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.AdjacentMeetings] = value;
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06002E99 RID: 11929 RVA: 0x000B3603 File Offset: 0x000B1803
		// (set) Token: 0x06002E9A RID: 11930 RVA: 0x000B3615 File Offset: 0x000B1815
		[DataMember(EmitDefaultValue = false, Order = 23)]
		public string Duration
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.Duration);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.Duration] = value;
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06002E9B RID: 11931 RVA: 0x000B3628 File Offset: 0x000B1828
		// (set) Token: 0x06002E9C RID: 11932 RVA: 0x000B363A File Offset: 0x000B183A
		[DataMember(EmitDefaultValue = false, Order = 24)]
		public string TimeZone
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.TimeZone);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.TimeZone] = value;
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06002E9D RID: 11933 RVA: 0x000B364D File Offset: 0x000B184D
		// (set) Token: 0x06002E9E RID: 11934 RVA: 0x000B365F File Offset: 0x000B185F
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, Order = 25)]
		public string AppointmentReplyTime
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.AppointmentReplyTime);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.AppointmentReplyTime] = value;
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06002E9F RID: 11935 RVA: 0x000B3672 File Offset: 0x000B1872
		// (set) Token: 0x06002EA0 RID: 11936 RVA: 0x000B367F File Offset: 0x000B187F
		[IgnoreDataMember]
		[XmlIgnore]
		public bool AppointmentReplyTimeSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.AppointmentReplyTime);
			}
			set
			{
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06002EA1 RID: 11937 RVA: 0x000B3681 File Offset: 0x000B1881
		// (set) Token: 0x06002EA2 RID: 11938 RVA: 0x000B3693 File Offset: 0x000B1893
		[DataMember(EmitDefaultValue = false, Order = 26)]
		public int? AppointmentSequenceNumber
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<int?>(CalendarItemSchema.AppointmentSequenceNumber);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.AppointmentSequenceNumber] = value;
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06002EA3 RID: 11939 RVA: 0x000B36AB File Offset: 0x000B18AB
		// (set) Token: 0x06002EA4 RID: 11940 RVA: 0x000B36B8 File Offset: 0x000B18B8
		[IgnoreDataMember]
		[XmlIgnore]
		public bool AppointmentSequenceNumberSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.AppointmentSequenceNumber);
			}
			set
			{
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06002EA5 RID: 11941 RVA: 0x000B36BA File Offset: 0x000B18BA
		// (set) Token: 0x06002EA6 RID: 11942 RVA: 0x000B36CC File Offset: 0x000B18CC
		[DataMember(EmitDefaultValue = false, Order = 27)]
		public int? AppointmentState
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<int?>(CalendarItemSchema.AppointmentState);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.AppointmentState] = value;
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06002EA7 RID: 11943 RVA: 0x000B36E4 File Offset: 0x000B18E4
		// (set) Token: 0x06002EA8 RID: 11944 RVA: 0x000B36F1 File Offset: 0x000B18F1
		[IgnoreDataMember]
		[XmlIgnore]
		public bool AppointmentStateSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.AppointmentState);
			}
			set
			{
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06002EA9 RID: 11945 RVA: 0x000B36F3 File Offset: 0x000B18F3
		// (set) Token: 0x06002EAA RID: 11946 RVA: 0x000B3705 File Offset: 0x000B1905
		[DataMember(EmitDefaultValue = false, Order = 28)]
		public RecurrenceType Recurrence
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<RecurrenceType>(CalendarItemSchema.OrganizerSpecific.Recurrence);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.OrganizerSpecific.Recurrence] = value;
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06002EAB RID: 11947 RVA: 0x000B3718 File Offset: 0x000B1918
		// (set) Token: 0x06002EAC RID: 11948 RVA: 0x000B372A File Offset: 0x000B192A
		[DataMember(EmitDefaultValue = false, Order = 29)]
		public OccurrenceInfoType FirstOccurrence
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<OccurrenceInfoType>(CalendarItemSchema.FirstOccurrence);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.FirstOccurrence] = value;
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06002EAD RID: 11949 RVA: 0x000B373D File Offset: 0x000B193D
		// (set) Token: 0x06002EAE RID: 11950 RVA: 0x000B374F File Offset: 0x000B194F
		[DataMember(EmitDefaultValue = false, Order = 30)]
		public OccurrenceInfoType LastOccurrence
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<OccurrenceInfoType>(CalendarItemSchema.LastOccurrence);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.LastOccurrence] = value;
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06002EAF RID: 11951 RVA: 0x000B3762 File Offset: 0x000B1962
		// (set) Token: 0x06002EB0 RID: 11952 RVA: 0x000B3774 File Offset: 0x000B1974
		[DataMember(EmitDefaultValue = false, Order = 31)]
		[XmlArrayItem("Occurrence", IsNullable = false)]
		public OccurrenceInfoType[] ModifiedOccurrences
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<OccurrenceInfoType[]>(CalendarItemSchema.ModifiedOccurrences);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.ModifiedOccurrences] = value;
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06002EB1 RID: 11953 RVA: 0x000B3787 File Offset: 0x000B1987
		// (set) Token: 0x06002EB2 RID: 11954 RVA: 0x000B3799 File Offset: 0x000B1999
		[DataMember(EmitDefaultValue = false, Order = 32)]
		[XmlArrayItem("DeletedOccurrence", IsNullable = false)]
		public DeletedOccurrenceInfoType[] DeletedOccurrences
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<DeletedOccurrenceInfoType[]>(CalendarItemSchema.DeletedOccurrences);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.DeletedOccurrences] = value;
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06002EB3 RID: 11955 RVA: 0x000B37AC File Offset: 0x000B19AC
		// (set) Token: 0x06002EB4 RID: 11956 RVA: 0x000B37BE File Offset: 0x000B19BE
		[DataMember(EmitDefaultValue = false, Order = 33)]
		public TimeZoneType MeetingTimeZone
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<TimeZoneType>(CalendarItemSchema.MeetingTimeZone);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.MeetingTimeZone] = value;
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06002EB5 RID: 11957 RVA: 0x000B37D1 File Offset: 0x000B19D1
		// (set) Token: 0x06002EB6 RID: 11958 RVA: 0x000B37E3 File Offset: 0x000B19E3
		[DataMember(EmitDefaultValue = false, Order = 34)]
		public TimeZoneDefinitionType StartTimeZone
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<TimeZoneDefinitionType>(CalendarItemSchema.OrganizerSpecific.StartTimeZone);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.OrganizerSpecific.StartTimeZone] = value;
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06002EB7 RID: 11959 RVA: 0x000B37F6 File Offset: 0x000B19F6
		// (set) Token: 0x06002EB8 RID: 11960 RVA: 0x000B3808 File Offset: 0x000B1A08
		[DataMember(EmitDefaultValue = false, Order = 35)]
		public TimeZoneDefinitionType EndTimeZone
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<TimeZoneDefinitionType>(CalendarItemSchema.OrganizerSpecific.EndTimeZone);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.OrganizerSpecific.EndTimeZone] = value;
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06002EB9 RID: 11961 RVA: 0x000B381B File Offset: 0x000B1A1B
		// (set) Token: 0x06002EBA RID: 11962 RVA: 0x000B382D File Offset: 0x000B1A2D
		[DataMember(EmitDefaultValue = false, Order = 36)]
		public int? ConferenceType
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<int?>(CalendarItemSchema.AttendeeSpecific.ConferenceType);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.AttendeeSpecific.ConferenceType] = value;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06002EBB RID: 11963 RVA: 0x000B3845 File Offset: 0x000B1A45
		// (set) Token: 0x06002EBC RID: 11964 RVA: 0x000B3852 File Offset: 0x000B1A52
		[IgnoreDataMember]
		[XmlIgnore]
		public bool ConferenceTypeSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.AttendeeSpecific.ConferenceType);
			}
			set
			{
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06002EBD RID: 11965 RVA: 0x000B3854 File Offset: 0x000B1A54
		// (set) Token: 0x06002EBE RID: 11966 RVA: 0x000B3866 File Offset: 0x000B1A66
		[DataMember(EmitDefaultValue = false, Order = 37)]
		public bool? AllowNewTimeProposal
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<bool?>(CalendarItemSchema.AttendeeSpecific.AllowNewTimeProposal);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.AttendeeSpecific.AllowNewTimeProposal] = value;
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06002EBF RID: 11967 RVA: 0x000B387E File Offset: 0x000B1A7E
		// (set) Token: 0x06002EC0 RID: 11968 RVA: 0x000B388B File Offset: 0x000B1A8B
		[XmlIgnore]
		[IgnoreDataMember]
		public bool AllowNewTimeProposalSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.AttendeeSpecific.AllowNewTimeProposal);
			}
			set
			{
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06002EC1 RID: 11969 RVA: 0x000B388D File Offset: 0x000B1A8D
		// (set) Token: 0x06002EC2 RID: 11970 RVA: 0x000B389F File Offset: 0x000B1A9F
		[DataMember(EmitDefaultValue = false, Order = 38)]
		public bool? IsOnlineMeeting
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<bool?>(CalendarItemSchema.AttendeeSpecific.IsOnlineMeeting);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.AttendeeSpecific.IsOnlineMeeting] = value;
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06002EC3 RID: 11971 RVA: 0x000B38B7 File Offset: 0x000B1AB7
		// (set) Token: 0x06002EC4 RID: 11972 RVA: 0x000B38C4 File Offset: 0x000B1AC4
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IsOnlineMeetingSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.AttendeeSpecific.IsOnlineMeeting);
			}
			set
			{
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x06002EC5 RID: 11973 RVA: 0x000B38C6 File Offset: 0x000B1AC6
		// (set) Token: 0x06002EC6 RID: 11974 RVA: 0x000B38D8 File Offset: 0x000B1AD8
		[DataMember(EmitDefaultValue = false, Order = 39)]
		public string MeetingWorkspaceUrl
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.AttendeeSpecific.MeetingWorkspaceUrl);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.AttendeeSpecific.MeetingWorkspaceUrl] = value;
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x06002EC7 RID: 11975 RVA: 0x000B38EB File Offset: 0x000B1AEB
		// (set) Token: 0x06002EC8 RID: 11976 RVA: 0x000B38FD File Offset: 0x000B1AFD
		[DataMember(EmitDefaultValue = false, Order = 40)]
		public string NetShowUrl
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.AttendeeSpecific.NetShowUrl);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.AttendeeSpecific.NetShowUrl] = value;
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x06002EC9 RID: 11977 RVA: 0x000B3910 File Offset: 0x000B1B10
		// (set) Token: 0x06002ECA RID: 11978 RVA: 0x000B3922 File Offset: 0x000B1B22
		[DataMember(Name = "Location", EmitDefaultValue = false, Order = 41)]
		public EnhancedLocationType EnhancedLocation
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<EnhancedLocationType>(MeetingRequestSchema.EnhancedLocation);
			}
			set
			{
				base.PropertyBag[MeetingRequestSchema.EnhancedLocation] = value;
			}
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06002ECB RID: 11979 RVA: 0x000B3935 File Offset: 0x000B1B35
		// (set) Token: 0x06002ECC RID: 11980 RVA: 0x000B3947 File Offset: 0x000B1B47
		[DataMember(EmitDefaultValue = false, Order = 42)]
		public ChangeHighlightsType ChangeHighlights
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<ChangeHighlightsType>(MeetingRequestSchema.ChangeHighlights);
			}
			set
			{
				base.PropertyBag[MeetingRequestSchema.ChangeHighlights] = value;
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06002ECD RID: 11981 RVA: 0x000B395A File Offset: 0x000B1B5A
		// (set) Token: 0x06002ECE RID: 11982 RVA: 0x000B396C File Offset: 0x000B1B6C
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, Order = 43)]
		public string StartWallClock
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.StartWallClock);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.StartWallClock] = value;
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06002ECF RID: 11983 RVA: 0x000B397F File Offset: 0x000B1B7F
		// (set) Token: 0x06002ED0 RID: 11984 RVA: 0x000B3991 File Offset: 0x000B1B91
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, Order = 44)]
		public string EndWallClock
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.EndWallClock);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.EndWallClock] = value;
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06002ED1 RID: 11985 RVA: 0x000B39A4 File Offset: 0x000B1BA4
		// (set) Token: 0x06002ED2 RID: 11986 RVA: 0x000B39B6 File Offset: 0x000B1BB6
		[DataMember(EmitDefaultValue = false, Order = 45)]
		public string StartTimeZoneId
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.StartTimeZoneId);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.StartTimeZoneId] = value;
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06002ED3 RID: 11987 RVA: 0x000B39C9 File Offset: 0x000B1BC9
		// (set) Token: 0x06002ED4 RID: 11988 RVA: 0x000B39DB File Offset: 0x000B1BDB
		[DataMember(EmitDefaultValue = false, Order = 46)]
		public string EndTimeZoneId
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.EndTimeZoneId);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.EndTimeZoneId] = value;
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06002ED5 RID: 11989 RVA: 0x000B39EE File Offset: 0x000B1BEE
		// (set) Token: 0x06002ED6 RID: 11990 RVA: 0x000B3A00 File Offset: 0x000B1C00
		[DataMember(EmitDefaultValue = false, Order = 47)]
		[XmlIgnore]
		public AttendeeCountsType AttendeeCounts
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<AttendeeCountsType>(CalendarItemSchema.AttendeeSpecific.AttendeeCounts);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.AttendeeSpecific.AttendeeCounts] = value;
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06002ED7 RID: 11991 RVA: 0x000B3A13 File Offset: 0x000B1C13
		internal override StoreObjectType StoreObjectType
		{
			get
			{
				return StoreObjectType.MeetingRequest;
			}
		}
	}
}
