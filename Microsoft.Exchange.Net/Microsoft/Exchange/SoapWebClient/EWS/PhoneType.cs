using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000263 RID: 611
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class PhoneType
	{
		// Token: 0x04000FB4 RID: 4020
		public string OriginalPhoneString;

		// Token: 0x04000FB5 RID: 4021
		public string PhoneString;

		// Token: 0x04000FB6 RID: 4022
		public string Type;
	}
}
