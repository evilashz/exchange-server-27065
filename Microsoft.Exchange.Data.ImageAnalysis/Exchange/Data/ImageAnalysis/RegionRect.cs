using System;

namespace Microsoft.Exchange.Data.ImageAnalysis
{
	// Token: 0x02000005 RID: 5
	public class RegionRect
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020D0 File Offset: 0x000002D0
		public RegionRect()
		{
			this.Left = 0;
			this.Top = 0;
			this.Right = 0;
			this.Bottom = 0;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020F4 File Offset: 0x000002F4
		public RegionRect(int left, int top, int right, int bottom)
		{
			this.Left = left;
			this.Top = top;
			this.Right = right;
			this.Bottom = bottom;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002119 File Offset: 0x00000319
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002121 File Offset: 0x00000321
		public int Left { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000212A File Offset: 0x0000032A
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002132 File Offset: 0x00000332
		public int Top { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000213B File Offset: 0x0000033B
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002143 File Offset: 0x00000343
		public int Right { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000214C File Offset: 0x0000034C
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002154 File Offset: 0x00000354
		public int Bottom { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000215D File Offset: 0x0000035D
		public int Height
		{
			get
			{
				return this.Bottom - this.Top;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000216C File Offset: 0x0000036C
		public int Width
		{
			get
			{
				return this.Right - this.Left;
			}
		}
	}
}
