using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003FB RID: 1019
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class CompleteFindInGALSpeechRecognitionType : BaseRequestType
	{
		// Token: 0x040015CB RID: 5579
		public RecognitionIdType RecognitionId;

		// Token: 0x040015CC RID: 5580
		[XmlElement(DataType = "base64Binary")]
		public byte[] AudioData;
	}
}
