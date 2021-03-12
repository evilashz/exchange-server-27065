using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001BE RID: 446
	public class ConnectToServerControl : ExchangePropertyPageControl
	{
		// Token: 0x0600124E RID: 4686 RVA: 0x00049DF8 File Offset: 0x00047FF8
		public ConnectToServerControl()
		{
			this.InitializeComponent();
			this.AutoSize = true;
			this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.serverPicker = this.CreateServerPicker();
			this.serverPickerLauncherTextBox.Picker = this.serverPicker;
			this.serverPickerLauncherTextBox.ValueMember = "Fqdn";
			base.BindingSource.DataSource = typeof(ConnectToServerParams);
			this.setDefaultServerCheckBox.DataBindings.Add("Checked", base.BindingSource, "SetAsDefaultServer");
			this.serverPickerLauncherTextBox.DataBindings.Add("SelectedValue", base.BindingSource, "ServerName");
			this.serverPickerLauncherTextBox.SelectedValueChanged += delegate(object param0, EventArgs param1)
			{
				this.setDefaultServerCheckBox.Enabled = !string.IsNullOrEmpty(this.serverPickerLauncherTextBox.SelectedValue.ToString());
			};
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00049EC1 File Offset: 0x000480C1
		protected virtual AutomatedObjectPicker CreateServerPicker()
		{
			return new AutomatedObjectPicker("ExchangeServerConfigurable");
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00049ED0 File Offset: 0x000480D0
		public override Size GetPreferredSize(Size proposedSize)
		{
			return Size.Add(this.tableLayoutPanel.Size, base.Padding.Size);
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x00049EFB File Offset: 0x000480FB
		// (set) Token: 0x06001252 RID: 4690 RVA: 0x00049F03 File Offset: 0x00048103
		[DefaultValue(true)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x00049F0C File Offset: 0x0004810C
		// (set) Token: 0x06001254 RID: 4692 RVA: 0x00049F14 File Offset: 0x00048114
		[DefaultValue(AutoSizeMode.GrowAndShrink)]
		public new AutoSizeMode AutoSizeMode
		{
			get
			{
				return base.AutoSizeMode;
			}
			set
			{
				base.AutoSizeMode = value;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x00049F1D File Offset: 0x0004811D
		// (set) Token: 0x06001256 RID: 4694 RVA: 0x00049F2A File Offset: 0x0004812A
		[DefaultValue("connectServerLabel")]
		public string ConnectServerLabelText
		{
			get
			{
				return this.connectServerLabel.Text;
			}
			set
			{
				this.connectServerLabel.Text = value;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x00049F38 File Offset: 0x00048138
		// (set) Token: 0x06001258 RID: 4696 RVA: 0x00049F45 File Offset: 0x00048145
		[DefaultValue("setDefaultServerCheckBox")]
		public string SetDefaultServerCheckBoxText
		{
			get
			{
				return this.setDefaultServerCheckBox.Text;
			}
			set
			{
				this.setDefaultServerCheckBox.Text = value;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x00049F53 File Offset: 0x00048153
		// (set) Token: 0x0600125A RID: 4698 RVA: 0x00049F6A File Offset: 0x0004816A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual ServerRole ServerRoleToPicker
		{
			get
			{
				return (ServerRole)this.serverPicker.GetValue("DesiredServerRoleBitMask");
			}
			set
			{
				this.serverPicker.InputValue("DesiredServerRoleBitMask", value);
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x00049F82 File Offset: 0x00048182
		protected override Size DefaultMinimumSize
		{
			get
			{
				return new Size(328, 79);
			}
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00049F90 File Offset: 0x00048190
		private void InitializeComponent()
		{
			this.connectServerLabel = new Label();
			this.serverPickerLauncherTextBox = new PickerLauncherTextBox();
			this.setDefaultServerCheckBox = new AutoHeightCheckBox();
			this.tableLayoutPanel = new AutoTableLayoutPanel();
			((ISupportInitialize)base.BindingSource).BeginInit();
			this.tableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			base.InputValidationProvider.SetEnabled(base.BindingSource, true);
			this.connectServerLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.connectServerLabel.AutoSize = true;
			this.connectServerLabel.Location = new Point(13, 12);
			this.connectServerLabel.Margin = new Padding(0);
			this.connectServerLabel.Name = "connectServerLabel";
			this.connectServerLabel.Size = new Size(289, 13);
			this.connectServerLabel.TabIndex = 0;
			this.connectServerLabel.Text = "connectServerLabel";
			this.serverPickerLauncherTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.serverPickerLauncherTextBox.AutoSize = true;
			this.serverPickerLauncherTextBox.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.serverPickerLauncherTextBox.Location = new Point(13, 28);
			this.serverPickerLauncherTextBox.Margin = new Padding(0, 3, 0, 0);
			this.serverPickerLauncherTextBox.Name = "serverPickerLauncherTextBox";
			this.serverPickerLauncherTextBox.Size = new Size(289, 23);
			this.serverPickerLauncherTextBox.TabIndex = 1;
			this.setDefaultServerCheckBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.setDefaultServerCheckBox.Enabled = false;
			this.setDefaultServerCheckBox.Location = new Point(16, 62);
			this.setDefaultServerCheckBox.Margin = new Padding(3, 11, 0, 0);
			this.setDefaultServerCheckBox.Name = "setDefaultServerCheckBox";
			this.setDefaultServerCheckBox.Size = new Size(286, 17);
			this.setDefaultServerCheckBox.TabIndex = 2;
			this.setDefaultServerCheckBox.Text = "setDefaultServerCheckBox";
			this.tableLayoutPanel.AutoLayout = true;
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.ColumnCount = 1;
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel.ContainerType = ContainerType.Control;
			this.tableLayoutPanel.Controls.Add(this.setDefaultServerCheckBox, 0, 2);
			this.tableLayoutPanel.Controls.Add(this.serverPickerLauncherTextBox, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.connectServerLabel, 0, 0);
			this.tableLayoutPanel.Dock = DockStyle.Top;
			this.tableLayoutPanel.Location = new Point(0, 0);
			this.tableLayoutPanel.Margin = new Padding(0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.Padding = new Padding(13, 12, 16, 0);
			this.tableLayoutPanel.RowCount = 3;
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.MinimumSize = new Size(318, 79);
			this.tableLayoutPanel.Size = new Size(318, 79);
			this.tableLayoutPanel.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.tableLayoutPanel);
			base.Name = "ConnectToServerControl";
			base.Size = new Size(318, 79);
			((ISupportInitialize)base.BindingSource).EndInit();
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040006F7 RID: 1783
		private AutomatedObjectPicker serverPicker;

		// Token: 0x040006F8 RID: 1784
		private Label connectServerLabel;

		// Token: 0x040006F9 RID: 1785
		private PickerLauncherTextBox serverPickerLauncherTextBox;

		// Token: 0x040006FA RID: 1786
		private AutoTableLayoutPanel tableLayoutPanel;

		// Token: 0x040006FB RID: 1787
		private AutoHeightCheckBox setDefaultServerCheckBox;
	}
}
