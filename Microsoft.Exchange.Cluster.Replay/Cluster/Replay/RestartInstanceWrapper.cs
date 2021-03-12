using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200030B RID: 779
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RestartInstanceWrapper
	{
		// Token: 0x06001FED RID: 8173 RVA: 0x00093748 File Offset: 0x00091948
		internal RestartInstanceWrapper(ReplicaInstanceContainer oldReplicaInstance)
		{
			this.OldReplicaInstance = oldReplicaInstance;
			this.IdentityGuid = oldReplicaInstance.ReplicaInstance.Configuration.IdentityGuid;
			this.Identity = oldReplicaInstance.ReplicaInstance.Configuration.Identity;
			this.DisplayName = oldReplicaInstance.ReplicaInstance.Configuration.DisplayName;
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06001FEE RID: 8174 RVA: 0x000937A4 File Offset: 0x000919A4
		// (set) Token: 0x06001FEF RID: 8175 RVA: 0x000937AC File Offset: 0x000919AC
		internal ReplicaInstanceContainer OldReplicaInstance { get; private set; }

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x000937B5 File Offset: 0x000919B5
		// (set) Token: 0x06001FF1 RID: 8177 RVA: 0x000937BD File Offset: 0x000919BD
		internal Guid IdentityGuid { get; private set; }

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06001FF2 RID: 8178 RVA: 0x000937C6 File Offset: 0x000919C6
		// (set) Token: 0x06001FF3 RID: 8179 RVA: 0x000937CE File Offset: 0x000919CE
		internal string Identity { get; private set; }

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06001FF4 RID: 8180 RVA: 0x000937D7 File Offset: 0x000919D7
		// (set) Token: 0x06001FF5 RID: 8181 RVA: 0x000937DF File Offset: 0x000919DF
		internal string DisplayName { get; private set; }
	}
}
