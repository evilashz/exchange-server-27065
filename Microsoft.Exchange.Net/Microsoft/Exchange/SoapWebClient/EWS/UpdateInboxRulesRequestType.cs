using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000426 RID: 1062
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[Serializable]
	public class UpdateInboxRulesRequestType : BaseRequestType
	{
		// Token: 0x0400166C RID: 5740
		public string MailboxSmtpAddress;

		// Token: 0x0400166D RID: 5741
		public bool RemoveOutlookRuleBlob;

		// Token: 0x0400166E RID: 5742
		[XmlIgnore]
		public bool RemoveOutlookRuleBlobSpecified;

		// Token: 0x0400166F RID: 5743
		[XmlArrayItem("CreateRuleOperation", typeof(CreateRuleOperationType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("DeleteRuleOperation", typeof(DeleteRuleOperationType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("SetRuleOperation", typeof(SetRuleOperationType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public RuleOperationType[] Operations;
	}
}
