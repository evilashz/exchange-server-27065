using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000221 RID: 545
	public class ScheduleControl : ExchangeUserControl
	{
		// Token: 0x060018EE RID: 6382 RVA: 0x0006BBBC File Offset: 0x00069DBC
		public ScheduleControl()
		{
			this.InitializeComponent();
			this.customizeButton.Text = Strings.ScheduleCustomizeButton;
			this.customizeButton.Click += delegate(object param0, EventArgs param1)
			{
				ScheduleEditorDialog scheduleEditorDialog = new ScheduleEditorDialog();
				scheduleEditorDialog.Schedule = this.Schedule;
				if (scheduleEditorDialog.ShowDialog(this) == DialogResult.OK)
				{
					this.SetCurrentSchedule(scheduleEditorDialog.Schedule);
				}
			};
			this.scheduleIntervalCombo.DataSource = this.cannedSchedules;
			this.scheduleIntervalCombo.ValueMember = "Schedule";
			this.scheduleIntervalCombo.DisplayMember = "Description";
			this.scheduleIntervalCombo.SelectedIndexChanged += delegate(object param0, EventArgs param1)
			{
				if (this.SetCurrentSchedule(this.scheduleIntervalCombo.SelectedIndex))
				{
					if (this.scheduleIntervalCombo.SelectedIndex != this.GetIndexOfUserCustomizedSchedule())
					{
						this.cannedSchedules[this.GetIndexOfUserCustomizedSchedule()].Schedule = this.Schedule;
						return;
					}
				}
				else
				{
					this.OnScheduleChanged(EventArgs.Empty);
				}
			};
			this.cannedSchedules.Add(new ScheduleControl.ScheduleWithDescription(this.Schedule, Strings.UseCustomSchedule));
			this.scheduleIntervalCombo.SelectedIndex = this.GetIndexOfUserCustomizedSchedule();
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x0006BCA0 File Offset: 0x00069EA0
		protected override UIValidationError[] GetValidationErrors()
		{
			if (this.IsCustomizeWithoutSelectSchedule())
			{
				return new UIValidationError[]
				{
					new UIValidationError(Strings.CustomizedWithoutSchedule, this.customizeButton)
				};
			}
			return base.GetValidationErrors();
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x0006BCD8 File Offset: 0x00069ED8
		internal bool IsCustomizeWithoutSelectSchedule()
		{
			int indexOfUserCustomizedSchedule = this.GetIndexOfUserCustomizedSchedule();
			int positionOfSchedueInCannedSchedulesExceptUserCustomizedSchedule = this.GetPositionOfSchedueInCannedSchedulesExceptUserCustomizedSchedule(this.cannedSchedules[indexOfUserCustomizedSchedule].Schedule);
			return this.scheduleIntervalCombo.SelectedIndex == indexOfUserCustomizedSchedule && positionOfSchedueInCannedSchedulesExceptUserCustomizedSchedule != -1;
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x0006BD1B File Offset: 0x00069F1B
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x0006BD3C File Offset: 0x00069F3C
		private void InitializeComponent()
		{
			this.components = new Container();
			this.customizeButton = new ExchangeButton();
			this.scheduleIntervalLabel = new Label();
			this.scheduleIntervalCombo = new ExchangeComboBox();
			this.tableLayoutPanel = new AutoTableLayoutPanel();
			this.tableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			this.customizeButton.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.customizeButton.AutoSize = true;
			this.customizeButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.customizeButton.Location = new Point(178, 16);
			this.customizeButton.Margin = new Padding(3, 3, 0, 0);
			this.customizeButton.Name = "customizeButton";
			this.customizeButton.Size = new Size(6, 23);
			this.customizeButton.TabIndex = 2;
			this.scheduleIntervalLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.scheduleIntervalLabel.AutoSize = true;
			this.tableLayoutPanel.SetColumnSpan(this.scheduleIntervalLabel, 3);
			this.scheduleIntervalLabel.Location = new Point(0, 0);
			this.scheduleIntervalLabel.Margin = new Padding(0);
			this.scheduleIntervalLabel.Name = "scheduleIntervalLabel";
			this.scheduleIntervalLabel.Size = new Size(184, 13);
			this.scheduleIntervalLabel.TabIndex = 0;
			this.scheduleIntervalCombo.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.scheduleIntervalCombo.DropDownStyle = ComboBoxStyle.DropDownList;
			this.scheduleIntervalCombo.FormattingEnabled = true;
			this.scheduleIntervalCombo.Location = new Point(3, 18);
			this.scheduleIntervalCombo.Margin = new Padding(3, 5, 0, 0);
			this.scheduleIntervalCombo.Name = "scheduleIntervalCombo";
			this.scheduleIntervalCombo.Size = new Size(164, 21);
			this.scheduleIntervalCombo.TabIndex = 1;
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.ColumnCount = 3;
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 8f));
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
			this.tableLayoutPanel.Controls.Add(this.customizeButton, 2, 1);
			this.tableLayoutPanel.Controls.Add(this.scheduleIntervalCombo, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.scheduleIntervalLabel, 0, 0);
			this.tableLayoutPanel.Dock = DockStyle.Top;
			this.tableLayoutPanel.AutoLayout = true;
			this.tableLayoutPanel.Location = new Point(0, 0);
			this.tableLayoutPanel.Margin = new Padding(0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.Padding = new Padding(0, 0, 16, 0);
			this.tableLayoutPanel.RowCount = 2;
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.Size = new Size(200, 39);
			this.tableLayoutPanel.TabIndex = 0;
			base.Controls.Add(this.tableLayoutPanel);
			base.Name = "ScheduleControl";
			base.Size = new Size(200, 39);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060018F3 RID: 6387 RVA: 0x0006C0D7 File Offset: 0x0006A2D7
		// (set) Token: 0x060018F4 RID: 6388 RVA: 0x0006C0E4 File Offset: 0x0006A2E4
		[DefaultValue(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		public bool CaptionVisible
		{
			get
			{
				return this.scheduleIntervalLabel.Visible;
			}
			set
			{
				this.scheduleIntervalLabel.Visible = value;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060018F5 RID: 6389 RVA: 0x0006C0F2 File Offset: 0x0006A2F2
		// (set) Token: 0x060018F6 RID: 6390 RVA: 0x0006C0FF File Offset: 0x0006A2FF
		[DefaultValue("")]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string Caption
		{
			get
			{
				return this.scheduleIntervalLabel.Text;
			}
			set
			{
				this.scheduleIntervalLabel.Text = value;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060018F7 RID: 6391 RVA: 0x0006C10D File Offset: 0x0006A30D
		// (set) Token: 0x060018F8 RID: 6392 RVA: 0x0006C11A File Offset: 0x0006A31A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ButtonText
		{
			get
			{
				return this.customizeButton.Text;
			}
			set
			{
				this.customizeButton.Text = value;
			}
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x0006C128 File Offset: 0x0006A328
		protected override Size DefaultSize
		{
			get
			{
				return new Size(400, 39);
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060018FA RID: 6394 RVA: 0x0006C136 File Offset: 0x0006A336
		// (set) Token: 0x060018FB RID: 6395 RVA: 0x0006C140 File Offset: 0x0006A340
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Schedule Schedule
		{
			get
			{
				return this.schedule;
			}
			set
			{
				Schedule currentSchedule = value ?? Schedule.Never;
				if (!this.Schedule.Equals(currentSchedule))
				{
					this.SetCurrentSchedule(currentSchedule);
				}
			}
		}

		// Token: 0x140000A7 RID: 167
		// (add) Token: 0x060018FC RID: 6396 RVA: 0x0006C16D File Offset: 0x0006A36D
		// (remove) Token: 0x060018FD RID: 6397 RVA: 0x0006C180 File Offset: 0x0006A380
		public event EventHandler ScheduleChanged
		{
			add
			{
				base.Events.AddHandler(ScheduleControl.ScheduleChangedObject, value);
			}
			remove
			{
				base.Events.RemoveHandler(ScheduleControl.ScheduleChangedObject, value);
			}
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x0006C194 File Offset: 0x0006A394
		protected virtual void OnScheduleChanged(EventArgs e)
		{
			base.UpdateError();
			EventHandler eventHandler = (EventHandler)base.Events[ScheduleControl.ScheduleChangedObject];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x0006C1C8 File Offset: 0x0006A3C8
		public static void ConvertScheduleToScheduleIntervalArray(ConvertEventArgs args)
		{
			ReadOnlyCollection<ScheduleInterval> readOnlyCollection = (args.Value == null) ? null : ((Schedule)args.Value).Intervals;
			ScheduleInterval[] array;
			if (readOnlyCollection == null)
			{
				array = new ScheduleInterval[0];
			}
			else
			{
				array = new ScheduleInterval[readOnlyCollection.Count];
				readOnlyCollection.CopyTo(array, 0);
			}
			args.Value = array;
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x0006C218 File Offset: 0x0006A418
		public static void ConvertScheduleIntervalArrayToSchedule(ConvertEventArgs args)
		{
			args.Value = ((args.Value == null) ? null : new Schedule((ScheduleInterval[])args.Value));
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x0006C23C File Offset: 0x0006A43C
		public void AddCannedSchedule(Schedule cannedSchedule, string scheduleDescription)
		{
			int positionOfSchedueInCannedSchedulesExceptUserCustomizedSchedule = this.GetPositionOfSchedueInCannedSchedulesExceptUserCustomizedSchedule(cannedSchedule);
			if (positionOfSchedueInCannedSchedulesExceptUserCustomizedSchedule == -1)
			{
				this.cannedSchedules.Insert(this.GetIndexOfInsertingNewCannedSchedule(), new ScheduleControl.ScheduleWithDescription(cannedSchedule, scheduleDescription));
				this.SetCurrentSchedule(this.Schedule);
			}
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x0006C27C File Offset: 0x0006A47C
		private int GetPositionOfSchedueInCannedSchedulesExceptUserCustomizedSchedule(Schedule targetSchedule)
		{
			int num = -1;
			for (int i = 0; i < this.cannedSchedules.Count; i++)
			{
				if (i != this.GetIndexOfUserCustomizedSchedule() && Schedule.Equals(this.cannedSchedules[i].Schedule, targetSchedule))
				{
					num = i;
				}
				if (num > 0)
				{
					break;
				}
			}
			return num;
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x0006C2CA File Offset: 0x0006A4CA
		private int GetIndexOfInsertingNewCannedSchedule()
		{
			return this.GetIndexOfUserCustomizedSchedule();
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0006C2D2 File Offset: 0x0006A4D2
		private int GetIndexOfUserCustomizedSchedule()
		{
			return this.cannedSchedules.Count - 1;
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x0006C2E1 File Offset: 0x0006A4E1
		public override Size GetPreferredSize(Size proposedSize)
		{
			return this.tableLayoutPanel.GetPreferredSize(proposedSize);
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x0006C2F0 File Offset: 0x0006A4F0
		private void SetCurrentSchedule(Schedule newSchedule)
		{
			int num = this.GetPositionOfSchedueInCannedSchedulesExceptUserCustomizedSchedule(newSchedule);
			if (num == -1)
			{
				this.cannedSchedules[this.GetIndexOfUserCustomizedSchedule()].Schedule = newSchedule;
				num = this.GetIndexOfUserCustomizedSchedule();
			}
			if (this.scheduleIntervalCombo.SelectedIndex != num)
			{
				this.scheduleIntervalCombo.SelectedIndex = num;
				return;
			}
			if (num == this.GetIndexOfUserCustomizedSchedule())
			{
				this.SetCurrentSchedule(num);
			}
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x0006C354 File Offset: 0x0006A554
		private bool SetCurrentSchedule(int newSelectedIndex)
		{
			bool result = false;
			Schedule secondSchedule = this.cannedSchedules[newSelectedIndex].Schedule;
			if (!Schedule.Equals(this.Schedule, secondSchedule))
			{
				this.schedule = secondSchedule;
				this.OnScheduleChanged(EventArgs.Empty);
				result = true;
			}
			return result;
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001908 RID: 6408 RVA: 0x0006C398 File Offset: 0x0006A598
		protected override string ExposedPropertyName
		{
			get
			{
				return "Schedule";
			}
		}

		// Token: 0x04000955 RID: 2389
		private BindingList<ScheduleControl.ScheduleWithDescription> cannedSchedules = new BindingList<ScheduleControl.ScheduleWithDescription>();

		// Token: 0x04000956 RID: 2390
		private IContainer components;

		// Token: 0x04000957 RID: 2391
		private ExchangeButton customizeButton;

		// Token: 0x04000958 RID: 2392
		private Label scheduleIntervalLabel;

		// Token: 0x04000959 RID: 2393
		private ExchangeComboBox scheduleIntervalCombo;

		// Token: 0x0400095A RID: 2394
		private AutoTableLayoutPanel tableLayoutPanel;

		// Token: 0x0400095B RID: 2395
		private Schedule schedule = Schedule.Never;

		// Token: 0x0400095C RID: 2396
		private static readonly object ScheduleChangedObject = new object();

		// Token: 0x02000222 RID: 546
		public class ScheduleWithDescription
		{
			// Token: 0x0600190C RID: 6412 RVA: 0x0006C3AB File Offset: 0x0006A5AB
			public ScheduleWithDescription()
			{
			}

			// Token: 0x0600190D RID: 6413 RVA: 0x0006C3B3 File Offset: 0x0006A5B3
			public ScheduleWithDescription(Schedule schedule, string description)
			{
				this.Schedule = schedule;
				this.Description = description;
			}

			// Token: 0x170005E6 RID: 1510
			// (get) Token: 0x0600190E RID: 6414 RVA: 0x0006C3C9 File Offset: 0x0006A5C9
			// (set) Token: 0x0600190F RID: 6415 RVA: 0x0006C3D1 File Offset: 0x0006A5D1
			public Schedule Schedule
			{
				get
				{
					return this.schedule;
				}
				internal set
				{
					this.schedule = value;
				}
			}

			// Token: 0x170005E7 RID: 1511
			// (get) Token: 0x06001910 RID: 6416 RVA: 0x0006C3DA File Offset: 0x0006A5DA
			// (set) Token: 0x06001911 RID: 6417 RVA: 0x0006C3E2 File Offset: 0x0006A5E2
			public string Description
			{
				get
				{
					return this.description;
				}
				internal set
				{
					this.description = value;
				}
			}

			// Token: 0x0400095D RID: 2397
			private Schedule schedule;

			// Token: 0x0400095E RID: 2398
			private string description;
		}
	}
}
