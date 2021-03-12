using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A4E RID: 2638
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class RecoverableItemsQuotaHelper
	{
		// Token: 0x06006065 RID: 24677 RVA: 0x001966AC File Offset: 0x001948AC
		private static bool IsIncreasingQuotaEnabled(ADUser user)
		{
			if (user != null)
			{
				VariantConfigurationSnapshot snapshot = VariantConfiguration.GetSnapshot(user.GetContext(null), null, null);
				return snapshot.Ipaed.IncreaseQuotaForOnHoldMailboxes.Enabled;
			}
			return false;
		}

		// Token: 0x06006066 RID: 24678 RVA: 0x001966E0 File Offset: 0x001948E0
		public static void IncreaseRecoverableItemsQuotaIfNeeded(ADUser user)
		{
			if (RecoverableItemsQuotaHelper.IsIncreasingQuotaEnabled(user) && VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled && user.IsInLitigationHoldOrInplaceHold && ((user.UseDatabaseQuotaDefaults != null && user.UseDatabaseQuotaDefaults.Value) || (!user.RecoverableItemsQuota.IsUnlimited && user.RecoverableItemsQuota.Value.ToGB() < RecoverableItemsQuotaHelper.RecoverableItemsQuotaForMailboxesOnHoldInGB)))
			{
				user.UseDatabaseQuotaDefaults = new bool?(false);
				user.RecoverableItemsQuota = ByteQuantifiedSize.FromGB(RecoverableItemsQuotaHelper.RecoverableItemsQuotaForMailboxesOnHoldInGB);
				user.RecoverableItemsWarningQuota = ByteQuantifiedSize.FromGB(RecoverableItemsQuotaHelper.RecoverableItemsWarningQuotaForMailboxesOnHoldInGB);
			}
		}

		// Token: 0x040036E8 RID: 14056
		public static readonly ulong RecoverableItemsQuotaForMailboxesOnHoldInGB = 100UL;

		// Token: 0x040036E9 RID: 14057
		public static readonly ulong RecoverableItemsWarningQuotaForMailboxesOnHoldInGB = 90UL;
	}
}
