using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001CF RID: 463
	public class CustomDateTimePicker : AutoSizePanel
	{
		// Token: 0x0600139E RID: 5022 RVA: 0x0004FCF4 File Offset: 0x0004DEF4
		public CustomDateTimePicker() : this(null)
		{
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0004FD10 File Offset: 0x0004DF10
		public CustomDateTimePicker(DateTime? defaultDateTime)
		{
			this.InitializeComponent();
			this.exchangeDateTimePicker.Enabled = false;
			if (defaultDateTime != null)
			{
				this.exchangeDateTimePicker.Value = defaultDateTime.Value;
			}
			this.exchangeDateTimePicker.ValueChanged += this.dateTimePicker_ValueChanged;
			this.titleCheckBox.CheckedChanged += this.titleCheckBox_CheckedChanged;
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x0004FD7E File Offset: 0x0004DF7E
		// (set) Token: 0x060013A1 RID: 5025 RVA: 0x0004FD8B File Offset: 0x0004DF8B
		[DefaultValue(true)]
		public bool TimeVisible
		{
			get
			{
				return this.exchangeDateTimePicker.TimeVisible;
			}
			set
			{
				this.exchangeDateTimePicker.TimeVisible = value;
			}
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0004FD9C File Offset: 0x0004DF9C
		private void InitializeComponent()
		{
			this.tableLayoutPanel = new AutoTableLayoutPanel();
			this.titleCheckBox = new AutoHeightCheckBox();
			this.exchangeDateTimePicker = new ExchangeDateTimePicker();
			this.tableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			this.tableLayoutPanel.AutoLayout = true;
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 16f));
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel.Controls.Add(this.titleCheckBox, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.exchangeDateTimePicker, 1, 1);
			this.tableLayoutPanel.Dock = DockStyle.Top;
			this.tableLayoutPanel.Location = new Point(0, 0);
			this.tableLayoutPanel.Margin = new Padding(0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 2;
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.Size = new Size(324, 45);
			this.tableLayoutPanel.TabIndex = 0;
			this.titleCheckBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.titleCheckBox.AutoSize = true;
			this.tableLayoutPanel.SetColumnSpan(this.titleCheckBox, 2);
			this.titleCheckBox.Location = new Point(0, 0);
			this.titleCheckBox.Margin = new Padding(3, 0, 0, 0);
			this.titleCheckBox.Name = "titleCheckBox";
			this.titleCheckBox.Size = new Size(324, 17);
			this.titleCheckBox.TabIndex = 1;
			this.titleCheckBox.UseVisualStyleBackColor = true;
			this.exchangeDateTimePicker.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.exchangeDateTimePicker.Location = new Point(16, 25);
			this.exchangeDateTimePicker.Margin = new Padding(0, 8, 0, 0);
			this.exchangeDateTimePicker.Name = "exchangeDateTimePicker";
			this.exchangeDateTimePicker.Size = new Size(308, 20);
			this.exchangeDateTimePicker.TabIndex = 2;
			this.exchangeDateTimePicker.AutoSize = true;
			this.exchangeDateTimePicker.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			base.Controls.Add(this.tableLayoutPanel);
			base.Name = "CustomDateTimePicker";
			base.Size = new Size(324, 45);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x00050070 File Offset: 0x0004E270
		private void titleCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			bool @checked = (sender as CheckBox).Checked;
			this.exchangeDateTimePicker.Enabled = @checked;
			this.OnCheckedChanged(EventArgs.Empty);
			this.OnValueChanged(EventArgs.Empty);
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x000500AB File Offset: 0x0004E2AB
		private void dateTimePicker_ValueChanged(object sender, EventArgs e)
		{
			this.OnValueChanged(EventArgs.Empty);
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x000500B8 File Offset: 0x0004E2B8
		// (set) Token: 0x060013A6 RID: 5030 RVA: 0x000500C5 File Offset: 0x0004E2C5
		[DefaultValue(false)]
		public bool Checked
		{
			get
			{
				return this.titleCheckBox.Checked;
			}
			set
			{
				this.titleCheckBox.Checked = value;
			}
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x000500D4 File Offset: 0x0004E2D4
		protected virtual void OnCheckedChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[CustomDateTimePicker.EventCheckedChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400007B RID: 123
		// (add) Token: 0x060013A8 RID: 5032 RVA: 0x00050102 File Offset: 0x0004E302
		// (remove) Token: 0x060013A9 RID: 5033 RVA: 0x00050115 File Offset: 0x0004E315
		public event EventHandler CheckedChanged
		{
			add
			{
				base.Events.AddHandler(CustomDateTimePicker.EventCheckedChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(CustomDateTimePicker.EventCheckedChanged, value);
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x00050128 File Offset: 0x0004E328
		// (set) Token: 0x060013AB RID: 5035 RVA: 0x00050135 File Offset: 0x0004E335
		[DefaultValue("")]
		public string TitleText
		{
			get
			{
				return this.titleCheckBox.Text;
			}
			set
			{
				this.titleCheckBox.Text = value;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x00050144 File Offset: 0x0004E344
		// (set) Token: 0x060013AD RID: 5037 RVA: 0x00050178 File Offset: 0x0004E378
		[DefaultValue(null)]
		public DateTime? Value
		{
			get
			{
				if (!this.titleCheckBox.Checked)
				{
					return null;
				}
				return new DateTime?(this.exchangeDateTimePicker.Value);
			}
			set
			{
				if (value != this.Value)
				{
					if (value != null)
					{
						this.exchangeDateTimePicker.Value = value.Value;
						this.Checked = true;
					}
					else
					{
						this.Checked = false;
					}
					this.OnValueChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x000501F8 File Offset: 0x0004E3F8
		protected virtual void OnValueChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[CustomDateTimePicker.EventValueChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400007C RID: 124
		// (add) Token: 0x060013AF RID: 5039 RVA: 0x00050226 File Offset: 0x0004E426
		// (remove) Token: 0x060013B0 RID: 5040 RVA: 0x00050239 File Offset: 0x0004E439
		public event EventHandler ValueChanged
		{
			add
			{
				base.Events.AddHandler(CustomDateTimePicker.EventValueChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(CustomDateTimePicker.EventValueChanged, value);
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x0005024C File Offset: 0x0004E44C
		protected override string ExposedPropertyName
		{
			get
			{
				return "Value";
			}
		}

		// Token: 0x04000743 RID: 1859
		private AutoHeightCheckBox titleCheckBox;

		// Token: 0x04000744 RID: 1860
		private ExchangeDateTimePicker exchangeDateTimePicker;

		// Token: 0x04000745 RID: 1861
		private AutoTableLayoutPanel tableLayoutPanel;

		// Token: 0x04000746 RID: 1862
		private static readonly object EventCheckedChanged = new object();

		// Token: 0x04000747 RID: 1863
		private static readonly object EventValueChanged = new object();
	}
}
