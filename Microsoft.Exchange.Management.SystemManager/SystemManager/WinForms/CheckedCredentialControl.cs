using System;
using System.ComponentModel;
using System.Drawing;
using System.Management.Automation;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001CE RID: 462
	public class CheckedCredentialControl : ExchangeUserControl
	{
		// Token: 0x0600138C RID: 5004 RVA: 0x0004F7AC File Offset: 0x0004D9AC
		public CheckedCredentialControl()
		{
			this.InitializeComponent();
			this.Caption = Strings.MasterAccountPageUseFollowingAccount;
			this.credentialControl.AllowPasswordConfirmation = false;
			this.checkBox.CheckedChanged += this.checkBox_CheckedChanged;
			this.credentialControl.StrongTypeChanged += this.credentialControl_StrongTypeChanged;
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x0004F80F File Offset: 0x0004DA0F
		// (set) Token: 0x0600138E RID: 5006 RVA: 0x0004F817 File Offset: 0x0004DA17
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

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x0004F820 File Offset: 0x0004DA20
		// (set) Token: 0x06001390 RID: 5008 RVA: 0x0004F828 File Offset: 0x0004DA28
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

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x0004F831 File Offset: 0x0004DA31
		// (set) Token: 0x06001392 RID: 5010 RVA: 0x0004F83E File Offset: 0x0004DA3E
		[DefaultValue(true)]
		public bool CheckBoxEnabled
		{
			get
			{
				return this.checkBox.Enabled;
			}
			set
			{
				if (value != this.CheckBoxEnabled)
				{
					if (!value)
					{
						this.checkBox.Checked = true;
					}
					this.checkBox.Enabled = value;
				}
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x0004F864 File Offset: 0x0004DA64
		// (set) Token: 0x06001394 RID: 5012 RVA: 0x0004F871 File Offset: 0x0004DA71
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Caption
		{
			get
			{
				return this.checkBox.Text;
			}
			set
			{
				this.checkBox.Text = value;
			}
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x0004F87F File Offset: 0x0004DA7F
		private void credentialControl_StrongTypeChanged(object sender, EventArgs e)
		{
			if (this.checkBox.Checked)
			{
				this.OnSelectedValueChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x0004F89C File Offset: 0x0004DA9C
		private void checkBox_CheckedChanged(object sender, EventArgs e)
		{
			bool @checked = this.checkBox.Checked;
			this.credentialControl.Enabled = @checked;
			if (!@checked || this.credentialControl.StrongType != null)
			{
				this.OnSelectedValueChanged(EventArgs.Empty);
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x0004F8DC File Offset: 0x0004DADC
		// (set) Token: 0x06001398 RID: 5016 RVA: 0x0004F8F8 File Offset: 0x0004DAF8
		[DefaultValue(null)]
		public PSCredential SelectedValue
		{
			get
			{
				if (!this.checkBox.Checked)
				{
					return null;
				}
				return this.credentialControl.StrongType;
			}
			set
			{
				if (value != this.SelectedValue)
				{
					this.suspendChangeNotification = true;
					this.checkBox.Checked = (null != value);
					if (this.checkBox.Checked)
					{
						this.credentialControl.StrongType = value;
					}
					this.suspendChangeNotification = false;
					this.OnSelectedValueChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0004F952 File Offset: 0x0004DB52
		private void OnSelectedValueChanged(EventArgs e)
		{
			if (!this.suspendChangeNotification && this.SelectedValueChanged != null)
			{
				this.SelectedValueChanged(this, e);
			}
		}

		// Token: 0x1400007A RID: 122
		// (add) Token: 0x0600139A RID: 5018 RVA: 0x0004F974 File Offset: 0x0004DB74
		// (remove) Token: 0x0600139B RID: 5019 RVA: 0x0004F9AC File Offset: 0x0004DBAC
		public event EventHandler SelectedValueChanged;

		// Token: 0x0600139C RID: 5020 RVA: 0x0004F9E1 File Offset: 0x0004DBE1
		public override Size GetPreferredSize(Size proposedSize)
		{
			proposedSize.Width = base.Width;
			return this.tableLayoutPanel.GetPreferredSize(proposedSize);
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0004F9FC File Offset: 0x0004DBFC
		private void InitializeComponent()
		{
			this.tableLayoutPanel = new AutoTableLayoutPanel();
			this.checkBox = new AutoHeightCheckBox();
			this.credentialControl = new CredentialControl();
			this.tableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			this.tableLayoutPanel.AutoLayout = true;
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.ColumnCount = 3;
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 16f));
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 16f));
			this.tableLayoutPanel.Controls.Add(this.checkBox, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.credentialControl, 1, 1);
			this.tableLayoutPanel.Dock = DockStyle.Top;
			this.tableLayoutPanel.Location = new Point(0, 0);
			this.tableLayoutPanel.Margin = new Padding(0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 2;
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.Size = new Size(100, 170);
			this.tableLayoutPanel.TabIndex = 0;
			this.checkBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.tableLayoutPanel.SetColumnSpan(this.checkBox, 3);
			this.checkBox.Location = new Point(3, 0);
			this.checkBox.Margin = new Padding(3, 0, 0, 0);
			this.checkBox.Name = "checkBox";
			this.checkBox.Size = new Size(97, 17);
			this.checkBox.TabIndex = 0;
			this.checkBox.Text = "checkBox";
			this.credentialControl.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.tableLayoutPanel.SetColumnSpan(this.credentialControl, 2);
			this.credentialControl.Enabled = false;
			this.credentialControl.Location = new Point(16, 25);
			this.credentialControl.Margin = new Padding(0, 8, 0, 0);
			this.credentialControl.Name = "credentialControl";
			this.credentialControl.Size = new Size(84, 145);
			this.credentialControl.TabIndex = 1;
			base.Controls.Add(this.tableLayoutPanel);
			base.Name = "CheckedCredentialControl";
			this.AutoSize = true;
			this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			base.Size = new Size(100, 170);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400073E RID: 1854
		private AutoHeightCheckBox checkBox;

		// Token: 0x0400073F RID: 1855
		private CredentialControl credentialControl;

		// Token: 0x04000740 RID: 1856
		private AutoTableLayoutPanel tableLayoutPanel;

		// Token: 0x04000741 RID: 1857
		private bool suspendChangeNotification;
	}
}
