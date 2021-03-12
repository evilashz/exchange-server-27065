using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002B8 RID: 696
	public class ImportContactListForm : PostBackForm
	{
		// Token: 0x06002C04 RID: 11268 RVA: 0x000888F8 File Offset: 0x00086AF8
		public ImportContactListForm()
		{
			base.NextButtonProgressDescription = OwaOptionStrings.ImportContactListProgress;
			base.CommitButton.Attributes["onclick"] = "\r\n            try\r\n            {\r\n                if (window.opener != null &&\r\n                    window.opener.RefreshContactList != null)\r\n                {\r\n                    window.opener.RefreshContactList();\r\n                }\r\n            }\r\n            catch(e) {} // Catch all\r\n              \r\n            window.close();";
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x0008892C File Offset: 0x00086B2C
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			Control control = base.ContentPanel.FindControl("ImportContactListProperties").FindControl("contentContainer");
			control.PreRender += this.Ctrl_PreRender;
			if (base.IsPostBack)
			{
				if (base.SenderButtonId == base.NextButton.ClientID)
				{
					this.ImportContactsRequest();
					return;
				}
			}
			else
			{
				this.currentPage = ImportContactListForm.ImportContactListPageState.UploadCsvFilePage;
			}
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x0008899C File Offset: 0x00086B9C
		private void ImportContactsRequest()
		{
			if (base.Request.Files.Count == 0 || string.IsNullOrEmpty(base.Request.Files[0].FileName))
			{
				this.currentPage = ImportContactListForm.ImportContactListPageState.UploadCsvFilePage;
				ErrorHandlingUtil.ShowServerError(OwaOptionStrings.ImportContactListNoFileUploaded, string.Empty, this.Page);
				return;
			}
			HttpPostedFile httpPostedFile = base.Request.Files[0];
			this.filename = string.Empty;
			try
			{
				this.filename = Path.GetFileName(httpPostedFile.FileName);
			}
			catch (ArgumentException)
			{
				this.filename = null;
			}
			if (string.IsNullOrEmpty(this.filename))
			{
				this.currentPage = ImportContactListForm.ImportContactListPageState.UploadCsvFilePage;
				ErrorHandlingUtil.ShowServerError(OwaOptionClientStrings.FileUploadFailed, string.Empty, this.Page);
				return;
			}
			ImportContactListParameters importContactListParameters = new ImportContactListParameters();
			importContactListParameters.CSVStream = httpPostedFile.InputStream;
			ImportContactList importContactList = new ImportContactList();
			PowerShellResults<ImportContactsResult> powerShellResults = importContactList.ImportObject(Identity.FromExecutingUserId(), importContactListParameters);
			if (!powerShellResults.Failed)
			{
				this.importResult = powerShellResults.Output[0];
				this.currentPage = ImportContactListForm.ImportContactListPageState.ImportContactListResultPage;
				return;
			}
			this.currentPage = ImportContactListForm.ImportContactListPageState.UploadCsvFilePage;
			if (powerShellResults.ErrorRecords[0].Exception is ImportContactsException)
			{
				ErrorHandlingUtil.ShowServerError(powerShellResults.ErrorRecords[0].Message, string.Empty, this.Page);
				return;
			}
			ErrorHandlingUtil.ShowServerErrors(powerShellResults.ErrorRecords, this.Page);
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x00088AFC File Offset: 0x00086CFC
		private void Ctrl_PreRender(object sender, EventArgs e)
		{
			Control control = (Control)sender;
			this.wizardPage1 = (Panel)control.FindControl("ImportContactListPage1");
			this.wizardPage2 = (Panel)control.FindControl("ImportContactListPage2");
			if (this.currentPage == ImportContactListForm.ImportContactListPageState.UploadCsvFilePage)
			{
				this.wizardPage1.Visible = true;
				this.wizardPage1.Enabled = !base.IsInError;
				this.wizardPage2.Visible = false;
				base.FooterPanel.State = ButtonsPanelState.SaveCancel;
				base.SetButtonVisibility(false, true, false);
				base.SetControlVisibility(base.CancelButton, true);
				return;
			}
			this.wizardPage1.Visible = false;
			this.wizardPage2.Visible = true;
			this.wizardPage2.Enabled = !base.IsInError;
			this.InitializePage2();
			base.FooterPanel.State = ButtonsPanelState.Save;
			base.SetButtonVisibility(false, false, true);
			base.SetControlVisibility(base.CancelButton, false);
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x00088BE8 File Offset: 0x00086DE8
		private void InitializePage2()
		{
			if (this.importResult != null)
			{
				Label label = (Label)this.wizardPage2.FindControl("lblImportResult");
				label.Text = OwaOptionStrings.ImportContactListPage2Result(this.filename);
				Label label2 = (Label)this.wizardPage2.FindControl("lblImportResultNumber");
				label2.Text = OwaOptionStrings.ImportContactListPage2ResultNumber(this.importResult.ContactsImported);
			}
		}

		// Token: 0x040021CA RID: 8650
		private const string CloseWindowScript = "\r\n            try\r\n            {\r\n                if (window.opener != null &&\r\n                    window.opener.RefreshContactList != null)\r\n                {\r\n                    window.opener.RefreshContactList();\r\n                }\r\n            }\r\n            catch(e) {} // Catch all\r\n              \r\n            window.close();";

		// Token: 0x040021CB RID: 8651
		private ImportContactsResult importResult;

		// Token: 0x040021CC RID: 8652
		private string filename;

		// Token: 0x040021CD RID: 8653
		private Panel wizardPage1;

		// Token: 0x040021CE RID: 8654
		private Panel wizardPage2;

		// Token: 0x040021CF RID: 8655
		private ImportContactListForm.ImportContactListPageState currentPage;

		// Token: 0x020002B9 RID: 697
		private enum ImportContactListPageState
		{
			// Token: 0x040021D1 RID: 8657
			UploadCsvFilePage,
			// Token: 0x040021D2 RID: 8658
			ImportContactListResultPage
		}
	}
}
