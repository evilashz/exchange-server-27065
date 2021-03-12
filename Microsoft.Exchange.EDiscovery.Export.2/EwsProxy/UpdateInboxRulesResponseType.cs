using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001AC RID: 428
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[Serializable]
	public class UpdateInboxRulesResponseType : ResponseMessageType
	{
		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x0002489D File Offset: 0x00022A9D
		// (set) Token: 0x0600121C RID: 4636 RVA: 0x000248A5 File Offset: 0x00022AA5
		[XmlArrayItem("RuleOperationError", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public RuleOperationErrorType[] RuleOperationErrors
		{
			get
			{
				return this.ruleOperationErrorsField;
			}
			set
			{
				this.ruleOperationErrorsField = value;
			}
		}

		// Token: 0x04000C54 RID: 3156
		private RuleOperationErrorType[] ruleOperationErrorsField;
	}
}
