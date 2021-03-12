using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Inference.GroupingModel
{
	// Token: 0x02000F5C RID: 3932
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IRecommendedGroupsAccessor : IRecommendedGroupsGetter
	{
		// Token: 0x060086B2 RID: 34482
		void SetRecommendedGroups(MailboxSession session, RecommendedGroupsInfo groupsInfo, int version, Action<string> traceDelegate, Action<Exception> traceErrorDelegate);
	}
}
