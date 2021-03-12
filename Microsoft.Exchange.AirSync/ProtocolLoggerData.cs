using System;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000102 RID: 258
	internal enum ProtocolLoggerData
	{
		// Token: 0x0400092D RID: 2349
		ProtocolVersion,
		// Token: 0x0400092E RID: 2350
		SyncStateNotFound,
		// Token: 0x0400092F RID: 2351
		NewMailboxSession,
		// Token: 0x04000930 RID: 2352
		ProxyingFrom,
		// Token: 0x04000931 RID: 2353
		ProxyingTo,
		// Token: 0x04000932 RID: 2354
		RedirectTo,
		// Token: 0x04000933 RID: 2355
		TotalFolders,
		// Token: 0x04000934 RID: 2356
		NumItemsOpened,
		// Token: 0x04000935 RID: 2357
		NumErrors,
		// Token: 0x04000936 RID: 2358
		AccountTerminated,
		// Token: 0x04000937 RID: 2359
		HeartBeatInterval,
		// Token: 0x04000938 RID: 2360
		RequestTimedOut,
		// Token: 0x04000939 RID: 2361
		RequestHangTime,
		// Token: 0x0400093A RID: 2362
		EmptyRequest,
		// Token: 0x0400093B RID: 2363
		EmptyResponse,
		// Token: 0x0400093C RID: 2364
		CompletionOffset,
		// Token: 0x0400093D RID: 2365
		FinalElapsedTime,
		// Token: 0x0400093E RID: 2366
		ExceptionStackTrace,
		// Token: 0x0400093F RID: 2367
		OOFVerb,
		// Token: 0x04000940 RID: 2368
		UserInformationVerb,
		// Token: 0x04000941 RID: 2369
		DeviceInfoModel,
		// Token: 0x04000942 RID: 2370
		DeviceInfoIMEI,
		// Token: 0x04000943 RID: 2371
		DeviceInfoFriendlyName,
		// Token: 0x04000944 RID: 2372
		DeviceInfoOS,
		// Token: 0x04000945 RID: 2373
		DeviceInfoOSLanguage,
		// Token: 0x04000946 RID: 2374
		DeviceInfoUserAgent,
		// Token: 0x04000947 RID: 2375
		DeviceInfoEnableOutboundSMS,
		// Token: 0x04000948 RID: 2376
		DeviceInfoMobileOperator,
		// Token: 0x04000949 RID: 2377
		SharePointDocs,
		// Token: 0x0400094A RID: 2378
		UNCFiles,
		// Token: 0x0400094B RID: 2379
		Attachments,
		// Token: 0x0400094C RID: 2380
		SharePointBytes,
		// Token: 0x0400094D RID: 2381
		UNCBytes,
		// Token: 0x0400094E RID: 2382
		AttachmentBytes,
		// Token: 0x0400094F RID: 2383
		PolicyKeyReceived,
		// Token: 0x04000950 RID: 2384
		PolicyAckStatus,
		// Token: 0x04000951 RID: 2385
		StatusCode,
		// Token: 0x04000952 RID: 2386
		CollectionStatusCode,
		// Token: 0x04000953 RID: 2387
		Error,
		// Token: 0x04000954 RID: 2388
		Message,
		// Token: 0x04000955 RID: 2389
		ADWriteReason,
		// Token: 0x04000956 RID: 2390
		ProxyUser,
		// Token: 0x04000957 RID: 2391
		NumberOfRecipientsToResolve,
		// Token: 0x04000958 RID: 2392
		AvailabilityRequested,
		// Token: 0x04000959 RID: 2393
		CertificatesRequested,
		// Token: 0x0400095A RID: 2394
		PictureRequested,
		// Token: 0x0400095B RID: 2395
		AccessStateAndReason,
		// Token: 0x0400095C RID: 2396
		MailSent,
		// Token: 0x0400095D RID: 2397
		Ssu,
		// Token: 0x0400095E RID: 2398
		ThrottledTime,
		// Token: 0x0400095F RID: 2399
		Host,
		// Token: 0x04000960 RID: 2400
		UserSmtpAddress,
		// Token: 0x04000961 RID: 2401
		TimeReceived,
		// Token: 0x04000962 RID: 2402
		TimeStarted,
		// Token: 0x04000963 RID: 2403
		TimeFinished,
		// Token: 0x04000964 RID: 2404
		TimeCompleted,
		// Token: 0x04000965 RID: 2405
		TimeHang,
		// Token: 0x04000966 RID: 2406
		TimeContinued,
		// Token: 0x04000967 RID: 2407
		TimeDeviceAccessCheckStarted,
		// Token: 0x04000968 RID: 2408
		TimePolicyCheckStarted,
		// Token: 0x04000969 RID: 2409
		TimeExecuteStarted,
		// Token: 0x0400096A RID: 2410
		TimeExecuteFinished,
		// Token: 0x0400096B RID: 2411
		NoOfDevicesRemoved,
		// Token: 0x0400096C RID: 2412
		Budget,
		// Token: 0x0400096D RID: 2413
		MailboxServer,
		// Token: 0x0400096E RID: 2414
		SNSServiceServerName,
		// Token: 0x0400096F RID: 2415
		DomainController,
		// Token: 0x04000970 RID: 2416
		DeviceBehaviorLoaded,
		// Token: 0x04000971 RID: 2417
		DeviceBehaviorSaved,
		// Token: 0x04000972 RID: 2418
		CommandHashCode,
		// Token: 0x04000973 RID: 2419
		SyncHashCode,
		// Token: 0x04000974 RID: 2420
		AutoBlockEvent,
		// Token: 0x04000975 RID: 2421
		SuggestedBackOffValue,
		// Token: 0x04000976 RID: 2422
		BackOffReason,
		// Token: 0x04000977 RID: 2423
		EmptyResponseDelayed,
		// Token: 0x04000978 RID: 2424
		IOEmptyFolderContents,
		// Token: 0x04000979 RID: 2425
		IOEmptyFolderContentsErrors,
		// Token: 0x0400097A RID: 2426
		IOFetchDocs,
		// Token: 0x0400097B RID: 2427
		IOFetchDocErrors,
		// Token: 0x0400097C RID: 2428
		IOFetchItems,
		// Token: 0x0400097D RID: 2429
		IOFetchItemErrors,
		// Token: 0x0400097E RID: 2430
		IOFetchAtts,
		// Token: 0x0400097F RID: 2431
		IOFetchEntAtts,
		// Token: 0x04000980 RID: 2432
		IOFetchAttErrors,
		// Token: 0x04000981 RID: 2433
		IOMoves,
		// Token: 0x04000982 RID: 2434
		IOMoveErrors,
		// Token: 0x04000983 RID: 2435
		MRItems,
		// Token: 0x04000984 RID: 2436
		MRErrors,
		// Token: 0x04000985 RID: 2437
		MItems,
		// Token: 0x04000986 RID: 2438
		MIErrors,
		// Token: 0x04000987 RID: 2439
		SearchProvider,
		// Token: 0x04000988 RID: 2440
		SearchDeep,
		// Token: 0x04000989 RID: 2441
		SearchQueryLength,
		// Token: 0x0400098A RID: 2442
		SearchQueryTime,
		// Token: 0x0400098B RID: 2443
		TotalPhotoRequests,
		// Token: 0x0400098C RID: 2444
		SuccessfulPhotoRequests,
		// Token: 0x0400098D RID: 2445
		PhotosFromCache,
		// Token: 0x0400098E RID: 2446
		VCertChains,
		// Token: 0x0400098F RID: 2447
		VCerts,
		// Token: 0x04000990 RID: 2448
		VCertCRL,
		// Token: 0x04000991 RID: 2449
		ClassName,
		// Token: 0x04000992 RID: 2450
		UpdateUserHasPartnerships,
		// Token: 0x04000993 RID: 2451
		SkipSend,
		// Token: 0x04000994 RID: 2452
		MeetingOrganizerLookup,
		// Token: 0x04000995 RID: 2453
		ExternallyManaged,
		// Token: 0x04000996 RID: 2454
		GraphApiCallData,
		// Token: 0x04000997 RID: 2455
		ActivityContextData,
		// Token: 0x04000998 RID: 2456
		NMDeferred,
		// Token: 0x04000999 RID: 2457
		QuickHierarchyChangeCheck,
		// Token: 0x0400099A RID: 2458
		BlockingClientAccessRuleName,
		// Token: 0x0400099B RID: 2459
		ClientAccessRulesLatency,
		// Token: 0x0400099C RID: 2460
		OrganizationId,
		// Token: 0x0400099D RID: 2461
		PUID,
		// Token: 0x0400099E RID: 2462
		OutlookExtensionsHeader,
		// Token: 0x0400099F RID: 2463
		OrganizationType
	}
}
