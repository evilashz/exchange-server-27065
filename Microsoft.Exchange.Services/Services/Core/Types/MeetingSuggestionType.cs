using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005F5 RID: 1525
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class MeetingSuggestionType : BaseEntityType
	{
		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06002EFA RID: 12026 RVA: 0x000B3BC6 File Offset: 0x000B1DC6
		// (set) Token: 0x06002EFB RID: 12027 RVA: 0x000B3BCE File Offset: 0x000B1DCE
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		[XmlArrayItem(ElementName = "EmailUser", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(EmailUserType))]
		public EmailUserType[] Attendees { get; set; }

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06002EFC RID: 12028 RVA: 0x000B3BD7 File Offset: 0x000B1DD7
		// (set) Token: 0x06002EFD RID: 12029 RVA: 0x000B3BDF File Offset: 0x000B1DDF
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string Location { get; set; }

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06002EFE RID: 12030 RVA: 0x000B3BE8 File Offset: 0x000B1DE8
		// (set) Token: 0x06002EFF RID: 12031 RVA: 0x000B3BF0 File Offset: 0x000B1DF0
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string Subject { get; set; }

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06002F00 RID: 12032 RVA: 0x000B3BF9 File Offset: 0x000B1DF9
		// (set) Token: 0x06002F01 RID: 12033 RVA: 0x000B3C01 File Offset: 0x000B1E01
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public string MeetingString { get; set; }

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06002F02 RID: 12034 RVA: 0x000B3C0A File Offset: 0x000B1E0A
		// (set) Token: 0x06002F03 RID: 12035 RVA: 0x000B3C12 File Offset: 0x000B1E12
		[IgnoreDataMember]
		public DateTime StartTime { get; set; }

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06002F04 RID: 12036 RVA: 0x000B3C1C File Offset: 0x000B1E1C
		// (set) Token: 0x06002F05 RID: 12037 RVA: 0x000B3C42 File Offset: 0x000B1E42
		[XmlIgnore]
		[DataMember(Name = "StartTime", IsRequired = false, EmitDefaultValue = false)]
		[DateTimeString]
		public string StartTimeString
		{
			get
			{
				ExDateTime dateTime = (ExDateTime)this.StartTime;
				return ExDateTimeConverter.ToOffsetXsdDateTime(dateTime, dateTime.TimeZone);
			}
			set
			{
				this.StartTime = (DateTime)ExDateTimeConverter.Parse(value);
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06002F06 RID: 12038 RVA: 0x000B3C55 File Offset: 0x000B1E55
		// (set) Token: 0x06002F07 RID: 12039 RVA: 0x000B3C5D File Offset: 0x000B1E5D
		[IgnoreDataMember]
		public DateTime EndTime { get; set; }

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06002F08 RID: 12040 RVA: 0x000B3C68 File Offset: 0x000B1E68
		// (set) Token: 0x06002F09 RID: 12041 RVA: 0x000B3C8E File Offset: 0x000B1E8E
		[XmlIgnore]
		[DateTimeString]
		[DataMember(Name = "EndTime", IsRequired = false, EmitDefaultValue = false)]
		public string EndTimeString
		{
			get
			{
				ExDateTime dateTime = (ExDateTime)this.EndTime;
				return ExDateTimeConverter.ToOffsetXsdDateTime(dateTime, dateTime.TimeZone);
			}
			set
			{
				this.EndTime = (DateTime)ExDateTimeConverter.Parse(value);
			}
		}
	}
}
