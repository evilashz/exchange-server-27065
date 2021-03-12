using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x02000011 RID: 17
	[Designer(typeof(ScrollbarControlDesigner))]
	public sealed class CustomScrollbar : UserControl
	{
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060000E7 RID: 231 RVA: 0x000063A8 File Offset: 0x000045A8
		// (remove) Token: 0x060000E8 RID: 232 RVA: 0x000063E0 File Offset: 0x000045E0
		public new event EventHandler Scroll;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060000E9 RID: 233 RVA: 0x00006418 File Offset: 0x00004618
		// (remove) Token: 0x060000EA RID: 234 RVA: 0x00006450 File Offset: 0x00004650
		public event EventHandler ValueChanged;

		// Token: 0x060000EB RID: 235 RVA: 0x00006488 File Offset: 0x00004688
		public CustomScrollbar()
		{
			this.InitializeComponent();
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.DoubleBuffer, true);
			this.upArrowImage = new Bitmap(64, 64);
			this.downArrowImage = new Bitmap(64, 64);
			this.ChannelColor = Color.Transparent;
			this.ForeColor = Color.FromArgb(152, 163, 166);
			base.Width = this.upArrowImage.Width;
			this.ComputeMidProperties();
			this.MinimumSize = new Size(base.Width, 32 + this.thumbRectangle.Height);
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00006552 File Offset: 0x00004752
		// (set) Token: 0x060000ED RID: 237 RVA: 0x0000655A File Offset: 0x0000475A
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
				if (base.AutoSize)
				{
					base.Width = this.upArrowImage.Width;
				}
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000657C File Offset: 0x0000477C
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00006584 File Offset: 0x00004784
		[DefaultValue(false)]
		[Description("LargeChange")]
		[Category("Behavior")]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		public int LargeChange
		{
			get
			{
				return this.largeChange;
			}
			set
			{
				this.largeChange = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00006593 File Offset: 0x00004793
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x0000659B File Offset: 0x0000479B
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(false)]
		[Description("SmallChange")]
		[Browsable(true)]
		[Category("Behavior")]
		public int SmallChange
		{
			get
			{
				return this.smallChange;
			}
			set
			{
				this.smallChange = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000065AA File Offset: 0x000047AA
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x000065B2 File Offset: 0x000047B2
		[Description("Minimum")]
		[Category("Behavior")]
		[DefaultValue(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		public int Minimum
		{
			get
			{
				return this.minimum;
			}
			set
			{
				this.minimum = value;
				base.Invalidate();
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x000065C1 File Offset: 0x000047C1
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x000065C9 File Offset: 0x000047C9
		[Category("Behavior")]
		[Description("Maximum")]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(true)]
		[DefaultValue(false)]
		public int Maximum
		{
			get
			{
				return this.maximum;
			}
			set
			{
				this.maximum = value;
				base.Invalidate();
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000065D8 File Offset: 0x000047D8
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x000065E0 File Offset: 0x000047E0
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("Value")]
		[Browsable(true)]
		public int Value
		{
			get
			{
				return this.scrollValue;
			}
			set
			{
				this.scrollValue = value;
				int num = Math.Abs(this.Maximum - this.LargeChange);
				if (this.scrollValue > num)
				{
					this.scrollValue = num;
				}
				else if (this.scrollValue < this.minimum)
				{
					this.scrollValue = this.minimum;
				}
				int num2 = this.trackHeight - this.thumbRectangle.Height;
				int num3 = num - this.Minimum;
				float num4 = 0f;
				if (num3 != 0)
				{
					num4 = (float)this.scrollValue / (float)num3;
				}
				float num5 = num4 * (float)num2;
				this.thumbRectangle.Y = (int)num5 + 16 + 2;
				base.Invalidate();
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00006682 File Offset: 0x00004882
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x0000668A File Offset: 0x0000488A
		[DefaultValue(false)]
		[Category("Skin")]
		[Description("Channel Color")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public Color ChannelColor
		{
			get
			{
				return this.channelColor;
			}
			set
			{
				this.channelColor = value;
				this.channelBrush = new SolidBrush(this.channelColor);
			}
		}

		// Token: 0x17000032 RID: 50
		// (set) Token: 0x060000FA RID: 250 RVA: 0x000066A4 File Offset: 0x000048A4
		[Browsable(true)]
		[Category("Skin")]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(false)]
		[Description("Foreground Color")]
		public override Color ForeColor
		{
			set
			{
				base.ForeColor = value;
				this.foreBrush = new SolidBrush(value);
				this.GenerateImages();
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000066C0 File Offset: 0x000048C0
		public void InitializeScrollbar(IntPtr handle, int height, int autoScrollOffsetY)
		{
			CustomScrollbar.SCROLLINFO scrollinfo = default(CustomScrollbar.SCROLLINFO);
			scrollinfo.Size = (uint)Marshal.SizeOf(scrollinfo);
			scrollinfo.Mask = 23U;
			CustomScrollbar.GetScrollInfo(handle, 1, ref scrollinfo);
			this.Minimum = 0;
			this.Maximum = scrollinfo.Max;
			this.LargeChange = this.Maximum / base.Height + height;
			this.SmallChange = 15;
			this.Value = Math.Abs(autoScrollOffsetY);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00006738 File Offset: 0x00004938
		public void CustomScroll(IntPtr handle, int units)
		{
			IntPtr lParam = new IntPtr(0);
			CustomScrollbar.SCROLLINFO scrollinfo = default(CustomScrollbar.SCROLLINFO);
			scrollinfo.Size = (uint)Marshal.SizeOf(scrollinfo);
			scrollinfo.Mask = 23U;
			CustomScrollbar.GetScrollInfo(handle, 1, ref scrollinfo);
			IntPtr wParam;
			if (units < scrollinfo.Max)
			{
				scrollinfo.Pos = units;
			}
			else
			{
				wParam = new IntPtr(8);
				CustomScrollbar.SendMessage(handle, 277U, wParam, lParam);
			}
			CustomScrollbar.SetScrollInfo(handle, 1, ref scrollinfo, true);
			wParam = new IntPtr(5 + 65536 * scrollinfo.Pos);
			CustomScrollbar.SendMessage(handle, 277U, wParam, lParam);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000067D4 File Offset: 0x000049D4
		internal void MoveScrollbar(IntPtr handle, int keyCode)
		{
			CustomScrollbar.SCROLLINFO scrollinfo = default(CustomScrollbar.SCROLLINFO);
			scrollinfo.Size = (uint)Marshal.SizeOf(scrollinfo);
			scrollinfo.Mask = 23U;
			CustomScrollbar.GetScrollInfo(handle, 1, ref scrollinfo);
			IntPtr wParam = new IntPtr(5 + 65536 * scrollinfo.Pos);
			switch (keyCode)
			{
			case 0:
				scrollinfo.Pos--;
				wParam = new IntPtr(0);
				break;
			case 1:
				scrollinfo.Pos++;
				wParam = new IntPtr(1);
				break;
			case 2:
				scrollinfo.Pos -= Convert.ToInt32(scrollinfo.Page);
				wParam = new IntPtr(2);
				break;
			case 3:
				scrollinfo.Pos += Convert.ToInt32(scrollinfo.Page);
				wParam = new IntPtr(3);
				break;
			}
			if (scrollinfo.Pos > scrollinfo.Max)
			{
				scrollinfo.Pos = scrollinfo.Max;
			}
			scrollinfo.Mask = 4U;
			CustomScrollbar.SetScrollInfo(handle, 1, ref scrollinfo, true);
			IntPtr lParam = new IntPtr(0);
			CustomScrollbar.SendMessage(handle, 277U, wParam, lParam);
			CustomScrollbar.GetScrollInfo(handle, 1, ref scrollinfo);
			this.Value = scrollinfo.Pos;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00006914 File Offset: 0x00004B14
		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.FillRectangle(this.channelBrush, new Rectangle(1, 16, base.Width - 2, base.Height - 16));
			e.Graphics.FillRectangle(this.foreBrush, this.thumbRectangle);
			InterpolationMode interpolationMode = e.Graphics.InterpolationMode;
			e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
			e.Graphics.DrawImage(this.upArrowImage, new Rectangle(new Point(0, 0), new Size(base.Width, 16)));
			e.Graphics.DrawImage(this.downArrowImage, new Rectangle(new Point(0, base.Height - 16 - 3), new Size(base.Width, 16)));
			e.Graphics.InterpolationMode = interpolationMode;
		}

		// Token: 0x060000FF RID: 255
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetScrollInfo(IntPtr hwnd, int fnBar, ref CustomScrollbar.SCROLLINFO lpsi);

		// Token: 0x06000100 RID: 256
		[DllImport("user32.dll")]
		private static extern int SetScrollInfo(IntPtr hwnd, int fnBar, [In] ref CustomScrollbar.SCROLLINFO lpsi, bool fRedraw);

		// Token: 0x06000101 RID: 257
		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x06000102 RID: 258 RVA: 0x000069E4 File Offset: 0x00004BE4
		private void CustomScrollbarMouseDown(object sender, MouseEventArgs e)
		{
			this.clickPoint = e.Location;
			if (this.thumbRectangle.Contains(this.clickPoint))
			{
				this.mouseScrollDown = true;
				this.mouseScrollDraggingLastY = e.Y;
				return;
			}
			Rectangle rectangle = new Rectangle(new Point(1, 16), new Size(base.Width, this.trackHeight));
			if (rectangle.Contains(this.clickPoint))
			{
				int y = this.thumbRectangle.Y;
				int num = (this.clickPoint.Y < y) ? (-this.LargeChange) : this.LargeChange;
				this.Value += num;
			}
			Rectangle rectangle2 = new Rectangle(new Point(1, 0), new Size(base.Width, 16));
			if (rectangle2.Contains(this.clickPoint))
			{
				this.Value -= this.SmallChange;
			}
			Rectangle rectangle3 = new Rectangle(new Point(1, 16 + this.trackHeight), new Size(base.Width, 16));
			if (rectangle3.Contains(this.clickPoint))
			{
				this.Value += this.SmallChange;
			}
			if (this.ValueChanged != null)
			{
				this.ValueChanged(this, new EventArgs());
			}
			if (this.Scroll != null)
			{
				this.Scroll(this, new EventArgs());
			}
			base.Invalidate();
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00006B45 File Offset: 0x00004D45
		private void CustomScrollbarMouseUp(object sender, MouseEventArgs e)
		{
			this.mouseScrollDown = false;
			this.mouseScrollDragging = false;
			this.mouseScrollDraggingLastY = -1;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00006B5C File Offset: 0x00004D5C
		private void MoveThumb(int y)
		{
			int num = this.trackHeight - this.thumbRectangle.Height;
			if (this.mouseScrollDown && num > 0)
			{
				int num2 = y - this.mouseScrollDraggingLastY;
				this.mouseScrollDraggingLastY = y;
				int num3 = (int)((float)(this.Maximum - this.Minimum) * ((float)num2 / (float)num) + 0.5f);
				this.Value += num3;
				Application.DoEvents();
				base.Invalidate();
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00006BD0 File Offset: 0x00004DD0
		private void CustomScrollbarMouseMove(object sender, MouseEventArgs e)
		{
			if (this.mouseScrollDown)
			{
				this.mouseScrollDragging = true;
			}
			if (this.mouseScrollDragging)
			{
				this.MoveThumb(e.Y);
				if (this.ValueChanged != null)
				{
					this.ValueChanged(this, new EventArgs());
				}
				if (this.Scroll != null)
				{
					this.Scroll(this, new EventArgs());
				}
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00006C32 File Offset: 0x00004E32
		private void OnResize(object sender, EventArgs e)
		{
			this.ComputeMidProperties();
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00006C3C File Offset: 0x00004E3C
		private void ComputeMidProperties()
		{
			this.trackHeight = base.Height - 36;
			int num = (int)((float)this.LargeChange / (float)this.Maximum) * this.trackHeight;
			if (num > this.trackHeight)
			{
				num = this.trackHeight;
			}
			if (num < 16)
			{
				num = 16;
			}
			this.thumbRectangle = new Rectangle(1, 18, base.Width - 2, num);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00006CA0 File Offset: 0x00004EA0
		private void GenerateImages()
		{
			Rectangle rect = new Rectangle(0, 0, 64, 64);
			using (Graphics graphics = Graphics.FromImage(this.upArrowImage))
			{
				graphics.FillRectangle(Brushes.Transparent, rect);
				graphics.FillPolygon(this.foreBrush, new Point[]
				{
					new Point(31, 17),
					new Point(1, 63),
					new Point(62, 63)
				});
			}
			using (Graphics graphics2 = Graphics.FromImage(this.downArrowImage))
			{
				graphics2.FillRectangle(Brushes.Transparent, rect);
				graphics2.FillPolygon(this.foreBrush, new Point[]
				{
					new Point(31, 47),
					new Point(1, 1),
					new Point(62, 1)
				});
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00006DC8 File Offset: 0x00004FC8
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006DE8 File Offset: 0x00004FE8
		private void InitializeComponent()
		{
			this.components = new Container();
			new ComponentResourceManager(typeof(CustomScrollbar));
			base.SuspendLayout();
			base.Name = "CustomScrollbar";
			base.MouseDown += this.CustomScrollbarMouseDown;
			base.MouseMove += this.CustomScrollbarMouseMove;
			base.MouseUp += this.CustomScrollbarMouseUp;
			base.Resize += this.OnResize;
			base.ResumeLayout(false);
		}

		// Token: 0x04000068 RID: 104
		private const int WMVSCROLL = 277;

		// Token: 0x04000069 RID: 105
		private const int SBLINEDOWN = 1;

		// Token: 0x0400006A RID: 106
		private const int SBLINEUP = 0;

		// Token: 0x0400006B RID: 107
		private const int SBPAGEDOWN = 3;

		// Token: 0x0400006C RID: 108
		private const int SBPAGEUP = 2;

		// Token: 0x0400006D RID: 109
		private const int SBTHUMBTRACK = 5;

		// Token: 0x0400006E RID: 110
		private const int SBENDSCROLL = 8;

		// Token: 0x0400006F RID: 111
		private const int MinImageHeight = 16;

		// Token: 0x04000070 RID: 112
		private const int UpDownArrowHeight = 16;

		// Token: 0x04000071 RID: 113
		private readonly Image upArrowImage;

		// Token: 0x04000072 RID: 114
		private readonly Image downArrowImage;

		// Token: 0x04000073 RID: 115
		private Point clickPoint;

		// Token: 0x04000074 RID: 116
		private bool mouseScrollDown;

		// Token: 0x04000075 RID: 117
		private bool mouseScrollDragging;

		// Token: 0x04000076 RID: 118
		private int mouseScrollDraggingLastY;

		// Token: 0x04000077 RID: 119
		private int largeChange = 10;

		// Token: 0x04000078 RID: 120
		private int smallChange = 1;

		// Token: 0x04000079 RID: 121
		private int minimum;

		// Token: 0x0400007A RID: 122
		private int maximum = 100;

		// Token: 0x0400007B RID: 123
		private int scrollValue;

		// Token: 0x0400007C RID: 124
		private Rectangle thumbRectangle;

		// Token: 0x0400007D RID: 125
		private int trackHeight;

		// Token: 0x0400007E RID: 126
		private Color channelColor;

		// Token: 0x0400007F RID: 127
		private Brush channelBrush;

		// Token: 0x04000080 RID: 128
		private Brush foreBrush;

		// Token: 0x04000083 RID: 131
		private IContainer components;

		// Token: 0x02000012 RID: 18
		private enum ScrollBarDirection
		{
			// Token: 0x04000085 RID: 133
			SB_HORZ,
			// Token: 0x04000086 RID: 134
			SB_VERT,
			// Token: 0x04000087 RID: 135
			SB_CTL,
			// Token: 0x04000088 RID: 136
			SB_BOTH
		}

		// Token: 0x02000013 RID: 19
		private enum ScrollInfoMask
		{
			// Token: 0x0400008A RID: 138
			SIF_RANGE = 1,
			// Token: 0x0400008B RID: 139
			SIF_PAGE,
			// Token: 0x0400008C RID: 140
			SIF_POS = 4,
			// Token: 0x0400008D RID: 141
			SIF_DISABLENOSCROLL = 8,
			// Token: 0x0400008E RID: 142
			SIF_TRACKPOS = 16,
			// Token: 0x0400008F RID: 143
			SIF_ALL = 23
		}

		// Token: 0x02000014 RID: 20
		private struct SCROLLINFO
		{
			// Token: 0x04000090 RID: 144
			public uint Size;

			// Token: 0x04000091 RID: 145
			public uint Mask;

			// Token: 0x04000092 RID: 146
			public int Min;

			// Token: 0x04000093 RID: 147
			public int Max;

			// Token: 0x04000094 RID: 148
			public uint Page;

			// Token: 0x04000095 RID: 149
			public int Pos;

			// Token: 0x04000096 RID: 150
			public int TrackPos;
		}
	}
}
