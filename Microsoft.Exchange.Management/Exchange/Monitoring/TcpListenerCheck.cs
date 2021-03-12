using System;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200056D RID: 1389
	internal class TcpListenerCheck : ReplicationCheck
	{
		// Token: 0x0600310B RID: 12555 RVA: 0x000C73D8 File Offset: 0x000C55D8
		public TcpListenerCheck(string serverName, IEventManager eventManager, string momeventsource, IADDatabaseAvailabilityGroup dag) : base("TcpListener", CheckId.TcpListener, Strings.TcpListenerCheckDesc, CheckCategory.SystemHighPriority, eventManager, momeventsource, serverName)
		{
			if (dag != null)
			{
				this.replicationPort = dag.ReplicationPort;
			}
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x000C7410 File Offset: 0x000C5610
		public TcpListenerCheck(string serverName, IEventManager eventManager, string momeventsource, uint? ignoreTransientErrorsThreshold, IADDatabaseAvailabilityGroup dag) : base("TcpListener", CheckId.TcpListener, Strings.TcpListenerCheckDesc, CheckCategory.SystemHighPriority, eventManager, momeventsource, serverName, ignoreTransientErrorsThreshold)
		{
			if (dag != null)
			{
				this.replicationPort = dag.ReplicationPort;
			}
		}

		// Token: 0x0600310D RID: 12557 RVA: 0x000C7454 File Offset: 0x000C5654
		protected override void InternalRun()
		{
			if (!ReplicationCheckGlobals.TcpListenerCheckHasRun)
			{
				ReplicationCheckGlobals.TcpListenerCheckHasRun = true;
			}
			else
			{
				ExTraceGlobals.HealthChecksTracer.TraceDebug((long)this.GetHashCode(), "TcpListenerCheck skipping because it has already been run once.");
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
			string text = null;
			bool flag = TcpHealthCheck.TestHealth(base.ServerName, (int)this.replicationPort, 5000, out text);
			ExTraceGlobals.HealthChecksTracer.TraceDebug<string, bool, string>((long)this.GetHashCode(), "TcpListenerCheck: TestHealth() for server '{0}' returned: healthy={1}, errMsg='{2}'", base.ServerName, flag, text);
			if (!flag)
			{
				base.Fail(Strings.TcpListenerRequestFailed(base.ServerName, text));
			}
		}

		// Token: 0x040022B6 RID: 8886
		private const int TimeOutMs = 5000;

		// Token: 0x040022B7 RID: 8887
		private readonly ushort replicationPort = 64327;
	}
}
