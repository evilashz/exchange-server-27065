using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AE2 RID: 2786
	[Cmdlet("Get", "OnPremisesOrganization", DefaultParameterSetName = "Identity")]
	public sealed class GetOnPremisesOrganization : GetMultitenancySystemConfigurationObjectTask<OnPremisesOrganizationIdParameter, OnPremisesOrganization>
	{
		// Token: 0x17001E01 RID: 7681
		// (get) Token: 0x060062F5 RID: 25333 RVA: 0x0019DC00 File Offset: 0x0019BE00
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
