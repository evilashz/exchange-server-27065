using System;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A0B RID: 2571
	public enum CalendarActionError
	{
		// Token: 0x04002977 RID: 10615
		None,
		// Token: 0x04002978 RID: 10616
		CalendarActionInvalidGroupName,
		// Token: 0x04002979 RID: 10617
		CalendarActionCannotCreateGroup,
		// Token: 0x0400297A RID: 10618
		CalendarActionInvalidGroupId,
		// Token: 0x0400297B RID: 10619
		CalendarActionCannotSaveGroup,
		// Token: 0x0400297C RID: 10620
		CalendarActionInvalidGroupTypeForDeletion,
		// Token: 0x0400297D RID: 10621
		CalendarActionCannotDeleteGroupStillHasChildren,
		// Token: 0x0400297E RID: 10622
		CalendarActionFolderIdNotCalendarFolder,
		// Token: 0x0400297F RID: 10623
		CalendarActionUnableToChangeColor,
		// Token: 0x04002980 RID: 10624
		CalendarActionInvalidCalendarName,
		// Token: 0x04002981 RID: 10625
		CalendarActionUnableToCreateCalendarFolder,
		// Token: 0x04002982 RID: 10626
		CalendarActionUnableToRenameCalendar,
		// Token: 0x04002983 RID: 10627
		CalendarActionUnableToRenameCalendarGroup,
		// Token: 0x04002984 RID: 10628
		CalendarActionUnableToDeleteCalendarGroup,
		// Token: 0x04002985 RID: 10629
		CalendarActionCalendarAlreadyExists,
		// Token: 0x04002986 RID: 10630
		CalendarActionUnableToCreateCalendarNode,
		// Token: 0x04002987 RID: 10631
		CalendarActionInvalidItemId,
		// Token: 0x04002988 RID: 10632
		CalendarActionFolderIdIsDefaultCalendar,
		// Token: 0x04002989 RID: 10633
		CalendarActionCannotRename,
		// Token: 0x0400298A RID: 10634
		CalendarActionCannotRenameCalendarNode,
		// Token: 0x0400298B RID: 10635
		CalendarActionCannotDeleteCalendar,
		// Token: 0x0400298C RID: 10636
		CalendarActionInvalidCalendarNodeOrder,
		// Token: 0x0400298D RID: 10637
		CalendarActionUnableToUpdateCalendarNode,
		// Token: 0x0400298E RID: 10638
		CalendarActionUnableToFindGroupWithId,
		// Token: 0x0400298F RID: 10639
		AddSharedCalendarFailed,
		// Token: 0x04002990 RID: 10640
		CalendarActionUnableToSubscribeToCalendar,
		// Token: 0x04002991 RID: 10641
		CalendarActionCannotSubscribeToOwnCalendar,
		// Token: 0x04002992 RID: 10642
		CalendarActionCannotSubscribeToDistributionList,
		// Token: 0x04002993 RID: 10643
		CalendarActionInvalidUrlFormat,
		// Token: 0x04002994 RID: 10644
		CalendarActionCalendarAlreadyPublished,
		// Token: 0x04002995 RID: 10645
		CalendarActionCannotSaveCalendar,
		// Token: 0x04002996 RID: 10646
		CalendarActionUnableToFindCalendarFolder,
		// Token: 0x04002997 RID: 10647
		CalendarActionNonExistentMailbox,
		// Token: 0x04002998 RID: 10648
		CalendarActionUnexpectedError,
		// Token: 0x04002999 RID: 10649
		CalendarActionDelegateManagementError,
		// Token: 0x0400299A RID: 10650
		CalendarActionSendSharingInviteError,
		// Token: 0x0400299B RID: 10651
		CalendarActionInvalidCalendarEmailAddress,
		// Token: 0x0400299C RID: 10652
		CalendarActionUnresolvedCalendarEmailAddress,
		// Token: 0x0400299D RID: 10653
		CalendarActionNotInBirthdayCalendarFlight
	}
}
