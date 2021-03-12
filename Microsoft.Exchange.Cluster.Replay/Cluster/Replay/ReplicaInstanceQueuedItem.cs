using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200030E RID: 782
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ReplicaInstanceQueuedItem : ReplayQueuedItemBase
	{
		// Token: 0x06002046 RID: 8262 RVA: 0x00096839 File Offset: 0x00094A39
		protected ReplicaInstanceQueuedItem(ReplicaInstance replicaInstance)
		{
			this.DbGuid = replicaInstance.Configuration.IdentityGuid;
			this.DbCopyName = replicaInstance.Configuration.DisplayName;
			this.DbName = replicaInstance.Configuration.DatabaseName;
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06002047 RID: 8263 RVA: 0x00096874 File Offset: 0x00094A74
		// (set) Token: 0x06002048 RID: 8264 RVA: 0x000968BC File Offset: 0x00094ABC
		public ReplicaInstance ReplicaInstance
		{
			get
			{
				if (this.m_instance == null)
				{
					ReplicaInstanceManager replicaInstanceManager = Dependencies.ReplayCoreManager.ReplicaInstanceManager;
					ReplicaInstance instance = null;
					if (!replicaInstanceManager.TryGetReplicaInstance(this.DbGuid, out instance))
					{
						Exception replicaInstanceNotFoundException = this.GetReplicaInstanceNotFoundException();
						throw replicaInstanceNotFoundException;
					}
					this.m_instance = instance;
				}
				return this.m_instance;
			}
			private set
			{
				this.m_instance = value;
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06002049 RID: 8265 RVA: 0x000968C5 File Offset: 0x00094AC5
		// (set) Token: 0x0600204A RID: 8266 RVA: 0x000968CD File Offset: 0x00094ACD
		public Guid DbGuid { get; private set; }

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x0600204B RID: 8267 RVA: 0x000968D6 File Offset: 0x00094AD6
		// (set) Token: 0x0600204C RID: 8268 RVA: 0x000968DE File Offset: 0x00094ADE
		public string DbCopyName { get; private set; }

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x0600204D RID: 8269 RVA: 0x000968E7 File Offset: 0x00094AE7
		// (set) Token: 0x0600204E RID: 8270 RVA: 0x000968EF File Offset: 0x00094AEF
		public string DbName { get; private set; }

		// Token: 0x0600204F RID: 8271 RVA: 0x000968F8 File Offset: 0x00094AF8
		public override bool IsEquivalentOrSuperset(IQueuedCallback otherCallback)
		{
			bool flag = base.IsEquivalentOrSuperset(otherCallback);
			if (!flag && otherCallback != null)
			{
				flag = (base.GetType() == otherCallback.GetType());
				if (flag)
				{
					ReplicaInstanceQueuedItem replicaInstanceQueuedItem = otherCallback as ReplicaInstanceQueuedItem;
					flag = this.DbGuid.Equals(replicaInstanceQueuedItem.DbGuid);
				}
			}
			return flag;
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x00096945 File Offset: 0x00094B45
		protected override void ExecuteInternal()
		{
			this.ReplicaInstance = null;
			this.CheckOperationApplicable();
		}

		// Token: 0x06002051 RID: 8273
		protected abstract void CheckOperationApplicable();

		// Token: 0x06002052 RID: 8274 RVA: 0x00096954 File Offset: 0x00094B54
		protected override Exception GetOperationCancelledException()
		{
			return new ReplayDatabaseOperationCancelledException(this.Name, this.DbCopyName);
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x00096967 File Offset: 0x00094B67
		protected override Exception GetOperationTimedoutException(TimeSpan timeout)
		{
			return new ReplayDatabaseOperationTimedoutException(this.Name, this.DbCopyName, timeout);
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x0009697B File Offset: 0x00094B7B
		protected virtual Exception GetReplicaInstanceNotFoundException()
		{
			return new ReplayServiceUnknownReplicaInstanceException(this.Name, this.DbCopyName);
		}

		// Token: 0x04000D35 RID: 3381
		private ReplicaInstance m_instance;
	}
}
