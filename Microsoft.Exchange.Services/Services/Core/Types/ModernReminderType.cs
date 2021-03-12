using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005F8 RID: 1528
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "ModernReminderType")]
	[Serializable]
	public class ModernReminderType
	{
		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06002F19 RID: 12057 RVA: 0x000B3D1F File Offset: 0x000B1F1F
		// (set) Token: 0x06002F1A RID: 12058 RVA: 0x000B3D27 File Offset: 0x000B1F27
		[DataMember(Order = 1)]
		public Guid Id { get; set; }

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06002F1B RID: 12059 RVA: 0x000B3D30 File Offset: 0x000B1F30
		// (set) Token: 0x06002F1C RID: 12060 RVA: 0x000B3D38 File Offset: 0x000B1F38
		[IgnoreDataMember]
		public ReminderTimeHint ReminderTimeHint { get; set; }

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06002F1D RID: 12061 RVA: 0x000B3D41 File Offset: 0x000B1F41
		// (set) Token: 0x06002F1E RID: 12062 RVA: 0x000B3D4E File Offset: 0x000B1F4E
		[DataMember(Name = "ReminderTimeHint", Order = 2)]
		[XmlIgnore]
		public string ReminderTimeHintString
		{
			get
			{
				return EnumUtilities.ToString<ReminderTimeHint>(this.ReminderTimeHint);
			}
			set
			{
				this.ReminderTimeHint = EnumUtilities.Parse<ReminderTimeHint>(value);
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06002F1F RID: 12063 RVA: 0x000B3D5C File Offset: 0x000B1F5C
		// (set) Token: 0x06002F20 RID: 12064 RVA: 0x000B3D64 File Offset: 0x000B1F64
		[IgnoreDataMember]
		public Hours Hours { get; set; }

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06002F21 RID: 12065 RVA: 0x000B3D6D File Offset: 0x000B1F6D
		// (set) Token: 0x06002F22 RID: 12066 RVA: 0x000B3D7A File Offset: 0x000B1F7A
		[DataMember(Name = "Hours", Order = 3)]
		[XmlIgnore]
		public string HoursString
		{
			get
			{
				return EnumUtilities.ToString<Hours>(this.Hours);
			}
			set
			{
				this.Hours = EnumUtilities.Parse<Hours>(value);
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06002F23 RID: 12067 RVA: 0x000B3D88 File Offset: 0x000B1F88
		// (set) Token: 0x06002F24 RID: 12068 RVA: 0x000B3D90 File Offset: 0x000B1F90
		[IgnoreDataMember]
		public Priority Priority { get; set; }

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06002F25 RID: 12069 RVA: 0x000B3D99 File Offset: 0x000B1F99
		// (set) Token: 0x06002F26 RID: 12070 RVA: 0x000B3DA6 File Offset: 0x000B1FA6
		[DataMember(Name = "Priority", Order = 4)]
		[XmlIgnore]
		public string PriorityString
		{
			get
			{
				return EnumUtilities.ToString<Priority>(this.Priority);
			}
			set
			{
				this.Priority = EnumUtilities.Parse<Priority>(value);
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06002F27 RID: 12071 RVA: 0x000B3DB4 File Offset: 0x000B1FB4
		// (set) Token: 0x06002F28 RID: 12072 RVA: 0x000B3DBC File Offset: 0x000B1FBC
		[DataMember(Order = 5)]
		public int Duration { get; set; }

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06002F29 RID: 12073 RVA: 0x000B3DC5 File Offset: 0x000B1FC5
		// (set) Token: 0x06002F2A RID: 12074 RVA: 0x000B3DCD File Offset: 0x000B1FCD
		[IgnoreDataMember]
		public DateTime ReferenceTime { get; set; }

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06002F2B RID: 12075 RVA: 0x000B3DD8 File Offset: 0x000B1FD8
		// (set) Token: 0x06002F2C RID: 12076 RVA: 0x000B3DFE File Offset: 0x000B1FFE
		[XmlIgnore]
		[DataMember(Name = "ReferenceTime", Order = 6)]
		[DateTimeString]
		public string ReferenceTimeString
		{
			get
			{
				ExDateTime dateTime = (ExDateTime)this.ReferenceTime;
				return ExDateTimeConverter.ToOffsetXsdDateTime(dateTime, dateTime.TimeZone);
			}
			set
			{
				this.ReferenceTime = (DateTime)ExDateTimeConverter.Parse(value);
			}
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06002F2D RID: 12077 RVA: 0x000B3E11 File Offset: 0x000B2011
		// (set) Token: 0x06002F2E RID: 12078 RVA: 0x000B3E19 File Offset: 0x000B2019
		[IgnoreDataMember]
		public DateTime CustomReminderTime { get; set; }

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06002F2F RID: 12079 RVA: 0x000B3E24 File Offset: 0x000B2024
		// (set) Token: 0x06002F30 RID: 12080 RVA: 0x000B3E4A File Offset: 0x000B204A
		[DataMember(Name = "CustomReminderTime", Order = 7)]
		[DateTimeString]
		[XmlIgnore]
		public string CustomReminderTimeString
		{
			get
			{
				ExDateTime dateTime = (ExDateTime)this.CustomReminderTime;
				return ExDateTimeConverter.ToOffsetXsdDateTime(dateTime, dateTime.TimeZone);
			}
			set
			{
				this.CustomReminderTime = (DateTime)ExDateTimeConverter.Parse(value);
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06002F31 RID: 12081 RVA: 0x000B3E5D File Offset: 0x000B205D
		// (set) Token: 0x06002F32 RID: 12082 RVA: 0x000B3E65 File Offset: 0x000B2065
		[IgnoreDataMember]
		public DateTime DueDate { get; set; }

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06002F33 RID: 12083 RVA: 0x000B3E70 File Offset: 0x000B2070
		// (set) Token: 0x06002F34 RID: 12084 RVA: 0x000B3E96 File Offset: 0x000B2096
		[DateTimeString]
		[XmlIgnore]
		[DataMember(Name = "DueDate", Order = 8)]
		public string DueDateString
		{
			get
			{
				ExDateTime dateTime = (ExDateTime)this.DueDate;
				return ExDateTimeConverter.ToOffsetXsdDateTime(dateTime, dateTime.TimeZone);
			}
			set
			{
				this.DueDate = (DateTime)ExDateTimeConverter.Parse(value);
			}
		}
	}
}
