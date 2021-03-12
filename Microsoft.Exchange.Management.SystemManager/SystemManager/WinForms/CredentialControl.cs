using System;
using System.ComponentModel;
using System.Drawing;
using System.Management.Automation;
using System.Security;
using System.Windows.Forms;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001CC RID: 460
	public class CredentialControl : StrongTypeEditor<PSCredential>
	{
		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x0004EE7D File Offset: 0x0004D07D
		// (set) Token: 0x0600137A RID: 4986 RVA: 0x0004EE85 File Offset: 0x0004D085
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

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x0600137B RID: 4987 RVA: 0x0004EE8E File Offset: 0x0004D08E
		// (set) Token: 0x0600137C RID: 4988 RVA: 0x0004EE96 File Offset: 0x0004D096
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

		// Token: 0x0600137D RID: 4989 RVA: 0x0004EE9F File Offset: 0x0004D09F
		private bool ShouldSerializeAccountNameText()
		{
			return !string.Equals(this.AccountNameText, Strings.UserNameText);
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0004EEB9 File Offset: 0x0004D0B9
		private bool ShouldSerializePasswordText()
		{
			return !string.Equals(this.PasswordText, Strings.PasswordText);
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0004EED3 File Offset: 0x0004D0D3
		public override Size GetPreferredSize(Size proposedSize)
		{
			proposedSize.Width = base.Width;
			return this.tableLayoutPanel.GetPreferredSize(proposedSize);
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0004EEF0 File Offset: 0x0004D0F0
		public CredentialControl()
		{
			this.InitializeComponent();
			this.userNameLabel.Text = Strings.UserNameText;
			this.passwordLabel.Text = Strings.PasswordText;
			this.passwordExchangeTextBox.UseSystemPasswordChar = true;
			this.confirmPasswordLabel.Text = Strings.ConfirmationPasswordLabel;
			this.confirmPasswordTextBox.UseSystemPasswordChar = true;
			base.BindingSource.DataSource = typeof(PSCredential);
			this.userNameExchangeTextBox.DataBindings.Add("Text", base.BindingSource, "userName", true, DataSourceUpdateMode.OnValidation);
			this.passwordExchangeTextBox.DataBindings.Add("Text", base.BindingSource, "password", true, DataSourceUpdateMode.OnValidation);
			base.Validator = new CredentialControl.CredentialControlDataHandler(this);
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001381 RID: 4993 RVA: 0x0004EFC7 File Offset: 0x0004D1C7
		// (set) Token: 0x06001382 RID: 4994 RVA: 0x0004EFD4 File Offset: 0x0004D1D4
		public string AccountNameText
		{
			get
			{
				return this.userNameLabel.Text;
			}
			set
			{
				this.userNameLabel.Text = value;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001383 RID: 4995 RVA: 0x0004EFE2 File Offset: 0x0004D1E2
		// (set) Token: 0x06001384 RID: 4996 RVA: 0x0004EFEF File Offset: 0x0004D1EF
		public string PasswordText
		{
			get
			{
				return this.passwordLabel.Text;
			}
			set
			{
				this.passwordLabel.Text = value;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001385 RID: 4997 RVA: 0x0004EFFD File Offset: 0x0004D1FD
		// (set) Token: 0x06001386 RID: 4998 RVA: 0x0004F005 File Offset: 0x0004D205
		[DefaultValue(false)]
		public bool AllowPasswordConfirmation
		{
			get
			{
				return this.allowPasswordConfirmation;
			}
			set
			{
				this.allowPasswordConfirmation = value;
				if (!value)
				{
					base.SuspendLayout();
					this.confirmPasswordLabel.Hide();
					this.confirmPasswordTextBox.Hide();
					base.ResumeLayout();
					base.PerformLayout();
				}
			}
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0004F03C File Offset: 0x0004D23C
		protected override UIValidationError[] GetValidationErrors()
		{
			if (base.Enabled && this.AllowPasswordConfirmation && this.passwordExchangeTextBox.Text != this.confirmPasswordTextBox.Text)
			{
				return new UIValidationError[]
				{
					new UIValidationError(Strings.InvalidPasswordError, this.confirmPasswordTextBox)
				};
			}
			return UIValidationError.None;
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x0004F098 File Offset: 0x0004D298
		private void InitializeComponent()
		{
			this.passwordLabel = new Label();
			this.userNameLabel = new Label();
			this.passwordExchangeTextBox = new ExchangeTextBox();
			this.userNameExchangeTextBox = new ExchangeTextBox();
			this.confirmPasswordLabel = new Label();
			this.confirmPasswordTextBox = new ExchangeTextBox();
			this.tableLayoutPanel = new AutoTableLayoutPanel();
			this.tableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			this.passwordLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.passwordLabel.AutoSize = true;
			this.passwordLabel.Location = new Point(0, 48);
			this.passwordLabel.Margin = new Padding(0, 12, 0, 0);
			this.passwordLabel.Name = "passwordLabel";
			this.passwordLabel.Size = new Size(182, 13);
			this.passwordLabel.TabIndex = 2;
			this.passwordLabel.Text = "passwordLabel";
			this.userNameLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.userNameLabel.AutoSize = true;
			this.userNameLabel.Location = new Point(0, 0);
			this.userNameLabel.Margin = new Padding(0);
			this.userNameLabel.Name = "userNameLabel";
			this.userNameLabel.Size = new Size(182, 13);
			this.userNameLabel.TabIndex = 0;
			this.userNameLabel.Text = "userNameLabel";
			this.passwordExchangeTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.passwordExchangeTextBox.Location = new Point(3, 64);
			this.passwordExchangeTextBox.Margin = new Padding(3, 3, 0, 0);
			this.passwordExchangeTextBox.Name = "passwordExchangeTextBox";
			this.passwordExchangeTextBox.Size = new Size(179, 20);
			this.passwordExchangeTextBox.TabIndex = 3;
			this.passwordExchangeTextBox.UseSystemPasswordChar = true;
			this.userNameExchangeTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.userNameExchangeTextBox.Location = new Point(3, 16);
			this.userNameExchangeTextBox.Margin = new Padding(3, 3, 0, 0);
			this.userNameExchangeTextBox.Name = "userNameExchangeTextBox";
			this.userNameExchangeTextBox.Size = new Size(179, 20);
			this.userNameExchangeTextBox.TabIndex = 1;
			this.confirmPasswordLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.confirmPasswordLabel.AutoSize = true;
			this.confirmPasswordLabel.Location = new Point(0, 96);
			this.confirmPasswordLabel.Margin = new Padding(0, 12, 0, 0);
			this.confirmPasswordLabel.Name = "confirmPasswordLabel";
			this.confirmPasswordLabel.Size = new Size(182, 13);
			this.confirmPasswordLabel.TabIndex = 4;
			this.confirmPasswordLabel.Text = "confirmPasswordLabel";
			this.confirmPasswordTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.confirmPasswordTextBox.Location = new Point(3, 112);
			this.confirmPasswordTextBox.Margin = new Padding(3, 3, 0, 0);
			this.confirmPasswordTextBox.Name = "confirmPasswordTextBox";
			this.confirmPasswordTextBox.Size = new Size(179, 20);
			this.confirmPasswordTextBox.TabIndex = 5;
			this.confirmPasswordTextBox.UseSystemPasswordChar = true;
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.AutoLayout = true;
			this.tableLayoutPanel.ColumnCount = 1;
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel.Controls.Add(this.userNameLabel, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.confirmPasswordTextBox, 0, 5);
			this.tableLayoutPanel.Controls.Add(this.userNameExchangeTextBox, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.confirmPasswordLabel, 0, 4);
			this.tableLayoutPanel.Controls.Add(this.passwordLabel, 0, 2);
			this.tableLayoutPanel.Controls.Add(this.passwordExchangeTextBox, 0, 3);
			this.tableLayoutPanel.Dock = DockStyle.Top;
			this.tableLayoutPanel.Location = new Point(0, 0);
			this.tableLayoutPanel.Margin = new Padding(0);
			this.tableLayoutPanel.Padding = new Padding(0, 0, 16, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 6;
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.Size = new Size(182, 132);
			this.tableLayoutPanel.TabIndex = 6;
			this.AutoSize = true;
			this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			base.Controls.Add(this.tableLayoutPanel);
			base.Name = "CredentialControl";
			base.Size = new Size(182, 197);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000736 RID: 1846
		private bool allowPasswordConfirmation;

		// Token: 0x04000737 RID: 1847
		private Label passwordLabel;

		// Token: 0x04000738 RID: 1848
		private Label userNameLabel;

		// Token: 0x04000739 RID: 1849
		private ExchangeTextBox passwordExchangeTextBox;

		// Token: 0x0400073A RID: 1850
		private ExchangeTextBox userNameExchangeTextBox;

		// Token: 0x0400073B RID: 1851
		private Label confirmPasswordLabel;

		// Token: 0x0400073C RID: 1852
		private ExchangeTextBox confirmPasswordTextBox;

		// Token: 0x0400073D RID: 1853
		private AutoTableLayoutPanel tableLayoutPanel;

		// Token: 0x020001CD RID: 461
		public class CredentialControlDataHandler : StrongTypeEditorDataHandler<PSCredential>
		{
			// Token: 0x06001389 RID: 5001 RVA: 0x0004F620 File Offset: 0x0004D820
			public CredentialControlDataHandler(CredentialControl control) : base(control, "PSCredential")
			{
			}

			// Token: 0x0600138A RID: 5002 RVA: 0x0004F630 File Offset: 0x0004D830
			protected override void UpdateStrongType()
			{
				string text = (!DBNull.Value.Equals(base.Table.Rows[0]["userName"])) ? ((string)base.Table.Rows[0]["userName"]) : string.Empty;
				string password = (!DBNull.Value.Equals(base.Table.Rows[0]["password"])) ? ((string)base.Table.Rows[0]["password"]) : string.Empty;
				if (string.IsNullOrEmpty(text))
				{
					throw new StrongTypeFormatException(Strings.MissingUserNameError, "userName");
				}
				if (string.IsNullOrEmpty(text.Trim()))
				{
					throw new StrongTypeFormatException(Strings.SpaceUserNameError, "userName");
				}
				SecureString password2 = password.AsSecureString();
				base.StrongType = new PSCredential(text, password2);
			}

			// Token: 0x0600138B RID: 5003 RVA: 0x0004F730 File Offset: 0x0004D930
			protected override void UpdateTable()
			{
				base.Table.Rows[0]["userName"] = ((base.StrongType == null) ? string.Empty : base.StrongType.UserName);
				base.Table.Rows[0]["password"] = ((base.StrongType == null) ? string.Empty : base.StrongType.Password.AsUnsecureString());
			}
		}
	}
}
