using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007C6 RID: 1990
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PolicyAllowedMemberRights
	{
		// Token: 0x06004ABF RID: 19135 RVA: 0x001390FC File Offset: 0x001372FC
		public static MemberRights GetAllowed(SharingPolicyAction allowedActions, StoreObjectType storeObjectType)
		{
			EnumValidator.ThrowIfInvalid<StoreObjectType>(storeObjectType, "storeObjectType");
			EnumValidator.ThrowIfInvalid<SharingPolicyAction>(allowedActions, "allowedActions");
			MemberRights memberRights = MemberRights.None;
			foreach (PolicyAllowedMemberRights policyAllowedMemberRights in PolicyAllowedMemberRights.Rights)
			{
				if (policyAllowedMemberRights.storeObjectType == storeObjectType && (allowedActions & policyAllowedMemberRights.action) == policyAllowedMemberRights.action)
				{
					memberRights |= policyAllowedMemberRights.allowedRights;
				}
			}
			return memberRights;
		}

		// Token: 0x06004AC0 RID: 19136 RVA: 0x0013915C File Offset: 0x0013735C
		public static bool IsAllowedOnFolder(StoreObjectType storeObjectType)
		{
			EnumValidator.ThrowIfInvalid<StoreObjectType>(storeObjectType, "storeObjectType");
			foreach (PolicyAllowedMemberRights policyAllowedMemberRights in PolicyAllowedMemberRights.Rights)
			{
				if (policyAllowedMemberRights.storeObjectType == storeObjectType)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004AC1 RID: 19137 RVA: 0x0013919C File Offset: 0x0013739C
		private PolicyAllowedMemberRights(StoreObjectType storeObjectType, SharingPolicyAction action, MemberRights allowedRights)
		{
			this.storeObjectType = storeObjectType;
			this.action = action;
			this.allowedRights = allowedRights;
		}

		// Token: 0x0400288E RID: 10382
		private static readonly PolicyAllowedMemberRights[] Rights = new PolicyAllowedMemberRights[]
		{
			new PolicyAllowedMemberRights(StoreObjectType.CalendarFolder, SharingPolicyAction.CalendarSharingFreeBusySimple, MemberRights.Visible | MemberRights.FreeBusySimple),
			new PolicyAllowedMemberRights(StoreObjectType.CalendarFolder, SharingPolicyAction.CalendarSharingFreeBusyDetail, MemberRights.Visible | MemberRights.FreeBusySimple | MemberRights.FreeBusyDetailed),
			new PolicyAllowedMemberRights(StoreObjectType.CalendarFolder, SharingPolicyAction.CalendarSharingFreeBusyReviewer, MemberRights.ReadAny | MemberRights.Visible | MemberRights.FreeBusySimple | MemberRights.FreeBusyDetailed),
			new PolicyAllowedMemberRights(StoreObjectType.ContactsFolder, SharingPolicyAction.ContactsSharing, MemberRights.ReadAny | MemberRights.Visible)
		};

		// Token: 0x0400288F RID: 10383
		private StoreObjectType storeObjectType;

		// Token: 0x04002890 RID: 10384
		private SharingPolicyAction action;

		// Token: 0x04002891 RID: 10385
		private MemberRights allowedRights;
	}
}
