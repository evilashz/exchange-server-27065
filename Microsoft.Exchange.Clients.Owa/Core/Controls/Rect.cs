using System;

namespace Microsoft.Exchange.Clients.Owa.Core.Controls
{
	// Token: 0x020002CF RID: 719
	public class Rect
	{
		// Token: 0x06001BEF RID: 7151 RVA: 0x000A0C61 File Offset: 0x0009EE61
		public Rect()
		{
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x000A0C6C File Offset: 0x0009EE6C
		public Rect(Rect source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.x = source.X;
			this.y = source.Y;
			this.width = source.Width;
			this.height = source.Height;
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x000A0CBD File Offset: 0x0009EEBD
		// (set) Token: 0x06001BF2 RID: 7154 RVA: 0x000A0CC5 File Offset: 0x0009EEC5
		public double X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06001BF3 RID: 7155 RVA: 0x000A0CCE File Offset: 0x0009EECE
		// (set) Token: 0x06001BF4 RID: 7156 RVA: 0x000A0CD6 File Offset: 0x0009EED6
		public double Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001BF5 RID: 7157 RVA: 0x000A0CDF File Offset: 0x0009EEDF
		// (set) Token: 0x06001BF6 RID: 7158 RVA: 0x000A0CE7 File Offset: 0x0009EEE7
		public double Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x000A0CF0 File Offset: 0x0009EEF0
		// (set) Token: 0x06001BF8 RID: 7160 RVA: 0x000A0CF8 File Offset: 0x0009EEF8
		public double Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x000A0D04 File Offset: 0x0009EF04
		public bool IntersectsY(Rect otherRect)
		{
			if (otherRect == null)
			{
				throw new ArgumentNullException("otherRect");
			}
			if (this.height == 0.0)
			{
				return this.y >= otherRect.y && this.y <= otherRect.y + otherRect.height;
			}
			if (otherRect.height == 0.0)
			{
				return otherRect.y >= this.y && otherRect.y <= this.y + this.height;
			}
			return this.y + this.height > otherRect.y && this.y < otherRect.y + otherRect.height;
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x000A0DC0 File Offset: 0x0009EFC0
		public void Add(Rect otherRect)
		{
			if (otherRect == null)
			{
				throw new ArgumentNullException("otherRect");
			}
			double val = this.y + this.height;
			double val2 = otherRect.y + otherRect.height;
			double num = Math.Max(val, val2);
			double val3 = this.x + this.width;
			double val4 = otherRect.x + otherRect.width;
			double num2 = Math.Max(val3, val4);
			this.x = Math.Min(this.x, otherRect.x);
			this.width = num2 - this.x;
			this.y = Math.Min(this.y, otherRect.y);
			this.height = num - this.y;
		}

		// Token: 0x040014A8 RID: 5288
		private double x;

		// Token: 0x040014A9 RID: 5289
		private double y;

		// Token: 0x040014AA RID: 5290
		private double width;

		// Token: 0x040014AB RID: 5291
		private double height;
	}
}
