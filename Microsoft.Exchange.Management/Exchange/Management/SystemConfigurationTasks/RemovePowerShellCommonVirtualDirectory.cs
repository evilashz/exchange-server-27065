using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C3A RID: 3130
	public abstract class RemovePowerShellCommonVirtualDirectory<T> : RemoveExchangeVirtualDirectory<T> where T : ADPowerShellCommonVirtualDirectory, new()
	{
		// Token: 0x1700247D RID: 9341
		// (get) Token: 0x06007674 RID: 30324
		protected abstract PowerShellVirtualDirectoryType AllowedVirtualDirectoryType { get; }

		// Token: 0x06007675 RID: 30325 RVA: 0x001E3A5C File Offset: 0x001E1C5C
		protected override void InternalValidate()
		{
			base.InternalValidate();
			T dataObject = base.DataObject;
			if (dataObject.VirtualDirectoryType != this.AllowedVirtualDirectoryType)
			{
				base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound((this.Identity != null) ? this.Identity.ToString() : null, typeof(T).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), (ErrorCategory)1003, null);
			}
		}
	}
}
