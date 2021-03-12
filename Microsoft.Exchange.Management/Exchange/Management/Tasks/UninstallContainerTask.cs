using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002E8 RID: 744
	[Cmdlet("uninstall", "Container", SupportsShouldProcess = true)]
	public sealed class UninstallContainerTask : RemoveSystemConfigurationObjectTask<ContainerIdParameter, Container>
	{
		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x0007269E File Offset: 0x0007089E
		// (set) Token: 0x060019B2 RID: 6578 RVA: 0x000726C4 File Offset: 0x000708C4
		[Parameter]
		public SwitchParameter Recursive
		{
			get
			{
				return (SwitchParameter)(base.Fields["Recursive"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Recursive"] = value;
			}
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x000726DC File Offset: 0x000708DC
		protected override IConfigDataProvider CreateSession()
		{
			PartitionId partitionId = (this.Identity.InternalADObjectId != null) ? this.Identity.InternalADObjectId.GetPartitionId() : PartitionId.LocalForest;
			ADSessionSettings sessionSettings = ADSessionSettings.FromAccountPartitionWideScopeSet(partitionId);
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession((base.ServerSettings == null) ? null : base.ServerSettings.PreferredGlobalCatalog(partitionId.ForestFQDN), false, ConsistencyMode.PartiallyConsistent, sessionSettings, 49, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\UninstallContainerTask.cs");
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x00072750 File Offset: 0x00070950
		protected override void InternalValidate()
		{
			try
			{
				base.InternalValidate();
			}
			catch (ManagementObjectNotFoundException ex)
			{
				base.WriteWarning(ex.Message);
				this.validationFailed = true;
			}
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x000727A0 File Offset: 0x000709A0
		protected override void InternalProcessRecord()
		{
			if (!this.validationFailed)
			{
				if (!this.Recursive)
				{
					base.InternalProcessRecord();
					return;
				}
				base.WriteWarning("Recursive");
				((IConfigurationSession)base.DataSession).DeleteTree(base.DataObject, delegate(ADTreeDeleteNotFinishedException de)
				{
					if (de != null)
					{
						base.WriteVerbose(de.LocalizedString);
					}
				});
			}
		}

		// Token: 0x04000B1C RID: 2844
		private bool validationFailed;
	}
}
