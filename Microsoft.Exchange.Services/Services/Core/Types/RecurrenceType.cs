using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200061B RID: 1563
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class RecurrenceType
	{
		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06003115 RID: 12565 RVA: 0x000B6D5D File Offset: 0x000B4F5D
		// (set) Token: 0x06003116 RID: 12566 RVA: 0x000B6D65 File Offset: 0x000B4F65
		[XmlElement("DailyRecurrence", typeof(DailyRecurrencePatternType))]
		[XmlElement("RelativeYearlyRecurrence", typeof(RelativeYearlyRecurrencePatternType))]
		[XmlElement("WeeklyRecurrence", typeof(WeeklyRecurrencePatternType))]
		[DataMember(EmitDefaultValue = false, IsRequired = true, Order = 1)]
		[XmlElement("RelativeMonthlyRecurrence", typeof(RelativeMonthlyRecurrencePatternType))]
		[XmlElement("AbsoluteMonthlyRecurrence", typeof(AbsoluteMonthlyRecurrencePatternType))]
		[XmlElement("AbsoluteYearlyRecurrence", typeof(AbsoluteYearlyRecurrencePatternType))]
		public RecurrencePatternBaseType RecurrencePattern { get; set; }

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06003117 RID: 12567 RVA: 0x000B6D6E File Offset: 0x000B4F6E
		// (set) Token: 0x06003118 RID: 12568 RVA: 0x000B6D76 File Offset: 0x000B4F76
		[XmlElement("EndDateRecurrence", typeof(EndDateRecurrenceRangeType))]
		[XmlElement("NoEndRecurrence", typeof(NoEndRecurrenceRangeType))]
		[XmlElement("NumberedRecurrence", typeof(NumberedRecurrenceRangeType))]
		[DataMember(EmitDefaultValue = false, IsRequired = true, Order = 2)]
		public RecurrenceRangeBaseType RecurrenceRange { get; set; }
	}
}
