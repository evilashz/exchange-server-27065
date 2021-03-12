using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000073 RID: 115
	[Cmdlet("Get", "ActiveSyncOrganizationSettings", DefaultParameterSetName = "Identity")]
	public sealed class GetActiveSyncOrganizationSettings : GetMultitenancySystemConfigurationObjectTask<ActiveSyncOrganizationSettingsIdParameter, ActiveSyncOrganizationSettings>
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000F760 File Offset: 0x0000D960
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
