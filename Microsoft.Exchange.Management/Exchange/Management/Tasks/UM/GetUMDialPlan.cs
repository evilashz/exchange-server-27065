using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D20 RID: 3360
	[Cmdlet("Get", "UMDialPlan", DefaultParameterSetName = "Identity")]
	public sealed class GetUMDialPlan : GetMultitenancySystemConfigurationObjectTask<UMDialPlanIdParameter, UMDialPlan>
	{
		// Token: 0x170027FF RID: 10239
		// (get) Token: 0x060080F9 RID: 33017 RVA: 0x0020FDE0 File Offset: 0x0020DFE0
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
