using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000225 RID: 549
	public class TabbedResultPane : ContainerResultPane
	{
		// Token: 0x06001926 RID: 6438 RVA: 0x0006D7F4 File Offset: 0x0006B9F4
		public TabbedResultPane()
		{
			this.InitializeComponent();
			this.tabControl.Multiline = true;
			this.tabControl.SizeMode = TabSizeMode.FillToRight;
			this.ResultPaneTabs.ListChanged += this.ResultPaneTabs_ListChanged;
			this.tabControl.SelectedIndexChanged += this.TabControl_SelectedIndexChanged;
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x0006D860 File Offset: 0x0006BA60
		private void InitializeComponent()
		{
			this.tabControl = new WorkPaneTabs();
			this.caption = new ResultPaneCaption();
			base.SuspendLayout();
			this.tabControl.SuspendLayout();
			this.tabControl.Dock = DockStyle.Fill;
			this.tabControl.Name = "tabControl";
			this.tabControl.Enabled = true;
			this.caption.AutoSize = true;
			this.caption.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.caption.BackColor = SystemColors.ControlDark;
			this.caption.BaseFont = new Font("Verdana", 9.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.caption.Dock = DockStyle.Top;
			this.caption.ForeColor = SystemColors.ControlLightLight;
			this.caption.Location = new Point(0, 0);
			this.caption.Name = "caption";
			this.caption.TabIndex = 0;
			this.caption.TabStop = false;
			base.Controls.Add(this.caption);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.tabControl);
			base.Name = "TabbedResultPane";
			base.Controls.SetChildIndex(this.tabControl, 0);
			this.tabControl.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x0006D9C5 File Offset: 0x0006BBC5
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.ResultPaneTabs.ListChanged -= this.ResultPaneTabs_ListChanged;
				this.tabControl.SelectedIndexChanged -= this.TabControl_SelectedIndexChanged;
			}
			base.Dispose(disposing);
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06001929 RID: 6441 RVA: 0x0006D9FF File Offset: 0x0006BBFF
		// (set) Token: 0x0600192A RID: 6442 RVA: 0x0006DA0C File Offset: 0x0006BC0C
		[DefaultValue(true)]
		public bool IsCaptionVisible
		{
			get
			{
				return this.caption.Visible;
			}
			set
			{
				this.caption.Visible = value;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x0600192B RID: 6443 RVA: 0x0006DA1A File Offset: 0x0006BC1A
		// (set) Token: 0x0600192C RID: 6444 RVA: 0x0006DA27 File Offset: 0x0006BC27
		[Category("Result Pane")]
		[DefaultValue("")]
		public string CaptionText
		{
			get
			{
				return this.caption.Text;
			}
			set
			{
				if (this.CaptionText != value)
				{
					this.caption.Text = value;
					this.OnCaptionTextChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x0600192D RID: 6445 RVA: 0x0006DA50 File Offset: 0x0006BC50
		protected virtual void OnCaptionTextChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TabbedResultPane.EventCaptionTextChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x140000A8 RID: 168
		// (add) Token: 0x0600192E RID: 6446 RVA: 0x0006DA7E File Offset: 0x0006BC7E
		// (remove) Token: 0x0600192F RID: 6447 RVA: 0x0006DA91 File Offset: 0x0006BC91
		public event EventHandler CaptionTextChanged
		{
			add
			{
				base.Events.AddHandler(TabbedResultPane.EventCaptionTextChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(TabbedResultPane.EventCaptionTextChanged, value);
			}
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x0006DAA4 File Offset: 0x0006BCA4
		protected override void OnStatusChanged(EventArgs e)
		{
			this.caption.Status = base.Status;
			base.OnStatusChanged(e);
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x0006DABE File Offset: 0x0006BCBE
		protected override void OnIconChanged(EventArgs e)
		{
			base.OnIconChanged(e);
			this.caption.Icon = base.Icon;
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001932 RID: 6450 RVA: 0x0006DAD8 File Offset: 0x0006BCD8
		internal WorkPaneTabs TabControl
		{
			get
			{
				return this.tabControl;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001933 RID: 6451 RVA: 0x0006DAE0 File Offset: 0x0006BCE0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Result Pane")]
		public BindingList<AbstractResultPane> ResultPaneTabs
		{
			get
			{
				return this.resultPaneTabs;
			}
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x0006DAE8 File Offset: 0x0006BCE8
		private void ResultPaneTabs_ListChanged(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.ItemAdded)
			{
				this.ResultPaneTabs_ResultPaneAdded(e.NewIndex);
				return;
			}
			if (e.ListChangedType == ListChangedType.ItemDeleted)
			{
				this.ResultPaneTabs_ResultPaneRemoved(e.NewIndex);
				return;
			}
			if (e.ListChangedType == ListChangedType.Reset)
			{
				this.ResultPaneTabs_ResultPaneResetted();
			}
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x0006DB24 File Offset: 0x0006BD24
		private void ResultPaneTabs_ResultPaneAdded(int index)
		{
			base.ResultPanes.Insert(index, this.ResultPaneTabs[index]);
			WorkPanePage workPanePage = new WorkPanePage();
			workPanePage.ResultPane = this.ResultPaneTabs[index];
			workPanePage.Name = "WorkPane" + this.ResultPaneTabs[index].Name;
			if (index == this.tabControl.TabPages.Count)
			{
				this.tabControl.TabPages.Add(workPanePage);
			}
			else
			{
				this.tabControl.TabPages.Insert(index, workPanePage);
			}
			if (this.tabControl.SelectedIndex == -1)
			{
				this.tabControl.SelectedIndex = 0;
			}
			this.TabControl_SelectedIndexChanged(this.tabControl, EventArgs.Empty);
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x0006DBE8 File Offset: 0x0006BDE8
		private void ResultPaneTabs_ResultPaneRemoved(int index)
		{
			if (index == this.tabControl.SelectedIndex)
			{
				if (this.tabControl.TabPages.Count != 1)
				{
					this.tabControl.SelectedIndex = ((index == 0) ? 1 : (index - 1));
				}
				else
				{
					this.tabControl.SelectedIndex = -1;
				}
				this.TabControl_SelectedIndexChanged(this.tabControl, EventArgs.Empty);
			}
			this.tabControl.TabPages.RemoveAt(index);
			base.ResultPanes.RemoveAt(index);
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x0006DC68 File Offset: 0x0006BE68
		private void ResultPaneTabs_ResultPaneResetted()
		{
			while (base.ResultPanes.Count > 0)
			{
				this.ResultPaneTabs_ResultPaneRemoved(base.ResultPanes.Count - 1);
			}
			for (int i = 0; i < this.ResultPaneTabs.Count; i++)
			{
				this.ResultPaneTabs_ResultPaneAdded(i);
			}
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x0006DCB8 File Offset: 0x0006BEB8
		protected override void OnSelectedResultPaneChanged(EventArgs e)
		{
			base.SuspendLayout();
			if (this.tabControl.SelectedResultPane != base.SelectedResultPane)
			{
				AbstractResultPane selectedResultPane = this.tabControl.SelectedResultPane;
				int selectedIndex = base.ResultPanes.IndexOf(base.SelectedResultPane);
				this.tabControl.SelectedIndex = selectedIndex;
				if (selectedResultPane != null)
				{
					if (base.IsActive)
					{
						selectedResultPane.OnKillActive();
					}
					else
					{
						base.ResultPanesActiveToContainer.Remove(selectedResultPane);
					}
				}
			}
			base.ResumeLayout(true);
			base.OnSelectedResultPaneChanged(e);
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x0006DD38 File Offset: 0x0006BF38
		private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.tabControl.SelectedResultPane != base.SelectedResultPane)
			{
				AbstractResultPane selectedResultPane = base.SelectedResultPane;
				base.SelectedResultPane = this.tabControl.SelectedResultPane;
				if (selectedResultPane != null)
				{
					if (base.IsActive)
					{
						selectedResultPane.OnKillActive();
						return;
					}
					base.ResultPanesActiveToContainer.Remove(selectedResultPane);
				}
			}
		}

		// Token: 0x04000975 RID: 2421
		private ResultPaneCaption caption;

		// Token: 0x04000976 RID: 2422
		private WorkPaneTabs tabControl;

		// Token: 0x04000977 RID: 2423
		private static readonly object EventCaptionTextChanged = new object();

		// Token: 0x04000978 RID: 2424
		private BindingList<AbstractResultPane> resultPaneTabs = new BindingList<AbstractResultPane>();
	}
}
