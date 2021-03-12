using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000267 RID: 615
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class RetentionTagType
	{
		// Token: 0x04000FC1 RID: 4033
		[XmlAttribute]
		public bool IsExplicit;

		// Token: 0x04000FC2 RID: 4034
		[XmlText]
		public string Value;
	}
}
