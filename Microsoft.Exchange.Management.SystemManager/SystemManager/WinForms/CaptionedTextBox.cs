using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001B2 RID: 434
	public class CaptionedTextBox : ExchangeUserControl, IFormatModeProvider, IBindableComponent, IComponent, IDisposable
	{
		// Token: 0x06001180 RID: 4480 RVA: 0x000454BC File Offset: 0x000436BC
		public CaptionedTextBox()
		{
			this.InitializeComponent();
			this.exchangeTextBox.TextChanged += delegate(object param0, EventArgs param1)
			{
				this.OnTextChanged(EventArgs.Empty);
			};
			this.exchangeTextBox.FormatModeChanged += delegate(object param0, EventArgs param1)
			{
				this.OnFormatModeChanged(EventArgs.Empty);
			};
			new TextBoxConstraintProvider(this, this.exchangeTextBox);
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001181 RID: 4481 RVA: 0x0004553F File Offset: 0x0004373F
		// (set) Token: 0x06001182 RID: 4482 RVA: 0x0004554C File Offset: 0x0004374C
		[DefaultValue(0)]
		public DisplayFormatMode FormatMode
		{
			get
			{
				return this.exchangeTextBox.FormatMode;
			}
			set
			{
				this.exchangeTextBox.FormatMode = value;
			}
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0004555C File Offset: 0x0004375C
		protected virtual void OnFormatModeChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[CaptionedTextBox.EventFormatModeChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400006E RID: 110
		// (add) Token: 0x06001184 RID: 4484 RVA: 0x0004558A File Offset: 0x0004378A
		// (remove) Token: 0x06001185 RID: 4485 RVA: 0x0004559D File Offset: 0x0004379D
		public event EventHandler FormatModeChanged
		{
			add
			{
				base.Events.AddHandler(CaptionedTextBox.EventFormatModeChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(CaptionedTextBox.EventFormatModeChanged, value);
			}
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x000455B0 File Offset: 0x000437B0
		private void InitializeComponent()
		{
			this.exchangeTextBox = new ExchangeTextBox();
			this.tableLayoutPanel = new TableLayoutPanel();
			this.tableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			this.exchangeTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.exchangeTextBox.Location = new Point(8, 0);
			this.exchangeTextBox.Margin = new Padding(8, 0, 0, 0);
			this.exchangeTextBox.Name = "exchangeTextBox";
			this.exchangeTextBox.Size = new Size(75, 20);
			this.exchangeTextBox.TabIndex = 1;
			this.tableLayoutPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
			this.tableLayoutPanel.Controls.Add(this.exchangeTextBox, 1, 0);
			this.tableLayoutPanel.Location = new Point(0, 0);
			this.tableLayoutPanel.Margin = new Padding(0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 1;
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.Size = new Size(83, 20);
			this.tableLayoutPanel.TabIndex = 0;
			base.Controls.Add(this.tableLayoutPanel);
			base.Name = "CaptionedTextBox";
			base.Size = new Size(83, 20);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x00045786 File Offset: 0x00043986
		protected override Padding DefaultMargin
		{
			get
			{
				return new Padding(0);
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x0004578E File Offset: 0x0004398E
		protected override Size DefaultSize
		{
			get
			{
				return new Size(366, 20);
			}
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x0004579C File Offset: 0x0004399C
		public override Size GetPreferredSize(Size proposedSize)
		{
			if (this.tableLayoutPanel.Width > proposedSize.Width)
			{
				proposedSize.Width = this.tableLayoutPanel.Width;
			}
			if (proposedSize != this.lastProposedSize || this.lastTLPSize != this.tableLayoutPanel.Size)
			{
				this.preferredSizeCache = this.tableLayoutPanel.GetPreferredSize(proposedSize);
				this.lastProposedSize = proposedSize;
				this.lastTLPSize = this.tableLayoutPanel.Size;
				this.preferredSizeCache.Width = this.preferredSizeCache.Width + base.Padding.Horizontal;
				this.preferredSizeCache.Height = this.preferredSizeCache.Height + base.Padding.Vertical;
			}
			return this.preferredSizeCache;
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x00045865 File Offset: 0x00043A65
		// (set) Token: 0x0600118B RID: 4491 RVA: 0x00045872 File Offset: 0x00043A72
		public override string Text
		{
			get
			{
				return this.exchangeTextBox.Text;
			}
			set
			{
				this.exchangeTextBox.Text = value;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x00045880 File Offset: 0x00043A80
		// (set) Token: 0x0600118D RID: 4493 RVA: 0x0004588D File Offset: 0x00043A8D
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public HorizontalAlignment TextAlign
		{
			get
			{
				return this.exchangeTextBox.TextAlign;
			}
			set
			{
				this.exchangeTextBox.TextAlign = value;
			}
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0004589B File Offset: 0x00043A9B
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeTextAlign()
		{
			return this.TextAlign != HorizontalAlignment.Left;
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x000458A9 File Offset: 0x00043AA9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void ResetTextAlign()
		{
			this.TextAlign = HorizontalAlignment.Left;
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001190 RID: 4496 RVA: 0x000458B2 File Offset: 0x00043AB2
		// (set) Token: 0x06001191 RID: 4497 RVA: 0x000458BF File Offset: 0x00043ABF
		[DefaultValue(75)]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public int TextBoxWidth
		{
			get
			{
				return this.exchangeTextBox.Width;
			}
			set
			{
				this.exchangeTextBox.Width = value;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001192 RID: 4498 RVA: 0x000458CD File Offset: 0x00043ACD
		protected override string ExposedPropertyName
		{
			get
			{
				return "Text";
			}
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x000458E0 File Offset: 0x00043AE0
		void IFormatModeProvider.add_BindingContextChanged(EventHandler A_1)
		{
			base.BindingContextChanged += A_1;
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x000458E9 File Offset: 0x00043AE9
		void IFormatModeProvider.remove_BindingContextChanged(EventHandler A_1)
		{
			base.BindingContextChanged -= A_1;
		}

		// Token: 0x040006B6 RID: 1718
		private Size lastProposedSize = Size.Empty;

		// Token: 0x040006B7 RID: 1719
		private Size lastTLPSize = Size.Empty;

		// Token: 0x040006B8 RID: 1720
		private Size preferredSizeCache = Size.Empty;

		// Token: 0x040006B9 RID: 1721
		private static readonly object EventFormatModeChanged = new object();

		// Token: 0x040006BA RID: 1722
		protected ExchangeTextBox exchangeTextBox;

		// Token: 0x040006BB RID: 1723
		protected TableLayoutPanel tableLayoutPanel;
	}
}
