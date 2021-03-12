using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.PolicyNudges
{
	// Token: 0x02000064 RID: 100
	[Cmdlet("Remove", "PolicyTipConfig", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemovePolicyTipConfig : RemoveSystemConfigurationObjectTask<PolicyTipConfigIdParameter, PolicyTipMessageConfig>
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000DA1E File Offset: 0x0000BC1E
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemovePolicyTipConfig(this.Identity.ToString());
			}
		}
	}
}
