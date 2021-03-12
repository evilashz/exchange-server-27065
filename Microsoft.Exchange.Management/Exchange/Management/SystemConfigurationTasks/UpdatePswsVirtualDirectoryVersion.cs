using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C58 RID: 3160
	[Cmdlet("Update", "PswsVirtualDirectoryVersion", SupportsShouldProcess = true)]
	public sealed class UpdatePswsVirtualDirectoryVersion : UpdatePowerShellCommonVirtualDirectoryVersion<ADPswsVirtualDirectory>
	{
		// Token: 0x1700250C RID: 9484
		// (get) Token: 0x060077E7 RID: 30695 RVA: 0x001E8AFE File Offset: 0x001E6CFE
		protected override PowerShellVirtualDirectoryType AllowedVirtualDirectoryType
		{
			get
			{
				return PowerShellVirtualDirectoryType.Psws;
			}
		}

		// Token: 0x060077E8 RID: 30696 RVA: 0x001E8B04 File Offset: 0x001E6D04
		protected override bool ShouldUpdateVirtualDirectory()
		{
			bool flag = base.ShouldUpdateVirtualDirectory();
			if (!flag && base.Server.IsMailboxServer)
			{
				flag = true;
			}
			return flag;
		}
	}
}
