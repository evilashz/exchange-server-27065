using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000345 RID: 837
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class UpdateInboxRulesRequestType : BaseRequestType
	{
		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06001B19 RID: 6937 RVA: 0x0002945D File Offset: 0x0002765D
		// (set) Token: 0x06001B1A RID: 6938 RVA: 0x00029465 File Offset: 0x00027665
		public string MailboxSmtpAddress
		{
			get
			{
				return this.mailboxSmtpAddressField;
			}
			set
			{
				this.mailboxSmtpAddressField = value;
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06001B1B RID: 6939 RVA: 0x0002946E File Offset: 0x0002766E
		// (set) Token: 0x06001B1C RID: 6940 RVA: 0x00029476 File Offset: 0x00027676
		public bool RemoveOutlookRuleBlob
		{
			get
			{
				return this.removeOutlookRuleBlobField;
			}
			set
			{
				this.removeOutlookRuleBlobField = value;
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06001B1D RID: 6941 RVA: 0x0002947F File Offset: 0x0002767F
		// (set) Token: 0x06001B1E RID: 6942 RVA: 0x00029487 File Offset: 0x00027687
		[XmlIgnore]
		public bool RemoveOutlookRuleBlobSpecified
		{
			get
			{
				return this.removeOutlookRuleBlobFieldSpecified;
			}
			set
			{
				this.removeOutlookRuleBlobFieldSpecified = value;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06001B1F RID: 6943 RVA: 0x00029490 File Offset: 0x00027690
		// (set) Token: 0x06001B20 RID: 6944 RVA: 0x00029498 File Offset: 0x00027698
		[XmlArrayItem("SetRuleOperation", typeof(SetRuleOperationType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("DeleteRuleOperation", typeof(DeleteRuleOperationType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		[XmlArrayItem("CreateRuleOperation", typeof(CreateRuleOperationType), Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public RuleOperationType[] Operations
		{
			get
			{
				return this.operationsField;
			}
			set
			{
				this.operationsField = value;
			}
		}

		// Token: 0x0400121A RID: 4634
		private string mailboxSmtpAddressField;

		// Token: 0x0400121B RID: 4635
		private bool removeOutlookRuleBlobField;

		// Token: 0x0400121C RID: 4636
		private bool removeOutlookRuleBlobFieldSpecified;

		// Token: 0x0400121D RID: 4637
		private RuleOperationType[] operationsField;
	}
}
