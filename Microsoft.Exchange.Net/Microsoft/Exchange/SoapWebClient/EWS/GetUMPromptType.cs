using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000451 RID: 1105
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetUMPromptType : BaseRequestType
	{
		// Token: 0x040016EC RID: 5868
		public string ConfigurationObject;

		// Token: 0x040016ED RID: 5869
		public string PromptName;
	}
}
