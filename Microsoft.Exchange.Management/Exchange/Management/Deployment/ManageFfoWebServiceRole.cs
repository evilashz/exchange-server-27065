using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001EE RID: 494
	[ClassAccessLevel(AccessLevel.Consumer)]
	public abstract class ManageFfoWebServiceRole : ManageRole
	{
		// Token: 0x060010DB RID: 4315 RVA: 0x0004A67B File Offset: 0x0004887B
		protected override void InternalBeginProcessing()
		{
			this.ResolveDomainController();
			base.InternalBeginProcessing();
			base.ExecuteScript("Add-PSSnapin -Name Microsoft.Exchange.Management.PowerShell.Setup", true, 0, LocalizedString.Empty);
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x0004A69C File Offset: 0x0004889C
		private void ResolveDomainController()
		{
			if (base.DomainController == null)
			{
				base.DomainController = new Fqdn(DirectoryUtilities.PickLocalDomainController().DnsHostName);
			}
			ManageFfoWebServiceRole.SetPreferredDC(base.DomainController.Domain);
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x0004A6CC File Offset: 0x000488CC
		private static void SetPreferredDC(string domainController)
		{
			string dnsHostName = DirectoryUtilities.PickGlobalCatalog(domainController).DnsHostName;
			SetupServerSettings setupServerSettings = SetupServerSettings.CreateSetupServerSettings();
			setupServerSettings.SetConfigurationDomainController(TopologyProvider.LocalForestFqdn, new Fqdn(domainController));
			setupServerSettings.SetPreferredGlobalCatalog(TopologyProvider.LocalForestFqdn, new Fqdn(dnsHostName));
			setupServerSettings.AddPreferredDC(new Fqdn(domainController));
			ADSessionSettings.SetProcessADContext(new ADDriverContext(setupServerSettings, ContextMode.Setup));
		}
	}
}
