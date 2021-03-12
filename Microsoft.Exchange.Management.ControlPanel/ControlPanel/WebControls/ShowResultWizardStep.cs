using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000659 RID: 1625
	public class ShowResultWizardStep : WizardStep
	{
		// Token: 0x060046B7 RID: 18103 RVA: 0x000D6048 File Offset: 0x000D4248
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.messageDiv = new Panel();
			this.messageDiv.ID = "msgDiv";
			this.messageDiv.CssClass = "PropertyDiv WizardTxtDiv";
			this.Controls.Add(this.messageDiv);
		}

		// Token: 0x1700273B RID: 10043
		// (get) Token: 0x060046B8 RID: 18104 RVA: 0x000D6097 File Offset: 0x000D4297
		// (set) Token: 0x060046B9 RID: 18105 RVA: 0x000D609F File Offset: 0x000D429F
		public string SucceededText { get; set; }

		// Token: 0x1700273C RID: 10044
		// (get) Token: 0x060046BA RID: 18106 RVA: 0x000D60A8 File Offset: 0x000D42A8
		// (set) Token: 0x060046BB RID: 18107 RVA: 0x000D60B0 File Offset: 0x000D42B0
		[IDReferenceProperty(typeof(WebServiceWizardStep))]
		public string WebServiceStepID { get; set; }

		// Token: 0x060046BC RID: 18108 RVA: 0x000D60BC File Offset: 0x000D42BC
		protected override ScriptControlDescriptor GetScriptDescriptor()
		{
			ScriptControlDescriptor scriptDescriptor = base.GetScriptDescriptor();
			scriptDescriptor.Type = "ShowResultWizardStep";
			Control control = this.NamingContainer.FindControl(this.WebServiceStepID);
			scriptDescriptor.AddComponentProperty("WebServiceStep", control.ClientID);
			scriptDescriptor.AddElementProperty("MessageDiv", this.messageDiv.ClientID);
			scriptDescriptor.AddProperty("SucceededText", this.SucceededText);
			return scriptDescriptor;
		}

		// Token: 0x04002FC6 RID: 12230
		private Panel messageDiv;
	}
}
