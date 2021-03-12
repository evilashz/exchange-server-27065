using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001AE RID: 430
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class RuleValidationErrorType
	{
		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x000248E0 File Offset: 0x00022AE0
		// (set) Token: 0x06001224 RID: 4644 RVA: 0x000248E8 File Offset: 0x00022AE8
		public RuleFieldURIType FieldURI
		{
			get
			{
				return this.fieldURIField;
			}
			set
			{
				this.fieldURIField = value;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x000248F1 File Offset: 0x00022AF1
		// (set) Token: 0x06001226 RID: 4646 RVA: 0x000248F9 File Offset: 0x00022AF9
		public RuleValidationErrorCodeType ErrorCode
		{
			get
			{
				return this.errorCodeField;
			}
			set
			{
				this.errorCodeField = value;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x00024902 File Offset: 0x00022B02
		// (set) Token: 0x06001228 RID: 4648 RVA: 0x0002490A File Offset: 0x00022B0A
		public string ErrorMessage
		{
			get
			{
				return this.errorMessageField;
			}
			set
			{
				this.errorMessageField = value;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x00024913 File Offset: 0x00022B13
		// (set) Token: 0x0600122A RID: 4650 RVA: 0x0002491B File Offset: 0x00022B1B
		public string FieldValue
		{
			get
			{
				return this.fieldValueField;
			}
			set
			{
				this.fieldValueField = value;
			}
		}

		// Token: 0x04000C57 RID: 3159
		private RuleFieldURIType fieldURIField;

		// Token: 0x04000C58 RID: 3160
		private RuleValidationErrorCodeType errorCodeField;

		// Token: 0x04000C59 RID: 3161
		private string errorMessageField;

		// Token: 0x04000C5A RID: 3162
		private string fieldValueField;
	}
}
