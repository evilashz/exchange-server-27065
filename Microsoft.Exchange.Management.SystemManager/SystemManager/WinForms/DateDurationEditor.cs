using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200012D RID: 301
	public class DateDurationEditor : StrongTypeEditor<DateDuration>
	{
		// Token: 0x06000BF4 RID: 3060 RVA: 0x0002AEF0 File Offset: 0x000290F0
		public DateDurationEditor()
		{
			this.InitializeComponent();
			this.startDateTimePicker.TimeVisible = false;
			this.endDateTimePicker.TimeVisible = false;
			base.BindingSource.DataSource = typeof(DateDuration);
			this.startDateTimePicker.TitleText = Strings.StartDateLabel;
			this.startDateTimePicker.DataBindings.Add("Value", base.BindingSource, "StartDate", true, DataSourceUpdateMode.OnPropertyChanged);
			this.endDateTimePicker.TitleText = Strings.EndDateLabel;
			this.endDateTimePicker.DataBindings.Add("Value", base.BindingSource, "EndDate", true, DataSourceUpdateMode.OnPropertyChanged);
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0002AFA6 File Offset: 0x000291A6
		// (set) Token: 0x06000BF6 RID: 3062 RVA: 0x0002AFB3 File Offset: 0x000291B3
		[DefaultValue("")]
		public string ContentLabelText
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

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0002AFC1 File Offset: 0x000291C1
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x0002AFC9 File Offset: 0x000291C9
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

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0002AFD2 File Offset: 0x000291D2
		public override Size GetPreferredSize(Size proposedSize)
		{
			proposedSize.Width = base.Width;
			return this.tableLayoutPanel.GetPreferredSize(proposedSize);
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0002AFF0 File Offset: 0x000291F0
		private void InitializeComponent()
		{
			this.tableLayoutPanel = new AutoTableLayoutPanel();
			this.startDateTimePicker = new CustomDateTimePicker(new DateTime?(DateTime.UtcNow - new TimeSpan(1, 0, 0, 0, 0)));
			this.endDateTimePicker = new CustomDateTimePicker();
			this.contentLabel = new Label();
			this.tableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			this.tableLayoutPanel.AutoLayout = true;
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.ColumnCount = 1;
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel.Controls.Add(this.startDateTimePicker, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.endDateTimePicker, 0, 2);
			this.tableLayoutPanel.Controls.Add(this.contentLabel, 0, 0);
			this.tableLayoutPanel.Dock = DockStyle.Top;
			this.tableLayoutPanel.Location = new Point(0, 0);
			this.tableLayoutPanel.Margin = new Padding(0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.Padding = new Padding(0, 0, 16, 0);
			this.tableLayoutPanel.RowCount = 3;
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.Size = new Size(531, 142);
			this.tableLayoutPanel.TabIndex = 0;
			this.startDateTimePicker.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.startDateTimePicker.Location = new Point(13, 28);
			this.startDateTimePicker.Margin = new Padding(0, 3, 0, 0);
			this.startDateTimePicker.Name = "startDateTimePicker";
			this.startDateTimePicker.Size = new Size(502, 45);
			this.startDateTimePicker.TabIndex = 0;
			this.startDateTimePicker.TimeVisible = true;
			this.startDateTimePicker.TitleText = "startDateTimePicker";
			this.endDateTimePicker.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.endDateTimePicker.Location = new Point(13, 85);
			this.endDateTimePicker.Margin = new Padding(0, 12, 0, 0);
			this.endDateTimePicker.Name = "endDateTimePicker";
			this.endDateTimePicker.Size = new Size(502, 45);
			this.endDateTimePicker.TabIndex = 1;
			this.endDateTimePicker.TimeVisible = true;
			this.endDateTimePicker.TitleText = "endDateTimePicker";
			this.contentLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.contentLabel.AutoSize = true;
			this.contentLabel.Location = new Point(13, 12);
			this.contentLabel.Margin = new Padding(0);
			this.contentLabel.Name = "contentLabel";
			this.contentLabel.Size = new Size(502, 13);
			this.contentLabel.TabIndex = 2;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			this.AutoSize = true;
			base.ClientSize = new Size(531, 196);
			base.Controls.Add(this.tableLayoutPanel);
			base.Name = "DateDurationEditor";
			base.Controls.SetChildIndex(this.tableLayoutPanel, 0);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040004DF RID: 1247
		private CustomDateTimePicker startDateTimePicker;

		// Token: 0x040004E0 RID: 1248
		private CustomDateTimePicker endDateTimePicker;

		// Token: 0x040004E1 RID: 1249
		private Label contentLabel;

		// Token: 0x040004E2 RID: 1250
		private AutoTableLayoutPanel tableLayoutPanel;
	}
}
