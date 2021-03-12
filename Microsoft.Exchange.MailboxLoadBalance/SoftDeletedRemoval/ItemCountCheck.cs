using System;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.Providers;

namespace Microsoft.Exchange.MailboxLoadBalance.SoftDeletedRemoval
{
	// Token: 0x020000FB RID: 251
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ItemCountCheck : SoftDeletedMailboxRemovalCheck
	{
		// Token: 0x0600078D RID: 1933 RVA: 0x000152B9 File Offset: 0x000134B9
		public ItemCountCheck(SoftDeletedRemovalData data, IDirectoryProvider directory) : base(data, directory)
		{
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x000152E0 File Offset: 0x000134E0
		protected override SoftDeleteMailboxRemovalCheckRemoval CheckRemoval()
		{
			DirectoryIdentity targetDatabase = base.Data.TargetDatabase;
			DirectoryMailbox mailbox = base.TargetDatabase.GetMailbox(base.Data.MailboxIdentity);
			IPhysicalMailbox physicalMailbox = mailbox.PhysicalMailboxes.FirstOrDefault((IPhysicalMailbox mbx) => mbx.Guid == base.Data.MailboxIdentity.Guid);
			if (physicalMailbox == null)
			{
				return SoftDeleteMailboxRemovalCheckRemoval.DisallowRemoval("An active mailbox for {0} could not be found.", new object[]
				{
					base.Data.MailboxIdentity
				});
			}
			if (base.Data.ItemCount > (long)physicalMailbox.ItemCount)
			{
				string reasonMessage = "Cannot remove soft deleted mailbox {0} because its ItemCount is {1} which is greater than the active copy's ItemCount of {2} on database '{3}'.";
				return SoftDeleteMailboxRemovalCheckRemoval.DisallowRemoval(reasonMessage, new object[]
				{
					base.Data.MailboxIdentity.Guid,
					base.Data.ItemCount,
					physicalMailbox.ItemCount,
					targetDatabase.Name
				});
			}
			return null;
		}
	}
}
