using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001D8 RID: 472
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class PhoneNumberAttributedValueType
	{
		// Token: 0x04000C8E RID: 3214
		public PersonaPhoneNumberType Value;

		// Token: 0x04000C8F RID: 3215
		[XmlArrayItem("Attribution", IsNullable = false)]
		public string[] Attributions;
	}
}
