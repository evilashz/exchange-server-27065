using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005ED RID: 1517
	[KnownType(typeof(MeetingCancellationMessageType))]
	[XmlInclude(typeof(MeetingCancellationMessageType))]
	[XmlInclude(typeof(MeetingRequestMessageType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(MeetingResponseMessageType))]
	[KnownType(typeof(MeetingResponseMessageType))]
	[KnownType(typeof(MeetingRequestMessageType))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "MeetingMessage")]
	[Serializable]
	public class MeetingMessageType : MessageType
	{
		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06002E09 RID: 11785 RVA: 0x000B2E76 File Offset: 0x000B1076
		// (set) Token: 0x06002E0A RID: 11786 RVA: 0x000B2E83 File Offset: 0x000B1083
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public ItemId AssociatedCalendarItemId
		{
			get
			{
				return base.GetValueOrDefault<ItemId>(MeetingMessageSchema.AssociatedCalendarItemId);
			}
			set
			{
				this[MeetingMessageSchema.AssociatedCalendarItemId] = value;
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06002E0B RID: 11787 RVA: 0x000B2E91 File Offset: 0x000B1091
		// (set) Token: 0x06002E0C RID: 11788 RVA: 0x000B2E9E File Offset: 0x000B109E
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public bool IsDelegated
		{
			get
			{
				return base.GetValueOrDefault<bool>(MeetingMessageSchema.IsDelegated);
			}
			set
			{
				this[MeetingMessageSchema.IsDelegated] = value;
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06002E0D RID: 11789 RVA: 0x000B2EB1 File Offset: 0x000B10B1
		// (set) Token: 0x06002E0E RID: 11790 RVA: 0x000B2EBE File Offset: 0x000B10BE
		[IgnoreDataMember]
		[XmlIgnore]
		public bool IsDelegatedSpecified
		{
			get
			{
				return base.IsSet(MeetingMessageSchema.IsDelegated);
			}
			set
			{
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06002E0F RID: 11791 RVA: 0x000B2EC0 File Offset: 0x000B10C0
		// (set) Token: 0x06002E10 RID: 11792 RVA: 0x000B2ECD File Offset: 0x000B10CD
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public bool IsOutOfDate
		{
			get
			{
				return base.GetValueOrDefault<bool>(MeetingMessageSchema.IsOutOfDate);
			}
			set
			{
				this[MeetingMessageSchema.IsOutOfDate] = value;
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06002E11 RID: 11793 RVA: 0x000B2EE0 File Offset: 0x000B10E0
		// (set) Token: 0x06002E12 RID: 11794 RVA: 0x000B2EED File Offset: 0x000B10ED
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IsOutOfDateSpecified
		{
			get
			{
				return base.IsSet(MeetingMessageSchema.IsOutOfDate);
			}
			set
			{
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06002E13 RID: 11795 RVA: 0x000B2EEF File Offset: 0x000B10EF
		// (set) Token: 0x06002E14 RID: 11796 RVA: 0x000B2EFC File Offset: 0x000B10FC
		[DataMember(EmitDefaultValue = false, Order = 4)]
		public bool HasBeenProcessed
		{
			get
			{
				return base.GetValueOrDefault<bool>(MeetingMessageSchema.HasBeenProcessed);
			}
			set
			{
				this[MeetingMessageSchema.HasBeenProcessed] = value;
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06002E15 RID: 11797 RVA: 0x000B2F0F File Offset: 0x000B110F
		// (set) Token: 0x06002E16 RID: 11798 RVA: 0x000B2F1C File Offset: 0x000B111C
		[IgnoreDataMember]
		[XmlIgnore]
		public bool HasBeenProcessedSpecified
		{
			get
			{
				return base.IsSet(MeetingMessageSchema.HasBeenProcessed);
			}
			set
			{
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06002E17 RID: 11799 RVA: 0x000B2F1E File Offset: 0x000B111E
		// (set) Token: 0x06002E18 RID: 11800 RVA: 0x000B2F35 File Offset: 0x000B1135
		[XmlElement]
		[IgnoreDataMember]
		public ResponseTypeType ResponseType
		{
			get
			{
				if (!this.ResponseTypeSpecified)
				{
					return ResponseTypeType.Unknown;
				}
				return EnumUtilities.Parse<ResponseTypeType>(this.ResponseTypeString);
			}
			set
			{
				this.ResponseTypeString = EnumUtilities.ToString<ResponseTypeType>(value);
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06002E19 RID: 11801 RVA: 0x000B2F43 File Offset: 0x000B1143
		// (set) Token: 0x06002E1A RID: 11802 RVA: 0x000B2F50 File Offset: 0x000B1150
		[XmlIgnore]
		[DataMember(Name = "ResponseType", EmitDefaultValue = false, Order = 5)]
		public string ResponseTypeString
		{
			get
			{
				return base.GetValueOrDefault<string>(MeetingMessageSchema.ResponseType);
			}
			set
			{
				this[MeetingMessageSchema.ResponseType] = value;
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06002E1B RID: 11803 RVA: 0x000B2F5E File Offset: 0x000B115E
		// (set) Token: 0x06002E1C RID: 11804 RVA: 0x000B2F6B File Offset: 0x000B116B
		[XmlIgnore]
		[IgnoreDataMember]
		public bool ResponseTypeSpecified
		{
			get
			{
				return base.IsSet(MeetingMessageSchema.ResponseType);
			}
			set
			{
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06002E1D RID: 11805 RVA: 0x000B2F6D File Offset: 0x000B116D
		// (set) Token: 0x06002E1E RID: 11806 RVA: 0x000B2F7A File Offset: 0x000B117A
		[DataMember(EmitDefaultValue = false, Order = 6)]
		public string UID
		{
			get
			{
				return base.GetValueOrDefault<string>(MeetingMessageSchema.ICalendarUid);
			}
			set
			{
				this[MeetingMessageSchema.ICalendarUid] = value;
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06002E1F RID: 11807 RVA: 0x000B2F88 File Offset: 0x000B1188
		// (set) Token: 0x06002E20 RID: 11808 RVA: 0x000B2F95 File Offset: 0x000B1195
		[DataMember(EmitDefaultValue = false, Order = 7)]
		public string RecurrenceId
		{
			get
			{
				return base.GetValueOrDefault<string>(CalendarItemSchema.ICalendarRecurrenceId);
			}
			set
			{
				this[CalendarItemSchema.ICalendarRecurrenceId] = value;
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06002E21 RID: 11809 RVA: 0x000B2FA3 File Offset: 0x000B11A3
		// (set) Token: 0x06002E22 RID: 11810 RVA: 0x000B2FB0 File Offset: 0x000B11B0
		[XmlIgnore]
		[IgnoreDataMember]
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

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06002E23 RID: 11811 RVA: 0x000B2FB2 File Offset: 0x000B11B2
		// (set) Token: 0x06002E24 RID: 11812 RVA: 0x000B2FBF File Offset: 0x000B11BF
		[DataMember(EmitDefaultValue = false, Order = 8)]
		[DateTimeString]
		public string DateTimeStamp
		{
			get
			{
				return base.GetValueOrDefault<string>(CalendarItemSchema.ICalendarDateTimeStamp);
			}
			set
			{
				this[CalendarItemSchema.ICalendarDateTimeStamp] = value;
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06002E25 RID: 11813 RVA: 0x000B2FCD File Offset: 0x000B11CD
		// (set) Token: 0x06002E26 RID: 11814 RVA: 0x000B2FDA File Offset: 0x000B11DA
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

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06002E27 RID: 11815 RVA: 0x000B2FDC File Offset: 0x000B11DC
		// (set) Token: 0x06002E28 RID: 11816 RVA: 0x000B2FEE File Offset: 0x000B11EE
		[DataMember(EmitDefaultValue = false, Order = 9)]
		public bool? IsOrganizer
		{
			get
			{
				return base.PropertyBag.GetNullableValue<bool>(CalendarItemSchema.IsOrganizer);
			}
			set
			{
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06002E29 RID: 11817 RVA: 0x000B2FF0 File Offset: 0x000B11F0
		// (set) Token: 0x06002E2A RID: 11818 RVA: 0x000B2FFD File Offset: 0x000B11FD
		[XmlIgnore]
		[IgnoreDataMember]
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

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06002E2B RID: 11819 RVA: 0x000B2FFF File Offset: 0x000B11FF
		internal override StoreObjectType StoreObjectType
		{
			get
			{
				return StoreObjectType.MeetingMessage;
			}
		}
	}
}
