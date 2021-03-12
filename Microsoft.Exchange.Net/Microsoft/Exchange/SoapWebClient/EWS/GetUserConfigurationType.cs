using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000436 RID: 1078
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetUserConfigurationType : BaseRequestType
	{
		// Token: 0x040016AB RID: 5803
		public UserConfigurationNameType UserConfigurationName;

		// Token: 0x040016AC RID: 5804
		public UserConfigurationPropertyType UserConfigurationProperties;
	}
}
