using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000623 RID: 1571
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "RelativeMonthlyRecurrence")]
	[Serializable]
	public class RelativeMonthlyRecurrencePatternType : IntervalRecurrencePatternBaseType
	{
		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06003135 RID: 12597 RVA: 0x000B6E73 File Offset: 0x000B5073
		// (set) Token: 0x06003136 RID: 12598 RVA: 0x000B6E7B File Offset: 0x000B507B
		[DataMember(EmitDefaultValue = false, IsRequired = true, Order = 1)]
		public string DaysOfWeek { get; set; }

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06003137 RID: 12599 RVA: 0x000B6E84 File Offset: 0x000B5084
		// (set) Token: 0x06003138 RID: 12600 RVA: 0x000B6E8C File Offset: 0x000B508C
		[XmlElement]
		[IgnoreDataMember]
		public DayOfWeekIndexType DayOfWeekIndex { get; set; }

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06003139 RID: 12601 RVA: 0x000B6E95 File Offset: 0x000B5095
		// (set) Token: 0x0600313A RID: 12602 RVA: 0x000B6EA2 File Offset: 0x000B50A2
		[DataMember(Name = "DayOfWeekIndex", EmitDefaultValue = false, IsRequired = true, Order = 1)]
		[XmlIgnore]
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
	}
}
