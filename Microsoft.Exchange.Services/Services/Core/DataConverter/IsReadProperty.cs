using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200017D RID: 381
	internal sealed class IsReadProperty : SimpleProperty, IPostSavePropertyCommand
	{
		// Token: 0x06000AE3 RID: 2787 RVA: 0x000347C6 File Offset: 0x000329C6
		public IsReadProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x000347CF File Offset: 0x000329CF
		public new static IsReadProperty CreateCommand(CommandContext commandContext)
		{
			return new IsReadProperty(commandContext);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x000347D7 File Offset: 0x000329D7
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			this.isRead = setPropertyUpdate.ServiceObject.GetValueOrDefault<bool>(this.commandContext.PropertyInformation);
			this.suppressReadReceipts = updateCommandSettings.SuppressReadReceipts;
			EWSSettings.PostSavePropertyCommands.Add(updateCommandSettings.StoreObject.StoreObjectId, this);
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00034818 File Offset: 0x00032A18
		public void ExecutePostSaveOperation(StoreObject item)
		{
			StoreObjectId parentId = item.ParentId;
			MailboxSession mailboxSession = item.Session as MailboxSession;
			bool flag = this.suppressReadReceipts || (mailboxSession != null && mailboxSession.IsDefaultFolderType(parentId) == DefaultFolderType.JunkEmail);
			if (this.isRead)
			{
				item.Session.MarkAsRead(flag, new StoreId[]
				{
					item.StoreObjectId
				});
				return;
			}
			item.Session.MarkAsUnread(flag, new StoreId[]
			{
				item.StoreObjectId
			});
		}

		// Token: 0x040007DA RID: 2010
		private bool isRead;

		// Token: 0x040007DB RID: 2011
		private bool suppressReadReceipts;
	}
}
