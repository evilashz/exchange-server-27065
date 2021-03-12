using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ProvisioningAgentTasks
{
	// Token: 0x02000CD6 RID: 3286
	[Cmdlet("Enable", "CmdletExtensionAgent", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class EnableCmdletExtensionAgent : FlipCmdletExtensionAgent
	{
		// Token: 0x1700275A RID: 10074
		// (get) Token: 0x06007EB9 RID: 32441 RVA: 0x00205C6F File Offset: 0x00203E6F
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageEnableCmdletExtensionAgent(this.Identity.ToString());
			}
		}

		// Token: 0x1700275B RID: 10075
		// (get) Token: 0x06007EBA RID: 32442 RVA: 0x00205C81 File Offset: 0x00203E81
		internal override bool FlipTo
		{
			get
			{
				return true;
			}
		}
	}
}
