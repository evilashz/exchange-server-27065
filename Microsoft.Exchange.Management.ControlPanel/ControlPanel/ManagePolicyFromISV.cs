using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001C6 RID: 454
	public class ManagePolicyFromISV : PostBackForm
	{
		// Token: 0x060024E3 RID: 9443 RVA: 0x000711FC File Offset: 0x0006F3FC
		public ManagePolicyFromISV()
		{
			base.NextButtonText = Strings.DLPUploadFileBUttonText;
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x00071214 File Offset: 0x0006F414
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			Control control = base.ContentPanel.FindControl("isvpropertypage").FindControl("contentContainer");
			control.PreRender += this.Ctrl_PreRender;
			if (base.IsPostBack && base.SenderButtonId == base.NextButton.ClientID)
			{
				this.ExecuteUpload();
			}
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x0007127C File Offset: 0x0006F47C
		private void Ctrl_PreRender(object sender, EventArgs e)
		{
			Control control = (Control)sender;
			this.wizardPage1 = (Panel)control.FindControl("step1");
			this.wizardPage1.Visible = true;
			this.wizardPage1.Enabled = !base.IsInError;
			base.FooterPanel.State = ButtonsPanelState.SaveCancel;
			base.SetButtonVisibility(false, true, false);
			base.SetControlVisibility(base.CancelButton, true);
			this.policyState.SelectedValue = "Enabled";
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x000712F8 File Offset: 0x0006F4F8
		private void ExecuteUpload()
		{
			try
			{
				if (base.Request.Files.Count == 0 || string.IsNullOrEmpty(base.Request.Files[0].FileName))
				{
					ErrorHandlingUtil.ShowServerError(Strings.ISVNoFileUploaded, string.Empty, this.Page);
				}
				else
				{
					DLPISVService dlpisvservice = new DLPISVService();
					HttpPostedFile httpPostedFile = base.Request.Files[0];
					byte[] array = new byte[httpPostedFile.ContentLength];
					httpPostedFile.InputStream.Read(array, 0, array.Length);
					PowerShellResults powerShellResults = dlpisvservice.ProcessUpload(new DLPNewPolicyUploadParameters
					{
						Mode = this.policyMode.SelectedValue,
						State = RuleState.Enabled.ToString(),
						Name = this.name.Text,
						Description = this.description.Text,
						TemplateData = array
					});
					if (powerShellResults.Failed)
					{
						ErrorHandlingUtil.ShowServerErrors(powerShellResults.ErrorRecords, this.Page);
					}
					else
					{
						this.Page.RegisterStartupScript("windowclose", string.Format("<script>{0}</script>", "window.opener.RefreshPolicyListView();window.close();"));
					}
				}
			}
			catch (Exception ex)
			{
				ErrorHandlingUtil.ShowServerError(ex.Message, string.Empty, this.Page);
			}
		}

		// Token: 0x04001EA6 RID: 7846
		private const string CloseWindowScript = "window.opener.RefreshPolicyListView();window.close();";

		// Token: 0x04001EA7 RID: 7847
		protected PlaceHolder uploadPanel;

		// Token: 0x04001EA8 RID: 7848
		protected TextBox name;

		// Token: 0x04001EA9 RID: 7849
		protected TextArea description;

		// Token: 0x04001EAA RID: 7850
		protected RadioButtonList policyMode;

		// Token: 0x04001EAB RID: 7851
		protected RadioButtonList policyState;

		// Token: 0x04001EAC RID: 7852
		protected Panel wizardPage1;
	}
}
