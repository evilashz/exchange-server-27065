using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000238 RID: 568
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class ImAddressDictionaryEntryType
	{
		// Token: 0x04000EB8 RID: 3768
		[XmlAttribute]
		public ImAddressKeyType Key;

		// Token: 0x04000EB9 RID: 3769
		[XmlText]
		public string Value;
	}
}
