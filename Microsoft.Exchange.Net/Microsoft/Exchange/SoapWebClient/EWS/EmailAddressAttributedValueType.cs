using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001D9 RID: 473
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class EmailAddressAttributedValueType
	{
		// Token: 0x04000C90 RID: 3216
		public EmailAddressType Value;

		// Token: 0x04000C91 RID: 3217
		[XmlArrayItem("Attribution", IsNullable = false)]
		public string[] Attributions;
	}
}
