using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Management.SystemManager.WinForms.Design;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200021B RID: 539
	[Designer(typeof(WorkCenterDesigner))]
	public class WorkCenter : ContainerResultPane
	{
		// Token: 0x0600187A RID: 6266 RVA: 0x00067D18 File Offset: 0x00065F18
		public WorkCenter()
		{
			this.InitializeComponent();
			this.splitContainer.MinimumSize = new Size(0, this.splitContainer.Panel1MinSize + this.splitContainer.Panel2MinSize + this.splitContainer.SplitterWidth);
			this.bottomResultPane.StatusChanged += this.BottomResultPane_StatusChanged;
			this.BottomResultPane_StatusChanged(this.bottomResultPane, EventArgs.Empty);
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x00067D8D File Offset: 0x00065F8D
		public override bool HasPermission()
		{
			return this.TopPanelResultPane.HasPermission();
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x00067D9C File Offset: 0x00065F9C
		private void InitializeComponent()
		{
			this.splitContainer = new SplitContainer();
			this.bottomResultPane = new TabbedResultPane();
			this.bottomResultPane.IsCaptionVisible = false;
			this.separator = new Panel();
			this.bottomPanelCaption = new ResultPaneCaption();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			base.SuspendLayout();
			this.splitContainer.Dock = DockStyle.Fill;
			this.splitContainer.Location = new Point(0, 22);
			this.splitContainer.Name = "splitContainer";
			this.splitContainer.Orientation = Orientation.Horizontal;
			this.splitContainer.Panel1MinSize = 120;
			this.splitContainer.Panel2MinSize = 120;
			this.splitContainer.Panel2.Controls.Add(this.bottomResultPane);
			this.splitContainer.Panel2.Controls.Add(this.separator);
			this.splitContainer.Panel2.Controls.Add(this.bottomPanelCaption);
			this.splitContainer.Size = new Size(400, 378);
			this.splitContainer.SplitterDistance = 143;
			this.splitContainer.TabIndex = 0;
			this.bottomResultPane.Dock = DockStyle.Fill;
			this.bottomResultPane.Location = new Point(0, 27);
			this.bottomResultPane.Name = "workCenter_TabbedResultPane";
			this.bottomResultPane.TabIndex = 0;
			this.separator.Dock = DockStyle.Top;
			this.separator.Location = new Point(0, 22);
			this.separator.Name = "separator";
			this.separator.Size = new Size(400, 5);
			this.separator.TabIndex = 0;
			this.separator.Paint += this.separator_Paint;
			this.bottomPanelCaption.AutoSize = true;
			this.bottomPanelCaption.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.bottomPanelCaption.BaseFont = new Font("Verdana", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.bottomPanelCaption.Dock = DockStyle.Top;
			this.bottomPanelCaption.Location = new Point(0, 0);
			this.bottomPanelCaption.Name = "bottomPanelCaption";
			this.bottomPanelCaption.TabIndex = 0;
			this.bottomPanelCaption.TabStop = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.splitContainer);
			base.Name = "WorkCenter";
			base.Controls.SetChildIndex(this.splitContainer, 0);
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x0006806A File Offset: 0x0006626A
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.bottomResultPane.StatusChanged -= this.BottomResultPane_StatusChanged;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x0006808D File Offset: 0x0006628D
		private void separator_Paint(object sender, PaintEventArgs e)
		{
			ControlPaint.DrawBorder3D(e.Graphics, 0, 0, this.separator.Width, this.separator.Height - 2, Border3DStyle.SunkenOuter, Border3DSide.Top | Border3DSide.Bottom);
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x000680B8 File Offset: 0x000662B8
		private void ResultPane_Enter(object sender, EventArgs e)
		{
			AbstractResultPane selectedResultPane = sender as AbstractResultPane;
			base.SelectedResultPane = selectedResultPane;
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x000680D3 File Offset: 0x000662D3
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Category("Result Pane")]
		public Control TopPanel
		{
			get
			{
				return this.splitContainer.Panel1;
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x000680E0 File Offset: 0x000662E0
		// (set) Token: 0x06001882 RID: 6274 RVA: 0x000680E8 File Offset: 0x000662E8
		[Category("ResultPane")]
		[DefaultValue(null)]
		public CaptionedResultPane TopPanelResultPane
		{
			get
			{
				return this.topPanelResultPane;
			}
			set
			{
				if (this.TopPanelResultPane != value)
				{
					if (this.TopPanelResultPane != null)
					{
						this.TopPanelResultPane.Enter -= this.ResultPane_Enter;
						this.BottomPanelResultPane.Enter -= this.ResultPane_Enter;
						this.TopPanel.Controls.Remove(this.TopPanelResultPane);
						base.ResultPanes.Remove(this.BottomPanelResultPane);
						base.ResultPanes.Remove(this.TopPanelResultPane);
						this.TopPanelResultPane.DependentResultPanes.Remove(this.BottomPanelResultPane);
						this.TopPanelResultPane.SelectionChanged -= this.TopPanelResultPane_SelectionChanged;
					}
					this.topPanelResultPane = value;
					if (this.TopPanelResultPane != null)
					{
						this.TopPanelResultPane.SelectionChanged += this.TopPanelResultPane_SelectionChanged;
						this.TopPanelResultPane.DependentResultPanes.Add(this.BottomPanelResultPane);
						base.ResultPanes.Add(this.TopPanelResultPane);
						base.ResultPanes.Add(this.BottomPanelResultPane);
						this.TopPanelResultPane.Dock = DockStyle.Fill;
						this.TopPanel.Controls.Add(this.TopPanelResultPane);
						this.TopPanelResultPane.Enter += this.ResultPane_Enter;
						this.BottomPanelResultPane.Enter += this.ResultPane_Enter;
						if (base.IsActive)
						{
							this.TopPanelResultPane.OnSetActive();
							this.BottomPanelResultPane.OnSetActive();
						}
						else
						{
							base.ResultPanesActiveToContainer.Add(this.TopPanelResultPane);
							base.ResultPanesActiveToContainer.Add(this.BottomPanelResultPane);
						}
						base.SelectedResultPane = this.TopPanelResultPane;
					}
				}
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x000682A1 File Offset: 0x000664A1
		// (set) Token: 0x06001884 RID: 6276 RVA: 0x000682AE File Offset: 0x000664AE
		[Category("Result Pane")]
		[DefaultValue(null)]
		public Icon WorkPaneIcon
		{
			get
			{
				return this.bottomPanelCaption.Icon;
			}
			set
			{
				this.bottomPanelCaption.Icon = value;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x000682BC File Offset: 0x000664BC
		// (set) Token: 0x06001886 RID: 6278 RVA: 0x000682C9 File Offset: 0x000664C9
		[DefaultValue("")]
		[Category("Result Pane")]
		public string WorkPaneText
		{
			get
			{
				return this.bottomPanelCaption.Text;
			}
			set
			{
				this.bottomPanelCaption.Text = value;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001887 RID: 6279 RVA: 0x000682D7 File Offset: 0x000664D7
		internal WorkPaneTabs TabControl
		{
			get
			{
				return this.bottomResultPane.TabControl;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001888 RID: 6280 RVA: 0x000682E4 File Offset: 0x000664E4
		internal TabbedResultPane BottomPanelResultPane
		{
			get
			{
				return this.bottomResultPane;
			}
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x000682EC File Offset: 0x000664EC
		private void BottomResultPane_StatusChanged(object sender, EventArgs e)
		{
			this.bottomPanelCaption.Status = this.bottomResultPane.Status;
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x00068304 File Offset: 0x00066504
		[Category("Result Pane")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public BindingList<AbstractResultPane> WorkPanePages
		{
			get
			{
				return this.BottomPanelResultPane.ResultPaneTabs;
			}
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x00068311 File Offset: 0x00066511
		protected override void OnSelectedResultPaneChanged(EventArgs e)
		{
			if (base.IsActive && base.SelectedResultPane != null && base.SelectedResultPane.Enabled)
			{
				base.ActiveControl = base.SelectedResultPane;
			}
			base.OnSelectedResultPaneChanged(e);
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x00068343 File Offset: 0x00066543
		protected override void OnSetActive(EventArgs e)
		{
			base.OnSetActive(e);
			if (base.SelectedResultPane != null && base.SelectedResultPane.Enabled)
			{
				base.ActiveControl = base.SelectedResultPane;
			}
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x00068370 File Offset: 0x00066570
		private void TopPanelResultPane_SelectionChanged(object sender, EventArgs e)
		{
			if (this.TopPanelResultPane.HasSelection)
			{
				string text = this.TopPanelResultPane.SelectionDataObject.GetText();
				this.WorkPaneText = text;
				if (this.WorkPaneIcon == null)
				{
					this.WorkPaneIcon = this.oldWorkPaneIcon;
					return;
				}
			}
			else
			{
				if (this.WorkPaneIcon != null)
				{
					this.oldWorkPaneIcon = this.WorkPaneIcon;
				}
				this.WorkPaneIcon = null;
				this.WorkPaneText = string.Empty;
			}
		}

		// Token: 0x0400092C RID: 2348
		private Panel separator;

		// Token: 0x0400092D RID: 2349
		private TabbedResultPane bottomResultPane;

		// Token: 0x0400092E RID: 2350
		private ResultPaneCaption bottomPanelCaption;

		// Token: 0x0400092F RID: 2351
		private SplitContainer splitContainer;

		// Token: 0x04000930 RID: 2352
		private CaptionedResultPane topPanelResultPane;

		// Token: 0x04000931 RID: 2353
		private Icon oldWorkPaneIcon;
	}
}
