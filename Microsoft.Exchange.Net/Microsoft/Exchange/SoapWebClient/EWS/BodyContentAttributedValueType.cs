using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001CA RID: 458
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class BodyContentAttributedValueType
	{
		// Token: 0x04000C38 RID: 3128
		public BodyContentType Value;

		// Token: 0x04000C39 RID: 3129
		[XmlArrayItem("Attribution", IsNullable = false)]
		public string[] Attributions;
	}
}
