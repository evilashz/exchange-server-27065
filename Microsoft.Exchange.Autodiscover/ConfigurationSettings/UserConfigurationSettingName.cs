using System;

namespace Microsoft.Exchange.Autodiscover.ConfigurationSettings
{
	// Token: 0x02000046 RID: 70
	public enum UserConfigurationSettingName
	{
		// Token: 0x040001AD RID: 429
		UserDisplayName,
		// Token: 0x040001AE RID: 430
		UserDN,
		// Token: 0x040001AF RID: 431
		UserDeploymentId,
		// Token: 0x040001B0 RID: 432
		InternalMailboxServer,
		// Token: 0x040001B1 RID: 433
		InternalRpcClientServer,
		// Token: 0x040001B2 RID: 434
		InternalMailboxServerDN,
		// Token: 0x040001B3 RID: 435
		InternalEcpUrl,
		// Token: 0x040001B4 RID: 436
		InternalEcpVoicemailUrl,
		// Token: 0x040001B5 RID: 437
		InternalEcpEmailSubscriptionsUrl,
		// Token: 0x040001B6 RID: 438
		InternalEcpTextMessagingUrl,
		// Token: 0x040001B7 RID: 439
		InternalEcpDeliveryReportUrl,
		// Token: 0x040001B8 RID: 440
		InternalEcpRetentionPolicyTagsUrl,
		// Token: 0x040001B9 RID: 441
		InternalEcpPublishingUrl,
		// Token: 0x040001BA RID: 442
		InternalEcpPhotoUrl,
		// Token: 0x040001BB RID: 443
		InternalEcpConnectUrl,
		// Token: 0x040001BC RID: 444
		InternalEcpTeamMailboxUrl,
		// Token: 0x040001BD RID: 445
		InternalEcpTeamMailboxCreatingUrl,
		// Token: 0x040001BE RID: 446
		InternalEcpTeamMailboxEditingUrl,
		// Token: 0x040001BF RID: 447
		InternalEcpTeamMailboxHidingUrl,
		// Token: 0x040001C0 RID: 448
		InternalEcpExtensionInstallationUrl,
		// Token: 0x040001C1 RID: 449
		InternalEwsUrl,
		// Token: 0x040001C2 RID: 450
		InternalEmwsUrl,
		// Token: 0x040001C3 RID: 451
		InternalOABUrl,
		// Token: 0x040001C4 RID: 452
		InternalPhotosUrl,
		// Token: 0x040001C5 RID: 453
		InternalUMUrl,
		// Token: 0x040001C6 RID: 454
		InternalWebClientUrls,
		// Token: 0x040001C7 RID: 455
		MailboxDN,
		// Token: 0x040001C8 RID: 456
		PublicFolderServer,
		// Token: 0x040001C9 RID: 457
		ActiveDirectoryServer,
		// Token: 0x040001CA RID: 458
		ExternalMailboxServer,
		// Token: 0x040001CB RID: 459
		ExternalMailboxServerRequiresSSL,
		// Token: 0x040001CC RID: 460
		ExternalMailboxServerAuthenticationMethods,
		// Token: 0x040001CD RID: 461
		EcpVoicemailUrlFragment,
		// Token: 0x040001CE RID: 462
		EcpEmailSubscriptionsUrlFragment,
		// Token: 0x040001CF RID: 463
		EcpTextMessagingUrlFragment,
		// Token: 0x040001D0 RID: 464
		EcpDeliveryReportUrlFragment,
		// Token: 0x040001D1 RID: 465
		EcpRetentionPolicyTagsUrlFragment,
		// Token: 0x040001D2 RID: 466
		EcpPublishingUrlFragment,
		// Token: 0x040001D3 RID: 467
		EcpPhotoUrlFragment,
		// Token: 0x040001D4 RID: 468
		EcpConnectUrlFragment,
		// Token: 0x040001D5 RID: 469
		EcpTeamMailboxUrlFragment,
		// Token: 0x040001D6 RID: 470
		EcpTeamMailboxCreatingUrlFragment,
		// Token: 0x040001D7 RID: 471
		EcpTeamMailboxEditingUrlFragment,
		// Token: 0x040001D8 RID: 472
		EcpExtensionInstallationUrlFragment,
		// Token: 0x040001D9 RID: 473
		ExternalEcpUrl,
		// Token: 0x040001DA RID: 474
		ExternalEcpVoicemailUrl,
		// Token: 0x040001DB RID: 475
		ExternalEcpEmailSubscriptionsUrl,
		// Token: 0x040001DC RID: 476
		ExternalEcpTextMessagingUrl,
		// Token: 0x040001DD RID: 477
		ExternalEcpDeliveryReportUrl,
		// Token: 0x040001DE RID: 478
		ExternalEcpRetentionPolicyTagsUrl,
		// Token: 0x040001DF RID: 479
		ExternalEcpPublishingUrl,
		// Token: 0x040001E0 RID: 480
		ExternalEcpPhotoUrl,
		// Token: 0x040001E1 RID: 481
		ExternalEcpConnectUrl,
		// Token: 0x040001E2 RID: 482
		ExternalEcpTeamMailboxUrl,
		// Token: 0x040001E3 RID: 483
		ExternalEcpTeamMailboxCreatingUrl,
		// Token: 0x040001E4 RID: 484
		ExternalEcpTeamMailboxEditingUrl,
		// Token: 0x040001E5 RID: 485
		ExternalEcpTeamMailboxHidingUrl,
		// Token: 0x040001E6 RID: 486
		ExternalEcpExtensionInstallationUrl,
		// Token: 0x040001E7 RID: 487
		ExternalEwsUrl,
		// Token: 0x040001E8 RID: 488
		ExternalEmwsUrl,
		// Token: 0x040001E9 RID: 489
		ExternalOABUrl,
		// Token: 0x040001EA RID: 490
		ExternalPhotosUrl,
		// Token: 0x040001EB RID: 491
		ExternalUMUrl,
		// Token: 0x040001EC RID: 492
		ExternalWebClientUrls,
		// Token: 0x040001ED RID: 493
		CrossOrganizationSharingEnabled,
		// Token: 0x040001EE RID: 494
		AlternateMailboxes,
		// Token: 0x040001EF RID: 495
		CasVersion,
		// Token: 0x040001F0 RID: 496
		EwsSupportedSchemas,
		// Token: 0x040001F1 RID: 497
		InternalPop3Connections,
		// Token: 0x040001F2 RID: 498
		ExternalPop3Connections,
		// Token: 0x040001F3 RID: 499
		InternalImap4Connections,
		// Token: 0x040001F4 RID: 500
		ExternalImap4Connections,
		// Token: 0x040001F5 RID: 501
		InternalSmtpConnections,
		// Token: 0x040001F6 RID: 502
		ExternalSmtpConnections,
		// Token: 0x040001F7 RID: 503
		InternalServerExclusiveConnect,
		// Token: 0x040001F8 RID: 504
		ExternalEwsVersion,
		// Token: 0x040001F9 RID: 505
		MobileMailboxPolicy,
		// Token: 0x040001FA RID: 506
		DocumentSharingLocations,
		// Token: 0x040001FB RID: 507
		UserMSOnline,
		// Token: 0x040001FC RID: 508
		InternalMailboxServerAuthenticationMethods,
		// Token: 0x040001FD RID: 509
		MailboxVersion,
		// Token: 0x040001FE RID: 510
		SPMySiteHostURL,
		// Token: 0x040001FF RID: 511
		SiteMailboxCreationURL,
		// Token: 0x04000200 RID: 512
		InternalRpcHttpServer,
		// Token: 0x04000201 RID: 513
		InternalRpcHttpConnectivityRequiresSsl,
		// Token: 0x04000202 RID: 514
		InternalRpcHttpAuthenticationMethod,
		// Token: 0x04000203 RID: 515
		ExternalServerExclusiveConnect,
		// Token: 0x04000204 RID: 516
		ExchangeRpcUrl,
		// Token: 0x04000205 RID: 517
		ShowGalAsDefaultView,
		// Token: 0x04000206 RID: 518
		AutoDiscoverSMTPAddress,
		// Token: 0x04000207 RID: 519
		InteropExternalEwsUrl,
		// Token: 0x04000208 RID: 520
		InteropExternalEwsVersion,
		// Token: 0x04000209 RID: 521
		PublicFolderInformation,
		// Token: 0x0400020A RID: 522
		RedirectUrl,
		// Token: 0x0400020B RID: 523
		EwsPartnerUrl,
		// Token: 0x0400020C RID: 524
		MapiHttpUrls,
		// Token: 0x0400020D RID: 525
		GroupingInformation,
		// Token: 0x0400020E RID: 526
		MapiHttpEnabled,
		// Token: 0x0400020F RID: 527
		MapiHttpEnabledForUser = 101
	}
}
