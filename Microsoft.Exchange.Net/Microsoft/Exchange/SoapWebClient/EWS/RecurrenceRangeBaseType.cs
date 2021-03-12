using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000221 RID: 545
	[XmlInclude(typeof(NumberedRecurrenceRangeType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlInclude(typeof(EndDateRecurrenceRangeType))]
	[XmlInclude(typeof(NoEndRecurrenceRangeType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public abstract class RecurrenceRangeBaseType
	{
		// Token: 0x04000E1C RID: 3612
		[XmlElement(DataType = "date")]
		public DateTime StartDate;
	}
}
