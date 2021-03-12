using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002CF RID: 719
	[Cmdlet("install", "ConfigurationUnitsContainer")]
	public sealed class InstallConfigurationUnitsContainerTask : NewFixedNameSystemConfigurationObjectTask<Container>
	{
		// Token: 0x0600193C RID: 6460 RVA: 0x00070F68 File Offset: 0x0006F168
		protected override IConfigDataProvider CreateSession()
		{
			List<ADServer> list = ADForest.GetLocalForest().FindAllGlobalCatalogsInLocalSite();
			if (list.Count == 0)
			{
				throw new CannotFindGlobalCatalogsInForest(ADForest.GetLocalForest().Fqdn);
			}
			return DirectorySessionFactory.Default.CreateTenantConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromAllTenantsPartitionId(list[0].Id.GetPartitionId()), 34, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\InstallConfigurationUnitsContainer.cs");
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00070FD4 File Offset: 0x0006F1D4
		protected override IConfigurable PrepareDataObject()
		{
			Container container = (Container)base.PrepareDataObject();
			container.SetId(ADSession.GetConfigurationUnitsRootForLocalForest());
			return container;
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x00070FFC File Offset: 0x0006F1FC
		protected override void InternalProcessRecord()
		{
			try
			{
				base.InternalProcessRecord();
			}
			catch (ADObjectAlreadyExistsException)
			{
			}
		}
	}
}
