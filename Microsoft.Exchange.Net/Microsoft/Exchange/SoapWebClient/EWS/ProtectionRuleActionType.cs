using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002AE RID: 686
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class ProtectionRuleActionType
	{
		// Token: 0x040011E1 RID: 4577
		[XmlElement("Argument")]
		public ProtectionRuleArgumentType[] Argument;

		// Token: 0x040011E2 RID: 4578
		[XmlAttribute]
		public ProtectionRuleActionKindType Name;
	}
}
