using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001B1 RID: 433
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetInboxRulesResponseType : ResponseMessageType
	{
		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x0002492C File Offset: 0x00022B2C
		// (set) Token: 0x0600122D RID: 4653 RVA: 0x00024934 File Offset: 0x00022B34
		public bool OutlookRuleBlobExists
		{
			get
			{
				return this.outlookRuleBlobExistsField;
			}
			set
			{
				this.outlookRuleBlobExistsField = value;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x0002493D File Offset: 0x00022B3D
		// (set) Token: 0x0600122F RID: 4655 RVA: 0x00024945 File Offset: 0x00022B45
		[XmlIgnore]
		public bool OutlookRuleBlobExistsSpecified
		{
			get
			{
				return this.outlookRuleBlobExistsFieldSpecified;
			}
			set
			{
				this.outlookRuleBlobExistsFieldSpecified = value;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x0002494E File Offset: 0x00022B4E
		// (set) Token: 0x06001231 RID: 4657 RVA: 0x00024956 File Offset: 0x00022B56
		[XmlArrayItem("Rule", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public RuleType[] InboxRules
		{
			get
			{
				return this.inboxRulesField;
			}
			set
			{
				this.inboxRulesField = value;
			}
		}

		// Token: 0x04000CCF RID: 3279
		private bool outlookRuleBlobExistsField;

		// Token: 0x04000CD0 RID: 3280
		private bool outlookRuleBlobExistsFieldSpecified;

		// Token: 0x04000CD1 RID: 3281
		private RuleType[] inboxRulesField;
	}
}
