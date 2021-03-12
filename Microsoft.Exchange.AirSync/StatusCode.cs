using System;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000250 RID: 592
	internal enum StatusCode
	{
		// Token: 0x04000CB0 RID: 3248
		None,
		// Token: 0x04000CB1 RID: 3249
		Success,
		// Token: 0x04000CB2 RID: 3250
		Sync_Success = 1,
		// Token: 0x04000CB3 RID: 3251
		Sync_ProtocolVersionMismatch,
		// Token: 0x04000CB4 RID: 3252
		Sync_InvalidSyncKey,
		// Token: 0x04000CB5 RID: 3253
		Sync_ProtocolError,
		// Token: 0x04000CB6 RID: 3254
		Sync_ServerError,
		// Token: 0x04000CB7 RID: 3255
		Sync_ClientServerConversion,
		// Token: 0x04000CB8 RID: 3256
		Sync_Conflict,
		// Token: 0x04000CB9 RID: 3257
		Sync_ObjectNotFound,
		// Token: 0x04000CBA RID: 3258
		Sync_OutOfDisk,
		// Token: 0x04000CBB RID: 3259
		Sync_NotificationGUID,
		// Token: 0x04000CBC RID: 3260
		Sync_NotificationsNotProvisioned,
		// Token: 0x04000CBD RID: 3261
		Sync_FolderHierarchyRequired,
		// Token: 0x04000CBE RID: 3262
		Sync_InvalidParameters,
		// Token: 0x04000CBF RID: 3263
		Sync_InvalidWaitTime,
		// Token: 0x04000CC0 RID: 3264
		Sync_TooManyFolders,
		// Token: 0x04000CC1 RID: 3265
		Sync_Retry,
		// Token: 0x04000CC2 RID: 3266
		GetItemEstimate_Success = 1,
		// Token: 0x04000CC3 RID: 3267
		GetItemEstimate_InvalidCollection,
		// Token: 0x04000CC4 RID: 3268
		GetItemEstimate_UnprimedSyncState,
		// Token: 0x04000CC5 RID: 3269
		GetItemEstimate_InvalidSyncKey,
		// Token: 0x04000CC6 RID: 3270
		FolderCmd_Success = 1,
		// Token: 0x04000CC7 RID: 3271
		FolderCmd_FolderAlreadyExistsError,
		// Token: 0x04000CC8 RID: 3272
		FolderCmd_SpecialFolderError,
		// Token: 0x04000CC9 RID: 3273
		FolderCmd_FolderNotFoundError,
		// Token: 0x04000CCA RID: 3274
		FolderCmd_ParentNotFoundError,
		// Token: 0x04000CCB RID: 3275
		FolderCmd_ExchangeServerError,
		// Token: 0x04000CCC RID: 3276
		FolderCmd_AccessDeniedError,
		// Token: 0x04000CCD RID: 3277
		FolderCmd_TimeOutError,
		// Token: 0x04000CCE RID: 3278
		FolderCmd_WrongSyncKeyError,
		// Token: 0x04000CCF RID: 3279
		FolderCmd_MisformattedRequestError,
		// Token: 0x04000CD0 RID: 3280
		FolderCmd_UnknownError,
		// Token: 0x04000CD1 RID: 3281
		MoveItems_InvalidSourceCollectionId = 1,
		// Token: 0x04000CD2 RID: 3282
		MoveItems_InvalidDestinationCollectionId,
		// Token: 0x04000CD3 RID: 3283
		MoveItems_Success,
		// Token: 0x04000CD4 RID: 3284
		MoveItems_SameSourceAndDestinationIds,
		// Token: 0x04000CD5 RID: 3285
		MoveItems_FailureInMoveOperation,
		// Token: 0x04000CD6 RID: 3286
		MoveItems_ExistingItemWithSameNameAtDestination,
		// Token: 0x04000CD7 RID: 3287
		MoveItems_LockedSourceOrDestinationItem,
		// Token: 0x04000CD8 RID: 3288
		MeetingResponse_Success = 1,
		// Token: 0x04000CD9 RID: 3289
		MeetingResponse_InvalidMeetingRequest,
		// Token: 0x04000CDA RID: 3290
		MeetingResponse_ErrorOccurredOnMailbox,
		// Token: 0x04000CDB RID: 3291
		MeetingResponse_ErrorOccurredOnExchangeServer,
		// Token: 0x04000CDC RID: 3292
		Search_Success = 1,
		// Token: 0x04000CDD RID: 3293
		Search_ProtocolError,
		// Token: 0x04000CDE RID: 3294
		Search_ServerError,
		// Token: 0x04000CDF RID: 3295
		Search_BadLink,
		// Token: 0x04000CE0 RID: 3296
		Search_AccessDenied,
		// Token: 0x04000CE1 RID: 3297
		Search_NotFound,
		// Token: 0x04000CE2 RID: 3298
		Search_ConnectionFailed,
		// Token: 0x04000CE3 RID: 3299
		Search_TooComplex,
		// Token: 0x04000CE4 RID: 3300
		Search_IndexNotLoaded,
		// Token: 0x04000CE5 RID: 3301
		Search_TimeOut,
		// Token: 0x04000CE6 RID: 3302
		Search_NeedToFolderSync,
		// Token: 0x04000CE7 RID: 3303
		Search_EndOfRetrieveableRangeWarning,
		// Token: 0x04000CE8 RID: 3304
		Search_AccessBlocked,
		// Token: 0x04000CE9 RID: 3305
		Search_CredentialsRequired,
		// Token: 0x04000CEA RID: 3306
		Settings_Success = 1,
		// Token: 0x04000CEB RID: 3307
		Settings_ProtocolError,
		// Token: 0x04000CEC RID: 3308
		Settings_AccessDenied,
		// Token: 0x04000CED RID: 3309
		Settings_ServerUnavailable,
		// Token: 0x04000CEE RID: 3310
		Settings_InvalidArguments,
		// Token: 0x04000CEF RID: 3311
		Settings_ConflictingArguments,
		// Token: 0x04000CF0 RID: 3312
		Settings_DeniedByPolicy,
		// Token: 0x04000CF1 RID: 3313
		Ping_NoChanges = 1,
		// Token: 0x04000CF2 RID: 3314
		Ping_Changes,
		// Token: 0x04000CF3 RID: 3315
		Ping_SendParameters,
		// Token: 0x04000CF4 RID: 3316
		Ping_Protocol,
		// Token: 0x04000CF5 RID: 3317
		Ping_HbiOutOfRange,
		// Token: 0x04000CF6 RID: 3318
		Ping_FoldersOutOfRange,
		// Token: 0x04000CF7 RID: 3319
		Ping_FolderSyncRequired,
		// Token: 0x04000CF8 RID: 3320
		Ping_ServerError,
		// Token: 0x04000CF9 RID: 3321
		ItemOperations_Success = 1,
		// Token: 0x04000CFA RID: 3322
		ItemOperations_ProtocolError,
		// Token: 0x04000CFB RID: 3323
		ItemOperations_ServerError,
		// Token: 0x04000CFC RID: 3324
		ItemOperations_BadLink,
		// Token: 0x04000CFD RID: 3325
		ItemOperations_AccessDenied,
		// Token: 0x04000CFE RID: 3326
		ItemOperations_NotFound,
		// Token: 0x04000CFF RID: 3327
		ItemOperations_ConnectionFailed,
		// Token: 0x04000D00 RID: 3328
		ItemOperations_OutOfRange,
		// Token: 0x04000D01 RID: 3329
		ItemOperations_UnknownStore,
		// Token: 0x04000D02 RID: 3330
		ItemOperations_FileIsEmpty,
		// Token: 0x04000D03 RID: 3331
		ItemOperations_DataTooLarge,
		// Token: 0x04000D04 RID: 3332
		ItemOperations_IOFailure,
		// Token: 0x04000D05 RID: 3333
		ItemOperations_InvalidBodyPreference,
		// Token: 0x04000D06 RID: 3334
		ItemOperations_ConversionFailed,
		// Token: 0x04000D07 RID: 3335
		ItemOperations_InvalidAttachmentId,
		// Token: 0x04000D08 RID: 3336
		ItemOperations_AccessBlocked,
		// Token: 0x04000D09 RID: 3337
		ItemOperations_PartialSuccess,
		// Token: 0x04000D0A RID: 3338
		ItemOperations_CredentialsRequired,
		// Token: 0x04000D0B RID: 3339
		Provision_Success = 1,
		// Token: 0x04000D0C RID: 3340
		Provision_ProtocolError,
		// Token: 0x04000D0D RID: 3341
		Provision_ServerError,
		// Token: 0x04000D0E RID: 3342
		ResolveRecipients_Success = 1,
		// Token: 0x04000D0F RID: 3343
		ResolveRecipients_RecipientIsAmbiguous,
		// Token: 0x04000D10 RID: 3344
		ResolveRecipients_RecipientIsAmbiguousPartialList,
		// Token: 0x04000D11 RID: 3345
		ResolveRecipients_RecipientNotFound,
		// Token: 0x04000D12 RID: 3346
		ResolveRecipients_ProtocolError,
		// Token: 0x04000D13 RID: 3347
		ResolveRecipients_ServerError,
		// Token: 0x04000D14 RID: 3348
		ResolveRecipients_NoCertificate,
		// Token: 0x04000D15 RID: 3349
		ResolveRecipients_GlobalLimitHit,
		// Token: 0x04000D16 RID: 3350
		ResolveRecipients_CertificateEnumerationFailure,
		// Token: 0x04000D17 RID: 3351
		ValidateCert_Success = 1,
		// Token: 0x04000D18 RID: 3352
		ValidateCert_ProtocolError,
		// Token: 0x04000D19 RID: 3353
		ValidateCert_SignatureNotValidated,
		// Token: 0x04000D1A RID: 3354
		ValidateCert_FromUntrustedSource,
		// Token: 0x04000D1B RID: 3355
		ValidateCert_InvalidCertChain,
		// Token: 0x04000D1C RID: 3356
		ValidateCert_InvalidForSigning,
		// Token: 0x04000D1D RID: 3357
		ValidateCert_ExpiredOrInvalid,
		// Token: 0x04000D1E RID: 3358
		ValidateCert_InvalidTimePeriods,
		// Token: 0x04000D1F RID: 3359
		ValidateCert_PurposeError,
		// Token: 0x04000D20 RID: 3360
		ValidateCert_MissingInfo,
		// Token: 0x04000D21 RID: 3361
		ValidateCert_WrongRole,
		// Token: 0x04000D22 RID: 3362
		ValidateCert_NotMatch,
		// Token: 0x04000D23 RID: 3363
		ValidateCert_Revoked,
		// Token: 0x04000D24 RID: 3364
		ValidateCert_NoServerContact,
		// Token: 0x04000D25 RID: 3365
		ValidateCert_ChainRevoked,
		// Token: 0x04000D26 RID: 3366
		ValidateCert_NoRevocationStatus,
		// Token: 0x04000D27 RID: 3367
		ValidateCert_UnknowServerError,
		// Token: 0x04000D28 RID: 3368
		First140Error = 101,
		// Token: 0x04000D29 RID: 3369
		InvalidContent = 101,
		// Token: 0x04000D2A RID: 3370
		InvalidWBXML,
		// Token: 0x04000D2B RID: 3371
		InvalidXML,
		// Token: 0x04000D2C RID: 3372
		InvalidDateTime,
		// Token: 0x04000D2D RID: 3373
		InvalidCombinationOfIDs,
		// Token: 0x04000D2E RID: 3374
		InvalidIDs,
		// Token: 0x04000D2F RID: 3375
		InvalidMIME,
		// Token: 0x04000D30 RID: 3376
		DeviceIdMissingOrInvalid,
		// Token: 0x04000D31 RID: 3377
		DeviceTypeMissingOrInvalid,
		// Token: 0x04000D32 RID: 3378
		ServerError,
		// Token: 0x04000D33 RID: 3379
		ServerErrorRetryLater,
		// Token: 0x04000D34 RID: 3380
		ActiveDirectoryAccessDenied,
		// Token: 0x04000D35 RID: 3381
		MailboxQuotaExceeded,
		// Token: 0x04000D36 RID: 3382
		MailboxServerOffline,
		// Token: 0x04000D37 RID: 3383
		SendQuotaExceeded,
		// Token: 0x04000D38 RID: 3384
		MessageRecipientUnresolved,
		// Token: 0x04000D39 RID: 3385
		MessageReplyNotAllowed,
		// Token: 0x04000D3A RID: 3386
		MessagePreviouslySent,
		// Token: 0x04000D3B RID: 3387
		MessageHasNoRecipient,
		// Token: 0x04000D3C RID: 3388
		MailSubmissionFailed,
		// Token: 0x04000D3D RID: 3389
		MessageReplyFailed,
		// Token: 0x04000D3E RID: 3390
		AttachmentIsTooLarge,
		// Token: 0x04000D3F RID: 3391
		UserHasNoMailbox,
		// Token: 0x04000D40 RID: 3392
		UserCannotBeAnonymous,
		// Token: 0x04000D41 RID: 3393
		UserPrincipalCouldNotBeFound,
		// Token: 0x04000D42 RID: 3394
		UserDisabledForSync,
		// Token: 0x04000D43 RID: 3395
		UserOnNewMailboxCannotSync,
		// Token: 0x04000D44 RID: 3396
		UserOnLegacyMailboxCannotSync,
		// Token: 0x04000D45 RID: 3397
		DeviceIsBlockedForThisUser,
		// Token: 0x04000D46 RID: 3398
		AccessDenied,
		// Token: 0x04000D47 RID: 3399
		AccountDisabled,
		// Token: 0x04000D48 RID: 3400
		SyncStateNotFound,
		// Token: 0x04000D49 RID: 3401
		SyncStateLocked,
		// Token: 0x04000D4A RID: 3402
		SyncStateCorrupt,
		// Token: 0x04000D4B RID: 3403
		SyncStateAlreadyExists,
		// Token: 0x04000D4C RID: 3404
		SyncStateVersionInvalid,
		// Token: 0x04000D4D RID: 3405
		CommandNotSupported,
		// Token: 0x04000D4E RID: 3406
		VersionNotSupported,
		// Token: 0x04000D4F RID: 3407
		DeviceNotFullyProvisionable,
		// Token: 0x04000D50 RID: 3408
		RemoteWipeRequested,
		// Token: 0x04000D51 RID: 3409
		LegacyDeviceOnStrictPolicy,
		// Token: 0x04000D52 RID: 3410
		DeviceNotProvisioned,
		// Token: 0x04000D53 RID: 3411
		PolicyRefresh,
		// Token: 0x04000D54 RID: 3412
		InvalidPolicyKey,
		// Token: 0x04000D55 RID: 3413
		ExternallyManagedDevicesNotAllowed,
		// Token: 0x04000D56 RID: 3414
		NoRecurrenceInCalendar,
		// Token: 0x04000D57 RID: 3415
		UnexpectedItemClass,
		// Token: 0x04000D58 RID: 3416
		RemoteServerHasNoSSL,
		// Token: 0x04000D59 RID: 3417
		InvalidStoredRequest,
		// Token: 0x04000D5A RID: 3418
		ItemNotFound,
		// Token: 0x04000D5B RID: 3419
		TooManyFolders,
		// Token: 0x04000D5C RID: 3420
		NoFoldersFound,
		// Token: 0x04000D5D RID: 3421
		ItemsLostAfterMove,
		// Token: 0x04000D5E RID: 3422
		FailureInMoveOperation,
		// Token: 0x04000D5F RID: 3423
		MoveCommandDisallowedForNonPersistentMoveAction,
		// Token: 0x04000D60 RID: 3424
		MoveCommandInvalidDestinationFolder,
		// Token: 0x04000D61 RID: 3425
		AvailabilityTooManyRecipients = 160,
		// Token: 0x04000D62 RID: 3426
		AvailabilityDLLimitReached,
		// Token: 0x04000D63 RID: 3427
		AvailabilityTransientFailure,
		// Token: 0x04000D64 RID: 3428
		AvailabilityFailure,
		// Token: 0x04000D65 RID: 3429
		LastKnown140Error = 163,
		// Token: 0x04000D66 RID: 3430
		BodyPartPreferenceTypeNotSupported,
		// Token: 0x04000D67 RID: 3431
		DeviceInformationRequired,
		// Token: 0x04000D68 RID: 3432
		InvalidAccountId,
		// Token: 0x04000D69 RID: 3433
		AccountSendDisabled,
		// Token: 0x04000D6A RID: 3434
		IRM_FeatureDisabled,
		// Token: 0x04000D6B RID: 3435
		IRM_TransientError,
		// Token: 0x04000D6C RID: 3436
		IRM_PermanentError,
		// Token: 0x04000D6D RID: 3437
		IRM_InvalidTemplateID,
		// Token: 0x04000D6E RID: 3438
		IRM_OperationNotPermitted,
		// Token: 0x04000D6F RID: 3439
		NoPicture,
		// Token: 0x04000D70 RID: 3440
		PictureTooLarge,
		// Token: 0x04000D71 RID: 3441
		PictureLimitReached,
		// Token: 0x04000D72 RID: 3442
		BodyPart_ConversationTooLarge,
		// Token: 0x04000D73 RID: 3443
		MaximumDevicesReached,
		// Token: 0x04000D74 RID: 3444
		LastKnown141Error = 177,
		// Token: 0x04000D75 RID: 3445
		InvalidMimeBodyCombination,
		// Token: 0x04000D76 RID: 3446
		InvalidSmartForwardParameters,
		// Token: 0x04000D77 RID: 3447
		InvalidStartTime,
		// Token: 0x04000D78 RID: 3448
		InvalidEndTime,
		// Token: 0x04000D79 RID: 3449
		InvalidTimezoneRange,
		// Token: 0x04000D7A RID: 3450
		InvalidDateTimeFormat,
		// Token: 0x04000D7B RID: 3451
		InvalidTimezone,
		// Token: 0x04000D7C RID: 3452
		InvalidRecipients,
		// Token: 0x04000D7D RID: 3453
		LastKnown160Error = 185
	}
}
