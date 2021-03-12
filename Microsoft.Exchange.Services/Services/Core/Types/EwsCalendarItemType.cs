using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005A9 RID: 1449
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "CalendarItem")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", TypeName = "CalendarItemType")]
	[Serializable]
	public class EwsCalendarItemType : ItemType
	{
		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06002A1E RID: 10782 RVA: 0x000ADFF8 File Offset: 0x000AC1F8
		// (set) Token: 0x06002A1F RID: 10783 RVA: 0x000AE00A File Offset: 0x000AC20A
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public string UID
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.ICalendarUid);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.ICalendarUid] = value;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06002A20 RID: 10784 RVA: 0x000AE01D File Offset: 0x000AC21D
		// (set) Token: 0x06002A21 RID: 10785 RVA: 0x000AE02F File Offset: 0x000AC22F
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public string RecurrenceId
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.ICalendarRecurrenceId);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.ICalendarRecurrenceId] = value;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06002A22 RID: 10786 RVA: 0x000AE042 File Offset: 0x000AC242
		// (set) Token: 0x06002A23 RID: 10787 RVA: 0x000AE04F File Offset: 0x000AC24F
		[IgnoreDataMember]
		[XmlIgnore]
		public bool RecurrenceIdSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.ICalendarRecurrenceId);
			}
			set
			{
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06002A24 RID: 10788 RVA: 0x000AE051 File Offset: 0x000AC251
		// (set) Token: 0x06002A25 RID: 10789 RVA: 0x000AE063 File Offset: 0x000AC263
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public string DateTimeStamp
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.ICalendarDateTimeStamp);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.ICalendarDateTimeStamp] = value;
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06002A26 RID: 10790 RVA: 0x000AE076 File Offset: 0x000AC276
		// (set) Token: 0x06002A27 RID: 10791 RVA: 0x000AE083 File Offset: 0x000AC283
		[IgnoreDataMember]
		[XmlIgnore]
		public bool DateTimeStampSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.ICalendarDateTimeStamp);
			}
			set
			{
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06002A28 RID: 10792 RVA: 0x000AE085 File Offset: 0x000AC285
		// (set) Token: 0x06002A29 RID: 10793 RVA: 0x000AE097 File Offset: 0x000AC297
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, Order = 4)]
		public string Start
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.Start);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.Start] = value;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06002A2A RID: 10794 RVA: 0x000AE0AA File Offset: 0x000AC2AA
		// (set) Token: 0x06002A2B RID: 10795 RVA: 0x000AE0B7 File Offset: 0x000AC2B7
		[IgnoreDataMember]
		[XmlIgnore]
		public bool StartSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.Start);
			}
			set
			{
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06002A2C RID: 10796 RVA: 0x000AE0B9 File Offset: 0x000AC2B9
		// (set) Token: 0x06002A2D RID: 10797 RVA: 0x000AE0CB File Offset: 0x000AC2CB
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, Order = 5)]
		public string End
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.End);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.End] = value;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06002A2E RID: 10798 RVA: 0x000AE0DE File Offset: 0x000AC2DE
		// (set) Token: 0x06002A2F RID: 10799 RVA: 0x000AE0EB File Offset: 0x000AC2EB
		[XmlIgnore]
		[IgnoreDataMember]
		public bool EndSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.End);
			}
			set
			{
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06002A30 RID: 10800 RVA: 0x000AE0ED File Offset: 0x000AC2ED
		// (set) Token: 0x06002A31 RID: 10801 RVA: 0x000AE0FF File Offset: 0x000AC2FF
		[DataMember(EmitDefaultValue = false, Order = 6)]
		[DateTimeString]
		public string OriginalStart
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.OriginalStart);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.OriginalStart] = value;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06002A32 RID: 10802 RVA: 0x000AE112 File Offset: 0x000AC312
		// (set) Token: 0x06002A33 RID: 10803 RVA: 0x000AE11F File Offset: 0x000AC31F
		[XmlIgnore]
		[IgnoreDataMember]
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

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06002A34 RID: 10804 RVA: 0x000AE121 File Offset: 0x000AC321
		// (set) Token: 0x06002A35 RID: 10805 RVA: 0x000AE133 File Offset: 0x000AC333
		[DataMember(EmitDefaultValue = true, Order = 7)]
		public bool? IsAllDayEvent
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(CalendarItemSchema.IsAllDayEvent);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(CalendarItemSchema.IsAllDayEvent, value);
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06002A36 RID: 10806 RVA: 0x000AE146 File Offset: 0x000AC346
		// (set) Token: 0x06002A37 RID: 10807 RVA: 0x000AE153 File Offset: 0x000AC353
		[XmlIgnore]
		[IgnoreDataMember]
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

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06002A38 RID: 10808 RVA: 0x000AE155 File Offset: 0x000AC355
		// (set) Token: 0x06002A39 RID: 10809 RVA: 0x000AE16C File Offset: 0x000AC36C
		[XmlElement]
		[IgnoreDataMember]
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

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06002A3A RID: 10810 RVA: 0x000AE17A File Offset: 0x000AC37A
		// (set) Token: 0x06002A3B RID: 10811 RVA: 0x000AE18C File Offset: 0x000AC38C
		[XmlIgnore]
		[DataMember(Name = "FreeBusyType", EmitDefaultValue = false, Order = 8)]
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

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06002A3C RID: 10812 RVA: 0x000AE19F File Offset: 0x000AC39F
		// (set) Token: 0x06002A3D RID: 10813 RVA: 0x000AE1AC File Offset: 0x000AC3AC
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

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06002A3E RID: 10814 RVA: 0x000AE1AE File Offset: 0x000AC3AE
		// (set) Token: 0x06002A3F RID: 10815 RVA: 0x000AE1C0 File Offset: 0x000AC3C0
		[IgnoreDataMember]
		public string Location
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.Location);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.Location] = value;
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06002A40 RID: 10816 RVA: 0x000AE1D3 File Offset: 0x000AC3D3
		// (set) Token: 0x06002A41 RID: 10817 RVA: 0x000AE1E5 File Offset: 0x000AC3E5
		[DataMember(EmitDefaultValue = false, Order = 9)]
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

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06002A42 RID: 10818 RVA: 0x000AE1F8 File Offset: 0x000AC3F8
		// (set) Token: 0x06002A43 RID: 10819 RVA: 0x000AE20A File Offset: 0x000AC40A
		[DataMember(EmitDefaultValue = false, Order = 10)]
		public bool? IsMeeting
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(CalendarItemSchema.IsMeeting);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(CalendarItemSchema.IsMeeting, value);
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06002A44 RID: 10820 RVA: 0x000AE21D File Offset: 0x000AC41D
		// (set) Token: 0x06002A45 RID: 10821 RVA: 0x000AE22A File Offset: 0x000AC42A
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

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06002A46 RID: 10822 RVA: 0x000AE22C File Offset: 0x000AC42C
		// (set) Token: 0x06002A47 RID: 10823 RVA: 0x000AE23E File Offset: 0x000AC43E
		[DataMember(EmitDefaultValue = false, Order = 11)]
		public bool? IsCancelled
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(CalendarItemSchema.IsCancelled);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(CalendarItemSchema.IsCancelled, value);
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06002A48 RID: 10824 RVA: 0x000AE251 File Offset: 0x000AC451
		// (set) Token: 0x06002A49 RID: 10825 RVA: 0x000AE25E File Offset: 0x000AC45E
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

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06002A4A RID: 10826 RVA: 0x000AE260 File Offset: 0x000AC460
		// (set) Token: 0x06002A4B RID: 10827 RVA: 0x000AE272 File Offset: 0x000AC472
		[DataMember(EmitDefaultValue = false, Order = 12)]
		public bool? IsRecurring
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(CalendarItemSchema.IsRecurring);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(CalendarItemSchema.IsRecurring, value);
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06002A4C RID: 10828 RVA: 0x000AE285 File Offset: 0x000AC485
		// (set) Token: 0x06002A4D RID: 10829 RVA: 0x000AE292 File Offset: 0x000AC492
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

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06002A4E RID: 10830 RVA: 0x000AE294 File Offset: 0x000AC494
		// (set) Token: 0x06002A4F RID: 10831 RVA: 0x000AE2A6 File Offset: 0x000AC4A6
		[DataMember(EmitDefaultValue = false, Order = 13)]
		public bool? MeetingRequestWasSent
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(CalendarItemSchema.MeetingRequestWasSent);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(CalendarItemSchema.MeetingRequestWasSent, value);
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06002A50 RID: 10832 RVA: 0x000AE2B9 File Offset: 0x000AC4B9
		// (set) Token: 0x06002A51 RID: 10833 RVA: 0x000AE2C6 File Offset: 0x000AC4C6
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

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06002A52 RID: 10834 RVA: 0x000AE2C8 File Offset: 0x000AC4C8
		// (set) Token: 0x06002A53 RID: 10835 RVA: 0x000AE2F8 File Offset: 0x000AC4F8
		[DataMember(EmitDefaultValue = false, Order = 14)]
		public bool? IsResponseRequested
		{
			get
			{
				if (base.IsSet(CalendarItemSchema.OrganizerSpecific.IsResponseRequested))
				{
					return base.PropertyBag.GetNullableValue<bool>(CalendarItemSchema.OrganizerSpecific.IsResponseRequested);
				}
				return base.PropertyBag.GetNullableValue<bool>(CalendarItemSchema.AttendeeSpecific.IsResponseRequested);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(CalendarItemSchema.OrganizerSpecific.IsResponseRequested, value);
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06002A54 RID: 10836 RVA: 0x000AE30B File Offset: 0x000AC50B
		// (set) Token: 0x06002A55 RID: 10837 RVA: 0x000AE327 File Offset: 0x000AC527
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IsResponseRequestedSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.OrganizerSpecific.IsResponseRequested) || base.IsSet(CalendarItemSchema.AttendeeSpecific.IsResponseRequested);
			}
			set
			{
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06002A56 RID: 10838 RVA: 0x000AE329 File Offset: 0x000AC529
		// (set) Token: 0x06002A57 RID: 10839 RVA: 0x000AE340 File Offset: 0x000AC540
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

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06002A58 RID: 10840 RVA: 0x000AE34E File Offset: 0x000AC54E
		// (set) Token: 0x06002A59 RID: 10841 RVA: 0x000AE360 File Offset: 0x000AC560
		[XmlIgnore]
		[DataMember(Name = "CalendarItemType", EmitDefaultValue = false, Order = 15)]
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

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06002A5A RID: 10842 RVA: 0x000AE373 File Offset: 0x000AC573
		// (set) Token: 0x06002A5B RID: 10843 RVA: 0x000AE380 File Offset: 0x000AC580
		[XmlIgnore]
		[IgnoreDataMember]
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

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06002A5C RID: 10844 RVA: 0x000AE382 File Offset: 0x000AC582
		// (set) Token: 0x06002A5D RID: 10845 RVA: 0x000AE399 File Offset: 0x000AC599
		[IgnoreDataMember]
		[XmlElement]
		public ResponseTypeType MyResponseType
		{
			get
			{
				if (!this.MyResponseTypeSpecified)
				{
					return ResponseTypeType.Unknown;
				}
				return EnumUtilities.Parse<ResponseTypeType>(this.MyResponseTypeString);
			}
			set
			{
				this.MyResponseTypeString = EnumUtilities.ToString<ResponseTypeType>(value);
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06002A5E RID: 10846 RVA: 0x000AE3A7 File Offset: 0x000AC5A7
		// (set) Token: 0x06002A5F RID: 10847 RVA: 0x000AE3B9 File Offset: 0x000AC5B9
		[DataMember(Name = "ResponseType", EmitDefaultValue = false, Order = 16)]
		[XmlIgnore]
		public string MyResponseTypeString
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

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06002A60 RID: 10848 RVA: 0x000AE3CC File Offset: 0x000AC5CC
		// (set) Token: 0x06002A61 RID: 10849 RVA: 0x000AE3D9 File Offset: 0x000AC5D9
		[XmlIgnore]
		[IgnoreDataMember]
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

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06002A62 RID: 10850 RVA: 0x000AE3DB File Offset: 0x000AC5DB
		// (set) Token: 0x06002A63 RID: 10851 RVA: 0x000AE3ED File Offset: 0x000AC5ED
		[DataMember(EmitDefaultValue = false, Order = 17)]
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

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06002A64 RID: 10852 RVA: 0x000AE400 File Offset: 0x000AC600
		// (set) Token: 0x06002A65 RID: 10853 RVA: 0x000AE430 File Offset: 0x000AC630
		[DataMember(EmitDefaultValue = false, Order = 18)]
		[XmlArrayItem("Attendee", IsNullable = false)]
		public EwsAttendeeType[] RequiredAttendees
		{
			get
			{
				if (base.IsSet(CalendarItemSchema.OrganizerSpecific.RequiredAttendees))
				{
					return base.PropertyBag.GetValueOrDefault<EwsAttendeeType[]>(CalendarItemSchema.OrganizerSpecific.RequiredAttendees);
				}
				return base.PropertyBag.GetValueOrDefault<EwsAttendeeType[]>(CalendarItemSchema.AttendeeSpecific.RequiredAttendees);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.OrganizerSpecific.RequiredAttendees] = value;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06002A66 RID: 10854 RVA: 0x000AE443 File Offset: 0x000AC643
		// (set) Token: 0x06002A67 RID: 10855 RVA: 0x000AE473 File Offset: 0x000AC673
		[XmlArrayItem("Attendee", IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Order = 19)]
		public EwsAttendeeType[] OptionalAttendees
		{
			get
			{
				if (base.IsSet(CalendarItemSchema.OrganizerSpecific.OptionalAttendees))
				{
					return base.PropertyBag.GetValueOrDefault<EwsAttendeeType[]>(CalendarItemSchema.OrganizerSpecific.OptionalAttendees);
				}
				return base.PropertyBag.GetValueOrDefault<EwsAttendeeType[]>(CalendarItemSchema.AttendeeSpecific.OptionalAttendees);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.OrganizerSpecific.OptionalAttendees] = value;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06002A68 RID: 10856 RVA: 0x000AE486 File Offset: 0x000AC686
		// (set) Token: 0x06002A69 RID: 10857 RVA: 0x000AE4B6 File Offset: 0x000AC6B6
		[XmlArrayItem("Attendee", IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Order = 20)]
		public EwsAttendeeType[] Resources
		{
			get
			{
				if (base.IsSet(CalendarItemSchema.OrganizerSpecific.Resources))
				{
					return base.PropertyBag.GetValueOrDefault<EwsAttendeeType[]>(CalendarItemSchema.OrganizerSpecific.Resources);
				}
				return base.PropertyBag.GetValueOrDefault<EwsAttendeeType[]>(CalendarItemSchema.AttendeeSpecific.Resources);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.OrganizerSpecific.Resources] = value;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06002A6A RID: 10858 RVA: 0x000AE4C9 File Offset: 0x000AC6C9
		// (set) Token: 0x06002A6B RID: 10859 RVA: 0x000AE4DB File Offset: 0x000AC6DB
		[DataMember(EmitDefaultValue = false, Order = 21)]
		public int? ConflictingMeetingCount
		{
			get
			{
				return base.PropertyBag.GetNullableValue<int>(CalendarItemSchema.ConflictingMeetingCount);
			}
			set
			{
				base.PropertyBag.SetNullableValue<int>(CalendarItemSchema.ConflictingMeetingCount, value);
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06002A6C RID: 10860 RVA: 0x000AE4EE File Offset: 0x000AC6EE
		// (set) Token: 0x06002A6D RID: 10861 RVA: 0x000AE4FB File Offset: 0x000AC6FB
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

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06002A6E RID: 10862 RVA: 0x000AE4FD File Offset: 0x000AC6FD
		// (set) Token: 0x06002A6F RID: 10863 RVA: 0x000AE50F File Offset: 0x000AC70F
		[DataMember(EmitDefaultValue = false, Order = 22)]
		public int? AdjacentMeetingCount
		{
			get
			{
				return base.PropertyBag.GetNullableValue<int>(CalendarItemSchema.AdjacentMeetingCount);
			}
			set
			{
				base.PropertyBag.SetNullableValue<int>(CalendarItemSchema.AdjacentMeetingCount, value);
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06002A70 RID: 10864 RVA: 0x000AE522 File Offset: 0x000AC722
		// (set) Token: 0x06002A71 RID: 10865 RVA: 0x000AE52F File Offset: 0x000AC72F
		[IgnoreDataMember]
		[XmlIgnore]
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

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06002A72 RID: 10866 RVA: 0x000AE531 File Offset: 0x000AC731
		// (set) Token: 0x06002A73 RID: 10867 RVA: 0x000AE543 File Offset: 0x000AC743
		[DataMember(EmitDefaultValue = false, Order = 23)]
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

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06002A74 RID: 10868 RVA: 0x000AE556 File Offset: 0x000AC756
		// (set) Token: 0x06002A75 RID: 10869 RVA: 0x000AE568 File Offset: 0x000AC768
		[DataMember(EmitDefaultValue = false, Order = 24)]
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

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06002A76 RID: 10870 RVA: 0x000AE57B File Offset: 0x000AC77B
		// (set) Token: 0x06002A77 RID: 10871 RVA: 0x000AE58D File Offset: 0x000AC78D
		[DataMember(EmitDefaultValue = false, Order = 25)]
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

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06002A78 RID: 10872 RVA: 0x000AE5A0 File Offset: 0x000AC7A0
		// (set) Token: 0x06002A79 RID: 10873 RVA: 0x000AE5B2 File Offset: 0x000AC7B2
		[DataMember(EmitDefaultValue = false, Order = 26)]
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

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06002A7A RID: 10874 RVA: 0x000AE5C5 File Offset: 0x000AC7C5
		// (set) Token: 0x06002A7B RID: 10875 RVA: 0x000AE5D7 File Offset: 0x000AC7D7
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, Order = 27)]
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

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06002A7C RID: 10876 RVA: 0x000AE5EA File Offset: 0x000AC7EA
		// (set) Token: 0x06002A7D RID: 10877 RVA: 0x000AE5F7 File Offset: 0x000AC7F7
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

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06002A7E RID: 10878 RVA: 0x000AE5F9 File Offset: 0x000AC7F9
		// (set) Token: 0x06002A7F RID: 10879 RVA: 0x000AE60B File Offset: 0x000AC80B
		[DataMember(EmitDefaultValue = false, Order = 28)]
		public int? AppointmentSequenceNumber
		{
			get
			{
				return base.PropertyBag.GetNullableValue<int>(CalendarItemSchema.AppointmentSequenceNumber);
			}
			set
			{
				base.PropertyBag.SetNullableValue<int>(CalendarItemSchema.AppointmentSequenceNumber, value);
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06002A80 RID: 10880 RVA: 0x000AE61E File Offset: 0x000AC81E
		// (set) Token: 0x06002A81 RID: 10881 RVA: 0x000AE62B File Offset: 0x000AC82B
		[XmlIgnore]
		[IgnoreDataMember]
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

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06002A82 RID: 10882 RVA: 0x000AE62D File Offset: 0x000AC82D
		// (set) Token: 0x06002A83 RID: 10883 RVA: 0x000AE63F File Offset: 0x000AC83F
		[DataMember(EmitDefaultValue = false, Order = 29)]
		public int? AppointmentState
		{
			get
			{
				return base.PropertyBag.GetNullableValue<int>(CalendarItemSchema.AppointmentState);
			}
			set
			{
				base.PropertyBag.SetNullableValue<int>(CalendarItemSchema.AppointmentState, value);
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06002A84 RID: 10884 RVA: 0x000AE652 File Offset: 0x000AC852
		// (set) Token: 0x06002A85 RID: 10885 RVA: 0x000AE65F File Offset: 0x000AC85F
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

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06002A86 RID: 10886 RVA: 0x000AE661 File Offset: 0x000AC861
		// (set) Token: 0x06002A87 RID: 10887 RVA: 0x000AE691 File Offset: 0x000AC891
		[DataMember(EmitDefaultValue = false, Order = 30)]
		public RecurrenceType Recurrence
		{
			get
			{
				if (base.IsSet(CalendarItemSchema.OrganizerSpecific.Recurrence))
				{
					return base.PropertyBag.GetValueOrDefault<RecurrenceType>(CalendarItemSchema.OrganizerSpecific.Recurrence);
				}
				return base.PropertyBag.GetValueOrDefault<RecurrenceType>(CalendarItemSchema.AttendeeSpecific.Recurrence);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.OrganizerSpecific.Recurrence] = value;
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06002A88 RID: 10888 RVA: 0x000AE6A4 File Offset: 0x000AC8A4
		// (set) Token: 0x06002A89 RID: 10889 RVA: 0x000AE6B6 File Offset: 0x000AC8B6
		[DataMember(EmitDefaultValue = false, Order = 31)]
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

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06002A8A RID: 10890 RVA: 0x000AE6C9 File Offset: 0x000AC8C9
		// (set) Token: 0x06002A8B RID: 10891 RVA: 0x000AE6DB File Offset: 0x000AC8DB
		[DataMember(EmitDefaultValue = false, Order = 32)]
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

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06002A8C RID: 10892 RVA: 0x000AE6EE File Offset: 0x000AC8EE
		// (set) Token: 0x06002A8D RID: 10893 RVA: 0x000AE700 File Offset: 0x000AC900
		[XmlArrayItem("Occurrence", IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Order = 33)]
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

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06002A8E RID: 10894 RVA: 0x000AE713 File Offset: 0x000AC913
		// (set) Token: 0x06002A8F RID: 10895 RVA: 0x000AE725 File Offset: 0x000AC925
		[XmlArrayItem("DeletedOccurrence", IsNullable = false)]
		[DataMember(EmitDefaultValue = false, Order = 34)]
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

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06002A90 RID: 10896 RVA: 0x000AE738 File Offset: 0x000AC938
		// (set) Token: 0x06002A91 RID: 10897 RVA: 0x000AE74A File Offset: 0x000AC94A
		[DataMember(EmitDefaultValue = false, Order = 35)]
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

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06002A92 RID: 10898 RVA: 0x000AE75D File Offset: 0x000AC95D
		// (set) Token: 0x06002A93 RID: 10899 RVA: 0x000AE78D File Offset: 0x000AC98D
		[DataMember(EmitDefaultValue = false, Order = 36)]
		public TimeZoneDefinitionType StartTimeZone
		{
			get
			{
				if (base.IsSet(CalendarItemSchema.OrganizerSpecific.StartTimeZone))
				{
					return base.PropertyBag.GetValueOrDefault<TimeZoneDefinitionType>(CalendarItemSchema.OrganizerSpecific.StartTimeZone);
				}
				return base.PropertyBag.GetValueOrDefault<TimeZoneDefinitionType>(CalendarItemSchema.AttendeeSpecific.StartTimeZone);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.OrganizerSpecific.StartTimeZone] = value;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06002A94 RID: 10900 RVA: 0x000AE7A0 File Offset: 0x000AC9A0
		// (set) Token: 0x06002A95 RID: 10901 RVA: 0x000AE7D0 File Offset: 0x000AC9D0
		[DataMember(EmitDefaultValue = false, Order = 37)]
		public TimeZoneDefinitionType EndTimeZone
		{
			get
			{
				if (base.IsSet(CalendarItemSchema.OrganizerSpecific.EndTimeZone))
				{
					return base.PropertyBag.GetValueOrDefault<TimeZoneDefinitionType>(CalendarItemSchema.OrganizerSpecific.EndTimeZone);
				}
				return base.PropertyBag.GetValueOrDefault<TimeZoneDefinitionType>(CalendarItemSchema.AttendeeSpecific.EndTimeZone);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.OrganizerSpecific.EndTimeZone] = value;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06002A96 RID: 10902 RVA: 0x000AE7E3 File Offset: 0x000AC9E3
		// (set) Token: 0x06002A97 RID: 10903 RVA: 0x000AE813 File Offset: 0x000ACA13
		[DataMember(EmitDefaultValue = false, Order = 38)]
		public int? ConferenceType
		{
			get
			{
				if (base.IsSet(CalendarItemSchema.OrganizerSpecific.ConferenceType))
				{
					return base.PropertyBag.GetNullableValue<int>(CalendarItemSchema.OrganizerSpecific.ConferenceType);
				}
				return base.PropertyBag.GetNullableValue<int>(CalendarItemSchema.AttendeeSpecific.ConferenceType);
			}
			set
			{
				base.PropertyBag.SetNullableValue<int>(CalendarItemSchema.OrganizerSpecific.ConferenceType, value);
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06002A98 RID: 10904 RVA: 0x000AE826 File Offset: 0x000ACA26
		// (set) Token: 0x06002A99 RID: 10905 RVA: 0x000AE842 File Offset: 0x000ACA42
		[XmlIgnore]
		[IgnoreDataMember]
		public bool ConferenceTypeSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.OrganizerSpecific.ConferenceType) || base.IsSet(CalendarItemSchema.AttendeeSpecific.ConferenceType);
			}
			set
			{
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06002A9A RID: 10906 RVA: 0x000AE844 File Offset: 0x000ACA44
		// (set) Token: 0x06002A9B RID: 10907 RVA: 0x000AE874 File Offset: 0x000ACA74
		[DataMember(EmitDefaultValue = false, Order = 39)]
		public bool? AllowNewTimeProposal
		{
			get
			{
				if (base.IsSet(CalendarItemSchema.OrganizerSpecific.AllowNewTimeProposal))
				{
					return base.PropertyBag.GetNullableValue<bool>(CalendarItemSchema.OrganizerSpecific.AllowNewTimeProposal);
				}
				return base.PropertyBag.GetNullableValue<bool>(CalendarItemSchema.AttendeeSpecific.AllowNewTimeProposal);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(CalendarItemSchema.OrganizerSpecific.AllowNewTimeProposal, value);
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06002A9C RID: 10908 RVA: 0x000AE887 File Offset: 0x000ACA87
		// (set) Token: 0x06002A9D RID: 10909 RVA: 0x000AE8A3 File Offset: 0x000ACAA3
		[IgnoreDataMember]
		[XmlIgnore]
		public bool AllowNewTimeProposalSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.OrganizerSpecific.AllowNewTimeProposal) || base.IsSet(CalendarItemSchema.AttendeeSpecific.AllowNewTimeProposal);
			}
			set
			{
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06002A9E RID: 10910 RVA: 0x000AE8A5 File Offset: 0x000ACAA5
		// (set) Token: 0x06002A9F RID: 10911 RVA: 0x000AE8D5 File Offset: 0x000ACAD5
		[DataMember(EmitDefaultValue = false, Order = 40)]
		public bool? IsOnlineMeeting
		{
			get
			{
				if (base.IsSet(CalendarItemSchema.OrganizerSpecific.IsOnlineMeeting))
				{
					return base.PropertyBag.GetNullableValue<bool>(CalendarItemSchema.OrganizerSpecific.IsOnlineMeeting);
				}
				return base.PropertyBag.GetNullableValue<bool>(CalendarItemSchema.AttendeeSpecific.IsOnlineMeeting);
			}
			set
			{
				base.PropertyBag.SetNullableValue<bool>(CalendarItemSchema.OrganizerSpecific.IsOnlineMeeting, value);
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06002AA0 RID: 10912 RVA: 0x000AE8E8 File Offset: 0x000ACAE8
		// (set) Token: 0x06002AA1 RID: 10913 RVA: 0x000AE904 File Offset: 0x000ACB04
		[IgnoreDataMember]
		[XmlIgnore]
		public bool IsOnlineMeetingSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.OrganizerSpecific.IsOnlineMeeting) || base.IsSet(CalendarItemSchema.AttendeeSpecific.IsOnlineMeeting);
			}
			set
			{
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06002AA2 RID: 10914 RVA: 0x000AE906 File Offset: 0x000ACB06
		// (set) Token: 0x06002AA3 RID: 10915 RVA: 0x000AE936 File Offset: 0x000ACB36
		[DataMember(EmitDefaultValue = false, Order = 41)]
		public string MeetingWorkspaceUrl
		{
			get
			{
				if (base.IsSet(CalendarItemSchema.OrganizerSpecific.MeetingWorkspaceUrl))
				{
					return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.OrganizerSpecific.MeetingWorkspaceUrl);
				}
				return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.AttendeeSpecific.MeetingWorkspaceUrl);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.OrganizerSpecific.MeetingWorkspaceUrl] = value;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06002AA4 RID: 10916 RVA: 0x000AE949 File Offset: 0x000ACB49
		// (set) Token: 0x06002AA5 RID: 10917 RVA: 0x000AE979 File Offset: 0x000ACB79
		[DataMember(EmitDefaultValue = false, Order = 42)]
		public string NetShowUrl
		{
			get
			{
				if (base.IsSet(CalendarItemSchema.OrganizerSpecific.NetShowUrl))
				{
					return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.OrganizerSpecific.NetShowUrl);
				}
				return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.AttendeeSpecific.NetShowUrl);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.OrganizerSpecific.NetShowUrl] = value;
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06002AA6 RID: 10918 RVA: 0x000AE98C File Offset: 0x000ACB8C
		// (set) Token: 0x06002AA7 RID: 10919 RVA: 0x000AE99E File Offset: 0x000ACB9E
		[DataMember(Name = "Location", EmitDefaultValue = false, Order = 43)]
		public EnhancedLocationType EnhancedLocation
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<EnhancedLocationType>(CalendarItemSchema.EnhancedLocation);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.EnhancedLocation] = value;
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06002AA8 RID: 10920 RVA: 0x000AE9B1 File Offset: 0x000ACBB1
		// (set) Token: 0x06002AA9 RID: 10921 RVA: 0x000AE9C3 File Offset: 0x000ACBC3
		[DataMember(EmitDefaultValue = false, Order = 44)]
		[DateTimeString]
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

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06002AAA RID: 10922 RVA: 0x000AE9D6 File Offset: 0x000ACBD6
		// (set) Token: 0x06002AAB RID: 10923 RVA: 0x000AE9E8 File Offset: 0x000ACBE8
		[DataMember(EmitDefaultValue = false, Order = 45)]
		[DateTimeString]
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

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06002AAC RID: 10924 RVA: 0x000AE9FB File Offset: 0x000ACBFB
		// (set) Token: 0x06002AAD RID: 10925 RVA: 0x000AEA0D File Offset: 0x000ACC0D
		[DataMember(EmitDefaultValue = false, Order = 46)]
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

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06002AAE RID: 10926 RVA: 0x000AEA20 File Offset: 0x000ACC20
		// (set) Token: 0x06002AAF RID: 10927 RVA: 0x000AEA32 File Offset: 0x000ACC32
		[DataMember(EmitDefaultValue = false, Order = 47)]
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

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06002AB0 RID: 10928 RVA: 0x000AEA45 File Offset: 0x000ACC45
		// (set) Token: 0x06002AB1 RID: 10929 RVA: 0x000AEA5C File Offset: 0x000ACC5C
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

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06002AB2 RID: 10930 RVA: 0x000AEA6A File Offset: 0x000ACC6A
		// (set) Token: 0x06002AB3 RID: 10931 RVA: 0x000AEA7C File Offset: 0x000ACC7C
		[DataMember(Name = "IntendedFreeBusyStatus", EmitDefaultValue = false, Order = 48)]
		[XmlIgnore]
		public string IntendedFreeBusyStatusString
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.IntendedFreeBusyStatus);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.IntendedFreeBusyStatus] = value;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06002AB4 RID: 10932 RVA: 0x000AEA8F File Offset: 0x000ACC8F
		// (set) Token: 0x06002AB5 RID: 10933 RVA: 0x000AEAA1 File Offset: 0x000ACCA1
		[DataMember(EmitDefaultValue = false, Order = 49)]
		public string JoinOnlineMeetingUrl
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.JoinOnlineMeetingUrl);
			}
			set
			{
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06002AB6 RID: 10934 RVA: 0x000AEAA3 File Offset: 0x000ACCA3
		// (set) Token: 0x06002AB7 RID: 10935 RVA: 0x000AEAB5 File Offset: 0x000ACCB5
		[DataMember(EmitDefaultValue = false, Order = 50)]
		public OnlineMeetingSettingsType OnlineMeetingSettings
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<OnlineMeetingSettingsType>(CalendarItemSchema.OnlineMeetingSettings);
			}
			set
			{
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06002AB8 RID: 10936 RVA: 0x000AEAB7 File Offset: 0x000ACCB7
		// (set) Token: 0x06002AB9 RID: 10937 RVA: 0x000AEAC9 File Offset: 0x000ACCC9
		[DataMember(EmitDefaultValue = false, Order = 51)]
		public bool? IsOrganizer
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(CalendarItemSchema.IsOrganizer);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.IsOrganizer] = value;
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06002ABA RID: 10938 RVA: 0x000AEAE1 File Offset: 0x000ACCE1
		// (set) Token: 0x06002ABB RID: 10939 RVA: 0x000AEAEE File Offset: 0x000ACCEE
		[IgnoreDataMember]
		[XmlIgnore]
		public bool IsOrganizerSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.IsOrganizer);
			}
			set
			{
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06002ABC RID: 10940 RVA: 0x000AEAF0 File Offset: 0x000ACCF0
		// (set) Token: 0x06002ABD RID: 10941 RVA: 0x000AEAFD File Offset: 0x000ACCFD
		[IgnoreDataMember]
		[XmlIgnore]
		public bool IntendedFreeBusyStatusSpecified
		{
			get
			{
				return base.IsSet(CalendarItemSchema.IntendedFreeBusyStatus);
			}
			set
			{
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06002ABE RID: 10942 RVA: 0x000AEAFF File Offset: 0x000ACCFF
		internal override StoreObjectType StoreObjectType
		{
			get
			{
				return StoreObjectType.CalendarItem;
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06002ABF RID: 10943 RVA: 0x000AEB03 File Offset: 0x000ACD03
		// (set) Token: 0x06002AC0 RID: 10944 RVA: 0x000AEB15 File Offset: 0x000ACD15
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, Order = 52)]
		public string AppointmentReplyName
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(CalendarItemSchema.AppointmentReplyName);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.AppointmentReplyName] = value;
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06002AC1 RID: 10945 RVA: 0x000AEB28 File Offset: 0x000ACD28
		// (set) Token: 0x06002AC2 RID: 10946 RVA: 0x000AEB3A File Offset: 0x000ACD3A
		[DataMember(EmitDefaultValue = false, Order = 53)]
		[XmlIgnore]
		public bool? IsSeriesCancelled
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(CalendarItemSchema.IsSeriesCancelled);
			}
			set
			{
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06002AC3 RID: 10947 RVA: 0x000AEB3C File Offset: 0x000ACD3C
		// (set) Token: 0x06002AC4 RID: 10948 RVA: 0x000AEB4E File Offset: 0x000ACD4E
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, Order = 54)]
		public InboxReminderType[] InboxReminders
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<InboxReminderType[]>(CalendarItemSchema.InboxReminders);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.InboxReminders] = value;
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06002AC5 RID: 10949 RVA: 0x000AEB61 File Offset: 0x000ACD61
		// (set) Token: 0x06002AC6 RID: 10950 RVA: 0x000AEB91 File Offset: 0x000ACD91
		[XmlIgnore]
		[DataMember(EmitDefaultValue = false, Order = 55)]
		public AttendeeCountsType AttendeeCounts
		{
			get
			{
				if (base.IsSet(CalendarItemSchema.OrganizerSpecific.AttendeeCounts))
				{
					return base.PropertyBag.GetValueOrDefault<AttendeeCountsType>(CalendarItemSchema.OrganizerSpecific.AttendeeCounts);
				}
				return base.PropertyBag.GetValueOrDefault<AttendeeCountsType>(CalendarItemSchema.AttendeeSpecific.AttendeeCounts);
			}
			set
			{
				base.PropertyBag[CalendarItemSchema.OrganizerSpecific.AttendeeCounts] = value;
			}
		}
	}
}
