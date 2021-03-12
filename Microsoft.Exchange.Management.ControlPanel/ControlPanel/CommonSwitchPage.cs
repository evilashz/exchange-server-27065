using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000039 RID: 57
	public class CommonSwitchPage : OrgSettingsPage
	{
		// Token: 0x06001951 RID: 6481 RVA: 0x0004F83C File Offset: 0x0004DA3C
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			string text = base.Request.QueryString["helpid"];
			if (!string.IsNullOrEmpty(text))
			{
				base.HelpId = text;
			}
		}
	}
}
