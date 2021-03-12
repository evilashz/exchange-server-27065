using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200021A RID: 538
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class RelativeMonthlyRecurrencePatternType : IntervalRecurrencePatternBaseType
	{
		// Token: 0x04000E0F RID: 3599
		public DayOfWeekType DaysOfWeek;

		// Token: 0x04000E10 RID: 3600
		public DayOfWeekIndexType DayOfWeekIndex;
	}
}
