using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ProvisioningAgentTasks
{
	// Token: 0x02000CD5 RID: 3285
	[Cmdlet("Disable", "CmdletExtensionAgent", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class DisableCmdletExtensionAgent : FlipCmdletExtensionAgent
	{
		// Token: 0x17002758 RID: 10072
		// (get) Token: 0x06007EB6 RID: 32438 RVA: 0x00205C52 File Offset: 0x00203E52
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageDisableCmdletExtensionAgent(this.Identity.ToString());
			}
		}

		// Token: 0x17002759 RID: 10073
		// (get) Token: 0x06007EB7 RID: 32439 RVA: 0x00205C64 File Offset: 0x00203E64
		internal override bool FlipTo
		{
			get
			{
				return false;
			}
		}
	}
}
