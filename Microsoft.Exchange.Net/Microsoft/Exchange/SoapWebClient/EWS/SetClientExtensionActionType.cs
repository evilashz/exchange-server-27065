using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003A0 RID: 928
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class SetClientExtensionActionType
	{
		// Token: 0x040014A6 RID: 5286
		public ClientExtensionType ClientExtension;

		// Token: 0x040014A7 RID: 5287
		[XmlAttribute]
		public SetClientExtensionActionIdType ActionId;

		// Token: 0x040014A8 RID: 5288
		[XmlAttribute]
		public string ExtensionId;
	}
}
