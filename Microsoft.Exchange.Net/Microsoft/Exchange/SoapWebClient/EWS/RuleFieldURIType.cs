using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000290 RID: 656
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum RuleFieldURIType
	{
		// Token: 0x040010AE RID: 4270
		RuleId,
		// Token: 0x040010AF RID: 4271
		DisplayName,
		// Token: 0x040010B0 RID: 4272
		Priority,
		// Token: 0x040010B1 RID: 4273
		IsNotSupported,
		// Token: 0x040010B2 RID: 4274
		Actions,
		// Token: 0x040010B3 RID: 4275
		[XmlEnum("Condition:Categories")]
		ConditionCategories,
		// Token: 0x040010B4 RID: 4276
		[XmlEnum("Condition:ContainsBodyStrings")]
		ConditionContainsBodyStrings,
		// Token: 0x040010B5 RID: 4277
		[XmlEnum("Condition:ContainsHeaderStrings")]
		ConditionContainsHeaderStrings,
		// Token: 0x040010B6 RID: 4278
		[XmlEnum("Condition:ContainsRecipientStrings")]
		ConditionContainsRecipientStrings,
		// Token: 0x040010B7 RID: 4279
		[XmlEnum("Condition:ContainsSenderStrings")]
		ConditionContainsSenderStrings,
		// Token: 0x040010B8 RID: 4280
		[XmlEnum("Condition:ContainsSubjectOrBodyStrings")]
		ConditionContainsSubjectOrBodyStrings,
		// Token: 0x040010B9 RID: 4281
		[XmlEnum("Condition:ContainsSubjectStrings")]
		ConditionContainsSubjectStrings,
		// Token: 0x040010BA RID: 4282
		[XmlEnum("Condition:FlaggedForAction")]
		ConditionFlaggedForAction,
		// Token: 0x040010BB RID: 4283
		[XmlEnum("Condition:FromAddresses")]
		ConditionFromAddresses,
		// Token: 0x040010BC RID: 4284
		[XmlEnum("Condition:FromConnectedAccounts")]
		ConditionFromConnectedAccounts,
		// Token: 0x040010BD RID: 4285
		[XmlEnum("Condition:HasAttachments")]
		ConditionHasAttachments,
		// Token: 0x040010BE RID: 4286
		[XmlEnum("Condition:Importance")]
		ConditionImportance,
		// Token: 0x040010BF RID: 4287
		[XmlEnum("Condition:IsApprovalRequest")]
		ConditionIsApprovalRequest,
		// Token: 0x040010C0 RID: 4288
		[XmlEnum("Condition:IsAutomaticForward")]
		ConditionIsAutomaticForward,
		// Token: 0x040010C1 RID: 4289
		[XmlEnum("Condition:IsAutomaticReply")]
		ConditionIsAutomaticReply,
		// Token: 0x040010C2 RID: 4290
		[XmlEnum("Condition:IsEncrypted")]
		ConditionIsEncrypted,
		// Token: 0x040010C3 RID: 4291
		[XmlEnum("Condition:IsMeetingRequest")]
		ConditionIsMeetingRequest,
		// Token: 0x040010C4 RID: 4292
		[XmlEnum("Condition:IsMeetingResponse")]
		ConditionIsMeetingResponse,
		// Token: 0x040010C5 RID: 4293
		[XmlEnum("Condition:IsNDR")]
		ConditionIsNDR,
		// Token: 0x040010C6 RID: 4294
		[XmlEnum("Condition:IsPermissionControlled")]
		ConditionIsPermissionControlled,
		// Token: 0x040010C7 RID: 4295
		[XmlEnum("Condition:IsReadReceipt")]
		ConditionIsReadReceipt,
		// Token: 0x040010C8 RID: 4296
		[XmlEnum("Condition:IsSigned")]
		ConditionIsSigned,
		// Token: 0x040010C9 RID: 4297
		[XmlEnum("Condition:IsVoicemail")]
		ConditionIsVoicemail,
		// Token: 0x040010CA RID: 4298
		[XmlEnum("Condition:ItemClasses")]
		ConditionItemClasses,
		// Token: 0x040010CB RID: 4299
		[XmlEnum("Condition:MessageClassifications")]
		ConditionMessageClassifications,
		// Token: 0x040010CC RID: 4300
		[XmlEnum("Condition:NotSentToMe")]
		ConditionNotSentToMe,
		// Token: 0x040010CD RID: 4301
		[XmlEnum("Condition:SentCcMe")]
		ConditionSentCcMe,
		// Token: 0x040010CE RID: 4302
		[XmlEnum("Condition:SentOnlyToMe")]
		ConditionSentOnlyToMe,
		// Token: 0x040010CF RID: 4303
		[XmlEnum("Condition:SentToAddresses")]
		ConditionSentToAddresses,
		// Token: 0x040010D0 RID: 4304
		[XmlEnum("Condition:SentToMe")]
		ConditionSentToMe,
		// Token: 0x040010D1 RID: 4305
		[XmlEnum("Condition:SentToOrCcMe")]
		ConditionSentToOrCcMe,
		// Token: 0x040010D2 RID: 4306
		[XmlEnum("Condition:Sensitivity")]
		ConditionSensitivity,
		// Token: 0x040010D3 RID: 4307
		[XmlEnum("Condition:WithinDateRange")]
		ConditionWithinDateRange,
		// Token: 0x040010D4 RID: 4308
		[XmlEnum("Condition:WithinSizeRange")]
		ConditionWithinSizeRange,
		// Token: 0x040010D5 RID: 4309
		[XmlEnum("Exception:Categories")]
		ExceptionCategories,
		// Token: 0x040010D6 RID: 4310
		[XmlEnum("Exception:ContainsBodyStrings")]
		ExceptionContainsBodyStrings,
		// Token: 0x040010D7 RID: 4311
		[XmlEnum("Exception:ContainsHeaderStrings")]
		ExceptionContainsHeaderStrings,
		// Token: 0x040010D8 RID: 4312
		[XmlEnum("Exception:ContainsRecipientStrings")]
		ExceptionContainsRecipientStrings,
		// Token: 0x040010D9 RID: 4313
		[XmlEnum("Exception:ContainsSenderStrings")]
		ExceptionContainsSenderStrings,
		// Token: 0x040010DA RID: 4314
		[XmlEnum("Exception:ContainsSubjectOrBodyStrings")]
		ExceptionContainsSubjectOrBodyStrings,
		// Token: 0x040010DB RID: 4315
		[XmlEnum("Exception:ContainsSubjectStrings")]
		ExceptionContainsSubjectStrings,
		// Token: 0x040010DC RID: 4316
		[XmlEnum("Exception:FlaggedForAction")]
		ExceptionFlaggedForAction,
		// Token: 0x040010DD RID: 4317
		[XmlEnum("Exception:FromAddresses")]
		ExceptionFromAddresses,
		// Token: 0x040010DE RID: 4318
		[XmlEnum("Exception:FromConnectedAccounts")]
		ExceptionFromConnectedAccounts,
		// Token: 0x040010DF RID: 4319
		[XmlEnum("Exception:HasAttachments")]
		ExceptionHasAttachments,
		// Token: 0x040010E0 RID: 4320
		[XmlEnum("Exception:Importance")]
		ExceptionImportance,
		// Token: 0x040010E1 RID: 4321
		[XmlEnum("Exception:IsApprovalRequest")]
		ExceptionIsApprovalRequest,
		// Token: 0x040010E2 RID: 4322
		[XmlEnum("Exception:IsAutomaticForward")]
		ExceptionIsAutomaticForward,
		// Token: 0x040010E3 RID: 4323
		[XmlEnum("Exception:IsAutomaticReply")]
		ExceptionIsAutomaticReply,
		// Token: 0x040010E4 RID: 4324
		[XmlEnum("Exception:IsEncrypted")]
		ExceptionIsEncrypted,
		// Token: 0x040010E5 RID: 4325
		[XmlEnum("Exception:IsMeetingRequest")]
		ExceptionIsMeetingRequest,
		// Token: 0x040010E6 RID: 4326
		[XmlEnum("Exception:IsMeetingResponse")]
		ExceptionIsMeetingResponse,
		// Token: 0x040010E7 RID: 4327
		[XmlEnum("Exception:IsNDR")]
		ExceptionIsNDR,
		// Token: 0x040010E8 RID: 4328
		[XmlEnum("Exception:IsPermissionControlled")]
		ExceptionIsPermissionControlled,
		// Token: 0x040010E9 RID: 4329
		[XmlEnum("Exception:IsReadReceipt")]
		ExceptionIsReadReceipt,
		// Token: 0x040010EA RID: 4330
		[XmlEnum("Exception:IsSigned")]
		ExceptionIsSigned,
		// Token: 0x040010EB RID: 4331
		[XmlEnum("Exception:IsVoicemail")]
		ExceptionIsVoicemail,
		// Token: 0x040010EC RID: 4332
		[XmlEnum("Exception:ItemClasses")]
		ExceptionItemClasses,
		// Token: 0x040010ED RID: 4333
		[XmlEnum("Exception:MessageClassifications")]
		ExceptionMessageClassifications,
		// Token: 0x040010EE RID: 4334
		[XmlEnum("Exception:NotSentToMe")]
		ExceptionNotSentToMe,
		// Token: 0x040010EF RID: 4335
		[XmlEnum("Exception:SentCcMe")]
		ExceptionSentCcMe,
		// Token: 0x040010F0 RID: 4336
		[XmlEnum("Exception:SentOnlyToMe")]
		ExceptionSentOnlyToMe,
		// Token: 0x040010F1 RID: 4337
		[XmlEnum("Exception:SentToAddresses")]
		ExceptionSentToAddresses,
		// Token: 0x040010F2 RID: 4338
		[XmlEnum("Exception:SentToMe")]
		ExceptionSentToMe,
		// Token: 0x040010F3 RID: 4339
		[XmlEnum("Exception:SentToOrCcMe")]
		ExceptionSentToOrCcMe,
		// Token: 0x040010F4 RID: 4340
		[XmlEnum("Exception:Sensitivity")]
		ExceptionSensitivity,
		// Token: 0x040010F5 RID: 4341
		[XmlEnum("Exception:WithinDateRange")]
		ExceptionWithinDateRange,
		// Token: 0x040010F6 RID: 4342
		[XmlEnum("Exception:WithinSizeRange")]
		ExceptionWithinSizeRange,
		// Token: 0x040010F7 RID: 4343
		[XmlEnum("Action:AssignCategories")]
		ActionAssignCategories,
		// Token: 0x040010F8 RID: 4344
		[XmlEnum("Action:CopyToFolder")]
		ActionCopyToFolder,
		// Token: 0x040010F9 RID: 4345
		[XmlEnum("Action:Delete")]
		ActionDelete,
		// Token: 0x040010FA RID: 4346
		[XmlEnum("Action:ForwardAsAttachmentToRecipients")]
		ActionForwardAsAttachmentToRecipients,
		// Token: 0x040010FB RID: 4347
		[XmlEnum("Action:ForwardToRecipients")]
		ActionForwardToRecipients,
		// Token: 0x040010FC RID: 4348
		[XmlEnum("Action:MarkImportance")]
		ActionMarkImportance,
		// Token: 0x040010FD RID: 4349
		[XmlEnum("Action:MarkAsRead")]
		ActionMarkAsRead,
		// Token: 0x040010FE RID: 4350
		[XmlEnum("Action:MoveToFolder")]
		ActionMoveToFolder,
		// Token: 0x040010FF RID: 4351
		[XmlEnum("Action:PermanentDelete")]
		ActionPermanentDelete,
		// Token: 0x04001100 RID: 4352
		[XmlEnum("Action:RedirectToRecipients")]
		ActionRedirectToRecipients,
		// Token: 0x04001101 RID: 4353
		[XmlEnum("Action:SendSMSAlertToRecipients")]
		ActionSendSMSAlertToRecipients,
		// Token: 0x04001102 RID: 4354
		[XmlEnum("Action:ServerReplyWithMessage")]
		ActionServerReplyWithMessage,
		// Token: 0x04001103 RID: 4355
		[XmlEnum("Action:StopProcessingRules")]
		ActionStopProcessingRules,
		// Token: 0x04001104 RID: 4356
		IsEnabled,
		// Token: 0x04001105 RID: 4357
		IsInError,
		// Token: 0x04001106 RID: 4358
		Conditions,
		// Token: 0x04001107 RID: 4359
		Exceptions
	}
}
