using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001B7 RID: 439
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class RuleActionsType
	{
		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x00024E60 File Offset: 0x00023060
		// (set) Token: 0x060012CA RID: 4810 RVA: 0x00024E68 File Offset: 0x00023068
		[XmlArrayItem("String", IsNullable = false)]
		public string[] AssignCategories
		{
			get
			{
				return this.assignCategoriesField;
			}
			set
			{
				this.assignCategoriesField = value;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x00024E71 File Offset: 0x00023071
		// (set) Token: 0x060012CC RID: 4812 RVA: 0x00024E79 File Offset: 0x00023079
		public TargetFolderIdType CopyToFolder
		{
			get
			{
				return this.copyToFolderField;
			}
			set
			{
				this.copyToFolderField = value;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x060012CD RID: 4813 RVA: 0x00024E82 File Offset: 0x00023082
		// (set) Token: 0x060012CE RID: 4814 RVA: 0x00024E8A File Offset: 0x0002308A
		public bool Delete
		{
			get
			{
				return this.deleteField;
			}
			set
			{
				this.deleteField = value;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x00024E93 File Offset: 0x00023093
		// (set) Token: 0x060012D0 RID: 4816 RVA: 0x00024E9B File Offset: 0x0002309B
		[XmlIgnore]
		public bool DeleteSpecified
		{
			get
			{
				return this.deleteFieldSpecified;
			}
			set
			{
				this.deleteFieldSpecified = value;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x00024EA4 File Offset: 0x000230A4
		// (set) Token: 0x060012D2 RID: 4818 RVA: 0x00024EAC File Offset: 0x000230AC
		[XmlArrayItem("Address", IsNullable = false)]
		public EmailAddressType[] ForwardAsAttachmentToRecipients
		{
			get
			{
				return this.forwardAsAttachmentToRecipientsField;
			}
			set
			{
				this.forwardAsAttachmentToRecipientsField = value;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x00024EB5 File Offset: 0x000230B5
		// (set) Token: 0x060012D4 RID: 4820 RVA: 0x00024EBD File Offset: 0x000230BD
		[XmlArrayItem("Address", IsNullable = false)]
		public EmailAddressType[] ForwardToRecipients
		{
			get
			{
				return this.forwardToRecipientsField;
			}
			set
			{
				this.forwardToRecipientsField = value;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x00024EC6 File Offset: 0x000230C6
		// (set) Token: 0x060012D6 RID: 4822 RVA: 0x00024ECE File Offset: 0x000230CE
		public ImportanceChoicesType MarkImportance
		{
			get
			{
				return this.markImportanceField;
			}
			set
			{
				this.markImportanceField = value;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x00024ED7 File Offset: 0x000230D7
		// (set) Token: 0x060012D8 RID: 4824 RVA: 0x00024EDF File Offset: 0x000230DF
		[XmlIgnore]
		public bool MarkImportanceSpecified
		{
			get
			{
				return this.markImportanceFieldSpecified;
			}
			set
			{
				this.markImportanceFieldSpecified = value;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x00024EE8 File Offset: 0x000230E8
		// (set) Token: 0x060012DA RID: 4826 RVA: 0x00024EF0 File Offset: 0x000230F0
		public bool MarkAsRead
		{
			get
			{
				return this.markAsReadField;
			}
			set
			{
				this.markAsReadField = value;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x00024EF9 File Offset: 0x000230F9
		// (set) Token: 0x060012DC RID: 4828 RVA: 0x00024F01 File Offset: 0x00023101
		[XmlIgnore]
		public bool MarkAsReadSpecified
		{
			get
			{
				return this.markAsReadFieldSpecified;
			}
			set
			{
				this.markAsReadFieldSpecified = value;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x00024F0A File Offset: 0x0002310A
		// (set) Token: 0x060012DE RID: 4830 RVA: 0x00024F12 File Offset: 0x00023112
		public TargetFolderIdType MoveToFolder
		{
			get
			{
				return this.moveToFolderField;
			}
			set
			{
				this.moveToFolderField = value;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x00024F1B File Offset: 0x0002311B
		// (set) Token: 0x060012E0 RID: 4832 RVA: 0x00024F23 File Offset: 0x00023123
		public bool PermanentDelete
		{
			get
			{
				return this.permanentDeleteField;
			}
			set
			{
				this.permanentDeleteField = value;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x00024F2C File Offset: 0x0002312C
		// (set) Token: 0x060012E2 RID: 4834 RVA: 0x00024F34 File Offset: 0x00023134
		[XmlIgnore]
		public bool PermanentDeleteSpecified
		{
			get
			{
				return this.permanentDeleteFieldSpecified;
			}
			set
			{
				this.permanentDeleteFieldSpecified = value;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060012E3 RID: 4835 RVA: 0x00024F3D File Offset: 0x0002313D
		// (set) Token: 0x060012E4 RID: 4836 RVA: 0x00024F45 File Offset: 0x00023145
		[XmlArrayItem("Address", IsNullable = false)]
		public EmailAddressType[] RedirectToRecipients
		{
			get
			{
				return this.redirectToRecipientsField;
			}
			set
			{
				this.redirectToRecipientsField = value;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x060012E5 RID: 4837 RVA: 0x00024F4E File Offset: 0x0002314E
		// (set) Token: 0x060012E6 RID: 4838 RVA: 0x00024F56 File Offset: 0x00023156
		[XmlArrayItem("Address", IsNullable = false)]
		public EmailAddressType[] SendSMSAlertToRecipients
		{
			get
			{
				return this.sendSMSAlertToRecipientsField;
			}
			set
			{
				this.sendSMSAlertToRecipientsField = value;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x060012E7 RID: 4839 RVA: 0x00024F5F File Offset: 0x0002315F
		// (set) Token: 0x060012E8 RID: 4840 RVA: 0x00024F67 File Offset: 0x00023167
		public ItemIdType ServerReplyWithMessage
		{
			get
			{
				return this.serverReplyWithMessageField;
			}
			set
			{
				this.serverReplyWithMessageField = value;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x060012E9 RID: 4841 RVA: 0x00024F70 File Offset: 0x00023170
		// (set) Token: 0x060012EA RID: 4842 RVA: 0x00024F78 File Offset: 0x00023178
		public bool StopProcessingRules
		{
			get
			{
				return this.stopProcessingRulesField;
			}
			set
			{
				this.stopProcessingRulesField = value;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x00024F81 File Offset: 0x00023181
		// (set) Token: 0x060012EC RID: 4844 RVA: 0x00024F89 File Offset: 0x00023189
		[XmlIgnore]
		public bool StopProcessingRulesSpecified
		{
			get
			{
				return this.stopProcessingRulesFieldSpecified;
			}
			set
			{
				this.stopProcessingRulesFieldSpecified = value;
			}
		}

		// Token: 0x04000D27 RID: 3367
		private string[] assignCategoriesField;

		// Token: 0x04000D28 RID: 3368
		private TargetFolderIdType copyToFolderField;

		// Token: 0x04000D29 RID: 3369
		private bool deleteField;

		// Token: 0x04000D2A RID: 3370
		private bool deleteFieldSpecified;

		// Token: 0x04000D2B RID: 3371
		private EmailAddressType[] forwardAsAttachmentToRecipientsField;

		// Token: 0x04000D2C RID: 3372
		private EmailAddressType[] forwardToRecipientsField;

		// Token: 0x04000D2D RID: 3373
		private ImportanceChoicesType markImportanceField;

		// Token: 0x04000D2E RID: 3374
		private bool markImportanceFieldSpecified;

		// Token: 0x04000D2F RID: 3375
		private bool markAsReadField;

		// Token: 0x04000D30 RID: 3376
		private bool markAsReadFieldSpecified;

		// Token: 0x04000D31 RID: 3377
		private TargetFolderIdType moveToFolderField;

		// Token: 0x04000D32 RID: 3378
		private bool permanentDeleteField;

		// Token: 0x04000D33 RID: 3379
		private bool permanentDeleteFieldSpecified;

		// Token: 0x04000D34 RID: 3380
		private EmailAddressType[] redirectToRecipientsField;

		// Token: 0x04000D35 RID: 3381
		private EmailAddressType[] sendSMSAlertToRecipientsField;

		// Token: 0x04000D36 RID: 3382
		private ItemIdType serverReplyWithMessageField;

		// Token: 0x04000D37 RID: 3383
		private bool stopProcessingRulesField;

		// Token: 0x04000D38 RID: 3384
		private bool stopProcessingRulesFieldSpecified;
	}
}
