using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002B3 RID: 691
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetServiceConfigurationResponseMessageType : ResponseMessageType
	{
		// Token: 0x040011EA RID: 4586
		[XmlArrayItem(IsNullable = false)]
		public ServiceConfigurationResponseMessageType[] ResponseMessages;
	}
}
