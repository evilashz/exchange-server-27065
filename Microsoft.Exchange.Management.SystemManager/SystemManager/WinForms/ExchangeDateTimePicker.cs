using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001DD RID: 477
	public class ExchangeDateTimePicker : AutoSizePanel
	{
		// Token: 0x06001591 RID: 5521 RVA: 0x00058BB0 File Offset: 0x00056DB0
		public ExchangeDateTimePicker()
		{
			this.InitializeComponent();
			this.datePicker.ValueChanged += this.dateTimePicker_ValueChanged;
			this.timePicker.ValueChanged += this.dateTimePicker_ValueChanged;
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001592 RID: 5522 RVA: 0x00058BFE File Offset: 0x00056DFE
		// (set) Token: 0x06001593 RID: 5523 RVA: 0x00058C08 File Offset: 0x00056E08
		[DefaultValue(true)]
		public bool TimeVisible
		{
			get
			{
				return this.timeVisible;
			}
			set
			{
				if (this.TimeVisible != value)
				{
					if (this.TimeVisible)
					{
						this.timePicker.ValueChanged -= this.dateTimePicker_ValueChanged;
						this.tableLayoutPanel.Controls.Remove(this.timePicker);
						this.tableLayoutPanel.SetColumnSpan(this.datePicker, 3);
					}
					else
					{
						this.tableLayoutPanel.SetColumnSpan(this.datePicker, 1);
						this.tableLayoutPanel.Controls.Add(this.timePicker, 2, 0);
						this.timePicker.ValueChanged += this.dateTimePicker_ValueChanged;
					}
					this.timeVisible = value;
				}
			}
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x00058CB4 File Offset: 0x00056EB4
		private void InitializeComponent()
		{
			this.datePicker = new ExtendedDateTimePicker();
			this.timePicker = new ExtendedDateTimePicker();
			this.tableLayoutPanel = new AutoTableLayoutPanel();
			this.tableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			this.datePicker.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.datePicker.Location = new Point(3, 0);
			this.datePicker.Margin = new Padding(3, 0, 0, 0);
			this.datePicker.Name = "datePicker";
			this.datePicker.Size = new Size(222, 20);
			this.datePicker.TabIndex = 2;
			this.timePicker.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.timePicker.Format = DateTimePickerFormat.Time;
			this.timePicker.Location = new Point(236, 0);
			this.timePicker.Margin = new Padding(3, 0, 0, 0);
			this.timePicker.Name = "timePicker";
			this.timePicker.ShowUpDown = true;
			this.timePicker.Size = new Size(72, 20);
			this.timePicker.TabIndex = 3;
			this.tableLayoutPanel.AutoLayout = true;
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.ColumnCount = 3;
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 8f));
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3f));
			this.tableLayoutPanel.Controls.Add(this.datePicker, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.timePicker, 2, 0);
			this.tableLayoutPanel.Dock = DockStyle.Top;
			this.tableLayoutPanel.Location = new Point(0, 0);
			this.tableLayoutPanel.Margin = new Padding(0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 1;
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.Size = new Size(308, 20);
			this.tableLayoutPanel.TabIndex = 0;
			base.Controls.Add(this.tableLayoutPanel);
			base.Name = "ExchangeDateTimePicker";
			base.Size = new Size(308, 20);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x00058F62 File Offset: 0x00057162
		private void dateTimePicker_ValueChanged(object sender, EventArgs e)
		{
			this.OnValueChanged(EventArgs.Empty);
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x00058F70 File Offset: 0x00057170
		// (set) Token: 0x06001597 RID: 5527 RVA: 0x00059018 File Offset: 0x00057218
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DateTime Value
		{
			get
			{
				DateTime date = this.datePicker.Value.Date;
				if (this.TimeVisible)
				{
					date = new DateTime(this.datePicker.Value.Year, this.datePicker.Value.Month, this.datePicker.Value.Day, this.timePicker.Value.Hour, this.timePicker.Value.Minute, this.timePicker.Value.Second);
				}
				return date;
			}
			set
			{
				if (value != this.Value)
				{
					this.datePicker.Value = value.Date;
					if (this.TimeVisible)
					{
						this.timePicker.Value = new DateTime(this.timePicker.MinDate.Year, this.timePicker.MinDate.Month, this.timePicker.MinDate.Day, value.Hour, value.Minute, value.Second);
					}
					this.OnValueChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x000590BC File Offset: 0x000572BC
		protected virtual void OnValueChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ExchangeDateTimePicker.EventValueChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400008D RID: 141
		// (add) Token: 0x06001599 RID: 5529 RVA: 0x000590EA File Offset: 0x000572EA
		// (remove) Token: 0x0600159A RID: 5530 RVA: 0x000590FD File Offset: 0x000572FD
		public event EventHandler ValueChanged
		{
			add
			{
				base.Events.AddHandler(ExchangeDateTimePicker.EventValueChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ExchangeDateTimePicker.EventValueChanged, value);
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x00059110 File Offset: 0x00057310
		// (set) Token: 0x0600159C RID: 5532 RVA: 0x0005911D File Offset: 0x0005731D
		[DefaultValue(DateTimePickerFormat.Long)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		public DateTimePickerFormat DateFormat
		{
			get
			{
				return this.datePicker.Format;
			}
			set
			{
				this.datePicker.Format = value;
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x0005912B File Offset: 0x0005732B
		// (set) Token: 0x0600159E RID: 5534 RVA: 0x00059138 File Offset: 0x00057338
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(null)]
		[Browsable(true)]
		public string CustomDateFormat
		{
			get
			{
				return this.datePicker.CustomFormat;
			}
			set
			{
				this.datePicker.CustomFormat = value;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x00059146 File Offset: 0x00057346
		// (set) Token: 0x060015A0 RID: 5536 RVA: 0x00059153 File Offset: 0x00057353
		[Browsable(true)]
		[DefaultValue(DateTimePickerFormat.Time)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public DateTimePickerFormat TimeFormat
		{
			get
			{
				return this.timePicker.Format;
			}
			set
			{
				this.timePicker.Format = value;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x00059161 File Offset: 0x00057361
		// (set) Token: 0x060015A2 RID: 5538 RVA: 0x0005917C File Offset: 0x0005737C
		[DefaultValue(null)]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string CustomTimeFormat
		{
			get
			{
				if (!this.TimeVisible)
				{
					return string.Empty;
				}
				return this.timePicker.CustomFormat;
			}
			set
			{
				this.timePicker.CustomFormat = value;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060015A3 RID: 5539 RVA: 0x0005918A File Offset: 0x0005738A
		// (set) Token: 0x060015A4 RID: 5540 RVA: 0x00059197 File Offset: 0x00057397
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DateTime MinDate
		{
			get
			{
				return this.datePicker.MinDate;
			}
			set
			{
				this.datePicker.MinDate = value.Date;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x000591AB File Offset: 0x000573AB
		protected override string ExposedPropertyName
		{
			get
			{
				return "Value";
			}
		}

		// Token: 0x040007CD RID: 1997
		private ExtendedDateTimePicker datePicker;

		// Token: 0x040007CE RID: 1998
		private ExtendedDateTimePicker timePicker;

		// Token: 0x040007CF RID: 1999
		private AutoTableLayoutPanel tableLayoutPanel;

		// Token: 0x040007D0 RID: 2000
		private bool timeVisible = true;

		// Token: 0x040007D1 RID: 2001
		private static readonly object EventValueChanged = new object();
	}
}
