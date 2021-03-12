using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003E9 RID: 1001
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class FractionalPageViewType : BasePagingType
	{
		// Token: 0x0400159E RID: 5534
		[XmlAttribute]
		public int Numerator;

		// Token: 0x0400159F RID: 5535
		[XmlAttribute]
		public int Denominator;
	}
}
