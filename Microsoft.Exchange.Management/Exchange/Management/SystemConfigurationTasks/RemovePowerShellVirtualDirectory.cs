using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C3B RID: 3131
	[Cmdlet("Remove", "PowerShellVirtualDirectory", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemovePowerShellVirtualDirectory : RemovePowerShellCommonVirtualDirectory<ADPowerShellVirtualDirectory>
	{
		// Token: 0x1700247E RID: 9342
		// (get) Token: 0x06007677 RID: 30327 RVA: 0x001E3AE5 File Offset: 0x001E1CE5
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemovePowerShellVirtualDirectory(base.DataObject.Name, base.DataObject.Server.ToString());
			}
		}

		// Token: 0x1700247F RID: 9343
		// (get) Token: 0x06007678 RID: 30328 RVA: 0x001E3B07 File Offset: 0x001E1D07
		protected override PowerShellVirtualDirectoryType AllowedVirtualDirectoryType
		{
			get
			{
				return PowerShellVirtualDirectoryType.PowerShell;
			}
		}
	}
}
