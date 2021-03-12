using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x0200065B RID: 1627
	[Cmdlet("Get", "CountryList", DefaultParameterSetName = "Identity")]
	public sealed class GetCountryList : GetSystemConfigurationObjectTask<CountryListIdParameter, CountryList>
	{
		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x060038F6 RID: 14582 RVA: 0x000EEAE2 File Offset: 0x000ECCE2
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
