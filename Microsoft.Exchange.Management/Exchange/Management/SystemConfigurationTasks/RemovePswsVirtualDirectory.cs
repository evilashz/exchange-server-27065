using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C3C RID: 3132
	[Cmdlet("Remove", "PswsVirtualDirectory", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemovePswsVirtualDirectory : RemovePowerShellCommonVirtualDirectory<ADPswsVirtualDirectory>
	{
		// Token: 0x17002480 RID: 9344
		// (get) Token: 0x0600767A RID: 30330 RVA: 0x001E3B12 File Offset: 0x001E1D12
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemovePswsVirtualDirectory(base.DataObject.Name, base.DataObject.Server.ToString());
			}
		}

		// Token: 0x17002481 RID: 9345
		// (get) Token: 0x0600767B RID: 30331 RVA: 0x001E3B34 File Offset: 0x001E1D34
		protected override PowerShellVirtualDirectoryType AllowedVirtualDirectoryType
		{
			get
			{
				return PowerShellVirtualDirectoryType.Psws;
			}
		}
	}
}
