using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001B1 RID: 433
	[Designer(typeof(ScrollableControlDesigner))]
	public class CaptionedResultPane : DataListViewResultPane
	{
		// Token: 0x06001172 RID: 4466 RVA: 0x00045291 File Offset: 0x00043491
		public CaptionedResultPane() : this(null, null)
		{
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x0004529B File Offset: 0x0004349B
		public CaptionedResultPane(IResultsLoaderConfiguration config) : this((config != null) ? config.BuildResultsLoaderProfile() : null, null)
		{
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x000452B0 File Offset: 0x000434B0
		public CaptionedResultPane(DataTableLoader loader) : this((loader != null) ? loader.ResultsLoaderProfile : null, loader)
		{
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x000452C5 File Offset: 0x000434C5
		public CaptionedResultPane(ObjectPickerProfileLoader profileLoader, string profileName) : this(profileLoader.GetProfile(profileName))
		{
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x000452D4 File Offset: 0x000434D4
		public CaptionedResultPane(ResultsLoaderProfile profile) : this(profile, null)
		{
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x000452DE File Offset: 0x000434DE
		protected CaptionedResultPane(ResultsLoaderProfile profile, DataTableLoader loader) : base(profile, loader)
		{
			this.InitializeComponent();
			base.Name = "CaptionedResultPane";
			if (base.ResultsLoaderProfile != null)
			{
				this.CaptionText = base.ResultsLoaderProfile.DisplayName;
			}
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00045312 File Offset: 0x00043512
		protected override void OnStatusChanged(EventArgs e)
		{
			base.OnStatusChanged(e);
			this.caption.Status = base.Status;
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x0004532C File Offset: 0x0004352C
		protected override void OnIconChanged(EventArgs e)
		{
			base.OnIconChanged(e);
			this.caption.Icon = base.Icon;
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x00045346 File Offset: 0x00043546
		// (set) Token: 0x0600117B RID: 4475 RVA: 0x00045353 File Offset: 0x00043553
		[DefaultValue("")]
		[Category("Result Pane")]
		public string CaptionText
		{
			get
			{
				return this.caption.Text;
			}
			set
			{
				this.caption.Text = value;
			}
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00045361 File Offset: 0x00043561
		protected override void OnLayout(LayoutEventArgs e)
		{
			this.caption.SendToBack();
			base.OnLayout(e);
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00045378 File Offset: 0x00043578
		private void InitializeComponent()
		{
			this.caption = new ResultPaneCaption();
			base.SuspendLayout();
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
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.caption);
			base.Name = "RootResultPane";
			base.Size = new Size(400, 400);
			base.ResumeLayout(false);
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x00045485 File Offset: 0x00043685
		// (set) Token: 0x0600117F RID: 4479 RVA: 0x00045492 File Offset: 0x00043692
		[DefaultValue(true)]
		public bool ShowCaption
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

		// Token: 0x040006B5 RID: 1717
		private ResultPaneCaption caption;
	}
}
