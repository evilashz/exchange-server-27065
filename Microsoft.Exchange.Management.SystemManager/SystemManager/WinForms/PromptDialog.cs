using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001FE RID: 510
	public partial class PromptDialog : PropertyPageDialog
	{
		// Token: 0x0600172B RID: 5931 RVA: 0x00061AE9 File Offset: 0x0005FCE9
		public PromptDialog() : this(true)
		{
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x00061AF2 File Offset: 0x0005FCF2
		public PromptDialog(bool canInputMessage) : this(canInputMessage, false)
		{
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x00061B94 File Offset: 0x0005FD94
		public PromptDialog(bool canInputMessage, bool multiline)
		{
			this.InitializeComponent();
			this.InitializeTextBoxContent(multiline);
			this.labelMessage.UseMnemonic = false;
			base.RegisterPropertyPage(this.panelAll);
			this.canInputMessage = canInputMessage;
			this.textBoxContent.ReadOnly = !this.canInputMessage;
			this.AllowEmpty = false;
			this.Message = "";
			this.textBoxContent.TextChanged += delegate(object param0, EventArgs param1)
			{
				base.OkEnabled = this.CanOkEnabled;
			};
			base.Shown += delegate(object param0, EventArgs param1)
			{
				this.SetButtons();
				MessageBoxDefaultButton messageBoxDefaultButton = this.DefaultButton;
				if (messageBoxDefaultButton != MessageBoxDefaultButton.Button1)
				{
					if (messageBoxDefaultButton == MessageBoxDefaultButton.Button2)
					{
						((Button)base.CancelButton).Select();
						goto IL_5F;
					}
					if (messageBoxDefaultButton == MessageBoxDefaultButton.Button3)
					{
						goto IL_5F;
					}
				}
				if (this.textBoxContent.ReadOnly)
				{
					((Button)base.AcceptButton).Select();
				}
				else
				{
					this.textBoxContent.Select();
				}
				IL_5F:
				if (this.InputMaxLength != 0)
				{
					this.textBoxContent.MaxLength = this.InputMaxLength;
				}
			};
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x00061C42 File Offset: 0x0005FE42
		// (set) Token: 0x0600172F RID: 5935 RVA: 0x00061C4A File Offset: 0x0005FE4A
		[DefaultValue(0)]
		public int InputMaxLength
		{
			get
			{
				return this.textMaxLength;
			}
			set
			{
				this.textMaxLength = value;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06001730 RID: 5936 RVA: 0x00061C53 File Offset: 0x0005FE53
		// (set) Token: 0x06001731 RID: 5937 RVA: 0x00061C5B File Offset: 0x0005FE5B
		[DefaultValue(false)]
		public bool AllowEmpty
		{
			get
			{
				return this.allowEmpty;
			}
			set
			{
				this.allowEmpty = value;
				base.OkEnabled = this.CanOkEnabled;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06001732 RID: 5938 RVA: 0x00061C70 File Offset: 0x0005FE70
		private bool CanOkEnabled
		{
			get
			{
				return this.AllowEmpty || !this.canInputMessage || !string.IsNullOrEmpty(this.ContentText);
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x00061C92 File Offset: 0x0005FE92
		// (set) Token: 0x06001734 RID: 5940 RVA: 0x00061CA4 File Offset: 0x0005FEA4
		[DefaultValue(null)]
		public object DataSource
		{
			get
			{
				return this.panelAll.BindingSource.DataSource;
			}
			set
			{
				this.panelAll.BindingSource.DataSource = value;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06001735 RID: 5941 RVA: 0x00061CB7 File Offset: 0x0005FEB7
		// (set) Token: 0x06001736 RID: 5942 RVA: 0x00061CF0 File Offset: 0x0005FEF0
		[DefaultValue("")]
		public string ValueMember
		{
			get
			{
				return this.valueMember;
			}
			set
			{
				if (this.ValueMember != value)
				{
					this.valueMember = (value ?? string.Empty);
					this.textBoxContent.DataBindings.Clear();
					if (!string.IsNullOrEmpty(this.ValueMember))
					{
						Binding binding = this.textBoxContent.DataBindings.Add("Text", this.panelAll.BindingSource, this.ValueMember, true, DataSourceUpdateMode.OnValidation);
						binding.Parse += delegate(object sender, ConvertEventArgs e)
						{
							ConvertEventHandler convertEventHandler = (ConvertEventHandler)base.Events[PromptDialog.EventParse];
							if (convertEventHandler != null)
							{
								convertEventHandler(sender, e);
							}
						};
					}
				}
			}
		}

		// Token: 0x1400009D RID: 157
		// (add) Token: 0x06001737 RID: 5943 RVA: 0x00061D7A File Offset: 0x0005FF7A
		// (remove) Token: 0x06001738 RID: 5944 RVA: 0x00061D8D File Offset: 0x0005FF8D
		public event ConvertEventHandler Parse
		{
			add
			{
				base.Events.AddHandler(PromptDialog.EventParse, value);
			}
			remove
			{
				base.Events.RemoveHandler(PromptDialog.EventParse, value);
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001739 RID: 5945 RVA: 0x00061DA0 File Offset: 0x0005FFA0
		// (set) Token: 0x0600173A RID: 5946 RVA: 0x00061DAD File Offset: 0x0005FFAD
		[DefaultValue("")]
		public string Message
		{
			get
			{
				return this.labelMessage.Text;
			}
			set
			{
				this.labelMessage.Text = value;
				this.labelMessage.Visible = !string.IsNullOrEmpty(value);
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x00061DCF File Offset: 0x0005FFCF
		// (set) Token: 0x0600173C RID: 5948 RVA: 0x00061DDC File Offset: 0x0005FFDC
		[DefaultValue("")]
		public string ContentText
		{
			get
			{
				return this.textBoxContent.Text;
			}
			set
			{
				this.textBoxContent.Text = value;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x0600173D RID: 5949 RVA: 0x00061DEA File Offset: 0x0005FFEA
		// (set) Token: 0x0600173E RID: 5950 RVA: 0x00061DF7 File Offset: 0x0005FFF7
		public string ExampleText
		{
			get
			{
				return this.exampleLabel.Text;
			}
			set
			{
				this.exampleLabel.Text = value;
				this.exampleLabel.Visible = !string.IsNullOrEmpty(value);
			}
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x00061E1C File Offset: 0x0006001C
		private void InitializeTextBoxContent(bool multiline)
		{
			base.SuspendLayout();
			this.tableLayoutPanel.SuspendLayout();
			this.textBoxContent.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.textBoxContent.Name = "textBoxContent";
			if (multiline)
			{
				this.textBoxContentPanel = new AutoSizePanel();
				this.textBoxContentPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
				this.textBoxContentPanel.BackColor = SystemColors.Window;
				this.textBoxContentPanel.Location = new Point(66, 53);
				this.textBoxContentPanel.Margin = new Padding(3, 0, 0, 0);
				this.textBoxContentPanel.Name = "textBoxContentPanel";
				this.textBoxContentPanel.Size = new Size(346, 100);
				this.textBoxContentPanel.TabIndex = 3;
				this.textBoxContent.Multiline = true;
				this.textBoxContent.Margin = new Padding(0);
				this.textBoxContent.TabIndex = 0;
				this.textBoxContent.ScrollBars = ScrollBars.Vertical;
				this.textBoxContent.Size = new Size(346, 100);
				this.textBoxContent.AcceptsReturn = true;
				this.tableLayoutPanel.Controls.Add(this.textBoxContentPanel, 1, 2);
				this.textBoxContentPanel.Controls.Add(this.textBoxContent);
			}
			else
			{
				this.textBoxContent.Margin = new Padding(3, 0, 0, 0);
				this.textBoxContent.TabIndex = 3;
				this.textBoxContent.AcceptsReturn = false;
				this.tableLayoutPanel.Controls.Add(this.textBoxContent, 1, 2);
			}
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06001740 RID: 5952 RVA: 0x00061FD0 File Offset: 0x000601D0
		// (set) Token: 0x06001741 RID: 5953 RVA: 0x00061FDD File Offset: 0x000601DD
		[DefaultValue("")]
		public string ContentLabel
		{
			get
			{
				return this.labelContentLabel.Text;
			}
			set
			{
				this.labelContentLabel.Text = value;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x00061FEB File Offset: 0x000601EB
		// (set) Token: 0x06001743 RID: 5955 RVA: 0x00061FF3 File Offset: 0x000601F3
		[DefaultValue("")]
		public string Title
		{
			get
			{
				return this.Text;
			}
			set
			{
				this.Text = value;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001744 RID: 5956 RVA: 0x00061FFC File Offset: 0x000601FC
		// (set) Token: 0x06001745 RID: 5957 RVA: 0x00062009 File Offset: 0x00060209
		[DefaultValue(0)]
		public DisplayFormatMode DisplayFormatMode
		{
			get
			{
				return this.textBoxContent.FormatMode;
			}
			set
			{
				this.textBoxContent.FormatMode = value;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x00062017 File Offset: 0x00060217
		// (set) Token: 0x06001747 RID: 5959 RVA: 0x0006201F File Offset: 0x0006021F
		[DefaultValue(MessageBoxButtons.YesNo)]
		public MessageBoxButtons Buttons
		{
			get
			{
				return this.buttons;
			}
			set
			{
				if (value != MessageBoxButtons.YesNo && value != MessageBoxButtons.OK && value != MessageBoxButtons.OKCancel)
				{
					throw new ArgumentOutOfRangeException("Buttons");
				}
				if (this.buttons != value)
				{
					this.buttons = value;
					this.SetButtons();
				}
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x0006204D File Offset: 0x0006024D
		// (set) Token: 0x06001749 RID: 5961 RVA: 0x00062055 File Offset: 0x00060255
		[DefaultValue(MessageBoxDefaultButton.Button1)]
		public MessageBoxDefaultButton DefaultButton
		{
			get
			{
				return this.defaultButton;
			}
			set
			{
				if (value != MessageBoxDefaultButton.Button1 && value != MessageBoxDefaultButton.Button2)
				{
					throw new ArgumentOutOfRangeException("DefaultButton");
				}
				this.defaultButton = value;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x0600174A RID: 5962 RVA: 0x00062074 File Offset: 0x00060274
		// (set) Token: 0x0600174B RID: 5963 RVA: 0x0006207C File Offset: 0x0006027C
		[DefaultValue(MessageBoxIcon.None)]
		public MessageBoxIcon MessageIcon
		{
			get
			{
				return this.messageIcon;
			}
			set
			{
				if (value != this.messageIcon)
				{
					Icon icon = null;
					if (value <= MessageBoxIcon.Question)
					{
						if (value != MessageBoxIcon.Hand)
						{
							if (value == MessageBoxIcon.Question)
							{
								icon = Icons.Help;
							}
						}
						else
						{
							icon = Icons.Error;
						}
					}
					else if (value != MessageBoxIcon.Exclamation)
					{
						if (value == MessageBoxIcon.Asterisk)
						{
							icon = Icons.Information;
						}
					}
					else
					{
						icon = Icons.Warning;
					}
					this.pictureBoxIcon.Image = ((icon == null) ? null : icon.ToBitmap());
					this.panelIcon.Visible = (null != icon);
					this.pictureBoxIcon.Visible = (null != icon);
				}
			}
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x0006210C File Offset: 0x0006030C
		private void SetButtons()
		{
			switch (this.Buttons)
			{
			case MessageBoxButtons.OK:
			case MessageBoxButtons.OKCancel:
				base.OkButtonText = Strings.Ok;
				base.CancelButtonText = Strings.Cancel;
				base.CancelVisible = (this.Buttons == MessageBoxButtons.OKCancel);
				base.AcceptButton.DialogResult = DialogResult.OK;
				base.CancelButton.DialogResult = DialogResult.Cancel;
				return;
			case MessageBoxButtons.YesNo:
				base.OkButtonText = Strings.Yes;
				base.CancelButtonText = Strings.No;
				base.CancelVisible = true;
				base.AcceptButton.DialogResult = DialogResult.Yes;
				base.CancelButton.DialogResult = DialogResult.No;
				return;
			}
			throw new NotSupportedException();
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x0600174D RID: 5965 RVA: 0x000621CC File Offset: 0x000603CC
		protected override string DefaultHelpTopic
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x000621D3 File Offset: 0x000603D3
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			base.HelpVisible = (base.HelpVisible && !string.IsNullOrEmpty(base.HelpTopic));
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x000621FC File Offset: 0x000603FC
		protected override void OnHelpRequested(HelpEventArgs hevent)
		{
			if (!hevent.Handled && base.HelpVisible)
			{
				if (base.HelpTopic == this.DefaultHelpTopic && this.DataSource != null)
				{
					base.HelpTopic = string.Format("{0}.{1}.{2}", base.GetType().FullName, this.DataSource.GetType().Name, this.ValueMember);
				}
				ExchangeHelpService.ShowHelpFromHelpTopicId(this, base.HelpTopic);
				hevent.Handled = true;
			}
			base.OnHelpRequested(hevent);
		}

		// Token: 0x040008A9 RID: 2217
		private bool canInputMessage;

		// Token: 0x040008AA RID: 2218
		private int textMaxLength;

		// Token: 0x040008AB RID: 2219
		private bool allowEmpty;

		// Token: 0x040008AC RID: 2220
		private string valueMember = string.Empty;

		// Token: 0x040008AD RID: 2221
		private static readonly object EventParse = new object();

		// Token: 0x040008AE RID: 2222
		private MessageBoxButtons buttons = MessageBoxButtons.YesNo;

		// Token: 0x040008AF RID: 2223
		private MessageBoxDefaultButton defaultButton;

		// Token: 0x040008B2 RID: 2226
		private MessageBoxIcon messageIcon;

		// Token: 0x040008B9 RID: 2233
		private AutoSizePanel textBoxContentPanel;
	}
}
