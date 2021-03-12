using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001DA RID: 474
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class PostalAddressAttributedValueType
	{
		// Token: 0x04000C92 RID: 3218
		public PersonaPostalAddressType Value;

		// Token: 0x04000C93 RID: 3219
		[XmlArrayItem("Attribution", IsNullable = false)]
		public string[] Attributions;
	}
}
