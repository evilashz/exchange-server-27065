using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001AF RID: 431
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum RuleFieldURIType
	{
		// Token: 0x04000C5C RID: 3164
		RuleId,
		// Token: 0x04000C5D RID: 3165
		DisplayName,
		// Token: 0x04000C5E RID: 3166
		Priority,
		// Token: 0x04000C5F RID: 3167
		IsNotSupported,
		// Token: 0x04000C60 RID: 3168
		Actions,
		// Token: 0x04000C61 RID: 3169
		[XmlEnum("Condition:Categories")]
		ConditionCategories,
		// Token: 0x04000C62 RID: 3170
		[XmlEnum("Condition:ContainsBodyStrings")]
		ConditionContainsBodyStrings,
		// Token: 0x04000C63 RID: 3171
		[XmlEnum("Condition:ContainsHeaderStrings")]
		ConditionContainsHeaderStrings,
		// Token: 0x04000C64 RID: 3172
		[XmlEnum("Condition:ContainsRecipientStrings")]
		ConditionContainsRecipientStrings,
		// Token: 0x04000C65 RID: 3173
		[XmlEnum("Condition:ContainsSenderStrings")]
		ConditionContainsSenderStrings,
		// Token: 0x04000C66 RID: 3174
		[XmlEnum("Condition:ContainsSubjectOrBodyStrings")]
		ConditionContainsSubjectOrBodyStrings,
		// Token: 0x04000C67 RID: 3175
		[XmlEnum("Condition:ContainsSubjectStrings")]
		ConditionContainsSubjectStrings,
		// Token: 0x04000C68 RID: 3176
		[XmlEnum("Condition:FlaggedForAction")]
		ConditionFlaggedForAction,
		// Token: 0x04000C69 RID: 3177
		[XmlEnum("Condition:FromAddresses")]
		ConditionFromAddresses,
		// Token: 0x04000C6A RID: 3178
		[XmlEnum("Condition:FromConnectedAccounts")]
		ConditionFromConnectedAccounts,
		// Token: 0x04000C6B RID: 3179
		[XmlEnum("Condition:HasAttachments")]
		ConditionHasAttachments,
		// Token: 0x04000C6C RID: 3180
		[XmlEnum("Condition:Importance")]
		ConditionImportance,
		// Token: 0x04000C6D RID: 3181
		[XmlEnum("Condition:IsApprovalRequest")]
		ConditionIsApprovalRequest,
		// Token: 0x04000C6E RID: 3182
		[XmlEnum("Condition:IsAutomaticForward")]
		ConditionIsAutomaticForward,
		// Token: 0x04000C6F RID: 3183
		[XmlEnum("Condition:IsAutomaticReply")]
		ConditionIsAutomaticReply,
		// Token: 0x04000C70 RID: 3184
		[XmlEnum("Condition:IsEncrypted")]
		ConditionIsEncrypted,
		// Token: 0x04000C71 RID: 3185
		[XmlEnum("Condition:IsMeetingRequest")]
		ConditionIsMeetingRequest,
		// Token: 0x04000C72 RID: 3186
		[XmlEnum("Condition:IsMeetingResponse")]
		ConditionIsMeetingResponse,
		// Token: 0x04000C73 RID: 3187
		[XmlEnum("Condition:IsNDR")]
		ConditionIsNDR,
		// Token: 0x04000C74 RID: 3188
		[XmlEnum("Condition:IsPermissionControlled")]
		ConditionIsPermissionControlled,
		// Token: 0x04000C75 RID: 3189
		[XmlEnum("Condition:IsReadReceipt")]
		ConditionIsReadReceipt,
		// Token: 0x04000C76 RID: 3190
		[XmlEnum("Condition:IsSigned")]
		ConditionIsSigned,
		// Token: 0x04000C77 RID: 3191
		[XmlEnum("Condition:IsVoicemail")]
		ConditionIsVoicemail,
		// Token: 0x04000C78 RID: 3192
		[XmlEnum("Condition:ItemClasses")]
		ConditionItemClasses,
		// Token: 0x04000C79 RID: 3193
		[XmlEnum("Condition:MessageClassifications")]
		ConditionMessageClassifications,
		// Token: 0x04000C7A RID: 3194
		[XmlEnum("Condition:NotSentToMe")]
		ConditionNotSentToMe,
		// Token: 0x04000C7B RID: 3195
		[XmlEnum("Condition:SentCcMe")]
		ConditionSentCcMe,
		// Token: 0x04000C7C RID: 3196
		[XmlEnum("Condition:SentOnlyToMe")]
		ConditionSentOnlyToMe,
		// Token: 0x04000C7D RID: 3197
		[XmlEnum("Condition:SentToAddresses")]
		ConditionSentToAddresses,
		// Token: 0x04000C7E RID: 3198
		[XmlEnum("Condition:SentToMe")]
		ConditionSentToMe,
		// Token: 0x04000C7F RID: 3199
		[XmlEnum("Condition:SentToOrCcMe")]
		ConditionSentToOrCcMe,
		// Token: 0x04000C80 RID: 3200
		[XmlEnum("Condition:Sensitivity")]
		ConditionSensitivity,
		// Token: 0x04000C81 RID: 3201
		[XmlEnum("Condition:WithinDateRange")]
		ConditionWithinDateRange,
		// Token: 0x04000C82 RID: 3202
		[XmlEnum("Condition:WithinSizeRange")]
		ConditionWithinSizeRange,
		// Token: 0x04000C83 RID: 3203
		[XmlEnum("Exception:Categories")]
		ExceptionCategories,
		// Token: 0x04000C84 RID: 3204
		[XmlEnum("Exception:ContainsBodyStrings")]
		ExceptionContainsBodyStrings,
		// Token: 0x04000C85 RID: 3205
		[XmlEnum("Exception:ContainsHeaderStrings")]
		ExceptionContainsHeaderStrings,
		// Token: 0x04000C86 RID: 3206
		[XmlEnum("Exception:ContainsRecipientStrings")]
		ExceptionContainsRecipientStrings,
		// Token: 0x04000C87 RID: 3207
		[XmlEnum("Exception:ContainsSenderStrings")]
		ExceptionContainsSenderStrings,
		// Token: 0x04000C88 RID: 3208
		[XmlEnum("Exception:ContainsSubjectOrBodyStrings")]
		ExceptionContainsSubjectOrBodyStrings,
		// Token: 0x04000C89 RID: 3209
		[XmlEnum("Exception:ContainsSubjectStrings")]
		ExceptionContainsSubjectStrings,
		// Token: 0x04000C8A RID: 3210
		[XmlEnum("Exception:FlaggedForAction")]
		ExceptionFlaggedForAction,
		// Token: 0x04000C8B RID: 3211
		[XmlEnum("Exception:FromAddresses")]
		ExceptionFromAddresses,
		// Token: 0x04000C8C RID: 3212
		[XmlEnum("Exception:FromConnectedAccounts")]
		ExceptionFromConnectedAccounts,
		// Token: 0x04000C8D RID: 3213
		[XmlEnum("Exception:HasAttachments")]
		ExceptionHasAttachments,
		// Token: 0x04000C8E RID: 3214
		[XmlEnum("Exception:Importance")]
		ExceptionImportance,
		// Token: 0x04000C8F RID: 3215
		[XmlEnum("Exception:IsApprovalRequest")]
		ExceptionIsApprovalRequest,
		// Token: 0x04000C90 RID: 3216
		[XmlEnum("Exception:IsAutomaticForward")]
		ExceptionIsAutomaticForward,
		// Token: 0x04000C91 RID: 3217
		[XmlEnum("Exception:IsAutomaticReply")]
		ExceptionIsAutomaticReply,
		// Token: 0x04000C92 RID: 3218
		[XmlEnum("Exception:IsEncrypted")]
		ExceptionIsEncrypted,
		// Token: 0x04000C93 RID: 3219
		[XmlEnum("Exception:IsMeetingRequest")]
		ExceptionIsMeetingRequest,
		// Token: 0x04000C94 RID: 3220
		[XmlEnum("Exception:IsMeetingResponse")]
		ExceptionIsMeetingResponse,
		// Token: 0x04000C95 RID: 3221
		[XmlEnum("Exception:IsNDR")]
		ExceptionIsNDR,
		// Token: 0x04000C96 RID: 3222
		[XmlEnum("Exception:IsPermissionControlled")]
		ExceptionIsPermissionControlled,
		// Token: 0x04000C97 RID: 3223
		[XmlEnum("Exception:IsReadReceipt")]
		ExceptionIsReadReceipt,
		// Token: 0x04000C98 RID: 3224
		[XmlEnum("Exception:IsSigned")]
		ExceptionIsSigned,
		// Token: 0x04000C99 RID: 3225
		[XmlEnum("Exception:IsVoicemail")]
		ExceptionIsVoicemail,
		// Token: 0x04000C9A RID: 3226
		[XmlEnum("Exception:ItemClasses")]
		ExceptionItemClasses,
		// Token: 0x04000C9B RID: 3227
		[XmlEnum("Exception:MessageClassifications")]
		ExceptionMessageClassifications,
		// Token: 0x04000C9C RID: 3228
		[XmlEnum("Exception:NotSentToMe")]
		ExceptionNotSentToMe,
		// Token: 0x04000C9D RID: 3229
		[XmlEnum("Exception:SentCcMe")]
		ExceptionSentCcMe,
		// Token: 0x04000C9E RID: 3230
		[XmlEnum("Exception:SentOnlyToMe")]
		ExceptionSentOnlyToMe,
		// Token: 0x04000C9F RID: 3231
		[XmlEnum("Exception:SentToAddresses")]
		ExceptionSentToAddresses,
		// Token: 0x04000CA0 RID: 3232
		[XmlEnum("Exception:SentToMe")]
		ExceptionSentToMe,
		// Token: 0x04000CA1 RID: 3233
		[XmlEnum("Exception:SentToOrCcMe")]
		ExceptionSentToOrCcMe,
		// Token: 0x04000CA2 RID: 3234
		[XmlEnum("Exception:Sensitivity")]
		ExceptionSensitivity,
		// Token: 0x04000CA3 RID: 3235
		[XmlEnum("Exception:WithinDateRange")]
		ExceptionWithinDateRange,
		// Token: 0x04000CA4 RID: 3236
		[XmlEnum("Exception:WithinSizeRange")]
		ExceptionWithinSizeRange,
		// Token: 0x04000CA5 RID: 3237
		[XmlEnum("Action:AssignCategories")]
		ActionAssignCategories,
		// Token: 0x04000CA6 RID: 3238
		[XmlEnum("Action:CopyToFolder")]
		ActionCopyToFolder,
		// Token: 0x04000CA7 RID: 3239
		[XmlEnum("Action:Delete")]
		ActionDelete,
		// Token: 0x04000CA8 RID: 3240
		[XmlEnum("Action:ForwardAsAttachmentToRecipients")]
		ActionForwardAsAttachmentToRecipients,
		// Token: 0x04000CA9 RID: 3241
		[XmlEnum("Action:ForwardToRecipients")]
		ActionForwardToRecipients,
		// Token: 0x04000CAA RID: 3242
		[XmlEnum("Action:MarkImportance")]
		ActionMarkImportance,
		// Token: 0x04000CAB RID: 3243
		[XmlEnum("Action:MarkAsRead")]
		ActionMarkAsRead,
		// Token: 0x04000CAC RID: 3244
		[XmlEnum("Action:MoveToFolder")]
		ActionMoveToFolder,
		// Token: 0x04000CAD RID: 3245
		[XmlEnum("Action:PermanentDelete")]
		ActionPermanentDelete,
		// Token: 0x04000CAE RID: 3246
		[XmlEnum("Action:RedirectToRecipients")]
		ActionRedirectToRecipients,
		// Token: 0x04000CAF RID: 3247
		[XmlEnum("Action:SendSMSAlertToRecipients")]
		ActionSendSMSAlertToRecipients,
		// Token: 0x04000CB0 RID: 3248
		[XmlEnum("Action:ServerReplyWithMessage")]
		ActionServerReplyWithMessage,
		// Token: 0x04000CB1 RID: 3249
		[XmlEnum("Action:StopProcessingRules")]
		ActionStopProcessingRules,
		// Token: 0x04000CB2 RID: 3250
		IsEnabled,
		// Token: 0x04000CB3 RID: 3251
		IsInError,
		// Token: 0x04000CB4 RID: 3252
		Conditions,
		// Token: 0x04000CB5 RID: 3253
		Exceptions
	}
}
