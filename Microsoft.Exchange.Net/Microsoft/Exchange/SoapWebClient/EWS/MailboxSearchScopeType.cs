using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000280 RID: 640
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class MailboxSearchScopeType
	{
		// Token: 0x0400106A RID: 4202
		public string Mailbox;

		// Token: 0x0400106B RID: 4203
		public MailboxSearchLocationType SearchScope;

		// Token: 0x0400106C RID: 4204
		[XmlArrayItem("ExtendedAttribute", IsNullable = false)]
		public ExtendedAttributeType[] ExtendedAttributes;
	}
}
