using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000563 RID: 1379
	internal class DatabasesFailedCheck : DatabaseCheck
	{
		// Token: 0x060030F0 RID: 12528 RVA: 0x000C6750 File Offset: 0x000C4950
		public DatabasesFailedCheck(string serverName, IEventManager eventManager, string momEventSource, List<ReplayConfiguration> replayConfigs, Dictionary<Guid, RpcDatabaseCopyStatus2> copyStatuses) : base("DBCopyFailed", CheckId.DatabasesFailed, Strings.DatabaseCopyStateCheckDesc(CopyStatus.Failed), eventManager, momEventSource, replayConfigs, copyStatuses, serverName, new uint?(0U))
		{
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x000C6780 File Offset: 0x000C4980
		protected override bool ShouldCheckConfig(ReplayConfiguration replayconfig)
		{
			bool result = IgnoreTransientErrors.HasPassed(base.GetDefaultErrorKey(typeof(DatabasesSuspendedCheck)));
			if (base.ShouldCheckConfig(replayconfig))
			{
				ExTraceGlobals.HealthChecksTracer.TraceDebug<string, string>((long)this.GetHashCode(), "ShouldCheckConfig(): Config '{0}': Dependent check SuspendCheck passed: {1}.", replayconfig.DisplayName, result.ToString());
				return result;
			}
			return false;
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x000C67D4 File Offset: 0x000C49D4
		protected override bool RunIndividualCheck(ReplayConfiguration configToCheck, RpcDatabaseCopyStatus2 copyStatus)
		{
			if ((this.UseReplayRpc() && copyStatus.CopyStatus == CopyStatusEnum.Failed) || (!this.UseReplayRpc() && configToCheck.ReplayState.ConfigBroken))
			{
				string text = this.UseReplayRpc() ? copyStatus.ErrorMessage : configToCheck.ReplayState.ConfigBrokenMessage;
				text = ((!string.IsNullOrEmpty(text)) ? text : Strings.ReplicationCheckBlankMessage);
				base.FailContinue(Strings.DatabaseCopyFailedCheck(new LocalizedReplayConfigType(configToCheck.Type).ToString(), configToCheck.DisplayName, CopyStatus.Failed.ToString(), base.ServerName, text), this.UseReplayRpc() ? copyStatus.ErrorEventId : 0U);
				return false;
			}
			return true;
		}
	}
}
