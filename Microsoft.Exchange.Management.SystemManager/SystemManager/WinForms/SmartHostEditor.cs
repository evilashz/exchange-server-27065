using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200020C RID: 524
	public class SmartHostEditor : StrongTypeEditor<SmartHost>
	{
		// Token: 0x060017C0 RID: 6080 RVA: 0x00063D4C File Offset: 0x00061F4C
		public SmartHostEditor()
		{
			this.InitializeComponent();
			this.isIpAddressRadioButton.Text = Strings.IPAddressText;
			this.isHostNameRadioButton.Text = Strings.FullyQualifiedDomainNameText;
			this.ipAddressLabel.Text = Strings.ExampleIPAddressText;
			this.hostNameLabel.Text = Strings.ExampleDomainNameText;
			this.isIpAddressRadioButton.Checked = true;
			base.BindingSource.DataSource = typeof(SmartHost);
			this.isIpAddressRadioButton.DataBindings.Add("Checked", base.BindingSource, "IsIpAddress", true, DataSourceUpdateMode.OnPropertyChanged);
			this.isHostNameRadioButton.DataBindings.Add("Checked", base.BindingSource, "IsIpAddress", true, DataSourceUpdateMode.Never).Format += SmartHostEditor.ConvertBoolNot;
			this.addressTextBox.DataBindings.Add("Text", base.BindingSource, "Address", true, DataSourceUpdateMode.OnValidation);
			this.domain.DataBindings.Add("Text", base.BindingSource, "Domain", true, DataSourceUpdateMode.OnValidation);
			base.DataBindings.Add("IsCloudOrganizationMode", base.BindingSource, "IsCloudOrganizationMode", true, DataSourceUpdateMode.Never);
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x00063E94 File Offset: 0x00062094
		protected override void OnValidating(CancelEventArgs e)
		{
			if (this.isHostNameRadioButton.Checked)
			{
				this.domain.DataBindings[0].WriteValue();
			}
			else
			{
				this.addressTextBox.DataBindings[0].WriteValue();
			}
			base.OnValidating(e);
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x00063EE3 File Offset: 0x000620E3
		internal static void ConvertBoolNot(object sender, ConvertEventArgs e)
		{
			e.Value = !(bool)e.Value;
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x00063EFE File Offset: 0x000620FE
		private void isIpAddress_CheckedChanged(object sender, EventArgs e)
		{
			this.addressTextBox.Enabled = this.isIpAddressRadioButton.Checked;
			this.domain.Enabled = !this.isIpAddressRadioButton.Checked;
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060017C4 RID: 6084 RVA: 0x00063F2F File Offset: 0x0006212F
		// (set) Token: 0x060017C5 RID: 6085 RVA: 0x00063F37 File Offset: 0x00062137
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override Size MinimumSize
		{
			get
			{
				return base.MinimumSize;
			}
			set
			{
				base.MinimumSize = value;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060017C6 RID: 6086 RVA: 0x00063F40 File Offset: 0x00062140
		// (set) Token: 0x060017C7 RID: 6087 RVA: 0x00063F48 File Offset: 0x00062148
		[DefaultValue(true)]
		public new bool AutoSize
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

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060017C8 RID: 6088 RVA: 0x00063F51 File Offset: 0x00062151
		// (set) Token: 0x060017C9 RID: 6089 RVA: 0x00063F59 File Offset: 0x00062159
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

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060017CA RID: 6090 RVA: 0x00063F62 File Offset: 0x00062162
		// (set) Token: 0x060017CB RID: 6091 RVA: 0x00063F74 File Offset: 0x00062174
		[DefaultValue(false)]
		public bool IsCloudOrganizationMode
		{
			get
			{
				return !this.isIpAddressRadioButton.Enabled;
			}
			set
			{
				if (this.IsCloudOrganizationMode != value)
				{
					this.isIpAddressRadioButton.Checked = (this.isIpAddressRadioButton.Enabled = (this.addressTextBox.Enabled = (this.ipAddressLabel.Enabled = !value)));
					this.isHostNameRadioButton.Checked = value;
				}
			}
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x00063FCF File Offset: 0x000621CF
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x00063FF0 File Offset: 0x000621F0
		private void InitializeComponent()
		{
			this.isIpAddressRadioButton = new AutoHeightRadioButton();
			this.addressTextBox = new ExchangeTextBox();
			this.isHostNameRadioButton = new AutoHeightRadioButton();
			this.domain = new ExchangeTextBox();
			this.tableLayoutPanel = new TableLayoutPanel();
			this.ipAddressLabel = new Label();
			this.hostNameLabel = new Label();
			this.tableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			this.isIpAddressRadioButton.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.isIpAddressRadioButton.Checked = true;
			this.tableLayoutPanel.SetColumnSpan(this.isIpAddressRadioButton, 2);
			this.isIpAddressRadioButton.Location = new Point(3, 0);
			this.isIpAddressRadioButton.Margin = new Padding(3, 0, 0, 0);
			this.isIpAddressRadioButton.Name = "isIpAddressRadioButton";
			this.isIpAddressRadioButton.Size = new Size(399, 17);
			this.isIpAddressRadioButton.TabIndex = 0;
			this.isIpAddressRadioButton.TabStop = true;
			this.isIpAddressRadioButton.Text = "IP Address";
			this.isIpAddressRadioButton.UseVisualStyleBackColor = true;
			this.isIpAddressRadioButton.CheckedChanged += this.isIpAddress_CheckedChanged;
			this.addressTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.addressTextBox.Location = new Point(19, 25);
			this.addressTextBox.Margin = new Padding(3, 8, 0, 0);
			this.addressTextBox.Name = "address";
			this.addressTextBox.Size = new Size(383, 20);
			this.addressTextBox.TabIndex = 1;
			this.isHostNameRadioButton.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.tableLayoutPanel.SetColumnSpan(this.isHostNameRadioButton, 2);
			this.isHostNameRadioButton.Location = new Point(3, 78);
			this.isHostNameRadioButton.Margin = new Padding(3, 12, 0, 0);
			this.isHostNameRadioButton.Name = "isHostNameRadioButton";
			this.isHostNameRadioButton.Size = new Size(399, 17);
			this.isHostNameRadioButton.TabIndex = 3;
			this.isHostNameRadioButton.Text = "Host name";
			this.isHostNameRadioButton.UseVisualStyleBackColor = true;
			this.domain.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.domain.Enabled = false;
			this.domain.Location = new Point(19, 103);
			this.domain.Margin = new Padding(3, 8, 0, 0);
			this.domain.Name = "domain";
			this.domain.Size = new Size(383, 20);
			this.domain.TabIndex = 4;
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.ColumnCount = 3;
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 16f));
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 16f));
			this.tableLayoutPanel.Controls.Add(this.domain, 1, 4);
			this.tableLayoutPanel.Controls.Add(this.isIpAddressRadioButton, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.isHostNameRadioButton, 0, 3);
			this.tableLayoutPanel.Controls.Add(this.addressTextBox, 1, 1);
			this.tableLayoutPanel.Controls.Add(this.ipAddressLabel, 1, 2);
			this.tableLayoutPanel.Controls.Add(this.hostNameLabel, 1, 5);
			this.tableLayoutPanel.Dock = DockStyle.Top;
			this.tableLayoutPanel.Location = new Point(0, 0);
			this.tableLayoutPanel.Margin = new Padding(0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 6;
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.Size = new Size(418, 144);
			this.tableLayoutPanel.TabIndex = 5;
			this.ipAddressLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.ipAddressLabel.AutoSize = true;
			this.ipAddressLabel.Location = new Point(16, 53);
			this.ipAddressLabel.Margin = new Padding(0, 8, 0, 0);
			this.ipAddressLabel.Name = "ipAddressLabel";
			this.ipAddressLabel.Size = new Size(386, 13);
			this.ipAddressLabel.TabIndex = 2;
			this.ipAddressLabel.Text = "label1";
			this.hostNameLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.hostNameLabel.AutoSize = true;
			this.hostNameLabel.Location = new Point(16, 131);
			this.hostNameLabel.Margin = new Padding(0, 8, 0, 0);
			this.hostNameLabel.Name = "hostNameLabel";
			this.hostNameLabel.Size = new Size(386, 13);
			this.hostNameLabel.TabIndex = 5;
			this.hostNameLabel.Text = "label1";
			this.AutoSize = true;
			this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			base.Controls.Add(this.tableLayoutPanel);
			this.MinimumSize = new Size(418, 0);
			base.Name = "SmartHostEditor";
			base.Size = new Size(418, 144);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040008E1 RID: 2273
		private IContainer components;

		// Token: 0x040008E2 RID: 2274
		private AutoHeightRadioButton isIpAddressRadioButton;

		// Token: 0x040008E3 RID: 2275
		private ExchangeTextBox addressTextBox;

		// Token: 0x040008E4 RID: 2276
		private AutoHeightRadioButton isHostNameRadioButton;

		// Token: 0x040008E5 RID: 2277
		private ExchangeTextBox domain;

		// Token: 0x040008E6 RID: 2278
		private TableLayoutPanel tableLayoutPanel;

		// Token: 0x040008E7 RID: 2279
		private Label ipAddressLabel;

		// Token: 0x040008E8 RID: 2280
		private Label hostNameLabel;
	}
}
