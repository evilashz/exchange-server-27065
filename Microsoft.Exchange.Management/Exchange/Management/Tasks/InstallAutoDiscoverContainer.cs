using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002CE RID: 718
	[Cmdlet("Install", "AutoDiscoverContainer")]
	public sealed class InstallAutoDiscoverContainer : NewFixedNameSystemConfigurationObjectTask<ADContainer>
	{
		// Token: 0x06001938 RID: 6456 RVA: 0x00070EDA File Offset: 0x0006F0DA
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 28, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\InstallAutoDiscoverContainer.cs");
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00070F04 File Offset: 0x0006F104
		protected override IConfigurable PrepareDataObject()
		{
			ADContainer adcontainer = (ADContainer)base.PrepareDataObject();
			ITopologyConfigurationSession topologyConfigurationSession = (ITopologyConfigurationSession)base.DataSession;
			ADObjectId autoDiscoverGlobalContainerId = topologyConfigurationSession.GetAutoDiscoverGlobalContainerId();
			adcontainer.SetId(autoDiscoverGlobalContainerId);
			return adcontainer;
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x00070F38 File Offset: 0x0006F138
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
