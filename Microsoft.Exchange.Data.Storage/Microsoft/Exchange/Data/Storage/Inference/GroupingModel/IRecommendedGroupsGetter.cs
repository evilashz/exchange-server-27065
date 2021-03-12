using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Inference.GroupingModel
{
	// Token: 0x02000F5B RID: 3931
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IRecommendedGroupsGetter
	{
		// Token: 0x060086B1 RID: 34481
		IReadOnlyList<IRecommendedGroupInfo> GetRecommendedGroups(MailboxSession session, Action<string> traceDelegate, Action<Exception> traceErrorDelegate);
	}
}
