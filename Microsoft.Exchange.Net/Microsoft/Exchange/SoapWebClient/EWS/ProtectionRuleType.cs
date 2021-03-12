using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002A7 RID: 679
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ProtectionRuleType
	{
		// Token: 0x040011CA RID: 4554
		public ProtectionRuleConditionType Condition;

		// Token: 0x040011CB RID: 4555
		public ProtectionRuleActionType Action;

		// Token: 0x040011CC RID: 4556
		[XmlAttribute]
		public string Name;

		// Token: 0x040011CD RID: 4557
		[XmlAttribute]
		public bool UserOverridable;

		// Token: 0x040011CE RID: 4558
		[XmlAttribute]
		public int Priority;
	}
}
