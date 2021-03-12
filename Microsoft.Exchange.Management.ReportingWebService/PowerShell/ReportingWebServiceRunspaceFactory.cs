using System;
using System.Management.Automation.Host;
using System.Management.Automation.Runspaces;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.Authorization;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PowerShell.RbacHostingTools;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Management.ReportingWebService.PowerShell
{
	// Token: 0x02000014 RID: 20
	internal class ReportingWebServiceRunspaceFactory : RunspaceFactory
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00002FE3 File Offset: 0x000011E3
		public ReportingWebServiceRunspaceFactory() : base(new ReportingWebServiceInitialSessionStateFactory(), ReportingWebServiceHost.Factory)
		{
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003028 File Offset: 0x00001228
		protected override Runspace CreateRunspace(PSHost host)
		{
			Runspace runspace2;
			using (new AverageTimePerfCounter(RwsPerfCounters.AveragePowerShellRunspaceCreation, RwsPerfCounters.AveragePowerShellRunspaceCreationBase, true))
			{
				Runspace runspace = null;
				ElapsedTimeWatcher.Watch(RequestStatistics.RequestStatItem.PowerShellCreateRunspaceLatency, delegate
				{
					runspace = this.<>n__FabricatedMethod5(host);
				});
				ReportingWebServiceRunspaceFactory.runspaceCounters.Increment();
				runspace2 = runspace;
			}
			return runspace2;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000030F8 File Offset: 0x000012F8
		protected override void InitializeRunspace(Runspace runspace)
		{
			ElapsedTimeWatcher.Watch(RequestStatistics.RequestStatItem.InitializeRunspaceLatency, delegate
			{
				this.<>n__FabricatedMethod9(runspace);
				if (ReportingWebServiceRunspaceFactory.RunspaceServerSettingsEnabled.Value)
				{
					this.SetRunspaceServerSettings(runspace);
					return;
				}
				ElapsedTimeWatcher.WatchMessage("CRSS", "Skip");
			});
			ElapsedTimeWatcher.WatchMessage("TOKEN", this.GetUserToken());
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003158 File Offset: 0x00001358
		protected override void ConfigureRunspace(Runspace runspace)
		{
			ElapsedTimeWatcher.Watch(RequestStatistics.RequestStatItem.ConfigureRunspaceLatency, delegate
			{
				this.<>n__FabricatedMethodd(runspace);
			});
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000318C File Offset: 0x0000138C
		protected override void OnRunspaceDisposed(Runspace runspace)
		{
			ReportingWebServiceRunspaceFactory.runspaceCounters.Decrement();
			base.OnRunspaceDisposed(runspace);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000031A0 File Offset: 0x000013A0
		private void SetRunspaceServerSettings(Runspace runspace)
		{
			try
			{
				runspace.SessionStateProxy.SetVariable(ExchangePropertyContainer.ADServerSettingsVarName, this.CreateRunspaceServerSettings());
			}
			catch (ADTransientException)
			{
				throw;
			}
			catch (ADExternalException)
			{
				throw;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000321C File Offset: 0x0000141C
		private RunspaceServerSettings CreateRunspaceServerSettings()
		{
			RunspaceServerSettings settings = null;
			string token = this.GetUserToken();
			ElapsedTimeWatcher.Watch(RequestStatistics.RequestStatItem.CreateRunspaceServerSettingsLatency, delegate
			{
				settings = ((token != null) ? RunspaceServerSettings.CreateGcOnlyRunspaceServerSettings(token.ToLowerInvariant(), false) : RunspaceServerSettings.CreateRunspaceServerSettings(false));
			});
			return settings;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000325B File Offset: 0x0000145B
		private string GetUserToken()
		{
			if (RbacPrincipal.Current.ExecutingUserId == null)
			{
				return RbacPrincipal.Current.CacheKeys[0];
			}
			return RbacPrincipal.Current.ExecutingUserId.ToString();
		}

		// Token: 0x0400003B RID: 59
		private static readonly BoolAppSettingsEntry RunspaceServerSettingsEnabled = new BoolAppSettingsEntry("RunspaceServerSettingsEnabled", true, ExTraceGlobals.RunspaceConfigTracer);

		// Token: 0x0400003C RID: 60
		private static readonly PerfCounterGroup runspaceCounters = new PerfCounterGroup(RwsPerfCounters.PowerShellRunspace, RwsPerfCounters.PowerShellRunspacePeak, RwsPerfCounters.PowerShellRunspaceTotal);
	}
}
