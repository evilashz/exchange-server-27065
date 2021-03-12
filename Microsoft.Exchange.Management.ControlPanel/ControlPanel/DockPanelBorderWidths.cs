using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020005AF RID: 1455
	internal class DockPanelBorderWidths
	{
		// Token: 0x0600428B RID: 17035 RVA: 0x000CAA7C File Offset: 0x000C8C7C
		public DockPanelBorderWidths(string widthString)
		{
			if (!string.IsNullOrEmpty(widthString))
			{
				string[] array = widthString.Split(new char[]
				{
					' '
				});
				int num = array.Length;
				try
				{
					this.Top = ((num > 0) ? int.Parse(array[0]) : 0);
					this.Right = ((num > 1) ? int.Parse(array[1]) : this.Top);
					this.Bottom = ((num > 2) ? int.Parse(array[2]) : this.Top);
					this.Left = ((num > 3) ? int.Parse(array[3]) : this.Right);
				}
				catch (FormatException ex)
				{
					throw new ArgumentException("Invalid border width value: " + ex.Source, ex);
				}
			}
		}

		// Token: 0x170025D1 RID: 9681
		// (get) Token: 0x0600428C RID: 17036 RVA: 0x000CAB40 File Offset: 0x000C8D40
		// (set) Token: 0x0600428D RID: 17037 RVA: 0x000CAB48 File Offset: 0x000C8D48
		public int Top { get; set; }

		// Token: 0x170025D2 RID: 9682
		// (get) Token: 0x0600428E RID: 17038 RVA: 0x000CAB51 File Offset: 0x000C8D51
		// (set) Token: 0x0600428F RID: 17039 RVA: 0x000CAB59 File Offset: 0x000C8D59
		public int Left { get; set; }

		// Token: 0x170025D3 RID: 9683
		// (get) Token: 0x06004290 RID: 17040 RVA: 0x000CAB62 File Offset: 0x000C8D62
		// (set) Token: 0x06004291 RID: 17041 RVA: 0x000CAB6A File Offset: 0x000C8D6A
		public int Right { get; set; }

		// Token: 0x170025D4 RID: 9684
		// (get) Token: 0x06004292 RID: 17042 RVA: 0x000CAB73 File Offset: 0x000C8D73
		// (set) Token: 0x06004293 RID: 17043 RVA: 0x000CAB7B File Offset: 0x000C8D7B
		public int Bottom { get; set; }

		// Token: 0x170025D5 RID: 9685
		// (get) Token: 0x06004294 RID: 17044 RVA: 0x000CAB84 File Offset: 0x000C8D84
		public int HorizontalWidth
		{
			get
			{
				return this.Left + this.Right;
			}
		}

		// Token: 0x170025D6 RID: 9686
		// (get) Token: 0x06004295 RID: 17045 RVA: 0x000CAB93 File Offset: 0x000C8D93
		public int VerticalWidth
		{
			get
			{
				return this.Top + this.Bottom;
			}
		}

		// Token: 0x06004296 RID: 17046 RVA: 0x000CABA4 File Offset: 0x000C8DA4
		public string FormatCssString(bool isRtl)
		{
			string format = isRtl ? "{0}px {3}px {2}px {1}px" : "{0}px {1}px {2}px {3}px";
			return string.Format(format, new object[]
			{
				this.Top,
				this.Right,
				this.Bottom,
				this.Left
			});
		}
	}
}
