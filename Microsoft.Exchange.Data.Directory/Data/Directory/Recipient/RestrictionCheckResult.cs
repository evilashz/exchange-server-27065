using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000202 RID: 514
	internal enum RestrictionCheckResult : uint
	{
		// Token: 0x04000B9E RID: 2974
		AcceptedNoPermissionList = 1U,
		// Token: 0x04000B9F RID: 2975
		AcceptedInRecipientList,
		// Token: 0x04000BA0 RID: 2976
		AcceptedInGroupList,
		// Token: 0x04000BA1 RID: 2977
		AcceptedAcceptanceListEmpty,
		// Token: 0x04000BA2 RID: 2978
		AcceptedSizeOK,
		// Token: 0x04000BA3 RID: 2979
		AcceptedPrivilegedSender,
		// Token: 0x04000BA4 RID: 2980
		AcceptedInBypassModerationRecipientList,
		// Token: 0x04000BA5 RID: 2981
		AcceptedInBypassModerationGroupList,
		// Token: 0x04000BA6 RID: 2982
		AcceptedInModeratorsList,
		// Token: 0x04000BA7 RID: 2983
		AcceptedInOwnersList,
		// Token: 0x04000BA8 RID: 2984
		AcceptedJournalReport,
		// Token: 0x04000BA9 RID: 2985
		Moderated = 1073741824U,
		// Token: 0x04000BAA RID: 2986
		Failed = 2147483648U,
		// Token: 0x04000BAB RID: 2987
		MessageTooLargeForReceiver,
		// Token: 0x04000BAC RID: 2988
		MessageTooLargeForSender,
		// Token: 0x04000BAD RID: 2989
		MessageTooLargeForOrganization,
		// Token: 0x04000BAE RID: 2990
		RejectedInRecipientList,
		// Token: 0x04000BAF RID: 2991
		RejectedInGroupList,
		// Token: 0x04000BB0 RID: 2992
		RejectedAcceptanceListNonEmpty,
		// Token: 0x04000BB1 RID: 2993
		SenderNotAuthenticated,
		// Token: 0x04000BB2 RID: 2994
		InvalidDirectoryObject,
		// Token: 0x04000BB3 RID: 2995
		RejectedAcceptanceGroupListNonEmpty
	}
}
