using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000222 RID: 546
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class EndDateRecurrenceRangeType : RecurrenceRangeBaseType
	{
		// Token: 0x04000E1D RID: 3613
		[XmlElement(DataType = "date")]
		public DateTime EndDate;
	}
}
