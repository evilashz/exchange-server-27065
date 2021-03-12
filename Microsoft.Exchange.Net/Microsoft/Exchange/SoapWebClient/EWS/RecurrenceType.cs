using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000210 RID: 528
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class RecurrenceType
	{
		// Token: 0x04000DF1 RID: 3569
		[XmlElement("RelativeYearlyRecurrence", typeof(RelativeYearlyRecurrencePatternType))]
		[XmlElement("WeeklyRecurrence", typeof(WeeklyRecurrencePatternType))]
		[XmlElement("AbsoluteYearlyRecurrence", typeof(AbsoluteYearlyRecurrencePatternType))]
		[XmlElement("RelativeMonthlyRecurrence", typeof(RelativeMonthlyRecurrencePatternType))]
		[XmlElement("AbsoluteMonthlyRecurrence", typeof(AbsoluteMonthlyRecurrencePatternType))]
		[XmlElement("DailyRecurrence", typeof(DailyRecurrencePatternType))]
		public RecurrencePatternBaseType Item;

		// Token: 0x04000DF2 RID: 3570
		[XmlElement("EndDateRecurrence", typeof(EndDateRecurrenceRangeType))]
		[XmlElement("NoEndRecurrence", typeof(NoEndRecurrenceRangeType))]
		[XmlElement("NumberedRecurrence", typeof(NumberedRecurrenceRangeType))]
		public RecurrenceRangeBaseType Item1;
	}
}
