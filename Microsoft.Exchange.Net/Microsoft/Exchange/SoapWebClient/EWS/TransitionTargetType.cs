using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003DB RID: 987
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class TransitionTargetType
	{
		// Token: 0x04001583 RID: 5507
		[XmlAttribute]
		public TransitionTargetKindType Kind;

		// Token: 0x04001584 RID: 5508
		[XmlText]
		public string Value;
	}
}
