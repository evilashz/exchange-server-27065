using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000244 RID: 580
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class TaskRecurrenceType
	{
		// Token: 0x04000F75 RID: 3957
		[XmlElement("MonthlyRegeneration", typeof(MonthlyRegeneratingPatternType))]
		[XmlElement("DailyRecurrence", typeof(DailyRecurrencePatternType))]
		[XmlElement("DailyRegeneration", typeof(DailyRegeneratingPatternType))]
		[XmlElement("AbsoluteYearlyRecurrence", typeof(AbsoluteYearlyRecurrencePatternType))]
		[XmlElement("WeeklyRegeneration", typeof(WeeklyRegeneratingPatternType))]
		[XmlElement("WeeklyRecurrence", typeof(WeeklyRecurrencePatternType))]
		[XmlElement("RelativeMonthlyRecurrence", typeof(RelativeMonthlyRecurrencePatternType))]
		[XmlElement("YearlyRegeneration", typeof(YearlyRegeneratingPatternType))]
		[XmlElement("RelativeYearlyRecurrence", typeof(RelativeYearlyRecurrencePatternType))]
		[XmlElement("AbsoluteMonthlyRecurrence", typeof(AbsoluteMonthlyRecurrencePatternType))]
		public RecurrencePatternBaseType Item;

		// Token: 0x04000F76 RID: 3958
		[XmlElement("EndDateRecurrence", typeof(EndDateRecurrenceRangeType))]
		[XmlElement("NoEndRecurrence", typeof(NoEndRecurrenceRangeType))]
		[XmlElement("NumberedRecurrence", typeof(NumberedRecurrenceRangeType))]
		public RecurrenceRangeBaseType Item1;
	}
}
