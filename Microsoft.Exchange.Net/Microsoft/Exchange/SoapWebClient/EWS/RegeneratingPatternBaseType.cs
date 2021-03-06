using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200021C RID: 540
	[XmlInclude(typeof(MonthlyRegeneratingPatternType))]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlInclude(typeof(WeeklyRegeneratingPatternType))]
	[XmlInclude(typeof(DailyRegeneratingPatternType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(YearlyRegeneratingPatternType))]
	[Serializable]
	public abstract class RegeneratingPatternBaseType : IntervalRecurrencePatternBaseType
	{
	}
}
