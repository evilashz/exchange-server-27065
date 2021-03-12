using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Providers;

namespace Microsoft.Exchange.MailboxLoadBalance.SoftDeletedRemoval
{
	// Token: 0x020000FA RID: 250
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DisconnectDateCheck : SoftDeletedMailboxRemovalCheck
	{
		// Token: 0x0600078B RID: 1931 RVA: 0x000151E8 File Offset: 0x000133E8
		public DisconnectDateCheck(SoftDeletedRemovalData data, IDirectoryProvider directory, DateTime removalCutoffDate) : base(data, directory)
		{
			this.removalCutoffDate = removalCutoffDate;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x000151FC File Offset: 0x000133FC
		protected override SoftDeleteMailboxRemovalCheckRemoval CheckRemoval()
		{
			if (base.Data.DisconnectDate == null)
			{
				return SoftDeleteMailboxRemovalCheckRemoval.DisallowRemoval("Cannot remove soft deleted mailbox {0} because it does not have a disconnect date.", new object[]
				{
					base.Data.MailboxIdentity
				});
			}
			if (base.Data.DisconnectDate > this.removalCutoffDate)
			{
				string reasonMessage = "Cannot remove soft deleted mailbox {0} because its DisconnectDate is {1} and the minimum date for removal is {2}.";
				return SoftDeleteMailboxRemovalCheckRemoval.DisallowRemoval(reasonMessage, new object[]
				{
					base.Data.MailboxIdentity,
					base.Data.DisconnectDate,
					this.removalCutoffDate
				});
			}
			return null;
		}

		// Token: 0x040002E8 RID: 744
		private readonly DateTime removalCutoffDate;
	}
}
