using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C56 RID: 3158
	public abstract class UpdatePowerShellCommonVirtualDirectoryVersion<T> : UpdateVirtualDirectoryVersion<T> where T : ADPowerShellCommonVirtualDirectory, new()
	{
		// Token: 0x1700250A RID: 9482
		// (get) Token: 0x060077E1 RID: 30689
		protected abstract PowerShellVirtualDirectoryType AllowedVirtualDirectoryType { get; }

		// Token: 0x060077E2 RID: 30690 RVA: 0x001E8A80 File Offset: 0x001E6C80
		protected override bool ShouldSkipVDir(T vDir)
		{
			return vDir.VirtualDirectoryType != this.AllowedVirtualDirectoryType;
		}
	}
}
