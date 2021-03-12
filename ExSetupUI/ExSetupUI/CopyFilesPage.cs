using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Setup.Common;
using Microsoft.Exchange.Setup.CommonBase;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x02000005 RID: 5
	internal class CopyFilesPage : ProgressPageBase
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00003618 File Offset: 0x00001818
		public CopyFilesPage(SetupBase theApp)
		{
			this.sourceDir = Path.Combine(theApp.ParsedArguments["sourcedir"].ToString(), "Setup\\ServerRoles\\Common");
			this.fileCopier = new FileCopier(this.sourceDir, Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
			this.fileCopier.FileCopierProgressEvent += this.FileCopier_ProgressChangedEvent;
			this.fileCopier.FileCopierCompletedEvent += this.FileCopier_CompletedEvent;
			this.fileCopier.FileCopierBeforeFileCopyEvent += this.FileCopier_FileCopierBeforeFileCopyEvent;
			this.fileCopier.FileCopierCancelEvent += this.FileCopier_CancelEvent;
			this.fileCopier.FileCopierErrorEvent += new FileCopierErrorHandler(this.FileCopierUpdatesStatus);
			this.InitializeComponent();
			base.WizardCancel += this.CopyFilePage_WizardCancel;
			base.PageTitle = Strings.CopyFilesPageTitle;
			this.copyFilesLabel.Text = Strings.CopyFilesText;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003720 File Offset: 0x00001920
		public void StartFileCopying()
		{
			this.fileCopier.StartFileCopying();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x0000372D File Offset: 0x0000192D
		internal void KillFileCopying()
		{
			if (this.copyThread != null)
			{
				this.copyThread.Abort();
				this.copyThread.Join();
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000374D File Offset: 0x0000194D
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000376C File Offset: 0x0000196C
		private void CopyFilesPage_SetActive(object sender, CancelEventArgs e)
		{
			base.SetPageTitle(base.PageTitle);
			base.SetWizardButtons(WizardButtons.None);
			base.SetVisibleWizardButtons(WizardButtons.None);
			base.EnableCancelButton(true);
			this.customProgressBarWithTitle.Title = Strings.CopyfileInstallText;
			this.customProgressBarWithTitle.Visible = true;
			if (base.FindPage("UpdatesDownloadsPage") != null)
			{
				base.SetPageVisibleControl(base.Name, "UpdatesDownloadsPage", false);
			}
			else
			{
				base.SetTopMost(true);
			}
			this.copyThread = new Thread(new ThreadStart(this.StartFileCopying));
			this.copyThread.Start();
			base.EnableCheckLoadedTimer(200);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003810 File Offset: 0x00001A10
		private void CopyFilesPage_CheckLoaded(object sender, CancelEventArgs e)
		{
			Control[] array = base.Controls.Find(this.copyFilesLabel.Name, true);
			if (array.Length > 0)
			{
				this.OnSetLoaded(new CancelEventArgs());
				SetupLogger.Log(Strings.PageLoaded(base.Name));
				base.SetTopMost(false);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000385D File Offset: 0x00001A5D
		private void CopyFilePage_WizardCancel(object sender, CancelEventArgs e)
		{
			if (this.fileCopier.PercentageCopiedFiles != 100)
			{
				this.CancelCopying();
				return;
			}
			ExSetupUI.ExitApplication(ExitCode.Success);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000387C File Offset: 0x00001A7C
		private void FileCopierUpdatesStatus(object message)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new CopyFilesPage.UpdateFileCopyingStatus(this.FileCopierUpdatesStatus), new object[]
				{
					message
				});
				return;
			}
			string text = null;
			if (message is ILocalizedString)
			{
				text = message.ToString();
				SetupLogger.Log((LocalizedString)message);
			}
			else if (message is FileCopierInsufficientDiskSpaceException)
			{
				FileCopierInsufficientDiskSpaceException e = (FileCopierInsufficientDiskSpaceException)message;
				SetupLogger.LogError(e);
				text = Strings.InsufficientDiskSpace.ToString();
			}
			else if (message is FileCopierReadFileException)
			{
				FileCopierReadFileException e2 = (FileCopierReadFileException)message;
				SetupLogger.LogError(e2);
				text = Strings.NoFilesToCopy(this.sourceDir).ToString();
			}
			else if (message is Exception)
			{
				SetupLogger.LogError((Exception)message);
				text = Strings.FileCopyingError.ToString();
			}
			MessageBoxHelper.ShowError(text);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000395F File Offset: 0x00001B5F
		private void FileCopier_CompletedEvent()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new FileCopierCompletedHandler(this.FileCopier_CompletedEvent));
				return;
			}
			base.SetWizardButtons(WizardButtons.Next);
			base.DoBtnNextClick();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000398A File Offset: 0x00001B8A
		private void FileCopier_CancelEvent()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new FileCopierCanceledHandler(this.FileCopier_CancelEvent));
				return;
			}
			ExSetupUI.ExitApplication(ExitCode.Success);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000039AE File Offset: 0x00001BAE
		private void CancelCopying()
		{
			if (this.fileCopier != null)
			{
				this.fileCopier.StopFileCopying();
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000039C4 File Offset: 0x00001BC4
		private void FileCopier_ProgressChangedEvent()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new FileCopierProgressChangeHandler(this.FileCopier_ProgressChangedEvent));
				return;
			}
			this.customProgressBarWithTitle.Visible = true;
			if (this.fileCopier.PercentageCopiedFiles == 100)
			{
				this.customProgressBarWithTitle.Title = Strings.FileCopyDone;
			}
			this.customProgressBarWithTitle.Value = this.fileCopier.PercentageCopiedFiles;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003A33 File Offset: 0x00001C33
		private void FileCopier_FileCopierBeforeFileCopyEvent()
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new FileCopierBeforeFileCopyHandler(this.FileCopier_FileCopierBeforeFileCopyEvent));
				return;
			}
			this.customProgressBarWithTitle.Visible = true;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003A60 File Offset: 0x00001C60
		private void InitializeComponent()
		{
			this.copyFilesLabel = new Label();
			base.SuspendLayout();
			this.customProgressBarWithTitle.Location = new Point(0, 40);
			this.customProgressBarWithTitle.Size = new Size(721, 76);
			this.copyFilesLabel.AutoSize = true;
			this.copyFilesLabel.BackColor = Color.Transparent;
			this.copyFilesLabel.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
			this.copyFilesLabel.Location = new Point(0, 0);
			this.copyFilesLabel.Margin = new Padding(0);
			this.copyFilesLabel.MaximumSize = new Size(720, 0);
			this.copyFilesLabel.Name = "copyFilesLabel";
			this.copyFilesLabel.Size = new Size(93, 17);
			this.copyFilesLabel.TabIndex = 19;
			this.copyFilesLabel.Text = "[copyFilesText]";
			base.Controls.Add(this.copyFilesLabel);
			base.Name = "CopyFilesPage";
			base.SetActive += this.CopyFilesPage_SetActive;
			base.CheckLoaded += this.CopyFilesPage_CheckLoaded;
			base.Controls.SetChildIndex(this.customProgressBarWithTitle, 0);
			base.Controls.SetChildIndex(this.copyFilesLabel, 0);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400001E RID: 30
		private const string UpdatesDownloadsPageName = "UpdatesDownloadsPage";

		// Token: 0x0400001F RID: 31
		private readonly string sourceDir;

		// Token: 0x04000020 RID: 32
		private Label copyFilesLabel;

		// Token: 0x04000021 RID: 33
		private IContainer components;

		// Token: 0x04000022 RID: 34
		private Thread copyThread;

		// Token: 0x04000023 RID: 35
		private FileCopier fileCopier;

		// Token: 0x02000006 RID: 6
		// (Invoke) Token: 0x0600005C RID: 92
		private delegate void UpdateFileCopyingStatus(object msg);
	}
}
