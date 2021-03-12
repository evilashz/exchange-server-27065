using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001DD RID: 477
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class StringArrayAttributedValueType
	{
		// Token: 0x04000CB0 RID: 3248
		[XmlArrayItem("Value", IsNullable = false)]
		public string[] Values;

		// Token: 0x04000CB1 RID: 3249
		[XmlArrayItem("Attribution", IsNullable = false)]
		public string[] Attributions;
	}
}
