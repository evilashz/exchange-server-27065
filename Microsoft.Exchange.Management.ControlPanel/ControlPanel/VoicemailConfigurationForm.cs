using System;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000CE RID: 206
	public class VoicemailConfigurationForm : WizardForm
	{
		// Token: 0x06001D56 RID: 7510 RVA: 0x0005A0BC File Offset: 0x000582BC
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			CommonMaster commonMaster = (CommonMaster)base.Master;
			commonMaster.AddCssFiles("VoicemailSprite.css");
			this.txtPhoneNumber_Validator2.ValidationExpression = "^(?:\\([2-9]\\d{2}\\)\\ ?|[2-9]\\d{2}(?:\\-?|\\ ?))[2-9]\\d{2}[- ]?\\d{4}$";
		}

		// Token: 0x04001BD2 RID: 7122
		private const string PhoneNumberValidationRegRex = "^(?:\\([2-9]\\d{2}\\)\\ ?|[2-9]\\d{2}(?:\\-?|\\ ?))[2-9]\\d{2}[- ]?\\d{4}$";

		// Token: 0x04001BD3 RID: 7123
		protected EcpRegularExpressionValidator txtPhoneNumber_Validator2;
	}
}
