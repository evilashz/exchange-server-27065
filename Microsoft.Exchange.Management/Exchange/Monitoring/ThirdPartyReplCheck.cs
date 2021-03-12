using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200056E RID: 1390
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ThirdPartyReplCheck : ReplicationCheck
	{
		// Token: 0x0600310E RID: 12558 RVA: 0x000C753F File Offset: 0x000C573F
		public ThirdPartyReplCheck(string serverName, IEventManager eventManager, string momeventsource) : base("ThirdPartyReplication", CheckId.ThirdPartyReplication, Strings.ThirdPartyReplCheckDesc, CheckCategory.SystemHighPriority, eventManager, momeventsource, serverName)
		{
		}

		// Token: 0x0600310F RID: 12559 RVA: 0x000C755C File Offset: 0x000C575C
		public ThirdPartyReplCheck(string serverName, IEventManager eventManager, string momeventsource, uint? ignoreTransientErrorsThreshold) : base("ThirdPartyReplication", CheckId.ThirdPartyReplication, Strings.ThirdPartyReplCheckDesc, CheckCategory.SystemHighPriority, eventManager, momeventsource, serverName, ignoreTransientErrorsThreshold)
		{
		}

		// Token: 0x06003110 RID: 12560 RVA: 0x000C7584 File Offset: 0x000C5784
		protected override void InternalRun()
		{
			if (!ReplicationCheckGlobals.ThirdPartyReplCheckHasRun)
			{
				ReplicationCheckGlobals.ThirdPartyReplCheckHasRun = true;
			}
			else
			{
				ExTraceGlobals.HealthChecksTracer.TraceDebug((long)this.GetHashCode(), "ThirdPartyReplCheck skipping because it has already been run once.");
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
			string error = null;
			bool flag = false;
			Exception ex = null;
			try
			{
				if (AmRpcVersionControl.IsCheckThirdPartyListenerSupported(ReplicationCheckGlobals.Server.AdminDisplayVersion))
				{
					if (ReplicationCheckGlobals.WriteVerboseDelegate != null)
					{
						ReplicationCheckGlobals.WriteVerboseDelegate(Strings.TestRHCheckTPRListener(ReplicationCheckGlobals.Server.AdminDisplayVersion.ToString(), AmRpcVersionControl.ThirdPartyReplListenerSupportedVersion.ToString()));
					}
					flag = AmRpcClientHelper.CheckThirdPartyListener(base.ServerName, out error);
				}
				else
				{
					ExTraceGlobals.HealthChecksTracer.TraceDebug<string>((long)this.GetHashCode(), "ThirdPartyReplCheck skipping because server {0} doesn't support the CheckThirdPartyListener RPC.", base.ServerName);
					base.Skip();
				}
			}
			catch (AmServerException ex2)
			{
				ex = ex2;
			}
			catch (AmServerTransientException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				base.Fail(Strings.ErrorCheckingTPRListener(base.ServerName, ex.Message));
			}
			ExTraceGlobals.HealthChecksTracer.TraceDebug<string, bool>((long)this.GetHashCode(), "TPR is healthy on server {0}: {1}", base.ServerName, flag);
			if (!flag)
			{
				base.Fail(Strings.TPRListenerNotHealthy(base.ServerName, error));
			}
		}
	}
}
