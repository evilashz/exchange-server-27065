using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000303 RID: 771
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class DeleteContactFolder : ServiceCommand<bool>
	{
		// Token: 0x060019D8 RID: 6616 RVA: 0x0005CCC6 File Offset: 0x0005AEC6
		public DeleteContactFolder(CallContext callContext, FolderId folderId) : base(callContext)
		{
			this.folderId = folderId;
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x0005CCD8 File Offset: 0x0005AED8
		protected override bool InternalExecute()
		{
			ExchangeVersion.Current = ExchangeVersion.Latest;
			MailboxSession mailboxIdentityMailboxSession = base.MailboxIdentityMailboxSession;
			IdAndSession idAndSession = base.IdConverter.ConvertFolderIdToIdAndSession(this.folderId, IdConverter.ConvertOption.IgnoreChangeKey);
			AggregateOperationResult aggregateOperationResult = mailboxIdentityMailboxSession.Delete(DeleteItemFlags.MoveToDeletedItems, new StoreId[]
			{
				idAndSession.Id
			});
			if (aggregateOperationResult.OperationResult == OperationResult.Succeeded)
			{
				PeopleFilterGroupPriorityManager peopleFilterGroupPriorityManager = new PeopleFilterGroupPriorityManager(mailboxIdentityMailboxSession, new XSOFactory());
				mailboxIdentityMailboxSession.ContactFolders.MyContactFolders.Set(peopleFilterGroupPriorityManager.GetMyContactFolderIds());
				return true;
			}
			return false;
		}

		// Token: 0x04000E49 RID: 3657
		private readonly FolderId folderId;
	}
}
