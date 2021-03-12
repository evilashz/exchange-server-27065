using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000569 RID: 1385
	internal class TargetCopyMultiChecks : MultiReplicationCheck
	{
		// Token: 0x06003101 RID: 12545 RVA: 0x000C6E02 File Offset: 0x000C5002
		public TargetCopyMultiChecks(string serverName, IEventManager eventManager, string momEventSource, DatabaseHealthValidationRunner validationRunner, List<ReplayConfiguration> replayConfigs, Dictionary<Guid, RpcDatabaseCopyStatus2> copyStatuses, uint ignoreTransientErrorsThreshold) : base(serverName, eventManager, momEventSource, validationRunner, replayConfigs, copyStatuses, ignoreTransientErrorsThreshold)
		{
		}

		// Token: 0x06003102 RID: 12546 RVA: 0x000C6E18 File Offset: 0x000C5018
		protected override void Initialize()
		{
			List<IReplicationCheck> list = new List<IReplicationCheck>();
			list.Add(new ReplayServiceCheck(this.m_ServerName, this.m_EventManager, this.m_MomEventSource, new uint?(this.m_IgnoreTransientErrorsThreshold)));
			list.Add(new ActiveManagerCheck(this.m_ServerName, this.m_EventManager, this.m_MomEventSource, new uint?(this.m_IgnoreTransientErrorsThreshold)));
			list.Add(new TasksRpcListenerCheck(this.m_ServerName, this.m_EventManager, this.m_MomEventSource, new uint?(this.m_IgnoreTransientErrorsThreshold)));
			list.Add(new DatabasesSuspendedCheck(this.m_ServerName, this.m_EventManager, this.m_MomEventSource, this.m_ReplayConfigs, this.m_CopyStatuses));
			list.Add(new DatabasesFailedCheck(this.m_ServerName, this.m_EventManager, this.m_MomEventSource, this.m_ReplayConfigs, this.m_CopyStatuses));
			if (ReplicationCheckGlobals.UsingReplayRpc)
			{
				list.Add(new DatabasesInitializingCheck(this.m_ServerName, this.m_EventManager, this.m_MomEventSource, this.m_ReplayConfigs, this.m_CopyStatuses, this.m_IgnoreTransientErrorsThreshold));
				list.Add(new DatabasesDisconnectedCheck(this.m_ServerName, this.m_EventManager, this.m_MomEventSource, this.m_ReplayConfigs, this.m_CopyStatuses, this.m_IgnoreTransientErrorsThreshold));
				list.Add(new DatabaseRedundancyCheck(this.m_ServerName, this.m_EventManager, this.m_MomEventSource, this.m_ValidationRunner, this.m_IgnoreTransientErrorsThreshold));
				list.Add(new DatabaseAvailabilityCheck(this.m_ServerName, this.m_EventManager, this.m_MomEventSource, this.m_ValidationRunner, this.m_IgnoreTransientErrorsThreshold));
			}
			list.Add(new DatabasesCopyKeepingUpCheck(this.m_ServerName, this.m_EventManager, this.m_MomEventSource, this.m_ReplayConfigs, this.m_CopyStatuses, this.m_IgnoreTransientErrorsThreshold));
			list.Add(new DatabasesReplayKeepingUpCheck(this.m_ServerName, this.m_EventManager, this.m_MomEventSource, this.m_ReplayConfigs, this.m_CopyStatuses, this.m_IgnoreTransientErrorsThreshold));
			this.m_Checks = list.ToArray();
		}
	}
}
