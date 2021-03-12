using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002C7 RID: 711
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class SetRuleOperationType : RuleOperationType
	{
		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x0600182E RID: 6190 RVA: 0x00027BC3 File Offset: 0x00025DC3
		// (set) Token: 0x0600182F RID: 6191 RVA: 0x00027BCB File Offset: 0x00025DCB
		public RuleType Rule
		{
			get
			{
				return this.ruleField;
			}
			set
			{
				this.ruleField = value;
			}
		}

		// Token: 0x04001068 RID: 4200
		private RuleType ruleField;
	}
}
