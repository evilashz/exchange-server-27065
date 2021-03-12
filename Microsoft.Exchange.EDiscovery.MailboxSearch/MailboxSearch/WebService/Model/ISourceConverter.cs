using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model
{
	// Token: 0x0200002A RID: 42
	internal interface ISourceConverter
	{
		// Token: 0x060001F0 RID: 496
		IEnumerable<SearchSource> Convert(ISearchPolicy policy, IEnumerable<SearchSource> sources);
	}
}
