using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001C5 RID: 453
	[ClientScriptResource("PostBackForm", "Microsoft.Exchange.Management.ControlPanel.Client.WizardProperties.js")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public class PostBackForm : BaseForm
	{
		// Token: 0x060024D2 RID: 9426 RVA: 0x00070F0C File Offset: 0x0006F10C
		public PostBackForm()
		{
			base.FooterPanel.State = ButtonsPanelState.Wizard;
			base.FooterPanel.CloseWindowOnCancel = true;
			this.currentObjectIdField = new HiddenField();
			this.currentObjectIdField.ID = "currentRequestIdField";
			base.ContentPanel.Controls.Add(this.currentObjectIdField);
			this.senderBtn = new HiddenField();
			this.senderBtn.ID = "senderBtn";
			base.ContentPanel.Controls.Add(this.senderBtn);
			Util.RequireUpdateProgressPopUp(this);
		}

		// Token: 0x17001B50 RID: 6992
		// (get) Token: 0x060024D3 RID: 9427 RVA: 0x00070F9F File Offset: 0x0006F19F
		// (set) Token: 0x060024D4 RID: 9428 RVA: 0x00070FAC File Offset: 0x0006F1AC
		protected string CurrentObjectId
		{
			get
			{
				return this.currentObjectIdField.Value;
			}
			set
			{
				this.currentObjectIdField.Value = value;
			}
		}

		// Token: 0x17001B51 RID: 6993
		// (get) Token: 0x060024D5 RID: 9429 RVA: 0x00070FBA File Offset: 0x0006F1BA
		protected string SenderButtonId
		{
			get
			{
				return this.senderBtn.Value;
			}
		}

		// Token: 0x17001B52 RID: 6994
		// (get) Token: 0x060024D6 RID: 9430 RVA: 0x00070FC7 File Offset: 0x0006F1C7
		// (set) Token: 0x060024D7 RID: 9431 RVA: 0x00070FCF File Offset: 0x0006F1CF
		protected string CommitButtonProgressDescription { get; set; }

		// Token: 0x17001B53 RID: 6995
		// (get) Token: 0x060024D8 RID: 9432 RVA: 0x00070FD8 File Offset: 0x0006F1D8
		// (set) Token: 0x060024D9 RID: 9433 RVA: 0x00070FE0 File Offset: 0x0006F1E0
		protected string BackButtonProgressDescription { get; set; }

		// Token: 0x17001B54 RID: 6996
		// (get) Token: 0x060024DA RID: 9434 RVA: 0x00070FE9 File Offset: 0x0006F1E9
		// (set) Token: 0x060024DB RID: 9435 RVA: 0x00070FF1 File Offset: 0x0006F1F1
		protected string NextButtonProgressDescription { get; set; }

		// Token: 0x17001B55 RID: 6997
		// (get) Token: 0x060024DC RID: 9436 RVA: 0x00070FFA File Offset: 0x0006F1FA
		// (set) Token: 0x060024DD RID: 9437 RVA: 0x00071002 File Offset: 0x0006F202
		protected bool IsInError { get; set; }

		// Token: 0x060024DE RID: 9438 RVA: 0x0007100C File Offset: 0x0006F20C
		protected override void OnLoad(EventArgs e)
		{
			this.EnableViewState = true;
			base.OnLoad(e);
			if (base.IsPostBack)
			{
				if (string.Compare(this.SenderButtonId, base.CommitButtonClientID) != 0 && string.Compare(this.SenderButtonId, base.BackButton.ClientID) != 0 && string.Compare(this.SenderButtonId, base.NextButton.ClientID) != 0)
				{
					this.ShowDisabledWizard(Strings.WebServiceErrorMessage);
					return;
				}
			}
			else
			{
				Exception lastError = base.Server.GetLastError();
				if (lastError != null)
				{
					InfoCore infoCore = lastError.ToErrorInformationBase().ToInfo();
					if (lastError.IsMaxRequestLengthExceededException())
					{
						infoCore.Message = Strings.MigrationFileTooBig;
						infoCore.Details = string.Empty;
						base.Server.ClearError();
					}
					ErrorHandlingUtil.ShowServerError(infoCore, this.Page);
				}
			}
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x000710DB File Offset: 0x0006F2DB
		protected void SetButtonVisibility(bool backButtonVisible, bool nextButtonVisible, bool commitButtonVisible)
		{
			this.SetControlVisibility(base.BackButton, backButtonVisible);
			this.SetControlVisibility(base.NextButton, nextButtonVisible);
			this.SetControlVisibility(base.CommitButton, commitButtonVisible);
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x00071104 File Offset: 0x0006F304
		protected void ShowDisabledWizard(string reason)
		{
			this.IsInError = true;
			base.NextButton.Disabled = true;
			base.BackButton.Disabled = true;
			base.CommitButton.Disabled = true;
			ErrorHandlingUtil.ShowServerError(reason, string.Empty, this.Page);
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x00071144 File Offset: 0x0006F344
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddProperty("CommitButtonProgressDescription", this.CommitButtonProgressDescription, true);
			descriptor.AddProperty("NextButtonProgressDescription", this.NextButtonProgressDescription, true);
			descriptor.AddProperty("BackButtonProgressDescription", this.BackButtonProgressDescription, true);
			descriptor.AddElementProperty("NextButton", base.NextButton.ClientID);
			descriptor.AddElementProperty("BackButton", base.BackButton.ClientID);
			descriptor.AddElementProperty("SenderButtonField", this.senderBtn.ClientID);
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x000711D0 File Offset: 0x0006F3D0
		protected void SetControlVisibility(HtmlControl ctrl, bool visible)
		{
			ctrl.Visible = visible;
			if (visible)
			{
				ctrl.Style.Remove(HtmlTextWriterStyle.Display);
				return;
			}
			ctrl.Style[HtmlTextWriterStyle.Display] = "none";
		}

		// Token: 0x04001EA0 RID: 7840
		private HiddenField currentObjectIdField;

		// Token: 0x04001EA1 RID: 7841
		private HiddenField senderBtn;
	}
}
