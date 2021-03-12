using System;
using System.Collections;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.FfoReporting.Providers
{
	// Token: 0x02000404 RID: 1028
	public interface IDalProvider
	{
		// Token: 0x06002420 RID: 9248
		IEnumerable GetSingleDataPage(string targetObjectTypeName, string dalObjectTypeName, string methodName, QueryFilter filter);

		// Token: 0x06002421 RID: 9249
		IEnumerable GetAllDataPages(string targetObjectTypeName, string dalObjectTypeName, string methodName, QueryFilter queryFilter);
	}
}
