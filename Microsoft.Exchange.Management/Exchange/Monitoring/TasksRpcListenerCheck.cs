using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200056C RID: 1388
	internal class TasksRpcListenerCheck : ReplicationCheck
	{
		// Token: 0x06003107 RID: 12551 RVA: 0x000C71B2 File Offset: 0x000C53B2
		public TasksRpcListenerCheck(string serverName, IEventManager eventManager, string momeventsource) : base("TasksRpcListener", CheckId.TasksRpcListener, Strings.TasksRpcListenerCheckDesc, CheckCategory.SystemHighPriority, eventManager, momeventsource, serverName)
		{
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x000C71D0 File Offset: 0x000C53D0
		public TasksRpcListenerCheck(string serverName, IEventManager eventManager, string momeventsource, uint? ignoreTransientErrorsThreshold) : base("TasksRpcListener", CheckId.TasksRpcListener, Strings.TasksRpcListenerCheckDesc, CheckCategory.SystemHighPriority, eventManager, momeventsource, serverName, ignoreTransientErrorsThreshold)
		{
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x000C71F8 File Offset: 0x000C53F8
		protected override void InternalRun()
		{
			if (!ReplicationCheckGlobals.TasksRpcListenerCheckHasRun)
			{
				ReplicationCheckGlobals.TasksRpcListenerCheckHasRun = true;
			}
			else
			{
				ExTraceGlobals.HealthChecksTracer.TraceDebug((long)this.GetHashCode(), "TasksRpcListenerCheck skipping because it has already been run once.");
				base.Skip();
			}
			if (!IgnoreTransientErrors.HasPassed(base.GetDefaultErrorKey(typeof(ReplayServiceCheck))))
			{
				ExTraceGlobals.HealthChecksTracer.TraceDebug<string>((long)this.GetHashCode(), "ReplayServiceCheck didn't pass! Skipping {0}.", base.Title);
				base.Skip();
			}
			if ((ReplicationCheckGlobals.ServerConfiguration & ServerConfig.Stopped) == ServerConfig.Stopped)
			{
				ExTraceGlobals.HealthChecksTracer.TraceDebug<string>((long)this.GetHashCode(), "Stopped server! Skipping {0}.", base.Title);
				base.Skip();
			}
			try
			{
				Dictionary<Guid, RpcDatabaseCopyStatus2> copyStatusRpcResults = this.GetCopyStatusRpcResults();
				if (copyStatusRpcResults != null)
				{
					ReplicationCheckGlobals.CopyStatusResults = copyStatusRpcResults;
				}
			}
			catch (TaskServerTransientException ex)
			{
				ExTraceGlobals.CmdletsTracer.TraceError<string>((long)this.GetHashCode(), "GetCopyStatusRpcResults(): TaskServerTransientException during GetCopyStatus RPC: {0}", ex.ToString());
				base.Fail(Strings.TasksRpcListenerRpcFailed(base.ServerName, ex.Message));
			}
			catch (TaskServerException ex2)
			{
				ExTraceGlobals.CmdletsTracer.TraceError<string>((long)this.GetHashCode(), "GetCopyStatusRpcResults(): TaskServerException during GetCopyStatus RPC: {0}", ex2.ToString());
				if (ex2 is ReplayServiceRpcUnknownInstanceException)
				{
					ExTraceGlobals.CmdletsTracer.TraceDebug((long)this.GetHashCode(), "GetCopyStatus() was not able to find a replica instance.");
				}
				else
				{
					base.Fail(Strings.TasksRpcListenerRpcFailed(base.ServerName, ex2.Message));
				}
			}
		}

		// Token: 0x0600310A RID: 12554 RVA: 0x000C7354 File Offset: 0x000C5554
		private Dictionary<Guid, RpcDatabaseCopyStatus2> GetCopyStatusRpcResults()
		{
			Dictionary<Guid, RpcDatabaseCopyStatus2> result = null;
			ServerVersion adminDisplayVersion = ReplicationCheckGlobals.Server.AdminDisplayVersion;
			if (ReplicationCheckGlobals.WriteVerboseDelegate != null)
			{
				ReplicationCheckGlobals.WriteVerboseDelegate(Strings.TestRHUseCopyStatusRpc(adminDisplayVersion.ToString(), ReplayRpcVersionControl.GetCopyStatusEx2SupportVersion.ToString()));
			}
			RpcDatabaseCopyStatus2[] copyStatus = ReplayRpcClientHelper.GetCopyStatus(base.ServerName, ReplicationCheckGlobals.RunningInMonitoringContext ? RpcGetDatabaseCopyStatusFlags2.None : RpcGetDatabaseCopyStatusFlags2.ReadThrough, null);
			if (copyStatus != null && copyStatus.Length > 0)
			{
				result = ReplayRpcClientHelper.ParseStatusResults(copyStatus);
				if (ReplicationCheckGlobals.WriteVerboseDelegate != null)
				{
					ReplicationCheckGlobals.WriteVerboseDelegate(Strings.TestRHRpcQueryAllDone(copyStatus.Length));
				}
			}
			return result;
		}
	}
}
