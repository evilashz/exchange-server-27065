using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C57 RID: 3159
	[Cmdlet("Update", "PowerShellVirtualDirectoryVersion", SupportsShouldProcess = true)]
	public sealed class UpdatePowerShellVirtualDirectoryVersion : UpdatePowerShellCommonVirtualDirectoryVersion<ADPowerShellVirtualDirectory>
	{
		// Token: 0x1700250B RID: 9483
		// (get) Token: 0x060077E4 RID: 30692 RVA: 0x001E8AA2 File Offset: 0x001E6CA2
		protected override PowerShellVirtualDirectoryType AllowedVirtualDirectoryType
		{
			get
			{
				return PowerShellVirtualDirectoryType.PowerShell;
			}
		}

		// Token: 0x060077E5 RID: 30693 RVA: 0x001E8AA8 File Offset: 0x001E6CA8
		protected override bool ShouldUpdateVirtualDirectory()
		{
			bool flag = base.ShouldUpdateVirtualDirectory();
			if (!flag && (base.Server.IsHubTransportServer || base.Server.IsMailboxServer || base.Server.IsUnifiedMessagingServer || base.Server.IsFrontendTransportServer))
			{
				flag = true;
			}
			return flag;
		}
	}
}
