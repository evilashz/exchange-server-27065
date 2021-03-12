using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005F0 RID: 1520
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class MeetingCancellationMessageType : MeetingMessageType
	{
		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06002ED9 RID: 11993 RVA: 0x000B3A1F File Offset: 0x000B1C1F
		internal override StoreObjectType StoreObjectType
		{
			get
			{
				return StoreObjectType.MeetingCancellation;
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06002EDA RID: 11994 RVA: 0x000B3A23 File Offset: 0x000B1C23
		// (set) Token: 0x06002EDB RID: 11995 RVA: 0x000B3A30 File Offset: 0x000B1C30
		[DateTimeString]
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public string Start
		{
			get
			{
				return base.GetValueOrDefault<string>(MeetingCancellationSchema.Start);
			}
			set
			{
				this[MeetingCancellationSchema.Start] = value;
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06002EDC RID: 11996 RVA: 0x000B3A3E File Offset: 0x000B1C3E
		// (set) Token: 0x06002EDD RID: 11997 RVA: 0x000B3A4B File Offset: 0x000B1C4B
		[DataMember(EmitDefaultValue = false, Order = 2)]
		[DateTimeString]
		public string End
		{
			get
			{
				return base.GetValueOrDefault<string>(MeetingCancellationSchema.End);
			}
			set
			{
				this[MeetingCancellationSchema.End] = value;
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06002EDE RID: 11998 RVA: 0x000B3A59 File Offset: 0x000B1C59
		// (set) Token: 0x06002EDF RID: 11999 RVA: 0x000B3A6B File Offset: 0x000B1C6B
		[IgnoreDataMember]
		public string Location
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(MeetingCancellationSchema.Location);
			}
			set
			{
				base.PropertyBag[MeetingCancellationSchema.Location] = value;
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06002EE0 RID: 12000 RVA: 0x000B3A7E File Offset: 0x000B1C7E
		// (set) Token: 0x06002EE1 RID: 12001 RVA: 0x000B3A90 File Offset: 0x000B1C90
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

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06002EE2 RID: 12002 RVA: 0x000B3AA3 File Offset: 0x000B1CA3
		// (set) Token: 0x06002EE3 RID: 12003 RVA: 0x000B3ABA File Offset: 0x000B1CBA
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

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06002EE4 RID: 12004 RVA: 0x000B3AC8 File Offset: 0x000B1CC8
		// (set) Token: 0x06002EE5 RID: 12005 RVA: 0x000B3ADA File Offset: 0x000B1CDA
		[XmlIgnore]
		[DataMember(Name = "CalendarItemType", EmitDefaultValue = false, Order = 4)]
		public string CalendarItemTypeString
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<string>(MeetingCancellationSchema.CalendarItemType);
			}
			set
			{
				base.PropertyBag[MeetingCancellationSchema.CalendarItemType] = value;
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06002EE6 RID: 12006 RVA: 0x000B3AED File Offset: 0x000B1CED
		// (set) Token: 0x06002EE7 RID: 12007 RVA: 0x000B3AFA File Offset: 0x000B1CFA
		[XmlIgnore]
		[IgnoreDataMember]
		public bool CalendarItemTypeSpecified
		{
			get
			{
				return base.IsSet(MeetingCancellationSchema.CalendarItemType);
			}
			set
			{
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06002EE8 RID: 12008 RVA: 0x000B3AFC File Offset: 0x000B1CFC
		// (set) Token: 0x06002EE9 RID: 12009 RVA: 0x000B3B0E File Offset: 0x000B1D0E
		[DataMember(Name = "Location", EmitDefaultValue = false, Order = 5)]
		public EnhancedLocationType EnhancedLocation
		{
			get
			{
				return base.PropertyBag.GetValueOrDefault<EnhancedLocationType>(MeetingCancellationSchema.EnhancedLocation);
			}
			set
			{
				base.PropertyBag[MeetingCancellationSchema.EnhancedLocation] = value;
			}
		}
	}
}
