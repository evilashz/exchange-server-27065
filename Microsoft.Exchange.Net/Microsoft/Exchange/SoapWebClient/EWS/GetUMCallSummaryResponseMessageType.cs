using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001A3 RID: 419
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetUMCallSummaryResponseMessageType : ResponseMessageType
	{
		// Token: 0x040009C8 RID: 2504
		[XmlArrayItem("UMReportRawCounters", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public UMReportRawCountersType[] UMReportRawCountersCollection;
	}
}
