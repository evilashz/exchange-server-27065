using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000319 RID: 793
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReplicaInstances : SafeInstanceTable<ReplicaInstance>
	{
		// Token: 0x060020A8 RID: 8360 RVA: 0x00096E58 File Offset: 0x00095058
		internal bool TryGetBackupReplicaInstance(Guid guid, out ReplicaInstance instance)
		{
			string identityFromGuid = ReplayConfiguration.GetIdentityFromGuid(guid);
			return base.TryGetInstance(identityFromGuid, out instance);
		}
	}
}
