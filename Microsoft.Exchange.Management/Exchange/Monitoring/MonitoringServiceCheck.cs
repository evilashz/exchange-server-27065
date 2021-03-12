using System;
using System.Threading.Tasks;
using Microsoft.Exchange.Cluster.Replay.Monitoring;
using Microsoft.Exchange.Cluster.Replay.Monitoring.Client;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000546 RID: 1350
	internal class MonitoringServiceCheck : ReplicationCheck
	{
		// Token: 0x06003042 RID: 12354 RVA: 0x000C3CEF File Offset: 0x000C1EEF
		public MonitoringServiceCheck(string serverName, IEventManager eventManager, string momeventsource) : base("MonitoringService", CheckId.MonitoringService, Strings.MonitoringServiceCheckDesc, CheckCategory.SystemMediumPriority, eventManager, momeventsource, serverName)
		{
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x000C3D0C File Offset: 0x000C1F0C
		public MonitoringServiceCheck(string serverName, IEventManager eventManager, string momeventsource, uint? ignoreTransientErrorsThreshold) : base("MonitoringService", CheckId.MonitoringService, Strings.MonitoringServiceCheckDesc, CheckCategory.SystemMediumPriority, eventManager, momeventsource, serverName, ignoreTransientErrorsThreshold)
		{
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x000C3E28 File Offset: 0x000C2028
		protected override void InternalRun()
		{
			if (!ReplicationCheckGlobals.MonitoringServiceCheckHasRun)
			{
				ReplicationCheckGlobals.MonitoringServiceCheckHasRun = true;
			}
			else
			{
				ExTraceGlobals.HealthChecksTracer.TraceDebug((long)this.GetHashCode(), "MonitoringServiceCheck skipping because it has already been run once.");
				base.Skip();
			}
			if ((ReplicationCheckGlobals.ServerConfiguration & ServerConfig.Stopped) == ServerConfig.Stopped)
			{
				ExTraceGlobals.HealthChecksTracer.TraceDebug<string>((long)this.GetHashCode(), "Stopped server! Skipping {0}.", base.Title);
				base.Skip();
			}
			TimeSpan timeout = TimeSpan.FromSeconds(5.0);
			Exception ex = MonitoringServiceClient.HandleException(delegate
			{
				using (MonitoringServiceClient monitoringServiceClient = MonitoringServiceClient.Open(this.ServerName, timeout, timeout, timeout, timeout))
				{
					Task<ServiceVersion> versionAsync = monitoringServiceClient.GetVersionAsync();
					if (!versionAsync.Wait(timeout))
					{
						ExTraceGlobals.HealthChecksTracer.TraceError<string, TimeSpan>((long)this.GetHashCode(), "MonitoringServiceCheck: GetVersionAsync() call to server '{0}' timed out after '{1}'", this.ServerName, timeout);
						this.Fail(Strings.MonitoringServiceRequestTimedout(this.ServerName, timeout));
					}
					else
					{
						ExTraceGlobals.HealthChecksTracer.TraceDebug<string, long>((long)this.GetHashCode(), "MonitoringServiceCheck: GetVersionAsync() call to server '{0}' returned: {1}", this.ServerName, versionAsync.Result.Version);
					}
				}
			});
			if (ex != null)
			{
				ExTraceGlobals.HealthChecksTracer.TraceError<string, Exception>((long)this.GetHashCode(), "MonitoringServiceCheck: GetVersionAsync() call to server '{0}' failed with exception: {1}", base.ServerName, ex);
				base.Fail(Strings.MonitoringServiceRequestFailed(base.ServerName, ex.Message));
			}
		}
	}
}
