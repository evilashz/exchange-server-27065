using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001D7 RID: 471
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class StringAttributedValueType
	{
		// Token: 0x04000C8C RID: 3212
		public string Value;

		// Token: 0x04000C8D RID: 3213
		[XmlArrayItem("Attribution", IsNullable = false)]
		public string[] Attributions;
	}
}
