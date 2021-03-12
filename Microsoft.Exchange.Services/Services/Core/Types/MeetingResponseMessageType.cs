using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005EE RID: 1518
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class MeetingResponseMessageType : MeetingMessageType
	{
		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06002E2D RID: 11821 RVA: 0x000B300B File Offset: 0x000B120B
		internal override StoreObjectType StoreObjectType
		{
			get
			{
				return StoreObjectType.MeetingResponse;
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06002E2E RID: 11822 RVA: 0x000B300F File Offset: 0x000B120F
		// (set) Token: 0x06002E2F RID: 11823 RVA: 0x000B301C File Offset: 0x000B121C
		[DataMember(EmitDefaultValue = false, Order = 1)]
		[DateTimeString]
		public string Start
		{
			get
			{
				return base.GetValueOrDefault<string>(MeetingResponseSchema.Start);
			}
			set
			{
				this[MeetingResponseSchema.Start] = value;
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06002E30 RID: 11824 RVA: 0x000B302A File Offset: 0x000B122A
		// (set) Token: 0x06002E31 RID: 11825 RVA: 0x000B3037 File Offset: 0x000B1237
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public string End
		{
			get
			{
				return base.GetValueOrDefault<string>(MeetingResponseSchema.End);
			}
			set
			{
				this[MeetingResponseSchema.End] = value;
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06002E32 RID: 11826 RVA: 0x000B3045 File Offset: 0x000B1245
		// (set) Token: 0x06002E33 RID: 11827 RVA: 0x000B3057 File Offset: 0x000B1257
		[IgnoreDataMember]
		public string Location
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(MeetingResponseSchema.Location);
			}
			set
			{
				base.PropertyBag[MeetingResponseSchema.Location] = value;
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06002E34 RID: 11828 RVA: 0x000B306A File Offset: 0x000B126A
		// (set) Token: 0x06002E35 RID: 11829 RVA: 0x000B307C File Offset: 0x000B127C
		[DataMember(EmitDefaultValue = false, Order = 3)]
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

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06002E36 RID: 11830 RVA: 0x000B308F File Offset: 0x000B128F
		// (set) Token: 0x06002E37 RID: 11831 RVA: 0x000B30A6 File Offset: 0x000B12A6
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

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06002E38 RID: 11832 RVA: 0x000B30B4 File Offset: 0x000B12B4
		// (set) Token: 0x06002E39 RID: 11833 RVA: 0x000B30C6 File Offset: 0x000B12C6
		[DataMember(Name = "CalendarItemType", EmitDefaultValue = false, Order = 4)]
		[XmlIgnore]
		public string CalendarItemTypeString
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(MeetingResponseSchema.CalendarItemType);
			}
			set
			{
				base.PropertyBag[MeetingResponseSchema.CalendarItemType] = value;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06002E3A RID: 11834 RVA: 0x000B30D9 File Offset: 0x000B12D9
		// (set) Token: 0x06002E3B RID: 11835 RVA: 0x000B30E6 File Offset: 0x000B12E6
		[IgnoreDataMember]
		[XmlIgnore]
		public bool CalendarItemTypeSpecified
		{
			get
			{
				return base.IsSet(MeetingResponseSchema.CalendarItemType);
			}
			set
			{
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06002E3C RID: 11836 RVA: 0x000B30E8 File Offset: 0x000B12E8
		// (set) Token: 0x06002E3D RID: 11837 RVA: 0x000B30F5 File Offset: 0x000B12F5
		[DataMember(EmitDefaultValue = false, Order = 5)]
		[DateTimeString]
		public string ProposedStart
		{
			get
			{
				return base.GetValueOrDefault<string>(MeetingResponseSchema.ProposedStart);
			}
			set
			{
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06002E3E RID: 11838 RVA: 0x000B30F7 File Offset: 0x000B12F7
		// (set) Token: 0x06002E3F RID: 11839 RVA: 0x000B3104 File Offset: 0x000B1304
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, Order = 6)]
		public string ProposedEnd
		{
			get
			{
				return base.GetValueOrDefault<string>(MeetingResponseSchema.ProposedEnd);
			}
			set
			{
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06002E40 RID: 11840 RVA: 0x000B3106 File Offset: 0x000B1306
		// (set) Token: 0x06002E41 RID: 11841 RVA: 0x000B3125 File Offset: 0x000B1325
		[XmlIgnore]
		[IgnoreDataMember]
		public bool IsNewTimeProposal
		{
			get
			{
				return !string.IsNullOrEmpty(this.ProposedStart) || !string.IsNullOrEmpty(this.ProposedEnd);
			}
			set
			{
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06002E42 RID: 11842 RVA: 0x000B3127 File Offset: 0x000B1327
		// (set) Token: 0x06002E43 RID: 11843 RVA: 0x000B3139 File Offset: 0x000B1339
		[DataMember(Name = "Location", EmitDefaultValue = false, Order = 7)]
		public EnhancedLocationType EnhancedLocation
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<EnhancedLocationType>(MeetingResponseSchema.EnhancedLocation);
			}
			set
			{
				base.PropertyBag[MeetingResponseSchema.EnhancedLocation] = value;
			}
		}
	}
}
