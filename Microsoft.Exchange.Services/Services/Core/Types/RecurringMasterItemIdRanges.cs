using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200085E RID: 2142
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "RecurringMasterItemIdRangesType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class RecurringMasterItemIdRanges : ItemId
	{
		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x06003D91 RID: 15761 RVA: 0x000D79ED File Offset: 0x000D5BED
		// (set) Token: 0x06003D92 RID: 15762 RVA: 0x000D79F5 File Offset: 0x000D5BF5
		[DataMember(IsRequired = false)]
		[XmlArrayItem("Range", IsNullable = false)]
		public RecurringMasterItemIdRanges.OccurrencesRange[] Ranges { get; set; }

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x06003D93 RID: 15763 RVA: 0x000D79FE File Offset: 0x000D5BFE
		// (set) Token: 0x06003D94 RID: 15764 RVA: 0x000D7A06 File Offset: 0x000D5C06
		[XmlIgnore]
		[DataMember(IsRequired = false)]
		public RecurringMasterItemIdRanges.ExpandAroundDateOccurrenceRangeType ExpandAroundDateOccurrenceRange { get; set; }

		// Token: 0x0200085F RID: 2143
		[XmlType(TypeName = "OccurrencesRangeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[DataContract(Name = "OccurrencesRange", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
		[Serializable]
		public class OccurrencesRange
		{
			// Token: 0x17000EC1 RID: 3777
			// (get) Token: 0x06003D96 RID: 15766 RVA: 0x000D7A17 File Offset: 0x000D5C17
			// (set) Token: 0x06003D97 RID: 15767 RVA: 0x000D7A1F File Offset: 0x000D5C1F
			[XmlAttribute]
			[DataMember(IsRequired = false)]
			public int Count { get; set; }

			// Token: 0x17000EC2 RID: 3778
			// (get) Token: 0x06003D98 RID: 15768 RVA: 0x000D7A28 File Offset: 0x000D5C28
			// (set) Token: 0x06003D99 RID: 15769 RVA: 0x000D7A30 File Offset: 0x000D5C30
			[DataMember(IsRequired = false)]
			[XmlAttribute]
			public bool CompareOriginalStartTime { get; set; }

			// Token: 0x17000EC3 RID: 3779
			// (get) Token: 0x06003D9A RID: 15770 RVA: 0x000D7A39 File Offset: 0x000D5C39
			// (set) Token: 0x06003D9B RID: 15771 RVA: 0x000D7A41 File Offset: 0x000D5C41
			[DataMember(IsRequired = false)]
			[XmlAttribute]
			public string Start { get; set; }

			// Token: 0x17000EC4 RID: 3780
			// (get) Token: 0x06003D9C RID: 15772 RVA: 0x000D7A4A File Offset: 0x000D5C4A
			// (set) Token: 0x06003D9D RID: 15773 RVA: 0x000D7A52 File Offset: 0x000D5C52
			[DataMember(IsRequired = false)]
			[XmlAttribute]
			public string End { get; set; }

			// Token: 0x17000EC5 RID: 3781
			// (get) Token: 0x06003D9E RID: 15774 RVA: 0x000D7A5B File Offset: 0x000D5C5B
			[XmlIgnore]
			public ExDateTime StartExDateTime
			{
				get
				{
					if (string.IsNullOrEmpty(this.Start))
					{
						return ExDateTime.MinValue;
					}
					return ExDateTimeConverter.ParseTimeZoneRelated(this.Start, EWSSettings.RequestTimeZone);
				}
			}

			// Token: 0x17000EC6 RID: 3782
			// (get) Token: 0x06003D9F RID: 15775 RVA: 0x000D7A80 File Offset: 0x000D5C80
			[XmlIgnore]
			public ExDateTime EndExDateTime
			{
				get
				{
					if (string.IsNullOrEmpty(this.End))
					{
						return ExDateTime.MaxValue;
					}
					return ExDateTimeConverter.ParseTimeZoneRelated(this.End, EWSSettings.RequestTimeZone);
				}
			}

			// Token: 0x06003DA0 RID: 15776 RVA: 0x000D7AA5 File Offset: 0x000D5CA5
			public OccurrencesRange()
			{
				this.Count = 1;
				this.CompareOriginalStartTime = false;
			}

			// Token: 0x0400236F RID: 9071
			[XmlIgnore]
			public const int MaxCount = 732;
		}

		// Token: 0x02000860 RID: 2144
		[DataContract(Name = "ExpandAroundDateOccurrenceRangeType", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
		public class ExpandAroundDateOccurrenceRangeType : RecurringMasterItemIdRanges.OccurrencesRange
		{
			// Token: 0x17000EC7 RID: 3783
			// (get) Token: 0x06003DA1 RID: 15777 RVA: 0x000D7ABB File Offset: 0x000D5CBB
			// (set) Token: 0x06003DA2 RID: 15778 RVA: 0x000D7AC3 File Offset: 0x000D5CC3
			[DataMember(IsRequired = true)]
			public string ExpandOccurrencesAroundDate { get; set; }

			// Token: 0x17000EC8 RID: 3784
			// (get) Token: 0x06003DA3 RID: 15779 RVA: 0x000D7ACC File Offset: 0x000D5CCC
			public ExDateTime? ExpandOccurrencesAroundExDateTime
			{
				get
				{
					if (!string.IsNullOrEmpty(this.ExpandOccurrencesAroundDate))
					{
						return new ExDateTime?(ExDateTimeConverter.ParseTimeZoneRelated(this.ExpandOccurrencesAroundDate, EWSSettings.RequestTimeZone));
					}
					return new ExDateTime?(ExDateTime.Now);
				}
			}
		}
	}
}
