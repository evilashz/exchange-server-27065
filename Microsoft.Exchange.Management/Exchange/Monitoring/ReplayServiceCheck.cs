using System;
using System.ComponentModel;
using System.ServiceProcess;
using Microsoft.Exchange.Common.Cluster;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000547 RID: 1351
	internal class ReplayServiceCheck : ReplicationCheck
	{
		// Token: 0x06003045 RID: 12357 RVA: 0x000C3EF6 File Offset: 0x000C20F6
		public ReplayServiceCheck(string serverName, IEventManager eventManager, string momeventsource) : base("ReplayService", CheckId.ReplayService, Strings.ReplayServiceCheckDesc, CheckCategory.SystemHighPriority, eventManager, momeventsource, serverName)
		{
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x000C3F14 File Offset: 0x000C2114
		public ReplayServiceCheck(string serverName, IEventManager eventManager, string momeventsource, uint? ignoreTransientErrorsThreshold) : base("ReplayService", CheckId.ReplayService, Strings.ReplayServiceCheckDesc, CheckCategory.SystemHighPriority, eventManager, momeventsource, serverName, ignoreTransientErrorsThreshold)
		{
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x000C3F3C File Offset: 0x000C213C
		protected override void InternalRun()
		{
			if (!ReplicationCheckGlobals.ReplayServiceCheckHasRun)
			{
				ReplicationCheckGlobals.ReplayServiceCheckHasRun = true;
			}
			else
			{
				ExTraceGlobals.HealthChecksTracer.TraceDebug((long)this.GetHashCode(), "ReplayServiceCheck skipping because it has already been run once.");
				base.Skip();
			}
			if ((ReplicationCheckGlobals.ServerConfiguration & ServerConfig.Stopped) == ServerConfig.Stopped)
			{
				ExTraceGlobals.HealthChecksTracer.TraceDebug<string>((long)this.GetHashCode(), "Stopped server! Skipping {0}.", base.Title);
				base.Skip();
			}
			this.CheckServiceOnNode(base.ServerName);
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x000C3FAC File Offset: 0x000C21AC
		private void CheckServiceOnNode(string machine)
		{
			string text = string.Empty;
			if (string.IsNullOrEmpty(machine))
			{
				text = Environment.MachineName;
			}
			else
			{
				text = machine;
			}
			ServiceController serviceController;
			if (Cluster.StringIEquals(text, Environment.MachineName))
			{
				serviceController = new ServiceController("msexchangerepl");
			}
			else
			{
				serviceController = new ServiceController("msexchangerepl", text);
			}
			using (serviceController)
			{
				try
				{
					if (serviceController.Status != ServiceControllerStatus.Running)
					{
						base.FailContinue(Strings.ReplayServiceNotRunning(text));
					}
				}
				catch (Win32Exception ex)
				{
					base.FailContinue(Strings.ErrorReadingServiceState(text, ex.Message));
				}
				catch (InvalidOperationException ex2)
				{
					base.FailContinue(Strings.ErrorReadingServiceState(text, ex2.Message));
				}
			}
		}

		// Token: 0x0400224B RID: 8779
		private const string ReplayService = "msexchangerepl";
	}
}
