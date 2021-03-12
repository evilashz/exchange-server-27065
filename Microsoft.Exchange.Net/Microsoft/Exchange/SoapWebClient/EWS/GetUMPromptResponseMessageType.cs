using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200031D RID: 797
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetUMPromptResponseMessageType : ResponseMessageType
	{
		// Token: 0x0400133C RID: 4924
		[XmlElement(DataType = "base64Binary")]
		public byte[] AudioData;
	}
}
