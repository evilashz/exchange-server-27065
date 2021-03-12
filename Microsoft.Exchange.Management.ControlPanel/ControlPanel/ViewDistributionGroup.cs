using System;
using System.Web.UI.HtmlControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000241 RID: 577
	public class ViewDistributionGroup : BaseForm
	{
		// Token: 0x06002839 RID: 10297 RVA: 0x0007D9AC File Offset: 0x0007BBAC
		protected override void OnPreRender(EventArgs e)
		{
			this.EnsureChildControls();
			if (!base.ReadOnly)
			{
				Properties properties = (Properties)base.ContentControl;
				HtmlButton commitButton = base.CommitButton;
				properties.AddBinding("ActionShown", commitButton, "innerHTML");
				properties.AddBinding("CommitConfirmMessage", base.CommitButton, "confirmMessage");
				properties.AddBinding("CommitConfirmMessageTargetName", base.CommitButton, "confirmMessageTarget");
				base.CancelButtonText = ClientStrings.Close;
			}
			base.OnPreRender(e);
		}
	}
}
