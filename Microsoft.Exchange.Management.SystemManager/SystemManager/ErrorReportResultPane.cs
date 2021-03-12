using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Management.SystemManager.WinForms;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200010A RID: 266
	public class ErrorReportResultPane : ContentResultPane
	{
		// Token: 0x06000981 RID: 2433 RVA: 0x000213BC File Offset: 0x0001F5BC
		public ErrorReportResultPane()
		{
			base.ViewModeCommands.Add(Theme.VisualEffectsCommands);
			base.EnableVisualEffects = true;
			this.InitializeComponent();
			this.LabelTitle = Strings.WelcomeToESM;
			this.UpdateTitleFont();
			this.titleImage.Image = IconLibrary.ToBitmap(Icons.Error, this.titleImage.Size);
			this.contentLabel.LinkClicked += this.contentLabel_LinkClicked;
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0002143C File Offset: 0x0001F63C
		private void contentLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			string text = e.Link.LinkData as string;
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					WinformsHelper.OpenUrl(new Uri(text));
				}
				catch (UrlHandlerNotFoundException ex)
				{
					base.ShowError(ex.Message);
				}
				catch (UriFormatException)
				{
				}
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x000214A0 File Offset: 0x0001F6A0
		// (set) Token: 0x06000984 RID: 2436 RVA: 0x000214AD File Offset: 0x0001F6AD
		public string ErrorMessage
		{
			get
			{
				return this.contentLabel.Text;
			}
			set
			{
				this.contentLabel.Text = value;
			}
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x000214BC File Offset: 0x0001F6BC
		private void UpdateContentLabelWithErrors()
		{
			string empty = string.Empty;
			this.contentLabel.Text = empty;
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x000214DB File Offset: 0x0001F6DB
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.UpdateTitleFont();
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x000214EC File Offset: 0x0001F6EC
		private void UpdateTitleFont()
		{
			if (this.labelTitle != null)
			{
				Font defaultFont = FontHelper.GetDefaultFont();
				this.labelTitle.Font = new Font(defaultFont.FontFamily, defaultFont.SizeInPoints + 2f, FontStyle.Bold);
			}
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0002152C File Offset: 0x0001F72C
		private void InitializeComponent()
		{
			this.tableLayoutPanel1 = new TableLayoutPanel();
			this.labelTitle = new Label();
			this.titleImage = new Label();
			this.contentTableLayoutPanel = new TableLayoutPanel();
			this.contentLabel = new LinkLabelCommand();
			this.tableLayoutPanel1.SuspendLayout();
			this.contentTableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.BackColor = Color.Transparent;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.Controls.Add(this.labelTitle, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.titleImage, 0, 0);
			this.tableLayoutPanel1.Dock = DockStyle.Top;
			this.tableLayoutPanel1.Location = new Point(12, 5);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new Padding(0, 0, 0, 12);
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel1.Size = new Size(126, 60);
			this.tableLayoutPanel1.TabIndex = 2;
			this.labelTitle.AutoSize = true;
			this.labelTitle.Dock = DockStyle.Fill;
			this.labelTitle.Location = new Point(57, 0);
			this.labelTitle.Name = "labelTitle";
			this.labelTitle.Size = new Size(66, 48);
			this.labelTitle.TabIndex = 1;
			this.labelTitle.TextAlign = ContentAlignment.MiddleLeft;
			this.titleImage.Location = new Point(3, 0);
			this.titleImage.Name = "titleImage";
			this.titleImage.Size = new Size(48, 48);
			this.titleImage.TabIndex = 2;
			this.contentTableLayoutPanel.AutoSize = true;
			this.contentTableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.contentTableLayoutPanel.BackColor = Color.Transparent;
			this.contentTableLayoutPanel.Dock = DockStyle.Top;
			this.contentTableLayoutPanel.ColumnCount = 1;
			this.contentTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.contentTableLayoutPanel.Controls.Add(this.contentLabel, 0, 0);
			this.contentTableLayoutPanel.Padding = new Padding(0, 0, 0, 0);
			this.contentTableLayoutPanel.Location = new Point(16, 91);
			this.contentTableLayoutPanel.Name = "contentTableLayoutPanel";
			this.contentTableLayoutPanel.RowCount = 1;
			this.contentTableLayoutPanel.RowStyles.Add(new RowStyle());
			this.contentTableLayoutPanel.Size = new Size(126, 50);
			this.contentTableLayoutPanel.TabIndex = 3;
			this.contentLabel.AutoSize = true;
			this.contentLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.contentLabel.Location = new Point(3, 0);
			this.contentLabel.Name = "contentLabel";
			this.contentLabel.Size = new Size(55, 13);
			this.contentLabel.TabIndex = 0;
			this.contentLabel.TabStop = true;
			this.contentLabel.Text = "linkLabel1";
			base.Controls.Add(this.contentTableLayoutPanel);
			base.Controls.Add(this.tableLayoutPanel1);
			base.Name = "ErrorReportResultPane";
			base.Padding = new Padding(12, 5, 12, 5);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.contentTableLayoutPanel.ResumeLayout(false);
			this.contentTableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x00021930 File Offset: 0x0001FB30
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x0002193D File Offset: 0x0001FB3D
		public string LabelTitle
		{
			get
			{
				return this.labelTitle.Text;
			}
			set
			{
				this.labelTitle.Text = value;
			}
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0002194B File Offset: 0x0001FB4B
		protected override string GetContent()
		{
			return this.ErrorMessage;
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x00021953 File Offset: 0x0001FB53
		public override string SelectionHelpTopic
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400041D RID: 1053
		private TableLayoutPanel tableLayoutPanel1;

		// Token: 0x0400041E RID: 1054
		private Label titleImage;

		// Token: 0x0400041F RID: 1055
		private TableLayoutPanel contentTableLayoutPanel;

		// Token: 0x04000420 RID: 1056
		private LinkLabelCommand contentLabel;

		// Token: 0x04000421 RID: 1057
		private Label labelTitle;
	}
}
