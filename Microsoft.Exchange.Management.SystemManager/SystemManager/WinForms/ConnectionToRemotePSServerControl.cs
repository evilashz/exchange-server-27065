using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.WinForms;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001B7 RID: 439
	public class ConnectionToRemotePSServerControl : GeneralPropertyPageControl
	{
		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060011CF RID: 4559 RVA: 0x000476D0 File Offset: 0x000458D0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new ChangeNotifyingCollection<GeneralPageSummaryInfo> GeneralPageSummaryInfoCollection
		{
			get
			{
				return base.GeneralPageSummaryInfoCollection;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x060011D0 RID: 4560 RVA: 0x000476D8 File Offset: 0x000458D8
		// (set) Token: 0x060011D1 RID: 4561 RVA: 0x000476E0 File Offset: 0x000458E0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Icon ObjectIcon
		{
			get
			{
				return base.ObjectIcon;
			}
			set
			{
				base.ObjectIcon = value;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x000476E9 File Offset: 0x000458E9
		// (set) Token: 0x060011D3 RID: 4563 RVA: 0x000476F1 File Offset: 0x000458F1
		[DefaultValue(false)]
		public new bool CanChangeHeaderText
		{
			get
			{
				return base.CanChangeHeaderText;
			}
			set
			{
				base.CanChangeHeaderText = value;
			}
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00047718 File Offset: 0x00045918
		public ConnectionToRemotePSServerControl(Icon icon)
		{
			this.InitializeComponent();
			base.HelpTopic = HelpId.ConnectionToRemotePSServerControlProperty.ToString();
			this.titleLabel.Text = Strings.ConnectToPSServerDescription;
			this.automaticallyRadioButton.Text = Strings.AutomaticallySelectedText;
			this.manualRadioButton.Text = Strings.ManuallySelectedText;
			this.userInfo.Text = Strings.AccountNameText;
			this.ObjectIcon = icon;
			this.modifiedInfo = null;
			base.Header.DataBindings.Add("Text", base.BindingSource, "DisplayName");
			base.Header.CanChangeHeaderText = false;
			base.BindingSource.DataSource = typeof(PSRemoteServer);
			this.automaticallyRadioButton.DataBindings.Add("Checked", base.BindingSource, "AutomaticallySelect", true, DataSourceUpdateMode.OnPropertyChanged);
			this.manualRadioButton.DataBindings.Add("Checked", base.BindingSource, "AutomaticallySelect", true, DataSourceUpdateMode.Never).Format += delegate(object sender, ConvertEventArgs e)
			{
				e.Value = !(bool)e.Value;
			};
			AutomatedObjectPicker automatedObjectPicker = new AutomatedObjectPicker("ExchangeServerConfigurable");
			automatedObjectPicker.InputValue("MinVersion", 14);
			automatedObjectPicker.InputValue("DesiredServerRoleBitMask", ServerRole.Mailbox | ServerRole.ClientAccess | ServerRole.UnifiedMessaging | ServerRole.HubTransport);
			automatedObjectPicker.InputValue("ExactVersion", this.GetExchangeVersion());
			this.pickerLauncherTextBox.Picker = automatedObjectPicker;
			this.pickerLauncherTextBox.ValueMember = "Fqdn";
			this.pickerLauncherTextBox.DataBindings.Add("SelectedValue", base.BindingSource, "RemotePSServer", true, DataSourceUpdateMode.OnPropertyChanged);
			this.pickerLauncherTextBox.DataBindings.Add("Enabled", this.manualRadioButton, "Checked", true, DataSourceUpdateMode.Never);
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x000478EF File Offset: 0x00045AEF
		public ConnectionToRemotePSServerControl() : this(Icons.Exchange)
		{
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x000478FC File Offset: 0x00045AFC
		private void InitializeComponent()
		{
			this.tableLayoutPanel = new AutoTableLayoutPanel();
			this.titleLabel = new Label();
			this.automaticallyRadioButton = new AutoHeightRadioButton();
			this.userInfo = new GeneralPageSummaryInfo();
			this.manualRadioButton = new AutoHeightRadioButton();
			this.pickerLauncherTextBox = new PickerLauncherTextBox();
			((ISupportInitialize)base.BindingSource).BeginInit();
			this.tableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			this.tableLayoutPanel.AutoLayout = true;
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 16f));
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel.Controls.Add(this.titleLabel, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.automaticallyRadioButton, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.manualRadioButton, 0, 2);
			this.tableLayoutPanel.Controls.Add(this.pickerLauncherTextBox, 1, 3);
			this.tableLayoutPanel.Dock = DockStyle.Top;
			this.tableLayoutPanel.Location = new Point(0, 90);
			this.tableLayoutPanel.Margin = new Padding(0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.Padding = new Padding(13, 0, 16, 12);
			this.tableLayoutPanel.RowCount = 4;
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.Size = new Size(557, 161);
			this.tableLayoutPanel.TabIndex = 6;
			this.titleLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.titleLabel.AutoSize = true;
			this.tableLayoutPanel.SetColumnSpan(this.titleLabel, 2);
			this.titleLabel.Location = new Point(13, 12);
			this.titleLabel.Margin = new Padding(0);
			this.titleLabel.Name = "titleLabel";
			this.titleLabel.Size = new Size(528, 17);
			this.titleLabel.TabIndex = 0;
			this.titleLabel.Text = "Select a server to connect to for remote PowerShell:";
			this.automaticallyRadioButton.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.tableLayoutPanel.SetColumnSpan(this.automaticallyRadioButton, 2);
			this.automaticallyRadioButton.Checked = true;
			this.automaticallyRadioButton.Location = new Point(16, 32);
			this.automaticallyRadioButton.Margin = new Padding(3, 3, 0, 0);
			this.automaticallyRadioButton.Name = "automaticallyRadioButton";
			this.automaticallyRadioButton.Size = new Size(525, 21);
			this.automaticallyRadioButton.TabIndex = 1;
			this.automaticallyRadioButton.TabStop = true;
			this.automaticallyRadioButton.Text = "Connect to a server automatically selected";
			this.automaticallyRadioButton.UseVisualStyleBackColor = true;
			this.manualRadioButton.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.tableLayoutPanel.SetColumnSpan(this.manualRadioButton, 2);
			this.manualRadioButton.Location = new Point(16, 95);
			this.manualRadioButton.Margin = new Padding(3, 12, 0, 0);
			this.manualRadioButton.Name = "manualRadioButton";
			this.manualRadioButton.Size = new Size(525, 21);
			this.manualRadioButton.TabIndex = 3;
			this.manualRadioButton.TabStop = true;
			this.manualRadioButton.Text = "Specify a server to connect to";
			this.manualRadioButton.UseVisualStyleBackColor = true;
			this.pickerLauncherTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.pickerLauncherTextBox.AutoSize = true;
			this.pickerLauncherTextBox.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.pickerLauncherTextBox.Location = new Point(29, 122);
			this.pickerLauncherTextBox.Margin = new Padding(0, 6, 0, 0);
			this.pickerLauncherTextBox.Name = "pickerLauncherTextBox";
			this.pickerLauncherTextBox.Size = new Size(512, 27);
			this.pickerLauncherTextBox.TabIndex = 4;
			this.userInfo.BindingSource = base.BindingSource;
			this.userInfo.PropertyName = "UserAccount";
			this.userInfo.Text = "userInfo";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.tableLayoutPanel);
			this.GeneralPageSummaryInfoCollection.Add(this.userInfo);
			base.Name = "ConnectionToRemotePSServerControl";
			base.Size = new Size(418, 396);
			base.Controls.SetChildIndex(this.tableLayoutPanel, 0);
			((ISupportInitialize)base.BindingSource).EndInit();
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x00047E44 File Offset: 0x00046044
		private string GetExchangeVersion()
		{
			string keyName = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup";
			int num = (int)(Registry.GetValue(keyName, "MsiProductMajor", null) ?? 0);
			int num2 = (int)(Registry.GetValue(keyName, "MsiProductMinor", null) ?? 0);
			int num3 = (int)(Registry.GetValue(keyName, "MsiBuildMajor", null) ?? 0);
			int num4 = (int)(Registry.GetValue(keyName, "MsiBuildMinor", null) ?? 0);
			return string.Format("{0}.{1}.{2}.{3}", new object[]
			{
				num,
				num2,
				num3,
				num4
			});
		}

		// Token: 0x040006D3 RID: 1747
		private AutoTableLayoutPanel tableLayoutPanel;

		// Token: 0x040006D4 RID: 1748
		private Label titleLabel;

		// Token: 0x040006D5 RID: 1749
		private AutoHeightRadioButton automaticallyRadioButton;

		// Token: 0x040006D6 RID: 1750
		private AutoHeightRadioButton manualRadioButton;

		// Token: 0x040006D7 RID: 1751
		private PickerLauncherTextBox pickerLauncherTextBox;

		// Token: 0x040006D8 RID: 1752
		private GeneralPageSummaryInfo userInfo;
	}
}
