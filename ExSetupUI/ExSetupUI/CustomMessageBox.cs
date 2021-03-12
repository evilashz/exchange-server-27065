using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x0200000E RID: 14
	public partial class CustomMessageBox : Form
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00004E7F File Offset: 0x0000307F
		public CustomMessageBox(Dictionary<MsgBoxButtons, string> buttonTexts, Dictionary<MsgBoxIcon, string> captureTexts)
		{
			this.buttonTexts = buttonTexts;
			this.captureTexts = captureTexts;
			this.InitializeComponent();
		}

		// Token: 0x1700001F RID: 31
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00004E9B File Offset: 0x0000309B
		public MessageBoxButtons Buttons
		{
			set
			{
				this.SetButtons(value);
			}
		}

		// Token: 0x17000020 RID: 32
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00004EA4 File Offset: 0x000030A4
		public MsgBoxIcon MessageIcon
		{
			set
			{
				this.SetIcon(value);
			}
		}

		// Token: 0x17000021 RID: 33
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00004EAD File Offset: 0x000030AD
		public string MessageText
		{
			set
			{
				this.messageLabel.Text = value;
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004EDC File Offset: 0x000030DC
		internal void SetButtons(MessageBoxButtons buttons)
		{
			this.btnOne.Visible = false;
			this.btnTwo.Visible = false;
			if (buttons != MessageBoxButtons.OK && buttons == MessageBoxButtons.YesNo)
			{
				this.btnOne.Text = this.buttonTexts[MsgBoxButtons.No];
				this.btnTwo.Text = this.buttonTexts[MsgBoxButtons.Yes];
				this.btnOne.DialogResult = DialogResult.No;
				this.btnTwo.DialogResult = DialogResult.Yes;
				this.btnOne.Visible = true;
				this.btnTwo.Visible = true;
				this.btnTwo.Focus();
				return;
			}
			this.btnOne.Text = this.buttonTexts[MsgBoxButtons.OK];
			this.btnOne.DialogResult = DialogResult.OK;
			this.btnOne.Visible = true;
			this.btnOne.Focus();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00004FB4 File Offset: 0x000031B4
		internal void SetIcon(MsgBoxIcon icon)
		{
			switch (icon)
			{
			case MsgBoxIcon.Error:
				this.messageImageBox.Image = this.customMessageBoxImageList.Images["SetupError.png"];
				this.captureLabel.Text = this.captureTexts[MsgBoxIcon.Error];
				return;
			case MsgBoxIcon.Warning:
				this.messageImageBox.Image = this.customMessageBoxImageList.Images["SetupWarning.png"];
				this.captureLabel.Text = this.captureTexts[MsgBoxIcon.Warning];
				return;
			case MsgBoxIcon.Cancel:
				this.messageImageBox.Image = this.customMessageBoxImageList.Images["SetupWarning.png"];
				this.captureLabel.Text = this.captureTexts[MsgBoxIcon.Cancel];
				return;
			}
			this.messageImageBox.Image = null;
			this.messageImageBox.Visible = false;
		}

		// Token: 0x04000059 RID: 89
		private Dictionary<MsgBoxButtons, string> buttonTexts;

		// Token: 0x0400005A RID: 90
		private Dictionary<MsgBoxIcon, string> captureTexts;
	}
}
