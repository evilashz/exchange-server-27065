using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000235 RID: 565
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class PhoneNumberDictionaryEntryType
	{
		// Token: 0x04000E9F RID: 3743
		[XmlAttribute]
		public PhoneNumberKeyType Key;

		// Token: 0x04000EA0 RID: 3744
		[XmlText]
		public string Value;
	}
}
