using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C15 RID: 3093
	[Cmdlet("Get", "PswsVirtualDirectory", DefaultParameterSetName = "Identity")]
	public sealed class GetPswsVirtualDirectory : GetPowerShellCommonVirtualDirectory<ADPswsVirtualDirectory>
	{
		// Token: 0x170023D7 RID: 9175
		// (get) Token: 0x060074AA RID: 29866 RVA: 0x001DC1A9 File Offset: 0x001DA3A9
		protected override PowerShellVirtualDirectoryType AllowedVirtualDirectoryType
		{
			get
			{
				return PowerShellVirtualDirectoryType.Psws;
			}
		}
	}
}
