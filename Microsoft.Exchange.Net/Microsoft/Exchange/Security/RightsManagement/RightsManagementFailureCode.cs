using System;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x02000A29 RID: 2601
	internal enum RightsManagementFailureCode
	{
		// Token: 0x04003013 RID: 12307
		Success,
		// Token: 0x04003014 RID: 12308
		UnknownFailure = -1,
		// Token: 0x04003015 RID: 12309
		InvalidLicense = -2147168512,
		// Token: 0x04003016 RID: 12310
		InfoNotInLicense,
		// Token: 0x04003017 RID: 12311
		InvalidLicenseSignature,
		// Token: 0x04003018 RID: 12312
		EncryptionNotPermitted = -2147168508,
		// Token: 0x04003019 RID: 12313
		UserRightNotGranted,
		// Token: 0x0400301A RID: 12314
		InvalidVersion,
		// Token: 0x0400301B RID: 12315
		InvalidEncodingType,
		// Token: 0x0400301C RID: 12316
		InvalidNumericalValue,
		// Token: 0x0400301D RID: 12317
		InvalidAlgorithmType,
		// Token: 0x0400301E RID: 12318
		EnvironmentNotLoaded,
		// Token: 0x0400301F RID: 12319
		EnvironmentCannotLoad,
		// Token: 0x04003020 RID: 12320
		TooManyLoadedEnvironments,
		// Token: 0x04003021 RID: 12321
		IncompatibleObjects = -2147168498,
		// Token: 0x04003022 RID: 12322
		LibraryFail,
		// Token: 0x04003023 RID: 12323
		EnablingPrincipalFailure,
		// Token: 0x04003024 RID: 12324
		InfoNotPresent,
		// Token: 0x04003025 RID: 12325
		BadGetInfoQuery,
		// Token: 0x04003026 RID: 12326
		KeyTypeUnsupported,
		// Token: 0x04003027 RID: 12327
		CryptoOperationUnsupported,
		// Token: 0x04003028 RID: 12328
		ClockRollbackDetected,
		// Token: 0x04003029 RID: 12329
		QueryReportsNoResults,
		// Token: 0x0400302A RID: 12330
		UnexpectedException,
		// Token: 0x0400302B RID: 12331
		BindValidityTimeViolated,
		// Token: 0x0400302C RID: 12332
		BrokenCertChain,
		// Token: 0x0400302D RID: 12333
		BindPolicyViolation = -2147168485,
		// Token: 0x0400302E RID: 12334
		ManifestPolicyViolation = -2147183860,
		// Token: 0x0400302F RID: 12335
		BindRevokedLicense = -2147168484,
		// Token: 0x04003030 RID: 12336
		BindRevokedIssuer,
		// Token: 0x04003031 RID: 12337
		BindRevokedPrincipal,
		// Token: 0x04003032 RID: 12338
		BindRevokedResource,
		// Token: 0x04003033 RID: 12339
		BindRevokedModule,
		// Token: 0x04003034 RID: 12340
		BindContentNotInEndUseLicense,
		// Token: 0x04003035 RID: 12341
		BindAccessPrincipalNotEnabling,
		// Token: 0x04003036 RID: 12342
		BindAccessUnsatisfied,
		// Token: 0x04003037 RID: 12343
		BindIndicatedPrincipalMissing,
		// Token: 0x04003038 RID: 12344
		BindMachineNotFoundInGroupIdentity,
		// Token: 0x04003039 RID: 12345
		LibraryUnsupportedPlugIn,
		// Token: 0x0400303A RID: 12346
		BindRevocationListStale,
		// Token: 0x0400303B RID: 12347
		BindNoApplicableRevocationList,
		// Token: 0x0400303C RID: 12348
		InvalidHandle = -2147168468,
		// Token: 0x0400303D RID: 12349
		BindIntervalTimeViolated = -2147168465,
		// Token: 0x0400303E RID: 12350
		BindNoSatisfiedRightsGroup,
		// Token: 0x0400303F RID: 12351
		BindSpecifiedWorkMissing,
		// Token: 0x04003040 RID: 12352
		NoMoreData = -2147168461,
		// Token: 0x04003041 RID: 12353
		LicenseAcquisitionFailed,
		// Token: 0x04003042 RID: 12354
		IdMismatch,
		// Token: 0x04003043 RID: 12355
		TooManyCertificates,
		// Token: 0x04003044 RID: 12356
		NoDistributionPointUrlFound,
		// Token: 0x04003045 RID: 12357
		AlreadyInProgress,
		// Token: 0x04003046 RID: 12358
		GroupIdentityNotSet,
		// Token: 0x04003047 RID: 12359
		RecordNotFound,
		// Token: 0x04003048 RID: 12360
		NoConnect,
		// Token: 0x04003049 RID: 12361
		NoLicense,
		// Token: 0x0400304A RID: 12362
		NeedsMachineActivation,
		// Token: 0x0400304B RID: 12363
		NeedsGroupIdentityActivation,
		// Token: 0x0400304C RID: 12364
		ActivationFailed = -2147168448,
		// Token: 0x0400304D RID: 12365
		Aborted,
		// Token: 0x0400304E RID: 12366
		OutOfQuota,
		// Token: 0x0400304F RID: 12367
		AuthenticationFailed,
		// Token: 0x04003050 RID: 12368
		ServerError,
		// Token: 0x04003051 RID: 12369
		InstallationFailed,
		// Token: 0x04003052 RID: 12370
		HidCorrupted,
		// Token: 0x04003053 RID: 12371
		InvalidServerResponse,
		// Token: 0x04003054 RID: 12372
		ServiceNotFound,
		// Token: 0x04003055 RID: 12373
		UseDefault,
		// Token: 0x04003056 RID: 12374
		ServerNotFound,
		// Token: 0x04003057 RID: 12375
		InvalidEmail,
		// Token: 0x04003058 RID: 12376
		ValidityTimeViolation,
		// Token: 0x04003059 RID: 12377
		OutdatedModule,
		// Token: 0x0400305A RID: 12378
		ServiceMoved = -2147168421,
		// Token: 0x0400305B RID: 12379
		ServiceGone,
		// Token: 0x0400305C RID: 12380
		AdEntryNotFound,
		// Token: 0x0400305D RID: 12381
		NotAChain,
		// Token: 0x0400305E RID: 12382
		RequestDenied,
		// Token: 0x0400305F RID: 12383
		InsufficientBuffer = -2147024774,
		// Token: 0x04003060 RID: 12384
		NotSet = -2147168434,
		// Token: 0x04003061 RID: 12385
		MetadataNotSet,
		// Token: 0x04003062 RID: 12386
		RevocationInfoNotSet,
		// Token: 0x04003063 RID: 12387
		InvalidTimeInfo,
		// Token: 0x04003064 RID: 12388
		RightNotSet,
		// Token: 0x04003065 RID: 12389
		LicenseBindingToWindowsIdentityFailed,
		// Token: 0x04003066 RID: 12390
		InvalidIssuanceLicenseTemplate,
		// Token: 0x04003067 RID: 12391
		InvalidKeyLength,
		// Token: 0x04003068 RID: 12392
		ExpiredOfficialIssuanceLicenseTemplate = -2147168425,
		// Token: 0x04003069 RID: 12393
		InvalidClientLicensorCertificate,
		// Token: 0x0400306A RID: 12394
		HidInvalid,
		// Token: 0x0400306B RID: 12395
		EmailNotVerified,
		// Token: 0x0400306C RID: 12396
		DebuggerDetected = -2147168416,
		// Token: 0x0400306D RID: 12397
		InvalidLockboxType = -2147168400,
		// Token: 0x0400306E RID: 12398
		InvalidLockboxPath,
		// Token: 0x0400306F RID: 12399
		InvalidRegistryPath,
		// Token: 0x04003070 RID: 12400
		NoAesCryptoProvider,
		// Token: 0x04003071 RID: 12401
		GlobalOptionAlreadySet,
		// Token: 0x04003072 RID: 12402
		OwnerLicenseNotFound,
		// Token: 0x04003073 RID: 12403
		InvalidWindow,
		// Token: 0x04003074 RID: 12404
		WindowRegistrationFailed,
		// Token: 0x04003075 RID: 12405
		SafeModeOSDetected,
		// Token: 0x04003076 RID: 12406
		PlatformPolicyViolation,
		// Token: 0x04003077 RID: 12407
		IssuanceLicenseLengthLimitExceeded = -2147168383,
		// Token: 0x04003078 RID: 12408
		Completed = 315140,
		// Token: 0x04003079 RID: 12409
		PreLicenseAcquisitionFailed = -2147160064,
		// Token: 0x0400307A RID: 12410
		ClcAcquisitionFailed,
		// Token: 0x0400307B RID: 12411
		RacAcquisitionFailed,
		// Token: 0x0400307C RID: 12412
		TemplateAcquisitionFailed,
		// Token: 0x0400307D RID: 12413
		UseLicenseAcquisitionFailed,
		// Token: 0x0400307E RID: 12414
		FindServiceLocationFailed,
		// Token: 0x0400307F RID: 12415
		InvalidTenantLicense,
		// Token: 0x04003080 RID: 12416
		TemplateDoesNotExist,
		// Token: 0x04003081 RID: 12417
		AttachmentProtectionFailed,
		// Token: 0x04003082 RID: 12418
		FailedToExtractTargetUriFromMex,
		// Token: 0x04003083 RID: 12419
		FailedToDownloadMexData,
		// Token: 0x04003084 RID: 12420
		GetServerInfoFailed,
		// Token: 0x04003085 RID: 12421
		InternalLicensingDisabled,
		// Token: 0x04003086 RID: 12422
		ExternalLicensingDisabled,
		// Token: 0x04003087 RID: 12423
		BindInsufficientRightsToCreateEncryptor,
		// Token: 0x04003088 RID: 12424
		InvalidRightsAccountCertificate,
		// Token: 0x04003089 RID: 12425
		BadDRMPropsSignature,
		// Token: 0x0400308A RID: 12426
		UnknownDRMFailure,
		// Token: 0x0400308B RID: 12427
		NotEnoughRightsToReEncrypt,
		// Token: 0x0400308C RID: 12428
		ExtractNotAllowed,
		// Token: 0x0400308D RID: 12429
		ExchangeMisConfiguration,
		// Token: 0x0400308E RID: 12430
		InvalidRecipient,
		// Token: 0x0400308F RID: 12431
		FederationNotEnabled,
		// Token: 0x04003090 RID: 12432
		FailedToDetermineExchangeMode,
		// Token: 0x04003091 RID: 12433
		FederatedMailboxNotSet,
		// Token: 0x04003092 RID: 12434
		FailedToRequestDelegationToken,
		// Token: 0x04003093 RID: 12435
		FederationCertificateAccessFailure,
		// Token: 0x04003094 RID: 12436
		ActiveFederationFault,
		// Token: 0x04003095 RID: 12437
		ActionNotSupported,
		// Token: 0x04003096 RID: 12438
		MessageSecurityError,
		// Token: 0x04003097 RID: 12439
		CommunicationError,
		// Token: 0x04003098 RID: 12440
		OperationTimeout,
		// Token: 0x04003099 RID: 12441
		InvalidCertificateChain,
		// Token: 0x0400309A RID: 12442
		InvalidBlackBox,
		// Token: 0x0400309B RID: 12443
		InvalidIssuanceLicense,
		// Token: 0x0400309C RID: 12444
		UnauthorizedAccess,
		// Token: 0x0400309D RID: 12445
		UntrustedRightsLabel,
		// Token: 0x0400309E RID: 12446
		PreCertificationFailed,
		// Token: 0x0400309F RID: 12447
		ClusterDecommissioned,
		// Token: 0x040030A0 RID: 12448
		EnablingBitsUnsupported,
		// Token: 0x040030A1 RID: 12449
		InvalidPersonaCertificate,
		// Token: 0x040030A2 RID: 12450
		Http3xxFailure,
		// Token: 0x040030A3 RID: 12451
		ConnectFailure,
		// Token: 0x040030A4 RID: 12452
		TrustFailure,
		// Token: 0x040030A5 RID: 12453
		HttpUnauthorizedFailure,
		// Token: 0x040030A6 RID: 12454
		HttpForbiddenFailure,
		// Token: 0x040030A7 RID: 12455
		HttpNotFoundFailure,
		// Token: 0x040030A8 RID: 12456
		ADUserNotFound,
		// Token: 0x040030A9 RID: 12457
		ADUserNotFederated,
		// Token: 0x040030AA RID: 12458
		OfflineRmsServerFailure,
		// Token: 0x040030AB RID: 12459
		ServerRightNotGranted,
		// Token: 0x040030AC RID: 12460
		InvalidLicensee,
		// Token: 0x040030AD RID: 12461
		FeatureDisabled,
		// Token: 0x040030AE RID: 12462
		NotSupported,
		// Token: 0x040030AF RID: 12463
		CorruptData,
		// Token: 0x040030B0 RID: 12464
		MissingLicense,
		// Token: 0x040030B1 RID: 12465
		InvalidLicensingLocation,
		// Token: 0x040030B2 RID: 12466
		ExpiredLicense
	}
}
