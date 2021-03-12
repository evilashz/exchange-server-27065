using System;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004D1 RID: 1233
	public class UMResetPinProperties : Properties
	{
		// Token: 0x06003C66 RID: 15462 RVA: 0x000B56CF File Offset: 0x000B38CF
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.SetDynamicLabels();
		}

		// Token: 0x06003C67 RID: 15463 RVA: 0x000B56E0 File Offset: 0x000B38E0
		private void SetDynamicLabels()
		{
			PowerShellResults<SetUMMailboxPinConfiguration> powerShellResults = (PowerShellResults<SetUMMailboxPinConfiguration>)base.Results;
			if (powerShellResults != null && powerShellResults.SucceededWithValue)
			{
				Section section = base.Sections["ResetUMPinSection"];
				RadioButtonList radioButtonList = (RadioButtonList)section.FindControl("rbResetPin");
				radioButtonList.Items[1].Text = Strings.UMMailboxPinDigitLabel(powerShellResults.Value.MinPinLength.ToString());
				EcpRegularExpressionValidator ecpRegularExpressionValidator = (EcpRegularExpressionValidator)section.FindControl("txtPin_Validator2");
				ecpRegularExpressionValidator.ValidationExpression = CommonRegex.NumbersOfSpecificLength(powerShellResults.Value.MinPinLength, 24).ToString();
				ecpRegularExpressionValidator.DefaultErrorMessage = Strings.UMResetPinManualPinError(powerShellResults.Value.MinPinLength);
			}
		}
	}
}
