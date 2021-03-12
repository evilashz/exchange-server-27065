using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200007B RID: 123
	internal enum ErrorConstants
	{
		// Token: 0x040001E9 RID: 489
		RequestStreamTooBig = 5000,
		// Token: 0x040001EA RID: 490
		IdentityArrayEmpty,
		// Token: 0x040001EB RID: 491
		IdentityArrayTooBig,
		// Token: 0x040001EC RID: 492
		TimeIntervalTooBig,
		// Token: 0x040001ED RID: 493
		InvalidMergedFreeBusyInterval,
		// Token: 0x040001EE RID: 494
		ResultSetTooBig = 5006,
		// Token: 0x040001EF RID: 495
		InvalidClientSecurityContext,
		// Token: 0x040001F0 RID: 496
		MailboxLogonFailed,
		// Token: 0x040001F1 RID: 497
		MailRecipientNotFound,
		// Token: 0x040001F2 RID: 498
		InvalidTimeInterval,
		// Token: 0x040001F3 RID: 499
		PublicFolderServerNotFound,
		// Token: 0x040001F4 RID: 500
		InvalidAccessLevel,
		// Token: 0x040001F5 RID: 501
		InvalidSecurityDescriptor,
		// Token: 0x040001F6 RID: 502
		Win32InteropError,
		// Token: 0x040001F7 RID: 503
		ProxyRequestNotAllowed,
		// Token: 0x040001F8 RID: 504
		ProxyRequestProcessingFailed,
		// Token: 0x040001F9 RID: 505
		PublicFolderRequestProcessingFailed,
		// Token: 0x040001FA RID: 506
		WorkingHoursXmlMalformed = 5019,
		// Token: 0x040001FB RID: 507
		ServiceDiscoveryFailed = 5021,
		// Token: 0x040001FC RID: 508
		AddressSpaceNotFound = 5023,
		// Token: 0x040001FD RID: 509
		AvailabilityConfigNotFound,
		// Token: 0x040001FE RID: 510
		InvalidCrossForestCredentials,
		// Token: 0x040001FF RID: 511
		InvalidFreeBusyViewType,
		// Token: 0x04000200 RID: 512
		TimeoutExpired,
		// Token: 0x04000201 RID: 513
		MissingArgument,
		// Token: 0x04000202 RID: 514
		NoCalendar,
		// Token: 0x04000203 RID: 515
		InvalidAuthorizationContext = 5032,
		// Token: 0x04000204 RID: 516
		LogonAsNetworkServiceFailed,
		// Token: 0x04000205 RID: 517
		InvalidNetworkServiceContext,
		// Token: 0x04000206 RID: 518
		InvalidSmtpAddress,
		// Token: 0x04000207 RID: 519
		IndividualMailboxLimitReached,
		// Token: 0x04000208 RID: 520
		NoFreeBusyAccess,
		// Token: 0x04000209 RID: 521
		AutoDiscoverFailed = 5039,
		// Token: 0x0400020A RID: 522
		MeetingSuggestionGenerationFailed,
		// Token: 0x0400020B RID: 523
		FreeBusyGenerationFailed,
		// Token: 0x0400020C RID: 524
		ClientDisconnected,
		// Token: 0x0400020D RID: 525
		FreeBusyDLLimitReached,
		// Token: 0x0400020E RID: 526
		E14orHigherProxyServerNotFound,
		// Token: 0x0400020F RID: 527
		ProxyForPersonalNotAllowed,
		// Token: 0x04000210 RID: 528
		MailboxFailover,
		// Token: 0x04000211 RID: 529
		InvalidOrganizationRelationshipForFreeBusy,
		// Token: 0x04000212 RID: 530
		DeliveryRestricted,
		// Token: 0x04000213 RID: 531
		GetFolderRequestProcessingFailed = 5100,
		// Token: 0x04000214 RID: 532
		NotDefaultCalendar
	}
}
