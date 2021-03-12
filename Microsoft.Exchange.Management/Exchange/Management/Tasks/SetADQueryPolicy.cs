using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002E4 RID: 740
	[Cmdlet("Set", "ADQueryPolicy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetADQueryPolicy : SetSystemConfigurationObjectTask<ADQueryPolicyIdParameter, ADQueryPolicy>
	{
		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x060019A7 RID: 6567 RVA: 0x000723A3 File Offset: 0x000705A3
		protected override ObjectId RootId
		{
			get
			{
				return this.ConfigurationSession.GetExchangeConfigurationContainer().Id.Parent.GetChildId("CN", "Windows NT");
			}
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x000723CC File Offset: 0x000705CC
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromRootOrgScopeSet(), 41, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\SetADQueryPolicy.cs");
		}
	}
}
