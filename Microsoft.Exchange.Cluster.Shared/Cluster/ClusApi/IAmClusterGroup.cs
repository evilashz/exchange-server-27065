using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000024 RID: 36
	internal interface IAmClusterGroup : IDisposable
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000150 RID: 336
		string Name { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000151 RID: 337
		AmGroupState State { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000152 RID: 338
		AmServerName OwnerNode { get; }

		// Token: 0x06000153 RID: 339
		bool IsCoreGroup();

		// Token: 0x06000154 RID: 340
		IEnumerable<AmClusterResource> EnumerateResourcesOfType(string resourceType);

		// Token: 0x06000155 RID: 341
		IEnumerable<AmClusterResource> EnumerateResources();

		// Token: 0x06000156 RID: 342
		IAmClusterResource CreateResource(string resName, string resType);

		// Token: 0x06000157 RID: 343
		IAmClusterResource FindResourceByTypeName(string desiredTypeName);

		// Token: 0x06000158 RID: 344
		void MoveGroup(TimeSpan timeout, AmServerName destinationNode);

		// Token: 0x06000159 RID: 345
		bool MoveGroupToReplayEnabledNode(IsReplayRunning isReplayRunning, string resourceType, TimeSpan timeout, out string finalDestinationNode);
	}
}
