using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DA9 RID: 3497
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PolicyAllowedDetailLevel
	{
		// Token: 0x06007820 RID: 30752 RVA: 0x00212314 File Offset: 0x00210514
		public static int GetMaxAllowed(SharingPolicyAction allowedActions)
		{
			EnumValidator.ThrowIfInvalid<SharingPolicyAction>(allowedActions, "allowedActions");
			int num = 0;
			foreach (PolicyAllowedDetailLevel policyAllowedDetailLevel in PolicyAllowedDetailLevel.Rules)
			{
				if ((allowedActions & policyAllowedDetailLevel.action) == policyAllowedDetailLevel.action && num < (int)policyAllowedDetailLevel.maxAllowedDetailLevel)
				{
					num = (int)policyAllowedDetailLevel.maxAllowedDetailLevel;
				}
			}
			return num;
		}

		// Token: 0x06007821 RID: 30753 RVA: 0x00212367 File Offset: 0x00210567
		private PolicyAllowedDetailLevel(SharingPolicyAction action, DetailLevelEnumType maxAllowedDetailLevel)
		{
			this.action = action;
			this.maxAllowedDetailLevel = maxAllowedDetailLevel;
		}

		// Token: 0x04005324 RID: 21284
		private static readonly PolicyAllowedDetailLevel[] Rules = new PolicyAllowedDetailLevel[]
		{
			new PolicyAllowedDetailLevel(SharingPolicyAction.CalendarSharingFreeBusySimple, DetailLevelEnumType.AvailabilityOnly),
			new PolicyAllowedDetailLevel(SharingPolicyAction.CalendarSharingFreeBusyDetail, DetailLevelEnumType.LimitedDetails),
			new PolicyAllowedDetailLevel(SharingPolicyAction.CalendarSharingFreeBusyReviewer, DetailLevelEnumType.FullDetails)
		};

		// Token: 0x04005325 RID: 21285
		private readonly SharingPolicyAction action;

		// Token: 0x04005326 RID: 21286
		private readonly DetailLevelEnumType maxAllowedDetailLevel;
	}
}
