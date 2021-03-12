using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002C1 RID: 705
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class UserConfigurationDictionaryObjectType
	{
		// Token: 0x04001205 RID: 4613
		public UserConfigurationDictionaryObjectTypesType Type;

		// Token: 0x04001206 RID: 4614
		[XmlElement("Value")]
		public string[] Value;
	}
}
