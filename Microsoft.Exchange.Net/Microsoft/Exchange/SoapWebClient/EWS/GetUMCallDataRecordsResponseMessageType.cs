using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001A7 RID: 423
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetUMCallDataRecordsResponseMessageType : ResponseMessageType
	{
		// Token: 0x040009DD RID: 2525
		[XmlArrayItem("CDRData", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public CDRDataType[] CallDataRecords;
	}
}
