using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.PolicyNudges
{
	// Token: 0x02000065 RID: 101
	[Cmdlet("Set", "PolicyTipConfig", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetPolicyTipConfig : SetSystemConfigurationObjectTask<PolicyTipConfigIdParameter, PolicyTipMessageConfig>
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000DA38 File Offset: 0x0000BC38
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetPolicyTipConfig(this.Identity.ToString());
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000DA4A File Offset: 0x0000BC4A
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.DataObject.Action == PolicyTipMessageConfigAction.Url && !NewPolicyTipConfig.IsAbsoluteUri(base.DynamicParametersInstance.Value))
			{
				base.WriteError(new NewPolicyTipConfigInvalidUrlException(), ErrorCategory.InvalidArgument, null);
			}
			TaskLogger.LogExit();
		}
	}
}
