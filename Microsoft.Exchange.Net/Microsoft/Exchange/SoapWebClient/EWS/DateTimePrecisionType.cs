using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000186 RID: 390
	[DesignerCategory("code")]
	[XmlRoot("DateTimePrecision", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class DateTimePrecisionType : SoapHeader
	{
		// Token: 0x040007AD RID: 1965
		[XmlText]
		public string[] Text;
	}
}
