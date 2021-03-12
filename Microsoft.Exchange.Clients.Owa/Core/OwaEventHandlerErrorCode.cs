using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000188 RID: 392
	public enum OwaEventHandlerErrorCode
	{
		// Token: 0x040009C2 RID: 2498
		None,
		// Token: 0x040009C3 RID: 2499
		ConflictResolution,
		// Token: 0x040009C4 RID: 2500
		ItemNotFound,
		// Token: 0x040009C5 RID: 2501
		MissingMeetingFields,
		// Token: 0x040009C6 RID: 2502
		UnexpectedError,
		// Token: 0x040009C7 RID: 2503
		MeetingCriticalUpdateProperties,
		// Token: 0x040009C8 RID: 2504
		WebPartSegmentationError,
		// Token: 0x040009C9 RID: 2505
		WebPartFirstAccessError,
		// Token: 0x040009CA RID: 2506
		WebPartContentsPermissionsError,
		// Token: 0x040009CB RID: 2507
		WebPartUnsupportedFolderTypeError,
		// Token: 0x040009CC RID: 2508
		WebPartUnsupportedItemError,
		// Token: 0x040009CD RID: 2509
		WebPartFolderPathError,
		// Token: 0x040009CE RID: 2510
		WebPartTaskFolderError,
		// Token: 0x040009CF RID: 2511
		WebPartCommandParameterError,
		// Token: 0x040009D0 RID: 2512
		WebPartInvalidParameterError,
		// Token: 0x040009D1 RID: 2513
		WebPartActionPermissionsError,
		// Token: 0x040009D2 RID: 2514
		WebPartNoMatchOnSmtpAddressError,
		// Token: 0x040009D3 RID: 2515
		WebPartCalendarFolderError,
		// Token: 0x040009D4 RID: 2516
		AddContactsToItemError,
		// Token: 0x040009D5 RID: 2517
		SendByEmailError,
		// Token: 0x040009D6 RID: 2518
		MailboxInTransitError,
		// Token: 0x040009D7 RID: 2519
		ComplianceLabelNotFoundError,
		// Token: 0x040009D8 RID: 2520
		WebPartAccessPublicFolderViaOwaBasicError,
		// Token: 0x040009D9 RID: 2521
		WebPartExplicitLogonPublicFolderViaOwaBasicError,
		// Token: 0x040009DA RID: 2522
		ExistentNotificationPipeError,
		// Token: 0x040009DB RID: 2523
		MailboxFailoverWithoutRedirection,
		// Token: 0x040009DC RID: 2524
		MailboxFailoverWithRedirection,
		// Token: 0x040009DD RID: 2525
		FolderNameExists,
		// Token: 0x040009DE RID: 2526
		UserNotIMEnabled,
		// Token: 0x040009DF RID: 2527
		IMOperationNotAllowedToSelf,
		// Token: 0x040009E0 RID: 2528
		ErrorEarlyBrowserOnPublishedCalendar,
		// Token: 0x040009E1 RID: 2529
		NotSet
	}
}
