using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200033A RID: 826
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class MoveContactFolder : ServiceCommand<ContactFolderResponse>
	{
		// Token: 0x06001B64 RID: 7012 RVA: 0x000680DD File Offset: 0x000662DD
		public MoveContactFolder(CallContext callContext, FolderId folderId, int priority) : base(callContext)
		{
			this.folderId = folderId;
			this.priority = priority;
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x000680F4 File Offset: 0x000662F4
		protected override ContactFolderResponse InternalExecute()
		{
			ExchangeVersion.Current = ExchangeVersion.Latest;
			MailboxSession mailboxIdentityMailboxSession = base.MailboxIdentityMailboxSession;
			IdAndSession idAndSession = base.IdConverter.ConvertFolderIdToIdAndSession(this.folderId, IdConverter.ConvertOption.IgnoreChangeKey);
			StoreId id;
			try
			{
				using (Folder folder = Folder.Bind(mailboxIdentityMailboxSession, idAndSession.Id))
				{
					PeopleFilterGroupPriorityManager.SetSortGroupPriorityOnFolder(folder, this.priority);
					folder.Save();
					folder.Load(new PropertyDefinition[]
					{
						FolderSchema.Id
					});
					id = folder.Id;
				}
			}
			catch (ObjectNotFoundException)
			{
				return new ContactFolderResponse
				{
					ResponseCode = ResponseCodeType.ErrorItemNotFound.ToString()
				};
			}
			PeopleFilterGroupPriorityManager peopleFilterGroupPriorityManager = new PeopleFilterGroupPriorityManager(mailboxIdentityMailboxSession, new XSOFactory());
			mailboxIdentityMailboxSession.ContactFolders.MyContactFolders.Set(peopleFilterGroupPriorityManager.GetMyContactFolderIds());
			ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(id, new MailboxId(mailboxIdentityMailboxSession), null);
			return new ContactFolderResponse
			{
				ResponseCode = ResponseCodeType.NoError.ToString(),
				FolderId = new FolderId
				{
					Id = concatenatedId.Id,
					ChangeKey = concatenatedId.ChangeKey
				}
			};
		}

		// Token: 0x04000F53 RID: 3923
		private readonly FolderId folderId;

		// Token: 0x04000F54 RID: 3924
		private readonly int priority;
	}
}
