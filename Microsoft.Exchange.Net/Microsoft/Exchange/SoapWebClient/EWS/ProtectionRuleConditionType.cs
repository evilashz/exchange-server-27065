using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002A8 RID: 680
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ProtectionRuleConditionType
	{
		// Token: 0x040011CF RID: 4559
		[XmlElement("AllInternal", typeof(string))]
		[XmlElement("And", typeof(ProtectionRuleAndType))]
		[XmlElement("SenderDepartments", typeof(ProtectionRuleSenderDepartmentsType))]
		[XmlElement("RecipientIs", typeof(ProtectionRuleRecipientIsType))]
		[XmlElement("True", typeof(string))]
		[XmlChoiceIdentifier("ItemElementName")]
		public object Item;

		// Token: 0x040011D0 RID: 4560
		[XmlIgnore]
		public ItemChoiceType ItemElementName;
	}
}
