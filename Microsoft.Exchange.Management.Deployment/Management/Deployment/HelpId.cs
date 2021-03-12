using System;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200006C RID: 108
	public enum HelpId
	{
		// Token: 0x040001CC RID: 460
		HostingModeNotAvailable,
		// Token: 0x040001CD RID: 461
		OSCheckedBuild,
		// Token: 0x040001CE RID: 462
		EventSystemStopped,
		// Token: 0x040001CF RID: 463
		MSDTCStopped,
		// Token: 0x040001D0 RID: 464
		MpsSvcStopped,
		// Token: 0x040001D1 RID: 465
		NetTcpPortSharingSvcNotAuto,
		// Token: 0x040001D2 RID: 466
		ComputerNotPartofDomain,
		// Token: 0x040001D3 RID: 467
		WarningInstallExchangeRolesOnDomainController,
		// Token: 0x040001D4 RID: 468
		InstallOnDCInADSplitPermissionMode,
		// Token: 0x040001D5 RID: 469
		SetADSplitPermissionWhenExchangeServerRolesOnDC,
		// Token: 0x040001D6 RID: 470
		ServerNameNotValid,
		// Token: 0x040001D7 RID: 471
		LocalComputerIsDCInChildDomain,
		// Token: 0x040001D8 RID: 472
		LoggedOntoDomain,
		// Token: 0x040001D9 RID: 473
		PrimaryDNSTestFailed,
		// Token: 0x040001DA RID: 474
		HostRecordMissing,
		// Token: 0x040001DB RID: 475
		LocalDomainModeMixed,
		// Token: 0x040001DC RID: 476
		DomainPrepRequired,
		// Token: 0x040001DD RID: 477
		ComputerRODC,
		// Token: 0x040001DE RID: 478
		InvalidADSite,
		// Token: 0x040001DF RID: 479
		W2K8R2PrepareSchemaLdifdeNotInstalled,
		// Token: 0x040001E0 RID: 480
		W2K8R2PrepareAdLdifdeNotInstalled,
		// Token: 0x040001E1 RID: 481
		MailboxRoleAlreadyExists,
		// Token: 0x040001E2 RID: 482
		ClientAccessRoleAlreadyExists,
		// Token: 0x040001E3 RID: 483
		UnifiedMessagingRoleAlreadyExists,
		// Token: 0x040001E4 RID: 484
		BridgeheadRoleAlreadyExists,
		// Token: 0x040001E5 RID: 485
		CafeRoleAlreadyExists,
		// Token: 0x040001E6 RID: 486
		FrontendTransportRoleAlreadyExists,
		// Token: 0x040001E7 RID: 487
		ServerWinWebEdition,
		// Token: 0x040001E8 RID: 488
		BridgeheadRoleNotPresentInSite,
		// Token: 0x040001E9 RID: 489
		ClientAccessRoleNotPresentInSite,
		// Token: 0x040001EA RID: 490
		LonghornWmspdmoxNotInstalled,
		// Token: 0x040001EB RID: 491
		Exchange2000or2003PresentInOrg,
		// Token: 0x040001EC RID: 492
		RebootPending,
		// Token: 0x040001ED RID: 493
		Win7RpcHttpAssocCookieGuidUpdateNotInstalled,
		// Token: 0x040001EE RID: 494
		SearchFoundationAssemblyLoaderKBNotInstalled,
		// Token: 0x040001EF RID: 495
		Win2k12UrefsUpdateNotInstalled,
		// Token: 0x040001F0 RID: 496
		Win2k12RefsUpdateNotInstalled,
		// Token: 0x040001F1 RID: 497
		Win2k12RollupUpdateNotInstalled,
		// Token: 0x040001F2 RID: 498
		UnifiedMessagingRoleNotInstalled,
		// Token: 0x040001F3 RID: 499
		BridgeheadRoleNotInstalled,
		// Token: 0x040001F4 RID: 500
		NotLocalAdmin,
		// Token: 0x040001F5 RID: 501
		FirstSGFilesExist,
		// Token: 0x040001F6 RID: 502
		SecondSGFilesExist,
		// Token: 0x040001F7 RID: 503
		ExchangeVersionBlock,
		// Token: 0x040001F8 RID: 504
		VoiceMessagesInQueue,
		// Token: 0x040001F9 RID: 505
		ProcessNeedsToBeClosedOnUpgrade,
		// Token: 0x040001FA RID: 506
		ProcessNeedsToBeClosedOnUninstall,
		// Token: 0x040001FB RID: 507
		SendConnectorException,
		// Token: 0x040001FC RID: 508
		MailboxLogDriveDoesNotExist,
		// Token: 0x040001FD RID: 509
		MailboxEDBDriveDoesNotExist,
		// Token: 0x040001FE RID: 510
		ServerIsSourceForSendConnector,
		// Token: 0x040001FF RID: 511
		ServerIsGroupExpansionServer,
		// Token: 0x04000200 RID: 512
		ServerIsDynamicGroupExpansionServer,
		// Token: 0x04000201 RID: 513
		MemberOfDatabaseAvailabilityGroup,
		// Token: 0x04000202 RID: 514
		DrMinVersionCheck,
		// Token: 0x04000203 RID: 515
		RemoteRegException,
		// Token: 0x04000204 RID: 516
		WinRMIISExtensionInstalled,
		// Token: 0x04000205 RID: 517
		LangPackBundleVersioning,
		// Token: 0x04000206 RID: 518
		LangPackBundleCheck,
		// Token: 0x04000207 RID: 519
		LangPackDiskSpaceCheck,
		// Token: 0x04000208 RID: 520
		LangPackInstalled,
		// Token: 0x04000209 RID: 521
		AlreadyInstalledUMLangPacks,
		// Token: 0x0400020A RID: 522
		UMLangPackDiskSpaceCheck,
		// Token: 0x0400020B RID: 523
		LangPackUpgradeVersioning,
		// Token: 0x0400020C RID: 524
		PendingRebootWindowsComponents,
		// Token: 0x0400020D RID: 525
		Iis32BitMode,
		// Token: 0x0400020E RID: 526
		SchemaUpdateRequired,
		// Token: 0x0400020F RID: 527
		AdUpdateRequired,
		// Token: 0x04000210 RID: 528
		GlobalUpdateRequired,
		// Token: 0x04000211 RID: 529
		DomainPrepWithoutADUpdate,
		// Token: 0x04000212 RID: 530
		LocalDomainPrep,
		// Token: 0x04000213 RID: 531
		GlobalServerInstall,
		// Token: 0x04000214 RID: 532
		DelegatedBridgeheadFirstInstall,
		// Token: 0x04000215 RID: 533
		DelegatedCafeFirstInstall,
		// Token: 0x04000216 RID: 534
		DelegatedFrontendTransportFirstInstall,
		// Token: 0x04000217 RID: 535
		DelegatedMailboxFirstInstall,
		// Token: 0x04000218 RID: 536
		DelegatedClientAccessFirstInstall,
		// Token: 0x04000219 RID: 537
		DelegatedUnifiedMessagingFirstInstall,
		// Token: 0x0400021A RID: 538
		DelegatedBridgeheadFirstSP1upgrade,
		// Token: 0x0400021B RID: 539
		DelegatedUnifiedMessagingFirstSP1upgrade,
		// Token: 0x0400021C RID: 540
		DelegatedClientAccessFirstSP1upgrade,
		// Token: 0x0400021D RID: 541
		DelegatedMailboxFirstSP1upgrade,
		// Token: 0x0400021E RID: 542
		CannotUninstallDelegatedServer,
		// Token: 0x0400021F RID: 543
		PrepareDomainNotAdmin,
		// Token: 0x04000220 RID: 544
		NoE12ServerWarning,
		// Token: 0x04000221 RID: 545
		NoE14ServerWarning,
		// Token: 0x04000222 RID: 546
		NotInSchemaMasterDomain,
		// Token: 0x04000223 RID: 547
		NotInSchemaMasterSite,
		// Token: 0x04000224 RID: 548
		ProvisionedUpdateRequired,
		// Token: 0x04000225 RID: 549
		SchemaFSMONotWin2003SPn,
		// Token: 0x04000226 RID: 550
		CannotUninstallClusterNode,
		// Token: 0x04000227 RID: 551
		CannotUninstallOABServer,
		// Token: 0x04000228 RID: 552
		DomainControllerIsOutOfSite,
		// Token: 0x04000229 RID: 553
		ComputerNameDiscrepancy,
		// Token: 0x0400022A RID: 554
		FqdnMissing,
		// Token: 0x0400022B RID: 555
		DNSDomainNameNotValid,
		// Token: 0x0400022C RID: 556
		DidTenantSettingCreatedAnException,
		// Token: 0x0400022D RID: 557
		DidOnPremisesSettingCreatedAnException,
		// Token: 0x0400022E RID: 558
		HybridIsEnabledAndTenantVersionIsNotUpgraded,
		// Token: 0x0400022F RID: 559
		AdcFound,
		// Token: 0x04000230 RID: 560
		AdInitErrorRule,
		// Token: 0x04000231 RID: 561
		NoConnectorToStar,
		// Token: 0x04000232 RID: 562
		DuplicateShortProvisionedName,
		// Token: 0x04000233 RID: 563
		ForestLevelNotWin2003Native,
		// Token: 0x04000234 RID: 564
		InhBlockPublicFolderTree,
		// Token: 0x04000235 RID: 565
		PrepareDomainNotFound,
		// Token: 0x04000236 RID: 566
		PrepareDomainModeMixed,
		// Token: 0x04000237 RID: 567
		RusMissing,
		// Token: 0x04000238 RID: 568
		ServerFQDNMatchesSMTPPolicy,
		// Token: 0x04000239 RID: 569
		SmtpAddressLiteral,
		// Token: 0x0400023A RID: 570
		UnwillingToRemoveMailboxDatabase,
		// Token: 0x0400023B RID: 571
		RootDomainModeMixed,
		// Token: 0x0400023C RID: 572
		ServerRemoveProvisioningCheck,
		// Token: 0x0400023D RID: 573
		InconsistentlyConfiguredDomain,
		// Token: 0x0400023E RID: 574
		OffLineABServerDeleted,
		// Token: 0x0400023F RID: 575
		ResourcePropertySchemaException,
		// Token: 0x04000240 RID: 576
		MessagesInQueue,
		// Token: 0x04000241 RID: 577
		AdditionalUMLangPackExists,
		// Token: 0x04000242 RID: 578
		ExchangeAlreadyInstalled,
		// Token: 0x04000243 RID: 579
		InstallWatermark,
		// Token: 0x04000244 RID: 580
		InterruptedUninstallNotContinued,
		// Token: 0x04000245 RID: 581
		W3SVCDisabledOrNotInstalled,
		// Token: 0x04000246 RID: 582
		ShouldReRunSetupForW3SVC,
		// Token: 0x04000247 RID: 583
		SMTPSvcInstalled,
		// Token: 0x04000248 RID: 584
		ClusSvcInstalledRoleBlock,
		// Token: 0x04000249 RID: 585
		LonghornIIS6MetabaseNotInstalled,
		// Token: 0x0400024A RID: 586
		LonghornIIS6MgmtConsoleNotInstalled,
		// Token: 0x0400024B RID: 587
		LonghornIIS7HttpCompressionDynamicNotInstalled,
		// Token: 0x0400024C RID: 588
		LonghornIIS7HttpCompressionStaticNotInstalled,
		// Token: 0x0400024D RID: 589
		LonghornWASProcessModelInstalled,
		// Token: 0x0400024E RID: 590
		LonghornIIS7BasicAuthNotInstalled,
		// Token: 0x0400024F RID: 591
		LonghornIIS7WindowsAuthNotInstalled,
		// Token: 0x04000250 RID: 592
		LonghornIIS7DigestAuthNotInstalled,
		// Token: 0x04000251 RID: 593
		LonghornIIS7NetExt,
		// Token: 0x04000252 RID: 594
		LonghornIIS6WMICompatibility,
		// Token: 0x04000253 RID: 595
		LonghornASPNET,
		// Token: 0x04000254 RID: 596
		LonghornISAPIFilter,
		// Token: 0x04000255 RID: 597
		LonghornClientCertificateMappingAuthentication,
		// Token: 0x04000256 RID: 598
		LonghornDirectoryBrowse,
		// Token: 0x04000257 RID: 599
		LonghornHttpErrors,
		// Token: 0x04000258 RID: 600
		LonghornHttpLogging,
		// Token: 0x04000259 RID: 601
		LonghornHttpRedirect,
		// Token: 0x0400025A RID: 602
		LonghornHttpTracing,
		// Token: 0x0400025B RID: 603
		LonghornRequestMonitor,
		// Token: 0x0400025C RID: 604
		LonghornStaticContent,
		// Token: 0x0400025D RID: 605
		ManagementServiceInstalled,
		// Token: 0x0400025E RID: 606
		HttpActivationInstalled,
		// Token: 0x0400025F RID: 607
		WcfHttpActivation45Installed,
		// Token: 0x04000260 RID: 608
		RsatAddsToolsInstalled,
		// Token: 0x04000261 RID: 609
		RsatClusteringInstalled,
		// Token: 0x04000262 RID: 610
		RsatClusteringMgmtInstalled,
		// Token: 0x04000263 RID: 611
		RsatClusteringPowerShellInstalled,
		// Token: 0x04000264 RID: 612
		RsatClusteringCmdInterfaceInstalled,
		// Token: 0x04000265 RID: 613
		UcmaRedistMsi,
		// Token: 0x04000266 RID: 614
		SpeechRedistMsi,
		// Token: 0x04000267 RID: 615
		Win7WindowsIdentityFoundationUpdateNotInstalled,
		// Token: 0x04000268 RID: 616
		Win8WindowsIdentityFoundationUpdateNotInstalled,
		// Token: 0x04000269 RID: 617
		MailboxRoleNotInstalled,
		// Token: 0x0400026A RID: 618
		MailboxMinVersionCheck,
		// Token: 0x0400026B RID: 619
		MailboxUpgradeMinVersionBlock,
		// Token: 0x0400026C RID: 620
		UnifiedMessagingMinVersionCheck,
		// Token: 0x0400026D RID: 621
		UnifiedMessagingUpgradeMinVersionBlock,
		// Token: 0x0400026E RID: 622
		BridgeheadMinVersionCheck,
		// Token: 0x0400026F RID: 623
		BridgeheadUpgradeMinVersionBlock,
		// Token: 0x04000270 RID: 624
		Exchange2013AnyOnExchange2007Server,
		// Token: 0x04000271 RID: 625
		Exchange2010ServerOnExchange2007AdminTools,
		// Token: 0x04000272 RID: 626
		UpdateNeedsReboot,
		// Token: 0x04000273 RID: 627
		CannotAccessAD,
		// Token: 0x04000274 RID: 628
		ConfigDCHostNameMismatch,
		// Token: 0x04000275 RID: 629
		OldADAMInstalled,
		// Token: 0x04000276 RID: 630
		ADAMWin7ServerInstalled,
		// Token: 0x04000277 RID: 631
		UpgradeGateway605Block,
		// Token: 0x04000278 RID: 632
		GatewayMinVersionCheck,
		// Token: 0x04000279 RID: 633
		GatewayUpgradeMinVersionBlock,
		// Token: 0x0400027A RID: 634
		ADAMSvcStopped,
		// Token: 0x0400027B RID: 635
		TargetPathCompressed,
		// Token: 0x0400027C RID: 636
		GatewayUpgrade605Block,
		// Token: 0x0400027D RID: 637
		ADAMDataPathExists,
		// Token: 0x0400027E RID: 638
		EdgeSubscriptionExists,
		// Token: 0x0400027F RID: 639
		ADAMPortAlreadyInUse,
		// Token: 0x04000280 RID: 640
		ADAMSSLPortAlreadyInUse,
		// Token: 0x04000281 RID: 641
		ServerIsLastHubForEdgeSubscription,
		// Token: 0x04000282 RID: 642
		LonghornIIS7ManagementConsoleInstalled,
		// Token: 0x04000283 RID: 643
		WindowsInstallerServiceDisabledOrNotInstalled,
		// Token: 0x04000284 RID: 644
		WinRMServiceDisabledOrNotInstalled,
		// Token: 0x04000285 RID: 645
		RSATWebServerNotInstalled,
		// Token: 0x04000286 RID: 646
		NETFrameworkNotInstalled,
		// Token: 0x04000287 RID: 647
		NETFramework45FeaturesNotInstalled,
		// Token: 0x04000288 RID: 648
		WebNetExt45NotInstalled,
		// Token: 0x04000289 RID: 649
		WebISAPIExtNotInstalled,
		// Token: 0x0400028A RID: 650
		WebASPNET45NotInstalled,
		// Token: 0x0400028B RID: 651
		RPCOverHTTPproxyNotInstalled,
		// Token: 0x0400028C RID: 652
		ServerGuiMgmtInfraNotInstalled,
		// Token: 0x0400028D RID: 653
		E15E14CoexistenceMinVersionRequirement,
		// Token: 0x0400028E RID: 654
		E15E14CoexistenceMinMajorVersionRequirement,
		// Token: 0x0400028F RID: 655
		E15E12CoexistenceMinVersionRequirement,
		// Token: 0x04000290 RID: 656
		E15E14CoexistenceMinVersionRequirementForDC,
		// Token: 0x04000291 RID: 657
		WindowsServer2008CoreServerEdition,
		// Token: 0x04000292 RID: 658
		ValidOSVersion,
		// Token: 0x04000293 RID: 659
		ValidOSVersionForAdminTools,
		// Token: 0x04000294 RID: 660
		Exchange2013AnyOnExchange2010Server,
		// Token: 0x04000295 RID: 661
		ServicesAreMarkedForDeletion,
		// Token: 0x04000296 RID: 662
		PowerShellExecutionPolicyCheckSet,
		// Token: 0x04000297 RID: 663
		MinimumFrameworkNotInstalled,
		// Token: 0x04000298 RID: 664
		AllServersOfHigherVersionRule,
		// Token: 0x04000299 RID: 665
		VC2012RedistDependencyRequirement,
		// Token: 0x0400029A RID: 666
		VC2013RedistDependencyRequirement
	}
}
