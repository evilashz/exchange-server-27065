using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200014E RID: 334
	public class PropertyPageCommandExposureControl : ExchangePropertyPageControl
	{
		// Token: 0x06000D90 RID: 3472 RVA: 0x000334E7 File Offset: 0x000316E7
		public PropertyPageCommandExposureControl()
		{
			this.InitializeComponent();
			this.Text = Strings.PropertyPageLoggingDialogTitle;
			this.exposedCommandLabel.Text = Strings.PropertyPageLoggingDialogText;
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000D91 RID: 3473 RVA: 0x0003351A File Offset: 0x0003171A
		// (set) Token: 0x06000D92 RID: 3474 RVA: 0x00033527 File Offset: 0x00031727
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string CommandToShow
		{
			get
			{
				return this.exposedCommandTextBox.Text;
			}
			set
			{
				this.exposedCommandTextBox.Text = value;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x00033535 File Offset: 0x00031735
		// (set) Token: 0x06000D94 RID: 3476 RVA: 0x0003353D File Offset: 0x0003173D
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x00033546 File Offset: 0x00031746
		private bool ShouldSerializeText()
		{
			return this.Text != Strings.PropertyPageLoggingDialogTitle;
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x0003355D File Offset: 0x0003175D
		// (set) Token: 0x06000D97 RID: 3479 RVA: 0x00033565 File Offset: 0x00031765
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

		// Token: 0x06000D98 RID: 3480 RVA: 0x0003356E File Offset: 0x0003176E
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x00033590 File Offset: 0x00031790
		private void InitializeComponent()
		{
			this.tableLayoutPanel1 = new AutoTableLayoutPanel();
			this.exposedCommandLabel = new Label();
			this.outputPanel = new AutoSizePanel();
			this.exposedCommandTextBox = new TextBox();
			((ISupportInitialize)base.BindingSource).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.outputPanel.SuspendLayout();
			base.SuspendLayout();
			this.tableLayoutPanel1.AutoLayout = true;
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel1.ContainerType = ContainerType.PropertyPage;
			this.tableLayoutPanel1.Controls.Add(this.exposedCommandLabel, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.outputPanel, 0, 1);
			this.tableLayoutPanel1.Dock = DockStyle.Top;
			this.tableLayoutPanel1.Location = new Point(0, 0);
			this.tableLayoutPanel1.Margin = new Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new Padding(13, 12, 16, 12);
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel1.Size = new Size(437, 240);
			this.tableLayoutPanel1.TabIndex = 0;
			this.exposedCommandLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.exposedCommandLabel.AutoSize = true;
			this.exposedCommandLabel.Location = new Point(13, 12);
			this.exposedCommandLabel.Margin = new Padding(0);
			this.exposedCommandLabel.Name = "exposedCommandLabel";
			this.exposedCommandLabel.Size = new Size(408, 13);
			this.exposedCommandLabel.TabIndex = 0;
			this.exposedCommandLabel.Text = "exposedCommandLabel";
			this.outputPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.outputPanel.BackColor = SystemColors.Window;
			this.outputPanel.Controls.Add(this.exposedCommandTextBox);
			this.outputPanel.Location = new Point(16, 28);
			this.outputPanel.Margin = new Padding(3, 3, 0, 0);
			this.outputPanel.Name = "outputPanel";
			this.outputPanel.Size = new Size(405, 200);
			this.outputPanel.TabIndex = 1;
			this.exposedCommandTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.exposedCommandTextBox.BackColor = SystemColors.Window;
			this.exposedCommandTextBox.Location = new Point(0, 0);
			this.exposedCommandTextBox.Margin = new Padding(0);
			this.exposedCommandTextBox.Multiline = true;
			this.exposedCommandTextBox.Name = "exposedCommandTextBox";
			this.exposedCommandTextBox.ReadOnly = true;
			this.exposedCommandTextBox.ScrollBars = ScrollBars.Vertical;
			this.exposedCommandTextBox.Size = new Size(405, 200);
			this.exposedCommandTextBox.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.AutoSize = true;
			base.Controls.Add(this.tableLayoutPanel1);
			base.Name = "PropertyPageCommandExposureControl";
			base.Size = new Size(437, 255);
			this.Text = "PropertyPageCommandExposureControl";
			((ISupportInitialize)base.BindingSource).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.outputPanel.ResumeLayout(false);
			this.outputPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400056B RID: 1387
		private IContainer components;

		// Token: 0x0400056C RID: 1388
		private AutoTableLayoutPanel tableLayoutPanel1;

		// Token: 0x0400056D RID: 1389
		private Label exposedCommandLabel;

		// Token: 0x0400056E RID: 1390
		private TextBox exposedCommandTextBox;

		// Token: 0x0400056F RID: 1391
		private AutoSizePanel outputPanel;
	}
}
