using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Setup.Common;
using Microsoft.Exchange.Setup.CommonBase;
using Microsoft.Exchange.Setup.ExSetupUI;

namespace Microsoft.Exchange.Setup.GUI
{
	// Token: 0x02000009 RID: 9
	internal class InstallationSpaceAndLocationPage : SetupWizardPage
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00005804 File Offset: 0x00003A04
		public InstallationSpaceAndLocationPage(RootDataHandler rootDataHandler)
		{
			this.modeDataHandler = rootDataHandler.ModeDatahandler;
			this.InitializeComponent();
			base.PageTitle = Strings.InstallationSpaceAndLocationPageTitle;
			this.diskSpaceRequiredLabel.Text = Strings.RequiredDiskSpaceDescriptionText;
			this.diskSpaceAvailableLabel.Text = Strings.AvailableDiskSpaceDescriptionText;
			this.requiredDiskSpaceCapacityUnitLabel.Text = Strings.DiskSpaceCapacityUnit;
			this.availableDiskSpaceCapacityUnitLabel.Text = Strings.DiskSpaceCapacityUnit;
			this.installationFolderPickerLabel.Text = Strings.InstallationPathTitle;
			this.btnBrowse.Text = Strings.BrowseInstallationPathButtonText;
			this.btnBrowse.Click += this.BtnBrowse_Click;
			base.WizardCancel += this.InstallationSpaceAndLocationPage_WizardCancel;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000058E0 File Offset: 0x00003AE0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00005900 File Offset: 0x00003B00
		private void BtnBrowse_Click(object sender, EventArgs e)
		{
			this.installationFolderDialog = new FolderBrowserDialog();
			this.installationFolderDialog.Description = Strings.FolderBrowserDialogDescriptionText;
			if (this.installationFolderDialog.ShowDialog() == DialogResult.OK)
			{
				NonRootLocalLongFullPath installationPath;
				try
				{
					installationPath = NonRootLocalLongFullPath.Parse(this.installationFolderDialog.SelectedPath);
				}
				catch (ArgumentException)
				{
					MessageBoxHelper.ShowError(Strings.InvalidInstallationLocation);
					return;
				}
				this.modeDataHandler.InstallationPath = installationPath;
				this.availableDiskSpaceCapacityLabel.Text = this.modeDataHandler.AvailableDiskSpace.ToString("0.#");
				this.browseTextBox.Text = this.installationFolderDialog.SelectedPath;
				this.ValidatePath();
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000059BC File Offset: 0x00003BBC
		private void BrowseTextBox_TextChanged(object sender, EventArgs e)
		{
			if (this.browseTextBox.Text == string.Empty)
			{
				base.SetWizardButtons(WizardButtons.Previous);
				return;
			}
			base.SetWizardButtons(WizardButtons.Previous | WizardButtons.Next);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000059E4 File Offset: 0x00003BE4
		private void InstallationSpaceAndLocationPage_CheckLoaded(object sender, CancelEventArgs e)
		{
			Control[] array = base.Controls.Find(this.installationFolderPickerLabel.Name, true);
			if (array.Length > 0)
			{
				this.OnSetLoaded(new CancelEventArgs());
				SetupLogger.Log(Strings.PageLoaded(base.Name));
				this.ValidatePath();
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00005A30 File Offset: 0x00003C30
		private void InstallationSpaceAndLocationPage_SetActive(object sender, CancelEventArgs e)
		{
			base.SetPageTitle(base.PageTitle);
			base.SetWizardButtons(WizardButtons.Previous | WizardButtons.Next);
			base.SetVisibleWizardButtons(WizardButtons.Previous | WizardButtons.Next);
			this.requiredDiskSpaceCapacityLabel.Text = this.modeDataHandler.RequiredDiskSpace.ToString("0.#");
			this.availableDiskSpaceCapacityLabel.Text = this.modeDataHandler.AvailableDiskSpace.ToString("0.#");
			string pathName = this.modeDataHandler.InstallationPath.PathName;
			if (!string.IsNullOrEmpty(pathName))
			{
				this.browseTextBox.Text = pathName;
			}
			else
			{
				this.browseTextBox.Text = string.Empty;
			}
			base.EnableCheckLoadedTimer(200);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00005ADF File Offset: 0x00003CDF
		private void InstallationSpaceAndLocationPage_WizardCancel(object sender, CancelEventArgs e)
		{
			ExSetupUI.ExitApplication(ExitCode.Success);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00005AE7 File Offset: 0x00003CE7
		private void ResizeRequiredDiskSpaceCapacityLabel(object sender, EventArgs e)
		{
			this.requiredDiskSpaceCapacityUnitLabel.Left = this.requiredDiskSpaceCapacityLabel.Right;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00005AFF File Offset: 0x00003CFF
		private void ResizeAvailableDiskSpaceCapacityLabel(object sender, EventArgs e)
		{
			this.availableDiskSpaceCapacityUnitLabel.Left = this.availableDiskSpaceCapacityLabel.Right;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00005B18 File Offset: 0x00003D18
		private void ValidatePath()
		{
			ValidationError[] array = this.modeDataHandler.Validate();
			string text = null;
			if (array != null && array.Length > 0)
			{
				text = array[0].Description;
			}
			else if (this.modeDataHandler.RequiredDiskSpace > this.modeDataHandler.AvailableDiskSpace)
			{
				text = Strings.NotEnoughSpace(Math.Round(this.modeDataHandler.RequiredDiskSpace / 1024m, 2, MidpointRounding.AwayFromZero).ToString());
			}
			if (text != null)
			{
				MessageBoxHelper.ShowError(text);
				base.SetWizardButtons(WizardButtons.Previous);
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00005BB0 File Offset: 0x00003DB0
		private void InitializeComponent()
		{
			this.diskSpaceAvailableLabel = new Label();
			this.requiredDiskSpaceCapacityUnitLabel = new Label();
			this.diskSpaceRequiredLabel = new Label();
			this.requiredDiskSpaceCapacityLabel = new Label();
			this.availableDiskSpaceCapacityUnitLabel = new Label();
			this.availableDiskSpaceCapacityLabel = new Label();
			this.installationFolderPickerLabel = new Label();
			this.browseTextBox = new TextBox();
			this.btnBrowse = new Button();
			base.SuspendLayout();
			this.diskSpaceAvailableLabel.AutoSize = true;
			this.diskSpaceAvailableLabel.BackColor = Color.Transparent;
			this.diskSpaceAvailableLabel.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
			this.diskSpaceAvailableLabel.Location = new Point(0, 30);
			this.diskSpaceAvailableLabel.Name = "diskSpaceAvailableLabel";
			this.diskSpaceAvailableLabel.Size = new Size(150, 17);
			this.diskSpaceAvailableLabel.TabIndex = 1;
			this.diskSpaceAvailableLabel.Text = "[diskSpaceAvailableText]";
			this.requiredDiskSpaceCapacityUnitLabel.AutoSize = true;
			this.requiredDiskSpaceCapacityUnitLabel.BackColor = Color.Transparent;
			this.requiredDiskSpaceCapacityUnitLabel.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel);
			this.requiredDiskSpaceCapacityUnitLabel.Location = new Point(376, 0);
			this.requiredDiskSpaceCapacityUnitLabel.Name = "requiredDiskSpaceCapacityUnitLabel";
			this.requiredDiskSpaceCapacityUnitLabel.Size = new Size(228, 17);
			this.requiredDiskSpaceCapacityUnitLabel.TabIndex = 1;
			this.requiredDiskSpaceCapacityUnitLabel.Text = "[requiredDiskSpaceCapacityUnitLabel]";
			this.diskSpaceRequiredLabel.AutoSize = true;
			this.diskSpaceRequiredLabel.BackColor = Color.Transparent;
			this.diskSpaceRequiredLabel.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
			this.diskSpaceRequiredLabel.Location = new Point(0, 0);
			this.diskSpaceRequiredLabel.Name = "diskSpaceRequiredLabel";
			this.diskSpaceRequiredLabel.Size = new Size(151, 17);
			this.diskSpaceRequiredLabel.TabIndex = 0;
			this.diskSpaceRequiredLabel.Text = "[diskSpaceRequiredText]";
			this.requiredDiskSpaceCapacityLabel.AutoSize = true;
			this.requiredDiskSpaceCapacityLabel.BackColor = Color.Transparent;
			this.requiredDiskSpaceCapacityLabel.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
			this.requiredDiskSpaceCapacityLabel.Location = new Point(185, 0);
			this.requiredDiskSpaceCapacityLabel.Name = "requiredDiskSpaceCapacityLabel";
			this.requiredDiskSpaceCapacityLabel.Size = new Size(205, 17);
			this.requiredDiskSpaceCapacityLabel.TabIndex = 2;
			this.requiredDiskSpaceCapacityLabel.Text = "[requiredDiskSpaceCapacityLabel]";
			this.requiredDiskSpaceCapacityLabel.Resize += this.ResizeRequiredDiskSpaceCapacityLabel;
			this.availableDiskSpaceCapacityUnitLabel.AutoSize = true;
			this.availableDiskSpaceCapacityUnitLabel.BackColor = Color.Transparent;
			this.availableDiskSpaceCapacityUnitLabel.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel);
			this.availableDiskSpaceCapacityUnitLabel.Location = new Point(376, 30);
			this.availableDiskSpaceCapacityUnitLabel.Name = "availableDiskSpaceCapacityUnitLabel";
			this.availableDiskSpaceCapacityUnitLabel.Size = new Size(229, 17);
			this.availableDiskSpaceCapacityUnitLabel.TabIndex = 2;
			this.availableDiskSpaceCapacityUnitLabel.Text = "[availableDiskSpaceCapacityUnitLabel]";
			this.availableDiskSpaceCapacityLabel.AutoSize = true;
			this.availableDiskSpaceCapacityLabel.BackColor = Color.Transparent;
			this.availableDiskSpaceCapacityLabel.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
			this.availableDiskSpaceCapacityLabel.Location = new Point(185, 30);
			this.availableDiskSpaceCapacityLabel.Name = "availableDiskSpaceCapacityLabel";
			this.availableDiskSpaceCapacityLabel.Size = new Size(175, 17);
			this.availableDiskSpaceCapacityLabel.TabIndex = 3;
			this.availableDiskSpaceCapacityLabel.Text = "[availableDiskSpaceCapacity]";
			this.availableDiskSpaceCapacityLabel.Resize += this.ResizeAvailableDiskSpaceCapacityLabel;
			this.installationFolderPickerLabel.AutoSize = true;
			this.installationFolderPickerLabel.Location = new Point(0, 70);
			this.installationFolderPickerLabel.Name = "installationFolderPickerLabel";
			this.installationFolderPickerLabel.Size = new Size(204, 17);
			this.installationFolderPickerLabel.TabIndex = 28;
			this.installationFolderPickerLabel.Text = "[installationFolderPickerLabelText]";
			this.browseTextBox.BackColor = Color.White;
			this.browseTextBox.ForeColor = Color.FromArgb(51, 51, 51);
			this.browseTextBox.Location = new Point(0, 100);
			this.browseTextBox.Multiline = true;
			this.browseTextBox.Name = "browseTextBox";
			this.browseTextBox.ReadOnly = true;
			this.browseTextBox.Size = new Size(300, 23);
			this.browseTextBox.TabIndex = 27;
			this.browseTextBox.TextChanged += this.BrowseTextBox_TextChanged;
			this.btnBrowse.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 114, 198);
			this.btnBrowse.FlatAppearance.MouseOverBackColor = Color.Transparent;
			this.btnBrowse.FlatStyle = FlatStyle.Flat;
			this.btnBrowse.ForeColor = Color.FromArgb(102, 102, 102);
			this.btnBrowse.Location = new Point(306, 97);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new Size(90, 30);
			this.btnBrowse.TabIndex = 26;
			this.btnBrowse.Text = "[Browse]";
			this.btnBrowse.UseVisualStyleBackColor = true;
			base.Controls.Add(this.btnBrowse);
			base.Controls.Add(this.installationFolderPickerLabel);
			base.Controls.Add(this.browseTextBox);
			base.Controls.Add(this.availableDiskSpaceCapacityUnitLabel);
			base.Controls.Add(this.requiredDiskSpaceCapacityUnitLabel);
			base.Controls.Add(this.requiredDiskSpaceCapacityLabel);
			base.Controls.Add(this.availableDiskSpaceCapacityLabel);
			base.Controls.Add(this.diskSpaceAvailableLabel);
			base.Controls.Add(this.diskSpaceRequiredLabel);
			base.Name = "InstallationSpaceAndLocationPage";
			base.SetActive += this.InstallationSpaceAndLocationPage_SetActive;
			base.CheckLoaded += this.InstallationSpaceAndLocationPage_CheckLoaded;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000037 RID: 55
		private Label diskSpaceAvailableLabel;

		// Token: 0x04000038 RID: 56
		private Label requiredDiskSpaceCapacityUnitLabel;

		// Token: 0x04000039 RID: 57
		private Label availableDiskSpaceCapacityUnitLabel;

		// Token: 0x0400003A RID: 58
		private Label availableDiskSpaceCapacityLabel;

		// Token: 0x0400003B RID: 59
		private Label diskSpaceRequiredLabel;

		// Token: 0x0400003C RID: 60
		private Label requiredDiskSpaceCapacityLabel;

		// Token: 0x0400003D RID: 61
		private Label installationFolderPickerLabel;

		// Token: 0x0400003E RID: 62
		private TextBox browseTextBox;

		// Token: 0x0400003F RID: 63
		private Button btnBrowse;

		// Token: 0x04000040 RID: 64
		private FolderBrowserDialog installationFolderDialog;

		// Token: 0x04000041 RID: 65
		private ModeDataHandler modeDataHandler;

		// Token: 0x04000042 RID: 66
		private IContainer components;
	}
}
