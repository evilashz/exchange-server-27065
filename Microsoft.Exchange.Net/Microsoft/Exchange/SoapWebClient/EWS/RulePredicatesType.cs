using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000294 RID: 660
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class RulePredicatesType
	{
		// Token: 0x0400112F RID: 4399
		[XmlArrayItem("String", IsNullable = false)]
		public string[] Categories;

		// Token: 0x04001130 RID: 4400
		[XmlArrayItem("String", IsNullable = false)]
		public string[] ContainsBodyStrings;

		// Token: 0x04001131 RID: 4401
		[XmlArrayItem("String", IsNullable = false)]
		public string[] ContainsHeaderStrings;

		// Token: 0x04001132 RID: 4402
		[XmlArrayItem("String", IsNullable = false)]
		public string[] ContainsRecipientStrings;

		// Token: 0x04001133 RID: 4403
		[XmlArrayItem("String", IsNullable = false)]
		public string[] ContainsSenderStrings;

		// Token: 0x04001134 RID: 4404
		[XmlArrayItem("String", IsNullable = false)]
		public string[] ContainsSubjectOrBodyStrings;

		// Token: 0x04001135 RID: 4405
		[XmlArrayItem("String", IsNullable = false)]
		public string[] ContainsSubjectStrings;

		// Token: 0x04001136 RID: 4406
		public FlaggedForActionType FlaggedForAction;

		// Token: 0x04001137 RID: 4407
		[XmlIgnore]
		public bool FlaggedForActionSpecified;

		// Token: 0x04001138 RID: 4408
		[XmlArrayItem("Address", IsNullable = false)]
		public EmailAddressType[] FromAddresses;

		// Token: 0x04001139 RID: 4409
		[XmlArrayItem("String", IsNullable = false)]
		public string[] FromConnectedAccounts;

		// Token: 0x0400113A RID: 4410
		public bool HasAttachments;

		// Token: 0x0400113B RID: 4411
		[XmlIgnore]
		public bool HasAttachmentsSpecified;

		// Token: 0x0400113C RID: 4412
		public ImportanceChoicesType Importance;

		// Token: 0x0400113D RID: 4413
		[XmlIgnore]
		public bool ImportanceSpecified;

		// Token: 0x0400113E RID: 4414
		public bool IsApprovalRequest;

		// Token: 0x0400113F RID: 4415
		[XmlIgnore]
		public bool IsApprovalRequestSpecified;

		// Token: 0x04001140 RID: 4416
		public bool IsAutomaticForward;

		// Token: 0x04001141 RID: 4417
		[XmlIgnore]
		public bool IsAutomaticForwardSpecified;

		// Token: 0x04001142 RID: 4418
		public bool IsAutomaticReply;

		// Token: 0x04001143 RID: 4419
		[XmlIgnore]
		public bool IsAutomaticReplySpecified;

		// Token: 0x04001144 RID: 4420
		public bool IsEncrypted;

		// Token: 0x04001145 RID: 4421
		[XmlIgnore]
		public bool IsEncryptedSpecified;

		// Token: 0x04001146 RID: 4422
		public bool IsMeetingRequest;

		// Token: 0x04001147 RID: 4423
		[XmlIgnore]
		public bool IsMeetingRequestSpecified;

		// Token: 0x04001148 RID: 4424
		public bool IsMeetingResponse;

		// Token: 0x04001149 RID: 4425
		[XmlIgnore]
		public bool IsMeetingResponseSpecified;

		// Token: 0x0400114A RID: 4426
		public bool IsNDR;

		// Token: 0x0400114B RID: 4427
		[XmlIgnore]
		public bool IsNDRSpecified;

		// Token: 0x0400114C RID: 4428
		public bool IsPermissionControlled;

		// Token: 0x0400114D RID: 4429
		[XmlIgnore]
		public bool IsPermissionControlledSpecified;

		// Token: 0x0400114E RID: 4430
		public bool IsReadReceipt;

		// Token: 0x0400114F RID: 4431
		[XmlIgnore]
		public bool IsReadReceiptSpecified;

		// Token: 0x04001150 RID: 4432
		public bool IsSigned;

		// Token: 0x04001151 RID: 4433
		[XmlIgnore]
		public bool IsSignedSpecified;

		// Token: 0x04001152 RID: 4434
		public bool IsVoicemail;

		// Token: 0x04001153 RID: 4435
		[XmlIgnore]
		public bool IsVoicemailSpecified;

		// Token: 0x04001154 RID: 4436
		[XmlArrayItem("String", IsNullable = false)]
		public string[] ItemClasses;

		// Token: 0x04001155 RID: 4437
		[XmlArrayItem("String", IsNullable = false)]
		public string[] MessageClassifications;

		// Token: 0x04001156 RID: 4438
		public bool NotSentToMe;

		// Token: 0x04001157 RID: 4439
		[XmlIgnore]
		public bool NotSentToMeSpecified;

		// Token: 0x04001158 RID: 4440
		public bool SentCcMe;

		// Token: 0x04001159 RID: 4441
		[XmlIgnore]
		public bool SentCcMeSpecified;

		// Token: 0x0400115A RID: 4442
		public bool SentOnlyToMe;

		// Token: 0x0400115B RID: 4443
		[XmlIgnore]
		public bool SentOnlyToMeSpecified;

		// Token: 0x0400115C RID: 4444
		[XmlArrayItem("Address", IsNullable = false)]
		public EmailAddressType[] SentToAddresses;

		// Token: 0x0400115D RID: 4445
		public bool SentToMe;

		// Token: 0x0400115E RID: 4446
		[XmlIgnore]
		public bool SentToMeSpecified;

		// Token: 0x0400115F RID: 4447
		public bool SentToOrCcMe;

		// Token: 0x04001160 RID: 4448
		[XmlIgnore]
		public bool SentToOrCcMeSpecified;

		// Token: 0x04001161 RID: 4449
		public SensitivityChoicesType Sensitivity;

		// Token: 0x04001162 RID: 4450
		[XmlIgnore]
		public bool SensitivitySpecified;

		// Token: 0x04001163 RID: 4451
		public RulePredicateDateRangeType WithinDateRange;

		// Token: 0x04001164 RID: 4452
		public RulePredicateSizeRangeType WithinSizeRange;
	}
}
