using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000F4 RID: 244
	[Cmdlet("Disable", "UMCallAnsweringRule", SupportsShouldProcess = true, DefaultParameterSetName = "Identity", ConfirmImpact = ConfirmImpact.High)]
	public sealed class DisableUMCallAnsweringRule : EnableDisableUMCallAnsweringRuleBase
	{
		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x0600124B RID: 4683 RVA: 0x00042A8C File Offset: 0x00040C8C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageDisableCallAnsweringRule(this.Identity.ToString());
			}
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00042A9E File Offset: 0x00040C9E
		public DisableUMCallAnsweringRule() : base(false)
		{
		}
	}
}
