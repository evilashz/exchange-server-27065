using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000149 RID: 329
	public partial class MessageBoxDialog : Form
	{
		// Token: 0x06000D40 RID: 3392 RVA: 0x00030A2D File Offset: 0x0002EC2D
		[Obsolete("Use of paramless constructor can cause serious performance problem. Use the other one instead.")]
		public MessageBoxDialog() : this(string.Empty, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1)
		{
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00030A44 File Offset: 0x0002EC44
		public MessageBoxDialog(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
		{
			this.InitializeComponent();
			this.messageScrollLabelAutoSizePanel.SuspendLayout();
			this.buttonsPanel.SuspendLayout();
			this.messagePanel.SuspendLayout();
			this.tableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			this.messageLabel.UseCompatibleTextRendering = true;
			this.messageScrollLabel.UseCompatibleTextRendering = true;
			this.captionLabel.UseCompatibleTextRendering = true;
			this.noButton.UseCompatibleTextRendering = true;
			this.yesButton.UseCompatibleTextRendering = true;
			this.cancelButton.UseCompatibleTextRendering = true;
			this.okButton.UseCompatibleTextRendering = true;
			this.okButton.Text = Strings.Ok;
			this.yesButton.Text = Strings.Yes;
			this.noButton.Text = Strings.No;
			this.cancelButton.Text = Strings.Cancel;
			this.ScrollBars = ScrollBars.None;
			this.Message = message;
			this.Text = caption;
			this.Buttons = buttons;
			this.Icon = icon;
			this.DefaultButton = defaultButton;
			this.messageScrollLabelAutoSizePanel.ResumeLayout(false);
			this.messageScrollLabelAutoSizePanel.PerformLayout();
			this.buttonsPanel.ResumeLayout(false);
			this.buttonsPanel.PerformLayout();
			this.messagePanel.ResumeLayout(false);
			this.messagePanel.PerformLayout();
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x00030BD7 File Offset: 0x0002EDD7
		// (set) Token: 0x06000D43 RID: 3395 RVA: 0x00030BE3 File Offset: 0x0002EDE3
		public override RightToLeft RightToLeft
		{
			get
			{
				if (!LayoutHelper.CultureInfoIsRightToLeft)
				{
					return RightToLeft.No;
				}
				return RightToLeft.Yes;
			}
			set
			{
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x00030BE5 File Offset: 0x0002EDE5
		// (set) Token: 0x06000D45 RID: 3397 RVA: 0x00030BED File Offset: 0x0002EDED
		public override bool RightToLeftLayout
		{
			get
			{
				return LayoutHelper.IsRightToLeft(this);
			}
			set
			{
			}
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x00030BEF File Offset: 0x0002EDEF
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.SelectDefaultButton();
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x00030BFE File Offset: 0x0002EDFE
		// (set) Token: 0x06000D48 RID: 3400 RVA: 0x00030C08 File Offset: 0x0002EE08
		[DefaultValue(ScrollBars.None)]
		public ScrollBars ScrollBars
		{
			get
			{
				return this.scrollBars;
			}
			private set
			{
				switch (value)
				{
				case ScrollBars.None:
					this.autoScrollPanel.Size = Size.Empty;
					this.messagePanel.ColumnStyles[0].Width = 100f;
					this.messageLabel.Visible = true;
					this.messagePanel.ColumnStyles[1].Width = 0f;
					this.autoScrollPanel.Visible = false;
					goto IL_F5;
				case ScrollBars.Vertical:
					this.messagePanel.ColumnStyles[0].Width = 0f;
					this.messageLabel.Visible = false;
					this.messagePanel.ColumnStyles[1].Width = 100f;
					this.autoScrollPanel.Visible = true;
					this.autoScrollPanel.Size = this.messageLabel.MaximumSize;
					goto IL_F5;
				}
				throw new InvalidEnumArgumentException("ScrollBars", (int)value, typeof(ScrollBars));
				IL_F5:
				this.scrollBars = value;
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x00030D11 File Offset: 0x0002EF11
		// (set) Token: 0x06000D4A RID: 3402 RVA: 0x00030D19 File Offset: 0x0002EF19
		[DefaultValue("")]
		public string Caption
		{
			get
			{
				return this.Text;
			}
			set
			{
				this.Text = value;
				this.captionLabel.Text = value;
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x00030D2E File Offset: 0x0002EF2E
		// (set) Token: 0x06000D4C RID: 3404 RVA: 0x00030D38 File Offset: 0x0002EF38
		[DefaultValue("")]
		public string Message
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
				StringBuilder stringBuilder = new StringBuilder();
				using (StringReader stringReader = new StringReader(this.Message))
				{
					int num = 0;
					string value2;
					while ((value2 = stringReader.ReadLine()) != null)
					{
						num++;
						if (num > 64)
						{
							stringBuilder.AppendLine();
							stringBuilder.Append(Strings.TooManyMessages);
							break;
						}
						if (num > 1)
						{
							stringBuilder.AppendLine();
						}
						stringBuilder.Append(value2);
					}
					this.messageLabel.Text = stringBuilder.ToString();
					this.messageScrollLabel.Text = this.messageLabel.Text;
					if (this.messageLabel.PreferredHeight >= this.messageLabel.MaximumSize.Height && this.ScrollBars != ScrollBars.Vertical)
					{
						this.ScrollBars = ScrollBars.Vertical;
					}
					else if (this.messageLabel.PreferredHeight < this.messageLabel.MaximumSize.Height && this.ScrollBars != ScrollBars.None)
					{
						this.ScrollBars = ScrollBars.None;
					}
				}
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x00030E4C File Offset: 0x0002F04C
		// (set) Token: 0x06000D4E RID: 3406 RVA: 0x00030E54 File Offset: 0x0002F054
		[DefaultValue(MessageBoxDefaultButton.Button1)]
		public MessageBoxDefaultButton DefaultButton
		{
			get
			{
				return this.defaultButton;
			}
			set
			{
				if (!Enum.IsDefined(typeof(MessageBoxDefaultButton), value))
				{
					throw new InvalidEnumArgumentException("DefaultButton", (int)value, typeof(MessageBoxDefaultButton));
				}
				if (value != this.DefaultButton)
				{
					this.defaultButton = value;
					if (base.IsHandleCreated)
					{
						this.SelectDefaultButton();
					}
				}
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x00030EAC File Offset: 0x0002F0AC
		// (set) Token: 0x06000D50 RID: 3408 RVA: 0x00030EB4 File Offset: 0x0002F0B4
		[DefaultValue(MessageBoxIcon.None)]
		public new MessageBoxIcon Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				if (value <= MessageBoxIcon.Hand)
				{
					if (value == MessageBoxIcon.None)
					{
						goto IL_CA;
					}
					if (value == MessageBoxIcon.Hand)
					{
						this.iconPictureBox.Image = IconLibrary.ToBitmap(Icons.Error, this.iconPictureBox.Size);
						goto IL_CA;
					}
				}
				else
				{
					if (value == MessageBoxIcon.Question)
					{
						this.iconPictureBox.Image = IconLibrary.ToBitmap(Icons.Help, this.iconPictureBox.Size);
						goto IL_CA;
					}
					if (value == MessageBoxIcon.Exclamation)
					{
						this.iconPictureBox.Image = IconLibrary.ToBitmap(Icons.Warning, this.iconPictureBox.Size);
						goto IL_CA;
					}
					if (value == MessageBoxIcon.Asterisk)
					{
						this.iconPictureBox.Image = IconLibrary.ToBitmap(Icons.Information, this.iconPictureBox.Size);
						goto IL_CA;
					}
				}
				throw new InvalidEnumArgumentException("Icon", (int)value, typeof(MessageBoxIcon));
				IL_CA:
				this.iconPictureBox.Visible = (value != MessageBoxIcon.None);
				this.icon = value;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x00030FA4 File Offset: 0x0002F1A4
		// (set) Token: 0x06000D52 RID: 3410 RVA: 0x00030FAC File Offset: 0x0002F1AC
		[DefaultValue(MessageBoxButtons.OK)]
		public MessageBoxButtons Buttons
		{
			get
			{
				return this.buttons;
			}
			set
			{
				List<Button> list = new List<Button>();
				switch (value)
				{
				case MessageBoxButtons.OK:
				case MessageBoxButtons.OKCancel:
					list.Add(this.okButton);
					if (MessageBoxButtons.OKCancel == value)
					{
						list.Add(this.cancelButton);
						goto IL_82;
					}
					goto IL_82;
				case MessageBoxButtons.YesNoCancel:
				case MessageBoxButtons.YesNo:
					list.Add(this.yesButton);
					list.Add(this.noButton);
					if (MessageBoxButtons.YesNoCancel == value)
					{
						list.Add(this.cancelButton);
						goto IL_82;
					}
					goto IL_82;
				}
				throw new InvalidEnumArgumentException("Buttons", (int)value, typeof(MessageBoxButtons));
				IL_82:
				this.btnControls = list.ToArray();
				this.SetButtonsAndColumnStyles();
				this.EnableXButton(value != MessageBoxButtons.YesNo);
				this.buttons = value;
			}
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00031064 File Offset: 0x0002F264
		private void SelectDefaultButton()
		{
			int num = (int)(this.DefaultButton / MessageBoxDefaultButton.Button2);
			if (this.btnControls.Length >= num + 1)
			{
				this.btnControls[num].Select();
			}
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00031098 File Offset: 0x0002F298
		private void EnableXButton(bool enabled)
		{
			this.isCloseBoxDisabled = !enabled;
			IntPtr systemMenu = UnsafeNativeMethods.GetSystemMenu(new HandleRef(this, base.Handle), false);
			int num = enabled ? 0 : 1;
			UnsafeNativeMethods.EnableMenuItem(new HandleRef(this, systemMenu), 61536, num);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x000310E0 File Offset: 0x0002F2E0
		private void SetButtonsAndColumnStyles()
		{
			for (int i = 0; i < this.buttonsPanel.ColumnStyles.Count; i++)
			{
				this.buttonsPanel.ColumnStyles[i].SizeType = SizeType.Absolute;
				this.buttonsPanel.ColumnStyles[i].Width = 0f;
				Button button;
				if ((button = (this.buttonsPanel.GetControlFromPosition(i, 0) as Button)) != null)
				{
					button.Visible = false;
				}
			}
			foreach (Button button2 in this.btnControls)
			{
				int column;
				if (-1 != (column = this.buttonsPanel.GetColumn(button2)))
				{
					this.buttonsPanel.ColumnStyles[column].SizeType = SizeType.Percent;
					this.buttonsPanel.ColumnStyles[column].Width = 100f / (float)this.btnControls.Length;
					if (button2 != this.btnControls[this.btnControls.Length - 1])
					{
						this.buttonsPanel.ColumnStyles[column + 1].SizeType = SizeType.Absolute;
						this.buttonsPanel.ColumnStyles[column + 1].Width = 8f;
					}
					button2.Visible = true;
				}
			}
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x00031220 File Offset: 0x0002F420
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (this.Buttons == MessageBoxButtons.OK && (keyData & (Keys.Control | Keys.Alt)) == Keys.None && (keyData & Keys.KeyCode) == Keys.Escape)
			{
				base.DialogResult = DialogResult.Cancel;
				return true;
			}
			return (this.isCloseBoxDisabled && (keyData & Keys.Alt) == Keys.Alt && (keyData & Keys.Control) == Keys.None && (keyData & Keys.KeyCode) == Keys.F4) || base.ProcessDialogKey(keyData);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00031285 File Offset: 0x0002F485
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == (Keys)131139)
			{
				WinformsHelper.SetDataObjectToClipboard(this.Content, true);
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000D58 RID: 3416 RVA: 0x000312A8 File Offset: 0x0002F4A8
		public string Content
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("---------------------------");
				stringBuilder.AppendLine(this.Caption);
				stringBuilder.AppendLine("---------------------------");
				stringBuilder.AppendLine(this.Message);
				stringBuilder.AppendLine("---------------------------");
				foreach (Button button in this.btnControls)
				{
					stringBuilder.Append(button.Text.Replace("&", "") + "   ");
				}
				stringBuilder.AppendLine();
				stringBuilder.AppendLine("---------------------------");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00031351 File Offset: 0x0002F551
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			if (MessageBoxDialog.Test_FormShown != null)
			{
				MessageBoxDialog.Test_FormShown(this, EventArgs.Empty);
			}
		}

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06000D5A RID: 3418 RVA: 0x00031374 File Offset: 0x0002F574
		// (remove) Token: 0x06000D5B RID: 3419 RVA: 0x000313A8 File Offset: 0x0002F5A8
		public static event EventHandler Test_FormShown;

		// Token: 0x04000543 RID: 1347
		private const int MaximumLinesNumberOfShowedMessage = 64;

		// Token: 0x04000544 RID: 1348
		private Button[] btnControls;

		// Token: 0x04000545 RID: 1349
		private bool isCloseBoxDisabled;

		// Token: 0x04000546 RID: 1350
		private ScrollBars scrollBars;

		// Token: 0x04000547 RID: 1351
		private string message = string.Empty;

		// Token: 0x04000548 RID: 1352
		private MessageBoxDefaultButton defaultButton;

		// Token: 0x04000549 RID: 1353
		private MessageBoxIcon icon;

		// Token: 0x0400054A RID: 1354
		private MessageBoxButtons buttons;
	}
}
