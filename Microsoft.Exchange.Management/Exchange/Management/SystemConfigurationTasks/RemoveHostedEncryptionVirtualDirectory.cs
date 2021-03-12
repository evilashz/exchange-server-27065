using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C34 RID: 3124
	[Cmdlet("Remove", "HostedEncryptionVirtualDirectory", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveHostedEncryptionVirtualDirectory : RemoveExchangeVirtualDirectory<ADE4eVirtualDirectory>
	{
		// Token: 0x17002477 RID: 9335
		// (get) Token: 0x0600765F RID: 30303 RVA: 0x001E34BA File Offset: 0x001E16BA
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveHostedEncryptionVirtualDirectory(this.Identity.ToString());
			}
		}
	}
}
