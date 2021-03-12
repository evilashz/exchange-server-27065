using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000292 RID: 658
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[Serializable]
	public class GetInboxRulesResponseType : ResponseMessageType
	{
		// Token: 0x04001121 RID: 4385
		public bool OutlookRuleBlobExists;

		// Token: 0x04001122 RID: 4386
		[XmlIgnore]
		public bool OutlookRuleBlobExistsSpecified;

		// Token: 0x04001123 RID: 4387
		[XmlArrayItem("Rule", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public RuleType[] InboxRules;
	}
}
