using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002C6 RID: 710
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class DeleteRuleOperationType : RuleOperationType
	{
		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x0600182B RID: 6187 RVA: 0x00027BAA File Offset: 0x00025DAA
		// (set) Token: 0x0600182C RID: 6188 RVA: 0x00027BB2 File Offset: 0x00025DB2
		public string RuleId
		{
			get
			{
				return this.ruleIdField;
			}
			set
			{
				this.ruleIdField = value;
			}
		}

		// Token: 0x04001067 RID: 4199
		private string ruleIdField;
	}
}
