using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200027A RID: 634
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetDiscoverySearchConfigurationResponseMessageType : ResponseMessageType
	{
		// Token: 0x0400104E RID: 4174
		[XmlArrayItem("DiscoverySearchConfiguration", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public DiscoverySearchConfigurationType[] DiscoverySearchConfigurations;
	}
}
