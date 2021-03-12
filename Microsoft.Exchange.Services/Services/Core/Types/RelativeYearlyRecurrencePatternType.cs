using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000620 RID: 1568
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "RelativeYearlyRecurrence")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class RelativeYearlyRecurrencePatternType : RecurrencePatternBaseType
	{
		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06003126 RID: 12582 RVA: 0x000B6DEB File Offset: 0x000B4FEB
		// (set) Token: 0x06003127 RID: 12583 RVA: 0x000B6DF3 File Offset: 0x000B4FF3
		[DataMember(EmitDefaultValue = false, IsRequired = true, Order = 1)]
		public string DaysOfWeek { get; set; }

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06003128 RID: 12584 RVA: 0x000B6DFC File Offset: 0x000B4FFC
		// (set) Token: 0x06003129 RID: 12585 RVA: 0x000B6E04 File Offset: 0x000B5004
		[IgnoreDataMember]
		[XmlElement]
		public DayOfWeekIndexType DayOfWeekIndex { get; set; }

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x0600312A RID: 12586 RVA: 0x000B6E0D File Offset: 0x000B500D
		// (set) Token: 0x0600312B RID: 12587 RVA: 0x000B6E1A File Offset: 0x000B501A
		[XmlIgnore]
		[DataMember(Name = "DayOfWeekIndex", EmitDefaultValue = false, IsRequired = true, Order = 2)]
		public string DayOfWeekIndexString
		{
			get
			{
				return EnumUtilities.ToString<DayOfWeekIndexType>(this.DayOfWeekIndex);
			}
			set
			{
				this.DayOfWeekIndex = EnumUtilities.Parse<DayOfWeekIndexType>(value);
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x0600312C RID: 12588 RVA: 0x000B6E28 File Offset: 0x000B5028
		// (set) Token: 0x0600312D RID: 12589 RVA: 0x000B6E30 File Offset: 0x000B5030
		[DataMember(EmitDefaultValue = false, IsRequired = true, Order = 3)]
		public string Month { get; set; }
	}
}
