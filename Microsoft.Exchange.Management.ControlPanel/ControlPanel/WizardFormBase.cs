using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000CC RID: 204
	public abstract class WizardFormBase : BaseForm
	{
		// Token: 0x06001D44 RID: 7492 RVA: 0x00059E5A File Offset: 0x0005805A
		public WizardFormBase()
		{
			base.FooterPanel.State = ButtonsPanelState.Wizard;
		}

		// Token: 0x17001949 RID: 6473
		// (get) Token: 0x06001D45 RID: 7493 RVA: 0x00059E6E File Offset: 0x0005806E
		// (set) Token: 0x06001D46 RID: 7494 RVA: 0x00059E76 File Offset: 0x00058076
		[DefaultValue(true)]
		[Bindable(true)]
		[Category("Behavior")]
		public bool CloseWindowOnCancel { get; set; }

		// Token: 0x06001D47 RID: 7495 RVA: 0x00059E80 File Offset: 0x00058080
		protected override void OnPreRender(EventArgs e)
		{
			base.AdditionalContentPanelStyle = " positionStatic " + ((!string.IsNullOrEmpty(base.AdditionalContentPanelStyle)) ? base.AdditionalContentPanelStyle : string.Empty);
			base.OnPreRender(e);
			base.FooterPanel.CloseWindowOnCancel = this.CloseWindowOnCancel;
		}

		// Token: 0x1700194A RID: 6474
		// (get) Token: 0x06001D48 RID: 7496 RVA: 0x00059ECF File Offset: 0x000580CF
		// (set) Token: 0x06001D49 RID: 7497 RVA: 0x00059ED7 File Offset: 0x000580D7
		[DefaultValue(false)]
		public bool ShowWizardStepTitle { get; set; }

		// Token: 0x06001D4A RID: 7498 RVA: 0x00059EE0 File Offset: 0x000580E0
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			if (this.ShowWizardStepTitle)
			{
				this.lblStepInfo = new Label();
				this.lblStepInfo.CssClass = "stepTxt";
				this.lblStepInfo.ID = "lblStepInfo";
				base.CaptionPanel.Controls.Add(this.lblStepInfo);
			}
		}

		// Token: 0x1700194B RID: 6475
		// (get) Token: 0x06001D4B RID: 7499 RVA: 0x00059F3C File Offset: 0x0005813C
		public string ShowWizardStepClientID
		{
			get
			{
				this.EnsureChildControls();
				return this.lblStepInfo.ClientID;
			}
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x00059F50 File Offset: 0x00058150
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			descriptor.AddElementProperty("BackButton", this.BackButtonClientID, true);
			descriptor.AddElementProperty("NextButton", this.NextButtonClientID, true);
			if (this.ShowWizardStepTitle)
			{
				descriptor.AddProperty("ShowWizardStepTitle", true);
				descriptor.AddElementProperty("ShowWizardStepLabel", this.ShowWizardStepClientID, true);
			}
			base.BuildScriptDescriptor(descriptor);
		}

		// Token: 0x1700194C RID: 6476
		// (get) Token: 0x06001D4D RID: 7501 RVA: 0x00059FB3 File Offset: 0x000581B3
		public string BackButtonClientID
		{
			get
			{
				this.EnsureChildControls();
				return base.FooterPanel.BackButtonClientID;
			}
		}

		// Token: 0x1700194D RID: 6477
		// (get) Token: 0x06001D4E RID: 7502 RVA: 0x00059FC6 File Offset: 0x000581C6
		public string NextButtonClientID
		{
			get
			{
				this.EnsureChildControls();
				return base.FooterPanel.NextButtonClientID;
			}
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x00059FD9 File Offset: 0x000581D9
		protected override void SetFormDefaultButton()
		{
		}

		// Token: 0x04001BCD RID: 7117
		private Label lblStepInfo;
	}
}
