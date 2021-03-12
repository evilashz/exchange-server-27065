using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000226 RID: 550
	public class NumericUpDownPage : ExchangePropertyPageControl
	{
		// Token: 0x0600193B RID: 6459 RVA: 0x0006DD9B File Offset: 0x0006BF9B
		public NumericUpDownPage()
		{
			this.InitializeComponent();
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x0600193D RID: 6461 RVA: 0x0006DDB7 File Offset: 0x0006BFB7
		// (set) Token: 0x0600193C RID: 6460 RVA: 0x0006DDA9 File Offset: 0x0006BFA9
		public string LabelText
		{
			get
			{
				return this.numericUpDownLabel.Text;
			}
			set
			{
				this.numericUpDownLabel.Text = value;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x0600193E RID: 6462 RVA: 0x0006DDC4 File Offset: 0x0006BFC4
		// (set) Token: 0x0600193F RID: 6463 RVA: 0x0006DDD6 File Offset: 0x0006BFD6
		public int Minimum
		{
			get
			{
				return (int)this.numericUpDown.Minimum;
			}
			set
			{
				this.numericUpDown.Minimum = value;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001940 RID: 6464 RVA: 0x0006DDE9 File Offset: 0x0006BFE9
		// (set) Token: 0x06001941 RID: 6465 RVA: 0x0006DDFB File Offset: 0x0006BFFB
		public int Maximum
		{
			get
			{
				return (int)this.numericUpDown.Maximum;
			}
			set
			{
				this.numericUpDown.Maximum = value;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001942 RID: 6466 RVA: 0x0006DE0E File Offset: 0x0006C00E
		// (set) Token: 0x06001943 RID: 6467 RVA: 0x0006DE20 File Offset: 0x0006C020
		public int Value
		{
			get
			{
				return (int)this.numericUpDown.Value;
			}
			set
			{
				this.numericUpDown.Value = value;
			}
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x0006DE33 File Offset: 0x0006C033
		public override Size GetPreferredSize(Size proposedSize)
		{
			proposedSize.Width = base.Width;
			return this.mainTableLayoutPanel.GetPreferredSize(proposedSize);
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x0006DE50 File Offset: 0x0006C050
		private void InitializeComponent()
		{
			this.mainTableLayoutPanel = new AutoTableLayoutPanel();
			this.numericUpDownLabel = new Label();
			this.numericUpDown = new ExchangeNumericUpDown();
			((ISupportInitialize)base.BindingSource).BeginInit();
			this.mainTableLayoutPanel.SuspendLayout();
			((ISupportInitialize)this.numericUpDown).BeginInit();
			base.SuspendLayout();
			this.mainTableLayoutPanel.AutoLayout = true;
			this.mainTableLayoutPanel.AutoSize = true;
			this.mainTableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.mainTableLayoutPanel.ColumnCount = 2;
			this.mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75f));
			this.mainTableLayoutPanel.ContainerType = ContainerType.PropertyPage;
			this.mainTableLayoutPanel.Controls.Add(this.numericUpDownLabel, 0, 0);
			this.mainTableLayoutPanel.Controls.Add(this.numericUpDown, 1, 0);
			this.mainTableLayoutPanel.Dock = DockStyle.Top;
			this.mainTableLayoutPanel.Location = new Point(0, 0);
			this.mainTableLayoutPanel.Margin = new Padding(0);
			this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
			this.mainTableLayoutPanel.Padding = new Padding(13, 12, 16, 12);
			this.mainTableLayoutPanel.RowCount = 1;
			this.mainTableLayoutPanel.RowStyles.Add(new RowStyle());
			this.mainTableLayoutPanel.Size = new Size(250, 44);
			this.mainTableLayoutPanel.TabIndex = 0;
			this.numericUpDownLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.numericUpDownLabel.AutoSize = true;
			this.numericUpDownLabel.Location = new Point(13, 15);
			this.numericUpDownLabel.Margin = new Padding(0, 3, 0, 4);
			this.numericUpDownLabel.Name = "numericUpDownLabel";
			this.numericUpDownLabel.Size = new Size(146, 13);
			this.numericUpDownLabel.TabIndex = 0;
			this.numericUpDownLabel.Text = "numericUpDownLabel";
			this.numericUpDown.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.numericUpDown.Location = new Point(162, 12);
			this.numericUpDown.Margin = new Padding(3, 0, 0, 0);
			this.numericUpDown.Name = "numericUpDown";
			this.numericUpDown.Size = new Size(72, 20);
			this.numericUpDown.TabIndex = 1;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.AutoSize = true;
			base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			base.Controls.Add(this.mainTableLayoutPanel);
			this.MinimumSize = new Size(250, 0);
			base.Name = "NumericUpDownPage";
			((ISupportInitialize)base.BindingSource).EndInit();
			this.mainTableLayoutPanel.ResumeLayout(false);
			this.mainTableLayoutPanel.PerformLayout();
			((ISupportInitialize)this.numericUpDown).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000979 RID: 2425
		private AutoTableLayoutPanel mainTableLayoutPanel;

		// Token: 0x0400097A RID: 2426
		private Label numericUpDownLabel;

		// Token: 0x0400097B RID: 2427
		private ExchangeNumericUpDown numericUpDown;
	}
}
