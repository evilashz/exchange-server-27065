using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000214 RID: 532
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class AbsoluteYearlyRecurrencePatternType : RecurrencePatternBaseType
	{
		// Token: 0x04000DF5 RID: 3573
		public int DayOfMonth;

		// Token: 0x04000DF6 RID: 3574
		public MonthNamesType Month;
	}
}
