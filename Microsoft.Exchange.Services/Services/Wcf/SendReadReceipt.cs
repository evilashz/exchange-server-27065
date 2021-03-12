using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000936 RID: 2358
	internal class SendReadReceipt : ServiceCommand<bool>
	{
		// Token: 0x0600445A RID: 17498 RVA: 0x000EA530 File Offset: 0x000E8730
		public SendReadReceipt(CallContext callContext, ItemId itemId) : base(callContext)
		{
			WcfServiceCommandBase.ThrowIfNull(itemId, "itemId", "SendReadReceipt::SendReadReceipt");
			this.session = callContext.SessionCache.GetMailboxIdentityMailboxSession();
			this.itemId = itemId;
		}

		// Token: 0x0600445B RID: 17499 RVA: 0x000EA564 File Offset: 0x000E8764
		protected override bool InternalExecute()
		{
			using (MessageItem messageItem = MessageItem.Bind(this.session, IdConverter.EwsIdToMessageStoreObjectId(this.itemId.Id)))
			{
				if (messageItem != null)
				{
					messageItem.SendReadReceipt();
					return true;
				}
			}
			return false;
		}

		// Token: 0x040027D4 RID: 10196
		private readonly MailboxSession session;

		// Token: 0x040027D5 RID: 10197
		private ItemId itemId;
	}
}
