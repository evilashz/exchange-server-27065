using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020002C8 RID: 712
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class CreateRuleOperationType : RuleOperationType
	{
		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x00027BDC File Offset: 0x00025DDC
		// (set) Token: 0x06001832 RID: 6194 RVA: 0x00027BE4 File Offset: 0x00025DE4
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

		// Token: 0x04001069 RID: 4201
		private RuleType ruleField;
	}
}
