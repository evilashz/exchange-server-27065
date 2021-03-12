using System;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model
{
	// Token: 0x02000026 RID: 38
	internal interface ISearchResultProvider
	{
		// Token: 0x060001CC RID: 460
		SearchMailboxesResults Search(ISearchPolicy policy, SearchMailboxesInputs input);
	}
}
