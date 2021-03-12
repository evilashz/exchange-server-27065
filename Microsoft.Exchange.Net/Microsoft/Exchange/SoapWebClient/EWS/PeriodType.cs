using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000189 RID: 393
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class PeriodType
	{
		// Token: 0x040007B5 RID: 1973
		[XmlAttribute(DataType = "duration")]
		public string Bias;

		// Token: 0x040007B6 RID: 1974
		[XmlAttribute]
		public string Name;

		// Token: 0x040007B7 RID: 1975
		[XmlAttribute]
		public string Id;
	}
}
