using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200058A RID: 1418
	[XmlType(TypeName = "RuleFieldURIType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public enum RuleFieldURI
	{
		// Token: 0x040018FB RID: 6395
		RuleId,
		// Token: 0x040018FC RID: 6396
		DisplayName,
		// Token: 0x040018FD RID: 6397
		Priority,
		// Token: 0x040018FE RID: 6398
		IsNotSupported,
		// Token: 0x040018FF RID: 6399
		Actions,
		// Token: 0x04001900 RID: 6400
		[XmlEnum("Condition:Categories")]
		ConditionCategories,
		// Token: 0x04001901 RID: 6401
		[XmlEnum("Condition:ContainsBodyStrings")]
		ConditionContainsBodyStrings,
		// Token: 0x04001902 RID: 6402
		[XmlEnum("Condition:ContainsHeaderStrings")]
		ConditionContainsHeaderStrings,
		// Token: 0x04001903 RID: 6403
		[XmlEnum("Condition:ContainsRecipientStrings")]
		ConditionContainsRecipientStrings,
		// Token: 0x04001904 RID: 6404
		[XmlEnum("Condition:ContainsSenderStrings")]
		ConditionContainsSenderStrings,
		// Token: 0x04001905 RID: 6405
		[XmlEnum("Condition:ContainsSubjectOrBodyStrings")]
		ConditionContainsSubjectOrBodyStrings,
		// Token: 0x04001906 RID: 6406
		[XmlEnum("Condition:ContainsSubjectStrings")]
		ConditionContainsSubjectStrings,
		// Token: 0x04001907 RID: 6407
		[XmlEnum("Condition:FlaggedForAction")]
		ConditionFlaggedForAction,
		// Token: 0x04001908 RID: 6408
		[XmlEnum("Condition:FromAddresses")]
		ConditionFromAddresses,
		// Token: 0x04001909 RID: 6409
		[XmlEnum("Condition:FromConnectedAccounts")]
		ConditionFromConnectedAccounts,
		// Token: 0x0400190A RID: 6410
		[XmlEnum("Condition:HasAttachments")]
		ConditionHasAttachments,
		// Token: 0x0400190B RID: 6411
		[XmlEnum("Condition:Importance")]
		ConditionImportance,
		// Token: 0x0400190C RID: 6412
		[XmlEnum("Condition:IsApprovalRequest")]
		ConditionIsApprovalRequest,
		// Token: 0x0400190D RID: 6413
		[XmlEnum("Condition:IsAutomaticForward")]
		ConditionIsAutomaticForward,
		// Token: 0x0400190E RID: 6414
		[XmlEnum("Condition:IsAutomaticReply")]
		ConditionIsAutomaticReply,
		// Token: 0x0400190F RID: 6415
		[XmlEnum("Condition:IsEncrypted")]
		ConditionIsEncrypted,
		// Token: 0x04001910 RID: 6416
		[XmlEnum("Condition:IsMeetingRequest")]
		ConditionIsMeetingRequest,
		// Token: 0x04001911 RID: 6417
		[XmlEnum("Condition:IsMeetingResponse")]
		ConditionIsMeetingResponse,
		// Token: 0x04001912 RID: 6418
		[XmlEnum("Condition:IsNDR")]
		ConditionIsNDR,
		// Token: 0x04001913 RID: 6419
		[XmlEnum("Condition:IsPermissionControlled")]
		ConditionIsPermissionControlled,
		// Token: 0x04001914 RID: 6420
		[XmlEnum("Condition:IsReadReceipt")]
		ConditionIsReadReceipt,
		// Token: 0x04001915 RID: 6421
		[XmlEnum("Condition:IsSigned")]
		ConditionIsSigned,
		// Token: 0x04001916 RID: 6422
		[XmlEnum("Condition:IsVoicemail")]
		ConditionIsVoicemail,
		// Token: 0x04001917 RID: 6423
		[XmlEnum("Condition:ItemClasses")]
		ConditionItemClasses,
		// Token: 0x04001918 RID: 6424
		[XmlEnum("Condition:MessageClassifications")]
		ConditionMessageClassifications,
		// Token: 0x04001919 RID: 6425
		[XmlEnum("Condition:NotSentToMe")]
		ConditionNotSentToMe,
		// Token: 0x0400191A RID: 6426
		[XmlEnum("Condition:SentCcMe")]
		ConditionSentCcMe,
		// Token: 0x0400191B RID: 6427
		[XmlEnum("Condition:SentOnlyToMe")]
		ConditionSentOnlyToMe,
		// Token: 0x0400191C RID: 6428
		[XmlEnum("Condition:SentToAddresses")]
		ConditionSentToAddresses,
		// Token: 0x0400191D RID: 6429
		[XmlEnum("Condition:SentToMe")]
		ConditionSentToMe,
		// Token: 0x0400191E RID: 6430
		[XmlEnum("Condition:SentToOrCcMe")]
		ConditionSentToOrCcMe,
		// Token: 0x0400191F RID: 6431
		[XmlEnum("Condition:Sensitivity")]
		ConditionSensitivity,
		// Token: 0x04001920 RID: 6432
		[XmlEnum("Condition:WithinDateRange")]
		ConditionWithinDateRange,
		// Token: 0x04001921 RID: 6433
		[XmlEnum("Condition:WithinSizeRange")]
		ConditionWithinSizeRange,
		// Token: 0x04001922 RID: 6434
		[XmlEnum("Exception:Categories")]
		ExceptionCategories,
		// Token: 0x04001923 RID: 6435
		[XmlEnum("Exception:ContainsBodyStrings")]
		ExceptionContainsBodyStrings,
		// Token: 0x04001924 RID: 6436
		[XmlEnum("Exception:ContainsHeaderStrings")]
		ExceptionContainsHeaderStrings,
		// Token: 0x04001925 RID: 6437
		[XmlEnum("Exception:ContainsRecipientStrings")]
		ExceptionContainsRecipientStrings,
		// Token: 0x04001926 RID: 6438
		[XmlEnum("Exception:ContainsSenderStrings")]
		ExceptionContainsSenderStrings,
		// Token: 0x04001927 RID: 6439
		[XmlEnum("Exception:ContainsSubjectOrBodyStrings")]
		ExceptionContainsSubjectOrBodyStrings,
		// Token: 0x04001928 RID: 6440
		[XmlEnum("Exception:ContainsSubjectStrings")]
		ExceptionContainsSubjectStrings,
		// Token: 0x04001929 RID: 6441
		[XmlEnum("Exception:FlaggedForAction")]
		ExceptionFlaggedForAction,
		// Token: 0x0400192A RID: 6442
		[XmlEnum("Exception:FromAddresses")]
		ExceptionFromAddresses,
		// Token: 0x0400192B RID: 6443
		[XmlEnum("Exception:FromConnectedAccounts")]
		ExceptionFromConnectedAccounts,
		// Token: 0x0400192C RID: 6444
		[XmlEnum("Exception:HasAttachments")]
		ExceptionHasAttachments,
		// Token: 0x0400192D RID: 6445
		[XmlEnum("Exception:Importance")]
		ExceptionImportance,
		// Token: 0x0400192E RID: 6446
		[XmlEnum("Exception:IsApprovalRequest")]
		ExceptionIsApprovalRequest,
		// Token: 0x0400192F RID: 6447
		[XmlEnum("Exception:IsAutomaticForward")]
		ExceptionIsAutomaticForward,
		// Token: 0x04001930 RID: 6448
		[XmlEnum("Exception:IsAutomaticReply")]
		ExceptionIsAutomaticReply,
		// Token: 0x04001931 RID: 6449
		[XmlEnum("Exception:IsEncrypted")]
		ExceptionIsEncrypted,
		// Token: 0x04001932 RID: 6450
		[XmlEnum("Exception:IsMeetingRequest")]
		ExceptionIsMeetingRequest,
		// Token: 0x04001933 RID: 6451
		[XmlEnum("Exception:IsMeetingResponse")]
		ExceptionIsMeetingResponse,
		// Token: 0x04001934 RID: 6452
		[XmlEnum("Exception:IsNDR")]
		ExceptionIsNDR,
		// Token: 0x04001935 RID: 6453
		[XmlEnum("Exception:IsPermissionControlled")]
		ExceptionIsPermissionControlled,
		// Token: 0x04001936 RID: 6454
		[XmlEnum("Exception:IsReadReceipt")]
		ExceptionIsReadReceipt,
		// Token: 0x04001937 RID: 6455
		[XmlEnum("Exception:IsSigned")]
		ExceptionIsSigned,
		// Token: 0x04001938 RID: 6456
		[XmlEnum("Exception:IsVoicemail")]
		ExceptionIsVoicemail,
		// Token: 0x04001939 RID: 6457
		[XmlEnum("Exception:ItemClasses")]
		ExceptionItemClasses,
		// Token: 0x0400193A RID: 6458
		[XmlEnum("Exception:MessageClassifications")]
		ExceptionMessageClassifications,
		// Token: 0x0400193B RID: 6459
		[XmlEnum("Exception:NotSentToMe")]
		ExceptionNotSentToMe,
		// Token: 0x0400193C RID: 6460
		[XmlEnum("Exception:SentCcMe")]
		ExceptionSentCcMe,
		// Token: 0x0400193D RID: 6461
		[XmlEnum("Exception:SentOnlyToMe")]
		ExceptionSentOnlyToMe,
		// Token: 0x0400193E RID: 6462
		[XmlEnum("Exception:SentToAddresses")]
		ExceptionSentToAddresses,
		// Token: 0x0400193F RID: 6463
		[XmlEnum("Exception:SentToMe")]
		ExceptionSentToMe,
		// Token: 0x04001940 RID: 6464
		[XmlEnum("Exception:SentToOrCcMe")]
		ExceptionSentToOrCcMe,
		// Token: 0x04001941 RID: 6465
		[XmlEnum("Exception:Sensitivity")]
		ExceptionSensitivity,
		// Token: 0x04001942 RID: 6466
		[XmlEnum("Exception:WithinDateRange")]
		ExceptionWithinDateRange,
		// Token: 0x04001943 RID: 6467
		[XmlEnum("Exception:WithinSizeRange")]
		ExceptionWithinSizeRange,
		// Token: 0x04001944 RID: 6468
		[XmlEnum("Action:AssignCategories")]
		ActionAssignCategories,
		// Token: 0x04001945 RID: 6469
		[XmlEnum("Action:CopyToFolder")]
		ActionCopyToFolder,
		// Token: 0x04001946 RID: 6470
		[XmlEnum("Action:Delete")]
		ActionDelete,
		// Token: 0x04001947 RID: 6471
		[XmlEnum("Action:ForwardAsAttachmentToRecipients")]
		ActionForwardAsAttachmentToRecipients,
		// Token: 0x04001948 RID: 6472
		[XmlEnum("Action:ForwardToRecipients")]
		ActionForwardToRecipients,
		// Token: 0x04001949 RID: 6473
		[XmlEnum("Action:MarkImportance")]
		ActionMarkImportance,
		// Token: 0x0400194A RID: 6474
		[XmlEnum("Action:MarkAsRead")]
		ActionMarkAsRead,
		// Token: 0x0400194B RID: 6475
		[XmlEnum("Action:MoveToFolder")]
		ActionMoveToFolder,
		// Token: 0x0400194C RID: 6476
		[XmlEnum("Action:PermanentDelete")]
		ActionPermanentDelete,
		// Token: 0x0400194D RID: 6477
		[XmlEnum("Action:RedirectToRecipients")]
		ActionRedirectToRecipients,
		// Token: 0x0400194E RID: 6478
		[XmlEnum("Action:SendSMSAlertToRecipients")]
		ActionSendSMSAlertToRecipients,
		// Token: 0x0400194F RID: 6479
		[XmlEnum("Action:ServerReplyWithMessage")]
		ActionServerReplyWithMessage,
		// Token: 0x04001950 RID: 6480
		[XmlEnum("Action:StopProcessingRules")]
		ActionStopProcessingRules,
		// Token: 0x04001951 RID: 6481
		IsEnabled,
		// Token: 0x04001952 RID: 6482
		IsInError,
		// Token: 0x04001953 RID: 6483
		Conditions,
		// Token: 0x04001954 RID: 6484
		Exceptions
	}
}
