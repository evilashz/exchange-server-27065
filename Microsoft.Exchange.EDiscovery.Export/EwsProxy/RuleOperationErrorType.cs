using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001AD RID: 429
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class RuleOperationErrorType
	{
		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x000248B6 File Offset: 0x00022AB6
		// (set) Token: 0x0600121F RID: 4639 RVA: 0x000248BE File Offset: 0x00022ABE
		public int OperationIndex
		{
			get
			{
				return this.operationIndexField;
			}
			set
			{
				this.operationIndexField = value;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001220 RID: 4640 RVA: 0x000248C7 File Offset: 0x00022AC7
		// (set) Token: 0x06001221 RID: 4641 RVA: 0x000248CF File Offset: 0x00022ACF
		[XmlArrayItem("Error", IsNullable = false)]
		public RuleValidationErrorType[] ValidationErrors
		{
			get
			{
				return this.validationErrorsField;
			}
			set
			{
				this.validationErrorsField = value;
			}
		}

		// Token: 0x04000C55 RID: 3157
		private int operationIndexField;

		// Token: 0x04000C56 RID: 3158
		private RuleValidationErrorType[] validationErrorsField;
	}
}
