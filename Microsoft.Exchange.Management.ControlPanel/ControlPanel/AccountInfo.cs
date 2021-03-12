using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200028A RID: 650
	public sealed class AccountInfo : Accounts, IAccountInfo, IAccounts, IDataSourceService<MailboxFilter, MailboxRecipientRow, Account, SetAccount, NewAccount, RemoveAccount>, IEditListService<MailboxFilter, MailboxRecipientRow, Account, NewAccount, RemoveAccount>, IGetListService<MailboxFilter, MailboxRecipientRow>, INewObjectService<MailboxRecipientRow, NewAccount>, IRemoveObjectsService<RemoveAccount>, IEditObjectForListService<Account, SetAccount, MailboxRecipientRow>, IGetObjectService<Account>, IGetObjectForListService<MailboxRecipientRow>
	{
		// Token: 0x17001CF6 RID: 7414
		// (get) Token: 0x06002A60 RID: 10848 RVA: 0x00084F8C File Offset: 0x0008318C
		protected override bool IsProfilePage
		{
			get
			{
				return true;
			}
		}
	}
}
