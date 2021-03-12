using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x020005CF RID: 1487
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("EcpWizardStep", "Microsoft.Exchange.Management.ControlPanel.Client.Wizard.js")]
	public class EcpWizardStep : WizardStepBase
	{
		// Token: 0x06004333 RID: 17203 RVA: 0x000CBB14 File Offset: 0x000C9D14
		public EcpWizardStep()
		{
			this.ViewModel = "WizardStepViewModel";
			base.ClientClassName = "EcpWizardStep";
		}

		// Token: 0x06004334 RID: 17204 RVA: 0x000CBB34 File Offset: 0x000C9D34
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			ScriptManager.GetCurrent(this.Page).RegisterScriptControl<EcpWizardStep>(this);
			this.AddAttribute("data-type", this.ViewModel);
			this.AddAttribute("vm-ViewModelID", this.ID);
			this.AddAttribute("vm-NextViewModelID", base.NextStepID);
		}

		// Token: 0x06004335 RID: 17205 RVA: 0x000CBB8C File Offset: 0x000C9D8C
		private void AddAttribute(string name, string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				base.Attributes.Add(name, value);
			}
		}

		// Token: 0x17002616 RID: 9750
		// (get) Token: 0x06004336 RID: 17206 RVA: 0x000CBBA3 File Offset: 0x000C9DA3
		// (set) Token: 0x06004337 RID: 17207 RVA: 0x000CBBAB File Offset: 0x000C9DAB
		public string ViewModel { get; set; }
	}
}
