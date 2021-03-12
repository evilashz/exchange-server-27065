using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.ControlPanel.DBMgmt
{
	// Token: 0x02000108 RID: 264
	public interface IServerResolver
	{
		// Token: 0x06001F8D RID: 8077
		IEnumerable<ServerResolverRow> ResolveObjects(IEnumerable<ADObjectId> identities);
	}
}
