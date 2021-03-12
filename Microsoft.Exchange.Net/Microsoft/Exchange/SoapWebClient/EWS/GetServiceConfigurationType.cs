using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200042B RID: 1067
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetServiceConfigurationType : BaseRequestType
	{
		// Token: 0x0400168B RID: 5771
		public EmailAddressType ActingAs;

		// Token: 0x0400168C RID: 5772
		[XmlArrayItem("ConfigurationName", IsNullable = false)]
		public ServiceConfigurationType[] RequestedConfiguration;

		// Token: 0x0400168D RID: 5773
		public ConfigurationRequestDetailsType ConfigurationRequestDetails;
	}
}
