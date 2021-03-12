using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200021F RID: 543
	public class ScheduleEditor : ExchangeUserControl
	{
		// Token: 0x060018A0 RID: 6304 RVA: 0x0006870C File Offset: 0x0006690C
		static ScheduleEditor()
		{
			ScheduleEditor.dayTexts = new string[]
			{
				Strings.Sunday,
				Strings.Monday,
				Strings.Tuesday,
				Strings.Wednesday,
				Strings.Thursday,
				Strings.Friday,
				Strings.Saturday
			};
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x0006879C File Offset: 0x0006699C
		public ScheduleEditor()
		{
			this.InitializeComponent();
			this.gridControl.SupportDoubleBuffer = true;
			this.gridHeaderControl.SupportDoubleBuffer = true;
			this.sunMoonControl.SupportDoubleBuffer = true;
			this.detailViewGroupBox.Text = Strings.ScheduleDetailView;
			this.hourRadioButton.Text = Strings.OneHourDetails;
			this.intervalRadioButton.Text = Strings.FifteenMinutesDetailsView;
			this.sunMoonControl.Paint += this.sunMoonControl_Paint;
			this.gridHeaderControl.Paint += this.gridHeaderControl_Paint;
			this.gridControl.MouseLeave += this.gridControl_MouseLeave;
			this.gridControl.MouseDown += this.gridControl_MouseDown;
			this.gridControl.MouseUp += this.gridControl_MouseUp;
			this.gridControl.MouseMove += this.gridControl_MouseMove;
			this.gridControl.Paint += this.gridControl_Paint;
			this.gridControl.PreviewKeyDown += this.gridControl_PreviewKeyDown;
			this.gridControl.KeyDown += this.gridControl_KeyDown;
			this.gridControl.LostFocus += delegate(object param0, EventArgs param1)
			{
				this.gridControl.Invalidate();
			};
			this.scrollPanel.Scroll += this.scrollPanel_Scroll;
			this.hourRadioButton.Click += this.hour_Click;
			this.intervalRadioButton.Click += this.interval_Click;
			this.hintLabel.Paint += this.hintLabel_Paint;
			this.hintLabel.TextChanged += this.hintLabel_TextChanged;
			this.Schedule = Schedule.Never;
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x000689C4 File Offset: 0x00066BC4
		private void InitializeComponent()
		{
			this.contentPanel = new AutoTableLayoutPanel();
			this.hintLabel = new OwnerDrawControl();
			this.scrollPanel = new Panel();
			this.gridControl = new OwnerDrawControl();
			this.intervalButtonsPanel = new FlowLayoutPanel();
			this.dayButtonsPanel = new TableLayoutPanel();
			this.gridHeaderControl = new OwnerDrawControl();
			this.sunMoonControl = new OwnerDrawControl();
			this.detailViewPanel = new Panel();
			this.detailViewGroupBox = new GroupBox();
			this.detailViewTableLayoutPanel = new TableLayoutPanel();
			this.hourRadioButton = new AutoHeightRadioButton();
			this.intervalRadioButton = new AutoHeightRadioButton();
			this.contentPanel.SuspendLayout();
			this.scrollPanel.SuspendLayout();
			this.detailViewPanel.SuspendLayout();
			this.detailViewGroupBox.SuspendLayout();
			this.detailViewTableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			this.contentPanel.AutoSize = true;
			this.contentPanel.ColumnCount = 3;
			this.contentPanel.ColumnStyles.Add(new ColumnStyle());
			this.contentPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.contentPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 16f));
			this.contentPanel.Controls.Add(this.hintLabel, 1, 4);
			this.contentPanel.Controls.Add(this.scrollPanel, 1, 3);
			this.contentPanel.Controls.Add(this.dayButtonsPanel, 0, 3);
			this.contentPanel.Controls.Add(this.gridHeaderControl, 0, 2);
			this.contentPanel.Controls.Add(this.sunMoonControl, 0, 1);
			this.contentPanel.Controls.Add(this.detailViewPanel, 1, 0);
			this.contentPanel.Dock = DockStyle.Top;
			this.contentPanel.Location = new Point(6, 0);
			this.contentPanel.Margin = new Padding(0);
			this.contentPanel.Name = "contentPanel";
			this.contentPanel.Padding = new Padding(0);
			this.contentPanel.RowCount = 5;
			this.contentPanel.RowStyles.Add(new RowStyle());
			this.contentPanel.RowStyles.Add(new RowStyle());
			this.contentPanel.RowStyles.Add(new RowStyle());
			this.contentPanel.RowStyles.Add(new RowStyle());
			this.contentPanel.RowStyles.Add(new RowStyle());
			this.contentPanel.Size = new Size(353, 306);
			this.contentPanel.TabIndex = 13;
			this.hintLabel.Dock = DockStyle.Top;
			this.hintLabel.Location = new Point(0, 283);
			this.hintLabel.Margin = new Padding(0);
			this.hintLabel.Name = "hintLabel";
			this.hintLabel.Size = new Size(353, 16);
			this.hintLabel.TabIndex = 35;
			this.scrollPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.scrollPanel.AutoScroll = true;
			this.scrollPanel.Controls.Add(this.gridControl);
			this.scrollPanel.Controls.Add(this.intervalButtonsPanel);
			this.scrollPanel.Location = new Point(0, 97);
			this.scrollPanel.Margin = new Padding(0);
			this.scrollPanel.Name = "scrollPanel";
			this.scrollPanel.Size = new Size(353, 186);
			this.scrollPanel.TabIndex = 34;
			this.gridControl.BackColor = SystemColors.Window;
			this.gridControl.Location = new Point(0, 19);
			this.gridControl.Margin = new Padding(0);
			this.gridControl.Name = "gridControl";
			this.gridControl.Size = new Size(353, 167);
			this.gridControl.TabIndex = 2;
			this.gridControl.TabStop = false;
			this.intervalButtonsPanel.Location = new Point(0, 0);
			this.intervalButtonsPanel.Margin = new Padding(0);
			this.intervalButtonsPanel.Name = "intervalButtonsPanel";
			this.intervalButtonsPanel.Size = new Size(353, 19);
			this.intervalButtonsPanel.TabIndex = 1;
			this.dayButtonsPanel.AutoSize = true;
			this.dayButtonsPanel.ColumnCount = 1;
			this.dayButtonsPanel.ColumnStyles.Add(new ColumnStyle());
			this.dayButtonsPanel.Dock = DockStyle.Left;
			this.dayButtonsPanel.Location = new Point(0, 97);
			this.dayButtonsPanel.Margin = new Padding(0);
			this.dayButtonsPanel.Name = "dayButtonsPanel";
			this.dayButtonsPanel.Size = new Size(0, 209);
			this.dayButtonsPanel.TabIndex = 33;
			this.contentPanel.SetColumnSpan(this.gridHeaderControl, 3);
			this.gridHeaderControl.Dock = DockStyle.Top;
			this.gridHeaderControl.Location = new Point(0, 76);
			this.gridHeaderControl.Margin = new Padding(0);
			this.gridHeaderControl.Name = "gridHeaderControl";
			this.gridHeaderControl.Size = new Size(353, 21);
			this.gridHeaderControl.TabIndex = 31;
			this.gridHeaderControl.TabStop = false;
			this.contentPanel.SetColumnSpan(this.sunMoonControl, 3);
			this.sunMoonControl.Dock = DockStyle.Top;
			this.sunMoonControl.Location = new Point(0, 51);
			this.sunMoonControl.Margin = new Padding(0);
			this.sunMoonControl.Name = "sunMoonControl";
			this.sunMoonControl.Size = new Size(353, 25);
			this.sunMoonControl.TabIndex = 30;
			this.sunMoonControl.TabStop = false;
			this.detailViewPanel.AutoSize = true;
			this.detailViewPanel.Controls.Add(this.detailViewGroupBox);
			this.detailViewPanel.Dock = DockStyle.Top;
			this.detailViewPanel.Location = new Point(0, 0);
			this.detailViewPanel.Margin = new Padding(0, 0, 0, 8);
			this.detailViewPanel.Name = "detailViewPanel";
			this.detailViewPanel.Size = new Size(353, 51);
			this.detailViewPanel.TabIndex = 28;
			this.detailViewPanel.Text = "detailViewPanel";
			this.detailViewGroupBox.Controls.Add(this.detailViewTableLayoutPanel);
			this.detailViewGroupBox.Dock = DockStyle.Top;
			this.detailViewGroupBox.Location = new Point(0, 0);
			this.detailViewGroupBox.Margin = new Padding(0);
			this.detailViewGroupBox.Name = "detailViewGroupBox";
			this.detailViewGroupBox.Size = new Size(353, 43);
			this.detailViewGroupBox.TabIndex = 36;
			this.detailViewGroupBox.TabStop = false;
			this.detailViewGroupBox.Text = "detailViewGroupBox";
			this.detailViewTableLayoutPanel.AutoSize = true;
			this.detailViewTableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.detailViewTableLayoutPanel.ColumnCount = 2;
			this.detailViewTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
			this.detailViewTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.detailViewTableLayoutPanel.Controls.Add(this.hourRadioButton, 0, 0);
			this.detailViewTableLayoutPanel.Controls.Add(this.intervalRadioButton, 1, 0);
			this.detailViewTableLayoutPanel.Dock = DockStyle.Top;
			this.detailViewTableLayoutPanel.Location = new Point(3, 16);
			this.detailViewTableLayoutPanel.Margin = new Padding(0);
			this.detailViewTableLayoutPanel.Name = "detailViewTableLayoutPanel";
			this.detailViewTableLayoutPanel.RowCount = 1;
			this.detailViewTableLayoutPanel.RowStyles.Add(new RowStyle());
			this.detailViewTableLayoutPanel.Size = new Size(347, 23);
			this.detailViewTableLayoutPanel.TabIndex = 0;
			this.hourRadioButton.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.hourRadioButton.CheckAlign = ContentAlignment.MiddleLeft;
			this.hourRadioButton.Location = new Point(3, 3);
			this.hourRadioButton.Name = "hourRadioButton";
			this.hourRadioButton.Size = new Size(108, 17);
			this.hourRadioButton.TabIndex = 30;
			this.hourRadioButton.TabStop = true;
			this.hourRadioButton.Text = "hourRadioButton";
			this.hourRadioButton.TextAlign = ContentAlignment.MiddleLeft;
			this.hourRadioButton.UseVisualStyleBackColor = true;
			this.intervalRadioButton.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.intervalRadioButton.CheckAlign = ContentAlignment.MiddleLeft;
			this.intervalRadioButton.Location = new Point(117, 3);
			this.intervalRadioButton.Name = "intervalRadioButton";
			this.intervalRadioButton.Size = new Size(227, 17);
			this.intervalRadioButton.TabIndex = 32;
			this.intervalRadioButton.TabStop = true;
			this.intervalRadioButton.Text = "intervalRadioButton";
			this.intervalRadioButton.TextAlign = ContentAlignment.MiddleLeft;
			this.intervalRadioButton.UseVisualStyleBackColor = true;
			base.Controls.Add(this.contentPanel);
			base.Name = "ScheduleEditor";
			base.Size = new Size(364, 307);
			this.contentPanel.ResumeLayout(false);
			this.contentPanel.PerformLayout();
			this.scrollPanel.ResumeLayout(false);
			this.detailViewPanel.ResumeLayout(false);
			this.detailViewGroupBox.ResumeLayout(false);
			this.detailViewGroupBox.PerformLayout();
			this.detailViewTableLayoutPanel.ResumeLayout(false);
			this.detailViewTableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x060018A3 RID: 6307 RVA: 0x000693E8 File Offset: 0x000675E8
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (this.hourIndicatorBrush == null)
			{
				this.hourIndicatorBrush = new SolidBrush(this.hintLabel.ForeColor);
			}
			if (this.hourIndicatorPen == null)
			{
				this.hourIndicatorPen = new Pen(this.hourIndicatorBrush);
			}
			if (this.gridBrush == null)
			{
				this.gridBrush = new SolidBrush(ControlPaint.Light(SystemColors.Highlight));
			}
			this.InitializeContextMenu();
			this.InitializeDayButtons();
			this.InitializeCellSize();
			this.LayoutIntervalButtons();
			this.AdjustScrollpanel();
			this.gridHeaderControl.Invalidate(true);
			this.sunMoonControl.Invalidate(true);
			this.gridControl.Invalidate(true);
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00069494 File Offset: 0x00067694
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.hourIndicatorPen != null)
				{
					this.hourIndicatorPen.Dispose();
				}
				if (this.hourIndicatorBrush != null)
				{
					this.hourIndicatorBrush.Dispose();
				}
				if (this.gridBrush != null)
				{
					this.gridBrush.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x000694E4 File Offset: 0x000676E4
		private void InitializeContextMenu()
		{
			this.hourMenuItem = new MenuItem();
			this.hourMenuItem.Checked = true;
			this.hourMenuItem.Index = 0;
			this.hourMenuItem.Text = Strings.OneHourDetails;
			this.hourMenuItem.Click += this.hour_Click;
			this.intervalMenuItem = new MenuItem();
			this.intervalMenuItem.Index = 1;
			this.intervalMenuItem.Text = Strings.FifteenMinutesDetailsView;
			this.intervalMenuItem.Click += this.interval_Click;
			this.contextMenu = new ContextMenu();
			this.contextMenu.MenuItems.AddRange(new MenuItem[]
			{
				this.hourMenuItem,
				this.intervalMenuItem
			});
			this.ContextMenu = this.contextMenu;
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x000695C4 File Offset: 0x000677C4
		private void hour_Click(object sender, EventArgs e)
		{
			this.hourRadioButton.Checked = true;
			this.ShowIntervals = false;
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x000695D9 File Offset: 0x000677D9
		private void interval_Click(object sender, EventArgs e)
		{
			this.intervalRadioButton.Checked = true;
			this.ShowIntervals = true;
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x000695F8 File Offset: 0x000677F8
		private void InitializeDayButtons()
		{
			base.SuspendLayout();
			this.dayButtonsPanel.SuspendLayout();
			string[] array = new string[8];
			array[0] = Strings.All;
			ScheduleEditor.dayTexts.CopyTo(array, 1);
			for (int i = 0; i < array.Length; i++)
			{
				Button button = new ExchangeButton();
				button.Margin = new Padding(0);
				button.Padding = new Padding(0);
				button.FlatStyle = FlatStyle.System;
				button.Text = array[i];
				button.AutoSize = true;
				button.TabIndex = i;
				button.TabStop = (i == 0);
				button.Dock = DockStyle.Top;
				button.Tag = i - 1;
				this.dayButtonsPanel.RowCount++;
				this.dayButtonsPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
				this.dayButtonsPanel.Controls.Add(button, 0, i);
				button.MouseEnter += this.dayButton_Enter;
				button.MouseLeave += this.dayButton_Leave;
				button.Enter += this.dayButton_Enter;
				button.Leave += this.dayButton_Leave;
				button.Click += ((i == 0) ? new EventHandler(this.all_Click) : new EventHandler(this.dayButton_Click));
				button.PreviewKeyDown += this.Button_PreviewKeyDown;
				button.KeyDown += this.DayButton_KeyDown;
				if (i == 0)
				{
					button.GotFocus += delegate(object param0, EventArgs param1)
					{
						this.SetFocus();
					};
				}
			}
			this.dayButtonsPanel.ResumeLayout(false);
			this.dayButtonsPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x000697B4 File Offset: 0x000679B4
		private void dayButton_Enter(object sender, EventArgs e)
		{
			Button button = sender as Button;
			this.UpdateHintText(new Point(-1, (int)button.Tag));
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x000697E0 File Offset: 0x000679E0
		private void dayButton_Leave(object sender, EventArgs e)
		{
			Button button = sender as Button;
			if (((Point)this.hintLabel.Tag).Equals(new Point(-1, (int)button.Tag)))
			{
				this.ResetHintText(sender, e);
			}
		}

		// Token: 0x060018AB RID: 6315 RVA: 0x00069834 File Offset: 0x00067A34
		private void all_Click(object sender, EventArgs e)
		{
			if (Schedule.Never == this.scheduleBuilder.Schedule)
			{
				this.scheduleBuilder = new ScheduleBuilder(Schedule.Always);
			}
			else
			{
				this.scheduleBuilder = new ScheduleBuilder(Schedule.Never);
			}
			this.selectionEnd = (this.selectionStart = new Point(-1, -1));
			this.gridControl.Invalidate(true);
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x00069898 File Offset: 0x00067A98
		private void dayButton_Click(object sender, EventArgs e)
		{
			DayOfWeek dayOfWeek = (DayOfWeek)((Button)sender).Tag;
			this.scheduleBuilder.SetStateOfDay(dayOfWeek, !this.scheduleBuilder.GetStateOfDay(dayOfWeek));
			this.selectionEnd = (this.selectionStart = new Point(-1, (int)dayOfWeek));
			this.gridControl.Invalidate(true);
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x000698F3 File Offset: 0x00067AF3
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			base.ScaleControl(factor, specified);
			this.cellSize = new Size((int)((float)this.cellSize.Width * factor.Width), (int)((float)this.cellSize.Height * factor.Height));
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x00069934 File Offset: 0x00067B34
		private void InitializeCellSize()
		{
			this.cellSize.Height = this.dayButtonsPanel.Controls[0].Height;
			Size proposedSize = new Size(int.MaxValue, int.MaxValue);
			for (int i = 0; i < 24; i++)
			{
				this.textSizes[i.ToString()] = TextRenderer.MeasureText(i.ToString(), this.hintLabel.Font, proposedSize, TextFormatFlags.HorizontalCenter | TextFormatFlags.NoPadding);
				this.cellSize.Width = Math.Max((this.textSizes[i.ToString()].Width + this.hourIndicatorSize.Width) / 2 + 1, this.cellSize.Width);
			}
			int num = this.cellSize.Width % 4;
			if (num == 0)
			{
				this.cellSize.Width = this.cellSize.Width + 1;
				return;
			}
			if (num != 1)
			{
				this.cellSize.Width = this.cellSize.Width + (4 - num + 1);
			}
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x00069A34 File Offset: 0x00067C34
		private int GetMaxTextHeight()
		{
			int num = int.MinValue;
			foreach (Size size in this.textSizes.Values)
			{
				num = Math.Max(num, size.Height);
			}
			return num;
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x00069A9C File Offset: 0x00067C9C
		private void sunMoonControl_Paint(object sender, PaintEventArgs e)
		{
			for (int i = this.GetStartVisibleFullHCell(); i <= this.GetEndVisibleFullHCell() + 1; i++)
			{
				if (!this.ShowIntervals || i % 4 == 0)
				{
					int hour = (this.ShowIntervals ? (i / 4) : i) % 24;
					Icon hourIcon = this.GetHourIcon(hour);
					if (hourIcon != null)
					{
						Rectangle rectangle = new Rectangle(this.GetLeftPaddingOfSunMoonControl() + (i - this.GetStartVisibleFullHCell()) * this.cellSize.Width, 0, ScheduleEditor.iconSize.Width, ScheduleEditor.iconSize.Height);
						e.Graphics.DrawIcon(hourIcon, LayoutHelper.MirrorRectangle(rectangle, this.sunMoonControl));
					}
				}
			}
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x00069B3D File Offset: 0x00067D3D
		private int GetLeftPaddingOfSunMoonControl()
		{
			return this.dayButtonsPanel.Width - ScheduleEditor.iconSize.Width / 2 - 1;
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x00069B59 File Offset: 0x00067D59
		private int GetRightOffSetOfSunMoonControl()
		{
			return ScheduleEditor.iconSize.Width - ScheduleEditor.iconSize.Width / 2 - 1;
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x00069B74 File Offset: 0x00067D74
		private Icon GetHourIcon(int hour)
		{
			if (hour == 0)
			{
				return Icons.Moon;
			}
			if (hour == 12)
			{
				return Icons.Sun;
			}
			return null;
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x00069B8C File Offset: 0x00067D8C
		private void gridHeaderControl_Paint(object sender, PaintEventArgs e)
		{
			for (int i = this.GetStartVisibleFullHCell(); i <= this.GetEndVisibleFullHCell() + 1; i++)
			{
				if (!this.ShowIntervals || i % 4 == 0)
				{
					int hour = (this.ShowIntervals ? (i / 4) : i) % 24;
					string hourText = this.GetHourText(hour);
					if (!string.IsNullOrEmpty(hourText))
					{
						Rectangle rectangle = new Rectangle(this.GetLeftPaddingOfGridHeaderControl() + (i - this.GetStartVisibleFullHCell()) * this.cellSize.Width, 0, this.cellSize.Width, this.GetMaxTextHeight());
						TextRenderer.DrawText(e.Graphics, hourText, this.hintLabel.Font, LayoutHelper.MirrorRectangle(rectangle, this.gridHeaderControl), this.hintLabel.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.NoPadding);
					}
					else
					{
						Rectangle rectangle2 = new Rectangle(this.dayButtonsPanel.Width + (i - this.GetStartVisibleFullHCell()) * this.cellSize.Width - 1 - this.hourIndicatorSize.Width / 2 - 1, this.gridHeaderControl.Height / 2 - this.hourIndicatorSize.Height / 2 - 1, this.hourIndicatorSize.Width, this.hourIndicatorSize.Height);
						e.Graphics.DrawEllipse(this.hourIndicatorPen, LayoutHelper.MirrorRectangle(rectangle2, this.gridHeaderControl));
						e.Graphics.FillEllipse(this.hourIndicatorBrush, LayoutHelper.MirrorRectangle(rectangle2, this.gridHeaderControl));
					}
				}
			}
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x00069D00 File Offset: 0x00067F00
		private int GetStartVisibleFullHCell()
		{
			int num = this.scrollPanel.HorizontalScroll.Value / this.cellSize.Width;
			if (LayoutHelper.IsRightToLeft(this))
			{
				int num2 = this.ShowIntervals ? 96 : 24;
				num = ((num2 <= this.GetMaxVisibleFullHCellsNumber()) ? 0 : (num2 - (num + this.GetMaxVisibleFullHCellsNumber())));
			}
			return num;
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x00069D59 File Offset: 0x00067F59
		private int GetEndVisibleFullHCell()
		{
			return this.GetStartVisibleFullHCell() + this.GetMaxVisibleFullHCellsNumber() - 1;
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x00069D6A File Offset: 0x00067F6A
		private int GetLeftPaddingOfGridHeaderControl()
		{
			return this.dayButtonsPanel.Width - this.cellSize.Width / 2 - 1;
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x00069D87 File Offset: 0x00067F87
		private int GetRightOffSetOfGridHeaderControl()
		{
			return this.cellSize.Width - this.cellSize.Width / 2 - 1;
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x00069DA4 File Offset: 0x00067FA4
		private string GetHourText(int hour)
		{
			if (this.ShowIntervals)
			{
				return hour.ToString();
			}
			if (hour % 2 == 0)
			{
				return hour.ToString();
			}
			return null;
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x00069DC4 File Offset: 0x00067FC4
		private void LayoutIntervalButtons()
		{
			base.SuspendLayout();
			this.scrollPanel.SuspendLayout();
			this.intervalButtonsPanel.SuspendLayout();
			try
			{
				if (this.intervalButtonsPanel.Controls.Count == 0)
				{
					for (int i = 0; i < 96; i++)
					{
						Button button = new ExchangeButton();
						button.Margin = new Padding(0);
						button.Padding = new Padding(0);
						button.FlatStyle = FlatStyle.System;
						button.Size = this.cellSize;
						button.TabStop = false;
						button.TabIndex = i;
						button.Tag = i;
						button.Dock = DockStyle.Top;
						button.Click += this.intervalButton_Click;
						button.MouseMove += this.intervalButton_MouseMove;
						button.MouseLeave += this.intervalButton_Leave;
						button.Enter += this.intervalButton_Enter;
						button.Leave += this.intervalButton_Leave;
						button.PreviewKeyDown += this.Button_PreviewKeyDown;
						button.KeyDown += this.intervalButton_KeyDown;
						this.intervalButtonsPanel.Controls.Add(button);
					}
				}
				for (int j = 0; j < 96; j++)
				{
					if (j < this.NumberOfHCells)
					{
						this.intervalButtonsPanel.Controls[j].Visible = true;
					}
					else
					{
						this.intervalButtonsPanel.Controls[j].Visible = false;
					}
				}
			}
			finally
			{
				this.intervalButtonsPanel.ResumeLayout(false);
				this.intervalButtonsPanel.PerformLayout();
				this.scrollPanel.ResumeLayout(false);
				this.scrollPanel.PerformLayout();
				base.ResumeLayout(false);
				base.PerformLayout();
			}
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x00069F98 File Offset: 0x00068198
		private Button GetFocusedIntervalButton()
		{
			foreach (object obj in this.intervalButtonsPanel.Controls)
			{
				Control control = (Control)obj;
				if (control.Visible && control.Focused)
				{
					return control as Button;
				}
			}
			return null;
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x0006A00C File Offset: 0x0006820C
		private void intervalButton_MouseMove(object sender, MouseEventArgs e)
		{
			Button button = sender as Button;
			this.newMousePositionInScrollPanel.X = button.Left + e.X - this.scrollPanel.HorizontalScroll.Value;
			this.newMousePositionInScrollPanel.Y = button.Top + e.Y - this.scrollPanel.VerticalScroll.Value;
			if (!this.newMousePositionInScrollPanel.Equals(this.oldMousePositionInScrollPanel))
			{
				this.oldMousePositionInScrollPanel = this.newMousePositionInScrollPanel;
				this.UpdateHintText(new Point((int)button.Tag, -1));
			}
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x0006A0B4 File Offset: 0x000682B4
		private void intervalButton_Enter(object sender, EventArgs e)
		{
			Button button = sender as Button;
			this.UpdateHintText(new Point((int)button.Tag, -1));
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x0006A0E0 File Offset: 0x000682E0
		private void intervalButton_Leave(object sender, EventArgs e)
		{
			Button button = sender as Button;
			if (((Point)this.hintLabel.Tag).Equals(new Point((int)button.Tag, -1)))
			{
				this.ResetHintText(sender, e);
			}
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x0006A134 File Offset: 0x00068334
		private void intervalButton_Click(object sender, EventArgs e)
		{
			int num = (int)((Button)sender).Tag;
			if (this.ShowIntervals)
			{
				this.scheduleBuilder.SetStateOfInterval(num, !this.scheduleBuilder.GetStateOfInterval(num));
			}
			else
			{
				this.scheduleBuilder.SetStateOfHour(num, !this.scheduleBuilder.GetStateOfHour(num));
			}
			this.selectionEnd = (this.selectionStart = new Point(num, -1));
			this.gridControl.Invalidate(true);
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x0006A1B4 File Offset: 0x000683B4
		private void Button_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			e.IsInputKey = (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || (sender.Equals(this.gridControl) && e.KeyCode == Keys.Space));
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x0006A210 File Offset: 0x00068410
		private void intervalButton_KeyDown(object sender, KeyEventArgs e)
		{
			Control control = sender as Control;
			if (((e.KeyCode == Keys.Left && !LayoutHelper.IsRightToLeft(this)) || (e.KeyCode == Keys.Right && LayoutHelper.IsRightToLeft(this))) && this.selectionEnd.X >= 0)
			{
				this.selectionEnd.X = this.selectionEnd.X - 1;
				this.selectionStart = this.selectionEnd;
				if (this.GetControlIndex(control) == 0)
				{
					this.SelectFirstChild(this.dayButtonsPanel);
					return;
				}
				this.InternalSelectNextControl(control, false);
				this.scrollPanel_Scroll(this.scrollPanel, new ScrollEventArgs(ScrollEventType.SmallIncrement, this.scrollPanel.HorizontalScroll.Value, ScrollOrientation.HorizontalScroll));
				return;
			}
			else
			{
				if (((e.KeyCode == Keys.Right && !LayoutHelper.IsRightToLeft(this)) || (e.KeyCode == Keys.Left && LayoutHelper.IsRightToLeft(this))) && this.selectionEnd.X < this.NumberOfHCells - 1)
				{
					this.selectionEnd.X = this.selectionEnd.X + 1;
					this.selectionStart = this.selectionEnd;
					this.InternalSelectNextControl(control, true);
					this.scrollPanel_Scroll(this.scrollPanel, new ScrollEventArgs(ScrollEventType.SmallIncrement, this.scrollPanel.HorizontalScroll.Value, ScrollOrientation.HorizontalScroll));
					return;
				}
				if (e.KeyCode == Keys.Down)
				{
					this.gridControl.Focus();
					this.selectionEnd.Y = this.selectionEnd.Y + 1;
					this.selectionStart = this.selectionEnd;
					this.EnsureCellVisible(this.selectionEnd);
					this.gridControl.Invalidate();
				}
				return;
			}
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0006A388 File Offset: 0x00068588
		private void DayButton_KeyDown(object sender, KeyEventArgs e)
		{
			Control control = sender as Control;
			if ((e.KeyCode == Keys.Right && !LayoutHelper.IsRightToLeft(this)) || (e.KeyCode == Keys.Left && LayoutHelper.IsRightToLeft(this)))
			{
				if (this.GetControlIndex(control) == 0)
				{
					this.SelectFirstChild(this.intervalButtonsPanel);
				}
				else
				{
					this.gridControl.Focus();
				}
				this.selectionEnd.X = this.selectionEnd.X + 1;
				this.selectionStart = this.selectionEnd;
				this.EnsureCellVisible(this.selectionEnd);
				this.gridControl.Invalidate();
				return;
			}
			if (e.KeyCode == Keys.Up && this.selectionEnd.Y >= 0)
			{
				this.selectionEnd.Y = this.selectionEnd.Y - 1;
				this.selectionStart = this.selectionEnd;
				this.InternalSelectNextControl(control, false);
				return;
			}
			if (e.KeyCode == Keys.Down && this.selectionEnd.Y < this.NumberOfVCells - 1)
			{
				this.selectionEnd.Y = this.selectionEnd.Y + 1;
				this.selectionStart = this.selectionEnd;
				this.InternalSelectNextControl(control, true);
			}
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x0006A4A0 File Offset: 0x000686A0
		private void InternalSelectNextControl(Control activeControl, bool forward)
		{
			if (activeControl.Parent != null)
			{
				int num = activeControl.Parent.Controls.IndexOf(activeControl);
				if (forward && num < activeControl.Parent.Controls.Count - 1)
				{
					num++;
				}
				else if (!forward && num > 0)
				{
					num--;
				}
				Control control = activeControl.Parent.Controls[num];
				if (control.Visible)
				{
					control.Focus();
				}
			}
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0006A514 File Offset: 0x00068714
		private void SelectFirstChild(Control control)
		{
			for (int i = 0; i < control.Controls.Count; i++)
			{
				Control control2 = control.Controls[i];
				if (control2.Visible)
				{
					control2.Focus();
					return;
				}
			}
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0006A554 File Offset: 0x00068754
		private int GetControlIndex(Control control)
		{
			if (control.Parent == null)
			{
				return -1;
			}
			return control.Parent.Controls.IndexOf(control);
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x0006A574 File Offset: 0x00068774
		private void gridControl_MouseDown(object sender, MouseEventArgs e)
		{
			if (!this.gridControl.Focused)
			{
				this.gridControl.Select();
			}
			if (e.Button == MouseButtons.Left)
			{
				this.isKeyboardMoveCellFocus = false;
				this.gridControl.Capture = true;
				this.selectionStart = this.CellFromGridXY(e.X, e.Y);
				this.selectionEnd = this.selectionStart;
			}
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x0006A5E0 File Offset: 0x000687E0
		private void gridControl_MouseUp(object sender, MouseEventArgs e)
		{
			if (this.gridControl.Capture)
			{
				this.gridControl.Capture = false;
				this.selectionEnd = this.CellFromGridXY(e.X, e.Y);
				bool selectedCellsState = !this.GetCellState(this.selectionStart);
				this.SetSelectedCellsState(selectedCellsState);
				this.gridControl.Invalidate();
			}
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x0006A640 File Offset: 0x00068840
		private void gridControl_MouseLeave(object sender, EventArgs e)
		{
			Point point = (Point)this.hintLabel.Tag;
			if (point.X >= 0 && point.Y >= 0)
			{
				this.ResetHintText(sender, e);
			}
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x0006A67C File Offset: 0x0006887C
		private void gridControl_MouseMove(object sender, MouseEventArgs e)
		{
			this.newMousePositionInScrollPanel.X = e.X - this.scrollPanel.HorizontalScroll.Value;
			this.newMousePositionInScrollPanel.Y = e.Y - this.scrollPanel.VerticalScroll.Value;
			if (!this.newMousePositionInScrollPanel.Equals(this.oldMousePositionInScrollPanel))
			{
				this.oldMousePositionInScrollPanel = this.newMousePositionInScrollPanel;
				if (this.gridControl.Capture)
				{
					this.selectionEnd = this.CellFromGridXY(e.X, e.Y);
					this.EnsureCellVisible(this.selectionEnd);
					this.gridControl.Invalidate(true);
				}
				this.UpdateHintText(this.CellFromGridXY(e.X, e.Y));
			}
		}

		// Token: 0x060018CA RID: 6346 RVA: 0x0006A74C File Offset: 0x0006894C
		private void SetCellRowStates(int startX, int endX, int y, bool state)
		{
			int num = Math.Min(startX, endX);
			int num2 = Math.Max(startX, endX);
			for (int i = num; i <= num2; i++)
			{
				this.SetCellState(new Point(i, y), state);
			}
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x0006A784 File Offset: 0x00068984
		private void SetCellColumnStates(int startY, int endY, int x, bool state)
		{
			int num = Math.Min(startY, endY);
			int num2 = Math.Max(startY, endY);
			for (int i = num; i <= num2; i++)
			{
				this.SetCellState(new Point(x, i), state);
			}
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x0006A7BC File Offset: 0x000689BC
		private void gridControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			e.IsInputKey = (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right);
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x0006A7F0 File Offset: 0x000689F0
		private void gridControl_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up || e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
			{
				bool flag = false;
				if (e.Shift)
				{
					flag = this.GetCellState(this.selectionStart);
					if (this.selectionEnd == this.selectionStart && this.isKeyboardMoveCellFocus)
					{
						flag = !flag;
						this.SetCellState(this.selectionStart, flag);
					}
				}
				if (e.KeyCode == Keys.Up)
				{
					if (this.selectionEnd.Y >= 0 && !e.Shift)
					{
						this.selectionEnd.Y = this.selectionEnd.Y - 1;
						if (this.selectionEnd.Y == -1)
						{
							this.intervalButtonsPanel.Controls[this.selectionEnd.X].Focus();
						}
					}
					else if (e.Shift && this.selectionEnd.Y > 0)
					{
						this.selectionEnd.Y = this.selectionEnd.Y - 1;
						if (this.selectionEnd.Y >= this.selectionStart.Y)
						{
							flag = !flag;
							this.SetCellRowStates(this.selectionStart.X, this.selectionEnd.X, this.selectionEnd.Y + 1, flag);
						}
						else
						{
							this.SetCellRowStates(this.selectionStart.X, this.selectionEnd.X, this.selectionEnd.Y, flag);
						}
					}
				}
				else if (e.KeyCode == Keys.Down && this.selectionEnd.Y < this.NumberOfVCells - 1)
				{
					this.selectionEnd.Y = this.selectionEnd.Y + 1;
					if (e.Shift)
					{
						if (this.selectionStart.Y >= this.selectionEnd.Y)
						{
							flag = !flag;
							this.SetCellRowStates(this.selectionStart.X, this.selectionEnd.X, this.selectionEnd.Y - 1, flag);
						}
						else
						{
							this.SetCellRowStates(this.selectionStart.X, this.selectionEnd.X, this.selectionEnd.Y, flag);
						}
					}
				}
				else if ((e.KeyCode == Keys.Left && !LayoutHelper.IsRightToLeft(this)) || (e.KeyCode == Keys.Right && LayoutHelper.IsRightToLeft(this)))
				{
					if (this.selectionEnd.X >= 0 && !e.Shift)
					{
						this.selectionEnd.X = this.selectionEnd.X - 1;
						if (this.selectionEnd.X == -1)
						{
							this.dayButtonsPanel.Controls[this.selectionEnd.Y + 1].Focus();
						}
					}
					if (e.Shift && this.selectionEnd.X > 0)
					{
						this.selectionEnd.X = this.selectionEnd.X - 1;
						if (this.selectionEnd.X >= this.selectionStart.X)
						{
							flag = !flag;
							this.SetCellColumnStates(this.selectionStart.Y, this.selectionEnd.Y, this.selectionEnd.X + 1, flag);
						}
						else
						{
							this.SetCellColumnStates(this.selectionStart.Y, this.selectionEnd.Y, this.selectionEnd.X, flag);
						}
					}
				}
				else if (((e.KeyCode == Keys.Right && !LayoutHelper.IsRightToLeft(this)) || (e.KeyCode == Keys.Left && LayoutHelper.IsRightToLeft(this))) && this.selectionEnd.X < this.NumberOfHCells - 1)
				{
					this.selectionEnd.X = this.selectionEnd.X + 1;
					if (e.Shift)
					{
						if (this.selectionStart.X >= this.selectionEnd.X)
						{
							flag = !flag;
							this.SetCellColumnStates(this.selectionStart.Y, this.selectionEnd.Y, this.selectionEnd.X - 1, flag);
						}
						else
						{
							this.SetCellColumnStates(this.selectionStart.Y, this.selectionEnd.Y, this.selectionEnd.X, flag);
						}
					}
				}
				if (!e.Shift)
				{
					this.selectionStart = this.selectionEnd;
					this.isKeyboardMoveCellFocus = true;
				}
				this.EnsureCellVisible(this.selectionEnd);
				this.UpdateHintText(this.selectionEnd);
				this.gridControl.Invalidate();
				e.Handled = true;
				return;
			}
			if (e.KeyCode == Keys.Space)
			{
				this.SetSelectedCellsState(!this.GetCellState(this.selectionEnd));
				this.gridControl.Invalidate();
				e.Handled = true;
			}
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x0006ACB0 File Offset: 0x00068EB0
		private void SetFocus()
		{
			if (this.dayButtonsPanel.Controls[0].Capture)
			{
				return;
			}
			if (this.selectionEnd.X == -1)
			{
				this.dayButtonsPanel.Controls[this.selectionEnd.Y + 1].Focus();
				return;
			}
			if (this.selectionEnd.Y == -1 && this.selectionEnd.X != -1)
			{
				this.intervalButtonsPanel.Controls[this.selectionEnd.X].Focus();
				return;
			}
			this.gridControl.Focus();
			this.gridControl.Invalidate();
		}

		// Token: 0x060018CF RID: 6351 RVA: 0x0006AD60 File Offset: 0x00068F60
		private void gridControl_Paint(object sender, PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			int num = this.ShowIntervals ? 1 : 4;
			for (DayOfWeek dayOfWeek = DayOfWeek.Sunday; dayOfWeek <= DayOfWeek.Saturday; dayOfWeek++)
			{
				for (int i = 0; i < 96; i++)
				{
					if (this.scheduleBuilder.GetStateOfInterval(dayOfWeek, i))
					{
						Rectangle rectangle = new Rectangle(i * this.cellSize.Width / num, (int)(dayOfWeek * (DayOfWeek)this.cellSize.Height), this.cellSize.Width / num, this.cellSize.Height);
						graphics.FillRectangle(this.gridBrush, LayoutHelper.MirrorRectangle(rectangle, this.gridControl));
					}
				}
			}
			int x = this.gridControl.Width - 1;
			for (int j = 1; j <= 7; j++)
			{
				int num2 = j * this.cellSize.Height - 1;
				graphics.DrawLine(SystemPens.ControlDarkDark, 0, num2, x, num2);
			}
			int y = this.gridControl.Height - 1;
			for (int k = 1; k < 97; k++)
			{
				int num3 = LayoutHelper.MirrorPosition(k * this.cellSize.Width - 1, this.gridControl);
				if (LayoutHelper.IsRightToLeft(this))
				{
					num3--;
				}
				graphics.DrawLine(SystemPens.ControlDarkDark, num3, 0, num3, y);
			}
			if (this.gridControl.Focused)
			{
				Point selectionTopLeft = this.SelectionTopLeft;
				Point selectionRightBottom = this.SelectionRightBottom;
				Rectangle rectangle2 = Rectangle.FromLTRB(selectionTopLeft.X * this.cellSize.Width, selectionTopLeft.Y * this.cellSize.Height, (selectionRightBottom.X + 1) * this.cellSize.Width - 1, (selectionRightBottom.Y + 1) * this.cellSize.Height - 1);
				ControlPaint.DrawFocusRectangle(graphics, LayoutHelper.MirrorRectangle(rectangle2, this.gridControl));
			}
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x0006AF28 File Offset: 0x00069128
		private void hintLabel_TextChanged(object sender, EventArgs e)
		{
			this.hintLabel.Invalidate();
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x0006AF35 File Offset: 0x00069135
		private void hintLabel_Paint(object sender, PaintEventArgs e)
		{
			TextRenderer.DrawText(e.Graphics, this.hintLabel.Text, this.hintLabel.Font, this.hintLabel.ClientRectangle, this.hintLabel.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.NoPadding);
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x0006AF74 File Offset: 0x00069174
		private void scrollPanel_Scroll(object sender, ScrollEventArgs e)
		{
			if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
			{
				int num = e.NewValue / this.cellSize.Width + ((e.NewValue % this.cellSize.Width == 0) ? 0 : 1);
				this.scrollPanel.AutoScrollPosition = new Point(num * this.cellSize.Width, this.scrollPanel.AutoScrollPosition.Y);
				Button focusedIntervalButton = this.GetFocusedIntervalButton();
				if (focusedIntervalButton != null)
				{
					Button button = focusedIntervalButton;
					if (this.GetStartVisibleFullHCell() > (int)focusedIntervalButton.Tag)
					{
						button = (this.intervalButtonsPanel.Controls[this.GetStartVisibleFullHCell()] as Button);
					}
					else if (this.GetEndVisibleFullHCell() < (int)focusedIntervalButton.Tag)
					{
						button = (this.intervalButtonsPanel.Controls[this.GetEndVisibleFullHCell()] as Button);
					}
					button.Focus();
				}
				this.gridHeaderControl.Invalidate(true);
				this.sunMoonControl.Invalidate(true);
			}
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x0006B074 File Offset: 0x00069274
		protected override void OnLayout(LayoutEventArgs e)
		{
			this.sunMoonControl.Height = ScheduleEditor.iconSize.Height;
			this.gridHeaderControl.Height = this.GetMaxTextHeight();
			this.intervalButtonsPanel.Top = 0;
			this.intervalButtonsPanel.Height = this.cellSize.Height;
			this.intervalButtonsPanel.Width = this.cellSize.Width * this.NumberOfHCells;
			this.gridControl.Top = this.intervalButtonsPanel.Bottom;
			this.gridControl.Height = this.cellSize.Height * this.NumberOfVCells;
			this.gridControl.Width = this.intervalButtonsPanel.Width;
			this.scrollPanel.Height = this.intervalButtonsPanel.Height + this.gridControl.Height + SystemInformation.HorizontalScrollBarHeight;
			this.scrollPanel.HorizontalScroll.SmallChange = this.cellSize.Width;
			this.hintLabel.Height = this.cellSize.Height;
			base.OnLayout(e);
			this.gridHeaderControl.Invalidate(true);
			this.sunMoonControl.Invalidate(true);
			this.gridControl.Invalidate(true);
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x0006B1B4 File Offset: 0x000693B4
		private void AdjustScrollpanel()
		{
			int right = (this.scrollPanel.Width + this.scrollPanel.Margin.Right) % this.cellSize.Width;
			this.scrollPanel.Margin = new Padding(0, 0, right, 0);
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x0006B201 File Offset: 0x00069401
		private int GetMaxVisibleFullHCellsNumber()
		{
			return Math.Min(this.scrollPanel.Width / this.cellSize.Width, this.ShowIntervals ? 96 : 24);
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x0006B22D File Offset: 0x0006942D
		public override Size GetPreferredSize(Size proposedSize)
		{
			return this.contentPanel.GetPreferredSize(proposedSize);
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x0006B23C File Offset: 0x0006943C
		private void UpdateHintText(Point cell)
		{
			bool flag = -1 != cell.Y;
			bool flag2 = -1 != cell.X;
			StringBuilder stringBuilder = new StringBuilder();
			if (flag)
			{
				stringBuilder.Append(ExchangeUserControl.RemoveAccelerator(ScheduleEditor.dayTexts[cell.Y]));
				if (flag2)
				{
					stringBuilder.Append(" ");
				}
			}
			if (flag2)
			{
				DateTime dateTime;
				if (this.ShowIntervals)
				{
					dateTime = new DateTime(1, 1, 1, cell.X / 4, 15 * (cell.X % 4), 0);
				}
				else
				{
					dateTime = new DateTime(1, 1, 1, cell.X, 0, 0);
				}
				stringBuilder.Append(dateTime.ToShortTimeString());
			}
			this.hintLabel.Text = stringBuilder.ToString();
			this.hintLabel.Tag = cell;
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x0006B306 File Offset: 0x00069506
		private void ResetHintText(object sender, EventArgs e)
		{
			this.hintLabel.Text = string.Empty;
			this.hintLabel.Tag = new Point(-1, -1);
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060018D9 RID: 6361 RVA: 0x0006B32F File Offset: 0x0006952F
		// (set) Token: 0x060018DA RID: 6362 RVA: 0x0006B33C File Offset: 0x0006953C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Schedule Schedule
		{
			get
			{
				return this.scheduleBuilder.Schedule;
			}
			set
			{
				if (this.scheduleBuilder == null || this.scheduleBuilder.Schedule != value)
				{
					if (value != null)
					{
						this.scheduleBuilder = new ScheduleBuilder(value);
					}
					else
					{
						this.scheduleBuilder = new ScheduleBuilder(Schedule.Never);
					}
					this.gridControl.Invalidate(true);
				}
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060018DB RID: 6363 RVA: 0x0006B38C File Offset: 0x0006958C
		// (set) Token: 0x060018DC RID: 6364 RVA: 0x0006B394 File Offset: 0x00069594
		[DefaultValue(false)]
		public bool ShowIntervals
		{
			get
			{
				return this.showIntervals;
			}
			set
			{
				if (this.ShowIntervals != value)
				{
					this.showIntervals = value;
					this.hourMenuItem.Checked = !this.ShowIntervals;
					this.intervalMenuItem.Checked = this.ShowIntervals;
					this.LayoutIntervalButtons();
					this.AdjustScrollpanel();
					if (this.ShowIntervals)
					{
						this.selectionStart = new Point(this.selectionStart.X * 4, this.selectionStart.Y);
						this.selectionEnd = new Point(this.selectionEnd.X * 4 + 4 - 1, this.selectionEnd.Y);
					}
					else
					{
						this.selectionStart = new Point(this.selectionStart.X / 4, this.selectionStart.Y);
						this.selectionEnd = new Point(this.selectionEnd.X / 4, this.selectionEnd.Y);
					}
					this.gridHeaderControl.Invalidate(true);
					this.sunMoonControl.Invalidate(true);
					this.gridControl.Invalidate(true);
					this.EnsureCellVisible(this.selectionEnd);
				}
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060018DD RID: 6365 RVA: 0x0006B4AF File Offset: 0x000696AF
		private int NumberOfHCells
		{
			get
			{
				if (!this.ShowIntervals)
				{
					return 24;
				}
				return 96;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060018DE RID: 6366 RVA: 0x0006B4BE File Offset: 0x000696BE
		private int NumberOfVCells
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x0006B4C4 File Offset: 0x000696C4
		private Point CellFromGridXY(int x, int y)
		{
			return new Point(Math.Max(0, Math.Min(LayoutHelper.MirrorPosition(x, this.gridControl) / this.cellSize.Width, this.NumberOfHCells - 1)), Math.Max(0, Math.Min(y / this.cellSize.Height, this.NumberOfVCells - 1)));
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x0006B524 File Offset: 0x00069724
		private void EnsureCellVisible(Point cell)
		{
			if (cell.X >= 0 && cell.X < this.intervalButtonsPanel.Controls.Count)
			{
				this.scrollPanel.ScrollControlIntoView(this.intervalButtonsPanel.Controls[cell.X]);
				this.scrollPanel_Scroll(this.scrollPanel, new ScrollEventArgs(ScrollEventType.SmallIncrement, this.scrollPanel.HorizontalScroll.Value, ScrollOrientation.HorizontalScroll));
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060018E1 RID: 6369 RVA: 0x0006B599 File Offset: 0x00069799
		private Point SelectionTopLeft
		{
			get
			{
				return new Point(Math.Min(this.selectionStart.X, this.selectionEnd.X), Math.Min(this.selectionStart.Y, this.selectionEnd.Y));
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x0006B5D6 File Offset: 0x000697D6
		private Point SelectionRightBottom
		{
			get
			{
				return new Point(Math.Max(this.selectionStart.X, this.selectionEnd.X), Math.Max(this.selectionStart.Y, this.selectionEnd.Y));
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060018E3 RID: 6371 RVA: 0x0006B77C File Offset: 0x0006997C
		private IEnumerable SelectedCells
		{
			get
			{
				Point selectionTopLeft = this.SelectionTopLeft;
				Point selectionRightBottom = this.SelectionRightBottom;
				for (int day = selectionTopLeft.Y; day <= selectionRightBottom.Y; day++)
				{
					for (int interval = selectionTopLeft.X; interval <= selectionRightBottom.X; interval++)
					{
						yield return new Point(interval, day);
					}
				}
				yield break;
			}
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x0006B799 File Offset: 0x00069999
		private bool GetCellState(Point cell)
		{
			if (!this.ShowIntervals)
			{
				return this.scheduleBuilder.GetStateOfHour((DayOfWeek)cell.Y, cell.X);
			}
			return this.scheduleBuilder.GetStateOfInterval((DayOfWeek)cell.Y, cell.X);
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x0006B7D6 File Offset: 0x000699D6
		private void SetCellState(Point cell, bool state)
		{
			if (this.ShowIntervals)
			{
				this.scheduleBuilder.SetStateOfInterval((DayOfWeek)cell.Y, cell.X, state);
				return;
			}
			this.scheduleBuilder.SetStateOfHour((DayOfWeek)cell.Y, cell.X, state);
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x0006B818 File Offset: 0x00069A18
		private void SetSelectedCellsState(bool cellState)
		{
			foreach (object obj in this.SelectedCells)
			{
				Point cell = (Point)obj;
				this.SetCellState(cell, cellState);
			}
		}

		// Token: 0x04000934 RID: 2356
		private Point selectionStart;

		// Token: 0x04000935 RID: 2357
		private Point selectionEnd;

		// Token: 0x04000936 RID: 2358
		private bool isKeyboardMoveCellFocus;

		// Token: 0x04000937 RID: 2359
		private AutoTableLayoutPanel contentPanel;

		// Token: 0x04000938 RID: 2360
		private OwnerDrawControl hintLabel;

		// Token: 0x04000939 RID: 2361
		private Panel scrollPanel;

		// Token: 0x0400093A RID: 2362
		private OwnerDrawControl gridControl;

		// Token: 0x0400093B RID: 2363
		private FlowLayoutPanel intervalButtonsPanel;

		// Token: 0x0400093C RID: 2364
		private TableLayoutPanel dayButtonsPanel;

		// Token: 0x0400093D RID: 2365
		private OwnerDrawControl gridHeaderControl;

		// Token: 0x0400093E RID: 2366
		private OwnerDrawControl sunMoonControl;

		// Token: 0x0400093F RID: 2367
		private Panel detailViewPanel;

		// Token: 0x04000940 RID: 2368
		private TableLayoutPanel detailViewTableLayoutPanel;

		// Token: 0x04000941 RID: 2369
		private AutoHeightRadioButton intervalRadioButton;

		// Token: 0x04000942 RID: 2370
		private AutoHeightRadioButton hourRadioButton;

		// Token: 0x04000943 RID: 2371
		private GroupBox detailViewGroupBox;

		// Token: 0x04000944 RID: 2372
		private static string[] dayTexts;

		// Token: 0x04000945 RID: 2373
		private MenuItem hourMenuItem;

		// Token: 0x04000946 RID: 2374
		private MenuItem intervalMenuItem;

		// Token: 0x04000947 RID: 2375
		private ContextMenu contextMenu;

		// Token: 0x04000948 RID: 2376
		private Size cellSize = new Size(13, 0);

		// Token: 0x04000949 RID: 2377
		private Dictionary<string, Size> textSizes = new Dictionary<string, Size>();

		// Token: 0x0400094A RID: 2378
		private Size hourIndicatorSize = new Size(3, 3);

		// Token: 0x0400094B RID: 2379
		private static Size iconSize = new Size(16, 16);

		// Token: 0x0400094C RID: 2380
		private SolidBrush hourIndicatorBrush;

		// Token: 0x0400094D RID: 2381
		private Pen hourIndicatorPen;

		// Token: 0x0400094E RID: 2382
		private SolidBrush gridBrush;

		// Token: 0x0400094F RID: 2383
		private Point newMousePositionInScrollPanel = new Point(-1, -1);

		// Token: 0x04000950 RID: 2384
		private Point oldMousePositionInScrollPanel = new Point(-1, -1);

		// Token: 0x04000951 RID: 2385
		private ScheduleBuilder scheduleBuilder;

		// Token: 0x04000952 RID: 2386
		private bool showIntervals;
	}
}
