using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002BE RID: 702
	[Cmdlet("Get", "ADQueryPolicy", DefaultParameterSetName = "Identity")]
	public sealed class GetADQueryPolicy : GetSystemConfigurationObjectTask<ADQueryPolicyIdParameter, ADQueryPolicy>
	{
		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x060018AE RID: 6318 RVA: 0x0006906A File Offset: 0x0006726A
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x0006906D File Offset: 0x0006726D
		protected override ObjectId RootId
		{
			get
			{
				return this.ConfigurationSession.GetExchangeConfigurationContainer().Id.Parent.GetChildId("CN", "Windows NT");
			}
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x00069094 File Offset: 0x00067294
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, 43, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\DirectorySetup\\GetADQueryPolicy.cs");
		}
	}
}
