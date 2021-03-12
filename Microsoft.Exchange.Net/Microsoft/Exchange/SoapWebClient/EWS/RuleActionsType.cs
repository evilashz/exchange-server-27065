using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000298 RID: 664
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class RuleActionsType
	{
		// Token: 0x04001179 RID: 4473
		[XmlArrayItem("String", IsNullable = false)]
		public string[] AssignCategories;

		// Token: 0x0400117A RID: 4474
		public TargetFolderIdType CopyToFolder;

		// Token: 0x0400117B RID: 4475
		public bool Delete;

		// Token: 0x0400117C RID: 4476
		[XmlIgnore]
		public bool DeleteSpecified;

		// Token: 0x0400117D RID: 4477
		[XmlArrayItem("Address", IsNullable = false)]
		public EmailAddressType[] ForwardAsAttachmentToRecipients;

		// Token: 0x0400117E RID: 4478
		[XmlArrayItem("Address", IsNullable = false)]
		public EmailAddressType[] ForwardToRecipients;

		// Token: 0x0400117F RID: 4479
		public ImportanceChoicesType MarkImportance;

		// Token: 0x04001180 RID: 4480
		[XmlIgnore]
		public bool MarkImportanceSpecified;

		// Token: 0x04001181 RID: 4481
		public bool MarkAsRead;

		// Token: 0x04001182 RID: 4482
		[XmlIgnore]
		public bool MarkAsReadSpecified;

		// Token: 0x04001183 RID: 4483
		public TargetFolderIdType MoveToFolder;

		// Token: 0x04001184 RID: 4484
		public bool PermanentDelete;

		// Token: 0x04001185 RID: 4485
		[XmlIgnore]
		public bool PermanentDeleteSpecified;

		// Token: 0x04001186 RID: 4486
		[XmlArrayItem("Address", IsNullable = false)]
		public EmailAddressType[] RedirectToRecipients;

		// Token: 0x04001187 RID: 4487
		[XmlArrayItem("Address", IsNullable = false)]
		public EmailAddressType[] SendSMSAlertToRecipients;

		// Token: 0x04001188 RID: 4488
		public ItemIdType ServerReplyWithMessage;

		// Token: 0x04001189 RID: 4489
		public bool StopProcessingRules;

		// Token: 0x0400118A RID: 4490
		[XmlIgnore]
		public bool StopProcessingRulesSpecified;
	}
}
