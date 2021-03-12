using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000216 RID: 534
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class RelativeYearlyRecurrencePatternType : RecurrencePatternBaseType
	{
		// Token: 0x04000E04 RID: 3588
		public string DaysOfWeek;

		// Token: 0x04000E05 RID: 3589
		public DayOfWeekIndexType DayOfWeekIndex;

		// Token: 0x04000E06 RID: 3590
		public MonthNamesType Month;
	}
}
