using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200014D RID: 333
	public sealed partial class PromptForChoicesDialog : ExchangeForm
	{
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000D81 RID: 3457 RVA: 0x00032853 File Offset: 0x00030A53
		// (set) Token: 0x06000D82 RID: 3458 RVA: 0x0003285B File Offset: 0x00030A5B
		internal ConfirmationChoice UserChoice
		{
			get
			{
				return this.internalUserChoice;
			}
			set
			{
				this.internalUserChoice = value;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x00032864 File Offset: 0x00030A64
		// (set) Token: 0x06000D84 RID: 3460 RVA: 0x00032871 File Offset: 0x00030A71
		public string Message
		{
			get
			{
				return this.warningMessageLabel.Text;
			}
			set
			{
				this.warningMessageLabel.Text = value;
			}
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x000328BC File Offset: 0x00030ABC
		public PromptForChoicesDialog()
		{
			this.InitializeComponent();
			this.yesButton.Text = Strings.Yes;
			this.yesButton.Click += delegate(object param0, EventArgs param1)
			{
				this.UserChoice = ConfirmationChoice.Yes;
				base.Close();
			};
			this.yesToAllButton.Text = Strings.YesToAll;
			this.yesToAllButton.Click += delegate(object param0, EventArgs param1)
			{
				this.UserChoice = ConfirmationChoice.YesToAll;
				base.Close();
			};
			this.noButton.Text = Strings.No;
			this.noButton.Click += delegate(object param0, EventArgs param1)
			{
				this.UserChoice = ConfirmationChoice.No;
				base.Close();
			};
			this.cancelButton.Text = Strings.Cancel;
			this.cancelButton.Click += delegate(object param0, EventArgs param1)
			{
				this.UserChoice = ConfirmationChoice.NoToAll;
				base.Close();
			};
			this.warningIconPictureBox.Image = IconLibrary.ToBitmap(Icons.Warning, this.warningIconPictureBox.Size);
			base.AcceptButton = this.yesButton;
			base.CancelButton = this.cancelButton;
			this.UpdateButtonStatus(true);
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x000329E8 File Offset: 0x00030BE8
		internal PromptForChoicesDialog(string message, ConfirmationChoice defaultChoice) : this()
		{
			this.Message = message;
			switch (defaultChoice)
			{
			case ConfirmationChoice.Yes:
				base.AcceptButton = this.yesButton;
				return;
			case ConfirmationChoice.YesToAll:
				base.AcceptButton = this.yesToAllButton;
				return;
			case ConfirmationChoice.No:
				base.AcceptButton = this.noButton;
				return;
			default:
				base.AcceptButton = this.cancelButton;
				return;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x00032A4B File Offset: 0x00030C4B
		// (set) Token: 0x06000D88 RID: 3464 RVA: 0x00032A53 File Offset: 0x00030C53
		[DefaultValue(true)]
		public bool HasChoiceForMultipleObjects
		{
			get
			{
				return this.hasChoiceForMultipleObjects;
			}
			set
			{
				if (this.HasChoiceForMultipleObjects != value)
				{
					if (!value && base.AcceptButton == this.yesToAllButton)
					{
						throw new ArgumentException("Can not change to single confirmation mode when default choice is YesToAll");
					}
					this.UpdateButtonStatus(value);
					this.hasChoiceForMultipleObjects = value;
				}
			}
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00032A88 File Offset: 0x00030C88
		private void UpdateButtonStatus(bool isMultiple)
		{
			this.yesToAllButton.Visible = isMultiple;
			this.cancelButton.Visible = isMultiple;
			this.buttonsPanel.SuspendLayout();
			if (isMultiple)
			{
				this.buttonsPanel.ColumnStyles[0].Width = 25f;
				this.buttonsPanel.ColumnStyles[2].Width = 25f;
				this.buttonsPanel.ColumnStyles[3].Width = 8f;
				this.buttonsPanel.ColumnStyles[4].Width = 25f;
				this.buttonsPanel.ColumnStyles[5].Width = 8f;
				this.buttonsPanel.ColumnStyles[6].Width = 25f;
			}
			else
			{
				this.buttonsPanel.ColumnStyles[0].Width = 50f;
				this.buttonsPanel.ColumnStyles[2].Width = 0f;
				this.buttonsPanel.ColumnStyles[3].Width = 0f;
				this.buttonsPanel.ColumnStyles[4].Width = 50f;
				this.buttonsPanel.ColumnStyles[5].Width = 0f;
				this.buttonsPanel.ColumnStyles[6].Width = 0f;
			}
			this.buttonsPanel.ResumeLayout(false);
			this.buttonsPanel.PerformLayout();
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00032C20 File Offset: 0x00030E20
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			Button button = base.AcceptButton as Button;
			if (button != null)
			{
				button.Select();
			}
			Size sz = base.Size - base.ClientSize;
			base.Size = this.tableLayoutPanel.Size + base.Padding.Size + sz;
		}

		// Token: 0x04000561 RID: 1377
		private ConfirmationChoice internalUserChoice = ConfirmationChoice.NoToAll;

		// Token: 0x0400056A RID: 1386
		private bool hasChoiceForMultipleObjects;
	}
}
