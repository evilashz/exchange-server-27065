using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model
{
	// Token: 0x02000020 RID: 32
	internal interface IDirectoryProvider
	{
		// Token: 0x060001C1 RID: 449
		IEnumerable<SearchRecipient> Query(ISearchPolicy policy, DirectoryQueryParameters request);
	}
}
