using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000453 RID: 1107
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[Serializable]
	public class CreateUMPromptType : BaseRequestType
	{
		// Token: 0x040016F0 RID: 5872
		public string ConfigurationObject;

		// Token: 0x040016F1 RID: 5873
		public string PromptName;

		// Token: 0x040016F2 RID: 5874
		[XmlElement(DataType = "base64Binary")]
		public byte[] AudioData;
	}
}
