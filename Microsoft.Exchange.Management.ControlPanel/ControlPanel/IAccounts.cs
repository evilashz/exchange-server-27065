using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000287 RID: 647
	[ServiceContract(Namespace = "ECP", Name = "Accounts")]
	public interface IAccounts : IDataSourceService<MailboxFilter, MailboxRecipientRow, Account, SetAccount, NewAccount, RemoveAccount>, IEditListService<MailboxFilter, MailboxRecipientRow, Account, NewAccount, RemoveAccount>, IGetListService<MailboxFilter, MailboxRecipientRow>, INewObjectService<MailboxRecipientRow, NewAccount>, IRemoveObjectsService<RemoveAccount>, IEditObjectForListService<Account, SetAccount, MailboxRecipientRow>, IGetObjectService<Account>, IGetObjectForListService<MailboxRecipientRow>
	{
	}
}
