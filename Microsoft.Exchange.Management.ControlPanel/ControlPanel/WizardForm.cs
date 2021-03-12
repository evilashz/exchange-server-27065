using System;
using System.Web.UI;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000CD RID: 205
	[ClientScriptResource("WizardForm", "Microsoft.Exchange.Management.ControlPanel.Client.Wizard.js")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class WizardForm : WizardFormBase
	{
		// Token: 0x1700194E RID: 6478
		// (get) Token: 0x06001D50 RID: 7504 RVA: 0x00059FDB File Offset: 0x000581DB
		// (set) Token: 0x06001D51 RID: 7505 RVA: 0x00059FE3 File Offset: 0x000581E3
		public string StartsWithStepID { get; private set; }

		// Token: 0x06001D52 RID: 7506 RVA: 0x00059FEC File Offset: 0x000581EC
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.StartsWithStepID = this.Page.Request.QueryString["StartsWith"];
			if (string.IsNullOrEmpty(this.StartsWithStepID))
			{
				Control control = this.FindFirstWizardStep(base.ContentPanel);
				this.StartsWithStepID = control.ID;
			}
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x0005A048 File Offset: 0x00058248
		private Control FindFirstWizardStep(Control control)
		{
			if (control is WizardStepBase)
			{
				return control;
			}
			if (control.HasControls())
			{
				for (int i = 0; i < control.Controls.Count; i++)
				{
					Control control2 = this.FindFirstWizardStep(control.Controls[i]);
					if (control2 != null)
					{
						return control2;
					}
				}
			}
			return null;
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x0005A096 File Offset: 0x00058296
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			descriptor.AddProperty("StartsWithStepID", this.StartsWithStepID, true);
			base.BuildScriptDescriptor(descriptor);
		}

		// Token: 0x04001BD0 RID: 7120
		private const string StartsWithQueryStringParameter = "StartsWith";
	}
}
