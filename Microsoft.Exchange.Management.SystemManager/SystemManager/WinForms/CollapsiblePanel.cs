using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200018A RID: 394
	[DefaultProperty("Text")]
	[Designer(typeof(ScrollableControlDesigner))]
	[DefaultEvent("StatusClick")]
	public class CollapsiblePanel : AutoSizePanel
	{
		// Token: 0x06000F62 RID: 3938 RVA: 0x0003B76C File Offset: 0x0003996C
		public CollapsiblePanel()
		{
			this.InitializeComponent();
			this.BackColor = CollapsiblePanel.DefaultExpandedBackColor;
			this.chevron.Image = Icons.Collapse;
			this.UpdateBackground();
			Theme.UseVisualEffectsChanged += this.Theme_UseVisualEffectsChanged;
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0003B7BE File Offset: 0x000399BE
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				Theme.UseVisualEffectsChanged -= this.Theme_UseVisualEffectsChanged;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0003B7DB File Offset: 0x000399DB
		private void Theme_UseVisualEffectsChanged(object sender, EventArgs e)
		{
			base.Invalidate(true);
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x0003B7E4 File Offset: 0x000399E4
		protected override Size DefaultSize
		{
			get
			{
				return new Size(150, 31);
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x0003B7F2 File Offset: 0x000399F2
		// (set) Token: 0x06000F67 RID: 3943 RVA: 0x0003B807 File Offset: 0x00039A07
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Color BackColor
		{
			get
			{
				if (Theme.UseVisualEffects)
				{
					return base.BackColor;
				}
				return SystemColors.Window;
			}
			set
			{
				if (this.UseDefaultExpandedBackColor)
				{
					base.BackColor = value;
				}
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x0003B818 File Offset: 0x00039A18
		// (set) Token: 0x06000F69 RID: 3945 RVA: 0x0003B820 File Offset: 0x00039A20
		[DefaultValue(true)]
		[Browsable(false)]
		public bool UseDefaultExpandedBackColor
		{
			get
			{
				return this.useDefaultExpandedBackColor;
			}
			set
			{
				if (this.useDefaultExpandedBackColor != value)
				{
					this.useDefaultExpandedBackColor = value;
					base.BackColor = (this.useDefaultExpandedBackColor ? CollapsiblePanel.DefaultExpandedBackColor : Color.Transparent);
				}
			}
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0003B84C File Offset: 0x00039A4C
		private bool ShouldSerializeBackColor()
		{
			return false;
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0003B84F File Offset: 0x00039A4F
		public override void ResetBackColor()
		{
			this.BackColor = (this.IsMinimized ? CollapsiblePanel.DefaultMinimizedBackColor : CollapsiblePanel.DefaultExpandedBackColor);
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000F6C RID: 3948 RVA: 0x0003B86B File Offset: 0x00039A6B
		protected override Padding DefaultMargin
		{
			get
			{
				return CollapsiblePanel.defaultMargin;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x0003B872 File Offset: 0x00039A72
		protected override Size DefaultMinimumSize
		{
			get
			{
				return CollapsiblePanel.defaultMinimumSize;
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x0003B879 File Offset: 0x00039A79
		// (set) Token: 0x06000F6F RID: 3951 RVA: 0x0003B881 File Offset: 0x00039A81
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override Size MinimumSize
		{
			get
			{
				return base.MinimumSize;
			}
			set
			{
				base.MinimumSize = value;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x0003B88A File Offset: 0x00039A8A
		// (set) Token: 0x06000F71 RID: 3953 RVA: 0x0003B894 File Offset: 0x00039A94
		[DefaultValue(null)]
		[Category("Appearance")]
		public Icon Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				if (value != this.Icon)
				{
					Bitmap bitmap = IconLibrary.ToSmallBitmap(value);
					if (this.image.Image != null)
					{
						this.image.Image.Dispose();
					}
					this.image.Image = bitmap;
					this.icon = value;
				}
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x0003B8E1 File Offset: 0x00039AE1
		// (set) Token: 0x06000F73 RID: 3955 RVA: 0x0003B8EE File Offset: 0x00039AEE
		[DefaultValue("")]
		[Category("Appearance")]
		public string Status
		{
			get
			{
				return this.status.Text;
			}
			set
			{
				value = (value ?? "");
				if (this.status.Text != value)
				{
					this.status.LinkVisited = false;
					this.status.Text = value;
					this.PerformAlign();
				}
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x0003B92D File Offset: 0x00039B2D
		// (set) Token: 0x06000F75 RID: 3957 RVA: 0x0003B93A File Offset: 0x00039B3A
		[Category("Appearance")]
		[DefaultValue(true)]
		public bool StatusVisible
		{
			get
			{
				return this.status.Visible;
			}
			set
			{
				if (this.status.Visible != value)
				{
					this.status.Visible = value;
					this.PerformAlign();
				}
			}
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0003B95C File Offset: 0x00039B5C
		private void PerformAlign()
		{
			if (base.Parent != null)
			{
				base.SuspendLayout();
				base.Parent.PerformLayout(null, CollapsiblePanel.AlignLayout);
				base.ResumeLayout();
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x0003B983 File Offset: 0x00039B83
		protected ToolStrip CaptionStrip
		{
			get
			{
				return this.captionStrip;
			}
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0003B98C File Offset: 0x00039B8C
		internal int GetStatusWidth()
		{
			if (!this.StatusVisible)
			{
				return 0;
			}
			return this.status.GetPreferredSize(Size.Empty).Width;
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0003B9BB File Offset: 0x00039BBB
		internal void SetStatusWidth(int width)
		{
			if (width != this.status.Width)
			{
				this.CaptionStrip.SuspendLayout();
				this.status.Width = width;
				this.HideOverflowButtonOfCaptionStrip();
				this.CaptionStrip.ResumeLayout(true);
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x0003B9F4 File Offset: 0x00039BF4
		// (set) Token: 0x06000F7B RID: 3963 RVA: 0x0003BA01 File Offset: 0x00039C01
		[DefaultValue(null)]
		[Category("Appearance")]
		public Image StatusImage
		{
			get
			{
				return this.status.Image;
			}
			set
			{
				if (this.StatusImage != value)
				{
					this.status.LinkVisited = false;
					this.status.Image = value;
					this.PerformAlign();
				}
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x0003BA2A File Offset: 0x00039C2A
		// (set) Token: 0x06000F7D RID: 3965 RVA: 0x0003BA37 File Offset: 0x00039C37
		[DefaultValue(false)]
		[Category("Appearance")]
		public bool StatusIsLink
		{
			get
			{
				return this.status.IsLink;
			}
			set
			{
				this.status.LinkVisited = false;
				this.status.IsLink = value;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x0003BA51 File Offset: 0x00039C51
		// (set) Token: 0x06000F7F RID: 3967 RVA: 0x0003BA59 File Offset: 0x00039C59
		[Bindable(true)]
		[Category("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0003BA62 File Offset: 0x00039C62
		protected override void OnTextChanged(EventArgs e)
		{
			this.caption.Text = this.Text.Replace("&", "&&");
			base.OnTextChanged(e);
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x0003BA8B File Offset: 0x00039C8B
		// (set) Token: 0x06000F82 RID: 3970 RVA: 0x0003BAA2 File Offset: 0x00039CA2
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[RefreshProperties(RefreshProperties.All)]
		[Browsable(false)]
		public int ExpandedHeight
		{
			get
			{
				if (this.IsMinimized)
				{
					return this.expandedHeight;
				}
				return base.Height;
			}
			set
			{
				this.expandedHeight = value;
				if (!this.IsMinimized)
				{
					base.Height = value;
				}
			}
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0003BABA File Offset: 0x00039CBA
		private bool ShouldSerializeExpandedHeight()
		{
			return this.IsMinimized;
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x0003BAC2 File Offset: 0x00039CC2
		// (set) Token: 0x06000F85 RID: 3973 RVA: 0x0003BACA File Offset: 0x00039CCA
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(false)]
		[Category("Appearance")]
		public bool IsMinimized
		{
			get
			{
				return this.isMinimized;
			}
			set
			{
				if (!this.smoothSizing && this.IsMinimized != value)
				{
					if (value)
					{
						this.Collapse();
					}
					else
					{
						this.Expand();
					}
					this.isMinimized = value;
					this.OnIsMinimizedChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x0003BB00 File Offset: 0x00039D00
		internal void FastSetIsMinimized(bool collapse)
		{
			if (CollapsiblePanel.Animate)
			{
				CollapsiblePanel.Animate = false;
				try
				{
					this.IsMinimized = collapse;
					return;
				}
				finally
				{
					CollapsiblePanel.Animate = true;
				}
			}
			this.IsMinimized = collapse;
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0003BB44 File Offset: 0x00039D44
		private void Collapse()
		{
			this.oldAutoSize = this.AutoSize;
			this.AutoSize = false;
			this.expandedHeight = base.Height;
			this.chevron.Image = Icons.CollapseToExpand;
			this.SmoothHeightChange(this.captionStrip.Height);
			this.chevron.Image = Icons.Expand;
			this.captionStrip.BackgroundImage = null;
			this.BackColor = CollapsiblePanel.DefaultMinimizedBackColor;
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0003BBB8 File Offset: 0x00039DB8
		private void Expand()
		{
			this.captionStrip.BackgroundImage = this.captionBackground;
			this.chevron.Image = Icons.ExpandToCollapse;
			this.SmoothHeightChange(this.expandedHeight);
			this.chevron.Image = Icons.Collapse;
			this.AutoSize = this.oldAutoSize;
			if (CollapsiblePanel.Animate && base.Parent is ScrollableControl)
			{
				(base.Parent as ScrollableControl).ScrollControlIntoView(this);
			}
			this.BackColor = CollapsiblePanel.DefaultExpandedBackColor;
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0003BC40 File Offset: 0x00039E40
		protected virtual void OnIsMinimizedChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[CollapsiblePanel.EventIsMinimizedChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000057 RID: 87
		// (add) Token: 0x06000F8A RID: 3978 RVA: 0x0003BC6E File Offset: 0x00039E6E
		// (remove) Token: 0x06000F8B RID: 3979 RVA: 0x0003BC81 File Offset: 0x00039E81
		[Category("Appearance")]
		public event EventHandler IsMinimizedChanged
		{
			add
			{
				base.Events.AddHandler(CollapsiblePanel.EventIsMinimizedChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(CollapsiblePanel.EventIsMinimizedChanged, value);
			}
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0003BC94 File Offset: 0x00039E94
		private void SmoothHeightChange(int endHeight)
		{
			base.SuspendLayout();
			this.smoothSizing = true;
			try
			{
				bool flag = Theme.UseVisualEffects && CollapsiblePanel.Animate && base.IsHandleCreated && base.Parent != null && base.Parent.Controls.Count <= 100;
				if (flag)
				{
					int height = base.Height;
					int num = endHeight - height;
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					do
					{
						this.smoothSizeProgress = Math.Max(0f, Math.Min((float)stopwatch.ElapsedMilliseconds / 200f, 1f));
						base.Height = height + (int)((float)num * this.smoothSizeProgress);
						this.UpdateBackground();
						base.Parent.Update();
					}
					while ((float)stopwatch.ElapsedMilliseconds < 200f);
				}
				this.smoothSizeProgress = 1f;
				base.Height = endHeight;
				this.UpdateBackground();
			}
			finally
			{
				this.smoothSizeProgress = 0f;
				this.smoothSizing = false;
				base.ResumeLayout();
			}
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0003BD9C File Offset: 0x00039F9C
		protected override void Select(bool directed, bool forward)
		{
			if (this.IsMinimized)
			{
				base.ActiveControl = this.captionStrip;
				this.chevron.Select();
				return;
			}
			base.Select(directed, forward);
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0003BDC6 File Offset: 0x00039FC6
		protected override void OnLayout(LayoutEventArgs e)
		{
			this.captionStrip.SendToBack();
			base.OnLayout(e);
			this.HideOverflowButtonOfCaptionStrip();
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0003BDE0 File Offset: 0x00039FE0
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			if (!this.IsMinimized && !this.smoothSizing)
			{
				this.expandedHeight = base.Height;
			}
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0003BE08 File Offset: 0x0003A008
		private void UpdateBackground()
		{
			float num = (this.IsMinimized && this.smoothSizing) ? this.smoothSizeProgress : (1f - this.smoothSizeProgress);
			int opacityLevel = (int)(255f * num);
			this.captionBackground = CollapsiblePanel.BackgroundCache.GetImage(opacityLevel);
			int num2 = (int)(this.IsMinimized ? CollapsiblePanel.DefaultMinimizedBackColor.R : CollapsiblePanel.DefaultExpandedBackColor.R);
			int num3 = (int)(this.IsMinimized ? CollapsiblePanel.DefaultExpandedBackColor.R : CollapsiblePanel.DefaultMinimizedBackColor.R);
			int num4 = (int)(this.smoothSizeProgress * (float)num3 + (1f - this.smoothSizeProgress) * (float)num2);
			this.BackColor = Color.FromArgb(num4, num4, num4);
			this.captionStrip.BackgroundImage = this.captionBackground;
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0003BEE2 File Offset: 0x0003A0E2
		private void chevron_Click(object sender, EventArgs e)
		{
			this.IsMinimized = !this.IsMinimized;
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0003BEF3 File Offset: 0x0003A0F3
		private void captionStrip_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == ' ')
			{
				this.chevron_Click(sender, e);
				e.Handled = true;
			}
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0003BF10 File Offset: 0x0003A110
		private void captionStrip_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				ToolStripItem itemAt = this.captionStrip.GetItemAt(e.Location);
				if (itemAt != this.chevron && itemAt != this.status)
				{
					this.chevron.PerformClick();
				}
			}
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0003BF5C File Offset: 0x0003A15C
		private void status_Click(object sender, EventArgs e)
		{
			this.status.LinkVisited = true;
			CancelEventArgs cancelEventArgs = new CancelEventArgs();
			this.OnStatusClick(cancelEventArgs);
			if (!cancelEventArgs.Cancel)
			{
				this.chevron.PerformClick();
			}
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0003BF98 File Offset: 0x0003A198
		protected virtual void OnStatusClick(CancelEventArgs e)
		{
			CancelEventHandler cancelEventHandler = (CancelEventHandler)base.Events[CollapsiblePanel.EventStatusClick];
			if (cancelEventHandler != null)
			{
				cancelEventHandler(this, e);
			}
		}

		// Token: 0x14000058 RID: 88
		// (add) Token: 0x06000F96 RID: 3990 RVA: 0x0003BFC6 File Offset: 0x0003A1C6
		// (remove) Token: 0x06000F97 RID: 3991 RVA: 0x0003BFD9 File Offset: 0x0003A1D9
		[Category("Appearance")]
		public event CancelEventHandler StatusClick
		{
			add
			{
				base.Events.AddHandler(CollapsiblePanel.EventStatusClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(CollapsiblePanel.EventStatusClick, value);
			}
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0003BFEC File Offset: 0x0003A1EC
		private void InitializeComponent()
		{
			this.captionStrip = new TabbableToolStrip();
			this.image = new ToolStripLabel();
			this.caption = new ToolStripLabel();
			this.chevron = new ToolStripButton();
			this.status = new ToolStripLabel();
			this.captionStrip.SuspendLayout();
			base.SuspendLayout();
			this.captionStrip.BackColor = Color.Transparent;
			this.captionStrip.BackgroundImageLayout = ImageLayout.Stretch;
			this.captionStrip.GripStyle = ToolStripGripStyle.Hidden;
			this.captionStrip.Items.AddRange(new ToolStripItem[]
			{
				this.image,
				this.caption,
				this.chevron,
				this.status
			});
			this.captionStrip.Location = new Point(0, 0);
			this.captionStrip.Name = "captionStrip";
			this.captionStrip.Size = new Size(150, 25);
			this.captionStrip.Stretch = true;
			this.captionStrip.TabIndex = 0;
			this.captionStrip.TabStop = true;
			this.captionStrip.KeyPress += this.captionStrip_KeyPress;
			this.captionStrip.MouseClick += this.captionStrip_MouseClick;
			this.image.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.image.Name = "image";
			this.image.Overflow = ToolStripItemOverflow.Never;
			this.image.Size = new Size(0, 22);
			this.caption.AutoSize = false;
			this.caption.BackColor = Color.Transparent;
			this.caption.ImageAlign = ContentAlignment.MiddleLeft;
			this.caption.Name = "caption";
			this.caption.Size = new Size(4, 22);
			this.caption.TextAlign = ContentAlignment.MiddleLeft;
			this.chevron.Alignment = ToolStripItemAlignment.Right;
			this.chevron.DisplayStyle = ToolStripItemDisplayStyle.Image;
			this.chevron.ImageScaling = ToolStripItemImageScaling.None;
			this.chevron.Margin = new Padding(4, 1, 1, 2);
			this.chevron.Name = "chevron";
			this.chevron.Overflow = ToolStripItemOverflow.Never;
			this.chevron.Size = new Size(23, 22);
			this.chevron.Click += this.chevron_Click;
			this.status.ActiveLinkColor = Color.FromArgb(153, 153, 153);
			this.status.Alignment = ToolStripItemAlignment.Right;
			this.status.AutoSize = false;
			this.status.BackColor = Color.Transparent;
			this.status.ForeColor = Color.FromArgb(153, 153, 153);
			this.status.ImageAlign = ContentAlignment.MiddleLeft;
			this.status.LinkColor = Color.FromArgb(153, 153, 153);
			this.status.Name = "status";
			this.status.Overflow = ToolStripItemOverflow.Never;
			this.status.Size = new Size(0, 22);
			this.status.TextAlign = ContentAlignment.MiddleLeft;
			this.status.VisitedLinkColor = Color.FromArgb(153, 153, 153);
			this.status.Click += this.status_Click;
			base.Controls.Add(this.captionStrip);
			base.Name = "CollapsiblePanel";
			base.Size = new Size(150, 25);
			this.captionStrip.ResumeLayout(false);
			this.captionStrip.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0003C39C File Offset: 0x0003A59C
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			this.captionStrip.Padding = (LayoutHelper.IsRightToLeft(this) ? new Padding(0, 0, 5, 0) : new Padding(5, 0, 0, 0));
			this.caption.Padding = (LayoutHelper.IsRightToLeft(this) ? new Padding(0, 0, 4, 0) : new Padding(4, 0, 0, 0));
			base.OnRightToLeftChanged(e);
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0003C3FE File Offset: 0x0003A5FE
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessTabKey(bool forward)
		{
			if (this.IsMinimized && base.Parent != null)
			{
				return base.Parent.SelectNextControl(this, forward, true, false, false);
			}
			return base.ProcessTabKey(forward);
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0003C428 File Offset: 0x0003A628
		private void HideOverflowButtonOfCaptionStrip()
		{
			int num = 0;
			int num2 = this.CaptionStrip.DisplayRectangle.Width;
			foreach (object obj in this.CaptionStrip.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem != this.caption && toolStripItem.Placement == ToolStripItemPlacement.Main)
				{
					num += toolStripItem.Width + toolStripItem.Margin.Horizontal;
				}
			}
			num2 -= num;
			num2 -= this.caption.Margin.Horizontal;
			this.caption.Width = num2;
			if (this.CaptionStrip.OverflowButton.Visible)
			{
				this.CaptionStrip.PerformLayout();
			}
		}

		// Token: 0x0400060C RID: 1548
		private const string category = "Appearance";

		// Token: 0x0400060D RID: 1549
		private Bitmap captionBackground;

		// Token: 0x0400060E RID: 1550
		private ToolStripLabel image;

		// Token: 0x0400060F RID: 1551
		internal static readonly string AlignLayout = "AlignStatus";

		// Token: 0x04000610 RID: 1552
		private bool useDefaultExpandedBackColor = true;

		// Token: 0x04000611 RID: 1553
		public static readonly Color DefaultMinimizedBackColor = Color.FromArgb(238, 238, 238);

		// Token: 0x04000612 RID: 1554
		public static readonly Color DefaultExpandedBackColor = Color.FromArgb(247, 247, 247);

		// Token: 0x04000613 RID: 1555
		private static readonly Padding defaultMargin = new Padding(0, 0, 0, 1);

		// Token: 0x04000614 RID: 1556
		private static readonly Size defaultMinimumSize = new Size(0, 25);

		// Token: 0x04000615 RID: 1557
		private Icon icon;

		// Token: 0x04000616 RID: 1558
		private int expandedHeight;

		// Token: 0x04000617 RID: 1559
		private bool isMinimized;

		// Token: 0x04000618 RID: 1560
		private bool oldAutoSize;

		// Token: 0x04000619 RID: 1561
		private static readonly object EventIsMinimizedChanged = new object();

		// Token: 0x0400061A RID: 1562
		internal static bool Animate = true;

		// Token: 0x0400061B RID: 1563
		private bool smoothSizing;

		// Token: 0x0400061C RID: 1564
		private float smoothSizeProgress;

		// Token: 0x0400061D RID: 1565
		private static readonly object EventStatusClick = new object();

		// Token: 0x0400061E RID: 1566
		private TabbableToolStrip captionStrip;

		// Token: 0x0400061F RID: 1567
		private ToolStripLabel caption;

		// Token: 0x04000620 RID: 1568
		private ToolStripLabel status;

		// Token: 0x04000621 RID: 1569
		private ToolStripButton chevron;

		// Token: 0x0200018B RID: 395
		private static class BackgroundCache
		{
			// Token: 0x06000F9D RID: 3997 RVA: 0x0003C58C File Offset: 0x0003A78C
			static BackgroundCache()
			{
				Size size = new Size(1, 25);
				Rectangle rect = new Rectangle(Point.Empty, size);
				for (int i = 0; i < 16; i++)
				{
					CollapsiblePanel.BackgroundCache.bitmaps[i] = new Bitmap(size.Width, size.Height);
					using (Graphics graphics = Graphics.FromImage(CollapsiblePanel.BackgroundCache.bitmaps[i]))
					{
						int alpha = (i + 1) * 16 - 1;
						Color color = Color.FromArgb(alpha, 255, 255, 255);
						Color color2 = Color.FromArgb(alpha, 204, 204, 204);
						using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, color, color2, LinearGradientMode.Vertical))
						{
							graphics.FillRectangle(linearGradientBrush, rect);
						}
					}
				}
			}

			// Token: 0x06000F9E RID: 3998 RVA: 0x0003C67C File Offset: 0x0003A87C
			public static Bitmap GetImage(int opacityLevel)
			{
				return CollapsiblePanel.BackgroundCache.bitmaps[opacityLevel / 16];
			}

			// Token: 0x04000622 RID: 1570
			private const int imageCount = 16;

			// Token: 0x04000623 RID: 1571
			private const int opacityFactorsPerImage = 16;

			// Token: 0x04000624 RID: 1572
			private static Bitmap[] bitmaps = new Bitmap[16];
		}
	}
}
