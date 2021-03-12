using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000670 RID: 1648
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class TaskRecurrenceType
	{
		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x06003262 RID: 12898 RVA: 0x000B7AEA File Offset: 0x000B5CEA
		// (set) Token: 0x06003263 RID: 12899 RVA: 0x000B7AF2 File Offset: 0x000B5CF2
		[XmlElement("YearlyRegeneration", typeof(YearlyRegeneratingPatternType))]
		[XmlElement("RelativeMonthlyRecurrence", typeof(RelativeMonthlyRecurrencePatternType))]
		[XmlElement("RelativeYearlyRecurrence", typeof(RelativeYearlyRecurrencePatternType))]
		[XmlElement("MonthlyRegeneration", typeof(MonthlyRegeneratingPatternType))]
		[XmlElement("WeeklyRegeneration", typeof(WeeklyRegeneratingPatternType))]
		[XmlElement("AbsoluteMonthlyRecurrence", typeof(AbsoluteMonthlyRecurrencePatternType))]
		[DataMember(Name = "RecurrencePattern", EmitDefaultValue = false, Order = 1)]
		[XmlElement("WeeklyRecurrence", typeof(WeeklyRecurrencePatternType))]
		[XmlElement("AbsoluteYearlyRecurrence", typeof(AbsoluteYearlyRecurrencePatternType))]
		[XmlElement("DailyRecurrence", typeof(DailyRecurrencePatternType))]
		[XmlElement("DailyRegeneration", typeof(DailyRegeneratingPatternType))]
		public RecurrencePatternBaseType RecurrencePattern { get; set; }

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06003264 RID: 12900 RVA: 0x000B7AFB File Offset: 0x000B5CFB
		// (set) Token: 0x06003265 RID: 12901 RVA: 0x000B7B03 File Offset: 0x000B5D03
		[XmlElement("EndDateRecurrence", typeof(EndDateRecurrenceRangeType))]
		[DataMember(Name = "RecurrenceRange", EmitDefaultValue = false, Order = 2)]
		[XmlElement("NoEndRecurrence", typeof(NoEndRecurrenceRangeType))]
		[XmlElement("NumberedRecurrence", typeof(NumberedRecurrenceRangeType))]
		public RecurrenceRangeBaseType RecurrenceRange { get; set; }
	}
}
