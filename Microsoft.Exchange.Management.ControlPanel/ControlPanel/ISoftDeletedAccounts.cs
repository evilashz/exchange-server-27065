using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002F1 RID: 753
	[ServiceContract(Namespace = "ECP", Name = "SoftDeletedAccounts")]
	public interface ISoftDeletedAccounts : IAccounts, IDataSourceService<MailboxFilter, MailboxRecipientRow, Account, SetAccount, NewAccount, RemoveAccount>, IEditListService<MailboxFilter, MailboxRecipientRow, Account, NewAccount, RemoveAccount>, IGetListService<MailboxFilter, MailboxRecipientRow>, INewObjectService<MailboxRecipientRow, NewAccount>, IRemoveObjectsService<RemoveAccount>, IEditObjectForListService<Account, SetAccount, MailboxRecipientRow>, IGetObjectService<Account>, IGetObjectForListService<MailboxRecipientRow>
	{
	}
}
