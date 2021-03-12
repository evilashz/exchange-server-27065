using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001BC RID: 444
	public class TridentsWizardPage : TitleWizardPage
	{
		// Token: 0x06001230 RID: 4656 RVA: 0x000490C0 File Offset: 0x000472C0
		public TridentsWizardPage()
		{
			this.InitializeComponent();
			base.ContentPanel.SuspendLayout();
			this.infoLabel.Text = "";
			this.statusLabel.Text = "";
			this.elapsedTimeLabel.Text = "";
			this.copyNoteLabel.Text = Strings.CompletionNote;
			base.ContentPanel.ResumeLayout();
			this.infoIcon.Image = IconLibrary.ToSmallBitmap(Icons.Information);
			this.workUnitsPanel.TaskStateChanged += this.workUnitsPanel_TaskStateChanged;
			this.updateTimer = new Timer();
			this.updateTimer.Interval = 1000;
			this.updateTimer.Tick += this.updateTimer_Tick;
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00049192 File Offset: 0x00047392
		private void workUnitsPanel_TaskStateChanged(object sender, EventArgs e)
		{
			this.updateTimer.Enabled = (this.WorkUnitsPanel.TaskState == 1);
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x000491AD File Offset: 0x000473AD
		private void updateTimer_Tick(object sender, EventArgs e)
		{
			this.ElapsedTimeText = ((this.WorkUnitsPanel.TaskState == null) ? string.Empty : this.WorkUnits.ElapsedTimeText);
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x000491D4 File Offset: 0x000473D4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.WorkUnits = null;
				this.updateTimer.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x000491F4 File Offset: 0x000473F4
		protected void AppendControlAboveCopyNotesLabel(Control control)
		{
			if (control != null)
			{
				this.configurationPanel.SuspendLayout();
				int row = this.configurationPanel.GetRow(this.copyNoteLabel);
				this.configurationPanel.RowCount++;
				this.configurationPanel.RowStyles.Add(new RowStyle());
				this.configurationPanel.SetRow(this.copyNoteLabel, row + 1);
				this.configurationPanel.Controls.Add(control, 0, row);
				this.configurationPanel.ResumeLayout(false);
				this.configurationPanel.PerformLayout();
			}
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0004928C File Offset: 0x0004748C
		protected void AppendControlToPanel(Control control)
		{
			if (control != null)
			{
				this.configurationPanel.SuspendLayout();
				this.configurationPanel.RowCount++;
				this.configurationPanel.RowStyles.Add(new RowStyle());
				this.configurationPanel.Controls.Add(control, 0, this.configurationPanel.RowCount - 1);
				this.configurationPanel.ResumeLayout(false);
				this.configurationPanel.PerformLayout();
			}
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00049308 File Offset: 0x00047508
		private void InitializeComponent()
		{
			this.infoPanel = new AutoTableLayoutPanel();
			this.infoIcon = new ExchangePictureBox();
			this.infoLabel = new Label();
			this.workUnitsPanel = new WorkUnitsPanel();
			this.statusLabel = new Label();
			this.copyNoteLabel = new Label();
			this.elapsedTimeLabel = new AutoHeightLabel();
			this.configurationPanel = new AutoTableLayoutPanel();
			this.configurationPanel.SuspendLayout();
			base.ContentPanel.SuspendLayout();
			((ISupportInitialize)base.BindingSource).BeginInit();
			this.infoPanel.SuspendLayout();
			((ISupportInitialize)this.infoIcon).BeginInit();
			base.SuspendLayout();
			base.ContentPanel.Controls.Add(this.configurationPanel);
			base.ContentPanel.Location = new Point(0, 44);
			base.ContentPanel.Size = new Size(454, 354);
			base.InputValidationProvider.SetEnabled(base.BindingSource, true);
			this.configurationPanel.ColumnCount = 1;
			this.configurationPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.configurationPanel.Controls.Add(this.statusLabel, 0, 0);
			this.configurationPanel.Controls.Add(this.workUnitsPanel, 0, 1);
			this.configurationPanel.Controls.Add(this.copyNoteLabel, 0, 2);
			this.configurationPanel.Dock = DockStyle.Fill;
			this.configurationPanel.Name = "configurationPanel";
			this.configurationPanel.Padding = new Padding(0, 0, 16, 0);
			this.configurationPanel.RowCount = 3;
			this.configurationPanel.RowStyles.Add(new RowStyle());
			this.configurationPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.configurationPanel.RowStyles.Add(new RowStyle());
			this.infoPanel.AutoLayout = false;
			this.infoPanel.AutoSize = true;
			this.infoPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.infoPanel.ColumnCount = 2;
			this.infoPanel.ColumnStyles.Add(new ColumnStyle());
			this.infoPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.infoPanel.ContainerType = ContainerType.Control;
			this.infoPanel.Controls.Add(this.infoIcon, 0, 0);
			this.infoPanel.Controls.Add(this.infoLabel, 1, 0);
			this.infoPanel.Dock = DockStyle.Top;
			this.infoPanel.Location = new Point(0, 0);
			this.infoPanel.Name = "infoPanel";
			this.infoPanel.Padding = new Padding(0, 0, 16, 0);
			this.infoPanel.RowCount = 1;
			this.infoPanel.RowStyles.Add(new RowStyle());
			this.infoPanel.Size = new Size(454, 28);
			this.infoPanel.TabIndex = 3;
			this.infoPanel.Visible = false;
			this.infoIcon.Location = new Point(3, 3);
			this.infoIcon.Name = "infoIcon";
			this.infoIcon.Size = new Size(16, 16);
			this.infoIcon.TabIndex = 0;
			this.infoIcon.TabStop = false;
			this.infoLabel.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
			this.infoLabel.AutoSize = true;
			this.infoLabel.Location = new Point(25, 3);
			this.infoLabel.Margin = new Padding(3, 3, 3, 12);
			this.infoLabel.Name = "infoLabel";
			this.infoLabel.Size = new Size(410, 13);
			this.infoLabel.TabIndex = 1;
			this.infoLabel.Text = "[info]";
			this.workUnitsPanel.BackColor = Color.Transparent;
			this.workUnitsPanel.Dock = DockStyle.Fill;
			this.workUnitsPanel.Location = new Point(0, 17);
			this.workUnitsPanel.Name = "workUnitsPanel";
			this.workUnitsPanel.Size = new Size(438, 324);
			this.workUnitsPanel.TabIndex = 0;
			this.statusLabel.AutoSize = true;
			this.statusLabel.Dock = DockStyle.Top;
			this.statusLabel.Location = new Point(0, 0);
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Margin = new Padding(0, 0, 0, 4);
			this.statusLabel.Size = new Size(57, 17);
			this.statusLabel.TabIndex = 1;
			this.statusLabel.Text = "[status]";
			this.copyNoteLabel.AutoSize = true;
			this.copyNoteLabel.Dock = DockStyle.Bottom;
			this.copyNoteLabel.Location = new Point(0, 337);
			this.copyNoteLabel.Name = "copyNoteLabel";
			this.copyNoteLabel.Margin = new Padding(0, 4, 0, 0);
			this.copyNoteLabel.Size = new Size(52, 17);
			this.copyNoteLabel.TabIndex = 0;
			this.copyNoteLabel.Text = "[copy]";
			this.elapsedTimeLabel.Dock = DockStyle.Top;
			this.elapsedTimeLabel.LinkArea = new LinkArea(0, 0);
			this.elapsedTimeLabel.Location = new Point(0, 28);
			this.elapsedTimeLabel.MinimumSize = new Size(0, 16);
			this.elapsedTimeLabel.Name = "elapsedTimeLabel";
			this.elapsedTimeLabel.Padding = new Padding(0, 0, 16, 0);
			this.elapsedTimeLabel.Size = new Size(454, 16);
			this.elapsedTimeLabel.TabIndex = 2;
			this.elapsedTimeLabel.Text = "[elapsedTimeLabel]";
			base.Controls.Add(this.elapsedTimeLabel);
			base.Controls.Add(this.infoPanel);
			base.Name = "TridentsWizardPage";
			base.Controls.SetChildIndex(this.infoPanel, 0);
			base.Controls.SetChildIndex(this.elapsedTimeLabel, 0);
			base.Controls.SetChildIndex(base.ContentPanel, 0);
			this.configurationPanel.ResumeLayout(false);
			this.configurationPanel.PerformLayout();
			base.ContentPanel.ResumeLayout(false);
			base.ContentPanel.PerformLayout();
			((ISupportInitialize)base.BindingSource).EndInit();
			this.infoPanel.ResumeLayout(false);
			this.infoPanel.PerformLayout();
			((ISupportInitialize)this.infoIcon).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x000499C4 File Offset: 0x00047BC4
		public WorkUnitsPanel WorkUnitsPanel
		{
			get
			{
				return this.workUnitsPanel;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x000499DA File Offset: 0x00047BDA
		// (set) Token: 0x06001238 RID: 4664 RVA: 0x000499CC File Offset: 0x00047BCC
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue("")]
		public string Status
		{
			get
			{
				return this.statusLabel.Text;
			}
			set
			{
				this.statusLabel.Text = value;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x000499E7 File Offset: 0x00047BE7
		// (set) Token: 0x0600123B RID: 4667 RVA: 0x000499F4 File Offset: 0x00047BF4
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.All)]
		public string ElapsedTimeText
		{
			get
			{
				return this.elapsedTimeLabel.Text;
			}
			set
			{
				this.elapsedTimeLabel.Text = value;
				this.elapsedTimeLabel.Links.Clear();
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x0600123C RID: 4668 RVA: 0x00049A12 File Offset: 0x00047C12
		// (set) Token: 0x0600123D RID: 4669 RVA: 0x00049A24 File Offset: 0x00047C24
		[DefaultValue(null)]
		internal WorkUnitCollection WorkUnits
		{
			get
			{
				return (WorkUnitCollection)this.workUnitsPanel.WorkUnits;
			}
			set
			{
				if (value != this.WorkUnits)
				{
					if (this.WorkUnits != null)
					{
						this.WorkUnits.ListChanged -= this.WorkUnits_ListChanged;
						this.statusLabel.Text = "";
					}
					this.workUnitsPanel.WorkUnits = value;
					if (this.WorkUnits != null)
					{
						this.WorkUnits.ListChanged += this.WorkUnits_ListChanged;
						this.WorkUnits_ListChanged(this.WorkUnits, new ListChangedEventArgs(ListChangedType.Reset, -1));
					}
				}
			}
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00049AA8 File Offset: 0x00047CA8
		private void WorkUnits_ListChanged(object sender, ListChangedEventArgs e)
		{
			if (e.PropertyDescriptor == null || e.PropertyDescriptor.Name == "Status")
			{
				if (base.InvokeRequired)
				{
					base.Invoke(new ListChangedEventHandler(this.WorkUnits_ListChanged), new object[]
					{
						sender,
						e
					});
					return;
				}
				this.statusLabel.Text = this.WorkUnits.Description;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x00049B1B File Offset: 0x00047D1B
		// (set) Token: 0x06001240 RID: 4672 RVA: 0x00049B28 File Offset: 0x00047D28
		[DefaultValue("")]
		public string InformationDescription
		{
			get
			{
				return this.infoLabel.Text;
			}
			set
			{
				this.infoLabel.Text = value;
				this.UpdateChildrenVisible();
			}
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x00049B3C File Offset: 0x00047D3C
		public virtual string GetSummaryText()
		{
			return this.WorkUnitsPanel.GetSummaryText();
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x00049B49 File Offset: 0x00047D49
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.UpdateChildrenVisible();
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00049B58 File Offset: 0x00047D58
		private void UpdateChildrenVisible()
		{
			if (base.IsHandleCreated)
			{
				this.infoPanel.Visible = !string.IsNullOrEmpty(this.InformationDescription);
			}
		}

		// Token: 0x040006EE RID: 1774
		private Label statusLabel;

		// Token: 0x040006EF RID: 1775
		private Label copyNoteLabel;

		// Token: 0x040006F0 RID: 1776
		[AccessedThroughProperty("WorkUnitsPanel")]
		private WorkUnitsPanel workUnitsPanel;

		// Token: 0x040006F1 RID: 1777
		private AutoTableLayoutPanel infoPanel;

		// Token: 0x040006F2 RID: 1778
		private ExchangePictureBox infoIcon;

		// Token: 0x040006F3 RID: 1779
		private AutoHeightLabel elapsedTimeLabel;

		// Token: 0x040006F4 RID: 1780
		private Label infoLabel;

		// Token: 0x040006F5 RID: 1781
		private Timer updateTimer;

		// Token: 0x040006F6 RID: 1782
		private AutoTableLayoutPanel configurationPanel;
	}
}
