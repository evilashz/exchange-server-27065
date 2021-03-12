using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000031 RID: 49
	public class EditAddressList : OrgSettingsPage
	{
		// Token: 0x06001940 RID: 6464 RVA: 0x0004F12C File Offset: 0x0004D32C
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			bool flag = false;
			if (bool.TryParse(base.Request.QueryString["IsCustom"], out flag) && flag)
			{
				base.FooterPanel.State = ButtonsPanelState.ReadOnly;
			}
		}

		// Token: 0x04001AA4 RID: 6820
		private const string IsCustomKey = "IsCustom";
	}
}
