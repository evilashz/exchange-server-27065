using System;
using System.ServiceModel;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003DF RID: 991
	[ServiceContract(Namespace = "ECP", Name = "MailboxSearches")]
	public interface IMailboxSearches : IDataSourceService<MailboxSearchFilter, MailboxSearchRow, MailboxSearch, SetMailboxSearchParameters, NewMailboxSearchParameters>, IDataSourceService<MailboxSearchFilter, MailboxSearchRow, MailboxSearch, SetMailboxSearchParameters, NewMailboxSearchParameters, BaseWebServiceParameters>, IEditListService<MailboxSearchFilter, MailboxSearchRow, MailboxSearch, NewMailboxSearchParameters, BaseWebServiceParameters>, IGetListService<MailboxSearchFilter, MailboxSearchRow>, INewObjectService<MailboxSearchRow, NewMailboxSearchParameters>, IRemoveObjectsService<BaseWebServiceParameters>, IEditObjectForListService<MailboxSearch, SetMailboxSearchParameters, MailboxSearchRow>, IGetObjectService<MailboxSearch>, IGetObjectForListService<MailboxSearchRow>
	{
		// Token: 0x06003324 RID: 13092
		[OperationContract]
		PowerShellResults<MailboxSearchRow> StartSearch(Identity[] identities, StartMailboxSearchParameters parameters);

		// Token: 0x06003325 RID: 13093
		[OperationContract]
		PowerShellResults<MailboxSearchRow> StopSearch(Identity[] identities, BaseWebServiceParameters parameters);
	}
}
