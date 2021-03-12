using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000199 RID: 409
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class AttachmentIdType : RequestAttachmentIdType
	{
		// Token: 0x040009AC RID: 2476
		[XmlAttribute]
		public string RootItemId;

		// Token: 0x040009AD RID: 2477
		[XmlAttribute]
		public string RootItemChangeKey;
	}
}
