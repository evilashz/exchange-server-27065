using System;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000066 RID: 102
	internal enum EvaluationErrors
	{
		// Token: 0x040000EA RID: 234
		None,
		// Token: 0x040000EB RID: 235
		[LocDescription(Strings.IDs.EvaluationErrorsGenericError)]
		GenericError,
		// Token: 0x040000EC RID: 236
		[LocDescription(Strings.IDs.EvaluationErrorsTimeout)]
		Timeout,
		// Token: 0x040000ED RID: 237
		[LocDescription(Strings.IDs.EvaluationErrorsStaleEvent)]
		StaleEvent,
		// Token: 0x040000EE RID: 238
		[LocDescription(Strings.IDs.EvaluationErrorsMailboxOffline)]
		MailboxOffline,
		// Token: 0x040000EF RID: 239
		[LocDescription(Strings.IDs.EvaluationErrorsAttachmentLimitReached)]
		AttachmentLimitReached,
		// Token: 0x040000F0 RID: 240
		[LocDescription(Strings.IDs.EvaluationErrorsMarsWriterTruncation)]
		MarsWriterTruncation,
		// Token: 0x040000F1 RID: 241
		[LocDescription(Strings.IDs.EvaluationErrorsDocumentParserFailure)]
		DocumentParserFailure,
		// Token: 0x040000F2 RID: 242
		[LocDescription(Strings.IDs.EvaluationErrorsAnnotationTokenError)]
		AnnotationTokenError,
		// Token: 0x040000F3 RID: 243
		[LocDescription(Strings.IDs.EvaluationErrorsPoisonDocument)]
		PoisonDocument,
		// Token: 0x040000F4 RID: 244
		[LocDescription(Strings.IDs.EvaluationErrorsRightsManagementFailure)]
		RightsManagementFailure,
		// Token: 0x040000F5 RID: 245
		[LocDescription(Strings.IDs.EvaluationErrorsSessionUnavailable)]
		SessionUnavailable,
		// Token: 0x040000F6 RID: 246
		[LocDescription(Strings.IDs.EvaluationErrorsMailboxQuarantined)]
		MailboxQuarantined,
		// Token: 0x040000F7 RID: 247
		[LocDescription(Strings.IDs.EvaluationErrorsMailboxLocked)]
		MailboxLocked,
		// Token: 0x040000F8 RID: 248
		[LocDescription(Strings.IDs.EvaluationErrorsNoSupport)]
		MapiNoSupport,
		// Token: 0x040000F9 RID: 249
		[LocDescription(Strings.IDs.EvaluationErrorsLoginFailed)]
		LoginFailed,
		// Token: 0x040000FA RID: 250
		[LocDescription(Strings.IDs.EvaluationErrorsTextConversionFailure)]
		TextConversionFailure,
		// Token: 0x040000FB RID: 251
		MaxFailureId = 200,
		// Token: 0x040000FC RID: 252
		MonitoringFailure,
		// Token: 0x040000FD RID: 253
		IntentionalTestFailure,
		// Token: 0x040000FE RID: 254
		RecrawlWatermark,
		// Token: 0x040000FF RID: 255
		IntentionalTestTransientFailure,
		// Token: 0x04000100 RID: 256
		IntentionalTestItemTruncationFailure,
		// Token: 0x04000101 RID: 257
		NonExistentErrorCode = 999999999
	}
}
