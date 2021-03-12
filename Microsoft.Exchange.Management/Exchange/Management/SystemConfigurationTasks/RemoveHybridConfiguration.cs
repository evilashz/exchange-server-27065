using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Hybrid;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008DA RID: 2266
	[Cmdlet("Remove", "HybridConfiguration", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveHybridConfiguration : RemoveSingletonSystemConfigurationObjectTask<HybridConfiguration>
	{
		// Token: 0x170017FF RID: 6143
		// (get) Token: 0x0600505D RID: 20573 RVA: 0x001503B8 File Offset: 0x0014E5B8
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return HybridStrings.RemoveHybidConfigurationConfirmation;
			}
		}
	}
}
