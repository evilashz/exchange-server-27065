using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000247 RID: 583
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class InternetHeaderType
	{
		// Token: 0x04000F81 RID: 3969
		[XmlAttribute]
		public string HeaderName;

		// Token: 0x04000F82 RID: 3970
		[XmlText]
		public string Value;
	}
}
