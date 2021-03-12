using System;
using System.Management.Automation.Host;
using System.Management.Automation.Runspaces;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.PowerShell.RbacHostingTools;
using Microsoft.Exchange.PowerShell.RbacHostingTools.Asp.Net;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.PowerShell.HostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200038C RID: 908
	public class EcpRunspaceFactory : RbacRunspaceFactory
	{
		// Token: 0x06003080 RID: 12416 RVA: 0x00093AC4 File Offset: 0x00091CC4
		public EcpRunspaceFactory(InitialSessionStateSectionFactory issFactory) : base(issFactory)
		{
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x00093ACD File Offset: 0x00091CCD
		public EcpRunspaceFactory(InitialSessionStateSectionFactory issFactory, PSHostFactory hostFactory) : base(issFactory, hostFactory)
		{
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x00093AD8 File Offset: 0x00091CD8
		internal override RunspaceServerSettings CreateRunspaceServerSettings()
		{
			string runspaceServerSettingsToken = this.GetRunspaceServerSettingsToken();
			if (runspaceServerSettingsToken == null)
			{
				return RunspaceServerSettings.CreateRunspaceServerSettings(false);
			}
			OrganizationId organizationId = RbacPrincipal.Current.RbacConfiguration.OrganizationId;
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Eac.OrgIdADSeverSettings.Enabled && organizationId != null && RbacPrincipal.Current.IsAdmin && !OrganizationId.ForestWideOrgId.Equals(organizationId) && !ADSessionSettings.IsForefrontObject(organizationId.PartitionId))
			{
				return RunspaceServerSettings.CreateGcOnlyRunspaceServerSettings(runspaceServerSettingsToken.ToLowerInvariant(), organizationId.PartitionId.ForestFQDN, false);
			}
			return RunspaceServerSettings.CreateGcOnlyRunspaceServerSettings(runspaceServerSettingsToken.ToLowerInvariant(), false);
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x00093B7C File Offset: 0x00091D7C
		protected override Runspace CreateRunspace(PSHost host)
		{
			Runspace result;
			using (new AverageTimePerfCounter(EcpPerfCounters.AveragePowerShellRunspaceCreation, EcpPerfCounters.AveragePowerShellRunspaceCreationBase, true))
			{
				using (EcpPerformanceData.CreateRunspace.StartRequestTimer())
				{
					Runspace runspace = base.CreateRunspace(host);
					EcpRunspaceFactory.runspaceCounters.Increment();
					result = runspace;
				}
			}
			return result;
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x00093BEC File Offset: 0x00091DEC
		protected override void OnRunspaceDisposed(Runspace runspace)
		{
			EcpRunspaceFactory.runspaceCounters.Decrement();
			base.OnRunspaceDisposed(runspace);
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x00093BFF File Offset: 0x00091DFF
		protected override void InitializeRunspace(Runspace runspace)
		{
			base.InitializeRunspace(runspace);
			runspace.SessionStateProxy.SetVariable("ExchangeDisableNotChangedWarning", true);
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x00093C20 File Offset: 0x00091E20
		protected override string GetRunspaceServerSettingsToken()
		{
			if (Datacenter.IsMultiTenancyEnabled() && RbacPrincipal.Current.IsAdmin && !OrganizationId.ForestWideOrgId.Equals(RbacPrincipal.Current.RbacConfiguration.OrganizationId))
			{
				return RunspaceServerSettings.GetTokenForOrganization(RbacPrincipal.Current.RbacConfiguration.OrganizationId);
			}
			return base.GetRunspaceServerSettingsToken();
		}

		// Token: 0x04002378 RID: 9080
		private static PerfCounterGroup runspaceCounters = new PerfCounterGroup(EcpPerfCounters.PowerShellRunspace, EcpPerfCounters.PowerShellRunspacePeak, EcpPerfCounters.PowerShellRunspaceTotal);
	}
}
