using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model
{
	// Token: 0x02000024 RID: 36
	internal interface IServerProvider
	{
		// Token: 0x060001C8 RID: 456
		IEnumerable<FanoutParameters> GetServer(ISearchPolicy policy, IEnumerable<SearchSource> sources);
	}
}
