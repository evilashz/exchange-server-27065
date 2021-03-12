using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200056A RID: 1386
	internal class RcrSourceMultiChecks : MultiReplicationCheck
	{
		// Token: 0x06003103 RID: 12547 RVA: 0x000C7019 File Offset: 0x000C5219
		public RcrSourceMultiChecks(string serverName, IEventManager eventManager, string momEventSource, DatabaseHealthValidationRunner validationRunner, List<ReplayConfiguration> replayConfigs, Dictionary<Guid, RpcDatabaseCopyStatus2> copyStatuses, uint ignoreTransientErrorsThreshold) : base(serverName, eventManager, momEventSource, validationRunner, replayConfigs, copyStatuses, ignoreTransientErrorsThreshold)
		{
		}

		// Token: 0x06003104 RID: 12548 RVA: 0x000C702C File Offset: 0x000C522C
		protected override void Initialize()
		{
			this.m_Checks = new List<IReplicationCheck>
			{
				new ReplayServiceCheck(this.m_ServerName, this.m_EventManager, this.m_MomEventSource, new uint?(this.m_IgnoreTransientErrorsThreshold)),
				new ActiveManagerCheck(this.m_ServerName, this.m_EventManager, this.m_MomEventSource, new uint?(this.m_IgnoreTransientErrorsThreshold)),
				new TasksRpcListenerCheck(this.m_ServerName, this.m_EventManager, this.m_MomEventSource, new uint?(this.m_IgnoreTransientErrorsThreshold)),
				new DatabaseRedundancyCheck(this.m_ServerName, this.m_EventManager, this.m_MomEventSource, this.m_ValidationRunner, this.m_IgnoreTransientErrorsThreshold),
				new DatabaseAvailabilityCheck(this.m_ServerName, this.m_EventManager, this.m_MomEventSource, this.m_ValidationRunner, this.m_IgnoreTransientErrorsThreshold)
			}.ToArray();
		}
	}
}
