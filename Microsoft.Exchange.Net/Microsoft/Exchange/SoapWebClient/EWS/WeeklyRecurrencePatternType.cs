using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000219 RID: 537
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class WeeklyRecurrencePatternType : IntervalRecurrencePatternBaseType
	{
		// Token: 0x04000E0D RID: 3597
		public string DaysOfWeek;

		// Token: 0x04000E0E RID: 3598
		public string FirstDayOfWeek;
	}
}
