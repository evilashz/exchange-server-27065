using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000F5 RID: 245
	[Cmdlet("Enable", "UMCallAnsweringRule", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class EnableUMCallAnsweringRule : EnableDisableUMCallAnsweringRuleBase
	{
		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x00042AA7 File Offset: 0x00040CA7
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageEnableCallAnsweringRule(this.Identity.ToString());
			}
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x00042AB9 File Offset: 0x00040CB9
		public EnableUMCallAnsweringRule() : base(true)
		{
		}
	}
}
